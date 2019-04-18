Imports System.Data.SqlClient
Public Class ClsReportExplorer
    Private Shared m_SelectionCriteria As String
    Private Shared m_CriteriaField As String
    Private Shared m_CriteriaValue As String
    Private Shared m_Condition As String

    Public Function GetFieldValue(ByVal m_field As String) As System.Data.DataTable
        Dim objconn As SqlConnection
        Dim strconn As String
        strconn = GetConnectionString()
        objconn = New SqlConnection(strconn)
        Dim objadapter As SqlDataAdapter
        Dim strstring As String
        strstring = "select " & m_field & " from " & Splittext(m_field)
        objadapter = New SqlDataAdapter(strstring, objconn)
        Dim objdatatable As New DataTable
        objadapter.Fill(objdatatable)
        Return objdatatable
    End Function
    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, ".")
            If arrstring.Length > 0 Then
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Else
            Return ""
        End If
    End Function
    Public Shared Property SelectionCriteria() As String
        Get
            Return m_SelectionCriteria
        End Get
        Set(ByVal Value As String)
            m_SelectionCriteria = Value
        End Set
    End Property

    Public Shared Property CriteriaField() As String
        Get
            Return m_CriteriaField
        End Get
        Set(ByVal Value As String)
            m_CriteriaField = Value
        End Set
    End Property
    Public Shared Property CriteriaValue() As String
        Get
            Return m_CriteriaValue
        End Get
        Set(ByVal Value As String)
            m_CriteriaValue = Value
        End Set
    End Property
    Public Shared Property Condition() As String
        Get
            Return m_Condition
        End Get
        Set(ByVal Value As String)
            m_Condition = Value
        End Set
    End Property
End Class
