Imports System.Data
Imports System.Data.SqlClient

Public Class DirectUserProviderAssociation

#Region "Class Attributes"

    Private nAssociationId As Int64 = 0
    Private nProviderID As Int64 = 0
    Private nUserId As Int64 = 0
    Private sFirstName As String = String.Empty
    Private sMiddleName As String = String.Empty
    Private sLastName As String = String.Empty
    Private _SSPID As String = String.Empty
#End Region

#Region "Constructor"

    Sub New(ByVal DataRow As DataRow)
        Me.AssociationID = DataRow("nAssociationID")
        Me.ProviderID = DataRow("nProviderID")
        Me.UserID = DataRow("nUserID")
        Me.FirstName = DataRow("sFirstName")
        Me.MiddleName = DataRow("sMiddleName")
        Me.sLastName = DataRow("sLastName")
        Me.SSPID = DataRow("sSPIID")
    End Sub

#End Region

#Region "Public Properties"


    Public Property SSPID() As String
        Get
            Return _SSPID
        End Get
        Set(ByVal value As String)
            _SSPID = value
        End Set
    End Property


    Public Property AssociationID() As Int64
        Get
            Return nAssociationId
        End Get
        Set(ByVal value As Int64)
            nAssociationId = value
        End Set
    End Property

    Public Property ProviderID() As Int64
        Get
            Return nProviderID
        End Get
        Set(ByVal value As Int64)
            nProviderID = value
        End Set
    End Property

    Public Property UserID() As Int64
        Get
            Return nUserId
        End Get
        Set(ByVal value As Int64)
            nUserId = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return sFirstName
        End Get
        Set(ByVal value As String)
            sFirstName = value
        End Set
    End Property

    Public Property MiddleName() As String
        Get
            Return sMiddleName
        End Get
        Set(ByVal value As String)
            sMiddleName = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return sLastName
        End Get
        Set(ByVal value As String)
            sLastName = value
        End Set
    End Property


#End Region

End Class

Public Class DirectUserProvider_DBLayer

    Public Function GetProvidersByUserID(ByVal UserID As Int64) As DataTable
        Dim dtAssociations As New DataTable
        Using mySqlConnection As New SqlConnection(GetConnectionString)

            Using SqlCommand As New SqlCommand("Direct_GetUserProvider", mySqlConnection)

                Dim SqlParameter As New SqlParameter("UserID", UserID)
                With SqlParameter
                    .DbType = DbType.Int64
                    .Direction = ParameterDirection.Input
                    .Value = UserID
                End With

                With SqlCommand
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.Add(SqlParameter)
                End With

                Using DataAdapter As New SqlDataAdapter(SqlCommand)
                    DataAdapter.Fill(dtAssociations)
                End Using
                SqlParameter = Nothing
            End Using
        End Using

        Return dtAssociations
    End Function

End Class