Imports System.Data.SqlClient
Imports ADODB

'Imports AuditLog
Public Class clsFlowSheet
    Implements IDisposable

    ' Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    Private conString As String = Nothing
    Public blnIsSave As Boolean = False

    Private disposed As Boolean = False

    Public Sub New()
        Dim conString As String = Nothing
        Try
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Initialize, "clsFlowSheet -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- New -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Initialize, "clsFlowSheet -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- New -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    'Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
    '    'Dim dv As DataView
    '    'dv = grdFlowSheet.DataSource

    '    dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '%" & txtSearch & "%'"
    '    'grdFlowSheet.DataSource = dv
    'End Function

    Public Sub Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String)
        'Dim dv As DataView
        'dv = grdFlowSheet.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '%" & txtSearch & "%'"
        End If

        'grdFlowSheet.DataSource = dv
    End Sub

    Public Function CheckDuplicate(ByVal ID As Long, ByVal FlowSheetName As String) As Boolean
        Dim cmd As New SqlCommand("gsp_CheckFlowsheet_MST", Con)
        Try
            'objBusLayer.Open_Con()
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@FlowsheetName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FlowSheetName

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            sqlParam = Nothing

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- CheckDuplicate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- CheckDuplicate -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- CheckDuplicate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- CheckDuplicate -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then    'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function



    Public Function CheckIsUsed(ByVal FlowsheetName As String) As Boolean
        Dim cmd As New SqlCommand("gsp_CheckFlowsheet1", Con)
        Dim sqlParam As SqlParameter
        Try
            'objBusLayer.Open_Con()

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@nFlowsheetID", FlowsheetID)
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", FlowsheetName)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowsheetID

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- CheckIsUsed -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- CheckIsUsed -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- CheckIsUsed -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- CheckIsUsed -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            'If Not IsNothing(cmd) Then    'obj disposed by Mitesh
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
        End Try
    End Function


    Public Function GetAllFlowSheet(Optional ByVal patientID As Int64 = 0) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = Nothing
        Try
            'objBusLayer.Open_Con()

            If patientID = 0 Then
                cmd = New SqlCommand("gsp_ViewFlowSheet_MST", Con)
                cmd.CommandType = CommandType.StoredProcedure
            Else
                Dim query As String
                query = " SELECT DISTINCT nFlowSheetID AS nFlowSheetID, sFlowSheetName FROM FlowSheet_MST " _
                        & " UNION SELECT DISTINCT 0 AS nFlowSheetID, sFlowSheetName FROM FlowSheet1 " _
                        & " WHERE sFlowSheetName not in (SELECT DISTINCT  sFlowSheetName FROM FlowSheet_MST) " _
                        & " AND nPatientID = " & patientID & " ORDER BY sFlowSheetName"
                cmd = New SqlCommand(query, Con)
                cmd.CommandType = CommandType.Text
            End If
            'Dim sqlParam As SqlParameter

            'sqlParam = cmd.Parameters.Add("@ID", SqlDbType.Int)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID
            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
           
            dt = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = New DataView(dt)
            da.Dispose()
            da = Nothing
            Return dv
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "clsFlowSheet -- GetAllFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
            'UpdateLog("clsFlowSheet -- GetAllFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "clsFlowSheet -- GetAllFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- GetAllFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then    'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function

    Public Sub SelectFlowSheet(ByVal ID As Long)
        Dim cmd As New SqlCommand("gsp_ScanFlowSheet_MST", Con)
        Dim sqlParam As SqlParameter = Nothing
        Dim dt As DataTable = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@ID", ID)
            sqlParam = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
           
            dt = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = dt.DefaultView()
            da.Dispose()
            da = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Select, "clsFlowSheet -- SelectFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- SelectFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Select, "clsFlowSheet -- SelectFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- SelectFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If Not IsNothing(cmd) Then    'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub

    Public Sub SelectFlowSheet(ByVal flowSheetName As String, ByVal patientID As Int64)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim dt As DataTable = Nothing
        Try
            'Change Sp for FlowSheet Issuse.
            cmd = New SqlCommand("gsp_ScanFlowSheet_Structure", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = New SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", flowSheetName)
            sqlParam.SqlDbType = SqlDbType.VarChar
            sqlParam.Direction = ParameterDirection.Input
            sqlParam = Nothing

            sqlParam = New SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", patientID)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input
            sqlParam = Nothing

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)
            
            dt = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = dt.DefaultView()
            da.Dispose()
            da = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Select, "clsFlowSheet -- SelectFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- SelectFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub

    Public Function GetFlowSheetStruture(ByVal flowSheetName As String, ByVal patientID As Int64) As DataTable
        Dim dtTemp As DataTable = Nothing
        'Dim sqlCon_Local As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("sp_ScanFlowSheet_Structure", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = New SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", flowSheetName)
            sqlParam.SqlDbType = SqlDbType.VarChar
            sqlParam.Direction = ParameterDirection.Input
            sqlParam = Nothing

            sqlParam = New SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", patientID)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input
            sqlParam = Nothing

            Dim da As SqlDataAdapter = New SqlDataAdapter(cmd)

            dtTemp = New DataTable
            da.Fill(dtTemp)
            da.Dispose()
            da = Nothing

            'For Each dr As DataRow In dt.Rows.Count - 1
            '    dtTemp.Columns.Add(dr.Item("sColumnName"))
            'Next
            'dt = dtTemp.Copy()
            Return dtTemp

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Select, "clsFlowSheet -- SelectFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- SelectFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dtTemp) Then
            '    dtTemp.Dispose()
            '    dtTemp = Nothing
            'End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function

    Public Function GetFlowSheetName(ByVal flowSheetID As Int64) As String
        Dim MyCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim oResult As Object = Nothing
        Try
            MyCon = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("SELECT sFlowSheetName FROM FlowSheet_MST WHERE nFlowSheetID = " & flowSheetID & "", MyCon)

            MyCon.Open()
            oResult = cmd.ExecuteScalar

            MyCon.Close()
            If IsNothing(oResult) = False Then
                Return CType(oResult, String)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oResult) Then   'obj disposed by Mitesh
                oResult = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(MyCon) Then
                If MyCon.State = ConnectionState.Open Then
                    MyCon.Close()
                End If
                MyCon.Dispose()
                MyCon = Nothing
            End If
        End Try
    End Function

    Public Function IsFlowSheetUsed(ByVal flowSheetName As String) As Boolean
        Dim MyCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim oResult As Object = Nothing
        Try
            MyCon = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("SELECT COUNT(*) FROM FlowSheet1 WHERE sFlowSheetName = '" & flowSheetName.Replace("'", "''") & "'", MyCon)

            MyCon.Open()
            oResult = cmd.ExecuteScalar
            MyCon.Close()
            If IsNothing(oResult) = False Then
                If CType(oResult, Int32) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(oResult) Then
                oResult = Nothing
            End If
            If Not IsNothing(cmd) Then    'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(MyCon) Then
                If MyCon.State = ConnectionState.Open Then
                    MyCon.Close()
                End If
                MyCon.Dispose()
                MyCon = Nothing
            End If
        End Try
    End Function

    Public Function IsFlowSheetInMaster(ByVal flowSheetName As String) As Boolean
        Dim MyCon As New SqlConnection(GetConnectionString)
        Dim cmd As New SqlCommand("SELECT COUNT(*) FROM FlowSheet_MST WHERE sFlowSheetName = '" & flowSheetName.Replace("'", "''") & "'", MyCon)
        Dim oResult As Object = Nothing
        Try

            MyCon.Open()
            oResult = cmd.ExecuteScalar
            MyCon.Close()
            If IsNothing(oResult) = False Then
                If CType(oResult, Int32) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(oResult) Then
                oResult = Nothing
            End If
            If Not IsNothing(cmd) Then    'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(MyCon) Then
                MyCon.Dispose()
                MyCon = Nothing
            End If
        End Try
    End Function



    ''tblFlowSheet.Rows(i)(0), tblFlowSheet.Rows(i)(1), tblFlowSheet.Rows(i)(2), tblFlowSheet.Rows(i)(3),   tblFlowSheet.Rows(i)(4),  tblFlowSheet.Rows(i)(5),   tblFlowSheet.Rows(i)(6), tblFlowSheet.Rows(i)(7), tblFlowSheet.Rows(i)(8), tblFlowSheet.Rows(i)(9), tblFlowSheet.Rows(i)(10), tblFlowSheet.Rows(i)(11)
    ''ByVal colNo As Integer, ByVal ColName As String,   ByVal Format As String, ByVal colWidth As Integer, ByVal FontName As String, ByVal FontSize As Integer, ByVal Bold As String,    ByVal Italic As String,  ByVal Underline As String, ByVal Allinment As String, ByVal FontColor As String, ByVal BackColor As String
    Public Sub AddNewFlowSheet(ByVal ID As Long, ByVal FlowSheetName As String, ByVal NoofColumns As Integer, ByVal dtFlosheet As DataTable, Optional ByVal _arrLabs As ArrayList = Nothing, Optional ByVal _arrOrders As ArrayList = Nothing, Optional ByVal _arrDiagnosisOption As ArrayList = Nothing, Optional ByVal _arrManagement As ArrayList = Nothing)

        Dim cmd As SqlCommand = Nothing
        Dim trFlowsheet As SqlTransaction = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trFlowsheet = Con.BeginTransaction()

        Try

            If ID > 0 Then
                '' DELETE DATADICTIONARY '' SUDHIR 200905012 ''
                '' IF MODIFY , DELETE DATADICTIONARY ITEM FOR OLD FLOWSHEET NAME, ONLY IF OLD FLOWSHEET NOT IN TRANSACTION ''
                Dim strFlowSheetName As String = GetFlowSheetName(ID)
                If IsFlowSheetUsed(strFlowSheetName) = False Then
                    Dim oDictionary As New clsDataDictionary
                    oDictionary.DeleteDataDictionary("FlowSheet1.sFlowSheetName|" & strFlowSheetName)
                    oDictionary.DeleteDataDictionary("FlowSheet1.sFlowSheetName|" & strFlowSheetName & "|SingleRow")
                    oDictionary = Nothing
                End If
                '' END DELETE DATADICTIONARY
            End If


            cmd = New SqlCommand("gsp_DeleteFlowSheet_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet
            Dim sqlParam1 As SqlParameter

            'sqlParam1 = cmd.Parameters.Add("@ID", ID)
            sqlParam1 = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam1.Direction = ParameterDirection.Input
            'sqlParam1.Value = ID

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam1 = Nothing

            Dim i As Integer
            If (IsNothing(dtFlosheet) = False) Then


                For i = 0 To dtFlosheet.Rows.Count - 1

                    cmd = New SqlCommand("gsp_InUpFlowSheet_MST", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Transaction = trFlowsheet
                    Dim sqlParam As SqlParameter
                    'Dim Repeat_Count As Int16

                    'nFlowSheetID nFlowSheetName nCols nColNumber sColumnName  sFormat dWidth sFontName nFontSize sForeColor  bIsBold  bIsItalic  bIsUnderline sAlignment sBackColor           
                    sqlParam = cmd.Parameters.Add("@Count", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = i

                    'sqlParam = cmd.Parameters.Add("@FlowSheetID", ID)
                    sqlParam = cmd.Parameters.AddWithValue("@FlowSheetID", ID)
                    sqlParam.Direction = ParameterDirection.Input
                    'sqlParam.Value = ID

                    sqlParam = cmd.Parameters.Add("@FlowSheetName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = FlowSheetName

                    sqlParam = cmd.Parameters.Add("@Cols", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = NoofColumns

                    sqlParam = cmd.Parameters.Add("@ColNumber", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CInt(dtFlosheet.Rows(i)(0))


                    'tblFlowSheet.Rows(i)(1), tblFlowSheet.Rows(i)(2), tblFlowSheet.Rows(i)(3), tblFlowSheet.Rows(i)(4), tblFlowSheet.Rows(i)(5), tblFlowSheet.Rows(i)(6), tblFlowSheet.Rows(i)(7), tblFlowSheet.Rows(i)(8), tblFlowSheet.Rows(i)(9), tblFlowSheet.Rows(i)(10), tblFlowSheet.Rows(i)(11)
                    sqlParam = cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(1))

                    sqlParam = cmd.Parameters.Add("@Format", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(2))

                    sqlParam = cmd.Parameters.Add("@Width", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CInt(dtFlosheet.Rows(i)(3))

                    ''Sandip Darade 20090504
                    ''send  blank values for font related fields
                    sqlParam = cmd.Parameters.Add("@FontName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = "" 'CStr(dtFlosheet.Rows(i)(4))

                    sqlParam = cmd.Parameters.Add("@FontSize", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = 0 'CInt(dtFlosheet.Rows(i)(5))

                    sqlParam = cmd.Parameters.Add("@ForeColor", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(5))

                    sqlParam = cmd.Parameters.Add("@IsBold", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    'If IsDBNull(dtFlosheet.Rows(i)(6)) Then
                    '    sqlParam.Value = 0
                    'Else
                    '    If dtFlosheet.Rows(i)(6) = "" Then
                    '        sqlParam.Value = 0
                    '    Else
                    '        sqlParam.Value = 1
                    '    End If
                    'End If
                    sqlParam.Value = 0
                    sqlParam = cmd.Parameters.Add("@IsItalic", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    'If IsDBNull(dtFlosheet.Rows(i)(7)) Then
                    '    sqlParam.Value = 0
                    'Else
                    '    If dtFlosheet.Rows(i)(7) = "" Then
                    '        sqlParam.Value = 0
                    '    Else
                    '        sqlParam.Value = 1
                    '    End If
                    'End If
                    sqlParam.Value = 0
                    sqlParam = cmd.Parameters.Add("@IsUnderline", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    'If IsDBNull(dtFlosheet.Rows(i)(8)) Then
                    '    sqlParam.Value = 0
                    'Else
                    '    If dtFlosheet.Rows(i)(8) = "" Then
                    '        sqlParam.Value = 0
                    '    Else
                    '        sqlParam.Value = 1
                    '    End If
                    'End If
                    sqlParam.Value = 0
                    sqlParam = cmd.Parameters.Add("@Alignment", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    ' sqlParam.Value = CStr(dtFlosheet.Rows(i)(9))
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(4))

                    sqlParam = cmd.Parameters.Add("@BackColor", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    'sqlParam.Value = (dtFlosheet.Rows(i)(11))
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(6))

                    sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = GetPrefixTransactionID()

                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    cmd.ExecuteNonQuery()

                    If cmd IsNot Nothing Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    sqlParam = Nothing
                Next
            End If
            '' Sudhir - 20090225 ''

            '' Fill DataDictionary With Current FlowSheetName 
            ''Insert FormField for All Rows
            cmd = New SqlCommand("gsp_InUpDataDictionary", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet
            Dim sqlParam3 As SqlParameter

            sqlParam3 = cmd.Parameters.AddWithValue("@DictionaryID", 0)
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.BigInt

            sqlParam3 = cmd.Parameters.AddWithValue("@FieldName", "FlowSheet1.sFlowSheetName|" & FlowSheetName & "")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            sqlParam3 = cmd.Parameters.AddWithValue("@TableName", "FlowSheet1")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            sqlParam3 = cmd.Parameters.AddWithValue("@Caption", FlowSheetName)
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            '' Caption changed from FlowSheet to Flowsheet
            sqlParam3 = cmd.Parameters.AddWithValue("@sTableCaption", "Flowsheet")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            sqlParam3 = Nothing

            ''Insert FormField for Single Row
            cmd = New SqlCommand("gsp_InUpDataDictionary", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet
            sqlParam3 = New SqlParameter

            sqlParam3 = cmd.Parameters.AddWithValue("@DictionaryID", 0)
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.BigInt

            sqlParam3 = cmd.Parameters.AddWithValue("@FieldName", "FlowSheet1.sFlowSheetName|" & FlowSheetName & "|SingleRow")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            sqlParam3 = cmd.Parameters.AddWithValue("@TableName", "FlowSheet1")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            sqlParam3 = cmd.Parameters.AddWithValue("@Caption", FlowSheetName & " - SingleRow")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            sqlParam3 = cmd.Parameters.AddWithValue("@sTableCaption", "Flowsheet")
            sqlParam3.Direction = ParameterDirection.Input
            sqlParam3.SqlDbType = SqlDbType.VarChar

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            sqlParam3 = Nothing
            '' End Sudhir ''

            trFlowsheet.Commit()
            ''Added by Mayuri:20100615
            'Dim str As Int64
            FlowSheetName = FlowSheetName.Replace("'", "''")
            Dim strQRY As String = "SELECT DISTINCT nFlowSheetID FROM FlowSheet_MST WHERE sFlowSheetName= '" & FlowSheetName & "'  and nCols= " & NoofColumns & ""

            cmd = New SqlCommand(strQRY, Con)
            Dim FlowSheetID As Int64
            FlowSheetID = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            If Not IsNothing(_arrLabs) AndAlso Not IsNothing(_arrOrders) AndAlso Not IsNothing(_arrDiagnosisOption) AndAlso Not IsNothing(_arrManagement) Then
                'If _arrLabs.Count > 0 Or _arrOrders.Count > 0 Or _arrDiagnosisOption.Count > 0 Then
                Dim strDeleteQRY As String = "DELETE AssociatedEMField where nFieldID= " & FlowSheetID & " and nFieldType= '3'"

                cmd = New SqlCommand()

                cmd.Connection = Con
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strDeleteQRY
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                ''
                'End If
            End If
            ''End code Added by Mayuri:20100615
            ''Shubhangi 20090228''


            Dim objParameter As SqlParameter
            'If ID = 0 Then
            '    'Declare a Variable
            '    'Dim strQRY As String = "SELECT DISTINCT nFlowSheetID FROM FlowSheet_MST WHERE sFlowSheetName= '" & FlowSheetName & "'  and nCols= " & NoofColumns & ""
            '    'cmd = New SqlCommand(strQRY, Con)
            '    'Dim FlowSheetID As Int64
            '    'FlowSheetID = cmd.ExecuteScalar()
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "FlowsheetID is selected", gloAuditTrail.ActivityOutCome.Success)
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "FlowsheetID is selected", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")



            '    'Chech Whether FlowSheetID is > 0
            '    If FlowSheetID > 0 And sAssociatedEMField <> "" Then


            '        cmd = New SqlCommand("InupAssociatedEMField", Con)
            '        cmd.CommandType = CommandType.StoredProcedure

            '        'Declare a Parameter for Stored Procedure
            '        objParameter = New SqlParameter
            '        objParameter.Direction = ParameterDirection.Input
            '        objParameter.SqlDbType = SqlDbType.BigInt
            '        objParameter.Value = FlowSheetID
            '        objParameter.ParameterName = "@nFieldID"

            '        'Pass the Parameter for Store Procedure
            '        cmd.Parameters.Add(objParameter)

            '        'Declare a Parameter for Stored Procedure
            '        objParameter = New SqlParameter
            '        objParameter.Direction = ParameterDirection.Input
            '        objParameter.SqlDbType = SqlDbType.VarChar
            '        'objParameter.Value = sAssociatedEMField
            '        objParameter.Value = CType(_arrLabs.Item(i), myList).AssociatedProperty
            '        objParameter.ParameterName = "@sAssociatedEMName"

            '        'Pass the Parameter for Store Procedure
            '        cmd.Parameters.Add(objParameter)

            '        'Declare a Parameter for Stored Procedure
            '        objParameter = New SqlParameter
            '        objParameter.Direction = ParameterDirection.Input
            '        objParameter.SqlDbType = SqlDbType.Int
            '        objParameter.Value = 3 '''' For Lab = 1, For Radiology = 2
            '        objParameter.ParameterName = "@nFieldType"

            '        'Pass the Parameter for Store Procedure
            '        cmd.Parameters.Add(objParameter)


            '        objParameter = New SqlParameter
            '        objParameter.SqlDbType = SqlDbType.VarChar
            '        objParameter.Direction = ParameterDirection.Input
            '        objParameter.ParameterName = "@sAssociatedEMCategory"
            '        'objParameter.Value = sAssociatedCategory
            '        objParameter.Value = CType(_arrLabs.Item(i), myList).AssociatedCategory
            '        cmd.Parameters.Add(objParameter)



            '        objParameter = New SqlParameter
            '        objParameter.SqlDbType = SqlDbType.VarChar
            '        objParameter.Direction = ParameterDirection.Input
            '        objParameter.ParameterName = "@sStatus"
            '        objParameter.Value = "True"
            '        cmd.Parameters.Add(objParameter)


            '        cmd.ExecuteNonQuery()

            '        'Audit Trail
            '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "EM Field Associated with flow sheet", gloAuditTrail.ActivityOutCome.Success)
            '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "EM Field Associated with flow sheet", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")


            '    End If

            'Else

            'If sAssociatedEMField = "" Then
            '    cmd = New SqlCommand("delete from AssociatedEMField where nFieldID=" & ID, Con)
            '    cmd.ExecuteNonQuery()

            '    'Audit Trail
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "Delete EM Record", gloAuditTrail.ActivityOutCome.Success)

            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Delete EM Record", gstrLoginName, gstrClientMachineName, 0)


            'Else
            If Not IsNothing(_arrLabs) Then
                'Dim j As Int16
                For j As Integer = 0 To _arrLabs.Count - 1
                    cmd = New SqlCommand("FillEMFields", Con)
                    cmd.CommandType = CommandType.StoredProcedure

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.BigInt
                    objParameter.Value = FlowSheetID
                    objParameter.ParameterName = "@nFieldID"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Value = CType(_arrLabs.Item(j), gloGeneralItem.gloItem).Description
                    objParameter.ParameterName = "@sAssociatedEMName"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.Int
                    objParameter.Value = 3 '''' For Lab = 1, For Radiology = 2
                    objParameter.ParameterName = "@nFieldType"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sAssociatedEMCategory"
                    objParameter.Value = CType(_arrLabs.Item(j), gloGeneralItem.gloItem).Code
                    cmd.Parameters.Add(objParameter)



                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sStatus"
                    objParameter.Value = CType(_arrLabs.Item(j), gloGeneralItem.gloItem).Status
                    cmd.Parameters.Add(objParameter)

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing

                    objParameter = Nothing
                    'Audit Trail
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Associated EM field modified", gloAuditTrail.ActivityOutCome.Success)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Associated EM field modified", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Next
            End If

            If Not IsNothing(_arrOrders) Then
                'Dim j As Int16
                For j As Integer = 0 To _arrOrders.Count - 1
                    cmd = New SqlCommand("FillEMFields", Con)
                    cmd.CommandType = CommandType.StoredProcedure

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.BigInt
                    objParameter.Value = FlowSheetID
                    objParameter.ParameterName = "@nFieldID"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Value = CType(_arrOrders.Item(j), gloGeneralItem.gloItem).Description
                    objParameter.ParameterName = "@sAssociatedEMName"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.Int
                    objParameter.Value = 3 '''' For Lab = 1, For Radiology = 2
                    objParameter.ParameterName = "@nFieldType"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sAssociatedEMCategory"
                    objParameter.Value = CType(_arrOrders.Item(j), gloGeneralItem.gloItem).Code
                    cmd.Parameters.Add(objParameter)

                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sStatus"
                    objParameter.Value = CType(_arrOrders.Item(j), gloGeneralItem.gloItem).Status
                    cmd.Parameters.Add(objParameter)

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    objParameter = Nothing

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Associated EM field modified", gloAuditTrail.ActivityOutCome.Success)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Associated EM field modified", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Next
            End If
            If Not IsNothing(_arrDiagnosisOption) Then
                'Dim j As Int16
                For j As Integer = 0 To _arrDiagnosisOption.Count - 1
                    cmd = New SqlCommand("FillEMFields", Con)
                    cmd.CommandType = CommandType.StoredProcedure

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.BigInt
                    objParameter.Value = FlowSheetID
                    objParameter.ParameterName = "@nFieldID"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Value = CType(_arrDiagnosisOption.Item(j), gloGeneralItem.gloItem).Description
                    objParameter.ParameterName = "@sAssociatedEMName"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.Int
                    objParameter.Value = 3 '''' For Lab = 1, For Radiology = 2
                    objParameter.ParameterName = "@nFieldType"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sAssociatedEMCategory"
                    objParameter.Value = CType(_arrDiagnosisOption.Item(j), gloGeneralItem.gloItem).Code
                    cmd.Parameters.Add(objParameter)



                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sStatus"
                    objParameter.Value = CType(_arrDiagnosisOption.Item(j), gloGeneralItem.gloItem).Status
                    cmd.Parameters.Add(objParameter)

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    objParameter = Nothing
                    'Audit Trail
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Associated EM field modified", gloAuditTrail.ActivityOutCome.Success)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Associated EM field modified", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Next
            End If

            If Not IsNothing(_arrManagement) Then
                'Dim j As Int16
                For j As Integer = 0 To _arrManagement.Count - 1
                    cmd = New SqlCommand("FillEMFields", Con)
                    cmd.CommandType = CommandType.StoredProcedure

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.BigInt
                    objParameter.Value = FlowSheetID
                    objParameter.ParameterName = "@nFieldID"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Value = CType(_arrManagement.Item(j), gloGeneralItem.gloItem).Description
                    objParameter.ParameterName = "@sAssociatedEMName"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    'Declare a Parameter for Stored Procedure
                    objParameter = New SqlParameter
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.SqlDbType = SqlDbType.Int
                    objParameter.Value = 3 '''' For Lab = 1, For Radiology = 2
                    objParameter.ParameterName = "@nFieldType"

                    'Pass the Parameter for Store Procedure
                    cmd.Parameters.Add(objParameter)

                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sAssociatedEMCategory"
                    objParameter.Value = CType(_arrManagement.Item(j), gloGeneralItem.gloItem).Code
                    cmd.Parameters.Add(objParameter)



                    objParameter = New SqlParameter
                    objParameter.SqlDbType = SqlDbType.VarChar
                    objParameter.Direction = ParameterDirection.Input
                    objParameter.ParameterName = "@sStatus"
                    objParameter.Value = CType(_arrManagement.Item(j), gloGeneralItem.gloItem).Status
                    cmd.Parameters.Add(objParameter)

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    objParameter = Nothing
                    'Audit Trail
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Associated EM field modified", gloAuditTrail.ActivityOutCome.Success)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Associated EM field modified", gstrLoginName, gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                Next
            End If



            'End If
            'End If
            'End If
            '_gloEMRDatabase.Add("InupAssociatedEMField")

            ''End Shubhangi''

            'Return objBusLayer.PassCmdGetDV(cmd)
            '  objBusLayer.Close_Con()

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, FlowSheetName & "Patient Flow Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            If ID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Flow Sheet Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Flow Sheet Modified", gstrLoginName, gstrClientMachineName)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Flow Sheet Added", gstrLoginName, gstrClientMachineName)
            End If
            'objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet -- AddNewFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- AddNewFlowSheet -- " & ex.ToString)
            trFlowsheet.Rollback()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet -- AddNewFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- AddNewFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trFlowsheet.Rollback()
        Finally
            Con.Close()
            If Not IsNothing(trFlowsheet) Then    'obj disposed by Mitesh
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub
    ''Added by Mayuri:20100615
    Public Function GetEMAssociatedField(ByVal ID As Int64) As DataTable

        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try

            Dim strQRY As String = "SELECT sAssociatedEMName,sAssociatedEMCategory,sStatus FROM AssociatedEMField WHERE nFieldID = " & ID & " AND nFieldType = " & FieldType.FlowSheet.GetHashCode() & ""
            cmd = New SqlCommand(strQRY, Con)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)

            Dim _result As New DataTable
            da.Fill(_result)
            Return _result

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(da) Then    'obj disposed by Mitesh
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(_result) Then
            '    _result.Dispose()
            '    _result = Nothing
            'End If
        End Try
        'Return _result
    End Function
    ''End code Added by Mayuri:20100615

    ''Shubhangi 20090228''
    Public Function retriveAssociatedName(ByVal m_ID As Int64) As String
        Dim AssociatedEMName As String = ""
        Dim _result As Object = Nothing
        Try
            Dim cmd As New SqlCommand
            Con.Open()

            Dim strsql As String = ""
            strsql = "select sAssociatedEMName from AssociatedEMField where nFieldID=" & m_ID & "and nFieldType=3"
            cmd = New SqlCommand(strsql, Con)
            _result = cmd.ExecuteScalar()

            If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(Con) Then
                If (Con.State = ConnectionState.Open) Then  ''connection close done
                    Con.Close()
                End If
            End If
        End Try
        If _result = Nothing Then
            Return ""
        Else

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Query, "select sAssociatedEMName", gloAuditTrail.ActivityOutCome.Success)
            Return _result
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "select sAssociatedEMName", gstrClientMachineName, 0, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        End If

    End Function

    Public Sub deleteAssociatedName(ByVal m_ID As Int64)
        Try
            'Shubhangi 20091124
            'Check whether connection is open or not
            Dim cmd As New SqlCommand
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Dim strsql As String = ""
            strsql = "delete from AssociatedEMField where nFieldID=" & m_ID & " and nFieldType=3"
            cmd = New SqlCommand(strsql, Con)
            cmd.ExecuteNonQuery()

            'Audit Trail
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Query, "Associated EM Name deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Associated EM Name deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Sub

    ''End Shubhangi
    Public Sub DeleteFlowSheet(ByVal FlowSheetID As Long, ByVal FlowSheetName As String)
        Dim cmd As New SqlCommand("gsp_DeleteFlowSheet_MST", Con)
        Try

            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            'sqlParam = cmd.Parameters.Add("@ID", FlowSheetID)
            sqlParam = cmd.Parameters.AddWithValue("@ID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowSheetID
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            '' Sudhir 20090225 ''
            '' To Delete FormField from DataDictionary ''
            If CheckIsUsed(FlowSheetName) = False Then
                Dim Query As String = "DELETE FROM DataDictionary_MST WHERE (sCaption = '" & FlowSheetName & "' OR sCaption = '" & FlowSheetName & " - SingleRow') AND sTableName = 'FlowSheet1'"
                If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

                cmd = New SqlCommand(Query, Con)
                cmd.CommandType = CommandType.Text
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
            End If
            '' End Sudhir

            sqlParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "Flow Sheet Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Flow Sheet Deleted", gstrClientMachineName)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeleteFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- DeleteFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeleteFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- DeleteFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    '==========================================================================================================
    ''NOT IN USE ''name changed to SaveFlowSheet
    Public Function SaveGrid(ByVal nFlowSheetRecordID As Long, ByVal FlowSheetName As String, ByVal VisitID As Long, ByVal PatientID As Long, ByVal FlowSheetID As Long, ByVal strTempFileName1 As String) As Long

        Dim cmd As SqlCommand = Nothing
        Dim trFlowsheet As SqlTransaction = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trFlowsheet = Con.BeginTransaction()

        Try
            cmd = New SqlCommand("gsp_DeletePatientFlowSheet", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet
            Dim sqlParam1 As SqlParameter

            'sqlParam1 = cmd.Parameters.Add("@nPatientID", PatientID)
            sqlParam1 = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.SqlDbType = SqlDbType.BigInt

            'sqlParam1 = cmd.Parameters.Add("@nFlowSheetID", FlowSheetID)
            sqlParam1 = cmd.Parameters.AddWithValue("@nFlowSheetID", FlowSheetID)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.SqlDbType = SqlDbType.BigInt

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            sqlParam1 = Nothing

            cmd = New SqlCommand("gsp_InsertPatientFlowSheet", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet
            Dim sqlParam As SqlParameter

            'sqlParam = cmd.Parameters.Add("@PatientID", PatientID)
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            'sqlParam = cmd.Parameters.Add("@VisitID", VisitID)
            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            'sqlParam = cmd.Parameters.Add("@FlowSheetID", FlowSheetID)
            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            sqlParam = cmd.Parameters.Add("@Result", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFileName1)


            sqlParam.Value = mstream.Read()
            mstream.Close()

            'sqlParam = cmd.Parameters.Add("@MachineID", GetPrefixTransactionID)
            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            'sqlParam = cmd.Parameters.Add("@FlowSheetRecordID", 0)
            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetRecordID", 0)
            sqlParam.Direction = ParameterDirection.Output
            sqlParam.SqlDbType = SqlDbType.BigInt

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            nFlowSheetRecordID = sqlParam.Value
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            mstream = Nothing


            trFlowsheet.Commit()

            sqlParam = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, FlowSheetName & " Patient Flow Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)

            Return nFlowSheetRecordID

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet -- SaveGrid -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- SaveGrid -- " & ex.ToString)
            trFlowsheet.Rollback()
            'trFlowsheet = Nothing
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet -- SaveGrid -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- SaveGrid -- " & ex.ToString)
            trFlowsheet.Rollback()
            'trFlowsheet = Nothing
            Return 0
        Finally
            Con.Close()
            If Not IsNothing(trFlowsheet) Then    'obj disposed by Mitesh
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    'Function Not In Use 
    Public Sub SaveFlowSheetWithReplication_old(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal dtFlowSheet As DataTable)

        '' THIS METHOD IS USED FOR DATABASES WHICH ARE IN REPLICATION ''
        '' IF YOU MADE ANY CHANGES IN THIS METHODS, SAME CHANGES HAS TO BE DONE IN NON-REPLICATION METHOD ABOVE ''

        Dim oCmd As SqlCommand = Nothing
        Dim oOutPara As SqlParameter
        Dim oPara As SqlParameter
        Dim oAdp As SqlDataAdapter

        Dim trFlowsheet As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trFlowsheet = Con.BeginTransaction()
        blnIsSave = False
        Try
            Dim dvFlowSheet As DataView
            Dim _dtFlowSheet As DataTable = Nothing
            ' Dim _dtRowGUID As New DataTable
            Dim _FlowSheetNames As New ArrayList
            Dim _CurrentFlowSheetName As String
            Dim nFlowSheetRecordID As Int64


            Dim _Query As String
            Dim oResult As Object
            Dim _PresentRecordCount As Integer

            If (IsNothing(dtFlowSheet) = False) Then


                '' TAKE ALL FLOWSHEET NAMES TO BE SAVED ''
                For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1
                    If _FlowSheetNames.Contains(dtFlowSheet.Rows(iRow)("sFlowSheetName").ToString) = False Then
                        _FlowSheetNames.Add(dtFlowSheet.Rows(iRow)("sFlowSheetName").ToString)
                    End If
                Next
            End If
            '' SAVE EACH FLOWSHEET ONE BY ONE ''
            For iFSName As Integer = 0 To _FlowSheetNames.Count - 1

                '' FIND IF ANY ROWS ARE PRESENT AGAINST SAME FLOWSHEET ''
                _PresentRecordCount = 0
                _CurrentFlowSheetName = _FlowSheetNames(iFSName).ToString().Replace("'", "''")

                _Query = " SELECT COUNT(sFlowSheetName) FROM Flowsheet1 " _
                        & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"
                oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                oCmd.CommandType = CommandType.Text
                oResult = oCmd.ExecuteScalar()
                If (oResult IsNot Nothing) Then
                    If oResult.ToString() <> "" Then
                        _PresentRecordCount = Convert.ToInt32(oResult)
                    End If
                End If
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing

                '' TO GET FLOW SHEET RECORD ID ''
                '' IF ALREADY RECORDS ARE PRESENT THEN FETCH SAME RECORD ID OR CREATE NEW ID ''
                If _PresentRecordCount = 0 Then
                    oCmd = New SqlCommand("gsp_GetFlowSheetRecordID1", Con)
                    oCmd.CommandType = CommandType.StoredProcedure
                    oCmd.Transaction = trFlowsheet

                    oOutPara = New SqlParameter
                    oOutPara = oCmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
                    oOutPara.Direction = ParameterDirection.Input
                    oOutPara.SqlDbType = SqlDbType.BigInt

                    oOutPara = oCmd.Parameters.AddWithValue("@FlowSheetRecordID", 0)
                    oOutPara.Direction = ParameterDirection.Output
                    oOutPara.SqlDbType = SqlDbType.BigInt

                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    oCmd.ExecuteNonQuery()
                    nFlowSheetRecordID = Convert.ToInt64(oOutPara.Value)
                    oCmd.Parameters.Clear()
                    oCmd.Dispose()
                    oCmd = Nothing


                Else
                    _Query = " SELECT nFlowSheetRecordID FROM Flowsheet1 " _
                            & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"
                    oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                    oCmd.CommandType = CommandType.Text
                    oResult = oCmd.ExecuteScalar()
                    If (oResult IsNot Nothing) Then
                        If oResult.ToString() <> "" Then
                            nFlowSheetRecordID = Convert.ToInt64(oResult)
                        End If
                    End If
                    oCmd.Parameters.Clear()
                    oCmd.Dispose()
                    oCmd = Nothing

                End If
                If (IsNothing(dtFlowSheet) = False) Then
                    dvFlowSheet = dtFlowSheet.Copy().DefaultView
                

                    dvFlowSheet.RowFilter = "sFlowSheetName = '" & _CurrentFlowSheetName & "'"

                    _dtFlowSheet = dvFlowSheet.ToTable()
                    dvFlowSheet.Dispose()

                    '' NOW KEEP ROWS WHICH HAS TO BE UPDATED AND LET OTHERS DELETE FROM TABLE ''
                    If _PresentRecordCount > _dtFlowSheet.Rows.Count Then
                        '' IF OLD RECORDS ARE MORE THAN CURRENT SAVING RECORDS, THEN DELETE ANY OF EXTRA ROWS FOR CURRENT FLOWSHEET ''

                        _Query = " DELETE TOP(" + Convert.ToString(_PresentRecordCount - _dtFlowSheet.Rows.Count) + ") FROM Flowsheet1 " _
                                & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"
                        oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                        oCmd.CommandType = CommandType.Text
                        oResult = oCmd.ExecuteNonQuery()
                        oCmd.Parameters.Clear()
                        oCmd.Dispose()
                        oCmd = Nothing

                    End If
                End If

                _Query = " SELECT nFlowSheetRecordID, rowguid FROM Flowsheet1 " _
                         & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"
                oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                oCmd.CommandType = CommandType.Text
                oAdp = New SqlDataAdapter(oCmd)
                Dim _dtRowGUID As New DataTable
                oAdp.Fill(_dtRowGUID)
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing

                Dim _RowIndex As Integer = 1
                If (IsNothing(_dtFlowSheet) = False) Then


                    For iRow As Integer = 0 To _dtFlowSheet.Rows.Count - 1
                        ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                        Dim str As String = _dtFlowSheet.Rows(iRow)("sValue").ToString.Replace("'", "''")
                        If str.Length() > 1000 Then
                            trFlowsheet.Rollback()
                            MessageBox.Show("The field '" & _dtFlowSheet.Rows(iRow)("sFieldName").ToString & "' having more than 1000 character.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                        ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                        If (IsNothing(_dtRowGUID) = False) Then


                            If _dtRowGUID.Rows.Count > iRow Then
                                '' UPDATE ROW ''
                                _Query = " UPDATE FlowSheet1 SET nVisitID = " & nVisitID & ", nPatientID = " & nPatientID _
                                & ", sFlowSheetName = '" & _CurrentFlowSheetName _
                                & "', sFieldName = '" & _dtFlowSheet.Rows(iRow)("sFieldName").ToString.Replace("'", "''") _
                                & "', sValue = '" & str _
                                & "', sDataType = '" & _dtFlowSheet.Rows(iRow)("sDataType").ToString.Replace("'", "''") _
                                & "', nRowIndex = " & _RowIndex _
                                & ", sUserName = '" & gstrLoginName.Replace("'", "''") _
                                & "', sMachineName = '" & gstrClientMachineName.Replace("'", "''") _
                                & "', dWidth = " & Convert.ToDecimal(_dtFlowSheet.Rows(iRow)("dWidth")) _
                                & ", sFormat = '" & _dtFlowSheet.Rows(iRow)("sFormat").ToString.Replace("'", "''") _
                                & "', sAlignment = '" & _dtFlowSheet.Rows(iRow)("sAlignment").ToString.Replace("'", "''") _
                                & "', nTotalCols = " & Convert.ToInt32(_dtFlowSheet.Rows(iRow)("nTotalCols")) _
                                & ", nColNumber = " & Convert.ToInt32(_dtFlowSheet.Rows(iRow)("nColNumber")) _
                                & ", nForeColor = " & Convert.ToInt64(_dtFlowSheet.Rows(iRow)("nForeColor")) _
                                & ", nBackColor = " & Convert.ToInt64(_dtFlowSheet.Rows(iRow)("nBackColor")) _
                                & ", sServerDatabaseName = '" & gstrSQLServerName.Replace("'", "''") & "-" & gstrDatabaseName.Replace("'", "''") _
                                & "' WHERE rowguid = '" & _dtRowGUID.Rows(iRow)("rowguid").ToString & "'"

                            Else
                                '' INSERT ROW ''

                                _Query = " INSERT INTO FlowSheet1 (nFlowSheetRecordID, nVisitID, nPatientID, sFlowSheetName, " _
                                    & " sFieldName, sValue, sDataType, nRowIndex, dWidth, sFormat, sAlignment, nTotalCols, " _
                                    & " nColNumber, nForeColor, nBackColor, sUserName, sMachineName, sServerDatabaseName) " _
                                    & " VALUES  (" & nFlowSheetRecordID & ", " & nVisitID & ", " & nPatientID _
                                    & ", '" & _CurrentFlowSheetName _
                                    & "', '" & _dtFlowSheet.Rows(iRow)("sFieldName").ToString.Replace("'", "''") _
                                    & "', '" & str _
                                    & "', '" & _dtFlowSheet.Rows(iRow)("sDataType").ToString.Replace("'", "''") _
                                    & "', " & _RowIndex & ", " & Convert.ToDecimal(_dtFlowSheet.Rows(iRow)("dWidth")) _
                                    & ", '" & _dtFlowSheet.Rows(iRow)("sFormat").ToString.Replace("'", "''") _
                                    & "', '" & _dtFlowSheet.Rows(iRow)("sAlignment").ToString.Replace("'", "''") _
                                    & "', " & Convert.ToInt32(_dtFlowSheet.Rows(iRow)("nTotalCols")) _
                                    & ", " & Convert.ToInt32(_dtFlowSheet.Rows(iRow)("nColNumber")) _
                                    & ", " & Convert.ToInt64(_dtFlowSheet.Rows(iRow)("nForeColor")) _
                                    & ", " & Convert.ToInt64(_dtFlowSheet.Rows(iRow)("nBackColor")) _
                                    & ", '" & gstrLoginName.Replace("'", "''") & "', '" & gstrClientMachineName.Replace("'", "''") _
                                    & "', '" & gstrSQLServerName.Replace("'", "''") & "-" & gstrDatabaseName.Replace("'", "''") & "')"


                            End If
                        End If
                        str = Nothing
                        oCmd = New SqlCommand(_Query, Con)
                        oCmd.CommandType = CommandType.Text
                        oCmd.Transaction = trFlowsheet

                        If _dtFlowSheet.Rows(iRow)("nTotalCols") = _dtFlowSheet.Rows(iRow)("nColNumber") Then
                            _RowIndex = _RowIndex + 1
                        End If

                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If
                        oCmd.ExecuteNonQuery()
                        oCmd.Parameters.Clear()
                        oCmd.Dispose()
                        oCmd = Nothing

                    Next
                End If
                oAdp.Dispose()
                oAdp = Nothing
                _dtFlowSheet.Dispose()
                'dvFlowSheet.Dispose()
                _dtRowGUID.Dispose()
            Next

            trFlowsheet.Commit()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            _FlowSheetNames.Clear()

            blnIsSave = True
        Catch ex As Exception
            trFlowsheet.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet : Save Flow Sheet " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oCmd IsNot Nothing Then
                oCmd.Dispose()
                oCmd = Nothing
            End If
            If Not IsNothing(trFlowsheet) Then
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If
            oPara = Nothing
            oOutPara = Nothing

            Con.Close()
        End Try
    End Sub

    'FlowSheet Change 
    Public Sub SaveFlowSheet(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal dtFlowSheet As DataTable, ByVal dtTempFlowSheetRowId As DataTable)

        Dim oCmd As SqlCommand = Nothing
        Dim oOutPara As SqlParameter = Nothing
        Dim oPara As SqlParameter = Nothing
        Dim oAdp As SqlDataAdapter = Nothing
        ' Dim addDefaultDateTime As Date

        Dim trFlowsheet As SqlTransaction = Nothing

        Dim _dtFlowSheet As DataTable = Nothing
        ' Dim _dtRowGUID As DataTable = Nothing

        Dim dvFlowSheet As DataView = Nothing

        Dim _FlowSheetNames As ArrayList = Nothing
        Dim _CurrentFlowSheetName As String = Nothing
        Dim nFlowSheetRecordID As Int64 = 0

        Dim _Query As String = Nothing
        Dim oResult As Object = Nothing
        Dim _PresentRecordCount As Integer = 0
        Try

            _FlowSheetNames = New ArrayList

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            trFlowsheet = Con.BeginTransaction()

            blnIsSave = False

            '' TAKE ALL FLOWSHEET NAMES TO BE SAVED ''
            If (IsNothing(dtFlowSheet) = False) Then


                For iRow As Integer = 0 To dtFlowSheet.Rows.Count - 1
                    If _FlowSheetNames.Contains(dtFlowSheet.Rows(iRow)("sFlowSheetName").ToString) = False Then
                        _FlowSheetNames.Add(dtFlowSheet.Rows(iRow)("sFlowSheetName").ToString)
                    End If
                Next
            End If
            '' SAVE EACH FLOWSHEET ONE BY ONE ''
            For iFSName As Integer = 0 To _FlowSheetNames.Count - 1

                nFlowSheetRecordID = 0
                _CurrentFlowSheetName = ""

                '' FIND IF ANY ROWS ARE PRESENT AGAINST SAME FLOWSHEET ''
                _PresentRecordCount = 0

                _CurrentFlowSheetName = _FlowSheetNames(iFSName).ToString().Replace("'", "''")

                _Query = " SELECT COUNT(sFlowSheetName) FROM Flowsheet1 " _
                        & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"

                oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                oCmd.CommandType = CommandType.Text
                oResult = oCmd.ExecuteScalar()
                If (oResult IsNot Nothing) Then
                    If oResult.ToString() <> "" Then
                        _PresentRecordCount = Convert.ToInt32(oResult)
                    End If
                End If
                If Not IsNothing(oCmd) Then
                    oCmd.Parameters.Clear()
                    oCmd.Dispose()
                    oCmd = Nothing
                End If

                '' TO GET FLOW SHEET RECORD ID ''
                '' IF ALREADY RECORDS ARE PRESENT THEN FETCH SAME RECORD ID OR CREATE NEW ID ''
                If _PresentRecordCount = 0 Then
                    oCmd = New SqlCommand("gsp_GetFlowSheetRecordID1", Con)
                    oCmd.CommandType = CommandType.StoredProcedure
                    oCmd.Transaction = trFlowsheet

                    oOutPara = New SqlParameter
                    oOutPara = oCmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
                    oOutPara.Direction = ParameterDirection.Input
                    oOutPara.SqlDbType = SqlDbType.BigInt

                    oOutPara = oCmd.Parameters.AddWithValue("@FlowSheetRecordID", 0)
                    oOutPara.Direction = ParameterDirection.Output
                    oOutPara.SqlDbType = SqlDbType.BigInt

                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    oCmd.ExecuteNonQuery()

                    nFlowSheetRecordID = Convert.ToInt64(oOutPara.Value)

                    If Not IsNothing(oCmd) Then
                        oCmd.Parameters.Clear()
                        oCmd.Dispose()
                        oCmd = Nothing
                    End If

                Else
                    _Query = " SELECT nFlowSheetRecordID FROM Flowsheet1 " _
                            & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "'"
                    oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                    oCmd.CommandType = CommandType.Text
                    oResult = oCmd.ExecuteScalar()
                    If (oResult IsNot Nothing) Then
                        If oResult.ToString() <> "" Then
                            nFlowSheetRecordID = Convert.ToInt64(oResult)
                        End If
                    End If
                    If Not IsNothing(oCmd) Then
                        oCmd.Parameters.Clear()
                        oCmd.Dispose()
                        oCmd = Nothing
                    End If

                    If Not IsNothing(oResult) Then
                        oResult = Nothing
                    End If

                End If

                If (IsNothing(dtFlowSheet) = False) Then


                    dvFlowSheet = dtFlowSheet.Copy().DefaultView
                    dvFlowSheet.RowFilter = "sFlowSheetName = '" & _CurrentFlowSheetName & "'"
                    _dtFlowSheet = dvFlowSheet.ToTable()
                End If
                If (IsNothing(dtTempFlowSheetRowId) = False) Then



                    '' NOW KEEP ROWS WHICH HAS TO BE UPDATED AND LET OTHERS DELETE FROM TABLE ''
                    If (dtTempFlowSheetRowId.Rows.Count > 0) Then
                        'If _PresentRecordCount > _dtFlowSheet.Rows.Count Then
                        '' IF OLD RECORDS ARE MORE THAN CURRENT SAVING RECORDS, THEN DELETE ANY OF EXTRA ROWS FOR CURRENT FLOWSHEET ''

                        oCmd = New SqlCommand("DeleteFlowSheetRecordBasedOnFlowSheetId", Con)
                        oCmd.CommandType = CommandType.StoredProcedure
                        oCmd.Transaction = trFlowsheet

                        oOutPara = oCmd.Parameters.AddWithValue("@MstflowSheet", dtTempFlowSheetRowId)
                        oOutPara.SqlDbType = SqlDbType.Structured
                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If
                        oResult = oCmd.ExecuteNonQuery()

                        If Not IsNothing(oCmd) Then
                            oCmd.Parameters.Clear()
                            oCmd.Dispose()
                            oCmd = Nothing
                        End If

                        If Not IsNothing(oResult) Then
                            oResult = Nothing
                        End If
                    End If
                End If
                _Query = " SELECT nFlowSheetRecordID, nflowSheetId,nRowID FROM Flowsheet1 " _
                         & " WHERE nPatientID = " + nPatientID.ToString + " AND sFlowSheetName = '" + _CurrentFlowSheetName + "' order by nAddedOnDateTime ,sServerDatabaseName "

                oCmd = New SqlCommand(_Query, Con, trFlowsheet)
                oCmd.CommandType = CommandType.Text
                oAdp = New SqlDataAdapter(oCmd)

                'If Not IsNothing(_dtRowGUID) Then
                '    _dtRowGUID.Clear()
                'End If
                Dim _dtRowGUID As DataTable = New DataTable
                oAdp.Fill(_dtRowGUID)

                If Not IsNothing(oAdp) Then
                    oAdp.Dispose()
                    oAdp = Nothing
                End If
                If Not IsNothing(oCmd) Then
                    oCmd.Parameters.Clear()
                    oCmd.Dispose()
                    oCmd = Nothing
                End If


                Dim _RowIndex As Integer = 1

                Dim currDateTime As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond)
                If (IsNothing(_dtFlowSheet) = False) Then


                    For iRow As Integer = 0 To _dtFlowSheet.Rows.Count - 1
                        ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                        'Bug #88781: 00000986: Quotation mark getting double in flowsheets
                        Dim str As String = _dtFlowSheet.Rows(iRow)("sValue").ToString()
                        If str.Length() > 1000 Then
                            trFlowsheet.Rollback()
                            MessageBox.Show("The field '" & _dtFlowSheet.Rows(iRow)("sFieldName").ToString & "' having more than 1000 character.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                        ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                        oCmd = New SqlCommand("gsp_InUpFlowSheet", Con)
                        oCmd.CommandType = CommandType.StoredProcedure
                        oCmd.Transaction = trFlowsheet

                        oOutPara = oCmd.Parameters.AddWithValue("@nFlowSheetRecordID ", nFlowSheetRecordID)
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@nVisitID ", nVisitID)
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@nPatientID ", nPatientID)
                        oOutPara.SqlDbType = SqlDbType.BigInt



                        oOutPara = oCmd.Parameters.AddWithValue("@sFlowSheetName ", _CurrentFlowSheetName.ToString.Replace("''", "'"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        'Bug #88781: 00000986: Quotation mark getting double in flowsheets
                        oOutPara = oCmd.Parameters.AddWithValue("@sFieldName", _dtFlowSheet.Rows(iRow)("sFieldName").ToString())
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@sValue", str)
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@sDataType ", _dtFlowSheet.Rows(iRow)("sDataType").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@nRowIndex", _RowIndex)
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@sUserName ", gstrLoginName.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@sMachineName", gstrClientMachineName.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@dWidth", Convert.ToDecimal(_dtFlowSheet.Rows(iRow)("dWidth")))
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@sFormat", _dtFlowSheet.Rows(iRow)("sFormat").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@sAlignment", _dtFlowSheet.Rows(iRow)("sAlignment").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        oOutPara = oCmd.Parameters.AddWithValue("@nTotalCols", _dtFlowSheet.Rows(iRow)("nTotalCols").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.SmallInt

                        oOutPara = oCmd.Parameters.AddWithValue("@nColNumber", _dtFlowSheet.Rows(iRow)("nColNumber").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.SmallInt

                        oOutPara = oCmd.Parameters.AddWithValue("@nForeColor", _dtFlowSheet.Rows(iRow)("nForeColor").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@nBackColor", _dtFlowSheet.Rows(iRow)("nBackColor").ToString.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        oOutPara = oCmd.Parameters.AddWithValue("@sServerDatabaseName", gstrSQLServerName.Replace("'", "''") & "-" & gstrDatabaseName.Replace("'", "''"))
                        oOutPara.SqlDbType = SqlDbType.VarChar

                        'addDefaultDateTime = Format(System.DateTime.Now, "yyyy.MM.dd HH:mm:ss:ffff")
                        ''objParam.Value = Format(System.DateTime.Now, "yyyy.MM.dd HH:mm:ss:ffff")
                        ''Format(Now, "MM/dd/yyyy hh:mm:ss tt")

                        currDateTime = currDateTime.AddMilliseconds(10 + iRow)

                        oOutPara = oCmd.Parameters.AddWithValue("@nAddedOnDateTime", currDateTime)
                        oOutPara.SqlDbType = SqlDbType.DateTime

                        oOutPara = oCmd.Parameters.AddWithValue("@nRowID", Convert.ToInt64(_dtFlowSheet.Rows(iRow)("nRowId")))
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        Dim dr As DataRow() = _dtRowGUID.Select("nRowId='" & _dtFlowSheet.Rows(iRow)("nRowId") & "'")

                        If dr.Length = 0 Then
                            oOutPara = oCmd.Parameters.AddWithValue("@nFlowSheetID", 0)
                        Else
                            oOutPara = oCmd.Parameters.AddWithValue("@nFlowSheetID", _dtRowGUID.Rows(iRow)("nFlowSheetId"))
                        End If

                        'oOutPara = oCmd.Parameters.AddWithValue("@nFlowSheetID", 0)
                        oOutPara.SqlDbType = SqlDbType.BigInt

                        If _dtFlowSheet.Rows(iRow)("nTotalCols") = _dtFlowSheet.Rows(iRow)("nColNumber") Then
                            _RowIndex = _RowIndex + 1
                        End If

                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If
                        oCmd.ExecuteNonQuery()

                        If Not IsNothing(oCmd) Then
                            oCmd.Parameters.Clear()
                            oCmd.Dispose()
                            oCmd = Nothing
                        End If
                        str = Nothing
                    Next
                End If
                ''added for Resolving FlowSheet MixUp Issue.
                'If _dtRowGUID.Rows.Count > 0 Then
                '    _dtRowGUID.Clear()
                'End If
                'If _dtFlowSheet.Rows.Count > 0 Then
                '    _dtFlowSheet.Clear()
                'End If
                'End of Code Added for 
                _dtRowGUID.Dispose()
                _dtFlowSheet.Dispose()
                dvFlowSheet.Dispose()
            Next

            trFlowsheet.Commit()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

            blnIsSave = True
        Catch ex As Exception
            trFlowsheet.Rollback()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "clsFlowSheet : Save Flow Sheet " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oPara) Then
                oPara = Nothing
            End If
            If Not IsNothing(oOutPara) Then
                oOutPara = Nothing
            End If
            If Not IsNothing(oAdp) Then
                oAdp.Dispose()
                oAdp = Nothing
            End If
            If Not IsNothing(trFlowsheet) Then
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If

            If Not IsNothing(oCmd) Then
                oCmd.Dispose()
                oCmd = Nothing
            End If

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Sub
    'FlowSheet Change 

    'Function Not In Use 
    Public Function IsReplicationPresent() As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oResult As Object
        Dim _Result As Boolean = False
        Try

            Dim _Query As String
            _Query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE  TABLE_NAME='FlowSheet1' AND COLUMN_NAME='rowguid'"
            oDB.Connect(False)
            oResult = oDB.ExecuteScalar_Query(_Query)
            oDB.Disconnect()

            If oResult IsNot Nothing Then
                If oResult.ToString <> "" Then
                    If Convert.ToInt32(oResult) > 0 Then
                        _Result = True
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return _Result
    End Function
  

    Public Function GetFlowSheetHistory(ByVal Interval As String, ByVal PatientID As Long, ByVal Sysdate As Date) As DataTable
        Dim cmd As New SqlCommand("gsp_ViewFlowSheet", Con)
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@Interval", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Interval

            'sqlParam = cmd.Parameters.Add("@PatientID", PatientID)
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dtSysdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Sysdate

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetHistory -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- GetFlowSheetHistory -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetHistory -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- GetFlowSheetHistory -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then   'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function GetFlowSheetMigratedHistory(ByVal PatientID As Long) As DataTable
        Dim cmd As New SqlCommand("gsp_ViewFlowSheet_MIG", Con)
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@PatientID", PatientID)
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
        
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetMigratedHistory -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- GetFlowSheetMigratedHistory -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetMigratedHistory -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- GetFlowSheetMigratedHistory -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then   'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function


    Public Function ScanFlowSheet(ByVal sFlowSheetName As String, ByVal PatientID As Long) As DataTable
        Dim dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try

            Dim oDBPara As New gloDatabaseLayer.DBParameters

            'oDBPara.Add("@nPatientID", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sFlowSheetName", sFlowSheetName, ParameterDirection.Input, SqlDbType.VarChar)

            oDB.Connect(False)
            oDB.Retrive("gsp_ScanPatientFlowsheet", oDBPara, dt)
            oDB.Disconnect()
            oDBPara.Dispose()
            oDBPara = Nothing
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then    'obj disposed by Mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
            'If dt IsNot Nothing Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
        Return dt
    End Function

    
    Public Function SelectPatientFlowSheet(ByVal PatientID As Long, ByVal FlowSheetName As String) As DataTable
        Dim cmd As New SqlCommand("gsp_SelectFlowSheet1", Con)
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@nFlowSheetID", FlowSheetID)
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", FlowSheetName)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowSheetID

            'sqlParam = cmd.Parameters.Add("@nPatientID", PatientID)
            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
           
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- SelectPatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- SelectPatientFlowSheet -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- SelectPatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- SelectPatientFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then   'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Function DeletePatientFlowSheet(ByVal nFlowSheetRecordID As Long, ByVal sFlowSheetName As String, ByVal PatientID As Long) As Boolean
        Dim cmd As New SqlCommand("gsp_DeletePatientFlowSheet1", Con)
        Dim sqlParam As SqlParameter
        Try

            cmd.CommandType = CommandType.StoredProcedure            

            '@sFlowSheetName,@nPatientID
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", sFlowSheetName)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            'sqlParam = cmd.Parameters.AddWithValue("@nPatientID", gnPatientID)
            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            Con.Open()
            cmd.ExecuteNonQuery()

            '' Sudhir 20090408 ''
            '' To Delete FormField from DataDictionary ''
            If IsFlowSheetUsed(sFlowSheetName) = False AndAlso IsFlowSheetInMaster(sFlowSheetName) = False Then
                Dim Query As String = "DELETE FROM DataDictionary_MST WHERE (sCaption = '" & sFlowSheetName.Replace("'", "''") & "' OR sCaption = '" & sFlowSheetName.Replace("'", "''") & " - SingleRow') AND sTableName = 'FlowSheet1'"
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                cmd = New SqlCommand(Query, Con)
                cmd.CommandType = CommandType.Text
                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
            End If
            '' End Sudhir

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, sFlowSheetName & " Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, sFlowSheetName & " Deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, sFlowSheetName & " Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)

            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then   'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function
    '--Added by Rahul Patel for deleting selected flowsheet on 13-10-2010
    '--resolving issuse in case id GLO2010-0006166
    Public Function DeletePatientFlowSheetBasedOnLastRow(ByVal sFlowSheetName As String, ByVal patientId As Long) As Boolean
        Dim cmd As New SqlCommand("gsp_DeletePatientFlowSheet1", Con)
        Dim sqlParam As SqlParameter
        Try

            cmd.CommandType = CommandType.StoredProcedure


            '@sFlowSheetName,@nPatientID
            sqlParam = cmd.Parameters.AddWithValue("@sFlowSheetName", sFlowSheetName)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", patientId)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            Con.Open()
            cmd.ExecuteNonQuery()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, sFlowSheetName & " Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, sFlowSheetName & " Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)

            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Delete, "clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- DeletePatientFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then   'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
        End Try
    End Function
   
    Public Function Fill_LockFlowSheet(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable

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
            Dim dt As New DataTable
            sqladpt.Fill(dt)

            Con.Close()
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- Fill_LockFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- Fill_LockFlowSheet -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- Fill_LockFlowSheet -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- Fill_LockFlowSheet -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then    'obj disposed by Mitesh
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            objParam = Nothing

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    '''''''This Function is added by Anil on 01/11/2007
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        dv.Sort = "[" & strsort & "]"
    End Sub
    ''''''''

    '' PSA 0n  20061209
#Region " PSA FLOWSHEET"

    Public Function GetFlowSheetMST_ID(ByVal FlowsheetName As String) As Long
        Dim cmd As New SqlCommand("gsp_GetFlowSheetMST_ID", Con)
        Dim sqlParam As SqlParameter
        Try
            Dim FlowSheetID As Long

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@FlowsheetName", FlowsheetName)
            sqlParam = cmd.Parameters.AddWithValue("@FlowsheetName", FlowsheetName)
            sqlParam.SqlDbType = SqlDbType.VarChar
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID

            Con.Open()
            FlowSheetID = cmd.ExecuteScalar()
            'da = New SqlDataAdapter
            'da.SelectCommand = cmd
            'dt = New DataTable
            'da.Fill(dt)
            'dv = dt.DefaultView()
            Return FlowSheetID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetMST_ID -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- GetFlowSheetMST_ID -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetFlowSheetMST_ID -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- GetFlowSheetMST_ID -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
        End Try
    End Function

    'gsp_GetPSAValues

    Public Function GetPSAValues(ByVal PatientAge As Long) As DataTable
        Dim cmd As New SqlCommand("gsp_GetPSAValues", Con)
        Dim sqlParam As SqlParameter
        Dim dt As DataTable = Nothing
        Try
            'Dim FlowSheetID As Long

            cmd.CommandType = CommandType.StoredProcedure


            'sqlParam = cmd.Parameters.Add("@PatientAge", PatientAge)
            sqlParam = cmd.Parameters.AddWithValue("@PatientAge", PatientAge)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
        
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetPSAValues -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsFlowSheet -- GetPSAValues -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, "clsFlowSheet -- GetPSAValues -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsFlowSheet -- GetPSAValues -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If Not IsNothing(cmd) Then      'obj disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function
#End Region

    Public Function IsFlowSheetPresent(ByVal sFlowSheetName As String, ByVal PatientID As Long) As Boolean
        Dim cmd As SqlCommand
        Dim oResult As Object
        Dim _Query As String
        '_Query = " SELECT sFlowSheetName FROM FlowSheet_MST WHERE sFlowSheetName = '" & sFlowSheetName.Trim.Replace("'", "''") & "' UNION " _
        '        & " SELECT sFlowSheetName FROM FlowSheet1 WHERE sFlowSheetName = '" & sFlowSheetName.Trim.Replace("'", "''") & "' AND nPatientID = " & gnPatientID & ""
        _Query = " SELECT sFlowSheetName FROM FlowSheet_MST WHERE sFlowSheetName = '" & sFlowSheetName.Trim.Replace("'", "''") & "' UNION " _
                & " SELECT sFlowSheetName FROM FlowSheet1 WHERE sFlowSheetName = '" & sFlowSheetName.Trim.Replace("'", "''") & "' AND nPatientID = " & PatientID & ""
        cmd = New SqlCommand(_Query, Con)
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        oResult = cmd.ExecuteScalar
        Con.Close()
        If Not IsNothing(cmd) Then      'obj disposed by Mitesh
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        If oResult IsNot Nothing Then
            If oResult.ToString = "" Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    'Changes for FlowSheet Issuse Fix. -Start

    'Public Function ScanFlowSheetReader(ByVal sFlowSheetName As String, ByVal PatientID As Long) As SqlDataReader
    '    Dim _result As SqlDataReader = Nothing
    '    Dim oDB As gloDatabaseLayer.DBLayer = Nothing
    '    Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

    '    Try
    '        oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
    '        oDBPara = New gloDatabaseLayer.DBParameters

    '        'oDBPara.Add("@nPatientID", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)
    '        oDBPara.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
    '        oDBPara.Add("@sFlowSheetName", sFlowSheetName, ParameterDirection.Input, SqlDbType.VarChar)

    '        oDB.Connect(False)
    '        oDB.Retrive("gsp_ScanPatientFlowsheet", oDBPara, _result)

    '        'oDB.Retrive("sp_ScanPatientFlowsheet", oDBPara, dt)
    '        'oDB.Disconnect()
    '        'oDB.Dispose()
    '        'oDB = Nothing

    '        Return _result

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        If Not IsNothing(oDBPara) Then
    '            oDBPara.Dispose()
    '            oDBPara = Nothing
    '        End If
    '        'If Not IsNothing(oDB) Then
    '        '    oDB.Dispose()
    '        '    oDB = Nothing
    '        'End If
    '    End Try
    '    ''Return dt
    'End Function
    Public Shared Function Pivot(ByVal dataValues As IDataReader, ByVal keyColumn As String, ByVal pivotNameColumn As String, ByVal pivotValueColumn As String) As DataTable
        Dim tmp As DataTable = Nothing
        Dim r As DataRow = Nothing
        Dim LastKey As String = "//dummy//"
        Dim i As Integer, pValIndex As Integer, pNameIndex As Integer
        Dim s As String = String.Empty
        Dim FirstRow As Boolean = True
        Dim dtPivotFlowSheetTable As DataTable = Nothing
        Try
            tmp = New DataTable()

            ' Add non-pivot columns to the data table:

            pValIndex = dataValues.GetOrdinal(pivotValueColumn)
            pNameIndex = dataValues.GetOrdinal(pivotNameColumn)

            For i = 0 To dataValues.FieldCount - 1
                If i <> pValIndex AndAlso i <> pNameIndex Then
                    tmp.Columns.Add(dataValues.GetName(i), dataValues.GetFieldType(i))
                End If
            Next

            r = tmp.NewRow()

            ' now, fill up the table with the data:
            While dataValues.Read()
                ' see if we need to start a new row
                If dataValues(keyColumn).ToString() <> LastKey Then
                    ' if this isn't the very first row, we need to add the last one to the table
                    If Not FirstRow Then
                        tmp.Rows.Add(r)
                    End If
                    r = tmp.NewRow()
                    FirstRow = False
                    ' Add all non-pivot column values to the new row:
                    For i = 0 To dataValues.FieldCount - 3
                        r(i) = dataValues(tmp.Columns(i).ColumnName)
                    Next
                    LastKey = dataValues(keyColumn).ToString()
                End If
                ' assign the pivot values to the proper column; add new columns if needed:
                s = dataValues(pNameIndex).ToString()
                If Not tmp.Columns.Contains(s) Then
                    tmp.Columns.Add(s, dataValues.GetFieldType(pValIndex))
                End If
                r(s) = dataValues(pValIndex)
            End While

            ' add that final row to the datatable:
            tmp.Rows.Add(r)

            ' Close the DataReader
            dataValues.Close()
            ' and that's it!

            dtPivotFlowSheetTable = tmp.Copy()

            Return dtPivotFlowSheetTable
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(r) Then
                r = Nothing
            End If
            If Not IsNothing(tmp) Then
                tmp.Dispose()
                tmp = Nothing
            End If
        End Try
    End Function

 
    Public Function ScanFlowSheetReaderAsTable(ByVal sFlowSheetName As String, ByVal PatientID As Long) As DataTable

        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Dim oDBPara As gloDatabaseLayer.DBParameters = Nothing

        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString)
            oDBPara = New gloDatabaseLayer.DBParameters

            'oDBPara.Add("@nPatientID", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBPara.Add("@sFlowSheetName", sFlowSheetName, ParameterDirection.Input, SqlDbType.VarChar)

            oDB.Connect(False)

            Dim _result As SqlDataReader = Nothing
            oDB.Retrive("gsp_ScanPatientFlowsheet", oDBPara, _result)
            Dim myData As DataTable = Pivot(_result, "nRowID", "sFieldName", "sValue")
            _result.Close()
            _result = Nothing
            'oDB.Retrive("sp_ScanPatientFlowsheet", oDBPara, dt)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            Return myData

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        ''Return dt
    End Function

    'Generate Row Id for Each newly entered row.
    Public Function GenerateRowID() As Long
        Dim cmd As SqlCommand = Nothing
        Dim nVisitID As Long = 0
        Dim objParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_GetNewRowID", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.ConnectionString = GetConnectionString()

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output

            cmd.ExecuteNonQuery()
            nVisitID = objParam.Value

            Return nVisitID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Con.State = ConnectionState.Closed Then
                Con.Close()
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function

    'End of changes for flowSheet Issuse Fix.

    ' Implement IDisposable.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                'If Not IsNothing(dt) Then
                '    dt.Dispose()
                '    dt = Nothing
                'End If
                'If Not IsNothing(da) Then
                '    da.Dispose()
                '    da = Nothing
                'End If
                'If Not IsNothing(ds) Then
                '    ds.Dispose()
                '    ds = Nothing
                'End If
                If Not IsNothing(dv) Then
                    dv.Dispose()
                    dv = Nothing
                End If
                If Not IsNothing(Con) Then
                    Con.Dispose()
                    Con = Nothing
                End If
                disposed = True
            End If
           

        End If
    End Sub

    Protected Overrides Sub Finalize()

        Dispose(False)
    End Sub
End Class


