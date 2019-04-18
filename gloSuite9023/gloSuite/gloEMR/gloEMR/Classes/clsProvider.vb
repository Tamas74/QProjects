Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.IO

Public Class clsProvider
    Implements IDisposable
#Region "   Private Variables"
    Dim _nProviderID As Int64
    Dim _sFirstName As String = Nothing
    Dim _sMiddleName As String = Nothing
    Dim _sLastName As String = Nothing
    Dim _sGender As String = Nothing
    Dim _sDEA As String = Nothing
    Dim _sAddress As String = Nothing
    Dim _sStreet As String = Nothing
    Dim _sCity As String = Nothing
    Dim _sState As String = Nothing
    Dim _sZIP As String = Nothing
    Dim _sPhone As String = Nothing
    Dim _sFAX As String = Nothing
    Dim _sMobile As String = Nothing
    Dim _sPager As String = Nothing
    Dim _sEmail As String = Nothing
    Dim _sURL As String = Nothing
    Dim _imgSignature As Image = Nothing
    Dim bAssigned As Boolean = False

    Dim _blnRequireSupervisingProviderforeRx As Boolean = False
#End Region
#Region "   Public Properties"
    Public Property ProviderID() As Int64
        Get
            Return _nProviderID
        End Get
        Set(ByVal Value As Int64)
            _nProviderID = Value
        End Set
    End Property
    Public Property FirstName() As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal Value As String)
            _sFirstName = Value
        End Set
    End Property
    Public Property MiddleName() As String
        Get
            Return _sMiddleName
        End Get
        Set(ByVal Value As String)
            _sMiddleName = Value
        End Set
    End Property
    Public Property LastName() As String
        Get
            Return _sLastName
        End Get
        Set(ByVal Value As String)
            _sLastName = Value
        End Set
    End Property
    Public Property Gender() As String
        Get
            Return _sGender
        End Get
        Set(ByVal Value As String)
            _sGender = Value
        End Set
    End Property
    Public Property DEA() As String
        Get
            Return _sDEA
        End Get
        Set(ByVal Value As String)
            _sDEA = Value
        End Set
    End Property
    Public Property Address() As String
        Get
            Return _sAddress
        End Get
        Set(ByVal Value As String)
            _sAddress = Value
        End Set
    End Property
    Public Property Street() As String
        Get
            Return _sStreet
        End Get
        Set(ByVal Value As String)
            _sStreet = Value
        End Set
    End Property
    Public Property City() As String
        Get
            Return _sCity
        End Get
        Set(ByVal Value As String)
            _sCity = Value
        End Set
    End Property
    Public Property State() As String
        Get
            Return _sState
        End Get
        Set(ByVal Value As String)
            _sState = Value
        End Set
    End Property
    Public Property ZIP() As String
        Get
            Return _sZIP
        End Get
        Set(ByVal Value As String)
            _sZIP = Value
        End Set
    End Property
    Public Property Phone() As String
        Get
            Return _sPhone
        End Get
        Set(ByVal Value As String)
            _sPhone = Value
        End Set
    End Property
    Public Property FAX() As String
        Get
            Return _sFAX
        End Get
        Set(ByVal Value As String)
            _sFAX = Value
        End Set
    End Property
    Public Property Mobile() As String
        Get
            Return _sMobile
        End Get
        Set(ByVal Value As String)
            _sMobile = Value
        End Set
    End Property
    Public Property Pager() As String
        Get
            Return _sPager
        End Get
        Set(ByVal Value As String)
            _sPager = Value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _sEmail
        End Get
        Set(ByVal Value As String)
            _sEmail = Value
        End Set
    End Property
    Public Property URL() As String
        Get
            Return _sURL
        End Get
        Set(ByVal Value As String)
            _sURL = Value
        End Set
    End Property
    Public Property Signature() As Image
        Get
            Return _imgSignature
        End Get
        Set(ByVal Value As Image)
            If (bAssigned) Then
                If (IsNothing(_imgSignature) = False) Then
                    _imgSignature.Dispose()
                    _imgSignature = Nothing
                End If
                bAssigned = False
            End If
          
            _imgSignature = Value

        End Set
    End Property
    Public Property RequireSupervisingProviderforeRx() As Boolean
        Get
            Return _blnRequireSupervisingProviderforeRx
        End Get
        Set(ByVal value As Boolean)
            _blnRequireSupervisingProviderforeRx = value
        End Set
    End Property
