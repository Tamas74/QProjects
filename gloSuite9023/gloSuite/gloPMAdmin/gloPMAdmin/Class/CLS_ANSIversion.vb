
Imports System.Data.SqlClient

''Sagar Ghodke: 11/21/2013 Making changes in class, this class will be used both for ANSI version and Paper Claim version settings
Public Class CLS_ANSIversion

#Region "Variable Declaration"
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private _DataBaseConnectionString As String

    Private m_nID As Int64
    Private m_nContactID As Int64
    Private m_nClinicID As Int64
    Private m_nClaimVersion As Int64
    Private m_nEligVersion As Int64
    Private m_dtCreatedDate As DateTime
    Private m_dtModifiedDate As DateTime
    Private m_sClaimVersionName As String
    Private m_sEligVersionName As String
    Private m_nPaperClaimVersion As Int64
    Private m_sPaperClaimVersionName As String

#End Region

#Region "Constructor & Destructor"

    Public Sub New()
        _DataBaseConnectionString = mdlGeneral.GetConnectionString
    End Sub

    Private disposed As Boolean = False

    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

#End Region

#Region "Public Property"

    Public Property ID() As Int64
        Get
            Return m_nID
        End Get
        Set(ByVal Value As Int64)
            m_nID = Value
        End Set
    End Property

    Public Property ContactID() As Int64
        Get
            Return m_nContactID
        End Get
        Set(ByVal Value As Int64)
            m_nContactID = Value
        End Set
    End Property


    Public Property ClinicID() As Int64
        Get
            Return m_nClinicID
        End Get
        Set(ByVal Value As Int64)
            m_nClinicID = Value
        End Set
    End Property

    Public Property ClaimVersion() As Int64
        Get
            Return m_nClaimVersion
        End Get
        Set(ByVal Value As Int64)
            m_nClaimVersion = Value
        End Set
    End Property

    Public Property EligVersion() As Int64
        Get
            Return m_nEligVersion
        End Get
        Set(ByVal Value As Int64)
            m_nEligVersion = Value
        End Set
    End Property

    Public Property CreatedDate() As DateTime
        Get
            Return m_dtCreatedDate
        End Get
        Set(ByVal Value As DateTime)
            m_dtCreatedDate = Value
        End Set
    End Property

    Public Property ModifiedDate() As DateTime
        Get
            Return m_dtModifiedDate
        End Get
        Set(ByVal Value As DateTime)
            m_dtModifiedDate = Value
        End Set
    End Property

    Public Property ClaimVersionName() As String
        Get
            Return m_sClaimVersionName
        End Get
        Set(ByVal Value As String)
            m_sClaimVersionName = Value
        End Set
    End Property

    Public Property EligVersionName() As String
        Get
            Return m_sEligVersionName
        End Get
        Set(ByVal Value As String)
            m_sEligVersionName = Value
        End Set
    End Property

    Public Property PaperClaimVersion() As Int64
        Get
            Return m_nPaperClaimVersion
        End Get
        Set(ByVal Value As Int64)
            m_nPaperClaimVersion = Value
        End Set
    End Property

    Public Property PaperClaimVersionName() As String
        Get
            Return m_sPaperClaimVersionName
        End Get
        Set(ByVal Value As String)
            m_sPaperClaimVersionName = Value
        End Set
    End Property

#End Region

