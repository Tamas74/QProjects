Imports System.Windows.Forms
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

Public Class frmUserCustomLinkConfigurations

#Region "Global variables declaration"

   
    Private _gloEMRUserCollection As New Collection
    Private _CustomLinksCollection As New Collection
    Private _EncryptionMethodCollection As New Collection

    Private _dupLinkName As String = String.Empty
    Private _dupUserName As String = String.Empty
    Private _txtMaxLength_Editor As New TextBox

#End Region
    Dim _strLinkName As String = String.Empty

#Region " C1 Constants "

    Private Const C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID As Byte = 0
    Private Const C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME As Byte = 1
    Private Const C1CUSTLNKUSRASSGN_COL_LINK_NAME As Byte = 2
    Private Const C1CUSTLNKUSRASSGN_COL_EXT_LOGINID As Byte = 3
    Private Const C1CUSTLNKUSRASSGN_COL_EXT_PWD As Byte = 4
    Private Const C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD As Byte = 5
    Private Const C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD As Byte = 6
    Private Const C1CUSTLNKUSRASSGN_COL_COUNT As Byte = 7

#End Region

#Region "User Defined Functions"
    ''Function to set format the grid
    Private Sub DesignC1CUSTLNKUSRASSNGGrid()
        flxData.DataSource = Nothing
        flxData.Clear(ClearFlags.All)

        With flxData
            .Rows.Fixed = 1
            .Rows.Count = 1
            .Cols.Count = C1CUSTLNKUSRASSGN_COL_COUNT

            .AllowDragging = AllowDraggingEnum.None

            .Cols(C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID).Width = 0.1 * .Width
            .Cols(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME).Width = 0.18 * .Width
            .Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).Width = 0.18 * Width
            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_LOGINID).Width = 0.18 * Width
            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_PWD).Width = 0.15 * .Width
            .Cols(C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD).Width = 0.18 * Width
            .Cols(C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD).Width = 0.12 * Width

            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID, "User External ID")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME, "gloEMR User Name")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_LINK_NAME, "Link")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_EXT_LOGINID, "Login ID")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_EXT_PWD, "Password")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD, "Confirm Password")
            flxData.SetData(0, C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD, "Encryption")

            '.Cols(C1CUSTLNKUSRASSNG_COL_USER_EXTERNAL_ID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            '.Cols(C1CUSTLNKUSRASSNG_COL_USER_EXTERNAL_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID).Visible = False

            .Cols(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            '.Cols(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME).AllowEditing = True

            .Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            '.Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).AllowEditing = True

            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_LOGINID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_LOGINID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_PWD).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_EXT_PWD).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

            .Cols(C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            '.Cols(C1CUSTLNKUSRASSNG_COL_ENCRYPTION_TYPE).AllowEditing = True

            .DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw

            .AllowSorting = AllowSortingEnum.None
            .SelectionMode = SelectionModeEnum.Row
            .AllowDragging = AllowDraggingEnum.None
        End With

    End Sub

    ''' <summary>
    ''' Code for checking duplicates
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckDuplicateRecords() As Boolean
        _dupLinkName = String.Empty
        _dupUserName = String.Empty
        Dim objAudit As New clsAudit
        Dim _lstUserCustom_LinkCollection As New Generic.List(Of String) ' New ArrayList()
        Try
            _gloEMRUserCollection = objAudit.Fill_Users()
            If _CustomLinksCollection.Count > 0 Then
                Dim nCount As Int16
                For nCount = 1 To flxData.Rows.Count - 1
                    If Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim() <> String.Empty And Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim() <> String.Empty Then
                        If Not _lstUserCustom_LinkCollection.Contains(Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim() & "," & Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim()) Then ' & "," & Convert.ToString(flxData.GetData(nCount, COL_USER)).Trim()) Then
                            _lstUserCustom_LinkCollection.Add(Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim() & "," & Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim()) ' & "," & Convert.ToString(flxData.GetData(nCount, COL_USER)).Trim())
                        Else
                            _dupLinkName = Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim()
                            _dupUserName = Convert.ToString(flxData.GetData(nCount, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim()
                            Return False
                        End If
                    End If
                Next
                Return True
            End If
        Catch ex As Exception
            Return False
        Finally
            _lstUserCustom_LinkCollection = Nothing
            objAudit = Nothing
        End Try
    End Function
#End Region

#Region "Form Events"

    Private Sub frmUserCustomLinkConfigurations_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _txtMaxLength_Editor.Text = String.Empty
        gloC1FlexStyle.Style(flxData)
        BindDataToGrid()
    End Sub

    Private Sub BindDataToGrid()
        Dim Encryption As String = String.Empty
        Try
            Call DesignC1CUSTLNKUSRASSNGGrid()
            _EncryptionMethodCollection.Add("None")
            _EncryptionMethodCollection.Add("3 DES")
            Encryption = " "
            Dim nCount As Int16 = 0
            For nCount = 1 To _EncryptionMethodCollection.Count
                Encryption += "|" & _EncryptionMethodCollection(nCount)
                'If flxData.Rows.Count <= clEncryption.Count Then
                '    flxData.Rows.Add()
                'End If
            Next
            flxData.Cols(C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD).ComboList = Encryption

            retriveUserCustomLink()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub flxData_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles flxData.EnterCell
        Dim _gloUser As String = String.Empty
        Dim _objAudit As New clsAudit
        Dim _nCount As Int16 = 0
        Try

            _gloEMRUserCollection = _objAudit.Fill_Users()

            If _gloEMRUserCollection.Count > 0 Then
                _gloUser = " "
                For _nCount = 1 To _gloEMRUserCollection.Count
                    _gloUser += "|" & _gloEMRUserCollection(_nCount)
                    'If flxData.Rows.Count <= gloEMRUsers.Count Then
                    '    flxData.Rows.Add()
                    'End If
                Next

                flxData.Cols(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME).ComboList = _gloUser
            End If

            _CustomLinksCollection = Fill_LinkName()
            If _CustomLinksCollection.Count > 0 Then
                _strLinkName = " "
                For _nCount = 1 To _CustomLinksCollection.Count
                    _strLinkName += "|" & _CustomLinksCollection(_nCount)
                    'If flxData.Rows.Count <= cCustomLinkMSTLink.Count Then
                    '    flxData.Rows.Add()
                    'End If
                Next
                flxData.Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).ComboList = _strLinkName
            End If

            If flxData.ColSel = C1CUSTLNKUSRASSGN_COL_EXT_PWD Then
                'Dim tb As New TextBox
                _txtMaxLength_Editor.MaxLength = 49
                _txtMaxLength_Editor.PasswordChar = "*"
                flxData.Cols(C1CUSTLNKUSRASSGN_COL_EXT_PWD).Editor = _txtMaxLength_Editor
            ElseIf flxData.ColSel = C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD Then
                'Dim tb As New TextBox
                _txtMaxLength_Editor.MaxLength = 49
                _txtMaxLength_Editor.PasswordChar = "*"
                flxData.Cols(C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD).Editor = _txtMaxLength_Editor
            ElseIf flxData.ColSel = C1CUSTLNKUSRASSGN_COL_EXT_LOGINID Then
                _txtMaxLength_Editor.MaxLength = 99
                _txtMaxLength_Editor.PasswordChar = ""
                flxData.Cols(C1CUSTLNKUSRASSGN_COL_EXT_LOGINID).Editor = _txtMaxLength_Editor
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            _objAudit = Nothing
            _gloUser = String.Empty
            _nCount = 0
            _strLinkName = String.Empty
        End Try
    End Sub

    Private Sub flxData_OwnerDrawCell(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles flxData.OwnerDrawCell
        If e.Row >= flxData.Rows.Fixed And flxData.Cols(e.Col).Index = C1CUSTLNKUSRASSGN_COL_EXT_PWD Then
            e.Text = New String("*"c, e.Text.Length)
        End If
        If e.Row >= flxData.Rows.Fixed And flxData.Cols(e.Col).Index = C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD Then
            e.Text = New String("*"c, e.Text.Length)
        End If
    End Sub

    Private Sub tstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked
        Dim intUserExternalID As Int64 = 0
        Try
            flxData.FinishEditing()
            Select Case Convert.ToString(e.ClickedItem.Tag).ToLower()
                Case "new"
                    AddNewRow()
                    'retriveCustomLinkUser()
                Case "saveclose"
                    AddUserCustomLink()
                    'retriveCustomLinkUser()
                Case "close"
                    If Not IsNothing(_txtMaxLength_Editor) Then
                        _txtMaxLength_Editor.Dispose()
                        _txtMaxLength_Editor = Nothing
                    End If
                    Me.Close()
                Case "delete"
                    If flxData.RowSel > 0 Then
                        ' If Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim() <> String.Empty Then
                        If Convert.ToInt64(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID)) <> 0 Then
                            If MessageBox.Show("Are you sure you want to delete selected user '" & Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim() & "'?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then

                                intUserExternalID = Convert.ToInt64(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID))

                                If intUserExternalID > 0 Then
                                    DeleteUserCustomLink(Convert.ToInt64(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID))) '.Trim(), intLinkID) ', Convert.ToString(flxData.GetData(flxData.RowSel, COL_LoginID)).Trim())
                                    flxData.Rows.Remove(flxData.RowSel)
                                End If
                                'DeleteUserCustomLink(Convert.ToString(flxData.GetData(flxData.RowSel, COL_EMRUSER)).Trim(), intLinkID) ', Convert.ToString(flxData.GetData(flxData.RowSel, COL_LoginID)).Trim())
                                'retriveCustomLinkUser()
                            End If
                        Else
                            'Code to check if user added record in grid and delete immediately by RK on 20110921
                            If Convert.ToString(flxData.GetData(flxData.RowSel, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim() <> String.Empty Then
                                If MessageBox.Show("Are you sure you want to delete the selected record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                    flxData.Rows.Remove(flxData.RowSel)
                                End If
                            End If
                        End If
                    End If
                Case "configure"
                    Dim ofrmCustomLink As New frmCustomLink()

                    ofrmCustomLink.ShowDialog()
                    ofrmCustomLink.Dispose()
                    ofrmCustomLink = Nothing

                    _CustomLinksCollection = Fill_LinkName()
                    Dim _nCount As Int16 = 0

                    If _CustomLinksCollection.Count > 0 Then
                        _strLinkName = " "
                        For _nCount = 1 To _CustomLinksCollection.Count
                            _strLinkName += "|" & _CustomLinksCollection(_nCount)
                            'If flxData.Rows.Count <= cCustomLinkMSTLink.Count Then
                            '    flxData.Rows.Add()
                            'End If
                        Next
                        flxData.Cols(C1CUSTLNKUSRASSGN_COL_LINK_NAME).ComboList = _strLinkName
                    End If

                    retriveUserCustomLink()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            intUserExternalID = 0
        End Try
        'retriveCustomLinkUser()
    End Sub

    ''' <summary>
    ''' Added new row with default value for Encryption column=None, UserExtrenalID=0
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddNewRow()
        Dim _RecordCnt As Int32 = flxData.Rows.Count
        flxData.Rows.Add()
        flxData.SetData(_RecordCnt, C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD, "None")
        flxData.SetData(_RecordCnt, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID, 0)
    End Sub

    ''' <summary>
    ''' Function for checking duplicate Records, validation
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddUserCustomLink()
        'flxData.FinishEditing()
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _strUserName As String = String.Empty
        Dim _strPassWord As String = String.Empty
        Dim _strConfirmPassword As String = String.Empty
        Dim _strLink As String = String.Empty
        Dim _strLoginID As String = String.Empty
        Dim _strEncryptMethod As String = String.Empty
        Dim _intLinkID As Int64 = 0
        Dim _intUID As Int64 = 0
        Dim _intEMRUserId As Int64 = 0
        Dim _strEncryptedPassWord As String = String.Empty
        Dim objEncryptPassword As New clsEncryption
        Dim _nCheckifRecordExist As Int32 = 0
        Dim intRecCnt As Int32
        Try
            If flxData.Rows.Count > 0 Then

                _oDbLayer.Connect(False)
                _nCheckifRecordExist = Convert.ToInt64(_oDbLayer.ExecuteScalar_Query("Select COUNT(*) from User_CustomLinks_MST"))
                _oDbLayer.Disconnect()

                If _nCheckifRecordExist <= 0 Then
                    MessageBox.Show("Please configure the link first.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If Not CheckDuplicateRecords() Then
                    MessageBox.Show("Multiple records found for link '" & _dupLinkName & "' and user '" & _dupUserName & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                'Loop for validation
                For intRecCnt = 0 To flxData.Rows.Count - 1
                    If intRecCnt > flxData.Rows.Count Then
                        Exit For
                    End If
                    If intRecCnt = 0 Then
                        Continue For
                    End If
                    _strUserName = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim()
                    If (_strUserName <> String.Empty) AndAlso (Not IsNothing(_strUserName)) Then 'check if gloEMR User Name is blank or NULL
                        _strPassWord = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_PWD)).Trim()
                        _strConfirmPassword = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD)).Trim
                        'check if Link Name is blank
                        If Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim = String.Empty Then
                            MessageBox.Show("Enter the link for user '" & _strUserName & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            flxData.Select(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)
                            Exit Sub
                        ElseIf _strPassWord.Trim <> String.Empty And Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_LOGINID)).Trim() = String.Empty Then 'check if Login ID is blank
                            MessageBox.Show("Enter the login ID for user '" & _strUserName & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            flxData.Select(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)
                            Exit Sub
                        ElseIf _strPassWord.Trim = String.Empty And Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_LOGINID)).Trim() <> String.Empty Then 'check if Password is blank
                            MessageBox.Show("Enter the password for user '" & _strUserName & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            flxData.Select(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)
                            Exit Sub
                        ElseIf _strPassWord <> String.Empty And _strPassWord <> _strConfirmPassword Then 'check if Password & Confirmed Password is same
                            MessageBox.Show("Password and confirm password must be same for user '" & _strUserName & "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            flxData.Select(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)
                            Exit Sub
                        End If
                    Else
                        Continue For
                    End If
                Next 'end of loop added for validation

                intRecCnt = 0
                'Loop for adding data
                For intRecCnt = 0 To flxData.Rows.Count - 1
                    If intRecCnt > flxData.Rows.Count Then
                        Exit For
                    End If
                    If intRecCnt = 0 Then
                        Continue For
                    End If

                    _strUserName = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME)).Trim()
                    _strPassWord = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_PWD)).Trim()
                    _strLink = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_LINK_NAME)).Trim()
                    _strLoginID = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_LOGINID)).Trim()
                    _strEncryptMethod = Convert.ToString(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD)).Trim()

                    If (_strUserName <> String.Empty) AndAlso (Not IsNothing(_strUserName)) Then
                        _intEMRUserId = RetrieveEMRUserId(_strUserName)
                        _intLinkID = RetrieveLinkId(_strLink)
                        _intUID = Convert.ToInt64(flxData.GetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID))
                        _strEncryptedPassWord = objEncryptPassword.EncryptToBase64String(_strPassWord, mdlGeneral.constEncryptDecryptKey)
                        SaveUserCustomLink(_intUID, _intEMRUserId, _strLoginID, _strEncryptedPassWord, _intLinkID, _strEncryptMethod)
                        'SaveUserCustomLink(_intUID, _intEMRUserId, _strLoginID, _strPassWord, _intLinkID, _strEncryptMethod)
                    Else
                        Continue For
                    End If
                    'Clean up for local variables.
                    _strUserName = String.Empty
                    _strPassWord = String.Empty
                    _strConfirmPassword = String.Empty
                    _strLink = String.Empty
                    _strLoginID = String.Empty
                    _strEncryptMethod = String.Empty
                    _intLinkID = 0
                    _intUID = 0
                    _intEMRUserId = 0
                Next 'end of loop added for adding data
            End If 'End of check if flxdata has rows.
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    ''' <summary>
    ''' Stored procedure to save/update the records into table
    ''' </summary>
    ''' <param name="_intUID"></param>
    ''' <param name="_intEMRID"></param>
    ''' <param name="_sLoginID"></param>
    ''' <param name="_sPassword"></param>
    ''' <param name="_nLiknID"></param>
    ''' <param name="_sEncryptMethode"></param>
    ''' <remarks></remarks>
    Private Sub SaveUserCustomLink(ByVal _intUID As Int64, ByVal _intEMRID As Int64, ByVal _sLoginID As String, ByVal _sPassword As String, ByVal _nLiknID As Int64, ByVal _sEncryptMethode As String)
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _oDbParameters As New gloDatabaseLayer.DBParameters()
        Try
            _oDbLayer.Connect(False)
            _oDbParameters.Add("@UID", _intUID, ParameterDirection.Input, SqlDbType.BigInt)
            _oDbParameters.Add("@UserId", _intEMRID, ParameterDirection.Input, SqlDbType.BigInt)
            _oDbParameters.Add("@sLoginID", _sLoginID, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@nLinkId", _nLiknID, ParameterDirection.Input, SqlDbType.BigInt)
            _oDbParameters.Add("@Password", _sPassword, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@sEncryption", _sEncryptMethode, ParameterDirection.Input, SqlDbType.VarChar)
            _oDbParameters.Add("@ModuleName", "CustomLink", ParameterDirection.Input, SqlDbType.VarChar)
            _oDbLayer.Execute("INUP_UserCustomLink", _oDbParameters)
            _oDbLayer.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
            If Not IsNothing(_oDbParameters) Then
                _oDbParameters.Dispose()
                _oDbParameters = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Function to retrieve gloEMR UserID from user Name
    ''' </summary>
    ''' <param name="strUserName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetrieveEMRUserId(ByVal strUserName As String) As Int64
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _intEMRUserId As Int64 = 0
        Try
            _oDbLayer.Connect(False)
            _intEMRUserId = Convert.ToInt64(_oDbLayer.ExecuteScalar_Query("Select ISNULL(nUserId,0) from User_Mst where sLoginName='" & strUserName & "'"))
            _oDbLayer.Disconnect()
        Catch ex As Exception
            _intEMRUserId = 0
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
        End Try
        Return _intEMRUserId
    End Function

    ''' <summary>
    ''' Function to Retrieve LinkID for Link Name
    ''' </summary>
    ''' <param name="strLinkName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetrieveLinkId(ByVal strLinkName As String) As Int64
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim _intLinkId As Int64 = 0
        Try
            _oDbLayer.Connect(False)
            _intLinkId = Convert.ToInt64(_oDbLayer.ExecuteScalar_Query("Select ISNULL(nLinkID,0) from User_CustomLinks_MST where sLinkName='" & strLinkName.Replace("'", "''") & "'"))
            _oDbLayer.Disconnect()
        Catch ex As Exception
            _intLinkId = 0
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
        End Try
        Return _intLinkId
    End Function
  
    ''' <summary>
    ''' Code to delete the record from Table
    ''' </summary>
    ''' <param name="_intUserExternalID"></param>
    ''' <remarks></remarks>
    Private Sub DeleteUserCustomLink(ByVal _intUserExternalID As Int64) ', ByVal sUserName As String)
        'Dim nUserId As Int64 = 0
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Try
            'nUserId = RetrieveEMRUserId(strUserName)
            _oDbLayer.Connect(False)
            _oDbLayer.Execute_Query("delete from User_ExternalCodes where nUserExternalId=" & _intUserExternalID & " AND sModuleName='CustomLink'")
            _oDbLayer.Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' SP to fill Link Name
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Fill_LinkName() As Collection
        Dim _LinkNameCollection As New Collection
        Dim _objCon As New SqlConnection
        _objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim _objCmd As New SqlCommand
        Dim _objSQLDataReader As SqlDataReader

        Try
            _objCmd.CommandType = CommandType.StoredProcedure
            _objCmd.CommandText = "Fill_LinkName"
            _objCmd.Connection = _objCon

            If _objCon.State = ConnectionState.Closed Then _objCon.Open()

            _objSQLDataReader = _objCmd.ExecuteReader()
            If _objSQLDataReader.HasRows = True Then
                While _objSQLDataReader.Read
                    _LinkNameCollection.Add(_objSQLDataReader.Item(0))
                End While
            End If
            _objSQLDataReader.Close()
            _objCon.Close()

            Return _LinkNameCollection
        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If Not IsNothing(_objCon) Then
                _objCon.Dispose()
                _objCon = Nothing
            End If
            If Not IsNothing(_objCmd) Then
                _objCmd.Dispose()
                _objCmd = Nothing
            End If
            If Not IsNothing(_objSQLDataReader) Then
                _objSQLDataReader.Dispose()
                _objSQLDataReader = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Code to show  Custom Link User in flxgrid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub retriveUserCustomLink()
        Dim _oDbLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim dtCoeastNeuro As New DataTable
        Dim _strQuery As String = String.Empty
        Dim _objdecryptpassword As New clsEncryption
        Dim _strDecryptedPassword As String = String.Empty
        Dim intRecCnt As Int32
        Try

            _strQuery = "SELECT ISNULL(User_ExternalCodes.nUserExternalId,0) AS [nUser External Id], ISNULL(User_MST.sLoginName,'') AS [gloEMR User Name], ISNULL(User_CustomLinks_MST.sLinkName,'') AS [Link Name], ISNULL(User_ExternalCodes.sLoginID,'') AS [Login ID]," & _
                        "ISNULL(User_ExternalCodes.sPassword,'') AS Password, ISNULL(User_ExternalCodes.sPassword,'') AS [Confirm Password], ISNULL(User_ExternalCodes.sEncryptionMethod,'') AS [Encryption] FROM User_ExternalCodes INNER JOIN User_MST ON User_ExternalCodes.nUserId = User_MST.nUserID " & _
                        "INNER JOIN User_CustomLinks_MST ON User_ExternalCodes.nLinkID = User_CustomLinks_MST.nLinkId GROUP BY User_MST.sLoginName, User_CustomLinks_MST.sLinkName, User_ExternalCodes.nUserExternalId, User_ExternalCodes.sLoginID,User_ExternalCodes.sPassword, User_ExternalCodes.sEncryptionMethod"

            'strQuery = "select nUserExternalId [User External Id], ISNULL((select sloginName from User_MST where nUserID=User_ExternalCodes.nUserId),'') [gloEMR User Name], ISNULL((select sLinkName from User_CustomLinks_MST where nLinkID=User_ExternalCodes.nLinkID),'') [Link], " & _
            '           "ISNULL(sLoginID,'') [User Name],ISNULL(sPassword,'') [Password],ISNULL(sPassword,'') [Confirm Password], ISNULL(sEncryption,'') [Encryption] from User_ExternalCodes WHERE sModulename='CustomLink' Group by [gloEMR User Name]"

            _oDbLayer.Connect(False)
            _oDbLayer.Retrive_Query(_strQuery, dtCoeastNeuro)
            _oDbLayer.Disconnect()

            If (Not IsNothing(dtCoeastNeuro)) AndAlso (dtCoeastNeuro.Rows.Count > 0) Then
                If flxData.Rows.Count < dtCoeastNeuro.Rows.Count + 1 Then
                    While flxData.Rows.Count < dtCoeastNeuro.Rows.Count + 1
                        flxData.Rows.Add()
                    End While
                End If

                For intRecCnt = 1 To dtCoeastNeuro.Rows.Count
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID, dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_USER_EXTERNAL_ID))
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME, dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_EMR_USER_NAME))
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_LINK_NAME, dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_LINK_NAME))
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_LOGINID, dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_EXT_LOGINID))

                    _strDecryptedPassword = _objdecryptpassword.DecryptFromBase64String(dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_EXT_PWD), mdlGeneral.constEncryptDecryptKey)

                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_EXT_PWD, _strDecryptedPassword)
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD, _strDecryptedPassword)
                    'flxData.SetData(i, C1CUSTLNKUSRASSGN_COL_EXT_PWD, dtCoeastNeuro.Rows(i - 1)(C1CUSTLNKUSRASSGN_COL_EXT_PWD))
                    'flxData.SetData(i, C1CUSTLNKUSRASSGN_COL_CONF_EXT_PWD, dtCoeastNeuro.Rows(i - 1)(C1CUSTLNKUSRASSGN_COL_EXT_PWD))
                    flxData.SetData(intRecCnt, C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD, dtCoeastNeuro.Rows(intRecCnt - 1)(C1CUSTLNKUSRASSGN_COL_ENCRYPTION_METHOD))
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally
            _strQuery = String.Empty
            _strDecryptedPassword = String.Empty
            _objdecryptpassword = Nothing
            If Not IsNothing(_oDbLayer) Then
                _oDbLayer.Dispose()
                _oDbLayer = Nothing
            End If
            If Not IsNothing(dtCoeastNeuro) Then
                dtCoeastNeuro.Dispose()
                dtCoeastNeuro = Nothing
            End If
        End Try
    End Sub
#End Region

End Class