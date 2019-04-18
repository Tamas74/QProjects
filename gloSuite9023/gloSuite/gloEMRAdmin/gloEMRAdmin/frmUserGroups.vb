Imports System.Data.SqlClient
'Form Added  By  Sandip Darade To Set User Groups 
Public Class frmUserGroups
    Dim blnckSelect As Boolean = True

#Region "Variables & Properties"
    Dim _groupID As Int64 = 0
    Dim _groupname As String = ""
    Dim _nClinicID As Int64 = 1
    Private lstNuserId As New ArrayList
    Private dtUser As New DataTable()
    Private strUserID As String = String.Empty
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

    Private Sub frmUserGroups_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(dtUser) Then
            dtUser.Dispose()
            dtUser = Nothing
        End If
    End Sub

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
                'Developer: Mitesh Patel
                'Date:24-Dec-2011'
                'PRD: Lab Usability Admin Setting
                If dtUser.Rows.Count <> 0 Then
                    Dim strtmp As String = validate_Labusers()
                    If strtmp <> "" Then
                        Dim oResult As DialogResult = MessageBox.Show(strtmp, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If oResult = Windows.Forms.DialogResult.Yes Then
                            Dim oUserGroups As New clsUserGroups
                            oUserGroups.Delete_LabuserTasks(_groupID, strUserID)
                            oUserGroups = Nothing
                            strUserID = String.Empty
                        Else
                            strUserID = String.Empty
                            Exit Sub
                        End If
                    End If

                End If
                AddUserGroups(txtGroupName.Text.Trim(), _groupID) 'Save group

                Save_Detail_new() 'Save user info to detail table
                Me.Close()
            Else
                MessageBox.Show("Group name already in use. Please enter unique group name. ", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Con.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As New SqlCommand("gsp_INUP_UserGroups", Con)
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
        Con = Nothing
        cmd = Nothing
        Return True
    End Function


    'Add  user  of the  group to  Detail table
    Private Function AddDetails(ByVal GroupID As Int64, ByVal UserID As Int64, ByVal UserName As String) As Boolean

        Dim Con As New SqlConnection
        Con.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim cmd As New SqlCommand("[gSP_INUP_UserGroupsDetail]", Con)
        cmd.CommandType = CommandType.StoredProcedure
        Dim sqlParam As SqlParameter
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
        Con.Open()
        cmd.ExecuteNonQuery()
        Con.Close()
        Con = Nothing
        cmd = Nothing
        Return True
    End Function


    'Check if group with same name exists
    Public Function CheckGroupExists(ByVal GroupName As String, Optional ByVal nGroupID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCon = Nothing
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
            MessageBox.Show("Please enter a name for the group. ", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show("Please select users to group. ", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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

        Catch ex As SqlException

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
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
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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

        Catch ex As SqlException

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
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
                dgListView.Columns(0).Width = _width * 1
                dgListView.Columns(2).Width = _width * 1
                dgListView.Columns(3).Width = CInt((_width * 1.5))
                dgListView.Columns(4).Width = CInt((_width * 1.5))
                dgListView.Columns(5).Width = CInt((_width * 1.5))
                dgListView.Columns(6).Width = _width * 1


                If _groupID <> 0 Then
                    txtGroupName.Text = _groupname
                    Dim dt As DataTable = Nothing
                    dt = GetGroupToModify(_groupID)
                    ''
                    dtUser = dt
                    ''
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
                    If dt.Rows.Count < dgListView.Rows.Count Then '' chetan added for checking ,unchecking select all checkbox
                        blnckSelect = False
                        chkSelectAll.Checked = False
                        blnckSelect = True
                    Else
                        blnckSelect = False
                        chkSelectAll.Checked = True
                        blnckSelect = True

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
            If blnckSelect = True Then '' chetan added condition on 13 nov 2010  
                dgListView.EndEdit()
                dgListView.RefreshEdit()

                SelectALL(chkSelectAll.Checked)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SelectALL(ByVal _Select As Boolean)

        Dim ind As Integer = -1
        For i As Integer = 0 To dgListView.RowCount - 1

            dgListView(0, i).Value = _Select

        Next

        dgListView.RefreshEdit()
    End Sub

    ''Search Users 
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged

        Dim dv As DataView = DirectCast(dgListView.DataSource, DataView)
        dgListView.DataSource = dv

        Dim strSearch As String = txtSearch.Text.Trim()
        'Replace ',%,[,* from search string
        strSearch = strSearch.Replace("'", "").Replace("%", "").Replace("*", "").Replace("[", "")

        If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
            dv.RowFilter = (dv.Table.Columns(3).ColumnName & " Like '%") + strSearch & "%'"

        Else
            dv.RowFilter = (dv.Table.Columns(3).ColumnName & " Like '") + strSearch & "%'"
        End If
        dgListView.DataSource = dv

    End Sub

#End Region

    Private Sub dgListView_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgListView.CellLeave


    End Sub

    Private Sub dgListView_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgListView.DoubleClick

    End Sub

    Private Sub dgListView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgListView.Click
        Try
            '' chetan added on 13-nov 2010 for checking unchecking select all check box  

            dgListView.EndEdit()
            dgListView.RefreshEdit()
            Dim dc As DataGridViewCell = dgListView.SelectedCells.Item(0)
            If dgListView.CurrentCell.ColumnIndex = 0 Then


                If Not IsNothing(dc.Value) Then
                    If dc.Value = False Then
                        For i As Integer = 0 To dgListView.RowCount - 1

                            If dgListView(0, i).Value = False And i <> dc.RowIndex Then
                                blnckSelect = False
                                chkSelectAll.Checked = False
                                blnckSelect = True

                                Exit Sub
                            End If

                        Next
                        blnckSelect = False
                        chkSelectAll.Checked = True
                        blnckSelect = True
                    Else

                        blnckSelect = False
                        chkSelectAll.Checked = False
                        blnckSelect = True
                    End If
                Else

                    For i As Integer = 0 To dgListView.RowCount - 1

                        If dgListView(0, i).Value = False And i <> dc.RowIndex Then
                            blnckSelect = False
                            chkSelectAll.Checked = False
                            blnckSelect = True

                            Exit Sub
                        End If

                    Next
                    blnckSelect = False
                    chkSelectAll.Checked = True
                    blnckSelect = True
                End If


            End If

            '' chetan added on 13-nov 2010 for checking unchecking select all check box  

        Catch ex As Exception
            blnckSelect = False
            chkSelectAll.Checked = False
            blnckSelect = True
        End Try
    End Sub
   

    'Private Function dtLabUsers() As DataTable

    '    Try
    '        Dim colProviderID As New DataColumn("nProviderID")
    '        colProviderID.DataType = GetType(Long)
    '        Dim colProviderName As New DataColumn("sProviderName")
    '        colProviderName.DataType = GetType(String)
    '        Dim colGroupID As New DataColumn("nGroupID")
    '        colGroupID.DataType = GetType(Long)
    '        Dim colUserID As New DataColumn("nUserID")
    '        colUserID.DataType = GetType(Long)
    '        Dim colUserName As New DataColumn("sUserName")
    '        colUserName.DataType = GetType(String)
    '        Dim colFlg As New DataColumn("nDel")
    '        colFlg.DataType = GetType(Integer)

    '        dtUser.Columns.Add(colProviderID)
    '        dtUser.Columns.Add(colProviderName)
    '        dtUser.Columns.Add(colGroupID)
    '        dtUser.Columns.Add(colUserID)
    '        dtUser.Columns.Add(colUserName)
    '        dtUser.Columns.Add(colFlg)

    '        Return dtUser
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        'If Not IsNothing(dtUser) Then
    '        '    dtUser.Dispose()
    '        '    dtUser = Nothing
    '        'End If
    '    End Try
    'End Function



    'Developer: Mitesh Patel
    'Date:24-Dec-2011'
    'PRD: Lab Usability Admin Setting

    Private Function validate_Labusers() As String
        Dim sMessage As String = String.Empty
        Dim str As String = String.Empty
        Try
            Dim sUid As String = String.Empty
            For i As Integer = 0 To dgListView.RowCount - 1
                If Convert.ToBoolean(dgListView.Rows(i).Cells(0).Value) = True Then
                    sUid = sUid & dgListView.Rows(i).Cells(1).Value.ToString() & ","
                End If
            Next
            If sUid <> "" Then
                sUid = sUid.Remove((sUid.Length - 1), 1)
            End If


            Dim sdv As DataView
            sdv = New DataView(dtUser, "nUserID Not in (" & sUid & ")", "nUserID", DataViewRowState.CurrentRows)

            strUserID = String.Empty
            For i As Integer = 0 To sdv.Count - 1
                strUserID = strUserID & sdv(i).Item("nUserID").ToString & ","
            Next

            Dim newDt As New DataTable

            If strUserID <> "" Then
                strUserID = strUserID.Remove((strUserID.Length - 1), 1)
                Dim oUserGroups As New clsUserGroups
                newDt = oUserGroups.Check_LabUserTask(_groupID.ToString(), strUserID)
                oUserGroups = Nothing
            End If


            If Not IsNothing(newDt) Then
                For i As Integer = 0 To newDt.Rows.Count - 1
                    str = str & vbCrLf & newDt.Rows(i)("sProviderName").ToString() & ":- " & newDt.Rows(i)("sUserName").ToString()
                Next
            End If


            If Not IsNothing(newDt) Then
                newDt.Dispose()
                newDt = Nothing
            End If

            If Not IsNothing(sdv) Then
                sdv.Dispose()
                sdv = Nothing
            End If

            If str <> "" Then
                sMessage = "Following users are assigned for Lab User Tasks." & vbCrLf & "Removing them from User/Groups will remove them from Lab User Tasks also. " & vbCrLf & str & vbCrLf & vbCrLf & "Do you want to continue?"
            End If

            Return sMessage
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""

        End Try
    End Function
   
End Class