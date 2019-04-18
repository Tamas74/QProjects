Imports System.Net
Imports System.IO
Imports System.Threading.Tasks
Imports SelectPdf
Imports gloEDocumentV3

Public Class frmPDMPWebBrowser

    Dim bIsDocumentLoaded As Boolean = False
    Dim webClient As New WebClient()

    Dim sFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".html", "MMddyyyyHHmmssffff")
    Dim sFilePathPDF As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".pdf", "MMddyyyyHHmmssffff")

    Public Property PatientID() As Int64

    Public viewURL As String
    Public ReportID As Long
    Public Property sPDMPURL() As String
        Get
            Return viewURL
        End Get
        Set(ByVal value As String)
            viewURL = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPDMPWebBrowser_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If File.Exists(sFilePath) Then
                File.Delete(sFilePath)
            End If
            If File.Exists(sFilePathPDF) Then
                File.Delete(sFilePathPDF)
            End If
            If webClient IsNot Nothing Then
                webClient.Dispose()
                webClient = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Public Function SaveReportHTML()
        Dim icnt As Integer = 0
        Try
            Dim HtmlString As String = New System.Net.WebClient().DownloadString(sFilePath)
            Dim objurl As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}
            icnt = objurl.UpdateReportHTML(ReportID, HtmlString)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "PDMP Report HTML Saved", PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try

        Return icnt
    End Function

    Private Sub frmPDMPWebBrowser_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            Task.Factory.StartNew(AddressOf NavigateToURL).ContinueWith(AddressOf NavigateToPath)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Private Sub NavigateToPath()
        Try
            If webBrowser IsNot Nothing Then
                webBrowser.Navigate(sFilePath)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Private Sub NavigateToURL()
        Try
            webClient.DownloadFile(New Uri(viewURL), sFilePath)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Private Function SaveToPDF() As Boolean
        Dim oDocManager As eDocManager.eDocManager = New gloEDocumentV3.eDocManager.eDocManager()
        Dim oSourceDocuments As New ArrayList()
        Dim oDialogContainerID As Long = 0
        Dim oDialogDocumentID As Long = 0
        Dim sDMSFileName As String = ""
        Dim nDocumentID As Int32 = 0
        Dim nDestinationCategoryID As Int32 = 0
        Dim dtCategories As New DataTable()
        Dim bReturned As Boolean = False
        Try
            Dim DMSConnectionstring As String = GetDMSConnectionString()

            Using eDoc As New gloEDocumentV3.eDocManager.eDocGetList()
                eDoc.GetCategories(gnClinicID, dtCategories)
            End Using

            If dtCategories IsNot Nothing AndAlso dtCategories.Rows.Count() > 0 Then
                If dtCategories.AsEnumerable().Any(Function(p) Convert.ToString(p("CategoryName")).ToUpper() = "PDMP") Then
                    nDestinationCategoryID = Convert.ToInt32(dtCategories.AsEnumerable().FirstOrDefault(Function(p) Convert.ToString(p("CategoryName")).ToUpper() = "PDMP")("CategoryID"))

                    Dim selectPDF As New SelectPdf.HtmlToPdf()
                    Dim selectPDFFile As SelectPdf.PdfDocument = selectPDF.ConvertUrl(sFilePath)

                    If File.Exists(sFilePathPDF) Then
                        File.Delete(sFilePathPDF)
                    End If

                    selectPDFFile.Save(sFilePathPDF)
                    selectPDFFile.Close()

                    Using pbDocument As New ProgressBar() With {.Minimum = 0, .Maximum = 100}
                        oDocManager.SetSettings(GetConnectionString(), DMSConnectionstring, gloEMRGeneralLibrary.gloGeneral.clsgeneral.gDMSV3TempPath + "DMSLogFile.txt", gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp")
                        oSourceDocuments.Add(sFilePathPDF)
                        sDMSFileName = eDocManager.eDocValidator.GetNewDocumentName(PatientID, "PDMP", gnClinicID, Enumeration.enum_OpenExternalSource.None)
                        bReturned = oDocManager.ImportSplit(PatientID, oSourceDocuments, sDMSFileName, nDestinationCategoryID, "PDMP", "", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), gnClinicID, oDialogContainerID, oDialogDocumentID, True, pbDocument, Enumeration.enum_OpenExternalSource.RxMeds)
                    End Using

                    dtCategories.Dispose()
                    dtCategories = Nothing

                    selectPDF = Nothing
                    selectPDFFile = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally
            If oSourceDocuments IsNot Nothing Then
                oSourceDocuments.Clear()
                oSourceDocuments = Nothing
            End If
        End Try

        Return bReturned
    End Function

    Private Sub webBrowser_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles webBrowser.DocumentCompleted
        pnlWait.Visible = False
        bIsDocumentLoaded = True

        'Checked for save  to DMS Setting and enable or disable  save to dms button.
        If gblPDMPSaveToDMS Then
            tblbtn_Save_32.Enabled = True

        Else
            tblbtn_Save_32.Enabled = False
            tblbtn_Save_32.ToolTipText = ""

        End If

        SaveReportHTML()
    End Sub

    Private Sub webBrowser_NewWindow(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles webBrowser.NewWindow
        Try
            If webBrowser.StatusText IsNot Nothing AndAlso Not String.IsNullOrEmpty(webBrowser.StatusText) AndAlso Not String.IsNullOrWhiteSpace(webBrowser.StatusText) Then
                e.Cancel = True
                webBrowser.Navigate(webBrowser.StatusText)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub

    Private Sub tblbtn_Save_32_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Save_32.Click
        Try
            If bIsDocumentLoaded Then
                If SaveToPDF() Then
                    MessageBox.Show("PMP report saved successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.Save, "Narx care report saved successfully.", PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    MessageBox.Show("PMP report could not be saved successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.Save, "Narx care report could not be saved successfully.", PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), PatientID, ReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub
End Class