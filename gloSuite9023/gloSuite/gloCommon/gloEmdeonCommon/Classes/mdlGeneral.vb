Imports System.IO
Imports System.Windows.Forms
Imports gloEMRReports
Imports System.Data.SqlClient
Imports gloUserControlLibrary
Imports gloEmdeonCommon
Imports gloGlobal
Public Module mdlGeneral
    'Variables from mdlgenreal
    Public Patientage As New gloUserControlLibrary.AgeDetail
    Public gstrgloEMRStartupPath As String
    Public gstrMessageBoxCaption As String
    Public gnClinicID As Int64 = 1  ''Bug : 00000875: Some Liquid Links not working from Orders and Results
    Public _GetConnectionString As String = String.Empty
    Public gstrSQLError As String
    ''Public multipleRecipients As Boolean
    'Public gnPatientID As Long
    Public gstrgloTempFolder As String
    Public gstrFAXOutputDirectory As String
    Public gnLoginProviderID As Int64
    Public gstrLoginName As String
    Public gstrEFaxUserID As String
    Public gstrEFaxUserPassword As String
    Public gblnFAXCoverPage As Boolean = False
    'Public multipleRecipients As Boolean
    Public gblnInternetFax As Boolean
    Public gblnPageNo As Boolean
    Public gblnUseDefaultPrinter As String
    Public bsuccess As Boolean
    Public gstrFAXPrinterName As String
    Public gblnWordColorHighlight As Boolean
    Public gnClientMachineID As Long
    Public pBlackIceDEVMODE As Integer 'pointer to the devmode
    Public gblnWordBackColor As Int32 = 7
    Public BITS_8 As Short = 2
    Public DITHER_SHARP As Short = 4
    Public gblShowAgeInDays As Boolean
    Public gblAgeLimit As Int64
    Public gblnFAXPrinterSettingsSet As Boolean
    '    Public AxBlackIceDEVMODE1 As AxBLACKICEDEVMODELib.AxBlackIceDEVMODE
    'Madan added this variable for Diagnosis & treatments.

    '20-Feb-2014 Ashish for Merge Orders
    Private bIsMergeOrderEnabled As Boolean = False

    Public gblnSetCPTtoAllICD9 As Boolean
    Public gblnShow8ICD9 As Boolean = True
    Public gblnShow4Modifier As Boolean = True
    Public gblnIsExamPTBillingEnabled As Boolean = False

    '' by Abhijeet On Date 20100419
    '' added variable for default printer selection setting
    Public gblnIsDefaultPrinter As Boolean = False


    '' End of changes by Abhijeet on date 20100419

    '' by Abhijeet On Date 20100614
    '' added variable for HL7 outbound message for Lab 
    'Public gblnSendChargesToHL7 As Boolean = False
    '' End of changes by Abhijeet on date 20100614
    Public gblHL7SENDOUTBOUNDGLOEMRonLabModule As Boolean = False
    Public gblnIsSelectRefContact As Boolean = False '' will be used for batch referral fax
    Public gnLoginID As Long
    Public gClinicID As Long = 1


    Public gstrServicesServerName As String
    Public gstrServicesDBName As String
    Public gstrServicesUserID As String
    Public gstrServicesPassWord As String
    Public gbServicesIsSQLAUTHEN As Boolean

    Public gstrSQLServerName As String

    Public gblnSQLAuthentication As Boolean
    Public gstrSQLUserEMR As String
    Public gstrSQLPasswordEMR As String


    Public Property IsMergeOrderEnabled() As Boolean
        Get
            Return bIsMergeOrderEnabled
        End Get
        Set(ByVal value As Boolean)
            bIsMergeOrderEnabled = value
        End Set
    End Property


    Public Function GetPatientInfo(ByVal patientid As Int64) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As New DataTable()

        Try
            oDB.Connect(False)
            'Get the Patient Demographic Details for dashboard.
            oParameters.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_PatientInfo", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetVisitdate(ByVal VisitID As Long) As Date
        Dim con As SqlConnection = Nothing
        Dim Cmd As SqlCommand
        Dim objParam As SqlParameter = Nothing
        Try

            'Call InitialzeCon()
            con = New SqlConnection(GetConnectionString())
            Cmd = New SqlCommand("gsp_GetVisitDate", con)
            objParam = Nothing
            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Cmd.CommandType = CommandType.StoredProcedure

            con.Open()
            Dim VisitDate As Date
            VisitDate = Cmd.ExecuteScalar
            con.Close()

            objParam = Nothing
            Cmd.Dispose()
            Cmd = Nothing

            con.Dispose()
            con = Nothing
            Return VisitDate
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
    '''' TO Get the Provider FullName , Login Name, ProviderID, UserID OF Current User
    Public Function Get_LoginProviderDetails(ByVal LoginName As String) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_GetLoginProviderDetails", con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@LoginName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = LoginName

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            con.Close()
            da.Dispose()

            objParam = Nothing

            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("gloEmdeonCommon -- mdlGeneral -- Get_LoginProviderDetails -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("gloEmdeonCommon -- mdlGeneral -- Get_LoginProviderDetails -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
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
    Public Function GetDoctorName(strLoginName) As String
        Dim dt As DataTable
        dt = Get_LoginProviderDetails(strLoginName)
        GetDoctorName = Nothing
        If IsNothing(dt) = False Then
            Try
                If dt.Rows.Count > 0 Then
                    gnLoginProviderID = dt.Rows(0)("nProviderID")
                    If Trim(dt.Rows(0)("ProviderName")) <> "" Then
                        Return Trim(dt.Rows(0)("ProviderName"))
                    ElseIf Trim(dt.Rows(0)("UserName")) <> "" Then
                        Return Trim(dt.Rows(0)("UserName"))
                    Else
                        Return gstrLoginName
                    End If
                Else
                    gnLoginProviderID = 0
                    Return Nothing
                End If

            Catch ex As Exception
            Finally
                dt.Dispose()
                dt = Nothing
            End Try

        Else
            gnLoginProviderID = 0
            Return Nothing
        End If

    End Function
    Public Function CreateReport(ByVal OrderID As Long, ByVal arrTests As ArrayList, ByVal PatientID As Long) As Rpt_LabOrder
        'Create the object for report
        Dim oLabs As Rpt_LabOrder = New Rpt_LabOrder()
        'Create the object for dataset i.e dsgloEMRReports.xsd
        Dim dsReports As dsgloEMRReports = New dsgloEMRReports()

        'Commented by Shweta 20091028
        'Added as sub report in main report but not needed as all functionality has done on main form
        'Dim oLabResult As Rpt_LabOrderResult = New Rpt_LabOrderResult()
        'And commenting 20091028

        Dim oConnection As SqlConnection = New SqlConnection
        Dim sqlCmd As SqlCommand = New SqlCommand
        Dim da As SqlDataAdapter = New SqlDataAdapter

        Dim strQuery As String = String.Empty '' Added by Abhijeet on 20100628

        oConnection.ConnectionString = GetConnectionString()
        oConnection.Open()
        sqlCmd.CommandType = CommandType.Text
        sqlCmd.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                             & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"
        sqlCmd.Connection = oConnection
        da = New SqlDataAdapter(sqlCmd)
        da.Fill(dsReports, "dt_Clinic_MST")
        If OrderID <> 0 Then

            Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim _strQryResultDateTime As String = String.Empty
            Dim _strResultDateTime As String = String.Empty
            Dim objResult As New Object()
            Try

                oDBLayer.Connect(False)
                _strQryResultDateTime = " select top 1 dbo.Lab_Order_Test_Result.labotr_TestResultDateTime from dbo.Lab_Order_Test_Result WHERE Lab_Order_Test_Result.labotr_OrderID =  '" & OrderID & "' " & " order by dbo.Lab_Order_Test_Result.labotr_TestResultDateTime  desc "

                objResult = oDBLayer.ExecuteScalar_Query(_strQryResultDateTime)
                If objResult IsNot Nothing AndAlso objResult.ToString() <> "" Then
                    ' _strResultDateTime = Convert.ToString(objResult);
                    _strResultDateTime = Convert.ToDateTime(objResult).ToString("yyyy-MM-dd HH:mm:ss.fff")
                End If
                oDBLayer.Disconnect()
            Catch ex As Exception

                gloAuditTrail.gloAuditTrail.ExceptionLog("" + ex.ToString(), False)
            Finally
                If oDBLayer IsNot Nothing Then
                    oDBLayer.Dispose()
                End If
                If objResult IsNot Nothing Then
                    objResult = Nothing
                End If
                _strQryResultDateTime = String.Empty
            End Try

            'Bug #60579: 00000576 : There is no Order Date printed on the Fax.
            'Multiple fields added in the query as per available in print.
            strQuery = ""
            strQuery = "SELECT Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                                & "Lab_Order_MST.labom_OrderNoPrefix + '-' + CONVERT(varchar(100), " _
                                & "Lab_Order_MST.labom_OrderNoID) AS OrderNumber, " _
                                & " dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTime  AS SpecimenRecievedDate, " _
                                & " dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTime AS ReportDate , " _
                                & " ISNULL(dbo.Lab_Order_MST.labom_ReceivingFacilityCode,'') AS ReceivingFacilityCode, " _
                                & " ISNULL(dbo.Lab_Order_TestDtl.labotd_Comment,'') As TestComments, " _
                                & " ISNULL(dbo.Lab_Order_MST.labom_LabComment,'') AS LabComment, " _
                                & " Lab_Order_MST.labom_CollectionDate AS LabMSTCollectionDate,  " _
                                & " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_LOINCID,'') AS LoinicCode, " _
                                & " ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AlternateResultName,'') AS AlternateResulttName, " _
                                & "CONVERT(varchar(100),ISNULL(dbo.Lab_Order_MST.labom_FileOrderIdentifier,'')) AS ExternalCode, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ProducerIdentifier,'') AS ProducerIdentifier, " _
                                & "Lab_Order_TestDtl.labotd_Instruction AS Instruction, " _
                                & "Lab_Order_TestDtl.labotd_Precaution AS Precaution, " _
                                & "Lab_Order_TestDtl.labotd_DateTime AS TestDate, " _
                                & "CONVERT(varchar(100),Lab_Order_MST.labom_OrderID) AS OrderID, " _
                                & " Lab_Order_TestDtl.labotd_TestID AS TestID, " _
                                & " CONVERT(varchar(100), Lab_Order_MST.labom_PatientID) AS PatientID, " _
                                & "User_MST.sLoginName AS SampledBy, " _
                                & "contacts_mst.sName AS ReferredBy, " _
                                & "ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, " _
                                & "ISNULL(dbo.Lab_Order_Test_Result.labotr_TestResultNumber, 0) AS TestResultNumber, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultLineNo,0) AS ResultLineNo, " _
                                & "case len(ISNUll(dbo.Lab_Order_Test_Result.labotr_TestResultName,'')) when 0 then '-' when null then '-' else dbo.Lab_Order_Test_Result.labotr_TestResultName END AS TestResultName, " _
                                & "dbo.Lab_Order_Test_Result.labotr_TestResultDateTime AS TestResultDateTime, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultName, '') AS ResultName, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultValue, '') AS ResultValue, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultUnit, '') AS ResultUnit, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultRange, '') AS ResultRange, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultType, '') AS ResultType, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag, '') AS AbnormalFlag, " _
                                & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultComment, '') AS ResultComment, " _
                                & "dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTime AS ResultDateTime, " _
                                & "dbo.Lab_Order_TestDtl.labotd_TestName AS TestName, " _
                                & "dbo.Lab_Order_TestDtl.labotd_SpecimenName AS Speciman, " _
                                & "ISNULL(Lab_Collection_Mst.labcm_Name, '') AS CollectionContainer, " _
                                & "dbo.Lab_Order_TestDtl.labotd_StorageName AS StorageTemperature, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_TransactionDateUTC),'') AS labom_TransactionDateUTC," _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_DateTimeUTC),'') AS labotd_DateTimeUTC," _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenTypeText,'') AS labotd_SpecimenTypeText, " _
                                & "Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTime AS labotd_SpecimenCollectionStartDateTime, " _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenRejectReason,'') AS labotd_SpecimenRejectReason, " _
                                & "ISNULL(Lab_Order_TestDtl.labotd_SpecimenCondition,'') AS labotd_SpecimenCondition, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_TestDtl.labotd_SpecimenCollectionStartDateTimeUTC),'') AS labotd_SpecimenCollectionStartDateTimeUTC, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityName,'') AS labotrd_LabFacilityName, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityStreetAddress,'') AS labotrd_LabFacilityStreetAddress, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCity,'') AS labotrd_LabFacilityCity, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityState,'') AS labotrd_LabFacilityState, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityZipCode,'') AS labotrd_LabFacilityZipCode, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountry,'') AS labotrd_LabFacilityCountry, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_LabFacilityCountyOrParishCode ,'') AS labotrd_LabFacilityCountyOrParishCode, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(Lab_Order_MST.labom_CollectionDateUTC),'') AS labom_CollectionDateUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_ResultTransferDateTimeUTC),'') AS labotr_ResultTransferDateTimeUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTimeUTC),'') AS ResultDateTimeUTC, " _
                                & "dbo.Lab_Order_MST.labom_OrderDate AS OrderDate, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_MST.labom_OrderDateUTC),'') AS OrderDateUTC, " _
                                & "ISNULL(dbo.Lab_GetUTCTimeString(dbo.Lab_Order_Test_Result.labotr_SpecimenReceivedDateTimeUTC),'') AS SpecimenReceivedDateTimeUTC, " _
                                & "ISNULL(Lab_Order_Test_ResultDtl.labotrd_ResultParentChildFlag,0) AS ParentChildResultFlag " _
                                & "FROM Lab_Order_MST INNER JOIN " _
                                & "Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID LEFT OUTER JOIN " _
                                & "Lab_Order_Test_Result ON Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_Result.labotr_TestID AND " _
                                & " Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN " _
                                & "Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND " _
                                & "Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND " _
                                & " Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber LEFT OUTER JOIN " _
                                & "User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                                & "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                                & "contacts_mst ON Lab_Order_MST.labom_ReferredByID = contacts_mst.nContactID LEFT OUTER JOIN " _
                                & "Lab_Specimen_Mst ON Lab_Order_TestDtl.labotd_SpecimenID = Lab_Specimen_Mst.labsm_ID LEFT OUTER JOIN " _
                                & "Lab_Collection_Mst ON Lab_Order_TestDtl.labotd_CollectionID = Lab_Collection_Mst.labcm_ID " _
                                & "WHERE Lab_Order_MST.labom_OrderID = " & OrderID

            If _strResultDateTime.Trim() <> "" Then
                strQuery += " and Lab_Order_Test_Result.labotr_TestResultDateTime ='" & _strResultDateTime & "' "
            End If
            strQuery += " order by Lab_Order_TestDtl.labotd_LineNo"
            sqlCmd.CommandText = strQuery

            sqlCmd.Connection = oConnection
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(dsReports, "dt_LabOrderMainReport")

            strQuery = ""
            strQuery = " SELECT Patient.sPatientCode AS PatientCode,ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'')+ SPACE(1) + ISNULL(Patient.sLastName,'') AS PatientName,ISNULL(Patient.sGender,'') as Gender," _
                                 & " ISNULL(Patient.SAddressLine1,'') + SPACE(1)+ ISNULL(Patient.sAddressLine2,'') AS PatientAddress,ISNULL(Patient.sPhone,'') AS PatientPhone," _
                                 & " ISNULL(Patient.sCity,'') AS PatientCity,ISNULL(Patient.sState,'') AS PatientState,ISNULL(Patient.sZIP, '') AS PatientZip,ISNULL(Patient.sCounty,'') AS PatientCounty," _
                                 & " Patient.dtDOB AS DateOfBirth, Lab_Order_MST.labom_PatientAgeYear AS AgeInYrs, " _
                                 & " Lab_Order_MST.labom_PatientAgeMonth AS AgeInMnths, Lab_Order_MST.labom_PatientAgeDay AS AgeInDays, ISNULL(Provider_MST.sFirstName, '') " _
                                 & " + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, ISNULL(User_MST.sLoginName, '') " _
                                 & " AS SampledBy, ISNULL(Contacts_MST.sFirstName, '') + ' ' + ISNULL(Contacts_MST.sMiddleName, '') + ' ' + ISNULL(Contacts_MST.sLastName, '') " _
                                 & " AS ReferredBy, CONVERT(varchar(100), Lab_Order_MST.labom_OrderID) AS OrderID, Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                                 & " Lab_Order_MST.labom_OrderNoPrefix + ' ' + CONVERT(varchar(100), Lab_Order_MST.labom_OrderNoID) AS OrderNumber" _
                                 & " FROM Lab_Order_MST INNER JOIN " _
                                 & " Patient ON Lab_Order_MST.labom_PatientID = Patient.nPatientID LEFT OUTER JOIN " _
                                 & " User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                                 & " Lab_ContactInfo ON Lab_Order_MST.labom_PreferredLabID = Lab_ContactInfo.labci_Id LEFT OUTER JOIN " _
                                 & " Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                                 & " Contacts_MST ON Lab_Order_MST.labom_ReferredByID = Contacts_MST.nContactID " _
                                 & " WHERE Patient.nPatientID = '" & PAtientID & "' And Lab_Order_MST.labom_OrderID=" & OrderID
            sqlCmd.Connection = oConnection
            sqlCmd.CommandText = strQuery
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(dsReports, "dt_PatientInfo")

            'Resolved By : Mitesh Patel 
            'Bug Id: 32738 
            strQuery = ""
            strQuery = " select  patientInsurance_dtl.nPatientID as Ins_patientid,patientInsurance_dtl.nInsuranceID as nInsuarnceID, " _
                              & " patientInsurance_dtl.nInsuranceID as Ins_id, " _
                              & " IsNull(patientInsurance_dtl.sSubscriberPolicy#,'') as Ins_subscriberPolicyNo, " _
                              & " IsNull(patientInsurance_dtl.sSubscriberID,'') as ins_SubscriberID, " _
                              & " IsNull(patientInsurance_dtl.sGroup,'') as Ins_group, " _
                              & " IsNull(patientInsurance_dtl.sEmployer,'') as Ins_employer, " _
                              & " IsNull(patientInsurance_dtl.dtDOB,'') as Ins_DOB," _
                              & " dbo.GET_NAME(patientInsurance_dtl.sSubFName,patientInsurance_dtl.sSubMName,patientInsurance_dtl.sSubLName) as INs_Subscribername," _
                              & " Isnull(patientInsurance_dtl.bPrimaryFlag,'') as ins_Primaryflag," _
                              & " Isnull(patientInsurance_dtl.sInsurancePhone,'') as ins_insurancephone," _
                              & " Isnull(patientInsurance_dtl.sInsuranceName,'') as InsuranceName " _
                              & " from patientInsurance_dtl where patientInsurance_dtl.nInsuranceFlag in (1,2,3) " _
                              & " and patientInsurance_dtl.nPatientID='" & PatientID & "'"
            sqlCmd.Connection = oConnection
            sqlCmd.CommandText = strQuery
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(dsReports, "dt_PatientInsDtl")



            strQuery = ""
            strQuery = "select isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') + '-' + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as description, " _
                                 & " convert(varchar(1), dbo.lab_order_testdtl_diagcpt.labodtl_type) + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') " _
                                 & "+ isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as diagcpttype, dbo.lab_order_testdtl_diagcpt.labodtl_type as type," _
                                 & " convert(varchar(100), dbo.lab_order_mst.labom_orderid) as orderid,  dbo.lab_order_testdtl.labotd_testid as testid, " _
                                 & "dbo.lab_order_mst.labom_patientid as patientid, dbo.lab_order_testdtl_diagcpt.labodtl_testname as testname" _
                                 & " from  dbo.lab_order_mst inner join " _
                                 & "dbo.lab_order_testdtl on dbo.lab_order_mst.labom_orderid = dbo.lab_order_testdtl.labotd_orderid inner join " _
                                 & "dbo.lab_order_testdtl_diagcpt on dbo.lab_order_testdtl.labotd_orderid = dbo.lab_order_testdtl_diagcpt.labodtl_orderid and " _
                                 & "dbo.lab_order_testdtl.labotd_testname = dbo.lab_order_testdtl_diagcpt.labodtl_testname " _
                                 & " where lab_order_mst.labom_orderid= '" & OrderID & "'"
            sqlCmd.Connection = oConnection
            sqlCmd.CommandText = strQuery
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(dsReports, "dt_LabOrderReportCPTICD9")
            '' End of changes by Abhijeet on 20100628 fpr showing only latest result set.
            da.Dispose()
            oLabs.SetDataSource(dsReports)

        End If
        If sqlCmd IsNot Nothing Then
            sqlCmd.Parameters.Clear()
            sqlCmd.Dispose()
            sqlCmd = Nothing
        End If
        If Not IsNothing(oConnection) Then  ''connection close changes done
            If (oConnection.State = ConnectionState.Open) Then
                oConnection.Close()
            End If
        End If
        Return oLabs
    End Function
    'End code Adding 20091028


    'Changed by Shweta 20091028
    'against PER with Case No :GLO2008-0001765

    'End Code Adding 20091028

    Private Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        strTIFFFileName = gnClientMachineID & "-" & Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
        Return strTIFFFileName
    End Function

    Public Function RetrieveFAXDetails(ByVal enmFAXDocumentType As enmFAXType, ByVal nPatientID As Long, ByVal sFAXTo As String, ByVal sFAXNo As String, ByVal sFAXTypeDetails As String, Optional ByVal nReferralID As Long = 0, Optional ByVal nVisitID As Long = 0, Optional ByVal nEXAMID As Long = 0, Optional ByVal blnFirstReferralLetter As Boolean = True) As Boolean
        'Check this is the First Referral or not
        'If gblnFAXCoverPage AndAlso enmFAXDocumentType = enmFAXType.ReferralLetter AndAlso blnFirstReferralLetter = False Then
        '    gstrFAXContactPerson = sFAXTo
        '    Dim objReferralNo As New clsFAX
        '    gstrFAXContactPersonFAXNo = objReferralNo.GetContactFAXNo(nReferralID)
        '    objReferralNo = Nothing
        '    If Trim(gstrFAXContactPersonFAXNo) = "" Then
        '        gstrFAXContactPersonFAXNo = InputBox(sFAXTo & " FAX No. has not been set." & vbCrLf & "Please enter the FAX No", gstrMessageBoxCaption)
        '    End If
        '    If Trim(gstrFAXContactPersonFAXNo) = "" Then
        '        MessageBox.Show("You have not entered the FAX No of " & sFAXTo & ". So FAX will not be send to " & sFAXTo, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return False
        '    End If
        '    Return True
        'End If
        UpdateLog("In the RetrieveFAXDetails method")

        'sarika 13th nov 07   -- one fax to multiple recipients
        multipleRecipients = False

        ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
        ''Change gstrFAXContacts by gstrfaxCollection
        gstrfaxCollection = Nothing

        gblnFAXPrinterSettingsSet = isPrinterSettingsSet(True)

        'Check Printer settings are set or not
        If gblnFAXPrinterSettingsSet = False Then
            'All necessary FAX Settings are not set
            UpdateLog("Setting FAX Printer settings")
            'isPrinterSettingsSet(True)
            UpdateLog("FAX Printer settings set")
            Return False
        End If
        'Check FAX Cover Page is enabled or not
        'If gblnFAXCoverPage Then
        '    UpdateLog("Cover Page FAX Setting is enabled")
        '    'Cover Page FAX Setting is enabled
        '    'Check the 'Patient Exam' Cover Page Template exists or not
        '    Dim blnTemplateAvailable As Boolean = False
        '    'Before, User has to design the different cover page for different FAX types
        '    'Now cover will be same for all types by the Name = 'Cover Page'
        '    UpdateLog("Check Template is exists or not")
        '    Dim objTemplate As New clsPatientExams
        '    blnTemplateAvailable = objTemplate.CheckFAXCoverPageTemplateExists()
        '    objTemplate = Nothing
        '    If blnTemplateAvailable = False Then
        '        'Patient Exam Cover Page does not exists
        '        MessageBox.Show("FAX Cover Page template by Name 'Cover Page' does not exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Return False
        '    End If
        '    UpdateLog("Template exists")
        'End If
        'FAX Type - Patient Exam with Exam Name
        UpdateLog("Retrieveing FAX Document Type")
        Select Case enmFAXDocumentType
            Case enmFAXType.FormGallery
                gstrFAXType = "Form Gallery"
            Case enmFAXType.PatientExam
                gstrFAXType = "Patient Exam"
            Case enmFAXType.PatientLetters
                gstrFAXType = "Patient Letter"
            Case enmFAXType.PatientMessages
                gstrFAXType = "Patient Message"
            Case enmFAXType.PatientOrders
                gstrFAXType = "Patient Order"
            Case enmFAXType.Prescription
                gstrFAXType = "Prescription"
            Case enmFAXType.PTProtocols
                gstrFAXType = "PT Protocol"
            Case enmFAXType.ReferralLetter
                gstrFAXType = "Referral Letter"
            Case enmFAXType.PatientMaterials
                gstrFAXType = "Patient Materials"
            Case enmFAXType.Labs
                gstrFAXType = "Labs"
                'sarika bug 877 30 july 08
            Case enmFAXType.PatientConsent
                gstrFAXType = "Patient Consent"
            Case enmFAXType.NurseNotes
                gstrFAXType = "Nurse Notes"
            Case enmFAXType.DisclosureManagement
                gstrFAXType = "Disclosure Management"
        End Select
        UpdateLog("FAX Document Type retrieved")
        'Retrieve the FAX To & FAX No
        Select Case enmFAXDocumentType
            Case enmFAXType.PatientExam, enmFAXType.PatientLetters, enmFAXType.PatientMessages, enmFAXType.PatientOrders, enmFAXType.PTProtocols, enmFAXType.FormGallery, enmFAXType.PatientMaterials, enmFAXType.Labs, enmFAXType.PatientConsent, enmFAXType.NurseNotes, enmFAXType.DisclosureManagement
                gstrFAXContactPerson = ""
                gstrFAXContactPersonFAXNo = ""
                'Dim frm As New frmSelectContactFAX
                UpdateLog("Creating object of frmSelectContactFAXWithFAXCoverPage")
                Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                UpdateLog("Object created")

                'sarika 20090717 Hide Cover Page for Lab Orders
                If enmFAXDocumentType = enmFAXType.Labs Then
                    gblnFAXCoverPage = False
                End If
                '------

                If gblnFAXCoverPage = False Then
                    UpdateLog("No FAX Cover Page")
                    'frm.pnlCoverPage.Visible = False
                    frm.pnlFaxCoverPg.Visible = False
                    frm.Panel4.Visible = False
                    frm.Splitter1.Visible = False
                    frm.btnUp1.Visible = True
                    frm.btnUp1.BackgroundImage = My.Resources.Resources.UP
                    frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                    frm.btnDown1.Visible = False
                    frm.pnlSelectFAXNo.Dock = DockStyle.Fill
                    'frm.pnlFAXButtons.Visible = False
                Else
                    UpdateLog("FAX Cover Page enabled.")
                    frm.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    frm.btnUp1.Visible = True
                    frm.btnUp1.BackgroundImage = My.Resources.Resources.UP
                    frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                    frm.btnDown1.Visible = False
                    UpdateLog("Loading FAX Cover Page")
                    frm.LoadFAXCoverPage()
                    UpdateLog("FAX Cover Page loaded")
                End If

                'code commented by sarika 7th dec 07
                'If IsNothing(_Owner) = False Then
                '    If frm.ShowDialog(_Owner) = DialogResult.OK Then
                '        gstrFAXCoverPage = frm.strFAXCoverPage
                '    Else
                '        Return False
                '    End If
                'Else

                '---
                If frm.ShowDialog(frm.Parent) = DialogResult.OK Then
                    gstrFAXCoverPage = frm.strFAXCoverPage
                Else
                    frm.Dispose()
                    frm = Nothing
                    Return False
                End If
                'End If

                frm.Dispose()
                frm = Nothing
            Case enmFAXType.Prescription
                If gblnFAXCoverPage Then
                    Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                    frm.pnlSelectFAXNo.Visible = False
                    frm.ts_btnShowGrid.Visible = False
                    frm.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    frm.LoadFAXCoverPage()
                    frm.lblContactPerson.Text = sFAXTo
                    frm.txtFAXNo.Text = sFAXNo
                    frm.pnlCoverPage.Dock = DockStyle.Fill
                    frm.btnUpdateFAXNo1.Visible = False
                    Dim objSender As Object = Nothing
                    Dim objE As EventArgs = Nothing
                    frm.btnRefreshCoverPage_Click(objSender, objE)
                    If frm.ShowDialog(frm.Parent) = DialogResult.OK Then
                        gstrFAXCoverPage = frm.strFAXCoverPage
                        ' File.Copy(gstrFAXCoverPage, "C:\gloEMRPrescriptionCoverPage.docx", True)
                    Else
                        frm.Dispose()
                        frm = Nothing
                        Return False
                    End If
                    gstrFAXCoverPage = frm.strFAXCoverPage
                    ' File.Copy(gstrFAXCoverPage, "C:\gloEMRPrescriptionCoverPage.docx", True)
                    frm.Dispose()
                    frm = Nothing


                Else


                End If

            Case enmFAXType.ReferralLetter
                'Find the Referral FAX No
                gstrFAXContactPerson = sFAXTo
                Dim objReferralNo As New clsFAX
                gstrFAXContactPersonFAXNo = objReferralNo.GetContactFAXNo(nReferralID)
                objReferralNo = Nothing
                If gblnFAXCoverPage = False Then
                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        gstrFAXContactPersonFAXNo = InputBox(sFAXTo & " FAX No. has not been set." & vbCrLf & "Please enter the FAX No", gstrMessageBoxCaption)
                    End If
                Else
                    Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
                    frm.btnUp1.Visible = False
                    frm.btnDown1.Visible = False
                    frm.pnlSelectFAXNo.Visible = False
                    frm.ts_btnShowGrid.Visible = False
                    frm.PatientID = nPatientID
                    frm.VisitId = nVisitID
                    frm.ExamId = nEXAMID
                    frm.ReferralId = nReferralID
                    frm.strFAXType = gstrFAXType
                    frm.LoadFAXCoverPage(blnFirstReferralLetter)
                    frm.lblContactPerson.Text = gstrFAXContactPerson
                    frm.txtFAXNo.Text = gstrFAXContactPersonFAXNo
                    'frm.pnlCoverPage.Dock = DockStyle.Fill
                    frm.btnUpdateFAXNo1.Visible = False
                    'sarika 29th nov 07
                    frm.pnlLeft.Visible = False
                    ' frm.Panel2.Visible = False
                    frm.pnlBottom.Dock = DockStyle.Fill
                    frm.Panel2.Dock = DockStyle.Top
                    frm.dsoFAXPreview.Dock = DockStyle.Fill
                    '-------------

                    Dim objSender As Object = Nothing
                    Dim objE As EventArgs = Nothing
                    frm.btnRefreshCoverPage_Click(objSender, objE)
                    If frm.ShowDialog(frm.Parent) = DialogResult.OK Then
                        gstrFAXCoverPage = frm.strFAXCoverPage
                    Else
                        frm.Dispose()
                        frm = Nothing
                        Return False
                    End If
                    frm.Dispose()
                    frm = Nothing
                End If
                'sarika 29th nov 07
                If Trim(gstrFAXContactPersonFAXNo) = "" Then
                    gstrFAXContactPersonFAXNo = InputBox(sFAXTo & " FAX No. has not been set." & vbCrLf & "Please enter the FAX No", gstrMessageBoxCaption)
                End If
                '------
        End Select
        'code commented by sarika 13th nov 07
        ''Select the FAX To & FAX No
        'If Trim(gstrFAXContactPersonFAXNo) = "" Then
        '    MessageBox.Show("FAX No has not been set. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Return False
        'End If
        '----------------------

        'code added by sarika 13th nov 07
        'Select the FAX To & FAX No
        If multipleRecipients = False Then
            If Trim(gstrFAXContactPersonFAXNo) = "" And enmFAXDocumentType <> enmFAXType.Medication Then
                MessageBox.Show("FAX No has not been set. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Else
            ''Bug #57310: 00000547 : UNABLE TO FAX LAB ORDER via CONTACT
            ''Change gstrFAXContacts by gstrfaxCollection
            If Not IsNothing(gstrfaxCollection) Then
                If gstrfaxCollection.Count = 0 And enmFAXDocumentType <> enmFAXType.Medication Then
                    MessageBox.Show("FAX No has not been set. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If
            End If
        End If
        '----------------------

        If Trim(sFAXTypeDetails) <> "" Then
            gstrFAXType = gstrFAXType & " - " & sFAXTypeDetails
        End If
        Return True
    End Function
    'Function GetPrefixTransactionID()

    'End Function
    Function GetPrefixTransactionID(ByVal PatientID As Long) As Long

        Dim PatientDOB As DateTime
        Dim strID As String
        Dim dtDate As DateTime

        'Get Patient Date Of Birth
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        oDB.Connect(GetConnectionString)
        PatientDOB = oDB.ExecuteQueryScaler("SELECT dtDOB FROM Patient WHERE nPatientID = " & PatientID & "")
        oDB.Disconnect()
        oDB = Nothing


        dtDate = System.DateTime.Now
        strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
        Return CLng(strID)

    End Function

    'Function GetVisitdate(ByVal VisitID As Long)

    'End Function
    'Function GetDoctorName()

    'End Function
    Public Function IsInternetConnectionAvailable() As Boolean
        ' Returns True if connection is available 
        ' Replace www.yoursite.com with a site that 
        ' is guaranteed to be online - perhaps your 
        ' corporate site, or microsoft.com 
        Dim objUrl As New System.Uri("http://www.Google.com/")
        ' Setup WebRequest 
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objUrl)
        Dim objResp As System.Net.WebResponse
        Try
            ' Attempt to get response and return True 
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False 
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            objWebReq = Nothing
            Return False
        End Try
    End Function
    Public ReadOnly Property ExamNewFaxFileName(ByVal _path As String, ByVal _extension As String) As String
        Get
            '' Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
            'Dim _NewDocumentName As String = ""
            '' Dim _Extension As String = _extension
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _extension
            ''While File.Exists(_path & "\" & _NewDocumentName) = True
            'While File.Exists(_path & _NewDocumentName) = True
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _extension
            'End While
            '' Return _path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(_path, _extension, "MMddyyyyHHmmssffff")

            'Return _path & _NewDocumentName
        End Get
    End Property
    Public Function SetFAXPrinterDefaultSettings1() As Boolean

        Try

            If gstrFAXPrinterName = "" Then
                '    MessageBox.Show("You must set the Fax printer name for sending Fax", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Dim frm As New frmSettings_New
                '    frm.ShowDialog()
                SetFAXPrinterDefaultSettings1 = Nothing
                Exit Function
            End If

            pBlackIceDEVMODE = Form1.AxBlackIceDEVMODE1.LoadBlackIceDEVMODE(gstrFAXPrinterName)

            If pBlackIceDEVMODE = 0 Then
                MsgBox("Cannot open '" & gstrFAXPrinterName & "' Printer driver", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                '  F_AllControlEnabled((False))
                SetFAXPrinterDefaultSettings1 = Nothing
                Exit Function
            End If

            ' Output directory
            bsuccess = Form1.AxBlackIceDEVMODE1.SetOutputDirectory(gstrFAXOutputDirectory, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetOutputDirectory'")
            End If

            ' File format TIFF group 4

            bsuccess = Form1.AxBlackIceDEVMODE1.SetFileFormat(7, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetFileFormat'")
            End If


            ' Orientation (Potrait/Landscape

            bsuccess = Form1.AxBlackIceDEVMODE1.SetOrientation(1, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetOrientation'")
            End If

            'disable generation of group file
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableGroupFile(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableGroupFile'")
            End If

            ''if group file is generated disable deletion of group file
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableDeleteGroupFile(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableDeleteGroupFile'")
            End If

            'The document will not be forced to be printed always using the printer’s resolution, regardless to the DPI setting stored in the document 
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableForcePrinterDPI(pBlackIceDEVMODE)
            If Not bsuccess Then
                MsgBox("Error in calling Active X function: 'DisableForcePrinterDPI'")
            End If

            bsuccess = Form1.AxBlackIceDEVMODE1.DisableAdvancedPaperSize(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableAdvancedPaperSize'")
            End If

            'vertical and horizontal resolution values.
            bsuccess = Form1.AxBlackIceDEVMODE1.SetXDPI(200, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetXDPI'")
            End If
            bsuccess = Form1.AxBlackIceDEVMODE1.SetYDPI(200, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetYDPI'")
            End If

            ' Color depth
            bsuccess = Form1.AxBlackIceDEVMODE1.SetColorDepth(BITS_8, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                'MsgBox "Error in calling Active X function: 'SetColorDepth'"
            End If

            'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
            bsuccess = Form1.AxBlackIceDEVMODE1.EnablePageNumbering(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'EnablePageNumbering'")
            End If

            '3 indicates Exact filename.
            'i.e., we ourself generate the filename, we are not using any Blackice filename generation method .
            bsuccess = Form1.AxBlackIceDEVMODE1.SetFileGenerationMethod(3, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetFileGenerationMethod'")
            End If

            bsuccess = Form1.AxBlackIceDEVMODE1.DisableFaxOutput(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'EnableFaxOutput'")
            End If

            'If this box is checked, the driver will set the page number tag of every page in the output TIFF file.
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableWriteText(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableWriteText'")
            End If


            'GammaLink compatible TIFF output requires this setting to be checked, because they can only send TIFF images with reverse bit order.
            ' the driver will create a TIFF file that is compatible with the requirements listed in File Format for Internet Fax.
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableInternetTiffFormat(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableInternetTiffformat'")
            End If

            'sarika 11/10/2007
            'if you are not capturing printer messages for end of printing, for example you can disable this in the printer options and it can get you a little more speed.
            bsuccess = Form1.AxBlackIceDEVMODE1.DisableMessagingInterface(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'DisableMessagingInterface'")
            End If

            'The Photo Quality option enables or disables the dithering of the Black Ice driver.

            'code commented by sarika 21st nov 07
            bsuccess = Form1.AxBlackIceDEVMODE1.SetDithering(DITHER_SHARP, pBlackIceDEVMODE)
            '---
            ''code added by sarika 21st nov 07
            ' bsuccess = AxBlackIceDEVMODE1.SetDithering(DITHER_FS4, pBlackIceDEVMODE)
            '---
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'SetDithering'")
            End If

            ''code added by sarika 7th dec 07
            bsuccess = Form1.AxBlackIceDEVMODE1.EnableMultipageImage(pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error in calling Active X function: 'EnableMultipageImage'")
            End If
            '---

            '' ''code added by sarika sarika 7th dec 07
            ''bsuccess = AxBlackIceDEVMODE1.DisableMultipageImage(pBlackIceDEVMODE)
            ''If (bsuccess = False) Then
            ''    MsgBox("Error in calling Active X function: 'EnableMultipageImage'")
            ''End If
            ' ''---

            'to save the settings applied to the black ice printer deriver
            ' The Smooth, Sharp, and Stucki filters produce better quality output.
            bsuccess = Form1.AxBlackIceDEVMODE1.SaveBlackIceDEVMODE(gstrFAXPrinterName, pBlackIceDEVMODE)
            If (bsuccess = False) Then
                MsgBox("Error saving the devmode")
                SetFAXPrinterDefaultSettings1 = Nothing
                Exit Function
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Finally

        End Try


    End Function
    Public Function GetAge(ByVal BirthDate As DateTime) As String
        Dim _BDate As DateTime = BirthDate
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 And BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = Now.Year Then
            months = Now.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + Now.Month
        End If
        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 And Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 And Now.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        'Return years & " years " & months & " months " & days & " days"
        'Following code to display age in Numeric and Text

        'age.Age = years & " Years " & months & " Months " & days & " Days"
        '' Cases

        ''20081119   ''Following Code to Store ExactAge in String
        Dim _AgeStr As String = ""
        If gblShowAgeInDays = True And gblAgeLimit >= DateDiff(DateInterval.Day, CType(_BDate, Date), Date.Now.Date) Then
            If years = 0 Then
                If months = 0 Then
                    '' Commented on 20081125
                    '' Not to show the Age in weeks
                    ''sudhir 20081121 Weeks code
                    '  Dim weeks As Int16 = days \ 7
                    ' days = days Mod 7
                    'If weeks = 0 Then
                    '    If days <= 1 Then
                    '        _AgeStr = days & " Day"
                    '    Else
                    '        _AgeStr = days & " Days"
                    '    End If
                    'ElseIf weeks = 1 Then
                    '    If days = 0 Then
                    '        _AgeStr = weeks & " Week"
                    '    ElseIf days = 1 Then
                    '        _AgeStr = weeks & " Week " & days & " Day"
                    '    Else
                    '        _AgeStr = weeks & " Week " & days & " Days"
                    '    End If
                    'ElseIf weeks > 1 Then
                    '    If days = 0 Then
                    '        _AgeStr = weeks & " Weeks"
                    '    ElseIf days = 1 Then
                    '        _AgeStr = weeks & " Weeks " & days & " Day"
                    '    Else
                    '        _AgeStr = weeks & " Weeks " & days & " Days"
                    '    End If
                    'End If
                    ''end sudhir weeks code

                    If days <= 1 Then
                        _AgeStr = days & " Day"
                    Else
                        _AgeStr = days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Month " & days & " Day"
                    Else
                        _AgeStr = months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Months " & days & " Day"
                    Else
                        _AgeStr = months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Year "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Month "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Months "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Years "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Months " & days & " Days"
                    End If
                End If
            End If
        Else 'ShowAgeInDay is False OR AgeLimit less than Settings.
            If years = 0 Then
                'Added by pravin on 11/25/2008
                '                If months = 0 And months = 1 Then
                If months = 1 Then
                    _AgeStr = months & " Month"
                ElseIf months > 1 Then
                    _AgeStr = months & " Months"
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    _AgeStr = years & " Year "
                ElseIf months = 1 Then
                    _AgeStr = years & " Year " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Year " & months & " Months "
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    _AgeStr = years & " Years "
                ElseIf months = 1 Then
                    _AgeStr = years & " Years " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Years " & months & " Months "
                End If
            End If
            'Added by pravin if age in days  11/25/2008
            If years = 0 And months = 0 Then
                If days <= 1 Then
                    _AgeStr = days & " Day"
                Else
                    _AgeStr = days & " Days"


                End If
            End If
        End If
        Patientage.Age = _AgeStr
        Patientage.Years = years
        Patientage.Months = months
        Patientage.Days = days
        Return _AgeStr
    End Function
    Public Sub UpdateLog(ByVal strLogMessage As String)
        Try

            
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, strLogMessage, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public Sub UpdateLogForFax(ByVal strLogMessage As String)
        Try


            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, strLogMessage, gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    'Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    'Modified by madan on 20100514
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Public Function GetConnectionString()
        'If _GetConnectionString.ToString() = "" Then
        If Not IsNothing(appSettings) Then
            _GetConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
        End If
        'End If
        Return _GetConnectionString
    End Function

    Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        Try
            If isSQLAuthentication = False Then
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
            Else
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
            End If
            ' ConnectionString = strConnectionString
            Return strConnectionString
        Catch ex As Exception
            Return Nothing
        Finally
            strConnectionString = Nothing

        End Try
    End Function

    'Added by madan on 20100514
    Public Function GetMessageBoxCaption()
        If Not IsNothing(appSettings) Then
            gstrMessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
        Else
            gstrMessageBoxCaption = "gloEMR"
        End If
        Return gstrMessageBoxCaption
    End Function

    Public Sub UpdateVoiceLog(ByVal strLogMessage As String)
        Try
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.None, strLogMessage, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Public Enum ControlType
        None = 0
        CheckBox = 1
        Text = 2
    End Enum
    Public ReadOnly Property ExamNewDocumentName() As String
        Get
            '    Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            '    Dim _NewDocumentName As String = ""
            '    Dim _Extension As String = ".docx"
            '    Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            '    Dim i As Integer = 0
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _Extension
            '    While File.Exists(_Path & _NewDocumentName) = True
            '        i = i + 1
            '        _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _Extension
            '    End While
            '    Return _Path & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff")
        End Get
    End Property
    Public Enum CategoryType
        None = 0
        General = 1
        Hitory = 2
        Physical_Examination = 3
        Medical_Decision_Making = 4
        HPI = 5
        Management_option = 6
        Labs = 7
        X_Ray_Radiology = 8
        Other_Diagonsis_Tests = 9
        ROS = 10
        DB_History = 11
    End Enum
    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            '' Was Commented Before 2090602
            '' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
            '' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetPrefixTransactionID(ByVal PatientDOB As DateTime) As Long
        Dim strID As String
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now
        strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
        Return CLng(strID)
    End Function

    Public Function GetPrefixTransactionID() As Long
        Dim strID As String
        Dim strPatientID As String = ""
        Dim strPatientTempID As String
        Dim nPatientID As Long
        nPatientID = nPatientID
        Randomize(strPatientID)
        strPatientID = CStr(nPatientID)
        If strPatientID.Length >= 15 Then
            strPatientTempID = strPatientID.Substring(4, 1) & strPatientID.Substring(9, 1) & strPatientID.Substring(14, 1)
        Else
            Select Case strPatientID.Length
                Case 1
                    strPatientTempID = "00" & strPatientID
                Case 2
                    strPatientTempID = "0" & strPatientID
                Case Else
                    strPatientTempID = Right(strPatientID, 3)
            End Select
        End If
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now

        strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date))
        strID = strID & strPatientTempID.Substring(0, 1)
        strID = strID & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate))
        strID = strID & strPatientTempID.Substring(1, 1)
        strID = strID & dtDate.Millisecond
        strID = strID & strPatientTempID.Substring(2, 1)
        Return CLng(strID)
    End Function


    

End Module
