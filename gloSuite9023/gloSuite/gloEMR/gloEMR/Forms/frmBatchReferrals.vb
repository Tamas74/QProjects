Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports System.Text
Imports gloUserControlLibrary
Imports System.Data.SqlClient

Public Class frmBatchReferrals

    Private bnlPrintInProgress As Boolean
    Private Col_Select As Integer = 0
    Private Col_ExamID As Integer = 1
    Private Col_VisitID As Integer = 2
    Private Col_ExamName As Integer = 3
    Private Col_PatientID As Integer = 4
    ' Private Col_PatientNotes As Integer = 5
    Private Col_IsFinish As Integer = 5
    Private Col_DOS As Integer = 6
    Private COl_Isopen As Integer = 7
    Private Col_UserName As Integer = 8
    Private Col_MachineName As Integer = 9
    Private Col_ProviderID As Integer = 10
    Private Col_ProviderName As Integer = 11
    Private Col_PatientCode As Integer = 12
    Private Col_PatientName As Integer = 13

    'Private Col_ExamID As Integer = 2
    'Private Col_PatientID As Integer = 3
    'Private Col_VisitID As Integer = 4
    'Private Col_ExamName As Integer = 5
    'Private Col_ProviderName As Integer = 6
    'Private Col_IsFinish As Integer = 7

    'Private Col_UserName As Integer = 9
    'Private Col_MachineName As Integer = 10
    'Private Col_ProviderID As Integer = 11
    'Private Col_PatientCode As Integer = 12
    'Private Col_PatientName As Integer = 13

    Private Col_Count As Integer = 14

    Dim ProvideName As String
    Dim ProviderID As Int64
    Dim TemplateId As Int64
    Dim ObjWord As clsWordDocument
    Dim objCriteria As DocCriteria
    ' Private WithEvents oCurDoc As Wd.Document
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Private NotesFileName As String

    Dim PatientCode As String
    Dim PatientDOS As DateTime
    Dim PatientName As String
    Dim ExamName As String
    'Dim Con As SqlConnection
    Dim strPrintLogFile As String
    Dim blnErrorLog As Boolean
    Dim _PatientID As Long

    Private Col_SelectLog As Integer = 0
    Private Col_LogID As Integer = 1
    Private Col_LogUserName As Integer = 2
    Private Col_LogDate As Integer = 3
    Private Col_FileName As Integer = 4
    Private Col_Content As Integer = 5
    Private Col_LogCount As Integer = 6
    ' Private WithEvents wdPrint As AxDSOFramer.AxFramerControl
    Dim strSelectNotes As String
    Dim r As Wd.Range
    ' Private WithEvents oTempDoc As Wd.Document

    Dim blnExamNotes As Boolean = False

    Dim IsselectAll As Boolean = False

    Dim lstSelectedTemplateName As String = ""
    Dim lstSelectedTemplateValue As String = ""


    'variable added by dipak patil to solve the problem in bug #1541 : as multiple Notes are selected, it shows the Print
    'Dialog that many times. used as flag variable in clsPrintFaX.PrintDoc function
    Private _blnShowPrinterDialog As Boolean = True
    Public Shared IsOpen As Boolean = False


#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    'Private Shared frm As frmBatchReferrals

    Public Shared Function GetInstance(ByVal PatientID As Long) As frmBatchReferrals


        Try
            IsOpen = False
            ''If frm Is Nothing Then
            Dim frm As frmBatchReferrals = Nothing
            For Each f As Form In Application.OpenForms
                If f.Name = "frmBatchReferrals" Then
                    'If CType(f, frmRpt_PatientICD9CPT) = PatientID Then
                    IsOpen = True
                    frm = f
                    'End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmBatchReferrals(PatientID)
            End If
            Return frm
        Finally

        End Try

    End Function

