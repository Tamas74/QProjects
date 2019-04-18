Imports System.DirectoryServices
Imports System.Net

Public Class clsGetADUser
    Public Shared Function CheckADuser() As Integer
        Dim RetVal As Integer = 0
        'Get the username and domain information
        Dim userName As String = Environment.UserName
        Dim domainName As String = Dns.GetHostByName(Dns.GetHostName()).HostName
        Dim result As SearchResult = Nothing
        'Dim _IsEmailconfig As Boolean = False
        Try
            Dim cnt As Integer = Environment.MachineName.Length + 1
            Dim actdomainname As String = domainName.Substring(cnt)
            domainName = actdomainname

            Dim _intcnt As Integer = domainName.LastIndexOf(".") + 1
            Dim _org As String = domainName.Substring(_intcnt, domainName.Length - _intcnt)

            Dim ldapQueryFormat As String = ("LDAP://" & actdomainname & "/DC=") + Environment.UserDomainName & ",DC=" & _org
            Dim queryFilterFormat As String = "(&(samAccountName=" & userName & ")(objectCategory=person)(objectClass=user))"

            Using root As New DirectoryEntry(ldapQueryFormat)
                Using searcher As New DirectorySearcher(root)
                    searcher.Filter = queryFilterFormat
                    Dim results As SearchResultCollection = searcher.FindAll()
                    result = If((results.Count <> 0), results(0), Nothing)
                End Using
            End Using
            If result IsNot Nothing AndAlso result.Properties.Count > 0 Then
                If DirectCast(result.Properties("mail"), System.Collections.ReadOnlyCollectionBase).Count > 0 Then
                    Dim _UserEmail As String = result.Properties("mail")(0).ToString()
                    '_IsEmailconfig = True
                    RetVal = 1
                End If
            End If
        Catch generatedExceptionName As Exception
            RetVal = 2
        End Try
        Return RetVal ''_IsEmailconfig
    End Function

    Public Shared Function GetADuser() As SearchResult
        'Get the username and domain information
        Dim userName As String = Environment.UserName
        Dim domainName As String = Dns.GetHostByName(Dns.GetHostName()).HostName
        Dim result As SearchResult = Nothing
        Dim _IsEmailconfig As Boolean = False
        Try
            Dim cnt As Integer = Environment.MachineName.Length + 1
            Dim actdomainname As String = domainName.Substring(cnt)
            domainName = actdomainname

            Dim _intcnt As Integer = domainName.LastIndexOf(".") + 1
            Dim _org As String = domainName.Substring(_intcnt, domainName.Length - _intcnt)

            Dim ldapQueryFormat As String = ("LDAP://" & actdomainname & "/DC=") + Environment.UserDomainName & ",DC=" & _org
            Dim queryFilterFormat As String = "(&(samAccountName=" & userName & ")(objectCategory=person)(objectClass=user))"

            Using root As New DirectoryEntry(ldapQueryFormat)
                Using searcher As New DirectorySearcher(root)
                    searcher.Filter = queryFilterFormat
                    Dim results As SearchResultCollection = searcher.FindAll()
                    result = If((results.Count <> 0), results(0), Nothing)
                End Using
            End Using
        Catch generatedExceptionName As Exception
        End Try
        Return result
    End Function
End Class
