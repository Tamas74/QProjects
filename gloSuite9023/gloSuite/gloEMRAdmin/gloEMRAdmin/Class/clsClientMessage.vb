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
Public Class clsClientMessage

    Public blnEndDate As Boolean

#Region " Private Variables"
    Dim _nMessageID As Integer
    Dim _dtFrom As DateTime
    Dim _dtTo As DateTime
    Dim _sCategory As String
    Dim _sMessage As String
#End Region

#Region " Public Properties"
    Public Property MessageID() As Integer
        Get
            Return _nMessageID
        End Get
        Set(ByVal Value As Integer)
            _nMessageID = Value
        End Set
    End Property
    Public Property Message() As String
        Get
            Return _sMessage
        End Get
        Set(ByVal Value As String)
            _sMessage = Value
        End Set
    End Property
    Public Property FromDate() As DateTime
        Get
            Return _dtFrom
        End Get
        Set(ByVal Value As DateTime)
            _dtFrom = Value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            Return _dtTo
        End Get
        Set(ByVal Value As DateTime)
            _dtTo = Value
        End Set
    End Property
    Public Property Category() As String
        Get
            Return _sCategory
        End Get
        Set(ByVal Value As String)
            _sCategory = Value
        End Set
    End Property
#End Region

#Region " Public functions"
    Public Function Fill_Category() As Collection
        Dim clClientMessage As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillMessageCategory"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clClientMessage.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clClientMessage
    End Function
    Public Function ViewMessages(ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal strCategory As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientMessages"
        objCmd.Parameters.Clear()

        Dim objParaCategory As New SqlParameter
        With objParaCategory
            .ParameterName = "@Category"
            .Value = strCategory
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaCategory)


        Dim objParaFromDate As New SqlParameter
        With objParaFromDate
            .ParameterName = "@FromDate"
            .Value = dtFrom
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaFromDate)

        Dim objParaToDate As New SqlParameter
        With objParaToDate
            .ParameterName = "@ToDate"
            .Value = dtTo
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaToDate)


        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)

    End Function
    Public Function RetrieveMessage() As String
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '   Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanClientMessage"
        Dim drMessage As SqlDataReader
        objCon.Open()
        objCmd.Connection = objCon
        drMessage = objCmd.ExecuteReader()
        If drMessage.HasRows = True Then
            drMessage.Read()
            _sMessage = drMessage.Item(0)
        Else
            _sMessage = ""
        End If

        objCon.Close()
        drMessage = Nothing
        objCmd = Nothing
        objCon = Nothing
        Return _sMessage
    End Function
    Public Sub ScanClientMessage(ByVal nMessageID As Integer)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanClientMessage"
        objCmd.Parameters.Clear()
        Dim objParaMessageID As New SqlParameter
        With objParaMessageID
            .ParameterName = "@MessageID"
            .Value = nMessageID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaMessageID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            If IsDBNull(objSQLDataReader.Item("FromDate")) = False Then
                _dtFrom = objSQLDataReader.Item("FromDate")
            End If
            If IsDBNull(objSQLDataReader.Item("ToDate")) = False Then
                blnEndDate = True
                _dtTo = objSQLDataReader.Item("ToDate")
            Else
                blnEndDate = False
            End If
            If IsDBNull(objSQLDataReader.Item("Category")) = False Then
                _sCategory = objSQLDataReader.Item("Category")
            End If
            If IsDBNull(objSQLDataReader.Item("Message")) = False Then
                _sMessage = objSQLDataReader.Item("Message")
            End If
        End If
        objSQLDataReader.Close()
        objCon.Close()
        objCon = Nothing
        objSQLDataReader = Nothing
        objCmd = Nothing
    End Sub
    Public Function UpdateMessage(ByVal nMessageID As Integer) As Boolean
        Return UpdateMessage(nMessageID, _dtFrom, _dtTo, _sCategory, _sMessage)
    End Function
    Public Function UpdateMessage(ByVal nMessageID As Integer, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal strCategory As String, ByVal strMessage As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '     Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InUpClientMessage"

        Dim objSQLParaMessageID As New SqlParameter
        With objSQLParaMessageID
            .ParameterName = "@MessageID"
            .Value = nMessageID
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objSQLParaMessageID)

        Dim objSQLParaFromDate As New SqlParameter
        With objSQLParaFromDate
            .ParameterName = "@FromDate"
            .Value = _dtFrom
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objSQLParaFromDate)

        If blnEndDate = True Then
            If IsNothing(_dtTo) = False Then
                If IsDBNull(_dtTo) = False Then
                    If IsDate(_dtTo) = True Then
                        Dim objSQLParaEndDate As New SqlParameter
                        With objSQLParaEndDate
                            .ParameterName = "@EndDate"
                            .Value = _dtTo
                            .SqlDbType = SqlDbType.DateTime
                        End With
                        objCmd.Parameters.Add(objSQLParaEndDate)
                    End If
                End If
            End If
        End If

        Dim objSQLParaCategory As New SqlParameter
        With objSQLParaCategory
            .ParameterName = "@Category"
            .Value = strCategory
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objSQLParaCategory)


        Dim objSQLParaMessage As New SqlParameter
        With objSQLParaMessage
            .ParameterName = "@Message"
            .Value = strMessage
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objSQLParaMessage)

        objCon.Open()
        objCmd.Connection = objCon
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function DeleteMessage(ByVal nMessageID As Integer) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        '  Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteClientMessage"

        Dim objSQLParaMessageID As New SqlParameter
        With objSQLParaMessageID
            .ParameterName = "@MessageID"
            .Value = nMessageID
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objSQLParaMessageID)
        objCon.Open()
        objCmd.Connection = objCon
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
#End Region

End Class
