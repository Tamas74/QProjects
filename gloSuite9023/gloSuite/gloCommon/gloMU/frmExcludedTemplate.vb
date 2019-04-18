Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports gloGeneralItem
Public Class frmExcludedTemplate
    Private _databaseConnectionString As String = String.Empty
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim Con As SqlConnection = New SqlConnection(_databaseConnectionString)
    Private _MessageBoxCaption As String = String.Empty
    'Dim ArrCategory As New Collection

    Private Sub tlb_ExcludedTemplate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlb_ExcludedTemplate.ItemClicked
        Dim oDB As New gloDatabaseLayer.DBLayer(_databaseConnectionString)
        Try
            Select Case e.ClickedItem.Tag
                Case "SelectAll"
                    SelectAll()

                Case "DeselectAll"
                    ClearAll()

                Case "SavenClose"
                    SaveExcludedTmplate()

                    Me.Close()

                Case "Close"
                    DialogResult = Windows.Forms.DialogResult.Cancel
                    Me.Close()


            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmExcludedTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim _dtemplate As New DataTable()
        Dim clsMUMeasure As New cls_MU_Measures()
        Try
            _dtemplate = clsMUMeasure.GetTemplateName()
           

            If Not IsNothing(_dtemplate) Then

                GloUC_trvTemplate.DataSource = _dtemplate
                GloUC_trvTemplate.ParentMember = "sCategoryName"
                GloUC_trvTemplate.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvTemplate.CodeMember = Convert.ToString(_dtemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.ValueMember = Convert.ToString(_dtemplate.Columns("nId").ColumnName)
                GloUC_trvTemplate.DescriptionMember = Convert.ToString(_dtemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.Name = Convert.ToString(_dtemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.ParentImageIndex = 1
                GloUC_trvTemplate.SelectedParentImageIndex = 1
                'Bug #46228: 00000403 : MU Dashboard 
                'if template count goes more than 1000 then exception will shown on MU Dashboard.
                'So we set the maxnodes limit as per our requirement.
                GloUC_trvTemplate.MaximumNodes = _dtemplate.Rows.Count
                GloUC_trvTemplate.FillTreeView()
                GloUC_trvTemplate.ExpandAll()
            End If
            FillAssociation()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub FillAssociation()
        Dim dt As New DataTable()
        Dim ocls As New cls_MU_Measures
        Dim TemplateName As String
        Dim CategoryName As String
        Try
            dt = ocls.FillAssociation()
            If (dt.Rows.Count > 0) Then
                For i As Int64 = 0 To dt.Rows.Count - 1
                    TemplateName = dt.Rows(i)("sTemplateName").ToString()
                    CategoryName = dt.Rows(i)("sCategoryname").ToString()

                    For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                        For Each oChild As TreeNode In oNode.Nodes
                            If (oChild.Text.Trim() = TemplateName And oChild.Parent.Text.Trim() = CategoryName) Then
                                oChild.Checked = True
                            End If
                        Next
                    Next
                Next
            End If
                
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SelectAll()
        Try
            If (GloUC_trvTemplate.Nodes.Count > 0) Then
                For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                    oNode.Checked = True
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ClearAll()

        Try
            If (GloUC_trvTemplate.Nodes.Count > 0) Then
                For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                    oNode.Checked = False
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trvTemplate_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvTemplate.AfterCheck

        Try
            RemoveHandler trvTemplate.AfterCheck, AddressOf trvTemplate_AfterCheck
            If (e.Node.Level = 0) Then

                For Each oChildNode As TreeNode In e.Node.Nodes
                    oChildNode.Checked = e.Node.Checked
                Next
            ElseIf (e.Node.Level = 1) Then
                Dim _CheckValue As Boolean = True
                If e.Node.Checked = True Then
                    For Each oChildNode As TreeNode In e.Node.Parent.Nodes
                        If (oChildNode.Checked = False) Then
                            _CheckValue = False
                            Exit For
                        End If
                    Next
                Else
                    _CheckValue = False
                End If
                e.Node.Parent.Checked = _CheckValue
            End If
           
            AddHandler trvTemplate.AfterCheck, AddressOf trvTemplate_AfterCheck
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub SaveExcludedTmplate()

        Dim ocls As New cls_MU_Measures
        Dim ArrTemplate As New Collection
        Try
            
            ArrTemplate.Clear()
            Dim item As gloItem

            For Each n As gloUserControlLibrary.myTreeNode In GloUC_trvTemplate.SelectedNodes
                item = New gloItem
                item.Code = n.Parent.Text
                item.Description = n.Text
                ArrTemplate.Add(item)
                item.Dispose()
            Next

            ocls.SaveExcludedTemplate(ArrTemplate, Convert.ToInt64(appSettings("ClinicID")))
            DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = Windows.Forms.DialogResult.Cancel

        Finally
            ocls = Nothing
            ArrTemplate = Nothing
        End Try

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        If appSettings("MessageBOXCaption") IsNot Nothing Then

            If appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR"
            End If
        End If
        If appSettings("DataBaseConnectionString") IsNot Nothing Then
            If appSettings("DataBaseConnectionString") <> "" Then


                _databaseConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
                Con = New SqlConnection(_databaseConnectionString)
            End If
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class