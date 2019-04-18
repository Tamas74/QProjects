Imports System.Data.SqlClient
Public Class clsSupport
    Public Enum gloMailFormat
        HTML
        TEXT
    End Enum
#Region "   Private Variables"
    Dim m_ClinicName As String = ""
    Dim m_ClinicEmailAddress As String = ""
    Dim m_LoginUserName As String = ""
    Dim m_SenderName As String = ""
    Dim m_SenderEmailAddress As String = ""
#End Region
#Region "   Public Properties"
    Public ReadOnly Property ClinicName() As String
        Get
            Return m_ClinicName
        End Get
    End Property
    Public ReadOnly Property ClinicEmailAddress() As String
        Get
            Return m_ClinicEmailAddress
        End Get
    End Property
    Public Property LoginUserName() As String
        Get
            Return m_LoginUserName
        End Get
        Set(ByVal Value As String)
            m_LoginUserName = Value
        End Set
    End Property
    Public ReadOnly Property SenderName() As String
        Get
            Return m_SenderName
        End Get
    End Property
    Public ReadOnly Property SenderEmailAddress() As String
        Get
            Return m_SenderEmailAddress
        End Get
    End Property
#End Region
#Region "   Public Functions"
    Public Sub Retrieve_SenderDetails()
        Retrieve_SenderDetails(m_LoginUserName)
    End Sub
    Public Sub Retrieve_SenderDetails(ByVal strLoginUserName As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_Fill_Support_SenderDetails"
        Dim objParaLoginUserName As New SqlParameter
        With objParaLoginUserName
            .ParameterName = "@LoginUserName"
            .Value = strLoginUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLoginUserName)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows Then
            objSQLDataReader.Read()
            If IsDBNull(objSQLDataReader.GetString(0)) = False Then
                m_ClinicName = objSQLDataReader.GetString(0)
            Else
                m_ClinicName = ""
            End If
            If IsDBNull(objSQLDataReader.GetString(1)) = False Then
                m_ClinicEmailAddress = objSQLDataReader.GetString(1)
            Else
                m_ClinicEmailAddress = ""
            End If
        End If
        objSQLDataReader.NextResult()
        If objSQLDataReader.HasRows Then
            objSQLDataReader.Read()
            If IsDBNull(objSQLDataReader.GetString(0)) = False Then
                m_SenderName = objSQLDataReader.GetString(0)
                If Trim(m_SenderName) = "" Then
                    m_SenderName = strLoginUserName
                End If
            Else
                m_SenderName = strLoginUserName
            End If
            If IsDBNull(objSQLDataReader.GetString(1)) = False Then
                m_SenderEmailAddress = objSQLDataReader.GetString(1)
            Else
                m_SenderEmailAddress = ""
            End If
        End If
        objSQLDataReader.Close()
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaLoginUserName = Nothing
        objSQLDataReader = Nothing
    End Sub
#End Region
End Class
