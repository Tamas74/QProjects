Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient


Public Class gloUC_TemplateTreeControl
    Dim Con As SqlConnection
    Public Event Treeview_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs, ByVal sFilename As String)
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtsearch.ResetText()
        txtsearch.Focus()
    End Sub
    Private Sub txtsearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvMain.Select()
        Else
            trvMain.SelectedNode = trvMain.Nodes.Item(0)
        End If
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try
            If Trim(txtsearch.Text) <> "" Then
                Dim mynode As TreeNode
                For Each myParentnode As TreeNode In trvMain.Nodes
                    For Each mynode In myParentnode.Nodes
                        Dim mychildnode As TreeNode
                        For Each mychildnode In mynode.Nodes
                            If Trim(mychildnode.Text) <> "" Then
                                Dim str As String
                                str = UCase(Trim(mychildnode.Text))
                                If str.Contains(UCase(txtsearch.Text.Trim)) Then

                                    mychildnode.Parent.ExpandAll()
                                    trvMain.SelectedNode = trvMain.SelectedNode.LastNode
                                    trvMain.SelectedNode = mychildnode
                                    txtsearch.Focus()
                                    Exit Sub
                                End If
                            End If
                        Next
                    Next
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UC_TemplateTreeControl, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvMain_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvMain.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Dim obje As System.EventArgs = Nothing
            Call trvMain_NodeMouseDoubleClick(sender, obje)
        End If
    End Sub
    Private Sub trvMain_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseClick
        Try
            trvMain.SelectedNode = e.Node
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UC_TemplateTreeControl, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Dim ObjWord As Object
    Public Property ObjClsWord() As Object
        Get
            Return ObjWord
        End Get
        Set(ByVal value As Object)
            ObjWord = value
        End Set
    End Property
    Dim isExpandConsent As Boolean = False
    Public Property ExpandConsent() As Object
        Get
            Return isExpandConsent
        End Get
        Set(ByVal value As Object)
            isExpandConsent = value
        End Set
    End Property
    Dim objCriteria As Object
    Public Property DocCriteria() As Object
        Get
            Return objCriteria
        End Get
        Set(ByVal value As Object)
            objCriteria = value
        End Set
    End Property
    Dim _ProviderID As Int64
    Public Property ProviderId() As Int64
        Get
            Return _ProviderID
        End Get
        Set(value As Int64)
            _ProviderID = value
        End Set
    End Property



    Private Sub trvMain_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseDoubleClick
        If trvMain.SelectedNode.Text = "My Templates" Then
            Fill_ExamTemplates(_ProviderID)
        ElseIf trvMain.SelectedNode.Text = "All Templates" Then
            Fill_ExamTemplates(0)
        End If
        Dim strTemplateFilePath As String
        Dim nTemplateId As Int64 = trvMain.SelectedNode.Tag
        strTemplateFilePath = GetTemplateNote(nTemplateId)
        If IO.File.Exists(strTemplateFilePath) = True Then
            RaiseEvent Treeview_NodeMouseDoubleClick(sender, e, strTemplateFilePath)
        End If
    End Sub
    Private Function GetTemplateNote(ByVal Id As Int64) As String

        Try
            Dim strFileName As String
            objCriteria.DocCategory = enumDocCategory.Template
            objCriteria.PrimaryID = Id
            ObjWord.DocumentCriteria = objCriteria
            strFileName = ObjWord.RetrieveDocumentFile()
            Return strFileName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UC_TemplateTreeControl, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Public Sub setFirstTreeNode()
        Dim MyNode As TreeNode
        MyNode = New TreeNode
        MyNode.Text = "My Templates"
        MyNode.ImageIndex = 0
        MyNode.SelectedImageIndex = 0
        trvMain.Nodes.Add(MyNode)

        MyNode = New TreeNode
        MyNode.Text = "All Templates"
        MyNode.ImageIndex = 1
        MyNode.SelectedImageIndex = 1
        trvMain.Nodes.Add(MyNode)
    End Sub
    Public Sub InitiliseControlParameter(strConnectionString As String)
        Con = New SqlConnection(strConnectionString)
        setFirstTreeNode()
    End Sub
    Public Sub FinalizeControlParameter(strConnectionString As String)
        If (IsNothing(Con) = False) Then '
            Con.Dispose()
            Con = Nothing
        End If
    End Sub
    Public Sub Fill_ExamTemplates(ByVal nDoctorId As Int64)
        Dim nTableCount As Integer
        Dim nTemplateCount As Integer
        Dim trvRootNode As TreeNode
        trvRootNode = New TreeNode
        Dim trvCategoryNode As TreeNode = Nothing
        Dim trvTemplateNode As TreeNode = Nothing

        Dim dtCategory As DataTable
        Dim dtTemplates As DataTable
        dtCategory = Fill_TemplatesCategory()
        trvMain.BeginUpdate()
        If nDoctorId = 0 Then
            trvRootNode = trvMain.Nodes(1)
        Else
            trvRootNode = trvMain.Nodes(0)
        End If


        Dim strCategoryID As String = String.Empty

        For nTableCount = 0 To dtCategory.Rows.Count - 1

            If nTableCount <> dtCategory.Rows.Count - 1 Then
                strCategoryID = strCategoryID & dtCategory.Rows(nTableCount).Item(0) & ","
            Else
                strCategoryID = strCategoryID & dtCategory.Rows(nTableCount).Item(0)
            End If

        Next

        dtTemplates = Fill_ExamTemplateNames_Speed(strCategoryID, nDoctorId)

        Dim dvTemplates As New DataView(dtTemplates)

        trvRootNode.Nodes.Clear()

        For nTableCount = 0 To dtCategory.Rows.Count - 1
            trvCategoryNode = New TreeNode

            trvCategoryNode.Text = dtCategory.Rows(nTableCount).Item(1)
            trvCategoryNode.Tag = dtCategory.Rows(nTableCount).Item(0)
            trvCategoryNode.ImageIndex = 2
            trvCategoryNode.SelectedImageIndex = 2
            trvCategoryNode.ForeColor = Color.Maroon
          

            trvRootNode.Nodes.Add(trvCategoryNode)



            dvTemplates.RowFilter = "nCategoryID=" & dtCategory.Rows(nTableCount).Item("nCategoryID")

            Dim CategoryID As Int64 = 0
            CategoryID = dtCategory.Rows(nTableCount).Item("nCategoryID")
            For nTemplateCount = 0 To dvTemplates.Count - 1
                trvTemplateNode = New TreeNode
                trvTemplateNode.Text = dvTemplates.Item(nTemplateCount)("sTemplateName")
                trvTemplateNode.ImageIndex = 3
                trvTemplateNode.SelectedImageIndex = 3
                trvTemplateNode.Tag = dvTemplates.Item(nTemplateCount)("nTemplateID")

                trvCategoryNode.Nodes.Add(trvTemplateNode)
                trvTemplateNode.ForeColor = Color.Blue

            Next

            If (trvCategoryNode.Text.Trim() = "Consent Form") And isExpandConsent Then
                trvCategoryNode.Expand()
            End If
            
        Next
        If IsNothing(trvCategoryNode) = False Then trvCategoryNode.EnsureVisible()

        trvMain.EndUpdate()

        If Not IsNothing(dtCategory) Then
            dtCategory.Dispose()
            dtCategory = Nothing
        End If
        If Not IsNothing(dtTemplates) Then
            dtTemplates.Dispose()
            dtTemplates = Nothing
        End If
        If Not IsNothing(dvTemplates) Then
            dvTemplates.Dispose()
            dvTemplates = Nothing
        End If
    End Sub
    Public Function Fill_ExamTemplateNames_Speed(ByVal sCategoryID As String, ByVal nProviderID As Long) As DataTable

        Dim dtMain As New DataTable
        Dim daMain As New SqlDataAdapter
        Dim cmdMain As New SqlCommand("gsp_ViewTemplateGallery_MST_Speed", Con)
        Dim sqlParam1 As SqlParameter = Nothing
        Try

            cmdMain.CommandType = CommandType.StoredProcedure
            sqlParam1 = cmdMain.Parameters.Add("@ID", SqlDbType.VarChar)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = sCategoryID

            If nProviderID > 0 Then
                sqlParam1 = cmdMain.Parameters.Add("@ProviderID", SqlDbType.BigInt)
                sqlParam1.Direction = ParameterDirection.Input
                sqlParam1.Value = nProviderID
            End If
            daMain.SelectCommand = cmdMain
            daMain.Fill(dtMain)
            Con.Close()
            Return dtMain
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UC_TemplateTreeControl, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            If Not IsNothing(sqlParam1) Then
                sqlParam1 = Nothing
            End If

            If Not IsNothing(cmdMain) Then  'Obj Disposed by Mitesh
                cmdMain.Parameters.Clear()
                cmdMain.Dispose()
                cmdMain = Nothing
            End If
            If Not IsNothing(daMain) Then
                daMain.Dispose()
                daMain = Nothing
            End If
            'If Not IsNothing(dtMain) Then
            '    dtMain.Dispose()
            '    dtMain = Nothing
            'End If
        End Try

    End Function
    Public Function Fill_ExamTemplateNames(ByVal nCategoryID As Long, ByVal nProviderID As Long) As DataTable
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As New SqlCommand("gsp_ViewTemplateGallery_MST", Con)
        Dim sqlParam1 As SqlParameter = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure


            sqlParam1 = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = nCategoryID 'dtCategory.Rows(nTableCount)(0)
            If nProviderID > 0 Then
                sqlParam1 = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
                sqlParam1.Direction = ParameterDirection.Input
                sqlParam1.Value = nProviderID 'dtCategory.Rows(nTableCount)(0)
            End If
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            Con.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(sqlParam1) Then
                sqlParam1 = Nothing
            End If
            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try

    End Function
    Public Function Fill_TemplatesCategory() As DataTable
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim cmd As New SqlCommand("gsp_FillCategory_MST", Con)
        Dim sqlParam As SqlParameter = Nothing
        Try

            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "Template"
            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            Con.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UC_TemplateTreeControl, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try

    End Function
   
    Public Enum enumDocCategory
        Template = 1
        Exam = 2
        Referrals = 3
        Orders = 4
        Message = 5
        Others = 6
        Addendum = 7
    End Enum
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
