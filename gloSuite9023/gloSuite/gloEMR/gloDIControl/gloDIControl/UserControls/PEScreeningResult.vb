Imports System.Drawing
Imports System.Windows.Forms
Public Class PEScreeningResult
    Inherits DIScreeningResults
#Region " Windows Form Designer generated code "
    Private objpatienteducation As PatientEducation
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        MyBase.btnMono.Text = "Print"
        lblHeader.Text = "Medical Instructions Screening"
        'Add any initialization after the InitializeComponent() call
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If (IsNothing(objpatienteducation) = False) Then
                objpatienteducation = Nothing
            End If
            Try
                If (IsNothing(PrintDialog1) = False) Then
                    PrintDialog1.Dispose()
                    PrintDialog1 = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(PrintDocument1) = False) Then
                    PrintDocument1.Dispose()
                    PrintDocument1 = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.SuspendLayout()
        '
        'btnMono
        '
        Me.btnMono.FlatAppearance.BorderSize = 0
        Me.btnMono.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMono.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PEScreeningResult
        '
        Me.Name = "PEScreeningResult"
        Me.ResumeLayout(False)

    End Sub
#End Region
    Private m_nFirstCharOnPage As Integer
    Private img As System.Drawing.Image
    Public Property ClinicLogo() As Image
        Get
            Return img
        End Get
        Set(ByVal value As Image)
            img = value
        End Set
    End Property
    Public Function LoadPatientEducation(ByVal _MIResult As String) As Boolean
        Try

            If Not IsNothing(txtdesc) Then

                If Trim(_MIResult) <> "" Then
                    If Not IsNothing(img) Then
                        Dim myImage As Image = aspectratio(img, 485, 146)
                        If (IsNothing(myImage) = False) Then

                            Try
                                Dim strEx As String = ""
                                Dim gotClip As Boolean = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                                Dim setClip As Boolean = Global.gloWord.gloWord.SetClipBoardImageWithRetry(myImage, 5, strEx)
                                If (setClip) Then
                                    txtdesc.Select(0, 0)
                                    Try
                                        txtdesc.Paste()
                                    Catch ex As Exception

                                    End Try
                                End If
                                If (gotClip) Then
                                    Global.gloWord.gloWord.SetClipboardData()
                                End If

                            Catch ex As Exception
                                'MessageBox.Show("Unable to get image from Clipboard", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                            End Try
                            myImage.Dispose()
                        End If
                        txtdesc.AppendText(vbCrLf)
                    End If

                    txtdesc.AppendText(_MIResult)
                    txtdesc.ReadOnly = True
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function aspectratio(ByVal bmp As Bitmap, ByVal Width As Integer, ByVal Height As Integer) As Image

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

        '----------------------------------------------------------------------------------

        img = New Bitmap(bmp, New Size(CInt(myPicWidth), CInt(myPicHeight)))
        ''picPD_Photo.Image = img



        'Dim change As Double
        'Dim newheight As Double
        'Dim newwidht As Double
        'Dim img As System.Drawing.Image = picPD_Photo.Image
        'If x = y Then
        '    ''image is square...
        '    ''we can use either height or width to resize....
        '    ''use height
        '    change = (yy) / (y)
        '    newheight = y * change
        '    newwidht = x * change
        '    img = New Bitmap(bmp, New Size(CInt(newwidht), CInt(newheight)))
        '    picPD_Photo.Image = img
        'ElseIf y < x Then
        '    '        'image is landscape...use width to resize
        '    change = xx / x
        '    newheight = x * change
        '    newwidht = xx
        '    img = New Bitmap(bmp, New Size(CInt(newwidht), CInt(newheight)))
        '    picPD_Photo.Image = img
        'Else
        '    ''image is portrait...
        '    change = yy / y
        '    newheight = yy
        '    newwidht = x * change
        '    img = New Bitmap(bmp, New Size(CInt(newwidht), CInt(newheight)))

        '    picPD_Photo.Image = img
        'End If
        aspectratio = img
    End Function
    ''End'1. TFS - 14530 - salesforce- GLO2011-0011843 - Porretta: Header on Medication Instructions

    Private Sub PEScreeningResult_MonoClk(ByVal sender As Object, ByVal e As System.EventArgs, ByVal monograph As String) Handles MyBase.MonoClk
        If ClsDIGeneral.IsDefaultPrinterSet Then
            PrintDocument1.Print()

            '' SUDHIR 20100918 ''
            'SLR: Changed to GetClipboardData() which is called in frmprescription as save_patienteduction for setClipboarddata
            Dim strEx As String = ""
            Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
            '   Clipboard.Clear()
            txtdesc.SelectAll()
            txtdesc.Copy()
            SavePatientEducation()
            '' END SUDHIR ''
        Else
            Me.PrintDialog1.Document = PrintDocument1
            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                PrintDocument1.Print()

                '' SUDHIR 20100918 ''
                'SLR: Changed to GetClipboardData() which is called in frmprescription as save_patienteduction for setClipboarddata
                Dim strEx As String = ""
                Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                ' Clipboard.Clear()
                txtdesc.SelectAll()
                txtdesc.Copy()
                SavePatientEducation()
                '' END SUDHIR ''
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
        m_nFirstCharOnPage = txtdesc.FormatRange(False, _
                                                e, _
                                                m_nFirstCharOnPage, _
                                                txtdesc.TextLength)

        ' check if there are more pages to print
        If (m_nFirstCharOnPage < txtdesc.TextLength) Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If
    End Sub
    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        ' Clean up cached information
        txtdesc.FormatRangeDone()
    End Sub
End Class


