Imports System.IO
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsPatient

    Enum enmPatientSearchCriteria
        PatientCode
        PatientFirstName
        PatientLastName
    End Enum
   
    Public Function Fill_LastPatients(Optional ByVal strProvider As String = "All", Optional ByVal strLocation As String = "All") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        '  Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillLastPatients"
        objCmd.Parameters.Clear()
        If strProvider <> "All" Then
            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = strProvider
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)
            objParaProvider = Nothing
        End If

        If strLocation <> "All" Then
            Dim objPara As New SqlParameter
            With objPara
                .ParameterName = "@Location"
                .Value = strLocation
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objPara)
            objPara = Nothing
        End If

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objDA.Dispose()
        objDA = Nothing
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        Dim myTable As DataTable = dsData.Tables(0).Copy()
        dsData.Dispose()
        dsData = Nothing
        Return myTable
        '''' PatientID, PatientCode, PatientFirstName, PatientLastName, SSNNo, Provider, PatientDOB, Phone, 
        '''' sMother_fName, sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
    End Function

    Public Function GetPatientPhoto(ByVal nPatientID As Long) As Image


        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillPatientPhoto"
        objCmd.Parameters.Clear()
        Dim objParaPatientID As New SqlParameter
        With objParaPatientID
            .ParameterName = "@PatientID"
            .Value = nPatientID
            .Direction = ParameterDirection.Input
            '.SqlDbType = SqlDbType.bigInt
        End With
        objCmd.Parameters.Add(objParaPatientID)

        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        Try
            If objSQLDataReader.HasRows = False Then

                Return Nothing

                '      Exit Function
            Else
                objSQLDataReader.Read()
                If IsDBNull(objSQLDataReader.Item(0)) = False Then
                    Dim arrPicture() As Byte = CType(objSQLDataReader.Item(0), Byte())
                    'SLR: Commented as static function was written

                    'Dim gloPicture As gloPictureBox.gloPictureBox = New gloPictureBox.gloPictureBox()
                    'gloPicture.sZoomVersion = "7X"
                    'gloPicture.byteImage = arrPicture
                    'Dim imgPatientPhoto As Image = CType(gloPicture.Image.Clone(), Image)
                    'gloPicture.Dispose()
                    ''Dim ms As New MemoryStream(arrPicture)
                    ''imgPatientPhoto = Image.FromStream(ms)
                    ''ms.Close()
                    ''ms.Dispose()
                    'Return imgPatientPhoto
                    Return gloPictureBox.gloImage.GetImage(arrPicture, True)
                Else
                   
                    Return Nothing

                End If
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            objSQLDataReader.Close()
            objSQLDataReader = Nothing
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objParaPatientID = Nothing
        End Try
       
        

    End Function

    Public Function Fill_Patients(ByVal strText As String, ByVal enmCriteria As enmPatientSearchCriteria) As DataTable

        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_SearchPatient"
        objCmd.Parameters.Clear()

        Dim objParaSearchCriteria As New SqlParameter
        With objParaSearchCriteria
            .ParameterName = "@SearchCriteria"
            Select Case enmCriteria
                Case enmPatientSearchCriteria.PatientCode
                    .Value = "Code"
                Case enmPatientSearchCriteria.PatientFirstName
                    .Value = "FirstName"
                Case enmPatientSearchCriteria.PatientLastName
                    .Value = "LastName"
            End Select
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSearchCriteria)

        Dim objParaSearchText As New SqlParameter
        With objParaSearchText
            .ParameterName = "@SearchText"
            .Value = strText
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSearchText)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objDA.Dispose()
        objDA = Nothing
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaSearchCriteria = Nothing
        objParaSearchText = Nothing

        Dim myTable As DataTable = dsData.Tables(0).Copy()
        dsData.Dispose()
        dsData = Nothing
        Return myTable
    End Function

    Public Function ChangePatientProvider(ByVal patientID As Int64, ByVal providerID As Int64) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            Dim query As String = " UPDATE Patient SET nProviderID = " & providerID & " WHERE nPatientID = " & patientID & " "
            Dim result As Integer
            oDB.Connect(False)
            result = oDB.Execute_Query(query)
            If result > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If IsNothing(oDB) = False Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

