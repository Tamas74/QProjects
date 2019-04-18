Public Class ClsParentICD9CPT
    Private m_Description As String
    Private m_Code As String
    Private m_Unit As Integer

    Public Sub New(ByVal strcode As String, ByVal strdescription As String, Optional ByVal intUnit As Integer = 0)
        MyBase.new()
        m_Code = strcode
        m_Description = strdescription
        m_Unit = intUnit
    End Sub

    Public Property Code()
        Get
            Code = m_Code
        End Get
        Set(ByVal Value)
            m_Code = Value
        End Set
    End Property

    Public Property Description()
        Get
            Description = m_Description
        End Get
        Set(ByVal Value)
            m_Description = Value
        End Set
    End Property

    Public Property Unit()
        Get
            Unit = m_Unit
        End Get
        Set(ByVal Value)
            m_Unit = Value
        End Set
    End Property

End Class

Public Class ClsChildICD9CPT

    Inherits ClsParentICD9CPT
    Public ColICD9CPT As Collection

    Public Sub New(ByVal strcode As String, ByVal strdescription As String, Optional ByVal intUnit As Integer = 0)
        MyBase.new(strcode, strdescription, intUnit)

    End Sub

End Class
