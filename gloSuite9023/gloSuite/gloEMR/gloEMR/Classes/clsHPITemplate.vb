Imports System.Data.SqlClient
Imports ADODB.StreamClass
Imports System.IO

Public Class clsHPITemplate
            
    ' Private da As SqlDataAdapter
    ' Private ds As New DataSet
    ' Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException   ' Catch the error.
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Return Nothing
        'grdCPT.DataSource = dv
    End Function

    Public Sub SortDataview(ByVal strsort As String)
        'DCatview.Sort = strsort
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]"
        End If

    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String, ByVal Dview As DataView)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(Dview) = False) Then
            str = Dview.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & Dview.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            Dview.RowFilter = strexpr
        End If

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

    Public Function CheckDuplicate(ByVal ID As Int64, ByVal TemplateName As String, ByVal CategoryID As Int64, ByVal ProviderID As Int64) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateName

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryID

            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderID

            'gstrDoctorName()

            Con.Open()
            Dim rowAffected As Int64
            '   Dim dataread As SqlDataReader
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
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllHPITemplate(ByVal ID As Long, Optional ByVal ProviderID As Long = 0) As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If ProviderID <> 0 Then
                sqlParam.Value = ProviderID
            End If

            Con.Open()
            'cmd.ExecuteNonQuery()
            'ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd

            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = New DataView(dt.Copy())
            da.Dispose()
            da = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            dt.Dispose()
            dt = Nothing
            Return dv
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function SelectHPITemplate(ByVal ID As Long)
        Try
            Dim cmd As New SqlCommand("gsp_ScanHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd

            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = dt.Copy().DefaultView()
            da.Dispose()
            da = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            dt.Dispose()
            dt = Nothing

        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function GetAllProvider() As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_FillProvider_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

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

        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetSelectedProviderID() As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_ScanProviderDetails", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ProviderName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            '' Commeneted on 20080922 
            ''sqlParam.Value = gstrDoctorName
            sqlParam.Value = gstrLoginProviderName

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
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    'Public Function GetAllCategory() As DataTable
    '    Try
    '        Dim cmd As New SqlCommand("gsp_FillCategory_MST", Con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter
    '        sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = "Template"

    '        da = New SqlDataAdapter
    '        da.SelectCommand = cmd
    '        dt = New DataTable
    '        da.Fill(dt)

    '        Return dt
    '    Catch ex As SqlException
    '        MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function
    Public Function GetAllCategory() As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_FillCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "HPI"

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1

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
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddNewHPITemplate(ByVal ID As Long, ByVal TemplateName As String, ByVal CategoryID As Long, ByVal ProviderID As Long, ByVal Description As String)
        Try
            Dim cmd As New SqlCommand("gsp_InUpHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateName

            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If CategoryID > 0 Then
                sqlParam.Value = CategoryID
            Else
                sqlParam.Value = -1
            End If

            sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If ProviderID > 0 Then
                sqlParam.Value = ProviderID
            Else
                sqlParam.Value = -1
            End If

            'sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(Description)
            ''mstream.LoadFromFile(Description)
            ''            Return mstream

            'sqlParam.Value = SaveTemplateDescription(Description).Read
            sqlParam.Value = mstream.Read
            mstream.Close()

            sqlParam = cmd.Parameters.Add("@MachineID ", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            Con.Open()
            cmd.ExecuteNonQuery()
            mstream = Nothing
            sqlParam = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            'Return objBusLayer.PassCmdGetDV(cmd)

            'Dim objAudit As New clsAudit
            If ID <> 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, TemplateName & " Template Modified", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, TemplateName & " Template Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'objAudit.CreateLog(clsAudit.enmActivityType.Modify, TemplateName & " Template Modified", gstrLoginName, gstrClientMachineName)
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, TemplateName & " Template Added", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, TemplateName & " Template Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'objAudit.CreateLog(clsAudit.enmActivityType.Add, TemplateName & " Template Added", gstrLoginName, gstrClientMachineName)
            End If
            'objAudit = Nothing
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function UpdateHPITemplate(ByVal TemplateID As Integer, ByVal strFileName As String)
        Try
            Dim cmd As New SqlCommand("gsp_UpdateHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateID


            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strFileName)
            ''mstream.LoadFromFile(Description)
            ''            Return mstream

            'sqlParam.Value = SaveTemplateDescription(Description).Read
            sqlParam.Value = mstream.Read
            mstream.Close()
            Con.Open()
            cmd.ExecuteNonQuery()
            mstream = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

        Catch ex As SqlException
            UpdateLog("UpdateHPITemplate - clHPITemplate - " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Update Templates", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function DeleteHPITemplate(ByVal ID As Long, ByVal TemplateName As String)
        Try
            Dim cmd As New SqlCommand("gsp_DeleteHPITemplate", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, TemplateName & " Template Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, TemplateName & " Template Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, TemplateName & " Template Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, "DeleteHPITemplate - clHPITemplate - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("DeleteHPITemplate - clHPITemplate - " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Delete, "DeleteHPITemplate - clHPITemplate - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Private Function SaveTemplateDescription(ByVal strFileName As String) As ADODB.Stream
        Dim mstream As ADODB.Stream
        mstream = New ADODB.Stream
        mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        mstream.Open()
        mstream.LoadFromFile(strFileName)
        Return mstream

    End Function
    Public Function GetHPITemplateContents(ByVal TemplateId As Long) As DataSet
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        'Dim objSQLDataReader As SqlDataReader
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetHPITemplateContents"
        Con.Open()
        Cmd.Parameters.Clear()
        sParam.ParameterName = "@nTemplateID"
        sParam.SqlDbType = SqlDbType.BigInt
        sParam.Value = TemplateId
        Cmd.Parameters.Add(sParam)
        'Dim dr As New SqlDataAdapter
        Cmd.Connection = Con
        Dim da As New SqlDataAdapter(Cmd)
        Dim dsData As New DataSet
        da.Fill(dsData)
        Con.Close()
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        da.Dispose()
        da = Nothing
        sParam = Nothing
        Return dsData
    End Function

    Public Function Fill_HPITemplateNames() As DataTable
        Dim dt As DataTable
        Dim da As SqlDataAdapter
        Dim cmd As New SqlCommand("gsp_FillHPITemplate", Con)
        cmd.CommandType = CommandType.StoredProcedure
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        Con.Close()
        cmd.Parameters.Clear()
        cmd.Dispose()
        cmd = Nothing
        da.Dispose()
        da = Nothing
        Return dt
    End Function
    Public Function sp_GetTriggerActions(ByVal nConditionID As Long, ByVal nType As Long, ByVal nGender As Int16) As DataTable
        Dim dt As DataTable
        Dim Cmd As New SqlCommand
        '        Dim sParam As New SqlParameter
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetTriggerActions"
        Con.Open()
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@nConditionID", nConditionID)
        Cmd.Parameters.AddWithValue("@nType", nType)
        Cmd.Parameters.AddWithValue("@nGender", nGender)
        Cmd.Connection = Con
        Dim da As SqlDataAdapter = New SqlDataAdapter
        da.SelectCommand = Cmd
        dt = New DataTable
        da.Fill(dt)
        Con.Close()
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        da.Dispose()
        da = Nothing
        Return dt
    End Function

    Public Function sp_GetTriggerConditions(ByVal sParameter As String, ByVal sValue As String, ByVal nGender As Int16) As DataTable
        Dim dt As DataTable
        Dim Cmd As New SqlCommand
        'Dim sParam As New SqlParameter
        'Dim objSQLDataReader As SqlDataReader
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetTriggerConditions"
        Con.Open()
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@sParameter", sParameter)
        Cmd.Parameters.AddWithValue("@sValue", sValue)
        Cmd.Parameters.AddWithValue("@nGender", nGender)
        Cmd.Connection = Con
        Dim da As SqlDataAdapter = New SqlDataAdapter
        da.SelectCommand = Cmd
        dt = New DataTable
        da.Fill(dt)
        Con.Close()
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        da.Dispose()
        da = Nothing
        Return dt
    End Function

    Public Function GetICD9CodeDesc(ByVal id As Long) As DataTable
        Dim dt As DataTable
        Dim Cmd As New SqlCommand
        '        Dim sParam As New SqlParameter
        'Dim objSQLDataReader As SqlDataReader
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_ScanICD9"
        Con.Open()
        Cmd.Parameters.Clear()
        Cmd.Parameters.AddWithValue("@ICD9ID", id)
        Cmd.Connection = Con
        Dim da As SqlDataAdapter = New SqlDataAdapter
        da.SelectCommand = Cmd
        dt = New DataTable
        da.Fill(dt)
        Con.Close()
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        da.Dispose()
        da = Nothing

        Return dt
    End Function

    Public Function GetPatientGender(ByVal nPatientId As Long) As Integer
        Try
            Dim strGender As String
            Dim cmd As New SqlCommand("gsp_GetPatientGender", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientId

            Con.Open()
            strGender = cmd.ExecuteScalar()
            Con.Close()
            Select Case strGender
                Case "Male"
                    GetPatientGender = 1
                Case "Female"
                    GetPatientGender = 2
                Case Else
                    GetPatientGender = 3
            End Select
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            sqlParam = Nothing
        Catch ex As SqlException
            UpdateLog("GetPatientGender - clHPITemplate - " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "HPT Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
End Class
