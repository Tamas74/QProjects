Imports System.Data.SqlClient
Imports System.Linq
Public Class gloCCDDatabaseLayer
    Implements IDisposable

    ' Dim ConnectionString As String = "Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Initial Catalog=gloCCD;Data Source=dev01"
    Dim ConnectionString As String = gloLibCCDGeneral.Connectionstring
    Dim _VisitID As Int64 = 0
    Private _ClinicID As Int64 = 1
    Dim _FromDate As String
    Dim _ToDate As String
    Private _databaseConnectionString As String = String.Empty
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Public Property VisitID() As String
        Get

            Return _VisitID
        End Get
        Set(ByVal Value As String)
            _VisitID = Value
        End Set
    End Property
    Public Property FromDate() As String
        Get

            Return _FromDate
        End Get
        Set(ByVal Value As String)
            _FromDate = Value
        End Set
    End Property
    Public Property ToDate() As String
        Get

            Return _ToDate
        End Get
        Set(ByVal Value As String)
            _ToDate = Value
        End Set
    End Property
    Private disposedValue As Boolean = False        ' To detect redundant calls
    'Private opatient As cl


    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

    Public Function GetMUReportCount() As Integer

        Dim intCount As Integer
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gspGetMUReportCount"
            intCount = cmd.ExecuteScalar

            Return intCount

        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
            Return Nothing

        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If

        End Try

    End Function

    Public Function getCode(ByVal Description As String, ByVal Category As String) As String
        Dim ogloDB As New gloDataBase
        Dim strCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select sCode from CCD_Codes where sDescription = " & "'" & Description & "' and sCategory = " & "'" & Category & "'"

            ogloDB.Connect(ConnectionString)
            strCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function


    'to retrieve the language spoken using the code
    Public Function getDescription(ByVal Code As String, ByVal Category As String) As String
        Dim ogloDB As New gloDataBase
        Dim strCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select sdescription from CCD_Codes where scode = " & "'" & Code & "' and sCategory = " & "'" & Category & "'"

            ogloDB.Connect(ConnectionString)
            strCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function


    Public Function getLanguageCountryCode(ByVal Language As String, ByVal Country As String) As String
        Dim ogloDB As New gloDataBase
        Dim strLanguageCountryCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select ((select scode from ccd_codes where sdescription = " & "'" & Language & "'" & "  and scategory = 'Languages')  + '-' + (select scode from ccd_codes where sdescription = '" & Country & "' and scategory = 'countries')) AS SSCode "

            ogloDB.Connect(ConnectionString)
            strLanguageCountryCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strLanguageCountryCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function


    Public Function getMaritalStatus(ByVal Description As String) As String
        Dim ogloDB As New gloDataBase
        Dim strMaritalStatus As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select code from MaritalStatus where name = " & "'" & Description & "'"

            ogloDB.Connect(ConnectionString)
            strMaritalStatus = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strMaritalStatus

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try

    End Function


    Public Function getAdverseEventTypesCode(ByVal Description As String) As String
        Dim ogloDB As New gloDataBase
        Dim strAdverseEventTypesCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select code from Adverse_Event_Types where name = " & "'" & Description & "'"

            ogloDB.Connect(ConnectionString)
            strAdverseEventTypesCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strAdverseEventTypesCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function

    Public Function getProblemTypes(ByVal Description As String) As String
        Dim ogloDB As New gloDataBase
        Dim strProblemTypesCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select code  from ProblemTypes where name = " & "'" & Description & "'"

            ogloDB.Connect(ConnectionString)
            strProblemTypesCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strProblemTypesCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function

    Public Function getAdvanceDirectiveTypes(ByVal Description As String) As String
        Dim ogloDB As New gloDataBase
        Dim strAdvance_Directive_Types As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "select code from Advance_Directive_Types where name =  " & "'" & Description & "'"

            ogloDB.Connect(ConnectionString)
            strAdvance_Directive_Types = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strAdvance_Directive_Types

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function
    Public Function InsertgloEMRData(ByVal mPatient As Patient) As Boolean
        Dim oconnection As New SqlConnection
        Dim oTransaction As SqlTransaction = Nothing
        Dim ocmd As New SqlCommand

        'Dim strAdvance_Directive_Types As String = ""
        Dim strSQl As String = ""
        Dim PatientID As Int64
        Try
            'oconnection = New SqlConnection
            oconnection.ConnectionString = gloLibCCDGeneral.Connectionstring

            oconnection.Open()
            oTransaction = oconnection.BeginTransaction


            PatientID = GeneratePatientID(mPatient.DateofBirth, oTransaction, oconnection)

            ocmd = New SqlClient.SqlCommand
            ocmd.Connection = oconnection
            ocmd.Transaction = oTransaction
            ocmd.CommandType = CommandType.Text
            Dim sqlquery As String = ""
            Dim nPatCode As Int64 = 0
            sqlquery = "Select isnull(MAX(Cast(sPatientCode AS Numeric )),0) + 1  from Patient  where  ISnumeric(sPatientCode)=1"

            ocmd.CommandText = sqlquery
            nPatCode = ocmd.ExecuteScalar
            ocmd.Parameters.Clear()
            ocmd.Dispose()
            ocmd = Nothing
            sqlquery = Nothing

            ocmd = New SqlClient.SqlCommand
            ocmd.Connection = oconnection
            ocmd.CommandType = CommandType.Text
            ocmd.Transaction = oTransaction

            Select Case mPatient.Gender
                Case "F"
                    mPatient.Gender = "Female"
                Case "M"
                    mPatient.Gender = "Male"
                Case Else
                    mPatient.Gender = "Other"
            End Select

            mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, "tel", "")
            mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, ":", "")
            mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, "+", "")
            mPatient.PatientName.PersonContactPhone.Phone = Replace(mPatient.PatientName.PersonContactPhone.Phone, "-", "")



            strSQl = "insert into Patient(nPatientID,sFirstName,sMiddleName, " _
                    & " sLastName,dtDOB,sGender,sAddressLine1,sCity,sState,sZIP,sPhone,sEmail, " _
                    & " sMaritalStatus,sRace,nProviderID,sPatientCode,nSSN) values (" & PatientID & ",'" & mPatient.PatientName.FirstName & "', " _
                    & " '" & mPatient.PatientName.MiddleName & "'," _
                    & " '" & mPatient.PatientName.LastName & "','" & mPatient.DateofBirth & "','" & mPatient.Gender & "'," _
                    & " '" & mPatient.PatientName.PersonContactAddress.Street & "','" & mPatient.PatientName.PersonContactAddress.City & "'," _
                    & " '" & mPatient.PatientName.PersonContactAddress.State & "','" & mPatient.PatientName.PersonContactAddress.Zip & "'," _
                    & " '" & mPatient.PatientName.PersonContactPhone.Phone & "','" & mPatient.PatientName.PersonContactPhone.Email & "'," _
                    & " '" & mPatient.MaritalStatus & "','" & mPatient.RaceCode & "',1,'" & nPatCode & "','')"

            ocmd.CommandText = strSQl
            ocmd.ExecuteNonQuery()

            InsertPatient_RaceSpecification(PatientID, mPatient.RaceCode, 0)

            If mPatient.PatientProviders.Count > 0 Then
                For Each oProvider As PatientProvider In mPatient.PatientProviders
                    InsertProvider(PatientID, oProvider, oTransaction, oconnection)
                Next
            End If

            oTransaction.Commit()
            oconnection.Close()
            If Not IsNothing(mPatient.PatientMedications) Then
                If mPatient.PatientMedications.Count > 0 Then
                    InsertMedications(PatientID, mPatient)
                End If

            End If
            If Not IsNothing(mPatient.PatientVitals) Then
                If mPatient.PatientVitals.Count > 0 Then
                    InsertVitals(PatientID, mPatient)
                End If
            End If

            If Not IsNothing(mPatient.PatientImmunizations) Then
                If mPatient.PatientImmunizations.Count > 0 Then
                    InsertImmunizations(PatientID, mPatient)
                End If
            End If
            If Not IsNothing(mPatient.PatientAllergies) Then
                If mPatient.PatientAllergies.Count > 0 Then
                    InsertAllergies(PatientID, mPatient)
                End If
            End If
        Catch ex As gloCCDException
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
            Throw ex
        Catch ex As Exception
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(ocmd) Then
                ocmd.Parameters.Clear()
                ocmd.Dispose()
                ocmd = Nothing
            End If
            If Not IsNothing(oconnection) Then
                If oconnection.State = ConnectionState.Open Then
                    oconnection.Close()
                End If
                oconnection.Dispose()
                oconnection = Nothing
            End If
            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
            strSQl = Nothing
        End Try
        Return Nothing
    End Function


    Public Function InsertPatient_RaceSpecification(ByVal _PatientID As Int64, ByVal _sRace As String, ByVal _Flag As Boolean) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim sqlparam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd = New SqlCommand("gsp_InupPatient_RaceSpecification", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@nPatientID ", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.InputOutput
            sqlparam.Value = _PatientID

            sqlparam = cmd.Parameters.Add("@sRace ", SqlDbType.VarChar, 1000)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _sRace

            sqlparam = cmd.Parameters.Add("@Flag ", SqlDbType.Bit, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _Flag

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlparam) Then
                sqlparam = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Private Sub InsertProvider(ByVal PatientID As Int64, ByVal mpatientProvider As PatientProvider, ByVal oTransaction As SqlTransaction, ByVal oconnection As SqlConnection)
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim ID As Int64
        Dim MachineID As Int64
        Dim strSQL As String = ""
        Try
            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = oTransaction
            strSQL = "select ncontactid from contacts_mst where sfirstname ='" & mpatientProvider.FirstName & "' and slastname ='" & mpatientProvider.LastName & "'"

            cmd.CommandText = strSQL

            ID = 0
            ID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If ID = 0 Then

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction
                cmd.CommandText = "SELECT ncontactid FROM contacts_mst WHERE convert(varchar(18),ncontactid) Like convert(varchar(18)," & MachineID & " )+ '%'"

                ID = 0
                ID = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate contact ID
                If ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(ncontactid),0)+1 from contacts_mst where convert(varchar(18),ncontactid) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                ID = 0
                ID = cmd.ExecuteScalar()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                mpatientProvider.HomePhone = Replace(mpatientProvider.HomePhone, "tel", "")
                mpatientProvider.HomePhone = Replace(mpatientProvider.HomePhone, ":", "")
                mpatientProvider.HomePhone = Replace(mpatientProvider.HomePhone, "+", "")
                mpatientProvider.HomePhone = Replace(mpatientProvider.HomePhone, "-", "")


                strSQL = "Insert into Contacts_mst (ncontactid,sfirstname,slastname,sStreet,sState,sCity,sZip,sPhone,sEmail,sName,scontacttype) " _
                & " values (" & ID & ",'" & mpatientProvider.FirstName & "','" & mpatientProvider.LastName & "'," _
                & " '" & mpatientProvider.StreetAddress & "','" & mpatientProvider.State & "','" & mpatientProvider.City & "'," _
                & " '" & mpatientProvider.zip & "','" & mpatientProvider.HomePhone & "','" & mpatientProvider.Email & "','','Physician')"

                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = oTransaction

            strSQL = "Update Patient set nPCPID=" & ID & "  where nPatientId= " & PatientID & ""

            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strSQL = Nothing
        End Try

    End Sub
    Private Function GeneratePatientID(ByVal PatientDOB As Date, ByVal oTransaction As SqlClient.SqlTransaction, ByVal conn As SqlConnection) As Long
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim ID As Int64
        Dim MachineID As Int64
        Try
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = oTransaction

            MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientDOB)
            ' Check if there is already PatientId against this MachineID
            cmd.CommandText = "SELECT npatientid FROM patient WHERE convert(varchar(18),npatientid) Like convert(varchar(18)," & MachineID & " )+ '%'"

            '  cmd.Transaction = TrInsert

            'Get this ID
            ID = 0
            ID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            '--------------------------------------------------------------------
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.Transaction = oTransaction

            'Generate Patient ID
            If ID = 0 Then
                cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
            Else
                cmd.CommandText = "select isnull(max(npatientid),0)+1 from patient where convert(varchar(18),npatientid) Like convert(varchar(18)," & MachineID & " )+ '%'"
            End If

            ID = 0
            ID = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return ID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Private Function InsertVitals(ByVal PatientID As Int64, ByVal mPatient As Patient) As Boolean
        Dim oconnection As New SqlConnection
        Dim oTransaction As SqlTransaction = Nothing
        Dim ocmd As New SqlCommand

        'Dim strAdvance_Directive_Types As String = ""
        Dim strSQl As String = ""
        Dim _VisitID As Int64 = 0
        Dim VitalID As Int64 = 0
        Try

            Dim ptWeightlbs As Double = 0
            Dim ptWeightKg As Double = 0
            Dim ptHeightft As Double = 0
            Dim ptTemperature As Double = 0
            Dim ptRespiratoryrate As Double = 0
            Dim ptHeightinch As Double = 0

            For Each oVital As Vitals In mPatient.PatientVitals
                If oVital.ResultValue <> "" Then

                    Select Case oVital.ResultCode
                        'Respiratory rate

                        Case "9279-1"
                            ptRespiratoryrate = oVital.ResultValue
                            'Body Temperature
                        Case "8310-5"
                            If oVital.ResultUnit = "Cel" Then
                                ptTemperature = (9 / 5) * oVital.ResultValue + 32
                            ElseIf oVital.ResultUnit = "[degF]" Then
                                ptTemperature = oVital.ResultValue
                            End If

                            'Body Height
                        Case "8302-2", "8306-3"
                            If oVital.ResultUnit = "m" Then
                                ptHeightft = Math.Round((oVital.ResultValue * 3.3))
                            ElseIf oVital.ResultUnit = "cm" Then
                                ptHeightft = Math.Round((oVital.ResultValue / 100) * 3.3)
                            ElseIf oVital.ResultUnit = "[in_us]" Then
                                ptHeightft = Math.Round(oVital.ResultValue / 12)
                            ElseIf oVital.ResultUnit = "[in_uk]" Then
                                ptHeightft = Math.Round(oVital.ResultValue / 12)
                            End If

                            'Body Weight
                        Case "3141-9"
                            If oVital.ResultUnit = "kg" Then
                                ptWeightKg = oVital.ResultValue
                                ptWeightlbs = ptWeightKg * 2.2
                            ElseIf oVital.ResultUnit = "g" Or oVital.ResultUnit = "gm" Then
                                ptWeightKg = oVital.ResultValue / 1000
                                ptWeightlbs = ptWeightKg * 2.2
                            ElseIf oVital.ResultUnit = "lbs" Then
                                ptWeightKg = oVital.ResultValue / 2.2
                                ptWeightlbs = oVital.ResultValue
                            ElseIf oVital.ResultUnit = "ounce" Then
                                ptWeightKg = oVital.ResultValue * 0.0283495231
                                ptWeightlbs = ptWeightKg * 2.2
                            End If

                    End Select
                End If
            Next
            If ptWeightKg > 0 AndAlso ptHeightft > 0 Then


                'oconnection = New SqlConnection
                oconnection.ConnectionString = gloLibCCDGeneral.Connectionstring

                oconnection.Open()
                oTransaction = oconnection.BeginTransaction
                _VisitID = GenerateVisitID(PatientID, oconnection, oTransaction)


                ocmd = New SqlCommand
                ocmd.Connection = oconnection
                ocmd.CommandType = CommandType.Text
                ocmd.Transaction = oTransaction

                VitalID = GenerateVitalID(PatientID, oconnection, oTransaction)

                Dim mVital As Vitals = mPatient.PatientVitals.Item(0)


                strSQl = "Insert into Vitals (nVitalID,nPatientId,nVisitID,dtVitalDate," _
                        & " sHeight,dWeightinlbs,dWeightinKg,dTemperature,dRespiratoryRate) values " _
                        & "(" & VitalID & "," & PatientID & "," & _VisitID & ",'" & mVital.ResultDate & "'," _
                        & "" & ptHeightft & " ," & ptWeightlbs & "," _
                        & "" & ptWeightKg & "," & ptTemperature & "," & ptRespiratoryrate & ")"

                ocmd.CommandText = strSQl
                ocmd.ExecuteNonQuery()
                ocmd.Parameters.Clear()
                ocmd.Dispose()
                ocmd = Nothing

                If Not IsNothing(mVital) Then
                    mVital.Dispose()
                    mVital = Nothing
                End If

                oTransaction.Commit()
                oconnection.Close()
            End If
        Catch ex As gloCCDException
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
            ex = Nothing
        Finally
            If Not IsNothing(ocmd) Then
                ocmd.Parameters.Clear()
                ocmd.Dispose()
                ocmd = Nothing
            End If
            If Not IsNothing(oconnection) Then
                If oconnection.State = ConnectionState.Open Then
                    oconnection.Close()
                End If
                oconnection.Dispose()
                oconnection = Nothing
            End If
            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
            strSQl = Nothing
        End Try
        Return Nothing
    End Function
    Private Function InsertMedications(ByVal PatientID As Int64, ByVal mPatient As Patient) As Boolean
        Dim oconnection As New SqlConnection
        Dim oTransaction As SqlTransaction = Nothing
        'Dim ocmd As New SqlCommand

        'Dim strAdvance_Directive_Types As String = ""
        'Dim strSQl As String = ""
        Dim _VisitID As Int64
        Try

            oconnection = New SqlConnection
            oconnection.ConnectionString = gloLibCCDGeneral.Connectionstring

            oconnection.Open()
            oTransaction = oconnection.BeginTransaction
            _VisitID = GenerateVisitID(PatientID, oconnection, oTransaction)

            For Each oMedication As Medication In mPatient.PatientMedications
                InsertMedication(_VisitID, PatientID, oMedication, oconnection, oTransaction)
            Next
            oTransaction.Commit()
            oconnection.Close()
        Catch ex As gloCCDException
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
        Finally
            If Not IsNothing(oconnection) Then
                If oconnection.State = ConnectionState.Open Then
                    oconnection.Close()
                End If
                oconnection.Dispose()
                oconnection = Nothing
            End If
            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If

        End Try
        Return Nothing
    End Function
    Private Function InsertImmunizations(ByVal PatientID As Int64, ByVal mPatient As Patient) As Boolean
        Dim oconnection As New SqlConnection
        Dim oTransaction As SqlTransaction = Nothing
        Dim ocmd As New SqlCommand
        Dim ImmunizationID As Int64 = 0

        'Dim strSQl As String = ""
        Dim _VisitID As Int64
        Try

            oconnection = New SqlConnection
            oconnection.ConnectionString = gloLibCCDGeneral.Connectionstring

            oconnection.Open()
            oTransaction = oconnection.BeginTransaction
            'Generate VisitID for Immunization
            _VisitID = GenerateVisitID(PatientID, oconnection, oTransaction)

            'Generate Immunization Transaction Id
            ImmunizationID = GenerateImmunizationID(PatientID, oconnection, oTransaction)

            ocmd = New SqlCommand
            ocmd.Connection = oconnection
            ocmd.CommandType = CommandType.Text
            ocmd.Transaction = oTransaction

            ocmd.CommandText = "Insert into IM_Trn_Mst (im_trn_mst_Id,im_trn_mst_nPatientID) values " _
                                & "(" & ImmunizationID & " ," & PatientID & ")"
            ocmd.ExecuteNonQuery()
            ocmd.Parameters.Clear()
            ocmd.Dispose()
            ocmd = Nothing

            Dim counterId As Int32 = 1
            For Each oImmunization As Immunization In mPatient.PatientImmunizations
                InsertImmunization(counterId, ImmunizationID, _VisitID, PatientID, oImmunization, oconnection, oTransaction)
                counterId = counterId + 1
            Next
            oTransaction.Commit()
            oconnection.Close()
        Catch ex As gloCCDException
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
            ex = Nothing
        Finally
            If Not IsNothing(ocmd) Then
                ocmd.Parameters.Clear()
                ocmd.Dispose()
                ocmd = Nothing
            End If
            If Not IsNothing(oconnection) Then
                If oconnection.State = ConnectionState.Open Then
                    oconnection.Close()
                End If
                oconnection.Dispose()
                oconnection = Nothing
            End If
            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Private Sub InsertImmunization(ByVal counterid As Int32, ByVal ImmunizationID As Int64, ByVal VisitID As Int64, ByVal PatientID As Int64, ByVal mImmunization As Immunization, ByVal oconnection As SqlConnection, ByVal otransaction As SqlTransaction)

        Dim strsql As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim im_item_Id As Int64 = 0
        Try

            im_item_Id = InsertintoImMaster(PatientID, oconnection, otransaction, mImmunization)

            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = otransaction

            strsql = "Insert into IM_Trn_Dtl (im_trn_mst_Id,im_trn_Date,im_trn_nVisitID,im_trn_ItemID," _
                    & " im_trn_CounterID,im_trn_Dategiven,im_trn_Userid) values " _
                    & "(" & ImmunizationID & ",'" & Now & "'," & VisitID & "," & im_item_Id & "," _
                    & "" & counterid & ",'" & mImmunization.ImmunizationDate & "',1)"

            cmd.CommandText = strsql
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strsql = Nothing
        End Try

    End Sub

    Private Sub InsertMedication(ByVal VisitID As Int64, ByVal PatientID As Int64, ByVal mMedication As Medication, ByVal oconnection As SqlConnection, ByVal otransaction As SqlTransaction)
        Dim medicationid As Int64
        Dim strsql As String = ""
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = otransaction

            medicationid = GenerateMedicationID(PatientID, oconnection, otransaction)

            strsql = "Insert into Medication (nMedicationId,nPatientId,nVisitID,sMedication," _
                    & " sDosage,dtStartdate,dtMedicationdate,sStatus,sReason) values " _
                    & "(" & medicationid & "," & PatientID & "," & VisitID & ",'" & mMedication.DrugName & "'," _
                    & "'" & mMedication.DrugStrength & " " & mMedication.StrengthUnits & "'," _
                    & "'" & mMedication.WrittenDate & "','" & Now.Date & "','','')"

            cmd.CommandText = strsql
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strsql = Nothing
        End Try
    End Sub
    Private Function GenerateVisitID(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction) As Int64
        Dim _ID As Int64 = 0
        Dim _VisitID As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""

        Try
            _VisitID = GetVisitID(PatientID, oconnection, oTransaction)


            If _VisitID = 0 Then
                'generate VisitID and insert a record in Visits

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                Dim CurrentDate As Date = Now.Date
                _strSQL = "SELECT Top 1 nAppointmentID AS AppointmentID FROM Appointments " _
                & " WHERE (nPatientID = " & PatientID & ") AND (CONVERT(varchar, dtAppointmentDate, 101) " _
                & " = '" & CurrentDate & "') order by dtAppointmentDate "

                cmd.CommandText = _strSQL

                Dim AppointmentId As Int64 = 0
                AppointmentId = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already VisitID against this MachineID
                cmd.CommandText = "SELECT nVisitid FROM Visits WHERE convert(varchar(18),nVisitid) Like convert(varchar(18)," & MachineID & " )+ '%'"

                '  cmd.Transaction = TrInsert

                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(nVisitid),0)+1 from Visits where convert(varchar(18),nVisitid) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                _VisitID = _ID
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                'insert into Visits
                'prepare the insert query
                _strSQL = "insert into Visits(nVisitID, nPatientID, nProviderID, nAppointmentID, dtVisitDate) values(" & _VisitID & "," & PatientID & "," & 1 & "," & AppointmentId & ",'" & Now.Date & "')"

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction
                cmd.CommandText = _strSQL

                'execute the query
                cmd.ExecuteNonQuery()

                'dispose the cmd obj
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If


            Return _VisitID
        Catch ex As Exception
            GenerateVisitID = Nothing
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function
    Private Function GenerateVitalID(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction) As Int64
        Dim _ID As Int64 = 0
        Dim VitalID As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""

        Try

            If VitalID = 0 Then

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already nVitalID against this MachineID
                cmd.CommandText = "SELECT nVitalID FROM Vitals WHERE convert(varchar(18),nVitalID) Like convert(varchar(18)," & MachineID & " )+ '%'"


                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(nVitalID),0)+1 from Vitals where convert(varchar(18),nVitalID) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                VitalID = _ID
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            End If


            Return VitalID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Private Function GenerateMedicationID(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction) As Int64
        Dim _ID As Int64 = 0
        Dim _MedicationID As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        'Dim _strSQL As String = ""

        Try


            If _MedicationID = 0 Then

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already nMedicationID against this MachineID
                cmd.CommandText = "SELECT nMedicationID FROM Medication WHERE convert(varchar(18),nVisitid) Like convert(varchar(18)," & MachineID & " )+ '%'"


                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(nMedicationID),0)+1 from Medication where convert(varchar(18),nMedicationID) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                _MedicationID = _ID
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            End If


            Return _MedicationID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Private Function GenerateImmunizationID(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction) As Int64
        Dim _ID As Int64 = 0
        Dim im_trn_mst_Id As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        'Dim _strSQL As String = ""

        Try

            If im_trn_mst_Id = 0 Then

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already im_trn_mst_Id against this MachineID
                cmd.CommandText = "SELECT im_trn_mst_Id FROM IM_Trn_Mst WHERE convert(varchar(18),im_trn_mst_Id) Like convert(varchar(18)," & MachineID & " )+ '%'"


                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(im_trn_mst_Id),0)+1 from IM_Trn_Mst where convert(varchar(18),im_trn_mst_Id) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                im_trn_mst_Id = _ID
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            End If

            Return im_trn_mst_Id
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Private Function InsertintoImMaster(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction, ByVal mImmunization As Immunization) As Int64
        Dim _ID As Int64 = 0
        Dim im_trn_mst_Id As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim im_item_Id As Int64 = 0
        Try

            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = oTransaction

            _strSQL = "select im_item_Id from IM_MST where im_item_Name='" & mImmunization.VaccineName & "'"
            cmd.CommandText = _strSQL
            im_item_Id = cmd.ExecuteScalar

            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If im_item_Id = 0 Then

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already im_item_Id against this MachineID
                cmd.CommandText = "SELECT im_item_Id FROM IM_Mst WHERE convert(varchar(18),im_item_Id) Like convert(varchar(18)," & MachineID & " )+ '%'"


                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar

                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(im_item_Id),0)+1 from IM_Mst where convert(varchar(18),IM_Mst) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                im_item_Id = _ID

                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction
                cmd.CommandText = "Insert into Im_mst (im_item_Id,im_item_Name,im_item_Count,im_vaccine_code,im_cpt_code,im_loinccode)" _
                & " values (" & im_item_Id & ",'" & mImmunization.VaccineName & "',1,'','','')"

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return im_item_Id
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function
    Private Function InsertintoHistoryMaster(ByVal PatientID As Int64, ByVal oconnection As SqlConnection, ByVal oTransaction As SqlTransaction, ByVal mAllergies As Allergies) As Int64
        Dim _ID As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim HistoryID As Int64 = 0
        Dim CategoryID As Int64 = 0
        Try



            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = oTransaction

            _strSQL = "select nHistoryID from History_MST where sDescription='" & mAllergies.ProductName & "'"
            cmd.CommandText = _strSQL
            HistoryID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            If HistoryID = 0 Then

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                _strSQL = "select nCategoryID from Category_MST where sCategoryType='History' and sDescription='Allergies'"
                cmd.CommandText = _strSQL
                CategoryID = cmd.ExecuteScalar

                MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction

                ' Check if there is already nHistoryID against this MachineID
                cmd.CommandText = "SELECT nHistoryID FROM History_MST WHERE convert(varchar(18),nHistoryID) Like convert(varchar(18)," & MachineID & " )+ '%'"


                'Get this ID
                _ID = 0
                _ID = cmd.ExecuteScalar

                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                '--------------------------------------------------------------------
                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.Transaction = oTransaction

                'Generate VisitID
                If _ID = 0 Then
                    cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
                Else
                    cmd.CommandText = "select isnull(max(nHistoryID),0)+1 from History_MST where convert(varchar(18),nHistoryID) Like convert(varchar(18)," & MachineID & " )+ '%'"
                End If

                _ID = 0
                _ID = cmd.ExecuteScalar()
                HistoryID = _ID
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                cmd = New SqlCommand
                cmd.Connection = oconnection
                cmd.CommandType = CommandType.Text
                cmd.Transaction = oTransaction
                cmd.CommandText = "Insert into History_MST (nHistoryID,sDescription,sComments,nCategoryID)" _
                & " values (" & HistoryID & ",'" & mAllergies.ProductName & "',''," & CategoryID & ")"

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return HistoryID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

    Private Function GetVisitID(ByVal PatientID As Int64, ByVal conn As SqlConnection, ByVal trInsert As SqlTransaction) As Int64
        Dim _strSQL As String = ""
        Dim _VisitID As Int64 = 0
        Dim cmd As SqlCommand = Nothing

        Try
            'prepare the query to get the visitid
            _strSQL = "select isnull(nVisitId,0) from Visits where convert(datetime,convert (varchar(50),datepart(mm,dtVisitdate)) + '/'+ convert(varchar(50),datepart(dd,dtVisitdate)) + '/'+ convert(varchar(50),datepart(yy,dtVisitdate))) = '" & Now.Date & "' and nPatientId=" & PatientID & ""

            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trInsert
            cmd.CommandText = _strSQL

            'execute the command 
            _VisitID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return _VisitID
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
            Return 0
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Private Function InsertAllergies(ByVal PatientID As Int64, ByVal mPatient As Patient) As Boolean
        Dim oconnection As New SqlConnection
        Dim oTransaction As SqlTransaction = Nothing
        Dim ocmd As New SqlCommand

        'Dim strSQl As String = ""
        Dim _VisitID As Int64
        Try

            oconnection = New SqlConnection
            oconnection.ConnectionString = gloLibCCDGeneral.Connectionstring

            oconnection.Open()
            oTransaction = oconnection.BeginTransaction

            'Generate VisitID for Allergies
            _VisitID = GenerateVisitID(PatientID, oconnection, oTransaction)

            For Each oAllergies As Allergies In mPatient.PatientAllergies
                InsertAllergy(_VisitID, PatientID, oAllergies, oconnection, oTransaction)
            Next

            oTransaction.Commit()
            oconnection.Close()
        Catch ex As gloCCDException
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            If Not IsNothing(oTransaction) Then
                oTransaction.Rollback()
            End If
            ex = Nothing
        Finally
            If Not IsNothing(ocmd) Then
                ocmd.Parameters.Clear()
                ocmd.Dispose()
                ocmd = Nothing
            End If
            If Not IsNothing(oconnection) Then
                If oconnection.State = ConnectionState.Open Then
                    oconnection.Close()
                End If
                oconnection.Dispose()
                oconnection = Nothing
            End If
            If Not IsNothing(oTransaction) Then
                oTransaction.Dispose()
                oTransaction = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Private Sub InsertAllergy(ByVal VisitID As Int64, ByVal PatientID As Int64, ByVal mAllergies As Allergies, ByVal oconnection As SqlConnection, ByVal otransaction As SqlTransaction)

        Dim strsql As String = ""
        Dim cmd As New SqlCommand
        Dim HistoryID As Int64 = 0
        Dim MachineID As Int64 = 0
        Dim _ID As Int64 = 0
        Try

            InsertintoHistoryMaster(PatientID, oconnection, otransaction, mAllergies)

            MachineID = gloLibCCDGeneral.GetPrefixTransactionID(PatientID)

            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = otransaction

            ' Check if there is already nHistoryID against this MachineID
            cmd.CommandText = "SELECT nHistoryID FROM History WHERE convert(varchar(18),nHistoryID) Like convert(varchar(18)," & MachineID & " )+ '%'"


            'Get this ID
            _ID = 0
            _ID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            '--------------------------------------------------------------------
            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.Transaction = otransaction

            'Generate VisitID
            If _ID = 0 Then
                cmd.CommandText = "select convert(numeric(18,0), convert(varchar(18)," & MachineID & ") + '01')"
            Else
                cmd.CommandText = "select isnull(max(nHistoryID),0)+1 from History where convert(varchar(18),nHistoryID) Like convert(varchar(18)," & MachineID & " )+ '%'"
            End If

            _ID = 0
            _ID = cmd.ExecuteScalar()
            HistoryID = _ID
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            cmd = New SqlCommand
            cmd.Connection = oconnection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = otransaction

            strsql = "Insert into History (nHistoryID,nVisitID,nPatientID,sHistoryCategory," _
                    & " sHistoryItem,sComments,sReaction) values " _
                    & "(" & HistoryID & "," & VisitID & "," & PatientID & ",'Allergies'," _
                    & "'" & mAllergies.ProductName & "','','" & mAllergies.AllergyType & "')"

            cmd.CommandText = strsql
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            strsql = Nothing
            'If Not IsNothing(cnn) Then
            '    If cnn.State = ConnectionState.Open Then
            '        cnn.Close()
            '    End If
            '    cnn.Dispose()
            '    cnn = Nothing
            'End If
        End Try
    End Sub

    Friend Shared Function InserttoCCDMessagelog() As Boolean
        'gloLibCCDGeneral.CCDMsgObject.Description

        Dim cmd As New SqlClient.SqlCommand
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try
            conn = New SqlConnection
            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text

            strquery = "Insert into CCD_MessageLog (nMsg_ID,dtDateTime,sDescription)" & _
                        "values('" & gloLibCCDGeneral.CCDMsgObject.MsgID & "','" & gloLibCCDGeneral.CCDMsgObject.Datetime & "'," & _
                        "'" & gloLibCCDGeneral.CCDMsgObject.Description & "'" & ")"

            cmd.CommandText = strquery
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As gloCCDException
            InserttoCCDMessagelog = Nothing
            Throw ex
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strquery = Nothing
        End Try

    End Function

    '\\added by suraj on 20081124
    ''' <summary>
    ''' this return obj of patient which reads patient information from patient table
    ''' </summary>
    ''' <param name="npatientid"></param>
    ''' <returns> patient object</returns>
    ''' <remarks></remarks>

    Public Function GetPatientInfo(ByVal npatientid As Int64) As gloCCDLibrary.Patient
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim opatient As gloCCDLibrary.Patient

        Try
            opatient = New gloCCDLibrary.Patient

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            strSQl = "select spatientcode,'sFirstName'=isnull(sFirstName,''),'sMiddleName'=isnull(sMiddleName,''),'sLastName'=isnull(sLastName,'')," _
            & " 'sGender'=isnull(sGender,''), dtDOB,'sAddressLine1'=isnull(sAddressLine1,''),'sAddressLine2'=isnull(sAddressLine2,''),'sCity'=isnull(sCity,''), " _
            & " 'sState'=isnull(sState,''),'sZip'=isnull(sZip,''),'sCounty'=isnull(sCounty,'')," _
            & " isnull(nSSN,'') as nSSN,isnull(sMaritalStatus,'') as sMaritalStatus,isnull(sPhone,'') as sPhone," _
            & " isnull(sMobile,'') as sMobile,isnull(sEmail,'') as sEmail,isnull(dbo.fn_GetRaceEthnicity(" & npatientid & ",'race','|'),'') as sRace, " _
            & " isnull(sGuardian_fName,'') as sGuardian_fName,isnull(sGuardian_mName,'') as sGuardian_mName," _
            & " isnull(sGuardian_lName,'') as sGuardian_lName,isnull(sGuardian_Address1,'') as sGuardian_Address1," _
            & " isnull(sGuardian_Address2,'') as sGuardian_Address2,isnull(sGuardian_City,'') as sGuardian_City," _
            & " isnull(sGuardian_State,'') as sGuardian_State,isnull(sGuardian_ZIP,'') as sGuardian_ZIP," _
            & " isnull(sGuardian_County,'') as sGuardian_County,isnull(sGuardian_Phone,'') as sGuardian_Phone," _
            & " isnull(sGuardian_Email,'') as sGuardian_Email,isnull(sGuardian_Country,'') as sGuardian_Country," _
            & " isnull(dbo.fn_GetRaceEthnicity(" & npatientid & ",'ethnicity','|'),'') as sEthn,isnull(sLang,'') as sLang,isnull(sCountry,'') as sCountry from Patient where nPatientId = " & npatientid & ""
            cmd.CommandText = strSQl
            Dim sqladpt As New SqlDataAdapter
            Dim _table As New DataTable

            sqladpt.SelectCommand = cmd
            sqladpt.Fill(_table)
            sqladpt.Dispose()
            sqladpt = Nothing
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    opatient.PatientName.Code = _table.Rows(0)("spatientcode")
                    opatient.PatientName.FirstName = _table.Rows(0)("sFirstName")
                    opatient.PatientName.MiddleName = _table.Rows(0)("sMiddleName")
                    opatient.PatientName.LastName = _table.Rows(0)("sLastName")
                    opatient.Gender = _table.Rows(0)("sGender")
                    opatient.DateofBirth = _table.Rows(0)("dtDOB")
                    opatient.PatientName.PersonContactAddress.Street = _table.Rows(0)("sAddressLine1")
                    opatient.PatientName.PersonContactAddress.AddressLine2 = _table.Rows(0)("sAddressLine2")
                    opatient.PatientName.PersonContactAddress.City = _table.Rows(0)("sCity")
                    opatient.PatientName.PersonContactAddress.State = _table.Rows(0)("sState")
                    opatient.PatientName.PersonContactAddress.Zip = _table.Rows(0)("sZip")
                    opatient.PatientName.PersonContactAddress.Country = _table.Rows(0)("sCountry")
                    opatient.County = _table.Rows(0)("sCounty")
                    opatient.SSN = _table.Rows(0)("nSSN")
                    opatient.MaritalStatus = _table.Rows(0)("sMaritalStatus")
                    opatient.Phone = _table.Rows(0)("sPhone")
                    opatient.Mobile = _table.Rows(0)("sMobile")
                    opatient.Email = _table.Rows(0)("sEmail")
                    opatient.Race = _table.Rows(0)("sRace")
                    opatient.Guardian_fName = _table.Rows(0)("sGuardian_fName")
                    opatient.Guardian_mName = _table.Rows(0)("sGuardian_mName")
                    opatient.Guardian_lName = _table.Rows(0)("sGuardian_lName")
                    opatient.Guardian_Address1 = _table.Rows(0)("sGuardian_Address1")
                    opatient.Guardian_Address2 = _table.Rows(0)("sGuardian_Address2")
                    opatient.Guardian_City = _table.Rows(0)("sGuardian_City")
                    opatient.Guardian_State = _table.Rows(0)("sGuardian_State")
                    opatient.Guardian_ZIP = _table.Rows(0)("sGuardian_ZIP")
                    opatient.Guardian_County = _table.Rows(0)("sGuardian_County")
                    opatient.Guardian_Phone = _table.Rows(0)("sGuardian_Phone")
                    opatient.Guardian_Email = _table.Rows(0)("sGuardian_Email")
                    opatient.Guardian_Country = _table.Rows(0)("sGuardian_Country")
                    opatient.Ethnicity = _table.Rows(0)("sEthn")
                    'Code Start-Added by kanchan on 20100916 
                    opatient.Language = _table.Rows(0)("sLang")
                    opatient.RaceCode = GetCodeForCategory(opatient.Race, "Race")
                    opatient.ethnicGroupCode = GetCodeForCategory(opatient.Ethnicity, "Ethnicity")
                    opatient.LanguageCode = GetCodeForCategory(opatient.Language, "Language")
                    'Code Start-Added by kanchan on 20100916 
                    'opatient.PatientSupport.PersonContactAddress.Country = _table.Rows(0)("sCounty")
                End If
                _table.Dispose()
                _table = Nothing
            End If


            cnn.Close()

            Return opatient

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            strSQl = Nothing
        End Try


    End Function

    Public Function GetPatientCode(ByVal nPatientID As Int64) As String

        Dim strSQl As String = ""

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim _PatientCode As String = ""

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            strSQl = "select spatientcode from Patient where nPatientId = " & nPatientID & ""
            cmd.CommandText = strSQl
            Dim _table As New DataTable
            Dim sqladpt As New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(_table)
            sqladpt.Dispose()
            sqladpt = Nothing
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    _PatientCode = _table.Rows(0)("spatientcode")
                End If
                _table.Dispose()
                _table = Nothing
            End If

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            strSQl = Nothing
        End Try
        Return _PatientCode
    End Function
    Public Function GetPatientLastName(ByVal nPatientID As Int64) As String

        Dim strSQl As String = ""

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim _PatientCode As String = ""

        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            strSQl = "select sLastName from Patient where nPatientId = " & nPatientID & ""
            cmd.CommandText = strSQl
            Dim _table As New DataTable
            Dim sqladpt As New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(_table)
            sqladpt.Dispose()
            sqladpt = Nothing
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    _PatientCode = _table.Rows(0)("sLastName")
                End If
                _table.Dispose()
                _table = Nothing
            End If

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            strSQl = Nothing
        End Try
        Return _PatientCode
    End Function

    Public Function GetPatientFamilyHistory(ByVal npatientid As Int64) As gloCCDLibrary.FamilyHistoryCol
        ' Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oFamilyHistory As gloCCDLibrary.FamilyHistory
        Dim oFamilyHistoryCol As gloCCDLibrary.FamilyHistoryCol
        Dim osqlpara As SqlParameter = Nothing

        Try
            oFamilyHistory = New gloCCDLibrary.FamilyHistory
            oFamilyHistoryCol = New gloCCDLibrary.FamilyHistoryCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientFamilyHistory"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oFamilyHistory = New FamilyHistory
                    oFamilyHistory.FmlyHxDescription = oDataReader.Item("sHistoryItem")
                    oFamilyHistory.FmlyHxQualifiers = ""
                    oFamilyHistory.FmlyHxComments = oDataReader.Item("sComments")
                    oFamilyHistory.FmlyHxConceptID = oDataReader.Item("sConceptID")
                    oFamilyHistory.FmlyHxRelation = Convert.ToString(oDataReader.Item("sRelation"))
                    oFamilyHistory.FmlyHxRelConceptID = Convert.ToString(oDataReader.Item("RelationConceptId"))
                    oFamilyHistory.FmlyHxHistoryId = Convert.ToString(oDataReader.Item("nHistoryId"))
                    If IsDBNull(oDataReader.Item("dtVisitDate")) Then
                        oFamilyHistory.FmlyHxDateReported = ""
                    Else
                        oFamilyHistory.FmlyHxDateReported = oDataReader.Item("dtVisitDate").ToString() ''''''Insurance company start date
                    End If

                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                            'Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                            'If Not IsNothing(temp) Then
                            '    'Dim temp1 As String() = temp(0).Split(":")
                            '    'If Not IsNothing(temp1) Then
                            '    '    oFamilyHistory.FmlyHxReaction = temp1(0)
                            '    '    oFamilyHistory.FmlyHxMemberId = temp1(1)
                            '    'End If
                            '    oFamilyHistory.FmlyHxStatus = temp(1)
                            '    If Not IsNothing(oFamilyHistory.FmlyHxStatus) Then
                            '        If oFamilyHistory.FmlyHxStatus.Trim() <> "Active" Then
                            '            oFamilyHistory.Dispose()
                            '            oFamilyHistory = Nothing
                            '        End If
                            '    End If
                            'End If
                        End If
                        oFamilyHistory.FmlyHxStatus = "Active"
                    End If

                    If IsNothing(oFamilyHistory) = False Then
                        oFamilyHistoryCol.Add(oFamilyHistory)
                        '            oFamilyHistory.Dispose()
                        oFamilyHistory = Nothing
                    End If

                End If
            End While
            oFamilyHistory = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oFamilyHistoryCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function


    Public Function GetPatientFamilyMember(ByVal MemberId As Long) As String()
        'Dim ogloDB As New gloDataBase
        'Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim MemberDetails() As String = New String() {"", ""}
        Dim osqlpara As SqlParameter = Nothing

        Try


            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_viewFamilyMember_MST"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nMemberID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = MemberId
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            Dim daMem As SqlDataAdapter

            daMem = New SqlDataAdapter
            daMem.SelectCommand = cmd
            Dim _table As New DataTable


            daMem.Fill(_table)
            daMem.Dispose()
            daMem = Nothing
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    MemberDetails(0) = _table.Rows(0)("Relation")
                    MemberDetails(1) = _table.Rows(0)("ConceptID")
                End If
                _table.Dispose()
                _table = Nothing
            End If

            cnn.Close()
            Return MemberDetails
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function


    Public Function GetPatientSocialHistory(ByVal npatientid As Int64) As gloCCDLibrary.SocialHistoryCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""

        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oSocialHistory As gloCCDLibrary.SocialHistory
        Dim oSocialHistoryCol As gloCCDLibrary.SocialHistoryCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oSocialHistory = New gloCCDLibrary.SocialHistory
            oSocialHistoryCol = New gloCCDLibrary.SocialHistoryCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientSocialHistory"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oSocialHistory = New SocialHistory
                    oSocialHistory.SocialHxDescription = oDataReader.Item("sHistoryItem")
                    oSocialHistory.SocialHxQualifiers = ""
                    oSocialHistory.SocialHxComments = oDataReader.Item("sComments")
                    If IsDBNull(oDataReader.Item("dtVisitDate")) Then
                        oSocialHistory.SocialHxDateReported = ""
                    Else
                        oSocialHistory.SocialHxDateReported = oDataReader.Item("dtVisitDate").ToString() ''''''Insurance company start date
                    End If
                    oSocialHistory.SocialHxConceptID = oDataReader.Item("sConceptID")

                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                            Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                            If Not IsNothing(temp) Then

                                oSocialHistory.SocialHxStatus = temp(1)
                                If Not IsNothing(oSocialHistory.SocialHxStatus) Then
                                    If oSocialHistory.SocialHxStatus.Trim() <> "Active" Then
                                        oSocialHistory.Dispose()
                                        oSocialHistory = Nothing

                                    End If
                                End If
                            End If
                        End If
                    End If
                    If IsNothing(oSocialHistory) = False Then
                        oSocialHistoryCol.Add(oSocialHistory)
                        'oSocialHistory.Dispose()
                        oSocialHistory = Nothing
                    End If

                End If
            End While
            oSocialHistory = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oSocialHistoryCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientInsuranceInfo(ByVal npatientid As Int64) As gloCCDLibrary.InsuranceCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""

        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oInsurance As gloCCDLibrary.Insurance
        Dim oInsuranceCol As gloCCDLibrary.InsuranceCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oInsurance = New gloCCDLibrary.Insurance
            oInsuranceCol = New gloCCDLibrary.InsuranceCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientInsurance"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oInsurance = New Insurance
                    oInsurance.InsSubscriberName = oDataReader.Item("sSubFName") & " " & oDataReader.Item("sSubMName") & " " & oDataReader.Item("sSubLName")
                    oInsurance.InsuranceName = oDataReader.Item("sInsuranceName")
                    oInsurance.InsuranceId = oDataReader.Item("sSubscriberId")
                    oInsurance.GroupNo = oDataReader.Item("sGroup")
                    oInsurance.InsSubsGender = oDataReader.Item("sSubscriberGender")

                    oInsurance.InsSubsAddressLine1 = oDataReader.Item("sAddressLine1") ''''''Insurance company address
                    oInsurance.InsSubsAddressLine2 = oDataReader.Item("sAddressLine2") ''''''Insurance company address
                    oInsurance.InsSubsCity = oDataReader.Item("sCity") ''''''Insurance company City
                    oInsurance.InsSubsState = oDataReader.Item("sState") ''''''Insurance company State
                    oInsurance.InsSubsZip = oDataReader.Item("sZip") ''''''Insurance company Zip
                    oInsurance.InsRelation = oDataReader.Item("sRelationShip") ''''''Insurance company Zip
                    If IsDBNull(oDataReader.Item("dtStartDate")) Then
                        oInsurance.InsStartdate = ""
                    Else
                        oInsurance.InsStartdate = oDataReader.Item("dtStartDate") ''''''Insurance company start date
                    End If
                    If IsDBNull(oDataReader.Item("dtEndDate")) Then
                        oInsurance.InsEndDate = ""
                    Else
                        oInsurance.InsEndDate = oDataReader.Item("dtEndDate") ''''''Insurance End date
                    End If

                    oInsurance.InsTypeCode = oDataReader.Item("InsTypeCode")
                    oInsurance.InsuranceType = oDataReader.Item("InsTypeDesc")
                    oInsuranceCol.Add(oInsurance)
                    'oInsurance.Dispose()
                    oInsurance = Nothing
                End If
            End While
            oInsurance = Nothing
            oDataReader.Close()
            oDataReader.Dispose()
            cnn.Close()

            Return oInsuranceCol

            'cmd.
            'strSQl = "select sFirstName, sLastName, sGender, dtDOB, sAddressLine1, sCity, sState, sZip, sCountry  from Patient where nPatientId = " & npatientid & ""
            'sqladpt.SelectCommand = cmd
            'sqladpt.Fill(_table)
            'If Not IsNothing(_table) Then
            '    opatient.PatientName.FirstName = _table.Rows(0)("sFirstName")
            '    opatient.PatientName.LastName = _table.Rows(0)("sLastName")
            '    opatient.Gender = _table.Rows(0)("sGender")
            '    opatient.DateofBirth = _table.Rows(0)("dtDOB")
            '    opatient.PatientSupport.PersonContactAddress.Street = _table.Rows(0)("sAddressLine1")
            '    opatient.PatientSupport.PersonContactAddress.City = _table.Rows(0)("sCity")
            '    opatient.PatientSupport.PersonContactAddress.State = _table.Rows(0)("sState")
            '    opatient.PatientSupport.PersonContactAddress.Zip = _table.Rows(0)("sZip")
            '    opatient.PatientSupport.PersonContactAddress.Country = _table.Rows(0)("sCountry")
            'End If
            'Return opatient
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function


    Public Function GetPatientEncounter(ByVal npatientid As Int64) As gloCCDLibrary.EncountersCol
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""

        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oEncounters As gloCCDLibrary.Encounters
        Dim oEncountersCol As gloCCDLibrary.EncountersCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oEncounters = New gloCCDLibrary.Encounters
            oEncountersCol = New gloCCDLibrary.EncountersCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientEncounter_Old"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oEncounters = New Encounters
                    oEncounters.ExamName = oDataReader.Item("sExamName")
                    oEncounters.ProvFirstName = oDataReader.Item("ProvFirstName")
                    oEncounters.ProvMName = oDataReader.Item("ProvMiddleName")
                    oEncounters.ProvLastName = oDataReader.Item("ProvLastName")
                    oEncounters.ProvSuffix = oDataReader.Item("ProvSuffix")
                    oEncounters.Location = Convert.ToString(oDataReader.Item("sLocation"))
                    oEncounters.DateOfService = oDataReader.Item("dtVisitDate").ToString()
                    oEncounters.ExamID = oDataReader.Item("nExamID").ToString()
                    'oEncounters.EncounterCode = oDataReader.Item("sCPTcode")
                    'oEncounters.EncounterName = oDataReader.Item("sCPTDescription")
                    ''integrated by Mayuri:20120814-from glosuite7010
                    ' oEncounters.Location = GetEncounterLocation(npatientid)
                    oEncountersCol.Add(oEncounters)
                    '        oEncounters.Dispose()
                    oEncounters = Nothing
                End If
            End While
            oEncounters = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oEncountersCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function


    'Code added by kanchan on 20100629, snomedsetting parameter is passed
    Public Function GetPatientProblems(ByVal npatientid As Int64, ByVal _IsNewProblemList As Boolean) As gloCCDLibrary.ProblemsCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oProblems As gloCCDLibrary.Problems
        Dim oProblemsCol As gloCCDLibrary.ProblemsCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oProblems = New gloCCDLibrary.Problems
            oProblemsCol = New gloCCDLibrary.ProblemsCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            'Code added by kanchan on 20100629, if true then take data from new problemlist table
            'If _IsNewProblemList = True Then
            '    cmd.CommandText = "sp_CCDPatientNewProblems"
            'Else
            'End If
            cmd.CommandText = "gsp_CCDPatientProblems"


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oProblems = New Problems
                    oProblems.Condition = oDataReader.Item("Condition")
                    oProblems.DateOfService = oDataReader.Item("DateofService")
                    'Code Start-Added by kanchan on 20100916
                    oProblems.ICD9 = oDataReader.Item("sICD9")
                    oProblems.ICD9Code = oDataReader.Item("ICD9Code")
                    oProblems.ConceptID = oDataReader.Item("ConceptID")
                    oProblems.ProblemType = oDataReader.Item("sProblemType")
                    oProblems.ICDRevision = oDataReader.Item("nICDRevision")
                    If oDataReader.Item("ConditionStatus") = "0" Or Convert.ToString(oDataReader.Item("sReaction")) <> "" Then
                        If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                            If oDataReader.Item("sReaction").ToString() <> "" Then
                                Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                                If Not IsNothing(temp) Then

                                    oProblems.ProblmReaction = temp(1)
                                    If Not IsNothing(oProblems.ProblmReaction) Then
                                        If oProblems.ProblmReaction.Trim() <> "Active" Then
                                            oProblems.Dispose()
                                            oProblems = Nothing
                                        Else
                                            oProblems.ConditionStatus = "Active"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If oDataReader.Item("ConditionStatus") = "1" Then
                            oProblems.ConditionStatus = "Resolved"
                        ElseIf oDataReader.Item("ConditionStatus") = "2" Then
                            oProblems.ConditionStatus = "Active"
                        ElseIf oDataReader.Item("ConditionStatus") = "3" Then
                            oProblems.ConditionStatus = "InActive"
                        ElseIf oDataReader.Item("ConditionStatus") = "4" Then
                            oProblems.ConditionStatus = "Chronic"
                        ElseIf oDataReader.Item("ConditionStatus") = "5" Then
                            oProblems.ConditionStatus = "All"
                        End If
                    End If

                    'End If
                    If IsNothing(oProblems) = False Then
                        oProblemsCol.Add(oProblems)
                        oProblems.Dispose()
                        oProblems = Nothing
                    End If

                End If
            End While
            oProblems = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oProblemsCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientVitals(ByVal npatientid As Int64) As gloCCDLibrary.VitalsCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oVitals As gloCCDLibrary.Vitals
        Dim oVitalsCol As gloCCDLibrary.VitalsCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oVitals = New gloCCDLibrary.Vitals
            oVitalsCol = New gloCCDLibrary.VitalsCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientVitals"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oVitals = New Vitals

                    oVitals.VitalDate = oDataReader.Item("dtVitalDate")
                    oVitals.BloodPressureSittingMax = oDataReader.Item("dBloodPressureSittingMax")
                    oVitals.BloodPressureSittingMin = oDataReader.Item("dBloodPressureSittingMin")
                    oVitals.PulsePerMinute = oDataReader.Item("dPulsePerMinute")
                    oVitals.RespiratoryRate = oDataReader.Item("dRespiratoryRate")
                    oVitals.Temperature = oDataReader.Item("dTemperature")
                    oVitals.HeightinInch = oDataReader.Item("dHeightinInch")
                    oVitals.Weightinlbs = oDataReader.Item("dWeightinlbs")
                    oVitals.BMI = oDataReader.Item("dBMI")
                    oVitals.BSA = oDataReader.Item("BodySurfaceArea")
                    oVitalsCol.Add(oVitals)
                    '           oVitals.Dispose()
                    oVitals = Nothing
                End If
            End While
            oVitals = Nothing
            oDataReader.Close()

            cnn.Close()

            Return oVitalsCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If


            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientImmunizations(ByVal npatientid As Int64) As gloCCDLibrary.ImmunizationCol
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oImmunization As gloCCDLibrary.Immunization
        Dim oImmunizationCol As gloCCDLibrary.ImmunizationCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            oImmunization = New gloCCDLibrary.Immunization
            oImmunizationCol = New gloCCDLibrary.ImmunizationCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientImmunizations"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oImmunization = New Immunization

                    oImmunization.VaccineName = oDataReader.Item("VaccName")
                    oImmunization.VaccineCode = oDataReader.Item("VaccCode") 'Added by kanchan on 20100626
                    oImmunization.Route = oDataReader.Item("VacRoute")
                    If Not IsNothing(oImmunization.Route) Then
                        If oImmunization.Route <> "" Then
                            'Code commented by kanchan on 20101123 as per new implementation
                            oImmunization.RouteCode = GetCodeForCategory(oImmunization.Route, "Route")
                            ''integrated by Mayuri:20120814-from glosuite7010
                            If IsDBNull(oDataReader.Item("RouteCode")) = False Then
                                If (oDataReader.Item("RouteCode")) <> "" Then
                                    oImmunization.RouteCode = oDataReader.Item("RouteCode")
                                Else
                                    oImmunization.RouteCode = ""
                                End If
                            Else
                                oImmunization.RouteCode = ""
                            End If
                        Else
                            oImmunization.RouteCode = ""
                            oImmunization.Route = ""
                        End If
                    End If

                    If IsDBNull(oDataReader.Item("VaccDateGiven")) Then
                        oImmunization.ImmunizationDate = ""
                    Else
                        If (oDataReader.Item("VaccDateGiven").ToString().Length > 0) Then
                            oImmunization.ImmunizationDate = oDataReader.Item("VaccDateGiven").ToString()
                        End If
                    End If

                    oImmunizationCol.Add(oImmunization)
                    oImmunization.Dispose()
                    oImmunization = Nothing
                End If
            End While
            oImmunization = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oImmunizationCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientVitalsinDT(ByVal npatientid As Int64) As DataTable
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As New SqlDataAdapter
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        'Dim oVitals As gloCCDLibrary.Vitals
        'Dim oVitalsCol As gloCCDLibrary.VitalsCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oVitals = New gloCCDLibrary.Vitals
            'oVitalsCol = New gloCCDLibrary.VitalsCol
            Dim dtVitals As New DataTable

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientVitals"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            oDataReader.SelectCommand = cmd
            oDataReader.Fill(dtVitals)


            'While oDataReader.Read
            '    If oDataReader.HasRows() Then
            '        oVitals = New Vitals

            '        oVitals.VitalDate = oDataReader.Item("dtVitalDate")
            '        oVitals.BloodPressureSittingMax = oDataReader.Item("dBloodPressureSittingMax")
            '        oVitals.BloodPressureSittingMin = oDataReader.Item("dBloodPressureSittingMin")
            '        oVitals.PulsePerMinute = oDataReader.Item("dPulsePerMinute")
            '        oVitals.RespiratoryRate = oDataReader.Item("dRespiratoryRate")
            '        oVitals.Temperature = oDataReader.Item("dTemperature")
            '        oVitals.HeightinInch = oDataReader.Item("dHeightinInch")
            '        oVitals.WeightinKg = oDataReader.Item("dWeightinKg")
            '        oVitals.BMI = oDataReader.Item("dBMI")
            '        oVitals.BSA = oDataReader.Item("BodySurfaceArea")
            '        oVitalsCol.Add(oVitals)
            '        oVitals.Dispose()
            '        oVitals = Nothing
            '    End If
            'End While
            'oVitals = Nothing
            'oDataReader.Close()
            oDataReader.Dispose()
            cnn.Close()

            Return dtVitals


        Catch ex As gloCCDException
            Return Nothing
            Throw ex
        Catch ex As Exception
            Return Nothing
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function
#Region "CCD Optimization  changes integrated by Mayuri:20120814-from glosuite7010"
    Public Function getCCHITSeetings_New(ByVal dt As DataTable) As Boolean

        Dim _result As Boolean = False


        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("sSettingsValue") = "" Then
                    _result = False
                Else
                    _result = dt.Rows(0)("sSettingsValue")
                End If
            End If
        End If
        Return _result
    End Function
    Public Function GetLatestMedicationinfo_New(ByVal npatientid As Int64) As gloCCDLibrary.MedicationsCol
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim omedication As gloCCDLibrary.Medication
        Dim omedicationCol As gloCCDLibrary.MedicationsCol
        '  Dim oRxBusinesslayer As gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(npatientid)
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Dim osqlpara As SqlParameter = Nothing
        Dim dtMedication As DataTable
        Dim dtFDA_HL7Codes As DataTable = Nothing
        Try
            omedication = New gloCCDLibrary.Medication
            omedicationCol = New gloCCDLibrary.MedicationsCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDLatestMedications"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@dtsystemdate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            da.Dispose()
            da = Nothing

            If IsNothing(ds) = False Then
                If ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "Medication"
                End If
            End If


            If IsNothing(ds.Tables("Medication")) = False Then
                dtMedication = ds.Tables("Medication")
                If dtMedication.Rows.Count > 0 Then
                    Dim ndcList As New List(Of String)
                    Using dtNDCs As DataTable = New DataView(dtMedication).ToTable(False, New String() {"NDCCode"})
                        If Not IsNothing(dtNDCs) Then
                            If Not IsNothing(dtNDCs) Or dtNDCs.Rows.Count > 0 Then
                                ndcList = dtNDCs.AsEnumerable().[Select](Of String)(Function(q) Convert.ToString(q("NDCCode"))).ToList()
                            End If
                        End If
                    End Using

                    Dim oDrugInfo As New gloGlobal.DIB.ResultSetRxnorm
                    Try
                        Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                            oDrugInfo = oDIBGSHelper.GetRxnormGenericName(ndcList)
                        End Using
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    End Try


                    Using DrugRouteTable As DataTable = New DataView(dtMedication).ToTable(False, New String() {"DrugRoute"})
                        dtFDA_HL7Codes = gloCCDInterface.GetFDAHL7ForRouteCode(DrugRouteTable)
                    End Using

                    For i As Integer = 0 To dtMedication.Rows.Count - 1
                        omedication = New Medication
                        If IsDBNull(dtMedication.Rows(i)("NDCCode")) = False Then
                            If dtMedication.Rows(i)("NDCCode") <> "" Then
                                omedication.ProdCode = dtMedication.Rows(i)("NDCCode")
                            Else
                                omedication.ProdCode = ""
                            End If
                        Else
                            omedication.ProdCode = ""
                        End If

                        omedication.DrugName = dtMedication.Rows(i)("sMedication")
                        omedication.DrugStrength = dtMedication.Rows(i)("sDosage")
                        omedication.DrugQuantity = dtMedication.Rows(i)("sAmount")
                        omedication.Days = dtMedication.Rows(i)("Duration")
                        If IsDBNull(dtMedication.Rows(i)("Refills")) Then
                            omedication.Refills = 0
                        ElseIf dtMedication.Rows(i)("Refills") = "" Then
                            omedication.Refills = 0
                        Else
                            omedication.Refills = dtMedication.Rows(i)("Refills")
                        End If
                        omedication.Frequency = dtMedication.Rows(i)("Frequency")
                        omedication.Pharmacy = dtMedication.Rows(i)("Pharmacy")
                        omedication.MedicationDate = dtMedication.Rows(i)("MedicationDate").ToString()
                        omedication.Status = dtMedication.Rows(i)("Status")
                        If omedication.Status = "" Then
                            omedication.Status = "Active"
                        End If
                        omedication.Route = dtMedication.Rows(i)("DrugRoute")
                        omedication.CheifComplaint = dtMedication.Rows(i)("Rx_sChiefComplaints")
                        omedication.DrugForm = dtMedication.Rows(i)("sDrugForm")

                        If IsNothing(oDrugInfo) = False Then
                            If IsNothing(oDrugInfo) = False Then
                                If oDrugInfo.lgrx.Count > 0 Then
                                    For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                        If item.Ndc = omedication.ProdCode Then
                                            omedication.RxNormCode = item.Rxnorm
                                            Exit For
                                        End If
                                    Next
                                End If
                            End If
                        End If
                        If Not IsNothing(omedication.ProdCode) Then
                            If omedication.ProdCode <> "" Then
                                If IsNothing(oDrugInfo) = False Then
                                    If oDrugInfo.lgrx.Count > 0 Then
                                        For Each item As gloGlobal.DIB.Rxnormdetails In oDrugInfo.lgrx
                                            If item.Ndc = omedication.ProdCode Then
                                                omedication.GenericName = item.Genericname
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        End If
                        If Not IsNothing(omedication.Route) Then
                            If IsNothing(dtFDA_HL7Codes) = False Then
                                If dtFDA_HL7Codes.Rows.Count > 0 Then
                                    For Each itemRow As DataRow In dtFDA_HL7Codes.Rows
                                        If itemRow("ConceptName").ToString().ToUpper() = omedication.Route.ToString().ToUpper() Then
                                            If itemRow("CodeSystem") = "FDA" Then
                                                omedication.FDACode = itemRow("ConceptCode")
                                            End If
                                            If itemRow("CodeSystem") = "HL7" Then
                                                omedication.HL7Code = itemRow("ConceptCode")
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        If IsNothing(omedication.ProdCode) Then
                            omedication.ProdCode = ""
                        End If
                        If IsNothing(omedication.RxNormCode) Then
                            omedication.RxNormCode = ""
                        End If
                        If IsNothing(omedication.FDACode) Then
                            omedication.FDACode = "C38313"
                        End If
                        If IsNothing(omedication.HL7Code) Then
                            omedication.HL7Code = "PO"
                        End If
                        If IsNothing(omedication.GenericName) Then
                            omedication.GenericName = ""
                        End If
                        omedicationCol.Add(omedication)
                        omedication = Nothing
                    Next
                End If
            End If
            omedication = Nothing
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
            cnn.Close()
            Return omedicationCol
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(dtFDA_HL7Codes) Then
                dtFDA_HL7Codes.Dispose()
                dtFDA_HL7Codes = Nothing
            End If
            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            'If Not IsNothing(oRxBusinesslayer) Then
            '    oRxBusinesslayer.Dispose()
            'End If
        End Try


    End Function
    Public Function GetPatientInsuranceInfo_New(ByVal _dtInsurance As DataTable) As gloCCDLibrary.InsuranceCol


        Dim oInsurance As gloCCDLibrary.Insurance
        Dim oInsuranceCol As gloCCDLibrary.InsuranceCol
        '        oInsurance = New gloCCDLibrary.Insurance
        oInsuranceCol = New gloCCDLibrary.InsuranceCol
        Try

            If IsNothing(_dtInsurance) = False Then
                If _dtInsurance.Rows.Count > 0 Then
                    'Bug #46771: 00000404 : EMR Dashboard : CCD Insurance issue
                    'For loop added to resolve the issue.
                    For i As Integer = 0 To _dtInsurance.Rows.Count - 1
                        oInsurance = New Insurance
                        oInsurance.InsSubscriberName = _dtInsurance.Rows(i)("sSubFName") & " " & _dtInsurance.Rows(i).Item("sSubMName") & " " & _dtInsurance.Rows(i).Item("sSubLName")
                        oInsurance.InsuranceName = _dtInsurance.Rows(i).Item("sInsuranceName")
                        oInsurance.InsuranceId = _dtInsurance.Rows(i).Item("sSubscriberId")
                        oInsurance.GroupNo = _dtInsurance.Rows(i).Item("sGroup")
                        oInsurance.InsSubsGender = _dtInsurance.Rows(i).Item("sSubscriberGender")
                        oInsurance.InsSubsAddressLine1 = _dtInsurance.Rows(i).Item("sAddressLine1") ''''''Insurance company address
                        oInsurance.InsSubsAddressLine2 = _dtInsurance.Rows(i).Item("sAddressLine2") ''''''Insurance company address
                        oInsurance.InsSubsCity = _dtInsurance.Rows(i).Item("sCity") ''''''Insurance company City
                        oInsurance.InsSubsState = _dtInsurance.Rows(i).Item("sState") ''''''Insurance company State
                        oInsurance.InsSubsZip = _dtInsurance.Rows(i).Item("sZip") ''''''Insurance company Zip
                        oInsurance.InsRelation = _dtInsurance.Rows(i).Item("sRelationShip") ''''''Insurance company Zip
                        If IsDBNull(_dtInsurance.Rows(i).Item("dtStartDate")) Then
                            oInsurance.InsStartdate = ""
                        Else
                            oInsurance.InsStartdate = _dtInsurance.Rows(i).Item("dtStartDate") ''''''Insurance company start date
                        End If
                        If IsDBNull(_dtInsurance.Rows(i).Item("dtEndDate")) Then
                            oInsurance.InsEndDate = ""
                        Else
                            oInsurance.InsEndDate = _dtInsurance.Rows(i).Item("dtEndDate") ''''''Insurance End date
                        End If

                        oInsurance.InsTypeCode = _dtInsurance.Rows(i).Item("InsTypeCode")
                        oInsurance.InsuranceType = _dtInsurance.Rows(i).Item("InsTypeDesc")
                        oInsuranceCol.Add(oInsurance)
                    Next
                End If
            End If
            Return oInsuranceCol
        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            '    If IsNothing(oInsurance) = False Then
            '        oInsurance.Dispose()
            '        oInsurance = Nothing
            '    End If
            '
        End Try


    End Function
    Public Function GetLabTestsWithResult(ByVal npatientid As Int64) As gloCCDLibrary.LabTestCol
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oLabResults As gloCCDLibrary.LabResults = Nothing
        Dim oLabResultsCol As gloCCDLibrary.LabResultsCol = Nothing
        Dim oLabTest As gloCCDLibrary.LabTest = Nothing
        Dim oLabTestCol As gloCCDLibrary.LabTestCol = Nothing

        Dim osqlpara As SqlParameter = Nothing
        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()

            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDLabResults"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            Dim dtResults As New DataTable
            Dim da As SqlDataAdapter

            da = New SqlDataAdapter(cmd)
            da.Fill(dtResults)
            da.Dispose()
            da = Nothing
            Dim dvResults As DataView = Nothing
            If IsNothing(dtResults) = False Then
                dvResults = dtResults.DefaultView
            End If
            Dim dt As DateTime
            Dim _testname As String = ""
            Dim _FillTestCode As String = ""
            Dim _FillOrderID As String = ""
            Dim _FillTestName As String = ""
            If IsNothing(dvResults) = False Then
                If dvResults.Count > 0 Then
                    oLabTestCol = New gloCCDLibrary.LabTestCol
                    For i As Integer = 0 To dvResults.Table.Rows.Count - 1
                        oLabResultsCol = New gloCCDLibrary.LabResultsCol
                        oLabTest = New gloCCDLibrary.LabTest

                        If _FillTestCode <> Convert.ToString(dvResults.Table.Rows(i)("labotd_TestID")) Or _FillOrderID <> Convert.ToString(dvResults.Table.Rows(i)("labom_OrderID")) Then
                            _FillTestCode = Convert.ToString(dvResults.Table.Rows(i)("labotd_TestID"))
                            _FillOrderID = Convert.ToString(dvResults.Table.Rows(i)("labom_OrderID"))
                            _FillTestName = Convert.ToString(dvResults.Table.Rows(i)("labotd_TestName"))

                            Dim strfilter As String = ""
                            Dim _resultnumber As Integer
                            _resultnumber = 0
                            strfilter = "labotd_TestID = '" & dvResults.Table.Rows(i)("labotd_TestID").ToString() & "' and labom_OrderID = '" & dvResults.Table.Rows(i)("labom_OrderID").ToString() & "' "

                            dvResults.RowFilter = strfilter

                            For j As Integer = 0 To dvResults.Count - 1
                                oLabResults = New gloCCDLibrary.LabResults
                                oLabResults.LabFacility = dvResults.Table.Rows(i)("labom_ReceivingFacilityCode")
                                oLabResults.OrderNo = dvResults.Table.Rows(i)("labom_OrderNoID")
                                oLabResults.ProviderName = dvResults.Table.Rows(i)("labom_ProviderName")
                                If IsNothing(dvResults(j)("labotd_TestID")) = False Then

                                    If IsNothing(dvResults(j)("labotd_TestID")) = False Then

                                        If Convert.ToString(dvResults(j)("labotd_TestID")) <> "" Then
                                            _testname = dvResults(j)("labotd_TestName")
                                            _resultnumber = dvResults(j)("labotr_testresultnumber")

                                            'Bug #45579: 00000391 : Lab Orders : CCD shows wrong flag status
                                            'Previously there was dtResults is used instead of dvResults
                                            oLabResults.AbnormalFlag = dvResults(j)("labotrd_AbnormalFlag")

                                            If IsDBNull(dvResults(j)("labotrd_ResultDateTime")) OrElse dvResults(j)("labotrd_ResultDateTime") = "" Then
                                                oLabResults.ResultDate = ""
                                            Else
                                                dt = dvResults(j)("labotrd_ResultDateTime")
                                                oLabResults.ResultDate = dt.ToString()
                                            End If
                                            oLabResults.ResultName = dvResults(j)("labotrd_ResultName")
                                            oLabResults.ResultRange = dvResults(j)("labotrd_ResultRange")
                                            oLabResults.ResultUnit = dvResults(j)("labotrd_ResultUnit")
                                            oLabResults.ResultValue = dvResults(j)("labotrd_ResultValue")

                                            If IsDBNull(dvResults(j)("labotr_SpecimenReceivedDateTime")) OrElse dvResults(j)("labotr_SpecimenReceivedDateTime") = "" Then
                                                oLabResults.SpecimenDate = ""
                                            Else
                                                dt = dvResults(j)("labotr_SpecimenReceivedDateTime")
                                                oLabResults.SpecimenDate = dt.ToString()
                                            End If

                                            oLabResults.TestName = dvResults(j)("labotrd_TestName")
                                            oLabResults.ResultComment = dvResults(j)("labotrd_ResultComment")
                                            oLabResults.ResultLOINCID = dvResults(j)("labotrd_LOINCID")

                                            oLabResults.TestCode = dvResults(j)("labtm_Code")
                                            oLabResults.ResultType = dvResults(j)("labotd_TestType")
                                            If IsNothing(dvResults(j)("IsAkw")) = False Then
                                                If Convert.ToString(dvResults(j)("IsAkw")) <> "" Then
                                                    If Convert.ToString(dvResults(j)("IsAkw")) <> "0" Then
                                                        oLabResults.IsAcknowledge = True
                                                    Else
                                                        oLabResults.IsAcknowledge = False
                                                    End If
                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                                oLabResultsCol.Add(oLabResults)
                                '                    oLabResults.Dispose()
                                oLabResults = Nothing
                            Next

                            oLabTest.LabResults = oLabResultsCol
                            oLabTestCol.Add(oLabTest)
                            '               oLabTest.Dispose()
                            oLabTest = Nothing
                        End If
                    Next
                End If
            End If
            If IsNothing(dtResults) = False Then
                dtResults.Dispose()
                dtResults = Nothing
            End If

            'If IsNothing(oLabTest) = False Then
            '    oLabTest.Dispose()
            '    oLabTest = Nothing
            'End If

            Return oLabTestCol

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Function
    Public Function GetLatestAllergiesinfo_New(ByVal npatientid As Int64) As gloCCDLibrary.AllergiesCol

        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        'Dim _table As New DataTable

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oAllergies As gloCCDLibrary.Allergies
        Dim oAllergiesCol As gloCCDLibrary.AllergiesCol
        '  Dim oRxBusinesslayer As gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(npatientid)
        Dim osqlpara As SqlParameter = Nothing


        Try
            '            oAllergies = New gloCCDLibrary.Allergies
            oAllergiesCol = New gloCCDLibrary.AllergiesCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDGetLatestAllergies"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@Category"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = "Allergies"

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@visitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            ''Added new Paramtwers -Mayuri:20120208-Optimization of CCD Generation Logic

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@sRxNormServerName"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = gloLibCCDGeneral.gRxNServerName

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@RxNormDatabaseName"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = gloLibCCDGeneral.gRxNDatabaseName

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            Dim ds As New DataSet
            Dim da As SqlDataAdapter
            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            If IsNothing(ds) = False Then
                If ds.Tables.Count > 0 Then
                    ds.Tables(0).TableName = "Allergy"
                    ds.Tables(1).TableName = "RxNorm"
                End If
            End If
            Dim dtAllergy As DataTable = Nothing
            Dim dtrxnorm As DataTable = Nothing

            If IsNothing(ds.Tables("Allergy")) = False Then
                dtAllergy = ds.Tables("Allergy")
            End If
            If IsNothing(ds.Tables("RxNorm")) = False Then
                dtrxnorm = ds.Tables("RxNorm")
            End If

            If (IsNothing(dtAllergy) = False) Then


                If dtAllergy.Rows.Count > 0 Then

                    For i As Integer = 0 To dtAllergy.Rows.Count - 1



                        oAllergies = New Allergies
                        oAllergies.ProductName = dtAllergy.Rows(i)("HistoryItem")
                        oAllergies.ProductCode = dtAllergy.Rows(i)("sNDCCode")
                        If oAllergies.ProductCode <> "" Then
                            Dim _sNDCCode As String = oAllergies.ProductCode
                            If _sNDCCode <> "" Then
                                If IsNothing(dtrxnorm) = False Then
                                    If dtrxnorm.Rows.Count > 0 Then
                                        For j As Integer = 0 To dtrxnorm.Rows.Count - 1
                                            If dtrxnorm.Rows(j)("ATV") = oAllergies.ProductCode Then
                                                oAllergies.RxNormID = dtrxnorm.Rows(j)("RxNorm")
                                                Exit For
                                            End If
                                        Next
                                    End If
                                End If
                                'oAllergies.RxNormID = oRxBusinesslayer.GetRxNormCode(_sNDCCode)
                                If IsNothing(oAllergies.RxNormID) Then
                                    oAllergies.RxNormID = ""
                                End If
                                If oAllergies.RxNormID <> "" Then
                                    oAllergies.ProductCode = oAllergies.RxNormID
                                Else
                                    oAllergies.ProductCode = ""
                                End If

                            End If

                        End If
                        oAllergies.Reaction = dtAllergy.Rows(i)("sReaction")
                        If IsDBNull(dtAllergy.Rows(i)("ReactionCode")) = False Then
                            If dtAllergy.Rows(i)("ReactionCode") <> "" Then
                                oAllergies.ReactionCode = dtAllergy.Rows(i)("ReactionCode")
                            End If
                        End If
                        If IsNothing(oAllergies.ReactionCode) Then
                            oAllergies.ReactionCode = ""
                        End If
                        oAllergies.ConceptID = dtAllergy.Rows(i)("sConceptID")

                        oAllergies.EffectiveStartTime = dtAllergy.Rows(i)("HistoryDate").ToString()

                        If Not IsNothing(oAllergies.Reaction) Then
                            If oAllergies.Reaction <> "" Then
                                Dim temp As String() = oAllergies.Reaction.Split("|")
                                If Not IsNothing(temp) Then
                                    oAllergies.Reaction = temp(0)
                                    oAllergies.Status = temp(1)
                                    If Not IsNothing(oAllergies.Status) Then
                                        If oAllergies.Status.Trim() <> "Active" Then
                                            oAllergies.Dispose()
                                            oAllergies = Nothing
                                            Continue For
                                        End If
                                    End If
                                End If
                            End If
                        End If


                        oAllergiesCol.Add(oAllergies)
                        '     oAllergies.Dispose()
                        oAllergies = Nothing


                    Next


                End If
            End If
            oAllergies = Nothing
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If

            cnn.Close()

            Return oAllergiesCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Function
    Public Function GetPatientAuthorInfo_New(ByVal dt As DataTable, ByVal dtclinic As DataTable) As gloCCDLibrary.PatientAuthor
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        Dim _table As DataTable
        'Dim sqladpt As New SqlDataAdapter
        'Dim cmd As SqlCommand = Nothing
        'Dim cnn As New SqlConnection()
        Dim _PersonFN As String = ""
        Dim _PersonLN As String = ""
        Dim _Organazation As String = ""
        Dim opatientAuthor As gloCCDLibrary.PatientAuthor

        Try
            opatientAuthor = New gloCCDLibrary.PatientAuthor

            _table = dt
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    opatientAuthor.PersonName.LastName = _table.Rows(0)("sLastName")
                    opatientAuthor.PersonName.FirstName = _table.Rows(0)("sFirstName")
                    If opatientAuthor.PersonName.FirstName = "" Then
                        opatientAuthor.PersonName.FirstName = _table.Rows(0)("sLoginName")
                    End If
                End If
            End If

            If Not IsNothing(dtclinic) Then
                If dtclinic.Rows.Count > 0 Then
                    _Organazation = dtclinic.Rows(0)("ClinicName")
                    If Not IsNothing(_Organazation) Then
                        opatientAuthor.Organization = _Organazation
                    Else
                        opatientAuthor.Organization = ""
                    End If
                End If
            End If



            '   cnn.Close()

            Return opatientAuthor

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            'If Not IsNothing(cmd) Then
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If
            'If Not IsNothing(cnn) Then
            '    If cnn.State = ConnectionState.Open Then
            '        cnn.Close()
            '    End If
            '    cnn.Dispose()
            '    cnn = Nothing
            'End If
        End Try

    End Function
    Public Function GetPatientDetailInformation(ByVal nPatientID As Int64, ByVal nLoginID As Int64) As DataSet
        Dim dsPatient As New DataSet

        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter = Nothing
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCD_GetPatientInfo"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@LoginID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nLoginID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            Dim daPatient As SqlDataAdapter = Nothing
            daPatient = New SqlDataAdapter(cmd)
            daPatient.Fill(dsPatient)
            dsPatient.Tables(0).TableName = "Patient"
            dsPatient.Tables(1).TableName = "User"
            dsPatient.Tables(2).TableName = "Clinic"
            dsPatient.Tables(3).TableName = "Insurance"
            dsPatient.Tables(4).TableName = "Setting"
            dsPatient.Tables(5).TableName = "Provider"
            daPatient.Dispose()
            daPatient = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
        Return dsPatient
    End Function
    Public Function GetPatientInformation(ByVal _table As DataTable) As gloCCDLibrary.Patient
        Dim opatient As gloCCDLibrary.Patient
        opatient = New gloCCDLibrary.Patient
        Try

            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    opatient.PatientName.Code = _table.Rows(0)("spatientcode")
                    opatient.PatientName.FirstName = _table.Rows(0)("sFirstName")
                    opatient.PatientName.MiddleName = _table.Rows(0)("sMiddleName")
                    opatient.PatientName.LastName = _table.Rows(0)("sLastName")
                    opatient.Gender = _table.Rows(0)("sGender")
                    opatient.DateofBirth = _table.Rows(0)("dtDOB")
                    opatient.PatientName.PersonContactAddress.Street = _table.Rows(0)("sAddressLine1")
                    opatient.PatientName.PersonContactAddress.AddressLine2 = _table.Rows(0)("sAddressLine2")
                    opatient.PatientName.PersonContactAddress.City = _table.Rows(0)("sCity")
                    opatient.PatientName.PersonContactAddress.State = _table.Rows(0)("sState")
                    opatient.PatientName.PersonContactAddress.Zip = _table.Rows(0)("sZip")
                    opatient.PatientName.PersonContactAddress.Country = _table.Rows(0)("sCountry")
                    opatient.County = _table.Rows(0)("sCounty")
                    opatient.SSN = _table.Rows(0)("nSSN")
                    opatient.MaritalStatus = _table.Rows(0)("sMaritalStatus")
                    opatient.Phone = _table.Rows(0)("sPhone")
                    opatient.Mobile = _table.Rows(0)("sMobile")
                    opatient.Email = _table.Rows(0)("sEmail")
                    opatient.Race = _table.Rows(0)("sRace")
                    opatient.Guardian_fName = _table.Rows(0)("sGuardian_fName")
                    opatient.Guardian_mName = _table.Rows(0)("sGuardian_mName")
                    opatient.Guardian_lName = _table.Rows(0)("sGuardian_lName")
                    opatient.Guardian_Address1 = _table.Rows(0)("sGuardian_Address1")
                    opatient.Guardian_Address2 = _table.Rows(0)("sGuardian_Address2")
                    opatient.Guardian_City = _table.Rows(0)("sGuardian_City")
                    opatient.Guardian_State = _table.Rows(0)("sGuardian_State")
                    opatient.Guardian_ZIP = _table.Rows(0)("sGuardian_ZIP")
                    opatient.Guardian_County = _table.Rows(0)("sGuardian_County")
                    opatient.Guardian_Phone = _table.Rows(0)("sGuardian_Phone")
                    opatient.Guardian_Email = _table.Rows(0)("sGuardian_Email")
                    opatient.Guardian_Country = _table.Rows(0)("sGuardian_Country")
                    opatient.Ethnicity = _table.Rows(0)("sEthn")
                    opatient.Language = _table.Rows(0)("sLang")
                    If IsDBNull(_table.Rows(0)("RaceCode")) = False Then
                        opatient.RaceCode = _table.Rows(0)("RaceCode")
                    Else
                        opatient.RaceCode = ""
                    End If
                    If IsDBNull(_table.Rows(0)("EthnCode")) = False Then
                        opatient.ethnicGroupCode = _table.Rows(0)("EthnCode")
                    Else
                        opatient.ethnicGroupCode = ""
                    End If
                    If IsDBNull(_table.Rows(0)("LangCode")) = False Then
                        opatient.LanguageCode = _table.Rows(0)("LangCode")
                    Else
                        opatient.LanguageCode = ""
                    End If


                End If
            End If
            Return opatient

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally

        End Try


    End Function
    Public Function GetPatientProviderInfo(ByVal _table As DataTable) As gloCCDLibrary.ProviderCol
        Dim oPatientProvider As gloCCDLibrary.ProviderCol = Nothing
        Dim oProvider As gloCCDLibrary.PatientProvider = Nothing
        Try
            oPatientProvider = New gloCCDLibrary.ProviderCol
            oProvider = New gloCCDLibrary.PatientProvider
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    oProvider.NPI = _table.Rows(0)("sNPI")
                    oProvider.LastName = _table.Rows(0)("sLastName")
                    oProvider.MiddleName = _table.Rows(0)("sMiddleName")
                    oProvider.FirstName = _table.Rows(0)("sFirstName")
                    oProvider.StreetAddress = _table.Rows(0)("sAddress") + " " + _table.Rows(0)("sStreet")
                    oProvider.City = _table.Rows(0)("sCity")
                    oProvider.State = _table.Rows(0)("sState")
                    oProvider.zip = _table.Rows(0)("sZIP")
                    oProvider.WorkPhone = _table.Rows(0)("sPhoneNo")
                    oProvider.MobilePhone = _table.Rows(0)("sMobileNo")
                    oProvider.Suffix = _table.Rows(0)("sSuffix")
                    oPatientProvider.Add(oProvider)
                End If
            End If

            Return oPatientProvider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            'If Not IsNothing(oProvider) Then
            '    oProvider.Dispose()
            '    oProvider = Nothing
            'End If
        End Try
    End Function
    Public Function GetClinicInfo(ByVal _table As DataTable) As gloCCDLibrary.Clinic
        Dim oClinic As gloCCDLibrary.Clinic = Nothing
        Try
            oClinic = New gloCCDLibrary.Clinic
            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    oClinic.ClinicName = _table.Rows(0)("ClinicName")
                    oClinic.PersonContactAddress.Street = _table.Rows(0)("sAddress1") + " " + _table.Rows(0)("sStreet")
                    oClinic.PersonContactAddress.City = _table.Rows(0)("sCity")
                    oClinic.PersonContactAddress.State = _table.Rows(0)("sState")
                    oClinic.PersonContactAddress.Zip = _table.Rows(0)("sZIP")
                    oClinic.PersonContactAddress.Country = _table.Rows(0)("sCountry")
                    oClinic.PersonContactPhone.Phone = _table.Rows(0)("sPhoneNo")
                End If
            End If
            Return oClinic
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally

        End Try
    End Function
