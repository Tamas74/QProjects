'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.DirectoryServices
Public Class clsWindowsGroupsUsers

    Enum enmWindowsGroupsUsers
        Groups
        Users
    End Enum
    Public Function PopulateWindowsGroups() As Collection
        Dim arrGroups As New Collection
        Try
            Dim objDirEntry As New DirectoryEntry
            With objDirEntry
                .Path = "WinNT://" & gstrDomainName & "/" & gstrWindowsServerName
                Dim entry As DirectoryEntry
                For Each entry In .Children
                    If Trim(entry.SchemaClassName) = "Group" Then
                        arrGroups.Add(entry.Name)
                    End If
                Next
            End With
            Return arrGroups
        Catch ex As Exception
            Return arrGroups
        End Try

    End Function

    Public Function PopulateWindowsUsers(ByVal strGroupName As String) As Collection
        Dim arrUsers As New Collection
        Dim strDirEntryPath As String
        strDirEntryPath = "WinNT://" & gstrDomainName & "/" & gstrWindowsServerName & "/" & strGroupName & ",group"
        Dim users As Object

        Dim group As New DirectoryEntry(strDirEntryPath)
        users = group.Invoke("members")

        Dim user1 As Object
        'Dim UsersCollection As New Collection

        For Each user1 In CType(users, IEnumerable)
            Try
                Dim userEntry As New System.DirectoryServices.DirectoryEntry(user1)
                arrUsers.Add(userEntry.Name)
            Catch e1 As Exception
                'Return e1
                Return Nothing
                Exit Function
            End Try
        Next


        Return arrUsers
    End Function

    Public Function PopulateWindowsUsers() As Collection
        Dim arrUsers As New Collection
        Try
            Dim objDirEntry As New DirectoryEntry
            With objDirEntry
                .Path = "WinNT://" & gstrDomainName & "/" & gstrWindowsServerName

                Dim entry As DirectoryEntry
                For Each entry In .Children
                    If Trim(entry.SchemaClassName) = "User" Then
                        arrUsers.Add(entry.Name)
                    End If
                Next
            End With
            Return arrUsers
        Catch ex As Exception
            Return arrUsers
        End Try
    End Function


    Public Function PopulateMachines() As Collection
        Dim arrUsers As New Collection
        Dim objDirEntry As New DirectoryEntry
        With objDirEntry
            .Path = "WinNT://" & gstrDomainName
            .Children.SchemaFilter.Add("computer")
            Dim entry As DirectoryEntry
            For Each entry In .Children
                arrUsers.Add(entry.Name)
            Next
        End With
        Return arrUsers
    End Function


    Public Function PopulateGroupsUsersInformation(ByVal enmWindowsGroups As enmWindowsGroupsUsers, ByVal strGroupsUserName As String) As DataTable
        Dim dtWindowsGroupsUsers As New DataTable
        Dim colPropertyName As New DataColumn("Property Name")
        Dim colPropertyValue As New DataColumn("Property Value")
        dtWindowsGroupsUsers.Columns.Add(colPropertyName)
        dtWindowsGroupsUsers.Columns.Add(colPropertyValue)
        Dim strProperty As String
        Dim drRow As DataRow

        Dim objDirEntry As New DirectoryEntry
        With objDirEntry
            .Path = "WinNT://" & gstrDomainName & "/" & gstrWindowsServerName
            Dim entry As DirectoryEntry
            For Each entry In .Children
                If enmWindowsGroups = enmWindowsGroupsUsers.Groups Then
                    If Trim(entry.SchemaClassName) = "Group" And Trim(entry.Name) = strGroupsUserName Then
                        For Each strProperty In entry.Properties.PropertyNames
                            drRow = dtWindowsGroupsUsers.NewRow
                            drRow(colPropertyName) = strProperty
                            drRow(colPropertyValue) = entry.Properties(strProperty)(0)
                            dtWindowsGroupsUsers.Rows.Add(drRow)
                        Next
                        Return dtWindowsGroupsUsers
                    End If
                Else
                    If Trim(entry.SchemaClassName) = "User" And Trim(entry.Name) = strGroupsUserName Then
                        For Each strProperty In entry.Properties.PropertyNames
                            drRow = dtWindowsGroupsUsers.NewRow
                            drRow(colPropertyName) = strProperty
                            drRow(colPropertyValue) = entry.Properties(strProperty)(0)
                            dtWindowsGroupsUsers.Rows.Add(drRow)
                        Next
                        Return dtWindowsGroupsUsers
                    End If
                End If
            Next
        End With
        Return dtWindowsGroupsUsers

    End Function


End Class
