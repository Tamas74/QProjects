Imports System.Data.SqlClient

Public Class clsDrugs
    'Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    ' Private conString As String
    Private nClinicID As Long = 0
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
    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        'grdCPT.DataSource = dv
        Return dv
    End Function

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' UpdateLog("clsDrugs -- New -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- New -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub

    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return ds
    '        'Return Ds
    '    End Get
    'End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv
            'Return Ds
        End Get
    End Property

    ''check for duplicate NDC in Drug_MST (Add NEW drug)
    Public Function IsNDCDuplicate(ByVal sNDC As String) As Boolean

        Try
            Dim cmd As New SqlCommand("gsp_IsDuplicateNDCDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@myNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sNDC

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    ''check for duplicate NDC in Drug_MST (Modify Drug)
    Public Function IsNDCDuplicateforModify(ByVal sNDC As String, ByVal nDrugID As Int64) As Boolean

        Try
            Dim cmd As New SqlCommand("gsp_IsDuplicateNDC_ModifyDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@myNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sNDC

            sqlParam = cmd.Parameters.Add("@mynDrugID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nDrugID

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function


    Public Function CheckDuplicate(ByVal ID As Long, ByVal DrugName As String, ByVal Dosage As String) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@DrugName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DrugName

            sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Dosage

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- CheckDuplicate -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- CheckDuplicate -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    ''CheckNDCinUsed(sNDC)
    ''check for NDC is in used in transaction -> in DrugProviderAssociation,Prescription,Medication
    Public Function CheckNDCinUsed(ByVal sNDC As String) As Boolean

        Try
            Dim cmd As New SqlCommand("gsp_IsNDCinUsed", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@myNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sNDC

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    ''Update old NDC with new NDC in -> 
    Public Function UpdateNDC(ByVal sOldNDC As String, ByVal sNewNDC As String) As Boolean

        Try
            Dim cmd As New SqlCommand("gsp_UpdateNDC", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@oldNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sOldNDC  'Old

            sqlParam = cmd.Parameters.Add("@NewNDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sNewNDC  'New

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function



    Public Function GetAllDrugs(ByVal ID As Int64) As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = New DataView(dt.Copy())
            dt.Dispose()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            Return dv

            ''DrugID= dv.Item(0)(0)
            ''DrugName= dv.Item(0)(1)
            ''GenericName= dv.Item(0)(2)
            ''Dosage= dv.Item(0)(3)
            ''Route= dv.Item(0)(4)
            ''Frequency= dv.Item(0)(5)
            ''Duration= dv.Item(0)(6)
            ''Clinical/NonClinical Drugs= dv.Item(0)(7)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- GetAllDrugs -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- GetAllDrugs -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try

    End Function

    Public Function AddNewDrug(ByVal ID As Long, ByVal Name As String, ByVal GenName As String, ByVal Dosage As String, ByVal Route As String, ByVal Frequency As String, ByVal Duration As String, ByVal IsClinicDrug As CheckState, ByVal stramount As String, ByVal strnarcotics As String, ByVal IsAllergicDrug As CheckState, ByVal NDCCode As String, ByVal DrugForm As String, ByVal PotencyUnit As String) As Long
        Try
            Dim cmd As New SqlCommand("gsp_InUpDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            Dim objsqlparam As SqlParameter

            objsqlparam = cmd.Parameters.Add("@DrugsID", SqlDbType.BigInt)
            objsqlparam.Direction = ParameterDirection.InputOutput
            objsqlparam.Value = ID

            sqlParam = cmd.Parameters.Add("@DrugName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Name

            sqlParam = cmd.Parameters.Add("@GenericName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GenName

            sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Dosage

            sqlParam = cmd.Parameters.Add("@Route", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Route

            sqlParam = cmd.Parameters.Add("@Frequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Frequency

            sqlParam = cmd.Parameters.Add("@Duration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Duration

            sqlParam = cmd.Parameters.Add("@IsClinicalDrug", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            If IsClinicDrug = CheckState.Unchecked Then
                sqlParam.Value = 0
            Else
                sqlParam.Value = 1
            End If

            ''''' By Mahesh - 20070124
            sqlParam = cmd.Parameters.Add("@IsAllergicDrug", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            If IsAllergicDrug = CheckState.Unchecked Then
                sqlParam.Value = 0
            Else
                sqlParam.Value = 1
            End If
            ''
            ''Sandip Darade 20090720
            ''parameter in sp has name @Amount
            '   sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
            sqlParam = cmd.Parameters.Add("@PotencyUnit", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PotencyUnit

            sqlParam = cmd.Parameters.Add("@Amount", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = stramount

            sqlParam = cmd.Parameters.Add("@nNarcotics", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input


            'Select Case strnarcotics
            '    Case "C1"
            '        sqlParam.Value = 0
            '    Case "C2"
            '        sqlParam.Value = 1
            '    Case "C3"
            '        sqlParam.Value = 2
            '    Case "C4"
            '        sqlParam.Value = 3
            '    Case "C5"
            '        sqlParam.Value = 4

            'End Select

            'Changes Done for case : GLO2010-0009973 i.e Narcotics flag in software doesn't equal narcotics flag set in database
            '- Start -
            Select Case strnarcotics.Trim()
                Case "Non-Scheduled"
                    sqlParam.Value = 0
                Case "Schedule II"
                    sqlParam.Value = 2
                Case "Schedule III"
                    sqlParam.Value = 3
                Case "Schedule IV"
                    sqlParam.Value = 4
                Case "Schedule V"
                    sqlParam.Value = 5
            End Select

            ' --End of change done for case :GLO2010-0009973 i.e Narcotics flag in software doesn't equal narcotics flag set in database

            ''''commented after we implement the new narcotic categories
            'If strnarcotics = "C2" Then
            '    sqlParam.Value = 1
            'Else
            '    sqlParam.Value = 0
            'End If

            '\\ Added by suraj on 20090128- for clinic ID
            sqlParam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetClinicID()

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            '\\ Added by suraj on 20090130 - For NDCCode
            sqlParam = cmd.Parameters.Add("@NDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = NDCCode


            '\\ For Drug Form--bug no 5494
            sqlParam = cmd.Parameters.Add("@DrugForm", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DrugForm

            '\\added by pradeep For beers List
            sqlParam = cmd.Parameters.Add("@nDrugType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 0


            Con.Open()
            cmd.ExecuteNonQuery()
            Dim myvalue As Object = objsqlparam.Value
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            objsqlparam = Nothing

            If ID <> 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Drug Modified", gstrLoginName, gstrClientMachineName)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, "Drug Modified", gloAuditTrail.ActivityOutCome.Success)
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Drug Added", gstrLoginName, gstrClientMachineName)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, "Drug Added", gloAuditTrail.ActivityOutCome.Success)
            End If

            Return myvalue
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- AddNewDrug -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- AddNewDrug -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    ''added by suraj 20090128 - for fetching top first clinic ID for drug updation
    Private Function GetClinicID() As Long
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Try
            conn.Open()
            _strSQL = "select top 1 nclinicid from Clinic_MST"

            cmd = New SqlCommand(_strSQL, conn)
            Dim myObject As Object = cmd.ExecuteScalar
            If Not IsDBNull(myObject) Then
                nClinicID = myObject
            Else
                nClinicID = 0
            End If
            'conn.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return CLng(nClinicID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    ''Not in Used 20100113
    Public Sub DeleteDrug(ByVal ID As Long, ByVal Name As String)
        Try
            Dim cmd As New SqlCommand("gsp_DeleteDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, Name & " Drug Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Drug Deleted", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, "Drug Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- DeleteDrug -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- DeleteDrug -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

    End Sub

    ''New delete drug against NDC from Drug_MST - 20100113
    Public Sub DeleteDrugAgainstNDC(ByVal sNDC As String, Optional ByVal sDrugtype As Integer = 0)
        Try
            Dim cmd As New SqlCommand("gsp_DeleteNDCDrugDrugs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@NDCCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sNDC

            sqlParam = cmd.Parameters.Add("@nDrugType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sDrugtype
            Con.Open()
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, "Drug Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- DeleteDrug -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- DeleteDrug -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try

    End Sub

    Public Function SelectDrug(ByVal ID As Long)
        Try
            '' to Get Drug's Information from database 
            Dim cmd As New SqlCommand("gsp_ScanDRUGS_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@DrugsID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dv = dt.Copy().DefaultView()
                dt.Dispose()
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing


            ''DrugName= dv.Item(0)(0)
            ''GenericName= dv.Item(0)(1)
            ''Dosage= dv.Item(0)(2)
            ''Route= dv.Item(0)(3)
            ''Frequency= dv.Item(0)(4)
            ''Duration= dv.Item(0)(5)
            ''Clinical/NonClinical Drugs= dv.Item(0)(6)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsDrugs -- SelectDrug -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsDrugs -- SelectDrug -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        If str <> dv.Table.Columns(7).ColumnName Then
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If
    End Sub
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        dv.Sort = "[" & strsort & "]" & strSortOrder
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return strsplittext
        End Try
    End Function

End Class
