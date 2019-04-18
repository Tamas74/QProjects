Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports gloGeneralItem
Public Class frmOBTemplates

#Region "Form Variables"

    Private _appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Private _MessageBoxCaption As String = "gloEMR Admin" 'String.Empty

#End Region '"Form Variables"

#Region "Constructors"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        ' Application MessageBox Caption
        If _appSettings("MessageBOXCaption") IsNot Nothing Then
            If _appSettings("MessageBOXCaption") <> "" Then

                _MessageBoxCaption = Convert.ToString(_appSettings("MessageBOXCaption"))
            Else
                _MessageBoxCaption = "gloEMR Admin"
            End If
        End If
      

    End Sub

#End Region '"Constructors"

#Region "Form Events"

    Private Sub frmOBTemplates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim _dtTemplate As New DataTable()
        Dim oclsSettings As New clsSettings()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.SuspendLayout()

            _dtTemplate = oclsSettings.GetOBTemplatesList()

            If Not IsNothing(_dtTemplate) Then

                GloUC_trvTemplate.DataSource = _dtTemplate
                GloUC_trvTemplate.ParentMember = "sCategoryName"
                GloUC_trvTemplate.DisplayType = gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvTemplate.CodeMember = Convert.ToString(_dtTemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.ValueMember = Convert.ToString(_dtTemplate.Columns("nTemplateID").ColumnName)
                GloUC_trvTemplate.DescriptionMember = Convert.ToString(_dtTemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.Name = Convert.ToString(_dtTemplate.Columns("sTemplateName").ColumnName)
                GloUC_trvTemplate.ParentImageIndex = 1
                GloUC_trvTemplate.SelectedParentImageIndex = 1
                'if template count goes more than 1000 then exception will shown 
                'So we set the maxnodes limit as per our requirement.
                GloUC_trvTemplate.MaximumNodes = _dtTemplate.Rows.Count
                GloUC_trvTemplate.FillTreeView()
                GloUC_trvTemplate.ExpandAll()

            End If
            FillAssociation()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            oclsSettings = Nothing
            Me.Cursor = Cursors.Default
            Me.ResumeLayout()
        End Try

    End Sub

#End Region '"Form Events"

#Region "Form Control Events"

    Private Sub tlb_ExcludedTemplate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlb_OBTemplates.ItemClicked

        Try
            Select Case e.ClickedItem.Tag
                Case "SelectAll"
                    SelectAll()

                Case "DeselectAll"
                    ClearAll()

                Case "SavenClose"
                    SaveOBTemplates()



                Case "Close"
                    DialogResult = Windows.Forms.DialogResult.Cancel
                    Me.Close()


            End Select
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

#Region "Supporting Methods"

    Private Sub SelectAll()
        Try
            Me.SuspendLayout()
            If (GloUC_trvTemplate.Nodes.Count > 0) Then
                For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                    oNode.Checked = True
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.ResumeLayout()
        End Try
    End Sub

    Private Sub ClearAll()

        Try
            Me.SuspendLayout()
            If (GloUC_trvTemplate.Nodes.Count > 0) Then
                For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                    oNode.Checked = False
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.ResumeLayout()
        End Try

    End Sub

    Private Sub FillAssociation()
        Dim dt As New DataTable
        Dim oclsSettings As New clsSettings()
        'Dim TemplateName As String
        'Dim CategoryName As String
        Dim TemplateID As Int64
        Try
            dt = oclsSettings.FillAssociation()
            If (dt.Rows.Count > 0) Then
                For Each oNode As TreeNode In GloUC_trvTemplate.Nodes
                    Dim ChkCnt As Int32 = 0
                    For Each oChild As myTreeNode In oNode.Nodes
                        If dt.Select("nTemplateId=" & oChild.ID).Length > 0 Then
                            oChild.Checked = True
                            ChkCnt = ChkCnt + 1
                        End If
                    Next

                    If (oNode.Nodes.Count = ChkCnt) Then
                        oNode.Checked = True
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub SaveOBTemplates()

        Dim ocls As New clsSettings
        Dim ArrTemplate As New Collection
        Try

            If GloUC_trvTemplate.SelectedNodes.Count <> 0 Then
                ArrTemplate.Clear()
                For Each n As myTreeNode In GloUC_trvTemplate.SelectedNodes
                    ArrTemplate.Add(n.ID)
                Next
                ocls.SaveOBTemplates(ArrTemplate)

                Me.Close()
            Else

                Dim oDialogResult As DialogResult
                oDialogResult = MessageBox.Show("You have not selected any OB template, this might remove all existing OB template(s)." & Environment.NewLine & _
                                        "Are you sure you want to continue?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)

                If (oDialogResult = Windows.Forms.DialogResult.Yes) Then
                    'Remove all associated templates
                    Using oDBLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
                        oDBLayer.Connect(False)
                        oDBLayer.Execute_Query("DELETE FROM OBTemplates")
                        oDBLayer.Disconnect()
                    End Using
                    
                    Me.Close()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            DialogResult = Windows.Forms.DialogResult.Cancel
            ex = Nothing
        Finally
            ocls = Nothing
            ArrTemplate = Nothing
        End Try

    End Sub

#End Region '"Supporting Methods"
#End Region '"Form Control Events" '


End Class