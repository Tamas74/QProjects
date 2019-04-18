Imports System.IO
Imports gloCCDLibrary
Imports gloCCDSchema
Imports gloPatientPortalCommon
Imports gloEDocumentV3.Forms ''added for Fax Implementation in 8030 phase2
Imports Microsoft.Office.Interop ''added for Fax Implementation in 8030 phase2
Imports pdftron.PDF
Imports System.Data.SqlClient

Public Class frmCCDAGenerateList
    Implements IHotKey
    Dim _nCDAId As Int64 = 0
    Dim _nPatientId As Int64 = 0
    Private _nExamID As Int64 = 0
    Private _nOrderID As Int64 = 0
    Dim _FromDate As String = Nothing
    Dim _ToDate As String = Nothing
    Dim _sGeneratedFrom As String
    Dim strFilePath As String = String.Empty
    Private _bIsSendToPortal As Boolean = False
    Private _bIsSendToAPI As Boolean = False

    Dim blnAllChkd As Boolean = False
    Dim blnChk As Boolean = False
    Private nContactID As Int64 = 0
    Private Const ClinicalSummary As Integer = 0
    Private Const AmbulatorySummary As Integer = 1
    Private Const careRecord As Integer = 2
    Private Const careSummary As Integer = 3
    Dim oSourceDocSelectedPages As ArrayList
    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
    Dim nTransactionProviderID As Int64 = 0
    Private Shared printDocument1 As New System.Drawing.Printing.PrintDocument
    Dim bln_ExportCDAClicked As Boolean = False ''added to  bypass  autodelete setting for cda generated successfully message when  export button clicked 
    Private WithEvents dgCustomGridselectExam As CustomTask
    Private WithEvents dgCustomGridselectOrder As CustomTask

    Dim sConfidentialityCode As String = "Normal"
    Dim pdfBase64 As String = String.Empty



#Region "Constants"
    Private Col_eExamID As Integer = 0
    Private Col_eVistitID As Integer = 1
    Private Col_eDos As Integer = 2
    Private Col_eExamName As Integer = 3
    Private Col_eTemplateName As Integer = 4
    Private Col_eFinished As Integer = 5
    Private Col_eProviderName As Integer = 6
    Private Col_eReviewedBy As Integer = 7
    Private Col_eSpeciality As Integer = 8
    Private Col_eCheck As Integer = 9
    Private Col_eCount As Integer = 10


    ''Order


    Private Const COL_ORDERID As Int16 = 0
    Private Const COL_ORDERPREFIX As Int16 = 1
    Private Const COL_ORDERNO As Int16 = 2
    Private Const COL_ORDERPROVIDERID As Int16 = 3
    Private Const COL_ORDERPROVIDERNAME As Int16 = 4
    Private Const COL_ORDERSTATUS As Int16 = 5
    Private Const COL_ORDERVISITID As Int16 = 6
    Private Const COL_ORDERIsClosed As Int16 = 7
    Private Const COL_ORDERDate As Int16 = 8
    Private Const COL_ORDERIsAck As Int16 = 9
    Private Const COL_ORDERIsResult As Int16 = 10

    Private Const COL_COUNT As Int16 = 11