#Region " Merge Patients "
    '' 20021222
    Public Function Fill_DuplicatePatient() As DataTable
        'gsp_FillDuplicatePatients
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand("gsp_FillDuplicatePatients", Con)
        cmd.CommandType = CommandType.StoredProcedure

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        da.Dispose()
        da = Nothing
        Con.Close()
        Con.Dispose()
        Con = Nothing
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        Return dt
    End Function

    Public Function Merge_Patients(ByVal PatientID As Long, ByVal PatientCode As String, ByVal MergeInPatientID As Long, ByVal MergeInPatientCode As String, Optional ByVal AlertType As gloStream.gloAlert.Alert.Alert_Type = 1, Optional ByVal PatientCodeAlert As String = "") As Boolean
        '   gsp_MergePatient
        '   @ActivityCategory varchar(100)='',
        '	@Description varchar(1000)='',
        '	@UserName varchar(100),
        '	@MachineName varchar(100),

        '	@PatientID numeric(18,0),
        '	@PatientCode varchar(50),	

        '	@MergeInPatientID numeric(18,0),
        '	@MergeInPatientCode varchar(50)	



        ''''Functionality Add By Pramod to move DMS file Into Merged Patient File Location.
        Dim DocumentFolderPath_DMS As String = ""
        Dim DocumentFolderNewPath_DMS As String = ""
        Dim i As Integer
        Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
        Dim con As New SqlConnection(GetConnectionString)
        Dim strselectQry_DMS As String = "Select SystemFolder,Container,Category,PatientID,Year,Month,DocumentFileName,Extension " _
                                         & " from DMS_MST WHERE PatientID = '" & PatientID & "' "
        Dim dt_PatienID_DMS As New DataTable
        Dim sda As New SqlDataAdapter(strselectQry_DMS, con)
        sda.Fill(dt_PatienID_DMS)
        sda.Dispose()
        sda = Nothing
        If Not IsNothing(dt_PatienID_DMS) Then
            If dt_PatienID_DMS.Rows.Count > 0 Then
                For i = 0 To dt_PatienID_DMS.Rows.Count - 1


                    DocumentFolderPath_DMS = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & dt_PatienID_DMS.Rows(i)("SystemFolder") & "\" & dt_PatienID_DMS.Rows(i)("Container") & "\" & dt_PatienID_DMS.Rows(i)("Category") & "\" & dt_PatienID_DMS.Rows(i)("PatientID") & "\" & dt_PatienID_DMS.Rows(i)("Year") & "\" & dt_PatienID_DMS.Rows(i)("Month") & "\" & dt_PatienID_DMS.Rows(i)("DocumentFileName") & "." & dt_PatienID_DMS.Rows(i)("Extension")

                    If File.Exists(DocumentFolderPath_DMS) = True Then
                        DocumentFolderNewPath_DMS = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(DMSRootPath)
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("SystemFolder") & "\"
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("Container") & "\"
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("Category") & "\"
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & MergeInPatientID & "\" 'dt_PatienID_DMS.Rows(i)("PatientID")
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("Year") & "\"
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("Month") & "\"
                        If Directory.Exists(DocumentFolderNewPath_DMS) = False Then
                            MkDir(DocumentFolderNewPath_DMS)
                        End If
                        DocumentFolderNewPath_DMS = DocumentFolderNewPath_DMS & dt_PatienID_DMS.Rows(i)("DocumentFileName") & "." & dt_PatienID_DMS.Rows(i)("Extension")

                        File.Move(DocumentFolderPath_DMS, DocumentFolderNewPath_DMS)
                    End If
                Next
            End If
            For i = 0 To dt_PatienID_DMS.Rows.Count - 1
                Dim PathToDelete As String = ""
                PathToDelete = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(DMSRootPath) & Trim(dt_PatienID_DMS.Rows(i)("SystemFolder")) & "\" & Trim(dt_PatienID_DMS.Rows(i)("Container")) & "\" & Trim(dt_PatienID_DMS.Rows(i)("Category")) & "\" & PatientID 'dt_PatienID_DMS.Rows(i)("PatientID")
                ' Dim TempPath As String = PathToDelete
                Dim TempPath As String = ""
                If Directory.Exists(PathToDelete) = True Then
                    If TempPath <> PathToDelete Then
                        Directory.Delete(PathToDelete, True)
                    End If
                    TempPath = PathToDelete
                End If
            Next
            dt_PatienID_DMS.Dispose()
            dt_PatienID_DMS = Nothing
        End If

      


        ''''Functionality Add By Pramod to move VMS file Into Merged Patient File Location.
        Dim DocumentFolderPath_VMS As String = ""
        Dim DocumentFolderNewPath_VMS As String = ""
        Dim j As Integer
        'Dim oPathFolders As New gloStream.gloDMS.Supporting.PathsAndFolders
        'Dim con As New SqlConnection(GetConnectionString)
        Dim strselectQry_VMS As String = "Select systemFolder,Container,Category,PatientID,Year,Month,DocumentFileName,Extension " _
                                         & " from VMS_MST WHERE PatientID = '" & PatientID & "' "
        Dim dt_PatienID_VMS As New DataTable
        sda = New SqlDataAdapter(strselectQry_VMS, con)
        sda.Fill(dt_PatienID_VMS)
        sda.Dispose()
        sda = Nothing
        If Not IsNothing(dt_PatienID_VMS) Then
            If dt_PatienID_VMS.Rows.Count > 0 Then
                For j = 0 To dt_PatienID_VMS.Rows.Count - 1

                    DocumentFolderPath_VMS = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(VMSRootPath) & dt_PatienID_VMS.Rows(j)("SystemFolder") & "\" & dt_PatienID_VMS.Rows(j)("Container") & "\" & dt_PatienID_VMS.Rows(j)("Category") & "\" & dt_PatienID_VMS.Rows(j)("PatientID") & "\" & dt_PatienID_VMS.Rows(j)("Year") & "\" & dt_PatienID_VMS.Rows(j)("Month") & "\" & dt_PatienID_VMS.Rows(j)("DocumentFileName") & dt_PatienID_VMS.Rows(j)("Extension")

                    If File.Exists(DocumentFolderPath_VMS) = True Then
                        DocumentFolderNewPath_VMS = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(VMSRootPath)
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("SystemFolder") & "\"
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("Container") & "\"
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("Category") & "\"
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & MergeInPatientID & "\"  'dt_PatienID_DMS.Rows(j)("PatientID")
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("Year") & "\"
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("Month") & "\"
                        If Directory.Exists(DocumentFolderNewPath_VMS) = False Then
                            MkDir(DocumentFolderNewPath_VMS)
                        End If
                        DocumentFolderNewPath_VMS = DocumentFolderNewPath_VMS & dt_PatienID_VMS.Rows(j)("DocumentFileName") & dt_PatienID_VMS.Rows(j)("Extension")

                        File.Move(DocumentFolderPath_VMS, DocumentFolderNewPath_VMS)
                    End If
                Next
            End If
            For j = 0 To dt_PatienID_VMS.Rows.Count - 1
                Dim PathToDelete As String = ""
                PathToDelete = gloStream.gloDMS.Supporting.PathsAndFolders.GetDMSPath(VMSRootPath) & Trim(dt_PatienID_VMS.Rows(j)("SystemFolder")) & "\" & Trim(dt_PatienID_VMS.Rows(j)("Container")) & "\" & Trim(dt_PatienID_VMS.Rows(j)("Category")) & "\" & PatientID 'dt_PatienID_DMS.Rows(j)("PatientID")
                ' Dim TempPath As String = PathToDelete
                Dim TempPath As String = ""
                If Directory.Exists(PathToDelete) = True Then
                    If TempPath <> PathToDelete Then
                        Directory.Delete(PathToDelete, True)
                    End If
                    TempPath = PathToDelete
                End If
            Next
            dt_PatienID_VMS.Dispose()
            dt_PatienID_VMS = Nothing
        End If

     


        Dim cmd As New SqlCommand("gsp_MergePatient", con)
        cmd.CommandType = CommandType.StoredProcedure
        '' Patient merge time out issue
        cmd.CommandTimeout = 0
        Dim sqlPara As SqlParameter

        sqlPara = cmd.Parameters.Add("@ActivityCategory", SqlDbType.VarChar, 100)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = "Merge"
        End With

        sqlPara = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 1000)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = ""
        End With
        ''Added by Mayuri:20100105-Added Pm Transaction tables replated to nInsuranceID and nClinicID
        sqlPara = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = gnClinicID
        End With

        sqlPara = cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = gstrLoginName
        End With

        sqlPara = cmd.Parameters.Add("@MachineName", SqlDbType.VarChar, 100)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = gstrClientMachineName
        End With

        sqlPara = cmd.Parameters.Add("@PatientID ", SqlDbType.BigInt)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = PatientID
        End With

        sqlPara = cmd.Parameters.Add("@PatientCode ", SqlDbType.VarChar, 50)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = PatientCode
        End With

        sqlPara = cmd.Parameters.Add("@MergeInPatientID ", SqlDbType.BigInt)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = MergeInPatientID
        End With

        sqlPara = cmd.Parameters.Add("@MergeInPatientCode ", SqlDbType.VarChar, 50)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = MergeInPatientCode
        End With
        ''Parameters Added by Mayuri:20100421-To fix case No:#GLO2010-0004863
        ''Specially Used for Alerts
        sqlPara = cmd.Parameters.Add("@AlertType ", SqlDbType.Int)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = AlertType
        End With
        sqlPara = cmd.Parameters.Add("@MachineID ", SqlDbType.BigInt)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = GetPrefixTransactionID()
        End With
        sqlPara = cmd.Parameters.Add("@PatientCodeAlert ", SqlDbType.VarChar)
        With sqlPara
            .Direction = ParameterDirection.Input
            .Value = PatientCodeAlert
        End With
        ''End code Added by Mayuri:20100421

        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        con.Dispose()
        con = Nothing
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        sqlPara = Nothing
        Return True

    End Function

    Public Function Fill_PatientDetails(ByVal PatientID As Long) As DataTable

        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand("gsp_FetchPatientDetails", Con)
        ' Dim dr As SqlDataReader
        cmd.CommandType = CommandType.StoredProcedure

        Dim SQLPara As SqlParameter
        SQLPara = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
        With SQLPara
            .Direction = ParameterDirection.Input
            .Value = PatientID
        End With

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        da.Dispose()
        da = Nothing
        Con.Close()
        Con.Dispose()
        Con = Nothing
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If

        SQLPara = Nothing
        Return dt
    End Function

