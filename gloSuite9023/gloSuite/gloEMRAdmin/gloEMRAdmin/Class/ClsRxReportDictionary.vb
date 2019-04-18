Imports System.Data.SqlClient
Public Class ClsRxReportDictionary
    Implements IDataDictionary
    Public Function GetDictionary(ByVal m_flag As Boolean) As System.Data.DataTable Implements mdlGeneral.IDataDictionary.GetDictionary
        'sarika 26th june 07
        Dim objadapter As SqlDataAdapter = Nothing
        '---

        Try

            Dim objconn As SqlConnection
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            If m_flag Then
                'objadapter = New SqlDataAdapter("SELECT patient.nPatientID, patient.sPatientCode,patient.sFirstName, patient.sMiddleName, patient.sLastName, patient.dtDOB, patient.nSSN, patient.sGender, patient.sMaritalStatus, patient.sAddressLine2, patient.sAddressLine1, patient.sCity,patient.sState, patient.sZIP, patient.sCounty, patient.sPhone, patient.sMobile, patient.sEmail, patient.sFAX, patient.sOccupation, patient.sEmploymentStatus, patient.sPlaceofEmployment, patient.sWorkAddressLine1,patient.sWorkAddressLine2, patient.sWorkCity, patient.sWorkState, patient.sWorkZIP, patient.sWorkPhone, patient.sWorkFAX, patient.sChiefComplaints, patient.nProviderID, patient.nPCPId, patient.sGuarantor, patient.nPharmacyID,patient.sSpousePhone, patient.sSpouseName, patient.sRace, patient.sPatientStatus, patient.iPhoto, patient.dtRegistrationDate, patient.dtInjuryDate, patient.dtSurgeryDate, patient.sHandDominance, patient.sLocation FROM Patient where 1=0", objconn)
                'code commented
                ' objadapter = New SqlDataAdapter("SELECT * FROM RxReport", objconn)
                'code added
                objadapter = New SqlDataAdapter("SELECT * FROM RxReport1", objconn)
                '-----
            Else
                ' objadapter = New SqlDataAdapter("SELECT provider_mst.nProviderID, provider_mst.sFirstName, provider_mst.sMiddleName, provider_mst.sLastName, provider_mst.sGender,provider_mst.sDEA,provider_mst.sAddress,provider_mst.sStreet, provider_mst.sCity,provider_mst. sState,provider_mst.sZIP, provider_mst.sPhoneNo,provider_mst.sFAX, provider_mst.sMobileNo,provider_mst.sPagerNo, provider_mst.sEmail, provider_mst.sURL, provider_mst.imgSignature FROM Provider_MST where 1=0", objconn)
            End If
            Dim objdatatable As New DataTable
            objadapter.Fill(objdatatable)
            Return objdatatable
        Catch ex As Exception
            ' MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function getReportData(ByVal strselect As String) As System.Data.DataTable Implements mdlGeneral.IDataDictionary.getReportData

        Try
            Dim objconn As SqlConnection
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            Dim objadapter As SqlDataAdapter
            objadapter = New SqlDataAdapter(strselect, objconn)
            Dim objdatatable As New DataTable
            objadapter.Fill(objdatatable)
            Return objdatatable
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function GetClinicLogo() As DataTable Implements mdlGeneral.IDataDictionary.GetClinicLogo
        Try
            Dim objconn As SqlConnection
            Dim objcmd As New SqlCommand
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            Dim strselect As String = "Select imgClinicLogo as ClinicLogo from Clinic_MST"

            Dim objadapter As SqlDataAdapter
            objadapter = New SqlDataAdapter(strselect, objconn)
            Dim objdatatable As New DataTable
            objadapter.Fill(objdatatable)
            Return objdatatable
        Catch ex As Exception
            MsgBox(ex.Message)
            'sarika 26th june 07
            Return Nothing
        End Try
    End Function
    Public Function GetProviderSign() As DataTable Implements mdlGeneral.IDataDictionary.GetProviderSign
        Try
            Dim objconn As SqlConnection
            Dim objcmd As New SqlCommand
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            Dim strselect As String = "SELECT Provider_MST.imgSignature AS ProviderSignature FROM Provider_MST INNER JOIN RxreportData ON Provider_MST.nProviderID = RxreportData.nProviderID"
            Dim objadapter As SqlDataAdapter
            objadapter = New SqlDataAdapter(strselect, objconn)
            Dim objdatatable As New DataTable
            objadapter.Fill(objdatatable)
            Return objdatatable
        Catch ex As Exception
            MsgBox(ex.Message)
            'sarika 26th june 07
            Return Nothing
        End Try

    End Function
    Public Function GetProviders() As ArrayList Implements mdlGeneral.IDataDictionary.GetProviders
        Try
            Dim objconn As SqlConnection
            Dim objcmd As New SqlCommand
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            Dim strQuery As String
            strQuery = "SELECT distinct ProviderSettings.nOthersID FROM RxreportData INNER JOIN ProviderSettings ON RxreportData.nProviderID = ProviderSettings.nProviderID and sSettingstype='ProviderSeniorAssignment'"
            Dim objadapter As SqlDataAdapter
            objadapter = New SqlDataAdapter(strQuery, objconn)
            Dim objdatatable As New DataTable
            objadapter.Fill(objdatatable)
            objadapter.Dispose()
            Dim objProviders As New ArrayList
            'sarika 26th june 07
            Dim objProv As New ArrayList
            '---
            Dim strProviderName As String
            If Not objdatatable Is Nothing Then
                If objdatatable.Rows.Count > 0 Then
                    For cntRow As Int16 = 0 To objdatatable.Rows.Count - 1
                        strQuery = "select isnull(Provider_MST.sFirstName,'') + SPACE(1) + isnull(Provider_MST.sMiddleName,'') + SPACE(1) + isnull(Provider_MST.sLastName,'') + Space(1) + isnull(Provider_mst.sDEA,'') AS ProviderName"
                        strQuery &= " from Provider_MST where nProviderID=" & objdatatable.Rows(cntRow).Item(0)
                        strProviderName = getProviderName(strQuery)
                        If Not strProviderName Is Nothing Then
                            objProviders.Add(strProviderName)
                        End If
                        strProviderName = Nothing
                        strQuery = Nothing
                    Next
                    objProv = objProviders
                    'Return objProviders
                Else
                    objProv = Nothing
                    'Return Nothing
                End If
            End If

            Return objProv

        Catch ex As Exception
            MsgBox(ex.Message)
            'sarika 26th june 07
            Return Nothing
        End Try
    End Function

    Private Function getProviderName(ByVal strSQL As String) As String
        Dim objconn As SqlConnection = Nothing
        Dim oReader As SqlDataReader
        Dim objcmd As New SqlCommand
        'sarika 26th june 07
        Dim ProviderName As String = ""
        '--
        Try
            Dim strconn As String
            strconn = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            objcmd.Connection = objconn
            objcmd.CommandText = strSQL
            objconn.Open()

            oReader = objcmd.ExecuteReader
            If Not IsDBNull(oReader) Then
                If oReader.HasRows Then
                    oReader.Read()
                    ProviderName = oReader("ProviderName")
                End If
            End If


            Return ProviderName
        Catch err As Exception
            Return ""
        Finally
            objconn.Close()
            oReader = Nothing
            objcmd = Nothing
        End Try

    End Function

End Class
