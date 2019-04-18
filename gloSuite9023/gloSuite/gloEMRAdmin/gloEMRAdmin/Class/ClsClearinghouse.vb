Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

    Public Enum TypeOfData
        None = 0
        TestData = 1
        ProductionData = 2
        Blank = 3
    End Enum

    Public Enum ClearinghouseFolderTypes
        None = 1
        Inbox = 2
        Outbox = 3
        General = 4
    End Enum

    Public Class ClearingHouse

#Region " Private Variables "
    Private _messageBoxCaption As String = gstrMessageBoxCaption
        Private _nClearingHouseID As Int64 = 0
        Private _sClearingHouseName As String = ""
        Private _sRecieverName As String = ""
        Private _sRecieverID As String = ""
        Private _sSubmitterID As String = ""
        Private _bIs1JQualifier As Boolean = False
        Private _s1JQualifier As String = ""
        Private _bIsSenderCode As Boolean = False
        Private _sSenderCode As String = ""
        Private _bIsVenderID As Boolean = False
        Private _sVenderID As String = ""
        Private _bIsLoop100B As Boolean = False
        Private _sLoop100B As String = ""
        Private _TypeOfData As TypeOfData = TypeOfData.None
        Private _bIsISA As Boolean = False
        Private _ClinicID As Int64 = 0

        'Fields to be added to detail table 
#Region "Variables for detail table fields "
        Private _sClearingHouseCode As String = ""
        Private _sURL As String = ""
        Private _sUserName As String = ""
        Private _sPassword As String = ""
        Private _sIn_271_ElgibilityResponse As String = ""
        Private _sIn_277_ClaimStatus As String = ""
        Private _sIn_835_Remitance As String = ""
        Private _sIn_997_Acknowledge As String = ""
        Private _sOut_276_ElgibilityEnquiry As String = ""
        Private _sOut_837P_ClaimSubmition As String = ""
        Private _sOut_997_Acknowledge As String = ""
        Private _sGen_CSRReports As String = ""
        Private _sGen_Letters As String = ""
        Private _sGen_Reports As String = ""
        Private _sGen_Statements As String = ""
        Private _sGen_WorkedTrans As String = ""
        Private _nFolderCategory As Int64 = 0
#End Region

        Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings

#End Region

#Region "Properties"

        Public Property ClearingHouseID() As Int64
            Get
                Return _nClearingHouseID
            End Get
            Set(ByVal value As Int64)
                _nClearingHouseID = value
            End Set
        End Property

        Public Property ClearingHouseName() As String
            Get
                Return _sClearingHouseName
            End Get
            Set(ByVal value As String)
                _sClearingHouseName = value
            End Set
        End Property

        Public Property RecieverName() As String
            Get
                Return _sRecieverName
            End Get
            Set(ByVal value As String)
                _sRecieverName = value
            End Set
        End Property

        Public Property RecieverID() As String
            Get
                Return _sRecieverID
            End Get
            Set(ByVal value As String)
                _sRecieverID = value
            End Set
        End Property

        Public Property SubmitterID() As String
            Get
                Return _sSubmitterID
            End Get
            Set(ByVal value As String)
                _sSubmitterID = value
            End Set
        End Property

        Public Property IsOneJQualifier() As Boolean
            Get
                Return _bIs1JQualifier
            End Get
            Set(ByVal value As Boolean)
                _bIs1JQualifier = value
            End Set
        End Property

        Public Property OneJQualifier() As String
            Get
                Return _s1JQualifier
            End Get
            Set(ByVal value As String)
                _s1JQualifier = value
            End Set
        End Property

        Public Property IsSenderCode() As Boolean
            Get
                Return _bIsSenderCode
            End Get
            Set(ByVal value As Boolean)
                _bIsSenderCode = value
            End Set
        End Property

        Public Property SenderCode() As String
            Get
                Return _sSenderCode
            End Get
            Set(ByVal value As String)
                _sSenderCode = value
            End Set
        End Property

        Public Property IsVenderID() As Boolean
            Get
                Return _bIsVenderID
            End Get
            Set(ByVal value As Boolean)
                _bIsVenderID = value
            End Set
        End Property

        Public Property VenderID() As String
            Get
                Return _sVenderID
            End Get
            Set(ByVal value As String)
                _sVenderID = value
            End Set
        End Property

        Public Property IsLoop1000B() As Boolean
            Get
                Return _bIsLoop100B
            End Get
            Set(ByVal value As Boolean)
                _bIsLoop100B = value
            End Set
        End Property

        Public Property Loop1000B() As String
            Get
                Return _sLoop100B
            End Get
            Set(ByVal value As String)
                _sLoop100B = value
            End Set
        End Property

        Public Property TypeOfData() As TypeOfData
            Get
                Return _TypeOfData
            End Get
            Set(ByVal value As TypeOfData)
                _TypeOfData = value
            End Set
        End Property

        Public Property IsISA() As Boolean
            Get
                Return _bIsISA
            End Get
            Set(ByVal value As Boolean)
                _bIsISA = value
            End Set
        End Property

        Public Property ClinicID() As Int64
            Get
                Return _ClinicID
            End Get
            Set(ByVal value As Int64)
                _ClinicID = value
            End Set
        End Property

