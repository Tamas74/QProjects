Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms


Public Class ClsContactDBLayer
    Implements IDisposable


    Public Sub New(ByVal ContactType As Boolean)
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        Type = ContactType
    End Sub

    Private Conn As SqlConnection
    'Private Adapter As System.Data.SqlClient.SqlDataAdapter

    Private Ds As System.Data.DataSet
    Private Dv As DataView
    'Private Tb As DataTable
    'Private Cmd As System.Data.SqlClient.SqlCommand
    Private Type As Boolean

    Public Function FetchData(ByVal strContactType As String)
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try

            Cmd = New SqlCommand("gsp_ViewContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strContactType
            Dim Adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = Cmd
            If (IsNothing(Ds) = False) Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            Adapter.Dispose()
            Adapter = Nothing
            'Tb = Ds.Tables(0)

            Dv = New DataView(Ds.Tables(0))
            Conn.Close()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- FetchData -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- FetchData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

        Return Ds
        'Return Ds
    End Function

    Public Function FetchContacts(ByVal strContactType As String, ByVal strFilter As String, ByVal nPatientID As Long)
        Dim objParam As SqlParameter = Nothing
        Dim objParam1 As SqlParameter = Nothing
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try


            Cmd = New SqlCommand("gsp_ViewContacts_Mst_Temp_Filtered", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@Type", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strContactType

            'Dim objParam1 As SqlParameter
            objParam1 = Cmd.Parameters.Add("@Filter", SqlDbType.VarChar, 2000)
            objParam1.Direction = ParameterDirection.Input
            objParam1.Value = strFilter

            objParam1 = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objParam1.Direction = ParameterDirection.Input
            objParam1.Value = nPatientID

            Dim Adapter As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = Cmd
            If (IsNothing(Ds) = False) Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            If (IsNothing(Adapter) = False) Then
                Adapter.Dispose()
                Adapter = Nothing
            End If


            Dv = New DataView(Ds.Tables(0))
            Conn.Close()


        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- FetchData -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- FetchData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If

            If Not IsNothing(objParam1) Then
                objParam1 = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

        'Return Ds
        Return Ds
    End Function

    Public Sub UpdateData(ByVal ArrList As ArrayList, ByVal arr1 As Array, ByVal arr2 As Array)
        Dim trcontacts As SqlTransaction = Nothing
        Dim trContactsBeginned As Boolean = False
        Dim objParamContactId As SqlParameter = Nothing
        Dim objParam As SqlParameter = Nothing
        Dim cmddetails As SqlCommand = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            'Dim objParamContactId As SqlParameter

            'Dim objParam As SqlParameter
            objParamContactId = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParamContactId.Direction = ParameterDirection.Input
            objParamContactId.Value = CType(ArrList.Item(0), System.Int64)

            objParam = Cmd.Parameters.Add("@sContactType", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(1), System.String)

            objParam = Cmd.Parameters.Add("@sStreet", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), System.String)

            objParam = Cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.String)

            objParam = Cmd.Parameters.Add("@sState", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(4), System.String)

            objParam = Cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(5), System.String)

            objParam = Cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(6), System.String)

            objParam = Cmd.Parameters.Add("@sFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(7), System.String)

            objParam = Cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(8), System.String)

            objParam = Cmd.Parameters.Add("@sPager", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(9), System.String)

            objParam = Cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(10), System.String)

            objParam = Cmd.Parameters.Add("@sURL", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(11), System.String)

            objParam = Cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(12), System.String)

            objParam = Cmd.Parameters.Add("@sName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(13), System.String)

            If Type = True Then
                objParam = Cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(14), System.String)


                objParam = Cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(15), System.String)

                objParam = Cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(16), System.String)


                objParam = Cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(17), System.String)


            End If
            If CType(ArrList.Item(1), System.String) = "Physician" Then
                objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(18), System.Int64)


                objParam = Cmd.Parameters.Add("@sDegree", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(19), System.String)

            End If

            If CType(ArrList.Item(1), System.String) = "Physician" Then
                objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(20), System.String)
            Else
                objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(17), System.String)
            End If
            If Type = False Then
                objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(14), System.Int64)

                objParam = Cmd.Parameters.Add("@sContact", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(15), System.String)
            End If

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()


            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            trcontacts = Conn.BeginTransaction
            trContactsBeginned = True
            Cmd.Transaction = trcontacts
            Cmd.ExecuteNonQuery()


            cmddetails = New SqlCommand("gsp_DeleteContacts_Detail", Conn)
            cmddetails.CommandType = CommandType.StoredProcedure
            cmddetails.Transaction = trcontacts

            objParam = cmddetails.Parameters.Add("@nContactsID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = objParamContactId.Value

            cmddetails.ExecuteNonQuery()
            cmddetails.Parameters.Clear()
            cmddetails.Dispose()
            cmddetails = Nothing
            Dim i As Integer
            For i = 0 To arr1.Length - 1
                cmddetails = New SqlCommand("gsp_InsertContacts_Details", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = trcontacts
                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objParamContactId.Value

                objParam = cmddetails.Parameters.Add("@nID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr1(i)

                objParam = cmddetails.Parameters.Add("@sType", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = "I"

                objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = GetPrefixTransactionID()

                cmddetails.ExecuteNonQuery()

                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next
            For i = 0 To arr2.Length - 1
                cmddetails = New SqlCommand("gsp_InsertContacts_Details", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = trcontacts

                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objParamContactId.Value

                objParam = cmddetails.Parameters.Add("@nID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr2(i)

                objParam = cmddetails.Parameters.Add("@sType", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = "H"

                objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = GetPrefixTransactionID()

                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next


            trcontacts.Commit()



            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Contact Details Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As SqlException
            If (trContactsBeginned) Then
                trcontacts.Rollback()
            End If
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- UpdateData -- " & ex.ToString)
        Catch ex As Exception
            If (trContactsBeginned) Then

                trcontacts.Rollback()
            End If
            UpdateLog("ClsContactDBLayer -- UpdateData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(cmddetails) Then
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            
            If Not IsNothing(objParamContactId) Then
                objParamContactId = Nothing
            End If

            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If (IsNothing(trcontacts) = False) Then
                trcontacts.Dispose()
                trcontacts = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try
    End Sub

    Public Function AddData(ByVal ArrList As ArrayList, ByVal arr1 As Array, ByVal arr2 As Array) As Long

        Dim TrContacts As SqlTransaction = Nothing
        Dim trContactsBeginned As Boolean = False
        Dim objParamContactId As SqlParameter = Nothing
        Dim objParam As SqlParameter = Nothing
        Dim cmddetails As SqlCommand = Nothing

        Dim _ReturnValue As Long = 0
        '--------------
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpContacts_Mst", Conn)

            Cmd.CommandType = CommandType.StoredProcedure

            objParamContactId = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParamContactId.Direction = ParameterDirection.InputOutput
            objParamContactId.Value = CType(ArrList.Item(0), System.Int64)

            objParam = Cmd.Parameters.Add("@sContactType", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            If IsDBNull(ArrList.Item(1)) Then
                objParam.Value = ""
            Else
                objParam.Value = CType(ArrList.Item(1), System.String)
            End If


            objParam = Cmd.Parameters.Add("@sStreet", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(2), System.String)

            objParam = Cmd.Parameters.Add("@sCity", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(3), System.String)

            objParam = Cmd.Parameters.Add("@sState", SqlDbType.VarChar, 10)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(4), System.String)

            objParam = Cmd.Parameters.Add("@sZip", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(5), System.String)

            objParam = Cmd.Parameters.Add("@sPhone", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(6), System.String)

            objParam = Cmd.Parameters.Add("@sFax", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(7), System.String)

            objParam = Cmd.Parameters.Add("@sMobile", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(8), System.String)

            objParam = Cmd.Parameters.Add("@sPager", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(9), System.String)

            objParam = Cmd.Parameters.Add("@sEmail", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(10), System.String)

            objParam = Cmd.Parameters.Add("@sURL", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(11), System.String)

            objParam = Cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(12), System.String)

            objParam = Cmd.Parameters.Add("@sName", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CType(ArrList.Item(13), System.String)

            If Type = True Then
                objParam = Cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(14), System.String)


                objParam = Cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(15), System.String)

                objParam = Cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(16), System.String)

                objParam = Cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(17), System.String)


            End If
            If CType(ArrList.Item(1), System.String) = "Physician" Then
                objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(18), System.Int64)


                objParam = Cmd.Parameters.Add("@sDegree", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(19), System.String)
            End If

            If CType(ArrList.Item(1), System.String) = "Physician" Then
                objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(20), System.String)
            Else
                objParam = Cmd.Parameters.Add("@sAddressLine2", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(17), System.String)
            End If

            If Type = False Then
                objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(14), System.Int64)

                objParam = Cmd.Parameters.Add("@sContact", SqlDbType.VarChar, 50)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = CType(ArrList.Item(15), System.String)
            End If

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            TrContacts = Conn.BeginTransaction
            trContactsBeginned = True


            Cmd.Transaction = TrContacts
            Cmd.ExecuteNonQuery()


            _ReturnValue = objParamContactId.Value



            Dim i As Integer
            For i = 0 To arr1.Length - 1

                cmddetails = New SqlCommand("gsp_InsertContacts_Details", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure

                cmddetails.Transaction = TrContacts
                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objParamContactId.Value

                objParam = cmddetails.Parameters.Add("@nID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr1(i)

                objParam = cmddetails.Parameters.Add("@sType", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = "I"

                objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = GetPrefixTransactionID()

                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next
            For i = 0 To arr2.Length - 1
                'Dim cmddetails As SqlCommand
                cmddetails = New SqlCommand("gsp_InsertContacts_Details", Conn)
                cmddetails.CommandType = CommandType.StoredProcedure
                cmddetails.Transaction = TrContacts

                objParam = cmddetails.Parameters.Add("@nContactID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = objParamContactId.Value

                objParam = cmddetails.Parameters.Add("@nID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = arr2(i)

                objParam = cmddetails.Parameters.Add("@sType", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = "H"

                objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = GetPrefixTransactionID()

                cmddetails.ExecuteNonQuery()
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            Next

            TrContacts.Commit()


            If _ReturnValue Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Contact Details Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If

            Return _ReturnValue

        Catch ex As SqlException
            If (trContactsBeginned) Then

                TrContacts.Rollback()
            End If
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- AddData -- " & ex.ToString)
            Return -1
        Catch ex As Exception
            If (trContactsBeginned) Then

                TrContacts.Rollback()
            End If
            UpdateLog("ClsContactDBLayer -- AddData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        Finally
            If Not IsNothing(cmddetails) Then
                cmddetails.Parameters.Clear()
                cmddetails.Dispose()
                cmddetails = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(objParamContactId) Then
                objParamContactId = Nothing
            End If
            If (IsNothing(TrContacts) = False) Then
                TrContacts.Dispose()
                TrContacts = Nothing
            End If
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try

    End Function

    Public Sub UpdateSpeciality(ByVal ID As Long, ByVal SpecialityID As Long, ByVal ContactType As String)
        Dim Cmd As SqlCommand = Nothing
        Try
            Conn.Open()
            Dim UpdateQry As String = "Update Contacts_MST set nSpecialtyID = '" & SpecialityID & "'  where nContactID = '" & ID & "' and sContactType = '" & ContactType & "'"
            Cmd = New SqlCommand(UpdateQry, Conn)
            Cmd.ExecuteNonQuery()
            Conn.Close()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- UpdateSpeciality -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- UpdateSpeciality -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub

    'Public Function selectSpeciality(ByVal ID As Long, ByVal ContactType As String) As ArrayList
    '    Dim SpecialityID As Long = 0
    '    Dim strDescription As String = ""
    '    Dim Cmd As SqlCommand = Nothing
    '    Try
    '        Conn.Open()
    '        Dim strSelectQry As String = "Select nSpecialtyID from Contacts_MST where nContactID = '" & ID & "' and sContactType = '" & ContactType & "'"
    '        Cmd = New SqlCommand(strSelectQry, Conn)
    '        If Not IsDBNull(Cmd.ExecuteScalar()) Then
    '            SpecialityID = Cmd.ExecuteScalar()
    '        Else
    '            SpecialityID = 0
    '        End If
    '        If (IsNothing(Cmd) = False) Then
    '            Cmd.Parameters.Clear()
    '            Cmd.Dispose()
    '            Cmd = Nothing
    '        End If
    '        Dim strDesQry = "Select sDescription from Specialty_MST where nSpecialtyID = '" & SpecialityID & "'"
    '        Cmd = New SqlCommand(strDesQry, Conn)
    '        If Not IsDBNull(Cmd.ExecuteScalar()) Then
    '            strDescription = Cmd.ExecuteScalar()
    '        Else
    '            strDescription = ""
    '        End If


    '        Dim arrlist As New ArrayList
    '        arrlist.Add(SpecialityID)
    '        arrlist.Add(strDescription)
    '        Conn.Close()
    '        Return arrlist
    '    Catch ex As SqlException
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        UpdateLog("ClsContactDBLayer -- selectSpeciality -- " & ex.ToString)
    '        Return Nothing
    '    Catch ex As Exception
    '        UpdateLog("ClsContactDBLayer -- selectSpeciality -- " & ex.ToString)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '        If (IsNothing(Cmd) = False) Then
    '            Cmd.Parameters.Clear()
    '            Cmd.Dispose()
    '            Cmd = Nothing
    '        End If
    '    End Try
    'End Function

    Public Sub DeleteData(ByVal id As Long)
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeleteContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id
            Conn.Open()
            Cmd.ExecuteNonQuery()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Contact Details Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- DeleteData -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- DeleteData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

    End Sub

    Public Sub DeletePhysician(ByVal id As Long)
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeleteContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id
            Conn.Open()
            Cmd.ExecuteNonQuery()

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Contact Details Deleted", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Contact Details Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- DeleteData -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- DeleteData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

    End Sub

    Public Function ChkPhyPatAss(ByVal id As Long) As Boolean
        Dim DELFlag As Boolean
        Dim _selQry As String = ""
        Dim cnt As Integer = 0
        Dim Cmd As SqlCommand = Nothing
        Try
            'check whether it matches the PCPid in Patient table
            Conn.Open()

            _selQry = "select count(*) from Patient where nPCPId=" & id

            Cmd = New SqlCommand
            Cmd.Connection = Conn
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = _selQry
            cnt = Cmd.ExecuteScalar
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Cmd = Nothing

            If cnt > 0 Then
                DELFlag = False
                Return DELFlag
            End If

            'check whether it matches the contactsid in Patient_dtl table
            _selQry = "select count(*) from Patient_Dtl where nContactId = " & id

            Cmd = New SqlCommand
            Cmd.CommandType = CommandType.Text
            Cmd.Connection = Conn
            Cmd.CommandText = _selQry
            cnt = Cmd.ExecuteScalar
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Cmd = Nothing

            If cnt > 0 Then
                DELFlag = False
            Else
                DELFlag = True
            End If

            Return DELFlag
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchDataForUpdate(ByVal id As Long, ByVal strcontacts As String) As ArrayList
        Dim arrlist As New ArrayList
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanContacts_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = Cmd.Parameters.Add("@ContactType", SqlDbType.VarChar, 15)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strcontacts

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()
                Dim i As Int16
                For i = 0 To 10
                    If Not IsDBNull(dreader.Item(i)) Then
                        arrlist.Add(dreader.Item(i))
                    Else
                        arrlist.Add("")
                    End If
                Next
                If ContactType = False Then

                    If Not IsDBNull(dreader.Item(11)) Then
                        arrlist.Add(dreader.Item(11))
                    Else
                        arrlist.Add("")
                    End If

                    If Not IsDBNull(dreader.Item(12)) Then
                        arrlist.Add(dreader.Item(12))
                    Else
                        arrlist.Add("")
                    End If

                    If Not IsDBNull(dreader.Item(13)) Then
                        arrlist.Add(dreader.Item(13))
                    Else
                        arrlist.Add("")
                    End If
                Else
                    For i = 11 To 14
                        If Not IsDBNull(dreader.Item(i)) Then
                            arrlist.Add(dreader.Item(i))
                        Else
                            arrlist.Add("")
                        End If
                        'arrlist.Add(dreader.Item(i))
                    Next
                    If strcontacts = "Physician" Then
                        ''''For loop count is increased by 1 on 20071122 for the new field "sDegree" 
                        For i = 15 To 21
                            arrlist.Add(dreader.Item(i))
                        Next
                    End If
                End If
            Loop
            Conn.Close()
            dreader.Close()
            dreader.Dispose()
            Return arrlist

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- FetchDataForUpdate -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- FetchDataForUpdate -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

        'Return Ds
    End Function

    Public ReadOnly Property DsDataSet() As DataSet
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Ds
            'Return Ds
        End Get
    End Property

    Public Property DsDataview() As DataView
        Get

            Return Dv

        End Get
        Set(ByVal Value As DataView)
            Dv = Value
        End Set
    End Property

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'Dv.Sort = strsort
        Dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub

    Public Property ContactType() As Boolean
        Get
            Return Type
        End Get
        Set(ByVal Value As Boolean)
            Type = Value
        End Set
    End Property

    Public Function FillControls(ByVal FillType As Char) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            If FillType = "S" Then
                Cmd = New SqlCommand("gsp_FillSpecialty_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
            Else
                Cmd = New SqlCommand("gsp_FillContacts_Mst", Conn)
                Cmd.CommandType = CommandType.StoredProcedure


                objParam = Cmd.Parameters.Add("@Type", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = FillType

            End If

            adpt.SelectCommand = Cmd
            adpt.Fill(ds)
            adpt.Dispose()
            adpt = Nothing
            Conn.Close()
            Return ds.Tables(0).Copy()
            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()

            'Do While dreader.Read
            '    Dim i As Integer
            '    i = dreader("nSpecialtyID")

            'Loop

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- FillControls -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- FillControls -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If (IsNothing(ds) = False) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Public Function FetchContactsDetail(ByVal id As Long, ByVal stype As Char) As DataTable
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanContacts_Detail", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@nContactId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = Cmd.Parameters.Add("@sType", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = stype

            sqladpt.SelectCommand = Cmd
            sqladpt.Fill(dt)
            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()
            'Dim arr() As Integer
            'Dim i As Integer
            'Do While dreader.Read()
            '    arr(i) = dreader.Item(0)
            '    i = i + 1
            'Loop
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- FetchContactsDetail -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- FetchContactsDetail -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If (IsNothing(sqladpt) = False) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
        End Try
    End Function

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = Dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String

        Dim arrstring() As String
        Try
            If Trim(strsplittext) <> "" Then

                arrstring = Split(strsplittext, " ")
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return strsplittext
        End Try
    End Function

    Public Function UpdateContactFAXNo(ByVal nContactID As Long, ByVal strFAXNo As String) As Boolean
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_UpdateContactFAXNo", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@ContactID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nContactID

            objParam = Cmd.Parameters.Add("@FAXNo", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strFAXNo

            Conn.Open()
            Cmd.ExecuteNonQuery()
            Conn.Close()
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("ClsContactDBLayer -- UpdateContactFAXNo -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("ClsContactDBLayer -- UpdateContactFAXNo -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                '23-Apr-13 Aniket: Resolving Memory Leaks
                'If IsNothing(Adapter) = False Then
                '    Adapter.Dispose()
                '    Adapter = Nothing
                'End If

                If IsNothing(Ds) = False Then
                    Ds.Dispose()
                    Ds = Nothing
                End If

                If IsNothing(Dv) = False Then
                    Dv.Dispose()
                    Dv = Nothing
                End If

                'If IsNothing(Tb) = False Then
                '    Tb.Dispose()
                '    Tb = Nothing
                'End If

                'If IsNothing(Cmd) = False Then
                '    Cmd.Dispose()
                '    Cmd = Nothing
                'End If

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


