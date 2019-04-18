Imports System.Data.SqlClient
Public Class ClsServiceConfiguration
#Region "Private Variables"
    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding
    ''    Dim strgloServiceDatabaseName As String = "gloServices"
    '  Dim strgloServiceDatabaseName As String = gstrServicesDBName
    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding

    Dim _sSendBatchTime As String = ""
    Dim _sCheckResponseTime As String = ""
    Dim _sTerminateCheckResponseTime As String = ""
    Dim _nReferanceID As Int64 = 0

#End Region
#Region "Public Properties"
    Public Property sSendBatchTime() As String
        Get
            Return _sSendBatchTime
        End Get
        Set(ByVal Value As String)
            _sSendBatchTime = Value
        End Set
    End Property
    Public Property sCheckResponseTime() As String
        Get
            Return _sCheckResponseTime
        End Get
        Set(ByVal Value As String)
            _sCheckResponseTime = Value
        End Set
    End Property
    Public Property sTerminateCheckResponseTime() As String
        Get
            Return _sTerminateCheckResponseTime
        End Get
        Set(ByVal Value As String)
            _sTerminateCheckResponseTime = Value
        End Set
    End Property
    Public Property nReferanceID() As Int64
        Get
            Return _nReferanceID
        End Get
        Set(ByVal Value As Int64)
            _nReferanceID = Value
        End Set
    End Property
#End Region
#Region "Public Method"
    Public Function UpdateSettings(ByVal sSettingName As String, ByVal sSettingValue As String) As Boolean

        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdateGlSettings"
            Dim objParaSettingsName As New SqlParameter
            Dim objParaSettingsValue As New SqlParameter
            Dim objParaReferenceID As New SqlParameter
            Dim objParaSettingsClinicID As New SqlParameter
            Dim objParaSettingsParentID As New SqlParameter
            Dim objParaSettingsUserClinicFlag As New SqlParameter

            objCmd.Connection = objCon

            objCon.Open()

            objCmd.Parameters.Clear()
            With objParaReferenceID
                .ParameterName = "@ReferanceID"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaReferenceID)

            With objParaSettingsName
                .ParameterName = "@SettingsName"
                .Value = sSettingName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSettingsName)

            With objParaSettingsValue
                .ParameterName = "@SettingsValue"
                .Value = sSettingValue
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaSettingsValue)

            With objParaSettingsParentID
                .ParameterName = "@nParentID"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With

            objCmd.Parameters.Add(objParaSettingsParentID)
            With objParaSettingsClinicID
                .ParameterName = "@nClinicID"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaSettingsClinicID)
            objCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

      
    End Function
    Public Function GetSettings() As Boolean
        Dim con As New SqlConnection
        Dim dtServicesDatabases As New DataSet()
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim ad As SqlDataAdapter
        Dim dt As New DataTable
        Try
            Dim _sqlQuery As String = " Select nSettingsID as SettingsID,sSettingsName as SettingsName,sSettingsValue as SettingsValue FROM GLSettings"

            ad = New SqlDataAdapter(_sqlQuery, con)
            ''filling the data into the datatable
            ad.Fill(dt)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
        Dim nCount As Integer
        For nCount = 0 To dt.Rows.Count - 1

            Select Case dt.Rows(nCount).Item(1).ToString.ToUpper
                'Case "Clinic Start Time".ToUpper
                Case "bSendBatchTime".ToUpper
                    If IsDBNull(dt.Rows(nCount).Item(2)) = False Then
                        _sSendBatchTime = CType(dt.Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If
                    'Case "Clinic Closing Time".ToUpper
                Case "bCheckResponseTime".ToUpper
                    If IsDBNull(dt.Rows(nCount).Item(2)) = False Then
                        _sCheckResponseTime = CType(dt.Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

                Case "bTerminateCheckResponseTime".ToUpper
                    If IsDBNull(dt.Rows(nCount).Item(2)) = False Then
                        _sTerminateCheckResponseTime = CType(dt.Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

            End Select
        Next
        Return True
    End Function

    Public Function DateAsDateTime(ByVal datetimevalue As TimeSpan) As DateTime
        Dim _result As DateTime = DateTime.Now.[Date]
        Dim TEst As String = datetimevalue.ToString().Replace(":"c, "-"c)
        '  DateTime sdf=d
        Try
            If datetimevalue.ToString().Length = 8 Then
                'string _internalresult = datetimevalue.ToString();
                Dim _internaldate As String = ""
                '_internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                '  _result = Convert.ToDateTime(_internaldate);
                _internaldate = _result.ToString()
                ' Time 
                Dim _AmPm As String = ""
                Dim _internaltime As String = ""

                Dim _Hour As Integer = 0
                ' Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                Dim _Minutes As Integer = 0
                ' Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());
                ''If datetimevalue IsNot Nothing Then
                _Hour = Convert.ToInt16(datetimevalue.Hours)
                ''End If

                ''If datetimevalue IsNot Nothing Then
                _Minutes = Convert.ToInt16(datetimevalue.Minutes)
                ''End If

                ' string _internalresult;

                If _Hour < 12 Then
                    _AmPm = "AM"
                ElseIf _Hour >= 12 Then
                    _AmPm = "PM"
                End If

                _internaltime = _Hour.ToString() & ":" & _Minutes.ToString() & " " & _AmPm

                Dim aaa As String = String.Format(Convert.ToDateTime(_result).ToShortDateString(), "MM/dd/yyyy")
                _result = Convert.ToDateTime(String.Format(Convert.ToDateTime(_result).ToShortDateString(), "MM/dd/yyyy") & " " & String.Format(Convert.ToDateTime(_internaltime).ToShortTimeString(), "hh:mm tt"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        Return _result
    End Function
#End Region


End Class
