Imports System.Data.SqlClient
Imports System
Imports System.IO


Public Class clsEARReporting


    'Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
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
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        'grdCPT.DataSource = dv
        Return dv
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            If str <> dv.Table.Columns(5).ColumnName Then
                strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
                dv.RowFilter = strexpr
            End If
        End If

    End Sub
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub

   
    Public Function RetrieveDocumentFile(ByVal EARFileType As String, ByVal EARfilename As String) As String
        Dim oResult As New Object
        Dim strFileName As String = ""
        Dim cmd As SqlCommand = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_RetrieveEARFileInfo", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@EARFileType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = EARFileType

            sqlParam = cmd.Parameters.Add("@EARFileName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = EARfilename

            ''''''if connection state is opened then no need to open the connection again
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If



            oResult = cmd.ExecuteScalar

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If oResult Is Nothing Then
                Return ""
            End If

            If IsDBNull(oResult) = False Then
                'strFileName = ExamNewFaxFileName(gstrgloEMRStartupPath & gstrgloTempFolder, ".xml")
                '' generate Physical file
                strFileName = GenerateFile(oResult, strFileName)
                Return strFileName
            Else
                Return ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
            '        Con.Dispose()
            Return ""
        Finally
            If Not IsNothing(Con) Then
                Con.Close()
                '           Con.Dispose()
            End If
            If Not IsNothing(oResult) Then
                oResult = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
            End If
        End Try
    End Function


    Public Function GenerateFile(ByVal cntFromDB As Object, ByVal strFileName As String) As String
        Try
            If Not cntFromDB Is Nothing Then
                Dim content() As Byte = CType(cntFromDB, Byte())
                'Dim stream As MemoryStream = New MemoryStream(content)
                Dim filename As String
                filename = My.Computer.FileSystem.GetTempFileName()

                Dim oFile As New System.IO.FileStream(filename, System.IO.FileMode.Create)
                oFile.Write(content, 0, content.Length)

                'stream.WriteTo(oFile)
                oFile.Close()
                'stream.Close()
                'stream.Dispose()
                oFile.Dispose()
                content = Nothing
                'Dim fileContents As String
                'rchtxtbxEARRequestFile.Text = My.Computer.FileSystem.ReadAllText(filename)
                'strbld.Append(My.Computer.FileSystem.ReadAllText(filename))
                Return filename
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
            
        End Try

    End Function
    Public Function GetAllSIG() As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter = Nothing
            Con.Open()
            'cmd.ExecuteNonQuery()
            'ds.Clear()

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
                dv = New DataView(dt.Copy())
                dt.Dispose()
            End If


            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            Return dv

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function CheckDuplicate(ByVal ID As Long, ByVal Dosage As String, ByVal sRoute As String, ByVal sFrequency As String, ByVal sDuration As String, ByVal sRefills As String, ByVal bAsNeeded As Boolean) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            'sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = Dosage

            ''Sandip Darade 20090806  
            ' nSIGID, sDosage, sRoute, sFrequency, sDuration, bAsNeeded, sRefills
            sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sRoute

            sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sFrequency

            sqlParam = cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sDuration

            sqlParam = cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sRefills

            sqlParam = cmd.Parameters.Add("@bAsNeeded", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bAsNeeded
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    Public Sub SelectSIG(ByVal ID As Long)
        Try
            Dim cmd As New SqlCommand("gsp_ScanSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@SIGID", SqlDbType.BigInt)
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

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub

    Public Function AddNewSIG(ByVal ID As Long, ByVal Dosage As String, ByVal Route As String, ByVal Freq As String, ByVal Duration As String, ByVal AsNeeded As CheckState, ByVal Refills As String) As Long
        Dim _ReturnValue As Long = 0

        Try
            Dim cmd As New SqlCommand("gsp_InUpSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter



            sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Dosage

            sqlParam = cmd.Parameters.Add("@Route", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Route

            sqlParam = cmd.Parameters.Add("@Frequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Freq

            sqlParam = cmd.Parameters.Add("@Duration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Duration

            sqlParam = cmd.Parameters.Add("@AsNeeded", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input

            Dim chk As Integer
            If AsNeeded = CheckState.Checked Then
                chk = 1
            Else
                chk = 0
            End If
            sqlParam.Value = chk


            'PER NO: 2140 - 20 jun 2009 - Saagar K
            sqlParam = cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Refills
            'PER NO: 2140 - 20 jun 2009 - Saagar K


            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            sqlParam = cmd.Parameters.Add("@SIGID", SqlDbType.BigInt)
            'sarika 15th oct 07
            'sqlParam.Direction = ParameterDirection.Input
            sqlParam.Direction = ParameterDirection.InputOutput
            '---------------------------------------
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()

            'sarika 15th oct 07
            _ReturnValue = sqlParam.Value
            '--------------------
            'Return objBusLayer.PassCmdGetDV(cmd)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

            If ID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, "SIG Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "SIG Modified", gstrLoginName, gstrClientMachineName)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, "SIG Added", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "SIG Added", gstrLoginName, gstrClientMachineName)

            End If

            Return _ReturnValue
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub DeleteSIG(ByVal ID As Long, ByVal Dosage As String)
        Try
            Dim cmd As New SqlCommand("gsp_DeleteSIG_MST", Con)
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
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, "SIG Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "SIG Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
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
