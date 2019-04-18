Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing

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
        Return Convert.ToInt32(n * 14.4)
    End Function

    ' Free cached data from rich edit control after printing
    Public Sub FormatRangeDone()
        Dim lParam As New IntPtr(0)
        SendMessage(Handle, EM_FORMATRANGE, 0, lParam)
    End Sub
End Class