#End Region

#Region "Properties for deteil table fields"
        Public Property ClearingHouseCode() As String
            Get
                Return _sClearingHouseCode
            End Get
            Set(ByVal value As String)
                _sClearingHouseCode = value
            End Set
        End Property
        Public Property URL() As String

            Get
                Return _sURL
            End Get
            Set(ByVal value As String)
                _sURL = value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _sUserName
            End Get
            Set(ByVal value As String)
                _sUserName = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _sPassword
            End Get
            Set(ByVal value As String)
                _sPassword = value
            End Set
        End Property
        Public Property In_271_ElgibilityResponse() As String
            Get
                Return _sIn_271_ElgibilityResponse
            End Get
            Set(ByVal value As String)
                _sIn_271_ElgibilityResponse = value
            End Set
        End Property
        Public Property In_277_ClaimStatus() As String
            Get
                Return _sIn_277_ClaimStatus
            End Get
            Set(ByVal value As String)
                _sIn_277_ClaimStatus = value
            End Set
        End Property
        Public Property In_835_Remitance() As String
            Get
                Return _sIn_835_Remitance
            End Get
            Set(ByVal value As String)
                _sIn_835_Remitance = value
            End Set
        End Property
        Public Property In_997_Acknowledge() As String
            Get
                Return _sIn_997_Acknowledge
            End Get
            Set(ByVal value As String)
                _sIn_997_Acknowledge = value
            End Set
        End Property
        Public Property Out_276_ElgibilityEnquiry() As String
            Get
                Return _sOut_276_ElgibilityEnquiry
            End Get
            Set(ByVal value As String)
                _sOut_276_ElgibilityEnquiry = value
            End Set
        End Property
        Public Property Out_837P_ClaimSubmition() As String
            Get
                Return _sOut_837P_ClaimSubmition
            End Get
            Set(ByVal value As String)
                _sOut_837P_ClaimSubmition = value
            End Set
        End Property
        Public Property Out_997_Acknowledge() As String
            Get
                Return _sOut_997_Acknowledge
            End Get
            Set(ByVal value As String)
                _sOut_997_Acknowledge = value
            End Set
        End Property
        Public Property Gen_CSRReports() As String
            Get
                Return _sGen_CSRReports
            End Get
            Set(ByVal value As String)
                _sGen_CSRReports = value
            End Set
        End Property
        Public Property Gen_Letters() As String
            Get
                Return _sGen_Letters
            End Get
            Set(ByVal value As String)
                _sGen_Letters = value
            End Set
        End Property
        Public Property Gen_Reports() As String
            Get
                Return _sGen_Reports
            End Get
            Set(ByVal value As String)
                _sGen_Reports = value
            End Set
        End Property
        Public Property Gen_Statements() As String
            Get
                Return _sGen_Statements
            End Get
            Set(ByVal value As String)
                _sGen_Statements = value
            End Set
        End Property
        Public Property Gen_WorkedTrans() As String
            Get
                Return _sGen_WorkedTrans
            End Get
            Set(ByVal value As String)
                _sGen_WorkedTrans = value
            End Set
        End Property
        Public Property FolderCategory() As Int64
            Get
                Return _nFolderCategory
            End Get
            Set(ByVal value As Int64)
                _nFolderCategory = value
            End Set
        End Property

#End Region

