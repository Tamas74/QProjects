Imports System.Data.SqlClient
Namespace gloStream
    Namespace gloAlert
        Public Class Alert

            Const strACTIVE = "Active"
            Const strINACTIVE = "Inactive"

            Public Enum Alert_Type
                General = 1
                History = 2
                Allergies = 3
                Message = 4
            End Enum

            Public Enum Alert_Status
                Active = 1
                Inactive = 2
            End Enum

#Region "Alert METHODS"

            Public Function SetAlert(ByVal PatientCode As String, ByVal AlertType As gloStream.gloAlert.Alert.Alert_Type, ByVal Description As String, ByVal AlertStatus As gloStream.gloAlert.Alert.Alert_Status) As Boolean
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                oDB.DBParameters.Add("@PatientCode", PatientCode, ParameterDirection.Input, SqlDbType.VarChar, 50)
                oDB.DBParameters.Add("@AlertType", AlertType, ParameterDirection.Input, SqlDbType.Int)
                oDB.DBParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar, 255)
                oDB.DBParameters.Add("@AlertStatus", AlertStatus, ParameterDirection.Input, SqlDbType.Int)
                oDB.DBParameters.Add("@MachineID", GetPrefixTransactionID, ParameterDirection.Input, SqlDbType.BigInt)

                oDB.Connect(GetConnectionString)
                If oDB.ExecuteNonQuery("gsp_InUpPatientAlert") = True Then
                    SetAlert = True
                Else
                    SetAlert = False
                End If
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End Function

            Public Function GetAlert(ByVal PatientCode As String, ByVal AlertType As gloStream.gloAlert.Alert.Alert_Type, Optional ByVal AlertStatus As gloStream.gloAlert.Alert.Alert_Status = Alert_Status.Inactive, Optional ByVal PatientID As Long = 0) As Collection

                Dim oDB As New gloStream.gloDataBase.gloDataBase

                oDB.DBParameters.Add("@PatientCode", PatientCode, ParameterDirection.Input, SqlDbType.VarChar, 50)
                oDB.DBParameters.Add("@AlertType", AlertType, ParameterDirection.Input, SqlDbType.Int)
                oDB.DBParameters.Add("@Status", AlertStatus, ParameterDirection.Input, SqlDbType.Int)
                oDB.DBParameters.Add("@nPatientid", PatientID, ParameterDirection.Input, SqlDbType.BigInt) ''patientid added for incident CAS-09269-N7F7S6 patient alert issue
                oDB.Connect(GetConnectionString)
                Dim dr As SqlDataReader
                Dim col As New Collection
                dr = oDB.ReadRecords("gsp_ScanPatientAlert")

                While dr.Read
                    col.Add(dr("sDescription"))
                    col.Add(dr("Status"))
                End While
                dr.Close()
                dr = Nothing
                oDB.Disconnect()

                oDB.Dispose() : oDB = Nothing

                Return col
            End Function

            Public Function PatientIMAlerts(ByVal PatientID As Long) As String
                ' gstrPatientCode()
                '  Dim Con As SqlClient.SqlConnection
                Dim strIMAlert As String = ""

                Dim oDR As SqlClient.SqlDataReader

                Dim oDB As New gloStream.gloDataBase.gloDataBase

                ' If Not oCriteriaName = "" Then
                'Criteria Master Record
                '_strSQL = "select * from Im_Trn_Dtl where im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 and im_trn_date is null"
                '_strSQL = "select im_trn_duedate from Im_Trn_Dtl,IM_Trn_Mst where IM_Trn_Mst.im_trn_mst_id = IM_Trn_Dtl.im_trn_mst_id and im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 and im_trn_date is null and IM_Trn_Mst.im_trn_mst_nPatientID = " & gnPatientID

                '_strSQL = " select im_mst.im_item_name,Im_Trn_Dtl.im_trn_counterID, im_trn_duedate from Im_Trn_Dtl " & _
                '    " inner join im_mst on  Im_Trn_Dtl.im_trn_itemid = im_mst.im_item_id " & _
                '    " inner join IM_Trn_Mst on  Im_Trn_Dtl.im_trn_mst_id = im_trn_mst.im_trn_mst_id " & _
                '    " and im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 " & _
                '    " and im_trn_date is null " & _
                '    " and IM_Trn_Mst.im_trn_mst_nPatientID = " & gnPatientID

                With oDB
                    .DBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    .Connect(GetConnectionString)
                    oDR = .ReadRecords("gIM_PatientIMAlerts")

                    If Not oDR Is Nothing Then
                        If oDR.HasRows = True Then
                            While oDR.Read
                               
                                If strIMAlert.Trim <> "" Then
                                    ''strIMAlert = strIMAlert & ", " & oDR.Item(0) & "-" & oDR.Item(1) & " on " & oDR.Item(2)
                                    strIMAlert = strIMAlert & ", " & oDR.Item("im_item_name") & " on " & Convert.ToDateTime(oDR.Item("im_trn_duedate")).ToShortDateString()
                                Else

                                    strIMAlert = "Immunization due for " & oDR.Item("im_item_name") & " on " & Convert.ToDateTime(oDR.Item("im_trn_duedate")).ToShortDateString()
                                End If

                                ' ShowPatientAlerts(imMsg)
                                ' Return CLng(_Result)
                                ' Else
                                ' Return 0
                            End While
                        End If
                        oDR.Close()
                        oDR = Nothing

                    End If
                    oDB.Disconnect()
                End With

                oDB.Dispose() : oDB = Nothing

                Return strIMAlert

                'Dim sqlconn As String
                'sqlconn = GetConnectionString()
                'Con = New System.Data.SqlClient.SqlConnection(sqlconn)

                'Dim cmd As New SqlClient.SqlCommand("gsp_patientIMalerts", Con)
                'cmd.CommandType = CommandType.StoredProcedure
                'Dim sqlParam As SqlClient.SqlParameter

                'sqlParam = cmd.Parameters.Add("@gnPatientID", gnPatientID)
                'sqlParam.Direction = ParameterDirection.Input

                'oDB.Connect(GetConnectionString)
                'oDa = oDB.ReadQueryRecords(_strSQL)

                ''Return Object
                'If Not oDR Is Nothing Then
                '    If oDR.HasRows = True Then
                '        While oDR.Read
                '            Dim imMsg As String = "Patient Immunization Alert. Patient's Due date of " & oDR.Item(0) & ", Dose number " & oDR.Item(1) & " on Date " & oDR.Item(2)
                '            ' ShowPatientAlerts(imMsg)
                '            ' Return CLng(_Result)
                '            ' Else
                '            ' Return 0
                '        End While
                '    End If
                '    oDB.Disconnect()
                '    oDa.Close()
                'End If

            End Function


            'Public Function PatientIMAlerts(ByVal PatientID As Long) As String
            '    ' gstrPatientCode()
            '    '  Dim Con As SqlClient.SqlConnection
            '    Dim strIMAlert As String = ""

            '    Dim oDR As SqlClient.SqlDataReader

            '    Dim oDB As New gloStream.gloDataBase.gloDataBase

            '    ' If Not oCriteriaName = "" Then
            '    'Criteria Master Record
            '    '_strSQL = "select * from Im_Trn_Dtl where im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 and im_trn_date is null"
            '    '_strSQL = "select im_trn_duedate from Im_Trn_Dtl,IM_Trn_Mst where IM_Trn_Mst.im_trn_mst_id = IM_Trn_Dtl.im_trn_mst_id and im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 and im_trn_date is null and IM_Trn_Mst.im_trn_mst_nPatientID = " & gnPatientID

            '    '_strSQL = " select im_mst.im_item_name,Im_Trn_Dtl.im_trn_counterID, im_trn_duedate from Im_Trn_Dtl " & _
            '    '    " inner join im_mst on  Im_Trn_Dtl.im_trn_itemid = im_mst.im_item_id " & _
            '    '    " inner join IM_Trn_Mst on  Im_Trn_Dtl.im_trn_mst_id = im_trn_mst.im_trn_mst_id " & _
            '    '    " and im_trn_duedate between dbo.gloGetDate() and dbo.gloGetDate()+7 " & _
            '    '    " and im_trn_date is null " & _
            '    '    " and IM_Trn_Mst.im_trn_mst_nPatientID = " & gnPatientID

            '    With oDB
            '        .DBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            '        .Connect(GetConnectionString)
            '        oDR = .ReadRecords("gIM_PatientIMAlerts")

            '        If Not oDR Is Nothing Then
            '            If oDR.HasRows = True Then
            '                While oDR.Read
            '                    If strIMAlert.Trim <> "" Then
            '                        'strIMAlert = strIMAlert & vbCrLf & "Patient's Due date of " & oDR.Item(0) & ", Dose number " & oDR.Item(1) & " on Date " & oDR.Item(2)
            '                        strIMAlert = strIMAlert & vbCrLf & "Dose number " & oDR.Item(1) & ", of dose " & oDR.Item(0) & " on " & oDR.Item(2)
            '                    Else
            '                        'strIMAlert = "Patient's Immunization Due date Alert." & vbCrLf & "Due date of " & oDR.Item(0) & ", Dose number " & oDR.Item(1) & " on Date " & oDR.Item(2)
            '                        strIMAlert = "Patient's Immunization Due date Alert." & vbCrLf & "Dose number " & oDR.Item(1) & ", of dose " & oDR.Item(0) & " on " & oDR.Item(2)
            '                    End If

            '                    ' ShowPatientAlerts(imMsg)
            '                    ' Return CLng(_Result)
            '                    ' Else
            '                    ' Return 0
            '                End While
            '            End If
            '        End If
            '        oDB.Disconnect()
            '        oDR.Close()
            '    End With

            '    oDB = Nothing

            '    Return strIMAlert
            '    'Dim sqlconn As String
            '    'sqlconn = GetConnectionString()
            '    'Con = New System.Data.SqlClient.SqlConnection(sqlconn)

            '    'Dim cmd As New SqlClient.SqlCommand("gsp_patientIMalerts", Con)
            '    'cmd.CommandType = CommandType.StoredProcedure
            '    'Dim sqlParam As SqlClient.SqlParameter

            '    'sqlParam = cmd.Parameters.Add("@gnPatientID", gnPatientID)
            '    'sqlParam.Direction = ParameterDirection.Input

            '    'oDB.Connect(GetConnectionString)
            '    'oDa = oDB.ReadQueryRecords(_strSQL)

            '    ''Return Object
            '    'If Not oDR Is Nothing Then
            '    '    If oDR.HasRows = True Then
            '    '        While oDR.Read
            '    '            Dim imMsg As String = "Patient Immunization Alert. Patient's Due date of " & oDR.Item(0) & ", Dose number " & oDR.Item(1) & " on Date " & oDR.Item(2)
            '    '            ' ShowPatientAlerts(imMsg)
            '    '            ' Return CLng(_Result)
            '    '            ' Else
            '    '            ' Return 0
            '    '        End While
            '    '    End If
            '    '    oDB.Disconnect()
            '    '    oDa.Close()
            '    'End If

            'End Function

#End Region

#Region "Supporting Methods"

            Public Function StausToString(ByVal Status As gloAlert.Alert.Alert_Status) As String
                Dim strStatus As String = ""
                Select Case Status
                    Case Alert_Status.Active
                        strStatus = strACTIVE
                    Case Alert_Status.Inactive
                        strStatus = strINACTIVE
                End Select
                Return strStatus
            End Function

            Public Function StausAsStatus(ByVal strStatus As String) As gloAlert.Alert.Alert_Status
                Dim Status As Alert_Status
                Select Case strStatus
                    Case strACTIVE
                        Status = Alert_Status.Active
                    Case strINACTIVE
                        Status = Alert_Status.Inactive
                End Select
                Return Status
            End Function


#End Region

        End Class
    End Namespace
End Namespace

