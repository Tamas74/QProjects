Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.Collections
Imports System.Threading
Imports gloPrintDialog
Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Runtime.InteropServices
Partial Public Class frmgloPrintWordReminderProgressController
    Inherits Form
    '<DllImport("winspool.drv", CharSet:=CharSet.Auto, SetLastError:=True)> _
    'Public Shared Function SetDefaultPrinter(Name As String) As Boolean
    'End Function
    Private _nPagesCnt As Integer = 0, _printPageIndex As Integer = 0
    Private _IsOnPagePDFDocCreated As Boolean = False
    '   Private _gloStandardPrintController As gloStandardPrintController = Nothing
    Private WithEvents oWordApp As Microsoft.Office.Interop.Word.Application
    Private _PDFFileName As [String] = Nothing
    ' Private _oPrintDocument As PrintDocument = Nothing
    Private _PrinterSetting As PrinterSettings = Nothing
    Private _ExtendedPrinterSettings As gloExtendedPrinterSettings = Nothing

    Public IsPrintingResumed As Boolean = False
    Public IsPrintingCanceled As Boolean = False

    Private IsPrintingCompletedWhilePressingPause As Boolean = True
    Private _objwordList As ArrayList = Nothing
    Public dtSelectPatient As DataTable = Nothing
    Public dictPatientLetter As Dictionary(Of Int64, String)
    Dim blntaskidPres As Boolean = False
    Public ChkRemiderForUnSchedle As Boolean = False
    Public Shared blnBackGroundPrint As Boolean = False
    Private oCurDoc As Microsoft.Office.Interop.Word.Document
    Private PrintDocument As Integer = 0
    Private _PatientID As Int64 = 0
    Private _PatientName As String = ""
    Private _TemplateID As Int64 = 0
    Private _TemplateName As String = ""
    Private _VisitID As Int64 = 0
    Private _TaskID As Int64 = 0
    Public _bIsUnscheduledCare As Boolean = False
    Public strOldPrinterName As String = String.Empty
    Private _nCommunicationTypeID As Int64 = 0
    Private clickTime As DateTime = DateTime.Now
    Private blnbtnPauseclicked As Boolean = False
    Private _isFormClose As Boolean = False
    Dim ocls As New clsPatientLetters
    Private _popUpDetails As gloClinicalQueueGeneral.QueueDocumentDocumentDetails = Nothing

    Private myCaller As Control = Nothing
    Public Sub New(Optional myControl As Control = Nothing)

        myCaller = myControl
        InitializeComponent()

    End Sub
    Public Sub New(sPrinterSettings As PrinterSettings, sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional oSourceDocSelectedPages As ArrayList = Nothing, Optional myControl As Control = Nothing)

        myCaller = myControl
        gloPrintWordProgressControllerCall(sPrinterSettings, sExtendedPrinterSettings, oSourceDocSelectedPages)

    End Sub


    Friend Sub gloPrintWordProgressControllerCall(sPrinterSettings As PrinterSettings, sExtendedPrinterSettings As gloExtendedPrinterSettings, Optional oSourceDocSelectedPages As ArrayList = Nothing)

        Try
            InitializeComponent()
            Me.Text = "Printing"
            '' Me.ControlBox = False
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

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
    Private Sub frmgloPrintWordProgressController_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

        
        methodToInvoke = Nothing


        If (IsNothing(myCaller) = False) Then
            CType(myCaller, MainMenu).PrintMenuEventclick(False, "frmgloPrintWordReminderProgressController")
        End If
        If Not IsNothing(myLoadWord) Then
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
        End If
        If Not IsNothing(dtSelectPatient) Then
            dtSelectPatient.Dispose()
            dtSelectPatient = Nothing
        End If
        If Not IsNothing(dictPatientLetter) Then
            dictPatientLetter.Clear()
            dictPatientLetter = Nothing
        End If
        blnBackGroundPrint = False
        If Not ocls Is Nothing Then
            ocls.Dispose()
            ocls = Nothing
            End If
        Catch ex As Exception
        Finally
            Try
                If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                    If (strOldPrinterName.Trim() <> "") Then
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(strOldPrinterName)
                    End If
                End If
            Catch ex As Exception

            End Try
        End Try
    End Sub




    Private Sub frmgloPrintWordProgressController_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Dim cntrec As Int64 = 0
            If Not IsNothing(dtSelectPatient) Then
                cntrec = dtSelectPatient.Rows.Count
            End If
            If Not IsNothing(dictPatientLetter) Then
                cntrec = cntrec * dictPatientLetter.Count
            End If
            pbDocument.Maximum = cntrec
            If gloGlobal.gloTSPrint.isCopyPrint Then
                lblPrinterName.Text = "Coping To :"
                lblPrinterNameValue.Text = " Mapped drive for printing"
            Else
                lblPrinterName.Text = "Printing To :"
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            End If



            If _ExtendedPrinterSettings.IsShowProgress Then
                PrintWithOrWithoutBackground()
            Else
                Hide()
                PrintWithOrWithoutBackground()
            End If
            If Me.Visible = True Then

                If (IsNothing(myCaller) = False) Then
                    CType(myCaller, MainMenu).PrintMenuEventclick(True, "frmgloPrintWordReminderProgressController")
                End If


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
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
                        'Exhaused waiting for 2 seconds
                        Exit While
                    End If



                End While
                IsPrintingCompletedWhilePressingPause = False
            End If
            '' Show priinter selection popup for gloTS print
            Dim res As Boolean = True
            _popUpDetails = gloWord.LoadAndCloseWord.getTSPrintDialogDetails(res)
            If Not res Then
                IsPrintingCompletedWhilePressingPause = True
                InvokeCompleteUpdateControls()
                Return
            End If
            
            If _ExtendedPrinterSettings.IsBackGroundPrint Then


                blnBackGroundPrint = True
                If Not IsNothing(methodToInvoke) Then
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
    Dim TempPrintDocument As Integer = 0
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


    Private Sub OnPrint(sender As Object, e As System.EventArgs)
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
            ''
            ' update your controls here
            '    lblDocumentName.Text = _oPrintDocument.DocumentName;
            'lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            If gloGlobal.gloTSPrint.isCopyPrint Then
                lblPrinterName.Text = "Coping To :"
                lblPrinterNameValue.Text = " Mapped drive for printing"
            Else
                lblPrinterName.Text = "Printing To :"
                lblPrinterNameValue.Text = _PrinterSetting.PrinterName
            End If
            '   pbDocument.Step = 1;
            pbDocument.Minimum = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub Print()
        Dim fromDoc As Int64 = 0
        Dim blnBuildBlockSetting As Boolean = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES"))
        Dim Copies As Short = If(gloGlobal.gloTSPrint.isCopyPrint, 1, _PrinterSetting.Copies)
        Dim Background As Object = False
        Dim Range As Object = Wd.WdPrintOutRange.wdPrintAllDocument
        Dim PageType As Object = Wd.WdPrintOutPages.wdPrintAllPages
        Dim PrintToFile As Object = False
        Dim Collate As Object = If(gloGlobal.gloTSPrint.isCopyPrint, True, _PrinterSetting.Collate)
        Dim ActivePrinterMacGX As Object = Type.Missing
        Dim ManualDuplexPrint As Object = False
        Dim PrintZoomColumn As Object = 1
        Dim PrintZoomRow As Object = 1
        Dim missing As Object = Type.Missing
        ''
        Try


            For Each dr As DataRow In dtSelectPatient.Rows


                For Each di As KeyValuePair(Of Int64, String) In dictPatientLetter


                    If (fromDoc < PrintDocument) Then

                        fromDoc += 1
                    Else
                        If (_isFormClose = True) Then ''added for bugid 92668
                            Exit Sub
                        End If
                        SyncLock Me
                            _PatientID = Convert.ToInt64(dr("PatientID"))
                            _PatientName = Convert.ToString(dr("Patient Name"))
                            _TemplateID = di.Key
                            _TemplateName = di.Value

                            If blntaskidPres = False Then ''''''''cheking if taskid exist or not
                                _TaskID = 0
                            Else
                                If IsNumeric(dr("nTaskID")) Then
                                    _TaskID = Convert.ToInt64(dr("nTaskID"))
                                Else
                                    _TaskID = 0
                                End If
                            End If


                            ''  _TaskID = Convert.ToInt64(c1Patients.GetData(i, 3))
                            _VisitID = GenerateVisitID(Now, _PatientID)
                            _bIsUnscheduledCare = ChkRemiderForUnSchedle

                            Dim strFileName As String = ""
                            Try


                                Fill_TemplateGallery()
                                strFileName = ExamNewDocumentName
                                If Not oCurDoc Is Nothing Then
                                    oCurDoc.SaveAs(strFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                                End If
                            Catch ex As Exception

                            End Try
                            If strFileName <> "" Then
                                If (myLoadWord Is Nothing) Then
                                    Exit Sub
                                End If
                                If Not oCurDoc Is Nothing Then
                                    gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)

                                    If gblnPageNo = True Then
                                        UpdateLog("InsertNamePageNo start ")

                                        If blnBuildBlockSetting Then
                                            InsertNamePageNo(oCurDoc, GetPatientDetails(_PatientID))
                                        Else
                                            InsertPageFooterWithoutMSWBuildingBlock(oCurDoc, GetPatientDetails(_PatientID))
                                        End If

                                        UpdateLog("InsertNamePageNo end")
                                    End If
                                    If Not oCurDoc Is Nothing Then
                                        If (Not gloGlobal.gloTSPrint.isCopyPrint) Then


                                            gloGlobal.gloTSPrint.SetDefaultPrinterSettings(_PrinterSetting.PrinterName)
                                            Application.DoEvents()


                                            'oCurDoc.Application.ActivePrinter = _PrinterSetting.PrinterName
                                        End If

                                        'oCurDoc.PrintOut(, , , , , , , Copies)
                                        gloWord.LoadAndCloseWord.PrintDocument(oCurDoc, Background, missing, missing, missing, missing, missing, _
                  missing, Copies, missing, missing, PrintToFile, Collate, _
                  missing, ManualDuplexPrint, PrintZoomColumn, PrintZoomRow, missing, missing, _PatientID, popupDetails:=_popUpDetails)


                                    End If
                                End If
                                If Not ocls Is Nothing Then

                                    If (ocls.SavePatientLetter(0, _PatientID, _TemplateID, Date.Now, strFileName, _TemplateName, False, _bIsUnscheduledCare, _nCommunicationTypeID) > 0) Then
                                        If _TaskID <> 0 Then   ''''''''Integrated by Chetan  as on 21 oct 2010 - for DM Setup Report
                                            UpdateReminder(_TaskID)
                                        End If

                                    End If
                                    '  Exit Sub
                                End If
                                If myLoadWord Is Nothing Then
                                    Exit Sub
                                End If
                                myLoadWord.CloseWordOnly(oCurDoc)
                                PrintDocument += 1
                                fromDoc = PrintDocument
                                InvokeProgressUpdateControls()
                            End If
                    If blnbtnPauseclicked Then
                        InvokeEnableDisablePauseButton()
                    End If
                    If IsPrintingResumed = True Then
                        Exit Sub
                    End If
                        End SyncLock
                    End If


                Next

            Next
            If (blnBackGroundPrint = False) And _isFormClose = False Then ''if foreground print is on then closing  form after completing printing
                _isFormClose = True
                Me.Close()
                Me.Dispose(True)
            End If
        Catch ex As Exception
            ex = Nothing
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
    Public Function GetPatientDetails(ByVal m_PatientId As Int64)
        Dim strName As String = ""

        Dim oDB As New DataBaseLayer
        Try

            'Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ' , DOB : ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId
            Dim strSQL As String = "Select isnull(sFirstname,'')+ ' ' +isnull(slastname,'')+ ', DOB: ' +convert(char(10), isnull(dtDOB,''),101) as Name from Patient where nPatientID=" & m_PatientId

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
    Public Sub InsertNamePageNo(ByRef oCurDoc As Wd.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then

            Exit Sub
        End If
        Try
            If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            End If
            oCurDoc.Activate()

            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryFooter

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
                If strtxt.Contains("14") = False And strtxt.Contains("15") = False Then
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

                Dim CurrentPage = Wd.WdFieldType.wdFieldPage

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, CurrentPage)

                oSection.Application.ActiveWindow.Selection.TypeText(" of ")

                Dim TotalPages = Wd.WdFieldType.wdFieldNumPages

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, TotalPages)

                If Not String.IsNullOrEmpty(sName.Trim()) Then
                    oSection.Application.Selection.HeaderFooter.Range.InsertBefore(sName.Trim() & vbTab & vbTab)
                End If

            Next



        Catch ex As Exception

        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        End Try
    End Sub


    Private Sub UpdateReminder(ByVal TaskID As Int64)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "UPDATE RM_Reminder_MST SET bIsDismissed ='TRUE' WHERE nRefrenceType = 2 AND nReferenceID = " & TaskID & " "
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
        Catch ex As SqlException
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If


        End Try
    End Sub
    Private Sub Fill_TemplateGallery() 'As String
        Dim strFileName As String = ""
        Dim objWord = New clsWordDocument
        Dim objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        objCriteria.PrimaryID = _TemplateID
        objWord.DocumentCriteria = objCriteria
        ''//Retrieving the Patient Education from DB and Save it as Physical File
        strFileName = objWord.RetrieveDocumentFile()
        objCriteria.Dispose()
        objCriteria = Nothing
        objWord = Nothing
        If (IsNothing(strFileName) = False) Then
            If strFileName <> "" Then
                LoadWordUserControl(strFileName, True)
                'Set the Start postion of the cursor in documents
                oCurdoc.Application.Selection.HomeKey(Microsoft.Office.Interop.Word.WdUnits.wdStory)

            End If

        End If



    End Sub
    Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)

        oCurDoc = Nothing
        oCurDoc = myLoadWord.LoadWordApplication(strFileName)
        If blnGetData Then
            ''//To retrieve the Form fields for the Word document
            Dim objWord = New clsWordDocument
            Dim objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = _PatientID
            objCriteria.VisitID = _VisitID
            objCriteria.PrimaryID = _TemplateID ''0
            objWord.DocumentCriteria = objCriteria
            objWord.CurDocument = oCurDoc
            ''Replace Form fields with Concerned data
            objWord.GetFormFieldData(enumDocType.None)
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
        Else
            Dim objWord = New clsWordDocument
            objWord.CurDocument = oCurDoc
            objWord.HighlightColor()
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objWord = Nothing
        End If
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
            ex = Nothing
            'Return Nothing
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
            If (_isFormClose = False) Then ''added for bugid 92017
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

    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
        Try
            IsPrintingResumed = False
            IsPrintingCanceled = False
            _printPageIndex = 0
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
            PrintWithOrWithoutBackground()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnPause_Click(sender As Object, e As EventArgs) Handles btnPause.Click
        Try


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

            '  DirectCast(sender, Button).Text = "&Pause"
            btnPause.Visible = False
            btnPlay.Visible = True
            IsPrintingResumed = True

            ' DirectCast(sender, Button).Update()
            btnRestart.Update()
            ' DirectCast(sender, Button).ResumeLayout()
            btnRestart.ResumeLayout()
            'If _gloStandardPrintController IsNot Nothing Then
            '    _gloStandardPrintController.IsRestart = True
            'End If

            ' PrintWithOrWithoutBackground()
            ' End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try

    End Sub

    Private IsInsidePrinting As Boolean = False
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
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
    Private WithEvents lblPrinterNameValue As System.Windows.Forms.Label
    Private WithEvents lblPrinterName As System.Windows.Forms.Label
    Private WithEvents lblPages As System.Windows.Forms.Label
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents pbDocument As System.Windows.Forms.ProgressBar
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnPause As System.Windows.Forms.Button
    Private WithEvents btnRestart As System.Windows.Forms.Button


    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmgloPrintWordReminderProgressController))
        Me.lblPrinterNameValue = New System.Windows.Forms.Label()
        Me.lblPrinterName = New System.Windows.Forms.Label()
        Me.lblPages = New System.Windows.Forms.Label()
        Me.lblPageNoOfDocument = New System.Windows.Forms.Label()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.btnRestart = New System.Windows.Forms.Button()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbDocument = New System.Windows.Forms.ProgressBar()
        Me.lblCopies = New System.Windows.Forms.Label()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel3.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblPrinterNameValue
        '
        Me.lblPrinterNameValue.AutoEllipsis = True
        Me.lblPrinterNameValue.AutoSize = True
        Me.lblPrinterNameValue.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPrinterNameValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrinterNameValue.Location = New System.Drawing.Point(85, 10)
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
        Me.lblPrinterName.Size = New System.Drawing.Size(75, 14)
        Me.lblPrinterName.TabIndex = 7
        Me.lblPrinterName.Text = "Printing To :"
        '
        'lblPages
        '
        Me.lblPages.AutoEllipsis = True
        Me.lblPages.AutoSize = True
        Me.lblPages.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPages.Location = New System.Drawing.Point(10, 10)
        Me.lblPages.Name = "lblPages"
        Me.lblPages.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblPages.Size = New System.Drawing.Size(88, 14)
        Me.lblPages.TabIndex = 2
        Me.lblPages.Text = "Please Wait... "
        '
        'lblPageNoOfDocument
        '
        Me.lblPageNoOfDocument.AutoSize = True
        Me.lblPageNoOfDocument.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblPageNoOfDocument.Location = New System.Drawing.Point(336, 10)
        Me.lblPageNoOfDocument.Name = "lblPageNoOfDocument"
        Me.lblPageNoOfDocument.Size = New System.Drawing.Size(0, 14)
        Me.lblPageNoOfDocument.TabIndex = 4
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
        Me.panel3.Location = New System.Drawing.Point(1, 36)
        Me.panel3.Name = "panel3"
        Me.panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.panel3.Size = New System.Drawing.Size(349, 91)
        Me.panel3.TabIndex = 4
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
        Me.btnRestart.Location = New System.Drawing.Point(215, 24)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(31, 39)
        Me.btnRestart.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnRestart, "Restart" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.btnRestart.UseVisualStyleBackColor = True
        '
        'btnPause
        '
        Me.btnPause.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPause.Image = CType(resources.GetObject("btnPause.Image"), System.Drawing.Image)
        Me.btnPause.Location = New System.Drawing.Point(246, 24)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(31, 39)
        Me.btnPause.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnPause, "Pause" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
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
        Me.btnPlay.Location = New System.Drawing.Point(277, 24)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(31, 39)
        Me.btnPlay.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.btnPlay, "Play")
        Me.btnPlay.UseVisualStyleBackColor = True
        Me.btnPlay.Visible = False
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
        Me.btnCancel.Location = New System.Drawing.Point(308, 24)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(31, 39)
        Me.btnCancel.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Close")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pbDocument
        '
        Me.pbDocument.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pbDocument.Location = New System.Drawing.Point(10, 63)
        Me.pbDocument.Name = "pbDocument"
        Me.pbDocument.Size = New System.Drawing.Size(329, 18)
        Me.pbDocument.TabIndex = 9
        '
        'lblCopies
        '
        Me.lblCopies.AutoEllipsis = True
        Me.lblCopies.AutoSize = True
        Me.lblCopies.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblCopies.Location = New System.Drawing.Point(336, 10)
        Me.lblCopies.Name = "lblCopies"
        Me.lblCopies.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCopies.Size = New System.Drawing.Size(3, 14)
        Me.lblCopies.TabIndex = 3
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.lblPageNoOfDocument)
        Me.panel1.Controls.Add(Me.lblCopies)
        Me.panel1.Controls.Add(Me.lblPrinterNameValue)
        Me.panel1.Controls.Add(Me.lblPrinterName)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(1, 1)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(10)
        Me.panel1.Size = New System.Drawing.Size(349, 35)
        Me.panel1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Silver
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(350, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 126)
        Me.Label1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Silver
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 126)
        Me.Label2.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Silver
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(351, 1)
        Me.Label3.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Silver
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(351, 1)
        Me.Label4.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Silver
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(349, 1)
        Me.Label5.TabIndex = 10
        '
        'frmgloPrintWordProgressController
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(351, 128)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.panel3)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmgloPrintWordReminderProgressController"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)

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

            PrintWithOrWithoutBackground()
            ' End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
        End Try

    End Sub
End Class

