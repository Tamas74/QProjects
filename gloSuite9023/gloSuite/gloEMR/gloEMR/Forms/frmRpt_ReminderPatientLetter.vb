Imports System.Data.SqlClient
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports gloPatientPortalCommon
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing


Public Class frmRpt_ReminderPatientLetter
    Private _clinicId As Int64 = 0
    Private _PatientID As Int64 = 0
    Private _PatientName As String = ""
    Private _TemplateID As Int64 = 0
    Private _TemplateName As String = ""
    Private _VisitID As Int64 = 0
    Private _TaskID As Int64 = 0
    Private _bIsUnscheduledCare As Boolean = False
    Private _nCommunicationTypeID As Int64 = 0
    Private _databaseconnectionstring As String = ""
    Private FormLoading As Boolean = True

    Private _messageBoxCaption As String = "gloEMR"

    '' chetan added for integration
    Public _dtPat As DataTable = Nothing

    Private WithEvents oCurDoc As Wd.Document
    Private WithEvents oTempDoc As Wd.Document
    Private WithEvents oWordApp As Wd.Application
    Dim objWord As clsWordDocument
    Dim objCriteria As DocCriteria
    Dim stroldSubject As String = ""
    Dim stroldMessage As String = ""
    Private SelectedRadio As Integer = 0
    Dim drrPatient As DataRow() = Nothing
    Dim dtPatient As DataTable = Nothing

    Private myCaller As Control = Nothing
    Public Sub New(Optional ByVal myControl As Control = Nothing)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        myCaller = myControl
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmRpt_ReminderPatientLetter_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        ''added for  bugid 86574
        If Not IsNothing(dtPatient) Then
            dtPatient.Dispose()
            dtPatient = Nothing
        End If
        If Not IsNothing(drrPatient) Then
            drrPatient = Nothing
        End If
        If Not IsNothing(dtselectedpatient) Then
            dtselectedpatient.Dispose()
            dtselectedpatient = Nothing
        End If
        'If Not IsNothing(dictTemp) Then
        '    dictTemp.Clear()

        'End If
        dictPatLetter = Nothing
    End Sub

    Private Sub frmRpt_ReminderPatientLetter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1Patients)
        AddHandler Me.rdoAll.CheckedChanged, AddressOf RadioOnChange
        AddHandler Me.rdoActivated.CheckedChanged, AddressOf RadioOnChange
        AddHandler Me.rdoNonActivated.CheckedChanged, AddressOf RadioOnChange

        '22-Apr-14 Aniket: Fixing reminder count issue
        FillReminderforUnscheduledCare()
        blnBatchPrintinProgress = False
        Try
            If gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
                btnSendtoportal.Visible = True
                rdoAll.Visible = True
                rdoActivated.Visible = True
                rdoNonActivated.Visible = True
                If SelectedRadio = 0 OrElse SelectedRadio = 2 Then
                    btnSendtoportal.Enabled = False
                Else
                    btnSendtoportal.Enabled = False
                End If
            Else
                btnSendtoportal.Visible = False
                rdoAll.Visible = False
                rdoActivated.Visible = False
                rdoNonActivated.Visible = False

                SelectedRadio = 0
            End If
            If _dtPat Is Nothing Then '' chetan integrated for integration of Reminder report

                '09-May-14 Aniket: Commented as it is not needed.
                'BindGrid()
                Fill_Patients()

                Fill_Templates()


                If (c1Patients.Rows.Count > 1) Then
                    c1Patients.Select(1, 0)
                End If
            Else


                c1Patients.DataSource = _dtPat
                c1Patients.Cols(0).DataType = GetType(Boolean)
                c1Patients.Cols(0).Width = Convert.ToInt32(c1Patients.Width * 0.1)
                c1Patients.Cols(1).Width = 0
                c1Patients.Cols(2).Width = Convert.ToInt32(c1Patients.Width * 0.15)
                c1Patients.Cols(3).Width = Convert.ToInt32(c1Patients.Width * 0.4)
                c1Patients.ExtendLastCol = True

                c1Patients.Cols(1).AllowEditing = False
                c1Patients.Cols(2).AllowEditing = False
                c1Patients.Cols(3).AllowEditing = False
                '09-May-14 Aniket: Bug #68299
                c1Patients.Cols(4).AllowEditing = False

                '' c1Patients.Cols(4).Width = Convert.ToInt32(c1Patients.Width * 0.1)
                pnlControl.Visible = False
                If gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
                    If c1Patients.Cols.Contains("RegisteredOnPortal") Then
                        c1Patients.Cols("RegisteredOnPortal").AllowEditing = False
                        c1Patients.Cols("RegisteredOnPortal").Visible = False
                    End If
                End If
                Fill_Templates()
                ' SelectAllClearAll(False)

                tlbtnSelectAll.Tag = "Clear"
                tlbtnSelectAll.Text = "Cl&ear All"
                tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
                tlbtnSelectAll.ToolTipText = "Clear All Patients"


            End If

            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloCommon.Cls_TabIndexSettings(Me)
            ' '' This method actually sets the order all the way down the control hierarchy.

            tom.SetTabOrder(scheme)
            ' BackgroundWorker1.WorkerReportsProgress = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindGrid()
        'c1Patients.Clear()
        c1Patients.DataSource = Nothing
        c1Patients.Cols.Fixed = 0
        Dim oBindTable As New DataTable()
        oBindTable.Columns.Add("Select")
        oBindTable.Columns.Add("PatientID")
        oBindTable.Columns.Add("Patient Name")
        oBindTable.Columns.Add("Task Id") 'TaskId'

        c1Patients.DataSource = oBindTable
        c1Patients.Cols(0).Width = Convert.ToInt32(c1Patients.Width * 0.2)
        c1Patients.Cols(1).Width = 0
        c1Patients.Cols(2).Width = Convert.ToInt32(c1Patients.Width * 0.8)
        c1Patients.Cols(3).Width = 0

        c1Patients.Cols(1).AllowEditing = False
        c1Patients.Cols(2).AllowEditing = False
        c1Patients.Cols(3).AllowEditing = False




    End Sub

    Public Function GetTemplates() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable = Nothing
        Try
            Dim strSQL As String = " "
            'strSQL = " SELECT  ISNULL(TemplateGallery_MST.nTemplateID,0) AS nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName,  " _
            '            & "  ISNULL(TemplateGallery_MST.nProviderID,0) AS nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " _
            '            & " + ISNULL(Provider_MST.sLastName, '') AS sProviderName,ISNULL(TemplateGallery_MST.nCategoryID,0) AS CategoryID, ISNULL(TemplateGallery_MST.sCategoryName,'') AS  CategoryName " _
            '            & " FROM  TemplateGallery_MST LEFT OUTER JOIN " _
            '            & " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " _
            '            & " WHERE  TemplateGallery_MST.sCategoryName = 'Patient Letters' "
            strSQL = " SELECT  ISNULL(TemplateGallery_MST.nTemplateID,0) AS nTemplateID, ISNULL(sTemplateName, '') AS sTemplateName,  " _
                        & " ISNULL(nCategoryID,0) AS CategoryID, ISNULL(sCategoryName,'') AS  CategoryName " _
                        & " FROM  TemplateGallery_MST  " _
             & " LEFT OUTER JOIN  dbo.TemplateGallery_Association" _
             & " ON dbo.TemplateGallery_MST.nTemplateID = dbo.TemplateGallery_Association.nTemplateID AND  TemplateGallery_Association.nAssociatedCategoryID=8 " _
             & " WHERE  sCategoryName = 'Patient Letters'  ORDER BY  ISNULL(TemplateGallery_Association.nAssociatedCategoryID, 0) " _
             & " DESC "
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text

            objCmd.CommandText = strSQL
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
        Catch ex As Exception

        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return dt
    End Function

    Private Sub Fill_Templates()

        trvTemplates.Nodes.Clear()

        Dim oNode As New TreeNode()
        oNode.Text = "Patient Letters"
        trvTemplates.Nodes.Add(oNode)

        Dim dtTemplates As DataTable
        dtTemplates = GetTemplates()
        If dtTemplates IsNot Nothing Then

            Dim j As Integer
            For j = 0 To dtTemplates.Rows.Count - 1
                oNode.Tag = dtTemplates.Rows(j)("CategoryID")
                Dim oTemplateNode As New TreeNode()
                oTemplateNode.Text = dtTemplates.Rows(j)("sTemplateName").ToString()
                oTemplateNode.Tag = dtTemplates.Rows(j)("nTemplateID")
                oNode.Nodes.Add(oTemplateNode)
            Next
            dtTemplates.Dispose()
            dtTemplates = Nothing

        End If


        trvTemplates.ExpandAll()
    End Sub


    Private Sub wdPatientLetter_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdPatientLetter.BeforeDocumentClosed
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
                                    '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    UpdateVoiceLog(ex.ToString)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            UpdateVoiceLog(ex.ToString)
                            ex = Nothing
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
        End Try
    End Sub

    Private Sub wdPatientLetter_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdPatientLetter.OnDocumentClosed
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

    '''' <summary>
    '''' To implemt the Dropdown and check Box selection change event
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <remarks></remarks>
    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)
        'Try

        'Catch 'ex As Exception

        'End Try

    End Sub

    Private Sub wdPatientLetter_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdPatientLetter.OnDocumentOpened
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

    ''to open the Concerned patient Letter frpm DB in DSO Control
    Private Sub Fill_TemplateGallery() 'As String
        Dim strFileName As String = ""
        objWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        objCriteria.PrimaryID = _TemplateID
        objWord.DocumentCriteria = objCriteria
        ''//Retrieving the Patient Education from DB and Save it as Physical File
        strFileName = objWord.RetrieveDocumentFile()
        objCriteria.Dispose()
        objCriteria = Nothing
        objWord = Nothing
        If (IsNothing(strFileName) = False) Then
            If strFileName <> "" Then
                LoadWordUserControl(strFileName, True)
                'Set the Start postion of the cursor in documents
                oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
            Else
                wdPatientLetter.Close()
            End If
        Else
            wdPatientLetter.Close()
        End If



    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)

        '  wdPatientLetter.Open(strFileName)
        '  Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdPatientLetter, strFileName, oCurDoc, oWordApp)
        If blnGetData Then
            ''//To retrieve the Form fields for the Word document
            objWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            objCriteria.PatientID = _PatientID
            objCriteria.VisitID = _VisitID
            objCriteria.PrimaryID = _TemplateID ''0
            objWord.DocumentCriteria = objCriteria
            objWord.CurDocument = oCurDoc
            ''Replace Form fields with Concerned data
            objWord.GetFormFieldData(enumDocType.None)
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
        Else
            objWord = New clsWordDocument
            objWord.CurDocument = oCurDoc
            objWord.HighlightColor()
            oCurDoc = objWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objWord = Nothing
        End If
    End Sub


    Private Function Fill_Patients_old() As DataTable


        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strSQL As String = ""
        Dim da As SqlDataAdapter
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text

            strSQL = "SELECT  DISTINCT  ISNULL(TM_TaskMST.nPatientID,0) AS nPatientID,(ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'')) AS PatientName,  " _
                     & " ISNULL(TM_TaskMST.nTaskID,0) AS nTaskID  FROM  Patient INNER JOIN  TM_TaskMST ON Patient.nPatientID = TM_TaskMST.nPatientID INNER JOIN  RM_Reminder_MST ON TM_TaskMST.nTaskID = RM_Reminder_MST.nReferenceID  " _
                     & " INNER JOIN  RM_Reminder_DTL ON RM_Reminder_MST.nReminderID = RM_Reminder_DTL.nReminderID " _
                     & " WHERE (RM_Reminder_MST.bIsDismissed = 0) AND (RM_Reminder_MST.nRefrenceType = 2) AND " _
                     & " RM_Reminder_DTL.dtReminderDate >= " & gloDateMaster.gloDate.DateAsNumber(dtp_FromDate.Value.ToShortDateString()) & " AND  RM_Reminder_DTL.dtReminderDate <=" & gloDateMaster.gloDate.DateAsNumber(dtp_ToDate.Value.ToShortDateString()) & ""


            'strSQL = "SELECT  ISNULL(TM_TaskMST.nPatientID,0) AS nPatientID,(ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'')) AS PatientName,  " _
            '         & " ISNULL(TM_TaskMST.nTaskID,0) AS nTaskID  FROM  Patient INNER JOIN  TM_TaskMST ON Patient.nPatientID = TM_TaskMST.nPatientID INNER JOIN  RM_Reminder_MST ON TM_TaskMST.nTaskID = RM_Reminder_MST.nReferenceID " _
            '         & " WHERE (RM_Reminder_MST.bIsDismissed = 0) AND (RM_Reminder_MST.nRefrenceType = 2) AND RM_Reminder_MST.dtReminderStartDate >= " & gloDateMaster.gloDate.DateAsNumber(dtp_FromDate.Value.ToShortDateString()) & " AND  RM_Reminder_MST.dtReminderStartDate <=" & gloDateMaster.gloDate.DateAsNumber(dtp_ToDate.Value.ToShortDateString()) & "" _
            '         & " AND  RM_Reminder_MST.dtReminderEndDate >= " & gloDateMaster.gloDate.DateAsNumber(dtp_FromDate.Value.ToShortDateString()) & " AND  RM_Reminder_MST.dtReminderEndDate <=" & gloDateMaster.gloDate.DateAsNumber(dtp_ToDate.Value.ToShortDateString()) & ""


            objCmd.CommandText = strSQL
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)

            ''''  c1Patients.SetData(0, 1, "Patient Name")
            Dim col As New DataColumn()
            col.ColumnName = "Select"
            col.DataType = Type.[GetType]("System.Boolean")
            col.DefaultValue = False
            dt.Columns.Add(col)


            If dt IsNot Nothing Then
                c1Patients.DataSource = dt
                FormLoading = False
            End If
            c1Patients.SetData(0, 1, "Patient Name")
            c1Patients.Cols(0).Visible = False
            c1Patients.Cols(1).Visible = True
            c1Patients.Cols(1).Width = 0
            c1Patients.Cols(1).Width = Convert.ToInt32(c1Patients.Width * 0.9)
            ' Width of PatientName Column 
            c1Patients.Cols(3).Width = Convert.ToInt32(c1Patients.Width * 0.07)
            ' Width of Select Column 
            c1Patients.Cols(2).Visible = False
            c1Patients.AllowEditing = True

            c1Patients.Cols.Move(3, 0)

            c1Patients.Cols(0).AllowEditing = True
            c1Patients.Cols(1).AllowEditing = False
            c1Patients.Cols(2).AllowEditing = False

            btnSelectAllClearAll.Tag = "Select"
            btnSelectAllClearAll.Text = "Select All"





            'oBindTable.Columns.Add("Task Id")

        Catch ex As Exception

        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return dt
    End Function
    Private Function Fill_Patients() As DataTable
        Dim dt As DataTable = Nothing
        Try
            dt = FillreminderPatients()

            If dt IsNot Nothing Then
                Dim col As New DataColumn()
                col.ColumnName = "Select"
                col.DataType = Type.[GetType]("System.Boolean")
                col.DefaultValue = False
                dt.Columns.Add(col)
                c1Patients.DataSource = dt
                FormLoading = False
            End If
            c1Patients.SetData(0, 1, "Patient Name")
            c1Patients.SetData(0, 3, "Comm. Preference")
            c1Patients.Cols(0).Visible = False
            c1Patients.Cols(1).Visible = True
            c1Patients.Cols(1).Width = 0
            c1Patients.Cols(1).Width = Convert.ToInt32(c1Patients.Width * 0.65)
            c1Patients.Cols(3).Width = Convert.ToInt32(c1Patients.Width * 0.2)
            ' Width of PatientName Column 
            c1Patients.Cols(4).Width = Convert.ToInt32(c1Patients.Width * 0.07)
            ' Width of Select Column 
            c1Patients.Cols(2).Visible = False
            c1Patients.AllowEditing = True

            If c1Patients.Cols.Contains("RegisteredOnPortal") Then
                c1Patients.Cols("RegisteredOnPortal").Visible = False
                c1Patients.Cols.Move(5, 0)
            Else
                c1Patients.Cols.Move(4, 0)
            End If

            c1Patients.Cols(0).AllowEditing = True
            c1Patients.Cols(1).AllowEditing = False
            c1Patients.Cols(2).AllowEditing = False
            c1Patients.Cols(3).AllowEditing = False
            '09-May-14 Aniket: Bug #68299
            c1Patients.Cols(4).AllowEditing = False

            btnSelectAllClearAll.Tag = "Select"
            btnSelectAllClearAll.Text = "Select All"

        Catch ex As Exception

        Finally
        End Try
        Return dt
    End Function

    Private Function FillreminderPatients() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dt As DataTable = Nothing
        Try

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters

            oParam.Add("@FromDate", gloDateMaster.gloDate.DateAsNumber(dtp_FromDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int)
            oParam.Add("@ToDate", gloDateMaster.gloDate.DateAsNumber(dtp_ToDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int)
            oParam.Add("@sCommunicationPref", CmbCommunicationPref.Text, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Retrive("FillReminderPatients", oParam, dt)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            oParam.Dispose()
            oParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally

        End Try
    End Function
    Private Sub dtp_FromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp_FromDate.ValueChanged
        Fill_Patients()
    End Sub

    Private Sub dtp_ToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp_ToDate.ValueChanged
        Fill_Patients()
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

    Private Sub btnSelectAllClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAllClearAll.Click
        Try
            If btnSelectAllClearAll.Tag.ToString() = "Select" Then
                btnSelectAllClearAll.Tag = "Clear"
                btnSelectAllClearAll.Text = "Clear All"
                SelectAllClearAll(True)
            Else
                btnSelectAllClearAll.Tag = "Select"
                btnSelectAllClearAll.Text = "Select All"
                SelectAllClearAll(False)
            End If
        Catch generatedExceptionName As Exception

            Throw
        End Try

    End Sub

    ''change function SelectAllClearAll for bugid 86574
    Private Sub SelectAllClearAll(ByVal [select] As Boolean)
        Dim strasc As String = ""
        Dim sortColmName As String = ""
        Try
            ''  c1Patients.BeginUpdate()
            Me.Cursor = Cursors.WaitCursor
            tls_OverDue.Enabled = False
            If (Not IsNothing(c1Patients.DataSource)) Then
                dtPatient = c1Patients.DataSource


                If (Not IsNothing(c1Patients.SortColumn)) Then
                    If (c1Patients.SortColumn.Sort.ToString() = "1") Then
                        strasc = "Asc"
                    Else
                        strasc = "Desc"
                    End If
                    sortColmName = c1Patients.SortColumn.Name
                    If sortColmName.Contains("Select") Then
                        c1Patients.Redraw = False
                    End If
                End If

                If gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then

                    If SelectedRadio = 1 Then
                        '' If [select] = True Then

                        If (Not IsNothing(dtPatient)) AndAlso strasc <> "" Then
                            drrPatient = dtPatient.Select("RegisteredOnPortal=1", sortColmName & " " & strasc)
                        Else
                            drrPatient = dtPatient.Select("RegisteredOnPortal=1")
                        End If
                        For Each dr As DataRow In drrPatient
                            dr("Select") = [select]
                        Next


                    ElseIf SelectedRadio = 2 Then
                        If (Not IsNothing(dtPatient)) AndAlso strasc <> "" Then
                            drrPatient = dtPatient.Select("RegisteredOnPortal=0", sortColmName & " " & strasc)
                        Else
                            drrPatient = dtPatient.Select("RegisteredOnPortal=0")
                        End If
                        For Each dr As DataRow In drrPatient
                            dr("Select") = [select]
                        Next


                    ElseIf SelectedRadio = 0 Then
                        ''If [select] = True Then
                        For Each dr As DataRow In dtPatient.Rows
                            dr("Select") = [select]
                        Next
                    End If
                Else
                    For Each dr As DataRow In dtPatient.Rows
                        dr("Select") = [select]
                    Next
                End If
                c1Patients.DataSource = dtPatient
            End If
            If sortColmName.Contains("Select") Then
                c1Patients.Redraw = True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at PatientReminderLetterReport " & ex.Message.ToString(), False)
        End Try
        tls_OverDue.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub trvTemplates_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvTemplates.AfterCheck
        Try
            If e.Node.Level = 0 Then
                For Each oNode As TreeNode In e.Node.Nodes
                    oNode.Checked = e.Node.Checked
                Next
            ElseIf e.Node.Level = 1 Then
                If e.Node.Checked = False Then
                    RemoveHandler Me.trvTemplates.AfterCheck, AddressOf Me.trvTemplates_AfterCheck
                    e.Node.Parent.Checked = False
                    AddHandler Me.trvTemplates.AfterCheck, AddressOf Me.trvTemplates_AfterCheck
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub c1Patients_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Patients.AfterEdit
        If FormLoading = False Then
            Dim _UncheckFound As Boolean = False
            For i As Integer = 1 To c1Patients.Rows.Count - 1
                If c1Patients.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    _UncheckFound = True
                    Exit For
                End If
            Next
            If _UncheckFound = True Then
                tlbtnSelectAll.Tag = "Select"
                tlbtnSelectAll.Text = "Select All"
                tlbtnSelectAll.ToolTipText = "Select All Patients"
                tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
            Else
                tlbtnSelectAll.Tag = "Clear"
                tlbtnSelectAll.Text = "Clear All"
                tlbtnSelectAll.ToolTipText = "Clear All Patients"
                tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
            End If
        End If

    End Sub

    Private Function Save() As Boolean

        Dim result As Boolean
        Try
            Dim ocls As New clsPatientLetters

            For i As Integer = 1 To c1Patients.Rows.Count - 1
                If c1Patients.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    'For k As Integer = 0 To trvTemplates.Nodes(0).Nodes.Count - 1
                    For Each n As TreeNode In trvTemplates.Nodes(0).Nodes
                        If (n.Checked = True) Then
                            _PatientID = Convert.ToInt64(c1Patients.GetData(i, 1))
                            _PatientName = Convert.ToString(c1Patients.GetData(i, 2))
                            _TemplateID = Convert.ToInt64(n.Tag)
                            _TemplateName = n.Text
                            _TaskID = Convert.ToInt64(c1Patients.GetData(i, 3))
                            _VisitID = GenerateVisitID(Now, _PatientID)
                            '  Dim strFileName As String = ""
                            Fill_TemplateGallery()

                            'strFileName = ExamNewDocumentName
                            ''wdCtrlPatientLetter.document.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatDocumentDefault)
                            ''wdPatientLetter.Save(strFileName, True, "", "")
                            'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                            'wdPatientLetter.Close()
                            Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientLetter, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                            Dim myBinaray As Object = Nothing
                            If (IsNothing(myByte) = False) Then
                                myBinaray = CType(myByte, Object)

                                If (ocls.SavePatientLetterBytes(0, _PatientID, _TemplateID, Date.Now, myBinaray, _TemplateName, False) > 0) Then
                                    UpdateReminder(_TaskID)
                                    result = True
                                Else
                                    result = False
                                End If
                            End If

                            If (IsNothing(oCurDoc) = False) Then
                                Try
                                    Marshal.ReleaseComObject(oCurDoc)
                                Catch ex As Exception


                                End Try
                                oCurDoc = Nothing

                            End If

                        End If
                    Next
                End If
            Next
            ocls.Dispose()
            ocls = Nothing
            Me.Close()
        Catch ex As Exception

        End Try
        Return result
    End Function

#Region "Button click events"
    Dim blnasynctask As Boolean = False
    Dim blnBatchPrintinProgress As Boolean = False ''added to check whether  printing dialog window  is Present 
    Private Sub ts_btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnPrint.Click
        If (CheckBatchPrintProcessRunning() = False) Then
            If (blnBatchPrintinProgress = False) Then
                Print()
            End If
        End If
    End Sub
    Private Function CheckBatchPrintProcessRunning() As Boolean
        Try


            For Each oFrm As Form In System.Windows.Forms.Application.OpenForms

                If oFrm.Name = "frmgloPrintWordReminderProgressController" Then

                    Dim dg As DialogResult = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (dg = DialogResult.Yes) Then
                        oFrm.Close()
                        blnBatchPrintinProgress = False
                        Return False
                        Exit For
                    Else
                        oFrm.Visible = True
                        Return True
                        Exit For
                    End If
                End If
            Next
            Return False
        Catch ex As Exception
            ex = Nothing
            Return False
        End Try
    End Function
    Dim dictPatLetter As New Dictionary(Of Int64, String)
    Private Sub Print()



        Dim _Ischecked = False
        Dim blnShowPrinterDialog As Boolean = True
        'variable added by dipak 20090825 for track printdialogbox's cancel button click
        Dim blnPrintDialogCancel As Boolean = False
        Dim blntaskidPres As Boolean = False
        dictPatLetter.Clear()
        For Each n As TreeNode In trvTemplates.Nodes(0).Nodes
            If (n.Checked = True) Then
                dictPatLetter.Add(Convert.ToInt64(n.Tag), Convert.ToString(n.Text))
            End If
        Next
        If dictPatLetter.Count > 0 Then

            setDatasouceOptimize()

            If Not dtselectedpatient Is Nothing Then
                If dtselectedpatient.Rows.Count > 0 Then

                    If dtselectedpatient.Columns.Contains("nTaskID") Then
                        blntaskidPres = True
                    End If
                    Dim ind As Integer = 0

                    Try

                        PrintBatch()
                    Catch ex As Exception

                    End Try

                End If
            End If
        End If


    End Sub
    Private Shared myPrinterSetting As New PrinterSettings
    Private Sub PrintBatch()


        Try
            Using oDialog As New gloPrintDialog.gloPrintDialog(True)
                Dim strOldPrinterName As String = String.Empty
                oDialog.ConnectionString = GetConnectionString()
                blnBatchPrintinProgress = True
                oDialog.TopMost = True
                oDialog.ShowPrinterProfileDialog = True
                oDialog.ModuleName = "Printing Batch ReferralLetter"

                oDialog.RegistryModuleName = "PrintBatchDocuments"

                If oDialog IsNot Nothing Then
                    If (Not gloGlobal.gloTSPrint.isCopyPrint) Then


                        oDialog.PrinterSettings = myPrinterSetting 'printDocument1.PrinterSettings
                        oDialog.AllowSomePages = True

                        oDialog.PrinterSettings.ToPage = 1
                        'maxPage;
                        oDialog.PrinterSettings.FromPage = 1
                        oDialog.PrinterSettings.MaximumPage = 1
                        ' maxPage;
                        oDialog.PrinterSettings.MinimumPage = 1

                        'PrintDialog1.AllowSomePages = True
                        'PrintDialog1.PrinterSettings.ToPage = 1
                        '' maxPage;
                        'PrintDialog1.PrinterSettings.FromPage = 1
                        'PrintDialog1.PrinterSettings.MaximumPage = 1
                        '' maxPage;
                        'PrintDialog1.PrinterSettings.MinimumPage = 1
                        ' }

                        Try
                            strOldPrinterName = myPrinterSetting.PrinterName
                        Catch ex As Exception

                        End Try


                    End If

                    If oDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        'printDocument1.PrinterSettings = oDialog.PrinterSettings
                        'If (Not gloGlobal.gloTSPrint.isCopyPrint) Then
                        'myPrinterSetting = oDialog.PrinterSettings
                        'End If


                        If (oDialog.bUseDefaultPrinter = True) Then
                            oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                            oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
                        End If
                        If gloGlobal.gloTSPrint.isCopyPrint AndAlso (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting:=False) = False) Then
                            oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                            oDialog.CustomPrinterExtendedSettings.IsShowProgress = False
                        End If
                        Dim ogloPrintProgressController As New frmgloPrintWordReminderProgressController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, myControl:=myCaller)
                        ogloPrintProgressController.dtSelectPatient = dtselectedpatient.Copy()
                        If Not IsNothing(ogloPrintProgressController.dtSelectPatient) Then
                            If ogloPrintProgressController.dtSelectPatient.Rows.Count > 0 Then
                                If Not IsNothing(ogloPrintProgressController.dtSelectPatient.Columns("nPatientID")) Then

                                    ogloPrintProgressController.dtSelectPatient.Columns("nPatientID").ColumnName = "PatientID"
                                End If
                                If Not IsNothing(ogloPrintProgressController.dtSelectPatient.Columns("PatientName")) Then

                                    ogloPrintProgressController.dtSelectPatient.Columns("PatientName").ColumnName = "Patient Name"
                                End If
                            End If
                        End If

                        ogloPrintProgressController.dictPatientLetter = dictPatLetter
                        ogloPrintProgressController.ChkRemiderForUnSchedle = ChkRemiderForUnSchedle.Checked
                        ogloPrintProgressController.strOldPrinterName = strOldPrinterName



                        If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                            If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then



                                ogloPrintProgressController.Show()
                            Else

                                ogloPrintProgressController.Show()
                            End If
                        Else
                            ogloPrintProgressController.TopMost = True
                            ogloPrintProgressController.ShowInTaskbar = False

                            ogloPrintProgressController.ShowDialog(Me)
                            If ogloPrintProgressController IsNot Nothing Then
                                ogloPrintProgressController.Dispose()
                            End If
                            ogloPrintProgressController = Nothing


                        End If
                        'if

                    End If
                Else
                    Dim _ErrorMessage As String = "Error in Showing Print Dialog"

                    If _ErrorMessage.Trim() <> "" Then
                        Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                        _MessageString = ""
                    End If


                    MessageBox.Show(_ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                End If

            End Using


        Catch ex As Exception


            Dim _ErrorMessage As String = ex.ToString()

            If _ErrorMessage.Trim() <> "" Then
                Dim _MessageString As String = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                _MessageString = ""
            End If



            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            ex = Nothing
        Finally
            blnBatchPrintinProgress = False

        End Try

    End Sub


    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        Try

            If _dtPat Is Nothing Then
                Dim _Ischecked = False
                Dim ocls As New clsPatientLetters
                For i As Integer = 1 To c1Patients.Rows.Count - 1
                    If c1Patients.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked And c1Patients.Rows(i).IsVisible Then
                        'For k As Integer = 0 To trvTemplates.Nodes(0).Nodes.Count - 1
                        For Each n As TreeNode In trvTemplates.Nodes(0).Nodes
                            If (n.Checked = True) Then
                                _PatientID = Convert.ToInt64(c1Patients.GetData(i, 1))
                                _PatientName = Convert.ToString(c1Patients.GetData(i, 2))
                                _TemplateID = Convert.ToInt64(n.Tag)
                                _TemplateName = n.Text
                                _TaskID = Convert.ToInt64(c1Patients.GetData(i, 3))
                                _VisitID = GenerateVisitID(Now, _PatientID)
                                _bIsUnscheduledCare = ChkRemiderForUnSchedle.Checked

                                '  Dim strFileName As String = ""
                                Fill_TemplateGallery()

                                ' strFileName = ExamNewDocumentName
                                'wdCtrlPatientLetter.document.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatDocumentDefault)
                                'wdPatientLetter.Save(strFileName, True, "", "")
                                'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                                'wdPatientLetter.Close()

                                'If strFileName <> "" Then
                                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientLetter, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                                Dim myBinaray As Object = Nothing
                                If (IsNothing(myByte) = False) Then
                                    myBinaray = CType(myByte, Object)
                                    If (ocls.SavePatientLetterBytes(0, _PatientID, _TemplateID, Date.Now, myBinaray, _TemplateName, False, _bIsUnscheduledCare, _nCommunicationTypeID) > 0) Then
                                        UpdateReminder(_TaskID)
                                        _Ischecked = True

                                    End If
                                End If

                                If (IsNothing(oCurDoc) = False) Then
                                    Try
                                        Marshal.ReleaseComObject(oCurDoc)
                                    Catch ex As Exception


                                    End Try
                                    oCurDoc = Nothing

                                End If

                            End If
                        Next
                    End If
                Next
                ocls.Dispose()
                ocls = Nothing
                If (_Ischecked = True) Then
                    Me.Close()
                Else

                End If

            Else
                '' chetan integrated for Report integration for Patient Reminder
                Dim _Ischecked = False
                Dim ocls As New clsPatientLetters
                For i As Integer = 1 To c1Patients.Rows.Count - 1
                    If c1Patients.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked And c1Patients.Rows(i).IsVisible Then
                        'For k As Integer = 0 To trvTemplates.Nodes(0).Nodes.Count - 1
                        For Each n As TreeNode In trvTemplates.Nodes(0).Nodes
                            If (n.Checked = True) Then
                                _PatientID = Convert.ToInt64(c1Patients.GetData(i, 1))
                                _PatientName = Convert.ToString(c1Patients.GetData(i, 2))
                                _TemplateID = Convert.ToInt64(n.Tag)
                                _TemplateName = n.Text

                                '12-Apr-13 Aniket: MU Reminder Letter Change
                                _bIsUnscheduledCare = ChkRemiderForUnSchedle.Checked

                                If IsNumeric(c1Patients.GetData(i, 3)) Then ''''''''Added by Ujwala Atre as on 20100917 - for DM Setup Report
                                    _TaskID = Convert.ToInt64(c1Patients.GetData(i, 3))
                                Else
                                    _TaskID = 0
                                End If

                                ' _VisitID = GenerateVisitID(Now, _PatientID)
                                '  Dim strFileName As String = ""
                                Fill_TemplateGallery()

                                'strFileName = ExamNewDocumentName
                                ''wdCtrlPatientLetter.document.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatDocumentDefault)
                                ''wdPatientLetter.Save(strFileName, True, "", "")
                                'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
                                'wdPatientLetter.Close()

                                'If strFileName <> "" Then
                                Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdPatientLetter, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, True)

                                Dim myBinaray As Object = Nothing
                                If (IsNothing(myByte) = False) Then
                                    myBinaray = CType(myByte, Object)
                                    '12-Apr-13 Aniket: MU Reminder Letter Change
                                    If (ocls.SavePatientLetterBytes(0, _PatientID, _TemplateID, Date.Now, myBinaray, _TemplateName, False, _bIsUnscheduledCare, _nCommunicationTypeID, 1) > 0) Then
                                        If _TaskID <> 0 Then ''''''''Added by Ujwala Atre as on 20100917 - for DM Setup Report
                                            UpdateReminder(_TaskID)
                                        End If
                                        _Ischecked = True

                                    End If
                                End If

                                If (IsNothing(oCurDoc) = False) Then
                                    Try
                                        Marshal.ReleaseComObject(oCurDoc)
                                    Catch ex As Exception


                                    End Try
                                    oCurDoc = Nothing

                                End If

                            End If
                        Next
                    End If
                Next
                If (_Ischecked = True) Then
                    Me.Close()
                End If
                ocls.Dispose()
                ocls = Nothing

            End If

        Catch

        Finally
            Me.Close()
        End Try

    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnRefresh.Click

        'If (dtp_FromDate.Value <> dtp_ToDate.Value And dtp_FromDate.Value <> dtp_ToDate.Value) Then
        '    MessageBox.Show("Reminder end date should be after reminder start date.  ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return
        'End If
        If IsNothing(_dtPat) Then
            Fill_Patients()
        End If
        '' solving TFS issue id - 1400
        tlbtnSelectAll.Tag = "Select"
        tlbtnSelectAll.Text = "Select All"
        tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
        tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
        tlbtnSelectAll.ToolTipText = "Select All Patients"
        SelectAllClearAll(False)
        '' end
    End Sub

#End Region


    Private Sub tlbtnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbtnSelectAll.Click
        Try
            tlbtnSelectAll.BackgroundImage = Nothing
            If tlbtnSelectAll.Tag.ToString() = "Select" Then
                tlbtnSelectAll.Tag = "Clear"
                tlbtnSelectAll.Text = "Cl&ear All"
                tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
                tlbtnSelectAll.ToolTipText = "Clear All Patients"
                SelectAllClearAll(True)
            Else
                tlbtnSelectAll.Tag = "Select"
                tlbtnSelectAll.Text = "S&elect All"
                tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                tlbtnSelectAll.BackgroundImageLayout = ImageLayout.Center
                tlbtnSelectAll.ToolTipText = "Select All Patients"
                SelectAllClearAll(False)
            End If
        Catch generatedExceptionName As Exception

            Throw
        End Try
    End Sub

    Private Sub c1Patients_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Patients.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub



    Private Sub FillReminderforUnscheduledCare()

        Dim objclsPatientLetters As New clsPatientLetters
        Dim ds As DataSet = objclsPatientLetters.FillReminderforUnscheduledCare().Copy()
        'objclsPatientLetters.Dispose()

        CmbCommunicationPref.DataSource = Nothing

        '18-Aug-16 Aniket: Bug #99519 ( Modified): glo EMR > Patient Reminder 
        'CmbCommunicationPref.Items.Clear()

        If IsNothing(ds) = False Then
            Dim dtReminderList As DataTable = ds.Tables(0)
            Dim dtCheckUnscheduledCare As DataTable = ds.Tables(1)
            If IsNothing(dtReminderList) = False Then
                'CmbCommunicationPref.DataSource = dt
                'CmbCommunicationPref.ValueMember = dt.Columns(0).ColumnName
                'CmbCommunicationPref.DisplayMember = dt.Columns(1).ColumnName
                If dtReminderList.Rows.Count > 0 Then
                    Dim row() As DataRow = dtReminderList.Select(" isSelected=1")
                    If (row.Length > 0) Then
                        ''CmbCommunicationPref.SelectedValue = row(0)("nCategoryID")
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

    Private Sub CmbCommunicationPref_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCommunicationPref.SelectionChangeCommitted
        ts_btnRefresh_Click(sender, e)
    End Sub

#Region "Patient Portal"

    Private Sub btnSendtoportal_Click(sender As System.Object, e As System.EventArgs) Handles btnSendtoportal.Click
        Try
            If gblnPatientPortalEnabled = True And gblnIntuitCommunication = True Then
                Dim dtPatientlist As New DataTable
                If c1Patients.Rows.Count <= 1 Then
                    MessageBox.Show("No patient selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If c1Patients.DataSource IsNot Nothing Then
                    Dim view As New DataView(TryCast(c1Patients.DataSource, DataTable))
                    If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                        view.RowFilter = "Select <> 'False' AND RegisteredOnPortal = '1'"
                    Else
                        view.RowFilter = "Select <> '0' AND Select <> 'False' AND RegisteredOnPortal = '1'"
                    End If
                    If view.ToTable().Rows.Count = 0 Then
                        MessageBox.Show("No patient selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    dtPatientlist = view.ToTable()
                Else
                    MessageBox.Show("No patient selected.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim PortalType As String = "gloPatientPortal"
                Dim clsPatientPortal As New clsgloPatientPortalEmail(GetConnectionString())

                Dim dtTemplatelist As New DataTable
                dtTemplatelist.Columns.Add("MailIcon", GetType(String))
                dtTemplatelist.Columns.Add("TemplateName", GetType(String))
                dtTemplatelist.Columns.Add("TemplateID", GetType(Decimal))
                dtTemplatelist.Columns.Add("TemplateFilepath", GetType(String))
                dtTemplatelist.Columns.Add("FileSize", GetType(String))
                dtTemplatelist.Columns.Add("Delete", GetType(String))


                Dim colIslocalattachment As New DataColumn("isnontemplatefile", GetType(System.Boolean))
                colIslocalattachment.DefaultValue = False
                dtTemplatelist.Columns.Add(colIslocalattachment)
                dtTemplatelist.AcceptChanges()

                Dim nodecnt As Integer = 0
                If trvTemplates.Nodes.Count >= 0 Then
                    For Each n As TreeNode In trvTemplates.Nodes(0).Nodes
                        If (n.Checked = True) Then
                            Dim drTemp As DataRow = dtTemplatelist.NewRow
                            nodecnt += 1
                            drTemp("TemplateID") = Convert.ToInt64(n.Tag)

                            Dim strFileName As String = ""
                            Try
                                objWord = New clsWordDocument
                                objCriteria = New DocCriteria
                                objCriteria.DocCategory = enumDocCategory.Template
                                objCriteria.PrimaryID = Convert.ToInt64(n.Tag)
                                objWord.DocumentCriteria = objCriteria
                                ''//Retrieving the Patient Education from DB and Save it as Physical File
                                strFileName = objWord.RetrieveDocumentFile()
                                objCriteria.Dispose()
                                objCriteria = Nothing
                                objWord = Nothing
                            Catch ex As Exception
                                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try
                            drTemp("TemplateFilepath") = strFileName
                            Dim strExt As String = Path.GetExtension(strFileName)
                            drTemp("TemplateName") = n.Text + strExt
                            Dim info As New FileInfo(strFileName)
                            drTemp("FileSize") = SetBytes(CType(info.Length, Long))
                            dtTemplatelist.Rows.Add(drTemp)
                        End If
                    Next
                End If
                If nodecnt <= 3 Then
                    Dim IsTaskFieldpresent As Boolean = False
                    If dtPatientlist.Columns.Contains("nTaskID") Then
                        IsTaskFieldpresent = True
                    End If
                    If dtPatientlist.Columns.Contains("nPatientID") Then
                        dtPatientlist.Columns("nPatientID").ColumnName = "PatientID"
                        dtPatientlist.AcceptChanges()
                    End If
                    If dtPatientlist.Columns.Contains("PatientName") Then
                        dtPatientlist.Columns("PatientName").ColumnName = "Patient Name"
                        dtPatientlist.AcceptChanges()
                    End If

                    Dim dblTotalSize As Double = 0
                    If (dtTemplatelist IsNot Nothing AndAlso dtTemplatelist.Rows.Count >= 1) Then
                        dblTotalSize = GetTotalAttachmentSize(dtTemplatelist)
                    End If

                    If dblTotalSize > 4 Then
                        Dim strDetails As String = GetAllTemplateNameandSize(dtTemplatelist)
                        If strDetails <> "" Then
                            System.Windows.MessageBox.Show("Attachments are exceeding maximum allowed limit 4 MB." & Environment.NewLine & Environment.NewLine & strDetails, gstrMessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
                        Else
                            System.Windows.MessageBox.Show("Attachments are exceeding maximum allowed limit 4 MB.", gstrMessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
                        End If

                        Exit Sub
                    End If

                    Dim frmpatientLetter As New frmPatientletterSecureMessage(dtPatientlist, dtTemplatelist, True, ChkRemiderForUnSchedle.Checked, gnClientMachineID, gstrClientMachineName)

                    If stroldSubject <> "" Then
                        frmpatientLetter.Subject = stroldSubject
                    End If

                    If stroldMessage <> "" Then
                        frmpatientLetter.Message = stroldMessage
                    End If
                    frmpatientLetter.ShowDialog()
                    stroldSubject = frmpatientLetter.Subject
                    stroldMessage = frmpatientLetter.Message
                    If IsTaskFieldpresent = True Then
                        If frmpatientLetter.IsAnytaskProcessed = True Then
                            Call ts_btnRefresh_Click(Nothing, Nothing)
                        End If
                    End If
                Else
                    MessageBox.Show("Maximum 3 patient letter selection allowed. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' 0 = All , 1 = Portal Activatd patient, 2 = Non Activated patient 
    Private Sub RadioOnChange(sender As System.Object, e As System.EventArgs)
        Try
            Dim rb = CType(sender, RadioButton)
            Dim strSelectedRadio As String = rb.Name
            If strSelectedRadio = "rdoAll" Then
                If rb.checked = True Then
                    rdoAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoNonActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    SelectedRadio = 0
                    setDatasouce()
                    btnSendtoportal.Enabled = False
                End If

            ElseIf strSelectedRadio = "rdoActivated" Then
                If rb.checked = True Then
                    rdoActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoNonActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    SelectedRadio = 1
                    setDatasouce()
                    btnSendtoportal.Enabled = True
                End If
            ElseIf strSelectedRadio = "rdoNonActivated" Then
                If rb.checked = True Then
                    rdoNonActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    rdoActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    SelectedRadio = 2
                    setDatasouce()
                    btnSendtoportal.Enabled = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Dim dtselectedpatient As DataTable = Nothing

    Public Sub setDatasouce()
        If c1Patients.DataSource Is Nothing Then
            dtselectedpatient = Nothing
            Exit Sub
        End If
        Dim view As New DataView(TryCast(c1Patients.DataSource, DataTable))
        Dim viewSelectedCnt As New DataView(TryCast(c1Patients.DataSource, DataTable))


        Try
            c1Patients.AllowFiltering = True

            For k As Integer = 0 To c1Patients.Cols.Count - 1
                If c1Patients.Cols(k).Caption.ToString().ToUpper() <> "REGISTEREDONPORTAL" Then
                    c1Patients.Cols(k).AllowFiltering = C1.Win.C1FlexGrid.AllowFiltering.None
                End If
            Next
            If SelectedRadio = 1 Then
                Dim filter As New C1.Win.C1FlexGrid.ConditionFilter
                filter.Condition1.Operator = C1.Win.C1FlexGrid.ConditionOperator.NotEquals
                filter.Condition1.Parameter = "0"
                c1Patients.Cols("RegisteredOnPortal").Filter = filter
                c1Patients.ApplyFilters()

                view.RowFilter = "RegisteredOnPortal = '1'"
                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' AND RegisteredOnPortal = '1' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False' AND RegisteredOnPortal = '1'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            ElseIf SelectedRadio = 2 Then
                Dim filter As New C1.Win.C1FlexGrid.ConditionFilter
                filter.Condition1.Operator = C1.Win.C1FlexGrid.ConditionOperator.NotEquals
                filter.Condition1.Parameter = "1"
                c1Patients.Cols("RegisteredOnPortal").Filter = filter
                c1Patients.ApplyFilters()

                view.RowFilter = "RegisteredOnPortal = '0'"
                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' AND RegisteredOnPortal = '0' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False' AND RegisteredOnPortal = '0'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            Else
                If c1Patients.DataSource IsNot Nothing Then
                    For i As Integer = 0 To c1Patients.Cols.Count - 1
                        If c1Patients.Cols(i).Filter IsNot Nothing Then
                            If c1Patients.Cols(i).Filter.IsActive = True Then
                                c1Patients.ClearFilter(i)
                            End If
                        End If
                    Next
                End If

                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            End If
            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloCommon.Cls_TabIndexSettings(Me)
            ' '' This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            view.Dispose()

            viewSelectedCnt.Dispose()
            view = Nothing
            viewSelectedCnt = Nothing
        End Try
    End Sub


    Public Sub setDatasouceOptimize()
        If c1Patients.DataSource Is Nothing Then
            dtselectedpatient = Nothing
            Exit Sub
        End If
        Dim view As New DataView(TryCast(c1Patients.DataSource, DataTable))
        Dim viewSelectedCnt As New DataView(TryCast(c1Patients.DataSource, DataTable))
        If Not IsNothing(dtselectedpatient) Then
            dtselectedpatient.Dispose()
            dtselectedpatient = Nothing
        End If

        Try
            c1Patients.AllowFiltering = True

            For k As Integer = 0 To c1Patients.Cols.Count - 1
                If c1Patients.Cols(k).Caption.ToString().ToUpper() <> "REGISTEREDONPORTAL" Then
                    c1Patients.Cols(k).AllowFiltering = C1.Win.C1FlexGrid.AllowFiltering.None
                End If
            Next
            If SelectedRadio = 1 Then
                Dim filter As New C1.Win.C1FlexGrid.ConditionFilter
                filter.Condition1.Operator = C1.Win.C1FlexGrid.ConditionOperator.NotEquals
                filter.Condition1.Parameter = "0"
                c1Patients.Cols("RegisteredOnPortal").Filter = filter
                c1Patients.ApplyFilters()

                view.RowFilter = "RegisteredOnPortal = '1'"
                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' AND RegisteredOnPortal = '1' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False' AND RegisteredOnPortal = '1'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            ElseIf SelectedRadio = 2 Then
                Dim filter As New C1.Win.C1FlexGrid.ConditionFilter
                filter.Condition1.Operator = C1.Win.C1FlexGrid.ConditionOperator.NotEquals
                filter.Condition1.Parameter = "1"
                c1Patients.Cols("RegisteredOnPortal").Filter = filter
                c1Patients.ApplyFilters()

                view.RowFilter = "RegisteredOnPortal = '0'"
                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' AND RegisteredOnPortal = '0' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False' AND RegisteredOnPortal = '0'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            Else
                If c1Patients.DataSource IsNot Nothing Then
                    For i As Integer = 0 To c1Patients.Cols.Count - 1
                        If c1Patients.Cols(i).Filter IsNot Nothing Then
                            If c1Patients.Cols(i).Filter.IsActive = True Then
                                c1Patients.ClearFilter(i)
                            End If
                        End If
                    Next
                End If

                If TryCast(c1Patients.DataSource, DataTable).Columns("Select").DataType.Name.ToUpper() = "BOOLEAN" Then
                    viewSelectedCnt.RowFilter = "Select <> 'False' "
                Else
                    viewSelectedCnt.RowFilter = "Select <> '0' AND Select <> 'False'"
                End If

                If view.ToTable.Rows.Count = viewSelectedCnt.ToTable.Rows.Count Then
                    tlbtnSelectAll.Tag = "Clear"
                    tlbtnSelectAll.Text = "Clear All"
                    tlbtnSelectAll.ToolTipText = "Clear All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                Else
                    tlbtnSelectAll.Tag = "Select"
                    tlbtnSelectAll.Text = "Select All"
                    tlbtnSelectAll.ToolTipText = "Select All Patients"
                    tlbtnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                End If
            End If
            Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New gloCommon.Cls_TabIndexSettings(Me)
            ' '' This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            view.Dispose()
            dtselectedpatient = viewSelectedCnt.ToTable()
            viewSelectedCnt.Dispose()
            view = Nothing
            viewSelectedCnt = Nothing
        End Try
    End Sub

    Public Function GetAllTemplateNameandSize(ByVal _dtTemplatelist As DataTable) As String
        Dim strMessage As String = ""
        Try
            If _dtTemplatelist IsNot Nothing Then
                If _dtTemplatelist.Rows.Count > 0 Then
                    For i As Integer = 0 To _dtTemplatelist.Rows.Count - 1
                        Dim strTempname As String = _dtTemplatelist.Rows(i)("TemplateName")
                        Dim info As New FileInfo(_dtTemplatelist.Rows(i)("TemplateFilepath"))
                        ' Get the size of the file in bytes.
                        Dim Bytes As Long = info.Length
                        strMessage += "Template : " & strTempname & "  " & "Size :  " & SetBytes(Bytes) & Environment.NewLine
                    Next
                End If
            End If
            Return strMessage
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    Public Function GetTotalAttachmentSize(ByVal _dtTemplatelist As DataTable) As Double
        Dim dblTotalSize As Double = 0
        Try
            If _dtTemplatelist IsNot Nothing Then
                If _dtTemplatelist.Rows.Count > 0 Then
                    'DirectCast(lstAttachment.Items(0),System.Windows.Controls.Border).Tag
                    For i As Integer = 0 To _dtTemplatelist.Rows.Count - 1
                        Dim info As New FileInfo(_dtTemplatelist.Rows(i)("TemplateFilepath"))
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

#End Region


End Class