#Region "Constructor & Destructor"

        Private _databaseconnectionstring As String = ""

        Public Sub New(ByVal DatabaseConnectionString As String)
            _databaseconnectionstring = DatabaseConnectionString

            If appSettings("ClinicID") IsNot Nothing Then
                If appSettings("ClinicID") <> "" Then
                    _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                Else
                    _ClinicID = 0
                End If
            Else
                _ClinicID = 0

            End If
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

        Public Function Add(ByVal oClearingHouse As ClearingHouse) As Long
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Dim oDBParameters As New gloDatabaseLayer.DBParameters()
            Dim Value As New Object()
            Dim _ReturnID As Int64 = 0
            Try

                oDB.Connect(False)

                oDBParameters.Add("@nClearingHouseID", oClearingHouse.ClearingHouseID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt)
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sReceiverID", oClearingHouse.RecieverID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sReceiverName", oClearingHouse.RecieverName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sSubmitterID", oClearingHouse.SubmitterID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@bIsOneJQulifier", oClearingHouse.IsOneJQualifier, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                oDBParameters.Add("@sOneJQulifier", oClearingHouse.OneJQualifier, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@bIsSenderCode", oClearingHouse.IsSenderCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                oDBParameters.Add("@sSenderCode", oClearingHouse.SenderCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@bIsVenderIDCode", oClearingHouse.IsVenderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                oDBParameters.Add("@sVenderIDCode", oClearingHouse.VenderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@bIsLoop1000BNM109", oClearingHouse.IsLoop1000B, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                oDBParameters.Add("@sLoop1000BNM109", oClearingHouse.Loop1000B, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@nTypeOfData", oClearingHouse.TypeOfData.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int)
                oDBParameters.Add("@bIsISA", oClearingHouse.IsISA, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit)
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)

                oDB.Execute("BL_INUP_ClearingHouseMST", oDBParameters, Value)
                If Value IsNot Nothing AndAlso Convert.ToString(Value) <> "" Then
                    _ReturnID = Convert.ToInt64(Value)
                End If

                '#Region "Adding To Detail Table" 
                'Delete entries from detail table 
                oDBParameters.Clear()
                Dim _sqlQuery As String = ""
            _sqlQuery = "DELETE FROM BL_ClearingHouse_DTL  WHERE nClearingHouseID = " & _ReturnID & " AND nClinicID = " & ClinicID & " "
                oDB.Execute_Query(_sqlQuery)

                'Insert entries into detail table 

                oDBParameters.Add("@nClearingHouseID", _ReturnID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt)
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sURL", oClearingHouse.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sUserName", oClearingHouse.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sPassword", oClearingHouse.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_271_ElgibilityResponse", oClearingHouse.In_271_ElgibilityResponse, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_277_ClaimStatus", oClearingHouse.In_277_ClaimStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_835_Remitance", oClearingHouse.In_835_Remitance, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_997_Acknowledge", oClearingHouse.In_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_276_ElgibilityEnquiry", oClearingHouse.Out_276_ElgibilityEnquiry, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_837P_ClaimSubmition", oClearingHouse.Out_837P_ClaimSubmition, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_997_Acknowledge", oClearingHouse.Out_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_CSRReports", oClearingHouse.Gen_CSRReports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Letters", oClearingHouse.Gen_Letters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Reports", oClearingHouse.Gen_Reports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Statements", oClearingHouse.Gen_Statements, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_WorkedTrans", oClearingHouse.Gen_WorkedTrans, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@nFolderCategory", ClearinghouseFolderTypes.None.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)

                oDB.Execute("BL_INSERT_ClearingHouseDTL", oDBParameters, Value)
                If Value IsNot Nothing AndAlso Convert.ToString(Value) <> "" Then
                    _ReturnID = Convert.ToInt64(Value)
                    '#End Region 

                End If
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDBParameters.Dispose()
                oDB.Dispose()
            End Try
            Return _ReturnID
        End Function

        Public Function Add_Detail(ByVal oClearingHouse As ClearingHouse) As Long
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Dim oDBParameters As New gloDatabaseLayer.DBParameters()
            Dim Value As New Object()
            Dim _ReturnID As Int64 = 0
            Try

                oDB.Connect(False)
                '@nClearingHouseID, @sClearingHouseCode, @sURL, @sUserName, @sPassword, @sIn_271_ElgibilityResponse, 
                ' @sIn_277_ClaimStatus, @sIn_835_Remitance, @sIn_997_Acknowledge, @sOut_276_ElgibilityEnquiry, 
                ' @sOut_837P_ClaimSubmition, @sOut_997_Acknowledge, @sGen_CSRReports, @sGen_Letters, @sGen_Reports, 
                '@sGen_Statements, @sGen_WorkedTrans, @nFolderCategory, @nClinicID 

                oDBParameters.Add("@nClearingHouseID", oClearingHouse.ClearingHouseID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt)
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sURL", oClearingHouse.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sUserName", oClearingHouse.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sPassword", oClearingHouse.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_271_ElgibilityResponse", oClearingHouse.In_271_ElgibilityResponse, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_277_ClaimStatus", oClearingHouse.In_277_ClaimStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_835_Remitance", oClearingHouse.In_835_Remitance, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sIn_997_Acknowledge", oClearingHouse.In_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_276_ElgibilityEnquiry", oClearingHouse.Out_276_ElgibilityEnquiry, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_837P_ClaimSubmition", oClearingHouse.Out_837P_ClaimSubmition, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sOut_997_Acknowledge", oClearingHouse.Out_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_CSRReports", oClearingHouse.Gen_CSRReports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Letters", oClearingHouse.Gen_Letters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Reports", oClearingHouse.Gen_Reports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_Statements", oClearingHouse.Gen_Statements, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@sGen_WorkedTrans", oClearingHouse.Gen_WorkedTrans, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
                oDBParameters.Add("@nFolderCategory", oClearingHouse.FolderCategory, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)

                oDB.Execute("BL_INUP_ClearingHouseDTL", oDBParameters, Value)
                If Value IsNot Nothing AndAlso Convert.ToString(Value) <> "" Then
                    _ReturnID = Convert.ToInt64(Value)
                End If
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDBParameters.Dispose()
                oDB.Dispose()
            End Try
            Return _ReturnID
        End Function

        Public Function GetClearingHouse(ByVal ClearingHouseID As Int64) As ClearingHouse
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Dim oClearingHouse As ClearingHouse = Nothing
            Try
                Dim dtClearing As New DataTable()
                Dim _sqlQuery As String = ""

                oDB.Connect(False)

            _sqlQuery = "SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS sClearingHouseName,ISNULL(sReceiverID,'') AS sReceiverID,ISNULL(sReceiverName,'') AS sReceiverName,ISNULL(sSubmitterID,'') AS sSubmitterID, " _
                        & "ISNULL(bIsOneJQulifier,'false') AS bIsOneJQulifier,ISNULL(sOneJQulifier,'') AS sOneJQulifier,ISNULL(bIsSenderCode,'false') AS bIsSenderCode,ISNULL(sSenderCode,'') AS sSenderCode,ISNULL(bIsVenderIDCode,'false') AS bIsVenderIDCode,ISNULL(sVenderIDCode,'') AS sVenderIDCode, " _
                        & " ISNULL(bIsLoop1000BNM109,'false') AS bIsLoop1000BNM109,ISNULL(sLoop1000BNM109,'') AS sLoop1000BNM109,ISNULL(nTypeOfData,0) AS nTypeOfData, " _
                        & " ISNULL(bIsISA,'') AS bIsISA  FROM BL_ClearingHouse_MST WHERE nClearingHouseID = " & ClearingHouseID & " " 'AND nClinicID = 1"

                oDB.Retrive_Query(_sqlQuery, dtClearing)

                If dtClearing IsNot Nothing AndAlso dtClearing.Rows.Count > 0 Then
                    oClearingHouse = New ClearingHouse(_databaseconnectionstring)

                    oClearingHouse.ClearingHouseID = ClearingHouseID
                    oClearingHouse.ClearingHouseName = Convert.ToString(dtClearing.Rows(0)("sClearingHouseName"))
                    oClearingHouse.RecieverName = Convert.ToString(dtClearing.Rows(0)("sReceiverName"))
                    oClearingHouse.RecieverID = Convert.ToString(dtClearing.Rows(0)("sReceiverID"))
                    oClearingHouse.SubmitterID = Convert.ToString(dtClearing.Rows(0)("sSubmitterID"))
                    oClearingHouse.IsOneJQualifier = Convert.ToBoolean(dtClearing.Rows(0)("bIsOneJQulifier"))
                    oClearingHouse.OneJQualifier = Convert.ToString(dtClearing.Rows(0)("sOneJQulifier"))
                    oClearingHouse.IsSenderCode = Convert.ToBoolean(dtClearing.Rows(0)("bIsSenderCode"))
                    oClearingHouse.SenderCode = Convert.ToString(dtClearing.Rows(0)("sSenderCode"))
                    oClearingHouse.IsVenderID = Convert.ToBoolean(dtClearing.Rows(0)("bIsVenderIDCode"))
                    oClearingHouse.VenderID = Convert.ToString(dtClearing.Rows(0)("sVenderIDCode"))
                    oClearingHouse.IsLoop1000B = Convert.ToBoolean(dtClearing.Rows(0)("bIsLoop1000BNM109"))
                    oClearingHouse.Loop1000B = Convert.ToString(dtClearing.Rows(0)("sLoop1000BNM109"))
                    oClearingHouse.TypeOfData = DirectCast(Convert.ToInt32(dtClearing.Rows(0)("nTypeOfData")), TypeOfData)
                    oClearingHouse.IsISA = Convert.ToBoolean(dtClearing.Rows(0)("bIsISA"))
                    oClearingHouse.ClinicID = ClinicID
                End If

                '#Region "Get Clearinghouse details" 

                Dim dtClearingDetail As New DataTable()

                oDB.Connect(False)

            _sqlQuery = "SELECT ISNULL(sClearingHouseCode,'') AS sClearingHouseCode,ISNULL(sURL,'') AS sURL,ISNULL(sUserName,'') AS sUserName, " _
            & " ISNULL(sPassword,'') AS sPassword,ISNULL(sIn_271_ElgibilityResponse,'') AS sIn_271_ElgibilityResponse, " _
            & " ISNULL(sIn_277_ClaimStatus,'') AS sIn_277_ClaimStatus,ISNULL(sIn_835_Remitance,'') AS sIn_835_Remitance, " _
            & " ISNULL(sIn_997_Acknowledge,'') AS sIn_997_Acknowledge,ISNULL(sOut_276_ElgibilityEnquiry,'') AS sOut_276_ElgibilityEnquiry, " _
            & " ISNULL(sOut_837P_ClaimSubmition,'') AS sOut_837P_ClaimSubmition,ISNULL(sOut_997_Acknowledge,'') AS sOut_997_Acknowledge, " _
            & " ISNULL(sGen_CSRReports,'') AS sGen_CSRReports,ISNULL(sGen_Letters,'') AS sGen_Letters,ISNULL(sGen_Reports,'') AS sGen_Reports," _
            & " ISNULL(sGen_Statements,'') AS sGen_Statements,ISNULL(sGen_WorkedTrans,'') AS sGen_WorkedTrans,ISNULL(nFolderCategory,0) AS nFolderCategory " & " FROM BL_ClearingHouse_DTL WHERE nClearingHouseID = " & ClearingHouseID & " " 'AND nClinicID = 1 "

                oDB.Retrive_Query(_sqlQuery, dtClearingDetail)

                If dtClearingDetail IsNot Nothing AndAlso dtClearingDetail.Rows.Count > 0 Then

                    oClearingHouse.ClearingHouseID = ClearingHouseID
                    oClearingHouse.ClearingHouseCode = Convert.ToString(dtClearingDetail.Rows(0)("sClearingHouseCode"))
                    oClearingHouse.URL = Convert.ToString(dtClearingDetail.Rows(0)("sURL"))
                    oClearingHouse.UserName = Convert.ToString(dtClearingDetail.Rows(0)("sUserName"))
                    oClearingHouse.Password = Convert.ToString(dtClearingDetail.Rows(0)("sPassword"))
                    oClearingHouse.In_271_ElgibilityResponse = Convert.ToString(dtClearingDetail.Rows(0)("sIn_271_ElgibilityResponse"))
                    oClearingHouse.In_277_ClaimStatus = Convert.ToString(dtClearingDetail.Rows(0)("sIn_277_ClaimStatus"))
                    oClearingHouse.In_835_Remitance = Convert.ToString(dtClearingDetail.Rows(0)("sIn_835_Remitance"))
                    oClearingHouse.In_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows(0)("sIn_997_Acknowledge"))
                    oClearingHouse.Out_276_ElgibilityEnquiry = Convert.ToString(dtClearingDetail.Rows(0)("sOut_276_ElgibilityEnquiry"))
                    oClearingHouse.Out_837P_ClaimSubmition = Convert.ToString(dtClearingDetail.Rows(0)("sOut_837P_ClaimSubmition"))
                    oClearingHouse.Out_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows(0)("sOut_997_Acknowledge"))
                    oClearingHouse.In_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows(0)("sIn_997_Acknowledge"))
                    oClearingHouse.Gen_CSRReports = Convert.ToString(dtClearingDetail.Rows(0)("sGen_CSRReports"))
                    oClearingHouse.Gen_Letters = Convert.ToString(dtClearingDetail.Rows(0)("sGen_Letters"))
                    oClearingHouse.Gen_Reports = Convert.ToString(dtClearingDetail.Rows(0)("sGen_Reports"))
                    oClearingHouse.Gen_Statements = Convert.ToString(dtClearingDetail.Rows(0)("sGen_Statements"))
                    oClearingHouse.Gen_WorkedTrans = Convert.ToString(dtClearingDetail.Rows(0)("sGen_WorkedTrans"))
                    oClearingHouse.FolderCategory = Convert.ToInt64(dtClearingDetail.Rows(0)("nFolderCategory"))

                    oClearingHouse.ClinicID = ClinicID

                    '#End Region 

                End If
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDB.Dispose()
            End Try
            Return oClearingHouse
        End Function

        Public Function GetClearingHouse() As DataTable
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Dim dtClearing As New DataTable()
            Try

                Dim _sqlQuery As String = ""

                oDB.Connect(False)

                _sqlQuery = ("SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS sClearingHouseName," & " ISNULL(sReceiverID,'') AS sReceiverID,ISNULL(sReceiverName,'') AS sReceiverName,ISNULL(sSubmitterID,'') AS sSubmitterID," & " ISNULL(bIsOneJQulifier,'false') AS bIsOneJQulifier,ISNULL(sOneJQulifier,'') AS sOneJQulifier,ISNULL(bIsSenderCode,'false') AS bIsSenderCode," & " ISNULL(sSenderCode,'') AS sSenderCode,ISNULL(bIsVenderIDCode,'false') AS bIsVenderIDCode,ISNULL(sVenderIDCode,'') AS sVenderIDCode," & " ISNULL(bIsLoop1000BNM109,'false') AS bIsLoop1000BNM109,ISNULL(sLoop1000BNM109,'') AS sLoop1000BNM109,ISNULL(nTypeOfData,0) AS nTypeOfData," & " CASE ISNULL(bIsISA,'FALSE') WHEN 'TRUE' THEN 'YES' WHEN 'FALSE' THEN 'NO' END AS bIsISA " & " FROM BL_ClearingHouse_MST" & " WHERE nClinicID = ") + ClinicID & " "


                oDB.Retrive_Query(_sqlQuery, dtClearing)
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDB.Dispose()
            End Try
            Return dtClearing
        End Function

        Public Sub Delete(ByVal ClearingHouseID As Long)

            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            Try

                Dim _sqlQuery As String = ""

                oDB.Connect(False)

            _sqlQuery = "DELETE FROM BL_ClearingHouse_MST  WHERE nClearingHouseID = " & ClearingHouseID & " " 'AND nClinicID = ") + ClinicID & " "

                oDB.Execute_Query(_sqlQuery)

            _sqlQuery = "DELETE FROM BL_ClearingHouse_DTL WHERE nClearingHouseID = " & ClearingHouseID & "" ' AND nClinicID = ") + ClinicID & " "



                oDB.Execute_Query(_sqlQuery)
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDB.Dispose()
            End Try
        End Sub

        Friend Function CheckDuplicate(ByVal ClearingHouseID As Int64, ByVal _ClearingHousecode As String) As Boolean
            Dim oDB As New gloDatabaseLayer.DBLayer(_databaseconnectionstring)
            'gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters(); 
            Dim strQuery As String = ""
            Dim _result As Boolean = False
            Try

                oDB.Connect(False)
                If ClearingHouseID = 0 Then
                    strQuery = (" select count(nClearingHouseID) FROM BL_ClearingHouse_MST where sClearingHouseCode = '" & _ClearingHousecode.Replace("'", "''") & "' AND nClinicID = ") + Me.ClinicID & " "
                Else
                    strQuery = ((" select count(nClearingHouseID) FROM BL_ClearingHouse_MST where sClearingHouseCode = '" & _ClearingHousecode.Replace("'", "''") & "' AND nClearingHouseID <> ") + ClearingHouseID & " AND nClinicID = ") + Me.ClinicID & " "
                End If

                Dim _intResult As Object = Nothing

                _intResult = oDB.ExecuteScalar_Query(strQuery)

                If _intResult IsNot Nothing Then
                    If _intResult.ToString().Trim() <> "" Then
                        If Convert.ToInt64(_intResult) > 0 Then
                            _result = True
                        End If
                    End If

                End If
            Catch ex As gloDatabaseLayer.DBException
                ex.ERROR_Log(ex.ToString())
            Catch ex As Exception
            Finally
                oDB.Disconnect()
                oDB.Dispose()
            End Try
            Return _result
        End Function


    End Class


