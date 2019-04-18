Imports System.Data.SqlClient
Imports gloEMRReports
Public Class ClsHCFAReport
    '  Private da As SqlDataAdapter
    ' Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    Private conString As String
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Con = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If

        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
    'Commented By Shweta 20091128
    'Public Function GetExams(ByVal m_fromdate As DateTime, ByVal m_todate As DateTime) As Boolean
    '    Try
    '        Dim cmd As New SqlCommand("RptViewHCFA", Con)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        Dim sqlParam As SqlParameter
    '        sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = gnPatientID

    '        sqlParam = cmd.Parameters.Add("@fromdate", SqlDbType.DateTime)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = m_fromdate.Date

    '        sqlParam = cmd.Parameters.Add("@todate", SqlDbType.DateTime)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = m_todate.Date

    '        Con.Open()
    '        'cmd.ExecuteNonQuery()
    '        da = New SqlDataAdapter
    '        da.SelectCommand = cmd
    '        dt = New DataTable
    '        da.Fill(dt)
    '        dv = New DataView(dt)
    '        Return True
    '    Catch ex As Exception
    '        'MessageBox.Show(ex.ToString, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    'Changed By Shweta 20091128  against case no :GLO2009 0003381
    Public Function GetExams(ByVal m_fromdate As DateTime, ByVal m_todate As DateTime, ByVal m_PatientID As Int64, ByVal m_ProviderID As Int64) As Boolean
        Try
            Dim cmd As New SqlCommand("RptViewHCFA", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            'Changed By shweta 20091128 against Case No : GLO2009 0003381
            ' sqlParam.Value = gnPatientID
            sqlParam.Value = m_PatientID
            'End

            sqlParam = cmd.Parameters.Add("@fromdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = m_fromdate.Date

            sqlParam = cmd.Parameters.Add("@todate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = m_todate.Date

            'Added  By Shweta 20091128 against Case No : GLO2009 0003381
            'As the new parameter added in store procedure
            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = m_ProviderID
            'End Shweta 

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
            End If

            da.Dispose()
            da = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing

            Return True
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "Drugs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
        End Try
    End Function

    'Added By Shweta 20091128  against case no :GLO2009 0003381
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
            objCon.Dispose()
            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objSQLDataReader = Nothing

            Return clProviders
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function RetrieveProviderID(ByVal strProviderName As String) As Long
        Dim nProviderID As Long = 0
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
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
        objCon.Dispose()
        If IsNothing(nProviderID) Then
            nProviderID = 0
        End If

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaProviderName = Nothing
        objCon = Nothing
        Return nProviderID
    End Function
    'End Shweta 20091128

    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv
            'Return Ds
        End Get
    End Property



    'Public Function CreateReport(ByVal nExamID As String, ByVal oCPT As rpt_CptDriven()) As rpt_CptDriven
    Public Function CreateReport(ByVal sExamID As String) As rpt_CptDriven
        'Create the object for report
        Dim oCPT As rpt_CptDriven = New rpt_CptDriven()
        'Create the object for dataset i.e dsgloEMRReports.xsd
        Dim _dsReports As dsgloEMRReports = New dsgloEMRReports()
        Dim oConnection As SqlConnection = New SqlConnection
        Dim sqlCmd As SqlCommand = New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        'Dim dt As New DataTable

        Try

            oConnection.ConnectionString = GetConnectionString()
            sqlCmd.CommandType = CommandType.Text

            sqlCmd.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                           & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"
            sqlCmd.Connection = oConnection
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_Clinic_MST")

            'Added by Shweta 20100111
            'Fill Insurance detail table
            sqlCmd.CommandText = ""
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.CommandText = " SELECT CONVERT(VARCHAR(50), nPatientID) AS Ins_PatientID, nInsuranceID, CONVERT(VARCHAR(50), nInsuranceID) AS Ins_ID, " _
                                 & " sSubscriberPolicy# AS Ins_SubscriberPolicyNo, sSubscriberID AS Ins_SubscriberID, sGroup AS Ins_Group, sEmployer AS Ins_Employer, " _
                                 & " dtDOB AS Ins_DOB, ISNULL(sSubFName, '') + ' ' + ISNULL(sSubMName, '') + ' ' + ISNULL(sSubLName, '') AS Ins_Subscribername, " _
                                 & " CASE nInsuranceFlag WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE 'InActive' END AS Ins_PrimaryFlag, " _
                                 & " sInsurancePhone AS Ins_InsurancePhone, sInsuranceName AS InsuranceName " _
                                 & " FROM PatientInsurance_DTL"
            sqlCmd.Connection = oConnection
            da.Dispose()
            da = Nothing
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_PatientInsDtl")
            'End Shweta

            'Fill the dt_CptICD9Driven table in dataset present in gloEMRReports usig store procedure
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Rpt_HIPPACPTDrivenReport"
            Dim ParaExamID As New SqlParameter
            With ParaExamID
                .ParameterName = "@EXAMID"
                .Value = sExamID

                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            sqlCmd.Parameters.Add(ParaExamID)
            sqlCmd.Connection = oConnection
            oConnection.Open()
            da.Dispose()
            da = Nothing

            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_CptICD9Driven")

            ParaExamID = Nothing
            'Added by Shweta 20091223
            'against the Bugzilla Id:1282 and Bugzilla Id:5693
            'If the there is any record against exam then only print the report           
            Dim examId As String()
            Dim blnDiagnois As Boolean = False
            examId = sExamID.Split(",")
            If _dsReports.Tables("dt_CptICD9Driven").Rows.Count > 0 Then
                'Check the no. of record for each examid
                For j As Integer = 0 To examId.Length - 1
                    Dim dv As DataView
                    dv = _dsReports.Tables("dt_CptICD9Driven").DefaultView
                    dv.RowFilter = " ExamID =" & examId(j) '.Substring(1, examId(j).Length - 2)
                    'If the record count greater than zero then only show report
                    If dv.Count <> 0 Then
                        oCPT.SetDataSource(_dsReports)
                    Else
                        'prompt the massage only first time 
                        If blnDiagnois = False Then
                            blnDiagnois = True
                            MessageBox.Show("Some Exam(s) do not have any diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Next
                Return oCPT
            Else
                MessageBox.Show("Exam(s) do not have any diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return Nothing
                Exit Function
            End If

            'End coding  by Shweta 20091223
        Catch ex As Exception
            Return Nothing
        Finally

            oConnection.Close()
            oConnection.Dispose()

            If (IsNothing(da) = False) Then
                da.Dispose()
                da = Nothing
            End If
         
            'dt = Nothing
            If sqlCmd IsNot Nothing Then
                sqlCmd.Parameters.Clear()
                sqlCmd.Dispose()
                sqlCmd = Nothing
            End If

            _dsReports = Nothing
            'dv = Nothing

        End Try
    End Function

    'Added By Shweta 20091231
    'To retrive the data related to fill the ICD9 driven report
    'Against Bug Id: 1282 and 5397
    Public Function CreateICD9Report(ByVal sExamID As String) As Rpt_HCFA_ICD9Driven
        'Create the object for report
        Dim oICD9 As Rpt_HCFA_ICD9Driven = New Rpt_HCFA_ICD9Driven()
        'Create the object for dataset i.e dsgloEMRReports.xsd
        Dim _dsReports As dsgloEMRReports = New dsgloEMRReports()
        Dim oConnection As SqlConnection = New SqlConnection
        Dim sqlCmd As SqlCommand = New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        'Dim dt As New DataTable
        Try

            oConnection.ConnectionString = GetConnectionString()
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                           & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"
            sqlCmd.Connection = oConnection
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_Clinic_MST")


            'Fill Insurance detail table
            sqlCmd.CommandText = ""
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.CommandText = " SELECT CONVERT(VARCHAR(50), nPatientID) AS Ins_PatientID, nInsuranceID, CONVERT(VARCHAR(50), nInsuranceID) AS Ins_ID, " _
                                 & " sSubscriberPolicy# AS Ins_SubscriberPolicyNo, sSubscriberID AS Ins_SubscriberID, sGroup AS Ins_Group, sEmployer AS Ins_Employer, " _
                                 & " dtDOB AS Ins_DOB, ISNULL(sSubFName, '') + ' ' + ISNULL(sSubMName, '') + ' ' + ISNULL(sSubLName, '') AS Ins_Subscribername, " _
                                 & " CASE nInsuranceFlag WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE 'InActive' END AS Ins_PrimaryFlag, " _
                                 & " sInsurancePhone AS Ins_InsurancePhone, sInsuranceName AS InsuranceName " _
                                 & " FROM PatientInsurance_DTL"
            sqlCmd.Connection = oConnection
            da.Dispose()
            da = Nothing
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_PatientInsDtl")

            'Fill the dt_ICD9CptDriven table in dataset present in gloEMRReports usig store procedure
            sqlCmd.CommandText = ""
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "Rpt_HIPPAICD9DrivenReport"
            Dim ParaExamID As New SqlParameter
            With ParaExamID
                .ParameterName = "@EXAMID"
                .Value = sExamID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            sqlCmd.Parameters.Add(ParaExamID)
            ParaExamID = Nothing
            sqlCmd.Connection = oConnection
            If oConnection.State = ConnectionState.Open Then
                oConnection.Close()
            End If
            oConnection.Open()
            da.Dispose()
            da = Nothing
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(_dsReports, "dt_ICD9CptDriven")

            'Added by Shweta 20091223
            'against the Bugzilla Id:1282 and Bugzilla Id:5693
            'If the there is any record against exam then only print the report 
            Dim examId As String()
            Dim blnDiagnois As Boolean = False
            examId = sExamID.Split(",")
            If _dsReports.Tables("dt_ICD9CptDriven").Rows.Count > 0 Then
                'Check the no. of record for each examid
                For j As Integer = 0 To examId.Length - 1
                    Dim dv As DataView = Nothing
                    dv = _dsReports.Tables("dt_ICD9CptDriven").DefaultView
                    dv.RowFilter = " ExamID =" & examId(j) '.Substring(1, examId(j).Length - 2)
                    'If the record count greater than zero then only show report
                    If dv.Count <> 0 Then
                        oICD9.SetDataSource(_dsReports)
                    Else
                        'prompt the massage only first time 
                        If blnDiagnois = False Then
                            blnDiagnois = True
                            MessageBox.Show("Some Exam(s) do not have any diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Next
                Return oICD9
            Else
                MessageBox.Show("Exam(s) do not have any diagnosis", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return Nothing
                Exit Function
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            oConnection.Close()
            oConnection.Dispose()
            If (IsNothing(da) = False) Then
                da.Dispose()
                da = Nothing
            End If

            'dt = Nothing
            If sqlCmd IsNot Nothing Then
                sqlCmd.Parameters.Clear()
                sqlCmd.Dispose()
                sqlCmd = Nothing
            End If
            _dsReports = Nothing
            'dv = Nothing
        End Try
    End Function
    '   'End coding  by Shweta 20091231
End Class
