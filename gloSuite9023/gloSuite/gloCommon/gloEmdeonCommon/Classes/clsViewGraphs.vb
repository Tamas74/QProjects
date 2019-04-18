Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Drawing
Public Class Hardcopy

    Public Shared Function CreateBitmap(ByVal Control As Control) As Bitmap

        Dim gDest As Drawing.Graphics

        Dim hdcDest As IntPtr

        Dim hdcSrc As Integer

        Dim hWnd As Integer = Control.Handle.ToInt32

        CreateBitmap = New Drawing.Bitmap(Control.Width, Control.Height)

        gDest = Drawing.Graphics.FromImage(CreateBitmap)

        hdcSrc = Win32.GetWindowDC(hWnd)

        hdcDest = gDest.GetHdc

        Win32.BitBlt(hdcDest.ToInt32, 0, 0, Control.Width, Control.Height, hdcSrc, 0, 0, Win32.SRCCOPY)

        gDest.ReleaseHdc(hdcDest)

        Win32.ReleaseDC(hWnd, hdcSrc)
        gDest.Dispose()
        Return CreateBitmap

    End Function

End Class
Public Class Win32

    Public Declare Function BitBlt Lib "gdi32" Alias "BitBlt" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer

    Public Declare Function GetWindowDC Lib "user32" Alias "GetWindowDC" (ByVal hwnd As Integer) As Integer

    Public Declare Function ReleaseDC Lib "user32" Alias "ReleaseDC" (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer

    Public Const SRCCOPY As Integer = &HCC0020

End Class
