Imports System.Data.SqlClient
Imports UnZipFileIonic
Imports gloUserControlLibrary

Public Class frmPHIReview
    Private Shared frmForReviewMode As frmPHIReview
    Private Shared frmForPHIMode As frmPHIReview
    Public Shared IsFormOpenedInReviewMode As Boolean = False
    Public Shared IsFormOpenedInPHIMode As Boolean = False
    Private Con As SqlConnection

    Private Col_PHIID As Integer = 0
    Private Col_PatientName As Integer = 1
    Private Col_Title As Integer = 2
    Private Col_Date As Integer = 3
    Private Col_PatientID As Integer = 4

    Private c1PHITaskListColCOUNT As Integer = 5
    Private c1PHITaskListColCOUNTFroPatient As Integer = 4

    Dim dtSource As New DataTable
    Public TaskID As Int64 = 0
    Public SelectedPHIID As Int64 = 0
    Public SelectedDocumentname As String = ""
    Dim strTempPHIFolder As String = gloSettings.FolderSettings.AppTempFolderPath + "\\PHI\\"
    Dim SelectedPatientID As Int64 = 0
    Public IsPHIReviewMode As Boolean


    Private m_PatientID As Long
    '    Friend WithEvents pnlOuter As System.Windows.Forms.Panel
    Private m_VisitDate As Date
    ' Friend WithEvents C1PatientRos As C1.Win.C1FlexGrid.C1FlexGrid
    '  Friend WithEvents pnlPatientHeader As System.Windows.Forms.Panel

    Public Sub New(ByVal VisitDate As Date, Optional ByVal nPatientID As Long = 0, Optional ByVal bIsForSelectedPatient As Boolean = False)
        InitializeComponent()
        SelectedPatientID = nPatientID
        IsPHIReviewMode = bIsForSelectedPatient
        m_PatientID = nPatientID
        m_VisitDate = VisitDate
    End Sub

    Private Sub frmPHIReview_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            ' ''

            ' ''
            If IsPHIReviewMode Then
                Me.Text = "Portal PHI Review"

                labIntuitPatientList.Text = "PHI Task List"
                FillandLoadControls()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "PHI viewed", gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Else
                Call Set_PatientDetailStrip()
                Me.Text = "Patient PHI"

                labIntuitPatientList.Text = "Patient PHI"
                FillandLoadForselectedPatientMode()
                '  Dim _selPatientID As Long = CType(c1PHITaskList.GetData(c1PHITaskList.RowSel, Col_PatientID - 1), Long)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ViewPHI, gloAuditTrail.ActivityType.View, "PHI viewed for patient", SelectedPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If
            ShowHideToolButton()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub ShowHideToolButton()
        If Not IsPHIReviewMode Then
            tlbbtn_Refresh.Visible = False
            tlbbtn_AcceptPatient.Visible = False
            tlbbtn_RejectPatient.Visible = False

        Else
            tlbbtn_Refresh.Visible = True
            tlbbtn_AcceptPatient.Visible = True
            tlbbtn_RejectPatient.Visible = True

        End If

    End Sub
    Public Shared Function GetInstance(ByVal VisitID As Long, ByVal VisitDate As Date, ByVal PatientID As Long, Optional ByVal _IsPHIReviewMode As Boolean = False, Optional ByVal blnRecordLock As Boolean = False) As frmPHIReview
        '_mu.WaitOne()
        Try
            IsFormOpenedInReviewMode = False
            IsFormOpenedInPHIMode = False
            ''If frm Is Nothing Then

            Dim openForms As New List(Of Form)()

            For Each oform As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
                If oform.Name = "frmPHIReview" Then

                    openForms.Add(oform)
                    If DirectCast(oform, frmPHIReview).IsPHIReviewMode = True Then
                        IsFormOpenedInReviewMode = True
                        frmForReviewMode = oform
                    Else
                        IsFormOpenedInPHIMode = True
                        frmForPHIMode = oform
                    End If
                End If
            Next

            If _IsPHIReviewMode = True Then
                If IsFormOpenedInReviewMode = True Then
                    Return frmForReviewMode
                Else
                    frmForReviewMode = New frmPHIReview(VisitDate, PatientID, _IsPHIReviewMode)
                    Return frmForReviewMode
                End If
            Else
                If IsFormOpenedInPHIMode = True Then
                    If DirectCast(frmForPHIMode, frmPHIReview).SelectedPatientID = PatientID Then
                        Return frmForPHIMode
                    Else
                        frmForPHIMode.Close()
                        frmForPHIMode = New frmPHIReview(VisitDate, PatientID, _IsPHIReviewMode)
                        Return frmForPHIMode
                    End If

                Else
                    frmForPHIMode = New frmPHIReview(VisitDate, PatientID, _IsPHIReviewMode)
                    Return frmForPHIMode
                End If
            End If

        Finally

        End Try

    End Function
    Private Sub ts_Main_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_Main.ItemClicked
        Try
            Select Case e.ClickedItem.Name
                Case tlbbtn_AcceptPatient.Name
                    Call AcceptRejectPHI(True)
                Case tlbbtn_RejectPatient.Name
                    Call AcceptRejectPHI(False)

                Case tlbbtn_Refresh.Name()
                    SelectedPHIID = 0
                    txtSearch.Clear()
                    Call FillandLoadControls()
                Case tlbbtn_Close.Name
                    If System.IO.Directory.Exists(strTempPHIFolder) Then
                        '  My.Computer.FileSystem.DeleteDirectory(strTempPHIFolder, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        DeleteDirectory(strTempPHIFolder)
                    End If
                    Me.Close()

            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "MenuItem clicked :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub c1PHITaskList_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PHITaskList.MouseMove
        '    gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, c1PHITaskList, e.Location)
    End Sub
    Public Sub c1PHITaskList_RowColChange(sender As System.Object, e As System.EventArgs) Handles c1PHITaskList.RowColChange
        Try
            If c1PHITaskList.RowSel > 0 Then
                Dim _selPHIID As Long = CType(c1PHITaskList.GetData(c1PHITaskList.RowSel, Col_PHIID), Long)
                FillSelectedPHI(_selPHIID)
                SelectedPHIID = _selPHIID
            Else
                txtTitle.Text = ""
                txtDescription.Text = ""
                txtLink.Text = ""
                dgDocuments.DataSource = Nothing
                txtUserComments.Text = ""
                cmbCategory.SelectedIndex = 0
                txtSubmissionDate.Text = ""
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "c1PHITaskList_RowColChange :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub c1PHITaskList_SelChange(sender As System.Object, e As System.EventArgs) Handles c1PHITaskList.SelChange

    End Sub

    Private Sub dgDocuments_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgDocuments.CellContentClick
        Try

            If e.RowIndex >= 0 Then
                If dgDocuments.Columns(e.ColumnIndex).Name = "Action" Then
                    SelectedDocumentname = dgDocuments.Rows(e.RowIndex).Cells("Document").Value.ToString()
                    ExtractAndShowDocumet(SelectedDocumentname, True)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "dgDocuments_CellContentClick :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Function ExtractAndShowDocumet(ByVal SelectedDocumentname As String, Optional ByVal bShowDocmentForPreview As Boolean = True) As String
        Dim retPDFDocPath As String = ""
        Try
            If bShowDocmentForPreview Then
                pnlProgress.Visible = True
                Application.DoEvents()
                Me.Refresh()
            End If
            dgDocuments.Enabled = False
            c1PHITaskList.Enabled = False
            Dim dtDocZipData As DataTable = GetDocuments(SelectedPHIID)
            If dtDocZipData IsNot Nothing AndAlso dtDocZipData.Rows.Count > 0 Then
                Dim datacontent As String = ""
                Dim bytesRead() As Byte = CType(dtDocZipData.Rows(0)("sDocument"), Byte())
                datacontent = System.Text.Encoding.ASCII.GetString(bytesRead)
                If Not System.IO.Directory.Exists(strTempPHIFolder) Then
                    System.IO.Directory.CreateDirectory(strTempPHIFolder)
                End If
                Dim strZipFilePath = System.IO.Path.Combine(strTempPHIFolder, "PHI" + SelectedPHIID.ToString() + DateTime.Now().ToString("yyyyddMMhhmmss") + ".zip")
                System.IO.File.WriteAllBytes(strZipFilePath, Convert.FromBase64String(datacontent))
                Dim FinalPath As String = clsExtractFile.ExtractZipFile(strZipFilePath)
                If System.IO.Path.GetExtension(SelectedDocumentname).ToUpper() <> ".PDF".ToUpper() Then
                    Dim sOtherDocument As String = System.IO.Path.Combine(FinalPath, SelectedDocumentname)
                    Dim strOtherDocumentasPDFpath As String = System.IO.Path.Combine(FinalPath, System.IO.Path.GetFileNameWithoutExtension(SelectedDocumentname) + ".pdf")
                    Dim sDestPdf1 As String = ""
                    sDestPdf1 = gloWord.gloWord.ConvertFileToPDF(sOtherDocument, FinalPath)
                    Dim file As New System.IO.FileInfo(sDestPdf1)
                    file.CopyTo(strOtherDocumentasPDFpath)
                    If file.Exists Then
                        file.Delete()
                    End If
                    retPDFDocPath = strOtherDocumentasPDFpath
                Else
                    retPDFDocPath = System.IO.Path.Combine(FinalPath, SelectedDocumentname)
                End If
                If bShowDocmentForPreview AndAlso Not String.IsNullOrEmpty(retPDFDocPath) Then
                    Dim frmPHIDocReview As New frmPHIDocumentReview(retPDFDocPath)
                    If bShowDocmentForPreview Then
                        pnlProgress.Visible = False
                    End If
                    frmPHIDocReview.ShowDialog()
                    frmPHIDocReview.Dispose()
                    frmPHIDocReview = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "ExtractAndShowDocumet :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
        dgDocuments.Enabled = True
        c1PHITaskList.Enabled = True
        Return retPDFDocPath
    End Function
    Public Function AcceptRejectPHI(Optional ByVal bAccptmode As Boolean = True) As Boolean
        Dim res As Boolean = False
        Dim nPatientProvider As Long = 0
        Try
            If dtSource Is Nothing Or dtSource.Rows.Count = 0 Then
                MessageBox.Show("No Records to Proccess.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return res
            End If

            Dim _selPatientID As Long = CType(c1PHITaskList.GetData(c1PHITaskList.RowSel, Col_PatientID), Long)

            Dim objProvider As New clsProvider()
            nPatientProvider = objProvider.GetPatientProvider(_selPatientID)
            If bAccptmode Then

                Dim DMScategoryID As Int16 = CType(cmbCategory.SelectedValue, Int16)
                Dim DMSCategoryName As String = cmbCategory.Text.Trim()
                Dim DMSConnectionstring As String = GetDMSConnectionString()

                Dim strDocname As String = ""
                Dim strDocFullPath As String = ""
                If Not dgDocuments.DataSource Is Nothing AndAlso dgDocuments.Rows.Count > 0 Then
                    If bAccptmode Then
                        If cmbCategory.SelectedIndex = 0 Then
                            MessageBox.Show("Select DMS Category.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return res
                        End If
                    End If
                    tlbbtn_Close.Enabled = False
                    pnlProgress.Visible = True
                    tlbbtn_Refresh.Enabled = False
                    tlbbtn_AcceptPatient.Enabled = False
                    tlbbtn_RejectPatient.Enabled = False
                    txtUserComments.Enabled = False
                    cmbCategory.Enabled = False

                    Me.Refresh()
                    For i As Integer = 0 To dgDocuments.Rows.Count - 1
                        Dim eDocumentId As Long = 0
                        Dim result As Boolean = False
                        strDocname = dgDocuments.Rows(i).Cells("Document").Value.ToString()
                        strDocFullPath = ExtractAndShowDocumet(strDocname, bShowDocmentForPreview:=False)
                        result = InsertPHIToDMS(DMScategoryID, DMSCategoryName, strDocFullPath, eDocumentId, eDocumentId)
                        Dim nStatusDetailid As Int32 = 1

                        If result Then
                            nStatusDetailid = 0
                        Else
                            nStatusDetailid = 2
                        End If

                        ProcessPHIDetails(SelectedPHIID, eDocumentId, strDocname, nStatusDetailid)
                    Next
                End If
                If gnLoginProviderID <> 0 Then
                    ProcessPHI(SelectedPHIID, DMScategoryID, txtUserComments.Text.Trim(), 0, gnLoginID, gnLoginProviderID)
                Else
                    ProcessPHI(SelectedPHIID, DMScategoryID, txtUserComments.Text.Trim(), 0, gnLoginID, nPatientProvider)
                End If


                res = True



                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.Accept, "PHI accepted", _selPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                ProcessPHI(SelectedPHIID, 0, txtUserComments.Text.Trim(), 2, gnLoginID, gnLoginProviderID)
                res = True
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.Reject, "PHI rejected", _selPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "AcceptRejectPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If res Then
                RemoveHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange
                FillPHI()
                cmbCategory.SelectedIndex = 0
                AddHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange
                If dtSource IsNot Nothing AndAlso dtSource.Rows.Count > 0 Then
                    SelectedPHIID = CType(dtSource.Rows(0).Item(0), Long)
                    c1PHITaskList.RowSel = c1PHITaskList.FindRowRegex(SelectedPHIID.ToString(), 0, 0, True)
                    c1PHITaskList.Select(c1PHITaskList.RowSel, 1)

                Else
                    SelectedPHIID = 0
                End If
                FillSelectedPHI(SelectedPHIID)
            End If


            pnlProgress.Visible = False
            tlbbtn_Close.Enabled = True
            tlbbtn_Refresh.Enabled = True
            tlbbtn_AcceptPatient.Enabled = True
            tlbbtn_RejectPatient.Enabled = True
            txtUserComments.Enabled = True
            cmbCategory.Enabled = True
        End Try
        Return res
    End Function
    Public Sub FillPHI(Optional ByVal nPHIID As Long = 0)
        Try
            dtSource = GetPHI()
            c1PHITaskList.DataSource = dtSource
            DisignGrid()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "FillPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Throw ex
        End Try


    End Sub
    Public Sub FillPHIForselectedPatientMode(ByVal nPatientID As Long)
        Try
            dtSource = GetPHIForselectedPatientMode(nPatientID)
            c1PHITaskList.DataSource = dtSource
            DisignGridForselectedPatientMode()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "FillPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Throw ex
        End Try


    End Sub
    Public Sub DisignGrid()
        c1PHITaskList.Cols.Count = c1PHITaskListColCOUNT
        c1PHITaskList.Cols(Col_PHIID).Visible = False
        c1PHITaskList.Cols(Col_PatientName).WidthDisplay = 80
        c1PHITaskList.Cols(Col_Title).WidthDisplay = 150
        c1PHITaskList.Cols(Col_Date).WidthDisplay = 80
        c1PHITaskList.Cols(Col_PatientName).AllowEditing = False
        c1PHITaskList.Cols(Col_Title).AllowEditing = False
        c1PHITaskList.Cols(Col_Date).AllowEditing = False
        c1PHITaskList.SetData(0, Col_PatientName, "Patient")
        c1PHITaskList.SetData(0, Col_Title, "Title")
        c1PHITaskList.SetData(0, Col_Date, "Date")
        c1PHITaskList.Cols(Col_Date).Format = "MM/dd/yyyy h:mm:ss tt"

        c1PHITaskList.SetData(0, Col_PatientID, "PatientID")
        c1PHITaskList.Cols(Col_PatientID).Visible = False


        c1PHITaskList.ExtendLastCol = True
    End Sub

    Public Sub DisignGridForselectedPatientMode()
        c1PHITaskList.Cols.Count = c1PHITaskListColCOUNTFroPatient
        c1PHITaskList.Cols(Col_PHIID).Visible = False
        c1PHITaskList.Cols(Col_PatientName).WidthDisplay = 150
        c1PHITaskList.Cols(Col_Title).WidthDisplay = 80
        c1PHITaskList.Cols(Col_PatientName).AllowEditing = False
        c1PHITaskList.Cols(Col_Title).AllowEditing = False
        c1PHITaskList.SetData(0, Col_PatientName, "Title")
        c1PHITaskList.SetData(0, Col_Title, "Date")
        c1PHITaskList.ExtendLastCol = True
        c1PHITaskList.Cols(Col_Title).Format = "MM/dd/yyyy h:mm:ss tt"

        c1PHITaskList.SetData(0, Col_PatientID - 1, "PatientID")
        c1PHITaskList.Cols(Col_PatientID - 1).Visible = False
        c1PHITaskList.ExtendLastCol = True
    End Sub
    Public Sub FillandLoadControls()
        Try
            txtSearch.Clear()
            RemoveHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange
            FillPHI()
            cmbCategory.DataSource = GetDMSCategory(True)
            cmbCategory.DisplayMember = "CategoryName"
            cmbCategory.ValueMember = "CategoryId"
            AddHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange
            'Dim RowIndex As Int16 = c1PHITaskList.FindRow(SelectedPHIID, 0, 0, False, False, False)
            'c1PHITaskList.Row = RowIndex
            If SelectedPHIID = 0 Then
                If dtSource IsNot Nothing AndAlso dtSource.Rows.Count > 0 Then
                    SelectedPHIID = CType(dtSource.Rows(0).Item(0), Long)
                    c1PHITaskList.RowSel = c1PHITaskList.FindRowRegex(SelectedPHIID.ToString(), 0, 0, True)
                    c1PHITaskList.Select(c1PHITaskList.RowSel, 1)

                Else
                    SelectedPHIID = 0
                End If
            End If
            FillSelectedPHI(SelectedPHIID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "FillandLoadControls :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub
    Public Sub FillandLoadForselectedPatientMode()
        Try
            txtSearch.Clear()
            RemoveHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange
            FillPHIForselectedPatientMode(SelectedPatientID)
            cmbCategory.DataSource = GetDMSCategory(True)
            cmbCategory.DisplayMember = "CategoryName"
            cmbCategory.ValueMember = "CategoryId"
            AddHandler c1PHITaskList.RowColChange, AddressOf c1PHITaskList_RowColChange


            'If SelectedPHIID = 0 Then
            If dtSource IsNot Nothing AndAlso dtSource.Rows.Count > 0 Then
                SelectedPHIID = CType(dtSource.Rows(0).Item(0), Long)
                c1PHITaskList.RowSel = c1PHITaskList.FindRowRegex(SelectedPHIID.ToString(), 0, 0, True)
                c1PHITaskList.Select(c1PHITaskList.RowSel, 1)

            Else
                SelectedPHIID = 0
            End If
            'End If
            FillSelectedPHI(SelectedPHIID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "FillandLoadControls :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub
    Public Sub FillSelectedPHI(Optional ByVal _selPHIID As Long = 0)
        Try
            If Not c1PHITaskList.DataSource Is Nothing AndAlso c1PHITaskList.Rows.Count > 1 Then
                If _selPHIID > 0 Then
                    Dim DtData As DataTable = GetPHI(_selPHIID)
                    If DtData IsNot Nothing AndAlso DtData.Rows.Count > 0 Then
                        txtTitle.Text = Convert.ToString(DtData.Rows.Item(0).Item("sTitle"))
                        txtLink.Text = Convert.ToString(DtData.Rows.Item(0).Item("sLink"))
                        txtDescription.Text = Convert.ToString(DtData.Rows.Item(0).Item("sDescription"))
                        If Not IsPHIReviewMode Then
                            cmbCategory.SelectedValue = Convert.ToString(DtData.Rows.Item(0).Item("nCategoryID"))
                            ' cmbCategory.Text = "No documents were attached "
                            cmbCategory.Enabled = False
                            txtUserComments.ReadOnly = True
                        Else
                            cmbCategory.SelectedValue = 0
                            cmbCategory.Enabled = True
                            txtUserComments.ReadOnly = False

                        End If

                        Dim strDocName = Convert.ToString(DtData.Rows.Item(0).Item("sDocumentNames"))
                        dgDocuments.DataSource = Nothing
                        dgDocuments.AllowUserToDeleteRows = False
                        dgDocuments.AllowUserToAddRows = False
                        dgDocuments.EditMode = DataGridViewEditMode.EditProgrammatically

                        Dim docTable As DataTable = GetDocumentsForGrid(_selPHIID)
                        Dim docTable1 As New DataTable
                        docTable1.Columns.Add("Document", GetType(String))
                        For Each docname As DataRow In docTable.Rows

                            docTable1.Rows.Add(docname(0))
                        Next
                        dgDocuments.DataSource = docTable1
                        Dim links As New DataGridViewLinkColumn()
                        With links
                            .Name = "Action"
                            .Text = "view"
                            .UseColumnTextForLinkValue = True
                            .HeaderText = "Action"
                            .DataPropertyName = "Document"
                            .ActiveLinkColor = Color.DarkBlue
                            .LinkBehavior = LinkBehavior.SystemDefault
                            .LinkColor = Color.Blue
                            .TrackVisitedState = True
                            .VisitedLinkColor = Color.Maroon
                            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        End With
                        If Not dgDocuments.Columns.Contains("Action") Then
                            dgDocuments.Columns.Add(links)
                            dgDocuments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        End If

                        Dim pnlWidth As Int32 = dgDocuments.Width
                        dgDocuments.Columns(0).Width = Convert.ToDouble(pnlWidth * 0.8)


                        ''If Not String.IsNullOrEmpty(strDocName) Then
                        ''    Dim arrDocNames() As String
                        ''    If True Then
                        ''        arrDocNames = strDocName.Split(",")
                        ''        If IsNothing(arrDocNames) = False Then
                        ''            If arrDocNames.Length > 0 Then
                        ''                Dim docTable As New DataTable
                        ''                docTable.Columns.Add("Document", GetType(String))
                        ''                For Each docname As String In arrDocNames
                        ''                    docTable.Rows.Add(docname)
                        ''                Next
                        ''                dgDocuments.DataSource = docTable
                        ''                Dim links As New DataGridViewLinkColumn()
                        ''                With links
                        ''                    .Name = "Action"
                        ''                    .Text = "view"
                        ''                    .UseColumnTextForLinkValue = True
                        ''                    .HeaderText = "Action"
                        ''                    .DataPropertyName = "Document"
                        ''                    .ActiveLinkColor = Color.DarkBlue
                        ''                    .LinkBehavior = LinkBehavior.SystemDefault
                        ''                    .LinkColor = Color.Blue
                        ''                    .TrackVisitedState = True
                        ''                    .VisitedLinkColor = Color.Maroon
                        ''                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        ''                End With
                        ''                dgDocuments.Columns.Add(links)
                        ''                dgDocuments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                        ''                Dim pnlWidth As Int32 = dgDocuments.Width
                        ''                dgDocuments.Columns(0).Width = Convert.ToDouble(pnlWidth * 0.8)

                        ''            End If
                        ''        End If
                        ''    End If

                        ''End If


                        txtUserComments.Text = Convert.ToString(DtData.Rows.Item(0).Item("sUserComments"))
                        cmbCategory.SelectedValue = Convert.ToInt32(DtData.Rows.Item(0).Item("nCategoryId"))
                        ''txtSubmissionDate.Text = Convert.ToString(DtData.Rows.Item(0).Item("dtCreationDate"))
                        SelectedPatientID = Convert.ToInt64(DtData.Rows.Item(0).Item("nPatientID"))
                        c1PHITaskList.RowSel = c1PHITaskList.FindRowRegex(SelectedPHIID.ToString(), 0, 0, True)
                        c1PHITaskList.Select(c1PHITaskList.RowSel, 1)
                    Else
                        SelectedPHIID = CType(dtSource.Rows(0).Item(0), Long)
                        c1PHITaskList.RowSel = c1PHITaskList.FindRowRegex(SelectedPHIID.ToString(), 0, 0, True)
                        c1PHITaskList.Select(c1PHITaskList.RowSel, 1)

                    End If
                Else
                End If
            Else
                txtTitle.Text = ""
                txtLink.Text = ""
                txtDescription.Text = ""
                dgDocuments.DataSource = Nothing
                txtUserComments.Text = ""
                txtSubmissionDate.Text = ""
                cmbCategory.SelectedIndex = 0
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "FillSelectedPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Shared Sub DeleteDirectory(target_dir As String)
        Try
            Dim files As String() = System.IO.Directory.GetFiles(target_dir)
            Dim dirs As String() = System.IO.Directory.GetDirectories(target_dir)

            For Each file__1 As String In files
                System.IO.File.SetAttributes(file__1, System.IO.FileAttributes.Normal)
                System.IO.File.Delete(file__1)
            Next

            For Each dir As String In dirs
                DeleteDirectory(dir)
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "DeleteDirectory :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try


        System.IO.Directory.Delete(target_dir, False)
    End Sub

    Public Function GetDocuments(ByVal nPHIID As Long) As DataTable
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            cmd = New SqlCommand("gsp_ScanPHIDocument", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@nPHIID", nPHIID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPHIID
            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetDocuments :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function GetDocumentsForGrid(ByVal nPHIID As Long) As DataTable
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            cmd = New SqlCommand("gsp_ScanPHIDocumentDetails", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@nPHIID", nPHIID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPHIID
            Con.Open()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetDocuments :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function GetPHI(Optional ByVal nPHIID As Long = 0) As DataTable
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)

            cmd = New SqlCommand("gsp_ScanPHI", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@nPHIID", nPHIID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPHIID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function GetPHIForselectedPatientMode(ByVal nPatientID As Long) As DataTable
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)

            cmd = New SqlCommand("gsp_ScanPatientPHI", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@nPatientID", nPatientID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatientID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetPHI :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function GetDMSCategory(Optional ByVal addSelectindata As Boolean = False) As DataTable
        Dim ds As DataSet = Nothing
        Dim adpt As SqlDataAdapter = Nothing
        Dim Conn As New SqlConnection
        Dim Cmd As New SqlCommand
        Try
            adpt = New SqlDataAdapter
            ds = New DataSet
            Conn = New System.Data.SqlClient.SqlConnection(GetDMSConnectionString())
            Dim strqry As String = ""
            If (addSelectindata = True) Then
                strqry = "SELECT 0 AS CategoryId , 'Select DMS Category' AS CategoryName,0 AS ClinicID UNION "
            End If

            strqry = (strqry + "select  CategoryId, CategoryName, isnull(ClinicID,0) as ClinicID from eDocument_Category_V3 where isnull(CategoryName,'') <> '' order by CategoryId")
            Cmd = New SqlCommand(strqry, Conn)
            Cmd.CommandType = CommandType.Text
            adpt.SelectCommand = Cmd
            adpt.Fill(ds)
            Conn.Close()
            Return ds.Tables(0)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetDMSCategory :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Conn.Close()
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "GetDMSCategory :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Conn.Close()
            Return Nothing
        Finally
            If (Not (Cmd) Is Nothing) Then
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If (Not (adpt) Is Nothing) Then
                adpt.Dispose()
                adpt = Nothing
            End If

            If (Not (Conn) Is Nothing) Then
                Conn.Dispose()
                Conn = Nothing
            End If
        End Try

    End Function
    Public Function ProcessPHIDetails(ByVal nPHIID As Long, ByVal eDocumentID As Long, ByVal sDocumentNames As String, ByVal nStatus As Int32) As Boolean
        Dim cmd As SqlCommand = Nothing
        Try
            Dim _result As Integer = 0
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            cmd = New SqlCommand("gsp_ProcessPHIDdetails", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nTaskId", 0)
            cmd.Parameters.AddWithValue("@nPHIID", nPHIID)
            cmd.Parameters.AddWithValue("@eDocumentID", eDocumentID)
            cmd.Parameters.AddWithValue("@sDocumentNames", sDocumentNames)
            cmd.Parameters.AddWithValue("@nStatus", nStatus)

            Con.Open()
            _result = cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.Accept, "ProcessPHI: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Return False
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function InsertPHIToDMS(ByVal DMScategoryID As Int16, ByVal DMSCategoryName As String, ByVal strPDFfullpath As String, ByRef DestContainerID As Long, ByRef DestDocumentID As Long) As Boolean
        Dim oDialogResultIsOK As Boolean = False
        Try
            Dim DMSConnectionstring As String = GetDMSConnectionString()
            If DMScategoryID > 0 And DMSConnectionstring <> "" And strPDFfullpath <> "" Then
                Dim filename As String = DateTime.Now.ToString("MM dd yyyy HH mm ss tt")
                Dim stryear As String = DateTime.Now.Year.ToString()
                Dim strmonth As String = DateTime.Now.ToString("MMMM")

                Dim oDialogDocumentID As Int64 = 0
                Dim oDialogContainerID As Int64 = 0
                Dim pbDocument As New ProgressBar()
                pbDocument.Minimum = 0
                pbDocument.Maximum = 100
                Dim oDocManager As gloEDocumentV3.eDocManager.eDocManager = New gloEDocumentV3.eDocManager.eDocManager()
                oDocManager._isgloServices = True
                filename = System.IO.Path.GetFileNameWithoutExtension(strPDFfullpath) + "_" + filename

                Dim oSourceDocuments As New ArrayList()
                oSourceDocuments.Add(strPDFfullpath)

                Dim nPatientId As Int64 = SelectedPatientID
                Dim nClinicId As Int64 = 1
                oDocManager.SetSettings(GetConnectionString(), DMSConnectionstring, gloEMRGeneralLibrary.gloGeneral.clsgeneral.gDMSV3TempPath + "DMSLogFile.txt", gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp")
                oDocManager.ConnectToPDFTron()
                oDialogResultIsOK = oDocManager.ImportSplit(nPatientId, oSourceDocuments, filename, DMScategoryID, DMSCategoryName, "", _
                 stryear, strmonth, nClinicId, oDialogContainerID, oDialogDocumentID, False, pbDocument, gloEDocumentV3.Enumeration.enum_OpenExternalSource.PHI)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.View, "InsertPHIToDMS :" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try



        Return oDialogResultIsOK
    End Function

    Public Function ProcessPHI(ByVal nPHIID As Long, ByVal nCategoryId As Integer, ByVal sUserComments As String, ByVal nStatusId As Integer, ByVal nReviewUserId As Long, ByVal nProviderId As Long) As Boolean

        Dim cmd As SqlCommand = Nothing
        Try
            Dim _result As Integer = 0
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            cmd = New SqlCommand("gsp_ProcessPHI", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nPHIID", nPHIID)
            cmd.Parameters.AddWithValue("@nCategoryId", nCategoryId)
            cmd.Parameters.AddWithValue("@sUserComments", sUserComments)
            cmd.Parameters.AddWithValue("@nStatusId", nStatusId)
            cmd.Parameters.AddWithValue("@nReviewUserId", nReviewUserId)
            cmd.Parameters.AddWithValue("@nProviderId", nProviderId)
            Con.Open()
            _result = cmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.Accept, "ProcessPHI: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Return False
        Finally
            'objBusLayer = Nothing
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function

    Private Sub btnSearchClose_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchClose.Click
        txtSearch.Clear()
    End Sub


    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged

        Try
            Dim dt As DataTable = dtSource
            Dim dv As New DataView(dt)
            If IsPHIReviewMode Then
                dv.RowFilter = String.Format("sTitle LIKE '%{0}%' or PatientName LIKE '%{0}%' ", txtSearch.Text)

            Else
                dv.RowFilter = String.Format("sTitle LIKE '%{0}%'  ", txtSearch.Text)

            End If
            c1PHITaskList.DataSource = dv.ToTable()
            If IsPHIReviewMode Then
                DisignGrid()
            Else
                DisignGridForselectedPatientMode()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PHI, gloAuditTrail.ActivityCategory.ReviewPHI, gloAuditTrail.ActivityType.Accept, "txtSearch_TextChanged: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try


    End Sub

#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUC_PatientStrip

        With gloUC_PatientStrip1
            .ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.PHI)
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .SendToBack()

            .DTPValue = Format(m_VisitDate, "MM/dd/yyyy")
            .DTPEnabled = False
        End With

        Me.Controls.Add(gloUC_PatientStrip1)
        pnlToolstrip.SendToBack()
    End Sub

#End Region
End Class

