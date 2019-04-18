Imports System
Imports System.Data
Imports System.Data.SqlClient
'Imports System.Data.SqlClient


Public Class clsCPT
    ' Private da As SqlDataAdapter = Nothing
    'Private ds As New DataSet
    ' Private dt As DataTable = Nothing
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Initialize, "clsCPT -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- New -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Initialize, "clsCPT -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- New -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
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
    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        Return dv
        'grdCPT.DataSource = dv
    End Function

    Public Function GetAllCPT(ByVal ID As Long) As DataView
        'Public Function GetAllCPT(ByVal ID As Long)
        'Dim strText As String
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()

            cmd = New SqlCommand("gsp_ViewCPT_MST", Con)
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
            dv = New DataView(dt.Copy())

            sqlParam = Nothing
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If

            Return dv
            'objBusLayer.Close_Con()
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Load, "clsCPT -- GetAllCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- GetAllCPT -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Load, "clsCPT -- GetAllCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- GetAllCPT -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try

    End Function

    Public Function GetAllCPT(Optional ByVal Speciality As String = "", Optional ByVal Category As String = "") As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            ''code commented for optimization 6020
            'Dim dtCPT As New DataTable
            'Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            'oDB.Connect(False)
            'Dim query As String
            ''Code Modified by Mayuri:20091005
            ''Used Speciality and Category varibles in order to fill treeview CPTCurrent according to selected Speciality or Category
            'If Speciality = "" Or Category = "" Or Speciality = "All" Then
            '    query = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE ISNULL(bIsBlocked,0) = 0"

            'Else
            '    query = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription,ISNULL(sCategoryDesc,'') as sCategoryDesc  FROM CPT_MST WHERE sSpecialityCode='" & Speciality & "' And sCategoryDesc='" & Category & "'"
            'End If
            ''end Code Modified by Mayuri:20091005
            ''If Category = "" Then
            ''    'query = ""
            ''Else
            ''    query = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription,ISNULL(sCategoryDesc,'') as sCategoryDesc  FROM CPT_MST WHERE sCategoryDesc='" & Category & "'"

            ''End If
            ''Dim query As String = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription,ISNULL(sCategoryDesc,'') as sCategoryDesc  FROM CPT_MST WHERE ISNULL(bIsBlocked,0) = 0"

            'oDB.Retrive_Query(query, dtCPT)
            'oDB.Disconnect()
            'oDB.Dispose()
            'oDB = Nothing
            'If IsNothing(dtCPT) = False Then
            '    Return dtCPT
            'End If
            ''''''''''''''''''''''
            ''converted query into stored procdure in 6020
            cmd = New SqlCommand("gsp_GetAllCPT", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@Speciality", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Speciality
            sqlParam = cmd.Parameters.Add("@Category", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Category

            Con.Open()
            'cmd.ExecuteNonQuery()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            sqlParam = Nothing
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If


            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Dispose()
                cmd = Nothing
            End If
           
        End Try

    End Function
    'Code Commented by Mayuri:20091005
    'Public Function GetSpeciality() As DataTable
    '    Try
    '        Dim dtSpeciality As New DataTable
    '        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
    '        oDB.Connect(False)
    '        'Dim query As String = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE ISNULL(bIsBlocked,0) = 0"
    '        'Dim query As String = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription,ISNULL(sCategoryDesc,'') as sCategoryDesc  FROM CPT_MST WHERE ISNULL(bIsBlocked,0) = 0"
    '        Dim query As String = "SELECT nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription,ISNULL(sCategoryDesc,'') as sCategoryDesc  FROM CPT_MST WHERE sSpecialityCode='Nutritionist'"

    '        oDB.Retrive_Query(query, dtSpeciality)
    '        oDB.Disconnect()
    '        oDB.Dispose()
    '        oDB = Nothing
    '        If IsNothing(dtSpeciality) = False Then
    '            Return dtSpeciality
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function

    'End Code Commented by Mayuri:20091005

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

    Public Function CheckDuplicate(ByVal CPTCode As String, ByVal description As String) As Boolean
        Try
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM CPT_MST WHERE sCPTCode = '" & CPTCode & "' AND sDescription = '" & description & "'", Con)
            Dim oResult As Object
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            oResult = cmd.ExecuteScalar
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If CType(oResult, Int32) > 0 Then
                Return True
            Else
                Return False
            End If

           

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Function CheckDuplicate(ByVal ID As Long, ByVal CPTCode As String, ByVal CategoryID As Int64) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckCPT_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@CPTCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CPTCode

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryID

            Con.Open()
            Dim rowAffected As Int64
            'Dim dataread As SqlDataReader
            rowAffected = CType(cmd.ExecuteScalar, Int64)
            'Do while  


            'Loop
            'rowAffected = cmd.ExecuteReader
            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- CheckDuplicate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- CheckDuplicate -- " & ex.ToString)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- CheckDuplicate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- CheckDuplicate -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
        End Try

    End Function
    Public Function SelectCPT(ByVal ID As Long)
        'Dim objBusLayer As New clsBuslayer
        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_ScanCPT_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@CPTID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            dv = dt.DefaultView()

            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            'Return objBusLayer.PassCmdGetDV(cmd)
            ' objBusLayer.Close_Con()
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- SelectCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- SelectCPT -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Select, "clsCPT -- SelectCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- SelectCPT -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function SelectCategory(ByVal ID As Long)
        'Dim objBusLayer As New clsBuslayer
        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_ScanCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()
            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'Return objBusLayer.PassCmdGetDV(cmd)
            'objBusLayer.Close_Con()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- SelectCategory -- " & ex.ToString)
        Catch ex As Exception
            'UpdateLog("clsCPT -- SelectCategory -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function GetAllSpeciality() As DataTable
        ' Dim objBusLayer As New clsBuslayer
        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_FillSpecialty_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'Con.Open()
            'cmd.ExecuteNonQuery()
            'ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)


            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
           
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            Return dt

            'Return objBusLayer.PassCmdGetDV(cmd)
            'objBusLayer.Close_Con()
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- GetAllSpeciality -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("clsCPT -- GetAllSpeciality -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllCategory() As DataTable
        'Dim objBusLayer As New clsBuslayer
        Try
            '   objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_FillCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "CPT"

            Con.Open()
            'cmd.ExecuteNonQuery()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)

            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- GetAllCategory -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            ' UpdateLog("clsCPT -- GetAllCategory -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function AddNewCPT(ByVal ID As Long, ByVal CPTCode As String, ByVal Description As String, ByVal SpecialtyID As Long, ByVal CategoryID As Long)
        'Dim objBusLayer As New clsBuslayer
        Try
            '   objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_InUpCPT_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@CPTID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@CPTCode", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CPTCode

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@SpecialtyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SpecialtyID

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryID

            sqlParam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnClinicID

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input


            Con.Open()
            cmd.ExecuteNonQuery()

            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Dim objAudit As New clsAudit
            If ID <> 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Modify, "CPT Modified", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Modify, "CPT Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, " CPT Modified", gstrLoginName, gstrClientMachineName)
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "CPT Added", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101008
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "CPT Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " CPT Added", gstrLoginName, gstrClientMachineName)
            End If
            'objAudit = Nothing

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "clsCPT -- AddNewCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- AddNewCPT -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "clsCPT -- AddNewCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- AddNewCPT -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function DeleteCPT(ByVal ID As Long, ByVal CPTCode As String)
        'Dim objBusLayer As New clsBuslayer
        'objBusLayer.Open_Con()
        Try
            Dim cmd As New SqlCommand("gsp_DeleteCPT_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()

            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'objBusLayer.Close_Con()
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "CPT Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101008
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "CPT Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, " CPT Deleted", gstrLoginName, gstrClientMachineName)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "clsCPT -- DeleteCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPT -- DeleteCPT -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "clsCPT -- DeleteCPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPT -- DeleteCPT -- " & ex.ToString)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        dv.RowFilter = strexpr
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
            Return strsplittext
        End Try
    End Function
End Class
