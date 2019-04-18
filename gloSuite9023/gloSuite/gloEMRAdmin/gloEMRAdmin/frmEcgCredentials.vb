Imports System.Windows.Forms
Imports C1.Win.C1FlexGrid
Imports System.Linq
Public Class frmEcgCredentials

#Region "Global variables"
    Private Const COL_ExternalID As Integer = 0
    Private Const COL_UserId As Integer = 1
    Private Const COL_gloEMRUser As Integer = 2
    Private Const COL_DeviceUser As Integer = 3
    Private Const COL_Password As Integer = 4
    Private Const COL_Confirmpass As Integer = 5
    Private DeletedUser(0) As Long
    Dim WithEvents cmb As New ComboBox
#End Region

#Region "Form Events"



    Private Sub frmEcgCredentials_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' New code Added by manoj jadhav on 20111007
        Try
            If Retrive_ActivatedDevice("USEWELCHALLYNECGDEVICE").Trim().ToUpper() = "True".ToUpper() Then
                cmbDeviceType.Items.Add("WelchAllyn ECG")
            End If

            If Retrive_ActivatedDevice("ECGENABLED").Trim().ToUpper() = "True".ToUpper() Then
                ' cmbDeviceType.Items.Add("Cardio EKG")
                cmbDeviceType.Items.Add("HeartCentrix ECG")
            End If

            If cmbDeviceType.Items.Count <= 0 Then
                Me.Close()
            Else
                cmbDeviceType.Sorted() = True
                cmbDeviceType.SelectedIndex = 0
            End If

            'set  combo box properties
            cmb.DropDownStyle = ComboBoxStyle.DropDownList
            cmb.AutoCompleteSource = AutoCompleteSource.ListItems
            cmb.AutoCompleteMode = AutoCompleteMode.Append
            Dim dtuserlist As DataTable = Get_gloUser()
            cmb.DataSource = dtuserlist
            cmb.DisplayMember = "sLoginName"
            cmb.ValueMember = "nUserID"


            gloC1FlexStyle.Style(flxData)

            Call FormatGrid()

            LoadData()

        Catch ex As Exception
            ex = Nothing
        End Try
        ' end of New Event code Added by manoj jadhav on 20111007
    End Sub


    'New Event code Added by manoj jadhav on 20111007
    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb.SelectedValueChanged
        If cmb.Focused And flxData.RowSel > 0 Then
            Try
                flxData.SetData(flxData.RowSel, COL_UserId, Convert.ToString(cmb.SelectedValue))
                flxData.SetData(flxData.RowSel, COL_DeviceUser, Convert.ToString(cmb.Text))
            Catch ex As Exception
                ex = Nothing
                flxData.SetData(flxData.RowSel, COL_UserId, "0")
            End Try
        End If
    End Sub
    'end of New Event code Added by manoj jadhav on 20111007

    Private Sub flxData_OwnerDrawCell(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles flxData.OwnerDrawCell
        If e.Row >= flxData.Rows.Fixed And flxData.Cols(e.Col).Index = COL_Password Then
            e.Text = New String("*"c, e.Text.Length)
        End If
        If e.Row >= flxData.Rows.Fixed And flxData.Cols(e.Col).Index = COL_Confirmpass Then
            e.Text = New String("*"c, e.Text.Length)
        End If
    End Sub


    Private Sub tstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked

        Try
            flxData.FinishEditing()

            'start of added code Added by manoj jadhav on 20111007
            Select Case Convert.ToString(e.ClickedItem.Tag).ToLower()

                Case "New".ToLower()

                    AddNewRow()

                Case "saveclose".ToLower()

                    Remove_BlankRows()

                    If Isvalidate() Then
                        Savechanges(cmbDeviceType.Text)
                        cmbDeviceType.Items.Clear()
                        Me.Close()
                    End If

                Case "close".ToLower()

                    Me.Close()

                Case "delete".ToLower()

                    If flxData.RowSel > 0 Then
                        Dim nExternalId As Long = 0
                        If Long.TryParse(Convert.ToString(flxData.GetData(flxData.RowSel, COL_ExternalID)), nExternalId) Then
                            If MessageBox.Show("Are you sure you want to delete the selected Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                If nExternalId > 0 Then
                                    ReDim Preserve DeletedUser(DeletedUser.Length)
                                    DeletedUser(DeletedUser.Length - 1) = nExternalId
                                End If
                                flxData.Rows.Remove(flxData.RowSel)
                            End If
                        End If
                    End If
            End Select
            'end of added code Added by manoj jadhav on 20111007
        Catch ex As Exception
            ex = Nothing
        End Try

    End Sub


    'added new event and code by manoj jadhav on 20111007
    Private Sub frmEcgCredentials_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim DeviceType As String = cmbDeviceType.Text
        If DeviceType.Trim.Length > 0 Then
            Try
                If CheckForChanges(DeviceType) Then

                    Select Case MessageBox.Show("Do you want to save  " & DeviceType & "  user information", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                        Case System.Windows.Forms.DialogResult.Yes
                            If Not Isvalidate() Then
                                e.Cancel = True
                            Else
                                Savechanges(DeviceType)
                            End If
                        Case System.Windows.Forms.DialogResult.No

                            e.Cancel = False

                        Case System.Windows.Forms.DialogResult.Cancel

                            e.Cancel = True

                    End Select

                End If

            Catch ex As Exception
                ex = Nothing
            Finally
                DeviceType = String.Empty
            End Try
        End If
    End Sub
    'end added new event and code by manoj jadhav on 20111007

    'start new event and code by manoj jadhav on 20111007

    Private Sub cmbDeviceType_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDeviceType.TextChanged

        If cmbDeviceType.Focused And cmbDeviceType.Text.Trim().Length > 0 Then

            Dim DeviceType As String = cmbDeviceType.Text

            Try
                Select Case DeviceType.Trim().ToLower()

                    Case "HeartCentrix ECG".ToLower()

                        DeviceType = "WelchAllyn ECG"

                    Case "WelchAllyn ECG".ToLower()

                        DeviceType = "HeartCentrix ECG"

                End Select

                If CheckForChanges(DeviceType) Then

                    Select Case MessageBox.Show("Do you want to save  " & DeviceType & "  user information", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                        Case System.Windows.Forms.DialogResult.Yes

                            'validate before saving
                            If Isvalidate() Then
                                Savechanges(DeviceType)
                                LoadData()

                            Else
                                flxData.Focus()
                                cmbDeviceType.Text = DeviceType
                            End If


                        Case System.Windows.Forms.DialogResult.No

                            LoadData()

                        Case System.Windows.Forms.DialogResult.Cancel

                            flxData.Focus()
                            cmbDeviceType.Text = DeviceType

                    End Select

                Else
                    LoadData()

                End If

            Catch ex As Exception
                ex = Nothing
            Finally
                DeviceType = String.Empty
            End Try
        End If
    End Sub
    'end of new event and code by manoj jadhav on 20111007

#End Region

   


#Region "User Defined Functions"


    ''start added new function and code by manoj jadhav on 20111007 to check device is activated or not
    Private Function Retrive_ActivatedDevice(ByVal SettingName As String) As String
        Dim Result As Object = Nothing
        Dim objclsSettings As New clsSettings()
        Try
            objclsSettings.GetSetting(SettingName, 0, 1, Result)
        Catch ex As Exception
            ex = Nothing
        Finally
            objclsSettings = Nothing
        End Try
        Retrive_ActivatedDevice = Convert.ToString(Result)
    End Function
    'end added new function and code by manoj jadhav on 20111007 to check device is activated or not

    ''start added function and code by manoj jadhav on 20111007 to retrieve gloEMR user 
    Private Function Get_gloUser() As DataTable
        Dim ObjDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim dtUser As DataTable = Nothing
        Dim sqlQury As String = String.Empty
        Try
            sqlQury = "Select nUserID,sLoginName from User_MST order by sLoginName"
            ObjDBLayer = New gloDatabaseLayer.DBLayer(mdlGeneral.GetConnectionString())
            ObjDBLayer.Connect(False)
            ObjDBLayer.Retrive_Query(sqlQury, dtUser)
            ObjDBLayer.Disconnect()
        Catch ex As Exception
            ex = Nothing
            dtUser = Nothing
        Finally
            sqlQury = String.Empty
            If Not ObjDBLayer Is Nothing Then
                ObjDBLayer.Dispose()
                ObjDBLayer = Nothing
            End If
        End Try
        Get_gloUser = dtUser
    End Function
    'end added function and code by manoj jadhav on 20111007 to retrieve gloEMR user 

    ''start modified function and code by manoj jadhav on 20111007 to add External ID Colum  and to apply style to grid
    ''Function to format the grid
    Private Sub FormatGrid()
        Dim gloUser As String = String.Empty
        Dim tb As New TextBox
        Try
            With flxData
                .DataSource = Nothing
                .Clear()
                .Cols.Count = 6
                .Rows.Count = 1
                .Rows.Fixed = 1

                .SetData(0, COL_ExternalID, "External ID")
                .SetData(0, COL_UserId, "User ID")
                .SetData(0, COL_gloEMRUser, "gloEMR User")
                .SetData(0, COL_DeviceUser, "Device User")
                .SetData(0, COL_Password, "Password")
                .SetData(0, COL_Confirmpass, "Confirm Password")

                .Cols(COL_ExternalID).Width = 0
                .Cols(COL_UserId).Width = 0
                .Cols(COL_gloEMRUser).Width = 0.2 * .Width
                .Cols(COL_DeviceUser).Width = 0.2 * .Width
                .Cols(COL_Password).Width = 0.3 * .Width
                .Cols(COL_Confirmpass).Width = 0.2 * Width

                .Cols(COL_ExternalID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_UserId).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_gloEMRUser).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_DeviceUser).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_Password).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_Confirmpass).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(COL_ExternalID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_UserId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_gloEMRUser).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_DeviceUser).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_Password).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_Confirmpass).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                .Cols(COL_ExternalID).AllowEditing = False
                .Cols(COL_UserId).AllowEditing = False
                .Cols(COL_gloEMRUser).AllowEditing = True
                .Cols(COL_DeviceUser).AllowEditing = True
                .Cols(COL_Password).AllowEditing = True
                .Cols(COL_Confirmpass).AllowEditing = True

                .AllowDragging = AllowDraggingEnum.None
                .DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                .AllowSorting = AllowSortingEnum.None
                .SelectionMode = SelectionModeEnum.Row


                .Cols(COL_ExternalID).Visible = False
                .Cols(COL_UserId).Visible = False
                .Cols(COL_gloEMRUser).Visible = True
                .Cols(COL_DeviceUser).Visible = True
                .Cols(COL_Password).Visible = True
                .Cols(COL_Confirmpass).Visible = True


                .ExtendLastCol = True

            End With
            flxData.Cols(COL_gloEMRUser).Editor = cmb
            tb.MaxLength = 25
            tb.PasswordChar = "*"
            flxData.Cols(COL_Password).Editor = tb
            flxData.Cols(COL_Confirmpass).Editor = tb

        Catch ex As Exception
            ex = Nothing
        Finally
            gloUser = String.Empty
        End Try
    End Sub

    ''added new method and code by manoj jadhav on 20111007
    Private Sub LoadData()
        Dim dttemp As DataTable = Nothing
        Try
            dttemp = RetrieveECGUserData(cmbDeviceType.Text)
            LoadUserData(dttemp)
        Catch ex As Exception
            ex = Nothing
        Finally
            If Not dttemp Is Nothing Then
                dttemp.Dispose()
                dttemp = Nothing
            End If
        End Try
    End Sub
    'end of new method and code by manoj jadhav on 20111007

    ''added new method and code by manoj jadhav on 20111007 for fill grid from data table
    Private Sub LoadUserData(ByVal dtTempData As DataTable)
        Try
            If flxData.Rows.Count > 1 Then
                flxData.Rows.RemoveRange(1, flxData.Rows.Count - 1)
            End If

            If Not dtTempData Is Nothing Then
                For i As Integer = 0 To dtTempData.Rows.Count - 1
                    flxData.Rows.Add()
                    flxData.SetData(i + 1, COL_ExternalID, dtTempData.Rows(i).Item("nUserExternalId").ToString())
                    flxData.SetData(i + 1, COL_UserId, dtTempData.Rows(i).Item("nUserId").ToString())
                    flxData.SetData(i + 1, COL_gloEMRUser, dtTempData.Rows(i).Item("user_name").ToString())
                    flxData.SetData(i + 1, COL_DeviceUser, dtTempData.Rows(i).Item("sDeviceUserName").ToString())
                    flxData.SetData(i + 1, COL_Password, EncryptOrDecrypt(dtTempData.Rows(i).Item("sPassword").ToString(), "decrypt"))
                    flxData.SetData(i + 1, COL_Confirmpass, EncryptOrDecrypt(dtTempData.Rows(i).Item("sPassword").ToString(), "decrypt"))
                Next

            End If

        Catch ex As Exception
            ex = Nothing
        Finally
            If Not dtTempData Is Nothing Then
                dtTempData.Dispose()
                dtTempData = Nothing
            End If
        End Try
    End Sub
    'end of added new method and code by manoj jadhav on 20111007 for fill grid from data table

    ''added new method and code by manoj jadhav on 20111007 for add new row to grid
    Private Sub AddNewRow()
        Dim _RecordCnt As Int32 = flxData.Rows.Count - 1
        Try
            If (_RecordCnt <= 0) Then
                _RecordCnt = flxData.Rows.Count
                flxData.Rows.Add()
                flxData.SetData(_RecordCnt, COL_ExternalID, 0)
                flxData.SetData(_RecordCnt, COL_UserId, 0)
                flxData.Select(_RecordCnt, COL_ExternalID)
            Else
                If Convert.ToString(flxData.GetData(_RecordCnt, COL_gloEMRUser)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(_RecordCnt, COL_DeviceUser)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(_RecordCnt, COL_Password)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(_RecordCnt, COL_Confirmpass)).Trim().Length <= 0 Then
                    '  MessageBox.Show("Blank Row Already Present")
                    flxData.Select(_RecordCnt, COL_ExternalID)
                Else
                    _RecordCnt = flxData.Rows.Count
                    flxData.Rows.Add()
                    flxData.SetData(_RecordCnt, COL_ExternalID, 0)
                    flxData.SetData(_RecordCnt, COL_UserId, 0)
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            _RecordCnt = 0
        End Try
    End Sub
    'end of added new method and code by manoj jadhav on 20111007 for add new row to grid

    ''added new method and code by manoj jadhav on 20111007 for remove blank rows from grid
    Private Sub Remove_BlankRows()
        For i As Integer = 1 To flxData.Rows.Count
            Try
                If Convert.ToString(flxData.GetData(i, COL_gloEMRUser)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(i, COL_DeviceUser)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(i, COL_Password)).Trim().Length <= 0 And Convert.ToString(flxData.GetData(i, COL_Confirmpass)).Trim().Length <= 0 Then
                    flxData.Rows.Remove(i)
                End If
            Catch ex As Exception
                ex = Nothing
            End Try
        Next
    End Sub
    'end of added new method and code by manoj jadhav on 20111007 for remove blank rows from grid

    Private Function RetrieveUserId(ByVal strUserName As String) As Int64
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim intUserId As Int64 = 0
        Try
            oDbLayer.Connect(False)
            intUserId = Convert.ToInt64(oDbLayer.ExecuteScalar_Query("Select nUserId from User_Mst where sLoginName='" & strUserName & "'"))
            oDbLayer.Disconnect()
        Catch ex As Exception
            intUserId = 0
        Finally
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Dispose()
            End If
        End Try
        Return intUserId
    End Function

    'added new function and code by manoj jadhav on 20111007 for retrieve  ECGuser data
    Private Function RetrieveECGUserData(ByVal DeviceType As String) As DataTable
        If DeviceType.Trim.ToLower = "HeartCentrix ECG".ToLower Then
            DeviceType = "ECG"
        End If
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim dtECGData As New DataTable
        Dim strQuery As String = String.Empty
        Try
            strQuery = "select nUserExternalId, nUserId, ISNULL((select TOP 1 sloginName from User_MST where nUserID=User_ExternalCodes.nUserId),'') as user_name,sDeviceUserName,sPassword ,sModulename,ISNULL(IsEncrypted,'') as IsEncrypted  from User_ExternalCodes WHERE sModulename ='" + DeviceType + "'"
            oDbLayer.Connect(False)
            oDbLayer.Retrive_Query(strQuery, dtECGData)
            oDbLayer.Disconnect()
            Return dtECGData
        Catch ex As Exception
            ex = Nothing
            dtECGData = Nothing
        Finally
            strQuery = String.Empty
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Dispose()
            End If
        End Try
        RetrieveECGUserData = dtECGData
    End Function
    'end of added new function and code by manoj jadhav on 20111007 for retrieve  ECGuser data

    Private Function EncryptOrDecrypt(ByVal sValue As String, ByVal sType As String) As String
        Dim oEncryption As New clsEncryption()
        Dim sResult As String = String.Empty
        Try
            Select Case sType.ToLower()
                Case "encrypt"
                    sResult = oEncryption.EncryptToBase64String(sValue, mdlGeneral.constEncryptDecryptKey)
                    Exit Select
                Case "decrypt"
                    sResult = oEncryption.DecryptFromBase64String(sValue, mdlGeneral.constEncryptDecryptKey)
                    Exit Select
                Case Else
                    sResult = String.Empty
                    Exit Select
            End Select

        Catch ex As Exception
            sResult = String.Empty
        Finally
            oEncryption = Nothing
        End Try
        Return sResult

    End Function

    'added new function and code by manoj jadhav on 20111007 for validate grid information
    Private Function Isvalidate() As Boolean
        Isvalidate = True
        Dim gloEMRUserName As String = String.Empty
        Dim DeviceUserName As String = String.Empty
        Dim Password As String = String.Empty
        Dim confiremPassword As String = String.Empty
        Try
            For i As Integer = 1 To flxData.Rows.Count - 1
                gloEMRUserName = String.Empty
                DeviceUserName = String.Empty
                Password = String.Empty
                confiremPassword = String.Empty
                gloEMRUserName = Convert.ToString(flxData.GetData(i, COL_gloEMRUser))
                DeviceUserName = Convert.ToString(flxData.GetData(i, COL_DeviceUser))
                Password = Convert.ToString(flxData.GetData(i, COL_Password))
                confiremPassword = Convert.ToString(flxData.GetData(i, COL_Confirmpass))
                If gloEMRUserName.Trim.Length <= 0 Then
                    MessageBox.Show("Select gloEMR user name", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    flxData.RowSel = i
                    flxData.Select(i, COL_ExternalID)
                    Isvalidate = False
                    Exit Try
                ElseIf DeviceUserName.Trim().Length <= 0 Then
                    MessageBox.Show("Enter device user name", mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    flxData.RowSel = i
                    flxData.Select(i, COL_ExternalID)
                    Isvalidate = False
                    Exit For
                ElseIf Password.Trim().Length <= 0 Then
                    MessageBox.Show("Enter password for user " & gloEMRUserName, mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    flxData.RowSel = i
                    flxData.Select(i, COL_ExternalID)
                    Isvalidate = False
                    Exit Try
                ElseIf confiremPassword.Trim().Length <= 0 Then
                    MessageBox.Show("Enter confirm password for user " & gloEMRUserName, mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    flxData.RowSel = i
                    flxData.Select(i, COL_ExternalID)
                    Isvalidate = False
                    Exit Try
                ElseIf Not String.Compare(Password, confiremPassword, False) = 0 Then
                    MessageBox.Show("Password and confirm password should be matched for user " & gloEMRUserName, mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    flxData.RowSel = i
                    flxData.Select(i, COL_ExternalID)
                    Isvalidate = False
                    Exit Try
                Else
                    'check into another rows
                    For j As Integer = 1 To flxData.Rows.Count - 1
                        If Not j = i Then
                            If String.Compare(gloEMRUserName, Convert.ToString(flxData.GetData(j, COL_gloEMRUser))) = 0 Then
                                Isvalidate = False
                                flxData.RowSel = i
                                flxData.Select(i, COL_ExternalID)
                                MessageBox.Show("Duplicate records found for user " & gloEMRUserName, mdlGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Try
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Isvalidate = False
            ex = Nothing
        Finally
            gloEMRUserName = String.Empty
            DeviceUserName = String.Empty
            Password = String.Empty
            confiremPassword = String.Empty
        End Try
    End Function
    'added new function and code by manoj jadhav on 20111007 for validate grid information

    'added new function and code by manoj jadhav on 20111007 for check  changes into database
    Private Function CheckForChanges(ByVal devicetype As String) As Boolean
        Dim dttemp As DataTable = RetrieveECGUserData(devicetype)
        If Not dttemp Is Nothing Then
            Dim Ischaged As Boolean = False
            Dim gloEMRUserName As String = String.Empty
            Dim DeviceUserName As String = String.Empty
            Dim Password As String = String.Empty
            Dim ConfirmPassword As String = String.Empty
            Try
                Remove_BlankRows()
                If Not dttemp.Rows.Count = flxData.Rows.Count - 1 Then
                    Ischaged = True
                Else
                    For i As Integer = 1 To flxData.Rows.Count - 1
                        gloEMRUserName = String.Empty
                        DeviceUserName = String.Empty
                        Password = String.Empty

                        gloEMRUserName = Convert.ToString(flxData.GetData(i, COL_gloEMRUser))
                        DeviceUserName = Convert.ToString(flxData.GetData(i, COL_DeviceUser))
                        Password = Convert.ToString(flxData.GetData(i, COL_Password))
                        ConfirmPassword = Convert.ToString(flxData.GetData(i, COL_Confirmpass))

                        If Not String.Compare(gloEMRUserName, Convert.ToString(dttemp.Rows(i - 1).Item("user_name"))) = 0 Then
                            Ischaged = True
                            Exit Try
                        ElseIf Not String.Compare(DeviceUserName, Convert.ToString(dttemp.Rows(i - 1).Item("sDeviceUserName"))) = 0 Then
                            Ischaged = True
                            Exit Try
                        ElseIf Not String.Compare(Password, EncryptOrDecrypt(Convert.ToString(dttemp.Rows(i - 1).Item("sPassword")), "decrypt")) = 0 Then
                            Ischaged = True
                            Exit Try
                        ElseIf Not String.Compare(ConfirmPassword, EncryptOrDecrypt(Convert.ToString(dttemp.Rows(i - 1).Item("sPassword")), "decrypt")) = 0 Then
                            Ischaged = True
                            Exit Try
                        Else
                            Ischaged = False
                        End If
                    Next
                End If
            Catch ex As Exception
                ex = Nothing
            Finally
                gloEMRUserName = String.Empty
                DeviceUserName = String.Empty
                Password = String.Empty
            End Try
            CheckForChanges = Ischaged
        End If
    End Function
    'added new function and code by manoj jadhav on 20111007 for check  changes into database

    'start modified function  code by manoj jadhav on 20111007 for save  changes into database
    Private Function Savechanges(ByVal devicetype As String) As Boolean
        If devicetype.Trim.ToLower = "HeartCentrix ECG".ToLower Then
            devicetype = "ECG"
        End If
        DeletedUsers()
        Dim nExternalId As Long = 0
        Dim gloUserName As String = String.Empty
        Dim ngloUserId As Long = 0
        Dim sDeviceUserName As String = String.Empty
        Dim Password As String = String.Empty
        Try
            For i As Integer = 1 To flxData.Rows.Count - 1
                nExternalId = 0
                gloUserName = String.Empty
                ngloUserId = 0
                sDeviceUserName = String.Empty
                Password = String.Empty
                Long.TryParse(Convert.ToString(flxData.GetData(i, COL_ExternalID)), nExternalId)
                gloUserName = Convert.ToString(flxData.GetData(i, COL_gloEMRUser))
                Long.TryParse(Convert.ToString(flxData.GetData(i, COL_UserId)), ngloUserId)
                If ngloUserId <= 0 Then
                    ngloUserId = RetrieveUserId(gloUserName)
                End If
                sDeviceUserName = Convert.ToString(flxData.GetData(i, COL_DeviceUser))
                Password = Convert.ToString(flxData.GetData(i, COL_Password))
                If Not ngloUserId = 0 Then
                    SaveUserInformation(nExternalId, ngloUserId, sDeviceUserName, Password, devicetype)
                End If
            Next
        Catch ex As Exception
            ex = Nothing
        Finally
            nExternalId = 0
            gloUserName = String.Empty
            ngloUserId = 0
            sDeviceUserName = String.Empty
            Password = String.Empty
        End Try

    End Function
    'end of modified function  code by manoj jadhav on 20111007 for save  changes into database

    'added new function and code by manoj jadhav on 20111007 for save user information as per device type
    Private Sub SaveUserInformation(ByVal nExternalID As Long, ByVal gloUserId As Long, ByVal DeviceUserName As String, ByVal Password As String, ByVal ModuleName As String)
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim oDbParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDbLayer.Connect(False)
            oDbParameters.Add("@UID", nExternalID, ParameterDirection.Input, SqlDbType.BigInt)
            oDbParameters.Add("@UserId", gloUserId, ParameterDirection.Input, SqlDbType.BigInt)
            oDbParameters.Add("@sDeviceUserName", DeviceUserName, ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameters.Add("@Password", EncryptOrDecrypt(Password, "encrypt"), ParameterDirection.Input, SqlDbType.VarChar)
            oDbParameters.Add("@ModuleName", ModuleName, ParameterDirection.Input, SqlDbType.VarChar)
            oDbLayer.Execute("IPUP_UserExternalcodes", oDbParameters)
            oDbLayer.Disconnect()
        Catch ex As Exception
            ex = Nothing
        Finally
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Dispose()
            End If
            If Not IsNothing(oDbParameters) Then
                oDbParameters.Dispose()
            End If
        End Try
    End Sub
    'end of added new function and code by manoj jadhav on 20111007 for save user information as per device type

    'start added function and code by manoj jadhav on 20111007 to deleted records from tables
    Private Sub DeletedUsers()
        If Not DeletedUser Is Nothing Then
            For Each ExternalID As Long In DeletedUser
                If Not ExternalID = 0 Then
                    DeleteUser(ExternalID)
                End If
            Next
        End If

    End Sub
    'end of added function and code by manoj jadhav on 20111007 to deleted records from tables

    'start added function and code by manoj jadhav on 20111007 to deleted records from tables
    Private Function DeleteUser(ByVal nExternalID As Long) As Boolean
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim sqlQury As String = String.Empty
        Try
            sqlQury = "delete from User_ExternalCodes where nUserExternalId=" & nExternalID
            oDbLayer.Connect(False)
            oDbLayer.Execute_Query(sqlQury)
            DeleteUser = True
            oDbLayer.Disconnect()
        Catch ex As Exception
            ex = Nothing
            DeleteUser = False
        Finally
            sqlQury = String.Empty
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Dispose()
                oDbLayer = Nothing
            End If
        End Try
    End Function
    'end of  added function and code by manoj jadhav on 20111007 to deleted records from tables

#End Region
    ' ''function to retrieve all users names
    'Dim _dupUser As String = String.Empty
    'Private Function CheckDuplicateRecords() As Boolean
    '    _dupUser = String.Empty
    '    ' User = ""
    '    Try
    '        Dim objusers As New clsAudit
    '        Users = objusers.Fill_Users
    '        objusers = Nothing
    '        Dim arrUsers As New ArrayList()
    '        If Users.Count > 0 Then
    '            Dim nCount As Int16
    '            For nCount = 1 To flxData.Rows.Count - 1
    '                ' User = User & Users(nCount) & "|"
    '                'User += "|" & Users(nCount)
    '                If Convert.ToString(flxData.GetData(nCount, COL_gloEMRUser)).Trim() <> String.Empty Then
    '                    If Not arrUsers.Contains(Convert.ToString(flxData.GetData(nCount, COL_gloEMRUser)).Trim()) Then
    '                        arrUsers.Add(Convert.ToString(flxData.GetData(nCount, COL_gloEMRUser)).Trim())
    '                    Else
    '                        _dupUser = Convert.ToString(flxData.GetData(nCount, COL_gloEMRUser)).Trim()
    '                        flxData.Select(nCount, COL_gloEMRUser)
    '                        Return False
    '                    End If
    '                End If
    '            Next
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    '#End Region

    '#Region "Form Control events"

    'Private Sub flxData_EnterCell(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxData.EnterCell
    '    'Dim User As String
    '    'User = ""
    '    'Dim objusers As New clsAudit
    '    'Users = objusers.Fill_Users
    '    'objusers = Nothing

    '    'If Users.Count > 0 Then
    '    '    Dim nCount As Int16
    '    '    User = " "
    '    '    For nCount = 1 To Users.Count
    '    '        ' User = User & Users(nCount) & "|"
    '    '        User += "|" & Users(nCount)
    '    '        If flxData.Rows.Count <= Users.Count Then
    '    '            flxData.Rows.Add()
    '    '        End If

    '    '    Next
    '    '    flxData.Cols(COL_EMRUSER).ComboList = User

    '    'End If
    '    ' ''Assigning Password character * Password and confirm password 
    '    'If flxData.ColSel = COL_PASSWORD Then
    '    '    Dim tb As New TextBox
    '    '    tb.PasswordChar = "*"
    '    '    flxData.Cols(COL_PASSWORD).Editor = tb
    '    'ElseIf flxData.ColSel = COL_CONFIRMPASS Then
    '    '    Dim tb As New TextBox
    '    '    tb.PasswordChar = "*"
    '    '    flxData.Cols(COL_CONFIRMPASS).Editor = tb
    '    'End If
    '    'Dim tb As New TextBox
    '    'Try
    '    '    tb.PasswordChar = "*"
    '    '    If flxData.ColSel = COL_PASSWORD Then
    '    '        flxData.Cols(COL_PASSWORD).Editor = tb
    '    '    ElseIf flxData.ColSel = COL_CONFIRMPASS Then
    '    '        flxData.Cols(COL_CONFIRMPASS).Editor = tb
    '    '    End If
    '    'Catch ex As Exception
    '    '    ex = Nothing
    '    'End Try

    'End Sub



    '#End Region

  
 

    

    'Private Function RetrieveECGMSTID(ByVal nUserId As Int64) As Int64
    '    Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
    '    Dim intECGId As Int64 = 0
    '    Try
    '        oDbLayer.Connect(False)
    '        intECGId = Convert.ToInt64(oDbLayer.ExecuteScalar_Query("Select ISNULL(nUserExternalId,0) from User_ExternalCodes where nUserId='" & nUserId & "'"))
    '        oDbLayer.Disconnect()
    '    Catch ex As Exception
    '        intECGId = 0
    '    Finally
    '        If Not IsNothing(oDbLayer) Then
    '            oDbLayer.Dispose()
    '        End If
    '    End Try
    '    Return intECGId
    'End Function

    'Private Sub flxData_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
    '    'Dim test As String = Convert.ToString(flxData.GetData(flxData.RowSel, flxData.ColSel))
    '    blnisModified = True
    'End Sub

    'Private Sub AddData(Optional ByRef evntArgs As System.Windows.Forms.FormClosingEventArgs = Nothing)
    '    flxData.FinishEditing()
    '    Dim strUserName As String = String.Empty
    '    Dim strPassWord As String = String.Empty
    '    Dim strConfirmPassword As String = String.Empty
    '    'Dim strUserProviderId As String = String.Empty
    '    Dim intECGUserID As Int64 = 0
    '    Dim intUserId As Int64 = 0
    '    Try
    '        If flxData.Rows.Count > 0 Then
    '            If Not CheckDuplicateRecords() Then
    '                MessageBox.Show("Multiple records found for '" + _dupUser + "'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                blnisModified = True
    '                blnisClosed = False
    '                If Not IsNothing(evntArgs) Then
    '                    ' blnisClosed = False
    '                    ' blnisModified = True
    '                    evntArgs.Cancel = True
    '                End If
    '                Exit Sub
    '            End If
    '            'Loop for validation
    '            For i As Integer = 0 To flxData.Rows.Count - 1
    '                If i > flxData.Rows.Count Then
    '                    Exit For
    '                End If
    '                If i = 0 Then
    '                    Continue For
    '                End If
    '                strUserName = Convert.ToString(flxData.GetData(i, COL_gloEMRUser)).Trim()
    '                If (strUserName <> String.Empty) AndAlso (Not IsNothing(strUserName)) Then
    '                    strPassWord = Convert.ToString(flxData.GetData(i, COL_Password)).Trim()
    '                    strConfirmPassword = Convert.ToString(flxData.GetData(i, COL_Confirmpass)).Trim()
    '                    'strUserProviderId = Convert.ToString(flxData.GetData(i, COL_USERPROVIDERID)).Trim()
    '                    If strPassWord = "" OrElse IsNothing(strPassWord) Then
    '                        MessageBox.Show("Password should not be blank for '" & strUserName & "'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        blnisModified = True
    '                        blnisClosed = False
    '                        If Not IsNothing(evntArgs) Then
    '                            'blnisClosed = False
    '                            'blnisModified = True
    '                            evntArgs.Cancel = True
    '                        End If
    '                        flxData.Select(i, COL_gloEMRUser)
    '                        Exit Sub
    '                    ElseIf strPassWord <> strConfirmPassword Then
    '                        MessageBox.Show("Password and confirm password should match for '" & strUserName & "'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        blnisModified = True
    '                        blnisClosed = False
    '                        If Not IsNothing(evntArgs) Then
    '                            'blnisClosed = False
    '                            'blnisModified = True
    '                            evntArgs.Cancel = True
    '                        End If
    '                        flxData.Select(i, COL_gloEMRUser)
    '                        Exit Sub
    '                    Else
    '                        'intUserId = RetrieveUserId(strUserName)
    '                        'intECGUserID = RetrieveECGMSTID(intUserId)
    '                        'SaveECGSettings(intECGUserID, intUserId, strPassWord)
    '                    End If
    '                Else
    '                    Continue For
    '                End If
    '            Next
    '            'Loop for adding data
    '            For i As Integer = 0 To flxData.Rows.Count - 1
    '                If i > flxData.Rows.Count Then
    '                    Exit For
    '                End If
    '                If i = 0 Then
    '                    Continue For
    '                End If
    '                strUserName = Convert.ToString(flxData.GetData(i, COL_gloEMRUser)).Trim()
    '                strPassWord = Convert.ToString(flxData.GetData(i, COL_Password)).Trim()
    '                intECGUserID = Convert.ToInt64((flxData.GetData(i, COL_USERID)))
    '                strConfirmPassword = Convert.ToString(flxData.GetData(i, COL_Confirmpass)).Trim()
    '                If (strUserName <> String.Empty) AndAlso (Not IsNothing(strUserName)) Then
    '                    intUserId = RetrieveUserId(strUserName)
    '                    'intECGUserID = RetrieveECGMSTID(intUserId)
    '                    SaveUserInformation(intECGUserID, intUserId, String.Empty, strPassWord, String.Empty)
    '                Else
    '                    Continue For
    '                End If
    '            Next
    '        End If
    '        Me.Close()
    '    Catch ex As Exception

    '    End Try
    'End Sub

   

    'Private Sub LoadData(ByVal dtUsers As DataTable)
    '    If Not IsNothing(dtUsers) AndAlso dtUsers.Rows.Count > 0 Then
    '        If flxData.Rows.Count < dtUsers.Rows.Count Then
    '            While flxData.Rows.Count < dtUsers.Rows.Count + 1
    '                flxData.Rows.Add()
    '            End While
    '        End If
    '        For i As Integer = 1 To dtUsers.Rows.Count
    '            flxData.SetData(i, COL_USERID, dtUsers.Rows(i - 1)(COL_USERID))
    '            flxData.SetData(i, COL_gloEMRUser, dtUsers.Rows(i - 1)(COL_gloEMRUser))
    '            'flxData.SetData(i, COL_PASSWORD, dtUsers.Rows(i - 1)(COL_PASSWORD))

    '            If String.IsNullOrEmpty(dtUsers.Rows(i - 1)("IsEncrypted")) Or Convert.ToBoolean(dtUsers.Rows(i - 1)("IsEncrypted")) = False Then
    '                flxData.SetData(i, COL_Password, dtUsers.Rows(i - 1)(COL_Password))
    '                flxData.SetData(i, COL_Confirmpass, dtUsers.Rows(i - 1)(COL_Confirmpass))
    '            Else
    '                flxData.SetData(i, COL_Password, EncryptOrDecrypt(dtUsers.Rows(i - 1)(COL_Password), "decrypt"))
    '                flxData.SetData(i, COL_Confirmpass, EncryptOrDecrypt(dtUsers.Rows(i - 1)(COL_Confirmpass), "decrypt"))
    '            End If

    '            'flxData.SetData(i, COL_CONFIRMPASS, dtUsers.Rows(i - 1)(COL_CONFIRMPASS))

    '            ' flxData.SetData(i, COL_USERPROVIDERID, dtUsers.Rows(i - 1)(COL_USERPROVIDERID))
    '        Next
    '    End If
    'End Sub


  
  



   
   

End Class