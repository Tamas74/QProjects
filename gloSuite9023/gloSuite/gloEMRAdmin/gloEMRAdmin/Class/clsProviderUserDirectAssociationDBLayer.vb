Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class clsProviderUserDirectAssociationDBLayer
    Implements IDisposable

    Public Function GetProviderUserList() As DataSet

        Dim returnedDataset As New DataSet
        Try
            Using dataAdapter As New SqlDataAdapter("Direct_GetProviderUserList", gloEMRAdmin.mdlGeneral.GetConnectionString)
                dataAdapter.Fill(returnedDataset)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        
        Return returnedDataset
    End Function

    Public Function ModifyProviderUserAssociation(ByVal DataTable_ToAdd As DataTable, ByVal DataTable_ToDelete As DataTable) As Int32

        Dim sqlCommand As SqlCommand = Nothing
        Dim sqlConnection As SqlConnection = Nothing
        Dim sqlParameter_Add As SqlParameter = Nothing
        Dim sqlParameter_Delete As SqlParameter = Nothing
        Dim nRowsInserted As Int32 = 0
        Try

            sqlConnection = New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
            sqlCommand = New SqlCommand("Direct_ModifyProviderUserAssociation", sqlConnection)

            sqlParameter_Add = New SqlParameter()
            With sqlParameter_Add
                .ParameterName = "TVP_Associations"
                .Value = DataTable_ToAdd
                .SqlDbType = SqlDbType.Structured
                .Direction = ParameterDirection.Input
            End With

            sqlParameter_Delete = New SqlParameter()
            With sqlParameter_Delete
                .ParameterName = "TVP_ToDelete"
                .Value = DataTable_ToDelete
                .SqlDbType = SqlDbType.Structured
                .Direction = ParameterDirection.Input
            End With

            With sqlCommand
                .Parameters.Add(sqlParameter_Add)
                .Parameters.Add(sqlParameter_Delete)
                .CommandType = CommandType.StoredProcedure
                .Connection.Open()
            End With

            nRowsInserted = sqlCommand.ExecuteNonQuery()
            sqlCommand.Connection.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If sqlCommand IsNot Nothing Then
                With sqlCommand
                    .Parameters.Clear()
                    .Dispose()
                End With
                sqlCommand = Nothing
            End If

            If sqlConnection IsNot Nothing Then
                sqlConnection.Dispose()
                sqlConnection = Nothing
            End If

            If sqlParameter_Add IsNot Nothing Then
                sqlParameter_Add = Nothing
            End If

            If sqlParameter_Delete IsNot Nothing Then
                sqlParameter_Delete = Nothing
            End If
        End Try

        Return nRowsInserted
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Class ProviderDirectAssociation    
    Implements IDisposable

#Region "Attributes"
    Private nProviderID As Int64
    Private nUserID As Long

    'Private sDirectAddress As String
    Private sFirstName As String
    Private sMiddleName As String
    Private sLastName As String

    Private bIsDefault As Boolean
    Private bIsBlocked As Boolean
    Private bIsNew As Boolean = False
    Private bIsLoadedFromDB As Boolean = False
    Private bIsDeleted As Boolean = False
#End Region
    
#Region "Properties"

    Public Property IsDefault() As Boolean
        Get
            Return bIsDefault
        End Get
        Set(ByVal value As Boolean)
            bIsDefault = value
        End Set
    End Property

    Public Property UserID() As Long
        Get
            Return nUserID
        End Get
        Set(ByVal value As Long)
            nUserID = value
        End Set
    End Property

    'Public Property HasDirectAddress() As Boolean
    '    Get
    '        Return Me.bHasDirectAddress
    '    End Get
    '    Private Set(value As Boolean)
    '        Me.bHasDirectAddress = value
    '    End Set
    'End Property

    Public Property ProviderID() As Int64
        Get
            Return nProviderID
        End Get
        Set(ByVal value As Int64)
            nProviderID = value
        End Set
    End Property

    Public Property IsBlocked() As Boolean
        Get
            Return bIsBlocked
        End Get
        Set(ByVal value As Boolean)
            bIsBlocked = value
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



    Public Property IsDeleted() As Boolean
        Get
            Return bIsDeleted
        End Get
        Set(ByVal value As Boolean)
            bIsDeleted = value
        End Set
    End Property

    Public Property IsNew() As Boolean
        Get
            Return bIsNew
        End Get
        Set(ByVal value As Boolean)
            bIsNew = value
        End Set
    End Property

    Public Property IsLoadedFromDB() As Boolean
        Get
            Return bIsLoadedFromDB
        End Get
        Set(ByVal value As Boolean)
            bIsLoadedFromDB = value
        End Set
    End Property

    'Public Property DirectAddress() As String
    '    Get
    '        Return sDirectAddress
    '    End Get
    '    Set(ByVal value As String)
    '        If Not String.IsNullOrEmpty(value) And Not String.IsNullOrWhiteSpace(value) Then
    '            sDirectAddress = value
    '            Me.HasDirectAddress = True
    '        End If
    '    End Set
    'End Property

#End Region

#Region "Functions"

    Public Shadows Function GetHashCode() As Long
        Return Me.nProviderID
    End Function

    Public Shared Function Clone(ByRef ProviderDirectAssociation As ProviderDirectAssociation) As ProviderDirectAssociation
        Return New ProviderDirectAssociation(ProviderDirectAssociation)
    End Function

