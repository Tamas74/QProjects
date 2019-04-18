Imports System.Diagnostics
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography


Public Class clsEncryption
    Public Class clsencryption

        ' Use DES CryptoService with Private key pair 
        Private key As Byte() = New Byte() {}
        ' we are going to pass in the key portion in our method calls 
        Private IV As Byte() = New Byte() {&H12, &H34, &H56, &H78, &H90, &HAB, _
        &HCD, &HEF}

        Public Function DecryptFromBase64String(ByVal stringToDecrypt As String, ByVal sEncryptionKey As String) As String
            Dim inputByteArray As Byte() = New Byte(stringToDecrypt.Length) {}
            ' Note: The DES CryptoService only accepts certain key byte lengths 
            ' We are going to make things easy by insisting on an 8 byte legal key length 

            Try
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8))
                Dim des As New DESCryptoServiceProvider()
                ' we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string 
                inputByteArray = Convert.FromBase64String(stringToDecrypt)
                ' now decrypt the regular string 
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function

        Public Function EncryptToBase64String(ByVal stringToEncrypt As String, ByVal SEncryptionKey As String) As String
            Try
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8))
                Dim des As New DESCryptoServiceProvider()
                ' convert our input string to a byte array 
                Dim inputByteArray As Byte() = Encoding.UTF8.GetBytes(stringToEncrypt)
                'now encrypt the bytearray 
                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                ' now return the byte array as a "safe for XMLDOM" Base64 String 
                Return Convert.ToBase64String(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function

    End Class
End Class
