Imports System.Data.SqlClient

Public Class frmUserList
    ''Public Event SelectedUser(ByVal strusers As String)
    Private blnUsers As Boolean


    Public Sub New(ByVal ShowOnlyUsers As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        blnUsers = ShowOnlyUsers
    End Sub

    Dim _strUsers As String = ""
    Public Property Users()
        Get
            Return _strUsers
        End Get
        Set(ByVal value)
            _strUsers = value
        End Set
    End Property

    Public Function FillUserCombo() As DataTable
        Try
            Dim dt1 As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString())

            Dim strQuery As String = ""
            If blnUsers Then
                strQuery = "select sLoginName as UserName from User_MST "
            Else
                strQuery = "select sLoginName as UserName from User_MST union select isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as UserName from Provider_mst"
            End If

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd

            'Fill data table_
            sqladpt.Fill(dt1)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqladpt.Dispose()
            sqladpt = Nothing
            conn.Dispose()
            conn = Nothing
            'Return data table
            Return dt1
        Catch ex As Exception
            FillUserCombo = Nothing
            Throw ex
        End Try

    End Function
    Private Sub frmUserList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim dt As DataTable
            dt = FillUserCombo()
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Dim myNode As TreeNode
                    myNode = New TreeNode
                    myNode.Text = "Users"
                    trUserList.Nodes.Add(myNode)
                    For icnt As Int32 = 0 To dt.Rows.Count - 1
                        Dim mychildNode As New TreeNode
                        mychildNode.Text = dt.Rows(icnt)("UserName")
                        myNode.Nodes.Add(mychildNode)
                    Next
                End If
                dt.Dispose()
                dt = Nothing
            End If
            Dim myUserNode As New TreeNode
            myUserNode.Text = "Selected Drugs"
            trUsers.Nodes.Add(myUserNode)
            Dim arrUsers() As String = Split(_strUsers, "|")
            'Dim strUserName As System.Text.StringBuilder
            If arrUsers.Length > 0 Then
                For icnt As Int32 = 0 To arrUsers.Length - 1
                    Dim mynode As New TreeNode
                    mynode.Text = arrUsers(icnt)
                    myUserNode.Nodes.Add(mynode)
                Next
            End If
            trUserList.ExpandAll()
            trUsers.ExpandAll()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub trUserList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trUserList.DoubleClick
        Try
            Dim blnExists As Boolean = False
            If Not IsNothing(trUserList.SelectedNode) Then
                If Not trUserList.SelectedNode Is trUserList.Nodes.Item(0) Then
                    For Each mynode As TreeNode In trUsers.Nodes.Item(0).Nodes
                        If trUserList.SelectedNode.Text.ToUpper = mynode.Text.ToUpper Then
                            blnExists = True
                            Exit For
                        End If
                    Next
                    If (blnExists = False) Then
                        Dim myUserNode As New TreeNode
                        myUserNode.Text = trUserList.SelectedNode.Text
                        trUsers.Nodes.Item(0).Nodes.Add(myUserNode)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tsUsers_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsUsers.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                Dim strUsers As New System.Text.StringBuilder
                Dim icnt As Int32
                icnt = 0
                For Each mynode As TreeNode In trUsers.Nodes.Item(0).Nodes
                    If icnt > 0 Then
                        strUsers.Append("|")
                    End If
                    strUsers.Append(mynode.Text)
                    icnt = icnt + 1
                Next
                _strUsers = strUsers.ToString()
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            Case "Close"
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub
End Class