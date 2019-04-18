Imports gloEMR.gloEMRWord
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Reflection

Public Class frmLiquidData
    Private WithEvents oLiquiddoc As Wd.Document
    Private m_arrList As ArrayList
    Private m_Title As String
    Private blnBooleanFlag As Boolean ''for checking the Boolean datatype
    Dim blnModify As Boolean '' FOr tracking the changes
    Dim m_ElementId As Int64 = 0 ''For preview/modify delete the data fields
    Dim objTemplate As New clsTemplateGallery
    Dim objclsPatientROS As New clsPatientROS
    Dim IsStandaredItem As Boolean = False
    Dim dtElement As DataTable = Nothing
    Dim dtSubElement As DataTable
    Dim dtSubElemetGroup As DataTable = Nothing
    Dim dv As DataView = Nothing
    Dim mylist As myList
    Dim IsforModify As Boolean = False
    Dim enumControlType As ControlType
    'Dim ItemListRowIndex As Integer
    Dim RowToEdit As Integer
    Dim enumCategoryType As CategoryType
    Dim strCategoryType As String
    Dim Index1 As Integer
    Dim IsfrmExam As Boolean = False
    Dim _ElementID As Int64 = 0
    Dim _FormField As ArrayList

    'SHUBHANGI  20100525 CHECK WHETHER THE RECORD IS MODIFIED OR NOT
    Dim IsModified As Boolean = False
    'shubhangi 20091027'
    Dim _selectedFieldtype As String = ""
    Dim _selectedCategory As String = ""
    Dim objclsTemplateGallery As New clsTemplateGallery
    Dim m_Id As Long

    Dim l_ArrayList As ArrayList
    'Shubhangi
    Dim m_TemplateName
    Dim m_arraylist As New ArrayList
    Dim m_FieldList As SortedList
    Dim m_list As myList
    Dim m_Required As Boolean
    Dim m_Group As Boolean
    Dim m_Category As String
    Dim m_Desc, m_TextValue, m_Caption, m_DataType, m_Fieldcategory, m_controlType As String

    Dim NewEMFieldID As Int64
    Dim selectedField As ArrayList
    Dim noCols As Int32
    Dim noRows As Int32
    ' "Modify Exam Template for EM Coding"
    Dim objDefaultBehaviorWord8 As Object = Wd.WdDefaultTableBehavior.wdWord8TableBehavior
    Dim objAutoFitFixed As Object = Wd.WdAutoFitBehavior.wdAutoFitFixed
    Dim LineStyleSingle As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle
    Dim objMissing As Object = System.Reflection.Missing.Value
    Dim arrFormField As ArrayList = Nothing
    Dim oControl As Microsoft.Office.Interop.Word.ContentControl
    Dim objHeadingStyle1 As Object = Wd.WdBuiltinStyle.wdStyleHeading1
    Dim objCollapseEnd As Object = Wd.WdCollapseDirection.wdCollapseEnd
    Dim objNormalStyle As Object = Wd.WdBuiltinStyle.wdStyleNormal
    Dim TextureNone As Wd.WdTextureIndex = Microsoft.Office.Interop.Word.WdTextureIndex.wdTextureNone
    Dim ColorGray10 As Wd.WdColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray10
    Dim LineStyleDouble As Wd.WdLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleDouble

    Dim strName1 As String = ""
    Dim _CategoryID As String = ""
    Dim _TemplateName As String = ""
    Dim m_EMTag As Int64
    Private nRow As Integer = 0

#Region "get data from SDK Variable"
   