#End Region



    '28052012 Bug No.27890 *On Activate Patient Referals will be Reloaded*
    Private Sub frmBatchReferrals_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadTemplate()
        If lstSelectedTemplateName <> "" AndAlso lstSelectedTemplateValue <> "" Then
            cmbTemplate.SelectedText = lstSelectedTemplateName
            cmbTemplate.SelectedValue = lstSelectedTemplateValue
        End If
    End Sub

    Private Sub frmBatchReferrals_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

        Try

            '01-Jul-14 Aniket: Resolved Bug #70126:
            If Not cmbTemplate.SelectedValue Is Nothing Then

                lstSelectedTemplateName = cmbTemplate.SelectedText.ToString()
                lstSelectedTemplateValue = cmbTemplate.SelectedValue.ToString()

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ex = Nothing
        End Try


    End Sub
    Private Sub frmBatchReferrals_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '10-Nov-14 Aniket: Resolving Bug #75643: gloEMR> Reports > Batch Referral Letters> View Log> It is not opening view log window
        btnViewlog.Visible = False

        gloC1FlexStyle.Style(C1ExamDetails)
        gloC1FlexStyle.Style(C1ShowLog)

        LoadTemplate()

        FillExamTypeCombo()
        FillProviderCombo()
        SetGridStyle()
        'cmbProvider.SelectedIndex = -1
        'ProviderID = cmbProvider.SelectedValue
        If (cmbExamtype.Items.Count > 0) Then
            cmbExamtype.SelectedIndex = 0
        End If

        SelectExam()
        SetLogGridstyle()

        If gblnExamSelection = 0 Then
            rbNone.Checked = True
        ElseIf gblnExamSelection = 1 Then
            rbNotes.Checked = True
        ElseIf gblnExamSelection = 2 Then
            rbSelect.Checked = True
        End If
        DTPTo.Select()
    End Sub

    Public Sub FillExamTypeCombo()
        cmbExamtype.Items.Add("Both")
        cmbExamtype.Items.Add("Finished")
        cmbExamtype.Items.Add("UnFinished")


    End Sub

    Public Sub FillProviderCombo()
        Dim oDB As New DataBaseLayer
        Dim strSelect As String = "select nProviderID,isnull(sFirstName,'') + ' ' + isnull(sLastName,'') as Name from Provider_MST"
        Dim dt As DataTable = Nothing

        'dt.Rows.Add(r)

        dt = oDB.GetDataTable_Query(strSelect)
        Dim r As DataRow
        r = dt.NewRow
        r.Item("Name") = "All"
        r.Item("nProviderID") = 0
        dt.Rows.InsertAt(r, 0)


        Dim strProviderName As String = ""

        cmbProvider.DataSource = dt
        cmbProvider.DisplayMember = dt.Columns("Name").ToString()
        cmbProvider.ValueMember = dt.Columns("nProviderID").ToString()
        oDB.Dispose()
        oDB = Nothing


    End Sub

    Public Sub SetGridStyle()
        With C1ExamDetails

            Dim _TotalWidth As Single = .Width - 5

            ' .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = Col_Count

            .Cols.Fixed = 0

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            'For i As Integer = 0 To C1ExamDetails.Cols.Count - 1
            '    C1ExamDetails.Cols(i).Visible = False
            'Next
            .Cols(Col_ExamID).Width = 0
            
            .Cols(Col_PatientID).Width = 0
            
            .Cols(Col_VisitID).Width = 0
            
            .Cols(Col_ExamName).Width = _TotalWidth * 0.3
            .Cols(Col_ExamName).AllowEditing = False
            ' .SetData(0, Col_ExamName, "Exam Name")
            .Cols(Col_ExamName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_ProviderName).Width = _TotalWidth * 0.2
            .Cols(Col_ProviderName).AllowEditing = False
            .Cols(Col_ProviderName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_IsFinish).Width = _TotalWidth * 0.08
            .Cols(Col_IsFinish).AllowEditing = False
            .Cols(Col_IsFinish).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_DOS).Width = _TotalWidth * 0.1
            .Cols(Col_DOS).AllowEditing = False
            '.SetData(0, Col_DOS, "Date of Service")
            .Cols(Col_DOS).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            '.Cols(Col_DOS).Visible = True
            .Cols(COl_Isopen).Width = 0
            ' .SetData(0, COl_Isopen, "Is Open")
            .Cols(COl_Isopen).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            ' .Cols(COl_Isopen).Visible = True
            .Cols(Col_UserName).Width = 0
            ' .SetData(0, Col_UserName, "UserName")
            .Cols(Col_UserName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_MachineName).Width = 0
            ' .SetData(0, Col_MachineName, "Machine Name")
            .Cols(Col_MachineName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_ProviderID).Width = 0
            '.SetData(0, Col_ProviderID, "ProviderID")
            ' .Cols(Col_ProviderID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_Select).Width = _TotalWidth * 0.04
            .Cols(Col_Select).DataType = GetType(Boolean)
            '.SetData(0, Col_Select, "Select")
            .Cols(Col_Select).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            ' .Cols(Col_Select).Visible = True
            ' .Cols(Col_PatientCode).Width = 0
            '.SetData(0, Col_PatientCode, "Patient Code")
            '.Cols(Col_PatientCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            '.Cols(Col_PatientName).Width = 0
            '.SetData(0, Col_PatientName, "Patient Name")
            '.Cols(Col_PatientName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            ''Sandip Darade 20100212
            .Cols(Col_PatientCode).Width = _TotalWidth * 0.08
            .Cols(Col_PatientCode).AllowEditing = False
            '.SetData(0, Col_PatientCode, "Patient Code")
            .Cols(Col_PatientCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            .Cols(Col_PatientName).Width = _TotalWidth * 0.2
            .Cols(Col_PatientName).AllowEditing = False
            '.SetData(0, Col_PatientName, "Patient Name")
            .Cols(Col_PatientName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            ' .Cols(Col_PatientNotes).Width = 0
            .Cols(Col_UserName).Width = 0
            .Cols(Col_MachineName).Width = 0
            .Cols(Col_ProviderID).Width = 0

        End With
    End Sub

    Public Sub SetLogGridstyle()
        With C1ShowLog
            Dim _TotalWidth As Single = .Width - 5

            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = Col_LogCount

            .Cols.Fixed = 0


            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn

            .Cols(Col_SelectLog).Width = _TotalWidth * 0.05
            .SetData(0, Col_SelectLog, "Select")
            .Cols(Col_SelectLog).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


            .Cols(Col_LogID).Width = 0
            .SetData(0, Col_LogID, "Log ID")
            .Cols(Col_LogID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_LogUserName).Width = _TotalWidth * 0.28
            .SetData(0, Col_LogUserName, "User Name")
            .Cols(Col_LogUserName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_LogDate).Width = _TotalWidth * 0.25
            .SetData(0, Col_LogDate, "Log Date")
            .Cols(Col_LogDate).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_FileName).Width = _TotalWidth * 0.25
            .SetData(0, Col_FileName, "File Name")
            .Cols(Col_FileName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            .Cols(Col_Content).Width = 0
            .SetData(0, Col_Content, "Content")
            .Cols(Col_Content).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
        End With
    End Sub

    Public Sub SelectExam()
        Try
           
            Dim oDB As New DataBaseLayer
            Dim strSelectQry As String = ""
            Dim oParamater As DBParameter
           


            oDB = New DataBaseLayer

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ExamType"
            oParamater.Value = cmbExamtype.Items(cmbExamtype.SelectedIndex).ToString
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dtDOSTo"
            oParamater.Value = DTPTo.Text
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dtDOSFrom"
            oParamater.Value = DTPFrom.Text
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

           


            If cmbProvider.SelectedValue = 0 Then  '  .Items(cmbProvider.SelectedIndex).ToString = "All" Then

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ProviderID"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsAllProvider"
                oParamater.Value = 1
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            Else
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ProviderID"
                oParamater.Value = ProviderID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsAllProvider"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If
            Dim dt As DataTable = Nothing
            dt = oDB.GetDataTable("gsp_SelectLogRecord")

            '  C1ExamDetails.DataSource = Nothing

            C1ExamDetails.DataSource = dt
            SetGridStyle()
            
            oDB.Dispose()
            oDB = Nothing
            'dt.Dispose()
            'dt = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    

    Private Sub cmbProvider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectionChangeCommitted
        ProvideName = cmbProvider.SelectedText
        ProviderID = cmbProvider.SelectedValue
        SelectExam()
    End Sub

   

    Private Sub cmbExamtype_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExamtype.SelectionChangeCommitted
        SelectExam()
    End Sub

    Private Sub LoadTemplate()
        Try
            Dim dt As DataTable
            Dim objReferralsDBLayer As ClsReferralsDBLayer
            objReferralsDBLayer = New ClsReferralsDBLayer
            dt = objReferralsDBLayer.FillControls("T", _PatientID)

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    cmbTemplate.DataSource = dt
                    cmbTemplate.DisplayMember = dt.Columns(1).ColumnName
                    cmbTemplate.ValueMember = dt.Columns(0).ColumnName
                    cmbTemplate.SelectedIndex = 0
                    TemplateId = CType(dt.Rows(0).Item(0), System.Int64)
                End If
            End If
            dt = Nothing
            objReferralsDBLayer.Dispose()
            objReferralsDBLayer = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Load, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Public Sub PrintAll()

        Dim IsReferalletter As Boolean = False
        Dim blnPrintCancel As Boolean = False

        Try

            bnlPrintInProgress = True

            strPrintLogFile = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".txt", "MMddyyyyHHmmssffff") 'gloSettings.FolderSettings.AppTempFolderPath & Format(Date.Now, "MMddyyyyHHmmssffff") & ".txt"
            For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
                If C1ExamDetails.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                    PrgBarPrintFax.Value = PrgBarPrintFax.Value + PrgBarPrintFax.Step
                    PatientCode = C1ExamDetails.GetData(i, Col_PatientCode)
                    PatientDOS = C1ExamDetails.GetData(i, Col_DOS)
                    PatientName = C1ExamDetails.GetData(i, Col_PatientName)
                    ExamName = C1ExamDetails.GetData(i, Col_ExamName)
                    Dim objReferralsDBLayer As ClsReferralsDBLayer
                    objReferralsDBLayer = New ClsReferralsDBLayer
                    Dim m_PatientId As Int64 = C1ExamDetails.GetData(i, Col_PatientID)
                    Dim m_VisitID As Int64 = C1ExamDetails.GetData(i, Col_VisitID)
                    Dim m_ExamId As Int64 = C1ExamDetails.GetData(i, Col_ExamID)

                    ''check if Referrals exists against given VisitId
                    If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamId, m_PatientId) Then
                        Dim dtVisitRef As DataTable = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, m_PatientId, m_ExamId)
                        blnPrintCancel = PrintRefContents(dtVisitRef, m_PatientId, m_VisitID, m_ExamId, True)
                        IsReferalletter = True
                        dtVisitRef.Dispose()
                        dtVisitRef = Nothing
                    Else
                        'if Referral Details do not exist for that visitid,
                        'Populate Referrals Details from Patient_Dtl Table
                        Dim dtPatRef As DataTable = objReferralsDBLayer.FillControls("R", m_PatientId)
                        blnPrintCancel = PrintRefContents(dtPatRef, m_PatientId, m_VisitID, m_ExamId, False)
                        dtPatRef.Dispose()
                        dtPatRef = Nothing
                    End If

                    C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    objReferralsDBLayer.Dispose()
                    objReferralsDBLayer = Nothing

                End If
                If (blnPrintCancel = True) Then
                    Exit For
                End If
            Next
            PrgBarPrintFax.Visible = False
            If IsReferalletter = False Then
                MessageBox.Show("No referral letter exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If blnErrorLog Then
                AddPrintLogInDB()
                MessageBox.Show("Processing of batch referral letters is complete. Please check the log for any errors.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            ElseIf (blnPrintCancel = False) Then
                MessageBox.Show("Processing of batch referral letters is successfully completed", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Load, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Referrals", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing

        Finally
            bnlPrintInProgress = False
        End Try

    End Sub

    Private Sub UpdatePrintLog(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(strPrintLogFile, True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function PrintRefContents(ByVal objTable As DataTable, ByVal m_PatientId As Int64, ByVal m_VisitId As Int64, ByVal m_ExamId As Int64, ByVal blnRef As Boolean) As Boolean

        If Not objTable Is Nothing Then
            If objTable.Rows.Count > 0 Then
                Dim strNotesFile As String
                'If Not IsNothing(strNotesFile) Then

                'Dim oPrint As New clsPrintFAX
                'variable added by dipak 20090825 for track printdialogbox's cancel button click
                Dim blnPrintCancel As Boolean = False
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                strNotesFile = SelectNotesFile(m_ExamId, myLoadWord)

                Try
                    For j As Int32 = 0 To objTable.Rows.Count - 1
                        Dim strRefName As String
                        Dim m_ContactId As Int64

                        If blnRef Then
                            strRefName = objTable.Rows(j)(2).ToString
                            If Not IsDBNull(objTable.Rows(j)(2)) Then
                                m_ContactId = CType(objTable.Rows(j)(2), Int64)
                            End If

                        Else
                            strRefName = objTable.Rows(j)(1).ToString
                            If Not IsDBNull(objTable.Rows(j)(0)) Then
                                m_ContactId = CType(objTable.Rows(j)(0), Int64)
                            End If

                        End If
                        Dim strFileName As String
                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Template
                        objCriteria.PrimaryID = TemplateId
                        ObjWord.DocumentCriteria = objCriteria
                        strFileName = ObjWord.RetrieveDocumentFile()
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        If Not IsNothing(strFileName) Then

                            If strFileName <> "" Then
                                ObjWord = New clsWordDocument
                                objCriteria = New DocCriteria
                                objCriteria.DocCategory = enumDocCategory.Referrals
                                objCriteria.PatientID = m_PatientId
                                objCriteria.VisitID = m_VisitId
                                objCriteria.PrimaryID = m_ContactId
                                ObjWord.DocumentCriteria = objCriteria



                                Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                                ObjWord.CurDocument = oCurDoc
                                ObjWord.GetFormFieldData(enumDocType.None)
                                oCurDoc = ObjWord.CurDocument
                                objCriteria.Dispose()
                                objCriteria = Nothing
                                ObjWord = Nothing
                                If strNotesFile <> "" Then
                                    If File.Exists(strNotesFile) Then
                                        UpdateVoiceLog("Inserting Exam Notes in PrintRefContents")
                                        oCurDoc.ActiveWindow.SetFocus()
                                        oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                        oCurDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                        oCurDoc.Application.Selection.InsertFile(strNotesFile)
                                        UpdateVoiceLog("Exam Notes Inserted in PrintRefContents")

                                    End If
                                End If

                                'ObjWord = New clsWordDocument
                                'ObjWord.CurDocument = oCurDoc
                                'ObjWord.CleanupDoc()
                                'oCurDoc = ObjWord.CurDocument
                                'ObjWord = Nothing
                                ''SetWordObject()
                                'oCurDoc.ActiveWindow.PrintOut()
                                Dim oPrint As New clsPrintFAX
                                If (oPrint.PrintDoc(oCurDoc, m_PatientId, _blnShowPrinterDialog) = DialogResult.Cancel) Then
                                    blnPrintCancel = True
                                End If
                                oPrint.Dispose()
                                oPrint = Nothing
                                _blnShowPrinterDialog = False
                                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, "Referral Printed", gloAuditTrail.ActivityOutCome.Success)
                                ''Added Rahul P on 20101011
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, "Referral Printed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                ''
                                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Referral Printed.", gstrLoginName, gstrClientMachineName, m_PatientId)
                                'Me.Controls.Remove(wdTemp)
                                'wdTemp.Close()
                                'wdTemp.Dispose()
                                'oCurDoc = Nothing
                                myLoadWord.CloseWordOnly(oCurDoc)
                            End If
                        End If
                        'code added by dipak 20050825 to exit from for loop when user click on cancel of print dialogbox
                        If (blnPrintCancel = True) Then
                            Exit For
                        End If

                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, ex.ToString & "PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Errors while Prepare and Print Referrals for Patient.", gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try


                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                PrintRefContents = blnPrintCancel
            Else
                ''log the record that there are no referrals available for this patient and exam
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, "PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Referrals are not available for Patient and Exam ", gloAuditTrail.ActivityOutCome.Success)
                'UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Referrals are not available for Patient and Exam ")
                'blnErrorLog = True
                Return Nothing
            End If
        Else


            UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Referrals are not available for Patient and Exam ")
            blnErrorLog = True
            Return Nothing
        End If
        'End If
    End Function

    Private Sub AddPrintLogInDB()

        '''''Convert File into Image to store it in database

        Dim oDB As New DataBaseLayer

        Try

            Dim conString As String
            conString = GetConnectionString()
            Dim Con As SqlConnection = New SqlConnection(conString)
            Dim cmd As New SqlCommand("gsp_InsertLog", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            Con.Open()

            sqlParam = cmd.Parameters.Add("@nLogID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID(Now)

            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName

            sqlParam = cmd.Parameters.Add("@Date", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Format(Now, "MM/dd/yyyy")

            sqlParam = cmd.Parameters.Add("@FileName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Path.GetFileName(strPrintLogFile)


            ObjWord = New clsWordDocument
            sqlParam = cmd.Parameters.Add("@FileContent", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ObjWord.ConvertFiletoBinary(strPrintLogFile & "")
            ObjWord = Nothing

            cmd.ExecuteNonQuery()
            'cmd = Nothing
            'Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Con IsNot Nothing Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

            sqlParam = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Print, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ex = Nothing
        End Try

    End Sub

    Private Sub FaxAll()

        'By Shweta 20090829
        'If CheckWordForException() = False Then
        '    Exit Sub
        'End If
        'End Shweta


        strPrintLogFile = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".txt", "MMddyyyyHHmmssffff") 'gloSettings.FolderSettings.AppTempFolderPath & Format(Date.Now, "MMddyyyyHHmmssffff") & ".txt"
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1

            If C1ExamDetails.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                Application.DoEvents()
                PrgBarPrintFax.Value = PrgBarPrintFax.Value + PrgBarPrintFax.Step
                Application.DoEvents()
                PatientCode = C1ExamDetails.GetData(i, Col_PatientCode)
                PatientDOS = C1ExamDetails.GetData(i, Col_DOS)
                PatientName = C1ExamDetails.GetData(i, Col_PatientName)
                ExamName = C1ExamDetails.GetData(i, Col_ExamName)
                Dim objReferralsDBLayer As ClsReferralsDBLayer
                objReferralsDBLayer = New ClsReferralsDBLayer
                Dim m_PatientId As Int64 = C1ExamDetails.GetData(i, Col_PatientID)
                Dim m_VisitID As Int64 = C1ExamDetails.GetData(i, Col_VisitID)
                Dim m_ExamId As Int64 = C1ExamDetails.GetData(i, Col_ExamID)
                Dim blnExamFinish As Boolean
                If C1ExamDetails.GetData(i, Col_IsFinish) = "Yes" Then
                    blnExamFinish = True
                Else
                    blnExamFinish = False
                End If

                ''check if Referrals exists against given VisitId
                If Not objReferralsDBLayer.CheckReferral(m_VisitID, m_ExamId, m_PatientId) Then
                    Dim dtVisitRef As DataTable = objReferralsDBLayer.FetchReferralsForUpdate(m_VisitID, m_PatientId, m_ExamId)
                    FaxRefContents(dtVisitRef, m_PatientId, m_VisitID, m_ExamId, blnExamFinish, True)
                    dtVisitRef.Dispose()
                    dtVisitRef = Nothing
                Else
                    'if Referral Details do not exist for that visitid,
                    'Populate Referrals Details from Patient_Dtl Table
                    Dim dtPatRef As DataTable = objReferralsDBLayer.FillControls("R", m_PatientId)
                    FaxRefContents(dtPatRef, m_PatientId, m_VisitID, m_ExamId, blnExamFinish, False)
                    dtPatRef.Dispose()
                    dtPatRef = Nothing
                End If

                C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                objReferralsDBLayer.Dispose()
                objReferralsDBLayer = Nothing
            End If

        Next
        PrgBarPrintFax.Visible = False
        If blnErrorLog Then
            If File.Exists(strPrintLogFile) Then
                AddPrintLogInDB()
            End If
            MessageBox.Show("Processing of batch referral letters is complete. Please check the log for any errors.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        Else
            MessageBox.Show("Processing of batch referral letters is successfully completed", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        End If

    End Sub

    Public Sub FaxRefContents(ByVal objTable As DataTable, ByVal m_PatientId As Int64, ByVal m_VisitId As Int64, ByVal m_ExamId As Int64, ByVal blnFinish As Boolean, ByVal blnRef As Boolean)

        UpdateVoiceLog("In FaxRefContents method")
        'Dim mstream As ADODB.Stream
        Dim strFileName As String

        Dim blnFAXPrinterHasToSet As Boolean = True
        Dim blnDSODefaultPrinterHasToSet As Boolean = True

        If Not objTable Is Nothing Then

            If objTable.Rows.Count > 0 Then
                '00000093 : batch faxing of referral letters sending 2 copies if the referring provider is the same as the PCP.
                'take only distinct records on the basis of nPCPid and PCPName.
                If Not blnRef Then
                    objTable = objTable.DefaultView.ToTable(True, "nPCPid", "PCPName")
                End If

                Dim strNotesFile As String
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                strNotesFile = SelectNotesFile(m_ExamId, myLoadWord)

                Try
                    For j As Int32 = 0 To objTable.Rows.Count - 1

                        '' Clear the FaxCollection Object
                        ' gstrfaxCollection = New Collection()
                        If (IsNothing(gstrfaxCollection) = False) Then
                            gstrfaxCollection.Clear()
                        Else
                            gstrfaxCollection = New Collection
                        End If

                        Dim strRefName As String
                        Dim m_ContactId As Int64

                        If blnRef Then
                            strRefName = objTable.Rows(j)("ContactName").ToString
                            If Not IsDBNull(objTable.Rows(j)("ReferralToFrom")) Then
                                'm_ContactId = CType(objTable.Rows(j)(2), Int64)
                                m_ContactId = CType(objTable.Rows(j)("ReferralToFrom"), Int64)
                            End If

                        Else
                            strRefName = objTable.Rows(j)("PCPName").ToString
                            If Not IsDBNull(objTable.Rows(j)("nPCPID")) Then
                                m_ContactId = CType(objTable.Rows(j)("nPCPID"), Int64)
                            End If

                        End If
                        ''GLO2011-0012371 : End : Commented : Using the Column number it will throw error Use Column name instead of number

                        UpdateVoiceLog("Sending Referral Letter - " & j + 1)

                        ObjWord = New clsWordDocument
                        objCriteria = New DocCriteria
                        objCriteria.DocCategory = enumDocCategory.Template
                        objCriteria.PrimaryID = TemplateId
                        ObjWord.DocumentCriteria = objCriteria
                        UpdateVoiceLog("Retrieving Referral Letter Contents from Database & Save it to Physical File")
                        strFileName = ObjWord.RetrieveDocumentFile()
                        objCriteria.Dispose()
                        objCriteria = Nothing
                        ObjWord = Nothing
                        If Not IsNothing(strFileName) Then
                            If strFileName <> "" Then
                                ObjWord = New clsWordDocument
                                objCriteria = New DocCriteria
                                objCriteria.DocCategory = enumDocCategory.Referrals
                                objCriteria.PatientID = m_PatientId
                                objCriteria.VisitID = m_VisitId
                                objCriteria.PrimaryID = m_ContactId
                                ObjWord.DocumentCriteria = objCriteria



                                Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

                                ObjWord.CurDocument = oCurDoc
                                ObjWord.GetFormFieldData(enumDocType.None)
                                oCurDoc = ObjWord.CurDocument
                                ObjWord = Nothing
                                objCriteria.Dispose()
                                objCriteria = Nothing

                                'Commented by Shweta 20100201
                                '''''''Against the bug id:5260 '''''''
                                'Check the FAX Cover Page is enabled or not.
                                'If the FAX Cover Page is enabled then Delete the Page Header from Exam
                                'If gblnFAXCoverPage Then
                                '    'To Delete Header
                                '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Deleting Referrals Page Header", gloAuditTrail.ActivityOutCome.Success)
                                '    'UpdateVoiceLog("Deleting Referrals Page Header")
                                '    Try

                                '        If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
                                '            oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
                                '        End If
                                '        oCurDoc.Activate()
                                '        oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader

                                '        If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
                                '            oCurDoc.Application.Selection.HeaderFooter.Range.Select()
                                '            oCurDoc.Application.Selection.HeaderFooter.Range.Delete()
                                '            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Patient Referrals Page Header deleted", gloAuditTrail.ActivityOutCome.Success)
                                '            'UpdateVoiceLog("Patient Referrals Page Header deleted")

                                '        End If

                                '    Catch ex As Exception
                                '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, "Error Deleting Patient Referrals Page Header - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                '        'UpdateVoiceLog("Error Deleting Patient Referrals Page Header - " & ex.ToString)
                                '    Finally
                                '        oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
                                '    End Try

                                'End If
                                'End Commenting

                                If strNotesFile <> "" Then
                                    If File.Exists(strNotesFile) Then
                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Inserting Exam Notes", gloAuditTrail.ActivityOutCome.Success)
                                        'UpdateVoiceLog("Inserting Exam Notes")
                                        oCurDoc.ActiveWindow.SetFocus()
                                        oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                        'oCurDoc.Application.Selection.InsertBreak(Type:=Wd.WdBreakType.wdPageBreak)
                                        oCurDoc.Application.Selection.InsertFile(strNotesFile)
                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Exam Notes Inserted", gloAuditTrail.ActivityOutCome.Success)
                                        'UpdateVoiceLog("Exam Notes Inserted")

                                    End If
                                End If
                                Dim myWordFileName As String = myLoadWord.SaveCurrentWord(oCurDoc, gloSettings.FolderSettings.AppTempFolderPath)
                                myLoadWord.CloseWordOnly(oCurDoc)
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Calling RetrieveFAXDetails method to retrieve FAX Details", gloAuditTrail.ActivityOutCome.Success)
                                'UpdateVoiceLog("Calling RetrieveFAXDetails method to retrieve FAX Details")

                                Dim blnFirstReferral As Boolean
                                If j = 0 Then
                                    blnFirstReferral = True
                                Else
                                    blnFirstReferral = False
                                End If

                                Dim strResult As String
                                strResult = BatchReferralsFAXDetails(strRefName, "", cmbTemplate.SelectedItem(1), m_ContactId, blnFirstReferral)

                                '' Add the Fax Contact details to the collection object
                                Dim node As New myTreeNode
                                node.FaxTo = gstrFAXContactPersonFAXNo
                                node.FaxName = gstrFAXContactPerson
                                node.FaxCoverPage = ""
                                gstrfaxCollection.Add(node)

                                If strResult = "Success" Then
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "FAX Details retrieved", gloAuditTrail.ActivityOutCome.Success)
                                    'UpdateVoiceLog("FAX Details retrieved")
                                    If j >= 1 Then
                                        blnFAXPrinterHasToSet = False
                                    End If
                                    If j >= objTable.Rows.Count - 1 Then
                                        blnDSODefaultPrinterHasToSet = True
                                    Else
                                        blnDSODefaultPrinterHasToSet = False
                                    End If

                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Creating object of clsPrintFAX class", gloAuditTrail.ActivityOutCome.Success)
                                    'UpdateVoiceLog("Creating object of clsPrintFAX class")
                                    Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
                                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Calling FAX Document method", gloAuditTrail.ActivityOutCome.Success)
                                    'UpdateVoiceLog("Calling FAX Document method")
                                    If objPrintFAX.FAXDocument(myLoadWord, myWordFileName, CStr(m_PatientId), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, cmbTemplate.SelectedItem(1), clsPrintFAX.enmFAXType.ReferralLetter, Not blnFinish, blnFAXPrinterHasToSet, blnDSODefaultPrinterHasToSet) = False Then
                                        'TIFF File has not been created
                                        If Trim(objPrintFAX.ErrorMessage) <> "" Then
                                            '' MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            '' log the objPrintFAX.ErrorMessage
                                            UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - " & objPrintFAX.ErrorMessage)
                                            blnErrorLog = True
                                        End If
                                    Else
                                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, "Document Faxed", gloAuditTrail.ActivityOutCome.Success)
                                        'UpdateVoiceLog("Document Faxed")
                                    End If
                                    'UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - No Referrals assaociated for the patient")
                                    'blnErrorLog = True
                                    objPrintFAX.Dispose()
                                    objPrintFAX = Nothing

                                    '28052012 Bug No.27890 * flag Set To False*
                                    If blnErrorLog Then
                                        blnErrorLog = False
                                    End If
                                Else
                                    ''log the Error reason from strResult variable
                                    UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - " & strResult)
                                    blnErrorLog = True
                                End If
                                'Me.Controls.Remove(wdTemp)
                                'wdTemp.Close()
                                'wdTemp.Dispose()
                                'oCurDoc = Nothing

                            Else
                                ''Log Error while creating Referral letter Template 
                                UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Error while accessing Referral Letter Template")
                                blnErrorLog = True
                            End If
                        Else
                            ''Log Error while creating Referral letter Template 
                            UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Error while accessing Referral Letter Template")
                            blnErrorLog = True
                        End If

                    Next
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Fax, ex.ToString & "PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - Error while accessing Referral Letter Template.", gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try


                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing

            Else
                ''Log that there are no referrals assaociated for that patient
                UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - No Referrals associated for the patient")
                blnErrorLog = True

            End If
        Else
            UpdatePrintLog("PatientCode: " & PatientCode & vbTab & "Name: " & PatientName & vbTab & "ExamName: " & ExamName & vbTab & "DOS: " & PatientDOS & vbTab & " - No Referrals associated for the patient")
            blnErrorLog = True
        End If

    End Sub

    Private Sub cmbTemplate_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplate.SelectionChangeCommitted
        TemplateId = cmbTemplate.SelectedValue
    End Sub

    Private Sub btnViewlog_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles btnViewlog.Click, tsb_Viewlogs.Click


        Try
            If pnlgrid.Visible = False Then
                pnlgrid.Visible = True

                'btnViewlog.Text = "&Hide Log"
                tsb_Viewlogs.Visible = False
                tsb_Hidelogs.Visible = True

                DTPTOLog.Text = Format(Now, "MM/dd/yyyy")
                FillLog()
                DTPFromLog.Select()
            Else
                'btnViewlog.Text = "&View Log"
                tsb_Hidelogs.Visible = False
                tsb_Viewlogs.Visible = True

                pnlgrid.Visible = False

            End If
            ToolStripButton6.Tag = "Selectall"
            ToolStripButton6.Visible = True
            ToolStripButton6.Text = "&Select All"
            ToolStripButton7.Visible = False
            ToolStripButton7.Text = "&Clear All"

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    Public Sub FillLog()
        Dim oDB As New DataBaseLayer
        Dim dt As DataTable
        Dim strSelectQry As String = "Select nLogID,UserName,Date,sFileName,FileContent  from PrintFaxLog Where Date>='" & DTPTOLog.Text & "' and Date<='" & DTPFromLog.Text & "'"
        dt = oDB.GetDataTable_Query(strSelectQry)
        C1ShowLog.Rows.Count = 1
        If (IsNothing(dt) = False) Then


            For i As Integer = 0 To dt.Rows.Count - 1
                With C1ShowLog
                    .Rows.Add()
                    .SetData(i + 1, Col_LogID, dt.Rows(i)("nLogID"))
                    .SetData(i + 1, Col_LogUserName, dt.Rows(i)("UserName"))
                    .SetData(i + 1, Col_LogDate, dt.Rows(i)("Date"))
                    .SetData(i + 1, Col_FileName, dt.Rows(i)("sFileName"))
                    .SetData(i + 1, Col_Content, dt.Rows(i)("FileContent"))

                    Dim rgSelect As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, Col_SelectLog, .Rows.Count - 1, Col_SelectLog)

                    rgSelect.StyleNew.DataType = GetType(Boolean)
                    rgSelect.StyleNew.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    rgSelect.StyleNew.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                    .SetCellCheck(i + 1, Col_SelectLog, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If

        C1ShowLog.Rows(0).AllowEditing = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub C1ShowLog_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ShowLog.MouseDoubleClick

        If C1ShowLog.RowSel <= 0 Then
            Exit Sub
        End If
        If C1ShowLog.Row <= 0 Then
            Exit Sub
        End If
        With C1ShowLog
            Dim Content As Object = .GetData(.Row, Col_Content)
            ObjWord = New clsWordDocument
            Dim NewFileName As String = ObjWord.GenerateFile(Content, gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".txt", "MMddyyyyHHmmssffff")) ' gloSettings.FolderSettings.AppTempFolderPath & Format(Now, "MMddyyyyHHmmssffff") & ".txt")
            ObjWord = Nothing
            System.Diagnostics.Process.Start(NewFileName)
        End With
    End Sub

    Private Sub DTPTOLog_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPTOLog.TextChanged
        FillLog()
    End Sub

    Private Sub DTPFromLog_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPFromLog.TextChanged
        FillLog()
    End Sub

    Private Sub btnLogSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogSelect.Click, ToolStripButton6.Click
        'For i As Integer = 1 To C1ShowLog.Rows.Count - 1
        '    C1ShowLog.SetCellCheck(i, Col_SelectLog, C1.Win.C1FlexGrid.CheckEnum.Checked)
        'Next
    End Sub

    Private Sub btnLogCLear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogCLear.Click
        'For i As Integer = 1 To C1ShowLog.Rows.Count - 1
        '    C1ShowLog.SetCellCheck(i, Col_SelectLog, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        'Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click, ToolStripButton8.Click
        Dim oDB As New DataBaseLayer
        With C1ShowLog
            For i As Integer = .Rows.Count - 1 To 1 Step -1
                If .GetCellCheck(i, Col_SelectLog) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    Dim LogID As Int64 = .GetData(i, Col_LogID)
                    Dim strDeleteQry As String = "Delete From PrintFaxLog Where nLogID = '" & LogID & "'"
                    If oDB.Delete_Query(strDeleteQry) = True Then
                        .Rows.Remove(i)
                    End If
                End If
            Next
        End With
        oDB.Dispose()
        oDB = Nothing
        FillLog()
    End Sub

    Private Function SelectNotesFile(ByVal m_ExamId As Int64, ByRef myLoadWord As gloWord.LoadAndCloseWord) As String

        If rbSelect.Checked Or rbNotes.Checked Then
            Dim strTempFile As String
            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            objCriteria.PrimaryID = m_ExamId
            ObjWord.DocumentCriteria = objCriteria
            strTempFile = ObjWord.RetrieveDocumentFile()
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            If (IsNothing(strTempFile) = False) Then
                If strTempFile <> "" Then
                    If File.Exists(strTempFile) Then
                        If rbNotes.Checked Then
                            Return strTempFile
                        ElseIf rbSelect.Checked Then
                            Return CreateSelectedNotes(strTempFile, myLoadWord)
                        End If
                    Else
                        Return ""

                    End If
                Else
                    Return ""

                End If
            Else
                Return ""
            End If
        Else
            Return ""

        End If
        Return ""
    End Function

    Private Function CreateSelectedNotes(ByVal strFilename As String, ByRef myLoadWord As gloWord.LoadAndCloseWord) As String


        Dim strFile As String = ""

        'Dim wdTemp As Wd.Application
        'wdTemp = New Wd.Application
        'Dim wdPrint1 As New Wd.Application
        'Dim objmissing As Object = System.Reflection.Missing.Value
        ''Not using framer control instead using word application
        ' wdPrint = New AxDSOFramer.AxFramerControl

        ' Me.Controls.Add(wdPrint)
        'wdPrint.CreateNew("Word.Document")
        'oTempDoc = wdPrint.ActiveDocument

        'Dim oTempDoc As Wd.Document
        'oTempDoc = wdPrint1.Documents.Add(objmissing, objmissing, objmissing)

        'Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        Try
            Dim oCurDoc As Wd.Document = myLoadWord.LoadWordApplication(strFilename)

            Try
                ' wdTemp = New AxDSOFramer.AxFramerControl

                ' Me.Controls.Add(wdTemp)
                'oCurDoc = wdTemp.Documents.Open(strFilename)
                Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(Nothing, False)
                'wdTemp.Open(strFilename)
                'oCurDoc = wdTemp.ActiveDocument

                '' oWordApp = oTempDoc.Application
                ' oCurDoc.ActiveWindow.SetFocus()
                Try
                    oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Bookmarks.DefaultSorting = Wd.WdBookmarkSortBy.wdSortByLocation
                    Dim setClipBoardSemaphore As Boolean = False
                    Dim gotClip As Boolean = False
                    Dim strBM1End As Integer = 0
                    Dim strBM2 As Wd.Bookmark = Nothing

                    Dim blnFlag As Boolean = False

                    For i As Int32 = 1 To oCurDoc.Range.Bookmarks.Count

                        strBM2 = oCurDoc.Range.Bookmarks.Item(i) '.Name
                        'If InStr(strBM2, "BM") Then
                        If strBM2.Name.StartsWith("BM") Then
                            If Not blnFlag Then
                                blnFlag = True
                                strBM1End = strBM2.End
                                'strBM2 = ""
                            Else 'If (strBM1 <> "") Then
                                blnFlag = False
                                If (setClipBoardSemaphore = False) Then
                                    Try
                                        Dim strEx As String = ""
                                        gotClip = Global.gloWord.gloWord.GetClipBoardWithRetry(5, strEx)
                                    Catch ex As Exception

                                    End Try
                                    setClipBoardSemaphore = True
                                End If

                                Call SelectBetweenBookmarks(oTempDoc, oCurDoc, strBM1End, strBM2.Start)
                                oCurDoc.ActiveWindow.SetFocus()
                                'strBM1 = ""
                                'strBM2 = ""
                            End If
                        End If
                    Next
                    'strBM1 = Nothing
                    strBM2 = Nothing
                    If ((setClipBoardSemaphore = True) AndAlso (gotClip = True)) Then
                        Try
                            Global.gloWord.gloWord.SetClipboardData()
                        Catch ex As Exception

                        End Try

                    End If
                    oTempDoc.ActiveWindow.SetFocus()
                    oTempDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)

                    If oTempDoc.Content.Text.Trim() = "" Then
                        strFile = ""
                    Else
                        strFile = ExamNewDocumentName
                        'wdPrint.Save(strFile, True, "", "")
                        oTempDoc.SaveAs(strFile, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                myLoadWord.CloseWordOnly(oTempDoc)

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            myLoadWord.CloseWordOnly(oCurDoc)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        'Try
        '    Me.Activate()
        'Catch ex As Exception

        'End Try
        'myLoadWord.CloseApplicationOnly()

        'myLoadWord = Nothing
        'If Not wdTemp Is Nothing Then
        '    wdTemp.Quit()

        '    If Not IsNothing(wdTemp) Then
        '        System.Runtime.InteropServices.Marshal.ReleaseComObject(wdTemp) '  'SLR: marshall free
        '    End If
        '    wdTemp = Nothing
        'End If
        'oCurDoc = Nothing

        'If Not wdPrint1 Is Nothing Then
        '    wdPrint1.Quit()

        '    If Not IsNothing(wdPrint1) Then
        '        System.Runtime.InteropServices.Marshal.ReleaseComObject(wdPrint1) '  'SLR: marshall free
        '    End If
        '    wdPrint1 = Nothing
        'End If
        'If Not IsNothing(oTempDoc) Then
        '    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTempDoc) '  'SLR: marshall free
        '    oTempDoc = Nothing
        'End If


        Return strFile
    End Function

    Private Sub SelectBetweenBookmarks(ByRef oTempDoc As Wd.Document, ByRef oCurDoc As Wd.Document, ByVal strBM1 As Integer, ByVal strBM2 As Integer)
        Try
            'Global.gloWord.gloWord.GetClipboardData()
            'r = oCurDoc.Range(oCurDoc.Bookmarks(strBM1).End, oCurDoc.Bookmarks(strBM2).Start)
            r = oCurDoc.Range(strBM1, strBM2)
            If (IsNothing(r) = False) Then


                r.Select()
                r.Copy()
                oTempDoc.ActiveWindow.SetFocus()
                oTempDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                Try
                    oTempDoc.Application.Selection.TypeText(vbNewLine)
                    oTempDoc.Application.Selection.Range.Paste()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                oCurDoc.ActiveWindow.SetFocus()

                r = Nothing
            End If
            'Clipboard.Clear()
            'Global.gloWord.gloWord.SetClipboardData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    Private Sub SelectAllTemp()
        'ToolStripButton1.Image.Dispose()
        ToolStripButton1.Text = "&Clear All"
        ToolStripButton1.Tag = "Clear"
        ToolStripButton1.Image = Global.gloEMR.My.Resources.Clear_All
        ToolStripButton1.ToolTipText = "Clear All"
        IsselectAll = True
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
        Next
    End Sub

    Private Sub ClearAllTemp()

        ToolStripButton1.Text = "&Select All"
        ToolStripButton1.Tag = "Selectall"
        ToolStripButton1.Image = Global.gloEMR.My.Resources.Select_All1
        ToolStripButton1.ToolTipText = "Select All"
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        Next
    End Sub

    Private Sub PrintTemp()
        Dim IsPrintExamcheck As Boolean = False
        PrgBarPrintFax.Value = 1
        PrgBarPrintFax.Minimum = 1
        PrgBarPrintFax.Maximum = C1ExamDetails.Rows.Count
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            If C1ExamDetails.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                IsPrintExamcheck = True
                Exit For
            End If
        Next
        If IsPrintExamcheck = True Then

            'btnPrint.Visible = False
            'btnFax.Visible = False
            'btnClose.Visible = False
            ' btnSelectall.Visible = False
            'btnClearall.Visible = False
            'pnlProgress.Visible = True
            'pnlProgress.BringToFront()
            ''Panel7.SendToBack()
            'PrgBarPrintFax.Visible = True
            'Panel7.BringToFront()

            pnlProgress.Visible = True
            PrgBarPrintFax.Visible = True
            Panel7.BringToFront()
            PrintAll()
            ToolStripButton1.Text = "&Select All"
            ToolStripButton1.Tag = "Selectall"
            ToolStripButton1.Image = Global.gloEMR.My.Resources.Select_All
            ToolStripButton1.ToolTipText = "Select All"
            'btnPrint.Visible = True
            ' btnFax.Visible = True
            ' btnClose.Visible = True
            ' btnSelectall.Visible = True
            ' btnClearall.Visible = True
            PrgBarPrintFax.Visible = False
            pnlProgress.Visible = False

            'For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            '    C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            'Next

            _blnShowPrinterDialog = True
        Else
            MessageBox.Show("Select exam to Print", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub FaxTemp()
        Dim IsFaxExamCheck As Boolean = False
        PrgBarPrintFax.Value = 1
        PrgBarPrintFax.Minimum = 1
        PrgBarPrintFax.Maximum = C1ExamDetails.Rows.Count
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            If C1ExamDetails.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                IsFaxExamCheck = True
                Exit For
            End If
        Next
        If IsFaxExamCheck = True Then
            'btnPrint.Visible = False
            'btnFax.Visible = False
            ''btnClose.Visible = False
            ''btnSelectall.Visible = False
            'btnClearall.Visible = False
            pnlProgress.Visible = True
            PrgBarPrintFax.Visible = True
            Panel7.BringToFront()
            FaxAll()
            ToolStripButton1.Text = "&Select All"
            ToolStripButton1.Tag = "Selectall"
            ToolStripButton1.Image = Global.gloEMR.My.Resources.Select_All
            ToolStripButton1.ToolTipText = "Select All"
            'btnPrint.Visible = True
            'btnFax.Visible = True

            'btnSelectall.Visible = True
            'btnClearall.Visible = True
            'PrgBarPrintFax.Visible = False
            pnlProgress.Visible = False
            'For i As Integer = 1 To C1ExamDetails.Rows.Count - 1
            '    C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            'Next
        Else
            MessageBox.Show("Select exam to Fax", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub CloseForm()

        If bnlPrintInProgress = False Then
            Me.Close()
        End If

    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Selectall"
                Call SelectAllTemp()
            Case "Clear"
                Call ClearAllTemp()
            Case "Print"
                Call PrintTemp()
            Case "Fax"
                Call FaxTemp()
            Case "Close"
                Call CloseForm()
            Case "ViewLogs"
                ViewLogs()
            Case "HideLogs"
                HideLogs()
            Case "SelectContactFax"
                Selectcontact()
        End Select
    End Sub

    Private Sub ViewLogs()
        Try

            pnlgrid.Visible = True
            tsb_Viewlogs.Visible = False
            tsb_Hidelogs.Visible = True

            DTPTOLog.Text = Format(Now, "MM/dd/yyyy")
            FillLog()
            DTPFromLog.Select()

            ToolStripButton6.Tag = "Selectall"
            ToolStripButton6.Visible = True
            ToolStripButton6.Text = "&Select All"
            ToolStripButton7.Visible = False
            ToolStripButton7.Text = "&Clear All"

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub
    Private Sub HideLogs()
        Try
            tsb_Hidelogs.Visible = False
            tsb_Viewlogs.Visible = True
            pnlgrid.Visible = False

            ToolStripButton6.Tag = "Selectall"
            ToolStripButton6.Visible = True
            ToolStripButton6.Text = "&Select All"
            ToolStripButton7.Visible = False
            ToolStripButton7.Text = "&Clear All"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


    ''**********************Added by Ojeswini 23April09***************************
    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewlog.MouseHover, btnLogSelect.MouseHover, btnLogCLear.MouseHover, btnDelete.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewlog.MouseLeave, btnLogSelect.MouseLeave, btnLogCLear.MouseLeave, btnDelete.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub


    ''**********************Added by Ojeswini 24April09***************************
    Private Sub rbNotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbNotes.CheckedChanged
        If rbNotes.Checked = True Then
            rbNotes.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbNotes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSelect.CheckedChanged
        If rbSelect.Checked = True Then
            rbSelect.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSelect.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbNone_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbNone.CheckedChanged
        If rbNone.Checked = True Then
            rbNone.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbNone.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

    End Sub

    Private Sub ToolStrip2_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip2.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Selectall"
                Call SelectAllTemp1()
            Case "Clear"
                Call ClearAllTemp1()
                'Case "Print"
                '    Call PrintTemp()
                'Case "Fax"
                '    Call FaxTemp()
                'Case "Delete"
                '    Call CloseForm()
        End Select
    End Sub
    Private Sub SelectAllTemp1()
        ToolStripButton6.Text = "&Clear All"
        ToolStripButton6.Tag = "Clear"
        ToolStripButton6.Image = Global.gloEMR.My.Resources.Clear_All
        ToolStripButton6.ToolTipText = "Clear All"
        IsselectAll = True
        For i As Integer = 1 To C1ShowLog.Rows.Count - 1
            C1ShowLog.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
        Next
    End Sub
    Private Sub ClearAllTemp1()
        ToolStripButton6.Text = "&Select All"
        ToolStripButton6.Tag = "Selectall"
        ToolStripButton6.Image = Global.gloEMR.My.Resources.Select_All1
        ToolStripButton6.ToolTipText = "Select All"
        For i As Integer = 1 To C1ShowLog.Rows.Count - 1
            C1ShowLog.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        Next
    End Sub

    Private Sub tlb_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlb_Close.Click
        Try
            HideLogs()
            'btnViewlog.Text = "&View Log"
            'pnlgrid.Visible = False
            'ToolStripButton6.Tag = "Selectall"
            'ToolStripButton6.Visible = True
            'ToolStripButton6.Text = "&Select All"
            'ToolStripButton7.Visible = False
            'ToolStripButton7.Text = "&Clear All"

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


    Private Sub C1ExamDetails_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ExamDetails.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub


    Public Shared Sub ShowToolTip(ByVal oC1ToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip, ByVal oGrid As C1.Win.C1FlexGrid.C1FlexGrid, ByVal nLocation As System.Drawing.Point)
        Try
            Dim myFont As Font = oGrid.Font
            Dim stringsize As SizeF
            Dim colsize As Integer = 0
            Dim sText As String = ""
            Dim nRow As Integer
            Dim nCol As Integer

            If oGrid.MouseCol > -1 AndAlso oGrid.MouseRow > -1 Then
                oC1ToolTip.Font = myFont
                oC1ToolTip.MaximumWidth = 400

                nRow = oGrid.MouseRow
                nCol = oGrid.MouseCol

                If nRow > 0 Then 'And nCol > 0 Then
                    If Not oGrid.GetData(nRow, nCol) Is Nothing Then
                        sText = oGrid.GetData(nRow, nCol)
                    End If
                    colsize = oGrid.Cols(nCol).WidthDisplay
                End If
                Dim oGrp As Graphics = oGrid.CreateGraphics()
                stringsize = oGrp.MeasureString(sText, myFont)
                ''Code Review Changes: Dispose Graphics object
                oGrp.Dispose()
                If stringsize.Width > colsize Then
                    oC1ToolTip.SetToolTip(oGrid, sText)
                Else
                    oC1ToolTip.SetToolTip(oGrid, "")
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Referrals, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub C1ShowLog_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1ShowLog.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Dim _IsExamSelected As Boolean = False

    Private Function SetDataFaxData() As DataTable

        _IsExamSelected = False
        strPrintLogFile = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".txt", "MMddyyyyHHmmssffff") 'gloSettings.FolderSettings.AppTempFolderPath & Format(Date.Now, "MM dd yyyy - hh mm ss tt") & ".txt"

        Dim dtnew As New DataTable()
        dtnew.Columns.Add("PatientID", GetType(System.Int64))
        dtnew.Columns.Add("PatientCode", GetType(System.String))
        dtnew.Columns.Add("PatientName", GetType(System.String))
        dtnew.Columns.Add("PatientDOS", GetType(System.String))
        dtnew.Columns.Add("ExamName", GetType(System.String))
        dtnew.Columns.Add("ExamID", GetType(System.Int64))
        dtnew.Columns.Add("VisitID", GetType(System.Int64))
        dtnew.Columns.Add("ExamFinish", GetType(System.String))
        dtnew.Columns.Add("TemplateID", GetType(System.Int64))
        Dim j As Int16 = 0
        For i As Integer = 1 To C1ExamDetails.Rows.Count - 1

            If C1ExamDetails.GetCellCheck(i, Col_Select) = C1.Win.C1FlexGrid.CheckEnum.Checked Then

                Dim sPatientCode As String = C1ExamDetails.GetData(i, Col_PatientCode)
                Dim sPatientDOS As String = C1ExamDetails.GetData(i, Col_DOS)
                Dim sPatientName As String = C1ExamDetails.GetData(i, Col_PatientName)
                Dim sExamName As String = C1ExamDetails.GetData(i, Col_ExamName)
                Dim nPatientId As Int64 = C1ExamDetails.GetData(i, Col_PatientID)
                Dim nVisitID As Int64 = C1ExamDetails.GetData(i, Col_VisitID)
                Dim nExamId As Int64 = C1ExamDetails.GetData(i, Col_ExamID)
                Dim blnIsFinished As Boolean
                If C1ExamDetails.GetData(i, Col_IsFinish) = "Yes" Then
                    blnIsFinished = True
                Else
                    blnIsFinished = False
                End If

                Dim dr As DataRow = dtnew.NewRow()
                dtnew.Rows.Add(dr)
                dtnew.Rows(j)("PatientID") = nPatientId
                dtnew.Rows(j)("PatientCode") = sPatientCode
                dtnew.Rows(j)("PatientName") = sPatientName
                dtnew.Rows(j)("PatientDOS") = sPatientDOS
                dtnew.Rows(j)("ExamName") = sExamName
                dtnew.Rows(j)("ExamID") = nExamId
                dtnew.Rows(j)("VisitID") = nVisitID
                dtnew.Rows(j)("ExamFinish") = blnIsFinished
                dtnew.Rows(j)("TemplateID") = TemplateId
                j = j + 1

                _IsExamSelected = True

                ''C1ExamDetails.SetCellCheck(i, Col_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked)

            End If

        Next
        Return dtnew
    End Function

    Private Sub Selectcontact()

        gstrFAXContactPerson = ""
        gstrFAXContactPersonFAXNo = ""
        Dim dt As DataTable
        dt = SetDataFaxData()
        If (_IsExamSelected = True) Then
            gblnIsSelectRefContact = True
            If (IsNothing(gstrfaxCollection) = False) Then
                gstrfaxCollection.Clear()
            Else
                gstrfaxCollection = New Collection
            End If
            Dim frm As New frmSelectContactFAXWithFAXCoverPage(gstrfaxCollection)
            frm.dtBatchRefFaxInfo = dt
            frm.blnIsFromBatchreferral = True
            frmSelectContactFAXWithFAXCoverPage.PatientID = _PatientID
            frmSelectContactFAXWithFAXCoverPage.strPrintLogFile = strPrintLogFile
            frmSelectContactFAXWithFAXCoverPage.strBatchRefTemplate = cmbTemplate.SelectedItem(1)
            frmSelectContactFAXWithFAXCoverPage._blnInsertExamNotes = rbNone.Checked            
            'Bug #43298: 00000340 : Faxing
            'When using a template with bookmarks and using the 'selected' notes option in this module, 
            'it sends out the entire note, not just the bookmarks
            frmSelectContactFAXWithFAXCoverPage._blnInsertSelectedExamNotes = rbSelect.Checked
            '-----
            If gblnFAXCoverPage = False Then

                frm.pnlFaxCoverPg.Visible = False
                frm.Panel4.Visible = False
                frm.Splitter1.Visible = False
                frm.btnUp1.Visible = True
                frm.btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
                frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                frm.btnDown1.Visible = False
                frm.pnlSelectFAXNo.Dock = DockStyle.Fill

            Else

                frm.btnUp1.Visible = True
                frm.btnUp1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
                frm.btnUp1.BackgroundImageLayout = ImageLayout.Center
                frm.btnDown1.Visible = False
                frm.LoadFAXCoverPage()
            End If
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            gblnIsSelectRefContact = False
            frm.Dispose()
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        Else
            MessageBox.Show("Select exam to fax. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub

    Private Sub DTPTo_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPTo.ValueChanged
        SelectExam()
    End Sub

    Private Sub DTPFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPFrom.ValueChanged
        SelectExam()
    End Sub
End Class