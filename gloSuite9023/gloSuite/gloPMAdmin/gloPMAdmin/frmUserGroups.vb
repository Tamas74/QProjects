Imports System.Data.SqlClient
'Form Added  By  Sandip Darade To Set User Groups 
Public Class frmUserGroups

#Region "Variables & Properties"
    Dim _groupID As Int64 = 0
    Dim _groupname As String = ""
    Dim _nClinicID As Int64 = 1

    Public Property ID() As Int64
        Get
            Return _groupID
        End Get
        Set(ByVal value As Int64)
            _groupID = value
        End Set
    End Property
#End Region

#Region "Constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal GroupId As Int64, ByVal GroupName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _groupID = GroupId
        _groupname = GroupName
        ' Add any initialization after the InitializeComponent() call.

    End Sub

#End Region

#Region "Form Load"

    Private Sub frmUserGroups_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetAllUsers() ''Fill users in grid
        txtGroupName.Focus()
    End Sub

#End Region

#Region "Select and Deselect all buttons for TreeView "

    'Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        For i As Integer = 0 To trvRights.Nodes.Count - 1
    '            For j As Integer = 0 To trvRights.Nodes(i).Nodes.Count - 1
    '                trvRights.Nodes(i).Nodes(j).Checked = True
    '            Next
    '        Next

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub btnDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        For i As Integer = 0 To trvRights.Nodes.Count - 1
    '            For j As Integer = 0 To trvRights.Nodes(i).Nodes.Count - 1
    '                trvRights.Nodes(i).Nodes(j).Checked = False
    '            Next
    '        Next

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub trvRights_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        If e.Node.Level = 0 Then
    '            For Each oNode As TreeNode In e.Node.Nodes
    '                oNode.Checked = e.Node.Checked
    '            Next
    '        End If
    '    Catch ex As Exception
    '    End Try

    'End Sub

#End Region

