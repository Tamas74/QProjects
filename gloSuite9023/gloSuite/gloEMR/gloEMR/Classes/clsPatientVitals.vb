


Imports System.Data.SqlClient


Public Class clsPatientVitals
    '********************************
    '  Private da As SqlDataAdapter
    ' Private ds As New DataSet
    '  Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    ' Private conString As String
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
    Public Sub New()
        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Con = New System.Data.SqlClient.SqlConnection(sqlconn)

        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        Return ds
    '    End Get
    'End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            Return dv
        End Get
    End Property


    Public Function CheckDuplicate(ByVal ID As Long, ByVal CPTCode As String, ByVal CategoryID As Int64) As Boolean

        Try
            Dim cmd As New SqlCommand("gsp_CheckPatientHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@CPTCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CPTCode

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryID

            Con.Open()
            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
        End Try
    End Function

    Public Function IsVitalPresent(PatientID As Long, VitalDate As Date) As Boolean

        Dim blnResult As Boolean
        Dim cmdMain As SqlCommand = Nothing
        Dim sqlParam As SqlParameter

        Try

            cmdMain = New SqlCommand("gsp_IsVitalPresent", Con)
            cmdMain.CommandType = CommandType.StoredProcedure

            sqlParam = cmdMain.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            sqlParam = cmdMain.Parameters.Add("@dtVitalDate", SqlDbType.Date)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VitalDate

            Con.Open()

            blnResult = cmdMain.ExecuteScalar

            Return blnResult

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            If IsNothing(cmdMain) = False Then
                cmdMain.Parameters.Clear()
                cmdMain.Dispose()
                cmdMain = Nothing
            End If

            sqlParam = Nothing
            Con.Close()

        End Try

    End Function

    ' For Edit 
    Public Function SelectPatientVital(ByVal VitalID As Long) As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_ScanVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@VitalID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VitalID

            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            da.Dispose()
            da = Nothing

            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
           
        End Try
    End Function
    Public Function GetVitalsODI(ByVal VitalID As Long) As DataTable
        'Dim objBusLayer As New clsBuslayer
        Dim objCmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()
            Dim objCon As New SqlConnection
            Dim _strSQL As String = ""
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            _strSQL = "SELECT sSectionName,sSectionValue from VitalsODI where nVitalID = " & VitalID

            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = _strSQL
            objCmd.Connection = objCon

            objCon.Open()
            objCmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = objCmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            da.Dispose()
            da = Nothing
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            'Con.Close()
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientDOB(ByVal PatientID As Long) As Date
        Dim cmd As SqlCommand = Nothing
        Try
            Dim strQry As String = "Select dtDOB from Patient where nPatientID = '" & PatientID & "'"
            Dim _DOB As Date
            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            _DOB = cmd.ExecuteScalar()
            Return _DOB
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function GetAllVitals(ByVal nPatientID As Long) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID
            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            dv = New DataView(dt)  ''code change for bugid 72167
            Return dv

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
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
    ''Added Code Changes for View OBVitals
    Public Function GetAllOBVitals(ByVal nPatientID As Long) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewOBVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID
            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            dv = New DataView(dt)  ''code change for bugid 72167
            Return dv

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
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
    ''Added Code Changes for OBVitals Comments
    Public Function GetAllOBVitalsComments(ByVal OBVitalsAckNotes_ID As Long, ByVal OBVitalsAckNotes_Type As Integer) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            cmd = New SqlCommand("OBVitals_SelectNotesBy_ID_Type", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@OBVitalsAckNotes_ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = OBVitalsAckNotes_ID

            sqlParam = cmd.Parameters.Add("@OBVitalsAckNotes_Type", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = OBVitalsAckNotes_Type

            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            dv = New DataView(dt)
            Return dv

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals Comments", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
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
    'Public Function AddODIAnswers(ByVal nVitalID As Long, ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal dtVitalDate As DateTime, ByVal ArrList As ArrayList)
    '    Try

    '            Dim cmd As New SqlCommand("gsp_InUpVitalsODI", Con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            Dim sqlParam As SqlParameter
    '            sqlParam = cmd.Parameters.Add("@nVitalID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = nVitalID

    '            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = nVisitID

    '            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = nPatientID

    '            sqlParam = cmd.Parameters.Add("@dtVitalDate", SqlDbType.DateTime)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = dtVitalDate

    '            sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = gstrLoginName

    '            sqlParam = cmd.Parameters.Add("@MachineID", GetPrefixTransactionID)
    '            sqlParam.Direction = ParameterDirection.Input

    '            sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = 1

    '        Dim lst As myList
    '        For i As Int16 = 0 To ArrList.Count - 1
    '            lst = New myList
    '            lst = CType(ArrList(i), myList)

    '            sqlParam = cmd.Parameters.Add("@Section" + lst.Code.ToString(), SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = lst.Code

    '            sqlParam = cmd.Parameters.Add("@Section" + lst.Code.ToString() + "_Val", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = lst.Description
    '        Next

    '        If Con.State <> ConnectionState.Open Then
    '            Con.Open()
    '        End If
    '        cmd.ExecuteNonQuery()

    '        cmd.Dispose()
    '        cmd = Nothing

    '        sqlParam = Nothing
    '        lst = Nothing

    '        Return True

    '    Catch ex As Exception
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    ''Added by Mayuri:20110105-for saving ODI answers in database
    Public Function AddODIAnswers(ByVal nVitalID As Long, ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal dtVitalDate As DateTime, ByVal ArrList As ArrayList)

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            Dim lst As myList
            For i As Int16 = 0 To ArrList.Count - 1
                'lst = New myList
                lst = CType(ArrList(i), myList)
                cmd = New SqlCommand("gsp_InUpVitalsODI", Con)
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

                sqlParam = cmd.Parameters.Add("@dtVitalDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = dtVitalDate

                sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gstrLoginName

                sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
                sqlParam.Direction = ParameterDirection.Input


                sqlParam = cmd.Parameters.Add("@sSectionName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.Code

                sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@sSectionValue", SqlDbType.VarChar)


                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = lst.Description
                If Con.State <> ConnectionState.Open Then


                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            Next
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
    Public Function AddNewVitals(ByVal nVitalID As Long, ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal dtVitalDate As Date, ByVal sHeight As String, ByVal dWeightinlbs As Decimal, ByVal dWeightChange As Decimal, ByVal dBMI As Decimal, ByVal dWeightinKg As Decimal, ByVal dTemperature As Decimal, ByVal dRespiratoryRate As Decimal, ByVal dPulsePerMinute As Decimal, ByVal dPulseOx As Decimal, ByVal dBloodPressureSittingMin As Decimal, ByVal dBloodPressureSittingMax As Decimal, ByVal dBloodPressureStandingMin As Decimal, ByVal dBloodPressureStandingMax As Decimal, ByVal sComments As String, ByVal dHeadCircumferance As Decimal, ByVal dStature As Decimal, ByVal dTHRPerMax As Decimal, ByVal dTHRMax As Decimal, ByVal dTHRPerMin As Decimal, ByVal dTHRMin As Decimal, ByVal dTHR As Decimal, ByVal dHeightinInch As Decimal, ByVal dHeightinCm As Decimal, ByVal sWeightinLbsOz As String, ByVal dTemperatureinCelcius As Decimal, ByVal nPainLevel As Int16, ByVal dPEFR1 As Decimal, ByVal dPEFR2 As Decimal, ByVal dPEFR3 As Decimal, ByVal dHeadCircuminInch As Decimal, ByVal dStatureinInch As Decimal, ByVal sSiteforbloodpressure As String, ByVal dtLastMenstrualPeriod As Date, ByVal dNeckCircuminCm As Decimal, ByVal dNeckCircuminInch As Decimal, ByVal dLeftEyePressure As Decimal, ByVal dRightEyePressure As Decimal, ByVal bStatusLMPeriod As Boolean, ByVal nPainLevelWithMedication As Int16, ByVal nPainLevelWithoutMedication As Int16, ByVal nPainLevelWorst As Int16, ByVal nODIPercent As Int64, ByVal nDAS28 As Single, ByVal dPulseOxSupplement As Decimal, ByVal dPulseRate As Decimal, Optional ByVal blnpainlvl As Boolean = True, Optional ByVal blnpainlvlWithMedication As Boolean = True, Optional ByVal blnpainlvlWithoutMedication As Boolean = True, Optional ByVal blnpainlvlWorst As Boolean = True, Optional ByVal nTotalPregnancies As Int16 = 0, Optional ByVal nFullTermDeliveries As Int16 = 0, Optional ByVal nLivingChildren As Int16 = 0, Optional ByVal nMultipleBirths As Int16 = 0, Optional ByVal nPremature As Int16 = 0, Optional ByVal nAbortionsInduced As Int16 = 0, Optional ByVal nAbortionsSpontaneous As Int16 = 0, Optional ByVal nEctopics As Int16 = 0, Optional ByVal nBMIPercentile As Int16 = 0) As Long

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Dim sqlParamId As SqlParameter
        Try
            cmd = New SqlCommand("gsp_InUpVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure

            '@nVitalID,@nVisitID,@nPatientID,@dtVitalDate,@dHeight,@dWeightinlbs,@dWeightChange,@dBMI,@dWeightinKg,@dTemperature,@dRespiratoryRate,@dPulsePerMinute,@dPulseOx,@dBloodPressureSittingMin,@dBloodPressureSittingMax,@dBloodPressureStandingMin,@dBloodPressureStandingMax,@sComments

            sqlParamId = cmd.Parameters.AddWithValue("@nVitalID", nVitalID)
            sqlParamId.Direction = ParameterDirection.InputOutput

            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitID

            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            sqlParam = cmd.Parameters.Add("@dtVitalDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtVitalDate

            sqlParam = cmd.Parameters.Add("@sHeight", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If sHeight = "" Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = sHeight
            End If


            sqlParam = cmd.Parameters.Add("@dWeightinlbs", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dWeightinlbs = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dWeightinlbs
            End If


            sqlParam = cmd.Parameters.Add("@dWeightChange", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dWeightChange = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dWeightChange
            End If


            '@dBMI,@dWeightinKg,@dTemperature,@dRespiratoryRate,@dPulsePerMinute,@dPulseOx,
            sqlParam = cmd.Parameters.Add("@dBMI", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dBMI = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dBMI
            End If



            sqlParam = cmd.Parameters.Add("@dWeightinKg", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dWeightinKg = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dWeightinKg
            End If


            sqlParam = cmd.Parameters.Add("@dTemperature", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTemperature = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTemperature
            End If


            sqlParam = cmd.Parameters.Add("@dRespiratoryRate", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dRespiratoryRate = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dRespiratoryRate
            End If


            sqlParam = cmd.Parameters.Add("@dPulsePerMinute", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPulsePerMinute = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPulsePerMinute
            End If


            sqlParam = cmd.Parameters.Add("@dPulseOx", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPulseOx = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPulseOx
            End If



            '@dBloodPressureSittingMin,@dBloodPressureSittingMax,@dBloodPressureStandingMin,
            '@dBloodPressureStandingMax,@sComments
            sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMin", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dBloodPressureSittingMin = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dBloodPressureSittingMin
            End If


            sqlParam = cmd.Parameters.Add("@dBloodPressureSittingMax", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dBloodPressureSittingMax = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dBloodPressureSittingMax
            End If


            sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMin", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dBloodPressureStandingMin = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dBloodPressureStandingMin
            End If


            sqlParam = cmd.Parameters.Add("@dBloodPressureStandingMax", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dBloodPressureStandingMax = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dBloodPressureStandingMax
            End If


            sqlParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sComments

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dHeadCircumferance", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dHeadCircumferance = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dHeadCircumferance
            End If


            sqlParam = cmd.Parameters.Add("@dStature", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dStature = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dStature
            End If


            sqlParam = cmd.Parameters.Add("@dTHRperMax", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTHRPerMax = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTHRPerMax
            End If

            sqlParam = cmd.Parameters.Add("@dTHRMax", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTHRMax = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTHRMax
            End If

            sqlParam = cmd.Parameters.Add("@dTHRperMin", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTHRPerMin = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTHRPerMin
            End If

            sqlParam = cmd.Parameters.Add("@dTHRMin", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTHRMin = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTHRMin
            End If

            sqlParam = cmd.Parameters.Add("@dTHR", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTHR = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTHR
            End If

            ''Sudhir - 20090124 ''
            sqlParam = cmd.Parameters.Add("@dHeightinInch", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dHeightinInch = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dHeightinInch
            End If

            sqlParam = cmd.Parameters.Add("@dHeightinCm", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dHeightinCm = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dHeightinCm
            End If

            sqlParam = cmd.Parameters.Add("@sWeightinLbsOz", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            If sWeightinLbsOz = Nothing Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = sWeightinLbsOz
            End If

            sqlParam = cmd.Parameters.Add("@dTemperatureinCelcius", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dTemperatureinCelcius = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dTemperatureinCelcius
            End If
            ''Start :: Painlevel checkBox Boolean
            If blnpainlvl = False Then
                sqlParam = cmd.Parameters.Add("@nPainLevel", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam = cmd.Parameters.Add("@nPainLevel", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nPainLevel
            End If
            ''End :: Painlevel checkBox Boolean

            sqlParam = cmd.Parameters.Add("@dPEFR1", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPEFR1 = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPEFR1
            End If

            sqlParam = cmd.Parameters.Add("@dPEFR2", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPEFR2 = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPEFR2
            End If

            sqlParam = cmd.Parameters.Add("@dPEFR3", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPEFR3 = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPEFR3
            End If

            sqlParam = cmd.Parameters.Add("@dHeadCircuminInch", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dHeadCircuminInch = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dHeadCircuminInch
            End If

            sqlParam = cmd.Parameters.Add("@dStatureinInch", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dStatureinInch = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dStatureinInch
            End If

            '' End Sudhir ''
            ''Added by Mayuri:20100608
            sqlParam = cmd.Parameters.Add("@sSiteForBloodPressure", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sSiteforbloodpressure

            sqlParam = cmd.Parameters.Add("@dtLastMenstrualPeriod", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtLastMenstrualPeriod
            ''End code Added by Mayuri:20100608
            ''Added on 20100612-Mayuri
            sqlParam = cmd.Parameters.Add("@dNeckCircuminCm", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dNeckCircuminCm = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dNeckCircuminCm
            End If

            sqlParam = cmd.Parameters.Add("@dNeckCircuminInch", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dNeckCircuminInch = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dNeckCircuminInch
            End If

            sqlParam = cmd.Parameters.Add("@dLeftEyePressure", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dLeftEyePressure = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dLeftEyePressure
            End If

            sqlParam = cmd.Parameters.Add("@dRightEyePressure", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dRightEyePressure = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dRightEyePressure
            End If
            sqlParam = cmd.Parameters.Add("@bStatusLMPeriod", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bStatusLMPeriod

            '' chetan added for Patient Taracking maintaining user name on 18-oct-2010
            sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName


            ''End
            ''Added by Mayuri:20101227
            If blnpainlvlWithMedication = False Then
                sqlParam = cmd.Parameters.Add("@nPainLevelWithMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam = cmd.Parameters.Add("@nPainLevelWithMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nPainLevelWithMedication
            End If

            If blnpainlvlWithoutMedication = False Then
                sqlParam = cmd.Parameters.Add("@nPainLevelWithoutMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam = cmd.Parameters.Add("@nPainLevelWithoutMedication", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nPainLevelWithoutMedication
            End If
            If blnpainlvlWorst = False Then
                sqlParam = cmd.Parameters.Add("@nPainLevelWorst", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam = cmd.Parameters.Add("@nPainLevelWorst", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nPainLevelWorst
            End If

            sqlParam = cmd.Parameters.Add("@nODIPercent", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If frmPatientVitals._IsODISettingOn = False Then
                If nODIPercent = 0 Then
                    sqlParam.Value = System.DBNull.Value
                Else
                    sqlParam.Value = nODIPercent
                End If
            Else
                sqlParam.Value = nODIPercent
            End If

            ''
            ''DAS 28
            sqlParam = cmd.Parameters.Add("@nDAS28", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If frmPatientVitals._IsDAS28SettingOn = False Then
                If nDAS28 = 0 Then
                    sqlParam.Value = System.DBNull.Value
                Else
                    sqlParam.Value = nDAS28
                End If
            Else
                sqlParam.Value = nDAS28
            End If
            ''
            'By Pranit on 17 july 2012
            sqlParam = cmd.Parameters.Add("@dPulseOxSupplement", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPulseOxSupplement = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPulseOxSupplement
            End If

            sqlParam = cmd.Parameters.Add("@dPulseRate", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Input
            If dPulseRate = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = dPulseRate
            End If


            sqlParam = cmd.Parameters.Add("@nTotalPregnancies", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nTotalPregnancies

            sqlParam = cmd.Parameters.Add("@nFullTermDeliveries", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nFullTermDeliveries

            sqlParam = cmd.Parameters.Add("@nLivingChildren", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nLivingChildren

            sqlParam = cmd.Parameters.Add("@nMultipleBirths", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nMultipleBirths

            sqlParam = cmd.Parameters.Add("@nPremature", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPremature

            sqlParam = cmd.Parameters.Add("@nAbortionsInduced", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nAbortionsInduced

            sqlParam = cmd.Parameters.Add("@nAbortionsSpontaneous", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nAbortionsSpontaneous

            sqlParam = cmd.Parameters.Add("@nEctopics", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nEctopics


            sqlParam = cmd.Parameters.Add("@nBMIPercentile", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            If nBMIPercentile = 0D Then
                sqlParam.Value = System.DBNull.Value
            Else
                sqlParam.Value = nBMIPercentile
            End If

            'End by pranit on 17 july 2012



            Con.Open()
            cmd.ExecuteNonQuery()

            If nVitalID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Vital Modified", nPatientID, nVitalID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                nVitalID = sqlParamId.Value
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Vital Added", nPatientID, nVitalID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            sqlParamId = Nothing

            Return nVitalID

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
            sqlParamId = Nothing
        End Try
    End Function

    Public Function DeleteVitals(ByVal VitalID As Long, ByVal PatientID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_DeleteVitals", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@VitalID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VitalID

            Con.Open()
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "Vital Deleted", PatientID, VitalID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
        Return Nothing
    End Function
    '*********************************
    Public Function GetPrevWeight(ByVal VitalID As Long, ByVal PatientID As Long, ByVal VitalDate As DateTime) As Decimal

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            Dim dWeightinLbs As Decimal

            cmd = New SqlCommand("gsp_GetPrevWeight", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@VitalID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VitalID

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            sqlParam = cmd.Parameters.Add("@VitalDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VitalDate

            sqlParam = cmd.Parameters.Add("@dWeightinLbs", SqlDbType.Float)
            sqlParam.Direction = ParameterDirection.Output

            Con.Open()
            cmd.ExecuteNonQuery()
            dWeightinLbs = sqlParam.Value()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            Return dWeightinLbs

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
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

    Public Function GetPrevObHistory(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter

        Try
            Dim strQRY As String
            strQRY = "SELECT top 1 nTotalPregnancies,nFullTermDeliveries,nLivingChildren,nMultipleBirths,nPremature,nAbortionsInduced,nAbortionsSpontaneous ,nEctopics FROM Vitals WHERE nPatientID= " & PatientID & " Order by dtVitalDate desc"
            cmd = New SqlCommand(strQRY, Con)
            Con.Open()
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dt)

            Return dt


        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient OB Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function GetPrevHeight(ByVal PatientID As Long) As String
        Dim cmd As SqlCommand = Nothing

        Try
            ' Dim dWeightinLbs As Decimal
            Dim sObj As Object
            Dim strHeight As String
            Dim strQRY As String

            strQRY = "SELECT top 1 sHeight FROM Vitals WHERE nPatientID= " & PatientID & " Order by dtVitalDate desc"
            cmd = New SqlCommand(strQRY, Con)

            Con.Open()
            sObj = cmd.ExecuteScalar()
            If Not IsNothing(sObj) Then
                strHeight = sObj.ToString()
            Else
                strHeight = ""
            End If

            If Not IsNothing(sObj) Then
                sObj = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return strHeight

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            Con.Close()
        End Try
    End Function
    Public Function Fill_LockPatientVitals(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Dim Cmd As New SqlCommand
        Dim objParam As SqlParameter
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Con)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)

            Con.Close()
            Return dt
        Catch ex As Exception
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function

    Public Function SaveVitalNorms(ByVal oVitalNorms As gloStream.Vitals.Supportings.VitalNorms, ByVal VitalNormEnabled As Boolean) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim query As String = ""
        Dim oTrans As SqlTransaction = Nothing
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            oTrans = Con.BeginTransaction()
            If oVitalNorms.Count > 0 Then
                query = " DELETE FROM VitalNorms "
                cmd = New SqlCommand()
                cmd.Connection = Con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = query
                cmd.Transaction = oTrans
                cmd.ExecuteNonQuery()

                Dim objSettings As New clsSettings
                If (VitalNormEnabled) Then
                    objSettings.AddSetting("VITAL_NORMS_ENABLED", "TRUE", gClinicID, 0, gloSettings.SettingFlag.Clinic)
                Else
                    objSettings.AddSetting("VITAL_NORMS_ENABLED", "FALSE", gClinicID, 0, gloSettings.SettingFlag.Clinic)
                End If

                objSettings.Dispose()
                objSettings = Nothing
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
                cmd = New SqlCommand()
                cmd.Connection = Con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "gsp_InVitalNorms"
                cmd.Transaction = oTrans

                For i As Integer = 1 To oVitalNorms.Count
                    cmd.Parameters.Clear()

                    cmd.Parameters.Add("@sNorm", SqlDbType.VarChar)
                    cmd.Parameters.Add("@nVitalType", SqlDbType.Int)
                    cmd.Parameters.Add("@dMinVal", SqlDbType.Decimal)
                    cmd.Parameters.Add("@dMaxVal", SqlDbType.Decimal)
                    cmd.Parameters.Add("@nFromAge", SqlDbType.BigInt)
                    cmd.Parameters.Add("@nToAge", SqlDbType.BigInt)
                    cmd.Parameters.Add("@sGender", SqlDbType.VarChar)


                    cmd.Parameters("@sNorm").Value = oVitalNorms(i).NormName
                    cmd.Parameters("@nVitalType").Value = oVitalNorms(i).VitalType.GetHashCode
                    cmd.Parameters("@dMinVal").Value = oVitalNorms(i).MinValue
                    cmd.Parameters("@dMaxVal").Value = oVitalNorms(i).MaxValue
                    cmd.Parameters("@nFromAge").Value = oVitalNorms(i).FromAge
                    cmd.Parameters("@nToAge").Value = oVitalNorms(i).ToAge
                    cmd.Parameters("@sGender").Value = oVitalNorms(i).Gender


                    cmd.ExecuteNonQuery()
                Next
                oTrans.Commit()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Save Vital Norms", gloAuditTrail.ActivityOutCome.Success)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            oTrans.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Error while Saving Vital Norms - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(oTrans) Then
                oTrans.Dispose()
                oTrans = Nothing
            End If
        End Try
    End Function

    Public Function CheckMedicalCatAtRisk(ByVal nPatientID As Long) As Boolean

        Dim cmd As SqlCommand = Nothing
        Dim query As String = ""
        Dim objParam As SqlParameter
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            cmd = New SqlCommand()
            cmd.Parameters.Clear()
            cmd.Connection = Con
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_GetMedicalCategoryAtRisk_Patient"
          
            objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nPatientID




            Dim obj As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            If (obj > 0) Then
                Return True
            Else
                Return False
            End If

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Save Vital Norms", gloAuditTrail.ActivityOutCome.Success)
            Return True

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Error while Saving Vital Norms - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function


    Public Function GetNorms(Optional ByVal normName As String = "") As gloStream.Vitals.Supportings.VitalNorms
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim query As String = ""
        Dim oVitalNorms As New gloStream.Vitals.Supportings.VitalNorms
        Dim oVitalNorm As gloStream.Vitals.Supportings.VitalNorm = Nothing
        Dim dtNorms As DataTable = Nothing
        Try
            query = " SELECT ISNULL(sNorm,'') AS sNorm, ISNULL(nVitalType,0) AS nVitalType, ISNULL(dMinVal,0) AS dMinVal, " _
                    & " ISNULL(dMaxVal,0) AS dMaxVal, ISNULL(nFromAge,0) AS nFromAge, ISNULL(nToAge,0) AS nToAge, ISNULL(sGender,'') AS sGender FROM VitalNorms "

            If normName <> "" Then
                query = query + " WHERE sNorm = '" & normName & "' "
            End If

            oDB.Connect(False)
            oDB.Retrive_Query(query, dtNorms)

            If IsNothing(dtNorms) = False Then
                For iRow As Integer = 0 To dtNorms.Rows.Count - 1
                    oVitalNorm = New gloStream.Vitals.Supportings.VitalNorm
                    oVitalNorm.NormName = dtNorms.Rows(iRow)("sNorm").ToString
                    oVitalNorm.VitalType = CType(dtNorms.Rows(iRow)("nVitalType"), gloStream.Vitals.Supportings.VitalTypes)
                    oVitalNorm.MinValue = Convert.ToDouble(dtNorms.Rows(iRow)("dMinVal"))
                    oVitalNorm.MaxValue = Convert.ToDouble(dtNorms.Rows(iRow)("dMaxVal"))
                    oVitalNorm.FromAge = Convert.ToInt16(dtNorms.Rows(iRow)("nFromAge"))
                    oVitalNorm.ToAge = Convert.ToInt16(dtNorms.Rows(iRow)("nToAge"))
                    oVitalNorm.Gender = dtNorms.Rows(iRow)("sGender").ToString
                    oVitalNorms.Add(oVitalNorm)
                    oVitalNorm = Nothing
                Next
            End If

            Return oVitalNorms
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oVitalNorms) Then
                oVitalNorms = Nothing
            End If
            If Not IsNothing(oVitalNorm) Then
                oVitalNorm = Nothing
            End If
            If Not IsNothing(dtNorms) Then
                dtNorms.Dispose()
                dtNorms = Nothing
            End If
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
    Public Function GetNormNames() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim query As String = ""
        Dim dtNormNames As DataTable = Nothing
        Try
            query = " SELECT DISTINCT sNorm, nFromAge, nToAge FROM VitalNorms ORDER BY nFromAge "
            oDB.Connect(False)
            oDB.Retrive_Query(query, dtNormNames)
            If IsNothing(dtNormNames) = False Then
                Return dtNormNames
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If Not IsNothing(dtNormNames) Then
            '    dtNormNames.Dispose()
            '    dtNormNames = Nothing
            'End If
        End Try
    End Function
End Class


Namespace gloStream
    Namespace Vitals
        Public Class Supportings

            Enum VitalTypes
                None = 0
                Height = 1
                Weight = 2
                RespiratoryRate = 3
                PulsePerMinute = 4
                PulseOX = 5
                BPSystolic = 6
                BPDiastolic = 7
                Temperature = 8
                HeadCircumference = 9
                Stature = 10
                NeckCircumference = 11
            End Enum

            Public Class VitalNorm
                Private _NormName As String = ""
                Private _VitalType As VitalTypes
                Private _MinValue As Double
                Private _MaxValue As Double
                Private _FromAge As Integer
                Private _ToAge As Integer
                Private _Gender As String


                Public Property NormName() As String
                    Get
                        Return _NormName
                    End Get
                    Set(ByVal value As String)
                        _NormName = value
                    End Set
                End Property
                Public Property VitalType() As VitalTypes
                    Get
                        Return _VitalType
                    End Get
                    Set(ByVal value As VitalTypes)
                        _VitalType = value
                    End Set
                End Property
                Public Property MinValue() As Double
                    Get
                        Return _MinValue
                    End Get
                    Set(ByVal value As Double)
                        _MinValue = value
                    End Set
                End Property
                Public Property MaxValue() As Double
                    Get
                        Return _MaxValue
                    End Get
                    Set(ByVal value As Double)
                        _MaxValue = value
                    End Set
                End Property
                Public Property FromAge() As Integer
                    Get
                        Return _FromAge
                    End Get
                    Set(ByVal value As Integer)
                        _FromAge = value
                    End Set
                End Property
                Public Property ToAge() As Integer
                    Get
                        Return _ToAge
                    End Get
                    Set(ByVal value As Integer)
                        _ToAge = value
                    End Set
                End Property
                Public Property Gender() As String
                    Get
                        Return _Gender
                    End Get
                    Set(ByVal value As String)
                        _Gender = value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class VitalNorms
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oVitalNorm As VitalNorm) As VitalNorm
                    mCol.Add(oVitalNorm)
                    Return Nothing
                End Function

                Public Function Add(ByVal NormName As String, ByVal VitalType As VitalTypes, ByVal MinValue As Double, ByVal MaxValue As Double, ByVal FromAge As Integer, ByVal ToAge As Integer, ByVal Gender As String) As VitalNorm
                    'create a new object
                    Dim oVitalNorm As VitalNorm
                    Try
                        oVitalNorm = New VitalNorm
                        oVitalNorm.NormName = NormName
                        oVitalNorm.VitalType = VitalType
                        oVitalNorm.MinValue = MinValue
                        oVitalNorm.MaxValue = MaxValue
                        oVitalNorm.FromAge = FromAge
                        oVitalNorm.ToAge = ToAge
                        oVitalNorm.Gender = Gender
                        mCol.Add(oVitalNorm)
                        Add = oVitalNorm
                        oVitalNorm = Nothing
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Add = Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As VitalNorm
                    Get
                        Item = mCol.Item(vntIndexKey)
                    End Get
                End Property

                Public ReadOnly Property Count() As Integer
                    Get
                        Count = mCol.Count()
                    End Get
                End Property

                Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                    'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                    'GetEnumerator = mCol.GetEnumerator
                    Return Nothing
                End Function

                Public Sub Remove(ByRef vntIndexKey As Object)
                    mCol.Remove(vntIndexKey)
                End Sub

                Public Sub New()
                    MyBase.New()
                    mCol = New Collection
                End Sub

                Protected Overrides Sub Finalize()
                    Clear()
                    mCol = Nothing
                    MyBase.Finalize()
                End Sub

                Public Sub Clear()
                    If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.

                    Dim i As Short
                    For i = mCol.Count() To 1 Step -1
                        mCol.Remove(i)
                    Next i
                End Sub

            End Class

        End Class
    End Namespace
End Namespace