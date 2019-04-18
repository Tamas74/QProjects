Public Class frmMSTDisclosureSet
    Dim lst As New myList
    Dim oDisclosure As clsDisclosureMgmt
    Private blnIsModify As Boolean
    Public Property IsModify() As Boolean
        Get
            Return blnIsModify
        End Get
        Set(ByVal value As Boolean)
            blnIsModify = value
        End Set
    End Property
    Private m_DisclosureSetId As Int64

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_DisclosureSetId = 0
    End Sub
    Public Sub New(ByVal nDisclosureSetId As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        m_DisclosureSetId = nDisclosureSetId

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub SaveSet()
        Try
            If txtCategoryName.Text.Trim() = "" Then
                MessageBox.Show("Please enter Disclosure Set name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtCategoryName.Focus()
                Exit Sub
            End If
            oDisclosure = New clsDisclosureMgmt
            Dim strResult As String
            If m_DisclosureSetId = 0 And blnIsModify = False Then
                strResult = oDisclosure.IsDuplicateDisclosureSet(txtCategoryName.Text.Trim())
            Else
                strResult = oDisclosure.IsDuplicateDisclosureSet(txtCategoryName.Text.Trim(), m_DisclosureSetId)

            End If
            If strResult <> "0" Then
                MessageBox.Show("Disclosure Set name already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim strAssociation As String = ""
            For Each mynode As TreeNode In trvSet.Nodes

                If mynode.Checked Then
                    If strAssociation <> "" Then
                        strAssociation &= ", "
                    End If
                    strAssociation &= mynode.Text
                End If
            Next

            m_DisclosureSetId = oDisclosure.SaveDisclosureSet(m_DisclosureSetId, txtCategoryName.Text.Trim(), strAssociation)
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmMSTDisclosureSet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FillDislosureInfo()
        If m_DisclosureSetId <> 0 Then
            oDisclosure = New clsDisclosureMgmt
            Dim strType As String()
            Dim strAssociation As String = oDisclosure.GetDisclosureAssociation(m_DisclosureSetId)
            If strAssociation <> "" Then
                strType = strAssociation.Split(",")
                For Each mynode As TreeNode In trvSet.Nodes
                    For cnt As Int32 = 0 To strType.Length - 1
                        If mynode.Text.Trim = strType(cnt).Trim Then
                            mynode.Checked = True
                        End If
                    Next
                Next
            End If
           
        End If
    End Sub
    Private Sub FillDislosureInfo()

        trvSet.Nodes.Clear()
        Dim oChildNode As TreeNode
        For i As Int32 = 1 To [Enum].GetValues(GetType(DisclosureSet)).Length - 1
            oChildNode = New TreeNode
            oChildNode.Tag = [Enum].GetValues(GetType(DisclosureSet)).GetValue(i)
            oChildNode.Text = DisclosureSetNames([Enum].GetValues(GetType(DisclosureSet)).GetValue(i))
            trvSet.Nodes.Add(oChildNode)
            oChildNode = Nothing
        Next
    End Sub

    
    Private Sub tlsDisclosureSet_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDisclosureSet.ItemClicked
        Select Case e.ClickedItem.Tag

            Case "Save"
                SaveSet()
            Case "Close"
                Me.Close()
        End Select


    End Sub
End Class