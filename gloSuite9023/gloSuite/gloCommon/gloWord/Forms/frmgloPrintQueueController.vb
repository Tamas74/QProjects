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
''Imports gloPrintDialog
Imports System.Data.SqlClient
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Runtime.InteropServices
Imports gloWord
Imports gloPrintDialog
Public Class frmgloPrintQueueController
    '<DllImport("winspool.drv", CharSet:=CharSet.Auto, SetLastError:=True)> _
    'Public Shared Function SetDefaultPrinter(Name As String) As Boolean
    'End Function
    ' private int _nPagesCnt = 0;
    ' private int _printPageIndex = 0;
    ' private bool _IsOnPagePDFDocCreated = false;
    '   Private _gloStandardPrintController As gloStandardPrintController = Nothing
    ' private Microsoft.Office.Interop.Word.Application oWordApp;
    ' private String _PDFFileName = null;
    ' Private _oPrintDocument As PrintDocument = Nothing
    Private _PrinterSetting As PrinterSettings = Nothing

    Private _ExtendedPrinterSettings As gloExtendedPrinterSettings = Nothing
    Public IsPrintingResumed As Boolean = False

    Public IsPrintingCanceled As Boolean = False
    Private IsPrintingCompletedWhilePressingPause As Boolean = True
    'private ArrayList _objwordList = null;
    Public dtSelectPatient As DataTable = Nothing
    Public dictPatientLetter As Dictionary(Of Int64, String)
    'bool blntaskidPres = false;
    Public ChkRemiderForUnSchedle As Boolean = False
    Public blnBackGroundPrint As Boolean = False
    'private Microsoft.Office.Interop.Word.Document oCurDoc;
    Private PrintDocument As Integer = 0
    'private Int64 _PatientID = 0;
    'private string _PatientName = "";
    'private Int64 _TemplateID = 0;
    ' private string _TemplateName = "";
    ' private Int64 _VisitID = 0;
    ' private Int64 _TaskID = 0;
    Public _bIsUnscheduledCare As Boolean = False
    '  private Int64 _nCommunicationTypeID = 0;
    Private clickTime As DateTime = DateTime.Now
    Private blnbtnPauseclicked As Boolean = False
    ' public long AccountID=0;
    Private _isFormClose As Boolean = False
    Public oldPrinterName As String = ""
    '  public List<gloOffice.gloTemplate> lstgloTemplate = null;
    Public bIsBatchGenerate As Boolean = False

    Public _databaseConnectionString As String = ""
    Private myCaller As Control = Nothing
    Public lstgloTemplate As New List(Of clsPrintWordQueue)()
    Private _blsisprinted As Boolean = False
    Public gblnPageNo As Boolean = True
    Public strpatname As String = String.Empty
    Public lnPatientId As String = 0
    Private _popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing

    Public Sub New(sPrinterSettings As PrinterSettings, sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional oSourceDocSelectedPages As ArrayList = Nothing, Optional myControl As Control = Nothing)

        AddHandler Me.Shown, AddressOf frmgloPrintQueueController_Shown
        AddHandler Me.FormClosed, AddressOf frmgloPrintQueueController_FormClosed
        myCaller = myControl

        gloPrintWordProgressControllerCall(sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages)
    End Sub

    Private Sub gloPrintWordProgressControllerCall(sPrinterSettings As PrinterSettings, sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional oSourceDocSelectedPages As ArrayList = Nothing)
        Try

            InitializeComponent()
            If (gloGlobal.gloTSPrint.isCopyPrint) Then
                Me.Text = "Copying"
            Else
                Me.Text = "Printing"
            End If

            '' Me.ControlBox = False
            Me.BringToFront()
            _PrinterSetting = sPrinterSettings
            _ExtendedPrinterSettings = sExtendedPrinterSettings
            If gloGlobal.gloTSPrint.isCopyPrint AndAlso (Not gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False)) Then
                _ExtendedPrinterSettings.IsBackGroundPrint = True
                _ExtendedPrinterSettings.IsShowProgress = False
            End If

            If gloGlobal.gloTSPrint.isCopyPrint Then
                lblPrinterName.Text = "Copying To :"
                lblPrinterNameValue.Text = " Mapped drive for printing"
            Else
                lblPrinterName.Text = "Printing To :"
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try

    End Sub

    Const CP_NOCLOSE_BUTTON As Integer = 512
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property

    Private Sub btnRestart_Click(sender As System.Object, e As System.EventArgs) Handles btnRestart.Click
        Try
            IsPrintingResumed = False
            IsPrintingCanceled = False
            '  _printPageIndex = 0;
            PrintDocument = 0

            btnRestart.Enabled = False
            'If btnPause.Text = "&Resume" Then
            '    btnPause.Text = "&Pause"
            '    IsPrintingResumed = False
            'End If

            btnPlay.Visible = False
            btnPause.Visible = True
            If (btnPause.Visible = True) Then
                IsPrintingResumed = False
            End If

            DirectCast(sender, Button).Enabled = False
            '     If _gloStandardPrintController IsNot Nothing Then
            '_gloStandardPrintController.IsRestart = True
            '  End If
            'DirectCast(sender, Button).Update()
            'btnPause.Update()
            'DirectCast(sender, Button).ResumeLayout()
            'btnPause.ResumeLayout()
            Try
                pbDocument.Value = PrintDocument
            Catch
            End Try
            PrintWithOrWithoutBackground()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnPause_Click(sender As System.Object, e As System.EventArgs) Handles btnPause.Click
        Try


            btnPause.Visible = False
            btnPlay.Visible = True
            IsPrintingResumed = True
            btnRestart.Update()

            btnRestart.ResumeLayout()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub
    Private Delegate Sub del()
    Private methodToInvoke As EventHandler
    Public Sub PrintWithOrWithoutBackground()
        Try
            If _blsisprinted = False Then
                _blsisprinted = True
                If _ExtendedPrinterSettings.IsShowProgress Then
                    Dim retry As Integer = 20
                    While Not IsPrintingCompletedWhilePressingPause
                        InvokeBackgroundUpdateControls()
                        Thread.Sleep(100)
                        If System.Math.Max(System.Threading.Interlocked.Decrement(retry), retry + 1) = 0 Then
                            'Exhaused waiting for 2 seconds
                            ' TODO: might not be correct. Was : Exit While
                            Exit While



                        End If
                    End While
                    IsPrintingCompletedWhilePressingPause = False
                End If

                '' Show priinter selection popup for gloTS print
                Dim res As Boolean = True
                _popUpDetails = LoadAndCloseWord.getTSPrintDialogDetails(res)
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
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub
    Private Sub OnPrint(sender As Object, e As System.EventArgs)

        Try


            Print()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub
    Private Sub Print()
        Dim fromDoc As Int64 = 0
        'bool blnBuildBlockSetting = false;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"));
        'Dim Copies As Short = _PrinterSetting.Copies
        'Int64 _TransactionID = 0;
        ''
        '   gloOffice.gloTemplate _gloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        Try
            'if (myLoadWord == null)
            '{
            '    myLoadWord = new gloWord.LoadAndCloseWord();
            '}
            'Dim missing_new As Object = Type.Missing
            'Dim saveOptions As Object = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges
            'for (int iCount = 0; iCount < lstgloTemplate.Count; iCount++)
            '{
            '    _ntempTemplateID = Convert.ToInt64(lstgloTemplate[iCount]);
            '    try
            '    {
            '       // PrintTemplate(_ntempTemplateID, ref myLoadWord);
            '    }
            '    catch (Exception ex1)
            '    {
            '       // MessageBox.Show("Error while Printing.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
            '    }
            '    _ntempTemplateID = 0;


            '}



            Try
                ' Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
                'wordApplication = new Microsoft.Office.Interop.Word.Application();



                Try
                    'if (bIsBatchGenerate)
                    '{


                    For i As Integer = 0 To 0
                        If fromDoc < PrintDocument Then
                            fromDoc += 1
                        Else
                            If _isFormClose = True Then
                                Return
                            End If
                            '  string BatchPatientLog = String.Format("TemplateFilePath {0} : TemplateName {1} : TemplateID {2} : PatientName {3} : PatientID {4} : i {5} : lstgloTemplate.Count {6} : UserID  {7} : UserName {8} : LoginProviderID {9}",
                            'lstgloTemplate[i].TemplateFilePath, lstgloTemplate[i].TemplateName, lstgloTemplate[i].TemplateID, lstgloTemplate[i].PatientName, lstgloTemplate[i].PatientID, i, lstgloTemplate.Count, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID);
                            '  gloAuditTrail.gloAuditTrail.PrintLog(" Before Print CALL : " + BatchPatientLog);
                            Dim objcls As clsPrintWordQueue = lstgloTemplate(i)

                            Print(lstgloTemplate(i).FilePath.ToString())
                            lstgloTemplate.RemoveAt(0)



                            '  prgFileGeneration.Value = i + 1;
                            '  lblFile.Text = "Printing File " + prgFileGeneration.Value + "/" + lstgloTemplate.Count;
                            '   this.Invalidate();
                            ' this.Refresh();
                            PrintDocument += 1
                            fromDoc = PrintDocument
                            InvokeProgressUpdateControls()
                            If blnbtnPauseclicked Then
                                InvokeEnableDisablePauseButton()
                            End If

                            If IsPrintingResumed Then
                                Exit For
                            End If
                        End If
                    Next


                Catch
                    ' MessageBox.Show(ex1.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    '(Exception ex1)
                    'if (myLoadWord != null)
                    '{
                    '    myLoadWord.CloseApplicationOnly();
                    '}
                    'myLoadWord = null;


                End Try

                '  MessageBox.Show(ext.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ' this.Close();
            Catch generatedExceptionName As System.Reflection.TargetInvocationException















            End Try
            ' ex = null;
        Catch generatedExceptionName As Exception
        Finally
            'if (_gloTemplate != null)
            '{
            '    _gloTemplate.Dispose();
            '    _gloTemplate = null;
            '}
            If (blnBackGroundPrint = False) And _isFormClose = False Then
                _isFormClose = True
                Me.Close()
                Me.Dispose(True)
            End If
            If lstgloTemplate IsNot Nothing Then
                lstgloTemplate.Clear()
            End If
            lstgloTemplate = Nothing
        End Try
    End Sub

    Private myLoadWord As LoadAndCloseWord = Nothing
    Public Sub Print(filePath As String)
        '  Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
        ' bool toQuit = false;
        'if (myLoadWord == null)
        '{
        '    myLoadWord = new gloWord.LoadAndCloseWord();
        '    // toQuit = true;
        '}
        If myLoadWord Is Nothing Then
            ' toQuit = true;
            myLoadWord = New LoadAndCloseWord()
        End If
        Dim Background As Object = False
        ' Dim Range As Object = Wd.WdPrintOutRange.wdPrintAllDocument
        Dim Copies As Object = If(gloGlobal.gloTSPrint.isCopyPrint, 1, _PrinterSetting.Copies)
        Dim PageType As Object = Wd.WdPrintOutPages.wdPrintAllPages
        Dim PrintToFile As Object = False
        Dim Collate As Object = If(gloGlobal.gloTSPrint.isCopyPrint, True, _PrinterSetting.Collate)
        Dim ActivePrinterMacGX As Object = Type.Missing
        Dim ManualDuplexPrint As Object = False
        Dim PrintZoomColumn As Object = 1
        Dim PrintZoomRow As Object = 1
        Dim missing As Object = Type.Missing
        'object missing_new = Type.Missing;
        'object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        'object templatename = gloSettings.FolderSettings.AppTempFolderPath;
        '   gloOffice.gloTemplate _gloTemplate = null;
        'foreach (gloOffice.gloTemplate template in gloTemplates)
        '{
        Try
            'gloOffice.Supporting.DataBaseConnectionString = databasestring;
            'gloOffice.Supporting.PatientID = template.PatientID;

            'gloOffice.Supporting.PrimaryID = template.TemplateID;
            'AccountID = template.nPAccountID;

            'gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            'gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

            'Create New Document in Word
            ' object missing = System.Reflection.Missing.Value;
            'object fileName = gloOffice.Supporting.GenerateDocumentFile();
            ' String strFileName = gloOffice.Supporting.NewDocumentName();
            'try
            '{
            '    System.IO.File.Copy(template.TemplateFilePath, strFileName.ToString());
            '}
            'catch (Exception ex)
            '{
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            '}


            Dim newTemplate As Object = False
            Dim docType As Object = 0
            Dim isVisible As Object = True

            ' Create a new Document, by calling the Add function in the Documents collection
            Dim aDoc As Microsoft.Office.Interop.Word.Document = myLoadWord.LoadWordApplication(filePath, False, False)
            'wdApplication.Documents.Open(filePath); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
            'gloOffice.Supporting.PrimaryID = template.PrimeryID;
            'gloOffice.Supporting.WdApplication = aDoc.Application;
            'gloOffice.Supporting.CurrentDocument = aDoc;

            'System.Windows.Forms.Application.DoEvents();
            'gloOffice.Supporting.isFromBatchPrint = true;
            'gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
            'gloOffice.Supporting.isFromBatchPrint = false;
            
            'gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
          
            ' need to see the created document, so make it visible
            'wordApplication.Visible = true;
            'aDoc.Activate();
            'object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
            'object oFileName = (object)template.TemplateFilePath;
            'aDoc.SaveAs(oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
            '   gloAuditTrail.gloAuditTrail.PrintLog([String].Format("Sent Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID))
            If aDoc IsNot Nothing Then
                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                    '' If oldPrinterName <> _PrinterSetting.PrinterName Then
                    ''gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName)
                    ''Application.DoEvents()
                    ''End If
                    _PrinterSetting.PrinterName = lblPrinterNameValue.Text
                End If

                'gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                '        ref missing, ref missing, ref missing, ref Copies,
                '        ref missing, ref missing, ref PrintToFile, ref Collate,
                '        ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                '        ref PrintZoomRow, ref missing, ref missing);
                'Dim topage As Object = _PrinterSetting.ToPage
                'Dim from As Object = _PrinterSetting.FromPage
                LoadAndCloseWord.CleanupDoc(aDoc)
                Dim printBookMark As String = ""
                If gblnPageNo = True Then
                    '    UpdateLog("InsertNamePageNo start ")
                    Dim Miss As Object = System.Reflection.Missing.Value
                    Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
                    Dim CheckToPageno = 0
                    If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                        printBookMark = LoadAndCloseWord.AssignPrinterBookMarks(aDoc, _PrinterSetting.FromPage, _PrinterSetting.ToPage)
                    Else
                        If (IsNothing(_popUpDetails) = False) Then
                            printBookMark = LoadAndCloseWord.AssignPrinterBookMarks(aDoc, _popUpDetails.PrintFrom, _popUpDetails.PrintTo)
                        End If
                    End If
                    If (String.IsNullOrEmpty(printBookMark)) Then
                        CheckToPageno = aDoc.ComputeStatistics(PageCountStat, Miss) ''added for bugid 96435
                    End If

                    Dim ToPageno As Int32 = 0
                    If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                        InsertNamePageNo(aDoc, strpatname)
                    Else
                        If gloGlobal.gloPMGlobal.MessageBoxCaption.ToString() = "gloEMR" Then
                            InsertPageFooterWithoutMSWBuildingBlock(aDoc, strpatname)
                        End If
                    End If

                    Try
                        ''added for bugid 96987 application not print template all pages

                        Thread.Sleep(250)
                        If (String.IsNullOrEmpty(printBookMark)) Then
                            ToPageno = aDoc.ComputeStatistics(PageCountStat, Miss)
                        End If
                        'MessageBox.Show(ToPageno.ToString())
                        If (CheckToPageno = 0) OrElse (CheckToPageno <> ToPageno) Then
                            Dim result As Boolean = False
                            If (Not String.IsNullOrEmpty(printBookMark)) Then
                                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                                    result = LoadAndCloseWord.GetRevisedPageNumbers(aDoc, printBookMark, _PrinterSetting.FromPage, _PrinterSetting.ToPage)
                                Else
                                    If (IsNothing(_popUpDetails) = False) Then
                                        result = LoadAndCloseWord.GetRevisedPageNumbers(aDoc, printBookMark, _popUpDetails.PrintFrom, _popUpDetails.PrintTo)
                                    End If
                                End If

                            End If
                            If (Not result) Then
                                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                                    If (_PrinterSetting.ToPage = CheckToPageno) Then
                                        _PrinterSetting.ToPage = ToPageno
                                    End If
                                    'If (_PrinterSetting.FromPage = CheckToPageno) Then
                                    '    _PrinterSetting.FromPage = ToPageno
                                    'End If
                                Else
                                    If (IsNothing(_popUpDetails) = False) Then
                                        If (_popUpDetails.PrintTo = CheckToPageno) Then
                                            _popUpDetails.PrintTo = ToPageno
                                        End If
                                        'If (_popUpDetails.PrintFrom = CheckToPageno) Then
                                        '    _popUpDetails.PrintFrom = ToPageno
                                        'End If
                                    End If
                                End If

                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    '    UpdateLog("InsertNamePageNo end")
                End If



                Dim RangeArg As Object ''added for bugid 96184
                Dim aFrom As Object = If(gloGlobal.gloTSPrint.isCopyPrint, If(IsNothing(_popUpDetails) = False, _popUpDetails.PrintFrom, 1), _PrinterSetting.FromPage)
                Dim aTo As Object = If(gloGlobal.gloTSPrint.isCopyPrint, If(IsNothing(_popUpDetails) = False, _popUpDetails.PrintTo, 1), _PrinterSetting.ToPage)

                RangeArg = Wd.WdPrintOutRange.wdPrintFromTo
                LoadAndCloseWord.PrintDocument(aDoc, Background, missing, RangeArg, missing, Convert.ToString(aFrom), Convert.ToString(aTo), _
                     missing, Copies, missing, missing, PrintToFile, Collate, _
                     missing, ManualDuplexPrint, PrintZoomColumn, PrintZoomRow, missing, missing, lnPatientId, popupDetails:=_popUpDetails, PrinterName:=
                     lblPrinterNameValue.Text)

                myLoadWord.CloseWordOnly(aDoc)
            End If
            ' gloAuditTrail.gloAuditTrail.PrintLog([String].Format("Finished Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID))
            ' gloAuditTrail.gloAuditTrail.PrintLog([String].Format("Closed Word Called for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID))
            '  myLoadWord.CloseWordOnly(ref aDoc);
            ' gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Finished for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
            '   GC.Collect();
            '   GC.WaitForPendingFinalizers();

            'if (System.IO.File.Exists(strFileName.ToString()))
            '{
            '    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
            '    System.IO.File.Delete(strFileName.ToString());
            '    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
            '}
            'else
            '{
            '    gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
            '}
            Try
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
                ex = Nothing
            End Try
        Catch ex As System.Runtime.InteropServices.COMException
            '  System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            gloAuditTrail.gloAuditTrail.PrintLog(strException:=ex.ToString(), ShowMessageBox:=False)
            ex = Nothing
            'ex = null;
        Catch generatedExceptionName As Exception
        Finally
            '  gloOffice.Supporting.isFromBatchPrint = false;
            If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                If oldPrinterName <> [String].Empty Then
                    gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oldPrinterName)
                    Application.DoEvents()

                End If
            End If
        End Try
        '}
    End Sub

    Public Sub InsertNamePageNo(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        If gloGlobal.gloTSPrint.isCopyPrint = True And gloGlobal.gloTSPrint.AddFooterInService = True Then
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
        If gloGlobal.gloTSPrint.isCopyPrint = True And gloGlobal.gloTSPrint.AddFooterInService = True Then
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
    Public Delegate Sub UpdateProgressControlsDelegate()
    Public Sub InvokeProgressUpdateControls()
        Try
            If (Me.IsDisposed) Then
                Return
            End If
            If Me.InvokeRequired Then
                If (Me.IsDisposed) Then
                    Return
                End If
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
            If (pbDocument.Value <= pbDocument.Maximum) Then
                pbDocument.Value = PrintDocument
                pbDocument.Refresh()
                ' If _objwordList IsNot Nothing Then
                lblPages.Text = "Printing " & PrintDocument.ToString() & " of " & pbDocument.Maximum.ToString()
                ' End If
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
    Private Sub OnPrintComplete(iar As IAsyncResult)
        'Dim r As IAsyncResult = Nothing

        Try
            IsPrintingCompletedWhilePressingPause = True
            ''  MessageBox.Show(PrintDocument.ToString())
            'If (PrintDocument <> pbDocument.Maximum) Then
            '    Print(PrintDocument)
            'End If
            Try
                If Not IsPrintingResumed Then
                    InvokeCompleteUpdateControls()
                End If
            Catch
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            'Return Nothing
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
        'Return r
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
            '' lblPageNoOfDocument.Text = "Waiting for Background to Finish Page Number " & PrintDocument.ToString() & " from Document"
            TempPrintDocument = PrintDocument
            If (TempPrintDocument = 0) Then
                TempPrintDocument = 1
            End If

            lblPages.Text = "Printing " & TempPrintDocument.ToString() & " of " & pbDocument.Maximum.ToString()
        Catch
        End Try
    End Sub
    Private Sub btnPlay_Click(sender As System.Object, e As System.EventArgs) Handles btnPlay.Click
        Try
            btnPause.Visible = True
            btnPlay.Visible = False

            'If (btnPause.Text = "&Resume") Or (pbDocument.Value >= pbDocument.Maximum) Then
            '    btnPause.Enabled = False
            '    clickTime = DateTime.Now
            '    blnbtnPauseclicked = True
            'End If
            'If DirectCast(sender, Button).Text = "&Pause" Then
            '    DirectCast(sender, Button).Text = "&Resume"
            '    IsPrintingResumed = True
            '    btnRestart.Enabled = True
            '    DirectCast(sender, Button).Update()
            '    btnRestart.Update()
            '    DirectCast(sender, Button).ResumeLayout()
            '    btnRestart.ResumeLayout()

            'Else
            '   DirectCast(sender, Button).Text = "&Pause"
            IsPrintingResumed = False

            ' DirectCast(sender, Button).Update()
            btnRestart.Update()
            ' DirectCast(sender, Button).ResumeLayout()
            btnRestart.ResumeLayout()
            'If _gloStandardPrintController IsNot Nothing Then
            '    _gloStandardPrintController.IsRestart = True
            'End If

            ' End If
            PrintWithOrWithoutBackground()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            IsPrintingCanceled = True
            'If _gloStandardPrintController IsNot Nothing Then
            '    _gloStandardPrintController.IsCancel = True
            'End If
            InvokeCompleteUpdateControls()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmgloPrintQueueController_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Try
            If _ExtendedPrinterSettings.IsShowProgress Then
                pbDocument.Maximum = 1
                'lstgloTemplate.Count;  
                Me.BringToFront()
                PrintWithOrWithoutBackground()
            Else
                Hide()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
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
            ''added for bugid 92017
            If (_isFormClose = False) Then
                _isFormClose = True
                Me.Close()
                Me.Dispose(True)

                btnRestart.Enabled = True
                'btnPause.Visible = False
                'btnPlay.Visible = True
                'IsPrintingResumed = True
                'If btnPause.Text = "&Pause" Then
                '    btnPause.Text = "&Resume"
                '    IsPrintingResumed = True
                'End If
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
    Private Sub frmgloPrintQueueController_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            If Not _ExtendedPrinterSettings.IsShowProgress Then
                Hide()
                PrintWithOrWithoutBackground()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            ex = Nothing
        End Try
    End Sub

    Private Sub frmgloPrintQueueController_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        methodToInvoke = Nothing
        If (myLoadWord IsNot Nothing) Then
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
        End If

    End Sub
End Class