#End Region
#Region "   Public Functions"


    Public Function GetProviderImageWidth(ByVal ProviderID As Int64) As Integer
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim adp As SqlDataAdapter = Nothing
        Dim dtProvider As DataTable = Nothing
        Dim intWidth As Integer = 0
        Dim query As String = ""
        Try
            con = New SqlConnection(GetConnectionString())
            query = "SELECT Isnull(imgSignature,'') as imgSignature,isnull(nImgWidth,0) as nImgWidth  from Provider_MST Where nProviderID=" & ProviderID
            cmd = New SqlCommand(query, con)
            adp = New SqlDataAdapter(cmd)
            dtProvider = New DataTable
            adp.Fill(dtProvider)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    If Convert.ToInt32(dtProvider.Rows(0)("nImgWidth")) = 0 Then
                        Dim arrImage As Byte() = Nothing
                        If Convert.ToString(dtProvider.Rows(0)("imgSignature")) = "" Then
                            arrImage = CType(dtProvider.Rows(0)("imgSignature"), Byte())
                        End If


                        If Not IsNothing(arrImage) Then
                            Dim ms As New MemoryStream(arrImage)

                            'If (bAssigned) Then
                            '    If (IsNothing(_imgSignature) = False) Then
                            '        _imgSignature.Dispose()
                            '        _imgSignature = Nothing
                            '    End If
                            '    bAssigned = False
                            'End If
                            '_imgSignature = CType(Image.FromStream(ms), Image)
                            '_imgSignature.Save(Application.StartupPath & "\Signature.bmp")
                            If (bAssigned) Then
                                If (IsNothing(_imgSignature) = False) Then
                                    _imgSignature.Dispose()
                                    _imgSignature = Nothing
                                End If
                            End If

                            _imgSignature = Image.FromStream(ms)
                            If (IsNothing(_imgSignature) = False) Then
                                intWidth = _imgSignature.Width
                            End If

                            bAssigned = True
                            'File.Delete(Application.StartupPath & "\Signature.bmp")
                            ms.Close()
                            ms.Dispose()
                            ms = Nothing
                            arrImage = Nothing
                            UpdateSignatureWidth(ProviderID, intWidth)
                        End If
                    Else
                        intWidth = Convert.ToInt32(dtProvider.Rows(0)("nImgWidth"))
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(dtProvider) Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
            If Not IsNothing(adp) Then
                adp.Dispose()
                adp = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
        Return intWidth
    End Function

    Public Function Fill_Providers() As Collection

        Dim clProviders As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanProviderName"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            While objSQLDataReader.Read
                clProviders.Add(objSQLDataReader.Item(0))
            End While
            objSQLDataReader.Close()
            objCon.Close()
            objSQLDataReader = Nothing

            Return clProviders
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try


    End Function

    Friend Function UpdateSignatureWidth(ByVal ProviderID As Int64, ByVal nWidth As Int32) As Boolean
        Dim ogloEMRDatabase As New DataBaseLayer
        Dim returnvalue As Boolean
        Try
            Dim _strSQl = "Update Provider_MST Set nImgWidth = " & nWidth & " Where nProviderID=" & ProviderID
            returnvalue = ogloEMRDatabase.Delete_Query(_strSQl)


        Catch ex As Exception
            returnvalue = False
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ogloEMRDatabase) Then
                ogloEMRDatabase.Dispose()
                ogloEMRDatabase = Nothing
            End If

        End Try
        Return returnvalue
    End Function


    Public Function Fill_ProvidersDS() As DataSet

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter = Nothing
        Dim dsData As New DataSet

        Try

            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanProviderNameWithALL"
            objCmd.Connection = objCon


            objCmd.Connection = objCon
            objCon.Open()
            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dsData)
            objCon.Close()

            dsData.Tables(0).TableName = "ProviderName"


            Return dsData
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally

            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function ScanProvider(ByVal strProviderName As String) As DataSet
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader 
        Dim objDA As SqlDataAdapter = Nothing
        Dim dsData As New DataSet
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanProvider"
            objCmd.Connection = objCon
            Dim objParaProviderName As New SqlParameter
            With objParaProviderName
                .ParameterName = "@ProviderName"
                .Value = strProviderName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderName)
            objCmd.Connection = objCon
            objCon.Open()
            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dsData)
            objCon.Close()
            objParaProviderName = Nothing
            Return dsData
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            'If Not IsNothing(dsData) Then
            '    dsData.Dispose()
            '    dsData = Nothing
            'End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    ''To check the provider Status for particualr visit
    Public Function CheckProviderStatus(ByVal nVisitId As Long, ByVal nPatientID As Long, ByVal strProviderName As String) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "Select nProviderId from Visits where nVisitID=" & nVisitId & " and nPatientID = " & nPatientID
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If
        Catch ex As Exception
            CheckProviderStatus = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function GetPatientProvider(ByVal nPatientID As Long) As Int64
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "Select nProviderId from Patient where nPatientID = " & nPatientID
            Dim myObject = oDB.GetRecord_Query(strSQL)
            If (IsNothing(myObject) = False) Then
                If Not IsDBNull(myObject) Then
                    Dim myString = CType(myObject, String)
                    If (myString <> "") Then
                        Dim nProviderId As Int64 = CType(myObject, Int64)
                        If nProviderId <> 0 Then
                            Return nProviderId
                        Else
                            Return 0
                        End If
                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            GetPatientProvider = 0
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function RetrieveProviderID(ByVal strProviderName As String) As Int64
        Dim nProviderID As Int64 = 0
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveProviderID"
            objCmd.Connection = objCon
            Dim objParaProviderName As New SqlParameter
            With objParaProviderName
                .ParameterName = "@ProviderName"
                .Value = strProviderName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderName)
            objCmd.Connection = objCon
            objCon.Open()
            nProviderID = objCmd.ExecuteScalar
            objCon.Close()
            If IsNothing(nProviderID) Then
                nProviderID = 0
            End If
            objParaProviderName = Nothing
        Catch ex As Exception
            RetrieveProviderID = Nothing
            Throw ex
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return nProviderID
    End Function

    Public Function ChangeProvider(ByVal nVisitId As Long, ByVal nPatientID As Long, ByVal strProviderName As String, Optional ByVal blnChangevisit As Boolean = True) As Boolean
        Dim blnChangePatient As Boolean = False
        Try
            Dim nProviderID As Long
            nProviderID = RetrieveProviderID(strProviderName)
            blnChangePatient = ChangePatientProvider(nPatientID, nProviderID)
        Catch ex As Exception
            Throw ex
        End Try
        Return blnChangePatient
    End Function

    Public Function ChangePatientProvider(ByVal nPatientID As Long, ByVal nProviderID As Long) As Boolean
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        'Dim objSQLDataReader As SqlDataReader
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdatePatientsProvider"
            objCmd.Connection = objCon

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatientID)

            Dim objParaProviderID As New SqlParameter
            With objParaProviderID
                .ParameterName = "@ProviderID"
                .Value = nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProviderID)


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            ' objCon = Nothing

            objParaProviderID = Nothing
            objParaPatientID = Nothing
            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

    Public Function GetProviders(ByVal activeProviders As Boolean) As DataTable
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim adp As SqlDataAdapter = Nothing
        Dim dtProvider As DataTable = Nothing
        Try
            con = New SqlConnection(GetConnectionString())
            Dim query As String = " SELECT nProviderID, ISNULL(sFirstName,'') + '  ' + ISNULL(sMiddleName,'') + '  ' + ISNULL(sLastName,'') AS ProviderName " _
                                & " FROM Provider_MST WHERE bIsblocked = '" & (Not activeProviders) & "' ORDER BY ProviderName"
            cmd = New SqlCommand(query, con)
            adp = New SqlDataAdapter(cmd)
            dtProvider = New DataTable
            adp.Fill(dtProvider)
            If IsNothing(dtProvider) = False Then
                If dtProvider.Rows.Count > 0 Then
                    Return dtProvider
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dtProvider) Then
            '    dtProvider.Dispose()
            '    dtProvider = Nothing
            'End If
            If Not IsNothing(adp) Then
                adp.Dispose()
                adp = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function

    '' SUDHIR 20090817 ''
    Public Function GetSeniorProviders(ByVal nProviderID As Int64) As DataTable

        Dim dtProvider As New DataTable
        Dim conn As New SqlConnection(GetConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim _sqlstr As String = ""

        'dtProvider = New DataTable
        Try
            'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
            _sqlstr = " SELECT  ISNULL(Provider_MST.sFirstName,'') + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When ISNULL(Provider_MST.sMiddleName,'') then  ISNULL(Provider_MST.sMiddleName,'')  END + ' ' + ISNULL(Provider_MST.sLastName,'') AS sProviderName, " _
                    & " Provider_MST.nProviderID, Provider_MST.sDEA " _
                    & " FROM Provider_MST INNER JOIN ProviderSettings ON Provider_MST.nProviderID = ProviderSettings.nOthersID  " _
                    & " WHERE (ProviderSettings.sSettingsType = 'ProviderSeniorAssignment')  AND ProviderSettings.nProviderID = " & nProviderID 'And Provider_MST.bIsblocked=0 


            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dtProvider)

            Return dtProvider
        Catch ex As Exception
            Return dtProvider
        Finally

            If IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            'If IsNothing(dtProvider) Then
            '    dtProvider.Dispose()
            '    dtProvider = Nothing
            'End If
        End Try
    End Function

    'Developer: Yatin N.Bhagat
    'Date:17/04/2012
    'Bug ID/PRD Name/Salesforce Case:Prescription Provider Association:  Senior Provider list includes inactive (or deactivated) providers. (FOA Item) 
    'Reason: New Function Added To check  Is Provider Blocked
    Public Function isBlockedProvider(ByVal SproviderId As Long, ByVal JproviderId As Long) As Boolean()
        Dim oDb As New DataBaseLayer
        Dim strSql As String
        Dim bProv(1) As Boolean

        Try
            strSql = "SELECT bIsblocked  FROM Provider_MST WHERE nProviderID=" + SproviderId.ToString()
            Dim strResult As String = oDb.GetRecord_Query(strSql)
            If Not IsDBNull(strResult) Then

                If strResult.ToString() = "True" Then
                    bProv(0) = True
                Else
                    bProv(0) = False
                End If
            Else
                bProv(0) = False
            End If

            strSql = "SELECT bIsblocked  FROM Provider_MST WHERE nProviderID=" + JproviderId.ToString()
            strResult = oDb.GetRecord_Query(strSql)
            If Not IsDBNull(strResult) Then

                If strResult.ToString() = "True" Then
                    bProv(1) = True
                Else
                    bProv(1) = False
                End If
            Else
                bProv(1) = False
            End If

            Return bProv
        Catch ex As Exception
            isBlockedProvider = Nothing
            Throw ex
        Finally
            oDb.Dispose()
            oDb = Nothing
        End Try
    End Function


    Public Function GetAllSeniorProviders() As DataTable
        Dim dt As DataTable
        Dim conn As New SqlConnection(GetConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
            _sqlstr = "SELECT nProviderID, ISNULL(sFirstName,'') + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When ISNULL(Provider_MST.sMiddleName,'') then  ISNULL(Provider_MST.sMiddleName,'')  END + ' ' +ISNULL(sLastName,'') AS sProviderName, " _
                      & "sDEA FROM Provider_MST WHERE nProviderType = 0"
            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return dt
        Finally
            If IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            'If IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function
    ''Added On 20101006 by sanjog 
    Public Function GetAllAssignProviders(ByVal userid As Int64) As DataTable
        Dim dt As DataTable
        Dim conn As New SqlConnection(GetConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
            _sqlstr = "select DISTINCT pr.nProviderID ,ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from ProviderSignature_DTl p inner join provider_mst pr on p.nproviderid=pr.nproviderid where p.nUSerID =" & userid & ""
            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            Return dt
        Finally
            If IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            'If IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function
    ''Added On 20101006 by sanjog 
    Public Function CheckExamProviderStatus(ByVal nExamId As Long, ByVal nVisitID As Long, ByVal nProviderID As Long) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            ''strSQL = "select * from PatientExams Inner Join visits On visits.nVisitID =PatientExams.nVisitID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " and visits.nProviderID=" & nProviderID & ""
            strSQL = "select nExamID from PatientExams where PatientExams.nExamID=" & nExamId & " and PatientExams.nProviderID=" & nProviderID & ""
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            CheckExamProviderStatus = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function CheckSignDelegateStatus() As Boolean

        Dim oDB As New DataBaseLayer
        Dim strSQL As String

        Try

            strSQL = "SELECT IsNull(sSettingsValue,'') from settings where sSettingsName Like '%USESIGNATUREDELEGATES%'"

            'If Not IsDBNull(oDB.GetRecord_Query(strSQL)) Then
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If strResult.ToString() <> "0" Then
                Return True
            Else
                Return False
            End If
            'Else
            'Return False
            'End If

        Catch ex As Exception
            CheckSignDelegateStatus = Nothing
            Throw ex

        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Function


    'Developer: Yatin N.Bhagat
    'Date:01/27/2012
    'Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688 
    'Reason: If Condition is added to check the Setting to add login user name in the Sign
    Public Function AddUserNameInProviderSignature() As Integer
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "SELECT sSettingsValue from settings where sSettingsName Like '%signatureformatvalue%'"
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult.ToString() = "1" Then
                    Return 1
                ElseIf strResult.ToString() = "2" Then
                    Return 2
                ElseIf strResult.ToString() = "3" Then
                    Return 3
                ElseIf strResult.ToString() = "4" Then
                    Return 4
                End If
                Return 0
            Else
                Return 0
            End If
        Catch ex As Exception
            AddUserNameInProviderSignature = 0
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function


    Public Function CheckpatientProviderStatus(ByVal nPatientId As Long, ByVal nProviderID As Long) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" & nPatientId & " and Patient.nProviderID=" & nProviderID & ""
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            CheckpatientProviderStatus = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    ''Added On 20101006 by sanjog 
    ''Added On 20101006 by sanjog 
    Public Function GetExamProvider(ByVal nExamId As Long, ByVal nVisitID As Long, Optional ByVal nPatientID As Long = 0) As String
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            'strSQL = "select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr Inner Join VISITS On visits.nProviderID = pr.nProviderID Inner Join PatientExams On visits.nVisitID =PatientExams.nVisitID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " and visits.nPatientID=" & nPatientID & ""
            strSQL = "select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr  Inner Join PatientExams ON PatientExams.nProviderID=pr.nProviderID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " "
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return strResult
                Else
                    Return strResult
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            GetExamProvider = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function GetExamProviderID(ByVal nExamId As Long, ByVal nVisitID As Long) As Int64
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            'strSQL = "select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr Inner Join VISITS On visits.nProviderID = pr.nProviderID Inner Join PatientExams On visits.nVisitID =PatientExams.nVisitID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " and visits.nPatientID=" & nPatientID & ""
            strSQL = "select pr.nProviderID AS nProviderID from Provider_MSt pr  Inner Join PatientExams ON PatientExams.nProviderID=pr.nProviderID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " "
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""

            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If IsDBNull(strResult) = False Then
                If strResult.Trim <> "" Then
                    Return Convert.ToInt64(strResult)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            GetExamProviderID = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function GetExamAssignProviders(ByVal nExamId As Long, ByVal nVisitID As Long, ByVal nUserID As Long, ByVal PatientID As Long) As DataSet
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        ' Dim strProviderName As String
        Dim ds As DataSet = Nothing
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nExamId"
            oParameter.Value = nExamId
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nVisitID"
            oParameter.Value = nVisitID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nUserID"
            oParameter.Value = nUserID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            'oParameter = New DBParameter
            'oParameter.DataType = SqlDbType.BigInt
            'oParameter.Direction = ParameterDirection.Input
            'oParameter.Name = "@patientID"
            'oParameter.Value = PatientID
            'oDB.DBParametersCol.Add(oParameter)
            'oParameter = Nothing

            ds = oDB.GetDataSet("gsp_GetAssignProviders")
            Return ds
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(ds) = False Then
            '    ds.Dispose()
            '    ds = Nothing
            'End If
        End Try
    End Function
    Public Function GetStatusOfProviderIDinSign(ByVal nProviderId As Long, ByVal nUserId As Long) As Boolean
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            'strSQL = "select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr Inner Join VISITS On visits.nProviderID = pr.nProviderID Inner Join PatientExams On visits.nVisitID =PatientExams.nVisitID where PatientExams.nExamID=" & nExamId & " and PatientExams.nVisitID=" & nVisitID & " and visits.nPatientID=" & nPatientID & ""
            strSQL = "select nProviderId,nUSerID from ProviderSignature_DTL where nProviderId =" & nProviderId & " AND nUSerID=" & nUserId & ""
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            GetStatusOfProviderIDinSign = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    ''Added On 20101006 by sanjog 
    Public Function GetProviderName(ByVal nProviderID As Long) As String
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "select DISTINCT ISNULL(sFirstName,'') + ' ' + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When ISNULL(sMiddleName,'') then  ISNULL(sMiddleName,'') + ' 'END + ISNULL(sLastName,'') AS sProviderName from Provider_MSt  where nProviderID=" & nProviderID & ""
            ''strSQL = "select * from PatientExams where nExamID=" & nExamId & " and nVisitID=" & nVisitID & " and nPatientID=" & nPatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return strResult
                Else
                    Return strResult
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            GetProviderName = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Function GetPatientProviderDetails(ByVal nPatientID As Long) As DataSet

        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        Dim ds As DataSet = Nothing

        Try

            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nPatientID"
            oParameter.Value = nPatientID
            oDB.DBParametersCol.Add(oParameter)

            ds = oDB.GetDataSet("gsp_GetPatientProviderBasicDetails")

            ds.Tables(0).TableName = "PatientProviderBasicDetails"

            Return ds

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Public Function GetPatientProviderName(ByVal npatientID As Long) As String

        Dim oDB As New DataBaseLayer
        Dim strSQL As String

        Try

            strSQL = "Select DISTINCT ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" & npatientID & ""

            'If Not IsDBNull(oDB.GetRecord_Query(strSQL)) Then
            Dim strResult As String = oDB.GetRecord_Query(strSQL)

            If strResult <> "" Then
                Return strResult
            Else
                Return strResult
            End If
            'Else
            'Return ""
            'End If

        Catch ex As Exception
            GetPatientProviderName = Nothing
            Throw ex

        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try

    End Function
    Public Function GetPatientProviderNameWithPrefix(ByVal npatientID As Long) As String
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Try
            strSQL = "select DISTINCT CASE ISNULL(pr.sPrefix,'') WHEN '' THEN '' WHEN ISNULL(pr.sPrefix,'') THEN ISNULL(pr.sPrefix,'')+' 'END +ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'')+ CASE ISNULL(pr.sSuffix,'') WHEN '' THen '' WHEN ISNULL(pr.sSuffix,'') THEN +' '+ ISNULL(pr.sSuffix,'') END AS sProviderName from Provider_MSt pr INNER JOIn PAtient ON Patient.nProviderID=pr.nProviderID where Patient.npatientID=" & npatientID & ""
            Dim strResult As String = oDB.GetRecord_Query(strSQL)
            If Not IsDBNull(strResult) Then

                If strResult <> "" Then
                    Return strResult
                Else
                    Return strResult
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            GetPatientProviderNameWithPrefix = Nothing
            Throw ex
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    ''Added On 20101006 by sanjog 
    Public Function GetAllJuniorProviders() As DataTable
        Dim dt As DataTable
        Dim conn As New SqlConnection(GetConnectionString)
        Dim da As SqlDataAdapter = Nothing
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
            _sqlstr = "SELECT nProviderID, ISNULL(sFirstName,'') + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST.sMiddleName  END + ' ' + ISNULL(sLastName,'') AS sProviderName, sDEA FROM Provider_MST WHERE nProviderType = 1"
            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return dt
        Finally
            If IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            'If IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Public Sub SaveProviderAssociation(ByVal nProviderID As Int64, ByVal arrSeniorProvider As ArrayList)
        Dim oTransaction As SqlTransaction = Nothing
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim _Query As String
        Try
            If arrSeniorProvider IsNot Nothing Then


                con.Open()
                oTransaction = con.BeginTransaction

                _Query = " DELETE FROM ProviderSettings WHERE sSettingsType = 'ProviderSeniorAssignment' AND nProviderID = " & nProviderID & " "
                cmd = New SqlCommand
                cmd.Connection = con
                cmd.Transaction = oTransaction
                cmd.CommandText = _Query
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                cmd = New SqlCommand
                cmd.Connection = con
                cmd.Transaction = oTransaction
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "adm_INSERT_JrSrAssociation"
                For i As Integer = 0 To arrSeniorProvider.Count - 1
                    cmd.Parameters.Clear()

                    cmd.Parameters.Add("@JrDocID", SqlDbType.BigInt)
                    cmd.Parameters.Add("@SrDocID", SqlDbType.BigInt)

                    cmd.Parameters("@JrDocID").Value = nProviderID
                    cmd.Parameters("@SrDocID").Value = arrSeniorProvider(i)
                    cmd.ExecuteNonQuery()
                Next

                oTransaction.Commit()
                con.Close()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                con.Dispose()
                con = Nothing
            End If
        Catch ex As Exception
            If IsNothing(oTransaction) = False Then
                oTransaction.Rollback()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(oTransaction) = False Then
                oTransaction.Dispose()
            End If
            If Not IsNothing(con) Then
                If (con.State = ConnectionState.Open) Then ''connection state close
                    con.Close()
                    con.Dispose()
                End If
            End If
        End Try
    End Sub

    Public Function IsProvider_Senior(ByVal nProviderID As Int64) As Boolean
        Dim con As SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim myReader As SqlDataReader
        Dim _nProviderType As Int16
        Try
            con = New SqlConnection(GetConnectionString)
            Dim _Query As String = "SELECT ISNull(nIsRequireSupervisingProviderforeRx,0),nProviderType FROM Provider_MST WHERE nProviderID = " & nProviderID
            cmd = New SqlCommand(_Query, con)
            con.Open()
            myReader = cmd.ExecuteReader()
            If myReader.HasRows Then
                Dim fNextResult As Boolean = True
                Do Until Not fNextResult
                    Do While myReader.Read()
                        _blnRequireSupervisingProviderforeRx = myReader.GetBoolean(0)
                        _nProviderType = myReader(1)
                    Loop
                    fNextResult = myReader.NextResult()
                Loop

               
            End If
            myReader.Close()
            con.Close()
            If _nProviderType = 0 Then
                Return True
            Else
                Return False
            End If
            con.Dispose()
            con = Nothing

        Catch ex As Exception
            Return True
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


        End Try
    End Function

    Public Function GetDefaultSupervisingProvider(ByVal nVisitID As Int64, ByVal nPatientID As Int64) As DataTable
        Dim oDB As DataBaseLayer = Nothing
        Dim oParameter As DBParameter = Nothing
        Dim strProviderName As String = String.Empty
        Dim dtDefaultSupervisingProvider As DataTable = Nothing
        Try
            oDB = New DataBaseLayer
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@VisitID"
            oParameter.Value = nVisitID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@PatientID"
            oParameter.Value = nPatientID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            dtDefaultSupervisingProvider = oDB.GetSupervisingProviderData("gsp_GetDefaultSupervisingProvider") ''''method name changed for Prescription Provider issue Bug #46975 in 7022
            Return dtDefaultSupervisingProvider
        Catch ex As SqlException
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return dtDefaultSupervisingProvider
        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return dtDefaultSupervisingProvider
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            'If IsNothing(dtDefaultSupervisingProvider) = False Then
            '    dtDefaultSupervisingProvider.Dispose()
            '    dtDefaultSupervisingProvider = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetProvidersInExam(ByVal PatientID As Int64) As DataSet
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        ' Dim strProviderName As String
        Dim ds As DataSet = Nothing
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nPatientID"
            oParameter.Value = PatientID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing


            ds = oDB.GetDataSet("gsp_GetProvidersinExam")
            ds.Tables(0).TableName = "ExamProvider"
            ds.Tables(1).TableName = "AllProvider"
            Return ds
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(ds) = False Then
            '    ds.Dispose()
            '    ds = Nothing
            'End If
        End Try
    End Function

    '' END SUDHIR ''
#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If (bAssigned) Then
                    If (IsNothing(_imgSignature) = False) Then
                        _imgSignature.Dispose()
                        _imgSignature = Nothing
                    End If
                    bAssigned = False
                End If
                _sFirstName = Nothing
                _sMiddleName = Nothing
                _sLastName = Nothing
                _sGender = Nothing
                _sDEA = Nothing
                _sAddress = Nothing
                _sStreet = Nothing
                _sCity = Nothing
                _sState = Nothing
                _sZIP = Nothing
                _sPhone = Nothing
                _sFAX = Nothing
                _sMobile = Nothing
                _sPager = Nothing
                _sEmail = Nothing
                _sURL = Nothing
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
