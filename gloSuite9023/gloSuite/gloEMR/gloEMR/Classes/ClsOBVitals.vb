Imports System.Data.SqlClient


Public Class ClsOBVitals
    '********************************
    'Private da As SqlDataAdapter
    '' Private ds As New DataSet
    'Private dt As DataTable
    'Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String

    Public Sub New()
        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Con = New System.Data.SqlClient.SqlConnection(sqlconn)

        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        Return ds
    '    End Get
    'End Property
    Public Sub Dispose()

      

        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
    'Public ReadOnly Property GetDataview() As DataView
    '    Get
    '        Return dv
    '    End Get
    'End Property


    

    Public Function GetOBVitals(ByVal nVitalID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_GetOBVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nVitalID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVitalID
            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function


    Function GetPreviousOBVitals(ByVal patID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_GetPreviousOBVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = patID
            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Function GetRecentOBVitals(ByVal patID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_GetRecentOBVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = patID
            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function
    
    Public Function AddOBVITALS(ByVal nVitalID As Long, ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal _hashTable As Hashtable, ByVal nCaseID As Long, Optional ByVal blnOBPopUp As Boolean = False)

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_InUpOBVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nVitalID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVitalID

            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitID

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            sqlParam = cmd.Parameters.Add("@nCaseID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If nCaseID <= 0 Then
                sqlParam.Value = System.Data.SqlTypes.SqlInt64.Null
            Else
                sqlParam.Value = nCaseID
            End If


            For Each element As DictionaryEntry In _hashTable
                Select Case element.Key
                    'dtOBVital Date
                    Case "dtVital"
                        sqlParam = cmd.Parameters.Add("@dtOBVitalDate", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Convert.ToDateTime(element.Value)

                        'Case "txtPreferredGesAge" 'Preffered Ges Age
                        '    sqlParam = cmd.Parameters.Add("@sConceptionGeAge", SqlDbType.VarChar)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    sqlParam.Value = Convert.ToString(element.Value)


                    Case "rdConception"  'Radio Conception
                        sqlParam = cmd.Parameters.Add("@bConception", SqlDbType.Bit)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Convert.ToBoolean(element.Value)

                    Case "dtpConEstDate"
                        sqlParam = cmd.Parameters.Add("@dtConceptionEst", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                    Case "dtpConDueDate"
                        sqlParam = cmd.Parameters.Add("@dtConceptionDue", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                        'Case "txtConGesAge"
                        '    sqlParam = cmd.Parameters.Add("@sConceptionGeAge", SqlDbType.VarChar)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToString(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If


                    Case "rdLMP"  'Radio LMP
                        sqlParam = cmd.Parameters.Add("@bLastMenstrualPeriod", SqlDbType.Bit)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Convert.ToBoolean(element.Value)

                    Case "dtpLMPEstDate"
                        sqlParam = cmd.Parameters.Add("@dtLMPEst", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                    Case "dtpLMPDueDate"
                        sqlParam = cmd.Parameters.Add("@dtLMPDue", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                    Case "txtLMPGesAge"
                        sqlParam = cmd.Parameters.Add("@sLMPGeAge", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If


                    Case "rdUltra"  'Radio ULTRA
                        sqlParam = cmd.Parameters.Add("@bUltraSound", SqlDbType.Bit)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Convert.ToBoolean(element.Value)

                        'Case "dtpUltraEstDate"
                        '    sqlParam = cmd.Parameters.Add("@dtUltraSoundEst", SqlDbType.DateTime)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If Not IsNothing(element.Value) Then
                        '        If element.Value <> String.Empty Then
                        '            sqlParam.Value = Convert.ToDateTime(element.Value)
                        '        Else
                        '            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        '        End If
                        '    Else
                        '        sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        '    End If

                    Case "dtpUltraDueDate"
                        sqlParam = cmd.Parameters.Add("@dtUltraSoundDue", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                        'Case "txtUltraGesAge"
                        '    sqlParam = cmd.Parameters.Add("@sUltraSoundGeAge", SqlDbType.VarChar)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value <> String.Empty Then
                        '        sqlParam.Value = Convert.ToString(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                    Case "txtWeightChange" 'Weight & Height Related
                        sqlParam = cmd.Parameters.Add("@dWeightChange", SqlDbType.Decimal)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToDecimal(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtPrePregWeight"
                        sqlParam = cmd.Parameters.Add("@dPrePregenancyWeight", SqlDbType.Decimal)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToDecimal(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If


                    Case "txtFundalHeight"
                        sqlParam = cmd.Parameters.Add("@dFundalHeight", SqlDbType.Decimal)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                        'Case "txtTotalPregenancies" 'Obstetric History
                        '    sqlParam = cmd.Parameters.Add("@nTotalPregnancies", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtFullTerm"
                        '    sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtPremature"
                        '    sqlParam = cmd.Parameters.Add("@nPremature", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If


                        'Case "txtAbortedInduced"
                        '    sqlParam = cmd.Parameters.Add("@nAbortionsInduced", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtAbortedSpontanoues"
                        '    sqlParam = cmd.Parameters.Add("@nAbortionsSpontaneous", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtEctopic"
                        '    sqlParam = cmd.Parameters.Add("@nEctopics", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtMultipleBirth"
                        '    sqlParam = cmd.Parameters.Add("@nMultipleBirths", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If

                        'Case "txtLiving"
                        '    sqlParam = cmd.Parameters.Add("@nLivingChildren", SqlDbType.Int)
                        '    sqlParam.Direction = ParameterDirection.Input
                        '    If element.Value.ToString() <> String.Empty Then
                        '        sqlParam.Value = Convert.ToInt16(element.Value)
                        '    Else
                        '        sqlParam.Value = DBNull.Value
                        '    End If
                    Case "dtpEstDueDate"
                        sqlParam = cmd.Parameters.Add("@dtEstimatedDueDate", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        If Not IsNothing(element.Value) Then
                            If element.Value <> String.Empty Then
                                sqlParam.Value = Convert.ToDateTime(element.Value)
                            Else
                                sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                            End If
                        Else
                            sqlParam.Value = System.Data.SqlTypes.SqlDateTime.Null
                        End If

                    Case "txtUrineAlbumin" 'Urine
                        sqlParam = cmd.Parameters.Add("@sUrineAlbumin", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtUrineGlucose"
                        sqlParam = cmd.Parameters.Add("@sUrineGlucose", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtDilation" 'Cervix
                        sqlParam = cmd.Parameters.Add("@sCervixExamDilation", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtEffacement"
                        sqlParam = cmd.Parameters.Add("@sCervixExamEffacement", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtStation"
                        sqlParam = cmd.Parameters.Add("@sCervixExamStation", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtFetalheartrate" 'Other
                        sqlParam = cmd.Parameters.Add("@sFetalHeartRate", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtEdema"
                        sqlParam = cmd.Parameters.Add("@sEdema", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "cbFetalMovement"
                        sqlParam = cmd.Parameters.Add("@sFetalMovement", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "cbPreTermLabor"
                        sqlParam = cmd.Parameters.Add("@sPreTermLaborSigns", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtPresentation"
                        sqlParam = cmd.Parameters.Add("@sPresentation", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "txtNextAppointment" 'Next Appointment
                        sqlParam = cmd.Parameters.Add("@sNextAppointment", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = Convert.ToString(element.Value)
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "cbGAWeeks" 'GAWeeks
                        sqlParam = cmd.Parameters.Add("@nGAWeeks", SqlDbType.SmallInt)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = element.Value
                        Else
                            sqlParam.Value = DBNull.Value
                        End If

                    Case "cbGADays" 'GADays
                        sqlParam = cmd.Parameters.Add("@nGADays", SqlDbType.SmallInt)
                        sqlParam.Direction = ParameterDirection.Input
                        If element.Value.ToString() <> String.Empty Then
                            sqlParam.Value = element.Value
                        Else
                            sqlParam.Value = DBNull.Value
                        End If
                        'bISPatientAtRisk =@bISPatientAtRisk,
                        '                      sRiskComments=@sRiskComments
                    Case "chkOBrsk"
                        sqlParam = cmd.Parameters.Add("@bISPatientAtRisk", SqlDbType.Bit)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = element.Value

                    Case "txtOBComment"
                        sqlParam = cmd.Parameters.Add("@sRiskComments", SqlDbType.NVarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Convert.ToString(element.Value)

                End Select
            Next
            sqlParam = cmd.Parameters.Add("@blnOBPopUp", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = blnOBPopUp

            If Con.State <> ConnectionState.Open Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Return False
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function


    Public Sub GetMinMaxValues(ByVal ageInMonths As Double, ByVal Gender As String, ByVal vitalType As gloStream.Vitals.Supportings.VitalTypes, ByRef minValue As Double, ByRef maxValue As Double)
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim query As String = ""
        Dim dtResult As DataTable = Nothing
        Try
            query = " SELECT dMinVal, dMaxVal FROM VitalNorms " _
                    & " WHERE nVitalType = " & vitalType.GetHashCode & " " _
                    & " AND sGender = '" & Gender & "' " _
                    & " AND nFromAge < " & ageInMonths & " " _
                    & " AND nToAge >= " & ageInMonths & " "

            oDB.Connect(False)
            oDB.Retrive_Query(query, dtResult)
            If IsNothing(dtResult) = False Then
                If dtResult.Rows.Count > 0 Then
                    minValue = Convert.ToDouble(dtResult.Rows(0)("dMinVal"))
                    maxValue = Convert.ToDouble(dtResult.Rows(0)("dMaxVal"))
                Else
                    minValue = 0
                    maxValue = 0
                End If
            Else
                minValue = 0
                maxValue = 0
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(dtResult) Then
                dtResult.Dispose()
                dtResult = Nothing
            End If
        End Try
    End Sub
    ' '''' End by Pranit

    Public Function GetActiveOBCaseWithHigherDate(ByVal patID As Long, ByVal dtVitalData As DateTime) As DataSet
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtResult As DataSet = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientId", patID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@Vitaldate", dtVitalData, ParameterDirection.Input, SqlDbType.DateTime)
            oDB.Retrive("gsp_GetActiveOBCases", oParam, dtResult)
            oDB.Disconnect()
            Return dtResult
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dtResult
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If
        End Try
    End Function



    Public Function GetNewPregnancyCase(ByVal patID As Long, ByVal dtVitalData As DateTime) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtResult As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientId", patID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@Vitaldate", dtVitalData, ParameterDirection.Input, SqlDbType.DateTime)
            oDB.Retrive("gsp_GetNewPregnancyCase", oParam, dtResult)
            oDB.Disconnect()
            Return dtResult
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dtResult
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If
        End Try
    End Function
End Class








