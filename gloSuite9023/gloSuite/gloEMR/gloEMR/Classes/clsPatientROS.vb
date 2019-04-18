
Imports System.Data.SqlClient


Public Class clsPatientROS
    'Private da As SqlDataAdapter
    ' Private ds As New DataSet
    ' Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    ' Private conString As String
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
    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        Return dv
        'grdCPT.DataSource = dv
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub

    Public Sub SortDataview(ByVal strsort As String)
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]"
        End If

    End Sub


    Public Function GetPatientROS(ByVal ID As Long) As DataView
        'Public Function GetAllCPT(ByVal ID As Long)
        'Dim strText As String
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ViewPatientROS", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            '   ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
            End If
            da.Dispose()
            da = Nothing
            sqlParam = Nothing
            Return dv
            'objBusLayer.Close_Con()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ' objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function CheckDuplicate(ByVal VisitID As Long, ByVal ROSCategory As String, ByVal ROSItem As String, ByRef trROS As SqlTransaction) As Boolean

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_CheckPatientROS", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trROS

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            ''            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@ROSCategory", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ROSCategory

            sqlParam = cmd.Parameters.Add("@ROSItem", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ROSItem

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim rowAffected As Int64
            'Dim dataread As SqlDataReader
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If rowAffected > 0 Then
                Return True     ' Duplicate Exists
            Else
                Return False    ' Duplicate Not Exists
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            ''''Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    ' For Edit 
    Public Function SelectPatientROS(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        'Dim objBusLayer As New clsBuslayer

        Dim cmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ScanROS", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt
            'dv = dt.DefaultView()
            'Return objBusLayer.PassCmdGetDV(cmd)
            ' objBusLayer.Close_Con()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Sub SelectCategory(ByVal CategotyID As Long)
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ScanCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategotyID

            Con.Open()
            cmd.ExecuteNonQuery()
            sqlParam = Nothing
            'Return objBusLayer.PassCmdGetDV(cmd)
            'objBusLayer.Close_Con()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable

        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_FillCategory_Mst", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "ROS"
            sqlParam.Value = CategoryType

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1
            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function GetAllROS(ByVal strGroup As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_FillROS", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@strGroup", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strGroup

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function GetAllVisits(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_FillVisit", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    'Fill Previous Histories in  trvPrevROS
    Public Function GetPrevROS(ByVal Interval As String, ByVal PatientID As Long, ByVal Sysdate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewROS", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@Interval", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Interval

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@dtSysdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Sysdate

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Sub AddNewROS(ByVal Count As Int16, ByVal ROSID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal ROSCategory As String, ByVal ROSItem As String, ByVal Comments As String)

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_InUpROS", Con)
            cmd.CommandType = CommandType.StoredProcedure            

            sqlParam = cmd.Parameters.AddWithValue("@ROSID", ROSID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@ROSCategory", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ROSCategory

            sqlParam = cmd.Parameters.Add("@ROSItem", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ROSItem

            sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Comments

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName   '' Rows(i)(2) ''Comments


            Con.Open()
            cmd.ExecuteNonQuery()
            If Count = 0 Then
                'Dim objAudit As New clsAudit
                If frmPatientROS.blnModify = True Then
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "ROS  modified. ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Modify, "ROS  modified. ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                Else
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "ROS  added. ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "ROS  Add. ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                End If
                'objAudit = Nothing
            End If

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient ROS  added.  ", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Sub


    Public Function AddNewROS(ByVal ROSID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal ArrLst As ArrayList) As Boolean
        Dim trROS As SqlTransaction = Nothing
        'Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            trROS = Con.BeginTransaction

            ''''' To Delete ROS 
            '''' If ROS is in Modify Mode then Use Delete-Insert Methode
            If frmPatientROS.blnModify = True Then
                Dim cmdDel As New SqlCommand("gsp_DeleteROS", Con)
                cmdDel.CommandType = CommandType.StoredProcedure
                cmdDel.Transaction = trROS

                Dim sqlParam1 As SqlParameter

                sqlParam1 = cmdDel.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam1.Direction = ParameterDirection.Input
                'sqlParam1.Value = VisitID

                sqlParam1 = cmdDel.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam1.Direction = ParameterDirection.Input

                cmdDel.ExecuteNonQuery()
                cmdDel.Parameters.Clear()
                cmdDel.Dispose()
                cmdDel = Nothing
                sqlParam1 = Nothing
            End If

            Dim i As Integer
            For i = 0 To ArrLst.Count - 1

                Dim lst As myList
                lst = CType(ArrLst(i), myList)
                '' Reset
                ROSID = 0

                Dim cmd As SqlCommand = New SqlCommand("gsp_InUpROS", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trROS

                '''' If Modify then All Recordes are Deleted 
                '''' there No Need to Check Duplicate Data 
                If frmPatientROS.blnModify = False Then
                    If CheckDuplicate(VisitID, lst.HistoryCategory, lst.HistoryItem, trROS) = True Then
                        '' if Exists then Modify (Update)
                        ROSID = 1  ''(Update)
                    Else
                        ''If not found any record then Add (Not Update)
                        ROSID = 0  '' (NEW)
                    End If

                End If

                sqlParam = cmd.Parameters.AddWithValue("@ROSID", ROSID)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = ROSID

                sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@ROSCategory", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.HistoryCategory  '' Rows(i)(0) ''ROSTable.ROSCategory

                sqlParam = cmd.Parameters.Add("@ROSItem", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.HistoryItem   '' Rows(i)(1) ''ROSItem

                sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.Description  '' Rows(i)(2) ''Comments

                sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
                sqlParam.Direction = ParameterDirection.Input

                '' chetan added for patient tracking on 18-oct-2010

                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gstrLoginName   '' Rows(i)(2) ''Comments
                '' chetan added for patient tracking on 18-oct-2010

                '''' Con.Open()

                If lst.ROSSource.Trim <> "" Then
                    sqlParam = cmd.Parameters.Add("@sROSSource", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ROSSource  '' Rows(i)(2) ''Comments
                End If

                If lst.ROSDateEntered <> Nothing Then
                    sqlParam = cmd.Parameters.Add("@dtTransactionDate", SqlDbType.DateTime)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ROSDateEntered
                End If

                If lst.ROSPatientFormID > 0 Then
                    sqlParam = cmd.Parameters.Add("@nPatientFormID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ROSPatientFormID
                End If
              

                cmd.ExecuteNonQuery()

                If i = 0 Then
                    If frmPatientROS.blnModify = True Then
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient ROS  modified.", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Modify, "Patient ROS  modified.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                    Else
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient ROS  added.  ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient ROS  added.  ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                    End If
                End If
LINE1:
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            Next

            trROS.Commit()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Patient ROS  added.  ", gloAuditTrail.ActivityOutCome.Success)
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            trROS.Rollback()
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            trROS.Rollback()
            Return False
        Finally
            Con.Close()

            If trROS IsNot Nothing Then

                trROS.Dispose()
                trROS = Nothing
            End If

            sqlParam = Nothing
        End Try
    End Function


    ''''''''Public Function UpdateROS(ByVal VisitID As Integer, ByVal PatientID As Integer, ByVal ROSCategory As String, ByVal ROSItem As String, ByVal Comments As String)
    ''''''''    Try
    ''''''''        Dim cmd As New SqlCommand("gsp_InUpROS", Con)
    ''''''''        cmd.CommandType = CommandType.StoredProcedure
    ''''''''        Dim sqlParam As SqlParameter

    ''''''''        sqlParam = cmd.Parameters.Add("@ROSID", SqlDbType.Int)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = ID

    ''''''''        sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.Int)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = VisitID

    ''''''''        sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.Int)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = PatientID

    ''''''''        sqlParam = cmd.Parameters.Add("@ROSCategory", SqlDbType.VarChar)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = ROSCategory

    ''''''''        sqlParam = cmd.Parameters.Add("@ROSItem", SqlDbType.VarChar)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = ROSItem

    ''''''''        sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
    ''''''''        sqlParam.Direction = ParameterDirection.Input
    ''''''''        sqlParam.Value = Comments

    ''''''''        Con.Open()
    ''''''''        cmd.ExecuteNonQuery()
    ''''''''        If Count = 0 Then
    ''''''''            Dim objAudit As New clsAudit
    ''''''''            If frmROS.blnModify = True Then
    ''''''''                objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Patient ROS Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
    ''''''''            Else
    ''''''''                objAudit.CreateLog(clsAudit.enmActivityType.Add, "Patient ROS Added", gstrLoginName, gstrClientMachineName, gnPatientID)
    ''''''''            End If
    ''''''''            objAudit = Nothing
    ''''''''        End If

    ''''''''    Catch ex As SqlException
    ''''''''        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''''''    Catch ex As Exception
    ''''''''        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''''''''    Finally
    ''''''''        Con.Close()
    ''''''''    End Try
    ''''''''End Function

    Public Sub DeleteROS(ByVal VisitID As Long, ByVal PatientID As Long)
        Dim sqlParam As SqlParameter
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_DeleteROS", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "ROS Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "ROS  deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "ROS  deleted.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try

    End Sub
    'function commented as not in use
    'Public Function CheckRecordCount(ByVal strflag As String) As Boolean
    '    Try
    '        Dim Cmd As SqlCommand
    '        Dim count As Int64
    '        Cmd = New SqlCommand("gsp_CheckRecordCount", Con)
    '        Cmd.CommandType = CommandType.StoredProcedure

    '        Dim objParam As SqlParameter
    '        objParam = Cmd.Parameters.Add("@Interval", SqlDbType.Char)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = strflag

    '        objParam = Cmd.Parameters.Add("@PatientID", gnPatientID)
    '        objParam.Direction = ParameterDirection.Input

    '        objParam = Cmd.Parameters.Add("@dtSysDate", SqlDbType.DateTime)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = Format(Now, "MM/dd/yyyy hh:mm:ss")

    '        objParam = Cmd.Parameters.Add("@formstatus", SqlDbType.Char)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = "R"

    '        Con.Open()
    '        count = Cmd.ExecuteScalar
    '        Con.Close()
    '        If count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function
    Public Sub SaveNarration(ByVal VisitID As Long, ByVal PatientID As Long, ByVal strTempFileName1 As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_InUpNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure

            'Dim ExamParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            'Narration as Word Document
            sqlParam = cmd.Parameters.Add("@Narration", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFileName1)
            sqlParam.Value = mstream.Read()
            mstream.Close()

            Con.Open()
            cmd.ExecuteNonQuery()
            mstream = Nothing
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, strExamName & " Narration Added", gstrLoginName, gstrClientMachineName, nPatinetID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Narration  added. ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Narration  added. ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Sub

    ''For Edit Narration
    Public Function SelectNarration(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_ScanNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            sqlParam = Nothing
            Return dt

            'Dim objAudit As New clsAudit
            ''objAudit.CreateLog(clsAudit.enmActivityType.Add, strExamName & " Narration Added", gstrLoginName, gstrClientMachineName, nPatinetID)
            'objAudit = Nothing
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Public Sub DeleteNarration(ByVal VisitID As Long, ByVal PatientID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_DeleteNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure            
            'Dim ExamParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@VisitId", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientId", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()

            'Dim objAudit As New clsAudit
            ''objAudit.CreateLog(clsAudit.enmActivityType.Add, strExamName & " Narration Added", gstrLoginName, gstrClientMachineName, nPatinetID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "Narration modified.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "Narration  modified. ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Sub

    Public Function Fill_LockPatientROS(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            
            'cmd = New SqlCommand
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

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            objParam = Nothing
            Return dt
        Catch ex As Exception
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function
    'Public Function GenerateVisitID() As Long
    '    Try
    '        Dim cmdVisits As SqlCommand
    '        Dim objParam As SqlParameter
    '        Dim objFlagParam As SqlParameter

    '        cmdVisits = New SqlCommand("gsp_InsertVisits", Con)
    '        cmdVisits.CommandType = CommandType.StoredProcedure
    '        Con.ConnectionString = GetConnectionString()

    '        objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.Int)
    '        objParam.Direction = ParameterDirection.Input
    '        'objParam.Value = objPrescription.PatientID
    '        objParam.Value = gnPatientID

    '        objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = Now

    '        'Retrieve Appointment ID
    '        Dim nAppointmentID As Integer
    '        Dim objAppointmentID As New clsAppointments
    '        nAppointmentID = objAppointmentID.GetPatientAppointment(System.DateTime.Now, gnPatientID)
    '        objAppointmentID = Nothing


    '        objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.Int)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = nAppointmentID

    '        objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = GetPrefixTransactionID

    '        objParam = cmdVisits.Parameters.Add("@VisitID", 0)
    '        objParam.Direction = ParameterDirection.Output
    '        'objParam.Value = 0

    '        objFlagParam = cmdVisits.Parameters.Add("@flag", SqlDbType.Int)
    '        objFlagParam.Direction = ParameterDirection.Output

    '        If Con.State = ConnectionState.Closed Then
    '            Con.Open()
    '        End If

    '        cmdVisits.ExecuteNonQuery()

    '        If objFlagParam.Value = 0 Then
    '            Dim objAudit As New clsAudit
    '            objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added on " & CType(Now, String), gstrLoginName, gstrClientMachineName)
    '            objAudit = Nothing
    '        End If

    '        Return objParam.Value

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
End Class
