Imports System.Data
Imports System.Configuration
Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
Imports System.Text


Public Class RC4
    Private Const N As Integer = 256
    Private sbox As Integer()
    Private m_password As String
    Private m_text As String

    Public Sub New(password As String, text As String)
        Me.m_password = password
        Me.m_text = text
    End Sub

    Public Sub New(password As String)
        Me.m_password = password
    End Sub

    Public Property Text() As String
        Get
            Return m_text
        End Get
        Set(value As String)
            m_text = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return m_password
        End Get
        Set(value As String)
            m_password = Value
        End Set
    End Property

    Public Function EnDeCrypt() As String
        RC4Initialize()

        Dim i As Integer = 0, j As Integer = 0, k As Integer = 0
        Dim cipher As New StringBuilder()
        For a As Integer = 0 To m_text.Length - 1
            i = (i + 1) Mod N
            j = (j + sbox(i)) Mod N
            Dim tempSwap As Integer = sbox(i)
            sbox(i) = sbox(j)
            sbox(j) = tempSwap

            k = sbox((sbox(i) + sbox(j)) Mod N)
            Dim cipherBy As Integer = (AscW(m_text(a))) Xor k
            'xor operation
            cipher.Append(Convert.ToChar(cipherBy))
        Next
        Return cipher.ToString()
    End Function

    Public Shared Function StrToHexStr(str As String) As String
        Dim sb As New StringBuilder()
        For i As Integer = 0 To str.Length - 1
            Dim v As Integer = Convert.ToInt32(str(i))
            sb.Append(String.Format("{0:X2}", v))
        Next
        Return sb.ToString()
    End Function

    Public Shared Function HexStrToStr(hexStr As String) As String
        Dim sb As New StringBuilder()
        For i As Integer = 0 To hexStr.Length - 1 Step 2
            Dim n As Integer = Convert.ToInt32(hexStr.Substring(i, 2), 16)
            sb.Append(Convert.ToChar(n))
        Next
        Return sb.ToString()
    End Function

    Private Sub RC4Initialize()
        sbox = New Integer(N - 1) {}
        Dim key As Integer() = New Integer(N - 1) {}
        Dim n__1 As Integer = m_password.Length
        For a As Integer = 0 To N - 1
            key(a) = AscW(m_password(a Mod n__1))
            sbox(a) = a
        Next

        Dim b As Integer = 0
        For a As Integer = 0 To N - 1
            b = (b + sbox(a) + key(a)) Mod N
            Dim tempSwap As Integer = sbox(a)
            sbox(a) = sbox(b)
            sbox(b) = tempSwap
        Next
    End Sub


End Class

