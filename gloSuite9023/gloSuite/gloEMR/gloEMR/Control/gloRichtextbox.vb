Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.IO

Public Class gloRichtextbox
    Inherits RichTextBox
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_RECT
        Public left As Int32
        Public top As Int32
        Public right As Int32
        Public bottom As Int32
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_CHARRANGE
        Public cpMin As Int32
        Public cpMax As Int32
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_FORMATRANGE
        Public hdc As IntPtr
        Public hdcTarget As IntPtr
        Public rc As STRUCT_RECT
        Public rcPage As STRUCT_RECT
        Public chrg As STRUCT_CHARRANGE
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure STRUCT_CHARFORMAT
        Public cbSize As Integer
        Public dwMask As UInt32
        Public dwEffects As UInt32
        Public yHeight As Int32
        Public yOffset As Int32
        Public crTextColor As Int32
        Public bCharSet As Byte
        Public bPitchAndFamily As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
        Public szFaceName() As Char
    End Structure
    ' Windows Message defines
    Private Const WM_USER As Int32 = &H400&
    Private Const EM_FORMATRANGE As Int32 = WM_USER + 57
    Private Const EM_GETCHARFORMAT As Int32 = WM_USER + 58
    Private Const EM_SETCHARFORMAT As Int32 = WM_USER + 68

    ' Defines for EM_GETCHARFORMAT/EM_SETCHARFORMAT
    Private SCF_SELECTION As Int32 = &H1&
    Private SCF_WORD As Int32 = &H2&
    Private SCF_ALL As Int32 = &H4&
