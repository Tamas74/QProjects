Imports System.IO
Imports System.Text
Imports System.Security.Cryptography


Public Class clsencryption
    Implements IDisposable
    ' Use DES CryptoService with Private key pair
    Private key() As Byte = {} ' we are going to pass in the key portion in our method calls
    Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

    Public Function DecryptFromBase64String(ByVal stringToDecrypt As String, ByVal sEncryptionKey As String) As String






        ' Note: The DES CryptoService only accepts certain key byte lengths
        ' We are going to make things easy by insisting on an 8 byte legal key length
        Dim ms As MemoryStream = Nothing
        Dim cs As CryptoStream = Nothing
        Dim des As DESCryptoServiceProvider = Nothing
        Try

            If Trim(stringToDecrypt) <> "" Then

                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8))
                des = New DESCryptoServiceProvider
                ' we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                Dim inputByteArray() As Byte = Convert.FromBase64String(stringToDecrypt)
                ' now decrypt the regular string
                ms = New MemoryStream
                cs = New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())

            Else
                Return ""
            End If

        Catch e As Exception
            Return e.Message
        Finally
            If (IsNothing(ms) = False) Then
                ms.Dispose()
                ms = Nothing
            End If
            If (IsNothing(cs) = False) Then
                cs.Dispose()
                cs = Nothing
            End If
            If (IsNothing(des) = False) Then
                des.Dispose()
                des = Nothing
            End If
        End Try
    End Function

    Public Function EncryptToBase64String(ByVal stringToEncrypt As String, ByVal SEncryptionKey As String) As String
        Dim ms As MemoryStream = Nothing
        Dim cs As CryptoStream = Nothing
        Dim des As DESCryptoServiceProvider = Nothing

        Try
            key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8))
            des = New DESCryptoServiceProvider
            ' convert our input string to a byte array
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(stringToEncrypt)
            'now encrypt the bytearray
            ms = New MemoryStream
            cs = New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            ' now return the byte array as a "safe for XMLDOM" Base64 String
            Return Convert.ToBase64String(ms.ToArray())
        Catch e As Exception
            Return e.Message
        Finally
            If (IsNothing(ms) = False) Then
                ms.Dispose()
                ms = Nothing
            End If
            If (IsNothing(cs) = False) Then
                cs.Dispose()
                cs = Nothing
            End If
            If (IsNothing(des) = False) Then
                des.Dispose()
                des = Nothing
            End If
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not IsNothing(key) Then
                    If key.Length > 0 Then
                        Array.Clear(key, 0, key.Length)
                    End If
                End If
                key = Nothing
                If Not IsNothing(IV) Then
                    If IV.Length > 0 Then
                        Array.Clear(IV, 0, IV.Length)
                    End If
                End If
                IV = Nothing
            End If
            ' TODO: dispose managed state (managed objects).
        End If

        ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
        ' TODO: set large fields to null.

        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
