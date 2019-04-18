Imports C1.Win.C1FlexGrid

Module mdlDataCather
    Public gServerName As String
    Public gDataBase As String

    'Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String) As String
    '    Dim strConnectionString As String
    '    strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
    '    Return strConnectionString
    'End Function
    'Public Function GetConnectionString() As String
    '    Return GetConnectionString(gServerName, gDataBase)
    'End Function

    Public Const COL_VER_MAPID = 0
    Public Const COL_VER_DBFIELD = 1
    Public Const COL_VER_DISPFIELD = 2
    Public Const COL_VER_DATA = 3
    Public Const COL_VER_LINENO = 4


    'Column Name to pick value & Assign from mapping
    Public ROW_PatCode As Integer = 1
    Public ROW_PatFName As Integer = 2
    Public ROW_PatMName As Integer = 3
    Public ROW_PatLName As Integer = 4
    Public ROW_PatSSN As Integer = 5
    Public ROW_PatDOB As Integer = 6
    Public ROW_PatGender As Integer = 7
    Public ROW_PatMS As Integer = 8
    Public ROW_PatAdd1 As Integer = 9
    Public ROW_PatAdd2 As Integer = 10
    Public ROW_PatCity As Integer = 11
    Public ROW_PatState As Integer = 12
    Public ROW_PatZip As Integer = 13
    Public ROW_PatCountry As Integer = 14
    Public ROW_PatPhone As Integer = 15
    Public ROW_PatMobile As Integer = 16
    Public ROW_PatEmail As Integer = 17
    Public ROW_PatFax As Integer = 18
    Public ROW_PatOccupation As Integer = 19
    Public ROW_PatEmpStatus As Integer = 20
    Public ROW_PatPlace As Integer = 21
    Public ROW_PatWrkAdd1 As Integer = 22
    Public ROW_PatWrkAdd2 As Integer = 23
    Public ROW_PatWrkCity As Integer = 24
    Public ROW_PatWrkState As Integer = 25
    Public ROW_PatWrkZip As Integer = 26
    Public ROW_PatWrkPhone As Integer = 27
    Public ROW_PatWrkFax As Integer = 28
    Public ROW_PatComplaints As Integer = 29
    Public ROW_PatProID As Integer = 30
    Public ROW_PatPCPID As Integer = 31
    Public ROW_PatGuarantor As Integer = 32
    Public ROW_PatPharmacyID As Integer = 33
    Public ROW_PatSpouseName As Integer = 34
    Public ROW_PatSpousePhone As Integer = 35
    Public ROW_PatRace As Integer = 36
    Public ROW_PatPatientStatus As Integer = 37
    Public ROW_PatPhoto As Integer = 38
    Public ROW_PatRegistrationDate As Integer = 39
    Public ROW_PatInjuryDate As Integer = 40
    Public ROW_PatSurgeryDate As Integer = 41
    Public ROW_PatHandDominance As Integer = 42
    Public ROW_PatLocation As Integer = 43
    Public ROW_sData As Integer = 44

    '  Public gloDBFields1 As String = "|||||||||sCity|sState|sZIP|sCounty|sPhone|sMobile|sEmail|sFAX|sOccupation|sEmploymentStatus|sPlaceofEmployment|sWorkAddressLine1|sWorkAddressLine2|sWorkCity|sWorkState|sWorkZIP|sWorkPhone|sWorkFAX|sChiefComplaints|nProviderID|nPCPId|sGuarantor|nPharmacyID|sSpouseName|sSpousePhone|sRace|sPatientStatus|iPhoto|dtRegistrationDate|dtInjuryDate|dtSurgeryDate|sHandDominance|"
    'Public gloDBFields As String = ""

    Public disPatCode As String = "Code"
    Public disPatFName As String = "First Name"
    Public disPatMName As String = "Middle Name"
    Public disPatLName As String = "Last Name"
    Public disPatSSN As String = "SSN"
    Public disPatDOB As String = "Date Of Birth"
    Public disPatGender As String = "Gender"
    Public disPatMS As String = "Marital Status"
    Public disPatAdd1 As String = "Address 1"
    Public disPatAdd2 As String = "Address 2"
    Public disPatCity As String = "City"
    Public disPatState As String = "State"
    Public disPatZip As String = "Zip Code"
    Public disPatCountry As String = "Country"
    Public disPatPhone As String = "Phone"
    Public disPatMobile As String = "Mobile"
    Public disPatEmail As String = "Email"
    Public disPatFax As String = "Fax Number"
    Public disPatOccupation As String = "Occupation"
    Public disPatEmpStatus As String = "Employment Status"
    Public disPatPlace As String = "Place of Employment"
    Public disPatWrkAdd1 As String = "Work Address 1"
    Public disPatWrkAdd2 As String = "Work Address 2"
    Public disPatWrkCity As String = "Work City"
    Public disPatWrkState As String = "Work State"
    Public disPatWrkZip As String = "Work Zip code"
    Public disPatWrkPhone As String = "Work Phone"
    Public disPatWrkFax As String = "Work Fax Number"
    Public disPatComplaints As String = "Chief Complaints"
    Public disPatProID As String = "Provider ID"
    Public disPatPCPID As String = "PCP ID"
    Public disPatGuarantor As String = "Guarantor"
    Public disPatPharmacyID As String = "Pharmacy ID"
    Public disPatSpouseName As String = "Spouse Name"
    Public disPatSpousePhone As String = "Spouse Phone"
    Public disPatRace As String = "Race"
    Public disPatPatientStatus As String = "Patient Status"
    Public disPatPhoto As String = "Photo"
    Public disPatRegistrationDate As String = "Registration Date"
    Public disPatInjuryDate As String = "Injury Date"
    Public disPatSurgeryDate As String = "Surgery Date"
    Public disPatHandDominance As String = "Hand Dominance"
    Public disPatLocation As String = "Location"
    Public dissData As String = "sData"



    Public dbFldPatCode As String = "sPatientCode"
    Public dbFldFName As String = "sFirstName"
    Public dbFldMName As String = "sMiddleName"
    Public dbFldLName As String = "sLastName"
    Public dbFldSSN As String = "nSSN"
    Public dbFldDOB As String = "dtDOB"
    Public dbFldGender As String = "sGender"
    Public dbFldMS As String = "sMaritalStatus"
    Public dbFldAdd1 As String = "sAddressLine1"
    Public dbFldAdd2 As String = "sAddressLine2"
    Public dbFldCity As String = "sCity"
    Public dbFldState As String = "sState"
    Public dbFldZip As String = "sZIP"
    Public dbFldCountry As String = "sCounty"
    Public dbFldPhone As String = "sPhone"
    Public dbFldMobile As String = "sMobile"
    Public dbFldEmail As String = "sEmail"
    Public dbFldFax As String = "sFAX"
    Public dbFldOccupation As String = "sOccupation"
    Public dbFldEmpStatus As String = "sEmploymentStatus"
    Public dbFldPlace As String = "sPlaceofEmployment"
    Public dbFldWrkAdd1 As String = "sWorkAddressLine1"
    Public dbFldWrkAdd2 As String = "sWorkAddressLine2"
    Public dbFldWrkCity As String = "sWorkCity"
    Public dbFldWrkState As String = "sWorkState"
    Public dbFldWrkZip As String = "sWorkZIP"
    Public dbFldWrkPhone As String = "sWorkPhone"
    Public dbFldWrkFax As String = "sWorkFAX"
    Public dbFldComplaints As String = "sChiefComplaints"
    Public dbFldProID As String = "nProviderID"
    Public dbFldPCPID As String = "nPCPId"
    Public dbFldGuarantor As String = "sGuarantor"
    Public dbFldPharmacyID As String = "nPharmacyID"
    Public dbFldSpouseName As String = "sSpouseName"
    Public dbFldSpousePhone As String = "sSpousePhone"
    Public dbFldRace As String = "sRace"
    Public dbFldPatientStatus As String = "sPatientStatus"
    Public dbFldPhoto As String = "iPhoto"
    Public dbFldRegistrationDate As String = "dtRegistrationDate"
    Public dbFldInjuryDate As String = "dtInjuryDate"
    Public dbFldSurgeryDate As String = "dtSurgeryDate"
    Public dbFldHandDominance As String = "sHandDominance"
    Public dbFldLocation As String = "sLocation"

    Public gloDBFields As String = disPatCode & "|" & disPatFName & "|" & disPatMName & "|" & disPatLName & "|" & disPatSSN & "|" & disPatDOB & "|" & disPatGender & "|" & disPatMS & "|" & disPatAdd1 & "|" & disPatAdd2 & "|" & disPatCity _
