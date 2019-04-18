Imports System.Collections

Public Class frmAddDropDown
    Private m_arrList As ArrayList
    Private m_Title As String
    Public Property GetDropdownItems() As ArrayList
        Get
            Return m_arrList
        End Get
        Set(ByVal value As ArrayList)
            m_arrList = value
        End Set
    End Property
    Public Property GetDropdownTitle() As String
        Get
            Return m_Title
        End Get
        Set(ByVal value As String)
            m_Title = value
        End Set
    End Property

    Private Sub frmAddDropDown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''Add default item
        If m_arrList Is Nothing Then
            lstItems.Items.Add("Choose an item")
        Else
            ''If modifying th exisitng load the drop doen items in the list
            FillDdlItems()
        End If
        ''enable the diabled buttons
        EnableBtns()
        ''If more than one items available select the first item by default
        If (lstItems.Items.Count > 0) Then
            lstItems.SelectedIndex = 0
        End If
        '' Bind the title to text box if exists
        If m_Title <> "" Then
            txtTitle.Text = m_Title
        End If
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim strValue As String = txtItem.Text.Trim
        ''check for empty text
        If strValue <> "" Then
            '' check duplicate items
            If Not IsDuplicate(strValue) Then
                strValue = txtItem.Text.Trim
                ''Add item to the list
                lstItems.Items.Add(strValue)
                ''clear the textbox and set focus
                txtItem.Text = String.Empty
                txtItem.Focus()

            Else
                MsgBox("An entry with the same name already exists - each entry must specify a unique entry", MsgBoxStyle.Exclamation, "gloEMR")
            End If
        Else
            MsgBox("Blank entry cannot be added", MsgBoxStyle.Exclamation, "gloEMR")
        End If

    End Sub

    Private Function IsDuplicate(ByVal strName As String) As Boolean

        For i As Int32 = 0 To lstItems.Items.Count - 1
            ''Check whether the same item is avilable in the list 
            If lstItems.Items(i).ToString = strName Then
                '' same value exists
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If Not IsNothing(lstItems.SelectedItem) Then
                ''Remove the selected item from the list
                lstItems.Items.Remove(lstItems.SelectedItem)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' To fill the field values in the list box
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDdlItems()
        ''Clear the list box and then add the field value to the list
        lstItems.Items.Clear()
        For _inc As Int32 = 0 To m_arrList.Count - 1
            lstItems.Items.Add(m_arrList.Item(_inc))
        Next
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Try
            ''check If item is selected
            If Not IsNothing(lstItems.SelectedItem) Then
                If lstItems.SelectedIndex <> lstItems.Items.Count - 1 Then
                    ''if the selected iten is not the last item
                    Dim intindex As Int16
                    intindex = lstItems.SelectedIndex
                    Dim str As String
                    str = lstItems.SelectedItem
                    ''get the selected item, store it and remove from the list
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex)
                    ''insert it in one position down and select it again
                    lstItems.Items.Insert(intindex + 1, str)
                    lstItems.SelectedItem = lstItems.Items.Item(intindex + 1)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Try
            If Not IsNothing(lstItems.SelectedItem) Then
                If lstItems.SelectedIndex <> 0 Then
                    ''if the selected iten is not the first item
                    Dim intindex As Int16
                    intindex = lstItems.SelectedIndex
                    Dim str As String
                    str = lstItems.SelectedItem
                    ''get the selected item, store it and remove from the list
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex)
                    ''insert it in one position up and select it again
                    lstItems.Items.Insert(intindex - 1, str)
                    lstItems.SelectedItem = lstItems.Items.Item(intindex - 1)

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddDropDown()
        ''Clear  the arraylist before adding
        If m_arrList Is Nothing Then
            m_arrList = New ArrayList
        Else
            m_arrList.Clear()
        End If
        ''Add the list items into arraylist for further purpose
        For i As Int32 = 0 To lstItems.Items.Count - 1
            m_arrList.Add(lstItems.Items(i).ToString)
        Next
        m_Title = txtTitle.Text.Trim
        ' Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    

    Private Sub txtItem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        ''Check for enter key
        If (e.KeyChar = ChrW(13)) Then
            Dim erg As System.EventArgs = Nothing
            If txtItem.Text.Length > 0 Then
                ''If txt is not empty then add the value
                btnAdd_Click(sender, erg)
            Else
                ''If txt is empty then add the entire data into db
                AddDropDown()
            End If
        Else
            ''If not enter key and txt is not empty, then enable the add button
            If txtItem.Text.Length > 0 Then
                btnAdd.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    '''  To enable the buttons for user
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EnableBtns()
        btnRemove.Enabled = True
        btnUp.Enabled = True
        btnDown.Enabled = True
        btnModify.Enabled = True
    End Sub

    ''' <summary>
    ''' To disable the buttons for validation
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisableBtns()
        btnRemove.Enabled = False
        btnUp.Enabled = False
        btnDown.Enabled = False
        btnModify.Enabled = False
    End Sub

    Private Sub lstItems_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstItems.MouseDown
        EnableBtns()
    End Sub

    Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If Not IsNothing(lstItems.SelectedItem) Then
            ''Get the selected item from the list
            txtItem.Text = lstItems.SelectedItem.ToString
            lstItems.Items.Remove(lstItems.SelectedItem)
        End If
    End Sub

   
    Private Sub tlsDropdown_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDropdown.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Ok"
                AddDropDown()
            Case "Cancel"
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub

    '<<<<<<<<<<<<<< Ojeswini 25 July 2008 >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


    Private Sub btnAdd_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnAdd.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnAdd_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnAdd.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnModify_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.MouseHover
        btnModify.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnModify.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnModify_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.MouseLeave
        btnModify.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnModify.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRemove_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.MouseHover
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnRemove.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRemove_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.MouseLeave
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnRemove.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnUp.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnUp.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnDown.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnDown.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
End Class








