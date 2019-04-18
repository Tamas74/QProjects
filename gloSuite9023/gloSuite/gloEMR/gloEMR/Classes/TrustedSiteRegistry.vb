Imports Microsoft.Win32
Imports System.Runtime.CompilerServices

Public Class TrustedSiteRegistry
    Const DomainsKeyLocation As String = "Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains"
    Const TRUSTED_SITE_CODE As Integer = &H2

    Public Shared Sub SaveToTrustedSite(domain As String)
        '' carecoordination.triarqclouds.com
        '' https://carecoordination.triarqclouds.com/poctest''
        Dim strhttptype As String = domain.Substring(0, domain.IndexOf("://"))
        domain = domain.Replace("https", "").Replace("http", "").Replace("://", "")
        If (domain.IndexOf("/") > 1) Then
            domain = domain.Substring(0, domain.IndexOf("/"))
        End If
        If (domain.IndexOf(":") > 1) Then
            domain = domain.Substring(0, domain.IndexOf(":"))
        End If
        ''domain = domain.Substring(0, domain.IndexOf("com") + 3).Replace("https", "").Replace("http", "").Replace("//", "").Replace("/", "").Replace(":", "")

        Dim splstrdomain As String() = domain.Split(".")
        If (splstrdomain.Length > 2) Then
            Dim domainstr As String = splstrdomain(1) & "." & splstrdomain(2)
            Dim subdomains As Dictionary(Of String, String) = New Dictionary(Of String, String)() From { _
                       {String.Empty, strhttptype} _
                                                                                                       }
            Using currentUserKey As RegistryKey = Registry.CurrentUser
                currentUserKey.GetOrCreateTrustedSubKey(DomainsKeyLocation, domainstr)
                currentUserKey.GetOrCreateTrustedSubKey(DomainsKeyLocation & "\" & domainstr, splstrdomain(0))
                For Each subdomain As Object In subdomains
                    CreateSubdomainKeyAndValue(currentUserKey, DomainsKeyLocation & "\" & domainstr, splstrdomain(0), CType(subdomain, Global.System.Collections.Generic.KeyValuePair(Of String, String)), TRUSTED_SITE_CODE)
                Next
            End Using
        ElseIf (splstrdomain.Length >= 1) Then

            Dim subdomains As Dictionary(Of String, String) = New Dictionary(Of String, String)() From { _
                       {String.Empty, strhttptype} _
                                                                                                       }
            Using currentUserKey As RegistryKey = Registry.CurrentUser

                currentUserKey.GetOrCreateTrustedSubKey(DomainsKeyLocation & "\", splstrdomain(0))
                For Each subdomain As Object In subdomains
                    CreateSubdomainKeyAndValue(currentUserKey, DomainsKeyLocation & "\", splstrdomain(0), CType(subdomain, Global.System.Collections.Generic.KeyValuePair(Of String, String)), TRUSTED_SITE_CODE)
                Next
            End Using
        End If
    End Sub

    Private Shared Sub CreateSubdomainKeyAndValue(currentUserKey As RegistryKey, domainsKeyLocation As String, domain As String, subdomain As KeyValuePair(Of String, String), zone As Integer)
        Using subdomainRegistryKey As RegistryKey = currentUserKey.GetOrCreateTrustedSubKey(String.Format("{0}\{1}", domainsKeyLocation, domain), subdomain.Key)
            Dim objSubDomainValue As Object = subdomainRegistryKey.GetValue(subdomain.Value)
            If objSubDomainValue Is Nothing OrElse Convert.ToInt32(objSubDomainValue) <> zone Then
                subdomainRegistryKey.SetValue(subdomain.Value, zone, RegistryValueKind.DWord)
            End If
        End Using
    End Sub
End Class
Public Module RegistryKeyExtensionMethods
    <Extension()> _
    Public Function GetOrCreateTrustedSubKey(registryKey As RegistryKey, parentString As String, subString As String) As RegistryKey

        Dim subKey As RegistryKey = Nothing
       
        subKey = registryKey.OpenSubKey(Convert.ToString(parentString & Convert.ToString("\")) & subString, True)
        If subKey Is Nothing Then
            subKey = registryKey.CreateTrustedSiteSubKey(parentString, subString)
        End If
      
        Return subKey
    End Function

    <Extension()> _
    Public Function CreateTrustedSiteSubKey(registryKey As RegistryKey, parentKeyLocation As String, key As String) As RegistryKey
        Dim parentKey As RegistryKey = registryKey.OpenSubKey(parentKeyLocation, True)
        If parentKey Is Nothing Then
            Throw New NullReferenceException(String.Format("Missing parent key: {0}", parentKeyLocation))
        End If
        Dim createdKey As RegistryKey = parentKey.CreateSubKey(key)
        If createdKey Is Nothing Then
            Throw New Exception(String.Format("Key not created: {0}", key))
        End If
        Return createdKey
    End Function
End Module

