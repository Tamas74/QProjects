Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Net
Imports System.Text
'Imports System.Web.Hosting
'Imports System.ServiceModel.Web
Imports System.Data
Imports System.Data.SqlClient
'Imports CDO
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports gloEMRGeneralLibrary.gloEMRActors
Imports System.Reflection
Imports gloPatientPortalCommon
Imports System.Drawing


Public Class frmInfoButtonBrowser

    Private Shared frmInfoForm As frmInfoButtonBrowser = Nothing
    Public BrowserLink As String = ""
    Public DocumentTitle As String = ""
    Public DocumentCompleted As Boolean = False


    Public PatientId As Long
    Public VisitID As Long
    Public EducationID As Long
    Public Source As Integer
    Public ResourceCategory As Integer
    Public ResourceType As Integer
    Public isViewed As Boolean = False
    Public HomeUrl As String = ""
    Public gblnUseDefaultPrinter As Boolean
    Public justToSaveLink As Boolean = False
    Public PathToSaveLink As String = ""
    Public dtLinks As DataTable = Nothing
    Public CoreURL As String = ""
    Private _visID As Long

    Public nPatientEducationID As Long = 0
    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
    Dim nProviderID As Int64 = 0
    Private nLoginProviderID As Long

    Public Property VISID As Long
        Get
            Return _visID
        End Get
        Set(ByVal value As Long)
            _visID = value
        End Set
    End Property


    Public Property LoginProviderID As Long
        Get
            Return nLoginProviderID
        End Get
        Set(ByVal value As Long)
            nLoginProviderID = value
        End Set
    End Property



    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()



    End Sub

    Public Shared Function GetInstance() As frmInfoButtonBrowser
        If frmInfoForm Is Nothing Then
            frmInfoForm = New frmInfoButtonBrowser
        End If

        Return frmInfoForm
    End Function

    Private Sub frmInfoButtonBrowser_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If justToSaveLink Then
            tls_gloCommunityDashboard.Enabled = False
         End If

        btnTag_Up.Visible = False
        lblInfobuttonLink.Text = CoreURL
        Try


            If Not IsNothing(dtLinks) Then
                If dtLinks.Rows.Count > 0 Then
                    pnlLinks.Visible = True
                    trvLinks.Nodes.Clear()
                    trvLinks.ImageKey = Nothing
                    trvLinks.SelectedImageIndex = -1

                    Dim node As TreeNode
                    Dim subNode As TreeNode
                    For Each row As DataRow In dtLinks.Rows
                        'search in the treeview if any country is already present
                        node = Searchnode(Convert.ToString(row.Item(0)), trvLinks)
                        If node IsNot Nothing Then
                            subNode = New TreeNode(Convert.ToString(row.Item(1)))
                            subNode.Tag = Convert.ToString(row.Item(2))
                            subNode.ForeColor = Color.Blue
                            node.Nodes.Add(subNode)
                        Else
                            node = New TreeNode(Convert.ToString(row.Item(0)))
                            subNode = New TreeNode(Convert.ToString(row.Item(1)))
                            subNode.Tag = Convert.ToString(row.Item(2))
                            subNode.ForeColor = Color.Blue
                            If subNode.Text = "" Then
                                node.Tag = Convert.ToString(row.Item(2))
                                node.ForeColor = Color.Blue
                            Else
                                node.Nodes.Add(subNode)
                            End If

                            trvLinks.Nodes.Add(node)
                        End If
                    Next
                    trvLinks.ExpandAll()
                    If dtLinks.Rows.Count >= 1 Then
                        If trvLinks IsNot Nothing Then
                            If trvLinks.Nodes IsNot Nothing Then
                                If trvLinks.Nodes(0) IsNot Nothing Then
                                    If trvLinks.Nodes(0).Nodes IsNot Nothing Then
                                        If trvLinks.Nodes(0).Nodes.Count > 0 Then
                                            trvLinks.SelectedNode = trvLinks.Nodes(0).Nodes(0)
                                        Else
                                            trvLinks.SelectedNode = trvLinks.Nodes(0)
                                        End If
                                        NavigateTo(Convert.ToString(dtLinks.Rows(0)("Link")))
                                    End If
                                End If

                            End If


                        End If

                        '' NavigateTo(dtLinks.Rows(0)("Link"))

                    End If
                Else
                    pnlLinks.Visible = False
                End If


            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + "INFOBUTTON Browser Execption : " + Convert.ToString(ex.InnerException), False)
        End Try
    End Sub

    Private Sub frmInfoButtonBrowser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        frmInfoForm = Nothing
    End Sub
    Private myPrinterSetting As System.Drawing.Printing.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
    Dim OldPrinterName As String
    Private Sub tls_gloCommunityDashboard_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_gloCommunityDashboard.ItemClicked
        If e.ClickedItem.Tag = "Home" Then
            InfoButtonWebBrowser.Navigate(HomeUrl)
        ElseIf e.ClickedItem.Tag = "Refresh" Then
            InfoButtonWebBrowser.Navigate(BrowserLink)
        ElseIf e.ClickedItem.Tag = "Next" Then
            InfoButtonWebBrowser.GoForward()
        ElseIf e.ClickedItem.Tag = "Previous" Then
            InfoButtonWebBrowser.GoBack()
        ElseIf e.ClickedItem.Tag = "Print" Then
            'Print Html Document
            If DocumentCompleted Then
                'Me.Cursor = Cursors.WaitCursor
                'Dim Copied As Boolean = False
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


                Try
                    If CheckBatchPrintProcessRunning() = False Then
                        '    Dim OldPrinterName As String = ""
                        '    If c1PatientTemplates.Rows.Count > 1 Then
                        '        If trv_viewPatientStatement.Nodes.Count > 0 Then
                        '            If Not trv_viewPatientStatement.SelectedNode.Checked Then
                        '                Dim oListTempleteIds As ArrayList = FetchTempleteId()
                        '                If oListTempleteIds.Count > 0 Then
                        '                    Dim ogloTemplate As gloOffice.gloTemplate
                        '                    Using __InlineAssignHelper(ogloTemplate, New gloTemplate(_databaseconnectionstring))
                        Using oDialog As gloPrintDialog.gloPrintDialog = New gloPrintDialog.gloPrintDialog(True)
                            oDialog.ConnectionString = gloEMRDatabase.DataBaseLayer.ConnectionString
                            oDialog.TopMost = True
                            oDialog.ModuleName = "PrintInfoDocuments"
                            oDialog.RegistryModuleName = "PrintInfoDocuments"
                            oDialog.ShowPrinterProfileDialog = True

                            If Not gloGlobal.gloTSPrint.isCopyPrint Then
                                Try
                                    OldPrinterName = myPrinterSetting.PrinterName
                                Catch
                                End Try

                                If oDialog IsNot Nothing Then
                                    oDialog.PrinterSettings = myPrinterSetting
                                    oDialog.AllowSomePages = True
                                    oDialog.bUseDefaultPrinter = True
                                    oDialog.PrinterSettings.ToPage = 1
                                    oDialog.PrinterSettings.FromPage = 1
                                    oDialog.PrinterSettings.MaximumPage = 1
                                    oDialog.PrinterSettings.MinimumPage = 1
                                End If
                            End If

                            oDialog.AllowSomePages = True
                            oDialog.bUseDefaultPrinter = gblnUseDefaultPrinter
                            If oDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                If (oDialog.bUseDefaultPrinter = True) Then
                                    oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                                    oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
                                End If
                                '' changes integrated from 9000
                                Dim ogloPrintProgressController As frmgloPrintPrintInfoController = New frmgloPrintPrintInfoController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, Nothing)
                                ogloPrintProgressController.OldPrinterName = OldPrinterName
                                ' ogloPrintProgressController.gblnUseDefaultPrinter = gblnUseDefaultPrinter
                                ogloPrintProgressController.InfoButtonWebBrowser = InfoButtonWebBrowser
                                ogloPrintProgressController._databaseConnectionString = gloEMRDatabase.DataBaseLayer.ConnectionString
                                If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                                    If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then
                                        ogloPrintProgressController.Show()
                                    Else
                                        ogloPrintProgressController.Show()
                                    End If
                                Else
                                    ogloPrintProgressController.TopMost = True
                                    ogloPrintProgressController.ShowInTaskbar = False
                                    ogloPrintProgressController.ShowDialog()
                                    If ogloPrintProgressController IsNot Nothing Then
                                        ogloPrintProgressController.Dispose()
                                    End If

                                    ogloPrintProgressController = Nothing
                                End If
                            End If
                        End Using
                    End If


                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
                    ex = Nothing
                Finally
                End Try







                Me.Cursor = Cursors.Default
                If Source = 1 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ElseIf Source = 3 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document Print", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        ElseIf e.ClickedItem.Tag = "Save&Close" Then



            If DocumentCompleted Then
                Try
                    If nLoginProviderID = 0 Then
                        nProviderID = Convert.ToInt64(Global.gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(PatientId))
                    Else
                        nProviderID = nLoginProviderID
                    End If

                    If Not getProviderTaxID(nProviderID) Then
                        Exit Sub
                    End If

                    'Save Link to Patient Education Table
                    'If Not isViewed Then

                    Dim oInfo As New clsInfobutton()
                    Dim oDocUrl As String
                    If DocumentTitle = "" Then
                        DocumentTitle = InfoButtonWebBrowser.DocumentTitle
                    End If

                    If Not IsNothing(InfoButtonWebBrowser.Document) Then
                        oDocUrl = InfoButtonWebBrowser.Document.Url.ToString()
                    Else
                        oDocUrl = InfoButtonWebBrowser.Url.OriginalString()
                    End If

                    'VisitID = GenerateVisitID()
                    _visID = VisitID

                    'Change made to save web content in database instead of null
                    Dim temFile As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".docx", "MMddyyyyHHmmssffff")
                    saveWebContentToFile(temFile)
                    Dim _speNotes As Object = CType(gloWord.LoadAndCloseWord.ConvertFiletoBinary(temFile), Object)

                    oInfo.SavePatientEducation(VisitID, PatientId, 0, _speNotes, DocumentTitle, Source, ResourceCategory, ResourceType, oDocUrl, EducationID, LoginProviderID)
                    nPatientEducationID = oInfo.nPatientEducationID

                    If nPatientEducationID <> 0 Then
                        Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, nPatientEducationID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientEducation.GetHashCode())
                        oclsselectProviderTaxID = Nothing
                    End If
                    Me.Close()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + "INFOBUTTON Save Execption : " + Convert.ToString(ex.InnerException), False)
                End Try
            End If
        ElseIf e.ClickedItem.Tag = "Close" Then
            Me.Close()
        ElseIf e.ClickedItem.Tag = "SendToPortal" Then
            SendToPortal()
        End If

    End Sub
    Private Function CheckBatchPrintProcessRunning() As Boolean
        Try


            For Each oFrm As Form In System.Windows.Forms.Application.OpenForms

                If oFrm.Name = "frmgloPrintPrintInfoController" Then

                    Dim dg As DialogResult = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (dg = DialogResult.Yes) Then
                        oFrm.Close()
                        Return False
                        Exit For
                    Else
                        oFrm.Visible = True
                        Return True
                        Exit For
                    End If
                End If
            Next
            Return False
        Catch ex As Exception
            ex = Nothing
            Return False
        End Try
    End Function

    Private Sub InfoButtonWebBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles InfoButtonWebBrowser.Navigated
        BrowserLink = InfoButtonWebBrowser.Url.ToString
        Me.BringToFront()  ''added for case no CAS-17324-V6Y5L1
    End Sub

    Private Sub InfoButtonWebBrowser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles InfoButtonWebBrowser.NewWindow
        'This is to prevent the WebBrowser control from opening
        'new widows.
        e.Cancel = True
        BrowserLink = InfoButtonWebBrowser.StatusText
        Me.BringToFront()  ''added for case no CAS-17324-V6Y5L1
        InfoButtonWebBrowser.Navigate(BrowserLink)
        Me.BringToFront()  ''added for case no CAS-17324-V6Y5L1
    End Sub

    Private Sub txtWebAddress_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            Navigation()
        End If
    End Sub


    Public Sub NavigateTo(ByVal resturl As String)
        BrowserLink = resturl
        HomeUrl = resturl
        'PostXmlDocument(txtWebAddress.Text)
        Navigation()
    End Sub

    Public Sub NavigateTo(ByVal Code As String, ByVal CodeSystem As String, ByVal strAge As String, ByVal strGender As String) ', ByVal Description As String)


        Dim ageP As String() = strAge.Split(" ")
        Dim AgeUnit As String = ""
        Dim age As String = ""
        If ageP.Length > 2 Then
            If ageP(1) = "Years" Then
                age = ageP(0)
                AgeUnit = "a"
            ElseIf ageP(1) = "Months" Then
                age = ageP(0)
                AgeUnit = "mo"
            ElseIf ageP(1) = "Days" Then
                age = ageP(0)
                AgeUnit = "d"
            End If

        End If
        Dim CoreURL As String = "https://apps2.nlm.nih.gov/medlineplus/services/mpconnect.cfm?"
        Dim nlmUrl = CoreURL + "mainSearchCriteria.v.c=" + Code + "&mainSearchCriteria.v.cs=" + CodeSystem + " &patientPerson.administrativeGenderCode.c=" + strGender + "&age.v.v=" + age + "&age.v.u=" + AgeUnit + "&performer.languageCode.c=en&knowledgeResponseType=text/XML"
        Dim ldmGroupUrl As String = "https://infobutton.ldmgrp.com/infobutton.aspx?p=1&lc=7890&dc=ABCD&pc=12345 &cs=" + CodeSystem + "&c=" + Code + "&l=en&f=pdf"
        Dim Nlmv4Url = CoreURL + "patientPerson.administrativeGenderCode.c = " + strGender + "& " +
                      "age.v.v=" + age + "& age.v.u=" + AgeUnit + "& " +
                      "mainSearchCriteria.v.c=" + Code + "& mainSearchCriteria.v.cs=" + CodeSystem + "&  " +
                      "observation.v.c=77386006& observation.v.cs=2.16.840.1.113883.6.96& " +
                      "observation.v.v=65& observation.v.u=mL/min " +
                      "performer.languageCode.c=en&knowledgeResponseType=text/XML& "
        BrowserLink = ldmGroupUrl

        PostXmlDocument(BrowserLink)
        Navigation()
    End Sub

    Private Sub Navigation()
        Try

            InfoButtonWebBrowser.Navigate(BrowserLink)
            'Do Until InfoButtonWebBrowser.ReadyState = WebBrowserReadyState.Complete
            '    Application.DoEvents()
            '    System.Threading.Thread.Sleep(25)
            'Loop

            If Source = 1 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf Source = 2 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf Source = 3 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Online education Document viewed", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If


            AddHandler InfoButtonWebBrowser.DocumentCompleted, AddressOf navigation_complete

            'Code To Save HTML page on local disk using WebClient
            '    'Dim LocalFilePath As String = "C:\lcal.html"
            '    'Dim objWebClient As New System.Net.WebClient
            '    'objWebClient.DownloadFile(BrowserLink, LocalFilePath)

            'Code To Save Web page on local disk as MHT(Mhtl) file
            '    SavePage(BrowserLink, "C:\lcal.mht")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.GetType().ToString() + " : " + ex.Message + "INFOBUTTON Browser Navigation Execption : " + Convert.ToString(ex.InnerException), False)
        End Try
    End Sub

    Private Sub navigation_complete(ByVal sender As System.Object, _
               ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)

        'Code To Save Web page on local disk using file stream
        'Dim HTMlAuthorCode As String = sender.DocumentText
        'My.Computer.FileSystem.WriteAllText("C:\tempAuthorCode.html", HTMlAuthorCode, True)

        'Dim strAuthorCode As String = sender.Document.Body.InnerText
        'My.Computer.FileSystem.WriteAllText("C:\tempAuthorCode.txt", strAuthorCode, True)
        'sender.Dispose()

        DocumentCompleted = True
        Me.BringToFront() ''added for case no CAS-17324-V6Y5L1
        If Not IsNothing(InfoButtonWebBrowser.Document) Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.Document.Title)
        End If

        If IsNothing(DocumentTitle) OrElse DocumentTitle = "" Then
            DocumentTitle = Convert.ToString(InfoButtonWebBrowser.DocumentTitle)
        End If
        If DocumentTitle = "Health Information for You: MedlinePlus Connect" Or DocumentTitle = "Información de salud para usted: MedlinePlus Connect" Then
            Try
                'Code To fetch H2 tag from html Source Code
                For Each element As HtmlElement In InfoButtonWebBrowser.Document.All
                    Dim HeaderElement() As String
                    If element.TagName().ToString.ToUpper() = "H2" Then
                        HeaderElement = element.InnerText.ToString().Split("[")
                        If HeaderElement.Length > 0 Then
                            DocumentTitle = HeaderElement(0)
                        End If
                    End If
                    HeaderElement = Nothing
                Next
                'Code to get HTML web page title
                'Dim x As New WebClient()
                'Dim source As String = x.DownloadString(resturl)
                'Dim title As String = Regex.Match(source, "\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups("Title").Value
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        If justToSaveLink Then
            saveWebContentToFile(PathToSaveLink)
            justToSaveLink = False
            Me.Close()
        End If

    End Sub

    Private Sub SavePage(ByVal Url As String, ByVal FilePath As String)
        'Dim iMessage As CDO.Message = New CDO.Message
        'iMessage.CreateMHTMLBody(Url, _
        'CDO.CdoMHTMLFlags.cdoSuppressNone, "", "")
        'Dim adodbstream As ADODB.Stream = New ADODB.Stream
        'adodbstream.Type = ADODB.StreamTypeEnum.adTypeText
        'adodbstream.Charset = "US-ASCII"
        'adodbstream.Open()
        'iMessage.DataSource.SaveToObject(adodbstream, "_Stream")
        'adodbstream.SaveToFile(FilePath, _
        '          ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
    End Sub

    Public Function PostXmlDocument(ByVal infoUrl As String) As Byte()
        Dim restURL As StringBuilder = New StringBuilder()
        Dim restRequest As HttpWebRequest = Nothing
        Dim restResponse As HttpWebResponse = Nothing
        Dim xDoc As XmlDocument = New XmlDocument()
        'Dim inStream As StreamReader
        Try
            restURL.AppendFormat(infoUrl)
            'Dim wc As WebClient = New System.Net.WebClient()
            'wc.DownloadFile(restURL.ToString(), "c:\infoPdf.pdf")

            restRequest = DirectCast(WebRequest.Create(restURL.ToString()), HttpWebRequest)
            restRequest.Method = "POST"
            Dim sw As StreamWriter = New StreamWriter(restRequest.GetRequestStream)
            Dim strPostData As String = String.Format("operation={0}&username={1}", "dosomething", "admin")
            sw.Write(strPostData)
            sw.Close()
            sw.Dispose()
            restResponse = restRequest.GetResponse()
            Dim stream As Stream = restResponse.GetResponseStream()
            Dim reader As StreamReader = New StreamReader(stream)
            Dim webContent As String = reader.ReadToEnd()
            Dim sw1 As StreamWriter = New StreamWriter("c:\info3.pdf")
            sw1.WriteLine(webContent)
            sw1.Close()
            sw1.Dispose()
            reader.Close()
            reader.Dispose()

        Catch ex As Exception
        Finally
            If restRequest IsNot Nothing Then

                restRequest = Nothing
            End If
            If restURL IsNot Nothing Then
                restURL.Clear()
                restURL = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
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

    Private Sub SendToPortal()

        Dim IsPatientRegisteredOrNotOnPortal As Boolean = False
        Dim clsPatientPortal As New clsgloPatientPortalEmail(gloEMRDatabase.DataBaseLayer.ConnectionString)
        IsPatientRegisteredOrNotOnPortal = clsPatientPortal.IsPatientRegisteredOnPortal(PatientId, True)

        If Not IsPatientRegisteredOrNotOnPortal Then
            Exit Sub
        End If


        'Dim myAssembly As System.Reflection.Assembly = Nothing
        'Dim myType As Type = Nothing
        'Dim magicConstructor As ConstructorInfo = Nothing
        'Dim magicClassObject As Object = Nothing
        'Dim Exampleb As MethodInfo = Nothing
        'Dim obj(1) As Object

        Try

            If DocumentCompleted Then
                'Save Link to Patient Education Table
                'If Not isViewed Then
                Dim oInfo As New clsInfobutton()
                Dim oDocUrl As String
                If DocumentTitle = "" Then
                    DocumentTitle = InfoButtonWebBrowser.DocumentTitle
                End If

                If Not IsNothing(InfoButtonWebBrowser.Document) Then
                    oDocUrl = InfoButtonWebBrowser.Document.Url.ToString()
                Else
                    oDocUrl = InfoButtonWebBrowser.Url.OriginalString()
                End If

                'VisitID = GenerateVisitID()
                _visID = VisitID

                oInfo.SavePatientEducation(VisitID, PatientId, 0, Nothing, DocumentTitle, Source, ResourceCategory, ResourceType, oDocUrl, EducationID, LoginProviderID)
                nPatientEducationID = oInfo.nPatientEducationID
            End If
            gloGlobal.LoadFromAssembly.OpenIntuitSendMessage("0", nPatientEducationID)
            'obj(1) = New Object
            'myAssembly = System.Reflection.Assembly.LoadFrom("gloEMR.exe")
            'myType = myAssembly.GetType("gloEMR.MainMenu")
            'magicConstructor = myType.GetConstructor(Type.EmptyTypes)
            'magicClassObject = magicConstructor.Invoke(New Object() {})
            'Exampleb = myType.GetMethod("OpenIntuitSendNewMessage")
            'obj(0) = "0"
            'obj(1) = nPatientEducationID
            'Exampleb.Invoke(magicClassObject, obj)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

            'If Not IsNothing(Exampleb) Then
            '    Exampleb = Nothing
            'End If
            'If Not IsNothing(magicClassObject) Then
            '    Exampleb = myType.GetMethod("Dispose")
            '    Exampleb.Invoke(magicClassObject, Nothing)
            '    magicClassObject = Nothing
            'End If
            'If Not IsNothing(magicConstructor) Then
            '    magicConstructor = Nothing
            'End If
            'If Not IsNothing(myType) Then
            '    myType = Nothing
            'End If
            'If Not IsNothing(myAssembly) Then
            '    myAssembly = Nothing
            'End If

        End Try
    End Sub

    Public Sub ValidatePortalFeatures()
        Dim clsPatientPortal As New clsgloPatientPortalEmail(gloEMRDatabase.DataBaseLayer.ConnectionString)
        clsPatientPortal.GetSettings()
        If clsPatientPortal.gblnUSEINTUITINTERFACE = False And clsPatientPortal.gblnIntuitCommunication = False Then
            ts_SendToPortal.Visible = False
        ElseIf clsPatientPortal.gblnPatientPortalEnabled = False And clsPatientPortal.gblnIntuitCommunication = False Then
            ts_SendToPortal.Visible = False
        End If
    End Sub

    Private Sub saveWebContentToFile(fileName As String)
        Dim tmpobjWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Dim tmpwd As Microsoft.Office.Interop.Word.Document
        tmpwd = tmpobjWord.WebToDocx(InfoButtonWebBrowser, fileName)
        If IsNothing(tmpwd) Then
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Save, "Not able to save infobutton referance material (Word Object not initialized).", gloAuditTrail.ActivityOutCome.Failure)
        End If
        tmpobjWord.CloseWordApplication(tmpwd)
        tmpobjWord = Nothing
    End Sub

    Private Function Searchnode(ByVal nodetext As String, ByVal trv As TreeView) As TreeNode
        For Each node As TreeNode In trv.Nodes
            If node.Text = nodetext Then
                Return node
            End If
        Next
        Return Nothing
    End Function


    Private Sub btnTag_Up_Click(sender As System.Object, e As System.EventArgs) Handles btnTag_Up.Click
        pnllblInfobuttonLink.Visible = True
        btnTag_Up.Visible = False
        btnTag_Down.Visible = True
    End Sub

    Private Sub btnTag_Down_Click(sender As System.Object, e As System.EventArgs) Handles btnTag_Down.Click
        pnllblInfobuttonLink.Visible = False
        btnTag_Up.Visible = True
        btnTag_Down.Visible = False
    End Sub

    Private Sub trvLinks_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvLinks.NodeMouseClick
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim SelectedLink As String = Convert.ToString(e.Node.Tag)
            If SelectedLink = "" Then
                If e.Node.Nodes.Count > 0 Then
                    SelectedLink = e.Node.FirstNode.Tag
                End If
            End If
            NavigateTo(SelectedLink)
            'lblInfobuttonLink.Text = SelectedLink
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Infobuttonbrowser :: " & ex.ToString(), False)
        End Try
       
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy.Click
        If lblInfobuttonLink.Text <> "" Then
            Clipboard.SetText(lblInfobuttonLink.Text)
        End If
    End Sub
End Class