#Region "Save & Close Buttons"

    Private Sub tsb_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Save.Click
        If ValidateData() = True Then
            If CheckGroupExists(txtGroupName.Text.Trim(), _groupID) = False Then ''check if group exists
                AddUserGroups(txtGroupName.Text.Trim(), _groupID) 'Save group
                Save_Detail_new() 'Save user info to detail table
                Me.Close()
            Else
                MessageBox.Show("Group name alredy in use. Please enter unique group name. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub tsb_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Close.Click
        _groupID = 0
        Me.Close()
    End Sub

#End Region
    'Sandip Darade 7th Feb 09
#Region "Save and Retrieve Methods"

    'Add  user group to  Master table
    Private Function AddUserGroups(ByVal GroupName As String, ByVal GroupID As Int64) As Boolean

        Dim Con As New SqlConnection
        Con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As New SqlCommand("gSP_INUP_UserGroups", Con)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sqlParam As SqlParameter
        'nGroupID, sGroupName, nType, nClinicID
        sqlParam = cmd.Parameters.Add("@nGroupID", SqlDbType.BigInt)
        sqlParam.Direction = ParameterDirection.InputOutput
        sqlParam.Value = GroupID

        sqlParam = cmd.Parameters.Add("@sGroupName", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = GroupName

        sqlParam = cmd.Parameters.Add("@nType", SqlDbType.Int)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = 0

        sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = _nClinicID
        Con.Open()

        cmd.ExecuteNonQuery()

        'Dim ngrpID As Int64 = 0
        _groupID = cmd.Parameters("@nGroupID").Value

        'Delete from detail tble
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "DELETE FROM SEC_UserGroups_DTL WHERE nGroupID = " & _groupID & " "
        cmd.Connection = Con
        cmd.ExecuteNonQuery()




        Con.Close()
        'Con = Nothing
        'cmd = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(Con) And Not IsNothing(cmd) Then
            Con.Dispose()
            cmd.Dispose()
        End If

        Return True
    End Function


    'Add  user  of the  group to  Detail table
    Private Function AddDetails(ByVal GroupID As Int64, ByVal UserID As Int64, ByVal UserName As String) As Boolean
        Dim Con As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            Con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            cmd = New SqlCommand("[gsp_INUP_UserGroupsDetail]", Con)
            cmd.CommandType = CommandType.StoredProcedure

            'nGroupID, nUserID, sUserName, nClinicID
            sqlParam = cmd.Parameters.Add("@nGroupID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GroupID

            sqlParam = cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = UserID

            sqlParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = UserName

            sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = _nClinicID

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing

        End Try

        'Con = Nothing
        'cmd = Nothing
        ''Sandip Darade 20091117


        Return True
    End Function


    'Check if group with same name exists
    Public Function CheckGroupExists(ByVal GroupName As String, Optional ByVal nGroupID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_Check_UserGroup_Exists"
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@GroupName"
            .Value = GroupName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)

        If nGroupID <> 0 Then
            Dim objParaGroupID As New SqlParameter
            With objParaGroupID
                .ParameterName = "@GroupID"
                .Value = nGroupID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaGroupID)
        End If
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        ' objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function


    'Validate group
    Private Function ValidateData() As Boolean
        Dim IsUserSelected As Boolean = False
        Dim _result As Boolean = True

        If txtGroupName.Text.Trim() = "" Then
            MessageBox.Show("Please enter a name for the group. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtGroupName.Focus()
            _result = False
            Return _result
        End If


        dgListView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For i As Integer = 0 To dgListView.RowCount - 1
            If Convert.ToBoolean(dgListView.Rows(i).Cells(0).Value) = True Then
                IsUserSelected = True
                Exit For
            End If
        Next

        If IsUserSelected = False Then
            MessageBox.Show("Please select users to group. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            _result = False
            Return _result
        End If
        Return _result

    End Function


    'get group to modify 
    Private Function GetGroupToModify(ByVal GroupID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strQry As String = ""
        Dim dt As New DataTable

        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            strQry = "SELECT  ISNULL(nUserID,0) AS nUserID FROM SEC_UserGroups_DTL WHERE nGroupID =" & GroupID & "  "
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
        End Try
        Return dt
    End Function

#End Region

    ''Region Added By Sandip Darade 10th Feb 09 
#Region "Save and Retrieve Methods"
    ''Get all  active users in a table 
    Private Function GetallUsers() As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strQry As String = ""
        Dim dt As New DataTable

        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            strQry = "Select ISNULL(nUserID,0) AS nUserID, ISNULL(nProviderID,0) AS nProviderID, ISNULL(sLoginName,'') AS sLoginName,ISNULL(sFirstName,'') AS sFirstName, ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName, ISNULL(sEmail,'') AS sEmail,ISNULL(sExchangeLogin,'') AS sExchangeLogin from User_MST WHERE nBlockstatus = 0 "
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)

            objCon.Close()
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
        End Try
        Return dt
    End Function

    ''Fill the users in the grid
    Private Sub SetAllUsers()
        Dim dtList As DataTable = GetallUsers()
        Dim _dvList As DataView
        If dtList IsNot Nothing Then
            If dtList.Rows.Count > 0 Then

                _dvList = dtList.DefaultView
                dgListView.DataSource = _dvList

                Dim _thiswidth As Integer = Panel2.Width

                dgListView.Columns.Insert(0, New DataGridViewCheckBoxColumn(False))
                dgListView.Columns(0).HeaderText = "Select"
                dgListView.Columns(1).HeaderText = "UserId"
                dgListView.Columns(2).HeaderText = "ProviderId"
                dgListView.Columns(3).HeaderText = "User Name"
                dgListView.Columns(4).HeaderText = "First Name"
                dgListView.Columns(5).HeaderText = "Middle Name"
                dgListView.Columns(6).HeaderText = "Last Name"
                dgListView.Columns(7).HeaderText = "Email"
                dgListView.Columns(8).HeaderText = "ExchangeLogin"

                dgListView.Columns(0).Visible = True
                dgListView.Columns(1).Visible = False
                dgListView.Columns(2).Visible = False
                dgListView.Columns(3).Visible = True
                dgListView.Columns(4).Visible = True
                dgListView.Columns(5).Visible = True
                dgListView.Columns(6).Visible = True
                dgListView.Columns(7).Visible = True
                dgListView.Columns(8).Visible = False

                Dim _width As Integer = (_thiswidth - 17) / 7

                ' added by sandip dhakane 20100721 for  resize the grid for display full e-mail id in grid
                ' dgListView.Columns(0).Width = _width * 1
                'dgListView.Columns(2).Width = _width * 1
                'dgListView.Columns(3).Width = CInt((_width * 1.5))
                'dgListView.Columns(4).Width = CInt((_width * 1.5))
                'dgListView.Columns(5).Width = CInt((_width * 1.5))
                'dgListView.Columns(6).Width = _width * 1

                dgListView.Columns(0).Width = 45
                dgListView.Columns(2).Width = _width * 1
                dgListView.Columns(3).Width = CInt((_width * 1))
                dgListView.Columns(4).Width = CInt((_width * 1.25))
                dgListView.Columns(5).Width = CInt((_width * 1.25))
                dgListView.Columns(6).Width = _width * 1.5
                ' end code

                If _groupID <> 0 Then
                    txtGroupName.Text = _groupname
                    Dim dt As DataTable = Nothing
                    dt = GetGroupToModify(_groupID)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For k As Integer = 0 To dgListView.RowCount - 1

                                If Convert.ToInt64(dgListView(1, k).Value) = Convert.ToInt64(dt.Rows(i)("nUserID")) Then
                                    dgListView(0, k).Value = 1
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If
                dgListView.Columns(0).ReadOnly = False
                dgListView.Columns(1).ReadOnly = True
                dgListView.Columns(2).ReadOnly = True
                dgListView.Columns(3).ReadOnly = True
                dgListView.Columns(4).ReadOnly = True
                dgListView.Columns(5).ReadOnly = True
                dgListView.Columns(6).ReadOnly = True
                dgListView.Columns(7).ReadOnly = True
                dgListView.Columns(8).ReadOnly = True
            End If
        End If

    End Sub

    'Add   each user  of the  group to  Detail table 
    Private Sub Save_Detail_new()
        Try
            For i As Integer = 0 To dgListView.RowCount - 1
                If Convert.ToBoolean(dgListView.Rows(i).Cells(0).Value) = True Then
                    Dim UserID As Int64 = Convert.ToInt64(dgListView(1, i).Value)
                    Dim UserName As String = Convert.ToString(dgListView(3, i).Value)
                    AddDetails(_groupID, UserID, UserName)
                End If
            Next
        Catch ex As Exception
            Dim _sErrorMessage As String = ex.Message
        End Try
    End Sub

    ''Select all users
    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try

            SelectALL(chkSelectAll.Checked)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SelectALL(ByVal _Select As Boolean)
        For i As Integer = 0 To dgListView.RowCount - 1
            dgListView(0, i).Value = _Select
        Next
    End Sub

    ''Search Users 
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Dim dv As DataView = DirectCast(dgListView.DataSource, DataView)
        dgListView.DataSource = dv

        Dim strSearch As String = txtSearch.Text.Trim()
        'Replace ',%,[,* from search string
        strSearch = strSearch.Replace("'", "").Replace("%", "").Replace("*", "").Replace("[", "")

        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
            dv.RowFilter = (dv.Table.Columns(2).ColumnName & " Like '%") + strSearch & "%'"

        Else
            dv.RowFilter = (dv.Table.Columns(2).ColumnName & " Like '") + strSearch & "%'"
        End If
        dgListView.DataSource = dv

    End Sub

#End Region

End Class