Imports System.IO

Public Class frmImportDICOM

    Private _FilesToImport As New ArrayList()
    Dim PatientId As Long
    Dim isValidated As Boolean = False
    Dim isKeyPressed As Boolean = False
    Dim isSaveClicked As Boolean = False
    Dim isRemovedAfterSave As Boolean = False


    Public Sub New(ByVal patId As Long)
        MyBase.New()
        InitializeComponent()
        PatientId = patId
    End Sub

    Public Property FilesToImport() As ArrayList
        Get
            Return _FilesToImport
        End Get
        Set(ByVal value As ArrayList)
            _FilesToImport = value
        End Set
    End Property


    Private Function ValidateList() As Boolean
        Dim oDicom As New clsDICOM()
        Dim _LongNameFiles As String = ""
        Dim _InvalidNameFiles As String = ""
        Dim isValid As Boolean = True

        If DICOMPath = "" Then
            MessageBox.Show("Please set the DICOM path from Tool->Settings->Server Path, to save the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ValidateList = Nothing
            Exit Function
        End If
        If Not Directory.Exists(DICOMPath & "\" & PatientId) Then
            Directory.CreateDirectory(DICOMPath & "\" & PatientId)
        End If

        Dim dInfo As New DirectoryInfo(DICOMPath & "\" & PatientId)
        Dim dfI() As FileInfo = dInfo.GetFiles()
        Try
            If lvDocuments.Items.Count > 0 Then
                For Count As Integer = 0 To lvDocuments.Items.Count - 1
                    If Count < lvDocuments.Items.Count Then
                        Dim eFor As Boolean = False
                        For dCounter As Integer = 0 To dfI.Length - 1
                            Dim dfName As String = dfI(dCounter).ToString()
                            Dim strArr() As String = dfName.Split(".")
                            If lvDocuments.Items.Count > 0 Then
                                If strArr(0) = lvDocuments.Items(Count).SubItems(0).Text Then
                                    Dim dResult As DialogResult = MessageBox.Show("File '" & lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text & "' is already added. Do you want to remove it?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                    If dResult.ToString() = "Yes" Then
                                        lvDocuments.Items(Count).Selected = True
                                        RemoveDICOMFile(1)
                                        If Count >= 0 Then
                                            Count = Count - 1
                                        End If
                                        eFor = True
                                        Exit For
                                    Else
                                        isValid = False
                                    End If
                                End If
                            End If
                        Next
                        If eFor = False And isValid = True Then
                            If Count < lvDocuments.Items.Count Then
                                If lvDocuments.Items(Count).SubItems(0).Text.Length > 50 Then
                                    If _LongNameFiles.Trim() = "" Then
                                        _LongNameFiles = lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text
                                    Else
                                        _LongNameFiles = _LongNameFiles + Environment.NewLine & lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text
                                    End If
                                ElseIf oDicom.CheckValidFileName(lvDocuments.Items(Count).SubItems(0).Text) = False Then
                                    If _InvalidNameFiles.Trim() = "" Then
                                        _InvalidNameFiles = lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text
                                    Else
                                        _InvalidNameFiles = _LongNameFiles + Environment.NewLine & lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text
                                    End If
                                Else

                                End If
                            End If
                        End If
                    End If
                Next
                If _LongNameFiles <> "" Then
                    MessageBox.Show("File names having more that 50 characters cannot be saved. Following file names needs to be changed before saving " + Environment.NewLine + _LongNameFiles, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                ElseIf _InvalidNameFiles <> "" Then
                    Return False
                End If

            Else
                MessageBox.Show("Add atleast one file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try


        Return isValid
    End Function
    Private Sub tls_MaintainDoc_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_MaintainDoc.ItemClicked
        Try
            If txtDocumentName.Focused Then
                Dim eVal As New EventArgs()
                isValidated = False
                txtDocumentName_Validated(sender, eVal)
                If isValidated = False Then
                    Exit Sub
                End If
            End If
            Select Case e.ClickedItem.Tag
                Case "OK"
                    'Open File In DICOM Viewer

                    Dim DuplicateFile As Boolean = False
                    isSaveClicked = True

                    If ValidateList() = True Then
                        'If lvDocuments.Items.Count > 0 Then
                        For Count As Integer = 0 To lvDocuments.Items.Count - 1
                            If Count < lvDocuments.Items.Count Then
                                If lvDocuments.Items(Count).SubItems(3).Text = "False" Then

                                    Dim srcfilepath As String = lvDocuments.Items(Count).SubItems(2).Text
                                    'Dim dicomFileName() As String = lvDocuments.Items(Count).SubItems(2).Text.Split("\")
                                    Dim destfilepath As String

                                    'create directory by name PatientID if it does not already exists
                                    Dim LocalDir As String
                                    If DICOMPath = "" Then
                                        MessageBox.Show("Please set the DICOM path from Tool->Settings->Server Path, to save the file.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                    LocalDir = DICOMPath & "\" & PatientId
                                    If Not Directory.Exists(LocalDir) Then
                                        Directory.CreateDirectory(LocalDir)
                                    End If
                                    'get the path of the file to be copied from the treeviews selected node
                                    Dim oFileInfo As New FileInfo(lvDocuments.Items(Count).SubItems(2).Text)

                                    srcfilepath = lvDocuments.Items(Count).SubItems(2).Text
                                    destfilepath = LocalDir & "\" & lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text
                                    'copy the file to the destination directory
                                    If File.Exists(destfilepath) Then
                                        Dim dResult As DialogResult = MessageBox.Show("File '" & lvDocuments.Items(Count).SubItems(0).Text & lvDocuments.Items(Count).SubItems(1).Text & "' is already added. Do you want to remove it?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                                        If dResult.ToString() = "Yes" Then
                                            lvDocuments.Items(Count).Selected = True
                                            RemoveDICOMFile(1)
                                            If Count >= -1 Then
                                                Count = Count - 1
                                            End If
                                        Else
                                            DuplicateFile = True
                                        End If
                                    Else
                                        File.Copy(srcfilepath, destfilepath)
                                        FilesToImport.Add(destfilepath)
                                        lvDocuments.Items(Count).SubItems(3).Text = "True"
                                    End If
                                    LocalDir = Nothing
                                    srcfilepath = Nothing
                                    destfilepath = Nothing
                                End If
                            End If
                        Next
                        'Else
                        '    MessageBox.Show("Add atleast one file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '    DuplicateFile = True
                        'End If



                        If DuplicateFile = False Then
                            If lvDocuments.Items.Count <= 0 Then
                                If isRemovedAfterSave = False Then
                                    MessageBox.Show("Add atleast one file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                DuplicateFile = True
                            Else
                                Me.Close()
                            End If
                        End If
                    End If

                   

                Case "Cancel"
                    'Cancel The Operation and Close The Form
                    Me.Close()
                Case "Add"
                    'Open File Dialouge,and add selected file to the list view
                    AddDICOMFile()
                Case "Remove"
                    'Remove File From The List
                    RemoveDICOMFile(0)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RemoveDICOMFile(ByVal msgFlag As Boolean)
        If lvDocuments.SelectedItems.Count > 0 Then
            isRemovedAfterSave = False
            Dim rDialoug As DialogResult

            If msgFlag Then
                rDialoug = Windows.Forms.DialogResult.Yes
            Else
                rDialoug = MessageBox.Show("Do you want to remove this file?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            End If

            If rDialoug.ToString() = "Yes" Then
                lvDocuments.SelectedItems(0).Remove()
                txtDocumentName.Text = ""
                If lvDocuments.Items.Count = 0 Then
                    tlb_Remove.Enabled = False
                End If

                If msgFlag Then
                    If lvDocuments.Items.Count <= 0 Then
                        isRemovedAfterSave = True
                    End If
                End If
            End If

        Else
            MessageBox.Show("Select a file to remove", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub AddDICOMFile()
        Dim oDialog As New OpenFileDialog()
        oDialog.Multiselect = True
        oDialog.Title = "Import Images"
        'oDialog.Filter = "DCM Files(*.DCM)|*.DCM|JPEG files(*.jpg*)|*.jpg*"
        Dim _DuplicateFiles As String = ""

        Try
            If oDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                For Each CurFile As String In oDialog.FileNames
                    Dim oFile As New System.IO.FileInfo(CurFile)
                    If oFile IsNot Nothing Then
                        If IsValidExtenstionFile(oFile.Extension) Then
                            Dim _AddThisFile As Boolean = True
                            For Count As Integer = 0 To lvDocuments.Items.Count - 1
                                Dim strFilename As String = oFile.Name.ToUpper() + oFile.Extension.ToUpper()
                                If lvDocuments.Items(Count).SubItems(2).Text.ToUpper() = oFile.FullName.ToUpper() Then
                                    _AddThisFile = False
                                    If _DuplicateFiles.Trim() = "" Then
                                        _DuplicateFiles = strFilename
                                    Else
                                        _DuplicateFiles = _DuplicateFiles + Environment.NewLine & strFilename
                                    End If
                                    Exit For
                                End If
                                strFilename = Nothing
                            Next
                            If _AddThisFile = True Then
                                Dim oItem As New ListViewItem()
                                If oItem IsNot Nothing Then
                                    Dim fName() As String = oFile.Name.Split(".")
                                    oItem.Text = fName(0) ' oFile.Name
                                    oItem.SubItems.Add(oFile.Extension.ToString())
                                    oItem.SubItems.Add(oFile.FullName)
                                    oItem.SubItems.Add("False")
                                    lvDocuments.Items.Add(oItem)
                                    txtDocumentName.Text = fName(0) 'oFile.Name
                                    lvDocuments.Items(lvDocuments.Items.Count - 1).Selected = True
                                    txtDocumentName.Focus()
                                    oItem = Nothing
                                End If
                            End If
                        End If
                    End If
                    If oFile IsNot Nothing Then
                        oFile = Nothing
                    End If
                Next
                If _DuplicateFiles.Trim() <> "" Then
                    MessageBox.Show("Following files are duplicate..." + Environment.NewLine & _DuplicateFiles, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            If lvDocuments.Items.Count > 0 Then
                tlb_Remove.Enabled = True
            Else
                tlb_Remove.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK)
        Finally
            If (IsNothing(oDialog) = False) Then
                oDialog.Dispose()
                oDialog = Nothing
            End If
            _DuplicateFiles = Nothing
        End Try

    End Sub

    Private Function IsValidExtenstionFile(ByVal Extenstion As String)
        Dim result As Boolean = False
        Dim strExtenstion = Extenstion.ToUpper()
        ' If strExtenstion = ".JPEG" Or strExtenstion = ".JPG" Or strExtenstion = ".DCM" Then
        result = True
        'End If
        Return result
    End Function

    Private Sub frmImportDICOM_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        FilesToImport = Nothing
    End Sub

    Private Sub frmImportDICOM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvDocuments.Items.Clear()
        tlb_Remove.Enabled = False
    End Sub

    Private Sub lvDocuments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvDocuments.SelectedIndexChanged
        If lvDocuments.SelectedItems.Count > 0 Then
            Dim fName() As String = lvDocuments.SelectedItems(0).SubItems(0).Text.Split(".")
            txtDocumentName.Text = fName(0)
            isKeyPressed = False
            isValidated = False
            fName = Nothing
        End If
    End Sub

    Private Sub txtDocumentName_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocumentName.Validated
        If isValidated = False Then

            txtDocumentName.Text = txtDocumentName.Text.Trim()
            If lvDocuments.SelectedItems.Count > 0 Then
                If txtDocumentName.Text = "" Then
                    MessageBox.Show("Please enter name for the file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    isValidated = False
                Else
                    If isKeyPressed Then
                        Dim oDicom As New clsDICOM()
                        If oDicom.CheckValidFileName(txtDocumentName.Text) Then
                            For count As Integer = 0 To lvDocuments.Items.Count - 1
                                If lvDocuments.Items(count).SubItems(0).Text.ToUpper() = txtDocumentName.Text.ToUpper() Then
                                    MessageBox.Show("File with the name '" + lvDocuments.Items(count).SubItems(0).Text + "' is already present", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    isValidated = False
                                    txtDocumentName.Text = lvDocuments.SelectedItems(0).Text
                                    Exit Sub
                                End If
                            Next

                            Dim dResult As DialogResult = MessageBox.Show("Are you sure you want to change the file name?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                            If dResult.ToString() = "Yes" Then
                                lvDocuments.SelectedItems(0).SubItems(0).Text = txtDocumentName.Text
                            Else
                                txtDocumentName.Text = lvDocuments.SelectedItems(0).SubItems(0).Text
                                lvDocuments.Items(lvDocuments.SelectedItems(0).Index).Selected = True
                            End If
                            isValidated = True
                            txtDocumentName.Text = ""
                            lvDocuments.Focus()
                        End If
                    Else
                        isValidated = True
                    End If
                End If
            Else
                isValidated = True
            End If

        End If
    End Sub


    Private Sub txtDocumentName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocumentName.KeyPress
        isKeyPressed = True
    End Sub

    Private Sub txtDocumentName_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDocumentName.KeyDown
        isKeyPressed = True
    End Sub
End Class