#End Region


    Private bDisablePreferredProvider As Boolean = False
    Public Property DisablePreferredProvider() As Boolean
        Get
            Return bDisablePreferredProvider
        End Get
        Set(ByVal value As Boolean)
            bDisablePreferredProvider = value
        End Set
    End Property


    Public Property ContactID()
        Get
            Return nContactID
        End Get
        Set(ByVal value)
            nContactID = value
        End Set
    End Property

    Public Property sGeneratedFrom()
        Get
            Return _sGeneratedFrom
        End Get
        Set(ByVal value)
            _sGeneratedFrom = value
        End Set
    End Property
    Public Property nExamId() As Int64
        Get
            Return _nExamID
        End Get
        Set(ByVal value As Int64)
            _nExamID = value
        End Set
    End Property

    Public Property nOrderId() As Int64
        Get
            Return _nOrderID
        End Get
        Set(ByVal value As Int64)
            _nOrderID = value
        End Set
    End Property

    Private _nVisitID As Int64
    Public Property nVisitId() As Int64
        Get
            Return _nVisitID
        End Get
        Set(ByVal value As Int64)
            _nVisitID = value
        End Set
    End Property

    Private _sDetail As String = String.Empty
    Public Property sDetail() As String
        Get
            Return _sDetail
        End Get
        Set(ByVal value As String)
            _sDetail = value
        End Set
    End Property

    Dim _nCDAFileType As CDAFileTypeEnum
    Public Property nCDAFileType() As CDAFileTypeEnum
        Get
            Return _nCDAFileType
        End Get
        Set(ByVal value As CDAFileTypeEnum)
            _nCDAFileType = value
        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID

    End Sub

    Private Sub tblClose_Click(sender As System.Object, e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub
    Private Function getPortalInvitationStatus(ByVal npatid As Int64) As Int32

        Dim clConfidentialityCodes As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand


        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "WS_CheckPatientRegisterOrNotOnPortalORInvited"
            objCmd.Connection = objCon
            objCmd.Parameters.AddWithValue("@nPatientID", npatid)
            objCon.Open()
            Dim objdata As Int32 = System.Convert.ToInt32(objCmd.ExecuteScalar())





            Return objdata
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return 0
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Close()
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function
    Private Sub RegisterPatientForAPIAccess()
        Dim nAPIAccessID As Int64 = 0
        Dim nProviderID As Int64 = 0
        Dim sEmailAddress As String = ""

        Dim apiAccess As clsAPIAcceess = Nothing
        Dim ProviderTaxID As gloGlobal.TIN.clsSelectProviderTaxID = Nothing

        Try
            apiAccess = New clsAPIAcceess()
            nProviderID = IIf(gnLoginProviderID = 0, gnPatientProviderID, gnLoginProviderID)

            Using dtPatientInfo As DataTable = mdlGeneral.GetPatientInfo(_nPatientId)
                If dtPatientInfo IsNot Nothing AndAlso dtPatientInfo.Columns.Contains("sEmail") AndAlso dtPatientInfo.Rows.Count() > 0 Then
                    sEmailAddress = System.Convert.ToString(dtPatientInfo.Rows(0)("sEmail"))
                End If
            End Using

            apiAccess.ActivateAPIAccess(_nPatientId, sEmailAddress, apiAccess.GetRandomNumber(), GetConnectionString())

            Using dbLayer As New ClsDBLayer()
                nAPIAccessID = dbLayer.GetPortalAccessID(_nPatientId)
            End Using

            ProviderTaxID = New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
            ProviderTaxID.InsertProviderTaxID(nProviderAssociationID, nAPIAccessID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.APIAccessActivate)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
            ex = Nothing
        Finally
            ProviderTaxID = Nothing
        End Try
    End Sub

    Private Sub tlbbtn_Email_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtn_Email.Click
        Try
            If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
                MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
                MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If strProviderDirectAddress <> "" OrElse gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                Dim sError As String = gloSurescriptSecureMessage.SecureMessage.ValidateZipCode(_nPatientId)
                If sError <> "" Then
                    MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                    Return
                Else
                    strFilePath = GenerateCDA("")
                    If File.Exists(strFilePath) Then
                        Dim ofrmSendNewMail As New InBox.NewMail(_nPatientId, strFilePath, ContactID)
                        ofrmSendNewMail.DisplayCDAButton = False
                        If gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation IsNot Nothing Then
                            If Not Me.DisablePreferredProvider Then
                                gloSurescriptSecureMessage.SecureMessage.SetPreferredProvider(gloEMR.gnPatientProviderID)
                            End If

                            ofrmSendNewMail.ListOfProviders = gloSurescriptSecureMessage.SecureMessageProperties.ListUserProviderAssociation
                        End If
                        ofrmSendNewMail.DocumentReferenceID = _nCDAId
                        ofrmSendNewMail.ShowDialog()
                        ofrmSendNewMail.Close()
                        ofrmSendNewMail = Nothing
                        'Added for Auto Deleting CCDA files
                        Try
                            If Not IsNothing(strFilePath) Then
                                If _isAutoDeleteCCDAFiles = True Then
                                    File.Delete(strFilePath)
                                End If
                            End If
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                        End Try
                    Else
                        MessageBox.Show("Error While generating attachment. Please try again", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If

            Else
                MessageBox.Show(gstrDirectWarningMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            _nCDAId = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'Incident CAS-02204-N9J2Y6 panel always visible after the message sent
            pnlPrintMessage.Visible = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub tblGenCCDA_Click(sender As System.Object, e As System.EventArgs)
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
            MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
            MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        'pnlPrintMessage.Visible = True
        'Label24.Visible = True
        'Label24.BringToFront()
        'lblFormularyTransactionMessage.Visible = False
        'pnlPrintMessage.BringToFront()
        'Application.DoEvents()
        'Me.Cursor = Cursors.WaitCursor

        GenerateCDA("")
        pnlPrintMessage.Visible = False
        Me.Cursor = Cursors.Default
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Generate, "CCDA File Generated. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        _nCDAId = 0
    End Sub


    Private Sub tlbbtn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtn_Print.Click

        Try

            Me.Cursor = Cursors.WaitCursor
            tlbbtn_Print.Enabled = False
            '08-Jun-16 Aniket: Resolving Bug #96832: Generate CDA : Print button prints CDA files only once and next time it shows 'Please wait' message and never printing
            If IsNothing(Me.WebBrowser1) = True Then
                Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
            End If
            If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
                MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
                MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'pnlPrintMessage.Visible = True
            'Label24.Visible = True
            'Label24.BringToFront()
            'lblFormularyTransactionMessage.Visible = True
            'pnlPrintMessage.BringToFront()
            'Application.DoEvents()
            'Me.Cursor = Cursors.WaitCursor
            Dim strPdfFilepath As String = ""
            Dim strFilepath As String = ""
            Dim objpdf As pdftron.PDF.PDFDoc
            strFilepath = GenerateCDA(strFilepath)

            If strFilepath <> "" Then
                Dim ofile As FileInfo = New FileInfo(strFilepath)

                Dim myXslTransform As New Xml.Xsl.XslTransform()
                Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

                myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                myXslTransform.Transform(strFilepath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
                Me.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))

                'Me.WebBrowser1.Navigate(ofile.FullName)
                If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loaded Or Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
                    While Me.WebBrowser1.ReadyState <> WebBrowserReadyState.Complete
                        Application.DoEvents()
                    End While
                End If


                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim objWdDoc As Word.Document = myLoadWord.LoadWordApplication(_strfileName)
                Try
                    If (IsNothing(objWdDoc) = False) Then
                        Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                        Try
                            thisAlertLevel = objWdDoc.Application.DisplayAlerts
                            objWdDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                        Catch ex As Exception

                        End Try

                        strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
                        Try
                            If gloGlobal.gloTSPrint.isCopyPrint Then
                                Dim dtPatientTable As DataTable = GetPatientInformation(_nPatientId, GetConnectionString())
                                Dim StrPatientName As String = ""
                                StrPatientName = dtPatientTable.Rows(0).Item("Patient Name") + ", DOB : " + dtPatientTable.Rows(0).Item("DOB")
                                If System.Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("USE_BUILDING_BLOCKS_IN_WORD_TEMPLATES")) Then
                                    InsertNamePageNo(objWdDoc, StrPatientName)
                                Else
                                    InsertPageFooterWithoutMSWBuildingBlock(objWdDoc, StrPatientName)
                                End If
                            End If
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
                myLoadWord = Nothing


                If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
                    If gblnUseDefaultPrinter = True Then
                        objpdf = New PDFDoc(strPdfFilepath)
                        Print(objpdf)
                    Else
                        objpdf = New PDFDoc(strPdfFilepath)
                        Print(objpdf)
                    End If

                    If Not IsNothing(objpdf) Then
                        objpdf.Dispose()
                        objpdf = Nothing
                    End If

                End If





            End If
            'Added for Auto Deleting CCDA files
            Try
                If Not IsNothing(strFilepath) Then
                    If _isAutoDeleteCCDAFiles = True Then
                        File.Delete(strFilepath)
                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            End Try

            pnlPrintMessage.Visible = False
            Me.Cursor = Cursors.Default
            Dim strsummtype As String = cmbSummaryType.Text
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Print, strsummtype & " CCDA File Printed. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            _nCDAId = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.Print, ex, gloAuditTrail.ActivityOutCome.Failure)
        Finally

            tlbbtn_Print.Enabled = True
            Me.Cursor = Cursors.Default
            If Not IsNothing(Me.WebBrowser1) Then
                Me.WebBrowser1.Dispose()
                Me.WebBrowser1 = Nothing
            End If

        End Try
    End Sub

    Public Sub InsertNamePageNo(ByRef oCurDoc As Word.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        If gloGlobal.gloTSPrint.isCopyPrint = True And gloGlobal.gloTSPrint.AddFooterInService = True Then
            Exit Sub
        End If

        Try
            If oCurDoc.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdOutlineView Then
                oCurDoc.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView
            End If
            oCurDoc.Activate()

            Try
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter
            Catch ex As Exception
                oCurDoc.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter
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
            For Each objTemp As Word.Template In oCurDoc.Application.Templates
                If objTemp.Name = "Building Blocks.dotx" Or objTemp.Name = "Built-In Building Blocks.dotx" Then
                    objTemp.BuildingBlockEntries.Item("Bold Numbers 3").Insert(Where:=oCurDoc.Application.Selection.HeaderFooter.Range, RichText:=True)
                End If
            Next
            If sName <> "" Then
                oCurDoc.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft
                oCurDoc.Application.Selection.HeaderFooter.Range.InsertBefore(sName & vbTab & vbTab)
                oCurDoc.Application.Selection.EndKey(Word.WdUnits.wdStory)
                oCurDoc.Application.Selection.TypeBackspace()
            End If

        Catch ex As Exception

        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument
        End Try
    End Sub

    Public Sub InsertPageFooterWithoutMSWBuildingBlock(ByRef oCurDoc As Word.Document, ByVal sName As String)
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        If gloGlobal.gloTSPrint.isCopyPrint = True And gloGlobal.gloTSPrint.AddFooterInService = True Then
            Exit Sub
        End If
        Dim strTrimmedName As String = sName.Trim()


        Try
            For Each oSection As Word.Section In oCurDoc.Sections

                If oSection.Application.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdOutlineView Then
                    oSection.Application.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView
                End If

                Try
                    oSection.Application.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekPrimaryFooter
                Catch ex As Exception
                    oSection.Application.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekCurrentPageFooter
                End Try

                oSection.Application.Selection.HeaderFooter.Range.Delete()
                oSection.Application.Selection.HeaderFooter.Range.Font.Name = "Arial"

                oSection.Application.Selection.HeaderFooter.Range.Font.Size = 8
                oSection.Application.Selection.HeaderFooter.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft

                oSection.Application.Selection.Range.Text = String.Empty

                oSection.Application.Selection.TypeText("Page ")

                Dim CurrentPage = Word.WdFieldType.wdFieldPage

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, CurrentPage)

                oSection.Application.ActiveWindow.Selection.TypeText(" of ")

                Dim TotalPages = Word.WdFieldType.wdFieldNumPages

                oSection.Application.ActiveWindow.Selection.Fields.Add(oSection.Application.Selection.Range, TotalPages)

                If Not String.IsNullOrEmpty(strTrimmedName) Then
                    oSection.Application.Selection.HeaderFooter.Range.InsertBefore(strTrimmedName & vbTab & vbTab)
                End If


            Next

        Catch ex As Exception

        Finally
            oCurDoc.ActiveWindow.ActivePane.View.SeekView = Word.WdSeekView.wdSeekMainDocument
        End Try
    End Sub
    Private bSendToWeb As Boolean
    Public Property SendToWeb() As Boolean
        Get
            Return bSendToWeb
        End Get
        Set(ByVal value As Boolean)
            bSendToWeb = value
        End Set
    End Property

    Private Sub tblSendPortal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblSendPortal.Click
        Me.SendToWeb = True
        nTransactionProviderID = (Global.gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(_nPatientId))
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
            MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
            MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf Not getProviderTaxID(nTransactionProviderID) Then
            Exit Sub
        End If
        Dim oclsgloPatientPortalEmail As New clsgloPatientPortalEmail(GetConnectionString())
        ''Task #67312: Do not allow sending Clinical Summary for patients without portal accounts.
        ''Added datatable to check whether the patient havinng active patient account on portal.
        ''If availabe then generate CDA and send email else Prompt message as "Selected patient does not have an active patient portal account.".
        Dim dtValidPortalUser As DataTable = Nothing
        Try
            dtValidPortalUser = oclsgloPatientPortalEmail.ToCheckPatientRegisterOrNotOnPortal(_nPatientId)
            If (dtValidPortalUser IsNot Nothing AndAlso dtValidPortalUser.Rows.Count > 0) OrElse gblnPatRegIntuitorPortal = True Then '' or condition added for change on Intuit setting 

                'pnlPrintMessage.Visible = True
                'Label24.Visible = True
                'Label24.BringToFront()
                'lblFormularyTransactionMessage.Visible = False
                'pnlPrintMessage.BringToFront()
                'Application.DoEvents()
                'Me.Cursor = Cursors.WaitCursor

                _bIsSendToPortal = True
                'Added to get FilePath 
                Dim strFilepath As String = ""
                strFilepath = GenerateCDA(strFilepath)

                If strFilepath <> String.Empty Then
                    ''Added for MU2 Patient Portal - Email notification after genrateing CCDA on 20130906

                    If (dtValidPortalUser IsNot Nothing AndAlso dtValidPortalUser.Rows.Count > 0 AndAlso gblnPatientPortalEnabled = True) Then

                        oclsgloPatientPortalEmail.SendPortalEmail(_nPatientId, gstrPatientPortalEmailService, gstrPatientPortalSiteNm, gnClinicID, "CDA", True)




                    End If
                    If _nCDAId <> 0 Then
                        Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nTransactionProviderID)
                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _nCDAId, sProviderTaxID, nTransactionProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ExportCCDADocument.GetHashCode())
                        oclsselectProviderTaxID = Nothing
                    End If
                    ''End
                End If
                pnlPrintMessage.Visible = False
                Me.Cursor = Cursors.Default
                'Added for Auto Deleting CCDA files
                Try
                    If Not IsNothing(strFilepath) Then
                        If _isAutoDeleteCCDAFiles = True Then
                            File.Delete(strFilepath)
                        End If
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                End Try
            Else
                MessageBox.Show("Selected patient does not have an active patient portal account.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        Catch ex As Exception
        Finally
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to portal. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            If oclsgloPatientPortalEmail IsNot Nothing Then
                oclsgloPatientPortalEmail = Nothing
            End If
            If Not IsNothing(dtValidPortalUser) Then  ''slr free dtPortalValidUser
                dtValidPortalUser.Dispose()
                dtValidPortalUser = Nothing
            End If
        End Try
        _nCDAId = 0

    End Sub

    Private Sub tblSendPortalAMB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblSendPortalAMBPortal.Click, tblSendPortalAMBBoth.Click, tblSendPortalAMBAPI.Click
        Dim btnType As Integer = 0
        _bIsSendToPortal = False
        _bIsSendToAPI = False
        Me.SendToWeb = True

        If sender.ToString().ToLower() = "portal".ToLower() Then
            btnType = 0
        ElseIf sender.ToString().ToLower() = "api".ToLower() Then
            btnType = 1
        ElseIf sender.ToString().ToLower() = "Portal and API".ToLower() Then
            btnType = 2
        End If

        nTransactionProviderID = (Global.gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(_nPatientId))
        If nCDAFileType = CDAFileTypeEnum.AmbulatorySummary AndAlso lblDetails.Text = "Exam" Then
            MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
            MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
            MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf Not getProviderTaxID(nTransactionProviderID) Then
            Exit Sub
        End If
        Dim oclsgloPatientPortalEmail As New clsgloPatientPortalEmail(GetConnectionString())
        ''Task #67312: Do not allow sending Clinical Summary for patients without portal accounts.
        ''Added datatable to check whether the patient havinng active patient account on portal.
        ''If availabe then generate CDA and send email else Prompt message as "Selected patient does not have an active patient portal account.".


        Try

            Dim bIsPatientactivatedForAPI As Boolean = False
            Dim bIsPatientactivatedForPortal As Boolean = False
            Dim bIsPatientPortalInvited As Boolean = False
            If (getPortalInvitationStatus(_nPatientId) >= 1) Then  ''if patient is invited then sent cda
                bIsPatientPortalInvited = True
                bIsPatientactivatedForPortal = True
            End If
            If btnType = 1 Or btnType = 2 Then
                Dim dtValidAPIUser As DataTable = Nothing
                Dim oClsAPIAcceess As New clsAPIAcceess
                dtValidAPIUser = oClsAPIAcceess.CheckPatientRegisterOrNotForAPI(GetConnectionString(), _nPatientId)
                If (dtValidAPIUser IsNot Nothing AndAlso dtValidAPIUser.Rows.Count > 0) Then
                    If dtValidAPIUser.Rows(0)("bAllowAccess").ToString().ToLower() = "true".ToLower() Then
                        bIsPatientactivatedForAPI = True
                    End If

                End If
                If Not IsNothing(dtValidAPIUser) Then  ''slr free dtPortalValidUser
                    dtValidAPIUser.Dispose()
                    dtValidAPIUser = Nothing
                End If
            End If

            If btnType = 0 Or btnType = 2 Then

                If (bIsPatientPortalInvited = False) Then
                    Dim dtValidPortalUser As DataTable = Nothing

                    dtValidPortalUser = oclsgloPatientPortalEmail.ToCheckPatientRegisterOrNotOnPortal(_nPatientId)
                    If (dtValidPortalUser IsNot Nothing AndAlso dtValidPortalUser.Rows.Count > 0) Then
                        bIsPatientactivatedForPortal = True
                    End If
                    If Not IsNothing(dtValidPortalUser) Then  ''slr free dtPortalValidUser
                        dtValidPortalUser.Dispose()
                        dtValidPortalUser = Nothing
                    End If
                End If
            End If
            If (bIsPatientPortalInvited = False) AndAlso bIsPatientactivatedForPortal = False Then  ''if not invited to portal and also portal is not activated
                If btnType = 0 Then
                    If bIsPatientactivatedForPortal = False Then
                        MessageBox.Show("Selected patient does not have an active patient portal account.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                ElseIf btnType = 1 Then
                    If bIsPatientactivatedForAPI = False Then
                        MessageBox.Show("Selected patient does not have an active API account.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                ElseIf btnType = 2 Then
                    If bIsPatientactivatedForPortal = False Then
                        MessageBox.Show("Selected patient does not have an active patient portal account.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If bIsPatientactivatedForAPI = False Then
                        MessageBox.Show("Selected patient does not have an active API account.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
            If (btnType <> 0) Then  ''if api and portal and api button click then only activate api 
                If (bIsPatientPortalInvited = True Or bIsPatientactivatedForPortal = True) Then  ''''if  invitation send or portal activated then  activate api 
                    If (bIsPatientactivatedForAPI = False) Then
                        bIsPatientactivatedForAPI = True
                        RegisterPatientForAPIAccess()

                    End If
                End If
            End If

            If btnType = 1 Then

                If bIsPatientactivatedForAPI = True Then
                    '----HD
                    _bIsSendToPortal = False
                    _bIsSendToAPI = True

                    'Added to get FilePath  
                    Dim strFilepath As String = ""
                    strFilepath = GenerateCDA(strFilepath, True)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to API. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    If strFilepath <> String.Empty Then

                        'Enrty in GL_messageQueue sart
                        Dim oclsMessageQueue As New ClsMessageQueue(GetConnectionString(), DateTime.Now, _nPatientId)
                        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
                        Dim sMessageID As String = String.Empty
                        Dim nUserId As Long = 0
                        If appSettings("UserID") <> Nothing Then
                            If appSettings("UserID") <> "" Then
                                nUserId = System.Convert.ToInt64(appSettings("UserID"))
                            End If
                        End If
                        Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
                        Dim ClientMachineID As String = oclsMessageQueue.IsClientAccess(_MachineName)
                        Dim MessageType As String = "API CDA"

                        sMessageID = oclsMessageQueue.InsertInMessageQueueForAPI(nUserId, ClientMachineID, _MachineName, False, False, False, 0, MessageType)
                        'Enrty in GL_messageQueue End

                        If _nCDAId <> 0 Then
                            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nTransactionProviderID)
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _nCDAId, sProviderTaxID, nTransactionProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ExportCCDADocument.GetHashCode())
                            oclsselectProviderTaxID = Nothing
                        End If
                        ''End
                    End If
                    pnlPrintMessage.Visible = False
                    Me.Cursor = Cursors.Default
                    'Added for Auto Deleting CCDA files
                    Try
                        If Not IsNothing(strFilepath) Then
                            If _isAutoDeleteCCDAFiles = True Then
                                File.Delete(strFilepath)
                            End If
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                    '----HD

                End If

                '-----
            Else
                '----------------------------------------------

                If (bIsPatientactivatedForPortal AndAlso bIsPatientactivatedForAPI) OrElse bIsPatientactivatedForPortal Then '' or condition added for change on Intuit setting 
                    If btnType = 0 Then
                        _bIsSendToPortal = True
                        _bIsSendToAPI = False
                    ElseIf btnType = 2 Then
                        _bIsSendToPortal = True
                        _bIsSendToAPI = True
                    End If

                    Dim strFilepath As String = ""
                    strFilepath = GenerateCDA(strFilepath, True)
                    If btnType = 0 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to portal. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ElseIf btnType = 2 Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to portal. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to API. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If

                    If strFilepath <> String.Empty Then
                        '''''''If Both then insert GL_MessageQue Email For API20130906
                        If btnType = 0 Or btnType = 2 Then
                            If (bIsPatientactivatedForPortal AndAlso gblnPatientPortalEnabled = True) Then
                                '  oclsgloPatientPortalEmail.SendPortalEmail(_nPatientId, gstrPatientPortalEmailService, gstrPatientPortalSiteNm, gnClinicID, "CDA", True)
                                If nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
                                    If btnType = 2 Then
                                        '' Portal and API email Combine integrated from 9011
                                        oclsgloPatientPortalEmail.SendPortalEmail(_nPatientId, gstrPatientPortalEmailService, gstrPatientPortalSiteNm, gnClinicID, "HealthCDA and API", True)
                                    Else
                                        oclsgloPatientPortalEmail.SendPortalEmail(_nPatientId, gstrPatientPortalEmailService, gstrPatientPortalSiteNm, gnClinicID, "HealthCDA", True)
                                    End If

                                Else
                                    oclsgloPatientPortalEmail.SendPortalEmail(_nPatientId, gstrPatientPortalEmailService, gstrPatientPortalSiteNm, gnClinicID, "CDA", True)
                                End If

                            End If
                        End If
                        'If btnType = 2 Then
                        '    'Enrty in GL_messageQueue sart
                        '    Dim oclsMessageQueue As New ClsMessageQueue(GetConnectionString(), DateTime.Now, _nPatientId)
                        '    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
                        '    Dim sMessageID As String = String.Empty
                        '    Dim nUserId As Long = 0
                        '    If appSettings("UserID") <> Nothing Then
                        '        If appSettings("UserID") <> "" Then
                        '            nUserId = System.Convert.ToInt64(appSettings("UserID"))
                        '        End If
                        '    End If
                        '    Dim _MachineName As String = System.Windows.Forms.SystemInformation.ComputerName
                        '    Dim ClientMachineID As String = oclsMessageQueue.IsClientAccess(_MachineName)
                        '    Dim MessageType As String = "API CDA"

                        '    sMessageID = oclsMessageQueue.InsertInMessageQueueForAPI(nUserId, ClientMachineID, _MachineName, False, False, False, 0, MessageType)
                        '    'Enrty in GL_messageQueue End
                        'End If

                        If _nCDAId <> 0 Then
                            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nTransactionProviderID)
                            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _nCDAId, sProviderTaxID, nTransactionProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ExportCCDADocument.GetHashCode())
                            oclsselectProviderTaxID = Nothing
                        End If
                        ''End
                    End If
                    pnlPrintMessage.Visible = False
                    Me.Cursor = Cursors.Default
                    'Added for Auto Deleting CCDA files
                    Try
                        If Not IsNothing(strFilepath) Then
                            If _isAutoDeleteCCDAFiles = True Then
                                File.Delete(strFilepath)
                            End If
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                End If

                '----------------------------------------------
            End If



        Catch ex As Exception
        Finally
            '_nVisitID = 0
            '_nExamID = 0
            lblDetails.Text = "Exam"
            lblDetails.Text = sDetail
            'If btnType = 0 Then
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to portal. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'ElseIf btnType = 1 Then
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to API. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'ElseIf btnType = 2 Then
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to portal. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Send, "CCDA File Send to API. ", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'End If
            If oclsgloPatientPortalEmail IsNot Nothing Then
                oclsgloPatientPortalEmail = Nothing
            End If


        End Try
        _nCDAId = 0

    End Sub

    Private Sub rbClinicalSummery_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbClinicalSummery.CheckedChanged
        If (rbClinicalSummery.Checked = True) Then
            chkDate.Checked = False
            pnlCCDMessage.Visible = False
            pnlExanDtl.Visible = True
            If nExamId = 0 Then
                lblDetails.Text = "Exam"
            Else
                lblDetails.Text = sDetail
            End If
            pnlClinicalSummary.Visible = True
            pnlAmbulatorySummary.Visible = False
            pnlTransitionCareRecord.Visible = False
            pnlCareSummary.Visible = False
            pnlFormDate.Visible = False
            tblSave.Visible = True
            tlbbtn_Print.Visible = True


            '07-Feb-14 Aniket: Show 'Send to Portal' button only if Patient Portal is enabled
            If gblnPatientPortalEnabled = True Or gblnIntuitCommunication = True Then
                tblSendPortal.Visible = True
            Else
                tblSendPortal.Visible = False
            End If

            tlbbtn_Email.Visible = False
            nCDAFileType = CDAFileTypeEnum.ClinicalSummary
            ''CheckAllNew()
            SetHeight()
            pnlCCDMessage.Visible = False
        End If
    End Sub

    Private Sub rbAmbulatorySummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAmbulatorySummary.CheckedChanged
        If (rbAmbulatorySummary.Checked = True) Then
            chkDate.Checked = False
            pnlCCDMessage.Visible = False
            pnlClinicalSummary.Visible = False
            pnlAmbulatorySummary.Visible = True
            pnlTransitionCareRecord.Visible = False
            pnlCareSummary.Visible = False
            pnlFormDate.Visible = True
            tblSave.Visible = True
            tlbbtn_Print.Visible = True
            tblSendPortal.Visible = False
            tlbbtn_Email.Visible = False
            nCDAFileType = CDAFileTypeEnum.AmbulatorySummary
            ''CheckAllNew()
            SetHeight()
            pnlExanDtl.Visible = False

        End If

    End Sub

    Private Sub rbCareRecord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCareRecord.CheckedChanged
        ''If (rbCareRecord.Checked = True) Then
        ''    chkDate.Checked = False
        ''    pnlCCDMessage.Visible = False
        ''    pnlClinicalSummary.Visible = False
        ''    pnlAmbulatorySummary.Visible = False
        ''    pnlTransitionCareRecord.Visible = True
        ''    pnlCareSummary.Visible = False
        ''    pnlFormDate.Visible = True
        ''    tblSave.Visible = True
        ''    tlbbtn_Print.Visible = True
        ''    tblSendPortal.Visible = False




        ''    '07-Feb-14 Aniket: Show 'Direct' button only if Patient Portal is enabled
        ''    If gblnIsSecureMsgEnable = True Then
        ''        tlbbtn_Email.Visible = True
        ''    Else
        ''        tlbbtn_Email.Visible = False
        ''    End If


        ''    nCDAFileType = CDAFileTypeEnum.CareRecordSummary
        ''    SelectAll()
        ''    SetHeight()
        ''    pnlExanDtl.Visible = True
        ''    If nOrderId = 0 Then
        ''        lblDetails.Text = "Order"
        ''    Else
        ''        lblDetails.Text = sDetail
        ''    End If
        ''End If
    End Sub

    Private Sub ChkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAll.CheckedChanged
        ''blnAllChkd = True
        ''If ChkAll.Checked = True Then
        ''    '    If cmbSummaryType.SelectedIndex = ClinicalSummary Or cmbSummaryType.SelectedIndex = AmbulatorySummary Then
        ''    '        Call SelectNewAll()
        ''    '    Else
        ''    '        ChkAll.Text = "Clear All"
        ''    '        Call SelectAll()
        ''    '    End If

        ''    ChkAll.Text = "Clear All"
        ''    Call SelectAll()
        ''Else
        ''    ChkAll.Text = "Select All"
        ''    If blnChk = False Then
        ''        Call UnSelectAll()
        ''    End If
        ''    blnChk = False
        ''End If
        ''blnAllChkd = False
    End Sub

    Private Sub frmCCDAGenerateList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If (nCDAFileType = CDAFileTypeEnum.ClinicalSummary) Then
        '    rbClinicalSummery.Checked = True
        'ElseIf (nCDAFileType = CDAFileTypeEnum.AmbulatorySummary) Then
        '    rbAmbulatorySummary.Checked = True
        'ElseIf (nCDAFileType = CDAFileTypeEnum.CareRecordSummary) Then
        '    rbCareRecord.Checked = True
        'End If

        ''Bug #72854: CDA -> Default Checkbox Selection is not consistent for "Referral Summary/Summary of care Records" 
        ''Changes to resolve the bug
        GetSettingsforCDA()

        '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
        ' ''Fill_ConfidentialityCodess()
        getPurposeofUseCodes()
        setFormData()
        '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End

        If (nCDAFileType = CDAFileTypeEnum.ClinicalSummary) Then
            cmbSummaryType.SelectedIndex = ClinicalSummary
        ElseIf (nCDAFileType = CDAFileTypeEnum.AmbulatorySummary) Then
            cmbSummaryType.SelectedIndex = AmbulatorySummary
        ElseIf (nCDAFileType = CDAFileTypeEnum.CareRecordSummary) Then
            cmbSummaryType.SelectedIndex = careRecord
        ElseIf (nCDAFileType = CDAFileTypeEnum.CarePlan) Then
            cmbSummaryType.SelectedIndex = careSummary
        End If
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Or nCDAFileType = CDAFileTypeEnum.CareRecordSummary Or nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
            pnlExanDtl.Visible = True
            lblDetails.Text = sDetail
        Else
            pnlExanDtl.Visible = False
        End If

        If Me.CallFromSecureInbox Then
            tlbbtn_Email.Visible = False
        End If

        '07-Feb-14 Aniket: Show 'Send to Portal' button only if Patient Portal is enabled
        If gblnPatientPortalEnabled = True Or gblnIntuitCommunication = True Then ''or condition added to show send to portal button if intuit setting is on
            tblSendPortal.Visible = True
        Else
            tblSendPortal.Visible = False
        End If

        '28Apr2014 Sagar Ghodke: Work item#67958
        SetUIForSelectedSummaryType()
        chkintime.Checked = False
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCCDAGenerateList_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'If Not IsNothing(Me.WebBrowser1) Then
        '    Me.WebBrowser1.Dispose()
        '    Me.WebBrowser1 = Nothing
        'End If
    End Sub

    Private Sub getPurposeofUseCodes()
        With cmbPurposeofUse
            .Items.Clear()
            Dim clPurposeofUseCodes As New Collection
            clPurposeofUseCodes = getPurposeofUseCodesVal()

            Dim nCount As Int16
            For nCount = 1 To clPurposeofUseCodes.Count
                .Items.Add(clPurposeofUseCodes.Item(nCount).ToString.Trim)
            Next
            .SelectedIndex = 0
        End With
    End Sub

    Private Function getPurposeofUseCodesVal() As Collection

        Dim clPurposeofUseCodes As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_getCCDAPurposeofUseCodes"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            While objSQLDataReader.Read
                clPurposeofUseCodes.Add(objSQLDataReader.Item(0))
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader = Nothing

            Return clPurposeofUseCodes
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function

    Private Sub setFormData()
        Dim dsCCDAPatientConsent As New DataSet
        Dim clCCDAPatientConsent As New DataView

        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()

        dsCCDAPatientConsent = oCDADataExtraction.getCCDAPatientConsentVal(_nPatientId)

        oCDADataExtraction = Nothing

        clCCDAPatientConsent = dsCCDAPatientConsent.Tables(2).DefaultView
        If clCCDAPatientConsent.Count > 0 Then
            cmbPurposeofUse.Text = clCCDAPatientConsent(0).Item(0).ToString()
        End If


        clCCDAPatientConsent = Nothing
        dsCCDAPatientConsent = Nothing
    End Sub

    Private Sub SelectAll()
        ''RemoveHandler rbSelectAllNormal.CheckedChanged, AddressOf rbSelectAllNormal_CheckedChanged
        ''rbSelectAllNormal.Checked = True
        ''AddHandler rbSelectAllNormal.CheckedChanged, AddressOf rbSelectAllNormal_CheckedChanged
        ''RemoveHandler rbSelectAllRestricted.CheckedChanged, AddressOf rbSelectAllRestricted_CheckedChanged
        ''rbSelectAllRestricted.Checked = True
        ''AddHandler rbSelectAllRestricted.CheckedChanged, AddressOf rbSelectAllRestricted_CheckedChanged


        'Common dataset
        chkCODemographic.Checked = True
        'chkCOSmoking.Checked = True  '28Apr2014, Sagar Ghodke: Smoking status check-box replaced by "Social  History" check-box
        chkCOProblems.Checked = True
        chkCOAllergy.Checked = True
        chkCOCareTeamMem.Checked = True
        chkCOProcedures.Checked = True

        chkCOVitalSigns.Checked = True
        chkCOlabResult.Checked = True
        chkCOLabTest.Checked = True
        chkCOMedication.Checked = True
        chkCSClinicalInstru.Checked = True 'Clinical instruction previously was part of Clinical Summary which is moved to common dataset now
        chkCOFamilyHistory.Checked = True
        chkCOSocialHistory.Checked = True
        chkImplant.Checked = True
        If nCDAFileType <> CDAFileTypeEnum.CarePlan Then
            ChkCOAssessments.Checked = True
            ChkCOTreatmentPlan.Checked = True
            ChkCOAssessments.Enabled = True
            ChkCOTreatmentPlan.Enabled = True
        Else
            ChkCOAssessments.Checked = False
            ChkCOTreatmentPlan.Checked = False
            ChkCOAssessments.Enabled = False
            ChkCOTreatmentPlan.Enabled = False
        End If
        ChkCOGoals.Checked = True
        ChkCOHealthConcerns.Checked = True
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then
            'chkCSClinicalInstru.Checked = True 'Clinical instruction previously was part of Clinical Summary which is moved to common dataset now
            chkCSProviderName.Checked = True
            chkCSFutureAppt.Checked = True
            chkCSOfcContact.Checked = True
            chkCSRefOtrProvider.Checked = True
            chkCSVisitInfo.Checked = True
            chkCSDecisionAids.Checked = True
            chkCSVisitMedications.Checked = True
            chkCSVisitImmunization.Checked = True
            chkCSDigTestPending.Checked = True
            chkCSFutureTest.Checked = True
            chkCSVisitReason.Checked = True
            ''CheckALL(1)
        ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
            chkAmbProviderName.Checked = True
            chkAmbProviderContact.Checked = True
            chkAmbImmunization.Checked = True

            chkambEncounters.Checked = True

            ChkAmbFunctionalStatus.Checked = True
            ChkAmbReasonReferral.Checked = True
            ChkAmbReferring.Checked = True
            ChkAmbMental.Checked = True
            chkambDatelocationvisit.Checked = True
            ''CheckALL(2)
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary Then
            chkTransCareEncounter.Checked = True
            chkTransCareImmunization.Checked = True
            chkTransCareCognitiveStat.Checked = True
            chkTransCareResReferral.Checked = True
            chkTransCareRefProvider.Checked = True
            chkTransCareFunctionalStat.Checked = True
            ChkCareProvider.Checked = True
            ChkCareOfficeContact.Checked = True
            chktransDateLocationvisit.Checked = True
        ElseIf nCDAFileType = CDAFileTypeEnum.CarePlan Then
            chkCareEncounterDiagnoses.Checked = True
            chkInterventions.Checked = True
            chkHealthStatus.Checked = True
            chkCareplanImmunizations.Checked = True
        End If
        If rbSelectAllRestricted.Checked Then
            chkPrivarySection.Checked = True
            chkPrivaryText.Checked = True
        Else
            chkPrivarySection.Checked = False
            chkPrivaryText.Checked = False
        End If

        '' Respect Admin settings case - CAS-17510-P2G3V6 by Ujwala as on 12032018
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary And chkOvrAdminSettings.Checked = False Then
            If Not IsNothing(_strDefaultClinicalSummary) Then
                UnSelectAll()
                checkCCDASections(_strDefaultClinicalSummary, "ClinicalCCDA")
                ''    CheckALL(1)
            End If
        ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary And chkOvrAdminSettings.Checked = False Then
            If Not IsNothing(_strDefaultAmbulatorySummary) Then
                UnSelectAll()
                checkCCDASections(_strDefaultAmbulatorySummary, "AmbulatoryCCDA")
                ''   CheckALL(2)
            End If
        End If
        '' Respect Admin settings case - CAS-17510-P2G3V6 by Ujwala as on 12032018
    End Sub
    Private Sub CheckSelection()
        If blnAllChkd = False Then
            If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then

                If chkCSProviderName.Checked = True AndAlso chkCSFutureAppt.Checked = True AndAlso chkCSOfcContact.Checked = True AndAlso chkCSRefOtrProvider.Checked = True AndAlso chkCSVisitInfo.Checked = True AndAlso chkCSDecisionAids.Checked = True AndAlso chkCSVisitMedications.Checked = True AndAlso chkCSVisitImmunization.Checked = True AndAlso chkCSDigTestPending.Checked = True AndAlso chkCSFutureTest.Checked = True AndAlso chkCSVisitReason.Checked = True AndAlso chkCODemographic.Checked = True AndAlso chkCOProblems.Checked = True AndAlso chkCOAllergy.Checked = True AndAlso chkCOCareTeamMem.Checked = True AndAlso chkCOProcedures.Checked = True AndAlso chkCOVitalSigns.Checked = True AndAlso chkCOlabResult.Checked = True AndAlso chkCOLabTest.Checked = True AndAlso chkCOMedication.Checked = True AndAlso chkCSClinicalInstru.Checked = True AndAlso chkCOFamilyHistory.Checked = True AndAlso chkCOSocialHistory.Checked = True AndAlso chkImplant.Checked = True AndAlso ChkCOGoals.Checked = True AndAlso ChkCOHealthConcerns.Checked = True AndAlso ChkCOTreatmentPlan.Checked = True AndAlso ChkCOAssessments.Checked = True Then
                    If ChkAll.Checked = False Then
                        ChkAll.Checked = True
                    End If
                Else
                    If ChkAll.Checked = True Then
                        blnChk = True
                        ChkAll.Checked = False
                    End If
                End If
            ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
                If chkambDatelocationvisit.Checked = True AndAlso chkAmbProviderName.Checked = True AndAlso chkAmbProviderContact.Checked = True AndAlso chkAmbImmunization.Checked = True AndAlso chkCODemographic.Checked = True AndAlso chkCOProblems.Checked = True AndAlso chkCOAllergy.Checked = True AndAlso chkCOCareTeamMem.Checked = True AndAlso chkCOProcedures.Checked = True AndAlso chkCOVitalSigns.Checked = True AndAlso chkCOlabResult.Checked = True AndAlso chkCOLabTest.Checked = True AndAlso chkCOMedication.Checked = True AndAlso chkCSClinicalInstru.Checked = True AndAlso chkCOFamilyHistory.Checked = True AndAlso chkCOSocialHistory.Checked = True AndAlso chkImplant.Checked = True AndAlso ChkCOGoals.Checked = True AndAlso ChkCOHealthConcerns.Checked = True AndAlso ChkAmbMental.Checked = True AndAlso ChkAmbReasonReferral.Checked = True AndAlso ChkAmbReferring.Checked = True AndAlso ChkAmbFunctionalStatus.Checked = True AndAlso chkambEncounters.Checked = True AndAlso ChkCOTreatmentPlan.Checked = True AndAlso ChkCOAssessments.Checked = True Then
                    If ChkAll.Checked = False Then
                        ChkAll.Checked = True
                    End If
                Else
                    If ChkAll.Checked = True Then
                        blnChk = True
                        ChkAll.Checked = False
                    End If
                End If
            ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary Then
                If chktransDateLocationvisit.Checked = True AndAlso chkTransCareEncounter.Checked = True AndAlso chkTransCareImmunization.Checked = True AndAlso chkTransCareCognitiveStat.Checked = True AndAlso chkTransCareResReferral.Checked = True AndAlso chkTransCareRefProvider.Checked = True AndAlso chkTransCareFunctionalStat.Checked = True AndAlso ChkCareProvider.Checked = True AndAlso ChkCareOfficeContact.Checked = True AndAlso chkCODemographic.Checked = True AndAlso chkCOProblems.Checked = True AndAlso chkCOAllergy.Checked = True AndAlso chkCOCareTeamMem.Checked = True AndAlso chkCOProcedures.Checked = True AndAlso chkCOVitalSigns.Checked = True AndAlso chkCOlabResult.Checked = True AndAlso chkCOLabTest.Checked = True AndAlso chkCOMedication.Checked = True AndAlso chkCSClinicalInstru.Checked = True AndAlso chkCOFamilyHistory.Checked = True AndAlso chkCOSocialHistory.Checked = True AndAlso chkImplant.Checked = True AndAlso ChkCOGoals.Checked = True AndAlso ChkCOHealthConcerns.Checked = True AndAlso ChkCOAssessments.Checked = True AndAlso ChkCOTreatmentPlan.Checked = True AndAlso chktransDateLocationvisit.Checked = True Then
                    If ChkAll.Checked = False Then
                        ChkAll.Checked = True
                    End If
                Else
                    If ChkAll.Checked = True Then
                        blnChk = True
                        ChkAll.Checked = False
                    End If
                End If
            ElseIf nCDAFileType = CDAFileTypeEnum.CarePlan Then

                If chkCareplanImmunizations.Checked = True AndAlso chkHealthStatus.Checked = True AndAlso chkInterventions.Checked = True AndAlso chkCODemographic.Checked = True AndAlso chkCOProblems.Checked = True AndAlso chkCOAllergy.Checked = True AndAlso chkCOCareTeamMem.Checked = True AndAlso chkCOProcedures.Checked = True AndAlso chkCOVitalSigns.Checked = True AndAlso chkCOlabResult.Checked = True AndAlso chkCOLabTest.Checked = True AndAlso chkCOMedication.Checked = True AndAlso chkCSClinicalInstru.Checked = True AndAlso chkCOFamilyHistory.Checked = True AndAlso chkCOSocialHistory.Checked = True AndAlso chkImplant.Checked = True AndAlso ChkCOGoals.Checked = True AndAlso ChkCOHealthConcerns.Checked = True AndAlso ChkAmbMental.Checked = True AndAlso ChkAmbReasonReferral.Checked = True AndAlso ChkAmbReferring.Checked = True AndAlso ChkAmbFunctionalStatus.Checked = True AndAlso chkambEncounters.Checked = True AndAlso chkCareEncounterDiagnoses.Checked = True Then
                    If ChkAll.Checked = False Then
                        ChkAll.Checked = True
                    End If
                Else
                    If ChkAll.Checked = True Then
                        blnChk = True
                        ChkAll.Checked = False
                    End If
                End If
            End If
        End If


    End Sub
    Public Function SetWriterParametrs() As gloCDAWriterParameters
        Dim objCDAWriterParameters As gloCDAWriterParameters = New gloCDAWriterParameters()

        objCDAWriterParameters.CDAFileType = nCDAFileType
        If _bIsSendToPortal Then
            objCDAWriterParameters.StyleSheetPath = "gloCCDAcss_MU2.xsl"
        End If

        If chkCODemographic.Checked = True Then
            objCDAWriterParameters.Demographics = True
        End If

        ''28Apr2014, Sagar Ghodke: Smoking status check-box replaced by "Social  History" check-box
        'If chkCOSmoking.Checked = True Then
        '    objCDAWriterParameters.SmokingStatus = True
        'End If

        If chkCOProblems.Checked = True Then
            objCDAWriterParameters.Problems = True
        End If

        If chkCOAllergy.Checked = True Then
            objCDAWriterParameters.Allergies = True
        End If

        If chkCOCareTeamMem.Checked = True Then
            objCDAWriterParameters.CareTeamMember = True
        End If

        If chkCOProcedures.Checked = True Then
            objCDAWriterParameters.Procedures = True
        End If



        If chkCOVitalSigns.Checked = True Then
            objCDAWriterParameters.VitalSigns = True
        End If

        If chkCOlabResult.Checked = True Then
            objCDAWriterParameters.LaboratoryResult = True
        End If

        If chkCOLabTest.Checked = True Then
            objCDAWriterParameters.LaboratoryTest = True
        End If

        If chkCOMedication.Checked = True Then
            objCDAWriterParameters.Medications = True
        End If

        If chkCOFamilyHistory.Checked = True Then
            objCDAWriterParameters.FamilyHistory = True
        End If

        If chkCOSocialHistory.Checked = True Then
            objCDAWriterParameters.SocialHistory = True
        End If

        'Date: 21Apr2014,Sagar Ghodke: Clinical instructions is moved from Clinical Summary to 
        'common dataset
        If chkCSClinicalInstru.Checked = True Then
            objCDAWriterParameters.ClinicalInstructions = True
        End If

        'Date: 23Apr2014, Sagar Ghodke: Setting flag to mark CCDA file with 'Patient Copy' text
        If chkPatientCopy.Checked = True Then
            objCDAWriterParameters.MarkPatientCopy = True
        End If
        If chkImplant.Checked = True Then
            objCDAWriterParameters.Implant = True
        End If
        If ChkCOGoals.Checked = True Then
            objCDAWriterParameters.Goals = True
        End If
        If ChkCOHealthConcerns.Checked = True Then
            objCDAWriterParameters.HealthConcern = True
        End If
        If ChkCOTreatmentPlan.Checked = True Then
            objCDAWriterParameters.TreatmentPlan = True
        End If
        If ChkCOAssessments.Checked = True Then
            objCDAWriterParameters.Assessments = True
        End If

        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then

            If chkCSProviderName.Checked = True Then
                objCDAWriterParameters.ProviderName = True
            End If
            If chkCSFutureAppt.Checked = True Then
                objCDAWriterParameters.FutureAppointments = True
            End If
            If chkCSOfcContact.Checked = True Then
                objCDAWriterParameters.OfficeContact = True
            End If
            If chkCSRefOtrProvider.Checked = True Then
                objCDAWriterParameters.ReferralsToOtherProviders = True
            End If
            If chkCSVisitInfo.Checked = True Then
                objCDAWriterParameters.Visit_DateAndLocation = True
            End If
            If chkCSDecisionAids.Checked = True Then
                objCDAWriterParameters.RecommendedPatientDecisionAids = True
            End If
            If chkCSVisitMedications.Checked = True Then
                objCDAWriterParameters.MedicationsAdministered = True
            End If
            If chkCSVisitImmunization.Checked = True Then
                objCDAWriterParameters.ImmunizationsAdministered = True
            End If
            If chkCSDigTestPending.Checked = True Then
                objCDAWriterParameters.DiagnosticTestsPending = True
            End If
            If chkCSFutureTest.Checked = True Then
                objCDAWriterParameters.FutureScheduledTests = True
            End If
            If chkCSVisitReason.Checked = True Then
                objCDAWriterParameters.ChiefComplaint = True
            End If
        ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
            If chkambDatelocationvisit.Checked = True Then
                objCDAWriterParameters.Visit_DateAndLocation = True
            End If
            If chkAmbProviderName.Checked = True Then
                objCDAWriterParameters.ProviderName = True
            End If
            If chkAmbProviderContact.Checked = True Then
                objCDAWriterParameters.OfficeContact = True
            End If
            If chkAmbImmunization.Checked = True Then
                objCDAWriterParameters.Immunizations = True
            End If
            If chkambEncounters.Checked = True Then
                objCDAWriterParameters.EncounterDiagnoses = True
            End If

            If ChkAmbMental.Checked = True Then
                objCDAWriterParameters.CognitiveStatus = True
            End If
            If ChkAmbReasonReferral.Checked = True Then
                objCDAWriterParameters.ReasonForReferral = True
            End If
            If ChkAmbReferring.Checked = True Then
                objCDAWriterParameters.ReferringProvider = True
            End If
            If ChkAmbFunctionalStatus.Checked = True Then
                objCDAWriterParameters.FunctionalStatus = True
            End If
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary Then
            If chktransDateLocationvisit.Checked = True Then
                objCDAWriterParameters.Visit_DateAndLocation = True
            End If
            If chkTransCareEncounter.Checked = True Then
                objCDAWriterParameters.EncounterDiagnoses = True
            End If
            If chkTransCareImmunization.Checked = True Then
                objCDAWriterParameters.Immunizations = True
            End If
            If chkTransCareCognitiveStat.Checked = True Then
                objCDAWriterParameters.CognitiveStatus = True
            End If
            If chkTransCareResReferral.Checked = True Then
                objCDAWriterParameters.ReasonForReferral = True
            End If
            If chkTransCareRefProvider.Checked = True Then
                objCDAWriterParameters.ReferringProvider = True
            End If
            If chkTransCareFunctionalStat.Checked = True Then
                objCDAWriterParameters.FunctionalStatus = True
            End If
            If ChkCareProvider.Checked = True Then
                objCDAWriterParameters.CareProvider = True
            End If
            If ChkCareOfficeContact.Checked = True Then
                objCDAWriterParameters.CareOfficeContact = True
            End If
        ElseIf nCDAFileType = CDAFileTypeEnum.CarePlan Then
            If chkCareEncounterDiagnoses.Checked = True Then
                objCDAWriterParameters.EncounterDiagnoses = True
            End If
            If chkCareplanImmunizations.Checked = True Then
                objCDAWriterParameters.Immunizations = True
            End If
            If chkInterventions.Checked = True Then
                objCDAWriterParameters.Interventions = True
            End If
            If chkHealthStatus.Checked = True Then
                objCDAWriterParameters.Outcomes = True
            End If
        End If

        Return objCDAWriterParameters

    End Function

    Private Sub UnSelectAll()

        ChkAll.Checked = False

        'Common dataset
        chkCODemographic.Checked = True
        'chkCOSmoking.Checked = False '28Apr2014, Sagar Ghodke: Smoking status check-box replaced by "Social  History" check-box
        chkCOProblems.Checked = False
        chkCOAllergy.Checked = False
        chkCOCareTeamMem.Checked = False
        chkCOProcedures.Checked = False

        chkCOVitalSigns.Checked = False
        chkCOlabResult.Checked = False
        chkCOLabTest.Checked = False
        chkCOMedication.Checked = False
        chkCSClinicalInstru.Checked = False 'Clinical instruction previously was part of Clinical Summary which is moved to common dataset now
        chkCOFamilyHistory.Checked = False
        chkCOSocialHistory.Checked = False
        chkImplant.Checked = False
        ChkCOGoals.Checked = False
        ChkCOHealthConcerns.Checked = False
        '   If nCDAFileType <> CDAFileTypeEnum.CarePlan Then
        ChkCOAssessments.Checked = False
        ChkCOTreatmentPlan.Checked = False
        ' Else
        'ChkCOAssessments.Checked = False
        ' ChkCOTreatmentPlan.Checked = False
        'End If

        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then

            'chkCSClinicalInstru.Checked = False 'Clinical instruction previously was part of Clinical Summary which is moved to common dataset now
            chkCSProviderName.Checked = False
            chkCSFutureAppt.Checked = False
            chkCSOfcContact.Checked = False
            chkCSRefOtrProvider.Checked = False
            chkCSVisitInfo.Checked = False
            chkCSDecisionAids.Checked = False
            chkCSVisitMedications.Checked = False
            chkCSVisitImmunization.Checked = False
            chkCSDigTestPending.Checked = False
            chkCSFutureTest.Checked = False
            chkCSVisitReason.Checked = False

        ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
            chkAmbProviderName.Checked = False
            chkAmbProviderContact.Checked = False
            chkAmbImmunization.Checked = False

            chkambEncounters.Checked = False
            ChkAmbReasonReferral.Checked = False
            ChkAmbReferring.Checked = False
            ChkAmbMental.Checked = False
            ChkAmbFunctionalStatus.Checked = False
            chkambDatelocationvisit.Checked = False
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary Then
            chkTransCareEncounter.Checked = False
            chkTransCareImmunization.Checked = False
            chkTransCareCognitiveStat.Checked = False
            chkTransCareResReferral.Checked = False
            chkTransCareRefProvider.Checked = False
            chkTransCareFunctionalStat.Checked = False
            ChkCareProvider.Checked = False
            ChkCareOfficeContact.Checked = False
            chktransDateLocationvisit.Checked = False
        ElseIf nCDAFileType = CDAFileTypeEnum.CarePlan Then
            chkCareEncounterDiagnoses.Checked = False
            chkInterventions.Checked = False
            chkHealthStatus.Checked = False
            chkCareplanImmunizations.Checked = False
        End If
        chkPrivarySection.Checked = False
        chkPrivaryText.Checked = False

    End Sub


    Private Function GenerateCDA(ByVal FilePath As String, Optional ByVal bIsForPortal As Boolean = False) As String


        Dim oCDADataExtraction As gloCCDLibrary.gloCDADataExtraction = Nothing
        Dim strFilePath As String = FilePath
        Dim msg As String = String.Empty
        Try
            If dtpFrom.Enabled = True Then
                If System.Convert.ToDateTime(dtpFrom.Value().ToShortDateString()) > System.Convert.ToDateTime(dtpToDate.Value().ToShortDateString()) Then
                    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GenerateCDA = Nothing
                    Exit Function
                End If
                '_FromDate = dtpFrom.Value.ToShortDateString()
                '_ToDate = dtpToDate.Value.ToShortDateString()
                If (dtpFrom.CustomFormat.Contains("hh")) Then
                    _FromDate = dtpFrom.Value  '' date format change to datetime 
                    _ToDate = dtpToDate.Value
                Else
                    _FromDate = dtpFrom.Value.Date    '' date format change to datetime 
                    _ToDate = dtpToDate.Value.Date
                    _FromDate = _FromDate & " " & "12:00:00 AM"
                    _ToDate = _ToDate & " " & "11:59:00 PM"
                End If

            Else
                _FromDate = Nothing
                _ToDate = Nothing
            End If

            lblCCDMessage.Visible = False
            pnlCCDMessage.Visible = False

            pnlPrintMessage.Visible = True
            Label24.Visible = True
            Label24.BringToFront()
            lblFormularyTransactionMessage.Visible = False
            pnlPrintMessage.BringToFront()
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ''
            Dim _PurposeofUse As String
            Dim _nId As Int64 = 0
            ''
            Dim strCCDDirectory As String

            If strFilePath = "" Then
                strCCDDirectory = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath
            Else
                strCCDDirectory = System.IO.Path.GetDirectoryName(strFilePath)
            End If


            '03-Mar-16 Aniket: Resolving Bug #93899: Export CDA : User defined export path not respected
            'If gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath <> "" Then
            If strCCDDirectory <> "" Then
                'If Directory.Exists(gstrCCDFilePath) = True Then
                If Directory.Exists(strCCDDirectory) Then

                    If _bIsSendToPortal Then
                        If Not System.IO.File.Exists(strCCDDirectory & "/gloCCDAcss_MU2.xsl") Then
                            System.IO.File.Copy(Application.StartupPath & "/gloCCDAcss_MU2.xsl", strCCDDirectory & "\gloCCDAcss_MU2.xsl", True)
                        End If

                    End If

                    ''  '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start                   
                    If rbSelectAllRestricted.Checked Then
                        gloLibCCDGeneral.sConfidentialityCode = "Restricted"
                    Else
                        gloLibCCDGeneral.sConfidentialityCode = "Normal"
                    End If

                    gloLibCCDGeneral.bIncludePrivacyText = chkPrivaryText.Checked
                    gloLibCCDGeneral.bIncludePrivacySection = chkPrivarySection.Checked

                    _PurposeofUse = cmbPurposeofUse.Text.Trim()




                    '''' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End 


                    ''
                    Dim objCDAWriterParameters As gloCDAWriterParameters = SetWriterParametrs()

                    oCDADataExtraction = New gloCCDLibrary.gloCDADataExtraction()
                    oCDADataExtraction.nExamId = _nExamID
                    ''
                    _nId = oCDADataExtraction.INUPCDAPurposeofUse(_nId, _nPatientId, _PurposeofUse)
                    ''

                    Dim _tempExamId As Int64 = 0
                    Dim _tempVisitId As Int64 = 0
                    Dim _tempOrderId As Int64 = 0
                    If (nCDAFileType = CDAFileTypeEnum.ClinicalSummary) Then
                        sGeneratedFrom = "Exam CDA"
                        _tempExamId = nExamId
                        _tempVisitId = _nVisitID
                    ElseIf (nCDAFileType = CDAFileTypeEnum.CareRecordSummary) Then
                        _tempOrderId = nOrderId
                        sGeneratedFrom = "Order CDA"
                    Else
                        sGeneratedFrom = Nothing
                    End If
                    ''If bIsForPortal Then
                    If (nCDAFileType = CDAFileTypeEnum.AmbulatorySummary) Then
                        _tempExamId = nExamId
                        _tempVisitId = _nVisitID
                    End If
                    '  End If
                  


                    'strFilePath = oCDADataExtraction.GenerateClinicalInformation(_nPatientId, gnLoginID, objCDAWriterParameters, _tempVisitId, _FromDate, _ToDate, strFilePath, False, Nothing, _tempOrderId, pdfBase64)
                    strFilePath = oCDADataExtraction.GenerateClinicalInformation(_nPatientId, gnLoginID, objCDAWriterParameters, _tempVisitId, _FromDate, _ToDate, strFilePath, False, Nothing, _tempOrderId)
                    msg = oCDADataExtraction.strmsg
                    If msg <> "" Then
                        MessageBox.Show(msg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End If

                    If strFilePath <> "" Then
                        lblCCDMessage.Text = "CDA file" & " ‘" & strFilePath & "’ " & "saved successfully. "
                        If Not CallFromSecureInbox Then
                            'Code Added not to show panel as while AutodeleteCCDAfiles is true
                            If _isAutoDeleteCCDAFiles = True AndAlso bln_ExportCDAClicked = False Then
                                pnlCCDMessage.Visible = False
                                lblCCDMessage.Visible = False
                            Else
                                pnlCCDMessage.Visible = True
                                lblCCDMessage.Visible = True
                            End If
                        End If

                        If Me.SendToWeb Then
                            lblCCDMessage.Text = "CDA file sent successfully."
                            pnlCCDMessage.Visible = True
                            lblCCDMessage.Visible = True
                        End If
                        
                        If pnlCCDMessage.Visible = True Then
                            SetHeight()
                        End If
                        'nTransactionProviderID = (Global.gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(_nPatientId))
                        'If Not getProviderTaxID(nTransactionProviderID) Then
                        '    GenerateCDA = Nothing
                        '    Exit Function
                        'End If
                        Dim strmsg As String = "CDA File Displayed"
                        If _bIsSendToPortal = True AndAlso _bIsSendToAPI = True Then
                            strmsg = "CCDA File Send to portal and API"
                        ElseIf _bIsSendToPortal = True AndAlso _bIsSendToAPI = False Then
                            strmsg = "CCDA File Send to portal"
                        ElseIf _bIsSendToPortal = False AndAlso _bIsSendToAPI = True Then
                            strmsg = "CCDA File Send to API"
                        Else
                            strmsg = "CDA File Displayed"


                        End If
                        _nCDAId = oCDADataExtraction.SaveExportedCDA(_nPatientId, strFilePath, strmsg, sGeneratedFrom, False, , , "CDA", nCDAFileType, _tempExamId, _bIsSendToPortal, _tempOrderId, bIsSendToAPI:=_bIsSendToAPI)
                        'If _nCDAId <> 0 Then
                        '    Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nTransactionProviderID)
                        '    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, _nCDAId, sProviderTaxID, nTransactionProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.ExportCCDADocument.GetHashCode())
                        '    oclsselectProviderTaxID = Nothing
                        'End If
                    Else
                        MessageBox.Show("CDA file not generated.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        GenerateCDA = Nothing
                        Exit Function
                    End If
                Else
                    'MessageBox.Show("Invalid CDA file path. Set a valid CDA path from gloEMR Admin.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("The CCD/C-CDA file path set in gloEMR admin '" & gstrCCDFilePath & "' could not be located/accessed. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GenerateCDA = Nothing
                    Exit Function
                End If
            Else
                MessageBox.Show("The CCD/C-CDA file path is not set in gloEMR admin. Please contact your system administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            If msg <> "" Then
                MessageBox.Show(msg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            GenerateCDA = Nothing
            '  MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            _bIsSendToPortal = False
            If Not IsNothing(oCDADataExtraction) Then
                oCDADataExtraction.Dispose()
                oCDADataExtraction = Nothing
            End If
            bln_ExportCDAClicked = False
            Me.SendToWeb = False
        End Try
        Return strFilePath
    End Function

    Private Sub Print(doc As PDFDoc)
        Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing
        Try
            Using oDialog As New gloPrintDialog.gloPrintDialog()
                oDialog.ConnectionString = GetConnectionString()
                oDialog.TopMost = True
                oDialog.ShowPrinterProfileDialog = True
                Dim intFilePageCount As Integer = 0

                oDialog.ModuleName = "PrintCCDA"
                oDialog.RegistryModuleName = "CCDA"

                If oDialog IsNot Nothing Then

                    doc.Lock()
                    Dim maxPage As Integer = doc.GetPageCount()
                    If (Not gloGlobal.gloTSPrint.isCopyPrint) Then



                        oDialog.PrinterSettings = printDocument1.PrinterSettings



                        oDialog.AllowSomePages = True

                        oDialog.PrinterSettings.ToPage = maxPage
                        oDialog.PrinterSettings.FromPage = 1
                        oDialog.PrinterSettings.MaximumPage = maxPage
                        oDialog.PrinterSettings.MinimumPage = 1

                        'PrintDialog1.AllowSomePages = True
                        'PrintDialog1.PrinterSettings.ToPage = maxPage
                        'PrintDialog1.PrinterSettings.FromPage = 1
                        'PrintDialog1.PrinterSettings.MaximumPage = maxPage
                        'PrintDialog1.PrinterSettings.MinimumPage = 1
                    End If
                    If (intFilePageCount <= 0) Then
                        intFilePageCount = doc.GetPageCount() + maxPage
                    Else
                        intFilePageCount = doc.GetPageCount() + intFilePageCount
                    End If


                    If oDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then

                        If (oDialog.bUseDefaultPrinter = True) Then
                            oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                            oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
                        End If
                        If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                            printDocument1.PrinterSettings = oDialog.PrinterSettings
                        End If
                        Dim footer As gloPrintDialog.gloPrintProgressController.FooterInfo = New gloPrintDialog.gloPrintProgressController.FooterInfo
                        Dim footerList As List(Of gloPrintDialog.gloPrintProgressController.FooterInfo) = New List(Of gloPrintDialog.gloPrintProgressController.FooterInfo)
                        If Not gloGlobal.gloTSPrint.isCopyPrint Then
                            Dim dtPatientTable As DataTable = GetPatientInformation(_nPatientId, GetConnectionString())
                            Dim StrPatientName As String = ""
                            StrPatientName = dtPatientTable.Rows(0).Item("Patient Name") + ", DOB : " + dtPatientTable.Rows(0).Item("DOB")
                            footer.FromPage = 1
                            footer.ToPage = intFilePageCount - 1
                            footer.StartingPage = 1
                            footer.TotalPages = intFilePageCount - maxPage
                            footer.CenterText = ""
                            footer.RightText = "[{PAGE()}] of [{TOTAL()}]"
                            footer.LeftText = StrPatientName
                            footerList.Add(footer)
                        End If

                        ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(doc, doc.GetFileName(), oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, Nothing, footerList, True)

                        'ogloPrintProgressController.ShowProgress(Me)


                        If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                            If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then
                                ogloPrintProgressController.Show()
                            Else
                                ogloPrintProgressController.Show()
                            End If
                        Else
                            ogloPrintProgressController.TopMost = True
                            ogloPrintProgressController.ShowInTaskbar = False

                            ogloPrintProgressController.ShowDialog(Me)
                            If ogloPrintProgressController IsNot Nothing Then
                                ogloPrintProgressController.Dispose()
                            End If
                            ogloPrintProgressController = Nothing
                        End If

                    End If

                    doc.Unlock()
                Else
                    Dim _ErrorMessage As String = "Error in Showing Print Dialog"
                    MessageBox.Show(_ErrorMessage, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])

                End If

            End Using
        Catch ex As Exception
            Dim _ErrorMessage As String = ex.ToString()
            If _ErrorMessage.Trim() <> "" Then
                Dim _MessageString As String = ("Date Time : " & DateTime.Now.ToString()) + Environment.NewLine & "ERROR : " & _ErrorMessage
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                _MessageString = ""
            End If
            MessageBox.Show(ex.Message, gloCCDGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            ex = Nothing
        Finally


        End Try

    End Sub
    Public Function GetPatientInformation(ByVal nPatinetId As Int64, ByVal strConnection As String) As DataTable
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDbLayer.Connect(False)
            Dim oPatientDataTable As DataTable = Nothing
            oDbLayer.Retrive_Query("SELECT dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) As 'Patient Name',Convert(Datetime,Patient.dtDOB,101)As DOB, datediff(yy,Patient.dtDOB,dbo.gloGetDate()) AS 'Age',Patient.sGender As Gender,  Clinic_MST.sClinicName 'Practice Name' FROM Clinic_MST INNER JOIN Patient ON Clinic_MST.nClinicID = Patient.nClinicID Where Patient.nPatientId=" + nPatinetId.ToString(), oPatientDataTable)
            Return oPatientDataTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Function

    Private Sub SetHeight()
        'If pnlCCDMessage.Visible = True Then
        '    If (rbClinicalSummery.Checked = True) Then
        '        Me.Height = 630
        '    ElseIf (rbAmbulatorySummary.Checked = True) Then
        '        Me.Height = 480
        '    ElseIf (rbCareRecord.Checked = True) Then
        '        Me.Height = 580
        '    End If
        'Else
        '    If (rbClinicalSummery.Checked = True) Then
        '        Me.Height = 580
        '    ElseIf (rbAmbulatorySummary.Checked = True) Then
        '        Me.Height = 410
        '    ElseIf (rbCareRecord.Checked = True) Then
        '        Me.Height = 530
        '    End If
        'End If
        If pnlCCDMessage.Visible = True Then
            If (cmbSummaryType.SelectedIndex = ClinicalSummary) Then
                ' Me.Height = 543 '666
            ElseIf (cmbSummaryType.SelectedIndex = AmbulatorySummary) Then
                ' Me.Height = 543
            ElseIf (cmbSummaryType.SelectedIndex = careRecord) Then
                ' Me.Height = 543
            ElseIf (cmbSummaryType.SelectedIndex = careSummary) Then
                '  Me.Height = 543
            End If
        Else
            If (cmbSummaryType.SelectedIndex = ClinicalSummary) Then
                '   Me.Height = 543
            ElseIf (cmbSummaryType.SelectedIndex = AmbulatorySummary) Then
                '   Me.Height = 494
            ElseIf (cmbSummaryType.SelectedIndex = careRecord) Then
                '   Me.Height = 470
            ElseIf (cmbSummaryType.SelectedIndex = careSummary) Then
                '   Me.Height = 494
            End If
        End If
    End Sub

    Private Sub chkDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDate.CheckedChanged
        If chkDate.Checked = True Then
            dtpFrom.Enabled = True
            dtpToDate.Enabled = True
            chkintime.Enabled = True
        Else
            dtpFrom.Enabled = False
            dtpToDate.Enabled = False
            chkintime.Enabled = False
        End If
        dtpFrom.CustomFormat = "MM/dd/yyyy"
        dtpToDate.CustomFormat = "MM/dd/yyyy"
        chkintime.Checked = False
    End Sub

    Private Sub btnExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExam.Click
        If cmbSummaryType.SelectedIndex = AmbulatorySummary Then
            LoadUserGridExam()
            '  dgCustomGridselectExam.Label1.Visible = False
            '  dgCustomGridselectExam.txtsearch.Visible = True
            'dgCustomGridselectExam.Panel2.Visible = False
            pnlSelect.Visible = True
            pnlSelect.BringToFront()
            pnlToolStrip.Visible = False
            pnlExanDtl.Visible = False
            Panel1.Visible = False
            ' pnlSelect.Dock = Dock.Fill
        ElseIf cmbSummaryType.SelectedIndex = ClinicalSummary Then
            LoadUserGridExam()
            '  dgCustomGridselectExam.Label1.Visible = False
            '  dgCustomGridselectExam.txtsearch.Visible = True
            'dgCustomGridselectExam.Panel2.Visible = False
            pnlSelect.Visible = True
            pnlSelect.BringToFront()
            pnlToolStrip.Visible = False
            pnlExanDtl.Visible = False
            Panel1.Visible = False
            ' pnlSelect.Dock = Dock.Fill
        ElseIf cmbSummaryType.SelectedIndex = careRecord Then
            LoadUserGridOrder()
            pnlSelect.Visible = True
            pnlSelect.BringToFront()
            pnlToolStrip.Visible = False
            pnlExanDtl.Visible = False
            Panel1.Visible = False
        End If
    End Sub

    Private Sub chkCODemographic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCODemographic.CheckedChanged
        chkCODemographic.Checked = True
    End Sub

    Private Sub tblSave_Click(sender As System.Object, e As System.EventArgs) Handles tblSave.Click
        ''
        Dim bIsRestricted As Boolean = False
        ''

        Try

            bln_ExportCDAClicked = True
            If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
                MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
                ' MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Exit Sub
            End If

            Dim ogloCCDDBLayer As gloCCDDatabaseLayer = New gloCCDDatabaseLayer
            Dim _PatientLastName As String = ogloCCDDBLayer.GetPatientLastName(_nPatientId)
            Dim _PatientCode As String = ogloCCDDBLayer.GetPatientCode(_nPatientId)

            If IsNothing(ogloCCDDBLayer) = False Then
                ogloCCDDBLayer.Dispose()
                ogloCCDDBLayer = Nothing
            End If

            ''
            If sConfidentialityCode.ToLower() = "restricted" And chkPrivarySection.Checked And chkPrivaryText.Checked Then
                bIsRestricted = True
            End If
            ''
            Dim _objfrmencrypt As New frmCCDEncryption(gstrCCDFilePath, _nPatientId, _PatientLastName, "CDA", bIsRestricted, _PatientCode, cmbPurposeofUse.Text.Trim())

            If Me.CallFromSecureInbox Then
                _objfrmencrypt.IsAttachment = True
            End If

            _objfrmencrypt.SumCareType = cmbSummaryType.Text & " "

            _objfrmencrypt.ShowDialog(IIf(IsNothing(_objfrmencrypt.Parent), Me, _objfrmencrypt.Parent))

            If _objfrmencrypt._issave = True Then

                Dim ogloInterface As gloCCDInterface = New gloCCDLibrary.gloCCDInterface()

                Dim strFilepath As String = ""
                strFilepath = _objfrmencrypt.FilePath.ToString()
                strFilepath = GenerateCDA(strFilepath)

                If strFilepath <> "" Then


                    If _objfrmencrypt.IsSecureDocument(_objfrmencrypt.sEncryptKey) = True Then
                        strFilepath = CompressCCDFile(strFilepath, _objfrmencrypt.sEncryptKey)
                    End If
                    If IsNothing(ogloInterface) = False Then
                        ogloInterface.Dispose()
                        ogloInterface = Nothing
                    End If
                End If
                pnlPrintMessage.Visible = False
                Me.Cursor = Cursors.Default

                If CallFromSecureInbox Then
                    gloSurescriptSecureMessage.SecureMessageProperties.CCDAFilePath = strFilepath
                    Me.Close()
                End If

            End If
            _objfrmencrypt.Dispose()
            _objfrmencrypt = Nothing
        Finally
            bln_ExportCDAClicked = False
            bIsRestricted = False
        End Try

    End Sub

    Private Sub tblShowCCD_Click(sender As System.Object, e As System.EventArgs) Handles tblShowCCD.Click
        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
            MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
            'MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Exit Sub
        End If

        'pnlPrintMessage.Visible = True
        'Label24.Visible = True
        'Label24.BringToFront()
        'lblFormularyTransactionMessage.Visible = False
        'pnlPrintMessage.BringToFront()
        'Application.DoEvents()
        'Me.Cursor = Cursors.WaitCursor

        Dim strFilepath As String = ""
        strFilepath = GenerateCDA("")

        If strFilepath <> "" Then

            Dim objfrm As New frmCCDForm
            Dim ofile1 As FileInfo = New FileInfo(strFilepath)
            Dim myXslTransform As New Xml.Xsl.XslTransform()
            Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "yyyyMMddhhmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

            myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
            myXslTransform.Transform(strFilepath, _strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))
            objfrm.WebBrowser1.Navigate(_strfileName) 'System.IO.Path.Combine(gloSettings.FolderSettings.AppTempFolderPath, _strfileName))


            Me.Focus()
            objfrm.ShowInTaskbar = False
            objfrm.isCDA = True
            'objfrm.TopMost = True
            objfrm.ShowDialog(IIf(IsNothing(objfrm.Parent), Me, objfrm.Parent))
            objfrm.Close()
            If Not IsNothing(objfrm.WebBrowser1) Then
                objfrm.WebBrowser1.Dispose()
                objfrm.WebBrowser1 = Nothing
            End If
            objfrm.Dispose()
            objfrm = Nothing
        End If
        'Added for Auto Deleting CCDA files
        Try
            If Not IsNothing(strFilepath) Then
                If _isAutoDeleteCCDAFiles = True Then
                    File.Delete(strFilepath)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

        pnlPrintMessage.Visible = False
        Me.Cursor = Cursors.Default
        If sConfidentialityCode.ToLower() = "restricted" And (chkPrivarySection.Checked) And (chkPrivaryText.Checked) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Preview, "CCDA File Previewed: with privacy restrictions; for Purpose of Use: " & cmbPurposeofUse.Text & ".", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Else
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Preview, "CCDA File Previewed; for Purpose of Use: " & cmbPurposeofUse.Text & ".", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        End If
    End Sub

    Private Function CompressCCDFile(ByVal filePath As String, ByVal sEncryotKey As String) As String

        Dim _compressedFilePath As String = ""
        Dim _fileInfo As FileInfo

        Try

            If sEncryotKey.Trim() <> "" Then

                _compressedFilePath = gloSecurity.gloEncryption.PerformFileEncryption(filePath, sEncryotKey, True)

                _fileInfo = New FileInfo(_compressedFilePath)

                If _fileInfo.Exists = True Then

                    _fileInfo = New FileInfo(filePath)
                    _fileInfo.Delete()

                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return _compressedFilePath

    End Function

    'Public Function AddAdditionalContact(ByVal FullName As String, ByVal nContactID

    Private Sub cmbSummaryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSummaryType.SelectedIndexChanged

        'nExamId = 0
        'nOrderId = 0
        ' _nVisitID = 0 ''commented for bugid 110154
        '   _nExamID = 0
        lblDetails.Text = "Exam"
        If (nCDAFileType = CDAFileTypeEnum.ClinicalSummary) Then
            nOrderId = 0
        ElseIf (nCDAFileType = CDAFileTypeEnum.CareRecordSummary) Then
            nExamId = 0
        End If
        chkintime.Checked = False
        chkintime.Enabled = False
        SetUIForSelectedSummaryType()
        If (nCDAFileType = CDAFileTypeEnum.AmbulatorySummary) Then

            tblSendPortal.Visible = False
            tblSendPortalAMB.Visible = True

            If gblnPatientPortalEnabled = True Or gblnIntuitCommunication = True Then
                tblSendPortalAMBPortal.Visible = True
                tblSendPortalAMBAPI.Visible = True
                tblSendPortalAMBBoth.Visible = True
            Else
                tblSendPortalAMBPortal.Visible = False
                tblSendPortalAMBAPI.Visible = True
                tblSendPortalAMBBoth.Visible = False
            End If

        ElseIf (nCDAFileType = CDAFileTypeEnum.ClinicalSummary) Then

            If gblnPatientPortalEnabled = True Or gblnIntuitCommunication = True Then
                tblSendPortal.Visible = True
            Else
                tblSendPortal.Visible = False
            End If
            tblSendPortalAMB.Visible = False
        Else
            tblSendPortal.Visible = False
            tblSendPortalAMB.Visible = False

        End If

        If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then
            chkOvrAdminSettings.Visible = True
        ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
            chkOvrAdminSettings.Visible = True
        Else
            chkOvrAdminSettings.Visible = False
        End If

        If rbSelectAllNormal.Checked Then
            SelectAll()
        ElseIf rbSelectAllRestricted.Checked Then
            SelectAll()
        ElseIf rbClearAll.Checked Then
            UnSelectAll()
        End If

    End Sub

    Private Sub SetUIForSelectedSummaryType()

        Try
            ChkCOAssessments.Enabled = True

            ChkCOTreatmentPlan.Enabled = True
            ''
            If chkPrivarySection.Checked Then
                '     pnlCommonMUData.Height = 270
                '    Me.Height = Me.Height + 25
            Else
                '  pnlCommonMUData.Height = 220
                '  Me.Height = Me.Height - 25
            End If

            If (cmbSummaryType.SelectedIndex = ClinicalSummary) Then
                ''pnlConfidentialityCode.Visible = False
                chkDate.Checked = False
                pnlCCDMessage.Visible = False
                pnlExanDtl.Visible = True
                If nExamId = 0 Then
                    lblDetails.Text = "Exam"
                Else
                    lblDetails.Text = sDetail
                End If
                pnlClinicalSummary.Visible = True
                pnlAmbulatorySummary.Visible = False
                pnlTransitionCareRecord.Visible = False
                pnlCareSummary.Visible = False
                pnlFormDate.Visible = False
                tblSave.Visible = True
                tlbbtn_Print.Visible = True


                '07-Feb-14 Aniket: Show 'Send to Portal' button only if Patient Portal is enabled
                If gblnPatientPortalEnabled = True Or gblnIntuitCommunication = True Then
                    tblSendPortal.Visible = True
                Else
                    tblSendPortal.Visible = False
                End If

                tlbbtn_Email.Visible = False
                nCDAFileType = CDAFileTypeEnum.ClinicalSummary
                'SelectAll()

                SetHeight()
                pnlCCDMessage.Visible = False
                chkPatientCopy.Checked = True
                btnExam.Tag = "Browse Exam"
                ''SetCheckboxFromSetting(1)
                ''CheckAllNew()
            ElseIf (cmbSummaryType.SelectedIndex = AmbulatorySummary) Then
                ''pnlConfidentialityCode.Visible = False
                chkDate.Checked = False
                pnlCCDMessage.Visible = False
                pnlClinicalSummary.Visible = False
                pnlAmbulatorySummary.Visible = True
                pnlTransitionCareRecord.Visible = False
                pnlCareSummary.Visible = False
                pnlFormDate.Visible = True
                tblSave.Visible = True
                tlbbtn_Print.Visible = True
                tblSendPortal.Visible = False
                tlbbtn_Email.Visible = False
                nCDAFileType = CDAFileTypeEnum.AmbulatorySummary
                '    SelectAll()
                If nExamId = 0 Then  ''added for bugid   110154
                    lblDetails.Text = "Exam"
                Else
                    lblDetails.Text = sDetail
                End If
                SetHeight()
                pnlExanDtl.Visible = True
                chkPatientCopy.Checked = False
                ''SetCheckboxFromSetting(2)
                ''CheckAllNew()
            ElseIf (cmbSummaryType.SelectedIndex = careRecord) Then
                ''pnlConfidentialityCode.Visible = True
                chkDate.Checked = False
                pnlExanDtl.Visible = True ''
                If nOrderId = 0 Or _nExamID <> 0 Then  ''added or condition for bugid   110154
                    lblDetails.Text = "Order"
                Else
                    lblDetails.Text = sDetail
                End If
                pnlCCDMessage.Visible = False
                pnlClinicalSummary.Visible = False
                pnlAmbulatorySummary.Visible = False
                pnlTransitionCareRecord.Visible = True
                pnlCareSummary.Visible = False

                pnlFormDate.Visible = True
                tblSave.Visible = True
                tlbbtn_Print.Visible = True
                tblSendPortal.Visible = False
                chkPatientCopy.Checked = False
                nCDAFileType = CDAFileTypeEnum.CareRecordSummary

                '31-Oct-17 Aniket: Resolving Bug #109223: CCDA>>provider direct message is not shown on summary of care record and exam label is shown instead of order
                If Not CallFromSecureInbox Then
                    If gblnIsSecureMsgEnable = True Then
                        tlbbtn_Email.Visible = True
                    Else
                        tlbbtn_Email.Visible = False
                    End If
                Else
                    tlbbtn_Email.Visible = False
                End If
                CheckSummaryCareRecord()
                'SelectAll()
                'CheckAllNew()
                SetHeight()

            ElseIf (cmbSummaryType.SelectedIndex = careSummary) Then
                ''pnlConfidentialityCode.Visible = True
                ChkCOAssessments.Checked = False
                ChkCOAssessments.Enabled = False
                ChkCOTreatmentPlan.Checked = False
                ChkCOTreatmentPlan.Enabled = False
                chkDate.Checked = False
                pnlCCDMessage.Visible = False
                pnlClinicalSummary.Visible = False
                pnlAmbulatorySummary.Visible = False
                pnlTransitionCareRecord.Visible = False
                pnlCareSummary.Visible = True
                pnlFormDate.Visible = True
                tblSave.Visible = True
                tlbbtn_Print.Visible = True
                tblSendPortal.Visible = False
                chkPatientCopy.Checked = False
                pnlExanDtl.Visible = False
                'If Not CallFromSecureInbox Then
                '    If gblnIsSecureMsgEnable = True Then
                '        tlbbtn_Email.Visible = True
                '    Else
                '        tlbbtn_Email.Visible = False
                '    End If
                'Else
                '    tlbbtn_Email.Visible = False
                'End If

                '31-Oct-17 Aniket: Resolving Bug #109223: CCDA>>provider direct message is not shown on summary of care record and exam label is shown instead of order
                tlbbtn_Email.Visible = False
                nCDAFileType = CDAFileTypeEnum.CarePlan
                ''SelectAll()
                'SetHeight()
                ' pnlExanDtl.Visible = True

            End If

            If rbSelectAllNormal.Checked Then
                SelectAll()
            ElseIf rbSelectAllRestricted.Checked Then
                SelectAll()
            ElseIf rbClearAll.Checked Then
                UnSelectAll()
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

#Region "Exam and Order Selector"



    Private Sub LoadUserGridExam()
        Try


            AddControlExam()
            If Not IsNothing(dgCustomGridselectExam) Then
                dgCustomGridselectExam.Visible = True
                dgCustomGridselectExam.Width = pnlSelect.Width
                dgCustomGridselectExam.Height = pnlSelect.Height
                dgCustomGridselectExam.txtsearch.Width = 120
                '  dgCustomGridselectExam.txtsearch.Width = 120
                dgCustomGridselectExam.SetVisible = False
                dgCustomGridselectExam.BringToFront()
                BindUserGridExam()
                dgCustomGridselectExam.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RemoveControlExam()
        If Not IsNothing(dgCustomGridselectExam) Then
            pnlSelect.Visible = False
            pnlToolStrip.Visible = True
            pnlExanDtl.Visible = True
            Panel1.Visible = True
            pnlSelect.Controls.Remove(dgCustomGridselectExam)
            dgCustomGridselectExam.Visible = False
            dgCustomGridselectExam.Dispose()
            dgCustomGridselectExam = Nothing
        End If
    End Sub
    Private Sub AddControlExam()

        If Not IsNothing(dgCustomGridselectExam) Then
            RemoveControlExam()
        End If
        dgCustomGridselectExam = New CustomTask
        dgCustomGridselectExam.Dock = DockStyle.Fill
        pnlSelect.Controls.Add(dgCustomGridselectExam)
        pnlSelect.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''

        ' pnlSelectExam.Location = New Point(500, pnlSelectExam.Location.Y)
        pnlSelect.Visible = True
        dgCustomGridselectExam.Visible = True
        pnlSelect.BringToFront()
        dgCustomGridselectExam.BringToFront()

    End Sub

    Private Sub BindUserGridExam()
        Try
            Dim dt As DataTable
            Dim objPatientDetail As New clsPatientDetails

            dt = objPatientDetail.Fill_PastExams(_nPatientId)
            objPatientDetail.Dispose()
            objPatientDetail = Nothing
            CustomDrugsGridStyleExam()

            If Not IsNothing(dt) Then
                dgCustomGridselectExam.datasource(dt.DefaultView)
            End If

            Dim _width As Single = dgCustomGridselectExam.C1Task.Width - 5

            dgCustomGridselectExam.C1Task.ShowCellLabels = True
            dgCustomGridselectExam.C1Task.AllowEditing = False
            dgCustomGridselectExam.C1Task.Cols(Col_eExamID).Visible = False
            dgCustomGridselectExam.C1Task.Cols(Col_eVistitID).Visible = False
            dgCustomGridselectExam.C1Task.Cols(Col_eDos).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eExamName).Width = _width * 0.24
            dgCustomGridselectExam.C1Task.Cols(Col_eTemplateName).Width = _width * 0.15
            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eProviderName).Width = _width * 0.2
            dgCustomGridselectExam.C1Task.Cols(Col_eFinished).Width = _width * 0.1
            dgCustomGridselectExam.C1Task.Cols(Col_eSpeciality).Width = _width * 0.11

            '31-May-16 Aniket: Resolving Bug #95883: CDA -> No spacing in grid headers
            dgCustomGridselectExam.C1Task.Cols(Col_eProviderName).Caption = "Provider Name"
            dgCustomGridselectExam.C1Task.Cols(Col_eReviewedBy).Caption = "Reviewed By"

        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub CustomDrugsGridStyleExam()

        Dim _TotalWidth As Single = dgCustomGridselectExam.C1Task.Width - 5


        With dgCustomGridselectExam.C1Task
            .Redraw = False
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_eCount
            .AllowEditing = False


            .Cols(Col_eExamID).Visible = False
            .Cols(Col_eVistitID).Visible = False
            .Cols(Col_eDos).Width = _TotalWidth * 0.1
            .Cols(Col_eExamName).Width = _TotalWidth * 0.24
            .Cols(Col_eTemplateName).Width = _TotalWidth * 0.15
            .Cols(Col_eReviewedBy).Width = _TotalWidth * 0.1
            .Cols(Col_eProviderName).Width = _TotalWidth * 0.2
            .Cols(Col_eFinished).Width = _TotalWidth * 0.1
            .Cols(Col_eSpeciality).Width = _TotalWidth * 0.11
            .Redraw = True

        End With

    End Sub





    Private Sub LoadUserGridOrder()
        Try


            AddControlOrder()
            If Not IsNothing(dgCustomGridselectOrder) Then
                dgCustomGridselectOrder.Visible = True
                dgCustomGridselectOrder.Width = pnlSelect.Width
                dgCustomGridselectOrder.Height = pnlSelect.Height
                dgCustomGridselectOrder.txtsearch.Width = 120
                '  dgCustomGridselectExam.txtsearch.Width = 120
                dgCustomGridselectOrder.SetVisible = False
                dgCustomGridselectOrder.BringToFront()
                BindUserGridOrder()
                dgCustomGridselectOrder.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RemoveControlOrder()
        If Not IsNothing(dgCustomGridselectExam) Then
            pnlSelect.Visible = False
            pnlToolStrip.Visible = True
            pnlExanDtl.Visible = True
            Panel1.Visible = True
            pnlSelect.Controls.Remove(dgCustomGridselectExam)
            dgCustomGridselectExam.Visible = False
            dgCustomGridselectExam.Dispose()
            dgCustomGridselectExam = Nothing
        End If
    End Sub
    Private Sub AddControlOrder()

        If Not IsNothing(dgCustomGridselectOrder) Then
            RemoveControlOrder()
        End If
        dgCustomGridselectOrder = New CustomTask
        dgCustomGridselectOrder.Dock = DockStyle.Fill
        pnlSelect.Controls.Add(dgCustomGridselectOrder)
        pnlSelect.BringToFront()

        Dim y As Int64
        Dim x As Int64
        x = 300
        y = 250
        ''''''''''''''''''''''

        ' pnlSelectExam.Location = New Point(500, pnlSelectExam.Location.Y)
        pnlSelect.Visible = True
        dgCustomGridselectOrder.Visible = True
        pnlSelect.BringToFront()
        dgCustomGridselectOrder.BringToFront()

    End Sub
    Private Sub BindUserGridOrder()
        Try
            Dim dt As DataTable
            Dim objlaborder As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder
            dt = objlaborder.GetOrderList(_nPatientId, "", "", "all", 0)
            objlaborder.Dispose()
            objlaborder = Nothing
            '  DesignGrid()
            'dgCustomGridselectOrder.C1Task.Clear()
            dgCustomGridselectOrder.C1Task.DataSource = Nothing
            dgCustomGridselectOrder.C1Task.Clear()

            dgCustomGridselectOrder.C1Task.Cols.Count = COL_COUNT
            dgCustomGridselectOrder.C1Task.Cols.Fixed = 0
            dgCustomGridselectOrder.C1Task.Rows.Count = 1

            If Not IsNothing(dt) Then
                dgCustomGridselectOrder.datasource(dt.DefaultView)
            End If
            CustomDrugsGridStyleOrder()




        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then

                blnCheck = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        Return blnCheck
    End Function


    Private Sub CustomDrugsGridStyleOrder()

        'dgCustomGridselectOrder.C1Task.AllowEditing = False
        ' set visibility of column
        dgCustomGridselectOrder.C1Task.ShowCellLabels = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERID).Visible = False
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPREFIX).Visible = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERNO).Visible = False
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPROVIDERID).Visible = False
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPROVIDERNAME).Visible = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERSTATUS).Visible = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERVISITID).Visible = False
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERIsClosed).Visible = False
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERDate).Visible = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERIsAck).Visible = True
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERIsResult).Visible = True
        dgCustomGridselectOrder.C1Task.ExtendLastCol = True

        Dim _width As Single = dgCustomGridselectOrder.C1Task.Width - 5

        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPREFIX).Width = _width * 0.2
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERNO).Width = 0
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPROVIDERNAME).Width = _width * 0.2
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERSTATUS).Width = _width * 0.2
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERDate).Width = _width * 0.18
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERIsAck).Width = _width * 0.15
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERIsResult).Width = _width * 0.1

        '31-May-16 Aniket: Resolving Bug #95883: CDA -> No spacing in grid headers
        dgCustomGridselectOrder.C1Task.Cols(COL_ORDERPROVIDERNAME).Caption = "Provider Name"
    End Sub

#End Region

    Private Sub dgCustomGridselectExam_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.CloseClick
        dgCustomGridselectExam.Visible = False
        pnlSelect.Visible = False
        pnlToolStrip.Visible = True
        pnlExanDtl.Visible = True
        Panel1.Visible = True
        ' pnlExanDtl.Visible = False

    End Sub

    Private Sub dgCustomGridselectExam_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.OKClick
        Try


            Dim _ExamID As String
            Dim _ExamVisitID As String
            Dim _ExamDtl As String
            _ExamID = System.Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eExamID))
            _ExamVisitID = System.Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eVistitID))
            _ExamDtl = dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eDos) & " - " & System.Convert.ToString(dgCustomGridselectExam.C1Task.GetData(dgCustomGridselectExam.C1Task.Row, Col_eExamName))
            _nExamID = _ExamID
            sDetail = _ExamDtl
            _nVisitID = _ExamVisitID
            pnlExanDtl.Visible = True
            lblDetails.Text = sDetail
        Catch ex As Exception

        Finally
            pnlSelect.Visible = False
            pnlToolStrip.Visible = True
            pnlExanDtl.Visible = True
            Panel1.Visible = True
        End Try
    End Sub

    Private Sub dgCustomGridselectExam_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectExam.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGridselectExam.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGridselectExam.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGridselectExam.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(Col_eExamName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eTemplateName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eProviderName).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(Col_eSpeciality).ColumnName & "] Like '%" & strPatientSearchDetails & "%' "

            dgCustomGridselectExam.Enabled = False
            dgCustomGridselectExam.datasource(dvPatient)
            dgCustomGridselectExam.Enabled = True
            Me.Cursor = Cursors.Default




            dgCustomGridselectExam.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dgCustomGridselectOrder_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectOrder.CloseClick
        dgCustomGridselectOrder.Visible = False
        pnlSelect.Visible = False
        pnlToolStrip.Visible = True
        pnlExanDtl.Visible = True
        Panel1.Visible = True
    End Sub

    Private Sub dgCustomGridselectOrder_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectOrder.OKClick
        Try


            Dim _OrderID As String
            Dim _OrderVisitID As String
            Dim _OrderDtl As String
            _OrderID = System.Convert.ToString(dgCustomGridselectOrder.C1Task.GetData(dgCustomGridselectOrder.C1Task.Row, COL_ORDERID))
            _OrderVisitID = System.Convert.ToString(dgCustomGridselectOrder.C1Task.GetData(dgCustomGridselectOrder.C1Task.Row, COL_ORDERVISITID))
            _OrderDtl = dgCustomGridselectOrder.C1Task.GetData(dgCustomGridselectOrder.C1Task.Row, COL_ORDERDate) & " - " & System.Convert.ToString(dgCustomGridselectOrder.C1Task.GetData(dgCustomGridselectOrder.C1Task.Row, COL_ORDERPREFIX))
            _nOrderID = _OrderID
            sDetail = _OrderDtl
            _nVisitID = _OrderVisitID
            pnlExanDtl.Visible = True
            lblDetails.Text = sDetail
        Catch ex As Exception

        Finally
            pnlSelect.Visible = False
            pnlToolStrip.Visible = True
            pnlExanDtl.Visible = True
            Panel1.Visible = True
        End Try
    End Sub

    Private Sub dgCustomGridselectOrder_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGridselectOrder.SearchChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As New DataView()
            dvPatient = CType(dgCustomGridselectOrder.C1Task.DataSource(), DataView) '' (CType(dt.DefaultView, DataView))
            If IsNothing(dvPatient) Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim strPatientSearchDetails As String
            If Trim(dgCustomGridselectOrder.txtsearch.Text) <> "" Then
                strPatientSearchDetails = Replace(dgCustomGridselectOrder.txtsearch.Text, "'", "''")
                strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
            Else
                strPatientSearchDetails = ""
            End If

            dvPatient.RowFilter = "[" & dvPatient.Table.Columns(COL_ORDERIsAck).ColumnName & "] Like '%" & strPatientSearchDetails & "%'  OR [" & dvPatient.Table.Columns(COL_ORDERPREFIX).ColumnName & "] Like '%" & strPatientSearchDetails & "%'  OR [" & dvPatient.Table.Columns(COL_ORDERIsResult).ColumnName & "] Like '%" & strPatientSearchDetails & "%' OR [" & dvPatient.Table.Columns(COL_ORDERSTATUS).ColumnName & "] Like '%" & strPatientSearchDetails & "%'  OR [" & dvPatient.Table.Columns(COL_ORDERPROVIDERNAME).ColumnName & "] Like '%" & strPatientSearchDetails & "%'  "

            dgCustomGridselectOrder.Enabled = False
            dgCustomGridselectOrder.datasource(dvPatient)
            dgCustomGridselectOrder.Enabled = True
            Me.Cursor = Cursors.Default



            dgCustomGridselectOrder.txtsearch.Focus()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#Region "Call From Secure Message Inbox"
    Private bCallFromSecureInbox As Boolean = False
    Public Property CallFromSecureInbox() As Boolean
        Get
            Return bCallFromSecureInbox
        End Get
        Set(ByVal value As Boolean)
            bCallFromSecureInbox = value

            If value Then
                Me.tblSave.Text = "Attach CDA"
                Me.tblSave.ToolTipText = Me.tblSave.Text

                Me.Text = "Attach CDA"
            End If
        End Set
    End Property
#End Region

    Private Sub btnMouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExam.MouseHover
        Try
            If sender IsNot Nothing Then
                DirectCast(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_ButtonHover
                DirectCast(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
                If (cmbSummaryType.SelectedIndex = ClinicalSummary) Then
                    tlTooltip.SetToolTip(btnExam, "Browse Exam")
                Else
                    tlTooltip.SetToolTip(btnExam, "Browse Order")
                End If
            End If
        Catch ex As Exception
            'Blank Catch
        End Try

    End Sub
    Private Sub btnMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExam.MouseLeave
        Try
            If sender IsNot Nothing Then
                DirectCast(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_LongButton
                DirectCast(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If
        Catch ex As Exception
            'Blank Catch
        End Try
    End Sub

    ''Bug #72854: CDA -> Default Checkbox Selection is not consistent for "Referral Summary/Summary of care Records" 
    ''Changes to resolve the bug
    '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
    Private Sub Fill_ConfidentialityCodess()
        With CmbConfidentialityCode
            .Items.Clear()
            Dim clConfidentialityCodes As New Collection
            clConfidentialityCodes = Fill_ConfidentialityCodes()

            Dim nCount As Int16
            For nCount = 1 To clConfidentialityCodes.Count
                .Items.Add(clConfidentialityCodes.Item(nCount).ToString.Trim)
            Next
            .SelectedIndex = 0
        End With
    End Sub

    Private Function Fill_ConfidentialityCodes() As Collection

        Dim clConfidentialityCodes As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_getConfidentialityCodes"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            While objSQLDataReader.Read
                clConfidentialityCodes.Add(objSQLDataReader.Item(0))
                ''CodeSystem.sconfidentialityCodesystem = objSQLDataReader.Item(1)
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader = Nothing

            Return clConfidentialityCodes
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function



    '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End

    Dim _strDefaultClinicalSummary(0) As String
    Dim _strDefaultAmbulatorySummary(0) As String


    Private Sub GetSettingsforCDA()
        Dim oclsSettings As New clsSettings
        Dim _dt As DataTable = Nothing
        Try
            _dt = oclsSettings.GetSetting("CCDAClinicalSummarySections")
            If IsNothing(_dt) = False Then
                If _dt.Rows.Count > 0 Then
                    If _dt.Rows(0)("sSettingsValue") <> "" Then
                        _strDefaultClinicalSummary = System.Convert.ToString(_dt.Rows(0)("sSettingsValue")).Trim.Split(",")
                    End If
                End If
            End If

            _dt = oclsSettings.GetSetting("CCDAAmbulatorySummarySections")
            If IsNothing(_dt) = False Then
                If _dt.Rows.Count > 0 Then
                    If _dt.Rows(0)("sSettingsValue") <> "" Then
                        _strDefaultAmbulatorySummary = System.Convert.ToString(_dt.Rows(0)("sSettingsValue")).Trim.Split(",")
                    End If
                End If
            End If
            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - Start
            _dt = oclsSettings.GetSetting("cdaprivacytext")
            If IsNothing(_dt) = False Then
                If _dt.Rows.Count > 0 Then
                    If _dt.Rows(0)("sSettingsValue") <> "" Then
                        gloLibCCDGeneral.sCDAPrivacyText = System.Convert.ToString(_dt.Rows(0)("sSettingsValue")).Trim()
                    End If
                End If
            End If
            _dt = oclsSettings.GetSetting("cdaprivacytitle")
            If IsNothing(_dt) = False Then
                If _dt.Rows.Count > 0 Then
                    If _dt.Rows(0)("sSettingsValue") <> "" Then
                        gloLibCCDGeneral.sCDAPrivacyTitle = System.Convert.ToString(_dt.Rows(0)("sSettingsValue")).Trim()
                    End If
                End If
            End If
            '' Added by Ujwala for certification criteria - 170.315(b)(7) Data Segmentation for Privacy – Send - End
        Catch ex As Exception
        Finally
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If

        End Try

    End Sub

    ''Bug #72854: CDA -> Default Checkbox Selection is not consistent for "Referral Summary/Summary of care Records" 
    ''Changes to resolve the bug
    Private Sub SetCheckboxFromSetting(Optional ByVal nSettingID As Integer = 0)
        If nSettingID = CDAFileTypeEnum.ClinicalSummary Then
            If Not IsNothing(_strDefaultClinicalSummary) Then
                UnSelectAll()
                checkCCDASections(_strDefaultClinicalSummary, "ClinicalCCDA")
                ''CheckALL(1)
            End If
        ElseIf nSettingID = CDAFileTypeEnum.AmbulatorySummary Then
            If Not IsNothing(_strDefaultAmbulatorySummary) Then
                UnSelectAll()
                checkCCDASections(_strDefaultAmbulatorySummary, "AmbulatoryCCDA")
                ''CheckALL(2)
            End If
        End If
    End Sub
    Private Sub CheckALL(Optional ByVal nSettingID As Integer = 0)
        ''Dim cb As Boolean = False
        ''If nSettingID = CDAFileTypeEnum.ClinicalSummary Then
        ''    For Each c As Control In pnlClinicalSummary.Controls
        ''        If CType(c, CheckBox).Checked Then
        ''            cb = True
        ''        Else
        ''            cb = False
        ''            Exit For
        ''        End If
        ''    Next

        ''ElseIf nSettingID = CDAFileTypeEnum.AmbulatorySummary Then
        ''    For Each c As Control In pnlAmbulatorySummary.Controls
        ''        If CType(c, CheckBox).Checked Then
        ''            cb = True
        ''        Else
        ''            cb = False
        ''            Exit For
        ''        End If

        ''    Next
        ''End If

        ''If cb = True Then
        ''    '     ChkAll.Checked = True
        ''End If
    End Sub
    Public Sub checkCCDASections(ByVal SelectedCCDA As String(), ByVal _CCDASettingsname As String)


        Dim _Section As Int16

        For _Section = 0 To SelectedCCDA.Length - 1
            If SelectedCCDA(_Section) = chkCODemographic.Tag Then
                chkCODemographic.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOProblems.Tag Then
                chkCOProblems.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOAllergy.Tag Then
                chkCOAllergy.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOCareTeamMem.Tag Then
                chkCOCareTeamMem.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOProcedures.Tag Then
                chkCOProcedures.Checked = True

            ElseIf SelectedCCDA(_Section) = chkCOVitalSigns.Tag Then
                chkCOVitalSigns.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOlabResult.Tag Then
                chkCOlabResult.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOLabTest.Tag Then
                chkCOLabTest.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOMedication.Tag Then
                chkCOMedication.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCSClinicalInstru.Tag Then
                chkCSClinicalInstru.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOSocialHistory.Tag Then
                chkCOSocialHistory.Checked = True
            ElseIf SelectedCCDA(_Section) = chkImplant.Tag Then
                chkImplant.Checked = True
            ElseIf SelectedCCDA(_Section) = ChkCOAssessments.Tag Then
                ChkCOAssessments.Checked = True
            ElseIf SelectedCCDA(_Section) = ChkCOTreatmentPlan.Tag Then
                ChkCOTreatmentPlan.Checked = True
            ElseIf SelectedCCDA(_Section) = ChkCOGoals.Tag Then
                ChkCOGoals.Checked = True
            ElseIf SelectedCCDA(_Section) = ChkCOHealthConcerns.Tag Then
                ChkCOHealthConcerns.Checked = True
            ElseIf SelectedCCDA(_Section) = chkCOFamilyHistory.Tag Then
                chkCOFamilyHistory.Checked = True
            End If
        Next

        If _CCDASettingsname = "ClinicalCCDA" Then
            For _Section = 0 To SelectedCCDA.Length - 1
                If SelectedCCDA(_Section) = chkCSProviderName.Tag Then
                    chkCSProviderName.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSFutureAppt.Tag Then
                    chkCSFutureAppt.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSOfcContact.Tag Then
                    chkCSOfcContact.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSRefOtrProvider.Tag Then
                    chkCSRefOtrProvider.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitInfo.Tag Then
                    chkCSVisitInfo.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSDecisionAids.Tag Then
                    chkCSDecisionAids.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitMedications.Tag Then
                    chkCSVisitMedications.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitImmunization.Tag Then
                    chkCSVisitImmunization.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSDigTestPending.Tag Then
                    chkCSDigTestPending.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSFutureTest.Tag Then
                    chkCSFutureTest.Checked = True
                ElseIf SelectedCCDA(_Section) = chkCSVisitReason.Tag Then
                    chkCSVisitReason.Checked = True
                End If
            Next
        ElseIf _CCDASettingsname = "AmbulatoryCCDA" Then
            For _Section = 0 To SelectedCCDA.Length - 1
                If SelectedCCDA(_Section) = chkAmbImmunization.Tag Then
                    chkAmbImmunization.Checked = True
                ElseIf SelectedCCDA(_Section) = chkAmbProviderContact.Tag Then
                    chkAmbProviderContact.Checked = True
                ElseIf SelectedCCDA(_Section) = chkAmbProviderName.Tag Then
                    chkAmbProviderName.Checked = True
                ElseIf SelectedCCDA(_Section) = chkambDatelocationvisit.Tag Then
                    chkambDatelocationvisit.Checked = True
                ElseIf SelectedCCDA(_Section) = chkambEncounters.Tag Then
                    chkambEncounters.Checked = True

                ElseIf SelectedCCDA(_Section) = ChkAmbFunctionalStatus.Tag Then
                    ChkAmbFunctionalStatus.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbMental.Tag Then
                    ChkAmbMental.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbReasonReferral.Tag Then
                    ChkAmbReasonReferral.Checked = True
                ElseIf SelectedCCDA(_Section) = ChkAmbReferring.Tag Then
                    ChkAmbReferring.Checked = True

                End If
            Next

        End If
    End Sub



    Private Sub tsb_Fax_Click(sender As System.Object, e As System.EventArgs) Handles tsb_Fax.Click
        Try

            ''added for Fax functionality from CDA 
            Me.Cursor = Cursors.WaitCursor
            If nCDAFileType = CDAFileTypeEnum.ClinicalSummary AndAlso lblDetails.Text = "Exam" Then
                MessageBox.Show("No exam selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf nCDAFileType = CDAFileTypeEnum.CareRecordSummary AndAlso lblDetails.Text = "Order" Then
                MessageBox.Show("No order selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If


            Dim strFilepath As String = ""
            Dim strPdfFilepath As String = ""
            strFilepath = GenerateCDA(strFilepath)

            If strFilepath <> "" Then
                Dim ofile As FileInfo = New FileInfo(strFilepath)

                Dim myXslTransform As New Xml.Xsl.XslTransform()
                Dim _strfileName As String = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".html", "MMddyyyyHHmmssffff") 'DateTime.Now.ToString("yyyyMMddhhmmssffff") & System.Guid.NewGuid().ToString() & ".html"

                '  Dim _strfileName As String = DateTime.Now.ToString("yyyyMMddhhmmssffff") & ".html"
                myXslTransform.Load(Application.StartupPath & "/gloCCDAcss_MU2.xsl")
                myXslTransform.Transform(strFilepath, _strfileName)

                'SLR: Changed to support office 2007 also.
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Dim objWdDoc As Word.Document = myLoadWord.LoadWordApplication(_strfileName)
                Try
                    If (IsNothing(objWdDoc) = False) Then
                        Dim thisAlertLevel As Microsoft.Office.Interop.Word.WdAlertLevel = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsAll
                        Try
                            thisAlertLevel = objWdDoc.Application.DisplayAlerts
                            objWdDoc.Application.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone
                        Catch ex As Exception

                        End Try

                        strPdfFilepath = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".pdf", "MMddyyyyHHmmssffff")
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
                myLoadWord = Nothing


                Dim objfrmEdocFax As New frmEDocEvent_Fax()
                objfrmEdocFax.pdfFileName = strPdfFilepath
                objfrmEdocFax.OpenFromCDA = True
                objfrmEdocFax.PatientID = _nPatientId
                objfrmEdocFax.oClinicID = gnClinicID
                objfrmEdocFax.ShowDialog(Me) ''added for Bug #73909 fax window dissapear when click on off side of window
                objfrmEdocFax.Dispose()
                objfrmEdocFax = Nothing
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Fax, "CCDA file Faxed.", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                'Added for Auto Deleting CCDA files
                Try
                    If Not IsNothing(strFilepath) Then
                        If _isAutoDeleteCCDAFiles = True Then
                            File.Delete(strFilepath)
                        End If
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
                End Try
            End If
        Catch ex As Exception
        Finally
            pnlPrintMessage.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        For Each frm As Form In Application.OpenForms
            If frm.Name = "frmEDocEvent_Fax" Then
                ' frm.Navigate(strstring)
                DirectCast(frm, gloEDocumentV3.Forms.frmEDocEvent_Fax).Navigate(strstring)
            End If
        Next
    End Sub

    Private Sub chkAmbImmunization_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAmbImmunization.CheckedChanged, chkAmbProviderContact.CheckedChanged, chkAmbProviderName.CheckedChanged, _
        ChkCareOfficeContact.CheckedChanged, ChkCareProvider.CheckedChanged, chkCOAllergy.CheckedChanged, chkCOCareTeamMem.CheckedChanged, chkCOFamilyHistory.CheckedChanged, _
        chkCOlabResult.CheckedChanged, chkCOLabTest.CheckedChanged, chkCOMedication.CheckedChanged, chkCOProblems.CheckedChanged, chkCOProcedures.CheckedChanged, chkCOSmoking.CheckedChanged, _
        chkCOSocialHistory.CheckedChanged, chkCOVitalSigns.CheckedChanged, chkCSClinicalInstru.CheckedChanged, chkCSDecisionAids.CheckedChanged, chkCSDigTestPending.CheckedChanged, _
        chkCSFutureAppt.CheckedChanged, chkCSFutureTest.CheckedChanged, chkCSOfcContact.CheckedChanged, chkCSProviderName.CheckedChanged, chkCSRefOtrProvider.CheckedChanged, _
        chkCSVisitImmunization.CheckedChanged, chkCSVisitInfo.CheckedChanged, chkCSVisitMedications.CheckedChanged, chkTransCareCognitiveStat.CheckedChanged, chkTransCareEncounter.CheckedChanged, _
        chkTransCareFunctionalStat.CheckedChanged, chkTransCareImmunization.CheckedChanged, chkTransCareRefProvider.CheckedChanged, chkTransCareResReferral.CheckedChanged, chkImplant.CheckedChanged, _
        ChkCOAssessments.CheckedChanged, ChkCOGoals.CheckedChanged, ChkCOTreatmentPlan.CheckedChanged, ChkCOHealthConcerns.CheckedChanged, chkambEncounters.CheckedChanged, chkambDatelocationvisit.CheckedChanged, _
        ChkAmbMental.CheckedChanged, ChkAmbFunctionalStatus.CheckedChanged, ChkAmbReferring.CheckedChanged, ChkAmbReasonReferral.CheckedChanged, chktransDateLocationvisit.CheckedChanged

        '16-Oct-17 Aniket: Resolving Bug #109696: gloEMR >> Care Team Section is default after preview, although we have Unticked the "Care Team Member(s)" section.
        chkCOCareTeamMem.Checked = True

        CheckSelection()
    End Sub


    Private Sub CmbConfidentialityCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbConfidentialityCode.SelectedIndexChanged
        gloLibCCDGeneral.sConfidentialityCode = CmbConfidentialityCode.Text

        If CmbConfidentialityCode.Text.ToLower() = "restricted" Then
            chkPrivarySection.Checked = True
            chkPrivaryText.Checked = True
        Else
            chkPrivarySection.Checked = False
            chkPrivaryText.Checked = False
        End If

    End Sub

    Private Sub chkPrivaryText_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPrivaryText.CheckedChanged
        gloLibCCDGeneral.bIncludePrivacyText = chkPrivaryText.Checked
    End Sub

    Private Sub chkPrivarySection_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPrivarySection.CheckedChanged
        gloLibCCDGeneral.bIncludePrivacySection = chkPrivarySection.Checked
        chkPrivaryText.Visible = chkPrivarySection.Checked
        If chkPrivarySection.Checked Then
            '   pnlCommonMUData.Height = 250
            '   Me.Height = Me.Height + 25
        Else
            '  pnlCommonMUData.Height = 220
            '  Me.Height = Me.Height - 25
        End If
    End Sub

    Private Sub chkintime_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkintime.CheckedChanged
        If (chkintime.Checked = True) Then
            dtpFrom.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
            dtpToDate.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Else
            dtpFrom.CustomFormat = "MM/dd/yyyy"
            dtpToDate.CustomFormat = "MM/dd/yyyy"
        End If
    End Sub

    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(nProviderID)
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult

                nProviderAssociationID = System.Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = System.Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function
    Private Sub CheckAllNew()
        ''If rbSelectAllNormal.Checked Then
        ''    SelectAll()
        ''ElseIf rbSelectAllRestricted.Checked Then
        ''    SelectAll()
        ''ElseIf rbClearAll.Checked Then
        ''    UnSelectAll()
        ''End If
        ' ChkAll.Checked = True
        'Dim FlagCheck As Boolean = False
        'Common dataset
        'chkCODemographic.Checked = True
        'chkCOProblems.Checked = True
        'chkCOAllergy.Checked = True
        'chkCOCareTeamMem.Checked = True
        'chkCOProcedures.Checked = True
        'chkCOVitalSigns.Checked = True
        'chkCOlabResult.Checked = True
        'chkCOLabTest.Checked = True
        'chkCOMedication.Checked = True
        'chkCSClinicalInstru.Checked = True 'Clinical instruction previously was part of Clinical Summary which is moved to common dataset now
        'chkCOFamilyHistory.Checked = True
        'chkCOSocialHistory.Checked = True
        'chkImplant.Checked = True
        'ChkCOGoals.Checked = True
        'ChkCOHealthConcerns.Checked = True
        'chkCSProviderName.Checked = True
        ''   ''If nCDAFileType <> CDAFileTypeEnum.CarePlan Then


        ''    ChkCOAssessments.Enabled = True
        ''    ChkCOTreatmentPlan.Enabled = True
        ''Else
        ''    ' ChkCOAssessments.Checked = False
        ''    ' ChkCOTreatmentPlan.Checked = False
        ''    ChkCOAssessments.Enabled = False
        ''    ChkCOTreatmentPlan.Enabled = False
        ''End If
        ''If (chkCODemographic.Checked AndAlso chkCOProblems.Checked AndAlso chkCOAllergy.Checked AndAlso chkCOCareTeamMem.Checked AndAlso chkCOProcedures.Checked AndAlso chkCOVitalSigns.Checked AndAlso chkCOlabResult.Checked AndAlso chkCOLabTest.Checked AndAlso chkCOMedication.Checked AndAlso chkCSClinicalInstru.Checked AndAlso chkCOFamilyHistory.Checked AndAlso chkCOSocialHistory.Checked AndAlso chkImplant.Checked AndAlso ChkCOGoals.Checked AndAlso ChkCOHealthConcerns.Checked AndAlso ChkCOTreatmentPlan.Checked AndAlso ChkCOAssessments.Checked) Then
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = True
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Text = "Clear All"
        ''Else
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = False
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    Exit Sub
        ''End If

        ''   If nCDAFileType = CDAFileTypeEnum.ClinicalSummary Then
        ''If (chkCSFutureAppt.Checked AndAlso chkCSOfcContact.Checked AndAlso chkCSRefOtrProvider.Checked AndAlso chkCSVisitInfo.Checked AndAlso chkCSDecisionAids.Checked AndAlso chkCSVisitMedications.Checked AndAlso chkCSVisitImmunization.Checked AndAlso chkCSDigTestPending.Checked AndAlso chkCSFutureTest.Checked AndAlso chkCSVisitReason.Checked AndAlso chkCSProviderName.Checked) Then
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = True
        ''    ChkAll.Text = "Clear All"
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''Else
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = False
        ''    ChkAll.Text = "Select All"
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged

        ''End If
        'chkCSFutureAppt.Checked = True
        'chkCSOfcContact.Checked = True
        'chkCSRefOtrProvider.Checked = True
        'chkCSVisitInfo.Checked = True
        'chkCSDecisionAids.Checked = True
        'chkCSVisitMedications.Checked = True
        'chkCSVisitImmunization.Checked = True
        'chkCSDigTestPending.Checked = True
        'chkCSFutureTest.Checked = True
        'chkCSVisitReason.Checked = True

        ''    ElseIf nCDAFileType = CDAFileTypeEnum.AmbulatorySummary Then
        ''If (chkAmbProviderName.Checked AndAlso chkAmbProviderContact.Checked AndAlso chkAmbImmunization.Checked AndAlso chkambEncounters.Checked AndAlso ChkAmbFunctionalStatus.Checked AndAlso ChkAmbReasonReferral.Checked AndAlso ChkAmbReferring.Checked AndAlso ChkAmbMental.Checked AndAlso chkambDatelocationvisit.Checked) Then
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = True
        ''    ChkAll.Text = "Clear All"
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''Else
        ''    RemoveHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged
        ''    ChkAll.Checked = False
        ''    ChkAll.Text = "Select All"
        ''    AddHandler ChkAll.CheckedChanged, AddressOf ChkAll_CheckedChanged

        ''End If
        'chkAmbProviderName.Checked = True
        'chkAmbProviderContact.Checked = True
        'chkAmbImmunization.Checked = True
        'chkambEncounters.Checked = True
        'ChkAmbFunctionalStatus.Checked = True
        'ChkAmbReasonReferral.Checked = True
        'ChkAmbReferring.Checked = True
        'ChkAmbMental.Checked = True
        'chkambDatelocationvisit.Checked = True

        ''    End If
    End Sub
    Private Sub CheckSummaryCareRecord()

        If nCDAFileType = CDAFileTypeEnum.CareRecordSummary Then

            chkCODemographic.Checked = True ''common panel data
            chkCOProblems.Checked = True
            chkCOAllergy.Checked = True
            chkCOCareTeamMem.Checked = True
            chkCOProcedures.Checked = True
            chkCOVitalSigns.Checked = True
            chkCOlabResult.Checked = True
            chkCOLabTest.Checked = True
            chkCOMedication.Checked = True
            chkCSClinicalInstru.Checked = True
            chkCOFamilyHistory.Checked = True
            chkCOSocialHistory.Checked = True
            chkImplant.Checked = True
            ChkCOAssessments.Checked = True
            ChkCOTreatmentPlan.Checked = True
            ChkCOGoals.Checked = True
            ChkCOHealthConcerns.Checked = True


            chkTransCareEncounter.Checked = True
            chkTransCareImmunization.Checked = True
            chkTransCareCognitiveStat.Checked = True
            chkTransCareResReferral.Checked = True
            chkTransCareRefProvider.Checked = True
            chkTransCareFunctionalStat.Checked = True
            ChkCareProvider.Checked = True
            ChkCareOfficeContact.Checked = True
            chktransDateLocationvisit.Checked = True
        End If

        ChkAll.Text = "Clear All"
        ChkAll.Checked = True

    End Sub

    Private Sub rbSelectAllNormal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSelectAllNormal.CheckedChanged
        If rbSelectAllNormal.Checked = True Then
            rbSelectAllNormal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            '' If blnChk = False Then
            Call SelectAll()
            ''
            sConfidentialityCode = "Normal"
            gloLibCCDGeneral.sConfidentialityCode = sConfidentialityCode
            ''
            ''End If
            ''blnChk = False
            chkPrivarySection.Checked = False
            chkPrivaryText.Checked = False
        Else
            rbSelectAllNormal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub

    Private Sub rbSelectAllRestricted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSelectAllRestricted.CheckedChanged
        If rbSelectAllRestricted.Checked = True Then
            rbSelectAllRestricted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            ''  If blnChk = False Then
            Call SelectAll()
            ''End If
            ''
            sConfidentialityCode = "Restricted"
            gloLibCCDGeneral.sConfidentialityCode = sConfidentialityCode
            ''
        Else
            rbSelectAllRestricted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub

    Private Sub rbClearAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbClearAll.CheckedChanged
        If rbClearAll.Checked = True Then
            rbClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Call UnSelectAll()
        Else
            rbClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub

    Private Sub chkOvrAdminSettings_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkOvrAdminSettings.CheckedChanged

        SelectAll()

    End Sub

End Class
