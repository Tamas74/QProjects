'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.Data.SqlClient
Public Class clsSelfNotes

#Region " Private Variables"
    Dim _nSelfNotesID As Integer
    Dim _dtSelfNotesDate As DateTime
    Dim _sSelfNotesCategory As String
    Dim _sSelfNotesHead As String
    Dim _sComments As String
    Dim _sStatus As String
    Dim strQuery As String
#End Region

    Public Enum enmSearchOn
        SelfNotesCategory
        SelfNotesStatus
    End Enum

#Region " Public Properties"
    Public Property SelfNotesID() As Integer
        Get
            Return _nSelfNotesID
        End Get
        Set(ByVal Value As Integer)
            _nSelfNotesID = Value
        End Set
    End Property
    Public Property SelfNotesDate() As DateTime
        Get
            Return _dtSelfNotesDate
        End Get
        Set(ByVal Value As DateTime)
            _dtSelfNotesDate = Value
        End Set
    End Property
    Public Property SelfNotesCategory() As String
        Get
            Return _sSelfNotesCategory
        End Get
        Set(ByVal Value As String)
            _sSelfNotesCategory = Value
        End Set
    End Property
    Public Property NotesHead() As String
        Get
            Return _sSelfNotesHead
        End Get
        Set(ByVal Value As String)
            _sSelfNotesHead = Value
        End Set
    End Property
    Public Property Comments() As String
        Get
            Return _sComments
        End Get
        Set(ByVal Value As String)
            _sComments = Value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return _sStatus
        End Get
        Set(ByVal Value As String)
            _sStatus = Value
        End Set
    End Property
#End Region

#Region " Public Functions"
    Public Function AddSelfNotes() As Boolean
        Return AddSelfNotes(_dtSelfNotesDate, _sSelfNotesHead, _sSelfNotesCategory, _sComments, _sStatus)
    End Function
    Public Function AddSelfNotes(ByVal dtSelfNotesDate As DateTime, ByVal sNotesHead As String, ByVal sNotesCategory As String, ByVal sComments As String, ByVal sStatus As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika commented on 25th june 07
        '        Dim objSQLDataReader As SqlDataReader

        '-------------------
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InsertSelfNotes"

        Dim objParaNotesDate As New SqlParameter
        With objParaNotesDate
            .ParameterName = "@NotesDate"
            .Value = dtSelfNotesDate
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaNotesDate)


        Dim objParaNotesCategory As New SqlParameter
        With objParaNotesCategory
            .ParameterName = "@NotesCategory"
            .Value = sNotesCategory
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesCategory)

        Dim objParaNotesHead As New SqlParameter
        With objParaNotesHead
            .ParameterName = "@NotesHead"
            .Value = sNotesHead
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesHead)

        Dim objParaNotesComments As New SqlParameter
        With objParaNotesComments
            .ParameterName = "@Comments"
            .Value = sComments
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesComments)


        Dim objParaNotesStatus As New SqlParameter
        With objParaNotesStatus
            .ParameterName = "@NotesStatus"
            .Value = sStatus
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesStatus)


        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function DeleteSelfNotes(ByVal nSelfNotesID As Integer) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '--
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteSelfNotes"
        Dim objParaFromDate As New SqlParameter
        With objParaFromDate
            .ParameterName = "@NotesID"
            .Value = nSelfNotesID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFromDate)
        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function EditSelfNotes(ByVal nSelfNotesID As Integer) As Boolean
        Return EditSelfNotes(nSelfNotesID, _dtSelfNotesDate, _sSelfNotesHead, _sSelfNotesCategory, _sComments, _sStatus)
    End Function
    Public Function EditSelfNotes(ByVal nSelfNotesID As Integer, ByVal dtSelfNotesDate As DateTime, ByVal sSelfNotesHead As String, ByVal sSelfNotesCategory As String, ByVal sComments As String, ByVal sStatus As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '---------
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_UpdateSelfNotes"

        Dim objParaNotesID As New SqlParameter
        With objParaNotesID
            .ParameterName = "@NotesID"
            .Value = nSelfNotesID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaNotesID)


        Dim objParaNotesDate As New SqlParameter
        With objParaNotesDate
            .ParameterName = "@NotesDate"
            .Value = dtSelfNotesDate
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaNotesDate)


        Dim objParaNotesCategory As New SqlParameter
        With objParaNotesCategory
            .ParameterName = "@NotesCategory"
            .Value = sSelfNotesCategory
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesCategory)

        Dim objParaNotesHead As New SqlParameter
        With objParaNotesHead
            .ParameterName = "@NotesHead"
            .Value = sSelfNotesHead
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesHead)

        Dim objParaNotesComments As New SqlParameter
        With objParaNotesComments
            .ParameterName = "@Comments"
            .Value = sComments
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesComments)


        Dim objParaNotesStatus As New SqlParameter
        With objParaNotesStatus
            .ParameterName = "@NotesStatus"
            .Value = sStatus
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNotesStatus)


        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True

    End Function
    Public Sub SearchSelfNotes(ByVal nSelfNotesID As Integer)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveSelfNotes"

        Dim objParaNotesID As New SqlParameter
        With objParaNotesID
            .ParameterName = "@NotesID"
            .Value = nSelfNotesID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaNotesID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        objSQLDataReader.Read()
        _dtSelfNotesDate = objSQLDataReader.Item("NotesDate")
        _sSelfNotesCategory = objSQLDataReader.Item("NotesCategory")
        _sSelfNotesHead = objSQLDataReader.Item("NotesHead")
        _sComments = objSQLDataReader.Item("Comments")
        _sStatus = objSQLDataReader.Item("SelfNotesStatus")
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
    End Sub
    Public Function ScanSelfNotes(ByVal sValue As String, ByVal sSearchOn As enmSearchOn, ByVal dtFrom As Date, ByVal dtTo As Date) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '-----
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanSelfNotes"
        objCmd.Parameters.Clear()
        Dim objParaFromDate As New SqlParameter
        With objParaFromDate
            .ParameterName = "@FromDate"
            .Value = dtFrom.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaFromDate)

        Dim objParaToDate As New SqlParameter
        With objParaToDate
            .ParameterName = "@ToDate"
            .Value = dtTo.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaToDate)

        Dim objParaSearch As New SqlParameter
        With objParaSearch
            .Value = sValue
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        If sSearchOn = enmSearchOn.SelfNotesCategory Then
            objParaSearch.ParameterName = "@NotesCategory"
        ElseIf sSearchOn = enmSearchOn.SelfNotesStatus Then
            objParaSearch.ParameterName = "@NotesStatus"
        End If
        objCmd.Parameters.Add(objParaSearch)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function PopulateCategory() As Collection
        Dim clSelfNotesCategory As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillCategory"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clSelfNotesCategory.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clSelfNotesCategory
    End Function
    Public Function PopulateStatus() As Collection
        Dim clSelfNotesStatus As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillStatus"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clSelfNotesStatus.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clSelfNotesStatus
    End Function
#End Region
End Class
