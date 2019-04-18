Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO

Public Class EncodingDetector
    ''' <summary>
    ''' Helper class to store information about encodings
    ''' with a preamble
    ''' </summary>
    Protected Class PreambleInfo
        Protected _encoding As Encoding
        Protected _preamble As Byte()

        ''' <summary>
        ''' Property Encoding (Encoding).
        ''' </summary>
        Public ReadOnly Property Encoding() As Encoding
            Get
                Return Me._encoding
            End Get
        End Property

        ''' <summary>
        ''' Property Preamble (byte[]).
        ''' </summary>
        Public ReadOnly Property Preamble() As Byte()
            Get
                Return Me._preamble
            End Get
        End Property

        ''' <summary>
        ''' Constructor with preamble and encoding
        ''' </summary>
        ''' <param name="encoding"></param>
        ''' <param name="preamble"></param>
        Public Sub New(encoding As Encoding, preamble As Byte())
            Me._encoding = encoding
            Me._preamble = preamble
        End Sub
    End Class

    ' The list of encodings with a preamble,
    ' sorted longest preamble first.
    Protected Shared _preambles As SortedList(Of Integer, PreambleInfo) = Nothing

    ' Maximum length of all preamles
    Protected Shared _maxPreambleLength As Integer = 0

    ''' <summary>
    ''' Read the contents of a text file as a string. Scan for a preamble first.
    ''' If a preamble is found, the corresponding encoding is used.
    ''' If no preamble is found, the supplied defaultEncoding is used.
    ''' </summary>
    ''' <param name="filename">The name of the file to read</param>
    ''' <param name="defaultEncoding">The encoding to use if no preamble present</param>
    ''' <param name="usedEncoding">The actual encoding used</param>
    ''' <returns>The contents of the file as a string</returns>
    Public Shared Function ReadAllText(filename As String, defaultEncoding As Encoding, ByRef usedEncoding As Encoding) As String
        ' Read the contents of the file as an array of bytes
        Dim bytes As Byte() = File.ReadAllBytes(filename)

        ' Detect the encoding of the file:
        usedEncoding = DetectEncoding(bytes)

        ' If none found, use the default encoding.
        ' Otherwise, determine the length of the encoding markers in the file
        Dim offset As Integer
        If usedEncoding Is Nothing Then
            offset = 0
            usedEncoding = defaultEncoding
        Else
            offset = usedEncoding.GetPreamble().Length
        End If

        ' Now interpret the bytes according to the encoding,
        ' skipping the preample (if any)
        Return usedEncoding.GetString(bytes, offset, bytes.Length - offset)
    End Function

    ''' <summary>
    ''' Detect the encoding in an array of bytes.
    ''' </summary>
    ''' <param name="bytes"></param>
    ''' <returns>The encoding found, or null</returns>
    Public Shared Function DetectEncoding(bytes As Byte()) As Encoding
        ' Scan for encodings if we haven't done so
        If _preambles Is Nothing Then
            ScanEncodings()
        End If

        ' Try each preamble in turn
        For Each info As PreambleInfo In _preambles.Values
            ' Match all bytes in the preamble
            Dim match As Boolean = True

            If bytes.Length >= info.Preamble.Length Then
                For i As Integer = 0 To info.Preamble.Length - 1
                    If bytes(i) <> info.Preamble(i) Then
                        match = False
                        Exit For
                    End If
                Next
                If match Then
                    Return info.Encoding
                End If
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Detect the encoding of a file. Reads just enough of
    ''' the file to be able to detect a preamble.
    ''' </summary>
    ''' <param name="filename">The path name of the file</param>
    ''' <returns>The encoding detected, or null if no preamble found</returns>
    Public Shared Function DetectEncoding(filename As String) As Encoding
        ' Scan for encodings if we haven't done so
        If _preambles Is Nothing Then
            ScanEncodings()
        End If

        Using stream As FileStream = File.OpenRead(filename)
            ' Never read more than the length of the file
            ' or the maximum preamble length
            Dim n As Long = stream.Length

            ' No bytes? No encoding!
            If n = 0 Then
                Return Nothing
            End If

            ' Read the minimum amount necessary
            If n > _maxPreambleLength Then
                n = _maxPreambleLength
            End If

            Dim bytes As Byte() = New Byte(n - 1) {}

            stream.Read(bytes, 0, CInt(n))

            ' Detect the encoding from the byte array
            Return DetectEncoding(bytes)
        End Using
    End Function

    ''' <summary>
    ''' Loop over all available encodings and store those
    ''' with a preamble in the _preambles list.
    ''' The list is sorted by preamble length,
    ''' longest preamble first. This prevents
    ''' a short preamble 'masking' a longer one
    ''' later in the list.
    ''' </summary>
    Protected Shared Sub ScanEncodings()
        ' Create a new sorted list of preambles
        _preambles = New SortedList(Of Integer, PreambleInfo)()

        ' Loop over all encodings
        For Each encodingInfo As EncodingInfo In Encoding.GetEncodings()
            ' Do we have a preamble?
            Dim preamble As Byte() = encodingInfo.GetEncoding().GetPreamble()
            If preamble.Length > 0 Then
                ' Add it to the collection, inversely sorted by preamble length
                ' (and code page, to keep the keys unique)
                _preambles.Add(-(preamble.Length * 1000000 + encodingInfo.CodePage), New PreambleInfo(encodingInfo.GetEncoding(), preamble))

                ' Update the maximum preamble length if this one's longer
                If preamble.Length > _maxPreambleLength Then
                    _maxPreambleLength = preamble.Length
                End If
            End If
        Next
    End Sub
End Class