#End Region
#Region "GetProviders"
    Public Function GetProviders(ByVal ClinicID As Int64) As DataTable
        Dim odb As New gloStream.gloDataBase.gloDataBase
        '   Dim objCon As New SqlConnection
        '  Dim objCmd As New SqlCommand
        Dim dtProvider As DataTable = Nothing
        Dim _strSQL As String = ""


        Try
            odb.Connect(GetConnectionString)
            '_strSQL = "Select ISNULL(sFirstName,'')+Space(2)+ISNULL(sMiddleName,'')+Space(2)+ISNULL(sLastName,'')  FROM Provider_MST WHERE ISNULL(bIsBlocked,0) = 0 Order by sFirstName,sMiddleName,sLastName "
            ''_strSQL = "SELECT * , (ISNULL(sFirstName,'')+ SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) +ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  WHERE  bIsblocked='FALSE' ORDER BY ProviderName"
            _strSQL = "SELECT nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  sMiddleName + SPACE(1) END +ISNULL(sLastName,''))AS ProviderName  FROM Provider_MST WHERE  ISNULL(bIsblocked,'false')='FALSE' AND nClinicID = " & ClinicID & " ORDER BY ProviderName"

            ''odb.Connect(GetConnectionString)
            dtProvider = odb.ReadQueryDataTable(_strSQL)
            odb.Disconnect()
            Return dtProvider
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If odb IsNot Nothing Then

                odb.Dispose()
                odb = Nothing
            End If
        End Try


    End Function
