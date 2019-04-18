''''''''''''''

Imports System.Data.SqlClient
Imports ADODB

Public Class cls_LM_LabResult
    ' Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub
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
    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get

    '        Return ds

    '    End Get
    'End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv
            'Return Ds
        End Get
    End Property

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView

        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '%" & txtSearch & "%'"
        End If

        Return Nothing
    End Function

    Public Function CheckDuplicate(ByVal LabResult_MST_ID As Long, ByVal FlowSheetName As String) As Boolean
        ''''' Created By Mahesh - 20070122
        ''''' Objective : To Check the Flow Sheet with the Same Name 
        ''''' I/P : LabResult_MST_ID  (It will be 0 for New FlowSheet) , FlowSheetName 
        ''''' O/P : if No of Count of the same FlowSheetName  is More than ZERO then return TRUE else return FALSE

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try

            cmd = New SqlCommand("gLM_CheckLabResult_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@ID", LabResult_MST_ID)
            sqlParam.Direction = ParameterDirection.Input


            sqlParam = cmd.Parameters.Add("@FlowsheetName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FlowSheetName

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If rowAffected > 0 Then
                '' if Flowsheet Name exits
                Return True
            Else
                '' if Flowsheet Name does not exits
                Return False
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If

            Con.Close()
        End Try

    End Function

    Public Function CheckIsUsed(ByVal FlowsheetID As Long) As Boolean
        ''''' Created By Mahesh - 20070122
        ''''' Objective : To check selected LabResult FlowSheet is in Use 
        ''''' I/P : LabResult_MST_ID 
        ''''' O/P : if No of Count is More than ZERO i.e FlowSheet is in use then return TRUE else return FALSE

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try

            cmd = New SqlCommand("gLM_CheckLabResult", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@nFlowsheetID", FlowsheetID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowsheetID

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)
            If rowAffected > 0 Then
                '' if Flowsheet is Used for Keeping the Lab Result 
                ''''' There is An ntry in Table Against this  FlowSheetID
                Return True
            Else
                '' if Flowsheet NOT in Use
                Return False
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If

            Con.Close()
        End Try
    End Function

    Public Function GetAllFlowSheet() As DataView
        ''''' Created By Mahesh - 20070122
        ''''' Objective: To Get All FlowSheet ID & Names From LM_LabResult_MST Table 
        ''''' I/P : None
        ''''' O/P : Dataview which contains all Lab Result FlowSheets with there IDss

        Dim cmd As SqlCommand = Nothing

        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gLM_ViewLabResult_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            dv = New DataView(dt.Copy())
            dt.Dispose()
            da.Dispose()
            Con.Close()
            ''''' Dataview which contains all Lab Result FlowSheets with there IDs
            Return dv
            '''''nFlowSheetID,sFlowSheetName
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If IsNothing(cmd) = False Then
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Public Function SelectFlowSheet(ByVal ID As Long)
        ''''' Created By Mahesh - 20070122
        ''''' Objective : To Get All Fields of Provided FlowSheetID in LM_LabResult_MST Table 
        ''''' I/P : LabResult_MST_ID 
        ''''' O/P : DataView with the following Fields 
        '''''' sFlowSheetName, nCols, nColNumber, sColumnName, sFormat, dWidth, sFontName, nFontSize, nForeColor, bIsBold, bIsItalic, bIsUnderline, sAlignment, nBackColor
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            cmd = New SqlCommand("gLM_ScanLM_LabResult_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input


            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd

            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            '' DataView with the following Fields 
            '' sFlowSheetName, nCols, nColNumber, sColumnName, sFormat, dWidth, sFontName, nFontSize, nForeColor, bIsBold, bIsItalic, bIsUnderline, sAlignment, nBackColor
            dv = dt.Copy().DefaultView()
            dt.Dispose()
            da.Dispose()
            'Con.Close()

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- SelectFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
        Return Nothing
    End Function

    ''tblFlowSheet.Rows(i)(0), tblFlowSheet.Rows(i)(1), tblFlowSheet.Rows(i)(2), tblFlowSheet.Rows(i)(3),   tblFlowSheet.Rows(i)(4),  tblFlowSheet.Rows(i)(5),   tblFlowSheet.Rows(i)(6), tblFlowSheet.Rows(i)(7), tblFlowSheet.Rows(i)(8), tblFlowSheet.Rows(i)(9), tblFlowSheet.Rows(i)(10), tblFlowSheet.Rows(i)(11)
    ''ByVal colNo As Integer, ByVal ColName As String,   ByVal Format As String, ByVal colWidth As Integer, ByVal FontName As String, ByVal FontSize As Integer, ByVal Bold As String,    ByVal Italic As String,  ByVal Underline As String, ByVal Allinment As String, ByVal FontColor As String, ByVal BackColor As String
    Public Function AddNewFlowSheet(ByVal ID As Long, ByVal FlowSheetName As String, ByVal NoofColumns As Integer, ByVal dtFlosheet As DataTable)

        Dim cmd As SqlCommand = Nothing
        Dim trFlowsheet As SqlTransaction = Nothing
        Dim sqlParam1 As SqlParameter = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trFlowsheet = Con.BeginTransaction()

        Try
            ''  To Delete Records of Provided FlowSheetID from LM_LabResult_MST Table 
            cmd = New SqlCommand("gLM_DeleteLabResult_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet


            sqlParam1 = cmd.Parameters.AddWithValue("@ID", ID) ''FlowSheetID
            sqlParam1.Direction = ParameterDirection.Input

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()            
            cmd.Dispose()
            cmd = Nothing

            Dim i As Integer
            If (IsNothing(dtFlosheet) = False) Then


                For i = 0 To dtFlosheet.Rows.Count - 1

                    cmd = New SqlCommand("gLM_InUpLabResult_MST", Con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Transaction = trFlowsheet
                    Dim sqlParam As SqlParameter
                    'Dim Repeat_Count As Int16

                    'nFlowSheetID nFlowSheetName nCols nColNumber sColumnName  sFormat dWidth sFontName nFontSize sForeColor  bIsBold  bIsItalic  bIsUnderline sAlignment sBackColor           
                    sqlParam = cmd.Parameters.Add("@Count", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = i

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
                    sqlParam.Value = CInt(dtFlosheet.Rows(i)(0))  '''' Column Number


                    'tblFlowSheet.Rows(i)(1), tblFlowSheet.Rows(i)(2), tblFlowSheet.Rows(i)(3), tblFlowSheet.Rows(i)(4), tblFlowSheet.Rows(i)(5), tblFlowSheet.Rows(i)(6), tblFlowSheet.Rows(i)(7), tblFlowSheet.Rows(i)(8), tblFlowSheet.Rows(i)(9), tblFlowSheet.Rows(i)(10), tblFlowSheet.Rows(i)(11)
                    sqlParam = cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(1))  '' Name Of the Column

                    sqlParam = cmd.Parameters.Add("@Format", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(2)) '' Data Format

                    sqlParam = cmd.Parameters.Add("@Width", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CInt(dtFlosheet.Rows(i)(3)) '' Column Width

                    sqlParam = cmd.Parameters.Add("@FontName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(4)) '' Font for the Column

                    sqlParam = cmd.Parameters.Add("@FontSize", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CInt(dtFlosheet.Rows(i)(5))  '' Font Size

                    sqlParam = cmd.Parameters.Add("@ForeColor", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = (dtFlosheet.Rows(i)(10)) '' Font Column

                    sqlParam = cmd.Parameters.Add("@IsBold", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    If IsDBNull(dtFlosheet.Rows(i)(6)) Then    '''' Bold
                        sqlParam.Value = 0
                    Else
                        If dtFlosheet.Rows(i)(6) = "" Then
                            sqlParam.Value = 0
                        Else
                            sqlParam.Value = 1
                        End If
                    End If

                    sqlParam = cmd.Parameters.Add("@IsItalic", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    If IsDBNull(dtFlosheet.Rows(i)(7)) Then  '' Italic
                        sqlParam.Value = 0
                    Else
                        If dtFlosheet.Rows(i)(7) = "" Then
                            sqlParam.Value = 0
                        Else
                            sqlParam.Value = 1
                        End If
                    End If

                    sqlParam = cmd.Parameters.Add("@IsUnderline", SqlDbType.Int)
                    sqlParam.Direction = ParameterDirection.Input
                    If IsDBNull(dtFlosheet.Rows(i)(8)) Then '' Under Line
                        sqlParam.Value = 0
                    Else
                        If dtFlosheet.Rows(i)(8) = "" Then
                            sqlParam.Value = 0
                        Else
                            sqlParam.Value = 1
                        End If
                    End If

                    sqlParam = cmd.Parameters.Add("@Alignment", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = CStr(dtFlosheet.Rows(i)(9)) '' Underline

                    sqlParam = cmd.Parameters.Add("@BackColor", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = (dtFlosheet.Rows(i)(11))   '' Back Color

                    sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = GetPrefixTransactionID()  '' PrefixID to Generate Uniqe FlowSheet ID

                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing

                    If sqlParam IsNot Nothing Then
                        sqlParam = Nothing
                    End If
                Next
            End If
            trFlowsheet.Commit()
            'Return objBusLayer.PassCmdGetDV(cmd)
            '  objBusLayer.Close_Con()

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, FlowSheetName & "Patient Flow Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            If ID <> 0 Then
                'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Lab Result Modified", gstrLoginName, gstrClientMachineName)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, FlowSheetName & "Patient Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)

            Else
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, FlowSheetName & "Lab Result Added", gloAuditTrail.ActivityOutCome.Success)
                'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Lab Result Added", gstrLoginName, gstrClientMachineName)
            End If
            'objAudit = Nothing
        Catch ex As SqlException
            If (IsNothing(trFlowsheet) = False) Then
                trFlowsheet.Rollback()
            End If

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("cls_LM_LabResult -- AddNewFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trFlowsheet.Rollback()
        Finally
            Con.Close()
            If (IsNothing(trFlowsheet) = False) Then
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam1 IsNot Nothing Then
                sqlParam1 = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Function DeleteFlowSheet(ByVal FlowSheetID As Long, ByVal FlowSheetName As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gLM_DeleteLabResult_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@ID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowSheetID

            Con.Open()
            cmd.ExecuteNonQuery()


            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "LabResult FlowSheet Deleted", gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Lab Result Flow Sheet Deleted", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- DeleteFlowSheet -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
        Return Nothing
    End Function

    ''''==========================================================================================================
    Public Function SaveLabsResult(ByVal nFlowSheetRecordID As Long, ByVal FlowSheetName As String, ByVal VisitID As Long, ByVal VisitDate As DateTime, ByVal PatientID As Long, ByVal FlowSheetID As Long, ByVal strTempFileName1 As String) As Long

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim trFlowsheet As SqlTransaction = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trFlowsheet = Con.BeginTransaction()
        Try



            cmd = New SqlCommand("gLM_InsertPatientLabResult", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trFlowsheet


            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = PatientID

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitDate

            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowSheetID

            sqlParam = cmd.Parameters.Add("@Result", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFileName1)


            sqlParam.Value = mstream.Read()
            mstream.Close()

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID(PatientID))
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetRecordID", nFlowSheetRecordID) ''
            sqlParam.Direction = ParameterDirection.InputOutput

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            If (IsNothing(sqlParam) = False) Then
                nFlowSheetRecordID = sqlParam.Value
            End If


            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            mstream = Nothing

            trFlowsheet.Commit()


            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Flow Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Patient Flow Sheet Added", gloAuditTrail.ActivityOutCome.Success)

            Return nFlowSheetRecordID

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- SaveLabsResult -- " & ex.ToString)
            trFlowsheet.Rollback()
            trFlowsheet = Nothing
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trFlowsheet.Rollback()
            trFlowsheet = Nothing
            Return 0
        Finally
            Con.Close()

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If

            If (IsNothing(trFlowsheet) = False) Then
                trFlowsheet.Dispose()
                trFlowsheet = Nothing
            End If

        End Try
    End Function


    Public Function GetLabResults(ByVal PatientID As Long, ByVal FlowsheetID As Long, ByVal VisitDate As Date) As DataTable
        '--- Objective: To Get Lab Results of Selected Patient & FlowSheet
        '--- I/P: FlowSheetID, PatientID, Date
        '--- O/P: nFlowSheetRecordID, nVisitID, dtVisitDate, Flag  
        '--- By Mahesh - Created On = 20070122
        '   PROCEDURE  gLM_ScanLabResult 	 	
        '	    @PatientID	    Numeric(18,0), 	
        '	    @FlowSheetID 	Numeric(18,0),
        '	    @dtSysDate	    DateTime

        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter = Nothing
        Try

            cmd = New SqlCommand("gLM_ScanLabResult", Con)
            cmd.CommandType = CommandType.StoredProcedure


            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmd.Parameters.Add("@FlowSheetID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = FlowsheetID

            objParam = cmd.Parameters.Add("@dtSysDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Format(VisitDate, "MM/dd/yyyy hh:mm tt")

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetLabResults -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetLabResults -- " & ex.ToString)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If objParam IsNot Nothing Then
                objParam = Nothing
            End If
        End Try

    End Function


    Public Function GetFlowSheetHistory(ByVal Interval As String, ByVal PatientID As Long, ByVal Sysdate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewFlowSheet", Con)
            cmd.CommandType = CommandType.StoredProcedure            

            sqlParam = cmd.Parameters.Add("@Interval", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Interval

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dtSysdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Sysdate

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            'Con.Close()
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetFlowSheetHistory -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetFlowSheetHistory -- " & ex.ToString)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function GetFlowSheetMigratedHistory(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewFlowSheet_MIG", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()

            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetFlowSheetMigratedHistory -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    '' to select Patient Flow sheet
    Public Function SelectLabResult(ByVal PatientID As Long, ByVal nFlowSheetRecordID As Long) As DataTable
        '--- Objective: To Get Lab Result of Selected Patient & FlowSheetRecordID 
        '--- I/P: FlowSheetID , PatientID
        '--- O/P: nFlowSheetID,sResult
        '--- By Mahesh - Created On = 20070122
        '   CREATE PROCEDURE gLM_ScanFlowSheet
        '	@PatientID Numeric(18,0),
        '	@FlowSheetRecordID Numeric(18,0)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gLM_SelectLabResult", Con)
            cmd.CommandType = CommandType.StoredProcedure            

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = gnPatientID

            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetRecordID", nFlowSheetRecordID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = nFlowSheetRecordID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            'ds = New DataSet
            da.Fill(dt)
            da.Dispose()
            ' ds.WriteXml(Application.StartupPath & "\FlowSheet.XML")
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("cls_LM_LabResult -- SelectLabResult -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function

    '' 
    Public Function SelectPatientFlowSheet(ByVal PatientID As Long, ByVal FlowSheetID As Long) As DataTable

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_SelectFlowSheet", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.AddWithValue("@nFlowSheetID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = FlowSheetID

            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- SelectPatientFlowSheet -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then                
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function DeletePatientFlowSheet(ByVal nFlowSheetRecordID As Long, ByVal FlowSheetName As String, ByVal VisitDate As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gLM_DeleteLabResult", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@nFlowSheetRecordID", nFlowSheetRecordID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = nFlowSheetRecordID

            Con.Open()
            cmd.ExecuteNonQuery()

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "Lab Result- of Date " & VisitDate & " Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Lab Result- of Date " & VisitDate & " Deleted", gloAuditTrail.ActivityOutCome.Success)
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            'UpdateLog("cls_LM_LabResult -- DeletePatientFlowSheet -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function


    ''===================================
    '' For Show Lab Results From Tasks
    '' to fill LabResults from Orders- LabResult Master for Modify 
    Public Function GetOrdersLabsResult(ByVal PatientID As Long, ByVal OrderDate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gLM_GetOrdersLabResults", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            'sqlParam = cmd.Parameters.Add("@VisitID", VisitID)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = OrderDate

            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            Return (dt)
            '' nFlowSheetID, sFlowSheetName, lm_test_Name, lm_OrderDate,sPatientCode
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("cls_LM_LabResult -- GetOrdersLabsResult -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If

            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function


    ''===================================
    '' For Show Lab Results From Tasks
    '' to Get nFlowSheetRecordID,sResult for given Patient & Visit Date-Time
    Public Function GetLM_LabResultRecID(ByVal PatientID As Long, ByVal FlowSheetID As Long, ByVal OrderDate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gLM_GetLM_LabResultRecID", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@FlowSheetID", FlowSheetID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FlowSheetID

            sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = OrderDate

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()

            Return (dt)
            ''nFlowSheetRecordID,sResult
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("cls_LM_LabResult -- GetLM_LabResultRecID -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    ''===================================
    '' PSA 0n  20061209
#Region " PSA FLOWSHEET"

    Public Function GetFlowSheetMST_ID(ByVal FlowsheetName As String) As Long
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            Dim FlowSheetID As Long
            cmd = New SqlCommand("gsp_GetFlowSheetMST_ID", Con)
            cmd.CommandType = CommandType.StoredProcedure


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
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("cls_LM_LabResult -- GetFlowSheetMST_ID -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function

    'gsp_GetPSAValues

    Public Function GetPSAValues(ByVal PatientAge As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'Dim FlowSheetID As Long
            cmd = New SqlCommand("gsp_GetPSAValues", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@PatientAge", PatientAge)
            sqlParam.SqlDbType = SqlDbType.BigInt
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()

            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("cls_LM_LabResult -- GetPSAValues -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function
#End Region





    ''''''
End Class
