'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports SQLDMO

Public Class clsDatabaseTool
#Region " Enumerators"
    Enum enmObjects
        Tables
        Views
        StoredProcedures
        Users
        Defaults
        Rules
        SystemDataTypes
        DatabaseRoles
        UserDefinedFunctions
        UserDefinedDatatypes
    End Enum
    Enum enmTableObjects
        Checks
        ClusteredIndex
        Columns
        Indexes
        Keys
        PrimaryKey
        Triggers
    End Enum
#End Region

#Region " Private Variables"
    Dim _sSQLServerName As String = gstrSQLServerName
    Dim _sSQLDatabaseName As String = gstrDatabaseName
#End Region

#Region " Public Properties"
    Public Property SQLServerName() As String
        Get
            Return _sSQLServerName
        End Get
        Set(ByVal Value As String)
            _sSQLServerName = Value
        End Set
    End Property
    Public Property SQLDatabaseName() As String
        Get
            Return _sSQLDatabaseName
        End Get
        Set(ByVal Value As String)
            _sSQLDatabaseName = Value
        End Set
    End Property
#End Region

#Region " Public Functions"
    Public Function Fill_Summary() As String
        Dim strSummary As String
        strSummary = "SQL Server Name" & vbCrLf
        strSummary = strSummary & Space(10) & _sSQLServerName & vbCrLf

        strSummary = strSummary & "Database Name" & vbCrLf
        strSummary = strSummary & Space(10) & _sSQLDatabaseName & vbCrLf

        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)
        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)

        strSummary = strSummary & "Create Date" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.CreateDate() & vbCrLf


        strSummary = strSummary & "Allocated Space" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.DataSpaceUsage() / 1000 & " KB" & vbCrLf

        strSummary = strSummary & "Free Space" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.SpaceAvailableInMB * 1000 & " KB" & vbCrLf

        strSummary = strSummary & "No of Tables" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.Tables.Count & vbCrLf

        strSummary = strSummary & "No of Stored Procedures" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.StoredProcedures.Count & vbCrLf

        strSummary = strSummary & "No of Views" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.Views.Count & vbCrLf


        strSummary = strSummary & "File Location" & vbCrLf
        strSummary = strSummary & Space(10) & objSQLDatabase.PrimaryFilePath & vbCrLf


        objSQLServer.Close()
        Return strSummary
    End Function
    Public Function Fill_SQLObjects(ByVal enmDBObjects As enmObjects) As Collection
        Dim clObjects As New Collection
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)


        Dim objSQLDatabase As Database2

        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)

        Dim nCount As Integer
        Select Case enmDBObjects
            Case enmObjects.Tables
                For nCount = 1 To objSQLDatabase.Tables.Count
                    clObjects.Add(objSQLDatabase.Tables.Item(nCount).Name)
                Next
            Case enmObjects.Views
                For nCount = 1 To objSQLDatabase.Views.Count
                    clObjects.Add(objSQLDatabase.Views.Item(nCount).Name)
                Next

            Case enmObjects.StoredProcedures
                For nCount = 1 To objSQLDatabase.StoredProcedures.Count
                    clObjects.Add(objSQLDatabase.StoredProcedures.Item(nCount).Name)
                Next
            Case enmObjects.Users
                For nCount = 1 To objSQLDatabase.Users.Count
                    clObjects.Add(objSQLDatabase.Users.Item(nCount).Name)
                Next
            Case enmObjects.Defaults
                For nCount = 1 To objSQLDatabase.Defaults.Count
                    clObjects.Add(objSQLDatabase.Defaults.Item(nCount).Name)
                Next
            Case enmObjects.Rules
                For nCount = 1 To objSQLDatabase.Rules.Count
                    clObjects.Add(objSQLDatabase.Rules.Item(nCount).Name)
                Next
            Case enmObjects.SystemDataTypes
                For nCount = 1 To objSQLDatabase.SystemDatatypes.Count
                    clObjects.Add(objSQLDatabase.SystemDatatypes.Item(nCount).Name)
                Next
            Case enmObjects.DatabaseRoles
                For nCount = 1 To objSQLDatabase.DatabaseRoles.Count
                    clObjects.Add(objSQLDatabase.DatabaseRoles.Item(nCount).Name)
                Next
            Case enmObjects.UserDefinedFunctions
                For nCount = 1 To objSQLDatabase.UserDefinedFunctions.Count
                    clObjects.Add(objSQLDatabase.UserDefinedFunctions.Item(nCount).Name)
                Next

            Case enmObjects.UserDefinedDatatypes
                For nCount = 1 To objSQLDatabase.UserDefinedDatatypes.Count
                    clObjects.Add(objSQLDatabase.UserDefinedDatatypes.Item(nCount).Name)
                Next
        End Select
        objSQLServer.Close()
        objSQLDatabase = Nothing
        objSQLServer = Nothing
        Return clObjects
    End Function
    Public Function Fill_TableObjects(ByVal strTableName As String, ByVal enmTBObjects As enmTableObjects) As Collection
        Dim clObjects As New Collection
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)
        Dim objTable As Table2
        objTable = objSQLDatabase.Tables.Item(strTableName)
        Dim nCount As Integer
        Select Case enmTBObjects
            Case enmTableObjects.Checks
                For nCount = 1 To objTable.Checks.Count
                    clObjects.Add(objTable.Checks.Item(nCount).Name)
                Next
            Case enmTableObjects.ClusteredIndex
                If IsNothing(objTable.ClusteredIndex) = False Then
                    If IsNothing(objTable.ClusteredIndex.Name) = False Then
                        clObjects.Add(objTable.ClusteredIndex.Name)
                    End If
                End If
            Case enmTableObjects.Columns
                For nCount = 1 To objTable.Columns.Count
                    clObjects.Add(objTable.Columns.Item(nCount).Name)
                Next
            Case enmTableObjects.Indexes
                For nCount = 1 To objTable.Indexes.Count
                    clObjects.Add(objTable.Indexes.Item(nCount).Name)
                Next
            Case enmTableObjects.Keys
                For nCount = 1 To objTable.Keys.Count
                    clObjects.Add(objTable.Keys.Item(nCount).Name)
                Next
            Case enmTableObjects.PrimaryKey
                If IsNothing(objTable.PrimaryKey) = False Then
                    If IsNothing(objTable.PrimaryKey.Name) = False Then
                        clObjects.Add(objTable.PrimaryKey.Name)
                    End If
                End If
            Case enmTableObjects.Triggers
                For nCount = 1 To objTable.Triggers.Count
                    clObjects.Add(objTable.Triggers.Item(nCount).Name)
                Next
        End Select

        objSQLServer.Close()
        Return clObjects
    End Function
    Public Function Fill_DetailsViewObjects(ByVal strViewName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objView As View
        objView = objSQLDatabase.Views.Item(strViewName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        'sarika 25th june 07
        'Dim nCount As Integer
        '---

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objView.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "System Object"
        drRow(1) = objView.SystemObject
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Text"
        drRow(1) = objView.Text
        dtTable.Rows.Add(drRow)

        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsStoredProcedureObjects(ByVal strSPName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objSP As StoredProcedure
        objSP = objSQLDatabase.StoredProcedures.Item(strSPName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow

        'sarika 25th june 07
        'Dim nCount As Integer
        '-----

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objSP.CreateDate
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objSP.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "System Object"
        drRow(1) = objSP.SystemObject
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Text"
        drRow(1) = objSP.Text
        dtTable.Rows.Add(drRow)

        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsRoleObjects(ByVal strRoleName As String) As DataTable
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        Dim drRow As DataRow
        'sarika 25th june 07
        'Dim nCount As Integer
        '--
        drRow = dtTable.NewRow
        drRow(0) = "Database Role"
        drRow(1) = strRoleName
        dtTable.Rows.Add(drRow)
        Return dtTable
    End Function
    Public Function Fill_DetailsTableObjects(ByVal strTableName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objTable As Table2
        objTable = objSQLDatabase.Tables.Item(strTableName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        'sarika 25th june 07
        'Dim nCount As Integer
        '---

        drRow = dtTable.NewRow
        drRow(0) = "No of Columns"
        drRow(1) = objTable.Columns.Count
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objTable.CreateDate
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objTable.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Has Index"
        drRow(1) = objTable.HasIndex
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Has Clustered Index"
        drRow(1) = objTable.HasClusteredIndex
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "No of Rows"
        drRow(1) = objTable.Rows
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Data Space Used"
        drRow(1) = objTable.DataSpaceUsed
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "System Object"
        drRow(1) = objTable.SystemObject
        dtTable.Rows.Add(drRow)

        objSQLServer.Close()
        Return dsData.Tables(0)

    End Function
    Public Function Fill_DetailsDefaultObjects(ByVal strDefaultName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objDefault As [Default]
        objDefault = objSQLDatabase.Defaults.Item(strDefaultName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        'sarika 25th june 07
        'Dim nCount As Integer
        '----

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objDefault.CreateDate
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objDefault.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Text"
        drRow(1) = objDefault.Text
        dtTable.Rows.Add(drRow)



        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsRulesObjects(ByVal strRuleName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objRule As Rule
        objRule = objSQLDatabase.Rules.Item(strRuleName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow

        'sarika 25th june 07
        'Dim nCount As Integer
        '-----

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objRule.CreateDate
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objRule.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Text"
        drRow(1) = objRule.Text
        dtTable.Rows.Add(drRow)


        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function

    Public Function Fill_DetailsSystemDataTypeObjects(ByVal strSystemDataTypeName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objSystemDatatype As SystemDatatype
        objSystemDatatype = objSQLDatabase.SystemDatatypes.Item(strSystemDataTypeName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow

        'sarika 25th june 07
        'Dim nCount As Integer
        '---

        drRow = dtTable.NewRow
        drRow(0) = "Allow Indentity"
        drRow(1) = objSystemDatatype.AllowIdentity
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Allow Length"
        drRow(1) = objSystemDatatype.AllowLength
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Allow Nulls"
        drRow(1) = objSystemDatatype.AllowNulls
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Is Numeric"
        drRow(1) = objSystemDatatype.IsNumeric
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Is Variable Length"
        drRow(1) = objSystemDatatype.IsVariableLength
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow

        drRow(0) = "Maximum Char"
        drRow(1) = objSystemDatatype.MaximumChar
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow

        drRow(0) = "Maximum Length"
        drRow(1) = objSystemDatatype.MaximumLength
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow


        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsUserDataTypeObjects(ByVal strUserDataTypeName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objUserDatatype As UserDefinedDatatype
        objUserDatatype = objSQLDatabase.UserDefinedDatatypes.Item(strUserDataTypeName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        'Dim nCount As Integer

        drRow = dtTable.NewRow
        drRow(0) = "Allow Indentity"
        drRow(1) = objUserDatatype.AllowIdentity
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Maximum Size"
        drRow(1) = objUserDatatype.MaxSize
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Allow Nulls"
        drRow(1) = objUserDatatype.AllowNulls
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "Is Variable Length"
        drRow(1) = objUserDatatype.IsVariableLength
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow

        drRow = dtTable.NewRow
        drRow(0) = "Rule"
        drRow(1) = objUserDatatype.Rule
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow

        drRow = dtTable.NewRow
        drRow(0) = "Numeric Precision"
        drRow(1) = objUserDatatype.NumericPrecision
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow

        drRow = dtTable.NewRow
        drRow(0) = "Numeric Scale"
        drRow(1) = objUserDatatype.NumericScale
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow


        drRow = dtTable.NewRow
        drRow(0) = "Deafult Value"
        drRow(1) = objUserDatatype.Default
        dtTable.Rows.Add(drRow)
        drRow = dtTable.NewRow



        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsUserFunctionsObjects(ByVal strUserFunctionsName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objUserDefinedFunction As UserDefinedFunction
        objUserDefinedFunction = objSQLDatabase.UserDefinedDatatypes.Item(strUserFunctionsName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        '  Dim nCount As Integer

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objUserDefinedFunction.CreateDate
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "Is Encrypted"
        drRow(1) = objUserDefinedFunction.Encrypted
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objUserDefinedFunction.Owner
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "System Object"
        drRow(1) = objUserDefinedFunction.SystemObject
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "Text"
        drRow(1) = objUserDefinedFunction.Text
        dtTable.Rows.Add(drRow)

        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsUserObjects(ByVal strUserName As String) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)


        Dim objUser As User
        objUser = objSQLDatabase.Users.Item(strUserName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        ' Dim nCount As Integer

        drRow = dtTable.NewRow
        drRow(0) = "Has Database Access"
        drRow(1) = objUser.HasDBAccess
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "System Object"
        drRow(1) = objUser.SystemObject
        dtTable.Rows.Add(drRow)

        objSQLServer.Close()
        objSQLServer = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DetailsTableObjects(ByVal strKey As String, ByVal strTableName As String, ByVal enmTBObjects As enmTableObjects) As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)
        Dim objTable As Table2
        objTable = objSQLDatabase.Tables.Item(strTableName)
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        ' Dim nCount As Integer
        Select Case enmTBObjects
            Case enmTableObjects.Checks
                Dim objCheck As Check
                objCheck = objTable.Checks.Item(strKey)

                drRow = dtTable.NewRow
                drRow(0) = "Checked"
                drRow(1) = objCheck.Checked
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Exclude Replication"
                drRow(1) = objCheck.ExcludeReplication
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Text"
                drRow(1) = objCheck.Text
                dtTable.Rows.Add(drRow)

                objCheck = Nothing
            Case enmTableObjects.ClusteredIndex
                drRow = dtTable.NewRow
                drRow(0) = "Index Name"
                drRow(1) = strKey
                dtTable.Rows.Add(drRow)
            Case enmTableObjects.Columns
                Dim objCoumn As Column
                objCoumn = objTable.Columns.Item(strKey)

                drRow = dtTable.NewRow
                drRow(0) = "Data Type"
                drRow(1) = objCoumn.Datatype
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Length"
                drRow(1) = objCoumn.Length
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Numeric Precision"
                drRow(1) = objCoumn.NumericPrecision
                dtTable.Rows.Add(drRow)


                drRow = dtTable.NewRow
                drRow(0) = "Numeric Scale"
                drRow(1) = objCoumn.NumericScale
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Allow Null"
                drRow(1) = objCoumn.AllowNulls
                dtTable.Rows.Add(drRow)


                drRow = dtTable.NewRow
                drRow(0) = "Default Value"
                drRow(1) = objCoumn.Default
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "In Primary Key"
                drRow(1) = objCoumn.InPrimaryKey
                dtTable.Rows.Add(drRow)
                objCoumn = Nothing
            Case enmTableObjects.Indexes
                Dim objIndex As Index
                objIndex = objTable.Indexes.Item(strKey)

                drRow = dtTable.NewRow
                drRow(0) = "Fill Factor"
                drRow(1) = objIndex.FillFactor
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Space Used"
                drRow(1) = objIndex.SpaceUsed
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Index Type"
                drRow(1) = objIndex.Type
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "No of Indexed Columns"
                drRow(1) = objIndex.ListIndexedColumns.Count
                dtTable.Rows.Add(drRow)

            Case enmTableObjects.Keys
                Dim objKey As Key
                objKey = objTable.Keys.Item(strKey)

                drRow = dtTable.NewRow
                drRow(0) = "Clustered"
                drRow(1) = objKey.Clustered
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "No Of Referenced Columns"
                drRow(1) = objKey.ReferencedColumns.Count
                dtTable.Rows.Add(drRow)


                drRow = dtTable.NewRow
                drRow(0) = "Reference key"
                drRow(1) = objKey.ReferencedKey
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Reference Table"
                drRow(1) = objKey.ReferencedTable
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Checked"
                drRow(1) = objKey.Checked
                dtTable.Rows.Add(drRow)


                drRow = dtTable.NewRow
                drRow(0) = "Exclude Replication"
                drRow(1) = objKey.ExcludeReplication
                dtTable.Rows.Add(drRow)

                objKey = Nothing
            Case enmTableObjects.PrimaryKey
                drRow = dtTable.NewRow
                drRow(0) = "Primary Key"
                drRow(1) = strKey
                dtTable.Rows.Add(drRow)
            Case enmTableObjects.Triggers
                Dim objTrigger As Trigger
                objTrigger = objTable.Triggers.Item(strKey)

                drRow = dtTable.NewRow
                drRow(0) = "Create Date"
                drRow(1) = objTrigger.CreateDate
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Enabled"
                drRow(1) = objTrigger.Enabled
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Owner"
                drRow(1) = objTrigger.Owner
                dtTable.Rows.Add(drRow)

                drRow = dtTable.NewRow
                drRow(0) = "Is System Object"
                drRow(1) = objTrigger.SystemObject
                dtTable.Rows.Add(drRow)

                objTrigger = Nothing
        End Select
        objSQLServer.Close()
        Return dsData.Tables(0)
    End Function
    Public Function Fill_DatabaseDetails() As DataTable
        Dim objSQLServer As New SQLServer2

        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        Dim objSQLDatabase As Database2
        objSQLDatabase = objSQLServer.Databases.Item(_sSQLDatabaseName)

        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow

        drRow = dtTable.NewRow
        drRow(0) = "Create Date"
        drRow(1) = objSQLDatabase.CreateDate
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Create For Attach"
        drRow(1) = objSQLDatabase.CreateForAttach
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Current Compatibility"
        drRow(1) = objSQLDatabase.CurrentCompatibility
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Data Space Usage"
        drRow(1) = objSQLDatabase.DataSpaceUsage
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Index Space Usage"
        drRow(1) = objSQLDatabase.IndexSpaceUsage
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Owner"
        drRow(1) = objSQLDatabase.Owner
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Primary File Path"
        drRow(1) = objSQLDatabase.PrimaryFilePath
        dtTable.Rows.Add(drRow)


        drRow = dtTable.NewRow
        drRow(0) = "Database Size"
        drRow(1) = objSQLDatabase.SizeInKB & " KB"
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Space Available"
        drRow(1) = objSQLDatabase.SpaceAvailableInMB & " MB"
        dtTable.Rows.Add(drRow)

        drRow = dtTable.NewRow
        drRow(0) = "Version"
        drRow(1) = objSQLDatabase.Version
        dtTable.Rows.Add(drRow)

        Return dsData.Tables(0)
    End Function
    Public Function Fill_SQLServerDetails() As DataTable
        Dim dsData As New DataSet
        Dim dtTable As New DataTable
        Dim clmnPropertyName As New DataColumn("PropertyName")
        Dim clmnPropertyValue As New DataColumn("PropertyValue")
        dtTable.Columns.Add(clmnPropertyName)
        dtTable.Columns.Add(clmnPropertyValue)
        dsData.Tables.Add(dtTable)
        Dim drRow As DataRow
        drRow = dtTable.NewRow
        drRow(0) = "SQL Server Name"
        drRow(1) = _sSQLServerName
        dtTable.Rows.Add(drRow)
        Return dsData.Tables(0)
    End Function
    Public Function DetachDB() As Boolean
        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(_sSQLServerName)

        objSQLServer.DetachDB(_sSQLDatabaseName, True)
        objSQLServer.Close()
        Return True
    End Function
#End Region
End Class