#End Region
    
#Region "Constructor"

    ' Uncomment below code for adding a new Constructor
    ' based on each Property
    'Sub New(nProviderID As Int64, ByVal UserID As Long, sFirstName As String, sMiddleName As String, sLastName As String, sDirectAddress As String, ByVal IsBlocked As Boolean)
    '    MyBase.New()
    '    Me.ProviderID = nProviderID
    '    Me.UserID = UserID

    '    Me.FirstName = sFirstName
    '    Me.MiddleName = sMiddleName
    '    Me.LastName = sLastName
    '    Me.DirectAddress = sDirectAddress
    '    Me.IsBlocked = IsBlocked
    'End Sub

    Sub New(ByVal DataRow As DataRow)
        MyBase.New()

        nProviderID = DataRow("nProviderID")
        UserID = DataRow("nUserID")

        sFirstName = DataRow("sFirstName")
        sMiddleName = DataRow("sMiddleName")
        sLastName = DataRow("sLastName")

        'sDirectAddress = DataRow("sDirectAddress")
        IsBlocked = DataRow("bIsBlocked")
    End Sub

    Sub New(ByVal ProviderToAdd As ProviderDirectAssociation)
        MyBase.New()
        Me.ProviderID = ProviderToAdd.ProviderID
        Me.UserID = ProviderToAdd.UserID

        Me.FirstName = ProviderToAdd.FirstName
        Me.MiddleName = ProviderToAdd.MiddleName
        Me.LastName = ProviderToAdd.LastName
        'Me.DirectAddress = ProviderToAdd.DirectAddress
        'Me.IsBlocked = ProviderToAdd.IsBlocked
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                Me.ProviderID = Nothing
                Me.UserID = Nothing
                'Me.DirectAddress = Nothing
                Me.FirstName = Nothing
                Me.MiddleName = Nothing
                Me.LastName = Nothing
                Me.IsBlocked = Nothing
                'Me.HasDirectAddress = Nothing

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public Class UserDirectAssociation
    Inherits Collections.Generic.Dictionary(Of Int64, ProviderDirectAssociation)
    Implements IDisposable


#Region "Class Attributes"
    Private nUserID As Int64 = 0
    Private sLoginName As String = String.Empty
#End Region

#Region "Constructors and Destructors"

    Sub New(ByVal DataRow As DataRow)
        Me.UserID = DataRow("nUserID")
        Me.LoginName = DataRow("sLoginName")        
    End Sub

    Sub New(ByVal User As UserDirectAssociation)
        Me.UserID = User.UserID
        Me.LoginName = User.LoginName        
    End Sub

    'Uncomment below code for adding Constructors
    'based on Properties
    'Sub New(ByVal UserID As Long, ByVal LoginName As String)
    '    Me.New(UserID, LoginName, False)
    'End Sub

    'Sub New(ByVal UserID As Long, ByVal LoginName As String, ByVal IsLoadedFromDB As Boolean)
    '    Me.UserID = UserID
    '    Me.LoginName = LoginName
    '    Me.IsLoadedFromDB = IsLoadedFromDB
    'End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.UserID = Nothing
                'Me.IsNew = Nothing
                'Me.IsDeleted = Nothing
                'Me.IsLoadedFromDB = Nothing
                Me.LoginName = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

#Region "Public Properties"

    Public Property UserID() As Int64
        Get
            Return nUserID
        End Get
        Set(ByVal value As Int64)
            nUserID = value
        End Set
    End Property

    Public Property LoginName() As String
        Get
            Return sLoginName
        End Get
        Set(ByVal value As String)
            sLoginName = value
        End Set
    End Property

#End Region

#Region "Public Functions"

    Public Overrides Function GetHashCode() As Integer
        Return Me.UserID
    End Function

    Public Shared Function Clone(ByRef UserDirectAssociation As UserDirectAssociation) As UserDirectAssociation
        Return New UserDirectAssociation(UserDirectAssociation)
    End Function

#End Region

End Class

Public Class DirectAssociation

#Region "Class Attributes"

    Private nAssociationId As Int64 = 0
    Private nProviderID As Int64 = 0
    Private nUserId As Int64 = 0

#End Region

#Region "Constructor"

    Sub New(ByVal DataRow As DataRow)
        Me.AssociationID = DataRow("nAssociationID")
        Me.ProviderID = DataRow("nProviderID")
        Me.UserID = DataRow("nUserID")
    End Sub

#End Region

#Region "Public Properties"

    Public Property UserID() As Int64
        Get
            Return nUserId
        End Get
        Set(ByVal value As Int64)
            nUserId = value
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

    Public Property AssociationID() As Int64
        Get
            Return nAssociationId
        End Get
        Set(ByVal value As Int64)
            nAssociationId = value
        End Set
    End Property

#End Region
    

End Class

'Public Class DirectUserProviderNodeSorter
'    Implements IComparer

'    Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare

'        Dim PrimaryNode As TreeNode = TryCast(x, TreeNode)
'        Dim SecondaryNode As TreeNode = TryCast(y, TreeNode)

'        If PrimaryNode.Parent IsNot Nothing Then
'            If SecondaryNode.Parent IsNot Nothing Then
'                Return New CaseInsensitiveComparer().Compare(PrimaryNode.Text.ToString, SecondaryNode.Text.ToString)
'            End If
'        End If


'    End Function

'End Class