#End Region

#Region "GetLocations"
    Public Function GetLocations(ByVal ClinicID As Int64) As DataTable
        Dim odb As New gloStream.gloDataBase.gloDataBase
        '        Dim objCon As New SqlConnection
        '       Dim objCmd As New SqlCommand
        Dim dtLocation As DataTable = Nothing
        Dim _strSQL As String = ""


        Try
            odb.Connect(GetConnectionString)
            '_strSQL = "Select ISNULL(sFirstName,'')+Space(2)+ISNULL(sMiddleName,'')+Space(2)+ISNULL(sLastName,'')  FROM Provider_MST WHERE ISNULL(bIsBlocked,0) = 0 Order by sFirstName,sMiddleName,sLastName "
            _strSQL = "SELECT nLocationID, ISNULL(sLocation,'') AS sLocation,nClinicID, bIsDefault FROM AB_Location WHERE bIsBlocked = 0 AND nClinicID = " & ClinicID & ""

            ''odb.Connect(GetConnectionString)
            dtLocation = odb.ReadQueryDataTable(_strSQL)
            odb.Disconnect()
            Return dtLocation
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing
        Finally
            If odb IsNot Nothing Then

                odb.Dispose()
                odb = Nothing
            End If
        End Try


    End Function
#End Region

#Region "fill Diagnosis"
    Public Function FillDiagnosis(ByVal SearchText As String, Optional ByVal ICDtype As String = "9") As DataTable
        Dim odb As New gloStream.gloDataBase.gloDataBase
        Dim odt As DataTable = Nothing
        Dim _strSQL As String = ""
        SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "")
        Try
            '_strSQL = "select Distinct (isNull(sICD9Code,'')) as sICD9Code,isNull(sICD9Description,'')) as sICD9Description from ExamICD9CPT"

            If (ICDtype = "9") Then
                If SearchText.Trim() <> "" Then

                    ' _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code, ISNULL(sICD9Description,'') AS sICD9Description FROM ExamICD9CPT Where sICD9code <> '' AND sICD9code like '%" & SearchText & "%' OR sICD9Description like '%" & SearchText & "%'"
                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> '' AND sICD9code like '%" & SearchText & "%' AND ISNULL(nICDRevision,9)=9 "

                Else

                    '_strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code, ISNULL(sICD9Description,'') AS sICD9Description FROM ExamICD9CPT Where sICD9code <> '' Order by sICD9code"
                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> '' AND ISNULL(nICDRevision,9)=9 Order by sICD9code"
                End If
            End If
            If (ICDtype = "10") Then
                If SearchText.Trim() <> "" Then

                    ' _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code, ISNULL(sICD9Description,'') AS sICD9Description FROM ExamICD9CPT Where sICD9code <> '' AND sICD9code like '%" & SearchText & "%' OR sICD9Description like '%" & SearchText & "%'"
                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> '' AND sICD9code like '%" & SearchText & "%' AND ISNULL(nICDRevision,9)=10 "

                Else

                    '_strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code, ISNULL(sICD9Description,'') AS sICD9Description FROM ExamICD9CPT Where sICD9code <> '' Order by sICD9code"
                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> ''  AND ISNULL(nICDRevision,9)=10  Order by sICD9code"
                End If
            End If

            ''added for All Condition
            If (ICDtype = "0") Then
                If SearchText.Trim() <> "" Then

                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> '' AND sICD9code like '%" & SearchText & "%' "



                Else

                    _strSQL = "SELECT DISTINCT ISNULL(sICD9code,'') AS sICD9code FROM ExamICD9CPT Where sICD9code <> '' Order by sICD9code"


                End If
            End If

            'If FlagType = 0 Then
            'Else
            '    _strSQL = "SELECT DISTINCT ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription FROM ExamICD9CPT WHERE sCPTcode=Order by sCPTcode"
            'End If
            '_strSQL = "SELECT DISTINCT ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription FROM ExamICD9CPT Order by sCPTcode"

            odb.Connect(GetConnectionString)
            odt = odb.ReadQueryDataTable(_strSQL)
            odb.Disconnect()
            Return odt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If odb IsNot Nothing Then

                odb.Dispose()
                odb = Nothing
            End If

        End Try
    End Function
