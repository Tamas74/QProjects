Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text

Public Class ScreeningResults

    Public Event CloseScreeningResult()
    'Public Event LoadMoreDetailScreeningResult(ByVal id As String)
    Public Event MonoClk(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal monograph As String)
    Private m_nFirstCharOnPage As Integer
    Private img As System.Drawing.Image
    Private m_Monograph As String

    Public Property ClinicLogo() As Image
        Get
            Return img
        End Get
        Set(ByVal value As Image)
            img = value
        End Set
    End Property
    Public Event SavePatient_Education()
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(ByVal Results As String, ByVal HeaderName As String, Optional ShowbtnClose As Boolean = True, Optional ShowbtnPrint As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rtbDescription.Text = Results
        lblHeader.Text = HeaderName
        btnClose.Visible = ShowbtnClose
        rtbDescription.ReadOnly = True
        btnPrint.Visible = ShowbtnPrint
        lblbtnPrintBrd.Visible = ShowbtnPrint

    End Sub


    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        RaiseEvent CloseScreeningResult()
    End Sub

    'Private Sub btnDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnDetails.Click
    '    RaiseEvent LoadMoreDetailScreeningResult("Details of ...")
    'End Sub


#Region "MI Print"

    Public Function LoadPatientEducation(ByVal screeningResult As String) As Boolean
        Try

            If String.IsNullOrWhiteSpace(screeningResult) Then
                Return False
            End If
            If Not IsNothing(rtbDescription) Then
                If Not IsNothing(img) Then
                    Dim myImage As Image = aspectratio(img, 485, 146)
                    If (IsNothing(myImage) = False) Then
                        rtbDescription.ReadOnly = False
                        Try
                             Dim strEx As String = ""
                            Dim gotClip As Boolean = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                            Dim setClip As Boolean = Global.gloWord.gloWord.SetClipBoardImageWithRetry(myImage, 5, strEx)
                            If (setClip) Then

                                rtbDescription.Select(0, 0)
                                Try
                                    rtbDescription.Paste()
                                Catch
                                End Try
                            End If
                            If (gotClip) Then
                                Global.gloWord.gloWord.SetClipboardData()
                            End If

                        Catch
                        Finally
                            myImage.Dispose()
                        End Try
                    End If
                    rtbDescription.AppendText(vbCrLf)
                End If

                rtbDescription.AppendText(screeningResult)
                rtbDescription.ReadOnly = True
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        Finally
            ClinicLogo = Nothing
        End Try

    End Function

    Private Function aspectratio(ByVal bmp As Bitmap, ByVal Width As Integer, ByVal Height As Integer) As Image
        Try
            'Dim bmp As New Bitmap(filename)
            Dim OutputWidth As Double = bmp.Width
            'currnetwidth
            Dim OutputHeight As Double = bmp.Height
            'currentheight
            Dim picOutputwidth As Double = Width
            ' desired width
            Dim picOutputheight As Double = Height
            ' desired height


            Dim img As System.Drawing.Image

            Dim myPicWidth As Double = picOutputwidth
            Dim myPicHeight As Double = picOutputheight
            Dim myScaleX As Double = myPicWidth / CDbl(OutputWidth)
            Dim myScaleY As Double = myPicHeight / CDbl(OutputHeight)
            Dim myStartX As Double = 0
            Dim myStartY As Double = 0

            If myScaleX > myScaleY Then
                myPicWidth = CDbl(OutputWidth) * myScaleY
                myStartX = (CDbl(picOutputwidth) - myPicWidth) / 2
            Else
                myPicHeight = CDbl(OutputHeight) * myScaleX
                myStartY = (CDbl(picOutputheight) - myPicHeight) / 2
            End If

            img = New Bitmap(bmp, New Size(CInt(myPicWidth), CInt(myPicHeight)))
            aspectratio = img

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub PEScreeningResult_MonoClk(ByVal sender As Object, ByVal e As System.EventArgs, ByVal monograph As String) Handles Me.MonoClk

        If ClsDIGeneral.IsDefaultPrinterSet Then
            PrintDocument1.Print()
            'SLR: Changed to GetClipboardData() which is called in frmprescription as save_patienteduction for setClipboarddata
            Dim strEx As String = ""
            Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
            rtbDescription.SelectAll()
            rtbDescription.Copy()
            SavePatientEducation()

        Else
            Me.PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                PrintDocument1.Print()
                'SLR: Changed to GetClipboardData() which is called in frmprescription as save_patienteduction for setClipboarddata
                Dim strEx As String = ""
                Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                rtbDescription.SelectAll()
                rtbDescription.Copy()
                SavePatientEducation()

            End If
        End If
    End Sub
    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        ' Start at the beginning of the text
        m_nFirstCharOnPage = 0
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' To print the boundaries of the current page margins
        ' uncomment the next line:
        'e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.MarginBounds)

        ' make the RichTextBoxEx calculate and render as much text as will
        ' fit on the page and remember the last character printed for the
        ' beginning of the next page
        m_nFirstCharOnPage = rtbDescription.FormatRange(False, _
                                                e, _
                                                m_nFirstCharOnPage, _
                                                rtbDescription.TextLength)

        ' check if there are more pages to print
        If (m_nFirstCharOnPage < rtbDescription.TextLength) Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If
    End Sub
    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        ' Clean up cached information
        rtbDescription.FormatRangeDone()
    End Sub
    Private Sub btnMono_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        RaiseEvent MonoClk(sender, e, m_Monograph)
    End Sub
    Protected Sub SavePatientEducation()
        RaiseEvent SavePatient_Education()
    End Sub
#End Region

End Class
