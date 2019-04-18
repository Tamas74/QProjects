Imports C1.Win.C1FlexGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports gloCommon
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloAuditTrail
Imports gloEMRGeneralLibrary.gloEMRPrescription

Public Class frmPrintFAXExam
    Inherits System.Windows.Forms.Form
    Private WithEvents oGeneralInterface As clsGeneralInterface
    Private blnIsInvalidHL7FilePath As Boolean

    'Dim odb As New gloStream.gloDataBase.gloDataBase
    'Dim odt As New DataTable
    'Dim dtExams As New DataTable
    Dim oCurDoc As Wd.Document
    ' Dim oWordApp As Wd.Application

    '''''For Age combobox
    Dim FOR_ALL As String = "For All"
    Dim FOR_AGE As String = "For Age"
    Dim FOR_LESSTHAN_AGE As String = "Less Than"
    Dim FOR_GREATERTHAN_AGE As String = "Greater Than"
    Dim FROMTO_AGE As String = "Between"
    '''''
    Dim ObjWord As clsWordDocument
    Dim objCriteria As DocCriteria

    ''Sandip Darade 20090310
    Private mFromDate As DateTime = Now
    Private mToDate As DateTime = Now
    Private mIsfromRptviewer As Boolean = False
    Private _PatientID As Long
    Dim oPrint As New clsPrintFAX
    Dim oToolTip As New System.Windows.Forms.ToolTip
    Private dsProviders As DataSet
    Private WithEvents _RxBusinessLayer As RxBusinesslayer
    Public Property FromDate() As DateTime
        Get
            Return mFromDate
        End Get
        Set(ByVal value As DateTime)
            mFromDate = value
        End Set
    End Property
    Public Property ToDate() As DateTime
        Get
            Return mToDate
        End Get
        Set(ByVal value As DateTime)
            mToDate = value
        End Set
    End Property

    Public Property IsfromRptviewer() As Boolean
        Get
            Return mIsfromRptviewer
        End Get
        Set(ByVal value As Boolean)
            mIsfromRptviewer = value
        End Set
    End Property

    Private Sub frmPrintFAXExam_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If Me.ParentForm IsNot Nothing Then
            CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
            CType(Me.ParentForm, MainMenu).ActiveDSO = wdExam
        End If
    End Sub
    Private Sub frmPrintFAXExam_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(oToolTip) = False) Then
            oToolTip.Dispose()
            oToolTip = Nothing
        End If

        If (IsNothing(oPrint) = False) Then
            oPrint.Dispose()
            oPrint = Nothing
        End If

        '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
        If (IsNothing(dsProviders) = False) Then
            dsProviders.Dispose()
            dsProviders = Nothing
        End If


        'If (IsNothing(odb) = False) Then
        '    odb.Dispose()
        '    odb = Nothing
        'End If
        'If (IsNothing(odt) = False) Then
        '    odt.Dispose()
        '    odt = Nothing
        'End If
        'If (IsNothing(dtExams) = False) Then
        '    dtExams.Dispose()
        '    dtExams = Nothing
        'End If


        oCurDoc = Nothing

        ObjWord = Nothing

        objCriteria = Nothing
        If (IsNothing(Me.ParentForm) = False) Then
            CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
        End If
    End Sub

    Private Sub frmPrintFAXExam_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1grdExams)

        Try
            btnMore.Text = "More >>"
            pnlDrugDiagnosis.Visible = False
            'tblbtn_CreateHL7Files_32.Visible = False
            pnlSearch.Visible = True
            rdbSelectedPatient.Checked = True
            rdbAllPatients.Checked = False
            GroupBox10.Visible = False
            GroupBox9.Visible = False
            cmbAgeFrom.Text = ""
            cmbAgeTo.Text = ""
            lblPrinting.Visible = False
            PrintProgress.Visible = False
            cmbProvider.Enabled = False
            Call FillDrugTree()
            ''  Call FillDiagnosis()  commented for ICD10 Implementation
            If (gblnIcd10Transition = True) Then
                rbICD10.Checked = True
            Else
                rbICD9.Checked = True
            End If
            Call Fill_Provider()

            With cmbStatus
                .Items.Clear()
                .Items.Add("All")
                .Items.Add("Finished")
                .Items.Add("Unfinished")
                .SelectedIndex = 0
            End With
            With cmbGender
                .Items.Clear()
                .Items.Add("All")
                .Items.Add("Male")
                .Items.Add("Female")
                .Items.Add("Other")
                .SelectedIndex = 0
            End With

            With cmbAge
                .Items.Clear()
                .Items.Add(FOR_ALL)
                .Items.Add(FOR_AGE)
                .Items.Add(FOR_LESSTHAN_AGE)
                .Items.Add(FOR_GREATERTHAN_AGE)
                .Items.Add(FROMTO_AGE)
            End With
            Dim i
            With cmbAgeFrom
                For i = 0 To 124
                    .Items.Add(i + 1)
                Next
            End With

            With cmbAgeTo
                For i = 0 To 124
                    .Items.Add(i + 1)
                Next
            End With
            ''Ojeswini
            Dim scheme As Cls_TabIndexSettings.TabScheme = Cls_TabIndexSettings.TabScheme.AcrossFirst
            Dim tom As New Cls_TabIndexSettings(Me)
            ' This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme)


            ''Sandip Darade 20090310
            If (mIsfromRptviewer = True) Then
                dtpicFrom.Value = System.DateTime.Now.Date
                dtpicTo.Value = System.DateTime.Now.Date
                If Not IsNothing(mFromDate) Then
                    dtpicFrom.Value = mFromDate
                    dtpicFrom.Enabled = False
                End If
                If Not IsNothing(mToDate) Then
                    dtpicTo.Value = mToDate
                    dtpicTo.Enabled = False
                End If
                btnShowExams_Click(Nothing, Nothing)
            End If

            oToolTip.SetToolTip(Me.picPriviewClose, "Close Preview")

            GroupBox1.Select()
            rdbAllPatients.Select()
            rdbSelectedPatient.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub FillDrugTree()


        Dim i
        Try
            Dim rootnode As myTreeNode = New myTreeNode("Drugs", -1)
            rootnode.ForeColor = Color.Black
            trvDrugs.Nodes.Add(rootnode)
            _RxBusinessLayer = New RxBusinesslayer(_PatientID)
            Dim dt As DataTable = _RxBusinessLayer.FillControls(1)

            If Not IsNothing(dt) Then
                For i = 0 To dt.Rows.Count - 1
                    Dim mychildnode As myTreeNode
                    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
                    mychildnode.NodeName = CType(dt.Rows(i)(3), String) & " Refills"
                    mychildnode.IsNarcotics = CType(dt.Rows(i)(4), Int16) 'to store if the drug is a narcotic drug or not
                    If mychildnode.IsNarcotics = 1 Then
                        mychildnode.ImageIndex = 1
                        mychildnode.SelectedImageIndex = 1
                    Else
                        mychildnode.ImageIndex = 0
                        mychildnode.SelectedImageIndex = 0
                    End If
                    mychildnode.ForeColor = Color.Black
                    trvDrugs.Nodes.Item(0).Nodes.Add(mychildnode)
                Next
                dt.Dispose()
                dt = Nothing
            End If
            trvDrugs.ExpandAll()
            trvDrugs.SelectedNode = trvDrugs.Nodes.Item(0)
            trvDrugs.EndUpdate()
        Catch ex As Exception
            Throw ex
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub FillDiagnosis()
        Dim odb As New gloStream.gloDataBase.gloDataBase

        Dim _strSQL As String = ""
        Try
            If (rbICD9.Checked = True) Then
                _strSQL = "select Distinct sICD9Code,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>'' AND Isnull(nICDRevision,9)=9"


            Else
                _strSQL = "select Distinct sICD9Code,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>'' AND Isnull(nICDRevision,9)=10"


            End If
            ''   _strSQL = "select Distinct sICD9Code,(isNull(sICD9Code,'') + ' : ' + isNull(sICD9Description,'')) as sICD9Display, (isNull(sICD9Code,'') + ' ' + isNull(sICD9Description,'')) as sICD9Values from ExamICD9CPT Where  sICD9Code <>'' AND sICD9Description<>''"

            odb.Connect(GetConnectionString)
            Dim odt As DataTable = odb.ReadQueryDataTable(_strSQL)

            With chkDiagnosis
                .DataSource = odt
                .DisplayMember = odt.Columns("sICD9Display").ColumnName.ToString 'Medication description
                .ValueMember = odt.Columns("sICD9Values").ColumnName

                'If odt.Rows.Count > 0 Then
                '    .SelectedIndex = 0
                'End If
            End With
        Catch ex As Exception
            Throw ex
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            odb.Disconnect()
            odb.Dispose()
        End Try
    End Sub

    Public Sub Fill_Provider()

        '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception


        Try
            With cmbProvider
                '.Items.Clear()
                '.Items.Add("All")

                Dim objProvider As New clsProvider
                'Dim clProviders As Collection = objProvider.Fill_Providers

                dsProviders = objProvider.Fill_ProvidersDS

                objProvider.Dispose()
                objProvider = Nothing

                .DataSource = dsProviders.Tables("ProviderName")
                .ValueMember = "nProviderID"
                .DisplayMember = "ProviderFullName"

                'If (IsNothing(clProviders) = False) Then
                '    Dim nCount As Int16
                '    For nCount = 1 To clProviders.Count
                '        .Items.Add(clProviders.Item(nCount))
                '    Next
                '    clProviders.Clear()
                'End If

                If Trim(gstrLoginProviderName) <> "" Then
                    .Text = gstrLoginProviderName
                Else
                    If (.Items.Count > 0) Then
                        .SelectedIndex = 0
                    End If

                End If
            End With
        Catch ex As Exception

            Throw ex
        End Try

    End Sub

    Private Conn As SqlConnection
    Private Dv As DataView
    'Private Cmd As System.Data.SqlClient.SqlCommand


    Private Sub SetGridStyle(ByVal dt As DataTable)         ''''' For Selected Patient
        With C1grdExams
            .Visible = False
            .DataSource = dt.DefaultView
            .Cols.Count = 16

            .AllowEditing = True
            .Width = .Width - 20

            .Cols(0).Width = 80 '.Width * 0.07
            .Cols(0).AllowEditing = True
            .Cols(0).DataType = System.Type.GetType("System.Boolean")
            .Cols(0).Name = "Select"
            .SetData(0, 0, "Select")
            .Cols(0).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(1).Width = .Width * 0
            .Cols(1).AllowEditing = False

            .Cols(2).Width = .Width * 0.2
            .SetData(0, 2, "Patient Name")
            .Cols(2).AllowEditing = False

            .Cols(3).Width = .Width * 0.25
            .SetData(0, 3, "Exam Name")
            .Cols(3).AllowEditing = False

            .Cols(4).Width = 100 '.Width * 0.15
            .SetData(0, 4, "DOS")
            .Cols(4).AllowEditing = False

            .Cols(5).Width = 0  '' VisitID
            .Cols(6).Width = 0  '' PatientID
            .Cols(7).Width = 0  '' Patient Code
            .Cols(8).Width = 0  '' DOB
            .Cols(9).Width = 0  '' ProviderID

            .Cols(10).Width = 150 '.Width * 0.3
            .SetData(0, 10, "Provider Name")
            .Cols(10).AllowEditing = False

            .Cols(11).Width = 80 '.Width * 0.1
            .SetData(0, 11, "Finished")
            .Cols(11).DataType = System.Type.GetType("System.string")
            .Cols(11).AllowEditing = False

            .Cols(12).Width = 0 '50 '.Width * 0.1
            .SetData(0, 12, "Age")
            .Cols(12).AllowEditing = False

            .Cols(13).Width = 0 '60 '.Width * 0.1
            .SetData(0, 13, "Gender")
            .Cols(13).AllowEditing = False

            .Cols(14).Width = 0            '.Width * 0.35
            .SetData(0, 14, "Medication")
            .Cols(14).AllowEditing = False

            .Cols(15).Width = 0
            .Cols(15).AllowEditing = False

            .Visible = True
            .Refresh()
        End With
    End Sub

    Private Sub SetGridStyleAllPatient(ByVal dt As DataTable)     ''''' For All Patients
        With C1grdExams
            .Visible = False
            .DataSource = dt.DefaultView
            .Cols.Count = 16

            .AllowEditing = True
            .Width = .Width - 20

            .Cols(0).Width = 80
            .Cols(0).AllowEditing = True
            .Cols(0).DataType = System.Type.GetType("System.Boolean")
            .Cols(0).Name = "Select"
            .SetData(0, 0, "Select")
            .Cols(0).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(1).Width = .Width * 0
            .Cols(1).AllowEditing = False

            .Cols(2).Width = .Width * 0.2
            .SetData(0, 2, "Patient Name")
            .Cols(2).AllowEditing = False

            .Cols(3).Width = .Width * 0.25
            .SetData(0, 3, "Exam Name")
            .Cols(3).AllowEditing = False

            .Cols(4).Width = 100
            .SetData(0, 4, "DOS")
            .Cols(4).AllowEditing = False

            .Cols(5).Width = 0  '' VisitID
            .Cols(6).Width = 0  '' PatientID
            .Cols(7).Width = 0  '' Patient Code
            .Cols(8).Width = 0  '' DOB
            .Cols(9).Width = 0  '' ProviderID

            .Cols(10).Width = 150 '.Width * 0.3
            .SetData(0, 10, "Provider Name")
            .Cols(10).AllowEditing = False

            .Cols(11).Width = 80 '.Width * 0.1
            .SetData(0, 11, "Finished")
            .Cols(11).DataType = System.Type.GetType("System.string")
            .Cols(11).AllowEditing = False

            ''Sandip Darade  20100118 age and gender made hidden
            .Cols(12).Width = 0 '50 '.Width * 0.1
            .SetData(0, 12, "Age")
            .Cols(12).AllowEditing = False

            .Cols(13).Width = 0 '60 '.Width * 0.1
            .SetData(0, 13, "Gender")
            .Cols(13).AllowEditing = False


            .Cols(14).Width = 0 '.Width * 0.35
            .SetData(0, 14, "Medication")
            .Cols(14).AllowEditing = False

            .Cols(15).Width = 0
            .Cols(15).AllowEditing = False

            .Visible = True
            .Refresh()
        End With

    End Sub

    Private Sub btnShowExams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowExams.Click, ToolStripButton2.Click

        '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
        Dim strFinishStatus As String = ""
        Dim intProvidersID As Int64
        Dim strDiagnosisICD9 As String = ""
        Dim bAllPatient As Boolean

        Dim nAgeFrom As Integer = 0
        Dim nAgeTo As Integer = 0
        Dim sAgeRange = ""
        Dim sAgeFrom = ""
        Dim sAgeTo = ""
        Dim sAgeType = 0

        Dim tempGender As String = ""

        Try
            If rdbAllPatients.Checked = True Then
                bAllPatient = True
            Else
                bAllPatient = False
            End If
            If bAllPatient = True Then

                strFinishStatus = cmbStatus.Text

                '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
                intProvidersID = cmbProvider.SelectedValue

                tempGender = cmbGender.Text
                sAgeRange = cmbAge.Text

                ''For No Age Mentioned
                If cmbAge.Text = "" Then
                    sAgeType = 0
                    nAgeFrom = 0
                    nAgeTo = 0

                    '' for particular Age
                ElseIf cmbAge.Text = FOR_AGE Then
                    nAgeFrom = CType(cmbAgeFrom.Text, Integer)
                    sAgeType = 1
                    If cmbAgeFrom.Text.Trim = "" Then
                        MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbAgeFrom.Focus()
                        Exit Sub
                    End If
                    ' for less than Given Age
                ElseIf cmbAge.Text = FOR_LESSTHAN_AGE Then
                    nAgeFrom = CType(cmbAgeFrom.Text, Integer)
                    sAgeType = 2
                    If cmbAgeFrom.Text.Trim = "" Then
                        MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbAgeFrom.Focus()
                        Exit Sub
                    End If
                    ' for Greater than Given Age
                ElseIf cmbAge.Text = FOR_GREATERTHAN_AGE Then
                    nAgeFrom = CType(cmbAgeFrom.Text, Integer)
                    sAgeType = 3
                    If cmbAgeFrom.Text.Trim = "" Then
                        MessageBox.Show("Please select the Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbAgeFrom.Focus()
                        Exit Sub
                    End If
                    ' for given age range
                ElseIf cmbAge.Text = FROMTO_AGE Then
                    sAgeType = 4
                    ' select the From and To age
                    If cmbAgeFrom.Text.Trim = "" Then
                        MessageBox.Show("Please select From Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbAgeFrom.Focus()
                        Exit Sub
                    End If
                    If cmbAgeTo.Text.Trim = "" Then
                        MessageBox.Show("Please select To Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cmbAgeTo.Focus()
                        Exit Sub
                    End If
                    nAgeFrom = CType(cmbAgeFrom.Text, Integer)
                    nAgeTo = CType(cmbAgeTo.Text, Integer)
                    If nAgeFrom > nAgeTo Then
                        MessageBox.Show(" From-age should be less than To-Age ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cmbAgeFrom.Focus()
                        Exit Sub
                    End If
                End If

                'if  blnAllPatient = False
            Else
                strFinishStatus = cmbStatus.Text
                'strProvidersName = cmbProvider.Text
                '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
                intProvidersID = cmbProvider.SelectedValue

                nAgeFrom = 0
                nAgeTo = 0
                sAgeType = 0
            End If

            'Medication
            Dim strDia As String = ""
            Dim strLabResult As String = ""
            Dim i As Integer
            chkMedication.Refresh()
            With chkMedication
                ' collect the selected data of check list
                For i = 0 To .CheckedItems.Count - 1
                    .SelectedItem = .CheckedItems(i)   '
                    If i = 0 Then
                        strDia = "'" & .Text & "'"
                    Else
                        strDia = strDia & ", '" & .Text & "'"
                    End If
                Next
            End With

            'Diagnosis
            Dim sbDiagnosis As New StringBuilder
            With chkDiagnosis
                ' collect the selected data of check list
                For i = 0 To .CheckedItems.Count - 1
                    .SelectedItem = .CheckedItems(i)
                    'If i = 0 Then
                    '    sbDiagnosis.Append("'")
                    '    sbDiagnosis.Append(CType(.CheckedItems.Item(i), System.Data.DataRowView).Row.ItemArray(2))
                    '    sbDiagnosis.Append("'")
                    'Else
                    '    sbDiagnosis.Append(",")
                    '    sbDiagnosis.Append("'")
                    '    sbDiagnosis.Append(CType(.CheckedItems.Item(i), System.Data.DataRowView).Row.ItemArray(2))
                    '    sbDiagnosis.Append("'")
                    'End If
                    If i = 0 Then
                        ''Sandip Darade 20090618
                        ''code commented to avoid error for single quote
                        'sbDiagnosis.Append("'")
                        sbDiagnosis.Append(CType(.CheckedItems.Item(i), System.Data.DataRowView).Row.ItemArray(2))
                        'sbDiagnosis.Append("'")
                    Else
                        sbDiagnosis.Append(",")
                        'sbDiagnosis.Append("'")
                        sbDiagnosis.Append(CType(.CheckedItems.Item(i), System.Data.DataRowView).Row.ItemArray(2))
                        'sbDiagnosis.Append("'")
                    End If
                Next
            End With
            'strDia = ""
            strDiagnosisICD9 = sbDiagnosis.ToString
            '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
            ShowCustomizeExams(_PatientID, tempGender, sAgeType, nAgeFrom, nAgeTo, strDia, strFinishStatus, intProvidersID, strDiagnosisICD9, bAllPatient)

            '' call to save the all parameter used for the view report
            Call setPrevReportData()

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, "Patient's print fax report viewed..", ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient's print fax report viewed..", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub ShowCustomizeExams(ByVal PatientID As Long, Optional ByVal Gender As String = "", Optional ByVal AgeType As Integer = 0, Optional ByVal AgeFrom As Integer = 0, Optional ByVal AgeTo As Integer = 0, Optional ByVal strMedication As String = "", Optional ByVal strFinishStatus As String = "", Optional ByVal ProviderID As Int64 = 0, Optional ByVal strDiagnosisICD9 As String = "", Optional ByVal bAllPatient As Boolean = False)

        Try
            If dtpicFrom.Value.Date.Date > dtpicTo.Value.Date.Date Then
                MessageBox.Show("Invalid Date Criteria, 'From date' should be less than or equal to 'To date'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else
                '' dtExams = objExams.Fill_AllExams_AllPatients1(dtpicFrom.Value.Date.Date, dtpicTo.Value.Date.AddDays(1).Date, , , Gender, AgeType, AgeFrom, AgeTo, strMedication, strFinishStatus, strProvidersName, strDiagnosisICD9, bAllPatient)
                '' Above line commented by Sandip Darade 20100111
                ''Bug ID 5697 exams thr selected dates will be shown only now 
                Dim objExams As New clsPatientExams_rpt
                '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
                Dim dtExams As DataTable = objExams.Fill_AllExams_AllPatients1(dtpicFrom.Value.Date.Date, dtpicTo.Value.Date.Date, PatientID, , , Gender, AgeType, AgeFrom, AgeTo, strMedication, strFinishStatus, ProviderID, strDiagnosisICD9, bAllPatient)
                objExams.Dispose()
                objExams = Nothing
                If (IsNothing(dtExams) = False) Then
                    If dtExams.Rows.Count > 0 Then
                        If rdbAllPatients.Checked = True Then
                            SetGridStyleAllPatient(dtExams)            '''' For All Patients
                        Else
                            SetGridStyle(dtExams)                      '''' For Selected Patient
                        End If
                    Else
                        Dim dt As New DataTable
                        SetGridStyle(dt)
                    End If
                Else
                    Dim dt As New DataTable
                    SetGridStyle(dt)
                End If


            End If

            SelectClearAll(False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Query, ex.ToString(), ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub SelectClearAll(Optional ByVal blnSelect As Boolean = True)
        With C1grdExams
            For i As Integer = 1 To .Rows.Count - 1
                .Rows(i)(0) = blnSelect
            Next
        End With
        'Shubhangi 
        'For Bug 5925 
        If blnSelect = False Then
            tblbtn_SelectAll_32.Image = Global.gloEMR.My.Resources.Resources.Select_All1
            tblbtn_SelectAll_32.ImageAlign = ContentAlignment.MiddleCenter
            tblbtn_SelectAll_32.Text = "Select &All"
        ElseIf blnSelect = True Then
            tblbtn_SelectAll_32.Image = Global.gloEMR.My.Resources.Resources.Clear_All1
            tblbtn_SelectAll_32.ImageAlign = ContentAlignment.MiddleCenter
            tblbtn_SelectAll_32.Text = "Clear &All"
        End If

    End Sub

    Private Sub setPrevReportData()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim _strSQL As String = ""
        Dim i As Integer
        Dim strDia As String = ""

        _strSQL = "DELETE FROM ReportPrintFaxExams"
        oDB.Connect(GetConnectionString)
        oDB.ExecuteNonSQLQuery(_strSQL)

        With chkMedication
            For i = 0 To .CheckedItems.Count - 1
                .SelectedItem = .CheckedItems(i)   '
                If i = 0 Then
                    strDia = .Text
                Else
                    strDia = strDia & "|" & .Text
                End If
            Next
        End With

        Dim bCheckAllPatient
        bCheckAllPatient = 0

        Dim selectedNode As String = ""
        Try
            _strSQL = " Insert Into ReportPrintFaxExams( dtFromDate, dtToDate, sGender, nAge, nAgeFrom, nAgeTo, sMedication, nSearchCrieteria, nDatewise) " _
                    & " VALUES( '" & dtpicFrom.Text & "' , '" & dtpicTo.Text & "' , '" & cmbGender.Text & "' , '" & cmbAge.Text & "' ,  '" & cmbAgeFrom.Text & "' ,  '" & cmbAgeTo.Text & "' , '" & strDia.Replace("'", "''") & "' , ' " & Trim(bCheckAllPatient) & "' , ' " & Trim(selectedNode) & " ')"

            oDB.ExecuteNonSQLQuery(_strSQL)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.None, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing
    End Sub

    Private Sub btnMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMore.Click, tsbtnMore.Click
        If rdbAllPatients.Checked = False Then
            GroupBox9.Visible = False
            GroupBox10.Visible = False
        Else
            cmbAge.Text = "For All"
            cmbAgeFrom.Visible = False
            cmbAgeTo.Visible = False
            GroupBox9.Visible = True
            GroupBox10.Visible = True
        End If
        If btnMore.Text = "More >>" Then
            If pnlDrugDiagnosis.Visible = False Then
                pnlDrugDiagnosis.Visible = True
                tsbtnMore.Text = " Hide "
                tsbtnMore.Image = Global.gloEMR.My.Resources.Resources.Hide
                tsbtnMore.ImageAlign = ContentAlignment.MiddleCenter
                cmbStatus.Select()
            Else
                pnlDrugDiagnosis.Visible = False
                tsbtnMore.Text = " More "
                tsbtnMore.Image = Global.gloEMR.My.Resources.Resources.Show
                tsbtnMore.ImageAlign = ContentAlignment.MiddleCenter
            End If
        Else
            If pnlDrugDiagnosis.Visible = True Then
                pnlDrugDiagnosis.Visible = False
                tsbtnMore.Text = " More "
                tsbtnMore.Image = Global.gloEMR.My.Resources.Resources.Show
                tsbtnMore.ImageAlign = ContentAlignment.MiddleCenter
            Else
                pnlDrugDiagnosis.Visible = True
                tsbtnMore.Text = " Hide "
                tsbtnMore.Image = Global.gloEMR.My.Resources.Resources.Hide
                tsbtnMore.ImageAlign = ContentAlignment.MiddleCenter
            End If
        End If
    End Sub

    Private Sub tblbtn_SelectAll_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_SelectAll_32.Click
        If tblbtn_SelectAll_32.Text = "Select &All" Then
            tblbtn_SelectAll_32.Image = Global.gloEMR.My.Resources.Resources.Clear_All1
            tblbtn_SelectAll_32.ImageAlign = ContentAlignment.MiddleCenter
            tblbtn_SelectAll_32.Text = "Clear &All"
            SelectClearAll()
        Else
            tblbtn_SelectAll_32.Image = Global.gloEMR.My.Resources.Resources.Select_All1
            tblbtn_SelectAll_32.ImageAlign = ContentAlignment.MiddleCenter
            tblbtn_SelectAll_32.Text = "Select &All"
            SelectClearAll()
            SelectClearAll(False)
        End If
    End Sub

    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click
        If Me.Cursor Is Cursors.Default Then
            If pnlDSO.Visible = True Then
                Priview_Close()
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnAddToSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSearch.Click
        Dim myTNode As TreeNode

        myTNode = trvDrugs.Nodes.Item(0)
        If Not trvDrugs.SelectedNode Is myTNode Then
            If chkMedication.Items.Contains(trvDrugs.SelectedNode.Text) = False Then
                chkMedication.Items.Add(trvDrugs.SelectedNode.Text, True)
            End If
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        chkMedication.Items.Remove(chkMedication.SelectedItem())
    End Sub

    Private Sub cmbAge_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAge.TextChanged
        If cmbAge.Text = FOR_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If

            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FOR_LESSTHAN_AGE Then
            'Shubhangi 
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FOR_GREATERTHAN_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FROMTO_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeTo.Visible = True
            lblAgeFrom.Text = "From"
            lblAgeTo.Text = "To"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = True
        Else
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            cmbAgeFrom.Visible = False
            cmbAgeTo.Visible = False
            lblAgeFrom.Visible = False
            lblAgeTo.Visible = False
        End If
    End Sub

    Private Sub rdbAllPatients_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbAllPatients.CheckedChanged
        If rdbAllPatients.Checked = False Then
            rdbAllPatients.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            GroupBox9.Visible = False
            GroupBox10.Visible = False
            cmbProvider.Enabled = False
        Else
            rdbAllPatients.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            cmbAge.Text = "For All"
            cmbAgeFrom.Visible = False
            cmbAgeTo.Visible = False
            GroupBox9.Visible = True
            GroupBox10.Visible = True
            cmbProvider.Enabled = True

            'code start by nilesh on 20110528 for case GLO2011-0011069
            'clear all records
            Dim dt As New DataTable
            SetGridStyle(dt)
            'dt.Dispose()
            'dt = Nothing
            'code end by nilesh on 20110528 for case GLO2011-0011069
        End If
    End Sub

    Private Sub tblbtn_Print_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Print_32.Click

        Dim COL_ExamIDs As New Collection
        Me.Cursor = Cursors.WaitCursor
        Try
            If C1grdExams.Rows.Count > 1 Then
                ''''' For Selected Patient
                ''''' 0-Select, 1-ExamID,2-DOS, 3-ExamName, 4-VisitID, 5-DOB, 6-ProviderID, 7-ProviderName , 8-bIsFinished

                '' For All Patients
                '' 0-Select,1-ExamID,2-PatientName ,3-ExamName,4-DOS,5-VisitID,6-PatientID,7-PatientCode,8-DOB,9-ProviderID,10-ProviderName, 11-bIsFinished 

                PrintProgress.Visible = True
                lblPrinting.Visible = True
                With C1grdExams
                    For i As Integer = 1 To .Rows.Count - 1
                        If .Rows(i)(0) = True Then
                            '' Add Selected ExamIDs & Its Status (Finish/UnFinish) to Collection

                            '' Problem 00000028 : When printing from Reports Exams print/fax the reports do not have footers on them
                            '' Patient ID added in the Parameter
                            If rdbAllPatients.Checked = False Then
                                '' For Selected Patient                                
                                COL_ExamIDs.Add(.Rows(i)(1) & "|" & .Rows(i)(8) & "|" & .Rows(i)(6))
                            Else
                                '' For All Patients                                
                                COL_ExamIDs.Add(.Rows(i)(1) & "|" & .Rows(i)(11) & "|" & .Rows(i)(6))
                            End If
                        End If
                    Next
                End With

                '' if Exam is Not selected 
                If COL_ExamIDs.Count < 1 Then
                    MessageBox.Show("You have not selected any Exam to Print. Please select atleast one exam.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                '******Shweta 20090828 *********'
                'To check exeception related to word
                'If CheckWordForException() = False Then
                '    Exit Sub
                'End If
                'End Shweta


                With PrintProgress
                    lblPrinting.Visible = True

                    If COL_ExamIDs.Count > 100 Then
                        .Maximum = COL_ExamIDs.Count
                    Else
                        .Maximum = 100
                    End If

                    .Value = 0
                    .Step = .Maximum / COL_ExamIDs.Count

                    For i As Integer = 1 To COL_ExamIDs.Count
                        If (oPrint.IsCancle = True) Then
                            oPrint.IsCancle = False
                            Exit For
                        End If
                        lblPrinting.Text = "Printing " & i & " of " & COL_ExamIDs.Count.ToString
                        Application.DoEvents()

                        If .Value + .Step >= .Maximum Then
                            PrintProgress.Value = .Maximum
                        Else
                            PrintProgress.Value = .Value + .Step
                        End If

                        .Update()
                        Dim objIDs() As String
                        objIDs = CStr(COL_ExamIDs(i)).Split("|")
                        Dim blnShowDialouge As Boolean
                        If (i = 1) Then
                            blnShowDialouge = True
                        Else
                            blnShowDialouge = False
                        End If
                        '' Problem 00000028 : When printing from Reports Exams print/fax the reports do not have footers on them
                        '' Patient ID added in the Parameter
                        If objIDs(1) = "Yes" Then
                            Fill_ExamContents(Long.Parse(objIDs(0)), True, , , Long.Parse(objIDs(2)), , blnShowDialouge)
                        Else
                            Fill_ExamContents(Long.Parse(objIDs(0)), False, , , Long.Parse(objIDs(2)), , , blnShowDialouge)
                        End If
                    Next

                    .Value = 0
                End With
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, "Patient's print fax  exam report Printed..", ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Patient's print fax  exam report Printed..", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                MessageBox.Show("No Exam for Print", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            COL_ExamIDs = Nothing
            PrintProgress.Visible = False
            lblPrinting.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function Fill_ExamContents(ByVal ExamID As Long, Optional ByVal blnExamFinished As Boolean = False, Optional ByVal blnFAX As Boolean = False, Optional ByVal ExamName As String = "", Optional ByVal PatientID As Long = 0, Optional ByVal VisitID As Long = 0, Optional ByVal blnBatchFax As Boolean = 0, Optional ByVal blnshowDialouge As Boolean = True)
        'Dim objExamContents As Object
        'Dim objclsPatientExams As New clsPatientExams_rpt
        '''''' Get Exam Contents of selected Exam
        'objExamContents = objclsPatientExams.GetExamContents(ExamID)
        'objclsPatientExams = Nothing
        ''If the contents are nothing then Exam will be print or FAXed
        'If IsNothing(objExamContents) Then Exit Sub

        'Dim mstream As ADODB.Stream
        'mstream = New ADODB.Stream
        'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        'mstream.Open()
        'mstream.Write(objExamContents)

        'Dim strFileName As String
        'strFileName = ExamNewDocumentName
        'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
        'mstream.Close()

        'wdExam.Close()

        Dim strFileName As String
        ObjWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Exam
        objCriteria.PrimaryID = ExamID
        ObjWord.DocumentCriteria = objCriteria
        strFileName = ObjWord.RetrieveDocumentFile()
        objCriteria.Dispose()
        objCriteria = Nothing
        ObjWord = Nothing
        If (rdbSelectedPatient.Checked = True) And (blnBatchFax = True) Then
            Return strFileName
        End If
        'If (IsNothing(strFileName) = False) Then


        '    '  wdExam.Open(strFileName)
        '    Dim oWordApp As Wd.Application = Nothing
        '    gloWord.LoadAndCloseWord.OpenDSO(wdExam, strFileName, oCurDoc, oWordApp)

        '    oCurDoc = wdExam.ActiveDocument
        '    '' Print Exam
        '    If blnFAX = False Then
        '        '' Print Exam
        '        If blnExamFinished = False Then
        '            'ReplaceFieldsTemp()
        '            'RelpaceNavigationIconsTemp()
        '            'If blnHPIEnabled = True Then
        '            '    CleanupHPITemp()
        '            'End If
        '            'ObjWord = New clsWordDocument
        '            'ObjWord.CurDocument = oCurDoc
        '            'ObjWord.CleanupDoc()
        '            'oCurDoc = ObjWord.CurDocument
        '            'ObjWord = Nothing
        '            gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
        '        End If
        '        'Dim oPrint As New clsPrintFAX
        '        oPrint.PrintDoc(oCurDoc, PatientID, blnshowDialouge)
        '        'oPrint = Nothing
        '    Else

        '        'wdTemp.Open(strFileName)

        '        ''----------
        '        '' wdTemp.Activate()
        '        'If wdTemp.ActiveDocument.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '        '    wdTemp.ActiveDocument.Application.ActiveDocument.Unprotect()
        '        'End If

        '        'Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        '        'UpdateLog("Object Created - Retrieveing FAX Details")
        '        'mdlFAX.Owner = Me
        '        'If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID) = False Then
        '        '    Exit Sub
        '        'End If
        '        'If objPrintFAX.FAXDocument(wdTemp.ActiveDocument, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, ExamName, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
        '        '    If Trim(objPrintFAX.ErrorMessage) <> "" Then
        '        '        MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        '    End If
        '        'End If
        '        'objPrintFAX = Nothing
        '        'wdTemp.Close()

        '        'wdExam.Activate()
        '        'wdExam.Select()
        '        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        '        'Send the document for Printing i.e. to generate the TIFF File
        '        UpdateVoiceLog("FaxExam is started")
        '        UpdateVoiceLog("Creating the object of Class")

        '        UpdateVoiceLog("Object Created - Retrieveing FAX Details")
        '        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        '        'mdlFAX.Owner = Me
        '        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID, True, Me) = False Then
        '            Fill_ExamContents = Nothing
        '            objPrintFAX.Dispose()
        '            objPrintFAX = Nothing
        '            Exit Function
        '        End If
        '        UpdateVoiceLog("FAX Details - FAX No, FAX To, Cover Page retrieved")

        '        'Commented by Shweta 20100201
        '        '''''''Against the bug id:5260 '''''''
        '        'Check the FAX Cover Page is enabled or not.
        '        ''If the FAX Cover Page is enabled then Delete the Page Header from Exam
        '        'If gblnFAXCoverPage Then
        '        '    ''Unprotect the document
        '        '    If blnExamFinished Then
        '        '        UpdateVoiceLog("Unprotecting Document in FaxExam")
        '        '        If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '        '            oCurDoc.Application.ActiveDocument.Unprotect()
        '        '            UpdateVoiceLog("Document UnProtected in FaxExam")
        '        '        Else
        '        '            UpdateVoiceLog("Already Document is Unprotected in FaxExam")
        '        '        End If

        '        '    End If
        '        'To Delete Header
        '        'UpdateVoiceLog("Deleting Exam Page Header")
        '        'Try

        '        '    If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '        '        oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        '    End If
        '        '    oCurDoc.Activate()
        '        '    oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader
        '        '    If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
        '        '        oCurDoc.Application.Selection.HeaderFooter.Range.Select()
        '        '        oCurDoc.Application.Selection.HeaderFooter.Range.Delete()
        '        '        UpdateVoiceLog("Exam Page Header deleted")
        '        '    End If

        '        'Catch ex As Exception
        '        '    UpdateVoiceLog("Error Deleting Exam Page Header - " & ex.ToString)
        '        'Finally
        '        '    oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '        'End Try
        '        'End If
        '        'End Commenting

        '        UpdateVoiceLog("Calling FAX Document method")
        '        Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
        '        Dim myApplication As Wd.Application = myLoadWord.SetWordApplication(oCurDoc.Application)

        '        If objPrintFAX.FAXDocument(myLoadWord, oCurDoc, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, lblExamName.Text, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
        '            If Trim(objPrintFAX.ErrorMessage) <> "" Then
        '                UpdateVoiceLog("Unable to send the FAX due to " & objPrintFAX.ErrorMessage)

        '                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            End If
        '        Else
        '            UpdateVoiceLog("Exam is Faxed")
        '        End If
        '        myLoadWord.ResetWordApplication(myApplication)
        '        myLoadWord = Nothing

        '        objPrintFAX.Dispose()
        '        objPrintFAX = Nothing
        '    End If
        '    wdExam.Close()
        '    oCurDoc = Nothing
        'End If
        '  wdExam.Open(strFileName)

        '' Print Exam
        If blnFAX = False Then
            ' '' Print Exam
            'Dim oWordApp As Wd.Application = Nothing
            'gloWord.LoadAndCloseWord.OpenDSO(wdExam, strFileName, oCurDoc, oWordApp)

            'oCurDoc = wdExam.ActiveDocument
            'If blnExamFinished = False Then
            '    'ReplaceFieldsTemp()
            '    'RelpaceNavigationIconsTemp()
            '    'If blnHPIEnabled = True Then
            '    '    CleanupHPITemp()
            '    'End If
            '    'ObjWord = New clsWordDocument
            '    'ObjWord.CurDocument = oCurDoc
            '    'ObjWord.CleanupDoc()
            '    'oCurDoc = ObjWord.CurDocument
            '    'ObjWord = Nothing
            '    gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
            'End If
            ''Dim oPrint As New clsPrintFAX
            'oPrint.PrintDoc(oCurDoc, PatientID, blnshowDialouge)
            'wdExam.Close()
            'oCurDoc = Nothing
            ''oPrint = Nothing

            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            Try
                PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, strFileName, True, PatientID, Nothing, 0, blnshowDialouge, iOwner:=Me)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing
        Else

            'wdTemp.Open(strFileName)

            ''----------
            '' wdTemp.Activate()
            'If wdTemp.ActiveDocument.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            '    wdTemp.ActiveDocument.Application.ActiveDocument.Unprotect()
            'End If

            'Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
            'UpdateLog("Object Created - Retrieveing FAX Details")
            'mdlFAX.Owner = Me
            'If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID) = False Then
            '    Exit Sub
            'End If
            'If objPrintFAX.FAXDocument(wdTemp.ActiveDocument, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, ExamName, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
            '    If Trim(objPrintFAX.ErrorMessage) <> "" Then
            '        MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If
            'End If
            'objPrintFAX = Nothing
            'wdTemp.Close()

            'wdExam.Activate()
            'wdExam.Select()
            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
            'Send the document for Printing i.e. to generate the TIFF File
            UpdateVoiceLog("FaxExam is started")
            UpdateVoiceLog("Creating the object of Class")

            UpdateVoiceLog("Object Created - Retrieveing FAX Details")
            Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
            'mdlFAX.Owner = Me
            If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID, True, Me) = False Then
                Fill_ExamContents = Nothing
                objPrintFAX.Dispose()
                objPrintFAX = Nothing
                Exit Function
            End If
            UpdateVoiceLog("FAX Details - FAX No, FAX To, Cover Page retrieved")

            'Commented by Shweta 20100201
            '''''''Against the bug id:5260 '''''''
            'Check the FAX Cover Page is enabled or not.
            ''If the FAX Cover Page is enabled then Delete the Page Header from Exam
            'If gblnFAXCoverPage Then
            '    ''Unprotect the document
            '    If blnExamFinished Then
            '        UpdateVoiceLog("Unprotecting Document in FaxExam")
            '        If oCurDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
            '            oCurDoc.Application.ActiveDocument.Unprotect()
            '            UpdateVoiceLog("Document UnProtected in FaxExam")
            '        Else
            '            UpdateVoiceLog("Already Document is Unprotected in FaxExam")
            '        End If

            '    End If
            'To Delete Header
            'UpdateVoiceLog("Deleting Exam Page Header")
            'Try

            '    If oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
            '        oCurDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
            '    End If
            '    oCurDoc.Activate()
            '    oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader
            '    If oCurDoc.Application.Selection.HeaderFooter.IsHeader Then
            '        oCurDoc.Application.Selection.HeaderFooter.Range.Select()
            '        oCurDoc.Application.Selection.HeaderFooter.Range.Delete()
            '        UpdateVoiceLog("Exam Page Header deleted")
            '    End If

            'Catch ex As Exception
            '    UpdateVoiceLog("Error Deleting Exam Page Header - " & ex.ToString)
            'Finally
            '    oCurDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
            'End Try
            'End If
            'End Commenting

            UpdateVoiceLog("Calling FAX Document method")
            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            'Dim myApplication As Wd.Application = myLoadWord.SetWordApplication(oCurDoc.Application)
            ' Dim oTempDoc = myLoadWord.LoadWordApplication(strFileName)
            If objPrintFAX.FAXDocument(myLoadWord, strFileName, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, lblExamName.Text, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
                If Trim(objPrintFAX.ErrorMessage) <> "" Then
                    UpdateVoiceLog("Unable to send the FAX due to " & objPrintFAX.ErrorMessage)

                    MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                UpdateVoiceLog("Exam is Faxed")
            End If
            '  myLoadWord.ResetWordApplication(myApplication)
            'myLoadWord.CloseWordApplication(oTempDoc)
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing

            objPrintFAX.Dispose()
            objPrintFAX = Nothing
        End If

        Return Nothing
    End Function



    Private Sub FaxExam(ByRef oTempDoc As Wd.Document, ByRef _PatientId As Int64)

    End Sub

    'Procedure to replace field names if fields are blank for print & fax

    Private Sub ReplaceFieldsTemp()
        Dim i As Integer
        For i = 1 To wdExam.ActiveDocument.FormFields.Count
            If wdExam.ActiveDocument.FormFields.Item(i).Type = Wd.WdFieldType.wdFieldFormTextInput Then
                If wdExam.ActiveDocument.FormFields.Item(i).StatusText = wdExam.ActiveDocument.FormFields.Item(i).Result Then
                    wdExam.ActiveDocument.FormFields.Item(i).Result = ""
                End If
            End If
        Next
    End Sub

    'Procedure to replace navigation icons for print & fax

    Private Sub RelpaceNavigationIconsTemp()

        'wdExam.ActiveDocument.Application.Selection.Find.ClearFormatting()
        'wdExam.ActiveDocument.Application.Selection.Find.Replacement.ClearFormatting()
        'Try
        '    With wdExam.ActiveDocument.Application.Selection.Find
        '        .Text = "[]"
        '        .Replacement.Text = " "
        '        .Forward = True
        '        .Wrap = Wd.WdFindWrap.wdFindContinue
        '        .Format = False
        '        .MatchCase = False
        '        .MatchWholeWord = False
        '        .MatchWildcards = False
        '        .MatchSoundsLike = False
        '        .MatchAllWordForms = False
        '    End With
        '    wdExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceAll)
        'Catch ex As Exception
        Try

            gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdExam.ActiveDocument.Application, FindText:="[]", ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceAll, MatchWildCards:=False, MatchWholeWord:=False)
        Catch ex2 As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex2.ToString(), ActivityOutCome.Failure)
            ex2 = Nothing
        End Try
        'End Try



    End Sub

    ''''-- Procedure to Clean up the HPI Contents for printing

    Private Sub CleanupHPITemp()
        Dim strFindText As String
        Dim i As Integer
        Dim strHPIValues As ArrayList
        Dim blnValue As Boolean
        strHPIValues = New ArrayList
        ' UpdateLog("Cleanup started ")
        For i = 1 To wdExam.ActiveDocument.FormFields.Count
            If wdExam.ActiveDocument.FormFields.Item(i).Type = Wd.WdFieldType.wdFieldFormCheckBox Then
                If wdExam.ActiveDocument.FormFields.Item(i).StatusText <> "" Then
                    strHPIValues.Add(wdExam.ActiveDocument.FormFields.Item(i).Name)  ' aField.HelpText & "|" & aField.StatusText)
                End If
            End If
        Next

        For i = 0 To strHPIValues.Count - 1 ' To ocurdoc1.Application.ActiveDocument.FormFields.Count
            strFindText = wdExam.ActiveDocument.FormFields.Item(strHPIValues(i)).StatusText
            blnValue = wdExam.ActiveDocument.FormFields.Item(strHPIValues(i)).CheckBox.Value
            wdExam.ActiveDocument.FormFields.Item(strHPIValues(i)).Delete()
            wdExam.ActiveDocument.Application.Selection.Range.Start = wdExam.ActiveDocument.Application.Selection.Start
            If blnValue = False Then
                Try
                    'With wdExam.ActiveDocument.Application.Selection.Find
                    '    .Text = strFindText
                    '    .Replacement.Text = ""
                    '    .Forward = True
                    '    .Wrap = Wd.WdFindWrap.wdFindContinue
                    '    .Format = False
                    '    .MatchCase = False
                    '    .MatchWholeWord = True
                    '    .MatchWildcards = False
                    '    .MatchSoundsLike = False
                    '    .MatchAllWordForms = False
                    '    .Forward = True
                    'End With
                    wdExam.ActiveDocument.Application.Selection.Collapse(Direction:=Wd.WdCollapseDirection.wdCollapseEnd)
                    '    wdExam.ActiveDocument.Application.Selection.Find.Execute(Replace:=Wd.WdReplace.wdReplaceOne)
                    'Catch ex As Exception
                    '    Try

                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=wdExam.ActiveDocument.Application, FindText:=strFindText, ReplaceWith:="", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceOne, MatchWildCards:=False, MatchWholeWord:=True)
                Catch ex2 As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex2.ToString(), ActivityOutCome.Failure)
                    ex2 = Nothing
                End Try
                'End Try


                wdExam.ActiveDocument.Application.Selection.TypeBackspace()
            End If
        Next
        strFindText = Nothing
        i = Nothing
        strHPIValues = Nothing
        blnValue = Nothing
        '  UpdateLog("Cleanup done ")
    End Sub

    Private Sub tblbtn_FAX_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_FAX_32.Click
        Dim COL_ExamIDs As New Collection
        Me.Cursor = Cursors.WaitCursor
        Try
            If C1grdExams.Rows.Count > 1 Then
                ' For Selected Patient
                ''''' 0-Select, 1-ExamID,2-DOS, 3-ExamName, 4-VisitID, 5-DOB, 6-ProviderID, 7-ProviderName , 8-bIsFinished

                '' For All Patients
                '' 0-Select,1-ExamID,2-PatientName ,3-ExamName,4-DOS,5-VisitID,6-PatientID,7-PatientCode,8-DOB,9-ProviderID,10-ProviderName, 11-bIsFinished 

                With C1grdExams
                    For i As Integer = 1 To .Rows.Count - 1
                        If .Rows(i)(0) = True Then
                            '' Add Selected ExamIDs to Collection
                            'If chkAllPatients.Checked = False Then
                            If rdbAllPatients.Checked = False Then
                                ' For Selected Patient
                                '               ExamID             IsFinish            Exam Name            VisitID              PatientID
                                COL_ExamIDs.Add(.Rows(i)(1) & "|" & .Rows(i)(8) & "|" & .Rows(i)(3) & "|" & .Rows(i)(4) & "|" & .Rows(i)(6))
                            Else
                                '' For All Patients
                                '''''              ExamID              IsFinish            Exam Name           VisitID           Patient ID    
                                COL_ExamIDs.Add(.Rows(i)(1) & "|" & .Rows(i)(11) & "|" & .Rows(i)(3) & "|" & .Rows(i)(5) & "|" & .Rows(i)(6))
                            End If
                        End If
                    Next
                End With

                '
                ' FAX Only Previewed Exam
                With C1grdExams
                    If pnlDSO.Visible = True Then
                        '' Clear Collection
                        For i As Integer = COL_ExamIDs.Count To 1 Step -1
                            COL_ExamIDs.Remove(i)
                        Next

                        '' Add Selected ExamIDs to Collection
                        'If chkAllPatients.Checked = False Then
                        If rdbAllPatients.Checked = False Then
                            ' For Selected Patient
                            '             ExamID             IsFinish            Exam Name            VisitID
                            COL_ExamIDs.Add(.Rows(.RowSel)(1) & "|" & .Rows(.RowSel)(8) & "|" & .Rows(.RowSel)(3) & "|" & .Rows(.RowSel)(4) & "|" & .Rows(.RowSel)(6))
                        Else
                            '' For All Patients
                            '''''              ExamID              IsFinish            Exam Name           VisitID
                            COL_ExamIDs.Add(.Rows(.RowSel)(1) & "|" & .Rows(.RowSel)(11) & "|" & .Rows(.RowSel)(3) & "|" & .Rows(.RowSel)(5) & "|" & .Rows(.RowSel)(6))
                        End If
                    End If
                End With
                '

                '' if Exam is Not selected Select
                If COL_ExamIDs.Count < 1 Then
                    MessageBox.Show("You have not selected any Exam for FAX. Please select atleast one exam.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'By Shweta 20090829
                'If CheckWordForException() = False Then
                '    Exit Sub
                'End If
                'End Shweta

                With PrintProgress
                    lblPrinting.Visible = True
                    PrintProgress.Visible = True
                    If COL_ExamIDs.Count > 100 Then
                        .Maximum = COL_ExamIDs.Count
                    Else
                        .Maximum = 100
                    End If

                    .Step = .Maximum / COL_ExamIDs.Count
                    .Value = 0
                    Dim strBatchFile As String = ""
                    Dim PatientID As Long
                    Dim FirstVisitID As Long
                    Dim FirstExamID As Long
                    Dim myLoadWord As gloWord.LoadAndCloseWord = Nothing
                    Try
                        For i As Integer = 1 To COL_ExamIDs.Count


                            lblPrinting.Text = "FAXing " & i & " of " & COL_ExamIDs.Count.ToString
                            Application.DoEvents()

                            If .Value + .Step >= .Maximum Then
                                .Value = .Maximum
                            Else
                                .Value = .Value + .Step
                            End If

                            .Update()

                            Dim objIDs() As String
                            objIDs = CStr(COL_ExamIDs(i)).Split("|")
                            Dim strExamName As String = ""
                            'Dim PatientID As Long

                            Dim VisitID As Long
                            Dim ExamID As Long

                            ExamID = Long.Parse(objIDs(0))
                            strExamName = objIDs(2)
                            PatientID = objIDs(4) ' gnPatientID
                            'Dim _Visitid() As String
                            '' commented Sandip Darade 20090725
                            '  VisitID = Long.Parse(objIDs(3))
                            'Sandip Darade 20090725
                            'VisitID = GetVisitID(objIDs(3))



                            If (rdbSelectedPatient.Checked = True) Then         ''dhruv 20100226 
                                'VisitID = GetVisitID(objIDs(3))                 ''(Selected Patient)rdbselected patient is selected it contains the date so before that required the conversion from the visitID
                                '
                                VisitID = GetVisitID(objIDs(3), PatientID)
                            Else
                                VisitID = Long.Parse(objIDs(3))                 ''(All Patient)There is no need of the conversion 
                            End If                                              ''end--------dhruv

                            If (rdbSelectedPatient.Checked = False) Then
                                If objIDs(1) = "Yes" Then
                                    Fill_ExamContents(ExamID, True, True, strExamName, PatientID, VisitID)
                                Else
                                    Fill_ExamContents(ExamID, False, True, strExamName, PatientID, VisitID)
                                End If

                            Else 'else case added for batch faxing of exam
                                If (i = 1) Then
                                    strBatchFile = Fill_ExamContents(ExamID, False, True, strExamName, PatientID, VisitID, True, True)
                                    FirstVisitID = VisitID
                                    FirstExamID = ExamID
                                    myLoadWord = New gloWord.LoadAndCloseWord()

                                    oCurDoc = myLoadWord.LoadWordApplication(strBatchFile)
                                    'bLoaded = True
                                Else
                                    Dim strTempBatchFile As String
                                    'Dim oCurDoc1 As Wd.Document
                                    strTempBatchFile = Fill_ExamContents(ExamID, False, True, strExamName, PatientID, VisitID, True, False)
                                    'open in DSO strBatchFile
                                    'wdTemp.Open(strBatchFile)

                                    'oCurDoc = wdTemp.ActiveDocument
                                    oCurDoc.ActiveWindow.SetFocus()
                                    oCurDoc.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                                    'oCurDoc.Application.Selection.InsertNewPage()
                                    oCurDoc.Application.Selection.InsertBreak()

                                    oCurDoc.Application.Selection.InsertFile(strTempBatchFile)
                                    oCurDoc.Save()

                                    ' oCurDoc.SaveAs(strBatchFile)
                                    'wdTemp.Close()

                                End If
                            End If 'end of code added for batch faxing of exam

                            'code start by nilesh on 20110528 for case GLO2011-0011069
                            'comment below code which sends each fax twice
                            'If objIDs(1) = "Yes" Then
                            '    Fill_ExamContents(ExamID, True, True, strExamName, PatientID, VisitID, True, True)
                            'Else
                            '    Fill_ExamContents(ExamID, False, True, strExamName, PatientID, VisitID, True, True)
                            'End If
                            'code end by nilesh on 20110528 for case GLO2011-0011069
                        Next
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
                        ex = Nothing
                    End Try

                    'below if condition added by nilesh on 20110528 for case GLO2011-0011069
                    'when criteria is on selected patient then only batch fax should call
                    If (IsNothing(myLoadWord) = False) Then
                        Try
                            myLoadWord.CloseWordOnly(oCurDoc)
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
                            ex = Nothing
                        End Try
                        myLoadWord.CloseApplicationOnly()
                        myLoadWord = Nothing
                    End If

                    If (rdbSelectedPatient.Checked) Then
                        Fill_ExamContents_batch(FirstExamID, False, True, "Exams", PatientID, FirstVisitID, strBatchFile)
                    End If

                    .Value = 0

                End With
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Fax, "Patient's print fax report fax sent.", ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Fax, "Patient's print fax report fax sent.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                MessageBox.Show("No Exam for FAX", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, "Patient's print fax report viewed..", ActivityOutCome.Success)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally
            COL_ExamIDs = Nothing
            PrintProgress.Visible = False
            lblPrinting.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tblbtn_Preview_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Preview_32.Click
        Try
            If C1grdExams.Rows.Count > 1 Then
                If C1grdExams.RowSel > 0 Then
                    C1grdExams.Visible = False
                    pnlDSO.Dock = DockStyle.Fill

                    'By Shweta 20090829
                    If CheckWordForException() = False Then
                        Exit Sub
                    End If
                    'End Shweta
                    pnlSearch.Visible = False
                    tblStrip_32.Visible = False
                    tblbtn_Preview_32.Enabled = False
                    tblbtn_SelectAll_32.Enabled = False

                    lblExamName.Text = " Exam Name:  " & C1grdExams.Rows(C1grdExams.RowSel)(3) '' Exam Name
                    If C1grdExams.Rows(C1grdExams.RowSel)(8).ToString = "Yes" Then
                        Call PreviewExam(C1grdExams.Rows(C1grdExams.RowSel)(1), True)             '' ExamID
                    Else
                        Call PreviewExam(C1grdExams.Rows(C1grdExams.RowSel)(1), False)            '' ExamID
                    End If

                    pnlDSO.Visible = True
                    SetWordObject(False)
                End If
                'gloAuditTrail.CreateAuditLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, "Patient's print fax report viewed..", ActivityOutCome.Success)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, ActivityCategory.None, ActivityType.None, "Patient's print fax report viewed..", ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient's print fax report viewed..", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                MessageBox.Show("No Exam for Preview", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, ActivityCategory.None, ActivityType.None, "Patient's print fax report viewed..", ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient's print fax report viewed..", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.View, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub PreviewExam(ByVal nPastExamId As Long, ByVal IsFinished As Boolean)
        Try
            Dim dsData As DataSet
            Dim clsExam As New clsPatientExams_rpt
            dsData = clsExam.GetPastExams(nPastExamId)
            clsExam.Dispose()
            clsExam = Nothing
            If (IsNothing(dsData)) Then
                Exit Sub
            End If
            Dim mstream As ADODB.Stream
            Dim strFileName As String
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.Write(dsData.Tables(0).Rows(0)(0))
            strFileName = ExamNewDocumentName

            wdExam.Close()
            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            mstream.Close()
            mstream = Nothing
            ' wdExam.Open(strFileName)
            Dim oWordApp As Wd.Application = Nothing
            Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdExam, strFileName, oCurDoc, oWordApp)
            If (strError <> String.Empty) Then
                MessageBox.Show(strError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                oCurDoc = wdExam.ActiveDocument

                'code start by nilesh on 20110528
                'To Clean up the Document for removing FormFields and Tags that doesn't contain data
                'ObjWord = New clsWordDocument
                'ObjWord.CurDocument = oCurDoc
                'ObjWord.CleanupDoc()
                'oCurDoc = ObjWord.CurDocument
                'ObjWord = Nothing
                gloWord.LoadAndCloseWord.CleanupDoc(oCurDoc)
                'code end by nilesh on 20110528

                oCurDoc.ActiveWindow.View.WrapToWindow = True
                oCurDoc.ActiveWindow.View.Type = Wd.WdViewType.wdPrintView
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SetWordObject(ByVal IsFinished As Boolean)
        ''UpdateLog("SetWordObject ")
        Try
            oCurDoc = wdExam.ActiveDocument
        Catch ex As Exception

        End Try
        If (IsNothing(oCurDoc)) Then
            Return
        End If
        '   oWordApp = oCurDoc.Application

        With oCurDoc.ActiveWindow.Application
            .CommandBars("Standard").Enabled = False
            .CommandBars("Standard").Visible = False

            .CommandBars("Formatting").Enabled = False
            .CommandBars("Formatting").Visible = False

            .CommandBars("Forms").Enabled = False
            .CommandBars("Forms").Visible = False

            .CommandBars("Web").Enabled = False
            .CommandBars("Web").Visible = False

            .CommandBars("Forms").Enabled = False
            .CommandBars("Forms").Visible = False

            .CommandBars("Control Toolbox").Enabled = False
            .CommandBars("Control Toolbox").Visible = False

            .CommandBars("Database").Enabled = False
            .CommandBars("Database").Visible = False

            .CommandBars("E-mail").Enabled = False
            .CommandBars("E-mail").Visible = False

            .CommandBars("Frames").Enabled = False
            .CommandBars("Frames").Visible = False

            .CommandBars("Mail Merge").Enabled = False
            .CommandBars("Mail Merge").Visible = False

            .CommandBars("Outlining").Enabled = False
            .CommandBars("Outlining").Visible = False

            .CommandBars("Visual Basic").Enabled = False
            .CommandBars("Visual Basic").Visible = False

            .CommandBars("Web Tools").Enabled = False
            .CommandBars("Web Tools").Visible = False

            .CommandBars("WordArt").Enabled = False
            .CommandBars("WordArt").Visible = False

            'code start by nilesh on 20110528
            'below code added to display values in liquid links 
            Application.DoEvents()
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            'code end by nilesh on 20110528

            oCurDoc.ActiveWindow.View.WrapToWindow = True
            oCurDoc.ActiveWindow.View.Type = Wd.WdViewType.wdWebView

            If oCurDoc.Application.ActiveDocument.ProtectionType <> Wd.WdProtectionType.wdAllowOnlyComments Then
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
                '   Timer1.Enabled = True
                '  Timer1.Interval = 10
            End If
        End With
        'UpdateLog("SetWordObject - E N D ")
    End Sub

    Private Sub picPriviewClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPriviewClose.Click
        Call Priview_Close()
    End Sub

    Sub Priview_Close()
        C1grdExams.Visible = True
        C1grdExams.Dock = DockStyle.Fill
        pnlDSO.Visible = False
        tblStrip_32.Visible = True
        pnlSearch.Visible = True
        tblbtn_Preview_32.Enabled = True
        tblbtn_SelectAll_32.Enabled = True
        wdExam.Close()
    End Sub



    Private Sub txtSearchDiagnosis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchDiagnosis.KeyPress
        If e.KeyChar = ChrW(13) Then
            chkDiagnosis.Focus()
        Else
            'chkDiagnosis.SetItemChecked(chkDiagnosis.SelectedIndex, True)
        End If

    End Sub

    Private Sub txtSearchDiagnosis_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchDiagnosis.TextChanged
        Try
            If rdbDiagnosisCode.Checked = True Then
                For i As Integer = 0 To chkDiagnosis.Items.Count - 1
                    With chkDiagnosis
                        Dim str As String
                        Dim drv As DataRowView
                        Dim str1 As String
                        Dim arrstring() As String
                        drv = CType(.Items(i), DataRowView)
                        str = UCase(drv(1))
                        arrstring = Split(str, ":")
                        If IsNothing(arrstring(0)) Then
                            txtSearchDiagnosis.Focus()
                            Exit Sub
                        Else
                            str1 = arrstring(0)
                            If Mid(Trim(str1), 1, Len(Trim(txtSearchDiagnosis.Text))) = UCase(Trim(txtSearchDiagnosis.Text)) Then
                                chkDiagnosis.SelectedItem = chkDiagnosis.Items(i)
                                txtSearchDiagnosis.Focus()
                                Exit Sub
                            End If
                        End If
                    End With
                Next
            ElseIf rdbDiagnosisDesc.Checked = True Then
                For i As Integer = 0 To chkDiagnosis.Items.Count - 1
                    With chkDiagnosis
                        Dim str As String
                        Dim drv As DataRowView
                        Dim str1 As String
                        Dim arrstring() As String
                        drv = CType(.Items(i), DataRowView)
                        str = UCase(drv(1))
                        arrstring = Split(str, ":")
                        If IsNothing(arrstring(1)) Then
                            txtSearchDiagnosis.Focus()
                            Exit Sub
                        Else
                            str1 = arrstring(1)
                            If Mid(Trim(str1), 1, Len(Trim(txtSearchDiagnosis.Text))) = UCase(Trim(txtSearchDiagnosis.Text)) Then
                                chkDiagnosis.SelectedItem = chkDiagnosis.Items(i)
                                txtSearchDiagnosis.Focus()
                                Exit Sub
                            End If
                        End If
                    End With
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Search, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub

    Private Sub txtSearchDrug_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchDrug.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvDrugs.Select()
        Else
            trvDrugs.SelectedNode = trvDrugs.Nodes.Item(0)
        End If
    End Sub

    Private Sub txtSearchDrug_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchDrug.TextChanged
        Try
            If Len(Trim(txtSearchDrug.Text)) <= 1 Then
                If txtSearchDrug.Tag <> Trim(txtSearchDrug.Text) Then
                    AddDrugs(1, Trim(txtSearchDrug.Text))
                    txtSearchDrug.Tag = Trim(txtSearchDrug.Text)
                End If
            End If

            Dim mychildnode As myTreeNode
            'child node collection
            For Each mychildnode In trvDrugs.Nodes.Item(0).Nodes
                Dim str As String
                str = UCase(Splittext(mychildnode.Tag))
                If Mid(str, 1, Len(Trim(txtSearchDrug.Text))) = UCase(Trim(txtSearchDrug.Text)) Then
                    If Not IsNothing(trvDrugs.SelectedNode) Then
                        If Not IsNothing(trvDrugs.SelectedNode.LastNode) Then
                            trvDrugs.SelectedNode = trvDrugs.SelectedNode.LastNode
                        End If
                    End If
                    trvDrugs.SelectedNode = mychildnode
                    txtSearchDrug.Focus()
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Search, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, "-")
            Return arrstring(0)
        Else
            Return ""
        End If
    End Function

    Private Sub AddDrugs(ByVal DrugType As Int16, Optional ByVal strsearch As String = "")
        Try
            trvDrugs.Visible = False
            If trvDrugs.GetNodeCount(False) > 0 Then
                trvDrugs.Nodes.Item(0).Remove()
                Dim objmytreenode As New myTreeNode
                objmytreenode.Text = "Drugs"
                objmytreenode.ForeColor = Color.Blue

                trvDrugs.Nodes.Add(objmytreenode)
            End If
            trvDrugs.Visible = True
            _RxBusinessLayer = New RxBusinesslayer(_PatientID)
            Dim dt As DataTable = _RxBusinessLayer.FillControls(DrugType, strsearch)
            If (IsNothing(dt) = False) Then
                Dim i As Integer

                trvDrugs.BeginUpdate()
                For i = 0 To dt.Rows.Count - 1
                    Dim mychildnode As myTreeNode
                    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0), CType(dt.Rows(i)(2), String))
                    mychildnode.NodeName = CType(dt.Rows(i)(3), String) & " Refills"
                    mychildnode.IsNarcotics = CType(dt.Rows(i)(4), Int16) 'to store if the drug is a narcotic drug or not

                    If mychildnode.IsNarcotics = 1 Then
                        mychildnode.ImageIndex = 1
                        mychildnode.SelectedImageIndex = 1
                    Else
                        mychildnode.ImageIndex = 0
                        mychildnode.SelectedImageIndex = 0
                    End If
                    mychildnode.ForeColor = Color.Blue
                    trvDrugs.Nodes.Item(0).Nodes.Add(mychildnode)

                Next

                trvDrugs.EndUpdate()

                dt.Dispose()
                dt = Nothing
            End If
            trvDrugs.ExpandAll()
        Catch ex As Exception
            trvDrugs.EndUpdate()
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.None, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        'btnMore.Text = "More >>"
        cmbAgeFrom.Text = ""
        cmbAgeTo.Text = ""
        cmbStatus.Text = "All"
        cmbProvider.Text = "All"
        cmbGender.Text = "All"
        cmbAge.Text = "For All"
        txtSearchDiagnosis.Text = ""
        txtSearchDrug.Text = ""
        dtpicFrom.Value = Now.Date
        dtpicTo.Value = Now.Date
        If tsbtnMore.Text = " Hide " Then
            pnlDrugDiagnosis.Visible = True
            'pnlSearch.Visible = False
        Else
            pnlDrugDiagnosis.Visible = False
            'pnlSearch.Visible = True
        End If
        rdbSelectedPatient.Checked = True
        rdbAllPatients.Checked = False
        rdbDiagnosisCode.Checked = True
        rdbDiagnosisDesc.Checked = False
        rbICD9.Checked = True
        chkDiagnosis.ClearSelected()
        Dim i As Int32
        For i = 0 To chkDiagnosis.Items.Count - 1
            chkDiagnosis.SetItemChecked(i, False)
        Next
        chkMedication.ClearSelected()
        For i = 0 To chkMedication.Items.Count - 1
            chkMedication.SetItemChecked(i, False)
        Next
        Dim dt As New DataTable
        SetGridStyle(dt)
    End Sub

    Private Sub chkDiagnosis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles chkDiagnosis.KeyPress
        'If e.KeyChar = ChrW(13) Then
        '    chkDiagnosis.Focus()
        'Else
        '    chkDiagnosis.SetItemChecked(chkDiagnosis.SelectedIndex, True)
        'End If
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<Ojeswini03032008>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    'To give radio btn Bold and Regular effect>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub rdbDiagnosisCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDiagnosisCode.CheckedChanged
        If rdbDiagnosisCode.Checked = True Then
            rdbDiagnosisCode.Font = gloGlobal.clsgloFont.gFont_BOLD ' New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbDiagnosisCode.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbDiagnosisDesc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDiagnosisDesc.CheckedChanged
        If rdbDiagnosisDesc.Checked = True Then
            rdbDiagnosisDesc.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rdbDiagnosisDesc.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rdbSelectedPatient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSelectedPatient.CheckedChanged

        If rdbSelectedPatient.Checked = True Then
            cmbProvider.Enabled = False
            rdbSelectedPatient.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

            'code start by nilesh on 20110528 for case GLO2011-0011069
            'clear all records
            Dim dt As New DataTable
            SetGridStyle(dt)
            'dt.Dispose()
            'dt = Nothing
            'code end by nilesh on 20110528 for case GLO2011-0011069
        Else
            cmbProvider.Enabled = True
            rdbSelectedPatient.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    'To give btn Hover and Leave image>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowExams.MouseHover, btnReset.MouseHover, btnRemove.MouseHover, btnMore.MouseHover, btnAddToSearch.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowExams.MouseLeave, btnReset.MouseLeave, btnRemove.MouseLeave, btnMore.MouseLeave, btnAddToSearch.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<Ojeswini03032008>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    Private Sub chkDiagnosis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDiagnosis.SelectedIndexChanged

    End Sub

    Private Sub cmbGender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbGender.SelectedIndexChanged

    End Sub

    Private Sub C1grdExams_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1grdExams.MouseDown

        Try
            If (e.Button = Windows.Forms.MouseButtons.Right) Then
                cmnu_Diagnosis.Items.Clear()
                'Try
                '    If (IsNothing(C1grdExams.ContextMenuStrip) = False) Then
                '        C1grdExams.ContextMenuStrip.Dispose()
                '        C1grdExams.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1grdExams.ContextMenuStrip = cmnu_Diagnosis
                C1grdExams.Row = C1grdExams.HitTest(e.X, e.Y).Row

                If C1grdExams.Row > 0 Then
                    If C1grdExams.Rows.Count > 1 Then
                        If Not IsNothing(CType(C1grdExams.GetData(C1grdExams.RowSel, 1), Int64)) Then  ''Exam ID
                            Dim oMenuItem_ViewDiagnosis As New ToolStripMenuItem
                            oMenuItem_ViewDiagnosis.Text = "View Diagnosis"
                            oMenuItem_ViewDiagnosis.Image = imgList_Common.Images(0)
                            oMenuItem_ViewDiagnosis.ForeColor = Color.FromArgb(31, 73, 125)
                            cmnu_Diagnosis.Items.Add(oMenuItem_ViewDiagnosis)
                            AddHandler oMenuItem_ViewDiagnosis.Click, AddressOf oMenuItem_ViewDiagnosis_Click
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub oMenuItem_ViewDiagnosis_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            '' SUDHIR 20090605 ''
            Dim _PatientID As Int64 = CType(C1grdExams.GetData(C1grdExams.Row, 6), Int64)
            Dim _ExamID As Int64 = CType(C1grdExams.GetData(C1grdExams.Row, 1), Int64)
            Dim _VisitID As Int64 = CType(C1grdExams.GetData(C1grdExams.Row, 5), Int64)
            Dim _DOS As DateTime = CType(C1grdExams.GetData(C1grdExams.Row, 4), DateTime)
            Dim _ExamName As String = C1grdExams.GetData(C1grdExams.Row, 3)

            ShowDiagnosis(_PatientID, _ExamID, _VisitID, _DOS, _ExamName)
            '' END SUDHIR ''
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub
    Private Sub C1grdExams_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1grdExams.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Private Function Generatefile()
        Dim strName As String = ""

        Try
            If C1grdExams.Rows.Count > 1 Then
                oGeneralInterface = New clsGeneralInterface
                Dim intSelectedRows As Int32 = 0
                oGeneralInterface.TotalRecords = 0
                'Shubhangi 20091110
                Dim oDiagnosis As New ClsDiagnosisDBLayer
                Dim blnExamSelected As Boolean = False
                For icnt As Int32 = 1 To C1grdExams.Rows.Count - 1
                    If C1grdExams.GetCellCheck(icnt, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        blnExamSelected = True
                        Exit For
                    End If
                Next
                If blnExamSelected = False Then
                    MessageBox.Show("Please Select atleast one Exam", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    oDiagnosis.Dispose()
                    oDiagnosis = Nothing
                    Return strName
                    Exit Function
                End If

                For icnt As Int32 = 1 To C1grdExams.Rows.Count - 1

                    If C1grdExams.GetCellCheck(icnt, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        Dim intExamId As Int64
                        Dim intPatientID As Int64
                        Dim strIsFinished As Boolean
                        intSelectedRows = intSelectedRows + 1
                        intExamId = C1grdExams.GetData(icnt, 1)
                        intPatientID = C1grdExams.GetData(icnt, 6)
                        strIsFinished = C1grdExams.GetData(icnt, 11)
                        oGeneralInterface.DOS = C1grdExams.GetData(icnt, 4) ''S

                        'code updated by kanchan on 20091202 for checking HL7 setting is on then only insert in queue
                        If gblnHL7SENDOUTBOUNDGLOEMR = True Then 'AndAlso gblnSaveandClose = True 'Removed as per workflow, User want to generate this as on demand
                            'Shubhangi 20091110
                            If oDiagnosis.IsICD9CPT_Present(intExamId, intPatientID) Then
                                'only generate charges for unfinished exam
                                oGeneralInterface.SendCharges(intExamId, intPatientID)
                                'code commented by kanchan on 20091202 for genius & Hl7 work simultaneously as case 3176
                                'If blnIsInvalidHL7FilePath = True Then
                                '    MessageBox.Show("Please Set a valid HL7 File Path from gloEMRAdmin", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '    blnIsInvalidHL7FilePath = False
                                '    Exit Function
                                'End If
                            End If
                            'End Shubhangi

                            'If strName.Length = 0 Then

                            '    strName = "'" & C1grdExams.GetData(icnt, 1) & "'"
                            'Else
                            '    strName = strName & "," & "'" & C1grdExams.GetData(icnt, 1) & "'"
                            'End If

                        End If
                    End If
                Next
                If gblnHL7SENDOUTBOUNDGLOEMR = True Then 'AndAlso gblnSaveandClose = True ' Removed as per workflow, User want to generate this as on demand
                    If intSelectedRows > 0 Then
                        If oGeneralInterface.TotalRecords = 0 Then
                            MessageBox.Show("Charges not Added to HL7 Message Queue for any of the selected exams as charges information is not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf oGeneralInterface.TotalRecords <> 0 AndAlso oGeneralInterface.TotalRecords < intSelectedRows Then
                            MessageBox.Show("Charges not Added to HL7 Message Queue for some of the selected exams as charges information is not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf oGeneralInterface.TotalRecords = intSelectedRows Then
                            MessageBox.Show("Charges Added to HL7 Message Queue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                    'code Commented by kanchan on 20091202 for checking HL7 setting is on then only insert in queue
                    'Else
                    '    If intSelectedRows > 0 Then
                    '        If oGeneralInterface.TotalRecords = 0 Then
                    '            MessageBox.Show("Charges not posted for any of the selected exams as charges information is not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        ElseIf oGeneralInterface.TotalRecords <> 0 AndAlso oGeneralInterface.TotalRecords < intSelectedRows Then
                    '            MessageBox.Show("Charges not posted for some of the selected exams as charges information is not available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        ElseIf oGeneralInterface.TotalRecords = intSelectedRows Then
                    '            MessageBox.Show("Charges posted successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        End If
                    '    End If
                End If
                oDiagnosis.Dispose()
                oDiagnosis = Nothing

            End If

            Return strName


        Catch ex As Exception
            Throw ex
        Finally
            If Not IsNothing(oGeneralInterface) Then
                oGeneralInterface.Dispose()
                oGeneralInterface = Nothing
            End If
        End Try
    End Function
    Private Sub oGeneralInterface_InvalidFilePath() Handles oGeneralInterface.InvalidFilePath
        blnIsInvalidHL7FilePath = True
    End Sub

    Private Sub tblbtnSendCharges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtnSendCharges.Click
        Try
            If gblnHL7SENDOUTBOUNDGLOEMR = True Then ' AndAlso gblnSaveandClose = True ' Removed as per workflow, User want to generate this as on demand
                'code commented by kanchan on 20091202 since we not use this path now for HL7
                'If gblnSendChargesToHL7 = True Then
                '    
                'If gstrHL7MessagePath = "" Then
                '    MessageBox.Show("Please set HL7 File Path from gloEMRAdmin,", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                'If System.IO.Directory.Exists(gstrHL7MessagePath) = False Then
                '    MessageBox.Show("Please set valid HL7 File Path from gloEMRAdmin,", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    Exit Sub
                'End If
                'End If
                Generatefile()
            End If

        Catch ex As Exception
            MessageBox.Show("Error Posting Charges", "Print/Fax Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Print, ex.ToString(), ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub tblStrip_32_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblStrip_32.Click

    End Sub
    Private Sub Fill_ExamContents_batch(ByVal ExamID As Long, Optional ByVal blnExamFinished As Boolean = False, Optional ByVal blnFAX As Boolean = False, Optional ByVal ExamName As String = "", Optional ByVal PatientID As Long = 0, Optional ByVal VisitID As Long = 0, Optional ByVal strFile As String = "")

        If (ExamID > 0) Then


            Dim strFileName As String
            'ObjWord = New clsWordDocument
            'objCriteria = New DocCriteria
            'objCriteria.DocCategory = enumDocCategory.Exam
            'objCriteria.PrimaryID = ExamID
            'ObjWord.DocumentCriteria = objCriteria
            'strFileName = ObjWord.RetrieveDocumentFile()
            strFileName = strFile
            'objCriteria.Dispose()
            'objCriteria = Nothing
            'ObjWord = Nothing

            ''   wdExam.Open(strFileName)
            'Dim oWordApp As Wd.Application = Nothing

            'gloWord.LoadAndCloseWord.OpenDSO(wdExam, strFileName, oCurDoc, oWordApp)

            'oCurDoc = wdExam.ActiveDocument

            'CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
            ''Send the document for Printing i.e. to generate the TIFF File
            'UpdateVoiceLog("FaxExam is started")
            'UpdateVoiceLog("Creating the object of Class")
            'Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
            'UpdateVoiceLog("Object Created - Retrieveing FAX Details")
            ''mdlFAX.Owner = Me
            'If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID, True, Me) = False Then
            '    objPrintFAX.Dispose()
            '    objPrintFAX = Nothing
            '    Exit Sub
            'End If
            'UpdateVoiceLog("FAX Details - FAX No, FAX To, Cover Page retrieved")

            'UpdateVoiceLog("Calling FAX Document method")
            'Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            'Dim myApplication As Wd.Application = myLoadWord.SetWordApplication(oCurDoc.Application)
            'If objPrintFAX.FAXDocument(myLoadWord, oCurDoc, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, lblExamName.Text, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
            '    If Trim(objPrintFAX.ErrorMessage) <> "" Then
            '        UpdateVoiceLog("Unable to send the FAX due to " & objPrintFAX.ErrorMessage)

            '        MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If
            'Else
            '    UpdateVoiceLog("Exam is Faxed")
            'End If
            'myLoadWord.ResetWordApplication(myApplication)
            'myLoadWord = Nothing

            'objPrintFAX.Dispose()
            'objPrintFAX = Nothing
            ''End If
            'wdExam.Close()
            'oCurDoc = Nothing
            '   wdExam.Open(strFileName)
            'Dim oWordApp As Wd.Application = Nothing

            'gloWord.LoadAndCloseWord.OpenDSO(wdExam, strFileName, oCurDoc, oWordApp)

            'oCurDoc = wdExam.ActiveDocument

            CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
            'Send the document for Printing i.e. to generate the TIFF File
            UpdateVoiceLog("FaxExam is started")
            UpdateVoiceLog("Creating the object of Class")
            Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
            UpdateVoiceLog("Object Created - Retrieveing FAX Details")
            'mdlFAX.Owner = Me
            If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(PatientID), "", "", ExamName, 0, VisitID, ExamID, True, Me) = False Then
                objPrintFAX.Dispose()
                objPrintFAX = Nothing
                Exit Sub
            End If
            UpdateVoiceLog("FAX Details - FAX No, FAX To, Cover Page retrieved")

            UpdateVoiceLog("Calling FAX Document method")
            'wdExam.Close()
            'oCurDoc = Nothing

            Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
            '   Dim myApplication As Wd.Application = myLoadWord.SetWordApplication(oCurDoc.Application)
            ' Dim tempDoc As Wd.Document = myLoadWord.LoadWordApplication(strFileName)

            If objPrintFAX.FAXDocument(myLoadWord, strFileName, CStr(PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, lblExamName.Text, clsPrintFAX.enmFAXType.PatientExam, Not blnExamFinished) = False Then
                If Trim(objPrintFAX.ErrorMessage) <> "" Then
                    UpdateVoiceLog("Unable to send the FAX due to " & objPrintFAX.ErrorMessage)

                    MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                UpdateVoiceLog("Exam is Faxed")
            End If
            ' myLoadWord.ResetWordApplication(myApplication)
            'myLoadWord.CloseWordApplication(tempDoc)
            myLoadWord.CloseApplicationOnly()
            myLoadWord = Nothing

            objPrintFAX.Dispose()
            objPrintFAX = Nothing
            'End If

            If (pnlDSO.Visible) Then
                tblbtn_Preview_32_Click(Nothing, Nothing)
            End If

        End If
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub

    Private Sub cmbAge_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAge.SelectedIndexChanged
        If cmbAge.Text = FOR_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FOR_LESSTHAN_AGE Then
            'Shubhangi 
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FOR_GREATERTHAN_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeFrom.Text = "Select"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = False
            lblAgeTo.Visible = False
        ElseIf cmbAge.Text = FROMTO_AGE Then
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            lblAgeFrom.Visible = True
            lblAgeTo.Visible = True
            lblAgeFrom.Text = "From"
            lblAgeTo.Text = "To"
            cmbAgeFrom.Visible = True
            cmbAgeTo.Visible = True
        Else
            If (cmbAgeFrom.Items.Count > 0) Then
                cmbAgeFrom.SelectedIndex = 0
            End If
            cmbAgeFrom.Visible = False
            cmbAgeTo.Visible = False
            lblAgeFrom.Visible = False
            lblAgeTo.Visible = False
        End If
    End Sub

    Private Sub rbICD9_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbICD9.CheckedChanged
        If rbICD9.Checked = True Then
            FillDiagnosis()
        End If

    End Sub

    Private Sub rbICD10_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbICD10.CheckedChanged
        If rbICD10.Checked = True Then
            FillDiagnosis()
        End If
    End Sub
End Class