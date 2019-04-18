Imports System.Data.SqlClient
Public Class clsProviderSettings
    Enum enmSettingsType
        ProviderReferralLetterConfiguration
        ProviderUserTaskAssignment
        ProviderLabTaskAssignment
        ProviderRxEligTaskAssignment
        ProviderDMSTaskAssignment
    End Enum

    Public Function Fill_ReferralLetters() As Collection
        Dim clReferraLetters As New Collection
        Dim objCon As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim objCmd As New SqlCommand
        objCmd.Connection = objCon
        objCmd.CommandText = "gsp_FillTemplateGallery_MST"
        objCmd.CommandType = CommandType.StoredProcedure
        Dim objParam As New SqlParameter
        objParam.ParameterName = "@flag"
        objParam.Direction = ParameterDirection.Input
        objParam.SqlDbType = SqlDbType.Int
        objParam.Value = 10
        objCmd.Parameters.Add(objParam)

        Dim objReader As SqlDataReader
        objCon.Open()
        objReader = objCmd.ExecuteReader()
        While objReader.Read
            clReferraLetters.Add(objReader.GetString(1))
        End While
        objReader.Close()
        objCon.Close()
        objReader = Nothing
        objCmd = Nothing
        objCon = Nothing
        Return clReferraLetters
    End Function

    Public Function DeleteProviderConfiguration(ByVal ProviderConfigurationSettings As enmSettingsType) As Boolean
        Try
            Dim objCon As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
            Dim objCmd As New SqlCommand
            objCmd.Connection = objCon
            objCmd.CommandText = "gsp_DeleteProviderSettings"
            objCmd.CommandType = CommandType.StoredProcedure
            Dim objParam As New SqlParameter
            objParam.ParameterName = "@SettingsType"
            objParam.Direction = ParameterDirection.Input
            objParam.SqlDbType = SqlDbType.VarChar
            Dim strConfiguration As String = ""
            Select Case ProviderConfigurationSettings
                Case enmSettingsType.ProviderReferralLetterConfiguration
                    strConfiguration = "Referralletter"
                Case enmSettingsType.ProviderUserTaskAssignment
                    strConfiguration = "User"
                Case enmSettingsType.ProviderLabTaskAssignment
                    strConfiguration = "LabUser"
                Case enmSettingsType.ProviderRxEligTaskAssignment
                    strConfiguration = "RxEligibilityUser"
                Case enmSettingsType.ProviderDMSTaskAssignment
                    strConfiguration = "DMSUser"
            End Select
            objParam.Value = strConfiguration
            objCmd.Parameters.Add(objParam)
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function AddProviderConfiguration(ByVal strProviderName As String, ByVal strConfingurationName As String, ByVal ProviderConfiguration As enmSettingsType) As Boolean
        Try
            Dim objCon As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
            Dim objCmd As New SqlCommand
            objCmd.Connection = objCon
            objCmd.CommandText = "gsp_InsertProviderSettings"
            objCmd.CommandType = CommandType.StoredProcedure
            Dim objParamProvider As New SqlParameter
            With objParamProvider
                .ParameterName = "@ProviderName"
                .SqlDbType = SqlDbType.VarChar
                .Value = strProviderName
            End With
            objCmd.Parameters.Add(objParamProvider)

            'End 

            Dim objParamConfiguration As New SqlParameter
            With objParamConfiguration
                .ParameterName = "@OthersName"
                .SqlDbType = SqlDbType.VarChar
                .Value = strConfingurationName
            End With
            objCmd.Parameters.Add(objParamConfiguration)

            Dim objParam As New SqlParameter
            objParam.ParameterName = "@SettingsType"
            objParam.Direction = ParameterDirection.Input
            objParam.SqlDbType = SqlDbType.VarChar
            Dim strConfiguration As String = ""
            Select Case ProviderConfiguration
                Case enmSettingsType.ProviderReferralLetterConfiguration
                    strConfiguration = "Referralletter"
                Case enmSettingsType.ProviderUserTaskAssignment
                    strConfiguration = "User"
                Case enmSettingsType.ProviderLabTaskAssignment
                    strConfiguration = "LabUser"
                Case enmSettingsType.ProviderRxEligTaskAssignment
                    strConfiguration = "RxEligibilityUser"
                Case enmSettingsType.ProviderDMSTaskAssignment
                    strConfiguration = "DMSUser"
            End Select
            objParam.Value = strConfiguration
            objCmd.Parameters.Add(objParam)

            'Shubhangi 20091123
            'Add new parameter of machine ID in SP to add not null cloumn
            Dim objParamID As New SqlParameter

            objParamID.Direction = ParameterDirection.Input
            objParamID.ParameterName = "@MachineID"
            objParamID.DbType = SqlDbType.BigInt
            objParamID.Value = mdlGeneral.GetPrefixTransactionID()
            objCmd.Parameters.Add(objParamID)

            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()

            objCmd = Nothing
            objCon = Nothing
            Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return False
        End Try
    End Function
    Public Function RetrieveSettings(ByVal strProviderName As String, ByVal enmConfiguration As enmSettingsType) As String
        Dim strSettings As String = ""
        Dim objCon As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim objCmd As New SqlCommand
        objCmd.Connection = objCon
        objCmd.CommandText = "gsp_RetrieveProviderConfiguration"
        objCmd.CommandType = CommandType.StoredProcedure
        Dim objParam As New SqlParameter
        objParam.ParameterName = "@SettingsType"
        objParam.Direction = ParameterDirection.Input
        objParam.SqlDbType = SqlDbType.VarChar
        Dim strConfiguration As String = ""
        Select Case enmConfiguration
            Case enmSettingsType.ProviderReferralLetterConfiguration
                strConfiguration = "Referralletter"
            Case enmSettingsType.ProviderUserTaskAssignment
                strConfiguration = "User"
            Case enmSettingsType.ProviderLabTaskAssignment
                strConfiguration = "LabUser"
            Case enmSettingsType.ProviderRxEligTaskAssignment
                strConfiguration = "RxEligibilityUser"
            Case enmSettingsType.ProviderDMSTaskAssignment
                strConfiguration = "DMSUser"
        End Select
        objParam.Value = strConfiguration
        objCmd.Parameters.Add(objParam)
        Dim objParamProvider As New SqlParameter
        With objParamProvider
            .ParameterName = "@ProviderName"
            .SqlDbType = SqlDbType.VarChar
            .Value = strProviderName
        End With
        objCmd.Parameters.Add(objParamProvider)

        objCon.Open()
        strSettings = objCmd.ExecuteScalar
        objCon.Close()
        If IsNothing(strSettings) Then
            strSettings = ""
        End If
        objCmd = Nothing
        objCon = Nothing
        Return strSettings
    End Function
End Class
