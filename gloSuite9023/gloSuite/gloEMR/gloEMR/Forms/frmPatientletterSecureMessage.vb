Imports System.Data.SqlClient
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports gloPatientPortalCommon
Imports System.Runtime.InteropServices
Imports gloOffice
Imports gloWord

Public Class frmPatientletterSecureMessage

    Private _dtPatientList As New DataTable
    Private _dtTemplateList As New DataTable
    Private _bIsPatientLetter As Boolean = False
    Private _IsRemiderForUnSchedle As Boolean = False
    Private ClientMachineID As String
    Private ClientMachineName As String
    Private _nCommunicationTypeID As Int64 = 0
    Private SendMessageto As Int32 = 0

    Friend WithEvents wdPatientLetter1 As AxDSOFramer.AxFramerControl

    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oTempDoc As Wd.Document
    Private WithEvents oWordApp As Wd.Application
    Dim objWord As clsWordDocument
    Dim objCriteria As DocCriteria
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _clinicId As Int64 = 0

    Structure PatientMessage
        Public PatientID As Int64
        Public Message As String
    End Structure

    Dim oPatientMessages As New List(Of PatientMessage)()

    Public Property Subject() As String
        Get
            Return txtSubject.Text
        End Get
        Set(ByVal value As String)
            txtSubject.Text = value
        End Set
    End Property


    Public Property Message() As String
        Get
            Return txtMessage.Text
        End Get
        Set(ByVal value As String)
            txtMessage.Text = value
        End Set
    End Property

    Public _IsAnytaskProcessed As Boolean = False
    Public Property IsAnytaskProcessed() As Boolean
        Get
            Return _IsAnytaskProcessed
        End Get
        Set(ByVal value As Boolean)
            _IsAnytaskProcessed = value
        End Set
    End Property




    Public Sub New(ByVal dtPatientList As DataTable, ByVal dtTemplateList As DataTable, ByVal bIsPatientLetter As Boolean, ByVal IsRemiderForUnSchedle As Boolean, ByVal sClientMachineID As String, ByVal sClientMachineName As String)
        InitializeComponent()

        'Patient Letter
        _dtPatientList = dtPatientList
        _dtTemplateList = dtTemplateList
        _bIsPatientLetter = bIsPatientLetter
        _IsRemiderForUnSchedle = IsRemiderForUnSchedle
        ClientMachineID = sClientMachineID
        ClientMachineName = sClientMachineName

    End Sub

    Private Sub frmPatientletterSecureMessage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            If appSettings("ClinicID") IsNot Nothing Then
                If appSettings("ClinicID") <> "" Then
                    _clinicId = Convert.ToInt64(appSettings("ClinicID"))
                Else
                    _clinicId = 0
                End If
            Else
                _clinicId = 0
            End If

            If _bIsPatientLetter = True Then
                chkAllowReply.Checked = True
                SendMessageto = 0
                If (cmbMessageSendto.Items.Count > 0) Then
                    cmbMessageSendto.SelectedIndex = 0
                End If

                txtSubject.SelectionStart = 0
                Dim _nuserID As String = mdlGeneral.gnLoginID.ToString()
                Dim dt As DataTable = Nothing
                dt = getLoginUserDetails(_nuserID)

                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    lblfrom.Text = dt.Rows(0)("LoginName").ToString()
                    lblportaldisplay.Text = dt.Rows(0)("PortalDisplayName").ToString()
                End If

                If _dtTemplateList IsNot Nothing Then

                    _dtTemplateList.Columns.Add("ContainsPatientAccountFields", GetType(Boolean))

                    c1Group.DataSource = _dtTemplateList
                    c1Group.Cols("TemplateID").Visible = False
                    c1Group.Cols("TemplateFilepath").Visible = False
                    c1Group.Cols("isnontemplatefile").Visible = False
                    c1Group.Cols("isnontemplatefile").AllowEditing = False
                    c1Group.Cols("TemplateName").Caption = "File name"
                    c1Group.Cols("TemplateName").AllowEditing = False
                    c1Group.Cols("TemplateName").Width = Convert.ToInt32(270)
                    c1Group.Cols("ContainsPatientAccountFields").Visible = False
                    c1Group.Cols("Delete").Width = Convert.ToInt32(35)
                    c1Group.Cols("Delete").ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                    c1Group.Cols("Delete").AllowEditing = False
                    c1Group.Cols("Delete").AllowResizing = False

                    c1Group.Cols("mailicon").Width = Convert.ToInt32(25)
                    c1Group.Cols("mailicon").ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                    c1Group.Cols("mailicon").AllowEditing = False
                    c1Group.Cols("mailicon").AllowResizing = False

                    c1Group.Cols("FileSize").Caption = "File Size"
                    c1Group.Cols("FileSize").AllowEditing = False
                    c1Group.Cols("FileSize").Width = Convert.ToInt32(80)

                    SetGridStyle()

                End If


                If _dtTemplateList IsNot Nothing AndAlso _dtPatientList IsNot Nothing Then
                    If _dtTemplateList.Rows.Count > 0 AndAlso _dtPatientList.Rows.Count > 0 Then
                        'gloC1FlexStyle.Style(c1Group)
                        If _dtTemplateList.Rows.Count >= 1 Then

                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle '= c1Group.Styles.Add("Button")
                            Try
                                If (c1Group.Styles.Contains("Button")) Then
                                    cStyle = c1Group.Styles("Button")
                                Else
                                    cStyle = c1Group.Styles.Add("Button")
                                End If
                            Catch ex As Exception
                                cStyle = c1Group.Styles.Add("Button")
                            End Try
                            Dim rgReaction As C1.Win.C1FlexGrid.CellRange = c1Group.GetCellRange(0, 0, _dtTemplateList.Rows.Count - 1, 0)
                            rgReaction.Style = cStyle

                            Dim cStyle2 As C1.Win.C1FlexGrid.CellStyle '= c1Group.Styles.Add("Button2")
                            Try
                                If (c1Group.Styles.Contains("Button2")) Then
                                    cStyle2 = c1Group.Styles("Button2")
                                Else
                                    cStyle2 = c1Group.Styles.Add("Button2")
                                End If
                            Catch ex As Exception
                                cStyle2 = c1Group.Styles.Add("Button2")
                            End Try
                            Dim rgReaction2 As C1.Win.C1FlexGrid.CellRange = c1Group.GetCellRange(0, 5, _dtTemplateList.Rows.Count - 1, 5)
                            rgReaction2.Style = cStyle2

                            For i As Integer = 0 To _dtTemplateList.Rows.Count - 1
                                c1Group.SetCellImage(i, 0, imgList.Images(3))
                                c1Group.SetCellImage(i, 5, imgList.Images(2))
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1Group_Click(sender As System.Object, e As System.EventArgs) Handles c1Group.Click
        Try
            If c1Group.DataSource IsNot Nothing Then
                If c1Group.Rows.Count >= 1 And c1Group.RowSel >= 0 Then
                    Dim drv As DataRowView = TryCast(c1Group.Rows(c1Group.RowSel).DataSource, System.Data.DataRowView)
                    If drv IsNot Nothing AndAlso drv.Row.Table.Rows.Count > 0 Then
                        If c1Group.ColSel = 5 Then
                            If MessageBox.Show("Do you want to delete file?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                For Each dr As DataRow In _dtTemplateList.Rows
                                    If dr("TemplateID").ToString() = c1Group.Rows(c1Group.RowSel)("TemplateID") Then
                                        dr.Delete()
                                        Exit For
                                    End If
                                Next
                                _dtTemplateList.AcceptChanges()
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddAttachment_Click(sender As System.Object, e As System.EventArgs) Handles btnAddAttachment.Click
        Try
            If _dtTemplateList IsNot Nothing Then
                If _dtTemplateList.Rows.Count >= 3 Then
                    System.Windows.MessageBox.Show("You can attach only three items.", gstrMessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
                    Exit Sub
                End If
            End If

            Dim strFileName As String = ""
            Dim strFilePath As String = ""
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim ogloInterface As New gloCCDLibrary.gloCCDInterface

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = "C:\"
            fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True
            fd.Multiselect = False
            fd.ShowDialog()

            If fd.FileName <> "" Then
                strFilePath = fd.FileName
                strFileName = fd.SafeFileName

                Dim info As New FileInfo(strFilePath)
                ' Get the size of the file in bytes.
                Dim Bytes As Long = info.Length

                Dim dblConvertSize As Double
                dblConvertSize = GetConvertedBytesSize(Bytes)

                Dim TotalSize As Double = GetTotalAttachmentSize()
                If (dblConvertSize > 4) Or (TotalSize + dblConvertSize) > 4 Then
                    'System.Windows.MessageBox.Show("Attachments are exceeding maximum allowed limit 4 MB" & Environment.NewLine & " File size of'" & strFileName & "' is " & SetBytes(Bytes) & Environment.NewLine & "Rest of File size " & TotalSize, gstrMessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
                    System.Windows.MessageBox.Show("Attachments are exceeding maximum allowed limit 4 MB.", gstrMessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
                    Exit Sub
                End If

                Dim drTemp As DataRow = _dtTemplateList.NewRow
                drTemp("TemplateID") = DateTime.Now.ToString("ddHHmmss")
                drTemp("TemplateName") = strFileName
                drTemp("TemplateFilepath") = strFilePath
                drTemp("isnontemplatefile") = True
                drTemp("FileSize") = strFileName
                Dim sizeinfo As New FileInfo(strFilePath)
                drTemp("FileSize") = SetBytes(CType(sizeinfo.Length, Long))


                _dtTemplateList.Rows.Add(drTemp)

                If _dtTemplateList.Rows.Count >= 1 Then
                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle '= c1Group.Styles.Add("Button")
                    Try
                        If (c1Group.Styles.Contains("Button")) Then
                            cStyle = c1Group.Styles("Button")
                        Else
                            cStyle = c1Group.Styles.Add("Button")
                        End If
                    Catch ex As Exception
                        cStyle = c1Group.Styles.Add("Button")
                    End Try
                    Dim rgReaction As C1.Win.C1FlexGrid.CellRange = c1Group.GetCellRange(0, 0, _dtTemplateList.Rows.Count - 1, 0)
                    rgReaction.Style = cStyle

                    Dim cStyle2 As C1.Win.C1FlexGrid.CellStyle '= c1Group.Styles.Add("Button2")
                    Try
                        If (c1Group.Styles.Contains("Button2")) Then
                            cStyle2 = c1Group.Styles("Button2")
                        Else
                            cStyle2 = c1Group.Styles.Add("Button2")
                        End If
                    Catch ex As Exception
                        cStyle2 = c1Group.Styles.Add("Button2")
                    End Try
                    Dim rgReaction2 As C1.Win.C1FlexGrid.CellRange = c1Group.GetCellRange(0, 5, _dtTemplateList.Rows.Count - 1, 5)
                    rgReaction2.Style = cStyle2

                    For i As Integer = 0 To _dtTemplateList.Rows.Count - 1
                        c1Group.SetCellImage(i, 0, imgList.Images(3))
                        c1Group.SetCellImage(i, 5, imgList.Images(2))
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Function GetConvertedBytesSize(ByVal Bytes) As Decimal

        Dim dblValue As Double
        Dim strarray As String()
        Dim strreturn As String = "0"

        Try

            If Bytes >= 1073741824 Then
                dblValue = Bytes / 1024 / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2)
                Else
                    strreturn = strarray(0)
                End If

            ElseIf Bytes >= 1048576 Then
                dblValue = Bytes / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2)
                Else
                    strreturn = strarray(0)
                End If
            End If

            Return CDbl(strreturn)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try
    End Function

    Public Function GetTotalAttachmentSize() As Double
        Dim dblTotalSize As Double = 0
        Try
            If _dtTemplateList IsNot Nothing Then
                If _dtTemplateList.Rows.Count > 0 Then
                    'DirectCast(lstAttachment.Items(0),System.Windows.Controls.Border).Tag
                    For i As Integer = 0 To _dtTemplateList.Rows.Count - 1
                        Dim info As New FileInfo(_dtTemplateList.Rows(i)("TemplateFilepath"))
                        ' Get the size of the file in bytes.
                        Dim Bytes As Long = info.Length
                        dblTotalSize += GetConvertedBytesSize(Bytes)
                    Next
                End If
            End If
            Return dblTotalSize
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dblTotalSize
        End Try
    End Function

    Function SetBytes(ByVal Bytes) As String

        Dim dblValue As Double
        Dim strarray As String()
        Dim strreturn As String = "0 Bytes"

        Try

            If Bytes >= 1073741824 Then
                dblValue = Bytes / 1024 / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2) & " GB"
                Else
                    strreturn = strarray(0) & " GB"
                End If

            ElseIf Bytes >= 1048576 Then
                dblValue = Bytes / 1024 / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2) & " MB"
                Else
                    strreturn = strarray(0) & " MB"
                End If

            ElseIf Bytes >= 1024 Then
                dblValue = Bytes / 1024
                strarray = CStr(dblValue).ToString.Split(".")
                If strarray.Length = 2 Then
                    strreturn = strarray(0) & "." & strarray(1).Substring(0, 2) & " KB"
                Else
                    strreturn = strarray(0) & " KB"
                End If
            End If

            Return strreturn

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            dblValue = Nothing
            strarray = Nothing
            strreturn = Nothing
        End Try

    End Function

    Private Function GetPracticeID() As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim PracticeID As Int64
        Try
            oDB.Connect(False)
            oDBParameters.Add("@PracticeID", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDB.Execute("Intuit_GetPracticeID", oDBParameters, PracticeID)
            oDB.Disconnect()

            Return PracticeID
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            PracticeID = Nothing
        End Try
    End Function

    Private Sub tlbtnSend_Click(sender As System.Object, e As System.EventArgs) Handles tlbtnSend.Click
        Try
            Dim dtPRdata As New DataTable
            If GetPracticeID() = 0 Then
                System.Windows.MessageBox.Show("There is no Practice ID added in the system which is needed to send a mail. This can be added from the Patient Portal service configuration settings or gloEMR admin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If SendMessageto = 1 Then
                dtPRdata = CheckRepresentativeCount(_dtPatientList)
                If dtPRdata IsNot Nothing AndAlso dtPRdata.Rows.Count > 0 Then
                    Dim intCount As Int64 = CType(dtPRdata.Compute("sum(PRcount)", String.Empty), Int64)

                    If intCount <= 0 Then
                        MessageBox.Show("There are no representative(s) associated for the selected patient(s).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                Else
                    MessageBox.Show("There are no representative(s) associated for the selected patient(s).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If

            If txtSubject.Text.ToString().Trim() = "" Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("Please enter the subject.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtSubject.Text) > 100 Then
                txtSubject.Focus()
                System.Windows.MessageBox.Show("The current subject length is " + Len(txtSubject.Text).ToString + ". The subject length must be less than 100 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If txtMessage.Text.ToString().Trim() = "" Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("Please enter the message.     ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If Len(txtMessage.Text) > 8000 Then
                txtMessage.Focus()
                System.Windows.MessageBox.Show("The current message length is " + Len(txtMessage.Text).ToString + ". The message length must be less than 8000 characters.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim strConfirmMessage As String = ""
            If SendMessageto = 1 Then
                strConfirmMessage = "You are sending a secure message to " & _dtPatientList.Rows.Count & " patient(s) representative(s)" & Environment.NewLine & "Please note that the process takes time according to number of patients selected." & Environment.NewLine & "Do you want to continue?"
            Else
                strConfirmMessage = "You are sending a secure message to " & _dtPatientList.Rows.Count & " patient(s)." & Environment.NewLine & "Please note that the process takes time according to number of patients selected." & Environment.NewLine & "Do you want to continue?"
            End If
            If MessageBox.Show(strConfirmMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim IsTaskFieldpresent As Boolean = False
                pnlPrintMessage.Visible = True
                Label24.Visible = True
                Label24.BringToFront()
                lblFormularyTransactionMessage.Visible = True
                pnlPrintMessage.BringToFront()
                Application.DoEvents()
                Me.Cursor = Cursors.WaitCursor
                tlbtnSend.Enabled = False
                ts_btnClose.Enabled = False

                If _dtPatientList IsNot Nothing Then
                    If _dtPatientList.Rows.Count > 0 Then
                        If _dtPatientList.Columns.Contains("nTaskID") Then
                            IsTaskFieldpresent = True
                        End If
                        FillReminderforUnscheduledCare()
                        Dim dtAttachment As New DataTable
                        dtAttachment.Columns.Add("filename", GetType(String))
                        dtAttachment.Columns.Add("filedata", GetType(Object))
                        dtAttachment.AcceptChanges()

                        Dim AllSelectedPatient As New System.Text.StringBuilder()
                        For Each drPatient As DataRow In _dtPatientList.Rows
                            If AllSelectedPatient.Length = 0 Then
                                AllSelectedPatient.Append(Convert.ToString(drPatient("PatientID")))
                            Else
                                AllSelectedPatient.Append("," + Convert.ToString(drPatient("PatientID")))
                            End If
                        Next

                        '' Will Check in Template having Account Field or not
                        Dim isAnySelectedTemplateContainsPatientAccountFields As Boolean = False
                        Dim mytempLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                        Try
                            For Each drTemp As DataRow In _dtTemplateList.Rows
                                Dim tempfiletype As String = drTemp("isnontemplatefile")
                                Dim IsAccountField As Boolean = False
                                If drTemp("isnontemplatefile") = False Then
                                    IsAccountField = CheckContainsPatientAccountFields(mytempLoadWord, Convert.ToString(drTemp("TemplateFilepath")))
                                    drTemp("ContainsPatientAccountFields") = IsAccountField
                                    If IsAccountField = True Then
                                        isAnySelectedTemplateContainsPatientAccountFields = True
                                    End If
                                Else
                                    drTemp("ContainsPatientAccountFields") = False
                                End If
                            Next

                        Catch ex As Exception
                        Finally
                            mytempLoadWord.CloseApplicationOnly()
                            mytempLoadWord = Nothing
                        End Try

                        '' Will Check patient having multiple Account Field or not
                        Dim dtAllPatientWithMultipleAccount As New DataTable
                        dtAllPatientWithMultipleAccount = GetAllPatientWithMultipleAccount(AllSelectedPatient.ToString())
                        AllSelectedPatient = Nothing

                        _dtPatientList.Columns.Add("HaveMultipleAccounts", GetType(Boolean))
                        _dtPatientList.AcceptChanges()

                        Dim viewdistinct As New DataView(_dtPatientList)
                        Dim dtdistinctpatient As DataTable = viewdistinct.ToTable(True, "PatientID")


                        Dim ogloInterface As New gloCCDLibrary.gloCCDInterface
                        Dim ocls As New clsPatientLetters


                        Dim AccountID As Int64 = 0
                        Dim isTemplateSkipped As Boolean = False
                        If dtdistinctpatient.Rows.Count = 1 Then
                            Dim IsPatientHaveMultipleAccounts As Boolean = False
                            If dtAllPatientWithMultipleAccount IsNot Nothing AndAlso dtAllPatientWithMultipleAccount.Rows.Count >= 1 Then
                                Dim nPatientid As Int64 = Convert.ToInt64(dtdistinctpatient.Rows(0)("PatientID"))
                                IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(nPatientid, dtAllPatientWithMultipleAccount)

                                If IsPatientHaveMultipleAccounts AndAlso isAnySelectedTemplateContainsPatientAccountFields Then

                                    Dim ofrmSelectPatientGuarantor As New frmSelectPatientGuarantor(nPatientid, _clinicId)
                                    Dim oClsSelectPatientGuarantor As New clsSelectPatientGuarantor(nPatientid, _clinicId)
                                    Dim dtAccounts As DataTable = Nothing
                                    dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(nPatientid, _clinicId)
                                    If (dtAccounts.Rows.Count = 1) Then
                                        AccountID = Convert.ToInt64(dtAccounts.Rows(0)("nPAccountID").ToString())
                                    ElseIf (dtAccounts.Rows.Count > 1) Then
                                        ofrmSelectPatientGuarantor.ShowDialog(Me)
                                        'aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                        If ofrmSelectPatientGuarantor.DialogResult = System.Windows.Forms.DialogResult.OK Then
                                            AccountID = ofrmSelectPatientGuarantor.SelectedAccount
                                        Else
                                            AccountID = -1
                                        End If
                                    Else
                                        AccountID = 0
                                    End If
                                    If (ofrmSelectPatientGuarantor Is Nothing) = False Then
                                        ofrmSelectPatientGuarantor.Dispose()
                                        ofrmSelectPatientGuarantor = Nothing
                                    End If
                                    If (oClsSelectPatientGuarantor Is Nothing) = False Then
                                        oClsSelectPatientGuarantor.Dispose()
                                        oClsSelectPatientGuarantor = Nothing
                                        'MessageBox.Show("Select Patient Account");
                                    End If
                                End If
                            End If
                        End If


                        Dim strExcludedTemplates As New System.Text.StringBuilder()
                        strExcludedTemplates.Append(System.Environment.NewLine)
                        'strExcludedTemplates.Append("Attention – Your Appointment for Print selection includes patients with multiple accounts. These patients cannot print as part of a batch and will be excluded from this batch. After this batch print is complete, select each excluded patient one at a time and print. You will be asked to select the correct account for each patient.")
                        strExcludedTemplates.Append("Attention – Your selection includes patients with multiple accounts. These patients cannot be as part of a batch and will be excluded from this batch. After this batch is complete, select each excluded patient one at a time and send the letters. You will be asked to select the correct account for each patients")
                        strExcludedTemplates.Append(System.Environment.NewLine)
                        strExcludedTemplates.Append(System.Environment.NewLine)
                        strExcludedTemplates.Append("Excluded Patient(s) (excluded template(s)):")
                        strExcludedTemplates.Append(System.Environment.NewLine)
                        strExcludedTemplates.Append("-------------------------------------------------------------------")

                        Dim cnt As Integer = 1
                        'Dim operationTime As Long = 0
                        'UpdateLog("*************************************** New Start ***************************************")
                        'Dim sw As New Stopwatch()
                        For Each drPatient As DataRow In _dtPatientList.Rows
                            'operationTime = 0
                            'sw.Start()
                            lblFormularyTransactionMessage.Text = "Sending secure message " + Convert.ToString(cnt) + " of " + Convert.ToString(_dtPatientList.Rows.Count)
                            Dim _PatientID As Int64 = Convert.ToInt64(drPatient("PatientID"))
                            Dim PatientName As String = Convert.ToString(drPatient("Patient Name"))
                            Dim _TaskID As Int64 = 0
                            If IsTaskFieldpresent = True Then
                                _TaskID = Convert.ToInt64(drPatient("nTaskID"))
                            End If
                            Dim IsPatientHaveMultipleAccounts As Boolean = False
                            If dtAllPatientWithMultipleAccount IsNot Nothing AndAlso dtAllPatientWithMultipleAccount.Rows.Count >= 1 Then
                                IsPatientHaveMultipleAccounts = CheckPatientHaveMultipleAccounts(_PatientID, dtAllPatientWithMultipleAccount)
                            End If
                            drPatient("HaveMultipleAccounts") = IsPatientHaveMultipleAccounts

                            If SendMessageto = 1 Then
                                Dim view As New DataView(dtPRdata)
                                view.RowFilter = "PatientID = '" + _PatientID.ToString() + "'"
                                If view.ToTable().Rows.Count > 0 Then
                                    If view.ToTable().Rows(0)("PRcount") = 0 Then
                                        Continue For
                                    End If
                                End If
                            End If
                            'UpdateLog("# Patient Name =>  " + PatientName)
                            Dim arrtemplate As New ArrayList
                            dtAttachment.Rows.Clear()
                            dtAttachment.AcceptChanges()
                            Dim processTempltecount As Int64 = 0
                            For Each drTemp As DataRow In _dtTemplateList.Rows
                                Dim drAttach As DataRow = dtAttachment.NewRow
                                Dim _TemplateID As Int64 = Convert.ToInt64(drTemp("TemplateID"))
                                Dim _TemplateName As String = Convert.ToString(drTemp("TemplateName"))
                                Dim _TemplateFilePath As String = Convert.ToString(drTemp("TemplateFilepath"))
                                Dim _TemplateContainsPatientAccountFields As Boolean = Convert.ToBoolean(drTemp("ContainsPatientAccountFields"))

                                Dim _VisitID As Int64 = GenerateVisitID(Now, _PatientID)

                                If drTemp("isnontemplatefile") = False Then

                                    If AccountID = 0 Then
                                        If IsPatientHaveMultipleAccounts AndAlso _TemplateContainsPatientAccountFields Then
                                            arrtemplate.Add(Path.GetFileNameWithoutExtension(_TemplateName))
                                            isTemplateSkipped = True
                                            Continue For
                                        End If
                                    End If

                                    processTempltecount += 1
                                    'Fill_TemplateGallery(_TemplateID, _PatientID, _VisitID)
                                    Dim strFileName As String = _TemplateFilePath

                                    Dim Wordfilewithdata As String = ""
                                    Dim strFileNamePDF As String = ""
                                    Try
                                        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                                        Dim aDoc As Microsoft.Office.Interop.Word.Document = myLoadWord.LoadWordApplication(strFileName)
                                        gloOffice.Supporting.DataBaseConnectionString = GetConnectionString()
                                        gloOffice.Supporting.PatientID = _PatientID
                                        gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.[Date].ToString("MM/dd/yyyy"))
                                        gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.[Date].ToString("MM/dd/yyyy"))
                                        ' wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
                                        gloOffice.Supporting.PrimaryID = _TemplateID
                                        gloOffice.Supporting.WdApplication = aDoc.Application
                                        gloOffice.Supporting.CurrentDocument = aDoc
                                        System.Windows.Forms.Application.DoEvents()
                                        gloOffice.Supporting.isFromBatchPrint = True
                                        gloOffice.Supporting.GetFormFieldDataRevised(aDoc, Nothing, AccountID)
                                        gloOffice.Supporting.isFromBatchPrint = False
                                        Wordfilewithdata = gloOffice.Supporting.NewDocumentName()
                                        aDoc.SaveAs(Wordfilewithdata)
                                        gloWord.LoadAndCloseWord.CleanupDoc(aDoc)
                                        strFileNamePDF = Path.Combine(Path.GetDirectoryName(Wordfilewithdata), Path.GetFileNameWithoutExtension(Wordfilewithdata) + ".pdf")
                                        aDoc.SaveAs(strFileNamePDF, Wd.WdSaveFormat.wdFormatPDF, False, "", False)
                                        myLoadWord.CloseWordOnly(aDoc)
                                        myLoadWord.CloseApplicationOnly()
                                        myLoadWord = Nothing
                                    Catch ex As Exception
                                        System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
                                    End Try

                                    Dim myBytePDF As Byte() = ogloInterface.ConvertFiletoBinary(strFileNamePDF)

                                    Dim PDFBinaray As Object = Nothing
                                    If (IsNothing(myBytePDF) = False) Then
                                        PDFBinaray = CType(myBytePDF, Object)
                                        drAttach("filename") = Path.GetFileNameWithoutExtension(_TemplateName) + ".pdf"
                                        drAttach("filedata") = PDFBinaray
                                        dtAttachment.Rows.Add(drAttach)
                                    End If
                                    Dim myByte As Byte() = gloWord.LoadAndCloseWord.ConvertFiletoBinary(Wordfilewithdata)

                                    Dim myBinaray As Object = Nothing
                                    If (IsNothing(myByte) = False) Then
                                        myBinaray = CType(myByte, Object)
                                    End If
                                    'wdPatientLetter.Close()
                                    If (ocls.SavePatientLetterBytes(0, _PatientID, _TemplateID, Date.Now, myBinaray, Path.GetFileNameWithoutExtension(_TemplateName), False, _IsRemiderForUnSchedle, _nCommunicationTypeID) > 0) Then
                                    End If

                                    Try
                                        If System.IO.File.Exists(Wordfilewithdata) Then
                                            System.IO.File.Delete(Wordfilewithdata)
                                        End If
                                        If System.IO.File.Exists(strFileNamePDF) Then
                                            System.IO.File.Delete(strFileNamePDF)
                                        End If
                                    Catch ex As Exception
                                    End Try
                                Else
                                    drAttach("filename") = _TemplateName
                                    drAttach("filedata") = ogloInterface.ConvertFiletoBinary(drTemp("TemplateFilepath"))
                                    dtAttachment.Rows.Add(drAttach)
                                End If
                                'UpdateLog("Template Name : " + _TemplateName)
                            Next
                            dtAttachment.AcceptChanges()

                            Dim skippedtemplatenames As String = ""
                            For index As Integer = 0 To arrtemplate.Count - 1
                                If skippedtemplatenames = "" Then
                                    skippedtemplatenames = arrtemplate(index).ToString()
                                Else
                                    skippedtemplatenames = skippedtemplatenames + ", " + arrtemplate(index).ToString()
                                End If
                            Next
                            If arrtemplate.Count > 0 Then
                                AddSkipedTemplateInfo(_PatientID, PatientName + " (" + skippedtemplatenames + ")")

                            End If


                            Dim IsInsert As Boolean = False
                            If _dtTemplateList.Rows.Count = 0 Then
                                IsInsert = True
                            ElseIf _dtTemplateList.Rows.Count > 0 Then
                                If _dtTemplateList.Select("isnontemplatefile = 'True'").Length = _dtTemplateList.Rows.Count Then
                                    IsInsert = True
                                ElseIf _dtTemplateList.Select("isnontemplatefile = 'False'").Length = _dtTemplateList.Rows.Count And processTempltecount > 0 Then
                                    IsInsert = True
                                ElseIf _dtTemplateList.Select("isnontemplatefile = 'True'").Length > 0 And _dtTemplateList.Select("isnontemplatefile = 'False'").Length > 0 Then
                                    IsInsert = True
                                End If
                            End If

                            If IsInsert Then

                                If IsTaskFieldpresent = True Then
                                    If _TaskID <> 0 Then
                                        UpdateReminder(_TaskID)
                                        If _IsAnytaskProcessed = False Then
                                            _IsAnytaskProcessed = True
                                        End If
                                    End If
                                End If

                                Dim dtIntuitCommAttachments As DataTable
                                dtIntuitCommAttachments = New DataTable("TVP_IntuitCommAttachments")

                                Dim dtGl_Messagequeue As DataTable
                                dtGl_Messagequeue = New DataTable("TVP_Gl_Messagequeue")

                                Dim dtIntuitCommDetails As DataTable
                                dtIntuitCommDetails = New DataTable("TVP_IntuitCommDetails")
                                'Master Entry of Message
                                dtGl_Messagequeue = GetGl_Messagequeue(_PatientID)
                                'Get Patient portal Message Details
                                dtIntuitCommDetails = GetPatientPortalCommDetails(_PatientID)
                                ''Get Patient portal Attachment Record
                                dtIntuitCommAttachments = GetIntuitCommAttachments(dtAttachment)
                                SaveMessage(dtGl_Messagequeue, dtIntuitCommDetails, dtIntuitCommAttachments, "PATIENT LETTERS", 0, SendMessageto)
                            End If
                            'operationTime = sw.ElapsedMilliseconds
                            'UpdateLog("Process Time => " + mSecToHMS(operationTime) + " [Min:Sec:Milisec]")
                            'UpdateLog("======================================== Patient " + Convert.ToString(cnt) + " / " + Convert.ToString(_dtPatientList.Rows.Count) + " ========================================")
                            cnt += 1
                        Next
                        ocls.Dispose()
                        ocls = Nothing
                        ogloInterface.Dispose()
                        ogloInterface = Nothing

                        If isTemplateSkipped Then
                            Dim intpatcount As Int64 = 1
                            For Each oMsg As PatientMessage In oPatientMessages
                                strExcludedTemplates.Append(System.Environment.NewLine)
                                strExcludedTemplates.Append(Convert.ToString(intpatcount).PadLeft(4, " ") + ". " + oMsg.Message)
                                intpatcount += 1
                            Next
                            Dim oMsgForm As New frmgloMessageBox()
                            oMsgForm.Text = gstrMessageBoxCaption
                            oMsgForm.Setmessage(strExcludedTemplates)
                            oMsgForm.ShowDialog(ParentForm)
                            oMsgForm.Dispose()
                            oMsgForm = Nothing
                        End If
                    End If
                End If

                tlbtnSend.Enabled = True
                ts_btnClose.Enabled = True
                pnlPrintMessage.Visible = False
                Me.Cursor = Cursors.Default
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            tlbtnSend.Enabled = True
            ts_btnClose.Enabled = True
            pnlPrintMessage.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub c1Group_DoubleClick(sender As Object, e As System.EventArgs) Handles c1Group.DoubleClick
        Try
            If _dtTemplateList IsNot Nothing AndAlso c1Group.RowSel >= 0 Then
                Dim strpath As String = c1Group.Rows(c1Group.RowSel)("TemplateFilepath")
                Dim startInfo As System.Diagnostics.ProcessStartInfo
                Dim pStart As New System.Diagnostics.Process
                startInfo = New System.Diagnostics.ProcessStartInfo(strpath)
                startInfo.UseShellExecute = True
                startInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                startInfo.CreateNoWindow = False
                Process.Start(startInfo)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetGl_Messagequeue(ByVal PatientID As Int64) As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_Gl_Messagequeue")

        'declaring one row for the table
        Dim Row1 As DataRow

        Dim contactID As DataColumn
        Dim fname As DataColumn
        Dim lname As DataColumn

        Try
            'declare a column 
            contactID = New DataColumn("sMachineID")
            contactID.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(contactID)

            fname = New DataColumn("sMachineName")
            fname.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(fname)

            lname = New DataColumn("nPatientID")
            lname.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(lname)

            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sMachineID") = ClientMachineID
            Row1.Item("sMachineName") = ClientMachineName
            Row1.Item("nPatientID") = PatientID

            'adding the completed row to the table
            dt.Rows.Add(Row1)

            If Not IsNothing(contactID) Then
                contactID.Dispose() : contactID = Nothing
            End If

            If Not IsNothing(fname) Then
                fname.Dispose() : fname = Nothing
            End If

            If Not IsNothing(lname) Then
                lname.Dispose() : lname = Nothing
            End If

            Return dt

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    Private Function GetPatientPortalCommDetails(ByVal PatientID As Int64) As DataTable
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommDetails")

        'declaring one row for the table
        Dim Row1 As DataRow

        Dim sCommSubject As DataColumn
        Dim sCommMessage As DataColumn
        Dim bCommMemberReply As DataColumn
        Dim nPatientID As DataColumn
        Dim nCommStaffID As DataColumn
        Dim sCommServiceType As DataColumn
        Dim nCommContainerID As DataColumn
        Dim dAppDateTime As DataColumn
        Dim nStatus As DataColumn
        Dim nCommunicationType As DataColumn
        Dim bisUnscheduledCare As DataColumn
        ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
        ''Added new column to pass value for TVP_IntuitCommDetails.
        Dim nUserID As DataColumn

        Try
            'declare a column 
            sCommSubject = New DataColumn("sCommSubject")
            sCommSubject.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommSubject)

            sCommMessage = New DataColumn("sCommMessage")
            sCommMessage.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommMessage)

            bCommMemberReply = New DataColumn("bCommMemberCannotReply")
            bCommMemberReply.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bCommMemberReply)

            nPatientID = New DataColumn("nPatientID")
            nPatientID.DataType = System.Type.GetType("System.Decimal")
            dt.Columns.Add(nPatientID)

            nCommStaffID = New DataColumn("nCommStaffID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommStaffID)

            sCommServiceType = New DataColumn("sCommServiceType")
            sCommServiceType.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommServiceType)

            nCommContainerID = New DataColumn("nCommContainerID")
            nCommContainerID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommContainerID)

            dAppDateTime = New DataColumn("dAppDateTime")
            dAppDateTime.DataType = System.Type.GetType("System.DateTime")
            dt.Columns.Add(dAppDateTime)

            nStatus = New DataColumn("nStatus")
            nStatus.DataType = System.Type.GetType("System.Int16")
            dt.Columns.Add(nStatus)

            bisUnscheduledCare = New DataColumn("bisUnscheduledCare")
            bisUnscheduledCare.DataType = System.Type.GetType("System.Boolean")
            dt.Columns.Add(bisUnscheduledCare)

            nCommunicationType = New DataColumn("nCommunicationType")
            nCommunicationType.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nCommunicationType)

            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            nUserID = New DataColumn("nUserID")
            nCommStaffID.DataType = System.Type.GetType("System.Int64")
            dt.Columns.Add(nUserID)


            'declaring a new row
            Row1 = dt.NewRow()
            Row1.Item("sCommSubject") = txtSubject.Text
            Row1.Item("sCommMessage") = txtMessage.Text
            Row1.Item("bCommMemberCannotReply") = chkAllowReply.Checked
            Row1.Item("nPatientID") = PatientID
            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Pass value as 0 as nCommStaffID is not used for portal.
            Row1.Item("nCommStaffID") = 0
            'Row1.Item("nCommStaffID") = lblFrom.Tag

            '' take 
            Row1.Item("bisUnscheduledCare") = _IsRemiderForUnSchedle
            If _IsRemiderForUnSchedle = True Then
                Row1.Item("nCommunicationType") = GetCommPreferenceselectedvalue()
            Else
                Row1.Item("nCommunicationType") = 0
            End If

            Row1.Item("sCommServiceType") = "Patient Messaging"
            Row1.Item("nCommContainerID") = 0

            Row1.Item("nUserID") = mdlGeneral.gnLoginID.ToString()
            '''''If (_strserviceType.ToUpper() = "PATIENT MESSAGING" Or _strserviceType.ToUpper() = "ASK A PROVIDER" Or _strserviceType.ToUpper() = "ASK A STAFF" Or _strserviceType.ToUpper() = "REFILL REQUEST" Or _strserviceType.ToUpper() = "APPOINTMENT REQUEST") Then
            '''''Row1.Item("nStatus") = 0
            '''''End If
            Row1.Item("nStatus") = 0

            dt.Rows.Add(Row1)

            If Not IsNothing(sCommSubject) Then
                sCommSubject.Dispose() : sCommSubject = Nothing
            End If

            If Not IsNothing(sCommMessage) Then
                sCommMessage.Dispose() : sCommMessage = Nothing
            End If

            If Not IsNothing(bCommMemberReply) Then
                bCommMemberReply.Dispose() : bCommMemberReply = Nothing
            End If

            If Not IsNothing(nPatientID) Then
                nPatientID.Dispose() : nPatientID = Nothing
            End If

            If Not IsNothing(nCommStaffID) Then
                nCommStaffID.Dispose() : nCommStaffID = Nothing
            End If

            If Not IsNothing(sCommServiceType) Then
                sCommServiceType.Dispose() : sCommServiceType = Nothing
            End If

            If Not IsNothing(nCommContainerID) Then
                nCommContainerID.Dispose() : nCommContainerID = Nothing
            End If

            If Not IsNothing(dAppDateTime) Then
                dAppDateTime.Dispose() : dAppDateTime = Nothing
            End If

            If Not IsNothing(nStatus) Then
                nStatus.Dispose() : nStatus = Nothing
            End If

            ''Task #67507: gloEMR & Patient Portal Send Message screen changes.
            ''Added new column to pass value for TVP_IntuitCommDetails.
            If Not IsNothing(nUserID) Then
                nUserID.Dispose() : nUserID = Nothing
            End If
            Return dt


        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    Private Function GetIntuitCommAttachments(ByVal dtAttach As DataTable)
        'create table
        Dim dt As DataTable
        dt = New DataTable("TVP_IntuitCommAttachments")

        'declaring one row for the table
        Dim Row1 As DataRow
        Dim sCommAttachOneName As DataColumn
        Dim sCommAttachTwoName As DataColumn
        Dim sCommAttachThreeName As DataColumn
        Dim ICommAttachOne As DataColumn
        Dim ICommAttachTwo As DataColumn
        Dim ICommAttachThree As DataColumn

        Try
            'declare a column for first attachment name
            sCommAttachOneName = New DataColumn("sCommAttachOneName")
            sCommAttachOneName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachOneName)

            'declare a column for second attachment name
            sCommAttachTwoName = New DataColumn("sCommAttachTwoName")
            sCommAttachTwoName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachTwoName)

            'declare a column for third attachment name 
            sCommAttachThreeName = New DataColumn("sCommAttachThreeName")
            sCommAttachThreeName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sCommAttachThreeName)

            'declare a column for first attachment
            ICommAttachOne = New DataColumn("ICommAttachOne")
            ICommAttachOne.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachOne)

            'declare a column for second attachment
            ICommAttachTwo = New DataColumn("ICommAttachTwo")
            ICommAttachTwo.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachTwo)

            'declare a column for third attachment
            ICommAttachThree = New DataColumn("ICommAttachThree")
            ICommAttachThree.DataType = System.Type.GetType("System.Byte[]")
            dt.Columns.Add(ICommAttachThree)

            If dtAttach.Rows.Count > 0 Then
                Dim i As Integer
                'declaring a new row
                Row1 = dt.NewRow()
                For i = 0 To dtAttach.Rows.Count - 1

                    If i = 0 Then
                        Row1.Item("sCommAttachOneName") = dtAttach(i)("filename")
                        Row1.Item("ICommAttachOne") = dtAttach(i)("filedata")
                    End If

                    If i = 1 Then
                        Row1.Item("sCommAttachTwoName") = dtAttach(i)("filename")
                        Row1.Item("ICommAttachTwo") = dtAttach(i)("filedata")
                    End If

                    If i = 2 Then
                        Row1.Item("sCommAttachThreeName") = dtAttach(i)("filename")
                        Row1.Item("ICommAttachThree") = dtAttach(i)("filedata")
                    End If

                Next
                'adding the completed row to the table
                dt.Rows.Add(Row1)
            End If

            If Not IsNothing(sCommAttachOneName) Then
                sCommAttachOneName.Dispose() : sCommAttachOneName = Nothing
            End If

            If Not IsNothing(sCommAttachTwoName) Then
                sCommAttachTwoName.Dispose() : sCommAttachTwoName = Nothing
            End If

            If Not IsNothing(sCommAttachThreeName) Then
                sCommAttachThreeName.Dispose() : sCommAttachThreeName = Nothing
            End If

            If Not IsNothing(ICommAttachOne) Then
                ICommAttachOne.Dispose() : ICommAttachOne = Nothing
            End If

            If Not IsNothing(ICommAttachTwo) Then
                ICommAttachTwo.Dispose() : ICommAttachTwo = Nothing
            End If

            If Not IsNothing(ICommAttachThree) Then
                ICommAttachThree.Dispose() : ICommAttachThree = Nothing
            End If

            Return dt

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            Row1 = Nothing
        End Try
    End Function

    Private Function getLoginUserDetails(nuserID As String) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dt As New DataTable
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            'Dim str As String = "select nUserID,sFirstName+' '+sLastName as LoginName ,sPortalDisplayName as PortalDisplayName from User_MST where nUserID=" & nuserID
            oDBParameters.Add("@UserID", Convert.ToInt64(nuserID), ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Connect(False)
            oDB.Retrive("ws_getLoggedInUserdetails", oDBParameters, dt)
            'oDB.Retrive_Query(str, dt)
            oDB.Disconnect()

            Return dt
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try

    End Function

    Private Sub FillReminderforUnscheduledCare()
        Dim objclsPatientLetters As New clsPatientLetters
        Dim ds As DataSet = objclsPatientLetters.FillReminderforUnscheduledCare().Copy()
        If IsNothing(ds) = False Then
            Dim dtReminderList As DataTable = ds.Tables(0)
            Dim dtCheckUnscheduledCare As DataTable = ds.Tables(1)
            If IsNothing(dtReminderList) = False Then

                If dtReminderList.Rows.Count > 0 Then
                    Dim row() As DataRow = dtReminderList.Select(" isSelected=1")
                    If (row.Length > 0) Then

                        If row(0)("nCategoryID") IsNot Nothing Then
                            _nCommunicationTypeID = Convert.ToInt64(row(0)("nCategoryID"))
                        End If

                    End If
                End If
            End If
            ds.Dispose()
            ds = Nothing
        End If
        objclsPatientLetters.Dispose()
        objclsPatientLetters = Nothing
    End Sub

    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)
        'Try

        'Catch 'ex As Exception

        'End Try
    End Sub

    Private Sub wdPatientLetter_BeforeDocumentClosed(sender As System.Object, e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientLetter.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception

                End Try

                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    UpdateVoiceLog(ex.ToString)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientLetter_OnDocumentClosed(sender As System.Object, e As System.EventArgs) Handles wdPatientLetter.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    '   Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientLetter_OnDocumentOpened(sender As System.Object, e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientLetter.OnDocumentOpened
        oCurDoc = wdPatientLetter.ActiveDocument
        oWordApp = oCurDoc.Application

        Try

            'RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            'AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception

        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    Private Function GetCommPreferenceselectedvalue() As Long
        Dim lVal As Long = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing
        Dim ds As New DataSet

        Try
            oDBParameters.Add("@sType", "SecureMessages", ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Retrive("gsp_FillReminderforUnscheduledCare", oDBParameters, ds)
            If IsNothing(ds) = False AndAlso ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
                If IsNothing(dt) = False Then
                    If dt.Rows.Count > 0 Then
                        Dim row() As DataRow = dt.Select(" isSelected=1")
                        If (row.Length > 0) Then
                            lVal = row(0)("nCategoryID")
                        Else
                            lVal = 0
                        End If

                    End If
                End If
            End If
            Return lVal
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return lVal
        Finally

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

            If Not IsNothing(ds) Then
                ds.Dispose() : ds = Nothing
            End If

        End Try
    End Function

    Private Function SaveMessage(ByVal Gl_Messagequeue As DataTable, ByVal IntuitCommDetails As DataTable, ByVal IntuitCommAttachments As DataTable, ByVal MessageType As String, Optional ByVal nPRID As Long = 0, Optional ByVal nSendMessageTo As Int16 = 0) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim MessageID As Int64 = 0
        Try
            oDB.Connect(False)

            oDBParameters.Add("@TVP_Gl_Messagequeue", Gl_Messagequeue, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_IntuitCommDetails", IntuitCommDetails, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@TVP_int_IntuitCommAttachments", IntuitCommAttachments, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@nPRID", nPRID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@MessageType", MessageType, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nSendMessageTo", nSendMessageTo, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@nMessageID", 0, ParameterDirection.Output, SqlDbType.Decimal)
            oDB.Execute("Portal_SaveMessage", oDBParameters, MessageID)
            oDB.Disconnect()

            Return MessageID

        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.Message, gstrMessageBoxCaption)
            Return Nothing
        Finally

            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If

            MessageID = Nothing
        End Try

    End Function

    Private Sub cmbMessageSendto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMessageSendto.SelectedIndexChanged
        SendMessageto = Convert.ToInt32(cmbMessageSendto.SelectedIndex)
    End Sub

    Public Sub SetGridStyle()
        c1Group.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        c1Group.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row

        '' Normal Style
        c1Group.Styles.Normal.BackColor = Color.White
        c1Group.Styles.Normal.Border.Color = Color.White
        c1Group.Styles.Normal.ForeColor = Color.Black

        '' Alternet Style
        c1Group.Styles.Alternate.BackColor = Color.White
        c1Group.Styles.Alternate.Border.Color = Color.White
        c1Group.Styles.Alternate.ForeColor = Color.Black

        '' Focus Style
        c1Group.Styles.Focus.BackColor = Color.White
        c1Group.Styles.Focus.Border.Color = Color.White
        c1Group.Styles.Focus.ForeColor = Color.Black


        '' New Row Style
        c1Group.Styles.NewRow.BackColor = Color.White
        c1Group.Styles.NewRow.Border.Color = Color.White
        c1Group.Styles.NewRow.ForeColor = Color.Black

        '' Empty Area Style
        c1Group.Styles.EmptyArea.BackColor = Color.White
        c1Group.Styles.EmptyArea.Border.Color = Color.White
        c1Group.Styles.EmptyArea.ForeColor = Color.White

        '' Focus Style
        c1Group.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        c1Group.Styles.Focus.ForeColor = Color.Black
    End Sub

    Public Function CheckRepresentativeCount(dtPat As DataTable) As DataTable
        Dim intCount As Int64 = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim dtPRdata As New DataTable
        Try
            If dtPat IsNot Nothing Then
                Dim dtpatCopy As New DataTable
                Dim columnsToKeep As String() = {"PatientID"}
                dtpatCopy = dtPat.Copy()
                For index As Integer = dtpatCopy.Columns.Count - 1 To 0 Step -1
                    Dim columnName As String = dtpatCopy.Columns(index).ColumnName
                    If Not columnsToKeep.Contains(columnName) Then
                        dtpatCopy.Columns.RemoveAt(index)
                    End If
                Next
                dtpatCopy.AcceptChanges()

                If dtpatCopy.Rows.Count > 0 Then
                    oDB.Connect(False)
                    oDBParameters.Add("@TVP_Patients", dtpatCopy, ParameterDirection.Input, SqlDbType.Structured)
                    oDB.Retrive("WS_GetPatRepresentative", oDBParameters, dtPRdata)
                    oDB.Disconnect()
                End If
            End If
            Return dtPRdata
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose() : oDB = Nothing
            End If
        End Try
    End Function

    Private Function GetAllPatientWithMultipleAccount(strPatientIDs As [String]) As DataTable
        Dim dtAllPatientsWithMultipleAccounts As New DataTable()
        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            '(Convert.ToString(appSettings["DatabaseConnectionString"]));
            Dim oPara As New gloDatabaseLayer.DBParameters()
            oDB.Connect(False)
            oPara.Add("@strPatientIDs", strPatientIDs, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Retrive("gsp_AllPatientwithMultipleAccounts", oPara, dtAllPatientsWithMultipleAccounts)
            oPara.Dispose()
            oPara = Nothing
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            Return dtAllPatientsWithMultipleAccounts
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            Return dtAllPatientsWithMultipleAccounts
        Finally
            dtAllPatientsWithMultipleAccounts.Dispose()
        End Try
    End Function

    Private Function CheckPatientHaveMultipleAccounts(PatientID As Int64, dtPatientsWithMultipleAccount As DataTable) As Boolean
        Dim dv As New DataView()
        Try
            dv = dtPatientsWithMultipleAccount.DefaultView
            dv.RowFilter = "nPatientID= " + PatientID.ToString() + ""
            If dv.Count >= 1 Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function


    Private Function CheckContainsPatientAccountFields(ByRef myLoadWord As gloWord.LoadAndCloseWord, sFilename As String) As Boolean
        Dim IsConytainsAccountFields As [Boolean] = False
        Dim toQuit As Boolean = False
        Try
            If myLoadWord Is Nothing Then
                myLoadWord = New gloWord.LoadAndCloseWord()
                toQuit = True
            End If

            Dim aDoc As Microsoft.Office.Interop.Word.Document = myLoadWord.LoadWordApplication(sFilename)

            If aDoc IsNot Nothing Then
                For Each aField As Wd.FormField In aDoc.FormFields
                    If aField.StatusText.StartsWith("pa_accounts.") OrElse aField.StatusText.StartsWith("PA_Accounts_Patients.") Then
                        IsConytainsAccountFields = True
                        Exit For
                    End If
                Next
                myLoadWord.CloseWordOnly(aDoc)
            End If

            If toQuit Then
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
            End If

            aDoc = Nothing
            Return IsConytainsAccountFields
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            Return IsConytainsAccountFields
        Finally
        End Try
    End Function

    Private Sub AddSkipedTemplateInfo(PatientID As Int64, PatientName As String)
        Dim IspatientExist As [Boolean] = False
        For Each oMsg As PatientMessage In oPatientMessages
            If oMsg.PatientID = PatientID Then
                IspatientExist = True
            End If
        Next
        If IspatientExist = False Then
            Dim oMessage As New PatientMessage()
            oMessage.Message = PatientName
            oMessage.PatientID = PatientID
            oPatientMessages.Add(oMessage)
        End If
    End Sub

    Private Sub UpdateReminder(ByVal TaskID As Int64)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "UPDATE RM_Reminder_MST SET bIsDismissed ='TRUE' WHERE nRefrenceType = 2 AND nReferenceID = " & TaskID & " "
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
        Catch ex As SqlException
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Sub

    'Public Sub UpdateLog(strMessage1 As String)
    '    Dim objFile As System.IO.StreamWriter
    '    Dim strMessage As New System.Text.StringBuilder
    '    Dim _fileName As String = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log"

    '    Dim LogPath As String = "C:\\Log\\ApplicationLog"
    '    If Not Directory.Exists(LogPath) Then

    '        System.IO.Directory.CreateDirectory(LogPath)
    '    End If

    '    objFile = New System.IO.StreamWriter(LogPath + "\\" + _fileName, True)


    '    strMessage.Append(strMessage1)

    '    objFile.WriteLine(strMessage.ToString())
    '    If (Not IsNothing(objFile)) Then
    '        objFile.Close()
    '        objFile.Dispose()
    '        objFile = Nothing

    '    End If
    '    If (Not IsNothing(strMessage)) Then
    '        strMessage.Remove(0, strMessage.Length)
    '        strMessage = Nothing
    '    End If
    'End Sub
    'Private Function mSecToHMS(ByVal millSec As Long) As String
    '    ' 
    '    Dim ts As TimeSpan
    '    Dim totHrs As Integer
    '    Dim H, M, S, MS, HMS As String
    '    ' 
    '    'Place milliseconds into timespand variable 
    '    'to expose conversion properties 
    '    ts = TimeSpan.FromMilliseconds(millSec)
    '    ' 
    '    'Get H M S values and format for leading zero 
    '    'Add a trailing semi colon on Hours and minutes 
    '    'Total hours will allow display of more than 24 hrs 
    '    'while minutes and seconds will be limited to 0-59 
    '    ' 
    '    totHrs = Math.Truncate(ts.TotalHours) 'strip away decimal points 
    '    H = Format(totHrs, "0#") & ":"
    '    M = Format(ts.Minutes, "0#") & ":"
    '    S = Format(ts.Seconds, "0#") & ":"
    '    MS = Convert.ToString(millSec)
    '    ' 
    '    'Combine Hours Minutes and seconds into HH:MM:SS string 
    '    'HMS = H & M & S
    '    HMS = M & S & MS
    '    ' 
    '    Return HMS
    'End Function

End Class



