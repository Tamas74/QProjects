Imports System.Data.SqlClient
Imports System.Data
Imports Schema = gloGlobal.Schemas.Surescript

Public Class gloSureScriptDBLayer
    Implements IDisposable

    Public Function GetPrescriberList() As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            'ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
            ''problem 557 : where bIsblocked condition added  to display only unblock provider
            ''''_strsql = "select isnull(sSPIID,'') as PrescriberID,isnull(sFirstName,'') + space(1) + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Isnull(Provider_MST.sMiddleName,'') then  Isnull(Provider_MST.sMiddleName,'') + SPACE(1) END + isnull(slastname,'') as ProviderName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone,isnull(sSPIID,'') as PrescriberID from Provider_Mst Where ISNULL(bIsblocked,0)=0"

            _strsql = "with p as (select isnull(sSPIID,'') as PrescriberID,isnull(sFirstName,'') + space(1) + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Isnull(Provider_MST.sMiddleName,'') then  Isnull(Provider_MST.sMiddleName,'') + SPACE(1) END + isnull(slastname,'') as ProviderName,isnull(sAddress,'') + space(1) + isnull(sStreet,'') as Address, isnull(sCity,'') as City,isnull(sstate,'') as State,isnull(szip,'') as Zip,isnull(sphoneno,'') as Phone from  Provider_Mst  Where  ISNULL(bIsblocked,0)=0) select * from p WHERE ISNULL(PrescriberID,'')!='' order by providername"

            sql.CommandText = _strsql
            conn.Open()

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
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

    'Dim oSQLCommand As SqlCommand = Nothing
    'Dim osqlParmeter As SqlParameter = Nothing
    ''' <summary>
    ''' Insert data into SurescriptMessageTransaction
    ''' </summary>
    ''' <param name="oMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertintoMessageTransaction(ByVal oMessage As SureScriptMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        'Dim sqladpt As New SqlDataAdapter
        'Dim dt As New DataTable
        conn.Open()
        Dim sql As New SqlCommand
        Dim _strsql As String
        Try

            sql.CommandType = CommandType.Text
            Dim intstatus As Int16 = 0
          
            _strsql = "Insert into SurescriptMessageTransaction (nMessageID,sMessageName,sRelatesToMessageID,sMessageFrom,sMessageTo,sDateTimeStamp,dtDateReceived,sReferenceNumber,IsAlertCheck) values ('" & oMessage.MessageID & "','" & oMessage.MessageName & "','" & oMessage.RelatesToMessageId & "','" & oMessage.MessageFrom & "','" & oMessage.MessageTo & "','" & oMessage.DateTimeStamp & "','" & oMessage.DateReceived & "','" & oMessage.TransactionID.Replace("'", "''") & "'," & intstatus & ")"
            sql.CommandText = _strsql
            sql.Connection = conn

            sql.ExecuteNonQuery()
            If oMessage.MessageName = "NewRx" Then
                Try
                    If Not IsNothing(sql) Then
                        sql.Parameters.Clear()
                        sql.Dispose()
                        sql = Nothing
                    End If
                    sql = New SqlCommand
                    sql.CommandType = CommandType.Text

                    _strsql = "Update surescriptmessagetransaction set isalertcheck='True'" _
                            & " where nMessageID in (select s.nMessageID " _
                            & " from surescriptmessagetransaction s inner join surescriptmessagetransaction s1 " _
                            & " on s.sRelatesToMessageID= s1.nMessageID where s1.sReferenceNumber='" & oMessage.TransactionID & "'" _
                            & " and s.sMessageName='Error' and s1.sMessageName='NewRx') and sMessageName='Error'"
                    sql.CommandText = _strsql
                    sql.Connection = conn

                    sql.ExecuteNonQuery()
                Catch ex As Exception

                End Try
            End If

            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
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

    ''' <summary>
    ''' Insert data into SurescriptMessageTransaction
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertintoERXWithoutInternet(ByVal bytesRead As String, ByVal objPrescription As EPrescription) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        'Dim sqladpt As New SqlDataAdapter
        'Dim dt As New DataTable
        conn.Open()
        Dim sql As New SqlCommand
        Dim _strsql As String
        Try

            sql.CommandType = CommandType.Text
            Dim intstatus As Int16 = 0
            Dim isTaskGenerated As Int16 = 0 ''''''''this will be directly sent as false by default
            _strsql = "Insert into eRxWithoutInternet (nPrescriptionID,nPatientID,nProviderId,eRxDateTime,eRxFile,eRxStatus,eRxStatusMessage,nMessageID,sMessageName,sRelatesToMessageID,sMessageFrom,sMessageTo,sDateTimeStamp,dtDateReceived,sReferenceNumber,IsAlertCheck,IsTaskGenerated,nloginUserId) " _
            & " values ('" & objPrescription.RxTransactionID & "','" & objPrescription.PatientID & "','" & objPrescription.ProviderID & "','" & DateTime.Now & "','" & bytesRead & "','" & "" & "','" & "" & "',  " _
            & " '" & objPrescription.DrugsCol.Item(0).MessageID & "', '" & objPrescription.DrugsCol.Item(0).MessageName & "','" & objPrescription.DrugsCol.Item(0).RelatesToMessageId & "','" & objPrescription.DrugsCol.Item(0).MessageFrom & "', " _
            & " '" & objPrescription.DrugsCol.Item(0).MessageTo & "','" & objPrescription.DrugsCol.Item(0).DateTimeStamp & "','" & objPrescription.DrugsCol.Item(0).DateReceived & "','" & objPrescription.DrugsCol.Item(0).TransactionID & "'," & intstatus & " , " & isTaskGenerated & "," & gloSurescriptGeneral.gnLoginUserId & ") "
            sql.CommandText = _strsql
            sql.Connection = conn

            sql.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
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

    ''' <summary>
    ''' Insert data into Error Details
    ''' </summary>
    ''' <param name="objError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertErrorDetails(ByVal objError As SureScriptErrorMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            conn.Open()
            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "delete from ErrorTransaction"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "delete from SureScriptMessageTransaction where smessagename='Error'"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "select isnull(max(isnull(nTransactionId,0)),0)+1 from ErrorTransaction"
            sql.CommandText = _strsql

            Dim Id As Int64 = sql.ExecuteScalar

            objError.TransactionID = Id.ToString

            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text

            _strsql = "Insert into ErrorTransaction values (nTransactionID,sErrorCode,sDescriptionCode,sDescription) (" & objError.TransactionID & " ,'" & objError.ErrorCode & "','" & objError.DescriptionCode & "','" & objError.Description & "')"
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If
            _strsql = ""
            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
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

    ''' <summary>
    ''' Function not in use implemented in Webservice
    ''' </summary>
    ''' <param name="objPrescription"></param>
    ''' <param name="blnflag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNewPrescription(ByVal objPrescription As EPrescription, Optional ByVal blnflag As Boolean = False) As Boolean

        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable

        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sSPIID,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            'Old query to fetch patient and pharmacy corresponding to Rx
            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sSPIID,'') as ProviderCode,isnull(pt.nRxTransactionID,0) as PrescriberOrderNumber from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Provider_Mst pr on pt.nvisitid=pr.nproviderid where pt.nRxTransactionID =" & objPrescription.RxTransactionID & "" 'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            'Old query to fetch patient and pharmacy corresponding to Rx

            _strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sSPIID,'') as ProviderCode,isnull(pt.nPharmacyId,0) as PharmacyID,isnull(Pr.sNPI,'') AS PrescriberNPI,isnull(Pr.sFAX,'') Fax from Patient p inner join Prescription pt on p.npatientid=pt.npatientid inner join Provider_Mst pr on pr.nproviderid=pt.nproviderid where pt.nPrescriptionID =" & objPrescription.RxTransactionID & "" 'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            sql.CommandText = _strsql
            sql.Connection = conn

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing

            If dt.Rows.Count > 0 Then
                objPrescription.RxPatient.PatientName.FirstName = dt.Rows(0)(0)
                objPrescription.RxPatient.PatientName.LastName = dt.Rows(0)(1)
                objPrescription.RxPatient.Gender = dt.Rows(0)(2)
                objPrescription.RxPatient.DateofBirth = dt.Rows(0)(3)
                objPrescription.RxPatient.PatientAddress.Address1 = dt.Rows(0)(4)
                objPrescription.RxPatient.PatientAddress.Address2 = dt.Rows(0)(5)
                objPrescription.RxPatient.PatientAddress.City = dt.Rows(0)(6)
                objPrescription.RxPatient.PatientAddress.State = dt.Rows(0)(7)
                objPrescription.RxPatient.PatientAddress.Zip = dt.Rows(0)(8)
                objPrescription.RxPatient.PatientPhone.Phone = dt.Rows(0)(9)
                objPrescription.RxPrescriber.PrescriberName.FirstName = dt.Rows(0)(10)
                objPrescription.RxPrescriber.PrescriberName.LastName = dt.Rows(0)(11)
                objPrescription.RxPrescriber.PrescriberAddress.Address1 = dt.Rows(0)(12)
                objPrescription.RxPrescriber.PrescriberAddress.Address2 = dt.Rows(0)(13)
                objPrescription.RxPrescriber.PrescriberAddress.City = dt.Rows(0)(14)
                objPrescription.RxPrescriber.PrescriberAddress.State = dt.Rows(0)(15)
                objPrescription.RxPrescriber.PrescriberAddress.Zip = dt.Rows(0)(16)
                objPrescription.RxPrescriber.PrescriberPhone.Phone = dt.Rows(0)(17)
                objPrescription.RxPrescriber.PrescriberID = dt.Rows(0)(18)
                objPrescription.RxPrescriber.PrescriberNPI = dt.Rows(0)("PrescriberNPI")
                objPrescription.RxPrescriber.PrescriberPhone.Fax = dt.Rows(0)("Fax")
                objPrescription.RxPharmacy.ContactID = dt.Rows(0)(19)
              

                dt.Dispose()
                dt = Nothing

                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing

                
                If Not IsNothing(objPrescription.RxPharmacy.ContactID) AndAlso objPrescription.RxPharmacy.ContactID <> "" Then
                    'Fetch the pharmacy information for given patient
                    sql = New SqlCommand
                    sql.Connection = conn
                    sql.CommandType = CommandType.Text
                    '_strsql = "select c.sname,c.sStreet,c.sCity,c.sstate,c.sZip,c.sPhone ,isnull(c.sNCPDPID,'') from Contacts_Mst c inner join Patient p on c.ncontactId=p.nPharmacyID inner join PatientTransaction pt on p.npatientid =pt.npatientid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
                    'old query to fetch Pharmacy details for Rx
                    '_strsql = "select c.sname,c.sStreet,c.sCity,c.sstate,c.sZip,c.sPhone ,isnull(c.sNCPDPID,'') from Contacts_Mst c inner join Patient p on c.ncontactId=p.nPharmacyID inner join PrescriptionTransaction pt on p.npatientid =pt.npatientid where pt.nRxTransactionID =" & objPrescription.RxTransactionID & "" 'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
                    'old query to fetch Pharmacy details for Rx

                    _strsql = "select isnull(c.sname,'') as sname, isnull(c.sAddressLine1,'') as sAddressLine1, isnull(c.sCity,'') as sCity, isnull(c.sstate,'') as sstate, isnull(c.sZip,'') as sZip, isnull(c.sPhone,'') as sPhone ,isnull(c.sNCPDPID,'') as sNCPDPID,ISNULL(c.sFax,'')AS Fax from Contacts_Mst c where c.nContactID =" & objPrescription.RxPharmacy.ContactID & "" 'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"

                    sql.CommandText = _strsql

                    dt = New DataTable
                    sqladpt = New SqlDataAdapter(sql)
                    sqladpt.Fill(dt)
                    sqladpt.Dispose()
                    sqladpt = Nothing

                    If dt.Rows.Count > 0 Then
                        objPrescription.RxPharmacy.PharmacyName = dt.Rows(0)("sname")
                        objPrescription.RxPharmacy.PharmacyAddress.Address1 = dt.Rows(0)("sAddressLine1")
                        objPrescription.RxPharmacy.PharmacyAddress.City = dt.Rows(0)("sCity")
                        objPrescription.RxPharmacy.PharmacyAddress.State = dt.Rows(0)("sstate")
                        objPrescription.RxPharmacy.PharmacyAddress.Zip = dt.Rows(0)("sZip")
                        objPrescription.RxPharmacy.PharmacyPhone.Phone = dt.Rows(0)("sPhone")
                        objPrescription.RxPharmacy.PharmacyID = dt.Rows(0)("sNCPDPID")
                        objPrescription.RxPharmacy.PharmacyPhone.Fax = dt.Rows(0)("Fax")
                        ' objPrescription.RxPharmacy.PharmacyNPI=""
                    End If

                    dt.Dispose()
                    dt = Nothing

                    sql.Parameters.Clear()
                    sql.Dispose()
                    sql = Nothing

                    
                End If
                'Fetch the pharmacy information for given patient
                sql = New SqlCommand
                sql.CommandType = CommandType.Text
                sql.Connection = conn
                '_strsql = "select isnull(ptd.sDrugName,'') as DrugName ,isnull(ptd.sDrugForm,'') as DrugForm,isnull(ptd.sStrength,'') as DrugStrength,isnull(ptd.sDosage,'') as DrugDosage,isnull(ptd.sRoute,'') as DrugRoute,isnull(ptd.sFrequency,'') as DrugFrequency,isnull(ptd.sDuration,'') as DrugDuration,isnull(ptd.sQuantity,'') as DrugQuantity,isnull(ptd.sQuantityQualifier,'') as DrugQuantityQualifier,isnull(ptd.sRefillQuantity,'') as RefillQuantity,isnull(ptd.sRefillQualifier,'') as RefillQualifier,isnull(ptd.bMaySubstitutions,0) as Maysubstitute,ptd.dtWrittendate as Writtendate from Prescriptiontransaction pt inner join PrescriptionTransactionDetails ptd on pt.nRxTransactionID=ptd.nRxTransactionID where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
                'get only drugs which have not been cancelled
                'query to fetch drug details that shall be used for cancellation,has been commented
                'If blnflag Then
                '    _strsql = "select isnull(ptd.sDrugName,'') as DrugName ,isnull(ptd.sDrugForm,'') as DrugForm,isnull(ptd.sStrength,'') as DrugStrength,isnull(ptd.sDosage,'') as DrugDosage,isnull(ptd.sRoute,'') as DrugRoute,isnull(ptd.sFrequency,'') as DrugFrequency,isnull(ptd.sDuration,'') as DrugDuration,isnull(ptd.sQuantity,'') as DrugQuantity,isnull(ptd.sQuantityQualifier,'') as DrugQuantityQualifier,isnull(ptd.sRefillQuantity,'') as RefillQuantity,isnull(ptd.sRefillQualifier,'') as RefillQualifier,isnull(ptd.bMaySubstitutions,'False') as Maysubstitute,ptd.dtWrittendate as Writtendate,nPrescriptionID as PrescriptionID from Prescriptiontransaction pt inner join PrescriptionTransactionDetails ptd on pt.nRxTransactionID=ptd.nRxTransactionID where ptd.sStatus<>'Cancelled' and pt.nRxTransactionID =" & objPrescription.RxTransactionID & ""  'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
                'Else
                '    _strsql = "select isnull(ptd.sDrugName,'') as DrugName ,isnull(ptd.sDrugForm,'') as DrugForm,isnull(ptd.sStrength,'') as DrugStrength,isnull(ptd.sDosage,'') as DrugDosage,isnull(ptd.sRoute,'') as DrugRoute,isnull(ptd.sFrequency,'') as DrugFrequency,isnull(ptd.sDuration,'') as DrugDuration,isnull(ptd.sQuantity,'') as DrugQuantity,isnull(ptd.sQuantityQualifier,'') as DrugQuantityQualifier,isnull(ptd.sRefillQuantity,'') as RefillQuantity,isnull(ptd.sRefillQualifier,'') as RefillQualifier,isnull(ptd.bMaySubstitutions,'False') as Maysubstitute,ptd.dtWrittendate as Writtendate,nPrescriptionID as PrescriptionID from Prescriptiontransaction pt inner join PrescriptionTransactionDetails ptd on pt.nRxTransactionID=ptd.nRxTransactionID where pt.nRxTransactionID =" & objPrescription.RxTransactionID & "" 'where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
                'End If
                'sql.CommandText = _strsql

                'dt = New DataTable
                'sqladpt = New SqlDataAdapter(sql)
                'sqladpt.Fill(dt)

                'Dim objdrug As EDrug
                'If dt.Rows.Count > 0 Then

                '    For icnt As Int32 = 0 To dt.Rows.Count - 1
                '        objdrug = New EDrug
                '        objdrug.DrugName = dt.Rows(icnt)(0)
                '        objdrug.Drugform = dt.Rows(icnt)(1)
                '        objdrug.DrugStrength = dt.Rows(icnt)(2)
                '        objdrug.Dosage = dt.Rows(icnt)(3)
                '        objdrug.DrugRoute = dt.Rows(icnt)(4)
                '        objdrug.DrugFrequency = dt.Rows(icnt)(5)
                '        objdrug.DrugDuration = dt.Rows(icnt)(6)
                '        objdrug.DrugQuantity = dt.Rows(icnt)(7)
                '        objdrug.DrugQuantityQualifier = dt.Rows(icnt)(8)
                '        objdrug.RefillQuantity = dt.Rows(icnt)(9)
                '        objdrug.MaySubstitute = dt.Rows(icnt)(11)
                '        objdrug.WrittenDate = dt.Rows(icnt)(12)
                '        objdrug.PrescriptionID = dt.Rows(icnt)(13)
                '        objdrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                '        objdrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
                '        objdrug.TransactionID = objdrug.PrescriptionID
                '        objPrescription.DrugsCol.Add(objdrug)
                '        objdrug = Nothing
                '    Next
                'End If
                'query to fetch drug details that shall be used for cancellation,has been commented

                If Not IsNothing(objPrescription.RxTransactionID) AndAlso objPrescription.RxTransactionID <> "" Then
                    _strsql = "select isnull(pt.sMedication,'') as DrugName ,isnull(pt.sDrugForm,'') as DrugForm,isnull(pt.sDosage,'') as DrugDosage, " _
                                            & " isnull(pt.sRoute,'') as DrugRoute,isnull(pt.sFrequency,'') as DrugFrequency,isnull(pt.sDuration,'') as DrugDuration," _
                                            & " isnull(pt.sAmount,'') as DrugQuantity,isnull(pt.sRefills,'') as RefillQuantity," _
                                            & " isnull(pt.sRefillQualifier,'') as RefillQualifier,isnull(pt.bMaySubstitute,'False') as Maysubstitute," _
                                            & " pt.dtPrescriptionDate as Writtendate,nPrescriptionID as PrescriptionID,sNotes,sFrequency from Prescription pt where " _
                                            & " pt.nPrescriptionID =" & objPrescription.RxTransactionID & ""
                    sql.CommandText = _strsql

                    dt = New DataTable
                    sqladpt = New SqlDataAdapter(sql)
                    sqladpt.Fill(dt)
                    sqladpt.Dispose()
                    sqladpt = Nothing

                    Dim objdrug As EDrug
                    If dt.Rows.Count > 0 Then

                        For icnt As Int32 = 0 To dt.Rows.Count - 1
                            objdrug = New EDrug
                            objdrug.DrugName = dt.Rows(icnt)(0)
                            objdrug.Drugform = dt.Rows(icnt)(1)
                            objdrug.Dosage = dt.Rows(icnt)(2)
                            objdrug.DrugRoute = dt.Rows(icnt)(3)
                            objdrug.DrugFrequency = dt.Rows(icnt)(4)
                            objdrug.DrugDuration = dt.Rows(icnt)(5)
                            objdrug.DrugQuantity = dt.Rows(icnt)(6)

                            objdrug.RefillQuantity = dt.Rows(icnt)(7)
                            objdrug.RefillsQualifier = dt.Rows(icnt)(8)
                            objdrug.MaySubstitute = dt.Rows(icnt)(9)
                            objdrug.WrittenDate = dt.Rows(icnt)(10)
                            objdrug.PrescriptionID = dt.Rows(icnt)(11)
                            objdrug.IseRxed = 0
                            objdrug.Notes = dt.Rows(icnt)(12)
                            objdrug.Directions = dt.Rows(icnt)(13)
                            objdrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                            objdrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"
                            objdrug.TransactionID = objdrug.PrescriptionID
                            objPrescription.DrugsCol.Add(objdrug)
                            objdrug = Nothing
                        Next
                    End If

                    dt.Dispose()
                    dt = Nothing
                    sql.Parameters.Clear()
                    sql.Dispose()
                    sql = Nothing

                End If
                Return True

            Else
               
                Return False
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
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

    Public Function GetPendingRefills(ByVal PrescriberId As String) As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim objDA As SqlDataAdapter = Nothing
        Dim dsData As DataSet = Nothing
        Dim oSQLCommand As SqlCommand = Nothing
        Dim osqlParmeter As SqlParameter = Nothing
        Try


            '''''-----------------------
            oSQLCommand = New SqlCommand
            osqlParmeter = New SqlParameter

            oSQLCommand.CommandType = CommandType.StoredProcedure
            oSQLCommand.CommandText = "gspGetPendingRefills"
            oSQLCommand.Connection = conn
            oSQLCommand.CommandTimeout = 0

            osqlParmeter.Direction = ParameterDirection.Input
            osqlParmeter.ParameterName = "@nProviderID"
            osqlParmeter.SqlDbType = SqlDbType.VarChar
            If PrescriberId = "" Then
                osqlParmeter.Value = 0
            Else
                osqlParmeter.Value = PrescriberId
            End If


            oSQLCommand.Parameters.Add(osqlParmeter)
            osqlParmeter = Nothing


            objDA = New SqlDataAdapter(oSQLCommand)
            dsData = New DataSet
            objDA.Fill(dsData)
            If dsData.Tables.Count > 0 Then
                If dsData.Tables(0).Rows.Count > 0 Then
                    Return dsData.Tables(0)
                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If

            If Not IsNothing(oSQLCommand) Then

                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            If Not IsNothing(osqlParmeter) Then
                osqlParmeter = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing

            End If
            If Not IsNothing(dsData) Then
                dsData.Dispose()
                dsData = Nothing
            End If
        End Try
    End Function

    Public Function GetRefillPrescription(ByVal MessageId As String) As EPrescription

        Dim objPrescription As New EPrescription()

        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        conn.Open()
        Dim sql As New SqlCommand
        Dim _strsql As String
        Try

            sql.CommandType = CommandType.Text

            _strsql = "select isnull(ptd.sPatientfirstname,'') as PatientFName, isnull(ptd.sPatientlastname,'') as PatientLastName," _
                   & " isnull(ptd.sPatientGender,'') as Gender, ptd.dtPatientdob as DOB, " _
                   & " isnull(ptd.sPatientMName,'') as PatientMiddleName, isnull(ptd.sPatientPrefix,'') as PatientPrefix," _
                   & " isnull(ptd.sPatientSuffix,'') as PatientSuffix, isnull(ptd.sPatientAddress1,'') as PatientAddress1," _
                   & " isnull(ptd.sPatientAddress2,'') as PatientAddress2, isnull(ptd.sPatientCity,'') as PatientCity," _
                   & " isnull(ptd.sPatientState,'') as PatientState, isnull(ptd.sPatientZipcode,'') as PatientZipCode," _
                   & " isnull(ptd.sPatientNumber,'') as PatientNumber, isnull(ptd.sPatientQualifier,'') as PatientQualifier, " _
                   & " isnull(ptd.sPatientWorkPhone,'') as PatientWorkPhone,isnull(ptd.sPatientEmail,'') as PatientEmail, " _
                   & " isnull(ptd.sPatientFax,'') as PatientFax," _
                   & " isnull(ptd.sPatientIdentifier,'') as PatientIdentifier,isnull(ptd.sPatientIdentifierType,'') as PatientIdentifierType," _
                   & " isnull(ptd.sPatientIdentifier1,'') as PatientIdentifier1,isnull(ptd.sPatientIdentifierType1,'') as PatientIdentifierType1," _
                   & " isnull(ptd.sPrFirstName,'') as Ptd_ProviderFirstName, isnull(ptd.sPrMName,'') as Ptd_ProviderMiddleName," _
                   & " isnull(ptd.sPrLastName,'') as Ptd_ProviderLastName, isnull(ptd.sPrPrefix,'') as Ptd_ProviderPrefix," _
                   & " isnull(ptd.sPrSuffix,'') as Ptd_ProviderSuffix, isnull(ptd.sPrAddress1,'') as Ptd_ProviderAddress1," _
                   & " isnull(ptd.sPrAddress2,'') as Ptd_ProviderAddress2, isnull(ptd.sPrCity,'') as Ptd_ProviderCity," _
                   & " isnull(ptd.sPrState,'') as Ptd_ProviderState, isnull(ptd.sPrZipcode,'') as Ptd_ProvidersZipcode," _
                   & " isnull(ptd.sPrNumber,'') as Ptd_ProviderNumber, isnull(ptd.sPrQualifier,'') as Ptd_ProvidersQualifier," _
                   & " isnull(ptd.sPrEmail,'') as Ptd_ProviderEmail, isnull(ptd.sPrAgentFirstName,'') as Ptd_ProviderAgentFirstName," _
                   & " isnull(ptd.sPrAgentMiddleName,'') as Ptd_ProviderAgentMiddleName, isnull(ptd.sPrAgentLastName,'') as Ptd_ProviderAgentLastName," _
                   & " isnull(ptd.sPrAgentLastName,'') as Ptd_ProviderAgentLastName, isnull(ptd.sPrAgentPrefix,'') as Ptd_ProviderAgentPrefix," _
                   & " isnull(ptd.sPrAgentSuffix,'') as Ptd_ProviderAgentSuffix, isnull(ptd.sPrSpecialtyType,'') as Ptd_ProviderSpecialtyType," _
                   & " isnull(ptd.sPrSpecialtyCode,'') as Ptd_ProviderSpecialtyCode,isnull(ptd.sPrFax,'') as ProviderFax," _
                   & " ptd.sRxReferenceNumber as RxReferenceNumber, ptd.sPharmacyId as PharmacyId, " _
                   & " isnull(pr.sServiceLevel,'0000000000000000') AS sServiceLevel, " _
                   & " pr.nProviderID as ProviderID,ptd.sPrescriberID as ProviderCode,isnull(sPrescriberClinic,'') as PrescriberClinic,isnull(sStatus,'') as MessageStatus,isnull(ptd.sPrNPI,'') as ProviderNPI," _
                   & " isnull(ptd.sPatientRelationship,'') as PatientRelationship,isnull(ptd.sPrescriberSSN,'') as PrescriberSSN,isnull(ptd.sDEA,'') as ProviderDEA,isnull(ptd.filedata,NULL) as FileData" _
                   & " from  PrescriptionRefillTransactiondetail ptd " _
                   & " inner join Provider_mst pr on ptd.sPrescriberID=pr.sSPIID " _
                   & " where ptd.sMessageID = '" & MessageId & "'"

            sql.CommandText = _strsql
            sql.Connection = conn

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)
            If dt.Rows.Count > 0 Then
                objPrescription.RxPatient.PatientName.FirstName = dt.Rows(0)("PatientFName")
                objPrescription.RxPatient.PatientName.LastName = dt.Rows(0)("PatientLastName")
                objPrescription.RxPatient.PatientName.MiddleName = dt.Rows(0)("PatientMiddleName")
                objPrescription.RxPatient.Gender = dt.Rows(0)("Gender")
                objPrescription.RxPatient.DateofBirth = dt.Rows(0)("DOB")

                objPrescription.RxPatient.PatientName.Prefix = dt.Rows(0)("PatientPrefix")
                objPrescription.RxPatient.PatientName.Suffix = dt.Rows(0)("PatientSuffix")
                objPrescription.RxPatient.PatientAddress.Address1 = dt.Rows(0)("PatientAddress1")
                objPrescription.RxPatient.PatientAddress.Address2 = dt.Rows(0)("PatientAddress2")
                objPrescription.RxPatient.PatientAddress.City = dt.Rows(0)("PatientCity")
                objPrescription.RxPatient.PatientAddress.State = dt.Rows(0)("PatientState")
                objPrescription.RxPatient.PatientAddress.Zip = dt.Rows(0)("PatientZipCode")

                objPrescription.RxPatient.PatientPhone.Phone = dt.Rows(0)("PatientNumber")
                objPrescription.RxPatient.PatientPhone.Qualifier = dt.Rows(0)("PatientQualifier")
                objPrescription.RxPatient.PatientWorkPhone.Phone = dt.Rows(0)("PatientWorkPhone")
                objPrescription.RxPatient.PatientPhone.Fax = dt.Rows(0)("PatientFax")
                objPrescription.RxPatient.PatientPhone.Email = dt.Rows(0)("PatientEmail")

                objPrescription.RxPatient.Identifier = dt.Rows(0)("PatientIdentifier")
                objPrescription.RxPatient.Identifier1 = dt.Rows(0)("PatientIdentifier1")

                objPrescription.RxPatient.IdentifierType = dt.Rows(0)("PatientIdentifierType")
                objPrescription.RxPatient.IdentifierType1 = dt.Rows(0)("PatientIdentifierType1")
                objPrescription.RxPatient.PatientRelationship = dt.Rows(0)("PatientRelationShip")


                objPrescription.RxPrescriber.PrescriberName.FirstName = dt.Rows(0)("Ptd_ProviderFirstName")
                objPrescription.RxPrescriber.PrescriberName.LastName = dt.Rows(0)("Ptd_ProviderLastName")
                objPrescription.RxPrescriber.PrescriberName.MiddleName = dt.Rows(0)("Ptd_ProviderMiddleName")
                objPrescription.RxPrescriber.PrescriberName.Prefix = dt.Rows(0)("Ptd_ProviderPrefix")
                objPrescription.RxPrescriber.PrescriberName.Suffix = dt.Rows(0)("Ptd_ProviderSuffix")
                objPrescription.RxPrescriber.PrescriberAddress.Address1 = dt.Rows(0)("Ptd_ProviderAddress1")
                objPrescription.RxPrescriber.PrescriberAddress.Address2 = dt.Rows(0)("Ptd_ProviderAddress2")
                objPrescription.RxPrescriber.PrescriberAddress.City = dt.Rows(0)("Ptd_ProviderCity")
                objPrescription.RxPrescriber.PrescriberAddress.State = dt.Rows(0)("Ptd_ProviderState")
                objPrescription.RxPrescriber.PrescriberAddress.Zip = dt.Rows(0)("Ptd_ProvidersZipcode")
                objPrescription.RxPrescriber.PrescriberPhone.Phone = dt.Rows(0)("Ptd_ProviderNumber")
                objPrescription.RxPrescriber.PrescriberPhone.Qualifier = dt.Rows(0)("Ptd_ProvidersQualifier")
                objPrescription.RxPrescriber.PrescriberEmail = dt.Rows(0)("Ptd_ProviderEmail")
                objPrescription.RxPrescriber.PrescriberPhone.Fax = dt.Rows(0)("ProviderFax")
                Dim strServiceLevel As String = Convert.ToString(dt.Rows(0)("sServiceLevel"))
                If strServiceLevel <> "" Then
                    If Mid(strServiceLevel, 5, 1) = 1 Then
                        objPrescription.RxPrescriber.IsEPCSEnable = True
                    Else
                        objPrescription.RxPrescriber.IsEPCSEnable = False
                    End If
                Else
                    objPrescription.RxPrescriber.IsEPCSEnable = False
                End If
                objPrescription.RxPrescriber.PrescAgntFName = dt.Rows(0)("Ptd_ProviderAgentFirstName")
                objPrescription.RxPrescriber.PrescAgntMName = dt.Rows(0)("Ptd_ProviderAgentMiddleName")
                objPrescription.RxPrescriber.PrescAgntLName = dt.Rows(0)("Ptd_ProviderAgentLastName")
                objPrescription.RxPrescriber.PrescAgntPrefix = dt.Rows(0)("Ptd_ProviderAgentPrefix")
                objPrescription.RxPrescriber.PrescAgntSuffix = dt.Rows(0)("Ptd_ProviderAgentSuffix")
                objPrescription.RxPrescriber.PrescSpcltyType = dt.Rows(0)("Ptd_ProviderSpecialtyType")
                objPrescription.RxPrescriber.PrescSpcltyCode = dt.Rows(0)("Ptd_ProviderSpecialtyCode")

                objPrescription.RxPrescriber.ProviderID = dt.Rows(0)("ProviderID") 'gloEMR Provider ID
                objPrescription.RxPrescriber.PrescriberID = dt.Rows(0)("ProviderCode") 'SPI ID
                objPrescription.RxPrescriber.PrescriberNPI = dt.Rows(0)("ProviderNPI")
                objPrescription.RxPrescriber.PrescriberDEA = dt.Rows(0)("ProviderDEA")
                objPrescription.RxReferenceNumber = dt.Rows(0)("RxReferenceNumber")
                objPrescription.RxPharmacy.PharmacyID = dt.Rows(0)("PharmacyId")
                objPrescription.ClinicName = dt.Rows(0)("PrescriberClinic")
                objPrescription.MessageStatus = dt.Rows(0)("MessageStatus")
                objPrescription.RxPrescriber.PrescriberSSN = dt.Rows(0)("PrescriberSSN")
                If Not IsDBNull(dt.Rows(0)("FileData")) Then
                    objPrescription.FileData = dt.Rows(0)("FileData")
                Else
                    objPrescription.FileData = Nothing
                End If
            End If

            dt.Dispose()
            dt = Nothing
            sql.Parameters.Clear()
            sql.Dispose()
            sql = Nothing

            sqladpt.Dispose()
            sqladpt = Nothing

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text

            _strsql = "select  isnull(ptd.sPhName,'') as PharmacyName,ISNULL(c.sServiceLevel,'') as PharmacyServiceLevel,  isnull(ptd.sPhFirstName,'') as PharmacyFirstName, isnull(ptd.sPhMName,'') as PharmacyMiddleName," _
                     & " isnull(ptd.sPhLastName,'') as PharmacyLastName, isnull(ptd.sPhPrefix,'') as PharmacyPrefix," _
                     & " isnull(ptd.sPhSuffix,'') as PharmacySuffix, isnull(ptd.sPhAddress1,'') as PharmacyAddress1," _
                     & " isnull(c.sAddressLine2,'') as PharmacyAddress2, isnull(ptd.sPhCity,'') as PharmacyCity," _
                     & " isnull(ptd.sPhState,'') as PharmacyState, isnull(ptd.sPhZipcode,'') as PharmacysZipcode," _
                     & " isnull(ptd.sPhNumber,'') as PharmacyNumber, isnull(ptd.sPhQualifier,'') as PharmacyQualifier," _
                     & " isnull(ptd.sPhAgentFirstName,'') as PharmacyAgentFirstName, isnull(ptd.sPhAgentMName,'') as PharmacyAgentMiddleName," _
                     & " isnull(ptd.sPhAgentLastName,'') as PharmacyAgentLastName, " _
                     & " isnull(ptd.sPhEmail,'') as PharmacyEmail,isnull(ptd.sPhFax,'') as PharmacyFax," _
                     & " isnull(c.ncontactId,0) as ContactId,isnull(ptd.sPharmacyNPI,'') as PharmacyNPI," _
                     & " isnull(ptd.sPharmacySpecialty,'') as PharmacySpeciality" _
                     & " from Contacts_Mst c " _
                     & " inner join PrescriptionRefillTransactiondetail as ptd on ptd.sPharmacyID=c.sNCPDPID " _
                     & " where ptd.sMessageID = '" & MessageId & "'"

            sql.CommandText = _strsql

            dt = New DataTable
            sqladpt = New SqlDataAdapter(sql)
            sqladpt.Fill(dt)

            If dt.Rows.Count > 0 Then
                objPrescription.RxPharmacy.PharmacyName = dt.Rows(0)("PharmacyName")
                objPrescription.RxPharmacy.PhServiceLevel = dt.Rows(0)("PharmacyServiceLevel")
                objPrescription.RxPharmacy.PharmacistFName = dt.Rows(0)("PharmacyFirstName")
                objPrescription.RxPharmacy.PharmacistMName = dt.Rows(0)("PharmacyMiddleName")
                objPrescription.RxPharmacy.PharmacistLName = dt.Rows(0)("PharmacyLastName")
                objPrescription.RxPharmacy.PharmacistPrefix = dt.Rows(0)("PharmacyPrefix")
                objPrescription.RxPharmacy.PharmacistSuffix = dt.Rows(0)("PharmacySuffix")
                objPrescription.RxPharmacy.PharmacyAddress.Address1 = dt.Rows(0)("PharmacyAddress1")
                objPrescription.RxPharmacy.PharmacyAddress.Address2 = dt.Rows(0)("PharmacyAddress2")
                objPrescription.RxPharmacy.PharmacyAddress.City = dt.Rows(0)("PharmacyCity")
                objPrescription.RxPharmacy.PharmacyAddress.State = dt.Rows(0)("PharmacyState")
                objPrescription.RxPharmacy.PharmacyAddress.Zip = dt.Rows(0)("PharmacysZipcode")
                objPrescription.RxPharmacy.PharmacyPhone.Phone = dt.Rows(0)("PharmacyNumber")
                objPrescription.RxPharmacy.PharmacyPhone.Qualifier = dt.Rows(0)("PharmacyQualifier")
                objPrescription.RxPharmacy.PharmacyPhone.Fax = dt.Rows(0)("PharmacyFax")

                objPrescription.RxPharmacy.PharmAgntFName = dt.Rows(0)("PharmacyAgentFirstName")
                objPrescription.RxPharmacy.PharmAgntMName = dt.Rows(0)("PharmacyAgentMiddleName")
                objPrescription.RxPharmacy.PharmAgntLName = dt.Rows(0)("PharmacyAgentLastName")
                objPrescription.RxPharmacy.PharmacyEmail = dt.Rows(0)("PharmacyEmail")
                objPrescription.RxPharmacy.ContactID = dt.Rows(0)("ContactId")
                objPrescription.RxPharmacy.PharmacyNPI = dt.Rows(0)("PharmacyNPI")
                objPrescription.RxPharmacy.PharmacySpeciality = dt.Rows(0)("PharmacySpeciality")

            End If

            dt.Dispose()
            dt = Nothing
            sql.Parameters.Clear()
            sql.Dispose()
            sql = Nothing

            sqladpt.Dispose()
            sqladpt = Nothing

            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            sql.Connection = conn

            _strsql = "select isnull(ptd.sDgClQualifier1,'') as DgClQualifier1, isnull(ptd.sPrimaryQualifier1,'') as " _
                      & " PrimaryQualifier1, isnull(ptd.sPrimaryValue1,'') as PrimaryValue1, " _
                      & " isnull(ptd.sSecQualifier1,'') as SecQualifier1, isnull(ptd.sSecValue1,'') as SecValue1, " _
                      & " isnull(ptd.sDgClQualifier2,'') as DgClQualifier2, " _
                      & " isnull(ptd.sPrimaryQualifier2,'') as PrimaryQualifier2, " _
                      & " isnull(ptd.sPrimaryValue2,'') as PrimaryValue2, isnull(ptd.sSecQualifier2,'') as SecQualifier2," _
                      & " isnull(ptd.sSecValue2,'') as SecValue2, isnull(ptd.sProductCode,'') as ProductCode," _
                      & " isnull(ptd.sProductCodeQualifier,'') as ProductCodeQualifier, isnull(sPotencyUnitCode,'') as PotencyUnitCode," _
                      & " isnull(ptd.sDosageForm,'') as DosageForm, isnull(ptd.sStrengthUnits,'') as StrengthUnits," _
                      & " isnull(ptd.sDrugDBCode,'') as DrugDBCode, isnull(ptd.sDrugDBCodeQualifier,'') as DrugDBCodeQualifier," _
                      & " isnull(ptd.sPriorAuthorizationQualifier,'') as PriorAuthorizationQualifier," _
                      & " isnull(ptd.sPriorAuthorizationValue,'') as PriorAuthorizationValue," _
                      & " isnull(ptd.sDrugName,'') as DrugName, isnull(ptd.sDrugForm,'') as DrugForm, " _
                      & " isnull(ptd.sStrength,'') as DrugStrength, isnull(ptd.sDosage,'') as DrugDosage," _
                      & " isnull(ptd.sRoute,'') as DrugRoute, isnull(ptd.sFrequency,'') as DrugFrequency," _
                      & " isnull(ptd.sDuration,'') as DrugDuration, isnull(ptd.sQuantity,'') as DrugQuantity," _
                      & " isnull(ptd.sQuantityQualifier,'') as DrugQuantityQualifier, " _
                      & " isnull(ptd.sRefillQuantity,0) as RefillQuantity, " _
                      & " isnull(ptd.sRefillQualifier,'') as RefillQualifier, isnull(ptd.bMaySubstitutions,'False') as Maysubstitute," _
                      & " isnull(ptd.dtWrittendate,'') as Writtendate, ISNULL(CONVERT(VARCHAR(10),CONVERT(DATETIME,ptd.dtlastdate,101),101),'') as dtlastdate, ptd.sRxReferenceNumber as RxReferenceNumber, ptd.sMessageID as MessageID," _
                      & " isnull(ptd.sNotes,'') as Notes, ptd.sPharmacyID,isnull(ptd.sDrugCoverageStatusCode,'') as DrugCoverageStatusCode, isnull(ptd.sMDDrugName,'') as MDDrugName,isnull(ptd.sMDDrugForm,'') as MDDrugForm, " _
                      & " isnull(ptd.sMDStrength,'') as MDStrength,isnull(ptd.sMDDosage,'') as MDDosage, isnull(ptd.sMDDosageForm,'') as MDDosageForm,isnull(ptd.sMDRoute,'') as MDRoute, " _
                      & " isnull(ptd.sMDFrequency,'') as MDFrequency, isnull(ptd.sMDDuration,'') as MDDuration, isnull(ptd.sMDQuantity,'') as MDQuantity, " _
                      & " isnull(ptd.sMDRefillQuantity,'') as MDRefillQuantity, isnull(ptd.sMDRefillQualifier,'') as MDRefillQualifier, isnull(ptd.MDbMaySubstitutions,'False') as MDbMaySubstitutions, " _
                      & " isnull(ptd.MDdtWrittendate,'') as MDdtWrittendate, isnull(ptd.sMDProductCode,'') as MDProductCode, isnull(ptd.sMDPotencyUnitCode,'') as MDPotencyUnitCode, " _
                      & " isnull(ptd.sMDStrengthUnits, '' ) as MDStrengthUnits, isnull(ptd.sMDDrugDBCode, '' ) as MDDrugDBCode, isnull(ptd.sMDDrugDBCodeQualifier, '' ) as MDDrugDBCodeQualifier," _
                      & " isnull(ptd.sMDProductCodeQualifier,'') as MDProductCodeQualifier,  isnull(ptd.sMDNotes, '' ) as sMDNotes, ISNULL(CONVERT(VARCHAR(10),CONVERT(DATETIME,ptd.MDdtlastdate,101),101),'') as MDdtlastdate, " _
                      & " isnull(ptd.sMDDrugCoverageStatusCode,'') as MDDrugCoverageStatusCode,Isnull(puc.sDescription,'')as DosageDescription,Isnull(mdpuc.sDescription,'')as MDDosageDescription    from PrescriptionRefillTransactiondetail ptd " _
                      & " Left outer join  PotencyCodeMaster puc on ptd.sPotencyUnitCode = puc.sPotencycode " _
                      & "Left outer join  PotencyCodeMaster mdpuc on ptd.sMDPotencyUnitCode = mdpuc.sPotencycode " _
                      & " where ptd.sMessageID = '" & MessageId & "'"
            sql.CommandText = _strsql

            dt = New DataTable
            sqladpt = New SqlDataAdapter(sql)
            sqladpt.Fill(dt)

            Dim objdrug As EDrug
            If dt.Rows.Count > 0 Then

                For icnt As Int32 = 0 To dt.Rows.Count - 1
                    objdrug = New EDrug
                    objdrug.DgClQualifier1 = dt.Rows(icnt)("DgClQualifier1")
                    objdrug.PrimaryQualifier1 = dt.Rows(icnt)("PrimaryQualifier1")
                    objdrug.PrimaryValue1 = dt.Rows(icnt)("PrimaryValue1")
                    objdrug.SecQualifier1 = dt.Rows(icnt)("SecQualifier1")
                    objdrug.SecValue1 = dt.Rows(icnt)("SecValue1")
                    objdrug.DgClQualifier2 = dt.Rows(icnt)("DgClQualifier2")
                    objdrug.PrimaryQualifier2 = dt.Rows(icnt)("PrimaryQualifier2")
                    objdrug.PrimaryValue2 = dt.Rows(icnt)("PrimaryValue2")
                    objdrug.SecQualifier2 = dt.Rows(icnt)("SecQualifier2")
                    objdrug.SecValue2 = dt.Rows(icnt)("SecValue2")
                    objdrug.ProdCode = dt.Rows(icnt)("ProductCode")
                    objdrug.ProdCodeQualifier = dt.Rows(icnt)("ProductCodeQualifier")
                    objdrug.DosageForm = dt.Rows(icnt)("DosageForm")
                    objdrug.StrengthUnits = dt.Rows(icnt)("StrengthUnits")
                    objdrug.DrugDBCode = dt.Rows(icnt)("DrugDBCode")
                    objdrug.DrugDBCodeQualifier = dt.Rows(icnt)("DrugDBCodeQualifier")
                    objdrug.PriorAuthorizationQualifier = dt.Rows(icnt)("PriorAuthorizationQualifier")
                    objdrug.PriorAuthorizationValue = dt.Rows(icnt)("PriorAuthorizationValue")

                    objdrug.DrugName = dt.Rows(icnt)("DrugName")
                    objdrug.Drugform = dt.Rows(icnt)("DrugForm")
                    objdrug.DrugStrength = dt.Rows(icnt)("DrugStrength")
                    objdrug.Dosage = dt.Rows(icnt)("DrugDosage")
                    objdrug.DrugRoute = dt.Rows(icnt)("DrugRoute")
                    objdrug.DrugFrequency = dt.Rows(icnt)("DrugFrequency")
                    objdrug.Directions = objdrug.DrugFrequency
                    objdrug.DrugDuration = dt.Rows(icnt)("DrugDuration")
                    objdrug.DrugQuantity = dt.Rows(icnt)("DrugQuantity")
                    objdrug.RefillQuantity = dt.Rows(icnt)("RefillQuantity")
                    'If objdrug.RefillQuantity.Trim.Length = 0 Then
                    '    objdrug.RefillQuantity = "0"
                    'End If
                    objdrug.RefillsQualifier = dt.Rows(icnt)("RefillQualifier")
                    If dt.Rows(icnt)("Maysubstitute") = False Then
                        objdrug.MaySubstitute = True
                    Else
                        objdrug.MaySubstitute = False
                    End If
                    'If CType(dt.Rows(icnt)(11), Boolean) = True Then
                    '    objdrug.MaySubstitute = False
                    'Else
                    '    objdrug.MaySubstitute = True
                    'End If

                    objdrug.DosageDescription = dt.Rows(icnt)("DosageDescription")
                    objdrug.MDDosageDescription = dt.Rows(icnt)("MDDosageDescription")

                    objdrug.WrittenDate = dt.Rows(icnt)("Writtendate")
                    objdrug.LastfillDate = dt.Rows(icnt)("dtlastdate")
                    objdrug.MessageFrom = "mailto:" & objPrescription.RxPrescriber.PrescriberID & ".spi@surescripts.com"
                    objdrug.MessageTo = "mailto:" & objPrescription.RxPharmacy.PharmacyID & ".ncpdp@surescripts.com"

                    objdrug.RxReferenceNumber = dt.Rows(icnt)("RxReferenceNumber")
                    objdrug.TransactionID = objdrug.RxReferenceNumber 'TransactionId for Individual Message Object
                    objdrug.RelatesToMessageId = dt.Rows(icnt)("MessageID")
                    objdrug.Notes = dt.Rows(icnt)("Notes")

                    objdrug.PotencyCode = dt.Rows(icnt)("PotencyUnitCode")
                    objdrug.DosageForm = GetDescriptionFromPotency(dt.Rows(icnt)("PotencyUnitCode"))
                    objdrug.DrugCoverageStatusCode = dt.Rows(icnt)("DrugCoverageStatusCode")

                    objdrug.MDDrugName = dt.Rows(icnt)("MDDrugName")
                    objdrug.MDDrugForm = dt.Rows(icnt)("MDDrugForm")
                    objdrug.MDStrength = dt.Rows(icnt)("MDStrength")
                    objdrug.MDDosage = dt.Rows(icnt)("MDDosage")
                    objdrug.MDDosageForm = dt.Rows(icnt)("MDDosageForm")
                    objdrug.MDRoute = dt.Rows(icnt)("MDRoute")
                    objdrug.MDFrequency = dt.Rows(icnt)("MDFrequency")
                    objdrug.MDDuration = dt.Rows(icnt)("MDDuration")
                    If dt.Rows(icnt)("MDDosageDescription") <> "" Then
                        objdrug.MDQuantity = dt.Rows(icnt)("MDQuantity") & " " & dt.Rows(icnt)("MDDosageDescription")
                    Else
                        objdrug.MDQuantity = dt.Rows(icnt)("MDQuantity")
                    End If

                    objdrug.MDRefillQuantity = dt.Rows(icnt)("MDRefillQuantity")
                    objdrug.MDRefillQualifier = dt.Rows(icnt)("MDRefillQualifier")

                    If dt.Rows(icnt)("MDbMaySubstitutions") = False Then
                        objdrug.MDbMaySubstitutions = True
                    Else
                        objdrug.MDbMaySubstitutions = False
                    End If
                    objdrug.MDdtWrittendate = dt.Rows(icnt)("MDdtWrittendate")
                    objdrug.MDProductCode = dt.Rows(icnt)("MDProductCode")
                    objdrug.MDPotencyUnitCode = dt.Rows(icnt)("MDPotencyUnitCode")
                    objdrug.MDDosageForm = GetDescriptionFromPotency(dt.Rows(icnt)("MDPotencyUnitCode"))
                    objdrug.MDdtlastdate = dt.Rows(icnt)("MDdtlastdate")
                    objdrug.MDProductCodeQualifier = dt.Rows(icnt)("MDProductCodeQualifier")
                    objdrug.MDStrengthUnits = dt.Rows(icnt)("MDStrengthUnits")
                    objdrug.MDDrugDBCode = dt.Rows(icnt)("MDDrugDBCode")
                    objdrug.MDDrugDBCodeQualifier = dt.Rows(icnt)("MDDrugDBCodeQualifier")
                    objdrug.MDNotes = dt.Rows(icnt)("sMDNotes")
                    objdrug.MDDrugCoverageStatusCode = dt.Rows(icnt)("MDDrugCoverageStatusCode")

                    objPrescription.DrugsCol.Add(objdrug)
                    objdrug = Nothing
                Next
                dt.Dispose()
                dt = Nothing
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing

                sqladpt.Dispose()
                sqladpt = Nothing

            End If
            Return objPrescription
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
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

    Friend Function GetDescriptionFromPotency(ByVal PotencyCode As String) As String
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim strDescription As String = Nothing
        Dim sql As SqlCommand = Nothing
        Try
            conn.Open()
            Dim _strsql As String = Nothing
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            sql.Connection = conn

            _strsql = " Select isnull(sDescription,'') from PotencyCodeMaster where sPotencycode='" & PotencyCode & "'"
            sql.CommandText = _strsql

            dt = New DataTable
            sqladpt = New SqlDataAdapter(sql)
            sqladpt.Fill(dt)
            If dt.Rows.Count > 0 Then
                strDescription = dt.Rows(0)(0)
                Return strDescription
            Else
                Return strDescription
            End If
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Parameters.Clear()
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
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

    Private disposedValue As Boolean = False        ' To detect redundant calls

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

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Public Function UpdateRefillStatus(ByVal objPrescription As EPrescription, ByVal estatus As RefillStatus, ByVal item As Integer) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String = ""
        Try


            Select Case estatus

                Case RefillStatus.eApproved
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Approved' where nRxTransactionId= " & objPrescription.RxTransactionID & " "
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Approved' where sRxReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
                    _strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Approved' where sMessageID= '" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "'"


                Case RefillStatus.eApprovedWithChanges
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='ApprovedWithChanges' where nRxTransactionId= " & objPrescription.RxTransactionID & " "
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='ApprovedWithChanges' where sRxReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
                    _strsql = "Update PrescriptionRefillTransactionDetail set sstatus='ApprovedWithChanges' where sMessageID= '" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "'"

                Case RefillStatus.eDenied
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Denied' where nRxTransactionId= " & objPrescription.RxTransactionID & " "
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Denied' where sRxReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
                    _strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Denied' where sMessageID= '" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "'"


                Case RefillStatus.eDeniedWithNewRxToFollow
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='DeniedWithNewRxToFollow' where nRxTransactionId= " & objPrescription.RxTransactionID & " "
                    '_strsql = "Update PrescriptionRefillTransactionDetail set sstatus='DeniedWithNewRxToFollow' where sRxReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
                    _strsql = "Update PrescriptionRefillTransactionDetail set sstatus='DeniedWithNewRxToFollow' where sMessageID= '" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "'"

            End Select
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            sql.CommandText = _strsql
            conn.Open()
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If


            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            '_strsql = "Update SureScriptMessageTransaction set IsAlertcheck='True' where sReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
            _strsql = "Update SureScriptMessageTransaction set IsAlertcheck='True' where nMessageID= '" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "' and sMessageName= 'RefillRequest'"

            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            Try
                sql = New SqlCommand
                sql.Connection = conn
                sql.CommandType = CommandType.Text
                '_strsql = "Update SureScriptMessageTransaction set IsAlertcheck='True' where sReferenceNumber= '" & objPrescription.RxReferenceNumber & "'"
                _strsql = "Update surescriptmessagetransaction set isalertcheck='True'" _
                          & " where nMessageID in (select s.nMessageID " _
                          & " from surescriptmessagetransaction s inner join surescriptmessagetransaction s1 " _
                          & " on s.sRelatesToMessageID= s1.nMessageID where " _
                          & " s1.sRelatesToMessageID='" & objPrescription.DrugsCol.Item(item).RelatesToMessageId & "'" _
                          & " and s.sMessageName='Error' and s1.sMessageName='RefillResponse') and sMessageName='Error'"

                sql.CommandText = _strsql
                sql.ExecuteNonQuery()

                If Not IsNothing(sql) Then
                    sql.Dispose()
                    sql = Nothing
                End If
            Catch ex As Exception

            End Try
            Return False
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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

    Public Function UpdateStatusCancel(ByVal Rxreferencenumber As String, ByVal MessageId As String) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String = ""
        Try


            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "Update SureScriptMessageTransaction set IsAlertcheck='True' where nMessageID= '" & MessageId & "'" ' and sMessageName= 'RefillRequest'"
            sql.CommandText = _strsql

            conn.Open()
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "Update PrescriptionRefillTransactionDetail set sstatus='Cancelled' where sMessageID= '" & MessageId & "' and sRxReferenceNumber = '" & Rxreferencenumber & "'"

            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            Return False
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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

    Public Function UpdateMessageTransaction(ByVal sReferenceNumber As String) As Boolean
        'Public Function UpdateMessageTransaction(ByVal MessageID As String) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String = ""
        Try
            conn.Open()

            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "Update SureScriptMessageTransaction set IsAlertcheck='True' where sReferenceNumber= '" & sReferenceNumber & "'"


            sql.CommandText = _strsql
            sql.ExecuteNonQuery()

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            Return False
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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

    Public Function GetPharmacyandPrescriberID(ByVal sReferenceNumber As String, ByVal stdatereceived As DateTime, ByVal MessageId As String) As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try

            sql.Connection = conn
            sql.CommandType = CommandType.Text
            '_strsql = "select pr.nProviderID,c.nContactID from  PrescriptionRefillTransactiondetail ptd " _
            '        & " inner join provider_mst pr on ptd.sPrescriberID=pr.sSPIID " _
            '        & " inner join contacts_mst c on ptd.sPharmacyID=c.sNCPDPID inner join " _
            '        & " Surescriptmessagetransaction s on ptd.sRxReferenceNumber =s.sReferenceNumber" _
            '        & " where ptd.sRxReferenceNumber = '" & sReferenceNumber & "'" _
            '        & " and s.dtdatereceived='" & stdatereceived & "'"
            ' ''sNCPDPID
            '\\Commented 20090828 : OLD LOGIC : get ncontactID for pharmacy ID
            ''_strsql = "select pr.nProviderID,c.nContactID,isnull(sProductCode,''),isnull(sProductCodeQualifier,'') from  PrescriptionRefillTransactiondetail ptd " _
            ''       & " inner join provider_mst pr on ptd.sPrescriberID=pr.sSPIID " _
            ''       & " inner join contacts_mst c on ptd.sPharmacyID=c.sNCPDPID inner join " _
            ''       & " Surescriptmessagetransaction s on ptd.sRxReferenceNumber =s.sReferenceNumber" _
            ''       & " where ptd.sRxReferenceNumber = '" & sReferenceNumber & "'" _
            ''       & " and s.dtdatereceived='" & stdatereceived & "' and ptd.sMessageId='" & MessageId & "'"

            '\\ change 20090828 : NEW LOGIC : replace ncontactID with sNCPDID [for pharmacy ID]
            _strsql = "select pr.nProviderID,c.sNCPDPID,isnull(sProductCode,'') as sProductCode,isnull(sProductCodeQualifier,'') as sProductCodeQualifier , isnull(c.nContactID,0) as nPhContactID " _
                 & " from  PrescriptionRefillTransactiondetail ptd " _
                 & " inner join provider_mst pr on ptd.sPrescriberID=pr.sSPIID " _
                 & " inner join contacts_mst c on ptd.sPharmacyID=c.sNCPDPID inner join " _
                 & " Surescriptmessagetransaction s on ptd.sRxReferenceNumber =s.sReferenceNumber" _
                 & " where ptd.sRxReferenceNumber = '" & sReferenceNumber.Replace("'", "''") & "'" _
                 & " and s.dtdatereceived='" & stdatereceived & "' and ptd.sMessageId='" & MessageId & "'"

            sql.CommandText = _strsql
            conn.Open()

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
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

    Public Function GetErrorMessageDetails(ByVal objError As SureScriptErrorMessage) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)

        conn.Open()
        Dim sql As New SqlCommand
        Dim _strsql As String
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        'Dim dt1 As New DataTable
        Dim Parameter As SqlParameter
        Try

            sql.CommandType = CommandType.StoredProcedure
            '_strsql = "select isnull(p.sfirstname,'') as PatientFName,isnull(p.slastname,'') as PatientLastName,isnull(p.sGender,'') as Gender,p.dtdob as DOB,isnull(p.sAddressLine1,'') as AddressLine1,isnull(p.sAddressLine2,'') as AddressLine2,isnull(p.sCity,'') as PatientCity,isnull(p.sState,'') as PatientState,isnull(p.sZip,'') as PatientZip,isnull(p.sPhone,'') as PatientPhone,isnull(pr.sFirstname,'') as ProviderFirstName ,isnull(pr.sLastName,'') as ProviderLastName,isnull(pr.sAddress,'') as Provideraddress1,isnull(pr.sStreet,'') as ProviderStreet,isnull(pr.sCity,'') as ProviderCity,isnull(pr.sState,'') as ProviderState,isnull(pr.sZip,'') as ProviderZip,isnull(pr.sphoneNo,'') as ProviderPhone,isnull(pr.sSPIID,'') as ProviderCode from Patient p inner join PrescriptionTransaction pt on p.npatientid=pt.npatientid inner join Visits v on pt.nvisitid=v.nvisitid inner join Provider_Mst pr on v.nproviderid=pr.nproviderid where pt.dtprescriptiondate = '" & objPrescription.PrescriptionDate & "'"
            _strsql = "select isnull(e.sErrorCode,'') as ErrorCode,isnull(e.sDescriptionCode,'') as DescriptionCode, isnull(e.sDescription,'') as sDescription, isnull(s1.sMessageName,'') as MessageName," _
                    & " isnull(s1.sReferenceNumber,'') as ReferenceNumber,s1.nMessageID, s1.sRelatesToMessageID,s1.dtDateReceived,s.nMessageID,s.dtDateReceived  from ErrorTransaction  e  inner join SureScriptMessageTransaction s on convert(varchar(50),e.nTransactionID) = s.sReferenceNumber inner join SureScriptMessageTransaction s1 on s.sRelatesToMessageID= s1.nMessageID where e.nTransactionID= " & objError.TransactionID & " and s.sMessageName='Error'"


            sql.CommandText = "gsp_GetErrorDetails"
            Parameter = New SqlParameter("@nRxTransactionID", objError.TransactionID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.String
            sql.Parameters.Add(Parameter)
            Parameter = Nothing

            Parameter = New SqlParameter("@MessageType", "Error")
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.String
            sql.Parameters.Add(Parameter)

            Parameter = Nothing
            sql.Connection = conn
            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            Dim sReferenceNumber As String
            If dt.Rows.Count > 0 Then
                objError.ErrorCode = dt.Rows(0)("ErrorCode") 'Error Code comes from Error Transaction
                objError.DescriptionCode = dt.Rows(0)("DescriptionCode") 'error descriptioncode comes from Error Transaction
                objError.Description = dt.Rows(0)("sDescription") 'error description comes from Error Transaction
                objError.RelatesToMessageName = dt.Rows(0)("MessageName") 'RelatesToMessageName,name of message for which error occurred ,comes from Surescriptmessagetransaction
                objError.MessageID = dt.Rows(0)("nMessageID")  'Message Id of the Error message,surescriptmessagetransaction
                If dt.Rows(0)("dtDateReceived").ToString <> "" Then
                    objError.DateReceived = CType(dt.Rows(0)("dtDateReceived"), DateTime) 'Date when Error message was received,surescriptmessagetransaction
                End If

                objError.MessageName = "Error"
                sReferenceNumber = dt.Rows(0)("ReferenceNumber") 'This is the transaction Id of the message for which error ocurred,
                'ex RefillResponse/NewRx

                sql.Dispose()
                sql = Nothing

                sqladpt.Dispose()
                sqladpt = Nothing

                Select Case objError.RelatesToMessageName

                    Case "NewRx"
                        objError.PrescriptionObject.RxTransactionID = sReferenceNumber
                        GetNewPrescription(objError.PrescriptionObject)
                        If Not IsNothing(objError.PrescriptionObject.DrugsCol) Then
                            If objError.PrescriptionObject.DrugsCol.Count > 0 Then
                                objError.PrescriptionObject.DrugsCol.Item(0).MessageName = "NewRx"
                                objError.PrescriptionObject.DrugsCol.Item(0).MessageID = dt.Rows(0)("SSMessageID") 'message id of NewRx
                                objError.PrescriptionObject.DrugsCol.Item(0).RelatesToMessageId = dt.Rows(0)("sRelatesToMessageID")
                                objError.PrescriptionObject.DrugsCol.Item(0).DateReceived = dt.Rows(0)("dtDateReceived")
                            End If

                        End If

                    Case "RefillResponse"
                        objError.PrescriptionObject.RxTransactionID = sReferenceNumber 'transactionid of refillresponse
                        objError.PrescriptionObject.RxReferenceNumber = sReferenceNumber 'transactionid of refillresponse
                        objError.PrescriptionObject = GetRefillPrescription(dt.Rows(0)("sRelatesToMessageID"))

                        If IsNothing(objError.PrescriptionObject) Then
                            GetErrorMessageDetails = Nothing
                            Exit Function
                        Else
                            If objError.PrescriptionObject.DrugsCol.Count > 0 Then
                                objError.PrescriptionObject.DrugsCol.Item(0).MessageName = "RefillResponse"
                                objError.PrescriptionObject.DrugsCol.Item(0).RelatesToMessageId = dt.Rows(0)("sRelatesToMessageID") 'messageid of refreq for which refillresponse posted
                            End If
                        End If
                End Select
            End If
            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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

    '*************************function written by Sagaar k. on 16 feb 2008
    Public Function GetErrorTransaction_Details(ByVal PrescriberID As String) As DataTable
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String = ""
        Dim sqladpt As New SqlDataAdapter
        Dim Parameter As SqlParameter = Nothing
        Dim dt As New DataTable
        Try

            sql.Connection = conn
            sql.CommandType = CommandType.StoredProcedure
            '_strsql = "select distinct e.nTransactionID as TransactionId, isnull(s.sRelatesToMessageID,'') as RelatesToMsgId, sErrorCode as ErrorCode, sDescriptionCode as DescriptionCode, sDescription as Description from ErrorTransaction e inner join SureScriptMessageTransaction s on Convert(varchar(50),e.nTransactionID)=s.sReferenceNumber   where substring(s.sMessageTo,8,13) ='" & PrescriberID & "' and s.sMessageName='Error' and nMessageId <> '0' and sRelatesToMessageID <> '' and IsAlertCheck ='False'" +
            '          " UNION select distinct e.nTransactionID as TransactionId, isnull(s.sRelatesToMessageID,'') as RelatesToMsgId, sErrorCode as ErrorCode, sDescriptionCode as DescriptionCode, sDescription as Description from ErrorTransaction e  inner join SureScriptMessageTransaction s on Convert(varchar(50),e.nTransactionID)=s.sReferenceNumber where (s.sMessageTo) ='" & PrescriberID & "' and s.sMessageName='Error' and nMessageId <> '0' and sRelatesToMessageID <> '' and IsAlertCheck ='False'"
            sql.CommandText = "gsp_getSurescriptErrorTransactionDetail"
            Parameter = New SqlParameter("@PrescriberID", PrescriberID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.String
            sql.Parameters.Add(Parameter)
            'Parameter = Nothing

            conn.Open()

            sqladpt.SelectCommand = sql
            sqladpt.Fill(dt)

            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return Nothing
        Finally
            If Not IsNothing(Parameter) Then
                Parameter = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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
    '*****************function written by Sagaar k. on 16 feb 2008

    Function UpdateMedicationStatus(ByVal PatientID As Int64, ByVal TransactionID As Int64, ByVal Status As String, Optional ByVal sLoginName As String = "")
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand()
        Dim objParameter As SqlParameter = Nothing

        Try
            sql.CommandType = CommandType.StoredProcedure
            sql.CommandText = "gspUpdateMedicationStatus"
            sql.Connection = conn

            objParameter = New SqlParameter
            objParameter.Value = TransactionID
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlDbType = SqlDbType.BigInt
            objParameter.ParameterName = "@nPrescription"
            sql.Parameters.Add(objParameter)
            objParameter = Nothing

            objParameter = New SqlParameter
            objParameter.Value = Status
            objParameter.Direction = ParameterDirection.Input
            objParameter.SqlDbType = SqlDbType.VarChar
            objParameter.ParameterName = "@sStatus"
            sql.Parameters.Add(objParameter)
            objParameter = Nothing

            If Status = "Cancelled" Or Status = "Discontinued" Then

                objParameter = New SqlParameter
                objParameter.Value = sLoginName
                objParameter.Direction = ParameterDirection.Input
                objParameter.SqlDbType = SqlDbType.VarChar
                objParameter.ParameterName = "@LoginName"
                sql.Parameters.Add(objParameter)
                objParameter = Nothing
            End If

            sql.Connection.Open()
            sql.ExecuteNonQuery()
            sql.Connection.Close()

            gloAuditTrail.gloAuditTrail.DatabaseConnectionString = gloSurescriptGeneral.GetconnectionString()
            gloAuditTrail.gloAuditTrail.CreateAuditLogService(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.Status, "Medication status updated to " + Status, PatientID, TransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloRxSniffer, True)

        Catch ex As Exception
            Throw ex
            Return Nothing

        Finally
            If sql IsNot Nothing Then
                sql.Connection.Close()
                sql.Dispose()
                sql = Nothing
            End If

            If conn IsNot Nothing Then
                conn.Dispose()
                conn = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Public Sub InsertIntoMessageTransaction(ByVal ResponseMessage As Schema.MessageType, ByVal MessageName As String)
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand()
        Dim param As SqlParameter = Nothing

        Try

            sql.Connection = conn
            sql.CommandType = CommandType.StoredProcedure
            sql.CommandText = "sc_InsertRxMsgTransaction"

            param = New SqlParameter()
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.MessageID), "", ResponseMessage.Header.MessageID)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@nMessageID"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = MessageName
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sMessageName"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.RelatesToMessageID), "", ResponseMessage.Header.RelatesToMessageID)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sRelatesToMessageID"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.From.Value), "", ResponseMessage.Header.From.Value)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sMessageFrom"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.To.Value), "", ResponseMessage.Header.To.Value)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sMessageTo"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = Date.Now
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sDateTimeStamp"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = Date.Now
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.DateTime
            param.ParameterName = "@dtDateReceived"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.RxReferenceNumber), "", ResponseMessage.Header.RxReferenceNumber)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sReferenceNumber"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = False
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@IsAlertCheck"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = DBNull.Value
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sSenderSoftwareVersion"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = DBNull.Value
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sSenderSoftwareDeveloper"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = DBNull.Value
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sSenderSoftwareProduct"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = DBNull.Value
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@FileXML"
            sql.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            param.Value = IIf(String.IsNullOrWhiteSpace(ResponseMessage.Header.PrescriberOrderNumber), "", ResponseMessage.Header.PrescriberOrderNumber)
            param.Direction = ParameterDirection.Input
            param.SqlDbType = SqlDbType.VarChar
            param.ParameterName = "@sPrescriberOrderNumber"
            sql.Parameters.Add(param)
            param = Nothing

            conn.Open()
            sql.ExecuteNonQuery()

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)

        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Insert Acknowledgements
    ''' </summary>
    ''' <param name="objStatus"></param>
    ''' <param name="blnType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertAcknowledgements(ByVal objStatus As StatusMessage, ByVal blnType As Boolean) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String
        conn.Open()
        Try

            sql.Connection = conn
            sql.CommandType = CommandType.Text
            _strsql = "select isnull(max(isnull(nTransactionId,0)),0)+1 from AcknowledgementTransaction"
            sql.CommandText = _strsql

            Dim Id As Int64 = sql.ExecuteScalar

            objStatus.TransactionID = Id.ToString

            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If

            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            If blnType Then
                _strsql = "Insert into AcknowledgementTransaction (nTransactionID,sCode,sType) values ( '" & objStatus.TransactionID & "','" & objStatus.StatusCode & "',1)"
            Else
                _strsql = "Insert into AcknowledgementTransaction (nTransactionID,sCode,sType) values ( '" & objStatus.TransactionID & "','" & objStatus.StatusCode & "',0)"
            End If

            sql.CommandText = _strsql
            sql.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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


    ''' <summary>
    ''' 
    ''' Used to insert Response data (ex Refill Response details)
    ''' </summary>
    ''' <param name="oResponseMessage"></param>
    ''' <param name="blnType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertResponseTransaction(ByVal oResponseMessage As SureScriptResponseMessage, ByVal blnType As Boolean) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As New SqlCommand
        Dim _strsql As String = ""
        conn.Open()
        Dim _DeniedStatus As String = ""

        Try

            sql.Connection = conn
            sql.CommandType = CommandType.Text
            Dim strApproved As String
            Select Case oResponseMessage.ApprovalStatus

                Case True
                    strApproved = "AP"
                Case False
                    strApproved = "DN"
                Case Else
                    strApproved = "DR"

            End Select
            If oResponseMessage.StatusMessageType <> "" Then
                Dim sDeniedMessage As String() = Split(oResponseMessage.StatusMessageType, ":")
                If sDeniedMessage.Length > 2 Then
                    _DeniedStatus = sDeniedMessage(5)
                End If
            End If

            Dim sDenyReason As String = ""
            If Not IsNothing(oResponseMessage.DenialReason) Then
                sDenyReason = oResponseMessage.DenialReason.Replace("'", "''")
            End If
            Dim sNotes As String = ""
            If Not IsNothing(oResponseMessage.Notes) Then
                sNotes = oResponseMessage.Notes.Replace("'", "''''")
            End If
            If blnType Then
                ''GLO2011-0014383 Refill error when an apostrophe is added to drug name
                _strsql = "Insert into ResponseTransaction (nMessageID,sType,sNotes,sDenialReasoncode,sDenialReason,bType,nPatientID,nProviderId,eRxStatus,eRxStatusMessage) values ( '" & oResponseMessage.MessageID & "','" & strApproved & "','" & sNotes & "','" & If(Not IsNothing(oResponseMessage.Denialcode), oResponseMessage.Denialcode, DBNull.Value) & "','" & sDenyReason & "',1,'" & oResponseMessage.RefReqPatientId & "','" & oResponseMessage.ProviderId & "','" & _DeniedStatus & "','" & oResponseMessage.StatusMessageType.ToString.Replace("'", "''") & "')"
            Else
                ''GLO2011-0014383 Refill error when an apostrophe is added to drug name
                _strsql = "Insert into ResponseTransaction (nMessageID,sType,sNotes,sDenialReasoncode,sDenialReason,bType,nPatientID,nProviderId,eRxStatus,eRxStatusMessage) values ( '" & oResponseMessage.MessageID & "','" & strApproved & "','" & sNotes & "','" & If(Not IsNothing(oResponseMessage.Denialcode), oResponseMessage.Denialcode, DBNull.Value) & "','" & sDenyReason & "',0,'" & oResponseMessage.RefReqPatientId & "','" & oResponseMessage.ProviderId & "','" & _DeniedStatus & "','" & oResponseMessage.StatusMessageType.ToString.Replace("'", "''") & "')"

            End If
            sql.CommandText = _strsql
            sql.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
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


    ''' <summary>
    ''' Used to Extract Pharmacies
    ''' </summary>
    ''' <param name="oPharmacies"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertPharmacy(ByVal oPharmacies As Pharmacies) As Boolean
        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sql As SqlCommand
        Dim _strsql As String
        conn.Open()

        Try
            Dim opharmacy As Pharmacy
            For icnt As Int64 = 0 To oPharmacies.Count - 1
                opharmacy = oPharmacies.Item(icnt)

                sql = New SqlCommand
                sql.Connection = conn
                sql.CommandType = CommandType.Text

                _strsql = "select ncontactId from Contacts_mst where sNCPDPID='" & opharmacy.PharmacyID & "'"

                sql.CommandText = _strsql
                Dim ncontactId As Int64 = sql.ExecuteScalar

                If Not IsNothing(sql) Then
                    sql.Dispose()
                    sql = Nothing
                End If

                'If ncontactId <> 0 Then
                '    _strsql = "Update Contacts_Mst set sName='" & opharmacy.PharmacyName & "',sStreet='" & opharmacy.PharmacyAddress.Address1 & "',sCity='" & opharmacy.PharmacyAddress.City & "',sState='" & opharmacy.PharmacyAddress.State & "',sZip='" & opharmacy.PharmacyAddress.Zip & "',sPhone='" & opharmacy.PharmacyPhone.Phone & "',sFax='" & opharmacy.PharmacyPhone.Fax & "' where nContactId=" & ncontactId & ""
                '    sql = New SqlCommand
                '    sql.Connection = conn
                '    sql.CommandType = CommandType.Text
                '    sql.CommandText = _strsql
                '    sql.ExecuteNonQuery()
                '    If Not IsNothing(sql) Then
                '        sql.Dispose()
                '        sql = Nothing
                '    End If
                'Else
                sql = New SqlCommand
                sql.Connection = conn
                sql.CommandType = CommandType.Text
                _strsql = "select isnull(max(isnull(nContactId,0)),0)+1 from Contacts_Mst"
                sql.CommandText = _strsql

                Dim Id As Int64 = sql.ExecuteScalar

                If Not IsNothing(sql) Then
                    sql.Dispose()
                    sql = Nothing
                End If

                sql = New SqlCommand
                sql.Connection = conn
                sql.CommandType = CommandType.Text
                _strsql = "Insert into Contacts_Mst (nContactID,sName,sStreet,sCity,sState,sZip,sPhone,sFax,scontacttype,sNCPDPID) values ( " & Id & ",'" & opharmacy.PharmacyName & "','" & opharmacy.PharmacyAddress.Address1 & "','" & opharmacy.PharmacyAddress.City & "','" & opharmacy.PharmacyAddress.State & "','" & opharmacy.PharmacyAddress.Zip & "','" & opharmacy.PharmacyPhone.Phone & "','" & opharmacy.PharmacyPhone.Fax & "','Pharmacy','" & opharmacy.PharmacyID & "')"
                sql.CommandText = _strsql
                sql.ExecuteNonQuery()
                If Not IsNothing(sql) Then
                    sql.Dispose()
                    sql = Nothing
                End If
                'End If
            Next
            Return True

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False
        Finally

            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function GetPrescriptionPatientPharmacy(ByRef objPrescription As EPrescription) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim dAdapter As SqlDataAdapter = Nothing
        Dim dsDataset As DataSet = Nothing
        Dim Parameter As SqlParameter = Nothing
        Dim dtPatientProvider As DataTable = Nothing
        Dim dtPharmacy As DataTable = Nothing
        Dim dtSupervisingProvider As DataTable = Nothing
        Try
            cmd = New SqlCommand()
            conn = New SqlConnection(gloSurescriptGeneral.GetconnectionString)
            cmd.Connection = conn
            cmd.CommandText = "gsp_getERxDetails"
            cmd.CommandType = CommandType.StoredProcedure

            Parameter = New SqlParameter("@PrescriptionID", objPrescription.RxTransactionID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.Int64
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            Parameter = New SqlParameter("@SupervisingProviderID", objPrescription.RxSupervisorProviderID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.Int64
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            dAdapter = New SqlDataAdapter(cmd)
            dsDataset = New DataSet()
            dAdapter.Fill(dsDataset)


            dtPatientProvider = dsDataset.Tables(0)
            dtPharmacy = dsDataset.Tables(1)
            dtSupervisingProvider = dsDataset.Tables(2)

            If dtPatientProvider.Rows.Count > 0 Then
                If dtPatientProvider.Rows(0)("PatientSSN") = "0" Then
                    objPrescription.RxPatient.SSN = ""
                Else
                    objPrescription.RxPatient.SSN = dtPatientProvider.Rows(0)("PatientSSN")
                End If

                objPrescription.RxPatient.PatientName.FirstName = dtPatientProvider.Rows(0)("PatientFName")
                objPrescription.RxPatient.PatientName.MiddleName = dtPatientProvider.Rows(0)("PatientMName")
                objPrescription.RxPatient.PatientName.MiddleName = objPrescription.RxPatient.PatientName.MiddleName.Replace(".", "")
                objPrescription.RxPatient.PatientName.LastName = dtPatientProvider.Rows(0)("PatientLastName")
                objPrescription.RxPatient.Gender = dtPatientProvider.Rows(0)("Gender")
                objPrescription.RxPatient.DateofBirth = dtPatientProvider.Rows(0)("DOB")
                objPrescription.RxPatient.PatientAddress.Address1 = dtPatientProvider.Rows(0)("AddressLine1")
                objPrescription.RxPatient.PatientAddress.Address2 = dtPatientProvider.Rows(0)("AddressLine2")
                objPrescription.RxPatient.PatientAddress.City = dtPatientProvider.Rows(0)("PatientCity")
                objPrescription.RxPatient.PatientAddress.State = dtPatientProvider.Rows(0)("PatientState")
                objPrescription.RxPatient.PatientAddress.Zip = dtPatientProvider.Rows(0)("PatientZip")
                objPrescription.RxPatient.PatientPhone.Phone = dtPatientProvider.Rows(0)("PatientPhone")
                objPrescription.RxPatient.PatientWorkPhone.Phone = dtPatientProvider.Rows(0)("PatientWorkPhone")
                objPrescription.RxPatient.PatientPhone.Fax = dtPatientProvider.Rows(0)("PatientFax")
                objPrescription.RxPatient.PatientPhone.Email = dtPatientProvider.Rows(0)("PatientEmail")
                objPrescription.RxPrescriber.PrescriberName.FirstName = dtPatientProvider.Rows(0)("ProviderFirstName")
                objPrescription.RxPrescriber.PrescriberName.MiddleName = dtPatientProvider.Rows(0)("ProviderMName")
                objPrescription.RxPrescriber.PrescriberName.MiddleName = objPrescription.RxPrescriber.PrescriberName.MiddleName.Replace(".", "")

                objPrescription.RxPrescriber.PrescriberName.LastName = dtPatientProvider.Rows(0)("ProviderLastName")
                objPrescription.RxPrescriber.PrescriberAddress.Address1 = dtPatientProvider.Rows(0)("Provideraddress1")
                objPrescription.RxPrescriber.PrescriberAddress.Address2 = dtPatientProvider.Rows(0)("ProviderStreet")
                objPrescription.RxPrescriber.PrescriberAddress.City = dtPatientProvider.Rows(0)("ProviderCity")
                objPrescription.RxPrescriber.PrescriberAddress.State = dtPatientProvider.Rows(0)("ProviderState")
                objPrescription.RxPrescriber.PrescriberAddress.Zip = dtPatientProvider.Rows(0)("ProviderZip")
                objPrescription.RxPrescriber.PrescriberPhone.Phone = dtPatientProvider.Rows(0)("ProviderPhone")
                objPrescription.RxPrescriber.PrescriberPhone.Email = dtPatientProvider.Rows(0)("ProviderEmail")
                objPrescription.RxPrescriber.PrescriberPhone.Fax = dtPatientProvider.Rows(0)("ProviderFax")
                objPrescription.RxPrescriber.PrescriberID = dtPatientProvider.Rows(0)("ProviderCode")
                objPrescription.RxPrescriber.PrescriberNPI = dtPatientProvider.Rows(0)("ProviderNPI")
                objPrescription.RxPrescriber.PrescriberDEA = dtPatientProvider.Rows(0)("ProviderDEA")
                objPrescription.RxPrescriber.PrescriberSSN = dtPatientProvider.Rows(0)("ProviderSSN")
            End If


            If dtPharmacy.Rows.Count > 0 Then
                objPrescription.RxPharmacy.PharmacyName = dtPharmacy.Rows(0)("sName")
                objPrescription.RxPharmacy.PharmacyAddress.Address1 = dtPharmacy.Rows(0)("sAddressLine1")
                objPrescription.RxPharmacy.PharmacyAddress.City = dtPharmacy.Rows(0)("sCity")
                objPrescription.RxPharmacy.PharmacyAddress.State = dtPharmacy.Rows(0)("sState")
                objPrescription.RxPharmacy.PharmacyAddress.Zip = dtPharmacy.Rows(0)("sZIP")
                objPrescription.RxPharmacy.PharmacyPhone.Phone = dtPharmacy.Rows(0)("sPhone")
                objPrescription.RxPharmacy.PharmacyID = dtPharmacy.Rows(0)("sNCPDPID")
                objPrescription.RxPharmacy.PharmacyAddress.Address2 = dtPharmacy.Rows(0)("sAddressLine2")
                objPrescription.RxPharmacy.PharmacyPhone.Fax = dtPharmacy.Rows(0)("sFax")
                objPrescription.RxPharmacy.PharmacyPhone.Email = dtPharmacy.Rows(0)("sEmail")
                objPrescription.RxPharmacy.PhServiceLevel = dtPharmacy.Rows(0)("PhServiceLevel")
            End If

            If dtSupervisingProvider.Rows.Count > 0 Then
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName = dtSupervisingProvider.Rows(0)("ProviderFirstName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName = dtSupervisingProvider.Rows(0)("ProviderMName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName = objPrescription.RxPrescriber.PrescriberName.MiddleName.Replace(".", "")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName = dtSupervisingProvider.Rows(0)("ProviderLastName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1 = dtSupervisingProvider.Rows(0)("Provideraddress1")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2 = dtSupervisingProvider.Rows(0)("ProviderStreet")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City = dtSupervisingProvider.Rows(0)("ProviderCity")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State = dtSupervisingProvider.Rows(0)("ProviderState")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip = dtSupervisingProvider.Rows(0)("ProviderZip")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone = dtSupervisingProvider.Rows(0)("ProviderPhone")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email = dtSupervisingProvider.Rows(0)("ProviderEmail")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax = dtSupervisingProvider.Rows(0)("ProviderFax")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID = dtSupervisingProvider.Rows(0)("ProviderCode")
                objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA = dtSupervisingProvider.Rows(0)("ProviderDEA")
                objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI = dtSupervisingProvider.Rows(0)("ProviderNPI")
            End If



            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False

        Finally
            If Not IsNothing(dtSupervisingProvider) Then
                dtSupervisingProvider.Dispose()
                dtSupervisingProvider = Nothing
            End If
            If Not IsNothing(dtPharmacy) Then
                dtPharmacy.Dispose()
                dtPharmacy = Nothing
            End If
            If Not IsNothing(dtPatientProvider) Then
                dtPatientProvider.Dispose()
                dtPatientProvider = Nothing
            End If
            If Not IsNothing(dsDataset) Then
                dsDataset.Dispose()
                dsDataset = Nothing
            End If
            If Not IsNothing(dAdapter) Then
                dAdapter.Dispose()
                dAdapter = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Parameter) Then
                Parameter = Nothing
            End If
        End Try
    End Function
    Public Function GetClinicInformation(clinicId As Long) As DataTable

        Dim conn As New SqlConnection(gloSurescriptGeneral.GetconnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dtclinic As New DataTable

        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "select * from Clinic_MST where nclinicid = " & clinicId & ""
            sql.CommandText = _strsql
            sql.Connection = conn
            sqladpt.SelectCommand = sql
            sqladpt.Fill(dtclinic)
            Return dtclinic
        Catch
            Return Nothing
        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
        End Try
    End Function
    Public Function GetERxDetailsFromIDs(ByRef objPrescription As EPrescription, ByVal nPatientID As Int64, ByVal nProviderID As Int64, ByVal nPhamracyID As Int64, ByVal nSupervisorID As Int64) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim dAdapter As SqlDataAdapter = Nothing
        Dim dsDataset As DataSet = Nothing
        Dim Parameter As SqlParameter = Nothing
        Dim dtPatient As DataTable = Nothing
        Dim dtProvider As DataTable = Nothing
        Dim dtPharmacy As DataTable = Nothing
        Dim dtSupervisingProvider As DataTable = Nothing
        Try
            cmd = New SqlCommand()
            conn = New SqlConnection(gloSurescriptGeneral.GetconnectionString)
            cmd.Connection = conn
            cmd.CommandText = "gsp_getERxDetailsFromIDs"
            cmd.CommandType = CommandType.StoredProcedure

            Parameter = New SqlParameter("@PrescriberID", nProviderID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.Int64
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            Parameter = New SqlParameter("@PatientID", nPatientID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.Int64
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            Parameter = New SqlParameter("@PharmacyID", nPhamracyID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.String
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            Parameter = New SqlParameter("@SupervisingProviderID", nSupervisorID)
            Parameter.Direction = ParameterDirection.Input
            Parameter.DbType = DbType.Int64
            cmd.Parameters.Add(Parameter)
            Parameter = Nothing

            dAdapter = New SqlDataAdapter(cmd)
            dsDataset = New DataSet()
            dAdapter.Fill(dsDataset)


            dtPatient = dsDataset.Tables(0)
            dtProvider = dsDataset.Tables(1)
            dtPharmacy = dsDataset.Tables(2)
            dtSupervisingProvider = dsDataset.Tables(3)

            If dtPatient.Rows.Count > 0 Then
                If dtPatient.Rows(0)("PatientSSN") = "0" Then
                    objPrescription.RxPatient.SSN = ""
                Else
                    objPrescription.RxPatient.SSN = dtPatient.Rows(0)("PatientSSN")
                End If

                objPrescription.RxPatient.PatientName.FirstName = dtPatient.Rows(0)("PatientFName")
                objPrescription.RxPatient.PatientName.MiddleName = dtPatient.Rows(0)("PatientMName")
                objPrescription.RxPatient.PatientName.MiddleName = objPrescription.RxPatient.PatientName.MiddleName.Replace(".", "")
                objPrescription.RxPatient.PatientName.LastName = dtPatient.Rows(0)("PatientLastName")
                objPrescription.RxPatient.Gender = dtPatient.Rows(0)("Gender")
                objPrescription.RxPatient.DateofBirth = dtPatient.Rows(0)("DOB")
                objPrescription.RxPatient.PatientAddress.Address1 = dtPatient.Rows(0)("AddressLine1")
                objPrescription.RxPatient.PatientAddress.Address2 = dtPatient.Rows(0)("AddressLine2")
                objPrescription.RxPatient.PatientAddress.City = dtPatient.Rows(0)("PatientCity")
                objPrescription.RxPatient.PatientAddress.State = dtPatient.Rows(0)("PatientState")
                objPrescription.RxPatient.PatientAddress.Zip = dtPatient.Rows(0)("PatientZip")
                objPrescription.RxPatient.PatientPhone.Phone = dtPatient.Rows(0)("PatientPhone")
                objPrescription.RxPatient.PatientWorkPhone.Phone = dtPatient.Rows(0)("PatientWorkPhone")
                objPrescription.RxPatient.PatientPhone.Fax = dtPatient.Rows(0)("PatientFax")
                objPrescription.RxPatient.PatientPhone.Email = dtPatient.Rows(0)("PatientEmail")
            End If
            If dtProvider.Rows.Count > 0 Then
                objPrescription.RxPrescriber.PrescriberName.FirstName = dtProvider.Rows(0)("ProviderFirstName")
                objPrescription.RxPrescriber.PrescriberName.MiddleName = dtProvider.Rows(0)("ProviderMName")
                objPrescription.RxPrescriber.PrescriberName.MiddleName = objPrescription.RxPrescriber.PrescriberName.MiddleName.Replace(".", "")

                objPrescription.RxPrescriber.PrescriberName.LastName = dtProvider.Rows(0)("ProviderLastName")
                objPrescription.RxPrescriber.PrescriberAddress.Address1 = dtProvider.Rows(0)("Provideraddress1")
                objPrescription.RxPrescriber.PrescriberAddress.Address2 = dtProvider.Rows(0)("ProviderStreet")
                objPrescription.RxPrescriber.PrescriberAddress.City = dtProvider.Rows(0)("ProviderCity")
                objPrescription.RxPrescriber.PrescriberAddress.State = dtProvider.Rows(0)("ProviderState")
                objPrescription.RxPrescriber.PrescriberAddress.Zip = dtProvider.Rows(0)("ProviderZip")
                objPrescription.RxPrescriber.PrescriberPhone.Phone = dtProvider.Rows(0)("ProviderPhone")
                objPrescription.RxPrescriber.PrescriberPhone.Email = dtProvider.Rows(0)("ProviderEmail")
                objPrescription.RxPrescriber.PrescriberPhone.Fax = dtProvider.Rows(0)("ProviderFax")
                objPrescription.RxPrescriber.PrescriberID = dtProvider.Rows(0)("ProviderCode")
                objPrescription.RxPrescriber.PrescriberNPI = dtProvider.Rows(0)("ProviderNPI")
                objPrescription.RxPrescriber.PrescriberDEA = dtProvider.Rows(0)("ProviderDEA")
                objPrescription.RxPrescriber.PrescriberSSN = dtProvider.Rows(0)("ProviderSSN")
            End If


            If dtPharmacy.Rows.Count > 0 Then
                objPrescription.RxPharmacy.PharmacyName = dtPharmacy.Rows(0)("sName")
                objPrescription.RxPharmacy.PharmacyAddress.Address1 = dtPharmacy.Rows(0)("sAddressLine1")
                objPrescription.RxPharmacy.PharmacyAddress.City = dtPharmacy.Rows(0)("sCity")
                objPrescription.RxPharmacy.PharmacyAddress.State = dtPharmacy.Rows(0)("sState")
                objPrescription.RxPharmacy.PharmacyAddress.Zip = dtPharmacy.Rows(0)("sZIP")
                objPrescription.RxPharmacy.PharmacyPhone.Phone = dtPharmacy.Rows(0)("sPhone")
                objPrescription.RxPharmacy.PharmacyID = dtPharmacy.Rows(0)("sNCPDPID")
                objPrescription.RxPharmacy.PharmacyAddress.Address2 = dtPharmacy.Rows(0)("sAddressLine2")
                objPrescription.RxPharmacy.PharmacyPhone.Fax = dtPharmacy.Rows(0)("sFax")
                objPrescription.RxPharmacy.PharmacyPhone.Email = dtPharmacy.Rows(0)("sEmail")
                objPrescription.RxPharmacy.PhServiceLevel = dtPharmacy.Rows(0)("PhServiceLevel")
            End If

            If dtSupervisingProvider.Rows.Count > 0 Then
                objPrescription.RxSupervisorProviderID = nSupervisorID
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.FirstName = dtSupervisingProvider.Rows(0)("ProviderFirstName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName = dtSupervisingProvider.Rows(0)("ProviderMName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.MiddleName = objPrescription.RxPrescriber.PrescriberName.MiddleName.Replace(".", "")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberName.LastName = dtSupervisingProvider.Rows(0)("ProviderLastName")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address1 = dtSupervisingProvider.Rows(0)("Provideraddress1")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Address2 = dtSupervisingProvider.Rows(0)("ProviderStreet")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.City = dtSupervisingProvider.Rows(0)("ProviderCity")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.State = dtSupervisingProvider.Rows(0)("ProviderState")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberAddress.Zip = dtSupervisingProvider.Rows(0)("ProviderZip")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Phone = dtSupervisingProvider.Rows(0)("ProviderPhone")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Email = dtSupervisingProvider.Rows(0)("ProviderEmail")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberPhone.Fax = dtSupervisingProvider.Rows(0)("ProviderFax")
                objPrescription.RxSupervisorPrescriber.SupervisorPrescriberID = dtSupervisingProvider.Rows(0)("ProviderCode")
                objPrescription.RxSupervisorPrescriber.SupervisorProviderDEA = dtSupervisingProvider.Rows(0)("ProviderDEA")
                objPrescription.RxSupervisorPrescriber.SupervisorProviderNPI = dtSupervisingProvider.Rows(0)("ProviderNPI")
            End If



            Return True
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog(ex.Message & ":" & ex.Source)
            Throw New gloSurescriptDBException(ex.Message)
            Return False

        Finally
            If Not IsNothing(dtSupervisingProvider) Then
                dtSupervisingProvider.Dispose()
                dtSupervisingProvider = Nothing
            End If
            If Not IsNothing(dtPharmacy) Then
                dtPharmacy.Dispose()
                dtPharmacy = Nothing
            End If
            If Not IsNothing(dtProvider) Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
            If Not IsNothing(dtPatient) Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            If Not IsNothing(dsDataset) Then
                dsDataset.Dispose()
                dsDataset = Nothing
            End If
            If Not IsNothing(dAdapter) Then
                dAdapter.Dispose()
                dAdapter = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

End Class


