Public Class frmPHIDocumentReview

    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oPDFDoc As pdftron.PDF.PDFDoc
    Public strDocumentPath As String
    Public Sub New(ByVal strDocPath As String)
        InitializeComponent()
        strDocumentPath = strDocPath

    End Sub

    Private Sub tlb_Cancel_Click(sender As System.Object, e As System.EventArgs) Handles tlb_Cancel.Click
        Me.Close()

    End Sub


    Private Sub ShowPDFPreviewforPHI(ByVal strNonXMLPath As String)
        Try


        

        lblPreviewStatus.Text = ""

        If pnlPreviewDMSDoc.Controls.Contains(oPDFView) Then
            pnlPreviewDMSDoc.Controls.Add(oPDFView)
        End If
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
        pnlPreviewCommand.Visible = True
        '  nPageNo = 1;

        If oPDFView Is Nothing Then
            'oPDFView = new pdftron.PDF.PDFViewCtrl();
                oPDFView = New pdftron.PDF.PDFViewCtrl()
                'RemoveHandler oPDFView.MouseWheel, AddressOf oPDFView_MouseWheel
                'AddHandler oPDFView.MouseWheel, AddressOf oPDFView_MouseWheel
        End If
        Dim oldDoc As pdftron.PDF.PDFDoc = oPDFView.GetDoc()

        oPDFDoc = New pdftron.PDF.PDFDoc(strNonXMLPath)
        If oPDFView Is Nothing Then
            oPDFView = New pdftron.PDF.PDFViewCtrl()
        End If
        oPDFView.Show()
        oPDFView.SetDoc(oPDFDoc)
        If oldDoc IsNot Nothing Then
            oldDoc.Dispose()
            oldDoc = Nothing
        End If
        pnlPreviewDMSDoc.Controls.Add(oPDFView)
        oPDFView.Dock = DockStyle.Fill
        oPDFView.BringToFront()
        oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page)
        oPDFView.SetCaching(True)
        oPDFView.SetProgressiveRendering(True)
        oPDFView.Visible = True
        oPDFView.Refresh()
        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page)
        oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width)
        Dim Percentage As String = "100%"
        oPDFView.SetZoom(System.Convert.ToDouble(Percentage.Substring(0, Percentage.Length - 1).ToString()) / 100)

        If oPDFView.GotoFirstPage() = True Then
            oPDFView.GetSelectionBeginPage()
        End If
            lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()
        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        If oPDFView.GetPageCount() > 1 Then
            btnNext.Enabled = True
            btnLast.Enabled = True
        Else
            btnNext.Enabled = False
            btnLast.Enabled = False
        End If
            oPDFView.EnableInteractiveForms(False)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub frmPHIDocumentReview_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            Dim oProcessLabel As Label = Nothing
            If oProcessLabel IsNot Nothing Then
                If Me.Controls.Contains(oProcessLabel) = True Then
                    Me.Controls.Remove(oProcessLabel)
                End If
                oProcessLabel.Dispose()
                oProcessLabel = Nothing
            End If
            oProcessLabel = New Label()
            Me.Controls.Add(oProcessLabel)
            oProcessLabel.Dock = DockStyle.Fill
            oProcessLabel.Location = New System.Drawing.Point(0, 0)
            oProcessLabel.ForeColor = Color.Blue
            Me.BackColor = Color.White
            oProcessLabel.Font = New System.Drawing.Font("Verdana", 30.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
            oProcessLabel.TextAlign = ContentAlignment.MiddleCenter
            oProcessLabel.Text = "Please wait !!!"
            oProcessLabel.Name = "lblProcess"
            oProcessLabel.Visible = True

            oProcessLabel.BringToFront()
            Application.DoEvents()
            System.Threading.Thread.Sleep(1000)
            ShowPDFPreviewforPHI(strDocumentPath)
            oProcessLabel.Visible = False
            oProcessLabel.SendToBack()

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub oPDFView_MouseWheel(sender As System.Object, e As System.EventArgs)
    '    lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()
    'End Sub


    Private Sub btnFirst_Click(sender As System.Object, e As System.EventArgs) Handles btnFirst.Click
        Try
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            '  nPageNo = 1;
            If oPDFView.GetDoc() IsNot Nothing Then
                oPDFView.GotoFirstPage()
            End If


            lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try

    End Sub

    Private Sub btnPrevious_Click(sender As System.Object, e As System.EventArgs) Handles btnPrevious.Click
        Try
            btnNext.Enabled = True
            btnLast.Enabled = True

            ' nPageNo = 1;
            If oPDFView.GetDoc() IsNot Nothing Then
                oPDFView.GotoPreviousPage()
            End If
            If oPDFView.GetCurrentPage() = 1 Then
                btnPrevious.Enabled = False
                btnFirst.Enabled = False
            End If


            lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    Private Sub btnNext_Click(sender As System.Object, e As System.EventArgs) Handles btnNext.Click
        Try
            btnPrevious.Enabled = True
            btnFirst.Enabled = True

            '   nPageNo = 1;
            If oPDFView.GetDoc() IsNot Nothing Then
                oPDFView.GotoNextPage()
            End If
            If oPDFView.GetCurrentPage() >= oPDFView.GetPageCount() Then
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If


            lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    Private Sub btnLast_Click(sender As System.Object, e As System.EventArgs) Handles btnLast.Click
        Try
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
            btnNext.Enabled = False
            btnLast.Enabled = False
            '  nPageNo = 1;
            If oPDFView.GetDoc() IsNot Nothing Then
                oPDFView.GotoLastPage()
            End If


            lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
        End Try
    End Sub

    Private Sub frmPHIDocumentReview_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not oPDFView Is Nothing Then
            oPDFView.Dispose()
            oPDFView = Nothing
        End If

        If Not oPDFDoc Is Nothing Then
            oPDFDoc.Dispose()
            oPDFDoc = Nothing
        End If

    End Sub

    'Private Sub frmPHIDocumentReview_Scroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles MyBase.Scroll
    '    lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

    'End Sub

    'Private Sub pnlPreviewDMSDoc_Scroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles pnlPreviewDMSDoc.Scroll
    '    lblPreviewStatus.Text = " Page " + oPDFView.GetCurrentPage().ToString() + "of " + oPDFView.GetPageCount().ToString()

    'End Sub
End Class