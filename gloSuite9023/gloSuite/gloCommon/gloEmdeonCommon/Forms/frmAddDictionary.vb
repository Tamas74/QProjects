Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Windows.Forms

Public Class frmAddDictionary
    Private oDB As gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    Private dt As DataTable

    ''Column used in Flexgrid
    Private Col_Select As Integer = 0
    Private Col_ID As Integer = 1
    Private Col_sFieldName As Integer = 2
    Private col_Hidden_TableName As Integer = 3
    Private Col_sTableName As Integer = 4
    Private Col_sCaption As Integer = 5
    Private Col_sTableCaption As Integer = 6
    Private Col_TableNamecaption As Integer = 7
    Private Col_Count As Integer = 8

    'Dim cmnu As ContextMenu
    Dim flag As Boolean
    'variable used for arraylist of data dictionary fields added
    Dim ArrlistDataDictionary As New ArrayList

    Private Sub frmAddDictionary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Fill_DataDictionary()
    End Sub

  

    Private Sub tlsDictionary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDictionary.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Ok"
                AddDataFields_new()
            Case "Cancel"
                Me.DialogResult = Windows.Forms.DialogResult.No
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

   
    Private Sub Fill_DataDictionary()
        Dim strSelectQry As String = "SELECT nDictionaryID, sFieldName, sTableName, sCaption, sTableCaption FROM DataDictionary_MST order by sTableCaption asc"
            dt = New DataTable
            oDB = New DataBaseLayer
            dt = oDB.GetDataTable_Query(strSelectQry)

        If Not IsNothing(dt) Then
            GloUC_trvDataDictionary.ImageIndex = 2
            GloUC_trvDataDictionary.SelectedImageIndex = 2
            GloUC_trvDataDictionary.ParentImageIndex = 1
            GloUC_trvDataDictionary.SelectedParentImageIndex = 1
            GloUC_trvDataDictionary.DataSource = dt
            GloUC_trvDataDictionary.ParentMember = "sTableCaption"
            GloUC_trvDataDictionary.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvDataDictionary.CodeMember = Convert.ToString(dt.Columns("sCaption").ColumnName)
            GloUC_trvDataDictionary.ValueMember = Convert.ToString(dt.Columns("nDictionaryID").ColumnName)
            GloUC_trvDataDictionary.DescriptionMember = Convert.ToString(dt.Columns("sFieldName").ColumnName)
            GloUC_trvDataDictionary.Name = Convert.ToString(dt.Columns("sTableName").ColumnName)
            GloUC_trvDataDictionary.FillTreeView()
            GloUC_trvDataDictionary.CollapseAll()
        End If


    End Sub
    ''Sandip Darade 20090601
    ''Add the selected data fields the exam
    Private Sub AddDataFields_new()
        Dim i As Integer = 0
        ''''code added by pradeep godse on 5/06/2009 to validate the checked fields 
        If GloUC_trvDataDictionary.SelectedNodes.Count = 0 Then
            MessageBox.Show("Select Data Dictionary fields to add in the document.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        For Each n As gloUserControlLibrary.myTreeNode In GloUC_trvDataDictionary.SelectedNodes
            Dim listData As New myList
            ''ID  
            listData.ID = n.ID
            ''Field Name
            listData.Code = n.Description
            ''Table Name
            listData.ParameterName = n.Parent.Text
            ''Caption
            listData.Description = n.Code
            ''Table Caption
            listData.Reaction = n.Parent.Text

            ArrlistDataDictionary.Add(listData)
            listData = Nothing
        Next
        'frmPatientExam.Arrlist = ArrlistDataDictionary
        Me.DialogResult = Windows.Forms.DialogResult.Yes
        '   Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    '' SUDHIR 20090630 '' 
    Private Sub GloUC_trvDataDictionary_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvDataDictionary.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try
                    If (IsNothing(GloUC_trvDataDictionary.ContextMenu) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_trvDataDictionary.ContextMenu)
                        If (IsNothing(GloUC_trvDataDictionary.ContextMenu.MenuItems) = False) Then
                            GloUC_trvDataDictionary.ContextMenu.MenuItems.Clear()
                        End If
                        GloUC_trvDataDictionary.ContextMenu.Dispose()
                        GloUC_trvDataDictionary.ContextMenu = Nothing
                    End If
                Catch ex As Exception

                End Try
                GloUC_trvDataDictionary.ContextMenu = Nothing
                Dim oContext As New ContextMenu
                Dim oContextItem As New MenuItem
                oContextItem.Text = "Add Table Template"
                oContext.MenuItems.Add(oContextItem)
                GloUC_trvDataDictionary.ContextMenu = oContext
                'AddHandler oContextItem.Click, AddressOf OnTableTemplate_Click
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub OnTableTemplate_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim ofrm As New frmMSTTableTemplate
    '        ofrm.ShowDialog(Me)
    '        Fill_DataDictionary()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    '' END SUDHIR '' 
    ''' <summary>
    ''' Property procedure to get IncludeCaption check box is checked or not
    ''' </summary>
    ''' <value></value>
    ''' <returns>true-Include Caption and false-Do not Include Caption</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IncludeCaption()
        Get
            If chckCaption.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ''' <summary>
    ''' Returns array list of liquid link fields for add in document
    ''' </summary>
    ''' <value></value>
    ''' <returns>ArrlistDataDictionary of type Array List</returns>
    ''' <remarks>Added by dipak 20091001</remarks>
    Public ReadOnly Property GetArrlistDataDictionary()
        Get
            Return ArrlistDataDictionary
        End Get
    End Property
End Class