#Region "Public and Private Methods"

    Public Function SaveRecords() As Int64

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim nReturn As Int64

        Try
            oDB.Connect(False)

            oDBParameters.Add("@nID", ID, ParameterDirection.Output, SqlDbType.BigInt)
            oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClaimVersion", ClaimVersion, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nEligVersion", EligVersion, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sClaimVersion", ClaimVersionName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sEligVersion", EligVersionName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@dtCreatedDate", CreatedDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@dtModifiedDate", ModifiedDate, ParameterDirection.Input, SqlDbType.DateTime)


            Dim htOutput As Hashtable = oDB.Execute("BL_INUP_ANSIVersion", oDBParameters, True)

            If htOutput.Count > 0 Then
                nReturn = htOutput("@ID")
            End If

        Catch ex As Exception
            Return 0
        Finally
            oDB.Disconnect()
        End Try

        Return nReturn

    End Function

    Public Function DeleteRecords(ByVal nID As Int64) As Boolean

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim bReturn As Boolean
        Dim sSQL As String

        Try
            oDB.Connect(False)
            sSQL = "DELETE BL_ANSIVersion "
            sSQL += "WHERE "
            sSQL += "nID = " + nID
            Dim oOutPut As Object = oDB.ExecuteScalar_Query(sSQL)

            If oOutPut <> Nothing Then
                bReturn = True
            Else
                bReturn = False
            End If


        Catch ex As Exception
            Return False
        Finally
            oDB.Disconnect()
        End Try

        Return bReturn

    End Function

    Public Function FetchPlanLevelRecords() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim sSQL As String
        Dim dt As New DataTable
        Try
            oDB.Connect(False)
            sSQL = "SELECT "
            sSQL += " CM.nContactID"
            sSQL += ",CM.sName"
            'sSQL += ",ANSI.nClaimVersion"
            'sSQL += ",ANSI.nEligVersion"
            sSQL += ",ANSI.sClaimVersion"
            sSQL += ",ANSI.sEligVersion"
            sSQL += " FROM "
            sSQL += " Contacts_MST CM WITH(NOLOCK) LEFT OUTER JOIN BL_ANSIVersion ANSI WITH(NOLOCK) "
            sSQL += " ON CM.nContactID = ANSI.nContactID "
            sSQL += " WHERE CM.sContactType = 'Insurance'"
            sSQL += " ORDER BY CM.sName"

            oDB.Retrive_Query(sSQL, dt)

        Catch ex As Exception

        Finally
            oDB.Disconnect()
        End Try

        Return dt

    End Function

    Public Function FetchClinicLevelRecords() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim sSQL As String
        Dim dt As New DataTable
        Try
            oDB.Connect(False)
            sSQL = "SELECT "
            sSQL += " ANSI.nClaimVersion"
            sSQL += ",ANSI.nEligVersion"
            sSQL += ",ANSI.sClaimVersion"
            sSQL += ",ANSI.sEligVersion"
            sSQL += " FROM "
            sSQL += " BL_ANSIVersion ANSI WITH(NOLOCK) "
            sSQL += "WHERE ANSI.nContactID = 0"

            oDB.Retrive_Query(sSQL, dt)

        Catch ex As Exception

        Finally
            oDB.Disconnect()
        End Try

        Return dt

    End Function

    'Paper Methods

    Public Function SavePaperClaimSettings() As Int64

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim nReturn As Int64

        Try
            oDB.Connect(False)

            oDBParameters.Add("@nID", ID, ParameterDirection.Output, SqlDbType.BigInt)
            oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClaimVersion", PaperClaimVersion, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sClaimVersion", PaperClaimVersionName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@dtCreatedDate", CreatedDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@dtModifiedDate", ModifiedDate, ParameterDirection.Input, SqlDbType.DateTime)


            Dim htOutput As Hashtable = oDB.Execute("BL_INUP_PaperFormVersion", oDBParameters, True)

            If htOutput.Count > 0 Then
                nReturn = htOutput("@ID")
            End If

        Catch ex As Exception
            Return 0
        Finally
            oDB.Disconnect()
        End Try

        Return nReturn

    End Function

    Public Function FetchPaperPlanLevelRecords() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim sSQL As String
        Dim dt As New DataTable
        Try
            oDB.Connect(False)
            sSQL = "SELECT "
            sSQL += " CM.nContactID"
            sSQL += ",CM.sName"
            sSQL += ",ANSI.sClaimVersion"
            sSQL += " FROM "
            sSQL += " Contacts_MST CM WITH(NOLOCK) LEFT OUTER JOIN BL_PaperFormVersion ANSI WITH(NOLOCK) "
            sSQL += " ON CM.nContactID = ANSI.nContactID "
            sSQL += " WHERE CM.sContactType = 'Insurance'"
            sSQL += " ORDER BY CM.sName"

            oDB.Retrive_Query(sSQL, dt)

        Catch ex As Exception

        Finally
            oDB.Disconnect()
        End Try

        Return dt


    End Function

    Public Function FetchPaperClinicLevelRecords() As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim sSQL As String
        Dim dt As New DataTable
        Try
            oDB.Connect(False)
            sSQL = "SELECT "
            sSQL += " ANSI.nClaimVersion"
            sSQL += ",ANSI.sClaimVersion"
            sSQL += " FROM "
            sSQL += " BL_PaperFormVersion ANSI WITH(NOLOCK) "
            sSQL += "WHERE ANSI.nContactID = 0"

            oDB.Retrive_Query(sSQL, dt)

        Catch ex As Exception

        Finally
            oDB.Disconnect()
        End Try

        Return dt

    End Function

#End Region

End Class