#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Component overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region
    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, _
                                       ByVal msg As Int32, _
                                       ByVal wParam As Int32, _
                                       ByVal lParam As IntPtr) As Int32
    End Function

    Public Function FormatRange(ByVal measureOnly As Boolean, _
                                    ByVal e As PrintPageEventArgs, _
                                    ByVal charFrom As Integer, _
                                    ByVal charTo As Integer) As Integer
        ' Specify which characters to print
        Dim cr As STRUCT_CHARRANGE
        cr.cpMin = charFrom
        cr.cpMax = charTo

        ' Specify the area inside page margins
        Dim rc As STRUCT_RECT
        rc.top = HundredthInchToTwips(e.MarginBounds.Top)
        rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom)
        rc.left = HundredthInchToTwips(e.MarginBounds.Left)
        rc.right = HundredthInchToTwips(e.MarginBounds.Right)

        ' Specify the page area
        Dim rcPage As STRUCT_RECT
        rcPage.top = HundredthInchToTwips(e.PageBounds.Top)
        rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom)
        rcPage.left = HundredthInchToTwips(e.PageBounds.Left)
        rcPage.right = HundredthInchToTwips(e.PageBounds.Right)

        ' Get device context of output device
        Dim hdc As IntPtr
        hdc = e.Graphics.GetHdc()

        ' Fill in the FORMATRANGE structure
        Dim fr As STRUCT_FORMATRANGE
        fr.chrg = cr
        fr.hdc = hdc
        fr.hdcTarget = hdc
        fr.rc = rc
        fr.rcPage = rcPage

        ' Non-Zero wParam means render, Zero means measure
        Dim wParam As Int32
        If measureOnly Then
            wParam = 0
        Else
            wParam = 1
        End If

        ' Allocate memory for the FORMATRANGE struct and
        ' copy the contents of our struct to this memory
        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr))
        Marshal.StructureToPtr(fr, lParam, False)

        ' Send the actual Win32 message
        Dim res As Integer
        res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam)

        ' Free allocated memory
        Marshal.FreeCoTaskMem(lParam)

        ' and release the device context
        e.Graphics.ReleaseHdc(hdc)

        Return res
    End Function
    ' Convert between 1/100 inch (unit used by the .NET framework)
    ' and twips (1/1440 inch, used by Win32 API calls)
    '
    ' Parameter "n": Value in 1/100 inch
    ' Return value: Value in twips
    Private Function HundredthInchToTwips(ByVal n As Integer) As Int32
        Return Convert.ToInt32(n * 14.4F)
    End Function
    ' Convert between 1/100 inch (unit used by the .NET framework)
    ' and twips (1/1440 inch, used by Win32 API calls)
    '
    ' Parameter "n": Value in 1/100 inch
    ' Return value: Value in twips
    Private Function HundredthInchToTwips(ByVal sf As Single) As Int32
        Return Convert.ToInt32(sf * 14.4F)
    End Function
    ' Free cached data from rich edit control after printing
    Public Sub FormatRangeDone()
        Dim lParam As New IntPtr(0)
        SendMessage(Handle, EM_FORMATRANGE, 0, lParam)
    End Sub

    Private toCreateEMF As Boolean = gloGlobal.gloTSPrint.UseEMFForImages
    Private Function CreateEMFForImages(thisGraphics As Graphics, bmpWidth As Single, bmpHeight As Single) As Integer
        Try
            thisGraphics.Clear(Color.White)
            FitImageToScaleAndKeepAtCenter(thisGraphics)

            Return 0
        Catch
            Return 1
        End Try

    End Function


    Private _FirstCharOnPage As Integer
    Dim _rcPage As STRUCT_RECT
    Dim _rc As STRUCT_RECT


    Public Function printdoc_Print_Conversion(pageWidth As Single, pageHeight As Single, marginBounds As Rectangle) As Dictionary(Of [String], [Byte]())
        Dim ResolutionRect As RectangleF
        If toCreateEMF Then
            ResolutionRect = gloGlobal.CreateEMF.GetMetaFileResolutions(pageWidth, pageHeight)
        Else
            ResolutionRect = New RectangleF(96, 96, 96, 96)
        End If
        Dim myList As New Dictionary(Of [String], [Byte]())()
        Try
            'Dim DpiX As Single = ResolutionRect.X
            'Dim DpiY As Single = ResolutionRect.Y
            ' Specify the area inside page margins
            _rc.top = HundredthInchToTwips(marginBounds.Top)
            _rc.bottom = HundredthInchToTwips(marginBounds.Bottom)
            _rc.left = HundredthInchToTwips(marginBounds.Left)
            _rc.right = HundredthInchToTwips(marginBounds.Right)
            ' Specify the Page area of total paper
            _rcPage.top = HundredthInchToTwips(0)
            _rcPage.bottom = HundredthInchToTwips(pageHeight * ResolutionRect.Height)
            _rcPage.left = HundredthInchToTwips(0)
            _rcPage.right = HundredthInchToTwips(pageWidth * ResolutionRect.Width)


            Dim bmpWidht As Integer = Convert.ToInt32(pageWidth * ResolutionRect.Width)
            Dim bmpHeight As Integer = Convert.ToInt32(pageHeight * ResolutionRect.Height)


            _FirstCharOnPage = 0
            Dim nPageCount As Integer = 1
            Dim bAnyErrorInPng As Boolean = True
            Dim bAnyErrorInEMF As Boolean = True
            Do While _FirstCharOnPage < Me.TextLength
                bAnyErrorInEMF = True
                bAnyErrorInPng = True
                Using NewBitmap As System.Drawing.Bitmap = New Bitmap(bmpWidht, bmpHeight)
                    Dim emfBytes As Byte() = Nothing
                    NewBitmap.SetResolution(ResolutionRect.X, ResolutionRect.Y)
                    Try
                        Try
                            If toCreateEMF Then
                                Dim emfBigBytes As Byte() = gloGlobal.CreateEMF.GetEMFBytes(CSng(NewBitmap.Width) / NewBitmap.HorizontalResolution, CSng(NewBitmap.Height) / NewBitmap.VerticalResolution, pageWidth * gloGlobal.CreateEMF.dotsper300Inch, pageHeight * gloGlobal.CreateEMF.dotsper300Inch, AddressOf CreateEMFForImages)
                                emfBytes = gloGlobal.CreateEMF.ScaleDownEMF(emfBigBytes, pageWidth, pageHeight)
                                bAnyErrorInEMF = False
                            End If
                        Catch
                        End Try
                        If bAnyErrorInEMF Then
                            'bAnyError = False
                            toCreateEMF = False
                            Try
                                Using eGraphics As System.Drawing.Graphics = Graphics.FromImage(NewBitmap)
                                    FitImageToScaleAndKeepAtCenter(eGraphics)

                                    Using ms As New MemoryStream()
                                        NewBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                        Try

                                            ms.Flush()
                                        Catch
                                        End Try

                                        myList.Add(nPageCount.ToString(), ms.ToArray())
                                        bAnyErrorInPng = False
                                        Try

                                            ms.Close()
                                        Catch
                                        End Try
                                    End Using
                                End Using
                            Catch ex As Exception

                            End Try

                        Else
                            myList.Add(nPageCount.ToString(), emfBytes)
                            bAnyErrorInPng = False
                        End If
                    Catch ex As Exception

                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during conversion before printing in gloRichTexBox", False)
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try
                    If bAnyErrorInPng Then
                        Return myList
                    End If
                End Using
                nPageCount += 1
            Loop
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return myList
    End Function
    ' Allocate memory for the FORMATRANGE struct and
    ' copy the contents of our struct to this memory
    Private Sub FitImageToScaleAndKeepAtCenter(eGraphics As System.Drawing.Graphics)
        eGraphics.Clear(Color.White)
        Dim myColor As Color = Me.BackColor
        Me.BackColor = Color.White
        ' Get device context of output device
        Dim hdc As IntPtr
        hdc = eGraphics.GetHdc()
        ' Fill in the FORMATRANGE structure
        Dim fr As STRUCT_FORMATRANGE
        ' Specify which characters to print
        fr.chrg.cpMin = _FirstCharOnPage
        fr.chrg.cpMax = Me.TextLength

        fr.hdc = hdc
        fr.hdcTarget = hdc
        
        fr.rc = _rc
        fr.rcPage = _rcPage

        ' Non-Zero wParam means render, Zero means measure
        Dim wParam As Int32 = 1
        

        Dim lParam As IntPtr
        lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr))
        Marshal.StructureToPtr(fr, lParam, False)

        ' Send the actual Win32 message

        _FirstCharOnPage = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam)

        ' Free allocated memory
        Marshal.FreeCoTaskMem(lParam)

        ' and release the device context
        eGraphics.ReleaseHdc(hdc)
        Me.BackColor = myColor
    End Sub


End Class