#End Region

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

    Public Property GetElement() As Int64
        Get
            Return m_ElementId
        End Get
        Set(ByVal value As Int64)
            m_ElementId = value
        End Set
    End Property
    Public Property ElementID() As Int64
        Get
            Return _ElementID
        End Get
        Set(ByVal value As Int64)
            _ElementID = value
        End Set
    End Property
    Public Property FormField() As ArrayList
        Get
            Return _FormField
        End Get
        Set(ByVal value As ArrayList)
            _FormField = value
        End Set
    End Property
    Public Property modifyArrayList() As ArrayList
        Get
            Return l_ArrayList
        End Get
        Set(ByVal value As ArrayList)
            l_ArrayList = value
        End Set
    End Property

    Public Sub New(ByVal ID As Long, ByVal TemplateName As String)
        m_Id = ID
        m_TemplateName = TemplateName

        InitializeComponent()
    End Sub
    Public Sub New(ByVal t_ArrayList As ArrayList, ByVal ID As Long, ByVal TemplateName As String)
        m_Id = ID
        l_ArrayList = t_ArrayList
        m_TemplateName = TemplateName

        InitializeComponent()

    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' To add the field values into the list procedure
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click, ToolStripButton1.Click


        Try
            Dim strValue As String = txtItem.Text.Replace(" ", "")
            Dim strDataType As String = cmbDataType.Text.Trim
            Dim strCategory As String = cmbFieldCategory.Text.Trim
            'Shubhangi 20091010
            'Display message for boolean that we are able to add only 2 recrds for boolean.
            If cmbDataType.Text = "Boolean" And dgItemList.RowCount = 2 And IsforModify = False Then
                MsgBox("For Boolean data type you allow to add only two items.  ", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)

            Else
                'Shubhangi 20091012
                'Validation for blank Field type & ield category
                If strDataType = "Choose an item" Then
                    MsgBox("Please select field type.  ", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    cmbDataType.Focus()
                    Exit Sub
                End If
                If strCategory = "Choose an item" Then
                    MsgBox("Please select field category.  ", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    cmbFieldCategory.Focus()
                    Exit Sub
                End If
                ''check for empty text
                If strValue = "" Then
                    MsgBox("White spaces cannot be added.  ", MsgBoxStyle.Exclamation, "gloEMR")
                    txtItem.Focus()
                    Exit Sub
                End If

                If CmbControl.SelectedIndex = 0 And (cmbDataType.Text = "Multiple Selection") Then
                    MsgBox("Please select the control type for the DataField.  ", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    CmbControl.Focus()
                    Exit Sub
                End If


                'If dgItemList.RowCount = 1 Or btnaddassociated.cli = True Then
                '    MessageBox.Show("Only one value allowed with Associated Data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                'End If

                ''Check duplicate items
                If Not IsDuplicate(strValue) Then
                    strValue = txtItem.Text.Trim

                    ''Check for no of items in the list for boolean datatype
                    If IsforModify = False Then
                        If Not CheckItems() Then
                            ''Add item to the list
                            'lstItems.Items.Add(strValue)

                            If CmbControl.Text = "Check Box" Then
                                enumControlType = ControlType.CheckBox
                            ElseIf CmbControl.Text = "Text" Then
                                enumControlType = ControlType.Text
                            Else
                                enumControlType = ControlType.None
                            End If

                            With dgItemList
                                .Rows.Add()
                                .Item(0, .Rows.Count - 1).Value = enumControlType.GetHashCode() '' control Type ID Hidden
                                .Item(1, .Rows.Count - 1).Value = strValue '' Hidden category
                                .Item(2, .Rows.Count - 1).Value = enumControlType.ToString() '' Control Name
                                .Item(3, .Rows.Count - 1).Value = cmbstddata.SelectedItem

                            End With

                            'dgItemList.Rows.Add(enumControlType.GetHashCode(), strValue)

                            IsModified = True    'Set the flag to show that record is modified
                            ''Clear the textbox and set focus
                            txtItem.Text = String.Empty
                            txtItem.Focus()
                        End If
                        'IsforModify means here when we open any revord by double clicking
                    ElseIf IsforModify = True And dgItemList.RowCount > 0 Then

                        If CmbControl.Text = "Check Box" Then
                            enumControlType = ControlType.CheckBox
                        ElseIf CmbControl.Text = "Text" Then
                            enumControlType = ControlType.Text
                        Else
                            enumControlType = ControlType.None
                        End If
                        If dgItemList.RowCount > 0 Then
                            If Not IsNothing(dgItemList.Item(0, RowToEdit).Value) Then
                                dgItemList.Item(0, RowToEdit).Value = enumControlType.GetHashCode()
                            Else
                                dgItemList.Item(0, RowToEdit).Value = ""
                            End If
                        Else
                            Exit Sub
                        End If
                        dgItemList.Item(1, RowToEdit).Value = strValue
                        dgItemList.Item(2, RowToEdit).Value = enumControlType.ToString()
                        dgItemList.Item(3, RowToEdit).Value = cmbstddata.SelectedItem

                        'dgItemList.Rows.RemoveAt(RowToEdit)
                        'dgItemList.Rows.Insert(RowToEdit, enumControlType.GetHashCode(), strValue)
                        RowToEdit = 0

                        'Dim ina As Integer = lstItems.SelectedIndex
                        'lstItems.Items.Remove(lstItems.SelectedItem.ToString())
                        'lstItems.Items.Insert(ina, strValue)
                        IsModified = True
                        IsforModify = False
                        ''Clear the textbox and set focus
                        txtItem.Text = String.Empty
                        cmbstddata.SelectedItem = Nothing
                        txtItem.Focus()
                    End If

                Else
                    MsgBox("An entry with the same name already exists - each entry must specify a unique entry.  ", MsgBoxStyle.Exclamation, "gloEMR")
                End If
                IsforModify = False
                    'Shubhangi 20090918 
                    'For Boolean Data type we should enter only 2 records.
                    If cmbDataType.Text = "Boolean" And dgItemList.RowCount = 2 Then
                        txtItem.Enabled = False
                        txtItem.Text = ""
                    Else
                        txtItem.Enabled = True
                    End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            IsforModify = False
        End Try
    End Sub

    ''' <summary>
    ''' To Check for items count in the list for boolean datatype to ensure it contains two items
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckItems() As Boolean
        If blnBooleanFlag Then
            ''Sudhir 20090228 ''
            If chkAssociatestddata.Visible = True And chkAssociatestddata.Checked Then
                If dgItemList.Rows.Count >= 1 Then
                    If cmbDataType.Text <> "Boolean" Then
                        blnBooleanFlag = False
                    End If
                    MessageBox.Show("Only one value allowed with Associated Data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    Return True
                End If
            End If
            ''  ''
            If dgItemList.Rows.Count >= 2 And pnlassociateStdItem.Visible = True Then
                blnBooleanFlag = False
                MessageBox.Show("Boolean DataField accepts two Field Values only", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' check for duplicate items in the list box
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDuplicate(ByVal strName As String) As Boolean
        'Shuhangi 20091126
        'Commented by shubhangi 
        'If blnModify = False Or IsforModify = False Then
        For i As Int32 = 0 To dgItemList.Rows.Count - 1
            If cmbDataType.Text = "Multiple Selection" Then
                ''Check whether the same item is available in the list 
                If dgItemList.Item(1, i).Value.ToString.ToUpper = strName.Trim.ToUpper And dgItemList.Item(2, i).Value.ToString.ToUpper = CmbControl.Text.Trim.ToUpper.Replace(" ", "") Then
                    'If dgItemList.Item(1, i).Value.ToString.ToUpper = strName.Trim.ToUpper And dgItemList.Item(2, i).Value.ToString.ToUpper = CmbControl.Text.Trim.ToUpper Then
                    '' same value exists
                    Return True
                    Exit For
                End If
            ElseIf dgItemList.Item(1, i).Value.ToString.ToUpper = strName.Trim.ToUpper Then
                Return True
                Exit For
            End If
        Next
        'End If
        Return False
    End Function


    Private Function IsDuplicateNew(ByVal ID As Int64) As Boolean
        'For i As Int32 = 0 To lstItems.Items.Count - 1

        'Next
        Return Nothing
    End Function

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click, ToolStripButton2.Click
        Try
            If dgItemList.Rows.Count > 0 Then
                If dgItemList.CurrentRow.Index >= 0 Then
                    'Shubhangi 20090926 
                    ' select any record & check  if record count is less than 2 then  
                    'make Field type  text enable true to add new records(At the time of Edit).
                    If dgItemList.Rows.Count - 1 <= 2 And txtItem.Enabled = False Then
                        txtItem.Enabled = True
                    End If
                    ''Remove selected item from DataGrid
                    dgItemList.Rows.RemoveAt(dgItemList.CurrentRow.Index)
                    IsModified = True 'Set the flag to show that record is modified
                    txtItem.Text = ""
                    If cmbDataType.Text = "Multiple Selection" Then
                        If (CmbControl.Items.Count > 0) Then
                            CmbControl.SelectedIndex = 0
                        End If

                    End If
                End If
            End If
            If dgItemList.RowCount = 0 Then
                IsforModify = False
            End If

            'If Not IsNothing(lstItems.SelectedItem) Then
            '    ''Remove the selected item from the list
            '    lstItems.Items.Remove(lstItems.SelectedItem)
            'End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' To fill the field values in the list box
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillFieldValues()
        ''clear the list box and then add the field value to the list
        dgItemList.Rows.Clear()
        For _inc As Int32 = 0 To m_arrList.Count - 1
            dgItemList.Rows.Add(0, m_arrList.Item(_inc))
            'lstItems.Items.Add(m_arrList.Item(_inc))
        Next
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        'Try
        ''check If item is selected
        'If Not IsNothing(lstItems.SelectedItem) Then
        '    If lstItems.SelectedIndex <> lstItems.Items.Count - 1 Then
        '        ''if the selected item is not the last item
        '        Dim intindex As Int16
        '        intindex = lstItems.SelectedIndex
        '        Dim str As String
        '        str = lstItems.SelectedItem
        '        ''get the selected item, store it and remove from the list
        '        lstItems.Items.RemoveAt(lstItems.SelectedIndex)
        '        ''insert it in one position down and select it again
        '        lstItems.Items.Insert(intindex + 1, str)
        '        lstItems.SelectedItem = lstItems.Items.Item(intindex + 1)
        '    End If
        'End If
        'Commented by shubhangi 20090925
        '    Index = dgItemList.CurrentRow.Index

        '    Index = Index + 1
        '    If dgItemList.Rows.Count > 0 Then
        '        dgItemList.Rows.Item(Index).Selected = True
        '        dgItemList_CellClick(Nothing, Nothing)
        '    End If

        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        ' Try
        'If Not IsNothing(lstItems.SelectedItem) Then
        '    If lstItems.SelectedIndex <> 0 Then
        '        ''if the selected item is not the first item
        '        Dim intindex As Int16
        '        intindex = lstItems.SelectedIndex
        '        Dim str As String
        '        str = lstItems.SelectedItem
        '        ''get the selected item, store it and remove from the list
        '        lstItems.Items.RemoveAt(lstItems.SelectedIndex)
        '        ''insert it in one position up and select it again
        '        lstItems.Items.Insert(intindex - 1, str)
        '        lstItems.SelectedItem = lstItems.Items.Item(intindex - 1)

        '    End If
        'End If

        'Commented by shubhangi 20090925
        '    Index = dgItemList.CurrentRow.Index
        '    If Index <> 0 Then
        '        Index = Index - 1
        '    Else
        '        Index = 0
        '    End If

        '    If dgItemList.Rows.Count > 0 Then
        '        dgItemList.Rows.Item(Index).Selected = True
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    '''' <summary>
    '''' To check for the data field existence for the same datatype
    '''' </summary>
    '''' <param name="strFieldName"></param>
    '''' <param name="strDataType"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function IsDuplicateField(ByVal strFieldName As String, ByVal strDataType As String) As Boolean
    '    Dim oDB As New DataBaseLayer
    '    Dim strSQL As String
    '    Dim cnt As Int32 = 2
    '    Try
    '        strSQL = "Select count(*) FROM LiquidData_MST WHERE sElementName = '" & strFieldName & "' and sElementType='" & strDataType & "'"
    '        ''check for DB Null value
    '        If Not IsDBNull(oDB.GetRecord_Query(strSQL)) Then
    '            ''Get the no of records present
    '            cnt = oDB.GetRecord_Query(strSQL)
    '            If cnt = 1 Then
    '                '' one record available which is the current one acceptable
    '                If blnModify Then
    '                    Return False
    '                ElseIf cnt = 0 Then
    '                    '' New Record with no duplicate
    '                    Return False
    '                Else
    '                    ''Records available
    '                    Return True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return True
    '    Finally
    '        oDB = Nothing
    '    End Try
    'End Function

    ''' <summary>
    ''' To insert the Liquid Data Field in Database
    ''' </summary>
    ''' <param name="strFieldName"></param>
    ''' <param name="strDataType"></param>
    ''' <param name="strFieldValue"></param>
    ''' <param name="m_Fieldvalues"></param>
    ''' <param name="strColumn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddDataField(ByVal strFieldName As String, ByVal strDataType As String, ByVal strFieldCategory As String, Optional ByVal strFieldValue As String = "", Optional ByVal m_Fieldvalues As ArrayList = Nothing, Optional ByVal strColumn As String = "") As Boolean

        Try
            'If strFieldCategory = "Medical Decision-Making" Then
            '    strFieldCategory = ""
            '    strFieldCategory = "MD"
            'End If
            '' Add the Parent field and get its id for adding the sub filed values to group under it
            If IsfrmExam = True Then
                _ElementID = m_ElementId
            Else
                _ElementID = objTemplate.AddDataFieldValue(_ElementID, 0, strFieldName, strDataType, chckRequired.Checked, Nothing, strFieldCategory)
            End If

            'Problem : 00000163
            'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
            'Change : Parameter nSequenceNo passed in below function call objTemplate.AddDataFieldValue() for data types "Table", "Group", "Multiple Selection" and for other data types to maintain sequence set by user.

            If _ElementID <> 0 Then
                ''If the datatype is of free text and user has entered some text than add asa sub field
                If strDataType = "Text" Then
                    If strFieldValue <> "" Then
                        objTemplate.AddDataFieldValue(0, _ElementID, strFieldValue, strDataType, chckRequired.Checked)
                    End If
                ElseIf strDataType = "Table" Then
                    For _inc As Int32 = 0 To m_Fieldvalues.Count - 1
                        objTemplate.AddDataFieldValue(0, _ElementID, "", strDataType, chckRequired.Checked, CType(m_Fieldvalues.Item(_inc), myList), "", _inc + 1)
                    Next
                ElseIf strDataType = "Group" Then
                    For _inc As Int32 = 0 To m_Fieldvalues.Count - 1
                        objTemplate.AddDataFieldValue(0, _ElementID, txtCaption.Text.Trim, strDataType, chckRequired.Checked, CType(m_Fieldvalues.Item(_inc), myList), "", _inc + 1)
                    Next
                ElseIf strDataType = "Multiple Selection" Then
                    For _inc As Int32 = 0 To m_Fieldvalues.Count - 1
                        objTemplate.AddDataFieldValue(0, _ElementID, txtCaption.Text.Trim, strDataType, chckRequired.Checked, CType(m_Fieldvalues.Item(_inc), myList), "", _inc + 1)
                    Next
                ElseIf Not m_Fieldvalues Is Nothing Then
                    ''For other datatypes a list of items  to be added and grouped under Parent field
                    For _inc As Int32 = 0 To m_Fieldvalues.Count - 1
                        Dim type As Type = m_Fieldvalues.Item(_inc).GetType()
                        If type.Name = "myList" Then
                            objTemplate.AddDataFieldValue(0, _ElementID, txtCaption.Text.Trim, strDataType, chckRequired.Checked, CType(m_Fieldvalues.Item(_inc), myList), "", _inc + 1)
                        Else
                            objTemplate.AddDataFieldValue(0, _ElementID, m_Fieldvalues.Item(_inc), strDataType, chckRequired.Checked, Nothing, "", _inc + 1)
                        End If

                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        End Try

    End Function

    '''' <summary>
    '''' To Add the liquid data field in to DataBase
    '''' </summary>
    '''' <param name="nElementId"></param>
    '''' <param name="nGroupID"></param>
    '''' <param name="strFieldName"></param>
    '''' <param name="strDataType"></param>
    '''' <param name="bIsRequired"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function AddDataFieldValue(ByVal nElementId As Int64, ByVal nGroupID As Int64, ByVal strFieldName As String, ByVal strDataType As String, ByVal bIsRequired As Boolean) As Int64
    '    Dim oDB As DataBaseLayer
    '    Dim oParamater As DBParameter
    '    Dim ElementID As Int64 = 0
    '    Try
    '        oDB = New DataBaseLayer

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ElementName"
    '        oParamater.Value = strFieldName
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.VarChar
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ElementType"
    '        oParamater.Value = strDataType
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.Bit
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@bIsMandatory"

    '        If bIsRequired Then
    '            oParamater.Value = 1
    '        Else
    '            oParamater.Value = 0
    '        End If

    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing


    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@GroupID"
    '        oParamater.Value = nGroupID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@ColumnID"
    '        oParamater.Value = 0
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@MachineID"
    '        oParamater.Value = GetPrefixTransactionID()
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.InputOutput
    '        oParamater.Name = "@ElementID"
    '        oParamater.Value = nElementId
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing

    '        ElementID = oDB.Add("Insert_DataFields")
    '        Return ElementID
    '    Catch ex As Exception
    '        Return 0
    '    Finally
    '        oDB = Nothing
    '    End Try

    'End Function

    '''' <summary>
    '''' Delete the selcted dataField  from datadictionary
    '''' </summary>
    '''' <param name="nElementId"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function DeleteDataField(ByVal nElementId As Int64) As Boolean
    '    Dim oDB As New DataBaseLayer
    '    Dim strSQL As String
    '    Try
    '        strSQL = "Delete FROM LiquidData_MST WHERE nElementId = " & nElementId & " or nGroupID= " & nElementId
    '        Return oDB.Delete_Query(strSQL)
    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        oDB = Nothing
    '    End Try
    'End Function

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If (cmbDataType.Text <> "Text" Or cmbDataType.Text <> "Table" Or cmbDataType.Text <> "Group") And IsModified = True Then
            Dim Result As Integer
            Result = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Result = DialogResult.Yes Then
                btnSave_Click(sender, e)
                Exit Sub
            ElseIf Result = DialogResult.No Then
                IsModified = False
            ElseIf Result = DialogResult.Cancel Then
                IsModified = True
                Exit Sub
            End If
        End If
        'SHUBHANGI 20100618 ADDED BY SHUBHANGI 20100618 TO SET FOCUS ON THE RECORD WHICH WE OPEN FOR MODIFY BUG 1926
        Dim trvChildNode As myTreeNode
        trvChildNode = trvDiscrete.SelectedNode
        trvChildNode.Key = m_ElementId
        trvDiscrete.Focus()
        'END

            'Shubhangi 20091027
            'Make m_Element Id as zero for open same record after closing the same panel;
            m_ElementId = 0

            'Shubhangi
            If pnl_ToolStrip.Visible = False Then
                pnl_ToolStrip.Visible = True
            End If

            ''hide the edit panel to the end user
            pnlEdit.Visible = False
            'pnlview.Visible = True
            'Shubhangi 20091014

            'Reset all fields B'Coz when we close this panel it should clear all data. So it is useful to check at the time of opening any record ot modify.
            ResetEditPanel()
            If IsfrmExam = True Then
                Me.Close()
            End If
    End Sub

    Private Sub txtItem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItem.KeyPress
        ''Check for enter key
        If (e.KeyChar = ChrW(13)) Then
            Dim erg As System.EventArgs = Nothing
            If txtItem.Text.Length > 0 Then
                ''If txt is not empty then add the value
                btnAdd_Click(sender, erg)
            End If
        End If
    End Sub
    'Shubhangi 20091222
    'Add this event to avoid the form resizing after opening multiple forms
    Private Sub frmLiquidData_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
        For Each myForm As Form In Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
    End Sub

    Private Sub frmLiquidData_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.TopMost = False
    End Sub

    Private Sub frmLiquidData_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter

    End Sub

    Private Sub frmLiquidData_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub frmLiquidData_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub frmLiquidData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Code Start added by kanchan on 20120102 for gloCommunity integration
        If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
            ts_gloCommunityDownload.Visible = gblngloCommunity
        End If
        'Code end added by kanchan on 20120102 for gloCommunity integration
        'pnlview.Visible = True
        pnlEdit.Visible = False
        If pnl_ToolStrip.Visible = False Then
            pnl_ToolStrip.Visible = True
        End If
        'DesignListBox()
        DesignGrid()
        FillLiquidData()
        FillLiquidDataType()
        FillLiquidDataCategory()
        FillControlType()
        dgItemList.Columns.Item(2).Visible = False
        dgTableField.ReadOnly = True

        If m_ElementId > 0 Then
            IsfrmExam = True
            If IsfrmExam = True Then
                ModifyfromExam(trvDiscrete.SelectedNode)
            Else
                'If IsNothing(trvDiscrete.SelectedNode) = False Then
                ModifyData(trvDiscrete.SelectedNode)
            End If

            tlsLiquidData.Enabled = False
            trvDiscrete.Enabled = False
            txtField.Enabled = False
            cmbDataType.Enabled = False
            cmbFieldCategory.Enabled = False
            'End If
        End If
    End Sub
    Public Sub FillControlType()
        With CmbControl.Items
            .Clear()
            .Add("Choose an item")
            .Add("Check Box")
            .Add("Text")
        End With
        ''Select the first Item 
        CmbControl.SelectedIndex = 0
    End Sub

    Public Sub FillLiquidDataCategory()
        With cmbFieldCategory.Items
            .Clear()
            .Add("Choose an item")
            .Add("General")
            .Add("History")
            .Add("Physical Examination")
            .Add("Medical Decision-Making")
            .Add("HPI")
            .Add("Management option")
            .Add("Labs")
            .Add("X-Ray/Radiology")
            ''Commented and added new because GLO2010-0008865 misspellings in software: says that other diagonsis tests should be other diagnostic tests
            ''Start
            '.Add("Other Diagonsis Tests")
            .Add("Other Diagnostic Tests")
            ''End
            .Add("ROS")
        End With
        ''Select the first Item 
        cmbFieldCategory.SelectedIndex = 0
    End Sub
    'Public Sub DesignListBox()
    '    lstItems.Items.Clear()
    '    lstItems.ClearSelected()
    '    lstItems.Items.Clear()
    '    lstItems.Items.Add(0)
    '    lstItems.Items.Add(1)
    'End Sub
    Public Sub DesignGrid()

    End Sub
    ''' <summary>
    ''' To fill the tree view with liquid data fields
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FillLiquidData()
        Dim trvRootNode As myTreeNode
        Dim trvChildNode As myTreeNode ''User defined tree node to hold custom values
        Try

            ''Get the Liquid data  for binding it to Treeview
            Dim objData As DataTable = objTemplate.GetLiquidData()
            If Not objData Is Nothing Then
                With trvDiscrete
                    .Nodes.Clear()
                    ''Add the default tree node
                    trvRootNode = New myTreeNode
                    trvRootNode.Text = "Liquid Data"
                    trvRootNode.ImageIndex = 0
                    trvRootNode.SelectedImageIndex = 0
                    .Nodes.Add(trvRootNode)
                    ''Add the Data fileds under the parent node
                    For _inc As Int32 = 0 To objData.Rows.Count - 1
                        ' trvChildNode = New myTreeNode
                        Dim _Flag As Int64 = 0
                        ''check for Mandatory field and set the flag
                        If objData.Rows(_inc)("bIsMandatory") = True Then
                            _Flag = 1
                        End If
                        ''Key Refers to Element Id, Strname refers to Element name, _Flag refers to IsMandatory 
                        trvChildNode = New myTreeNode(objData.Rows(_inc)("sElementName"), objData.Rows(_inc)("nElementID"), _Flag)
                        trvChildNode.ImageIndex = 2
                        trvChildNode.SelectedImageIndex = 2
                        trvChildNode.NodeName = objData.Rows(_inc)("sElementType")
                        trvRootNode.Nodes.Add(trvChildNode)
                        trvChildNode = Nothing
                    Next
                    ' .SelectedNode = trvRootNode

                    .ExpandAll()
                End With
                'trvDiscrete.SelectedNode = trvDiscrete.Nodes(0)
            End If
            Dim n As TreeNode = trvDiscrete.Nodes(0)
            'Shubhangi 20091014
            ' If blnModify = False Then
            trvDiscrete.SelectedNode = n
            trvDiscrete.Focus()
            ' Else
            'trvDiscrete.SelectedNode = SelectedLiquid
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            trvRootNode = Nothing
        End Try
    End Sub
   
    ''Public Function GetLiquidData() As DataTable

    ''    Dim oDB As New DataBaseLayer
    ''    Dim oParamater As DBParameter
    ''    Dim oResultTable As New DataTable
    ''    Dim strSQL As String
    ''    Try
    ''        ''Query to get the root level DataField
    ''        strSQL = "select * FROM LiquidData_MST WHERE nGroupId = 0"
    ''        oResultTable = oDB.GetDataTable_Query(strSQL)

    ''        If Not oResultTable Is Nothing Then
    ''            Return oResultTable
    ''        Else
    ''            Return Nothing
    ''        End If

    ''    Catch ex As Exception
    ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    ''        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''        Return Nothing
    ''    Finally
    ''        oDB = Nothing
    ''    End Try

    'End Function

    Private Sub cmbDataType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDataType.Click
        'Shubhangi 20091107
        _selectedFieldtype = cmbDataType.Text

    End Sub

    Private Sub cmbDataType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectionChangeCommitted

        'SHUBHANGI 20090919  
        ''DISPLAY MESSAGE WHEN WE CHANGE FIELD TYPE

        If cmbDataType.Text <> "Choose an item" And dgItemList.Rows.Count <> 0 Then
            If MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                'dgTableField.Rows.Clear()
                dgItemList.Rows.Clear()
                'Shubhangi 20091026
                'Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
                'It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event
                IsforModify = False
            Else
                'cmbDataType.Text = strDatatype
                cmbDataType.Text = _selectedFieldtype
                Exit Sub
            End If
        End If
        If cmbDataType.Text <> "Choose an item" And dgTableField.Rows.Count <> 0 Then
            If MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                dgTableField.Rows.Clear()
                'Shubhangi 20091026
                'Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
                'It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event
                IsforModify = False
            Else
                cmbDataType.Text = _selectedFieldtype
                Exit Sub
            End If
        End If
        '_selectedFieldtype = cmbDataType.Text
        'Shubhangi 20091007

        If cmbDataType.Text = "Choose an item" And Panel5.Visible = True Then
            '  pa()
            Exit Sub
        End If
        If cmbDataType.Text <> "Table" Or cmbDataType.Text <> "Group" Then
            pnlFieldValues.Dock = DockStyle.Fill
        Else
            pnlFieldValues.Dock = DockStyle.Top
        End If
        dgTableField.Rows.Clear()
        dgItemList.Rows.Clear()
        ValidateDatatype(cmbDataType.Text)
    End Sub

    Private Sub ValidateDatatype(ByVal strDataType As String)
        ''To validate the user with necessary actions based on datatype
        Select Case strDataType
            Case "Boolean"
                ToolStripButton1.Enabled = True
                ToolStripButton2.Enabled = True
                ''If Boolean datatype, user should be restricted to enter two values only
                blnBooleanFlag = True
                Panel2.Height = 139
                pnlFieldValues.Visible = True
                pnlFieldValues.BringToFront()
                ''pnlBtns.Visible = True
                txtItem.Visible = True
                Label36.Visible = True
                Label3.Visible = True
                ''check the items in list box to warn the user for not more than two items
                If blnBooleanFlag Then
                    If dgItemList.Rows.Count > 2 Then
                        blnBooleanFlag = False
                        MessageBox.Show("Boolean DataField accepts two Field Values only", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    End If
                End If
                pnlTableEntry.Visible = False
                chkAssociatestddata.Visible = True

                Panel5.Visible = False
                If dgItemList.ColumnCount > 1 Then
                    'Shubhangi 20091027
                    If cmbFieldCategory.Text = "History" Or cmbFieldCategory.Text = "Management option" Or cmbFieldCategory.Text = "X-Ray/Radiology" Or cmbFieldCategory.Text = "Other Diagonsis Tests" Or cmbFieldCategory.Text = "Labs" Then

                        dgItemList.Columns.Item(2).Visible = False
                        dgItemList.Columns.Item(3).Visible = True
                    Else

                        dgItemList.Columns.Item(2).Visible = False
                        dgItemList.Columns.Item(3).Visible = False
                    End If
                End If
                'dgItemList.Columns.Item(2).Visible = False
                'chkAssociatestddata.Visible = False
                'pnlassociateStdItem.Visible = False
                'pnlStandardEM.Visible = False
            Case "Text"
                ToolStripButton1.Enabled = False
                ToolStripButton2.Enabled = False
                ''If Text datatype - hide the Field values list and add/modify/delete buttons
                Panel2.Height = 139
                pnlTableEntry.Visible = False
                pnlFieldValues.Visible = False
                Label3.Visible = True
                txtItem.Visible = True
                Label36.Visible = True
                ''pnlBtns.Visible = True

                pnlFieldValues.Visible = False
                ''pnlBtns.Visible = False
                Panel5.Visible = False
                dgItemList.Columns.Item(2).Visible = False
                chkAssociatestddata.Visible = False
                pnlassociateStdItem.Visible = False
                'pnlStandardEM.Visible = False
            Case "Table"
                ToolStripButton1.Enabled = False
                ToolStripButton2.Enabled = False
                Panel2.Height = 107
                pnlTableEntry.Visible = True
                pnlFieldValues.Visible = False
                Label3.Visible = False
                txtItem.Visible = False
                Label36.Visible = False
                ''pnlBtns.Visible = False
                lblcaption.Visible = False
                txtCaption.Visible = False
                lblControl.Visible = True
                CmbControl.Visible = True
                If (CmbControl.Items.Count > 0) Then
                    CmbControl.SelectedIndex = 0
                End If


                Panel5.Visible = True
                If Panel2.Controls.Contains(Panel5) = True Then
                    ''Panel2.Controls.Remove(Panel5)
                    pnlAddCategory.Controls.Add(Panel5)
                    'Panel5.Location = New System.Drawing.Point(62, 92) ''  (366, 85)
                End If
                Panel5.Location = New System.Drawing.Point(62, 92) ''  (366, 85)
                If blnModify = False Then
                    chkAssociatestddata.Visible = False
                    pnlassociateStdItem.Visible = False
                    chkAssociateStd.Checked = False
                    chkAssociateStd.Visible = False
                End If
                chkAssociatestddata.Visible = False
                pnlassociateStdItem.Visible = False
                'pnlStandardEM.Visible = True
                'If pnlFieldValues.Controls.Contains(pnlStandardEM) = True Then
                '    pnlFieldValues.Controls.Remove(pnlStandardEM)
                '    pnlAddCategory.Controls.Add(pnlStandardEM)
                '    pnlStandardEM.Location = New System.Drawing.Point(501, 6)
                'End If

            Case "Group"
                ToolStripButton1.Enabled = False
                ToolStripButton2.Enabled = False
                Panel2.Height = 107
                pnlTableEntry.Visible = True
                pnlFieldValues.Visible = False
                Label3.Visible = False
                txtItem.Visible = False
                Label36.Visible = False     'To make lavel of txtItem field
                ''pnlBtns.Visible = False
                lblcaption.Visible = True
                txtCaption.Visible = True

                Panel5.Visible = True
                'PANEL2 IS A PANEL CONTAINS UPPER AREA NOT ANY DG
                If Panel2.Controls.Contains(Panel5) = True Then
                    Panel2.Controls.Remove(Panel5)
                    pnlAddCategory.Controls.Add(Panel5)
                    'Panel5.Location = New System.Drawing.Point(62, 92) ''  (366, 85)
                End If
                Panel5.Location = New System.Drawing.Point(62, 92)

                If blnModify = False Then
                    chkAssociatestddata.Visible = False
                    pnlassociateStdItem.Visible = False
                    chkAssociateStd.Checked = False

                End If
                chkAssociatestddata.Visible = False
                pnlassociateStdItem.Visible = False
                'pnlStandardEM.Visible = True
                'If pnlFieldValues.Controls.Contains(pnlStandardEM) = True Then
                '    pnlFieldValues.Controls.Remove(pnlStandardEM)
                '    pnlAddCategory.Controls.Add(pnlStandardEM)
                '    pnlStandardEM.Location = New System.Drawing.Point(501, 6)
                'End If
            Case "Single Selection"
                ToolStripButton1.Enabled = True
                ToolStripButton2.Enabled = True
                Panel2.Height = 139
                blnBooleanFlag = False
                pnlFieldValues.Visible = True
                ''pnlBtns.Visible = True
                pnlFieldValues.BringToFront()
                pnlTableEntry.Visible = False

                Label3.Visible = True
                txtItem.Visible = True
                Label36.Visible = True
                ''pnlBtns.Visible = True

                Panel5.Visible = False
                If dgItemList.ColumnCount > 1 Then

                    dgItemList.Columns.Item(2).Visible = False
                    dgItemList.Columns.Item(3).Visible = False
                End If
                chkAssociatestddata.Visible = False
                chkAssociateStd.Visible = False
                pnlassociateStdItem.Visible = False
                'pnlStandardEM.Visible = False
            Case Else
                ToolStripButton1.Enabled = True
                ToolStripButton2.Enabled = True
                blnBooleanFlag = False
                Panel2.Height = 169
                pnlFieldValues.Visible = True
                ''pnlBtns.Visible = True
                pnlFieldValues.BringToFront()
                pnlTableEntry.Visible = False

                Label3.Visible = True
                txtItem.Visible = True
                Label36.Visible = True
                ''pnlBtns.Visible = True

                Panel5.Visible = True
                If pnlAddCategory.Controls.Contains(Panel5) = True Then
                    pnlAddCategory.Controls.Remove(Panel5)
                    Panel2.Controls.Add(Panel5)
                    Panel5.Location = New System.Drawing.Point(61, 130) ''  (366, 106)
                End If
                dgItemList.Columns.Item(2).Visible = True
                If blnModify = False Then
                    chkAssociatestddata.Visible = False
                    pnlassociateStdItem.Visible = False
                End If
                fillcombobox()

                'pnlStandardEM.Visible = False
        End Select
    End Sub

    ''' <summary>
    ''' To Fill the data types for liquid data fields
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillLiquidDataType()
        With cmbDataType.Items
            ''Clear the items before adding the items into combobox
            .Clear()
            .Add("Choose an item")
            .Add("Boolean")
            .Add("Single Selection")
            .Add("Multiple Selection")
            '.Add("Linked Selection")
            .Add("Text")
            .Add("Table")
            .Add("Group")
        End With
        ''Select the first Item 
        cmbDataType.SelectedIndex = 0
    End Sub

    Private Sub tlsLiquidData_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsLiquidData.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                AddData()
            Case "Modify"
                If IsNothing(trvDiscrete.SelectedNode) = False Then
                    ModifyData(trvDiscrete.SelectedNode)
                End If
            Case "Delete"
                DeleteData()
            Case "Refresh"
                RefreshData()
            Case "Close"
                Me.Close()
        End Select
    End Sub

    ''' <summary>
    ''' To add a new Liquid Data field 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddData()
        ''Reset the form and make panels visibility based on the selection
        ResetEditPanel()
        Panel2.Height = 139
        'pnlview.Visible = False
        pnlEdit.Visible = True
        pnlFieldValues.Visible = False
        pnlTableEntry.Visible = False
        txtField.Focus()
        chkAssociatestddata.Visible = False
        chkAssociatestddata.Checked = False
        'chkAssociatestddata.Enabled = True
        pnlStandardEM.Visible = False
        cmbstddata.Items.Clear()
        blnModify = False
        'Shubhangi 20100108
        pnl_ToolStrip.Visible = False
        Panel5.Location = New System.Drawing.Point(61, 140)
        'txtItem.Enabled = True


    End Sub

    ''' <summary>
    ''' To modify the selected DataField
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ModifyData(ByVal SelectedNode As myTreeNode)
        Try
            'If cmbFieldCategory.Text = "HPI" Or cmbFieldCategory.Text = "Physical Examination" Or cmbFieldCategory.Text = "Management option" Or cmbFieldCategory.Text = "X-Ray/Radiology" Or cmbFieldCategory.Text = "Other Diagonsis Tests" Or cmbFieldCategory.Text = "ROS" Then
            '    chkAssociatestddata.Visible = True
            'End If

            If txtItem.Enabled = False Then
                txtItem.Enabled = True
            End If
            If dgItemList.Rows.Count < 2 Then
                txtItem.Enabled = True
            End If

            'Shubhangi commented by shubhangi 20100419
            If SelectedNode.Text <> "Liquid Data" Or IsfrmExam = True Then

                'Dim myNode As New myTreeNode
                'myNode = CType(trvDiscrete.SelectedNode, myTreeNode)
                If IsfrmExam = False Then
                    m_ElementId = SelectedNode.Key
                End If

                If m_ElementId = 0 Then
                    If trvDiscrete.SelectedNode Is Nothing Then
                        Exit Sub
                    End If

                    If trvDiscrete.SelectedNode.Level = 0 Then
                        Exit Sub
                    End If
                    'Dim myNode As New myTreeNode
                    ' myNode = CType(trvDiscrete.SelectedNode, myTreeNode)
                    ''Set the ElementId  for further processing
                    'm_ElementId = myNode.Key
                    '  trvDiscrete.SelectedNode = trvDiscrete.Nodes(0)
                End If
            Else
                MsgBox("Select record to modify", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                Exit Sub
            End If
            '  Shubhangi()
            pnl_ToolStrip.Visible = False
            ' End shubhangi

            blnModify = True

            'Shubhangi 20091022
            'B'Coz we dont require this Check box for Table & Group.
            'If cmbDataType.Text = "Table" Or cmbDataType.Text = "Group" Then
            '    chkAssociatestddata.Visible = False
            'End If
            ''End shubhangi
            txtcategory.Enabled = True
            txtCatItem.Enabled = True
            CmbControl.Enabled = True
            ResetEditPanel()

            Dim objdata As DataTable = objTemplate.GetDataField(m_ElementId)
            Dim strFieldCategory As String = ""
            If Not objdata Is Nothing Then
                dgTableField.Rows.Clear()

                For _inc As Int32 = 0 To objdata.Rows.Count - 1
                    If objdata.Rows(_inc)("nGroupID") = 0 Then
                        txtField.Text = objdata.Rows(_inc)("sElementName").ToString
                        cmbDataType.Text = objdata.Rows(_inc)("sElementType").ToString
                        If cmbDataType.Text = "Multiple Selection" Then
                            dgItemList.Columns.Item(2).Visible = True
                            '''''''''''''''Integrated by Mayuri:20100731'''''''''''''''''''
                            pnlFieldValues.Dock = DockStyle.Fill

                            '''''''''''''''Integrated by Mayuri:20100731'''''''''''''''''''
                        Else
                            dgItemList.Columns.Item(2).Visible = False
                        End If
                        'ALREADY COMMENTED 
                        'strFieldCategory = objdata.Rows(_inc)("sCategoryName").ToString()
                        'If strFieldCategory = "MD" Then
                        '    strFieldCategory = ""
                        '    strFieldCategory = "Medical Decision-Making"
                        'End If
                        If CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.None Then
                            cmbFieldCategory.SelectedIndex = CategoryType.None.GetHashCode()

                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.General Then
                            cmbFieldCategory.SelectedIndex = CategoryType.General.GetHashCode()

                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Hitory Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Hitory.GetHashCode()

                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Physical_Examination Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Physical_Examination.GetHashCode()

                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Medical_Decision_Making Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Medical_Decision_Making.GetHashCode()
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.HPI Then
                            cmbFieldCategory.SelectedIndex = CategoryType.HPI.GetHashCode()
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Management_option Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Management_option.GetHashCode()
                            If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                                fillbooleancombobox()
                            Else
                                fillcombobox()
                            End If
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Labs Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Labs.GetHashCode()
                            If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                                fillbooleancombobox()
                            Else
                                fillcombobox()
                            End If
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.X_Ray_Radiology Then
                            cmbFieldCategory.SelectedIndex = CategoryType.X_Ray_Radiology.GetHashCode()
                            If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                                fillbooleancombobox()
                            Else
                                fillcombobox()
                            End If
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Other_Diagonsis_Tests Then
                            cmbFieldCategory.SelectedIndex = CategoryType.Other_Diagonsis_Tests.GetHashCode()
                            If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                                fillbooleancombobox()
                            Else
                                fillcombobox()
                            End If
                        ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.ROS Then
                            cmbFieldCategory.SelectedIndex = CategoryType.ROS.GetHashCode()
                            fillcombobox()
                        End If

                        chckRequired.Checked = objdata.Rows(_inc)("bIsMandatory")
                        Exit For
                    End If
                Next
                For _inc As Int32 = 0 To objdata.Rows.Count - 1
                    ''m_elemntId is the root field
                    'If objdata.Rows(_inc)("nElementId") = m_ElementId Then

                    ' Else



                    If objdata.Rows(_inc)("nGroupID") <> 0 Then
                        ''The SubItems are to be binded to the list
                        ''If the datatype is of text then no need to show list box
                        If objdata.Rows(_inc)("sElementType") = "Text" Then
                            ValidateDatatype(objdata.Rows(_inc)("sElementType").ToString)
                            txtItem.Text = objdata.Rows(_inc)("sElementName").ToString
                            pnlFieldValues.Dock = DockStyle.Top
                        ElseIf objdata.Rows(_inc)("sElementType") = "Table" Or objdata.Rows(_inc)("sElementType") = "Group" Then

                            ValidateDatatype(objdata.Rows(_inc)("sElementType").ToString)
                            If objdata.Rows(_inc)("sAssociateditem").ToString <> "" Then
                                chkAssociateStd.Visible = True
                                chkAssociateStd.Checked = True
                                pnlStandardEM.Visible = True
                                dgTableField.Columns(5).Visible = True
                                dgTableField.Columns(6).Visible = True
                            Else
                                chkAssociateStd.Checked = False
                                chkAssociateStd.Visible = False
                            End If
                            InsertData(objdata.Rows(_inc)("sCategoryName").ToString, objdata.Rows(_inc)("sItemName").ToString, CType(objdata.Rows(_inc)("nControlType"), ControlType).GetHashCode(), objdata.Rows(_inc)("sAssociatedCategory"), objdata.Rows(_inc)("sAssociateditem"), objdata.Rows(_inc)("sAssociatedProperty"))
                            txtCaption.Text = objdata.Rows(_inc)("sElementName").ToString()
                            pnlFieldValues.Dock = DockStyle.Top
                            'txtItem.Text = objdata.Rows(_inc)("sElementName").ToString
                        ElseIf objdata.Rows(_inc)("sElementType") = "Multiple Selection" Then

                            With dgItemList
                                .Rows.Add()
                                .Item(0, .Rows.Count - 1).Value = CType(objdata.Rows(_inc)("nControlType"), ControlType).GetHashCode()
                                .Item(1, .Rows.Count - 1).Value = objdata.Rows(_inc)("sElementName").ToString()
                                .Item(2, .Rows.Count - 1).Value = CType(objdata.Rows(_inc)("nControlType"), ControlType).ToString()
                                If objdata.Rows(_inc)("sAssociatedProperty").ToString() <> "" Then
                                    .Item(3, .Rows.Count - 1).Value = objdata.Rows(_inc)("sAssociatedProperty").ToString()
                                    .Columns(3).Visible = True
                                    chkAssociatestddata.Checked = True
                                    chkAssociatestddata.Visible = True
                                    pnlassociateStdItem.Visible = True
                                    'chkAssociatestddata.Enabled = False
                                Else
                                    .Columns(3).Visible = False
                                    chkAssociatestddata.Checked = False
                                    chkAssociatestddata.Visible = False
                                    pnlassociateStdItem.Visible = False
                                    'chkAssociatestddata.Enabled = True
                                End If
                            End With

                            ''dgItemList.Rows.Add(0, objdata.Rows(_inc)("sElementName").ToString)
                            '''''''''''''''Integrated by Mayuri:20100731'''''''''''''''''''
                            pnlFieldValues.Dock = DockStyle.Fill
                            ''pnlAddCategory.Visible = False
                            ''Panel1.Visible = False
                            ''dgTableField.Visible = False
                            '''''''''''''''Integrated by Mayuri:20100731
                        ElseIf objdata.Rows(_inc)("sElementType") = "Text" Or objdata.Rows(_inc)("sElementType") = "Boolean" Or objdata.Rows(_inc)("sElementType") = "Single Selection" Or objdata.Rows(_inc)("sElementType") = "Group" Then

                            'lstItems.Items.Add(objdata.Rows(_inc)("sElementName").ToString)
                            dgItemList.Rows.Add(0, objdata.Rows(_inc)("sElementName").ToString)
                            ''Sudhir 20090228''
                            If objdata.Rows(_inc)("sElementType") = "Boolean" And objdata.Rows(_inc)("sAssociatedProperty").ToString() <> "" Then
                                dgItemList.Item(3, dgItemList.Rows.Count - 1).Value = objdata.Rows(_inc)("sAssociatedProperty").ToString()
                                dgItemList.Columns(3).Visible = True
                                chkAssociatestddata.Visible = True
                                chkAssociatestddata.Checked = True

                                pnlassociateStdItem.Visible = True
                            End If

                            '''''''''''''''Integrated by Mayuri:20100731'''''''''''''''''''
                            pnlFieldValues.Dock = DockStyle.Fill
                            ''pnlAddCategory.Visible = False
                            ''Panel1.Visible = False
                            ''dgTableField.Visible = False
                            '''''''''''''''Integrated by Mayuri:20100731'''''''''''''''''''
                            '' -- ''
                        End If

                    End If
                Next
                If objdata.Rows.Count > 0 Then
                    ValidateDatatype(objdata.Rows(0)("sElementType").ToString)
                End If
            End If
            '********
            ' End If
            'Shubhangi 20091010

            If cmbFieldCategory.Text = "HPI" Or cmbFieldCategory.Text = "Management option" Or cmbFieldCategory.Text = "X-Ray/Radiology" Or cmbFieldCategory.Text = "Other Diagonsis Tests" Or cmbFieldCategory.Text = "ROS" Or cmbFieldCategory.Text = "Labs" Then
                If cmbDataType.Text = "Table" Or cmbDataType.Text = "Group" Or cmbDataType.Text = "Single Selection" Or cmbDataType.Text = "Boolean" Or cmbDataType.Text = "Text" Then
                    chkAssociatestddata.Visible = False
                Else
                    chkAssociatestddata.Visible = True
                End If

            ElseIf cmbFieldCategory.Text = "Physical Examination" Then
                If cmbDataType.Text = "Group" Or cmbDataType.Text = "Table" Then
                    chkAssociateStd.Visible = True
                Else
                    chkAssociatestddata.Visible = False
                End If

            ElseIf cmbDataType.Text = "Boolean" Then
                If cmbFieldCategory.Text = "History" Or cmbFieldCategory.Text = "Management option" Or cmbFieldCategory.Text = "X-Ray/Radiology" Or cmbFieldCategory.Text = "Other Diagonsis Tests" Or cmbAssociatedCategory.Text = "Labs" Then
                    chkAssociatestddata.Visible = True
                Else
                    'Shubhangi 20091027 For Field type as 'Boolean' n Category as General
                    chkAssociatestddata.Visible = False
                End If
            Else
                chkAssociateStd.Checked = False
                chkAssociateStd.Visible = False
                chkAssociatestddata.Checked = False
                chkAssociatestddata.Visible = False
            End If
            'pnlview.Visible = False
            pnlEdit.Visible = True
            txtField.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    'SHUBHANGI THIS FUNCTION IS ADDED TO ADD DATA WHEN WE OPEN ANY TABLE FROM TEMPLATE FOR MODIFY AT THAT TIME WE HAVE ARRAYLIST & WE ARE RETTRIVING DATA FROM ARRAYLIST
    Private Sub ModifyfromExam(ByVal SelectedNode As myTreeNode)
        If txtItem.Enabled = False Then
            txtItem.Enabled = True
        End If
        If dgItemList.Rows.Count < 2 Then
            txtItem.Enabled = True
        End If

        'Shubhangi commented by shubhangi 20100419
        If SelectedNode.Text <> "Liquid Data" Or IsfrmExam = True Then
            If IsfrmExam = False Then
                m_ElementId = SelectedNode.Key
            End If

            If m_ElementId = 0 Then
                If trvDiscrete.SelectedNode Is Nothing Then
                    Exit Sub
                End If
                If trvDiscrete.SelectedNode.Level = 0 Then
                    Exit Sub
                End If
            End If
        Else
            MsgBox("Select record to modify", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
            Exit Sub
        End If

        pnl_ToolStrip.Visible = False
        blnModify = True
        txtcategory.Enabled = True
        txtCatItem.Enabled = True
        CmbControl.Enabled = True
        CmbControl.Visible = True
        ResetEditPanel()
        Panel2.Controls.Add(Panel5)
        Panel5.Location = New System.Drawing.Point(60, 133)
        If l_ArrayList.Count > 0 Then


            txtField.Text = CType(l_ArrayList.Item(0), myList).Description.Trim()
        End If
        Dim objdata As DataTable = objTemplate.GetDataField(m_ElementId)
        cmbDataType.Text = objdata.Rows(0)("sElementType").ToString
        If cmbDataType.Text = "Multiple Selection" Then
            dgItemList.Columns.Item(2).Visible = True
        Else
            dgItemList.Columns.Item(2).Visible = False
        End If


        For _inc As Int32 = 0 To objdata.Rows.Count - 1
            If objdata.Rows(_inc)("nGroupID") = 0 Then
                'cmbFieldCategory.SelectedIndex = (CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType)).GetHashCode()

                If CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.None Then
                    cmbFieldCategory.SelectedIndex = CategoryType.None.GetHashCode()

                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.General Then
                    cmbFieldCategory.SelectedIndex = CategoryType.General.GetHashCode()

                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Hitory Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Hitory.GetHashCode()

                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Physical_Examination Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Physical_Examination.GetHashCode()

                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Medical_Decision_Making Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Medical_Decision_Making.GetHashCode()
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.HPI Then
                    cmbFieldCategory.SelectedIndex = CategoryType.HPI.GetHashCode()
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Management_option Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Management_option.GetHashCode()
                    If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                        fillbooleancombobox()
                    Else
                        fillcombobox()
                    End If
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Labs Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Labs.GetHashCode()
                    If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                        fillbooleancombobox()
                    Else
                        fillcombobox()
                    End If
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.X_Ray_Radiology Then
                    cmbFieldCategory.SelectedIndex = CategoryType.X_Ray_Radiology.GetHashCode()
                    If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                        fillbooleancombobox()
                    Else
                        fillcombobox()
                    End If
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.Other_Diagonsis_Tests Then
                    cmbFieldCategory.SelectedIndex = CategoryType.Other_Diagonsis_Tests.GetHashCode()
                    If objdata.Rows(_inc)("sElementType").ToString = "Boolean" Then
                        fillbooleancombobox()
                    Else
                        fillcombobox()
                    End If
                ElseIf CType(objdata.Rows(_inc)("sCategoryName").ToString(), CategoryType) = CategoryType.ROS Then
                    cmbFieldCategory.SelectedIndex = CategoryType.ROS.GetHashCode()
                    fillcombobox()
                End If

                chckRequired.Checked = objdata.Rows(_inc)("bIsMandatory")
                Exit For
            End If
        Next
        'WE STARTED THE COUNT FROM 1 COZ THE 0TH INDEX IS OF TITLE & WE DONT WANT TITLE TO BIND
        For _inc1 As Int32 = 1 To l_ArrayList.Count - 1

            With dgItemList
                .Rows.Add()
                .Item(0, .Rows.Count - 1).Value = l_ArrayList.Item(_inc1).ControlType.GetHashCode
                '.Item(1, .Rows.Count - 1).Value = CType(l_ArrayList.Item(_inc1), myList).AssociatedItem
                .Item(1, .Rows.Count - 1).Value = CType(l_ArrayList.Item(_inc1), myList).Description
                If l_ArrayList.Item(_inc1).ControlType = 71 Then
                    .Item(2, .Rows.Count - 1).Value = "CheckBox"
                    .Item(0, .Rows.Count - 1).Value = 1
                Else
                    .Item(2, .Rows.Count - 1).Value = "Text"
                    .Item(0, .Rows.Count - 1).Value = 2
                End If

                If CType(l_ArrayList.Item(_inc1), myList).AssociatedCategory <> "" Then
                    .Item(3, .Rows.Count - 1).Value = CType(l_ArrayList.Item(_inc1), myList).AssociatedCategory
                    .Columns(3).Visible = True
                    chkAssociatestddata.Checked = True
                    chkAssociatestddata.Visible = True
                    pnlassociateStdItem.Visible = True

                Else
                    .Columns(3).Visible = False
                    chkAssociatestddata.Checked = False
                    chkAssociatestddata.Visible = False
                    pnlassociateStdItem.Visible = False
                End If

            End With
        Next
        pnlEdit.Visible = True
        txtField.Focus()
    End Sub
    Public Sub FillGrid()

    End Sub
    ''' <summary>
    ''' To Delete dataField from the Liquid Data Dictionary
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteData()
        Try
            trvDiscrete.Focus()
            If trvDiscrete.SelectedNode Is Nothing Then
                Exit Sub
            End If
            If trvDiscrete.SelectedNode.Level = 0 Then
                Exit Sub
            End If
            Dim myNode As myTreeNode
            myNode = CType(trvDiscrete.SelectedNode, myTreeNode)
            ''set the elementId  for further processing amd make panel visible based on user selction for edit/view
            m_ElementId = myNode.Key
            'pnlview.Visible = True
            pnlEdit.Visible = False
            ''Ask for user confirmation before deleting the Datafield
            If MessageBox.Show("Do you want to delete selected DataField?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                objTemplate.DeleteDataField(m_ElementId, True)
                FillLiquidData()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    ''' <summary>
    ''' To reset the Form to initial stage
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshData()
        FillLiquidData()

        'pnlview.Visible = True
        ' 20091221 Commented by Shubhangi
        'pnlEdit.Visible = False
    End Sub

    Private Sub trvDiscrete_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDiscrete.NodeMouseClick
        Try
            ''Select the node in the treeview to make default selection
            trvDiscrete.SelectedNode = e.Node

            'MsgBox(e.Node.Text)
            ''If the user pressed mouse right button
            If e.Button = Windows.Forms.MouseButtons.Right Then
                ''If the selected node should be available
                If Not IsNothing(trvDiscrete.SelectedNode) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CntData)
                    CntData.MenuItems.Clear()
                    'Try
                    '    If (IsNothing(trvDiscrete.ContextMenu) = False) Then
                    '        trvDiscrete.ContextMenu.Dispose()
                    '        trvDiscrete.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvDiscrete.ContextMenu = CntData
                    Dim oMenuItem As MenuItem
                    ' Dim oChildItem As MenuItem
                    ''If selected node is parent node then user should enable for add only
                    If trvDiscrete.Nodes.Item(0) Is trvDiscrete.SelectedNode Then
                        ''Add Context menu
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Add DataField"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        AddHandler oMenuItem.Click, AddressOf SetMenus

                        CntData.MenuItems.Add(oMenuItem)
                    Else
                        ''User is enabled for add/modify/delete

                        ''Add Context menu
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Add DataField"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        AddHandler oMenuItem.Click, AddressOf SetMenus
                        CntData.MenuItems.Add(oMenuItem)

                        ''Modify Context menu
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Modify DataField"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        AddHandler oMenuItem.Click, AddressOf SetMenus
                        CntData.MenuItems.Add(oMenuItem)

                        ''Delete Context menu
                        oMenuItem = New MenuItem
                        With oMenuItem
                            .Text = "Delete DataField"
                            .Shortcut = Shortcut.CtrlShiftE
                            .ShowShortcut = False
                        End With
                        AddHandler oMenuItem.Click, AddressOf SetMenus
                        CntData.MenuItems.Add(oMenuItem)

                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvDiscrete_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDiscrete.NodeMouseDoubleClick
        Try

            'shubhangi 20091012 MSG box display
            Dim SelectedLiquid As myTreeNode = trvDiscrete.SelectedNode
            If m_ElementId = CType(e.Node, myTreeNode).Key Then
                MsgBox("Select record to modify", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                Exit Sub
            End If
            If dgItemList.RowCount > 0 Or dgTableField.RowCount > 0 Or cmbDataType.Text = "Text" Then


                If MessageBox.Show("Open record to modify looses selected values. Do you want to Save it? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    blnModify = True
                    btnSave_Click(sender, e)
                    trvDiscrete.SelectedNode = e.Node ''SelectedLiquid

                    'Else
                    '    trvDiscrete.SelectedNode = 
                    '    ModifyData()
                    '    If cmbDataType.Text = "Boolean" And dgItemList.RowCount = 2 Then
                    '        txtItem.Enabled = False
                    '    Else
                    '        txtItem.Enabled = True
                    '    Enidd If
                End If
            End If
            ''Select the node in the treeview to make default selection

            ' m_ElementId = 0
            ''To modify the data
            ' If trvDiscrete.SelectedNode.Text <> "Liquid Data" Then
            blnModify = True


            trvDiscrete.SelectedNode = e.Node ''SelectedLiquid
            If IsNothing(e.Node) = False Then
                ModifyData(e.Node)
            End If

            'Shubhangi 20090918
            'For boolean we should enter only two Values
            If cmbDataType.Text = "Boolean" And dgItemList.RowCount = 2 Then
                txtItem.Enabled = False
            Else
                txtItem.Enabled = True
            End If
            '''''Integrated by Mayuri:20100731- for making radiobuttons visible ''''
            If cmbDataType.Text.Trim <> "Choose an item" And cmbFieldCategory.Text.Trim = "HPI" Then
                pnlHPIExtended.Visible = True
                RdbtnBrief.Checked = False
                RdbtnExtended.Checked = False
            Else
                pnlHPIExtended.Visible = False
            End If
            ''''' Integrated by Mayuri:20100731 - for making radiobuttons visible ''''


            '''' Integrated by Mayuri:20100731 - for splitting text and checking respective radiobutton '''
            If txtField.Text.Contains("Brief") Then
                Dim retval As String() = SplitTextHypen(txtField.Text)
                If Not IsNothing(retval) Then
                    If retval.Length > 1 Then
                        txtField.Text = retval(0)
                        RdbtnBrief.Checked = True
                    End If
                End If
            ElseIf txtField.Text.Contains("Extended") Then
                Dim retval As String() = SplitTextHypen(txtField.Text)
                If Not IsNothing(retval) Then
                    If retval.Length > 1 Then
                        txtField.Text = retval(0)
                        RdbtnExtended.Checked = True
                    End If
                End If
            End If
            ''''' Integrated by Mayuri:20100731 - for splitting text and checking respective radiobutton '''

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''' Integrated by Mayuri:20100731 '''
    Private Function SplitTextHypen(ByVal NodeValue As String) As Array
        Try
            Dim _result As String()
            _result = NodeValue.Split("(")
            Return _result
        Catch ex As Exception
            Return Nothing
        End Try
        ''''' Integrated by Mayuri:20100731 '''
    End Function

    ''' <summary>
    ''' To implement the context menus of the treeview based on user selection
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        Try
            Select Case oCurrentMenu.Text
                Case "Add DataField"
                    AddData()
                Case "Modify DataField"
                    If IsNothing(trvDiscrete.SelectedNode) = False Then
                        ModifyData(trvDiscrete.SelectedNode)
                    End If
                Case "Delete DataField"
                    DeleteData()
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCurrentMenu = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' To clear the values of all controls in the Edit panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetEditPanel()
        ''Reset all the controls
        txtField.Text = String.Empty

        txtItem.Text = String.Empty
        ''chckRequired.Checked = False
        ''Clear the items in DataGrid
        dgItemList.Rows.Clear()
        ''Make Edit Buttons panel and Field values panel visible true
        pnlFieldValues.Visible = True
        ''pnlBtns.Visible = True
        dgTableField.Rows.Clear()
        txtcategory.Text = ""
        txtCatItem.Text = ""
        txtItem.Text = ""
        CmbControl.SelectedIndex = 0
        cmbFieldCategory.SelectedIndex = 0
        cmbDataType.SelectedIndex = 0
        txtCaption.Text = ""
        chckRequired.Checked = False
        txtItem.Enabled = True
        txtcategory.Enabled = True
        txtCaption.Enabled = True
        txtCatItem.Enabled = True
        CmbControl.Enabled = True
        ToolStripButton1.Enabled = True
        ToolStripButton2.Enabled = True
        If Panel5.Visible = False Then
            Panel5.Visible = True
        End If


    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        ''Check for Validating the Field description and Datatype Selection

        Try

            If txtField.Text.Trim = "" Then
                MsgBox("Data description field cannot be empty.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                txtField.Focus()
                Exit Sub
            End If
            If cmbDataType.SelectedIndex = 0 Then
                MsgBox("Select the field type for the field discription.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                cmbDataType.Focus()
                Exit Sub
            End If

            If cmbFieldCategory.SelectedIndex = 0 Then
                MsgBox("Select the category for the data Field.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                cmbFieldCategory.Focus()
                Exit Sub
            End If
            'Shubhangi 20091014
            'Check whether record is open for modify or not
            If blnModify = True Then

                If dgItemList.RowCount = 0 And dgTableField.RowCount = 0 And cmbDataType.Text <> "Text" Then
                    MsgBox("Enter the field type.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    Exit Sub
                End If
            ElseIf blnModify = False Then
                If dgItemList.RowCount = 0 And dgTableField.RowCount = 0 And cmbDataType.Text <> "Text" Then
                    MsgBox("Enter the item fields.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    Exit Sub
                End If
            End If
            'If CmbControl.SelectedIndex = 0 And (cmbDataType.Text = "Multiple Selection" Or cmbDataType.Text = "Table" Or cmbDataType.Text = "Group") And blnModify = False Then
            '    MsgBox("Please select the control type for the DataField", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
            '    CmbControl.Focus()
            '    Exit Sub
            'End If
            ''check for Duplicate data field for the same data type
            If objTemplate.IsDuplicateField(txtField.Text.Trim, cmbDataType.Text, blnModify) Then
                MsgBox("DataField with the same Description is already exists.", MsgBoxStyle.Information, gstrMessageBoxCaption)
                txtField.Focus()
                Exit Sub

            End If
            ''check for boolean datatype and the items contain more than two items
            If (cmbDataType.Text = "Boolean") And (dgItemList.Rows.Count > 2) Then
                MessageBox.Show("Boolean data field accepts two field values only.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If
            If (cmbDataType.Text = "Group") Then
                If txtCaption.Text = "" Then
                    MsgBox("Enter caption for the group.", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    txtCaption.Focus()
                    Exit Sub
                End If
            End If

            ''Clear  the arraylist before adding
            If m_arrList Is Nothing Then
                m_arrList = New ArrayList
            Else
                m_arrList.Clear()
            End If

            ''Add the list items into arraylist for further purpose
            If cmbDataType.Text = "Table" Or cmbDataType.Text = "Group" Then
                For i As Integer = 0 To dgTableField.Rows.Count - 1
                    mylist = New myList
                    If dgTableField.Item(1, i).Value <> "" Then
                        mylist.HistoryCategory = dgTableField.Item(2, i).Value.ToString
                        mylist.HistoryItem = dgTableField.Item(1, i).Value.ToString
                        mylist.ControlType = CType(dgTableField.Item(3, i).Value, ControlType)
                        mylist.AssociatedCategory = dgTableField.Item(7, i).Value.ToString
                        mylist.AssociatedItem = dgTableField.Item(6, i).Value.ToString
                        mylist.AssociatedProperty = dgTableField.Item(8, i).Value.ToString
                        m_arrList.Add(mylist)
                    End If
                Next
            ElseIf cmbDataType.Text = "Multiple Selection" Then
                ''Sudhir 20090204

                'Shubhangi 20090403'
                ' AxFramerControl1.Open("document")
                'Commented on 05/04
                For i As Integer = 0 To dgItemList.Rows.Count - 1
                    'For i As Integer = 1 To dgItemList.Rows.Count

                    mylist = New myList
                    If dgItemList.Item(1, i).Value <> "" Then

                        mylist.Value = dgItemList.Item(1, i).Value.ToString

                        mylist.ControlType = CType(dgItemList.Item(0, i).Value, ControlType)
                        If Not IsNothing(dgItemList.Item(3, i).Value) Then
                            mylist.AssociatedProperty = dgItemList.Item(3, i).Value.ToString
                        Else
                            mylist.AssociatedProperty = ""
                        End If
                        m_arrList.Add(mylist)

                    End If
                Next


            Else
                'Shubhangi 20091208
                'Change the condition that dgItemList.rows.Count < 1 coz it  is dgItemList
                If dgItemList.Rows.Count <= 0 Then
                    For i As Int32 = 0 To dgItemList.Rows.Count - 1
                        mylist = New myList
                        mylist.Value = dgItemList.Item(1, i).Value.ToString
                        If Not IsNothing(dgItemList.Item(3, i).Value) Then
                            mylist.AssociatedProperty = dgItemList.Item(3, i).Value.ToString
                        Else
                            mylist.AssociatedProperty = ""
                        End If
                        m_arrList.Add(mylist)
                    Next
                Else
                    For i As Int32 = 0 To dgItemList.Rows.Count - 1
                        'mylist.Value = dgItemList.Item(1, i).Value.ToString
                        m_arrList.Add(dgItemList.Item(1, i).Value.ToString)
                    Next
                End If

            End If

            If blnModify And m_ElementId <> 0 And IsfrmExam = False Then

                objTemplate.DeleteDataField(m_ElementId, True)
                blnModify = False

            ElseIf IsfrmExam = True Then
                'objTemplate.DeleteDataField(m_ElementId, False)

                blnModify = False

            End If

            If cmbFieldCategory.SelectedIndex = 0 Then
                strCategoryType = Convert.ToString(CategoryType.None.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 1 Then
                strCategoryType = Convert.ToString(CategoryType.General.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 2 Then
                strCategoryType = Convert.ToString(CategoryType.Hitory.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 3 Then
                strCategoryType = Convert.ToString(CategoryType.Physical_Examination.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 4 Then
                strCategoryType = Convert.ToString(CategoryType.Medical_Decision_Making.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 5 Then
                strCategoryType = Convert.ToString(CategoryType.HPI.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 6 Then
                strCategoryType = Convert.ToString(CategoryType.Management_option.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 7 Then
                strCategoryType = Convert.ToString(CategoryType.Labs.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 8 Then
                strCategoryType = Convert.ToString(CategoryType.X_Ray_Radiology.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 9 Then
                strCategoryType = Convert.ToString(CategoryType.Other_Diagonsis_Tests.GetHashCode())
            ElseIf cmbFieldCategory.SelectedIndex = 10 Then
                strCategoryType = Convert.ToString(CategoryType.ROS.GetHashCode())
            End If

            ''''' Integrated by Mayuri:20100731 - To concatenate radiobutton text with txtfield text ''''
            Dim str1 As String = ""
            Dim str2 As String = ""
            If RdbtnBrief.Checked Then
                str1 = RdbtnBrief.Text.Trim.ToString()
                ''str2 = str1 + "-" + txtField.Text.Trim.ToString()
            ElseIf RdbtnExtended.Checked Then
                str1 = RdbtnExtended.Text.Trim.ToString()
            End If
            str2 = txtField.Text.Trim.ToString() + "(" + str1 + ")"
            ''''' Integrated by Mayuri:20100731 - To concatenate radiobutton text with txtfield text ''''

            'Dim objdata As DataTable = objTemplate.GetDataField(m_ElementId)
            'Dim dvData As DataView = objdata.DefaultView
            'dvData.RowFilter = "Where"
            ''Add the Data Field into DB
            If IsfrmExam = False Then
                'AddDataField(txtField.Text.Trim, cmbDataType.Text, strCategoryType, txtItem.Text.Trim, m_arrList)
                'IsModified = False
                If RdbtnBrief.Checked Or RdbtnExtended.Checked Then
                    AddDataField(str2, cmbDataType.Text, strCategoryType, txtItem.Text.Trim, m_arrList)
                Else
                    AddDataField(txtField.Text.Trim, cmbDataType.Text, strCategoryType, txtItem.Text.Trim, m_arrList)
                End If

            Else
                _ElementID = m_ElementId
            End If
            '' And refresh the datadictionary
            FillLiquidData()
            pnlEdit.Visible = False
            ResetEditPanel()
            _FormField = CType(m_arrList.Clone(), ArrayList)
            'pnlview.Visible = True
            'm_ElementId = 0
            blnModify = False
            If IsfrmExam = True Then
                arrFormField = CType(_FormField.Clone(), ArrayList)
                FillTemplate()

                RefillTable()
                If Not IsNothing(oLiquiddoc) Then

                    'oLiquiddoc.SaveAs(strName1, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)


                    'wdTemp.Close()
                    Dim thisAPP As Wd.Application = oLiquiddoc.Application

                    Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdTemp, oLiquiddoc, thisAPP, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                    Dim myBinaray As Object = Nothing
                    If (IsNothing(myByte) = False) Then
                        myBinaray = CType(myByte, Object)
                    End If

                    Dim strsql As String = ""
                    Dim result As String = ""
                    Dim objTemplate As New clsTemplateGallery
                    result = objTemplate.CategoryName(_CategoryID)
                    objTemplate.AddNewTemplateGalleryBytes(m_Id, _TemplateName, _CategoryID, result, 0, myBinaray)
                    objTemplate.Dispose()
                    objTemplate = Nothing

                    If (IsNothing(oLiquiddoc) = False) Then
                        Try
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oLiquiddoc)
                        Catch ex As Exception


                        End Try
                        oLiquiddoc = Nothing

                    End If

                End If
                Me.Close()
            End If
            'Shubhangi
            pnl_ToolStrip.Visible = True

        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub FillTemplate()
        Try

            'Dim objfile As New String
            Dim ObjTemplate As New clsTemplateGallery
            Dim objFileName As New clsWordDocument
            Dim _TemplateID As Int64
            'Dim strname1 As String
            'Dim m_Id As Int64

            Dim dv As New DataView


            _TemplateID = ObjTemplate.GetTemplate(m_TemplateName)
            If _TemplateID > 0 Then
                'ObjTemplate.SelectTemplateGallery(_TemplateID)
                dv = ObjTemplate.GetDataview

                If dv IsNot Nothing Then
                    If dv.ToTable.Rows.Count > 0 Then

                        _TemplateName = dv.Item(0)(0).ToString

                        _CategoryID = dv.Item(0)(1)

                        'Give path to the file through function which usese Tme date code'

                        strName1 = ExamNewDocumentName
                        Dim ObjWord As New clsWordDocument

                        'Generate physical file by calling function GenerateFile function '
                        strName1 = objFileName.GenerateFile(dv.Item(0)(3), strName1)

                        'wdTemp.Open(strName1)
                        Dim thisApplication As Wd.Application = Nothing
                        Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdTemp, strName1, oLiquiddoc, thisApplication)
                        If (strError <> String.Empty) Then
                            MessageBox.Show(strError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            If Not IsNothing(wdTemp) Then
                                oLiquiddoc = wdTemp.ActiveDocument
                            End If
                        End If
                        'thisApplication = Nothing

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefillTable()
        Try

            Dim objclsTemplateGallery As New clsTemplateGallery
            Dim oData As DataTable = objclsTemplateGallery.GetDataField(m_ElementId)

            m_arraylist = Nothing

            If Not oData Is Nothing Then
                m_FieldList = New SortedList
                m_arraylist = New ArrayList
                For _inc As Int32 = 0 To oData.Rows.Count - 1
                    ''set the Values to gobal variables for further processing
                    m_Required = oData.Rows(0)("bIsMandatory")
                    m_DataType = oData.Rows(_inc)("sElementType")
                    If oData.Rows(_inc)("nElementID") = NewEMFieldID Then
                        m_Desc = oData.Rows(_inc)("sElementName").ToString
                        m_Fieldcategory = oData.Rows(_inc)("sCategoryName").ToString
                    Else
                        If m_DataType = "Text" Then
                            m_TextValue = oData.Rows(_inc)("sElementName").ToString
                        ElseIf m_DataType = "Table" Or m_DataType = "Group" Then
                            m_list = New myList
                            m_list.ID = oData.Rows(_inc)("nElementID")
                            m_list.HistoryCategory = oData.Rows(_inc)("sCategoryName").ToString()
                            m_list.HistoryItem = oData.Rows(_inc)("sItemName").ToString()
                            m_list.ControlType = CType(oData.Rows(_inc)("nControlType"), ControlType)
                            m_list.AssociatedCategory = CType(oData.Rows(_inc)("sAssociatedCategory"), String)
                            m_list.AssociatedItem = CType(oData.Rows(_inc)("sAssociateditem"), String)
                            m_list.AssociatedProperty = CType(oData.Rows(_inc)("sAssociatedProperty"), String)
                            m_Caption = oData.Rows(_inc)("sElementName")
                            m_arraylist.Add(m_list)
                        Else
                            m_list = New myList
                            m_list.ID = oData.Rows(_inc)("nElementID")
                            m_list.HistoryItem = oData.Rows(_inc)("sElementName")
                            m_list.ControlType = CType(oData.Rows(_inc)("nControlType"), ControlType)
                            m_list.AssociatedProperty = CType(oData.Rows(_inc)("sAssociatedProperty"), String)
                            m_arraylist.Add(m_list)
                            ''m_FieldList.Add(oData.Rows(_inc)("nElementID").ToString, oData.Rows(_inc)("sElementName").ToString)
                        End If
                    End If
                Next
                ''Only add the controls in the document if the count greate than one
                If oData.Rows.Count > 0 Then
                    AddLiquidControl()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub AddLiquidControl()
        Select Case m_DataType
            Case "Single Selection"
                InsertDropDown()
            Case "Boolean"
                InsertCheckbox()
            Case "Multiple Selection"
                InsertCheckboxInBooleanFormat()
            Case "Text"
                InsertTextControl()
            Case "Table"
                InsertTable()
            Case "Group"
                InsertGroup() 'In Liquid data project it name is InsertGroupNew()
            Case Else

        End Select
    End Sub
    Private Sub InsertDropDown()
        Try
            If Not IsNothing(m_FieldList) Then
                With oLiquiddoc.Application.Selection
                    'Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                    'If Not IsNothing(cntcontrol) Then
                    '    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                    'End If
                    .Range.ParentContentControl.Delete(True)
                    '.TypeText(Text:=m_Desc & ": ")
                    .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlDropdownList)

                    ''Title with be Field description
                    .ParentContentControl.Title = m_Desc
                    ''Elementid, Required flag  for reference stored in tag and temporary variables
                    .ParentContentControl.Tag = NewEMFieldID & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                    '.ParentContentControl.Temporary = m_Required
                    .ParentContentControl.DropdownListEntries.Clear()
                    ''Loop thourgh each entry in hash table and add as drop down items 
                    'For Each objEntry As DictionaryEntry In m_FieldList
                    '    .ParentContentControl.DropdownListEntries.Add(Text:=objEntry.Value.ToString, Value:=objEntry.Key.ToString)
                    'Next
                    For Each objEntry As myList In m_arraylist
                        .ParentContentControl.DropdownListEntries.Add(Text:=objEntry.HistoryItem.ToString, Value:=objEntry.ID.ToString)
                    Next
                    .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                    .InsertParagraph()
                    .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                End With
                oLiquiddoc.ActiveWindow.SetFocus()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertDropDown, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oLiquiddoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oLiquiddoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
        End Try

    End Sub
    Private Sub InsertCheckbox()
        Try

            If Not IsNothing(m_arraylist) Then

                With oLiquiddoc.Application.Selection
                    ''Add the rich text Content control
                    .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                    ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                    ''Title will be Field description
                    .ParentContentControl.Title = m_Desc
                    ''Elementid, Required flag  for reference stored in tag and temporary variables
                    .ParentContentControl.Tag = NewEMFieldID & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory

                    If m_arraylist.Count > 1 Then
                        '  .ParentContentControl.Temporary = m_Required
                        ''Get Table rows and column count based on the available items
                        GetRCTable(m_arraylist.Count)
                        ''for proper alignment and formatiing,  insert the items in a table
                        'Dim t1Check As Wd.Table = .Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=objDefaultBehaviorWord8, AutoFitBehavior:=objAutoFitFixed)
                        Dim t1Check As Wd.Table = .Tables.Add(oLiquiddoc.Application.Selection.Range, 1, noCols, objDefaultBehaviorWord8, objAutoFitFixed)

                        PopulateAndExtendTableCheckbox(t1Check)
                        With t1Check
                            'If .Style <> "Table Grid" Then
                            .Style = "Table Grid"
                            'End If
                            .ApplyStyleHeadingRows = True
                            .ApplyStyleLastRow = False
                            .ApplyStyleFirstColumn = True
                            .ApplyStyleLastColumn = False
                            .ApplyStyleRowBands = True
                            .ApplyStyleColumnBands = False
                            .Borders(Wd.WdBorderType.wdBorderLeft).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderRight).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderTop).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderHorizontal).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle 'Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderDiagonalDown).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                            .Borders(Wd.WdBorderType.wdBorderDiagonalUp).LineStyle = Wd.WdLineStyle.wdLineStyleNone
                            .Borders.Shadow = False
                        End With
                    Else
                        For Each objEntry As myList In m_arraylist
                            Dim oNameField As Wd.FormField
                            oNameField = .FormFields.Add(.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = objEntry.HistoryItem.ToString
                            oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                            .Text = objEntry.HistoryItem.ToString
                            oNameField.CheckBox.Value = False
                            oNameField = Nothing
                        Next
                        oLiquiddoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
                        oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                        oLiquiddoc.Application.Selection.InsertParagraph()
                        oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                        '.Move(Wd.WdUnits.wdLine, Count:=1)
                    End If

                End With
                oLiquiddoc.ActiveWindow.SetFocus()
                '  oCurDoc.Application.Selection.Next()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub InsertCheckboxInBooleanFormat()
        Try
            selectedField = New ArrayList
            Dim strTag() As String
            Dim strFieldHeader As String = ""
            Dim found As Boolean = False
            'oLiquiddoc = AxFramerControl1

            If Not IsNothing(oLiquiddoc) Then
                With oLiquiddoc.Application.Selection
                    For Each _cntcontrol As Wd.ContentControl In oLiquiddoc.ContentControls    'count no of content control
                        strTag = Split(_cntcontrol.Tag, "|")
                        If strTag.GetValue(0) = Convert.ToString(GetElement) And strTag.Length <= 4 And _cntcontrol.Title <> "" Then
                            For Each t1 As Wd.Table In _cntcontrol.Range.Tables 'count of Content control contain no fo tables 
                                For nrow As Integer = t1.Rows.Count To 2 Step -1 'count of no of rows of tables in word document
                                    Dim ncol As Integer = 1
                                    If (t1.Columns.Count >= 1) Then
                                        strFieldHeader = t1.Cell(nrow, ncol).Range.Text   'Specify a range that is the insertion point at the beginning of a document and inserts the text New Text (note the spaces) at the insertion point. 

                                    End If
                                    found = False
                                    'arrFormField THIS ARRAY LIST CONTAIN THE UPDATED RECORDS
                                    For i As Integer = arrFormField.Count - 1 To 0 Step -1  'conut of no of rows inthe dgitemlist
                                        If strFieldHeader.Contains(CType(arrFormField.Item(i), myList).Value) = True Then     'Check whether we delete some field or not
                                            If (t1.Columns.Count >= 2) Then
                                                If t1.Cell(nrow, 2).Range.FormFields.Count > 0 And CType(CType(arrFormField.Item(i), myList).ControlType, ControlType) = ControlType.CheckBox Then
                                                    arrFormField.RemoveAt(i)
                                                    found = True
                                                    Exit For
                                                End If
                                            End If
                                           
                                        End If

                                        If strFieldHeader.Contains(CType(arrFormField.Item(i), myList).Value) = True Then
                                            If (t1.Columns.Count >= 2) Then
                                                If t1.Cell(nrow, 2).Range.ContentControls.Count > 0 And CType(CType(arrFormField.Item(i), myList).ControlType, ControlType) = ControlType.Text Then
                                                    arrFormField.RemoveAt(i)
                                                    found = True
                                                    Exit For
                                                End If
                                            End If
                                          
                                        End If
                                    Next

                                    'THIS FLAG IS USE WHETHER RECORD IS MODIFIED OR NOT IF NOT MODIFIED THEN DELETE FROM THAT TABLE IN TEMPLATE .
                                    If found = False Then
                                        t1.Rows(nrow).Delete()
                                    End If
                                Next
                            Next

                            'when we add new element into dgitemlist at the time of modify then that newly added 
                            'rows are added into the list

                            For Each t1 As Wd.Table In _cntcontrol.Range.Tables
                                _cntcontrol.Range.Select()
                                'oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdTable, Count:=1)
                                'THIS ARRAY LIST arrFormField CONTAINS ONLY UPDATED RECORDS
                                For i As Integer = 0 To arrFormField.Count - 1
                                    Dim nRow As Integer
                                    nRow = t1.Rows.Count
                                    t1.Rows.Add(objMissing)  '''' new Row
                                    nRow = t1.Rows.Count
                                    'oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow, Count:=nRow) '''' Move Cursor to Newly Added Row in table

                                    Dim oNameField As Wd.FormField
                                    If (t1.Columns.Count >= 1) Then
                                        t1.Cell(nRow, 1).Application.Selection.Move(Wd.WdUnits.wdRow, Count:=nRow)
                                        t1.Cell(nRow, 1).Application.Selection.Select()
                                    End If
                                 
                                    oLiquiddoc.Application.Selection.SelectCell()

                                    't1.Cell(nRow, 1).Application.Selection.Select()
                                    't1.Cell(nRow, 1).Application.Selection.SelectCell()
                                    'oLiquiddoc.Application.Selection.SelectCell()

                                    'HERE WE ARE ADDING THAT UPDATED RECORD INTO THAT TABLE OF THE TEMPLATE
                                    If CType(CType(arrFormField.Item(i), myList).ControlType, ControlType) = ControlType.CheckBox Then
                                        'oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow, Count:=1)
                                        If (t1.Columns.Count >= 1) Then
                                            t1.Cell(nRow, 1).Application.Selection.Text = CType(arrFormField.Item(i), myList).Value
                                        End If

                                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)

                                        oLiquiddoc.Application.Selection.Select()
                                        If (t1.Columns.Count >= 2) Then
                                            t1.Cell(nRow, 2).Application.Selection.Select()

                                            oNameField = t1.Cell(nRow, 2).Application.Selection.FormFields.Add(t1.Cell(nRow, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                                            oNameField.HelpText = NewEMFieldID & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty  ''NewEMFieldID 'Convert.ToString(NewEMFieldID) 
                                            oNameField.StatusText = CType(arrFormField.Item(i), myList).Value
                                            t1.Cell(nRow, 2).Application.Selection.Text = " Yes"
                                        End If
                                       

                                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)
                                        oLiquiddoc.Application.Selection.Select()
                                        If (t1.Columns.Count >= 3) Then
                                            oNameField = t1.Cell(nRow, 3).Application.Selection.FormFields.Add(t1.Cell(nRow, 3).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                                            oNameField.StatusText = CType(arrFormField.Item(i), myList).Value
                                            oNameField.HelpText = NewEMFieldID & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty  ''NewEMFieldID 'Convert.ToString(NewEMFieldID) 
                                            t1.Cell(nRow, 3).Application.Selection.Text = " No"
                                        End If
                                      

                                    ElseIf CType(CType(arrFormField.Item(i), myList).ControlType, ControlType) = ControlType.Text Then
                                        If (t1.Columns.Count >= 1) Then
                                            t1.Cell(nRow, 1).Application.Selection.Text = CType(arrFormField.Item(i), myList).Value

                                        End If
                                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)
                                        't1.Cell(nRow, 2).Application.Selection.SelectCell()
                                        oLiquiddoc.Application.Selection.Select()
                                        If (t1.Columns.Count >= 2) Then
                                            oControl = t1.Cell(nRow, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)

                                            t1.Cell(nRow, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                                            Dim tmpstr As String = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty
                                            t1.Cell(nRow, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty

                                        End If
                                                                               't1.Cell(nRow, 2).Application.Selection.Range.ParentContentControl.
                                        ''Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText.
                                    Else
                                        If (t1.Columns.Count >= 1) Then
                                            t1.Cell(nRow, 1).Application.Selection.Text = CType(arrFormField.Item(i), myList).Value

                                        End If
                                         oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)

                                        oLiquiddoc.Application.Selection.Select()
                                        't1.Cell(nRow, 2).Application.Selection.SelectCell()
                                        't1.Cell(nRow, 2).Application.Selection.SelectCell()
                                        If (t1.Columns.Count >= 2) Then
                                            oNameField = t1.Cell(nRow, 2).Application.Selection.FormFields.Add(t1.Cell(nRow, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                                            oNameField.HelpText = NewEMFieldID & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty  ''NewEMFieldID 'Convert.ToString(NewEMFieldID) 
                                            oNameField.StatusText = CType(arrFormField.Item(i), myList).Value
                                            't1.Cell(nRow, 2).Select()
                                            t1.Cell(nRow, 2).Application.Selection.Text = " Yes"

                                        End If
                                      

                                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCell, Count:=1)
                                        oLiquiddoc.Application.Selection.Select()
                                        If (t1.Columns.Count >= 3) Then
                                            oNameField = t1.Cell(nRow, 3).Application.Selection.FormFields.Add(t1.Cell(nRow, 3).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                                            oNameField.HelpText = NewEMFieldID & "|" & CType(arrFormField.Item(i), myList).AssociatedProperty ''NewEMFieldID 'Convert.ToString(NewEMFieldID) 
                                            oNameField.StatusText = CType(arrFormField.Item(i), myList).Value
                                            t1.Cell(nRow, 3).Application.Selection.Text = " No"
                                        End If
                                       

                                    End If
                                Next
                            Next 'Table 
                        End If
                    Next 'Content control 
                    'C

                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertCheckBox, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub InsertTextControl()
        Try
            With oLiquiddoc.Application.Selection
                'Dim cntcontrol As Wd.ContentControl = oLiquiddoc.Application.Selection.Range.ParentContentControl
                'If Not IsNothing(cntcontrol) Then
                '    oLiquiddoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                'End If
                ''Type the caption for the Text Datafield
                .TypeText(Text:=m_Desc & ": ")
                .Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)


                .ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText

                '.Range.ContentControls(1).DefaultTextStyle = oCurDoc.Application.Selection.Style
                ''Title will be Field description
                .ParentContentControl.Title = m_Desc
                ''Elementid, Required flag  for reference stored in tag and temporary variables
                .ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                ' .ParentContentControl.Temporary = m_Required

                If m_TextValue <> "" Then
                    ''Type the text int he Richtext control
                    .TypeText(Text:=m_TextValue)
                End If

                ''move cursor out of the rich text content control
                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
                .InsertParagraph()
                .MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            End With
            oLiquiddoc.ActiveWindow.SetFocus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oLiquiddoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oLiquiddoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
        End Try
    End Sub

    Private Sub InsertTable()
        Try
            If Not IsNothing(m_arraylist) Then
                If m_arraylist.Count > 0 Then
                    With oLiquiddoc.Application.Selection
                        'Dim cntcontrol As Wd.ContentControl = oCurDoc.Application.Selection.Range.ParentContentControl
                        'If Not IsNothing(cntcontrol) Then
                        '    oCurDoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        'End If
                        'oCurDoc.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdNormalView
                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = 2
                        ''Add the rich text Content control
                        oLiquiddoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                        ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                        ''Title will be Field description
                        oLiquiddoc.Application.Selection.ParentContentControl.Title = m_Desc

                        ''oCurDoc.Application.Selection.ParentContentControl.Temporary
                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        oLiquiddoc.Application.Selection.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                        Dim wdRng As Wd.Range = oLiquiddoc.Application.Selection.Range
                        CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendTable(tb1) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateTableStyle()
                            FormatTables(style, tb1)
                        End If
                    End With
                    oLiquiddoc.ActiveWindow.SetFocus()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            oLiquiddoc.Application.Selection.InsertParagraph()
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
        End Try
    End Sub
    Private Sub InsertGroup()
        Try
            If Not IsNothing(m_arraylist) Then
                If m_arraylist.Count > 0 Then
                    With oLiquiddoc.Application.Selection

                        Dim cntcontrol As Wd.ContentControl = oLiquiddoc.Application.Selection.Range.ParentContentControl
                        If Not IsNothing(cntcontrol) Then
                            oLiquiddoc.Application.Selection.Move(Unit:=Wd.WdUnits.wdSection, Count:=1)
                        End If
                        'oCurDoc.ActiveWindow.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdNormalView

                        ''''Create Basic Table
                        Dim nrRows As Integer = 1
                        Dim nrCols As Integer = 2
                        ''Add the rich text Content control
                        oLiquiddoc.Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                        ' .ParentContentControl.DefaultTextStyle = oCurDoc.Application.Selection.Style
                        ''Title will be Field description
                        oLiquiddoc.Application.Selection.ParentContentControl.Title = m_Desc

                        ''oCurDoc.Application.Selection.ParentContentControl.Temporary

                        ''Elementid, Required flag  for reference stored in tag and temporary variables
                        oLiquiddoc.Application.Selection.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                        Dim wdRng As Wd.Range = oLiquiddoc.Application.Selection.Range
                        CreateIntro(wdRng, m_Desc)
                        '.Tables.Add(Range:=oCurDoc.Application.Selection.Range, NumRows:=noRows, NumColumns:=noCols, DefaultTableBehavior:=Wd.WdDefaultTableBehavior.wdWord9TableBehavior, AutoFitBehavior:=Wd.WdAutoFitBehavior.wdAutoFitFixed)
                        Dim tb1 As Wd.Table = wdRng.Tables.Add(wdRng, nrRows, nrCols, objDefaultBehaviorWord8, objAutoFitFixed)
                        If PopulateAndExtendTableNew(tb1) Then
                            'wdRng = CreateSpaceAfterTable(tb1)
                            Dim style As Wd.Style = CreateTableStyle()
                            FormatTables(style, tb1)
                        End If
                    End With
                    oLiquiddoc.ActiveWindow.SetFocus()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            oLiquiddoc.Application.Selection.InsertParagraph()
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
        End Try
    End Sub
    Private Sub GetRCTable(ByVal _cnt As Int32)
        If _cnt > 0 Then
            ''By default assuming columns count as 4 for proper formatting if sub items count is greater than 2
            If _cnt >= 3 Then
                noCols = 4
                Dim _Rem As Int32
                ''set the Rows count based on the no of sub items
                noRows = Math.DivRem(_cnt, 3, _Rem)
                If _Rem = 0 Then
                Else
                    noRows += 1
                End If
            Else
                ''set the columns count as 3 and rows count as 1, if the no of sub items is less than 3
                noCols = 3
                noRows = 1
            End If
        End If
    End Sub

    Public Function PopulateAndExtendTableCheckbox(ByVal T1ChkBox As Wd.Table) As Boolean
        Try

            '''''Move Cursor to the Table 
            oLiquiddoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            Dim i As Integer = 1
            'SLR: is '0' Allowed on 9/1/2014
            T1ChkBox.Cell(0, 0).Application.Selection.Text = m_Desc
            oLiquiddoc.Application.Selection.MoveRight()
            Dim Reminder As Integer = 0
            Dim Result As Integer = 0
            Dim CurrnetRow As Integer = 0
            For Each objEntry As myList In m_arraylist
                i = i + 1
                Reminder = 0
                System.Math.DivRem(i, 5, Reminder)  ''Math Rem Rem(i, 4, Result)
                If Reminder <> 0 Then

                    If CurrnetRow <> 0 Then
                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                        oLiquiddoc.Application.Selection.MoveRight()
                    Else
                        oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdColumn)
                    End If
                    Dim oNameField As Wd.FormField
                    If ((CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count)) Then
                        T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Select()
                    End If


                    If CType(objEntry.ControlType, ControlType) = ControlType.CheckBox Or CType(objEntry.ControlType, ControlType) = ControlType.None Then
                        If ((CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count)) Then
                            oNameField = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.FormFields.Add(T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = objEntry.HistoryItem.ToString
                            oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Text = objEntry.HistoryItem.ToString
                            oNameField.CheckBox.Value = False
                            oNameField = Nothing
                        End If
                      
                    ElseIf CType(objEntry.ControlType, ControlType) = ControlType.Text Then
                        If ((CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count)) Then
                            oControl = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Tag = objEntry.ID.ToString() & "|" & objEntry.HistoryItem.ToString()
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.TypeText(Text:=objEntry.HistoryItem.ToString)
                        End If
                      

                    End If
                Else
                    CurrnetRow = CurrnetRow + 1
                    T1ChkBox.Rows.Add(objMissing)

                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow)
                    oLiquiddoc.Application.Selection.MoveRight()
                    Dim oNameField As Wd.FormField
                    i = 2
                    If ((CurrnetRow <= T1ChkBox.Rows.Count) And (i <= T1ChkBox.Columns.Count)) Then


                        T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Select()

                        If CType(objEntry.ControlType, ControlType) = ControlType.CheckBox Or CType(objEntry.ControlType, ControlType) = ControlType.None Then
                            oNameField = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.FormFields.Add(T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = objEntry.HistoryItem.ToString
                            oNameField.HelpText = objEntry.ID.ToString & "|" & objEntry.AssociatedProperty.ToString
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Text = objEntry.HistoryItem.ToString
                            'oCurDoc.Application.Selection.MoveDown(Wd.WdUnits.wdLine)
                            oNameField.CheckBox.Value = False
                            oNameField = Nothing
                        ElseIf CType(objEntry.ControlType, ControlType) = ControlType.Text Then
                            oControl = T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.Range.ParentContentControl.Tag = objEntry.ID.ToString() & "|" & objEntry.HistoryItem.ToString()

                            T1ChkBox.Cell(CurrnetRow, i).Application.Selection.TypeText(Text:=objEntry.HistoryItem.ToString)

                        End If

                    End If



                End If
            Next

            oLiquiddoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oLiquiddoc.Application.Selection.InsertParagraph()
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            Return True
            'oCurDoc.Application.Selection.MoveRight()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oLiquiddoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oLiquiddoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function

    Public Sub CreateIntro(ByVal wdRange As Wd.Range, ByVal HeaderText As String)
        Try
            wdRange.Text = HeaderText & vbNewLine

            wdRange.Style = objHeadingStyle1
            wdRange.Collapse(objCollapseEnd)
            wdRange.Style = objNormalStyle
            'wdRange.InsertParagraph()
            wdRange.Collapse(objCollapseEnd)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function PopulateAndExtendTableNew(ByVal tb1 As Wd.Table) As Boolean
        Try

            Dim nrCols As Integer = 2
            Dim nrRows As Integer = 1
            'tb1.Cell(1, 1).Range.Text = "Category"
            'tb1.Cell(1, 2).Range.Text = "Items"
            ''''''Move Cursor to the Table 

            oLiquiddoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            'SLR: is '0' allowed on 9/1/2014

            Dim _oNameField As Wd.FormField
            _oNameField = tb1.Cell(0, 0).Application.Selection.FormFields.Add(tb1.Cell(0, 0).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
            _oNameField.CheckBox.Value = True
            _oNameField.StatusText = m_Caption
            _oNameField.HelpText = "Group"
            _oNameField = Nothing
            tb1.Cell(0, 0).Application.Selection.Text = m_Caption
            oLiquiddoc.Application.Selection.MoveRight()

            ''''''Move Cursor down in the Table
            'oCurDoc.Application.Selection.MoveDown()
            'oCurDoc.Application.Selection.MoveRight()
            ' Dim t_list As myList
            Dim t_categoryName As String
            Dim t_itemName As String
            Dim t_ElementID As String
            Dim t_Control_Type As ControlType
            Dim t_AssociatedCategory As String
            Dim t_AssociatedItem As String
            Dim t_AssoicaitedProperty As String
            For i As Integer = 0 To m_arraylist.Count - 1
                t_categoryName = CType(m_arraylist.Item(i), myList).HistoryCategory.ToString
                t_itemName = CType(m_arraylist.Item(i), myList).HistoryItem.ToString
                t_ElementID = CType(m_arraylist.Item(i), myList).ID.ToString
                t_Control_Type = CType(m_arraylist.Item(i), myList).ControlType
                t_AssociatedCategory = CType(m_arraylist.Item(i), myList).AssociatedCategory
                t_AssociatedItem = CType(m_arraylist.Item(i), myList).AssociatedItem
                t_AssoicaitedProperty = CType(m_arraylist.Item(i), myList).AssociatedProperty
                If i = 0 Then
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oLiquiddoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If


                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then


                        tb1.Cell(nrRows, 2).Application.Selection.Select()

                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty   ''t_ElementID 'Convert.ToString(t_ElementID) 

                        ElseIf t_Control_Type = ControlType.Text Then
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.BuildingBlockCategory = strCatItem
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 

                        End If


                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                    End If
                    'oNameField = Nothing
                ElseIf CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i - 1), myList).HistoryCategory.ToString Then ''''' If the New category The add it in new Row
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oLiquiddoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 1) Then
                        '''' Add Catergory in New Row and category Column
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        Dim strCatItem As String = t_itemName & "-" & t_categoryName
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType

                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If




                        '' Convert.ToString(t_ElementID)
                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If

                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If

                    Else '''' If the category is already add then add Item in the category
                        Dim oNameField As Wd.FormField
                    If (nrRows <= tb1.Rows.Count) And (tb1.Columns.Count >= 2) Then
                        tb1.Cell(nrRows, 2).Application.Selection.Select()

                        Dim strCatItem As String = t_itemName & "-" & t_categoryName
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then

                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)

                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_DataType
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.BuildingBlockCategory = strCatItem
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If
                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToS) '' .MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.NextField() '' .MoveRight(Wd.WdUnits.wdWor, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If

                            End If

                        Else
                            If t_Control_Type <> ControlType.Text Then
                                tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                            End If
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If
                End If


            Next
            If (tb1.Rows.Count >= 1) And (tb1.Columns.Count >= 2) Then
                tb1.Cell(1, 1).Merge(tb1.Cell(1, 2))
            End If


            ''''Move Cursor down in the Table


            oLiquiddoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oLiquiddoc.Application.Selection.InsertParagraph()
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            'oCurDoc.Application.Selection.MoveRight()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oLiquiddoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oLiquiddoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function
    Public Function CreateTableStyle() As Wd.Style
        Dim styleTypeTable As Object = Wd.WdStyleType.wdStyleTypeTable
        Dim StyleName As String = "New Table Style" & Convert.ToString(DateTime.Now)
        Dim styl As Wd.Style = oLiquiddoc.Styles.Add(StyleName, styleTypeTable)
        styl.Font.Name = "Arial"
        styl.Font.Size = 10
        Dim stylTbl As Wd.TableStyle = styl.Table
        stylTbl.Borders.Enable = 1

        Dim evenrowbinding As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdEvenRowBanding)
        evenrowbinding.Shading.Texture = TextureNone
        evenrowbinding.Shading.BackgroundPatternColor = ColorGray10
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical).LineStyle = LineStyleSingle
        evenrowbinding.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom).LineStyle = LineStyleSingle

        Dim FirstRow As Wd.ConditionalStyle = stylTbl.Condition(Microsoft.Office.Interop.Word.WdConditionCode.wdFirstRow)
        'FirstRow.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorWhite
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop).LineStyle = LineStyleDouble
        FirstRow.Borders(Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight).LineStyle = LineStyleDouble
        FirstRow.Font.Size = 14
        FirstRow.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdAuto
        FirstRow.Font.Bold = 1

        stylTbl.RowStripe = 1
        Return styl
    End Function

    Public Sub FormatTables(ByVal tstyle As Wd.Style, ByVal tb1 As Wd.Table)

        'For Each t1 As Wd.Table In oCurDoc.Tables
        Dim objtStyl As Object = CType(tstyle, Object)
        tb1.Range.Style = tstyle
        'Next
    End Sub
    Public Function PopulateAndExtendTable(ByVal tb1 As Wd.Table) As Boolean
        Try

            Dim nrCols As Integer = 2
            Dim nrRows As Integer = 1
            If (tb1.Rows.Count >= 1) And (tb1.Columns.Count >= 1) Then
                tb1.Cell(1, 1).Range.Text = "Category"
            End If

            If (tb1.Rows.Count >= 1) And (tb1.Columns.Count >= 2) Then
                tb1.Cell(1, 2).Range.Text = "Items"
            End If


            '''''Move Cursor to the Table 
            oLiquiddoc.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToTable)

            ''''''Move Cursor down in the Table
            '   Dim t_list As myList
            Dim t_categoryName As String
            Dim t_itemName As String
            Dim t_ElementID As String

            Dim ocontrolLength As Integer
            Dim t_Control_Type As ControlType

            Dim t_AssoicatedCategory As String
            Dim t_AssoicaitedItem As String
            Dim t_AssoicaitedProperty As String
            For i As Integer = 0 To m_arraylist.Count - 1
                t_categoryName = CType(m_arraylist.Item(i), myList).HistoryCategory.ToString

                t_itemName = CType(m_arraylist.Item(i), myList).HistoryItem.ToString
                t_ElementID = CType(m_arraylist.Item(i), myList).ID.ToString
                t_Control_Type = CType(m_arraylist.Item(i), myList).ControlType
                t_AssoicatedCategory = CType(m_arraylist.Item(i), myList).AssociatedCategory
                t_AssoicaitedItem = CType(m_arraylist.Item(i), myList).AssociatedItem
                t_AssoicaitedProperty = CType(m_arraylist.Item(i), myList).AssociatedProperty
                If i = 0 Then
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oLiquiddoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (tb1.Rows.Count >= nrRows) And (tb1.Columns.Count >= 1) Then


                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category 
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    Dim strAssCatItem As String = t_AssoicaitedItem & "-" & t_AssoicatedCategory
                    Dim oNameField As Wd.FormField = Nothing
                    If (tb1.Rows.Count >= nrRows) And (tb1.Columns.Count >= 2) Then



                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem

                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        ElseIf t_Control_Type = ControlType.Text Then
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName

                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl. .DateDisplayFormat = t_AssoicaitedProperty
                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem

                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty  ''t_ElementID 'Convert.ToString(t_ElementID) 
                        End If

                        If t_Control_Type = ControlType.Text Then
                            oNameField.Name = t_itemName
                        End If

                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                    End If
                    'oNameField = Nothing
                ElseIf CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i - 1), myList).HistoryCategory.ToString Then ''''' If the New category The add it in new Row
                    tb1.Rows.Add(objMissing)  '''' new Row
                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdRow) '''' Move Cursor to Newly Added Row in table
                    oLiquiddoc.Application.Selection.MoveRight()
                    nrRows = nrRows + 1
                    '''' Add Catergory in New Row and category Column
                    If (tb1.Rows.Count >= nrRows) And (tb1.Columns.Count >= 1) Then
                        tb1.Cell(nrRows, 1).Range.Text = t_categoryName
                    End If

                    '''' Add Item for Selected category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String = t_itemName & "-" & t_categoryName
                    If (tb1.Rows.Count >= nrRows) And (tb1.Columns.Count >= 2) Then


                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then
                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName

                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        End If


                        '' Convert.ToString(t_ElementID)
                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If
                                'tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                            End If
                        Else
                            tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                        End If
                        'oNameField.CheckBox.Value = False
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If
                Else '''' If the category is already add then add Item in the category
                    Dim oNameField As Wd.FormField
                    Dim strCatItem As String
                    strCatItem = t_itemName & "-" & t_categoryName
                    If (tb1.Rows.Count >= nrRows) And (tb1.Columns.Count >= 2) Then


                        tb1.Cell(nrRows, 2).Application.Selection.Select()
                        'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                        If t_Control_Type = ControlType.CheckBox Then
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)

                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID & "|" & t_AssoicaitedProperty
                        ElseIf t_Control_Type = ControlType.Text Then

                            oControl = tb1.Cell(nrRows, 2).Application.Selection.Range.ContentControls.Add(Wd.WdContentControlType.wdContentControlRichText)
                            ocontrolLength = oControl.Range.Characters.Count
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText
                            ''Title will be Field description
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Title = m_Desc
                            ''Elementid, Required flag  for reference stored in tag and temporary variables
                            'tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory
                            tb1.Cell(nrRows, 2).Application.Selection.Range.ParentContentControl.Tag = m_ElementId & "|" & m_DataType & "|" & m_Required & "|" & m_Fieldcategory & "|" & t_categoryName

                            tb1.Cell(nrRows, 2).Application.Selection.TypeText(Text:=t_itemName)
                            'oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormTextInput)
                        Else
                            oNameField = tb1.Cell(nrRows, 2).Application.Selection.FormFields.Add(tb1.Cell(nrRows, 2).Application.Selection.Range, Wd.WdFieldType.wdFieldFormCheckBox)
                            oNameField.StatusText = strCatItem
                            oNameField.HelpText = t_ElementID
                        End If



                        If i <> m_arraylist.Count - 1 Then
                            If CType(m_arraylist.Item(i), myList).HistoryCategory.ToString <> CType(m_arraylist.Item(i + 1), myList).HistoryCategory.ToString Then
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                                Else
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                                End If
                            Else
                                If t_Control_Type <> ControlType.Text Then
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName & ", "
                                Else
                                    'tb1.Cell(nrRows, 2).Application.Application.Selection.GoToNext(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToS) '' .MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count)
                                    ''tb1.Cell(nrRows, 2).Application.Application.Selection.NextField() '' .MoveRight(Wd.WdUnits.wdWor, oControl.Range.Characters.Count)
                                    oLiquiddoc.Application.Selection.Move(Wd.WdUnits.wdCharacter)
                                    tb1.Cell(nrRows, 2).Application.Selection.Text = ", "
                                End If
                            End If

                        Else
                            If t_Control_Type <> ControlType.Text Then
                                tb1.Cell(nrRows, 2).Application.Selection.Text = t_itemName
                            End If
                        End If
                        'oNameField.CheckBox.Value = False
                        'If t_Control_Type = ControlType.Text Then
                        '    tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, oControl.Range.Characters.Count) '' .MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'Else

                        'End If
                        tb1.Cell(nrRows, 2).Application.Application.Selection.MoveRight(Wd.WdUnits.wdCharacter, 1)
                        'oNameField = Nothing
                    End If
                End If
            Next

            ''''Move Cursor down in the Table


            oLiquiddoc.Application.Selection.MoveDown(Unit:=Wd.WdUnits.wdLine, Count:=1)
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)
            oLiquiddoc.Application.Selection.InsertParagraph()
            oLiquiddoc.Application.Selection.MoveRight(Unit:=Wd.WdUnits.wdCharacter, Count:=1)

            'For i As Integer = 0 To tb1.Columns.Count - 1
            '    tb1.Columns(i + 1).AutoFit()
            'Next
            Return True
            'oCurDoc.Application.Selection.MoveRight()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If oLiquiddoc.Application.Selection.Range.ParentContentControl.Type = Microsoft.Office.Interop.Word.WdContentControlType.wdContentControlRichText Then
                oLiquiddoc.Application.Selection.Range.ParentContentControl.Delete(True)
            End If
            Return False
        End Try
    End Function
    'End Shubhangi'

    Private Sub btncatAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncatAdd.Click
        Try

            If txtcategory.Text.Trim = "" Then
                MessageBox.Show("Enter Category name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtcategory.Focus()
                Exit Sub
            End If
            Dim strItem As String = ""
            Dim strControl As String
            If IsforModify = True And dgTableField.RowCount > 0 Then

                strItem = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)
                'SHUBHANGI 20090922
                'To get value of control
                strControl = CmbControl.Text
            End If
            If strItem <> "" And IsforModify = True Then
                If txtCatItem.Text.Trim = "" Then
                    MessageBox.Show("Enter Item name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCatItem.Focus()
                    Exit Sub
                End If

                If CmbControl.SelectedIndex = 0 Then
                    MsgBox("Select the control type for the DataField", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    CmbControl.Focus()
                    Exit Sub
                End If
            End If

            If strItem = "" And IsforModify = False Then
                If txtCatItem.Text.Trim = "" Then
                    MessageBox.Show("Enter Item name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCatItem.Focus()
                    Exit Sub
                End If

                If CmbControl.SelectedIndex = 0 Then
                    MsgBox("Select the control type for the DataField", MsgBoxStyle.Exclamation, gstrMessageBoxCaption)
                    CmbControl.Focus()
                    Exit Sub
                End If
            End If
            If IsforModify = False Then
                InsertData(txtcategory.Text.Trim, txtCatItem.Text.Trim, CmbControl.SelectedIndex, cmbAssociatedCategory.Text.Trim(), cmbAssoicatedItem.Text.Trim())
            ElseIf dgTableField.RowCount > 0 And IsforModify = True Then
                Dim currentCategory As String = dgTableField.Item(0, dgTableField.CurrentRow.Index).Value
                If strItem = "" Then
                    For i As Integer = dgTableField.Rows.Count - 1 To dgTableField.CurrentRow.Index Step -1
                        If i = dgTableField.CurrentRow.Index Then
                            dgTableField.Item(0, dgTableField.CurrentRow.Index).Value = txtcategory.Text.Trim
                            'Shubhangi 20091022 to display the content when open for modify (which is not having Associated values)
                            dgTableField.Item(2, dgTableField.CurrentRow.Index).Value = txtcategory.Text.Trim
                            'Commented by Shubhangi for that which is not having associated items 
                            'dgTableField.Item(0, dgTableField.CurrentRow.Index).Value = cmbAssociatedCategory.Text.Trim.ToString()
                            'dgTableField.Item(0, dgTableField.CurrentRow.Index).Value = txtcategory.Text
                        End If
                        If Convert.ToString(dgTableField.Item(2, i).Value) = currentCategory Then
                            dgTableField.Item(2, i).Value = txtcategory.Text.Trim
                        End If
                    Next
                End If
                If strItem <> "" And IsforModify = True Then
                    'Original

                    dgTableField.Item(1, dgTableField.CurrentRow.Index).Value = txtCatItem.Text.Trim
                    dgTableField.Item(3, dgTableField.CurrentRow.Index).Value = CType(CmbControl.SelectedIndex, ControlType).GetHashCode
                    dgTableField.Item(4, dgTableField.CurrentRow.Index).Value = CmbControl.Text.ToString()
                    dgTableField.Item(5, dgTableField.CurrentRow.Index).Value = cmbAssociatedCategory.Text.Trim.ToString()

                    'IsforModify = False
                    'For i = 0 To dgTableField.Rows.Count - 1
                    '    If .Item(2, i).Value = CategoryName Then
                    '        ' Check for Item is already exists & control is already present 
                    '        If .Item(1, i).Value = ItemName And .Item(4, i).Value = enumControlType.ToString Then
                    '            MessageBox.Show("Item is already present under this category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '            txtCatItem.Focus()
                    '            Exit Sub
                    '        End If
                    '        IsCategoryPresent = True
                    '        If i <> dgTableField.Rows.Count - 1 Then '''' Find the last row of the category to insert new item at end of the category list
                    '            If .Item(2, i + 1).Value <> CategoryName Then
                    '                Exit For
                    '            End If
                    '        End If
                    '    Else
                    '        IsCategoryPresent = False
                    '    End If
                    'Next

                End If
            End If
            'End If
            IsModified = True  'Set the flag to show that record is modified
            IsforModify = False
            ' End If
            If btncatAdd.Text = "Save" Then
                btncatAdd.Text = "Add"
            End If
            txtcategory.Enabled = True
            txtCatItem.Enabled = True
            CmbControl.Enabled = True
            txtCatItem.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            IsforModify = False
        End Try
    End Sub

    'Public Sub InsertData(ByVal CategoryName As String, ByVal ItemName As String, ByVal nSelectedIndex As Int16, ByVal AssociateCategory As String, ByVal AssociatedItem As String, Optional ByVal AssocaitedProperty As String = "")
    Public Function InsertData(ByVal CategoryName As String, ByVal ItemName As String, ByVal nSelectedIndex As Int16, ByVal AssociateCategory As String, ByVal AssociatedItem As String, Optional ByVal AssocaitedProperty As String = "") As Boolean
        Try

            Dim IsCategoryPresent As Boolean = False
            If nSelectedIndex = 1 Then
                enumControlType = ControlType.CheckBox
            ElseIf nSelectedIndex = 2 Then
                enumControlType = ControlType.Text
            Else
                enumControlType = ControlType.None
            End If

            Dim strProperty As String = ""
            'shubhangi 20091021
            'If cmbAssociatedCategory.Items.Count > 0 Then
            If cmbFieldCategory.Text = "Physical Examination" Then
                If AssocaitedProperty = "" And chkAssociateStd.Checked = True And cmbAssociateSubItem.Visible = False Then
                    strProperty = CType(cmbAssociatedCategory.SelectedItem, clsAssocatedLiquidData).GrouppropertyId & CType(cmbAssoicatedItem.SelectedItem, clsAssocatedLiquidData).ItempropertyId
                ElseIf AssocaitedProperty = "" And chkAssociateStd.Checked = True And cmbAssociateSubItem.Visible = True Then
                    strProperty = CType(cmbAssociatedCategory.SelectedItem, clsAssocatedLiquidData).GrouppropertyId & CType(cmbAssoicatedItem.SelectedItem, clsAssocatedLiquidData).ItempropertyId & CType(cmbAssociateSubItem.SelectedItem, clsAssocatedLiquidData).SubElementproperty

                Else
                    strProperty = AssocaitedProperty
                End If
            Else
                strProperty = ""
                AssociateCategory = ""
                AssociatedItem = ""

            End If

            If dgTableField.RowCount = 0 Then '''' If the grid is empty 
                With dgTableField
                    .Rows.Add()
                    .Item(0, .Rows.Count - 1).Value = CategoryName '' Category 
                    .Item(2, .Rows.Count - 1).Value = CategoryName '' Hidden category
                    .Item(5, .Rows.Count - 1).Value = AssociateCategory '' Category 
                    .Item(7, .Rows.Count - 1).Value = AssociateCategory '' Hidden category
                    .Rows.Add()
                    .Item(1, .Rows.Count - 1).Value = ItemName '' Item
                    .Item(2, .Rows.Count - 1).Value = CategoryName '' Hiddent Category
                    .Item(3, .Rows.Count - 1).Value = enumControlType.GetHashCode()
                    .Item(4, .Rows.Count - 1).Value = enumControlType.ToString()
                    .Item(6, .Rows.Count - 1).Value = AssociatedItem '' Item
                    .Item(7, .Rows.Count - 1).Value = AssociateCategory '' Hidden category
                    .Item(8, .Rows.Count - 1).Value = strProperty
                End With
                txtCatItem.Text = ""
            Else

                With dgTableField
                    IsCategoryPresent = False
                    Dim i As Integer = 0
                    'Commented by SHUBHANGI 20090921
                    'If control type is different then allow to add same records in dgTableField 
                    '' Check for category is present
                    For i = 0 To dgTableField.Rows.Count - 1
                        If .Item(2, i).Value = CategoryName Then
                            '' Check for Item is already exists & control is already present 
                            If .Item(1, i).Value = ItemName And .Item(4, i).Value = enumControlType.ToString Then
                                MessageBox.Show("Item is already present under this category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtCatItem.Focus()
                                Return False
                            End If
                            IsCategoryPresent = True
                            If i <> dgTableField.Rows.Count - 1 Then '''' Find the last row of the category to insert new item at end of the category list
                                If .Item(2, i + 1).Value <> CategoryName Then
                                    Exit For
                                End If
                            End If
                        Else
                            IsCategoryPresent = False
                        End If
                    Next

                    If IsCategoryPresent = False Then '''' If category is not present the add category and item
                        .Rows.Add()
                        .Item(0, .Rows.Count - 1).Value = CategoryName
                        .Item(2, .Rows.Count - 1).Value = CategoryName
                        .Item(5, .Rows.Count - 1).Value = AssociateCategory '' Category 
                        .Item(7, .Rows.Count - 1).Value = AssociateCategory '' Hidden category
                        .Rows.Add()
                        .Item(1, .Rows.Count - 1).Value = ItemName
                        .Item(2, .Rows.Count - 1).Value = CategoryName
                        .Item(3, .Rows.Count - 1).Value = enumControlType.GetHashCode()
                        .Item(4, .Rows.Count - 1).Value = enumControlType.ToString()
                        .Item(6, .Rows.Count - 1).Value = AssociatedItem '' Item
                        .Item(7, .Rows.Count - 1).Value = AssociateCategory '' Hidden category
                        .Item(8, .Rows.Count - 1).Value = strProperty
                    Else
                        Try
                            If i = .Rows.Count Then '''' If the new row is and the end of the grid
                                .Rows.Insert(i, 1)
                                .Item(1, i).Value = ItemName
                                .Item(2, i).Value = CategoryName
                                .Item(3, i).Value = enumControlType.GetHashCode()
                                .Item(4, i).Value = enumControlType.ToString()
                                .Item(6, i).Value = AssociatedItem '' Item
                                .Item(7, i).Value = AssociateCategory '' Hidden category
                                .Item(8, i).Value = strProperty
                            Else '''' If the new row is in between the grid rows.
                                .Rows.Insert(i + 1, 1)
                                .Item(1, i + 1).Value = ItemName
                                .Item(2, i + 1).Value = CategoryName
                                .Item(3, i + 1).Value = enumControlType.GetHashCode()
                                .Item(4, i + 1).Value = enumControlType.ToString()
                                .Item(6, i + 1).Value = AssociatedItem '' Item
                                .Item(7, i + 1).Value = AssociateCategory '' Hidden category
                                .Item(8, i + 1).Value = strProperty
                            End If

                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        End Try
                    End If
                End With
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    Private Sub btnCatModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatModify.Click

        ModifyItem()
    End Sub

    Private Sub ModifyItem()
        Try
            Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)
            Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
            Dim ControlType As Integer = CType(dgTableField.Item(3, dgTableField.CurrentRow.Index).Value, ControlType).GetHashCode()
            If _selItem <> "" Then
                txtCatItem.Text = _selItem
                txtcategory.Text = _selCategory
                CmbControl.SelectedIndex = ControlType
                'If ControlType = 1 Then
                '    cmbDataType.Text = "Check Box"
                'ElseIf ControlType = 2 Then
                '    cmbDataType.Text = "Text"
                'Else
                '    cmbDataType.Text = "Check Box"
                'End If
                IsforModify = True
            Else
                txtCatItem.Text = ""
                txtcategory.Text = ""
                If (CmbControl.Items.Count > 0) Then
                    CmbControl.SelectedIndex = 0
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlFieldValues.Click, btnDelete.Click
        Try
            If dgTableField.RowCount > 0 Then
                Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)   '_selItem take Selected value of Combo box
                Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)

                'SHUBHANGI 20090919 
                'When only 1 item is present associte to that Category. & if we delete that item then Category also deleted from  table.
                Dim cnt As Integer = 0         'Counter for how many records of same category
                For i As Integer = 0 To dgTableField.Rows.Count - 1
                    If (_selCategory = dgTableField.Item(2, i).Value) Then
                        cnt = cnt + 1
                    End If
                Next
                'Check if more than 2 records of same category are present, then allow to delete 1 
                If _selItem <> "" And cnt > 2 Then

                    dgTableField.Rows.Remove(dgTableField.Rows(dgTableField.CurrentRow.Index))
                    'dgTableField.Rows.Remove(dgTableField.Rows(dgTableField.CurrentRow.Index))
                    txtcategory.Text = ""
                    txtItem.Text = ""
                    CmbControl.SelectedIndex = 0
                    IsModified = True
                End If
                'cnt = cnt - 1
                Dim CurrenrRow As Integer
                CurrenrRow = dgTableField.CurrentRow.Index

                If _selItem <> "" And cnt <= 2 Then 'Check if  2 records of same category are present, then delete item alog with category.

                    'dgTableField.Rows.Remove(dgTableField.Rows(dgTableField.CurrentRow.Index))
                    'dgTableField.Rows.Remove(dgTableField.Rows(dgTableField.CurrentRow.Index))

                    dgTableField.Rows.Remove(dgTableField.Rows(CurrenrRow))
                    dgTableField.Rows.Remove(dgTableField.Rows(CurrenrRow - 1))
                    IsModified = True
                ElseIf _selItem = "" Then
                    If MessageBox.Show("Are you sure to delete category and item under this category", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim currentIndex As Integer = dgTableField.CurrentRow.Index
                        Dim currentCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                        For i As Integer = dgTableField.Rows.Count - 1 To currentIndex Step -1
                            If i = currentIndex Then
                                dgTableField.Rows.Remove(dgTableField.Rows(currentIndex))
                                'End If
                            ElseIf Convert.ToString(dgTableField.Item(2, i).Value) = currentCategory Then
                                dgTableField.Rows.Remove(dgTableField.Rows(i))
                            End If
                        Next
                        IsModified = True
                    Else
                        Exit Sub
                    End If
                End If

                'End If
                'Try
                '    Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)
                '    Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                '    If _selItem <> "" Then
                '        dgTableField.Rows.Remove(dgTableField.Rows(dgTableField.CurrentRow.Index))
                '        txtcategory.Text = ""
                '        txtItem.Text = ""
                '        CmbControl.SelectedIndex = 0
                '    Else
                '        If MessageBox.Show("Are you sure to delete category and item under this category", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            Dim currentIndex As Integer = dgTableField.CurrentRow.Index
                '            Dim currentCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                '            For i As Integer = dgTableField.Rows.Count - 1 To currentIndex Step -1
                '                If i = currentIndex Then
                '                    dgTableField.Rows.Remove(dgTableField.Rows(currentIndex))
                '                End If
                '                If Convert.ToString(dgTableField.Item(2, i).Value) = currentCategory Then
                '                    dgTableField.Rows.Remove(dgTableField.Rows(i))
                '                End If
                '            Next

                '        End If
                '    End If
            End If
            If dgTableField.RowCount = 0 Then
                IsforModify = False
                btncatAdd.Text = "Add"
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub dgTableField_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTableField.CellContentDoubleClick

    End Sub

    Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        'If dgItemList.Rows.Count > 0 Then
        '    If dgItemList.CurrentRow.Index >= 0 Then
        '        Dim strItem As String = dgItemList.Item(1, dgItemList.CurrentRow.Index).Value
        '        txtItem.Text = strItem.Trim
        '        Dim ctrlType As ControlType = CType(dgItemList.Item(0, dgItemList.CurrentRow.Index).Value, ControlType)
        '        CmbControl.Text = ctrlType.ToString()
        '        'CmbControl.Text = dgItemList.Item(0, dgItemList.CurrentRow.Index).Value.ToString
        '        RowToEdit = dgItemList.CurrentRow.Index
        '        IsforModify = True
        '    End If
        'End If

        'If Not IsNothing(lstItems.SelectedItem) Then
        '    Dim strItem As String = lstItems.SelectedItem.ToString()
        '    txtItem.Text = strItem.Trim
        '    IsforModify = True
        'End If
    End Sub

    Private Sub btnAdd_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseEnter

        ToolTip1.SetToolTip(btnAdd, "Add")


    End Sub


    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.MouseHover, btnUp.MouseHover, btnModify.MouseHover, btnDown.MouseHover, btnCatModify.MouseHover, btncatAdd.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch

        If btncatAdd.Text = "Add" Then
            ToolTip1.SetToolTip(btncatAdd, "Add")
        ElseIf btncatAdd.Text = "Save" Then
            ToolTip1.SetToolTip(btncatAdd, "Save")
        End If
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.MouseLeave, btnUp.MouseLeave, btnRemove.MouseLeave, btnModify.MouseLeave, btnDown.MouseLeave, btnCatModify.MouseLeave, btncatAdd.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub dgItemList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellClick
        'ItemListRowIndex = e.RowIndex
        txtItem.Text = ""
        If (CmbControl.Items.Count > 0) Then
            CmbControl.SelectedIndex = 0
        End If
        cmbstddata.SelectedItem = Nothing
    End Sub

    Private Sub dgTableField_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTableField.CellDoubleClick
        TableField()
    End Sub
    Public Sub TableField()
        Try
            If dgTableField.RowCount > 0 Then
                Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)
                Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                Dim _selControlType As Integer = Convert.ToInt16(dgTableField.Item(3, dgTableField.CurrentRow.Index).Value)
                txtCatItem.Text = _selItem
                txtcategory.Text = _selCategory
                CmbControl.SelectedIndex = _selControlType
                IsforModify = True
                btncatAdd.Text = "Save"

                'SHUBHANGI 20090922
                'Counter for checking same itmes are present more than one time(along with different Control type 
                Dim cnt As Integer = 0
                For i As Integer = 0 To dgTableField.Rows.Count - 1
                    If (dgTableField.Item(1, i).Value = _selItem) Then
                        cnt = cnt + 1
                    End If
                Next
                'Check for Item of same Control is alreay present or not if yes then make control combo enable false.
                If cnt > 1 Then
                    CmbControl.Enabled = False
                Else
                    CmbControl.Enabled = True
                End If
                'IsforModify = False
                'Next
                'If IsforModify = False Then
                '    CmbControl.Enabled = False
                'Else
                '    CmbControl.Enabled = True
                'End If
                'If CmbControl.Text = dgTableField.Item(4, dgTableField.CurrentRow.Index).Value Then
                '    CmbControl.Enabled = False
                'Else
                '    CmbControl.Enabled = True
                'End If

                If _selItem <> "" Then
                    txtcategory.Enabled = False
                Else
                    txtCatItem.Enabled = False
                    CmbControl.Enabled = False
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dgItemList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgItemList.CellDoubleClick
        ItemField()
    End Sub

    Private Sub ItemField()
        'Try
        '    If txtItem.Enabled = False Then
        '        txtItem.Enabled = True
        '    End If
        '    With dgItemList
        '        If dgItemList.RowCount > 0 Then
        '            Dim _selItem As String = Convert.ToString(.Item(1, .CurrentRow.Index).Value)
        '            txtItem.Text = _selItem
        '            If cmbDataType.Text = "Multiple Selection" Or (cmbDataType.Text = "Boolean" And .Item(3, .CurrentRow.Index).Value <> "") Then
        '                Dim _selControlType As Integer = Convert.ToInt16(.Item(0, .CurrentRow.Index).Value)
        '                Dim _AssociatedProperty As String
        '                If Not IsNothing(.Item(3, .CurrentRow.Index).Value) Then
        '                    _AssociatedProperty = .Item(3, .CurrentRow.Index).Value.ToString
        '                Else
        '                    _AssociatedProperty = ""
        '                End If

        '                'SHUBHNAGI 20090922
        '                Dim cnt As Integer = 0
        '                For i As Integer = 0 To dgItemList.Rows.Count - 1
        '                    If (dgItemList.Item(1, i).Value = _AssociatedProperty) Then
        '                        cnt = cnt + 1
        '                    End If
        '                Next
        '                'Check for Item of same Control is alreay present or not if yes then make control combo enable false.
        '                If cnt > 1 Then
        '                    CmbControl.Enabled = False
        '                Else
        '                    CmbControl.Enabled = True
        '                End If
        '                CmbControl.SelectedIndex = _selControlType
        '                cmbstddata.Text = _AssociatedProperty
        '            End If
        '        Else
        '            Exit Sub
        '        End If
        '    End With
        Try
            If txtItem.Enabled = False Then
                txtItem.Enabled = True
            End If
            With dgItemList
                If dgItemList.RowCount > 0 Then
                    Dim _selItem As String = Convert.ToString(.Item(1, .CurrentRow.Index).Value)
                    txtItem.Text = _selItem
                    If cmbDataType.Text = "Multiple Selection" Or (cmbDataType.Text = "Boolean" And .Item(3, .CurrentRow.Index).Value <> "") Then
                        Dim _selControlType As Integer = Convert.ToInt16(.Item(0, .CurrentRow.Index).Value)
                        Dim _AssociatedProperty As String
                        If Not IsNothing(.Item(3, .CurrentRow.Index).Value) Then
                            _AssociatedProperty = .Item(3, .CurrentRow.Index).Value.ToString
                        Else
                            _AssociatedProperty = ""
                        End If

                        'SHUBHNAGI 20090922
                        Dim cnt As Integer = 0
                        For i As Integer = 0 To dgItemList.Rows.Count - 1
                            If (dgItemList.Item(1, i).Value = _AssociatedProperty) Then
                                cnt = cnt + 1
                            End If
                        Next
                        'Check for Item of same Control is alreay present or not if yes then make control combo enable false.
                        If cnt > 1 Then
                            CmbControl.Enabled = False
                        Else
                            CmbControl.Enabled = True
                        End If
                        ' If IsfrmExam = True And _selControlType = 71 Then
                        If IsfrmExam = True And _selControlType = 1 Then
                            CmbControl.SelectedIndex = 1
                            'ElseIf IsfrmExam = True And _selControlType <> 71 Then
                        ElseIf IsfrmExam = True And _selControlType <> 1 Then
                            CmbControl.SelectedIndex = 2
                        ElseIf IsfrmExam = False Then
                            CmbControl.SelectedIndex = _selControlType
                        End If

                        cmbstddata.Text = _AssociatedProperty
                    End If
                Else
                    Exit Sub
                End If
            End With

            RowToEdit = dgItemList.CurrentRow.Index
            IsforModify = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dgTableField_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgTableField.CellClick
        txtcategory.Text = ""
        txtCatItem.Text = ""
        If (CmbControl.Items.Count > 0) Then
            CmbControl.SelectedIndex = 0
        End If
        btncatAdd.Text = "Add"
        txtcategory.Enabled = True
        txtCatItem.Enabled = True
        CmbControl.Enabled = True
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click, ToolStripButton3.Click
        'COMMENTED BY SHUBHANGI 20100622 
        'If dgTableField.Rows.Count > 0 Then
        '    dgTableField.Rows.Item(0).Selected = True
        'End If
        'CmbControl.Enabled = True
        'txtcategory.Text = ""
        'txtCatItem.Text = ""
        'CmbControl.SelectedIndex = 0
        'btncatAdd.Text = "Add"
        'txtcategory.Enabled = True
        'txtCatItem.Enabled = True
        'CmbControl.Enabled = True

        ''SHUBHANGI 20090923
        'cmbFieldCategory.SelectedIndex = 0
        'cmbDataType.SelectedIndex = 0

        'txtField.ResetText()
        ''cmbDataType.SelectedIndex = 0
        ''cmbFieldCategory.SelectedIndex = 0
        'txtItem.ResetText()
        'CmbControl.ResetText()
        'cmbstddata.SelectedIndex = -1
        'chkAssociateStd.Checked = False
        'chkAssociatestddata.Visible = False
        'dgItemList.Rows.Clear()

        'pnlassociateStdItem.Visible = False
        ''Shubhangi 20091201
        ''After clicking on refresh it will enable Add, remove & it will display the UI same as that of New Liquid data
        'ToolStripButton1.Enabled = True
        'ToolStripButton2.Enabled = True
        'ValidateDatatype(cmbDataType.Text)
        'dgTableField.Rows.Clear()

        ''SHUBHANGI 20100618
        ''If dgItemList.Rows.Count > 0 Then
        ''    dgItemList.Rows.Item(0).Selected = True
        ''Else
        ''    ' dgItemList.Rows.Clear()
        ''    pnlFieldValues.Visible = False
        ''End If


        ''Shubhangi 20091027
        ''Make IsMandatory Check box unchecked on refresh Button
        'If chckRequired.Checked = True Then
        '    chckRequired.Checked = False
        'End If


    End Sub

    Private Sub btn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Refresh.Click
        txtItem.Text = ""
        If dgItemList.Rows.Count > 0 Then
            dgItemList.Rows.Item(0).Selected = True
        End If
        If (CmbControl.Items.Count > 0) Then
            CmbControl.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbFieldCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFieldCategory.Click
        _selectedCategory = cmbFieldCategory.Text

    End Sub

    Private Sub cmbFieldCategory_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFieldCategory.SelectedValueChanged

    End Sub
    Private Sub cmbFieldCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFieldCategory.SelectionChangeCommitted

        'Shubhangi 20091026
        'Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
        'It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event
        IsforModify = False
        'SHUBHANGI 20090919  
        ''DISPLAY MESSAGE WHEN WE CHANGE CATEGORY
        ''''' Integrated by Mayuri:20100731 - for making radiobuttons visible ''''
        If cmbDataType.Text.Trim <> "Choose an item" And cmbFieldCategory.Text.Trim = "HPI" And cmbDataType.Text.Trim = "Text" Then
            pnlHPIExtended.Visible = True
            RdbtnBrief.Checked = False
            RdbtnExtended.Checked = False
        Else
            pnlHPIExtended.Visible = False
            RdbtnBrief.Checked = False
            RdbtnExtended.Checked = False
        End If
        ''''' Integrated by Mayuri:20100731 - for making radiobuttons visible ''''

        If cmbFieldCategory.Text <> "Choose an item" And dgItemList.Rows.Count <> 0 Then
            'SHUBHANGI 20090924
            If Panel2.Visible = True And pnlAddCategory.Visible = True And pnlFieldValues.Visible = True Then
                'If Panel2.Visible = True And pnlAddCategory.Visible = False And pnlFieldValues.Visible = True Then
                'MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                If MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    dgTableField.Rows.Clear()
                Else
                    cmbFieldCategory.Text = _selectedCategory
                    Exit Sub
                End If
            End If
        End If
        If cmbFieldCategory.Text <> "Choose an item" And dgTableField.Rows.Count <> 0 Then
            'If Panel2.Visible = True And pnlAddCategory.Visible = True And pnlFieldValues.Visible = True Then
            If Panel2.Visible = True And pnlAddCategory.Visible = True And pnlFieldValues.Visible = True Then
                If MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    dgTableField.Rows.Clear()
                Else
                    cmbFieldCategory.Text = _selectedCategory
                    Exit Sub
                End If
                dgTableField.RowCount = 0
                dgTableField.Rows.Clear()
            End If
        End If
        'If Category is Physical Examination 
        If cmbFieldCategory.Text.ToString.Trim = "Physical Examination" Then
            chkAssociateStd.Visible = True
        Else
            chkAssociateStd.Visible = False
            chkAssociateStd.Checked = False
            dgTableField.Columns(5).Visible = False
            dgTableField.Columns(6).Visible = False
        End If

        'If Category is Management Option and Field Type is Multiple Selection Then
        If cmbFieldCategory.Text.Trim = "Management option" And cmbDataType.Text.ToString.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Managment option"

            'If Category is Labs and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Labs" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Labs"

            'If Category is X-Ray/Radiology and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "X-Ray/Radiology" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard X-Ray/Radiology"

            'If Category is Other Diagnosis Test and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Other Diagonsis Tests" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Other Diagonsis Tests"

            'If Category is HPI and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "HPI" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard HPI"

            'If Category is ROS and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "ROS" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard ROS"

            'Else

            '    chkAssociatestddata.Checked = False
            '    chkAssociatestddata.Visible = False
            '    pnlStandardEM.Visible = False
        End If

        'If Category is History and Field Type is Boolean Then
        If cmbFieldCategory.Text.ToString.Trim = "History" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Histroy"

            'If Category is Labs and Field Type is Boolean Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Labs" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Labs"

            'If Category is Management Option and Field Type is Boolean Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Management option" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Management Option"

        ElseIf cmbFieldCategory.Text.ToString.Trim = "X-Ray/Radiology" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated X-Ray/Radiology"

        ElseIf cmbFieldCategory.Text.ToString.Trim = "Other Diagonsis Tests" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Other Diagonsis Tests"
        ElseIf cmbFieldCategory.Text.ToString.Trim = "General" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Choose an item" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Physical Examination" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "ROS" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "HPI" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Medical Decision-Making" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        End If

        If cmbFieldCategory.Text.ToString.Trim = "Medical Decision-Making" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "General" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Choose an item" And cmbDataType.Text.Trim = "Multiple Selection" Then
            pnlFieldValues.Dock = DockStyle.Fill
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        End If
        'End If
        '_selectedCategory = cmbFieldCategory.Text
    End Sub

   

    Private Sub FillStdPhysicalExamination(ByVal _sender As System.Object, ByVal _e As System.EventArgs)
        Dim ElementGroupNode As myTreeNode
        Dim ElementNode As myTreeNode
        '        Dim subElementGroupNode As myTreeNode
        'Dim subElementNode As myTreeNode
        Dim manifestResourceStream As IO.Stream = Nothing
        Dim write As IO.FileStream = Nothing
        Dim XmlFilePath As String = Nothing
        Dim _ds As DataSet = Nothing
        Dim dtElementGroup As DataTable = Nothing




        Try
            Dim examName As String = "AlphaII.CodeWizard.Entities.Objects.EvaluationManagement.Exams.General Multi-System Exam.xml"
            manifestResourceStream = Reflection.Assembly.GetAssembly(GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.Exams.GeneralMultiSystemExam)).GetManifestResourceStream(examName)
            'SLR: Changed from Startup path to Apptempfoldepath
            XmlFilePath = gloSettings.FolderSettings.AppTempFolderPath & "\" & "General Multi-System Exam" & System.Guid.NewGuid().ToString() & ".xml" 'SLR: TO avoid concureency error added guid
            If IO.File.Exists(XmlFilePath) Then
                IO.File.Delete(XmlFilePath)
            End If
            write = New IO.FileStream(XmlFilePath, IO.FileMode.Create, IO.FileAccess.Write)
            ReadWriteStream(manifestResourceStream, write)
            _ds = GetDataSet(XmlFilePath)

            If Not IsNothing(_ds) Then
                If _ds.Tables.Count > 0 Then
                    ' dtElementGroup = New DataTable
                    dtElementGroup = _ds.Tables(1)

                    ' dtElement = New DataTable
                    dtElement = _ds.Tables(4).Copy()

                    ' dtSubElemetGroup = New DataTable
                    dtSubElemetGroup = _ds.Tables(2).Copy()

                    'dtSubElement = New DataTable

                    dtSubElement = _ds.Tables(3).Copy()

                    '''' Fill Treeview Start
                    trvstd.Nodes.Clear()
                    For i As Integer = 0 To dtElementGroup.Rows.Count - 1
                        ElementGroupNode = New myTreeNode
                        ElementGroupNode.Text = dtElementGroup.Rows(i)("Title").ToString()
                        ElementGroupNode.Key = Convert.ToInt64(dtElementGroup.Rows(i)("ElementGroup_Id"))
                        trvstd.Nodes.Add(ElementGroupNode)
                        For j As Integer = 0 To dtElement.Rows.Count - 1
                            If dtElementGroup.Rows(i)("ElementGroup_Id") = dtElement.Rows(j)("ElementGroup_Id") Then
                                ' Dim ele As New TreeNode
                                ElementNode = New myTreeNode
                                ElementNode.Text = dtElement.Rows(j)("Text")
                                ElementNode.Key = dtElement.Rows(j)("ElementGroup_Id")
                                ElementNode.Tag = dtElementGroup.Rows(i)("Title")
                                ElementGroupNode.Nodes.Add(ElementNode)

                            End If
                        Next
                    Next

                    If trvstd.Nodes.Count > 0 Then
                        For Each parentNode As myTreeNode In trvstd.Nodes
                            For k As Integer = 0 To dtSubElemetGroup.Rows.Count - 1
                                If parentNode.Key = dtSubElemetGroup.Rows(k)("ElementGroup_Id") Then
                                    ElementGroupNode = New myTreeNode
                                    ElementGroupNode.Text = dtSubElemetGroup.Rows(k)("Title")
                                    ElementGroupNode.Key = dtSubElemetGroup.Rows(k)("SubElementGroup_Id")
                                    parentNode.Nodes.Add(ElementGroupNode)
                                    For l As Integer = 0 To dtSubElement.Rows.Count - 1
                                        If dtSubElement.Rows(l)("SubElementGroup_Id") = dtSubElemetGroup.Rows(k)("SubElementGroup_Id") Then
                                            ElementNode = New myTreeNode
                                            ElementNode.Text = dtSubElement.Rows(l)("Text")
                                            ElementNode.Key = dtSubElement.Rows(l)("SubElementGroup_Id")
                                            ElementGroupNode.Nodes.Add(ElementNode)
                                        End If
                                    Next
                                End If
                            Next
                        Next
                    End If
                End If
                '''' Fill Treeview End
                Dim oColElementGroup As New List(Of clsAssocatedLiquidData)
                Dim oElemnetGroup As clsAssocatedLiquidData
                For i As Integer = 0 To dtElementGroup.Rows.Count - 1
                    oElemnetGroup = New clsAssocatedLiquidData
                    oElemnetGroup.GroupTitle = dtElementGroup.Rows(i)("Title").ToString()
                    oElemnetGroup.Group_ID = Convert.ToInt64(dtElementGroup.Rows(i)("ElementGroup_Id"))
                    oElemnetGroup.GrouppropertyId = dtElementGroup.Rows(i)("Id")
                    oColElementGroup.Add(oElemnetGroup)
                Next


                cmbAssociatedCategory.DataSource = oColElementGroup
                cmbAssociatedCategory.DisplayMember = "GroupTitle"
                cmbAssociatedCategory.ValueMember = "Group_ID"
                If (oColElementGroup.Count > 0) Then
                    cmbAssociatedCategory.SelectedIndex = 0
                End If

                cmbAssociatedCategory_SelectionChangeCommitted(_sender, _e)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(write) = False) Then
                write.Close()
                write.Dispose()
            End If
            If (IsNothing(manifestResourceStream) = False) Then
                manifestResourceStream.Close()
                manifestResourceStream.Dispose()
            End If

            If IO.File.Exists(XmlFilePath) Then
                IO.File.Delete(XmlFilePath)
            End If
            If (IsNothing(_ds) = False) Then

                _ds.Dispose()
            End If
        End Try
    End Sub

    Private Function GetDataSet(ByVal strFilePath As String) As DataSet
        Dim ds As DataSet
        Try
            If IO.File.Exists(strFilePath) Then
                ds = New DataSet()
                ds.ReadXml(strFilePath)
                If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                    Return ds
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        End Try
    End Function

    Private Sub ReadWriteStream(ByVal readStream As IO.Stream, ByVal writeStream As IO.Stream)
        Dim Length As Integer = 256
        Dim buffer As Byte() = New Byte(Length - 1) {}
        Dim bytesRead As Integer = readStream.Read(buffer, 0, Length)
        ' write the required bytes 
        While bytesRead > 0
            writeStream.Write(buffer, 0, bytesRead)
            bytesRead = readStream.Read(buffer, 0, Length)
        End While
        readStream.Close()
        writeStream.Close()
    End Sub


    Private Sub chkAssociateStd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAssociateStd.CheckedChanged
        If chkAssociateStd.Checked = True Then
            pnlStandardEM.Visible = True
            If cmbFieldCategory.Text.ToString.Trim = "Physical Examination" Then
                FillStdPhysicalExamination(sender, e)
            End If
            dgTableField.Columns(5).Visible = True
            dgTableField.Columns(6).Visible = True
        Else
            pnlStandardEM.Visible = False
            ' cmbAssociatedCategory.DataSource = Nothing
            'cmbAssoicatedItem.DataSource = Nothing
            'cmbAssociateSubItem.DataSource = Nothing
            chkAssociateStd.Checked = False
            dgTableField.Columns(5).Visible = False
            dgTableField.Columns(6).Visible = False
        End If
    End Sub

    Private Sub cmbAssociatedCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAssociatedCategory.SelectionChangeCommitted, cmbstddata.SelectionChangeCommitted
        Try
            Dim strSelectedValue As String = ""

            cmbAssoicatedItem.DataSource = Nothing
            cmbAssoicatedItem.Items.Clear()
            cmbAssociateSubItem.DataSource = Nothing
            cmbAssociateSubItem.Items.Clear()

            lblAssociateSubItem.Visible = False
            cmbAssociateSubItem.Visible = False
            If Not IsNothing(dtElement) Then
                dv = New DataView(dtElement)
                strSelectedValue = cmbAssociatedCategory.SelectedValue.ToString()
                ''ParentID = "+int.Parse(ddlSort.SelectedValue.ToString()); "
                dv.RowFilter = dv.Table.Columns("ElementGroup_Id").ColumnName & "=" & CType(strSelectedValue, Integer)   ''" Like '" & strSelectedValue & "'"
            End If


            Dim _dt As DataTable = Nothing
            'Shubhangi 20091010
            If Not IsNothing(dv) Then
                _dt = dv.ToTable
            End If

            txtcategory.Text = ""
            txtItem.Text = ""

            If Not IsNothing(dtSubElemetGroup) Then

                dv = New DataView(dtSubElemetGroup)
                dv.RowFilter = dv.Table.Columns("ElementGroup_Id").ColumnName & "=" & CType(strSelectedValue, Integer)   ''" Like '" & strSelectedValue & "'"

                Dim _dtnew As New DataTable
                _dtnew = dv.ToTable
                Dim newrow As DataRow

                For i As Integer = 0 To _dtnew.Rows.Count - 1

                    newrow = _dt.NewRow()
                    newrow("Id") = _dtnew.Rows(i)("Id")
                    newrow("Text") = _dtnew.Rows(i)("Title")
                    newrow("ElementGroup_Id") = _dtnew.Rows(i)("ElementGroup_Id")
                    _dt.Rows.Add(newrow)

                Next


                Dim oColItem As New List(Of clsAssocatedLiquidData)
                Dim oItem As clsAssocatedLiquidData
                For i As Integer = 0 To _dt.Rows.Count - 1
                    oItem = New clsAssocatedLiquidData
                    oItem.ItemGroup_ID = Convert.ToInt64(_dt.Rows(i)("ElementGroup_Id"))
                    oItem.ItemTitle = _dt.Rows(i)("Text")
                    oItem.ItempropertyId = _dt.Rows(i)("Id")
                    oColItem.Add(oItem)
                Next

                cmbAssoicatedItem.DataSource = oColItem
                cmbAssoicatedItem.DisplayMember = "ItemTitle"
                cmbAssoicatedItem.ValueMember = "ItemGroup_ID"
                If (_dt.Rows.Count > 0) Then
                    cmbAssoicatedItem.SelectedIndex = 0
                End If

                cmbAssoicatedItem_SelectionChangeCommitted(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub cmbAssoicatedItem_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssoicatedItem.SelectionChangeCommitted

        'Handle Exception using try-catch block
        Try
            'Declare avariable
            Dim strSelectedValue As String
            Dim nGroupId As Integer
            Dim IsPresent As Boolean = False
            If Not IsNothing(dtSubElement) Then
                dv = New DataView(dtSubElement)
                For i As Integer = 0 To dtSubElemetGroup.Rows.Count - 1
                    If dtSubElemetGroup.Rows(i)("ElementGroup_Id") = cmbAssoicatedItem.SelectedValue And dtSubElemetGroup.Rows(i)("Title") = cmbAssoicatedItem.Text Then
                        nGroupId = dtSubElemetGroup.Rows(i)("SubElementGroup_Id")
                        IsPresent = True
                        Exit For
                    End If
                Next
                If IsPresent = True Then
                    strSelectedValue = nGroupId.ToString()
                    ''ParentID = "+int.Parse(ddlSort.SelectedValue.ToString()); "
                    dv.RowFilter = dv.Table.Columns("SubElementGroup_Id").ColumnName & "=" & CType(strSelectedValue, Integer)   ''" Like '" & strSelectedValue & "'"
                End If

            End If
            Dim _dt As New DataTable

            'Shubhangi 20091010
            If Not IsNothing(dv) Then
                _dt = dv.ToTable
            End If
            '_dt = dv.ToTable

            If IsPresent = True Then
                Dim oColSubItem As New List(Of clsAssocatedLiquidData)
                Dim oSubItem As clsAssocatedLiquidData
                For i As Integer = 0 To _dt.Rows.Count - 1
                    oSubItem = New clsAssocatedLiquidData
                    oSubItem.SubElementGroup_ID = Convert.ToInt64(_dt.Rows(i)("SubElementGroup_Id"))
                    oSubItem.SubElementTitle = _dt.Rows(i)("Text")
                    oSubItem.SubElementproperty = _dt.Rows(i)("Id")
                    oColSubItem.Add(oSubItem)
                Next
                lblAssociateSubItem.Visible = True
                cmbAssociateSubItem.Visible = True
                cmbAssociateSubItem.DataSource = oColSubItem
                cmbAssociateSubItem.DisplayMember = "SubElementTitle"
                cmbAssociateSubItem.ValueMember = "SubElementGroup_ID"
            Else
                lblAssociateSubItem.Visible = False
                cmbAssociateSubItem.Visible = False

                cmbAssociateSubItem.DataSource = Nothing
                cmbAssociateSubItem.Items.Clear()
            End If

            'Throw Exception
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub fillcombobox()
        Try

            Dim strprop As String = ""
            Dim pType As Type
            'Check Combo box for Management option
            If (cmbFieldCategory.Text = "Management option") Then

                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType
                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        cmbstddata.Items.Add(strprop)
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard Management option"

                'Check Combo box for Lab option
            ElseIf (cmbFieldCategory.Text = "Labs") Then

                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType
                    'Add Items in combo box
                    If pType.Name = "Boolean" Then ''FullName = "System.Boolean"
                        If strprop.EndsWith("Urgent") <> True Then
                            cmbstddata.Items.Add(strprop)
                        End If
                    End If

                Next
                chkAssociatestddata.Text = "Associate standard Labs"

                'Check Combo box for X-Ray/Radiology option
            ElseIf (cmbFieldCategory.Text = "X-Ray/Radiology") Then

                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType

                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        If strprop.EndsWith("Urgent") <> True Then
                            cmbstddata.Items.Add(strprop)
                        End If
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard X-Ray/Radiology"

                'Check Combo box for OtheDiagnosisTests option
            ElseIf (cmbFieldCategory.Text = "Other Diagonsis Tests") Then

                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType

                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        If strprop.EndsWith("Urgent") <> True Then
                            cmbstddata.Items.Add(strprop)
                        End If
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard Other Diagonsis Tests"

                'Check Combo box for ROS option
            ElseIf (cmbFieldCategory.Text = "ROS") Then
                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType
                    If strprop.Contains("Ros") Then
                        'Add Items in combo box
                        If pType.Name = "Boolean" Then
                            strprop = strprop.Substring(3) ''FullName = "System.Boolean"
                            cmbstddata.Items.Add(strprop)
                        End If
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard ROS"

                'Check Combo box for HPI option
            ElseIf (cmbFieldCategory.Text = "HPI") Then
                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name

                    pType = propertyInfo.PropertyType
                    If strprop.Contains("Hpi") Then
                        'Add Items in combo box
                        If pType.Name = "Boolean" Then
                            strprop = strprop.Substring(3) ''FullName = "System.Boolean"
                            cmbstddata.Items.Add(strprop)
                        End If
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard HPI"

                'Check Combo box for History option
            ElseIf (cmbFieldCategory.Text = "History") Then
                'Clear Associated Item combo box
                cmbstddata.Items.Clear()

                'Declare a variable for PropertyInfo
                Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtHistory).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType

                    If strprop = "UnableComprehensiveHistory" Then
                        If strprop.Contains("History") Then
                            'Add Items in combo box
                            If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                                cmbstddata.Items.Add(strprop)
                            End If
                        End If
                    End If
                Next
                chkAssociatestddata.Text = "Associate standard History"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function fillbooleancombobox()

        Dim strprop As String = ""
        Dim pType As Type

        'For Management Option
        If (cmbFieldCategory.Text = "Management option") Then
            cmbstddata.Items.Clear()

            'Declare a variable for PropertyInfo
            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexManagementOptions).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType

                If strprop = "DecisionObtainMedicalRecsOther" Or strprop = "ReviewMedicalRecsOther" Or strprop = "DiscussCaseWHealthProvider" Then

                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        cmbstddata.Items.Add(strprop)
                    End If

                End If

            Next
            chkAssociatestddata.Text = "Associated Management Option"

        End If
        'For Labs
        If (cmbFieldCategory.Text = "Labs") Then
            cmbstddata.Items.Clear()

            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexLabs).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType

                If strprop = "IndependentVisualTest" Or strprop = "DiscussionWPerformingPhys" Then
                    If pType.Name = "Boolean" Then
                        cmbstddata.Items.Add(strprop)
                    End If
                End If
            Next

            chkAssociatestddata.Text = "Associated Labs"
        End If

        'For X-Ray/Radiology
        If (cmbFieldCategory.Text = "X-Ray/Radiology") Then
            cmbstddata.Items.Clear()
            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType

                If strprop = "IndependentVisualTest" Or strprop = "DiscussWPerformingPhys" Then

                    If pType.Name = "Boolean" Then
                        cmbstddata.Items.Add(strprop)
                    End If
                End If

            Next
            chkAssociatestddata.Text = "Associated X-Ray/Radiology"
        End If

        'For Other Diagonsis Tests
        If (cmbFieldCategory.Text = "Other Diagonsis Tests") Then
            cmbstddata.Items.Clear()

            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            For Each propertyInfo As PropertyInfo In propertyInfos
                strprop = propertyInfo.Name
                pType = propertyInfo.PropertyType
                If strprop = "IndependentVisualTest" Or strprop = "DiscussWPerformingPhys" Then

                    If pType.Name = "Boolean" Then
                        cmbstddata.Items.Add(strprop)
                    End If
                End If

            Next
            chkAssociatestddata.Text = "Associated Other Diagonsis Tests"
        End If
        Return Nothing
    End Function

    Private Sub chkAssociatestddata_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAssociatestddata.CheckedChanged

        'Check Whether check box is Ticked or not
        If chkAssociatestddata.Checked = True Then
            pnlassociateStdItem.Visible = True

            If cmbDataType.Text = "Boolean" Then
                chkAssociatestddata.Visible = True
                fillbooleancombobox()
                dgItemList.Columns(3).Visible = True
            Else
                fillcombobox()
                dgItemList.Columns(3).Visible = True
            End If
            'ElseIf cmbDataType.Text = "Multiple Selection" Then
            '    chkAssociatestddata.Visible = True
        Else
            cmbstddata.SelectedIndex = -1
            'Make Associated Item Check box invisible
            pnlassociateStdItem.Visible = False
            dgItemList.Columns(3).Visible = False
        End If
    End Sub

    Private Sub CmbControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbControl.SelectedIndexChanged

    End Sub
    Private Sub btnaddphyexam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddstandreddata.Click

        'Declaration of Variables
        Dim i As Integer
        Dim j As Integer
        Dim strcat As String
        Dim stritem As String
        'Dim CategoryName As String
        'Dim ItemName As String
        'Dim nSelectedIndex As Integer
        'Dim AssociateCategory As String
        'Dim AssociatedItem As String
        'Dim AssocaitedProperty As String
        'Dim labs As String
        'Dim s1 As String
        Dim strSubcat As String = ""
        Dim strSubItem As String = ""
        'Handle Exception using Try-Catch block

        'Commented by SHUBHANGI 20090921
        'If txtcategory.Text = "" And txtCatItem.Text = "" Then
        '    MessageBox.Show("Please Enter Category and Item Value", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        'Else
        Try

            'For loop for Associated category
            For i = 0 To cmbAssociatedCategory.Items.Count - 1
                cmbAssociatedCategory.SelectedIndex = i
                strcat = cmbAssociatedCategory.Text

                cmbAssociatedCategory_SelectionChangeCommitted(sender, e)
                'For loop for Associated Items
                For j = 0 To cmbAssoicatedItem.Items.Count - 1
                    cmbAssoicatedItem.SelectedIndex = j
                    stritem = cmbAssoicatedItem.Text
                    cmbAssoicatedItem_SelectionChangeCommitted(sender, e)
                    If CmbControl.Text = "Check Box" Then
                        enumControlType = ControlType.CheckBox
                    ElseIf CmbControl.Text = "Text" Then
                        enumControlType = ControlType.Text
                    Else
                        MessageBox.Show("Please Select Control Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        CmbControl.Focus()
                        Exit Sub
                    End If
                    If cmbAssociateSubItem.Visible = True Then
                        For k As Integer = 0 To cmbAssociateSubItem.Items.Count - 1
                            cmbAssociateSubItem.SelectedIndex = k
                            strSubcat = strcat & "-" & stritem
                            strSubItem = cmbAssociateSubItem.Text
                            InsertData(strSubcat, strSubItem, enumControlType, strSubcat, strSubItem)
                        Next
                    Else

                        If InsertData(strcat, stritem, enumControlType, strcat, stritem) = False Then
                            Exit Sub
                        End If
                    End If


                Next
            Next
            'Catch Block Which Throw Exception
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        'End If
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub btnaddassociated_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddassociated.Click

        'Declarae Variables
        Dim i As Integer
        Dim strassitem As String = ""
        'Dim ManagementOption As String
        'Dim strValue As Integer
        'Dim strControl As String
        Try
            If cmbDataType.Text = "Boolean" Then
                MessageBox.Show("Only one value allowed with Associated Data", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            Else
                'If dgItemList is empty
                If dgItemList.Rows.Count = 0 Then
                    'Shubhangi
                    'Set the control type is which is selected at the tome of edit
                    For i = 0 To cmbstddata.Items.Count - 1
                        cmbstddata.SelectedIndex = i
                        strassitem = cmbstddata.Text
                        If CmbControl.Text = "Check Box" Then
                            enumControlType = ControlType.CheckBox
                        ElseIf CmbControl.Text = "Text" Then
                            enumControlType = ControlType.Text
                        Else
                            MessageBox.Show("Please Select Control Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                            CmbControl.Focus()
                            Exit Sub
                        End If

                        'SHUBHANGI 20090926 
                        'Allow to record present with same item & different Category
                        dgItemList.Rows.Add()
                        dgItemList.Item(0, dgItemList.Rows.Count - 1).Value = enumControlType.GetHashCode()
                        dgItemList.Item(1, dgItemList.Rows.Count - 1).Value = strassitem
                        dgItemList.Item(2, dgItemList.Rows.Count - 1).Value = enumControlType.ToString()
                        dgItemList.Item(3, dgItemList.Rows.Count - 1).Value = strassitem
                    Next
                    IsModified = True 'Set the flag to show that record is modified

                    'If dgItemList is not empty then check item &it's control to avoid same record
                ElseIf dgItemList.Rows.Count > 0 Then
                    cmbstddata.SelectedIndex = 1
                    If CmbControl.Text = "Check Box" Then
                        enumControlType = ControlType.CheckBox
                    ElseIf CmbControl.Text = "Text" Then
                        enumControlType = ControlType.Text
                    Else
                        MessageBox.Show("Please Select Control Type", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        CmbControl.Focus()
                        Exit Sub
                    End If

                    Dim j As Integer
                    'Set Boolean variable to display message only once
                    Dim isMEssaegShown As Boolean = False
                    For i = 0 To cmbstddata.Items.Count - 1
                        'Set Boolean variable for displaying remaing records except which are already present.
                        Dim isItemExists As Boolean = False
                        For j = 0 To dgItemList.Rows.Count - 1
                            cmbstddata.SelectedIndex = i
                            strassitem = cmbstddata.Text
                            If dgItemList.Item(1, j).Value = strassitem And dgItemList.Item(2, j).Value = enumControlType.ToString Then
                                If isMEssaegShown = False Then
                                    MessageBox.Show("Item along with same category is already present in Table", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                                    isMEssaegShown = True
                                End If
                                isItemExists = True
                                Exit For
                            End If
                        Next
                        'Check variable if it is not present then add
                        If (isItemExists = False) Then
                            dgItemList.Rows.Add()
                            dgItemList.Item(0, dgItemList.Rows.Count - 1).Value = enumControlType.GetHashCode()
                            dgItemList.Item(1, dgItemList.Rows.Count - 1).Value = strassitem
                            dgItemList.Item(2, dgItemList.Rows.Count - 1).Value = enumControlType.ToString()
                            dgItemList.Item(3, dgItemList.Rows.Count - 1).Value = strassitem
                        End If

                    Next
                End If
            End If
            'Catch Block Which Throw Exception
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub


    Private Sub btnaddfieldvalue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddfieldvalue.Click


        'Display selected Associated Item in Feild Value text box
        txtItem.Text = cmbstddata.Text
    End Sub

    Private Sub btnaddcategory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcategory.Click

        'Display selected Associated Category in Category text box
        txtcategory.Text = cmbAssociatedCategory.Text

        'Display selected Associated Item in Category Item text box
        txtCatItem.Text = cmbAssoicatedItem.Text
    End Sub

    Private Sub pnlFieldValues_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlFieldValues.Paint

    End Sub

    Private Sub txtField_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmbDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataType.SelectedIndexChanged
        'If cmbDataType.Text.ToString.Trim = "Multiple Selection" Then
        '    chkAssociatestddata.Visible = True
        'End If
        'If cmbDataType.Text.ToString.Trim = "Table" Then
        '    chkAssociatestddata.Visible = True
        'End If
        'If cmbDataType.Text.ToString.Trim = "Boolean" Then
        '    chkAssociatestddata.Visible = True
        'End If
        'If cmbDataType.Text.Trim = "Single Selection" Then
        '    chkAssociatestddata.Checked = False
        'End If
        'Shubhangi 20091026
        'If we open record of Field type Table for modify & then change the Field category, it should enable all Fields.
        txtCaption.Enabled = True
        txtcategory.Enabled = True
        txtCatItem.Enabled = True
        CmbControl.Enabled = True
        cmbFieldCategory_SelectionChangeCommitted(Nothing, Nothing)
        txtItem.ResetText()

        txtItem.Enabled = True
    End Sub

    Private Sub cmbFieldCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFieldCategory.SelectedIndexChanged
        'Shubhangi 20091026
        'Display message box when we change the category that "Change of the category field will clear all the table field. Do you want to save it?"
        If cmbDataType.Text.Trim <> "Choose an item" And cmbFieldCategory.Text.Trim = "HPI" And cmbDataType.Text.Trim = "Text" Then
            pnlHPIExtended.Visible = True
            RdbtnBrief.Checked = False
            RdbtnExtended.Checked = False
        Else
            pnlHPIExtended.Visible = False
            RdbtnBrief.Checked = False
            RdbtnExtended.Checked = False
        End If

        'DISPLAY MESSAGE WHEN WE CHANGE FIELD TYPE
        'If cmbFieldCategory.Text <> "Choose an item" And dgItemList.Rows.Count <> 0 Then
        If cmbFieldCategory.Text <> "Choose an item" And dgItemList.Rows.Count <> 0 And cmbFieldCategory.Text <> _selectedCategory Then
            If MessageBox.Show("Change of Field Category clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                dgItemList.Rows.Clear()
                'Shubhangi 20091026
                'Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
                'It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event
                IsforModify = False
            Else
                cmbFieldCategory.Text = _selectedCategory
                'chkAssociatestddata.Text(_selectedCategory)

                Exit Sub
            End If
        End If
        If cmbFieldCategory.Text <> "Choose an item" And dgTableField.Rows.Count <> 0 And cmbFieldCategory.Text <> _selectedCategory Then
            If MessageBox.Show("Change of Data type clear Table Field. Do you want to Continue? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                dgTableField.Rows.Clear()
                'Shubhangi 20091026
                'Make this variable False B'Coz we select Multiple selection & add record, Open the record for modify then the field type as single selection & try to add data
                'It was giving an error: B'coz now IsforModify variable is true so make it False on data type chage event
                IsforModify = False
            Else
                cmbFieldCategory.Text = _selectedCategory

                Exit Sub
            End If
        End If

        If cmbFieldCategory.Text.ToString.Trim = "Physical Examination" Then
            chkAssociateStd.Visible = True
            chkAssociatestddata.Visible = False
        Else
            chkAssociateStd.Visible = False
            chkAssociateStd.Checked = False
            dgTableField.Columns(5).Visible = False
            dgTableField.Columns(6).Visible = False
        End If
        'SHUBHANHGI
        If cmbFieldCategory.Text.Trim = "Management option" And cmbDataType.Text.ToString.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Managment option"

            'If Category is Labs and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Labs" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Labs"

            'If Category is X-Ray/Radiology and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "X-Ray/Radiology" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard X-Ray/Radiology"

            'If Category is Other Diagnosis Test and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Other Diagonsis Tests" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Other Diagonsis Tests"

            'If Category is HPI and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "HPI" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard HPI"

            'If Category is ROS and Field Type is Multiple Selection Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "ROS" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard ROS"

            'Else

            '    chkAssociatestddata.Checked = False
            '    chkAssociatestddata.Visible = False
            '    pnlStandardEM.Visible = False
        End If

        'If Category is History and Field Type is Boolean Then
        If cmbFieldCategory.Text.ToString.Trim = "History" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillcombobox()
            chkAssociatestddata.Text = "Associate standard Histroy"

            'If Category is Labs and Field Type is Boolean Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Labs" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Labs"

            'If Category is Management Option and Field Type is Boolean Then
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Management option" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Management Option"

        ElseIf cmbFieldCategory.Text.ToString.Trim = "X-Ray/Radiology" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated X-Ray/Radiology"

        ElseIf cmbFieldCategory.Text.ToString.Trim = "Other Diagonsis Tests" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = True
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
            fillbooleancombobox()
            chkAssociatestddata.Text = "Associated Other Diagonsis Tests"
        ElseIf cmbFieldCategory.Text.ToString.Trim = "General" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Choose an item" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Physical Examination" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "ROS" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "HPI" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Medical Decision-Making" And cmbDataType.Text.Trim = "Boolean" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        End If

        If cmbFieldCategory.Text.ToString.Trim = "Medical Decision-Making" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "General" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        ElseIf cmbFieldCategory.Text.ToString.Trim = "Choose an item" And cmbDataType.Text.Trim = "Multiple Selection" Then
            chkAssociatestddata.Visible = False
            chkAssociatestddata.Checked = False
            pnlStandardEM.Visible = False
        End If
        'End If
        'chkAssociatestddata.Visible = True
        'dgItemList.RowCount = 0

        txtItem.Enabled = True
    End Sub

    Private Sub frmLiquidData_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseHover


    End Sub

    Private Sub frmLiquidData_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave

    End Sub

    Private Sub btnaddfieldvalue_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddfieldvalue.MouseHover

        'Set tool tip property to add field value
        ToolTip1.SetToolTip(btnaddfieldvalue, "Add Field Value")
    End Sub

    Private Sub btnaddassociated_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddassociated.MouseHover

        'set tool tip property to add associated
        ToolTip1.SetToolTip(btnaddassociated, "Add Element")
    End Sub

    Private Sub btnaddfieldvalue_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddfieldvalue.MouseLeave

        'Remove tool tip property from add field 
        ToolTip1.RemoveAll()
    End Sub

    Private Sub btnaddcategory_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcategory.MouseHover
        ToolTip1.SetToolTip(btnaddcategory, "Add Category and Item")
    End Sub

    Private Sub btnaddstandreddata_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddstandreddata.MouseHover

        'Set Tool Tip Property for button add standred
        ToolTip1.SetToolTip(btnaddstandreddata, "Add Standard Data")

    End Sub

    Private Sub btnaddstandreddata_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnaddcategory_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddcategory.MouseLeave

        'Set Tool Tip Property for button add category
        ToolTip1.SetToolTip(btnaddcategory, "Add Category and Item")

    End Sub

    Private Sub btnAdd_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnAdd_MouseLeave_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        ToolTip1.RemoveAll()

    End Sub

    Private Sub btnRemove_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.MouseEnter
        ToolTip1.SetToolTip(btnRemove, "Remove")

    End Sub

    Private Sub btn_Refresh_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Refresh.MouseEnter
        ToolTip1.SetToolTip(btn_Refresh, "Refresh")
    End Sub

    Private Sub btn_Refresh_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Refresh.MouseLeave
        ToolTip1.RemoveAll()
    End Sub

    Private Sub btncatAdd_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncatAdd.MouseEnter
        ToolTip1.SetToolTip(btncatAdd, "Add")
    End Sub

    Private Sub btnDelete_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.MouseEnter
        ToolTip1.SetToolTip(btnDelete, "Delete")
    End Sub

    Private Sub btnRefresh_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.MouseEnter
        ToolTip1.SetToolTip(btnRefresh, "Refresh")
    End Sub

    Private Sub btnCatModify_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCatModify.MouseEnter
        ToolTip1.SetToolTip(btnCatModify, "Modify")

    End Sub

    Private Sub cmbAssociatedCategory_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssociatedCategory.MouseHover
        ToolTip1.SetToolTip(cmbAssociatedCategory, cmbAssociatedCategory.Text)
    End Sub

    Private Sub cmbAssoicatedItem_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssoicatedItem.MouseHover
        ToolTip1.SetToolTip(cmbAssoicatedItem, cmbAssoicatedItem.Text)
    End Sub

    Private Sub cmbAssociateSubItem_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAssociateSubItem.MouseHover
        ToolTip1.SetToolTip(cmbAssociateSubItem, cmbAssociateSubItem.Text)
    End Sub

    Private Sub cmbstddata_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstddata.MouseHover
        ToolTip1.SetToolTip(cmbstddata, cmbstddata.Text)
    End Sub

    Private Sub CmbControl_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbControl.MouseHover
        ToolTip1.SetToolTip(CmbControl, CmbControl.Text)
    End Sub

    Private Sub cmbFieldCategory_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFieldCategory.MouseHover
        ToolTip1.SetToolTip(cmbFieldCategory, cmbFieldCategory.Text)
    End Sub

    Private Sub cmbDataType_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataType.MouseHover
        ToolTip1.SetToolTip(cmbDataType, cmbDataType.Text)
    End Sub

    Private Sub txtField_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtField.TextChanged

    End Sub


    Private Sub dgTableField_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgTableField.MouseDown
        nRow = -1

        Try

            Dim htInfo As DataGridView.HitTestInfo = dgTableField.HitTest(e.X, e.Y)
            If dgTableField.Rows.Count > 0 Then

                'Dim hti As DataGrid.HitTestInfo = dgTableField.HitTest(point)
                If e.Button = MouseButtons.Right Then


                    ' '' Mahesh 20071001
                    ' '' On Empty Area on the Patient Grid Should not display Context Menu
                    If htInfo.RowIndex >= 0 Then
                        'Fill templates in menu
                        ' FillMenus()
                        nRow = htInfo.RowIndex
                        cmnuDelete.Items.Clear()
                        Dim oMenuItem As New ToolStripMenuItem
                        oMenuItem.Text = "Delete Table Field"
                        oMenuItem.Tag = "Delete Table Field"
                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                        'oMenuItem.Image = Imgts_PatientDetails.Images(24)
                        cmnuDelete.Items.Add(oMenuItem)
                        dgTableField.Rows(nRow).Selected = True
                        'SHUBHANGI 20090923
                        'To select dgTable Field row & Cells (0) b'coz row containing collection of cells start with 0th cell
                        dgTableField.CurrentCell = dgTableField.Rows(nRow).Cells(0)
                        'Try
                        '    If (IsNothing(dgTableField.ContextMenuStrip) = False) Then
                        '        dgTableField.ContextMenuStrip.Dispose()
                        '        dgTableField.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgTableField.ContextMenuStrip = cmnuDelete
                        AddHandler oMenuItem.Click, AddressOf cmnuDelete_Click
                        oMenuItem = Nothing

                        oMenuItem = New ToolStripMenuItem
                        oMenuItem.Text = "Edit Table Field"
                        oMenuItem.Tag = "Edit Table Field"

                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                        'oMenuItem.Image = Imgts_PatientDetails.Images(24)
                        cmnuDelete.Items.Add(oMenuItem)
                        dgTableField.Rows(nRow).Selected = True
                        'To select dgTable Field row & Cells (0) b'coz row containing collection of cells start with 0th cell
                        dgTableField.CurrentCell = dgTableField.Rows(nRow).Cells(0)
                        'Try
                        '    If (IsNothing(dgTableField.ContextMenuStrip) = False) Then
                        '        dgTableField.ContextMenuStrip.Dispose()
                        '        dgTableField.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgTableField.ContextMenuStrip = cmnuDelete
                        AddHandler oMenuItem.Click, AddressOf cmnuEdit_Click

                    Else
                        'Try
                        '    If (IsNothing(dgTableField.ContextMenu) = False) Then
                        '        dgTableField.ContextMenu.Dispose()
                        '        dgTableField.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgTableField.ContextMenu = Nothing
                    End If
                    '
                Else

                    If htInfo.RowIndex >= 0 Then
                        'Fill templates in menu
                        ' FillMenus()
                        nRow = htInfo.RowIndex
                    Else
                        'Messagebox.Show("",gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information)
                        'Try
                        '    If (IsNothing(dgTableField.ContextMenu) = False) Then
                        '        dgTableField.ContextMenu.Dispose()
                        '        dgTableField.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgTableField.ContextMenu = Nothing
                        Exit Sub
                    End If
                    'Try
                    '    If (IsNothing(dgTableField.ContextMenuStrip) = False) Then
                    '        dgTableField.ContextMenuStrip.Dispose()
                    '        dgTableField.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    dgTableField.ContextMenuStrip = cmnuDelete
                End If
            Else
                'Try
                '    If (IsNothing(dgTableField.ContextMenuStrip) = False) Then
                '        dgTableField.ContextMenuStrip.Dispose()
                '        dgTableField.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                dgTableField.ContextMenuStrip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Private Sub MenuDelete_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs)

    'End Sub

    Private Sub cmnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If CType(sender, ToolStripMenuItem).Tag.ToString.ToUpper = "Delete Table field".ToString.ToUpper Then
            btnDelete_Click(sender, e)
        ElseIf CType(sender, ToolStripMenuItem).Tag.ToString.ToUpper = "Delete Item field".ToString.ToUpper Then
            btnRemove_Click(sender, e)
        End If

    End Sub


    Private Sub cmnuEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If CType(sender, ToolStripMenuItem).Tag.ToString.ToUpper = "Edit Table Field".ToString.ToUpper Then
            TableField()
        Else
            If CType(sender, ToolStripMenuItem).Tag.ToString.ToUpper = "Edit Item Field".ToString.ToUpper Then
                ItemField()
            End If
        End If

        ' btncatAdd_Click(sender, e)

    End Sub


    Private Sub dgItemList_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgItemList.MouseDown

        'SHUBHANGI 20090922
        'Add context nenu to edit the fields of Data grid View.
        nRow = -1
        Try
            If dgItemList.Rows.Count > 0 Then
                Dim htInfo As DataGridView.HitTestInfo = dgItemList.HitTest(e.X, e.Y)
                'Dim hti As DataGrid.HitTestInfo = dgTableField.HitTest(point)
                If e.Button = MouseButtons.Right Then

                    ' '' On Empty Area on the Patient Grid Should not display Context Menu
                    If htInfo.RowIndex >= 0 Then

                        nRow = htInfo.RowIndex
                        cmnuDelete.Items.Clear()
                        Dim oMenuItem As New ToolStripMenuItem
                        oMenuItem.Text = "Delete Item Field"
                        oMenuItem.Tag = "Delete Item Field"
                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)

                        cmnuDelete.Items.Add(oMenuItem)
                        dgItemList.Rows(nRow).Selected = True
                        'SHUBHANGI 20090923
                        'To select dgTable Field row & Cells (0) b'coz row containing collection of cells start with 1th cell (not from visible false cell)
                        dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)
                        'Try
                        '    If (IsNothing(dgItemList.ContextMenuStrip) = False) Then
                        '        dgItemList.ContextMenuStrip.Dispose()
                        '        dgItemList.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgItemList.ContextMenuStrip = cmnuDelete

                        AddHandler oMenuItem.Click, AddressOf cmnuDelete_Click
                        oMenuItem = Nothing


                        oMenuItem = New ToolStripMenuItem
                        ' IsforDelete = False
                        oMenuItem.Text = "Edit Item Field"
                        oMenuItem.Tag = "Edit Item Field"
                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125)
                        'oMenuItem.Image = Imgts_PatientDetails.Images(24)
                        cmnuDelete.Items.Add(oMenuItem)
                        'dgItemList.Rows(nRow).Selected = True
                        'To select dgTable Field row & Cells (0) b'coz row containing collection of cells start with 1st cell (not from visible false cell)
                        dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)
                        'Try
                        '    If (IsNothing(dgItemList.ContextMenuStrip) = False) Then
                        '        dgItemList.ContextMenuStrip.Dispose()
                        '        dgItemList.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgItemList.ContextMenuStrip = cmnuDelete
                        AddHandler oMenuItem.Click, AddressOf cmnuEdit_Click

                    Else
                        'Try
                        '    If (IsNothing(dgItemList.ContextMenu) = False) Then
                        '        dgItemList.ContextMenu.Dispose()
                        '        dgItemList.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgItemList.ContextMenu = Nothing
                    End If
                    '
                Else
                    If htInfo.RowIndex >= 0 Then
                        'Fill templates in menu
                        ' FillMenus()
                        nRow = htInfo.RowIndex
                        dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)
                    Else
                        'Messagebox.Show("",gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information)
                        'Try
                        '    If (IsNothing(dgItemList.ContextMenu) = False) Then
                        '        dgItemList.ContextMenu.Dispose()
                        '        dgItemList.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        dgItemList.ContextMenu = Nothing
                        Exit Sub
                    End If
                    'Try
                    '    If (IsNothing(dgItemList.ContextMenuStrip) = False) Then
                    '        dgItemList.ContextMenuStrip.Dispose()
                    '        dgItemList.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    dgItemList.ContextMenuStrip = cmnuDelete
                End If
            Else
                'Try
                '    If (IsNothing(dgItemList.ContextMenuStrip) = False) Then
                '        dgItemList.ContextMenuStrip.Dispose()
                '        dgItemList.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                dgItemList.ContextMenuStrip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub


    Private Sub btnItemDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItemDown.Click

        'SHUBHANGI 20090925
        'Allow for the ability to change the order of items in the field table.
        Try
            ' dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)
            If dgItemList.RowCount > 0 Then
                Index1 = dgItemList.CurrentRow.Index
                Dim Index2 As Integer
                Index2 = Index1
                If Index1 <> dgItemList.Rows.Count - 1 Then
                    Index1 = Index1 + 1
                Else
                    Exit Sub
                    Index1 = 0
                End If

                If dgItemList.Rows.Count > 0 Then
                    Dim r As New DataGridViewRow

                    'Take the which we want to shift
                    r = dgItemList.Rows(Index2)

                    ' dgItemList.CurrentCell = dgItemList.Rows(Index1).Cells(1)
                    'First remove the row which we want to remove o.w. it will give the error that row is already present
                    dgItemList.Rows.Remove(r)

                    'Indsert that row
                    dgItemList.Rows.Insert(Index1, r)

                    'Set the focus to the shifted row
                    dgItemList.CurrentCell = dgItemList.Rows(Index1).Cells(1)
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub


    Private Sub btnItemUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnItemUp.Click

        'SHUBHANGI 20090925
        'Allow for the ability to change the order of items in the field table.
        Try
            If dgItemList.RowCount > 0 Then
                ' dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)
                Index1 = dgItemList.CurrentRow.Index
                Dim Index2 As Integer
                Index2 = Index1
                If Index1 <> 0 Then
                    Index1 = Index1 - 1
                Else
                    Index1 = 0
                End If

                If dgItemList.Rows.Count > 0 Then
                    Dim r As New DataGridViewRow

                    'Take the which we want to shift
                    r = dgItemList.Rows(Index2)

                    ' dgItemList.CurrentCell = dgItemList.Rows(Index1).Cells(1)
                    'First remove the row which we want to remove o.w. it will give the error that row is already present
                    dgItemList.Rows.Remove(r)

                    'Indsert that row
                    dgItemList.Rows.Insert(Index1, r)

                    'Set the focus to the shifted row
                    dgItemList.CurrentCell = dgItemList.Rows(Index1).Cells(1)
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub btnTableUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTableUp.Click
        'SHUBHANGI 20090925
        'Allow for the ability to change the order of items in the field table.
        Try
            ' dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)

            Dim cnt As Integer = 0
            If dgTableField.RowCount > 0 Then

                Index1 = dgTableField.CurrentRow.Index
                Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)   '_selItem take Selected value of Combo box
                Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                For i As Integer = 0 To dgTableField.Rows.Count - 1
                    If (_selCategory = dgTableField.Item(2, i).Value) Then
                        cnt = cnt + 1
                    End If
                Next
                ' If _selCategory <> Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value) Then
                Dim Index2 As Integer
                Index2 = Index1
                If _selItem <> "" And cnt > 2 Then

                    If Index1 <> 0 Then
                        Index1 = Index1 - 1
                    Else
                        Index1 = 0
                    End If
                    If dgTableField.Rows.Count > 0 Then
                        ' Dim prevIndex As Integer
                        'prevIndex = Index1 - 1
                        Dim _SelItemNew As String
                        Dim _SelCategoryNew As String

                        'Take the Category name & Item of next Index (At which index we want to shift record)
                        _SelCategoryNew = Convert.ToString(dgTableField.Item(0, Index1).Value)
                        _SelItemNew = Convert.ToString(dgTableField.Item(1, Index1).Value)

                        'Check whether selected record is item & it is last item of that Category 
                        'then it should not shift into next category
                        If _SelCategoryNew = "" And _SelItemNew <> "" Then

                            Dim r As New DataGridViewRow

                            'Take the row which we want to shift
                            r = dgTableField.Rows(Index2)

                            ' dgItemList.CurrentCell = dgItemList.Rows(Index1).Cells(1)
                            'First remove the row which we want to remove o.w. it will give the error that row is already present
                            dgTableField.Rows.Remove(r)

                            'Indsert that row
                            dgTableField.Rows.Insert(Index1, r)

                            'Set the focus to the shifted row
                            dgTableField.CurrentCell = dgTableField.Rows(Index1).Cells(1)
                        End If
                    End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub btnTableDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTableDown.Click
        'SHUBHANGI 20090925
        'Allow for the ability to change the order of items in the field table.
        Try
            ' dgItemList.CurrentCell = dgItemList.Rows(nRow).Cells(1)

            Dim cnt As Integer = 0
            If dgTableField.RowCount > 0 Then
                Index1 = dgTableField.CurrentRow.Index
                Dim _selItem As String = Convert.ToString(dgTableField.Item(1, dgTableField.CurrentRow.Index).Value)   '_selItem take Selected value of Combo box
                Dim _selCategory As String = Convert.ToString(dgTableField.Item(2, dgTableField.CurrentRow.Index).Value)
                For i As Integer = 0 To dgTableField.Rows.Count - 1
                    If (_selCategory = dgTableField.Item(2, i).Value) Then
                        cnt = cnt + 1
                    End If
                Next

                Dim Index2 As Integer
                Index2 = Index1
                If _selItem <> "" And cnt > 2 And Index1 < dgTableField.Rows.Count - 1 Then

                    If Index1 <> 0 Then
                        Index1 = Index1 + 1
                    Else
                        Index1 = 0
                    End If
                Else
                    Exit Sub   'If Selected record is last then Exit Sub
                End If
                'Check whether record is present or not
                If dgTableField.Rows.Count > 0 Then
                    'Dim prevIndex As Integer
                    Dim _SelItemNew As String
                    Dim _SelCategoryNew As String
                    'Take the Category name & Item of next Index (At which index we want to shift record)
                    _SelCategoryNew = Convert.ToString(dgTableField.Item(0, Index1).Value)
                    _SelItemNew = Convert.ToString(dgTableField.Item(1, Index1).Value)

                    'Check whether selected record is item & it is last item of that Category 
                    'then it should not shift into next category
                    If _SelCategoryNew = "" And _SelItemNew <> "" Then
                        Dim r As New DataGridViewRow

                        'Take the row which we want to shift
                        r = dgTableField.Rows(Index2)

                        'First remove the row which we want to remove o.w. it will give the error that row is already present
                        dgTableField.Rows.Remove(r)

                        'Indsert that row
                        dgTableField.Rows.Insert(Index1, r)

                        'Set the focus to the shifted row
                        dgTableField.CurrentCell = dgTableField.Rows(Index1).Cells(1)
                    End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.InsertField, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Label31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("LiquidData", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmgloCommunityViewData.Name, FrmgloCommunityViewData.Text) = False Then
                Try
                    With FrmgloCommunityViewData
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ''Added for fixed Bug # : 35658 on 20120904
    Private Function CheckUser() As Boolean
        Dim oCommunity As gloCommunity.Classes.clsgloCommunityUsers = Nothing
        Dim _blnUserCheck As Boolean = False
        Try
            oCommunity = New gloCommunity.Classes.clsgloCommunityUsers()
            _blnUserCheck = oCommunity.CheckAuthentication()
        Catch ex As Exception
        Finally
            If Not IsNothing(oCommunity) Then
                oCommunity = Nothing
            End If
        End Try
        Return _blnUserCheck
    End Function
    ''End
End Class