& "|" & disPatState & "|" & disPatZip & "|" & disPatCountry & "|" & disPatPhone & "|" & disPatMobile & "|" & disPatFax & "|" & disPatEmail & "|" & disPatOccupation & "|" & disPatEmpStatus & "|" & disPatPlace & "|" & disPatWrkAdd1 & "|" & disPatWrkAdd2 _
& "|" & disPatWrkCity & "|" & disPatWrkState & "|" & "|" & disPatWrkPhone & "|" & disPatWrkFax & "|" & disPatWrkZip & "|" & disPatComplaints & "|" & disPatProID & "|" & disPatPCPID & "|" & disPatGuarantor & "|" & disPatPharmacyID & "|" & disPatSpouseName & "|" & disPatSpousePhone _
& "|" & disPatRace & "|" & disPatPatientStatus & "|" & disPatPhoto & "|" & disPatRegistrationDate & "|" & disPatInjuryDate & "|" & disPatSurgeryDate & "|" & disPatHandDominance & "|" & disPatLocation


    Public Function AssignMapColumn(ByVal TemplateName As String) As Boolean
        'Find Template ID
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim TemplateID As Integer
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@TemplateName", TemplateName, ParameterDirection.Input, SqlDbType.VarChar)
        oDB.DBParameters.Add("@TemplateType", gloStream.gloDataCather.Supporting.TemplateType.PatientRegistration, ParameterDirection.Input, SqlDbType.Int)
        TemplateID = Val(oDB.ExecuteScaler("gsp_DC_ScanTemplateID"))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
            If TemplateID > 0 Then
                ROW_PatCode = GetColIdForMapping(disPatCode, TemplateID)
                ROW_PatFName = GetColIdForMapping(disPatFName, TemplateID)
                ROW_PatMName = GetColIdForMapping(disPatMName, TemplateID)
                ROW_PatLName = GetColIdForMapping(disPatLName, TemplateID)
                ROW_PatSSN = GetColIdForMapping(disPatSSN, TemplateID)
                ROW_PatDOB = GetColIdForMapping(disPatDOB, TemplateID)
                ROW_PatGender = GetColIdForMapping(disPatGender, TemplateID)
                ROW_PatMS = GetColIdForMapping(disPatMS, TemplateID)
                ROW_PatAdd1 = GetColIdForMapping(disPatAdd1, TemplateID)
                ROW_PatAdd2 = GetColIdForMapping(disPatAdd2, TemplateID)
                ROW_PatCity = GetColIdForMapping(disPatCity, TemplateID)
                ROW_PatState = GetColIdForMapping(disPatState, TemplateID)
                ROW_PatZip = GetColIdForMapping(disPatZip, TemplateID)
                ROW_PatCountry = GetColIdForMapping(disPatCountry, TemplateID)
                ROW_PatPhone = GetColIdForMapping(disPatPhone, TemplateID)
                ROW_PatMobile = GetColIdForMapping(disPatMobile, TemplateID)
                ROW_PatEmail = GetColIdForMapping(disPatEmail, TemplateID)
                ROW_PatFax = GetColIdForMapping(disPatFax, TemplateID)
                ROW_PatOccupation = GetColIdForMapping(disPatOccupation, TemplateID)
                ROW_PatEmpStatus = GetColIdForMapping(disPatEmpStatus, TemplateID)
                ROW_PatPlace = GetColIdForMapping(disPatPlace, TemplateID)
                ROW_PatWrkAdd1 = GetColIdForMapping(disPatWrkAdd1, TemplateID)
                ROW_PatWrkAdd2 = GetColIdForMapping(disPatWrkAdd2, TemplateID)
                ROW_PatWrkCity = GetColIdForMapping(disPatWrkCity, TemplateID)
                ROW_PatWrkState = GetColIdForMapping(disPatWrkState, TemplateID)
                ROW_PatWrkZip = GetColIdForMapping(disPatWrkZip, TemplateID)
                ROW_PatWrkPhone = GetColIdForMapping(disPatWrkPhone, TemplateID)
                ROW_PatWrkFax = GetColIdForMapping(disPatWrkFax, TemplateID)
                ROW_PatComplaints = GetColIdForMapping(disPatComplaints, TemplateID)
                ROW_PatProID = GetColIdForMapping(disPatProID, TemplateID)
                ROW_PatPCPID = GetColIdForMapping(disPatPCPID, TemplateID)
                ROW_PatGuarantor = GetColIdForMapping(disPatGuarantor, TemplateID)
                ROW_PatPharmacyID = GetColIdForMapping(disPatPharmacyID, TemplateID)
                ROW_PatSpouseName = GetColIdForMapping(disPatSpouseName, TemplateID)
                ROW_PatSpousePhone = GetColIdForMapping(disPatSpousePhone, TemplateID)
                ROW_PatRace = GetColIdForMapping(disPatRace, TemplateID)
                ROW_PatPatientStatus = GetColIdForMapping(disPatPatientStatus, TemplateID)
                ROW_PatPhoto = GetColIdForMapping(disPatPhoto, TemplateID)
                ROW_PatRegistrationDate = GetColIdForMapping(disPatRegistrationDate, TemplateID)
                ROW_PatInjuryDate = GetColIdForMapping(disPatInjuryDate, TemplateID)
                ROW_PatSurgeryDate = GetColIdForMapping(disPatSurgeryDate, TemplateID)
                ROW_PatHandDominance = GetColIdForMapping(disPatHandDominance, TemplateID)
                ROW_PatLocation = GetColIdForMapping(disPatLocation, TemplateID)
                'ROW_sData = GetColIdForMapping(dissData, TemplateID)
                Return True
            Else
                Return False
            End If

    End Function

    Private Function GetColIdForMapping(ByVal FieldName As String, ByVal TemplateID As String) As Integer
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim nColNo As Integer
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@FieldName", FieldName, ParameterDirection.Input, SqlDbType.VarChar)
        oDB.DBParameters.Add("@TemplateID", TemplateID, ParameterDirection.Input, SqlDbType.Int)
        nColNo = Val(oDB.ExecuteScaler("gsp_DC_ScanMapID"))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return nColNo
    End Function

    Public Function GetVerificationName(ByVal MapID As Integer, ByVal TemplateID As Integer) As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As String
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@MapID", MapID, ParameterDirection.Input, SqlDbType.Int)
        oDB.DBParameters.Add("@TemplateID", TemplateID, ParameterDirection.Input, SqlDbType.Int)
        sVerName = Trim(oDB.ExecuteScaler("gsp_DC_ScanVerificationName"))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function

    Public Function GetDBFieldName(ByVal MapID As Integer, ByVal TemplateID As Integer) As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As String
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@MapID", MapID, ParameterDirection.Input, SqlDbType.Int)
        oDB.DBParameters.Add("@TemplateID", TemplateID, ParameterDirection.Input, SqlDbType.Int)
        sVerName = Trim(oDB.ExecuteScaler("gsp_DC_ScanDBFieldName"))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function

    Public Function GetProviderID(ByVal FullName As String) As Int64
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim ProID As Int64
        'Dim sFName As String
        'Dim sMName As String
        'Dim sLName As String
        'Dim arr() As String

        oDB.Connect(GetConnectionString)

        oDB.DBParameters.Add("@FullName", FullName, ParameterDirection.Input, SqlDbType.Text)

        ProID = Trim(oDB.ExecuteScaler("gsp_DC_ScanProID"))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return ProID
    End Function

    Public Function GetProviderFromLoginID(ByVal LoginID As Int64) As Int64
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim ProID As Int64
        'Dim sFName As String
        'Dim sMName As String
        'Dim sLName As String
        'Dim arr() As String

        oDB.Connect(GetConnectionString)
        'ProID = Trim(oDB.ExecuteScaler)
        ProID = oDB.ExecuteQueryScaler("SELECT  nProviderID from user_mst where nUserID = " & LoginID & "")
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return ProID
    End Function

    Public Function GetLineCount() As Integer
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As Integer
        oDB.Connect(GetConnectionString)
        sVerName = Val(Trim(oDB.ExecuteScaler("gsp_DC_ScanLineCount")))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function

    Public Function GetTempID(ByVal Template As String, ByVal TempType As String) As Integer
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As Int16

        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@TemplateName", Template, ParameterDirection.Input, SqlDbType.Text)
        oDB.DBParameters.Add("@TemplateType", TempType, ParameterDirection.Input, SqlDbType.Int)
        sVerName = Val(Trim(oDB.ExecuteScaler("gsp_DC_ScanTemplateID")))
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function
    Public Function DeleteTemp(ByVal SetTemplate As String, ByVal TemplateType As Int16) As Boolean
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As Boolean
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@TemplateName", SetTemplate, ParameterDirection.Input, SqlDbType.Text)
        oDB.DBParameters.Add("@TemplateType", TemplateType, ParameterDirection.Input, SqlDbType.Int)
        sVerName = oDB.ExecuteNonQuery("gsp_DC_DeleteTemp")
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function

    Public Function CheckMap(ByVal TempID As Int16) As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim sVerName As String
        oDB.Connect(GetConnectionString)
        oDB.DBParameters.Add("@TempID", TempID, ParameterDirection.Input, SqlDbType.Int)
        sVerName = oDB.ExecuteScaler("gsp_DC_ScanMapping")
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
        Return sVerName
    End Function


    Public Function GetGender(ByVal Gender As String) As String
        Dim sGenName As String = ""
        Select Case Gender
            Case "M"
                sGenName = "Male"
            Case "F"
                sGenName = "Female"
            Case "O"
                sGenName = "Other"
        End Select
        Return sGenName
    End Function

    'Public Function GetPrefixTransactionID(ByVal PatientDOB As DateTime) As Long

    '    Dim strID As String
    '    Dim dtDate As DateTime
    '    dtDate = System.DateTime.Now
    '    strID = DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date) & DateDiff(DateInterval.Second, dtDate.Date, dtDate) & DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date)
    '    Return CLng(strID)
    'End Function

    'Function setGridCommbo
    Public Sub setGridCombo(ByVal SearchText As String, ByVal cmbList As String, ByVal flxGrid As C1FlexGrid, Optional ByVal defaultValue As String = "")
        Try
            Dim row As Integer
            With flxGrid
                For row = 1 To .Rows.Count - 1
                    If UCase(Trim(SearchText)) = UCase(Trim(.GetData(row, COL_VER_DISPFIELD))) Then
                        Dim cs As CellStyle '= .Styles.Add("MyList")
                        Try
                            If (.Styles.Contains("MyList")) Then
                                cs = .Styles("MyList")
                            Else
                                cs = .Styles.Add("MyList")
                              
                            End If
                        Catch ex As Exception
                            cs = .Styles.Add("MyList")
                           
                        End Try
                        cs.ComboList = Trim(cmbList)

                        Dim rg As CellRange = .GetCellRange(row, COL_VER_DATA)
                        rg.Style = .Styles("MyList")
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Function getList() As String
        Try
            Dim strSQL As String
            Dim FullName As String
            Dim FullNameList As String = ""

            Dim odb As New gloStream.gloDataBase.gloDataBase
            odb.Connect(GetConnectionString)
            Dim DataReader As SqlClient.SqlDataReader

            strSQL = "Select sFirstName,sMiddleName,sLastName from Provider_Mst"
            DataReader = odb.ReadQueryRecords(strSQL)

            If IsNothing(DataReader) = False Then
                While DataReader.Read

                    FullName = Trim(DataReader.Item(0)) & " " & Trim(DataReader.Item(1)) & " " & Trim(DataReader.Item(2))
                    FullNameList = FullNameList & "|" & Trim(FullName)
                End While
            End If
            odb.Disconnect()
            odb.Dispose()
            odb = Nothing
            Return Right(Trim(FullNameList), Len(Trim(FullNameList)) - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function

    Public Function isExist(ByVal cmbString As String, ByVal searchValue As String) As Boolean
        Try
            Dim i As Integer
            Dim arr() As String
            arr = Split(Trim(cmbString), "|")

            For i = 0 To UBound(arr)
                If UCase(Trim(searchValue)) = UCase(Trim(arr(i))) Then
                    Return True
                    Exit Function
                End If
            Next
            Return False
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function getGridValue(ByVal SearchText As String, ByVal flxGrid As C1FlexGrid) As String
        Try
            Dim row As Integer
            With flxGrid
                For row = 1 To .Rows.Count - 1
                    If UCase(Trim(SearchText)) = UCase(Trim(.GetData(row, COL_VER_DISPFIELD))) Then
                        Return .GetData(row, COL_VER_DATA) & ""
                    End If
                Next
                Return ""
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function


    Public Function isProviderIDExist(ByVal cmbString As String, ByVal searchValue As String) As Boolean
        Try
            Dim i As Integer
            Dim arr() As String
            arr = Split(Trim(cmbString), "|")

            For i = 0 To UBound(arr)
                If UCase(Trim(searchValue)) = UCase(Trim(arr(i))) Then
                    Return True
                    Exit Function
                End If
            Next
            Return False
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function


End Module