#End Region

    Public Function CheckPatientCommunicationPrefers(ByVal nPatientID As Long) As String


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim dt As DataTable = Nothing

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientId"
            oParamater.Value = nPatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            dt = oDB.GetDataTable("gsp_checkPatitentPrefcomm")
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt(0)(0).ToString()
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally



            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Function

#Region " ''Start :: call from mergePatient"
    Public Function GetPatientExamCount(ByVal nPatientID As Int64) As Integer
        ' Dim ExamCount As Integer
        Dim Con As New SqlConnection(GetConnectionString)
        Dim strQRY As String
        Dim _Result As Object
        Dim _ExamCount As Integer
        Dim cmd As SqlCommand = Nothing
        Try
            If Not IsNothing(Con) Then
                strQRY = "SELECT Count(nExamID) FROM PatientExams where nPatientID = " & nPatientID & ""
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd = New SqlCommand(strQRY, Con)
                If Not IsNothing(cmd) Then
                    cmd.CommandType = CommandType.Text
                    _Result = cmd.ExecuteScalar()
                    If Con.State = ConnectionState.Open Then
                        Con.Close()
                    End If

                    If Not IsNothing(_Result) Then
                        If _Result.ToString() <> "" Then
                            _ExamCount = CType(_Result, Integer)
                        Else
                            _ExamCount = 0
                        End If

                    Else
                        _ExamCount = 0
                    End If
                End If
            End If
        Catch
        Finally
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        Return _ExamCount
    End Function

    Public Function Merge_Demographics(ByVal PatientID As Long, ByVal MergeInPatientID As Long) As Boolean
        ''''''''''''''To Merge Demographic Details by Ujwala Atre as on 20100409
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand
        Dim sqlPara As SqlParameter
        Try
            If Not IsNothing(con) Then
                cmd = New SqlCommand("MergePatientDemo", con)
                If Not IsNothing(cmd) Then
                    cmd.CommandType = CommandType.StoredProcedure
                    ''''''''''''''
                    sqlPara = cmd.Parameters.Add("@SourcePatientID", SqlDbType.BigInt) '("@PatientID", SqlDbType.BigInt)
                    With sqlPara
                        .Direction = ParameterDirection.Input
                        .Value = PatientID
                    End With
                    ''''''''''''''
                    ''''''''''''''
                    sqlPara = cmd.Parameters.Add("@DestinationPatientID", SqlDbType.BigInt) '("@MergeInPatientID", SqlDbType.BigInt)
                    With sqlPara
                        .Direction = ParameterDirection.Input
                        .Value = MergeInPatientID
                    End With
                    ''''''''''''''
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    cmd.ExecuteNonQuery()
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlPara = Nothing
        End Try
       

        Return True
        ''''''''''''''To Merge Demographic Details by Ujwala Atre as on 20100409
    End Function

#End Region
    ''End :: call from mergePatient
End Class