#End Region
    Public Function GetPatientProcedure(ByVal npatientid As Int64) As gloCCDLibrary.ProceduresCol
        ' Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oProcedures As gloCCDLibrary.Procedures
        Dim oProceduresCol As gloCCDLibrary.ProceduresCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            '   oProcedures = New gloCCDLibrary.Procedures
            oProceduresCol = New gloCCDLibrary.ProceduresCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientProcedure"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oProcedures = New Procedures
                    oProcedures.ExamName = oDataReader.Item("ExamName")
                    oProcedures.CPTCode = oDataReader.Item("sCPTcode")
                    oProcedures.CPTDescription = oDataReader.Item("sCPTDescription")
                    oProcedures.ICD9_code = oDataReader.Item("sICD9Code")
                    oProcedures.ICD9Description = oDataReader.Item("sICD9Description")
                    oProcedures.ProviderFirstName = oDataReader.Item("ProvFirstName")
                    oProcedures.ProviderMiddleName = oDataReader.Item("ProvMName")
                    oProcedures.ProviderLastName = oDataReader.Item("ProvLastName")
                    oProcedures.ProviderSuffix = oDataReader.Item("ProvSuffix")
                    oProcedures.DateOfService = oDataReader.Item("DateOfService")
                    oProcedures.SnomedCode = oDataReader.Item("sConceptID")
                    oProcedures.ICDRevision = oDataReader.Item("nICDRevision")
                    If oProcedures.DateOfService = "1/1/1900" Then
                        oProcedures.DateOfService = ""

                    End If
                    If Not IsNothing(oDataReader.Item("sReaction").ToString()) Then
                        If oDataReader.Item("sReaction").ToString() <> "" Then
                            Dim temp As String() = oDataReader.Item("sReaction").ToString().Split("|")
                            If Not IsNothing(temp) Then

                                oProcedures.ProcedureStatus = temp(1)
                                If Not IsNothing(oProcedures.ProcedureStatus) Then
                                    If oProcedures.ProcedureStatus.Trim() <> "Active" Then
                                        oProcedures.Dispose()
                                        oProcedures = Nothing

                                    End If
                                End If
                            End If
                        End If
                    End If
                    If IsNothing(oProcedures) = False Then
                        oProceduresCol.Add(oProcedures)
                        '                oProcedures.Dispose()
                        oProcedures = Nothing
                    End If

                End If
            End While
            oProcedures = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oProceduresCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetPatientAdvDirectives(ByVal npatientid As Int64) As gloCCDLibrary.AdvDirectiveCol
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oAdvancedirective As gloCCDLibrary.Advancedirective
        Dim oAdvDirectiveCol As gloCCDLibrary.AdvDirectiveCol

        Dim osqlpara As SqlParameter = Nothing


        Try
            '  oAdvancedirective = New gloCCDLibrary.Advancedirective
            oAdvDirectiveCol = New gloCCDLibrary.AdvDirectiveCol

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientAdvDirective"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _FromDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = _ToDate

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            oDataReader = cmd.ExecuteReader()

            While oDataReader.Read
                If oDataReader.HasRows() Then
                    oAdvancedirective = New Advancedirective
                    oAdvancedirective.AdvDirectiveName = oDataReader.Item("DirectiveName")
                    oAdvancedirective.AdvDirectivePatAware = "Yes"
                    oAdvancedirective.AdvDirectiveThirdParty = ""
                    oAdvancedirective.AdvDirectiveVerification = oDataReader.Item("CreatedDateTime")
                    oAdvancedirective.AdvDirectiveReviewed = "True"


                    oAdvDirectiveCol.Add(oAdvancedirective)
                    oAdvancedirective.Dispose()
                    oAdvancedirective = Nothing
                End If
            End While
            oAdvancedirective = Nothing
            oDataReader.Close()
            oDataReader.Dispose()

            cnn.Close()

            Return oAdvDirectiveCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function GetLabTests(ByVal npatientid As Int64) As gloCCDLibrary.LabTestCol
        'Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim oLabResults As gloCCDLibrary.LabResults = Nothing
        Dim oLabResultsCol As gloCCDLibrary.LabResultsCol = Nothing

        Dim oLabTest As gloCCDLibrary.LabTest = Nothing
        Dim oLabTestCol As gloCCDLibrary.LabTestCol = Nothing

        Dim osqlpara As SqlParameter = Nothing
        Try

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            strSQl = "select lo.labom_OrderID,lt.labotd_TestID from Lab_Order_MST lo inner join Lab_Order_TestDtl lt on " _
            & "lo.labom_OrderID=lt.labotd_OrderID where labom_PatientID=" & npatientid

            cmd.CommandText = strSQl
            Dim dtOrderTest As New DataTable
            Dim sqladpt As New SqlDataAdapter

            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtOrderTest)
            sqladpt.Dispose()
            sqladpt = Nothing

            If Not IsNothing(dtOrderTest) Then
                If dtOrderTest.Rows.Count > 0 Then
                    oLabTestCol = New gloCCDLibrary.LabTestCol
                    For i As Integer = 0 To dtOrderTest.Rows.Count - 1

                        oLabTest = New gloCCDLibrary.LabTest

                        cmd = New SqlCommand
                        cmd.Connection = cnn
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "gsp_CCDLabResults"

                        osqlpara = New SqlParameter
                        osqlpara.ParameterName = "@OrderID"
                        osqlpara.Direction = ParameterDirection.Input
                        osqlpara.DbType = DbType.Int64
                        osqlpara.Value = dtOrderTest.Rows(i)("labom_OrderID")
                        cmd.Parameters.Add(osqlpara)
                        osqlpara = Nothing

                        osqlpara = New SqlParameter
                        osqlpara.ParameterName = "@TestID"
                        osqlpara.Direction = ParameterDirection.Input
                        osqlpara.DbType = DbType.Int64
                        osqlpara.Value = dtOrderTest.Rows(i)("labotd_TestID")
                        cmd.Parameters.Add(osqlpara)
                        osqlpara = Nothing

                        osqlpara = New SqlParameter
                        osqlpara.ParameterName = "@VisitID"
                        osqlpara.Direction = ParameterDirection.Input
                        osqlpara.DbType = DbType.Int64
                        osqlpara.Value = _VisitID
                        cmd.Parameters.Add(osqlpara)
                        osqlpara = Nothing

                        osqlpara = New SqlParameter
                        osqlpara.ParameterName = "@StartDate"
                        osqlpara.Direction = ParameterDirection.Input
                        osqlpara.DbType = DbType.String
                        osqlpara.Value = FromDate
                        cmd.Parameters.Add(osqlpara)
                        osqlpara = Nothing

                        osqlpara = New SqlParameter
                        osqlpara.ParameterName = "@EndDate"
                        osqlpara.Direction = ParameterDirection.Input
                        osqlpara.DbType = DbType.String
                        osqlpara.Value = ToDate
                        cmd.Parameters.Add(osqlpara)
                        osqlpara = Nothing

                        oDataReader = cmd.ExecuteReader()
                        oLabResultsCol = New gloCCDLibrary.LabResultsCol
                        Dim dt As DateTime
                        While oDataReader.Read
                            If oDataReader.HasRows() Then
                                oLabResults = New gloCCDLibrary.LabResults
                                oLabResults.LabFacility = oDataReader.Item("labom_ReceivingFacilityCode")
                                oLabResults.AbnormalFlag = oDataReader.Item("labotrd_AbnormalFlag")
                                oLabResults.OrderNo = oDataReader.Item("labom_OrderNoID")
                                oLabResults.ProviderName = GetProviderName(oDataReader.Item("labom_ProviderID"))
                                'Added 'or' condition by kanchan on 20101110 for bug 6069
                                If IsDBNull(oDataReader.Item("labotrd_ResultDateTime")) OrElse oDataReader.Item("labotrd_ResultDateTime") = "" Then
                                    oLabResults.ResultDate = ""
                                Else
                                    dt = oDataReader.Item("labotrd_ResultDateTime")
                                    oLabResults.ResultDate = dt.ToString()
                                End If
                                oLabResults.ResultName = oDataReader.Item("labotrd_ResultName")
                                oLabResults.ResultRange = oDataReader.Item("labotrd_ResultRange")
                                oLabResults.ResultUnit = oDataReader.Item("labotrd_ResultUnit")
                                oLabResults.ResultValue = oDataReader.Item("labotrd_ResultValue")
                                'Added 'or' condition by kanchan on 20101110 for bug 6069
                                If IsDBNull(oDataReader.Item("labotr_SpecimenReceivedDateTime")) OrElse oDataReader.Item("labotr_SpecimenReceivedDateTime") = "" Then
                                    oLabResults.SpecimenDate = ""
                                Else
                                    dt = oDataReader.Item("labotr_SpecimenReceivedDateTime")
                                    oLabResults.SpecimenDate = dt.ToString()
                                End If

                                oLabResults.TestName = oDataReader.Item("labotrd_TestName")
                                oLabResults.ResultComment = oDataReader.Item("labotrd_ResultComment")
                                oLabResults.ResultLOINCID = oDataReader.Item("labotrd_LOINCID")

                                oLabResults.TestCode = oDataReader.Item("labtm_Code")
                                oLabResults.ResultType = oDataReader.Item("labotd_TestType")
                                If IsNothing(oDataReader.Item("IsAkw")) = False Then
                                    If Convert.ToString(oDataReader.Item("IsAkw")) <> "" Then
                                        If Convert.ToString(oDataReader.Item("IsAkw")) <> "0" Then
                                            oLabResults.IsAcknowledge = True
                                        Else
                                            oLabResults.IsAcknowledge = False
                                        End If
                                    End If

                                End If

                                oLabResultsCol.Add(oLabResults)
                                '                    oLabResults.Dispose()
                                oLabResults = Nothing
                            End If
                        End While
                        oLabTest.LabResults = oLabResultsCol
                        oLabTestCol.Add(oLabTest)
                        '           oLabTest.Dispose()
                        oLabTest = Nothing
                        oDataReader.Close()
                        oDataReader.Dispose()
                    Next
                End If
                dtOrderTest.Dispose()
                dtOrderTest = Nothing
            End If

            Return oLabTestCol


        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Private Function GetEncounterLocation(ByVal nPatientID As Int64) As String
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try

            Dim LocationName As String = ""

            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strquery = "select isnull(sLocationName,'') as LocationName from AS_Appointment_MST where npatientID =" & nPatientID
            cmd.CommandText = strquery

            LocationName = cmd.ExecuteScalar()


            If IsNothing(LocationName) Then
                Return ""
            Else
                Return LocationName
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            conn.Close()
            'cmd.Dispose()
            Return Nothing
        Finally
            conn.Close()
            'cmd.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try

    End Function

    Public Function GetProviderName(ByVal nProviderID As Int64) As String
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try

            Dim ProviderName As String = ""

            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            'Code Commented & Added by kanchan on 20100913
            'strquery = "select sLastName + ',' + sFirstName + ' ' + sMiddleName + ' ' + sPrefix from Provider_MST where nProviderID= " & nProviderID
            strquery = "select isnull(sLastName + ',' + sFirstName,'') + ' ' + isnull(sMiddleName,'') + ' ' + isnull(sPrefix,'') from Provider_MST where nProviderID= " & nProviderID
            cmd.CommandText = strquery

            ProviderName = cmd.ExecuteScalar()


            If IsNothing(ProviderName) Then
                Return ""
            Else
                Return ProviderName
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            conn.Close()
            cmd.Dispose()
            Return Nothing
        Finally

            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function


    Public Function GetNDCCode(ByVal nDrugId As Int64) As String
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try

            Dim NDCCode As String = ""

            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text

            strquery = "select top 1 isnull(sNDCCode,'') as  sNDCCode  from drugs_mst where nDrugsId = " & nDrugId
            cmd.CommandText = strquery

            NDCCode = cmd.ExecuteScalar()


            If IsNothing(NDCCode) Then
                Return ""
            Else
                Return NDCCode
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            conn.Close()
            'cmd.Dispose()
            Return Nothing
        Finally
            conn.Close()
            'cmd.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strquery = Nothing
        End Try

    End Function

    Public Function GetClinicData() As DataTable

        'Dim ogloDB As New gloDataBase
        Dim _table As New DataTable
        'Dim sqladp As SqlDataAdapter
        Dim cnn As New SqlConnection()
        Dim cmd As New SqlCommand()
        Dim _nClinicid As New Int64
        Dim osqlpara As SqlParameter = Nothing


        Try
            _nClinicid = 1
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nClinicID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _nClinicid
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_CCD_ClinicInfo"
            Dim oDataReader As New SqlDataAdapter

            oDataReader.SelectCommand = cmd
            oDataReader.Fill(_table)
            oDataReader.Dispose()
            oDataReader = Nothing
            cnn.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

            _table = Nothing

        Finally

            If Not IsNothing(cnn) Then
                cnn.Dispose()
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        Return _table

    End Function

    '\\added by suraj on 20081125
    ''' <summary>
    ''' this gives patientAuthor info- FN, LN, Organization name
    ''' </summary>
    ''' <param name="nLoginid"></param>
    ''' <returns>obj of patientAuthor</returns>
    ''' <remarks></remarks>
    Public Function GetPatientAuthorInfo(ByVal nLoginid As Int64) As gloCCDLibrary.PatientAuthor
        '        Dim ogloDB As New gloDataBase
        Dim strSQl As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        'Dim _PersonFN As String = ""
        'Dim _PersonLN As String = ""
        Dim _Organazation As String = ""
        Dim opatientAuthor As gloCCDLibrary.PatientAuthor

        Try
            opatientAuthor = New gloCCDLibrary.PatientAuthor

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            strSQl = "select 'sFirstName'=isnull(sFirstName,''), 'sLastName'=isnull(sLastName,''),'sLoginName'=isnull(sLoginName,'') from User_MST where nUserID = " & nLoginid & ""
            Dim sqladpt As New SqlDataAdapter

            sqladpt.SelectCommand = cmd
            cmd.CommandText = strSQl
            Dim _table As New DataTable

            sqladpt.Fill(_table)
            sqladpt.Dispose()
            sqladpt = Nothing

            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    opatientAuthor.PersonName.LastName = _table.Rows(0)("sLastName")
                    opatientAuthor.PersonName.FirstName = _table.Rows(0)("sFirstName")
                    If opatientAuthor.PersonName.FirstName = "" Then
                        opatientAuthor.PersonName.FirstName = _table.Rows(0)("sLoginName")
                    End If
                End If
                _table.Dispose()
                _table = Nothing
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            strSQl = ""
            strSQl = "select isnull(sClinicName,'') from Clinic_MST"
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQl
            _Organazation = cmd.ExecuteScalar()
            If Not IsNothing(_Organazation) Then
                opatientAuthor.Organization = _Organazation
            Else
                opatientAuthor.Organization = ""
            End If

            cnn.Close()

            Return opatientAuthor

        Catch ex As gloCCDException
            Throw ex
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            strSQl = Nothing
            _Organazation = Nothing
        End Try

    End Function

    Public Function GetCodeForCategory(ByVal _Description As String, ByVal _CategoryType As String) As String
        Dim _strSQL As String = ""
        Dim _CategoryCode As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            'prepare the query to get the visitid
            _strSQL = "select isnull(sCode,'') from category_mst where sCategoryType='" & _CategoryType & "' and sDescription='" & _Description.Replace("'", "''") & "'"

            cmd.CommandText = _strSQL

            'execute the command 
            _CategoryCode = cmd.ExecuteScalar
            If IsNothing(_CategoryCode) Then
                _CategoryCode = ""
            End If
            cmd.Dispose()
            cmd = Nothing

            Return _CategoryCode
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
            Return ""
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
            _strSQL = Nothing
        End Try
    End Function
    Public Function GetEncounterCodeAndDescription(ByVal Examid As String, ByVal PatientID As String) As DataTable
        'Dim _strSQL As String = ""
        'Dim _CategoryCode As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Try
            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GetCPTCodeWithDEscription"
            cmd.Parameters.AddWithValue("@nexamid", Examid)
            cmd.Parameters.AddWithValue("@PatientID", PatientID)
            cmd.Connection = cnn
            'prepare the query to get the visitid
            ''_strSQL = "select ISNULL(ExamICD9CPT.sCPTCode,'') AS sCPTCode,   ISNULL(ExamICD9CPT.sCPTDescription,'') as sCPTDescription FROM examicd9cpt INNER JOIN PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  INNER JOIN Visits ON PatientExams.nVisitID = Visits.nVisitID   INNER JOIN Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID where (isnull(sCPTCode,'') <> '' OR isnull(sCPTDEscription,'') <> '') and nexamid='" & Examid & "' "
            Dim dt As New DataTable
            Dim da As SqlDataAdapter

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()
            da = Nothing


            cmd.Dispose()
            cmd = Nothing

            Return dt
        Catch ex As Exception
            Throw New gloCCDException(ex.ToString())
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Function
    'Private Function getProviderInfo(ByVal npatientid As Int64) As DataTable
    '    ' Dim sqladp As SqlDataAdapter
    '    'Dim cmd As SqlCommand
    '    Dim cnn As New SqlConnection()
    '    Dim str As String
    '    Try
    '        cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
    '        str = "SELECT isnull(pv.sAddress,''),isnull(pv.sStreet,''),isnull(pv.sCity,''),isnull(pv.sState,''),isnull(pv.szip,'') from provider_mst pv inner join patient p on" _
    '        & " PV.nproviderid = p.nproviderid where npatientid = " & npatientid

    '    Catch ex As Exception

    '    End Try

    'End Function

    'Code Start- Added by kanchan on 20101011 for CCD/CCR , to get default provider id
    Public Function getDefaultProviderId() As Int64
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _Provider As Int64 = 0
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "select sSettingsValue from settings where sSettingsName like 'PatientDefaultProvider'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _Provider = CType(temp, Int64)
                If _Provider = 0 Then
                    strQuery = "select top 1 nProviderID from provider_mst"
                    cmd.CommandText = strQuery
                    Dim temp1 As Object = cmd.ExecuteScalar()
                    If Not IsNothing(temp1) Then
                        _Provider = CType(temp1, Int64)
                    End If
                End If
            End If
            Return _Provider
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strQuery = Nothing
        End Try
    End Function

    'Code Start-Added by kanchan on 20101011 for Modular CCD Rendering & save
    Public Function UpdateCategoryMaster(ByVal _CategoryDesc As String, ByVal _Category As String, ByVal _PatientID As Int64) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim sqlparam As SqlParameter = Nothing
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd = New SqlCommand("gsp_CCDINUP_CategoryMST", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.InputOutput
            sqlparam.Value = 0

            sqlparam = cmd.Parameters.Add("@CategoryDescription", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _CategoryDesc

            sqlparam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar, 50)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _Category

            sqlparam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = _ClinicID

            sqlparam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = gloLibCCDGeneral.GetPrefixTransactionID(_PatientID)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return False
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlparam) Then
                sqlparam = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function
    'Code End-Added by kanchan on 20101011 for Modular CCD Rendering & save

    'Code Start-Added by kanchan on 20101011 for Modular CCD Rendering & save
    Public Function getDefaultLocation() As String
        Dim cmd As New SqlCommand
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim _Location As String = ""
        Try
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "Select isnull(sLocation,'') from AB_Location where bIsDefault='True'"
            cmd.CommandText = strQuery
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim temp As Object = cmd.ExecuteScalar()
            If Not IsNothing(temp) Then
                _Location = temp.ToString()
                If _Location = "" Then
                    strQuery = "Select Top 1 sLocation from AB_Location"
                    cmd.CommandText = strQuery
                    Dim temp1 As Object = cmd.ExecuteScalar()
                    If Not IsNothing(temp1) Then
                        _Location = temp1.ToString()
                    End If
                End If
            End If
            Return _Location
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strQuery = Nothing
        End Try
    End Function
    'Code End-Added by kanchan on 20101011 for Modular CCD Rendering & save
    'Code Start-Added by kanchan on 20101011 for Conversion fo CCR Date time in string
    Public Function ConvertDateTime(ByVal strDateTime As String) As String
        ' Dim CurrentDate As DateTime = Now
        'Dim DateString As String = ""
        'Try
        '    If _Date <> "" Then
        '        Try
        '            Dim dt As DateTime
        '            dt = CType(_Date, DateTime)
        '            DateString = dt.ToString()
        '        Catch ex As Exception
        '            _Date = _Date.Replace("T", " ")
        '            Dim supportedFormats() As String = New String() {"MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy", "yyyymmdd", "yyyyMMddHHmmss", "u", "MM/dd/yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss.fff", "yyyyMMddHHmmss-ffff", "yyyy-MM-dd HH:mm:ss K", "MM/d/yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ssK"}
        '            Dim dDate As DateTime
        '            dDate = DateTime.ParseExact(_Date, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
        '            DateString = Format(CDate(dDate), "yyyy-MM-dd HH:mm:ss")
        '        End Try
        '    End If

        '    'If _Date <> "" Then
        '    '    Dim dt As DateTime
        '    '    dt = CType(_Date, DateTime)
        '    '    DateString = dt.ToString()
        '    'End If

        '    'If _Date <> "" Then
        '    '    _Date = _Date.Replace("-", "")
        '    '    If _Date.Length = 8 Then
        '    '        DateString = _Date
        '    '    Else
        '    '        'If _Date.Length > 14 Then
        '    '        '    _Date = _Date.Remove(14)
        '    '        'End If
        '    '        '   DateString = Format(CDate(_Date), "yyyy-MM-dd HH:mm:ss.fff")
        '    '        DateString = dDate.ToString()
        '    '    End If

        '    'Else
        '    '    DateString = ""
        '    'End If

        '    Return DateString
        'Catch
        '    Return ""
        'End Try
        Dim oResult As System.Nullable(Of DateTime) = Nothing
        Dim _isSuccess As [Boolean] = False

        Try
            '-------------Convert To date by .net standerd format------------
            Try
                oResult = Convert.ToDateTime(strDateTime)
                _isSuccess = True
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                ex = Nothing
                _isSuccess = False
            End Try
            '-------------Convert To date by .net standerd format------------


            '#Region "en [English] culture"
            '-------------Convert Date TO en English culture------------
            If _isSuccess = False Then
                Try
                    Dim dtTempDate As DateTime
                    Dim culture As IFormatProvider = New System.Globalization.CultureInfo("en", True)
                    If DateTime.TryParse(strDateTime, culture, System.Globalization.DateTimeStyles.RoundtripKind, dtTempDate) = True Then
                        oResult = dtTempDate
                        _isSuccess = True
                    Else
                        _isSuccess = False
                    End If
                    culture = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    _isSuccess = False
                End Try
            End If
            '-------------Convert Date TO en English culture------------

            '#End Region

            '#Region "fr-CA [French-canada] culture "
            '-------------Convert Date TO fr-CA [French-canada] culture ------------
            If _isSuccess = False Then
                Try
                    Dim dtTempDate As DateTime
                    Dim culture As IFormatProvider = New System.Globalization.CultureInfo("fr-CA", True)
                    If DateTime.TryParse(strDateTime, culture, System.Globalization.DateTimeStyles.RoundtripKind, dtTempDate) = True Then
                        oResult = dtTempDate
                        _isSuccess = True
                    Else
                        _isSuccess = False
                    End If
                    culture = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                    _isSuccess = False
                End Try
            End If
            '-------------Convert Date TO fr-CA [French-canada] culture------------

            '#End Region

            '#Region "Invariant Culture  "
            '-------------Invariant Culture ------------
            If _isSuccess = False Then
                If _isSuccess = False Then
                    Try
                        Dim dtTempDate As DateTime
                        Dim culture As IFormatProvider = New System.Globalization.CultureInfo("", True)
                        If DateTime.TryParse(strDateTime, culture, System.Globalization.DateTimeStyles.RoundtripKind, dtTempDate) = True Then
                            oResult = dtTempDate
                            _isSuccess = True
                        Else
                            _isSuccess = False
                        End If
                        culture = Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
                        _isSuccess = False
                    End Try

                End If
            End If


            '-------------Invariant Culture------------

            '#End Region

            '#Region "Custom Formats"
            '-------------Custom Formats ------------
            If _isSuccess = False Then
                If _isSuccess = False Then
                    Try
                        Dim dtTempDate As DateTime
                        Dim culture As IFormatProvider = New System.Globalization.CultureInfo("", True)

                        'Not standerd Formats observed in CCR/CCD files
                        Dim supportedFormats As [String]() = New [String]() {"ddMMMyyyy", "dMMMyyyy", "yyyyMMdd", "yyyyMMddHHmm", "yyyyMMddHHmmz", "yyyyMMddHHmmzz", _
                         "yyyyMMddHHmmzzz", "yyyyMMddHHmmK", "yyyyMMddHHmmss", "yyyyMMddHHmmssz", "yyyyMMddHHmmsszz", "yyyyMMddHHmmsszzz", _
                         "yyyyMMddHHmmssK", "u"}


                        If DateTime.TryParseExact(strDateTime, supportedFormats, culture, System.Globalization.DateTimeStyles.RoundtripKind, dtTempDate) = True Then
                            oResult = dtTempDate
                            _isSuccess = True
                        Else
                            _isSuccess = False
                        End If

                        culture = Nothing
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

                        _isSuccess = False
                    End Try

                End If


                '-------------Custom Formats------------

                '#End Region

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            oResult = Nothing
        End Try

        Return oResult.ToString()


    End Function
    'Code Start-Added by kanchan on 20101011 for Conversion fo CCR Date time in int
    Public Function ConvertDateTimeinInt(ByVal _Date As String) As Int32
        ' Dim CurrentDate As DateTime = Now
        Dim DateInt As Int32
        Try
            If _Date <> "" Then
                DateInt = Format(CDate(_Date), "yyyyMMdd")
            Else
                DateInt = 0
            End If

            Return DateInt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return 0
        End Try
    End Function

    'Code Start-Added by kanchan on 20101113 for patient external code
    Public Function UpdatePatientExternalCode(ByVal nPatientId As Int64, ByVal _ExternalCode As String) As Boolean
        Dim cmd As New SqlClient.SqlCommand
        Dim conn As New SqlClient.SqlConnection
        Dim strquery As String = ""
        Try
            conn.ConnectionString = gloLibCCDGeneral.Connectionstring
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strquery = "UPDATE Patient SET sExternalCode='" & _ExternalCode & "' where nPatientID=" & nPatientId & ""
            cmd.CommandText = strquery
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            conn.Close()
            'cmd.Dispose()
            Return False
        Finally
            conn.Close()
            'cmd.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
            strquery = Nothing
        End Try

    End Function

    'Code Start-Added by kanchan on 20101115 for CCHIT
    Private Function GetDataTable(ByVal Sqlquery As String) As DataTable
        Dim sqlcon As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As New DataTable

        Try
            sqlcon.ConnectionString = gloLibCCDGeneral.Connectionstring
            sqlcon.Open()
            cmd.Connection = sqlcon
            cmd.CommandText = Sqlquery
            Dim sqldata As SqlDataAdapter
            sqldata = New SqlDataAdapter(cmd)
            sqldata.Fill(dt)
            sqldata.Dispose()
            sqldata = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            sqlcon.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlcon) Then
                If sqlcon.State = ConnectionState.Open Then
                    sqlcon.Close()
                End If
                sqlcon.Dispose()
                sqlcon = Nothing
            End If
        End Try
        Return dt
    End Function
    'Code End-Added by kanchan on 20101115 for CCHIT
    'Code Start-Added by kanchan on 20101115 for CCHIT
    Public Function getCCHITSeetings() As Boolean
        Dim strQuery As String = ""
        Dim _result As Boolean = False
        strQuery = "SELECT sSettingsName,isnull(sSettingsValue,'False') as sSettingsValue FROM dbo.Settings WHERE sSettingsName ='ISCCHIT'"
        Dim dt As DataTable = GetDataTable(strQuery)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("sSettingsValue") = "" Then
                    _result = False
                Else
                    _result = dt.Rows(0)("sSettingsValue")
                End If
            End If
            dt.Dispose()
            dt = Nothing
        End If
        strQuery = Nothing
        Return _result
    End Function


    Public Function getCCDTemplateID(ByVal strModule As String) As String
        Dim ogloDB As New gloDataBase
        Dim strCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "SELECT TemplateId FROM CCD_Modules where SectionName='" + strModule + "'"

            ogloDB.Connect(ConnectionString)
            strCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function
    Public Function getCDATemplateID(ByVal strModule As String) As String
        Dim ogloDB As New gloDataBase
        Dim strCode As String = ""
        Dim strSQl As String = ""
        Try
            strSQl = "SELECT CDATemplateId FROM CCD_Modules where SectionName='" + strModule + "'"

            ogloDB.Connect(ConnectionString)
            strCode = ogloDB.ExecuteQueryScaler(strSQl)
            ogloDB.Disconnect()
            Return strCode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return ""
        Finally
            ogloDB = Nothing
            strSQl = Nothing
        End Try
    End Function
    Public Function GetProviders(ByVal ClinicID As Int64, Optional ByVal ProviderID As Int64 = 0) As DataTable
        _databaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)

        'Dim objCon As New SqlConnection
        'Dim objCmd As New SqlCommand
        Dim dtProvider As DataTable = Nothing
        'Dim _strSQL As String = ""
        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing

        Try

            odb.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter
            oParameters = New gloDatabaseLayer.DBParameters

            '_strSQL = "SELECT nProviderID,(ISNULL(sFirstName,'')+ SPACE(1) +  CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  ISNULL(Provider_MST.sMiddleName,'') + SPACE(1)  END + " _
            '    & " ISNULL(sLastName,'')) AS ProviderName FROM  Provider_MST  WHERE  ISNULL(bIsblocked,'false')='FALSE' AND nClinicID = " & ClinicID & " ORDER BY ProviderName"

            oParameter.DataType = SqlDbType.BigInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nClinicID"
            oParameter.Value = ClinicID
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@nProviderID"
            oParameter.Value = ProviderID
            oParameters.Add(oParameter)
            oParameter = Nothing

            'odb.Retrive_Query(_strSQL, dtProvider)
            odb.Retrive("gsp_GetMUDashboardProviders", oParameters, dtProvider)

            odb.Disconnect()

            Return dtProvider

        Catch ex As Exception
            'MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
            Return Nothing

        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If

            'If Not IsNothing(objCmd) Then
            '    objCmd.Parameters.Clear()
            '    objCmd.Dispose()
            '    objCmd = Nothing
            'End If
        End Try


    End Function

    Public Function InsertCDAErrorResponse(ByVal sFilename As String, ByVal CDAErrorDescription As Byte()) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim sqlparam As SqlParameter = Nothing
        Dim username As String = String.Empty
        Dim nCDAErrorid As Int64 = 0
        Try
            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName").ToString() <> "" Then
                    username = Convert.ToString(appSettings("UserName"))
                    ' cmbProviders.SelectedValue = _UserID
                End If
            End If
            conn = New SqlConnection(gloLibCCDGeneral.Connectionstring)
            cmd = New SqlCommand("gsp_InsertCDAResponse", conn)
            cmd.CommandType = CommandType.StoredProcedure

            sqlparam = cmd.Parameters.Add("@sFilename ", SqlDbType.VarChar)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = sFilename

            sqlparam = cmd.Parameters.Add("@CdaErrorDescription ", SqlDbType.Image)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = CDAErrorDescription

            sqlparam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
            sqlparam.Direction = ParameterDirection.Input
            sqlparam.Value = username

            sqlparam = cmd.Parameters.Add("@nCDAErrorId", SqlDbType.BigInt)
            sqlparam.Direction = ParameterDirection.Output
            sqlparam.Value = 0

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            cmd.ExecuteNonQuery()
            nCDAErrorid = sqlparam.Value
            If nCDAErrorid <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.RecordCDAErrors, gloAuditTrail.ActivityType.Add, "C-CDA Errors recorded.", 0, nCDAErrorid, 0, gloAuditTrail.ActivityOutCome.Success)
            End If
            Return nCDAErrorid
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return 0
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlparam) Then
                sqlparam = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

        End Try
    End Function

    Public Function getRaceEthnicityDescription(ByVal strModule As String, ByVal strCode As String) As String
        Dim Sqlquery As String = ""
        Dim sqlcon As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dt As New DataTable
        Dim sDesc As String = ""

        Try
            If strModule.ToLower() = "race" Then
                Sqlquery = "SELECT CM.sDescription FROM  dbo.Category_MST AS CM WHERE CM.sCategoryType IN ('race','race specification') AND CM.sCode IN (" + strCode + ")"
            ElseIf strModule.ToLower() = "ethnicity" Then
                Sqlquery = "SELECT CM.sDescription FROM  dbo.Category_MST AS CM WHERE CM.sCategoryType IN ('ethnicity','ethnicity specification') AND CM.sCode IN (" + strCode + ")"
            End If
            sqlcon.ConnectionString = gloLibCCDGeneral.Connectionstring
            sqlcon.Open()
            cmd.Connection = sqlcon
            cmd.CommandText = Sqlquery
            Dim sqldata As SqlDataAdapter
            sqldata = New SqlDataAdapter(cmd)
            sqldata.Fill(dt)
            sqldata.Dispose()
            sqldata = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return Nothing
        Finally
            sqlcon.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlcon) Then
                If sqlcon.State = ConnectionState.Open Then
                    sqlcon.Close()
                End If
                sqlcon.Dispose()
                sqlcon = Nothing
            End If
        End Try
        If Not IsNothing(dt) And dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If sDesc = "" Then
                    sDesc = Convert.ToString(dt.Rows(i)(0))
                Else
                    sDesc = sDesc + "|" + Convert.ToString(dt.Rows(i)(0))
                End If
            Next
        End If
        Return sDesc
    End Function
    Public Function GetPatientPrefferedLanguage(ByVal sLangCode As String) As String
        _databaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)

        Dim dtLangDesc As DataTable = Nothing

        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim sLangDesc As String = String.Empty
        Try

            odb.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter
            oParameters = New gloDatabaseLayer.DBParameters


            oParameter.DataType = SqlDbType.VarChar
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@sCode"
            oParameter.Value = sLangCode
            oParameters.Add(oParameter)
            oParameter = Nothing

            odb.Retrive("gsp_CCD_GetPrefferedLanguageDescription", oParameters, dtLangDesc)

            odb.Disconnect()

            If Not IsNothing(dtLangDesc) And dtLangDesc.Rows.Count > 0 Then
                sLangDesc = Convert.ToString(dtLangDesc.Rows(0)(0))
            End If

        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If


        End Try
        Return sLangDesc

    End Function

    Public Function GetPatientMedicationStatus(ByVal sStatusCode As String) As String
        _databaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString
        Dim odb As New gloDatabaseLayer.DBLayer(_databaseConnectionString)

        Dim dtLangDesc As DataTable = Nothing

        Dim oParameter As gloDatabaseLayer.DBParameter = Nothing
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim sLangDesc As String = String.Empty
        Try

            odb.Connect(False)

            oParameter = New gloDatabaseLayer.DBParameter
            oParameters = New gloDatabaseLayer.DBParameters


            oParameter.DataType = SqlDbType.VarChar
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.ParameterName = "@sMedicationStatus"
            oParameter.Value = sStatusCode
            oParameters.Add(oParameter)
            oParameter = Nothing

            odb.Retrive("gsp_CCD_GetMedicationStatus", oParameters, dtLangDesc)

            odb.Disconnect()

            If Not IsNothing(dtLangDesc) And dtLangDesc.Rows.Count > 0 Then
                sLangDesc = Convert.ToString(dtLangDesc.Rows(0)(0))
            End If

        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally

            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If


        End Try
        Return sLangDesc

    End Function

    ''added for case CAS-22862-B2C8D4
    Public Function GetPatientVitalsold(ByVal npatientid As Int64) As DataTable
        'Dim ogloDB As New gloDataBase
        'Dim strSQl As String = ""
        'Dim _table As New DataTable
        Dim oDataReader As New SqlDataAdapter
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        'Dim oVitals As gloCCDLibrary.Vitals
        'Dim oVitalsCol As gloCCDLibrary.VitalsCol

        Dim osqlpara As SqlParameter = Nothing

        Try
            'oVitals = New gloCCDLibrary.Vitals
            'oVitalsCol = New gloCCDLibrary.VitalsCol
            Dim dtVitals As New DataTable

            cnn.ConnectionString = gloLibCCDGeneral.Connectionstring
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_CCDPatientVitals_old"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = npatientid

            cmd.Parameters.Add(osqlpara)

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitId"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = _VisitID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StartDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = FromDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@EndDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ToDate
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing


            oDataReader.SelectCommand = cmd
            oDataReader.Fill(dtVitals)



            oDataReader.Dispose()
            cnn.Close()

            Return dtVitals


        Catch ex As gloCCDException
            Return Nothing
            Throw ex
        Catch ex As Exception
            Return Nothing
            Throw New gloCCDException(ex.ToString())
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(osqlpara) Then
                osqlpara = Nothing
            End If

            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try

    End Function
End Class





