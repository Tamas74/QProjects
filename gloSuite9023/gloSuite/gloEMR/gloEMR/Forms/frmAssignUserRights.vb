Imports System.Data.SqlClient

Public Class frmAssignUserRights

    Private Sub frmAssignUserRights_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FillAvailableUsers()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Public Function FillAvailableUsers()
        Try
            Dim dtProvider As New DataTable
            Dim sqladpt As SqlDataAdapter
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString())

            Dim strQuery As String = ""
            strQuery = "SELECT nUserID,sLoginName,dbo.GET_NAME(sFirstName,sMiddleName,sLastName) AS sName FROM User_MST"
            Dim cmd As SqlCommand = New SqlCommand(strQuery, conn)
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            sqladpt.Fill(dtProvider)


            If dtProvider IsNot Nothing Then
                Dim oNode As myTreeNode
                For iRow As Integer = 0 To dtProvider.Rows.Count - 1
                    oNode = New myTreeNode
                    oNode.Key = dtProvider.Rows(iRow)("nUserID")
                    'oNode.Text = dtProvider.Rows(iRow)("sLoginName") & " : " & dtProvider.Rows(iRow)("sDEA")
                    oNode.Text = dtProvider.Rows(iRow)("sLoginName")
                    oNode.TemplateResult = dtProvider.Rows(iRow)("sLoginName")
                    oNode.Tag = dtProvider.Rows(iRow)("nUserID")
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0
                    trvAvailableUsers.Nodes.Add(oNode)

                Next
                dtProvider.Dispose()
                dtProvider = Nothing

            End If

            trvAvailableUsers.ExpandAll()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqladpt IsNot Nothing Then

                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
        Return Nothing
    End Function

    Private Sub btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            trvAvailableUsers_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
        Try
            trvAssociatedUsers_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub trvAssociatedUsers_DoubleClick(sender As System.Object, e As System.EventArgs) Handles trvAssociatedUsers.DoubleClick

        Try
            trvAssociatedUsers.SelectedNode.Remove()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub trvAvailableUsers_DoubleClick(sender As System.Object, e As System.EventArgs) Handles trvAvailableUsers.DoubleClick
        Try

            Dim oNode As myTreeNode = CType(trvAvailableUsers.SelectedNode, myTreeNode)
            Dim jNode As myTreeNode = CType(trvAssociatedUsers.SelectedNode, myTreeNode)
            If oNode IsNot Nothing Then

                If trvAssociatedUsers.Nodes.Count > 0 Then
                    For iNode As Integer = 0 To trvAssociatedUsers.Nodes.Count - 1
                        If CType(trvAssociatedUsers.Nodes(iNode), myTreeNode).Key = CType(oNode, myTreeNode).Key Then
                            Exit Sub
                        End If
                    Next
                End If

                Dim oNodeToAdd As New myTreeNode
                oNodeToAdd.Key = oNode.Key
                oNodeToAdd.TemplateResult = oNode.TemplateResult.ToString
                oNodeToAdd.Text = oNode.Text
                oNodeToAdd.Tag = oNode.Tag
                oNodeToAdd.ImageIndex = 0
                oNodeToAdd.SelectedImageIndex = 0
                trvAssociatedUsers.Nodes.Add(oNodeToAdd)

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub

    Private Sub tlsbtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnCancel.Click
        Me.Close()
    End Sub

End Class