Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports gloAuditTrail
Imports gloReports
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmRpt_PatientICD9CPT

#Region "Variable Decalration"
    'Dim _providerID As Int64
    Dim _ClinicID As Int64 = 0
    Dim _DefaultPrinter As Boolean = False
    Dim objPatient As New clsPatient
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _DatabaseConnectionString As String = ""
    'Private oGridListControl As gloUserControlLibrary.gloUC_GridList
    Private oProcedureListControl As gloUserControlLibrary.gloUC_GridList
    'Private _ICD9CPTModified As Boolean = False
    'Private _ICD9CPTFilling As Boolean = False
    Public Event GridListLoaded()
    Public Event GridListClosed()
    'Public Event ICD9_Inserted(ByVal oICD9 As Object)
    Dim count As Integer
    Dim strProviderIDs As String = ""
    Dim strLocations As String = ""
    Dim strICD9s As String = ""
    Dim strCPTs As String = ""
    Dim strCPTFrom As String = ""
    Dim strCPTTo As String = ""
    'Dim _rangeCPT As String = ""
    'Dim strCriteria As String = ""
    'Private Dv As DataView
    'Dim dtPatients As New DataTable
    Private oColSelectedNodes As New Collection
    'Private bParentTrigger As Boolean = True
    'Private bChildTrigger As Boolean = True
    'Private _ID As Int64
    Private arrSelectedNodeIDs As New ArrayList
    Private Col_PatientID As Int16 = 0
    Private Col_Diagnosis As Int16 = 1
    Private Col_Procedure As Int16 = 2
    Private Col_Unit As Int16 = 3
    Private Col_DOS As Int16 = 4
    Private Col_PatientCode As Int16 = 5
    Private Col_PatientName As Int16 = 6
    Private Col_Location As Int16 = 7
    Private Col_Provider As Int16 = 8
    Private Col_ExamName As Int16 = 9
    Private Col_ExamId As Int16 = 10
    Private _dtICD As DataTable = Nothing
    Public Shared IsOpen As Boolean = False
    'Dim _NodeCount As Integer


#End Region
#Region "Property Procedures"
    'Public Property ClinicID() As Int64
    '    Get
    '        Return _ClinicID
    '    End Get
    '    Set(ByVal value As Int64)
    '        _ClinicID = value
    '    End Set
    'End Property
    Public ReadOnly Property SelectedNodes() As Collection
        Get
            Return oColSelectedNodes
        End Get
    End Property
    'Public Property ID() As Int64
    '    Get
    '        Return _ID
    '    End Get
    '    Set(ByVal value As Int64)
    '        _ID = value
    '    End Set
    'End Property
    Public Property SelectedNodeIDs() As ArrayList
        Get
            Return arrSelectedNodeIDs
        End Get
        Set(ByVal value As ArrayList)
            If IsNothing(value) Then
                arrSelectedNodeIDs.Clear()
            Else
                arrSelectedNodeIDs = value
            End If
        End Set
    End Property
   
    Public Property DatabaseConnectionString() As String
        Get
            Return _DatabaseConnectionString
        End Get
        Set(ByVal value As String)
            _DatabaseConnectionString = value
        End Set
    End Property
#End Region
#Region "Constructor"
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID").ToString() <> "" Then
                _ClinicID = Convert.ToInt64(appSettings("ClinicID"))
                cmbProvider.SelectedValue = _ClinicID
            End If
        End If

        If Not IsNothing(appSettings("DefaultPrinter")) AndAlso Convert.ToString(appSettings("DefaultPrinter")) <> "" Then
            If Convert.ToBoolean(appSettings("DefaultPrinter")) Then
                _DefaultPrinter = True
            Else
                _DefaultPrinter = False
            End If
        Else
            _DefaultPrinter = False
        End If

    End Sub
#End Region
    ''Added by Mayuri:20101110-To fix issue:6080-multiple instance
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean

    Private Shared frm As frmRpt_PatientICD9CPT

    Public Shared Function GetInstance() As frmRpt_PatientICD9CPT


        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmRpt_PatientICD9CPT" Then
                    'If CType(f, frmRpt_PatientICD9CPT) = PatientID Then
                    IsOpen = True
                    frm = f
                    'End If

                End If
            Next
            If (IsOpen = False) Then
                frm = New frmRpt_PatientICD9CPT()
            End If

        Finally

        End Try
        Return frm
    End Function

#End Region
    ''
#Region "Load"
    Private Sub frmRpt_PatientICD9CPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtPAtient As DataTable
        gloC1FlexStyle.Style(C1grdPatients)

        Try
            'Default Dx by Proc radio button should checked
            RbDxbyProc.Checked = True
            'To get variables to fetch data according to selected criteria
            GetVariables()
            'To Fetch Details according to selected critera
            dtPAtient = FetchDetails()
            'To Design flexgrid in order to show Fetched details
            DesignPatientGrid(dtPAtient)
            'To fill  providers in treeview
            FillProviders()
            'To fill Patient Locations in treeview
            FillLocations()
            'To fill Diagnosis in treeview
            'FillDiagnosisTree()
            
            rbALL.Checked = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

#End Region

#Region "Fill Providers"
    ''' <summary>
    ''' To Fill Providers
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillProviders()
        Try
            'Dim rootNode As TreeNode
            Dim dtProvider As DataTable
            'Dim dr As DataRow
            Dim i As Int16
            dtProvider = objPatient.GetProviders(_ClinicID)
            If Not IsNothing(dtProvider) Then
                For i = 0 To dtProvider.Rows.Count - 1
                    Dim mychildnode As New myTreeNode
                    mychildnode.Text = Convert.ToString(dtProvider.Rows(i)("ProviderName"))
                    mychildnode.Tag = Convert.ToString(dtProvider.Rows(i)("nProviderID"))
                    mychildnode.ForeColor = Color.Black
                    trvProvider.Nodes.Add(mychildnode)
                Next
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
            trvProvider.ExpandAll()
            If trvProvider.Nodes.Count > 0 Then
                trvProvider.SelectedNode = trvProvider.Nodes.Item(0)
            End If
            trvProvider.EndUpdate()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Fill Locations"
    ''' <summary>
    ''' To fill Locations
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillLocations()
        Try
            Dim dtLocation As DataTable
            'Dim dr As DataRow
            dtLocation = objPatient.GetLocations(_ClinicID)
            Dim i As Int16

            If Not IsNothing(dtLocation) Then
                For i = 0 To dtLocation.Rows.Count - 1
                    Dim mychildnode As New myTreeNode
                    mychildnode.Text = Convert.ToString(dtLocation.Rows(i)("sLocation"))
                    mychildnode.Tag = Convert.ToString(dtLocation.Rows(i)("nLocationID"))
                    mychildnode.ForeColor = Color.Black

                    trvLocation.Nodes.Add(mychildnode)
                Next
                dtLocation.Dispose()
                dtLocation = Nothing
            End If
            trvLocation.ExpandAll()
            If trvLocation.Nodes.Count > 0 Then
                trvLocation.SelectedNode = trvLocation.Nodes.Item(0)
            End If
            trvLocation.EndUpdate()
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Fill Diagnosis Tree"
    ''' <summary>
    ''' To fill Diagnosis in treeview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDiagnosisTree(Optional ByVal ICdType As String = "9")
        Dim rootnode As myTreeNode

        Dim i As Int16
        Try
            rootnode = New myTreeNode("Diagnosis", -1)
            rootnode.ForeColor = Color.Black
            trvDiagnosis.Nodes.Add(rootnode)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            _dtICD = objPatient.FillDiagnosis("", ICdType)
            If (IsNothing(_dtICD) = False) Then
                If txtSearchDiagnosis.Text.Trim() = "" Then

                    If Not IsNothing(_dtICD) Then
                        For i = 0 To _dtICD.Rows.Count - 1

                            Dim mychildnode As New myTreeNode
                            mychildnode.Text = Convert.ToString(_dtICD.Rows(i)(0))
                            '& "-" & Convert.ToString(dt.Rows(i)(1))
                            mychildnode.Tag = _dtICD.Rows(i)(0).ToString().Trim  '' ICD9Code
                            mychildnode.DrugName = _dtICD.Rows(i)(0).ToString()   '' ICD9Code
                            'mychildnode.Dosage = dt.Rows(i)(1).ToString() '' ICD9 description
                            mychildnode.ForeColor = Color.Black
                            mychildnode.ImageIndex = 2
                            mychildnode.SelectedImageIndex = 2
                            trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                            'End If
                        Next
                        '_dtICD.Dispose()
                        '_dtICD = Nothing
                    End If
                Else
                    Dim drr As DataRow() = _dtICD.Select("sICD9code like '%" & txtSearchDiagnosis.Text & "%'")

                    For i = 0 To drr.Length - 1

                        Dim mychildnode As New myTreeNode
                        mychildnode.Text = Convert.ToString(drr(i)(0))
                        '& "-" & Convert.ToString(dt.Rows(i)(1))
                        mychildnode.Tag = drr(i)(0).ToString().Trim  '' ICD9Code
                        mychildnode.DrugName = drr(i)(0).ToString()   '' ICD9Code
                        'mychildnode.Dosage = dt.Rows(i)(1).ToString() '' ICD9 description
                        mychildnode.ForeColor = Color.Black
                        mychildnode.ImageIndex = 2
                        mychildnode.SelectedImageIndex = 2
                        trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                        'End If
                    Next

                End If
            End If
           
            trvDiagnosis.ExpandAll()
            If trvDiagnosis.Nodes.Count > 0 Then
                trvDiagnosis.SelectedNode = trvDiagnosis.Nodes.Item(0)
            End If
            trvDiagnosis.EndUpdate()
        Catch ex As Exception
            'Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region


    'Private Sub txtDiagnosis_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiagnosis.TextChanged
    '    Try
    '        Dim _strSearchString As String = ""
    '        _strSearchString = txtDiagnosis.Text
    '        OpenInternalControl(gloUserControlLibrary.gloGridListControlType.ICD9, "ICD9", "")
    '        oGridListControl.FillControl(_strSearchString)
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#Region "OpenControl"
    'Private Sub OpenInternalControl(ByVal ControlType As gloUserControlLibrary.gloGridListControlType, ByVal ControlHeader As String, ByVal SearchText As String)

    '    Try
    '        If oGridListControl IsNot Nothing Then
    '            CloseInternalControl()
    '        End If
    '        oGridListControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.ICD9, True, pnlInternalControl.Width)
    '        oGridListControl.DatabaseConnectionString = GetConnectionString()
    '        AddHandler oGridListControl.ItemSelected, AddressOf oGridListControl_ItemSelected
    '        AddHandler oGridListControl.InternalGridKeyDown, AddressOf oGridListControl_InternalGridKeyDown
    '        oGridListControl.ControlHeader = ControlHeader
    '        pnlInternalControl.Controls.Add(oGridListControl)
    '        oGridListControl.Dock = DockStyle.Fill
    '        If SearchText <> "" Then
    '            oGridListControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
    '        End If
    '        oGridListControl.Show()
    '        pnlInternalControl.Visible = True
    '        RaiseEvent GridListLoaded()
    '        pnlInternalControl.BringToFront()

    '    Catch ex As Exception

    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try

    'End Sub
#End Region
#Region "Open Procedure Control"
    Private Sub OpenProcedureControl(ByVal ControlType As gloUserControlLibrary.gloGridListControlType, ByVal ControlHeader As String, ByVal SearchText As String)

        Try
            If oProcedureListControl IsNot Nothing Then
                CloseProcedureControl()
            End If
            oProcedureListControl = New gloUserControlLibrary.gloUC_GridList(gloUserControlLibrary.gloGridListControlType.Procedures, True, pnlInternalControl.Width)
            oProcedureListControl.DatabaseConnectionString = GetConnectionString()
            AddHandler oProcedureListControl.ItemSelected, AddressOf oProcedureListControl_ItemSelected
            AddHandler oProcedureListControl.InternalGridKeyDown, AddressOf oProcedureListControl_InternalGridKeyDown
            AddHandler oProcedureListControl.InternalGridLostFocus, AddressOf oProcedureListControl_InternalGridLostFocus
            oProcedureListControl.ControlHeader = ControlHeader
            pnlProcedureControl.Controls.Add(oProcedureListControl)
            oProcedureListControl.Dock = DockStyle.Fill
            If SearchText <> "" Then
                oProcedureListControl.Search(SearchText, gloUserControlLibrary.SearchColumn.Code)
            End If
            oProcedureListControl.Show()
            pnlProcedureControl.Visible = True
            RaiseEvent GridListLoaded()
            pnlProcedureControl.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            _
        Finally

        End Try

    End Sub
#End Region
#Region "Close control"
    'Private Sub CloseInternalControl()

    '    Try

    '        For i As Integer = 0 To pnlInternalControl.Controls.Count - 1
    '            pnlInternalControl.Controls.RemoveAt(i)
    '        Next
    '        If oGridListControl IsNot Nothing Then
    '            oGridListControl.Dispose()
    '            oGridListControl = Nothing
    '        End If
    '        pnlInternalControl.Visible = False
    '        RaiseEvent GridListClosed()
    '        pnlInternalControl.SendToBack()

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
    '    Finally
    '    End Try

    'End Sub
#End Region
#Region "Close Procedure Control"
    Private Sub CloseProcedureControl()

        Try
            'SLR: Changed on 4/2/2014

            For i As Integer = pnlProcedureControl.Controls.Count - 1 To 0 Step -1
                pnlProcedureControl.Controls.RemoveAt(i)
            Next

            If oProcedureListControl IsNot Nothing Then
                RemoveHandler oProcedureListControl.ItemSelected, AddressOf oProcedureListControl_ItemSelected
                RemoveHandler oProcedureListControl.InternalGridKeyDown, AddressOf oProcedureListControl_InternalGridKeyDown
                RemoveHandler oProcedureListControl.InternalGridLostFocus, AddressOf oProcedureListControl_InternalGridLostFocus

                oProcedureListControl.Dispose()
                oProcedureListControl = Nothing
            End If
            pnlProcedureControl.Visible = False
            RaiseEvent GridListClosed()
            pnlProcedureControl.SendToBack()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)

        Finally
        End Try

    End Sub
#End Region
    'Private Sub oGridListControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Dim oICD9 As gloGeneralItem.gloItem
    '        oICD9 = oGridListControl.SelectedItems(0)
    '        If oGridListControl.SelectedItems IsNot Nothing Then
    '            If oGridListControl.SelectedItems.Count > 0 Then
    '                Select Case oGridListControl.ControlType

    '                    Case gloUserControlLibrary.gloGridListControlType.ICD9
    '                        txtDiagnosis.Text = oICD9.Code
    '                        'cmbDiagnosis.Items.Add(oICD9.Code)
    '                        'cmbDiagnosis.Text = oICD9.Code

    '                End Select
    '                ' _ICD9CPTModified = True
    '                CloseInternalControl()
    '            Else

    '            End If
    '        Else

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub oProcedureListControl_ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim oProcedure As gloGeneralItem.gloItem
            'oProcedure = oProcedureListControl.SelectedItems(0)
            If oProcedureListControl.SelectedItems IsNot Nothing Then
                If oProcedureListControl.SelectedItems.Count > 0 Then
                    oProcedure = oProcedureListControl.SelectedItems(0)
                    Select Case oProcedureListControl.ControlType
                        Case gloUserControlLibrary.gloGridListControlType.Procedures
                            txtProcedure.Text = oProcedure.Code
                            'If AddICD9(oGridListControl.SelectedItems(0), oGridListControl.ParentRowIndex, oGridListControl.ParentColIndex) Then
                            '    'RemoveICD9()
                            'Else

                    End Select
                    '_ICD9CPTModified = True
                    CloseProcedureControl()
                Else

                End If
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oProcedureListControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub oProcedureListControl_InternalGridLostFocus(ByVal sender As Object, ByVal e As EventArgs)
        If pnlProcedureControl.Visible Then
            If oProcedureListControl IsNot Nothing Then
                txtProcedure.Text = ""
                CloseProcedureControl()
            End If
        End If
    End Sub
    'Private Sub RemoveICD9(ByVal ICD9Code As String, ByVal ICD9Description As String)
    '    Try
    '        If isICD9Present(ICD9Code) = False Then
    '            Dim oICD9s As New gloGeneralItem.gloItems
    '            oICD9s.Add(0, ICD9Code, ICD9Description)
    '            RaiseEvent ICD9_Removed(oICD9s)
    '        End If
    '    Catch
    '    End Try
    'End Sub
    'Private Function AddICD9(ByVal oICD9 As gloGeneralItem.gloItem, ByVal nRow As Integer, ByVal nCol As Integer) As Boolean
    '    Try
    '        If (oICD9.Code = txtDiagnosis.Text) Then
    '        End If

    '        If txtDiagnosis.Text = "" Then
    '            txtDiagnosis.Text = oICD9.ID
    '        End If

    '        RaiseEvent ICD9_Inserted(oICD9)
    '        Return True
    '    Catch ex As Exception
    '    End Try
    'End Function

    'Private Sub oGridListControl_InternalGridKeyDown(ByVal sender As Object, ByVal e As EventArgs)

    'End Sub

    Private Sub tls_ReportCriteria_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ReportCriteria.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "PrintReport"
                    btn_tls_Close.Enabled = False ''added to resolve bugid 71389
                    PrintReport()
                Case "ViewReport"
                    btn_tls_Close.Enabled = False
                    ViewReport()
                Case "Close"
                    Me.Close()
                    Try
                        'Application.DoEvents()
                        Me.Dispose()
                    Catch exdispose As Exception

                    End Try
                Case "ShowReportList"
                    ShowReportList()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btn_tls_Close.Enabled = True   ''added to resolve bugid 71389
        End Try
    End Sub
#Region "Show Report"
    Private Sub ShowReportList()
        Try
            'Dim dtPatients As New DataTable
            ' Dim oRpt As New rpt_PatientICD9CPT()
            GetVariables()
            Dim dtPAtient As DataTable
            'Dim dtProcunitcount As New DataTable
            Dim strSQL As String = ""

            If txtFrom.Text = "" And txtToproc.Text = "" Or txtFrom.Text <> "" And txtToproc.Text <> "" Then
            Else

                If txtFrom.Text = "" Or txtToproc.Text <> "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtFrom.Text <> "" Or txtToproc.Text = "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            'If dtpicFrom.Value() > dtpicTo.Value() Then
            '    MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            Dim bIsEndDateSmaller As Boolean = CompareDates()
            If bIsEndDateSmaller = True Then
                MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            dtPAtient = FetchDetails()
            DesignPatientGrid(dtPAtient)
            If (IsNothing(dtPAtient)) Then
                MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If dtPAtient.Rows.Count <= 0 Then
                    MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
          

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Fetch Patients"
    'Public Function FetchPatients(ByRef strSQL As String) As DataTable
    '    Dim conn As New SqlConnection(GetConnectionString())
    '    Dim Dv As DataView
    '    Dim oCmd As SqlCommand
    '    Dim _strSQL As String = ""
    '    Dim _result As Object
    '    Dim dt As New DataTable
    '    Dim ad As SqlDataAdapter

    '    Try

    '        Dim viewCriteria As Int16 = 0

    '        If RbDrDx.Checked = True Then
    '            viewCriteria = 1
    '        ElseIf RbDrDxLocation.Checked = True Then
    '            viewCriteria = 3
    '        ElseIf RbDrDxProc.Checked = True Then
    '            viewCriteria = 2
    '        ElseIf RbDxbyProc.Checked = True Then
    '            viewCriteria = 4
    '        End If
    '        '_strSQL = " SELECT DISTINCT ISNULL(Patient.nPatientID,0) as nPatientID ,  ISNULL(ExamICD9CPT.sICD9Code,'') + ' - ' + ISNULL(ExamICD9CPT.sICD9Description,'') As Diagnosis,  ISNULL(ExamICD9CPT.sCPTcode,'') + ' - ' + ISNULL(ExamICD9CPT.sICD9Description,'') As Procedures, " _
    '        '        & " ISNULL(PatientExams.dtDOS,'') As [DOS], ISNULL(Patient.sPatientCode,'') AS [Patient Code], ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') as [Patient Name], " _
    '        '        & " ISNULL(Patient.sLocation,'') as Location, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') as Provider , ISNULL(PatientExams.sExamName,'') As [Exam Name] " _
    '        '        & " FROM ExamICD9CPT INNER JOIN " _
    '        '        & " Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID INNER JOIN " _
    '        '        & " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " _
    '        '        & " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  Inner Join " _
    '        '        & " AB_Location ON Patient.sLocation = AB_Location.sLocation INNER JOIN " _
    '        '        & " Visits ON ExamICD9CPT.nVisitID = Visits.nVisitID " _
    '        '        & " WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) >= '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  Convert(Varchar(50), Visits.dtVisitDate ,101) <= '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        ''_strSQL = " SELECT DISTINCT ISNULL(Patient.nPatientID,0) as nPatientID ,  ISNULL(ExamICD9CPT.sICD9Code,'') As Diagnosis,  ISNULL(ExamICD9CPT.sCPTcode,'') As Procedures, ISNULL(ExamICD9CPT.nUnit,1) As Unit," _
    '        ''        & " ISNULL(PatientExams.dtDOS,'') As [DOS], ISNULL(Patient.sPatientCode,'') AS [Patient Code], ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') as [Patient Name], " _
    '        ''        & " ISNULL(Patient.sLocation,'') as Location, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') as Provider , ISNULL(PatientExams.sExamName,'') As [Exam Name] " _
    '        ''        & " FROM ExamICD9CPT INNER JOIN " _
    '        ''        & " Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID INNER JOIN " _
    '        ''        & " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " _
    '        ''        & " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  Inner Join " _
    '        ''        & " AB_Location ON Patient.sLocation = AB_Location.sLocation INNER JOIN " _
    '        ''        & " Visits ON ExamICD9CPT.nVisitID = Visits.nVisitID " _
    '        ''        & " WHERE Visits.dtVisitDate >= '" & dtpicFrom.Value.Date & "' AND  Visits.dtVisitDate<= '" & dtpicTo.Value.Date & "' "
    '        ''
    '        ''Added to fix case No:#0004171
    '        _strSQL = " SELECT DISTINCT ISNULL(Patient.nPatientID,0) as nPatientID ,  ISNULL(ExamICD9CPT.sICD9Code,'') As Diagnosis,  ISNULL(ExamICD9CPT.sCPTcode,'') As Procedures, ISNULL(ExamICD9CPT.nUnit,1) As Units," _
    '                & " ISNULL(PatientExams.dtDOS,'') As [DOS], ISNULL(Patient.sPatientCode,'') AS [Patient Code], ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') as [Patient Name], " _
    '                & " ISNULL(Patient.sLocation,'') as Location, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') as Provider , ISNULL(PatientExams.sExamName,'') As [Exam Name], " _
    '                & " ViewCriteria = " & viewCriteria & " FROM ExamICD9CPT INNER JOIN " _
    '                & " Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID INNER JOIN " _
    '                & " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " _
    '                & " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  Inner Join " _
    '                & " AB_Location ON Patient.sLocation = AB_Location.sLocation INNER JOIN " _
    '                & " Visits ON ExamICD9CPT.nVisitID = Visits.nVisitID "
    '        ''& " WHERE Visits.dtVisitDate >= '" & dtpicFrom.Value.Date & "' AND  Visits.dtVisitDate<= '" & dtpicTo.Value.Date & "' "
    '        'If dtpicFrom.Value.ToString() = dtpicTo.Value.ToString() Then
    '        _strSQL = _strSQL & "WHERE CAST(PatientExams.DTDOS as Smalldatetime) BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        'Else
    '        '_strSQL = _strSQL & "WHERE Visits.dtVisitDate >= '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  Visits.dtVisitDate<= '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        'End If
    '        ''


    '        '''& " WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "

    '        '& " WHERE Visits.dtVisitDate BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        ''Commenetd to fix case No:#0004171
    '        ''& " WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) >= '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  Convert(Varchar(50), Visits.dtVisitDate ,101) <= '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "


    '        If strProviderIDs.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND Patient.nProviderID in (" & strProviderIDs & ") "
    '        End If

    '        If strLocations.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND Patient.sLocation in (" & strLocations & ") "
    '        End If

    '        If strICD9s.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND ExamICD9CPT.sICD9Code in (" & strICD9s & ")"
    '        End If

    '        If strCPTs.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode in (" & strCPTs & ") "
    '        End If
    '        ''20100222
    '        If txtFrom.Text.ToString.Trim <> "" And txtToproc.Text.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode >= " & strCPTFrom & " AND ExamICD9CPT.sCPTCode <= " & strCPTTo & ""
    '            '_strSQL = _strSQL & " AND ExamICD9CPT.sCPTCode BETWEEN " & strCPTFrom & " AND " & strCPTTo & ""

    '        End If
    '        'If _IsDiagnosis = True Then
    '        '    _strSQL =_strSQL & "AND " &
    '        'End If
    '        ''End 20100222
    '        ''commenetd on 20100222
    '        ''If TextBox2.Text.ToString.Trim <> "" And TextBox3.Text.ToString.Trim <> "" And _rangeCPT <> "" Then

    '        ''    _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode in (" & _rangeCPT & ") "

    '        ''End If
    '        ''end 20100222
    '        oCmd = New SqlCommand()

    '        oCmd.Connection = conn
    '        oCmd.CommandType = CommandType.Text
    '        oCmd.CommandText = _strSQL
    '        conn.Open()
    '        ad = New SqlDataAdapter(oCmd)

    '        ad.Fill(dt)
    '        Dv = New DataView(dt)
    '        strSQL = _strSQL
    '        Dim dtdiagnosis As New DataTable
    '        Dim dtProcedures As New DataTable

    '        ' dtdiagnosis = FetchDiagnosiscount(dt, strSQL)
    '        'dtProcedures = FetchProcedureUnitCount(dt)
    '        Return dt
    '        ' FetchDiagnosiscount(dt)

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If Not IsNothing(conn) Then
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '                conn.Dispose()
    '                conn = Nothing

    '                If Not IsNothing(oCmd) Then
    '                    oCmd.Dispose()
    '                    oCmd = Nothing
    '                End If
    '            End If
    '        End If
    '    End Try
    'End Function

    Public Function FetchSummaryDetails(ByVal _dsReports As ds_PtICD9CPT) As ds_PtICD9CPT

        Dim ICdType As Int16 = 0
        If (rbALL.Checked = True) Then
            ICdType = 0
        End If
        If (rbICD10.Checked = True) Then
            ICdType = 10
        End If
        If (rbICD9.Checked = True) Then
            ICdType = 9
        End If

        Dim viewCriteria As Int16 = 0
        If RbDrDx.Checked = True Then
            viewCriteria = 1
        ElseIf RbDrDxLocation.Checked = True Then
            viewCriteria = 3
        ElseIf RbDrDxProc.Checked = True Then
            viewCriteria = 2
        ElseIf RbDxbyProc.Checked = True Then
            viewCriteria = 4
        End If
        Dim oConnection As New SqlConnection
        Dim strConString As String = ""
        Dim sqlCmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim sqlParam As SqlParameter

        strConString = GetConnectionString()
        oConnection = New SqlConnection(strConString)
        sqlCmd.CommandText = ""
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "RPT_PatientICD9CPT_Summary"

        sqlParam = sqlCmd.Parameters.Add("@ViewCriteria", SqlDbType.BigInt)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = viewCriteria

        sqlParam = sqlCmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = ICdType


        sqlParam = sqlCmd.Parameters.Add("@FromDate", SqlDbType.DateTime)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = dtpicFrom.Value.ToShortDateString()

        sqlParam = sqlCmd.Parameters.Add("@ToDate", SqlDbType.DateTime)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = dtpicTo.Value.ToShortDateString()

        sqlParam = sqlCmd.Parameters.Add("@strProviderIDs", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strProviderIDs

        sqlParam = sqlCmd.Parameters.Add("@strLocations", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strLocations

        sqlParam = sqlCmd.Parameters.Add("@strICD9s", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strICD9s

        sqlParam = sqlCmd.Parameters.Add("@strCPTs", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTs

        sqlParam = sqlCmd.Parameters.Add("@strCPTFrom", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTFrom

        sqlParam = sqlCmd.Parameters.Add("@strCPTTo", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTTo

        sqlCmd.Connection = oConnection
        If oConnection.State = ConnectionState.Open Then
            oConnection.Close()
        End If

        oConnection.Open()

        '07-Nov-14 Aniket: Resolving Bug #75733: gloEMR> Reports> Patient ICD9/10-CPT> It is showing exception "System.Data.Sqlclient.SqlException"
        sqlCmd.CommandTimeout = 0
        da = New SqlDataAdapter(sqlCmd)

        da.Fill(_dsReports, "dt_SummaryDetails")
        da.Dispose()
        da = Nothing
        If sqlCmd IsNot Nothing Then
            sqlCmd.Parameters.Clear()
            sqlCmd.Dispose()
            sqlCmd = Nothing
        End If

        sqlParam = Nothing
        oConnection.Close()
        oConnection.Dispose()
        oConnection = Nothing

        Return _dsReports

    End Function
    ''
    Public Function FetchMainReportDetails(ByVal _dsReports As ds_PtICD9CPT) As ds_PtICD9CPT
        ''20100325
        'Dim viewCriteria As Int16 = 0
        'If RbDrDx.Checked = True Then
        '    viewCriteria = 1
        'ElseIf RbDrDxLocation.Checked = True Then
        '    viewCriteria = 3
        'ElseIf RbDrDxProc.Checked = True Then
        '    viewCriteria = 2
        'ElseIf RbDxbyProc.Checked = True Then
        '    viewCriteria = 4
        'End If
        ''End 20100325
        Dim ICdType As Int16 = 0
        If (rbALL.Checked = True) Then
            ICdType = 0
        End If
        If (rbICD10.Checked = True) Then
            ICdType = 10
        End If
        If (rbICD9.Checked = True) Then
            ICdType = 9
        End If
        Dim oConnection As New SqlConnection
        Dim strConString As String = ""
        Dim sqlCmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim sqlParam As SqlParameter

        strConString = GetConnectionString()
        oConnection = New SqlConnection(strConString)
        sqlCmd.CommandText = ""
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "RPT_PatientICD9CPT"
        ''20100325
        'sqlParam = sqlCmd.Parameters.Add("@ViewCriteria", SqlDbType.BigInt)
        'sqlParam.Direction = ParameterDirection.Input
        'sqlParam.Value = viewCriteria
        ''End 20100325
        sqlParam = sqlCmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = ICdType

        sqlParam = sqlCmd.Parameters.Add("@FromDate", SqlDbType.DateTime)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = dtpicFrom.Value.ToShortDateString()

        sqlParam = sqlCmd.Parameters.Add("@ToDate", SqlDbType.DateTime)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = dtpicTo.Value.ToShortDateString()

        sqlParam = sqlCmd.Parameters.Add("@strProviderIDs", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strProviderIDs

        sqlParam = sqlCmd.Parameters.Add("@strLocations", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strLocations

        sqlParam = sqlCmd.Parameters.Add("@strICD9s", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strICD9s

        sqlParam = sqlCmd.Parameters.Add("@strCPTs", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTs

        sqlParam = sqlCmd.Parameters.Add("@strCPTFrom", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTFrom

        sqlParam = sqlCmd.Parameters.Add("@strCPTTo", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = strCPTTo


        sqlCmd.Connection = oConnection
        If oConnection.State = ConnectionState.Open Then
            oConnection.Close()
        End If
        oConnection.Open()
        da = New SqlDataAdapter(sqlCmd)
        da.Fill(_dsReports, "Dt_PatientICD9_CPT")
        da.Dispose()
        da = Nothing

        If sqlCmd IsNot Nothing Then
            sqlCmd.Parameters.Clear()
            sqlCmd.Dispose()
            sqlCmd = Nothing
        End If

        sqlParam = Nothing
        oConnection.Close()
        oConnection.Dispose()
        oConnection = Nothing

        Return _dsReports

    End Function
    ''
    ''
    Public Function FetchDetails() As DataTable
        ''20100325
        'Dim viewCriteria As Int16 = 0
        'If RbDrDx.Checked = True Then
        '    viewCriteria = 1
        'ElseIf RbDrDxLocation.Checked = True Then
        '    viewCriteria = 3
        'ElseIf RbDrDxProc.Checked = True Then
        '    viewCriteria = 2
        'ElseIf RbDxbyProc.Checked = True Then
        '    viewCriteria = 4
        'End If
        ''End 20100325

        'Dim oConn As SqlConnection
        'Dim ocmd As SqlCommand
        'Dim oPara As SqlParameter
        Dim _result As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Try


            Dim oParameter As gloDatabaseLayer.DBParameter


            oDB.Connect(False)


           

            'oParameter = New gloDatabaseLayer.DBParameter()
            'oParameter.ParameterName = "@ViewCriteria"
            'oParameter.ParameterDirection = ParameterDirection.Input
            'oParameter.DataType = SqlDbType.BigInt
            'oParameter.Value = viewCriteria
            'oParameters.Add(oParameter)
            'oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@FromDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.DateTime
            oParameter.Value = dtpicFrom.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing


            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@ToDate"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.DateTime
            oParameter.Value = dtpicTo.Value.ToShortDateString()
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strProviderIDs"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strProviderIDs
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strLocations"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strLocations
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strICD9s"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strICD9s
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strCPTs"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strCPTs
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strCPTFrom"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strCPTFrom
            oParameters.Add(oParameter)
            oParameter = Nothing

            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@strCPTTo"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Value = strCPTTo
            oParameters.Add(oParameter)
            oParameter = Nothing
            '''''

            '''''
            Dim ICdType As Int16 = 0
            If (rbALL.Checked = True) Then
                ICdType = 0
            End If
            If (rbICD10.Checked = True) Then
                ICdType = 10
            End If
            If (rbICD9.Checked = True) Then
                ICdType = 9
            End If
            oParameter = New gloDatabaseLayer.DBParameter()
            oParameter.ParameterName = "@nICDRevision"
            oParameter.ParameterDirection = ParameterDirection.Input
            oParameter.DataType = SqlDbType.SmallInt
            oParameter.Value = ICdType
            oParameters.Add(oParameter)
            oParameter = Nothing
            ''@nICDRevision   added for ICD10 Implementation
            oDB.Retrive("RPT_PatientICD9CPT", oParameters, _result)
            oDB.Disconnect()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParameters) = False Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
        End Try
        Return _result

    End Function
    ''

    ''

    ''start 20100313
    'Public Function FetchProcCount(ByRef strSQL As String) As DataTable
    '    Dim conn As New SqlConnection(GetConnectionString())
    '    Dim Dv As DataView
    '    Dim oCmd As SqlCommand
    '    Dim _strSQL As String = ""
    '    Dim _result As Object
    '    Dim dt As New DataTable
    '    Dim ad As SqlDataAdapter

    '    Try


    '        '_strSQL = " SELECT DISTINCT ISNULL(Patient.nPatientID,0) as nPatientID ,  ISNULL(ExamICD9CPT.sICD9Code,'') As Diagnosis,  ISNULL(ExamICD9CPT.sCPTcode,'') As Procedures, ISNULL(ExamICD9CPT.nUnit,1) As Units," _
    '        '        & " ISNULL(PatientExams.dtDOS,'') As [DOS], ISNULL(Patient.sPatientCode,'') AS [Patient Code], ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') as [Patient Name], " _
    '        '        & " ISNULL(Patient.sLocation,'') as Location, ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') as Provider , ISNULL(PatientExams.sExamName,'') As [Exam Name] " _
    '        '        & " FROM ExamICD9CPT INNER JOIN " _
    '        '        & " Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID INNER JOIN " _
    '        '        & " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " _
    '        '        & " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  Inner Join " _
    '        '        & " AB_Location ON Patient.sLocation = AB_Location.sLocation INNER JOIN " _
    '        '        & " Visits ON ExamICD9CPT.nVisitID = Visits.nVisitID "
    '        _strSQL = "SELECT  ISNULL(ExamICD9CPT.sCPTCode,'') As Procedures,ISNULL(ExamICD9CPT.nUnit,1) As Units, " _
    '                  & "sum(ExamICD9CPT.nUnit) as procunits ,ISNULL(ExamICD9CPT.sICD9Code,'') As Diagnosis, count(ExamICD9CPT.sICD9Code) as Diacount " _
    '                  & "FROM ExamICD9CPT INNER JOIN Patient ON ExamICD9CPT.nPatientID = Patient.nPatientID INNER JOIN " _
    '                  & "Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " _
    '                  & "PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  Inner Join " _
    '                  & "AB_Location ON Patient.sLocation = AB_Location.sLocation INNER JOIN " _
    '                  & "Visits ON ExamICD9CPT.nVisitID = Visits.nVisitID "
    '        ''& " WHERE Visits.dtVisitDate >= '" & dtpicFrom.Value.Date & "' AND  Visits.dtVisitDate<= '" & dtpicTo.Value.Date & "' "
    '        If dtpicFrom.Value.ToString() = dtpicTo.Value.ToString() Then
    '            _strSQL = _strSQL & "WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        Else
    '            _strSQL = _strSQL & "WHERE Visits.dtVisitDate >= '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  Visits.dtVisitDate<= '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        End If
    '        ''


    '        '''& " WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "

    '        '& " WHERE Visits.dtVisitDate BETWEEN '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "
    '        ''Commenetd to fix case No:#0004171
    '        ''& " WHERE Convert(Varchar(50), Visits.dtVisitDate ,101) >= '" & dtpicFrom.Value.ToString("MM/dd/yyyy") & "' AND  Convert(Varchar(50), Visits.dtVisitDate ,101) <= '" & dtpicTo.Value.ToString("MM/dd/yyyy") & "' "


    '        If strProviderIDs.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND Patient.nProviderID in (" & strProviderIDs & ") "
    '        End If

    '        If strLocations.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND Patient.sLocation in (" & strLocations & ") "
    '        End If

    '        If strICD9s.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & " AND ExamICD9CPT.sICD9Code in (" & strICD9s & ")"
    '        End If

    '        If strCPTs.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode in (" & strCPTs & ") "
    '        End If
    '        ''20100222
    '        If txtFrom.Text.ToString.Trim <> "" And txtToproc.Text.ToString.Trim <> "" Then
    '            _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode >= " & strCPTFrom & " AND ExamICD9CPT.sCPTCode <= " & strCPTTo & ""
    '            '_strSQL = _strSQL & " AND ExamICD9CPT.sCPTCode BETWEEN " & strCPTFrom & " AND " & strCPTTo & ""

    '        End If
    '        _strSQL = _strSQL & "AND ExamICD9CPT.sCPTCode <> '' group by ExamICD9CPT.sCPTCode,ExamICD9CPT.nUnit,ExamICD9CPT.sICD9Code "
    '        'If _IsDiagnosis = True Then
    '        '    _strSQL =_strSQL & "AND " &
    '        'End If
    '        ''End 20100222
    '        ''commenetd on 20100222
    '        ''If TextBox2.Text.ToString.Trim <> "" And TextBox3.Text.ToString.Trim <> "" And _rangeCPT <> "" Then

    '        ''    _strSQL = _strSQL & "AND  ExamICD9CPT.sCPTCode in (" & _rangeCPT & ") "

    '        ''End If
    '        ''end 20100222
    '        oCmd = New SqlCommand()

    '        oCmd.Connection = conn
    '        oCmd.CommandType = CommandType.Text
    '        oCmd.CommandText = _strSQL
    '        conn.Open()
    '        ad = New SqlDataAdapter(oCmd)

    '        ad.Fill(dt)
    '        Dv = New DataView(dt)
    '        strSQL = _strSQL
    '        'Dim dtdiagnosis As New DataTable
    '        'Dim dtProcedures As New DataTable

    '        'dtdiagnosis = FetchDiagnosiscount(dt, strSQL)
    '        'dtProcedures = FetchProcedureUnitCount(dt)
    '        Return dt
    '        ' FetchDiagnosiscount(dt)

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If Not IsNothing(conn) Then
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '                conn.Dispose()
    '                conn = Nothing

    '                If Not IsNothing(oCmd) Then
    '                    oCmd.Dispose()
    '                    oCmd = Nothing
    '                End If
    '            End If
    '        End If
    '    End Try
    'End Function
    ''End 20100313

    ''
#End Region
#Region "Commenetd"
    ''Commenetd by Mayuri:20100319
    'Public Function FetchDiagnosiscount(Optional ByVal dtDiagnosis As DataTable = Nothing, Optional ByVal strSQL As String = "") As DataTable
    '    Dim _dv As DataView
    '    Dim _dv1 As DataView
    '    Dim dt1 As New DataTable
    '    'Dim strSql As String = ""
    '    Dim i As Int32
    '    Dim strFilter As String = ""
    '    Dim strFilter1 As String = ""
    '    Dim dtdiacount As DataTable
    '    Try
    '        'dtDiagnosis = FetchPatients(strSql)
    '        _dv1 = dtDiagnosis.DefaultView
    '        strFilter1 = "Diagnosis <> '' and Diagnosis<> ''''"
    '        _dv1.RowFilter = strFilter1
    '        dt1 = _dv1.ToTable
    '        _dv = dt1.DefaultView
    '        Dim str As [String]() = New [String](0) {}
    '        str(0) = "Diagnosis"
    '        Dim dtTemp As DataTable
    '        dtTemp = dt1.DefaultView.ToTable(True, str)

    '        '' Table to Show distinct Dx & their respect count
    '        dtdiacount = New DataTable("DiaCount")
    '        ''
    '        'declaring a column named Name
    '        Dim Name As DataColumn = New DataColumn("DiaName")
    '        'setting the datatype for the column
    '        Name.DataType = System.Type.GetType("System.String")

    '        dtdiacount.Columns.Add(Name)

    '        'declaring a column named Name
    '        Dim NameCount As DataColumn = New DataColumn("DiaCount")
    '        'setting the datatype for the column
    '        NameCount.DataType = System.Type.GetType("System.Int64")
    '        dtdiacount.Columns.Add(NameCount)
    '        ''

    '        For i = 0 To dtTemp.Rows.Count - 1
    '            strFilter = "Diagnosis='" & dtTemp.Rows(i)("Diagnosis").ToString + "'"
    '            'strFilter = "count(ExamICD9CPT.sICD9Code) where sICD9Code='" + dt1.Rows(i)("Diagnosis").ToString + "'"
    '            dt1.DefaultView.RowFilter = strFilter
    '            Dim DiaCount = dt1.DefaultView.Count
    '            Dim DiaName = dtTemp.Rows(i)("Diagnosis").ToString
    '            ''
    '            'Dim dtdiacount As DataTable
    '            '  dtdiacount = New DataTable("DiaCount")

    '            Dim rowDianame, rowDiacount As DataRow

    '            rowDianame = dtdiacount.NewRow()
    '            rowDianame("DiaName") = DiaName
    '            rowDianame("DiaCount") = DiaCount
    '            dtdiacount.Rows.Add(rowDianame)
    '            'rowDianame.Item("DiaName") = dtTemp.Rows(i)("Diagnosis").ToString

    '            'rowDiacount = dtdiacount.NewRow()
    '            'rowDiacount.Item("DiaCount") = dt1.DefaultView.Count 
    '            'ds_rpt.Tables.Add(dtdiacount)
    '        Next
    '        'strSQL = strFilter

    '        'Dim dt As DataTable = AgingBucketSorting(dtDiagnosis, "Diagnosis", "Procedures")

    '        Return dtdiacount

    '        '_dv.RowFilter = strFilter
    '        'dtDiagnosis = _dv.ToTable
    '        '_dv = dtDiagnosis.DefaultView()


    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function


    'Private Sub DesignGrid(ByVal dt As DataTable)
    '    Try
    '        C1grdPatients.ScrollBars = ScrollBars.None
    '        C1grdPatients.DataSource = dt
    '        C1grdPatients.ShowCellLabels = True
    '        C1grdPatients.ScrollBars = ScrollBars.None
    '        C1grdPatients.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None

    '        ''Visisble
    '        C1grdPatients.Cols(0).Visible = False
    '        C1grdPatients.Cols(1).Visible = True
    '        C1grdPatients.Cols(2).Visible = True
    '        C1grdPatients.Cols(3).Visible = True
    '        C1grdPatients.Cols(4).Visible = True
    '        C1grdPatients.Cols(5).Visible = True
    '        C1grdPatients.Cols(6).Visible = True
    '        C1grdPatients.Cols(7).Visible = True
    '        C1grdPatients.Cols(8).Visible = True
    '        C1grdPatients.Cols(9).Visible = True

    '        ''Width
    '        C1grdPatients.Cols(0).Width = 0   '' PatID
    '        C1grdPatients.Cols(1).Width = Width * 0.12 '' Dia
    '        C1grdPatients.Cols(2).Width = Width * 0.12  '' Proc
    '        C1grdPatients.Cols(3).Width = Width * 0.05 '' DOS
    '        C1grdPatients.Cols(4).Width = Width * 0.08 ''Pat Cod
    '        C1grdPatients.Cols(5).Width = Width * 0.12 '' Pat Name
    '        C1grdPatients.Cols(6).Width = Width * 0.08 '' Location
    '        C1grdPatients.Cols(7).Width = Width * 0.12 '' Provider
    '        C1grdPatients.Cols(8).Width = Width * 0.15 '' Exam Name
    '        C1grdPatients.Cols(9).Width = Width * 0.15
    '        ''end---------width
    '        C1grdPatients.ScrollBars = ScrollBars.Both
    '        C1grdPatients.AllowEditing = False



    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try



    '    'End If


    'End Sub

    'Private Sub txtDiagnosis_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDiagnosis.KeyUp
    '    '        Dim _isdeleted As Boolean = True

    '    _ICD9CPTModified = True

    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            e.SuppressKeyPress = True
    '            '#Region "Enter Key"

    '            If pnlInternalControl.Visible Then
    '                If oGridListControl IsNot Nothing Then
    '                    Dim _IsItemSelected As Boolean = oGridListControl.GetCurrentSelectedItem()
    '                    If _IsItemSelected Then

    '                    End If
    '                End If
    '            End If

    '        ElseIf e.KeyCode = Keys.Down Then
    '            e.SuppressKeyPress = True
    '            e.Handled = True
    '            '#Region "Down Key"
    '            If pnlInternalControl.Visible Then
    '                If oGridListControl IsNot Nothing Then
    '                    oGridListControl.Focus()
    '                End If
    '                '#End Region
    '            End If

    '        ElseIf e.KeyCode = Keys.Escape Then
    '            e.SuppressKeyPress = True
    '            '#Region "Escape Key"
    '            If pnlInternalControl.Visible Then
    '                If oGridListControl IsNot Nothing Then
    '                    CloseInternalControl()
    '                End If
    '                '#End Region
    '            End If



    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '    End Try
    'End Sub
#End Region
#Region "Design Patient Grid"
    Private Sub DesignPatientGrid(ByVal dt As DataTable)
        Try
            C1grdPatients.ScrollBars = ScrollBars.None
            C1grdPatients.DataSource = dt
            'C1grdPatients.ShowCellLabels = True
            C1grdPatients.ScrollBars = ScrollBars.None
            C1grdPatients.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None


            ''Visible
            C1grdPatients.Cols(Col_PatientID).Visible = False
            C1grdPatients.Cols(Col_Diagnosis).Visible = True
            C1grdPatients.Cols(Col_Procedure).Visible = True
            C1grdPatients.Cols(Col_Unit).Visible = True
            C1grdPatients.Cols(Col_DOS).Visible = True
            C1grdPatients.Cols(Col_PatientCode).Visible = True
            C1grdPatients.Cols(Col_PatientName).Visible = True
            C1grdPatients.Cols(Col_Location).Visible = True
            C1grdPatients.Cols(Col_Provider).Visible = True
            C1grdPatients.Cols(Col_ExamName).Visible = True
            C1grdPatients.Cols(Col_ExamId).Visible = False

            ''Width
            C1grdPatients.Cols(Col_PatientID).Width = 0   '' PatID
            C1grdPatients.Cols(Col_Diagnosis).Width = Width * 0.15 '' Dia
            C1grdPatients.Cols(Col_Procedure).Width = Width * 0.09  '' Proc
            C1grdPatients.Cols(Col_Unit).Width = Width * 0.04 '' Units
            C1grdPatients.Cols(Col_DOS).Width = Width * 0.06 ''DOS
            C1grdPatients.Cols(Col_PatientCode).Width = Width * 0.09 '' PAt Code
            C1grdPatients.Cols(Col_PatientName).Width = Width * 0.1 '' Pat name
            C1grdPatients.Cols(Col_Location).Width = Width * 0.1 '' Location
            C1grdPatients.Cols(Col_Provider).Width = Width * 0.15 '' Provider
            C1grdPatients.Cols(Col_ExamName).Width = Width * 0.2  '' Exam name
            C1grdPatients.Cols(Col_ExamId).Visible = 0         ''Exam Id
            ''end---------width
            C1grdPatients.ScrollBars = ScrollBars.Both
            C1grdPatients.AllowEditing = False



        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



        'End If


    End Sub
#End Region

    

    Private Sub txtProcedure_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProcedure.KeyUp
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = True
                '#Region "Enter Key"

                If pnlProcedureControl.Visible Then
                    If oProcedureListControl IsNot Nothing Then
                        Dim _IsItemSelected As Boolean = oProcedureListControl.GetCurrentSelectedItem()
                        'If _IsItemSelected Then

                        'End If
                    End If
                End If

            ElseIf e.KeyCode = Keys.Down Then
                e.SuppressKeyPress = True
                e.Handled = True
                '#Region "Down Key"
                If pnlProcedureControl.Visible Then
                    If oProcedureListControl IsNot Nothing Then
                        oProcedureListControl.Focus()
                    End If
                    '#End Region
                End If

            ElseIf e.KeyCode = Keys.Escape Then
                e.SuppressKeyPress = True
                '#Region "Escape Key"
                If pnlProcedureControl.Visible Then
                    If oProcedureListControl IsNot Nothing Then
                        oProcedureListControl_InternalGridLostFocus(Nothing, Nothing)
                    End If
                    '#End Region
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    Private Sub txtProcedure_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProcedure.LostFocus
        'e.SuppressKeyPress = True

        If pnlProcedureControl.Visible Then
            If oProcedureListControl IsNot Nothing Then
                If oProcedureListControl.Focus() = False Then
                    If oProcedureListControl.C1GridList.Focus() = False Then
                        CloseProcedureControl()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtProcedure_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProcedure.TextChanged
        Try
            Dim _strSearchString As String = ""
            _strSearchString = txtProcedure.Text
            If (_strSearchString.Trim() <> "") Then
                OpenProcedureControl(gloUserControlLibrary.gloGridListControlType.Procedures, "Procedures", "")
                oProcedureListControl.FillControl(_strSearchString)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddToSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor
           
            FillSelectedDiagnosis()

            'FillSelectedDiagnosis()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSearch.MouseHover, btnRemove.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToSearch.MouseLeave, btnRemove.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            'If trvDiagnosis.Nodes.Count > 1 Then
            If IsNothing(trvselecteddia.SelectedNode) = False Then
                trvselecteddia.Nodes.Remove(trvselecteddia.SelectedNode)
               
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'chkDiagnosis.Items.Remove(chkDiagnosis.SelectedItem())
    End Sub

    Private Sub btnSelectProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectProvider.Click
        Try
            Dim i As Int16
            If trvProvider.Nodes.Count > 0 Then
                For i = 0 To trvProvider.Nodes.Count - 1
                    trvProvider.Nodes(i).Checked = True
                Next
            End If

            btnDeSelectProvider.Visible = True
            btnSelectProvider.Visible = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeSelectProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeSelectProvider.Click
        Try
            Dim i As Int16
            If trvProvider.Nodes.Count > 0 Then
                For i = 0 To trvProvider.Nodes.Count - 1
                    trvProvider.Nodes(i).Checked = False
                Next
            End If

            btnDeSelectProvider.Visible = False
            btnSelectProvider.Visible = True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearchDiagnosis_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchDiagnosis.TextChanged
        Try

      
            AddDiagnosis(Trim(txtSearchDiagnosis.Text))
            CheckSelectedNodes()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Search, ex.ToString(), ActivityOutCome.Failure)

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

    Private Sub AddDiagnosis(Optional ByVal SearchText As String = "")

        Dim rootnode As myTreeNode
        ' Dim dt As New DataTable
        Dim i As Int64
        Dim ICDType As String = ""
        Dim drr As DataRow()
        Try
            If (rbICD9.Checked = True) Then
                ICDType = "9"
            End If
            If (rbICD10.Checked = True) Then
                ICDType = "10"
            End If
            If (rbALL.Checked = True) Then
                ICDType = "0"
            End If
            trvDiagnosis.Visible = False

            If trvDiagnosis.GetNodeCount(False) > 0 Then
                trvDiagnosis.Nodes.Item(0).Remove()
                trvDiagnosis.Nodes.Clear()
                rootnode = New myTreeNode("Diagnosis", -1)
                rootnode.ForeColor = Color.Black
                rootnode.ImageIndex = 0
                rootnode.SelectedImageIndex = 0
                trvDiagnosis.Nodes.Add(rootnode)
            End If
            trvDiagnosis.Visible = True
            If (IsNothing(_dtICD)) Then
                _dtICD = objPatient.FillDiagnosis("", ICDType)

            End If
            trvDiagnosis.BeginUpdate()
            If (IsNothing(_dtICD) = False) Then
                If SearchText.Trim() <> "" Then
                    drr = _dtICD.Select("sICD9code like '%" & SearchText & "%'")


                    If (drr.Length > 0) Then
                        For i = 0 To drr.Length - 1
                            ''If dt.Rows(i)(0) <> "" Or dt.Rows(i)(1) <> "" Then
                            'Dim mychildnode As New myTreeNode
                            'mychildnode.Text = Convert.ToString(dt.Rows(i)(0)) & "-" & Convert.ToString(dt.Rows(i)(1))
                            ' ''mychildnode = New myTreeNode(dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
                            ''mychildnode.NodeName = New myTreeNode(dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
                            'mychildnode.ForeColor = Color.Black
                            'trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                            ''End If

                            Dim mychildnode As New myTreeNode
                            mychildnode.Text = Convert.ToString(drr(i)(0))
                            ''& "-" & Convert.ToString(dt.Rows(i)(1))
                            mychildnode.Tag = drr(i)(0).ToString().Trim  '' ICD9Code
                            mychildnode.DrugName = drr(i)(0).ToString()   '' ICD9Code
                            ' mychildnode.Dosage = dt.Rows(i)(1).ToString() '' ICD9 description
                            mychildnode.ForeColor = Color.Black
                            mychildnode.ImageIndex = 2
                            mychildnode.SelectedImageIndex = 2
                            trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                        Next
                    End If
                Else
                    For i = 0 To _dtICD.Rows.Count - 1
                        ''If dt.Rows(i)(0) <> "" Or dt.Rows(i)(1) <> "" Then
                        'Dim mychildnode As New myTreeNode
                        'mychildnode.Text = Convert.ToString(dt.Rows(i)(0)) & "-" & Convert.ToString(dt.Rows(i)(1))
                        ' ''mychildnode = New myTreeNode(dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
                        ''mychildnode.NodeName = New myTreeNode(dt.Rows(i)(0), CType(dt.Rows(i)(1), String))
                        'mychildnode.ForeColor = Color.Black
                        'trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                        ''End If

                        Dim mychildnode As New myTreeNode
                        mychildnode.Text = Convert.ToString(_dtICD.Rows(i)(0))
                        ''& "-" & Convert.ToString(dt.Rows(i)(1))
                        mychildnode.Tag = _dtICD.Rows(i)(0).ToString().Trim  '' ICD9Code
                        mychildnode.DrugName = _dtICD.Rows(i)(0).ToString()   '' ICD9Code
                        ' mychildnode.Dosage = dt.Rows(i)(1).ToString() '' ICD9 description
                        mychildnode.ForeColor = Color.Black
                        mychildnode.ImageIndex = 2
                        mychildnode.SelectedImageIndex = 2
                        trvDiagnosis.Nodes.Item(0).Nodes.Add(mychildnode)
                    Next
                End If
            End If
            


            trvDiagnosis.ExpandAll()
            If (trvDiagnosis.Nodes.Count > 0) Then
                trvDiagnosis.SelectedNode = trvDiagnosis.Nodes.Item(0)
            End If
            trvDiagnosis.EndUpdate()
        Catch ex As Exception
            'Throw ex
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub btnSelectLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectLocation.Click
        Try
            Dim i As Int16
            If trvLocation.Nodes.Count > 0 Then
                For i = 0 To trvLocation.Nodes.Count - 1
                    trvLocation.Nodes(i).Checked = True
                Next
            End If
            btnSelectLocation.Visible = False
            btnbtnDeselectLocation.Visible = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnbtnDeselectLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbtnDeselectLocation.Click
        Try
            Dim i As Int16
            If trvLocation.Nodes.Count > 0 Then
                For i = 0 To trvLocation.Nodes.Count - 1
                    trvLocation.Nodes(i).Checked = False
                Next
            End If
            btnSelectLocation.Visible = True
            btnbtnDeselectLocation.Visible = False

        Catch ex As Exception

        End Try
    End Sub

    Private Sub trvDiagnosis_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvDiagnosis.AfterCheck
        Try
            '' ''
            Dim _Node As myTreeNode
            _Node = e.Node
            If _Node.Text = "Diagnosis" Then
                If _Node.Checked = True Then
                    For Each _Node In trvDiagnosis.Nodes(0).Nodes
                        _Node.Checked = True
                    Next
                Else
                    For Each _Node In trvDiagnosis.Nodes(0).Nodes
                        _Node.Checked = False
                    Next
                End If
            End If
            ''
            ' If e.Node.Text <> "Diagnosis" Then
            If _Node.Checked = True Then

                Dim _ID As String = _Node.Tag
                If arrSelectedNodeIDs.Contains(_ID) = False Then
                    If oColSelectedNodes.Contains(_Node.Text) = False Then
                        oColSelectedNodes.Add(_Node, _Node.Text)
                        arrSelectedNodeIDs.Add(_ID)
                    End If
                End If
                ' oColSelectedNodes.Add(e.Node, e.Node.Text)
                ' trvDiagnosis.SelectedNode = e.Node
            Else
                '' Dim _ID As Int64 = e.Node.Tag
                Dim _ID As String = _Node.Tag
                If arrSelectedNodeIDs.Contains(_ID) = True Then
                    If oColSelectedNodes.Contains(_Node.Text) Then
                        oColSelectedNodes.Remove(_Node.Text)
                        arrSelectedNodeIDs.Remove(_ID)
                    End If

                End If
                'If oColSelectedNodes.Contains(e.Node.Text) Then
                'oColSelectedNodes.Remove(e.Node.Text)
                'End If
            End If
            '' ''
            'If bChildTrigger Then

            '    CheckAllChildren(e.Node, e.Node.Checked)
            '    If e.Node.Text <> "Diagnosis" Then
            '        If e.Node.Checked = True Then

            '            Dim _ID As String = e.Node.Tag
            '            If arrSelectedNodeIDs.Contains(_ID) = False Then
            '                If oColSelectedNodes.Contains(e.Node.Text) = False Then
            '                    oColSelectedNodes.Add(e.Node, e.Node.Text)
            '                    arrSelectedNodeIDs.Add(_ID)
            '                End If
            '            End If
            '            ' oColSelectedNodes.Add(e.Node, e.Node.Text)
            '            ' trvDiagnosis.SelectedNode = e.Node
            '        Else
            '            '' Dim _ID As Int64 = e.Node.Tag
            '            Dim _ID As String = e.Node.Tag
            '            If arrSelectedNodeIDs.Contains(_ID) = True Then
            '                If oColSelectedNodes.Contains(e.Node.Text) Then
            '                    oColSelectedNodes.Remove(e.Node.Text)
            '                    arrSelectedNodeIDs.Remove(_ID)
            '                End If

            '            End If
            '            'If oColSelectedNodes.Contains(e.Node.Text) Then
            '            'oColSelectedNodes.Remove(e.Node.Text)
            '            'End If
            '        End If
            '    End If

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''To check nodes present in collection(trvDiagnosis)
    Private Sub CheckSelectedNodes()
        If trvDiagnosis.CheckBoxes And arrSelectedNodeIDs.Count > 0 Then
            Dim iNode As myTreeNode
            For Each iNode In trvDiagnosis.Nodes(0).Nodes
                If arrSelectedNodeIDs.Contains(iNode.Tag) Then
                    iNode.Checked = True
                End If
            Next
        End If
    End Sub
    ''

#Region " Tree Check Uncheck "
    'Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
    '    ' bParentTrigger = False
    '    For Each ctn As TreeNode In tn.Nodes
    '        bChildTrigger = False
    '        ctn.Checked = bCheck
    '        bChildTrigger = True
    '        ''
    '        '' ''If bCheck = True Then
    '        '' ''    If oColSelectedNodes.Contains(ctn.Text) = False Then
    '        '' ''        oColSelectedNodes.Add(ctn, ctn.Text)
    '        '' ''    End If
    '        '' ''    trvDiagnosis.SelectedNode = ctn
    '        '' ''Else
    '        '' ''    If oColSelectedNodes.Contains(ctn.Text) Then
    '        '' ''        oColSelectedNodes.Remove(ctn.Text)
    '        '' ''    End If
    '        '' ''End If
    '        ''
    '        If bCheck = True Then
    '            ''
    '            Dim _ID As String = ctn.Tag
    '            If arrSelectedNodeIDs.Contains(_ID) = False Then
    '                'If oColSelectedNodes.Contains(ctn.Text) = False Then
    '                'oColSelectedNodes.Add(ctn, ctn.Text)
    '                arrSelectedNodeIDs.Add(_ID)
    '                'trvDiagnosis.SelectedNode = ctn
    '            End If
    '            'End If
    '            If oColSelectedNodes.Contains(ctn.Text) = False Then
    '                oColSelectedNodes.Add(ctn, ctn.Text)
    '                trvDiagnosis.SelectedNode = ctn
    '            End If
    '            ''
    '            'If oColSelectedNodes.Contains(ctn.Text) = False Then
    '            '    oColSelectedNodes.Add(ctn, ctn.Text)
    '            'End If
    '            'trvDiagnosis.SelectedNode = ctn
    '        Else
    '            Dim _ID As String = ctn.Tag
    '            If arrSelectedNodeIDs.Contains(_ID) = True Then
    '                'If oColSelectedNodes.Contains(ctn.Text) Then
    '                '    oColSelectedNodes.Remove(ctn.Text)
    '                arrSelectedNodeIDs.Remove(_ID)
    '                'End If

    '            End If
    '            If oColSelectedNodes.Contains(ctn.Text) Then
    '                oColSelectedNodes.Remove(ctn.Text)
    '                'arrSelectedNodeIDs.Remove(_ID)
    '            End If
    '            ''
    '        End If
    '        ''
    '        ''
    '        CheckAllChildren(ctn, bCheck)
    '    Next
    '    ' bParentTrigger = True
    'End Sub

#End Region

    Private Sub trvDiagnosis_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvDiagnosis.MouseDoubleClick
        Try
            
            FillSelectedDiagnosis()


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillSelectedDiagnosis()
        Try
            ''20100224
            Dim oCollection As Collection = SelectedNodes()
            Dim myTNode As myTreeNode = Nothing
            Dim _IsNodePresent As Boolean = False
            Dim _IsrootNode As Boolean = False
            If IsNothing(oCollection) = False Then
                If oCollection.Count > 0 Then
                    For i As Integer = 1 To oCollection.Count

                        If Not trvDiagnosis.SelectedNode Is myTNode Then
                           
                            For Each oNode As TreeNode In trvselecteddia.Nodes

                                If oNode.Tag = CType(oCollection.Item(i), myTreeNode).Tag Then
                                    'If oNode.Text.Trim = CType(oCollection.Item(i), myTreeNode).Text Then
                                    _IsNodePresent = True
                                    Exit For
                                Else
                                    _IsNodePresent = False
                                End If
                            Next

                        End If
                        If _IsNodePresent = False Then

                            myTNode = CType(oCollection(i), myTreeNode)
                            myTNode = CType(myTNode.Clone, myTreeNode)
                            myTNode.ImageIndex = 1
                            myTNode.SelectedImageIndex = 1
                            If IsNothing(myTNode.Tag) Then
                                _IsrootNode = True
                                'Exit For
                            Else
                                If _IsrootNode = True Then
                                    Exit For
                                End If
                                trvselecteddia.Nodes.Add(myTNode)
                            End If
                            If _IsrootNode = True AndAlso IsNothing(myTNode.Tag) = False Then
                                trvselecteddia.Nodes.Add(myTNode)
                            End If
                        End If
                        _IsrootNode = False
                    Next
                    'Else
                    '    For Each oNode As TreeNode In trvDiagnosis.Nodes(0).Nodes
                    '        Dim _ID As String = oNode.Tag

                    '        If oColSelectedNodes.Contains(oNode.Text) = False Then
                    '            oColSelectedNodes.Add(oNode, oNode.Text)
                    '            Dim myTNode1 As New myTreeNode
                    '            myTNode1 = CType(oNode.Clone, myTreeNode)
                    '            myTNode1.ImageIndex = 1
                    '            myTNode1.SelectedImageIndex = 1
                    '            If IsNothing(myTNode1.Tag) Then
                    '                Exit For
                    '            Else
                    '                trvselecteddia.Nodes.Add(myTNode1)
                    '            End If
                    '        End If

                    '    Next

                    'End If
                End If
            End If

            'If Not trvDiagnosis.SelectedNode Is myTNode Then

            '    For Each oNode As TreeNode In trvselecteddia.Nodes
            '        If oNode.Text.Trim = trvDiagnosis.SelectedNode.Text.Trim Then
            '            Exit Sub
            '        End If
            '    Next

            'myTNode = CType(trvDiagnosis.SelectedNode, myTreeNode)

            '' myTNode = CType(myTNode.Clone, myTreeNode)
            'myTNode.ImageIndex = 1
            'myTNode.SelectedImageIndex = 1
            ''trvselecteddia.Nodes.Add(myTNode)
            ''end 20100224
            ''Commenetd by Mayuri:20100224
            'Dim myTNode As myTreeNode

            'myTNode = CType(trvDiagnosis.Nodes.Item(0), myTreeNode)

            'If Not trvDiagnosis.SelectedNode Is myTNode Then

            '    For Each oNode As TreeNode In trvselecteddia.Nodes
            '        If oNode.Text.Trim = trvDiagnosis.SelectedNode.Text.Trim Then
            '            Exit Sub
            '        End If
            '    Next

            '    myTNode = CType(trvDiagnosis.SelectedNode.Clone, myTreeNode)
            '    myTNode.ImageIndex = 1
            '    myTNode.SelectedImageIndex = 1
            '    trvselecteddia.Nodes.Add(myTNode)
            ''End code commenetd by Mayuri:20100224

            'Dim _Node As Int32 = trvselecteddia.Nodes.IndexOf(trvDiagnosis.SelectedNode)
            'If True Then
            '    ''trvselecteddia.Nodes.Find(trvDiagnosis.SelectedNode.Text,False) = False Then
            '    myTNode = New myTreeNode
            '    'If trvDiagnosis.Nodes.Contains(trvDiagnosis.SelectedNode) = False Then
            '    myTNode = CType(trvDiagnosis.SelectedNode.Clone, myTreeNode)
            '    trvselecteddia.Nodes.Add(myTNode)
            'End If
            'If chkDiagnosis.Items.Contains(trvDiagnosis.SelectedNode.Text) = False Then
            'chkDiagnosis.Items.Add(trvDiagnosis.SelectedNode.Text)
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





#Region "To view/Print the Report"
    ''' <summary>
    ''' To View the Report
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ViewReport()


        'Dim dt As New DataTable()

        Dim ofrm As frmrpt_PatientICD9CPTViewer = Nothing
        'Dim strSQL As String = ""
        Dim strSQLClinic As String = ""
        'Dim strSQLProcedure As String = ""
        'Dim strDiagnosiscount As String = ""
        'Dim Dt_DiagnosisCount As New DataTable()
        'Dim dtProcunitcount As New DataTable()

        Try
            GetVariables()
            If txtFrom.Text = "" And txtToproc.Text = "" Or txtFrom.Text <> "" And txtToproc.Text <> "" Then
            Else
                If txtFrom.Text = "" Or txtToproc.Text <> "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtFrom.Text <> "" Or txtToproc.Text = "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            'If dtpicFrom.Value() > dtpicTo.Value() Then
            '    MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            Dim bIsEndDateSmaller As Boolean = CompareDates()
            If bIsEndDateSmaller = True Then
                MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim dtPAtient As DataTable = FetchDetails()
            ''dt = FetchPatients(strSQL)
            ' Dt_DiagnosisCount = FetchDiagnosiscount(dt, strDiagnosiscount)
            'dtProcunitcount = FetchProcCount(strSQLProcedure)
            ''DesignPatientGrid(dt)
            ''DesignGrid(dt)
            'FetchSummaryDetails()
            ''DesignGrid(dt)
            If (IsNothing(dtPAtient)) Then
                MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else

                If dtPAtient.Rows.Count > 0 Then
                    strSQLClinic = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                                              & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"

                    Dim ds As New ds_PtICD9CPT
                    Dim da1 As New SqlDataAdapter(strSQLClinic, GetConnectionString)
                    da1.Fill(ds, "dt_Clinic_MST")
                    da1.Dispose()
                    da1 = Nothing

                    ds = FetchSummaryDetails(ds)
                    ds = FetchMainReportDetails(ds)

                    'Dim da2 As New SqlDataAdapter(strSQLProcedure, GetConnectionString)
                    'da2.Fill(ds, "Dt_Procunitcount")
                    Dim oRpt As New rpt_PatientICD9CPT()

                    ''ds.Dt_DiagnosisCount.Merge(Dt_DiagnosisCount)
                    'da2.Fill(ds, "Dt_DiagnosisCount")
                    'ds.Tables.Add(Dt_DiagnosisCount)
                    oRpt.SetDataSource(ds)

                    'oRpt.Subreports.Item("SubRptPtICD9CPTSummary.rpt").SetDataSource(ds.Tables("dt_SummaryInfo"))
                    oRpt.Subreports.Item("SubRpt_PtICD9_CPT.rpt").SetDataSource(ds.Tables("dt_summaryDetails"))
                    'Dim fgroup As FieldObject = DirectCast(oRpt.Subreports.Item("SubRpt_PtICD9_CPT.rpt").ReportDefinition.Sections("GroupFooterSection3").ReportObjects(""), FieldObject)
                    'fgroup.Left = 620
                    ofrm = New frmrpt_PatientICD9CPTViewer()
                    ofrm.crtlrptview_patientICD9CPT.ReportSource = oRpt

                    ofrm.ShowInTaskbar = False
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    oRpt.Dispose()
                    oRpt = Nothing
                    ds.Dispose()
                    ds = Nothing
                Else
                    MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                dtPAtient.Dispose()
                dtPAtient = Nothing
            End If
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(ofrm) = False) Then
                ofrm.Dispose()
                ofrm = Nothing
            End If
        End Try
        ' ShowReportList()
    End Sub
    Private Function CompareDates() As Boolean
        If dtpicFrom.Value.ToString() <> String.Empty Then
            If dtpicTo.Value.ToString() <> String.Empty Then
                Dim startTime As DateTime = Convert.ToDateTime(dtpicFrom.Value)
                Dim endTime As DateTime = Convert.ToDateTime(dtpicTo.Value)
                Dim ts As TimeSpan
                ts = endTime.Subtract(startTime)
                If (ts.Days >= 0) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function
    ''' <summary>
    ''' To Print the Printer against Printer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintReport()

        'Dim strSQL As String = ""
        Dim strSQLClinic As String = ""


        Try
            GetVariables()
            If txtFrom.Text = "" And txtToproc.Text = "" Or txtFrom.Text <> "" And txtToproc.Text <> "" Then
            Else

                If txtFrom.Text = "" Or txtToproc.Text <> "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf txtFrom.Text <> "" Or txtToproc.Text = "" Then
                    MessageBox.Show("Procedure range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If
            'If dtpicFrom.Value() > dtpicTo.Value() Then
            '    MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            Dim bIsEndDateSmaller As Boolean = CompareDates()
            If bIsEndDateSmaller = True Then
                MessageBox.Show("Date range is not valid.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim dtPAtient As DataTable = FetchDetails()
            If (IsNothing(dtPAtient)) Then
                MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                ''DesignGrid(dt)
                If dtPAtient.Rows.Count > 0 Then
                    strSQLClinic = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                                   & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"

                    'Dim da As New SqlDataAdapter(strSQL, GetConnectionString)
                    'da.Fill(ds, "Dt_PatientICD9_CPT")
                    Dim ds As New ds_PtICD9CPT
                    Dim da1 As New SqlDataAdapter(strSQLClinic, GetConnectionString)
                    da1.Fill(ds, "dt_Clinic_MST")
                    da1.Dispose()
                    da1 = Nothing

                    ds = FetchSummaryDetails(ds)
                    ds = FetchMainReportDetails(ds)
                    Dim oRpt As New rpt_PatientICD9CPT()
                    oRpt.SetDataSource(ds)
                    'oRpt.Subreports.Item("SubRptPtICD9CPTSummary.rpt").SetDataSource(ds.Tables("dt_SummaryInfo"))
                    oRpt.Subreports.Item("SubRpt_PtICD9_CPT.rpt").SetDataSource(ds.Tables("dt_summaryDetails"))

                    Try
                    
                        If _DefaultPrinter Then
                            oRpt.PrintToPrinter(1, False, 0, 0)
                        Else
                            Dim printDialog As New PrintDialog()

                            If PrintDialog.ShowDialog() = DialogResult.OK Then
                                oRpt.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName
                                oRpt.PrintToPrinter(PrintDialog.PrinterSettings.Copies, False, PrintDialog.PrinterSettings.FromPage, PrintDialog.PrinterSettings.ToPage)
                            End If
                            If Not IsNothing(printDialog) Then
                                printDialog.Dispose()
                                printDialog = Nothing
                            End If

                        End If


                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                        ex = Nothing
                    End Try

                    oRpt.Dispose()
                    oRpt = Nothing
                    ds.Dispose()
                    ds = Nothing

                Else
                    MessageBox.Show("No records found with the selected criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                dtPAtient.Dispose()
                dtPAtient = Nothing
            End If
           

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region
#Region "Procedure KeyPress Events"
    Private Sub txtFrom_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFrom.KeyPress
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtToproc_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtToproc.KeyPress
        If Not ((e.KeyChar >= ChrW(48) AndAlso e.KeyChar <= ChrW(57)) OrElse (e.KeyChar = ChrW(8))) Then
            e.Handled = True
        End If
    End Sub
#End Region
    Private Sub GetVariables()
        Try
            ''20100222
            strProviderIDs = ""
            For count = 0 To trvProvider.Nodes.Count - 1
                If trvProvider.Nodes(count).Checked = True Then
                    If strProviderIDs = "" Then
                        strProviderIDs = trvProvider.Nodes(count).Tag
                    Else
                        strProviderIDs = strProviderIDs & "," & trvProvider.Nodes(count).Tag
                    End If

                End If
            Next

            strLocations = ""
            For count = 0 To trvLocation.Nodes.Count - 1
                If trvLocation.Nodes(count).Checked = True Then
                    If strLocations = "" Then
                        strLocations = "'" & trvLocation.Nodes(count).Text.Trim.Replace("'", "''") & "'"
                    Else
                        strLocations = strLocations & "," & "'" & trvLocation.Nodes(count).Text.Trim.Replace("'", "''") & "'"
                    End If
                End If
            Next

            strICD9s = ""
            For count = 0 To trvselecteddia.Nodes.Count - 1
                If trvselecteddia.Nodes(count).Tag IsNot Nothing Then


                    If strICD9s = "" Then
                        strICD9s = "'" & CType(trvselecteddia.Nodes(count), myTreeNode).Tag.Trim.Replace("'", "''") & "'"
                    Else
                        strICD9s = strICD9s & "," & "'" & trvselecteddia.Nodes(count).Tag.Trim.Replace("'", "''") & "'"
                    End If
                End If
            Next

            strCPTs = ""
            If txtProcedure.Text.Trim <> "" Then
                strCPTs = "'" & txtProcedure.Text.Trim.Replace("'", "''") & "'"
            End If

            ''_rangeCPT = ""
            ''Dim i As Int64
            ''If TextBox2.Text.Trim <> "" And TextBox3.Text.Trim <> "" Then
            ''    Dim _From As Int64
            ''    Dim _To As Int64
            ''    Dim _isValidInteger As Boolean = True
            ''    Try
            ''        _From = Convert.ToInt64(TextBox2.Text.Trim)

            ''        _To = Convert.ToInt64(TextBox3.Text.Trim)
            ''    Catch ex As Exception
            ''        _isValidInteger = False
            ''    End Try

            ''    If (_isValidInteger = True) Then

            ''        For i = Convert.ToInt64(TextBox2.Text.Trim) To Convert.ToInt64(TextBox3.Text.Trim)
            ''            If _rangeCPT = "" Then
            ''                _rangeCPT = "'" & i.ToString().Replace("'", "''") & "'"
            ''            Else
            ''                _rangeCPT = _rangeCPT & "," & "'" & i.ToString().Replace("'", "''") & "'"
            ''            End If

            ''        Next

            ''    End If

            ''End If
            ''20100222
            strCPTFrom = ""
            strCPTTo = ""
            If txtFrom.Text.Trim <> "" Then
                strCPTFrom = "'" & txtFrom.Text.Trim.Replace("'", "''") & "'"
            End If
            If txtToproc.Text.Trim <> "" Then
                strCPTTo = "'" & txtToproc.Text.Trim.Replace("'", "''") & "'"
            End If
            ''End 20100222
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Exam, ActivityCategory.None, ActivityType.Open, ex.ToString(), ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added to show notes ,Issue:#5663-Tooltip problem – data truncating
    Private Sub C1grdPatients_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1grdPatients.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClearAllSelectedDx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAllSelectedDx.Click
        trvselecteddia.Nodes.Clear()
        'SelectedNodes.Clear()
    End Sub

    'code start by nilesh on 20101027
    'generate the message queue entry for A28
    Private Sub GeneratePublicSurveillanceHL7MessageQueue()
        Try
            ' Dim _dr As DataRow
            Dim _dtDiagnosis As New DataTable
            _dtDiagnosis.Columns.Add("PatientID")
            _dtDiagnosis.Columns.Add("Diagnosis")

            Dim dv As DataView
            dv = CType(C1grdPatients.DataSource, DataTable).DefaultView
            dv.Sort = "nPatientID"

            Dim DestTable As DataTable = Nothing
            Dim strColsToExport() As String = {"nPatientID", "Diagnosis", "Patient Name"}
            'Copy Only three columns from the datatableDim strColsToExport() As String={"EmpID","FirstName","DateOfJoin"}
            DestTable = dv.ToTable("tempTableName", False, strColsToExport)
            DestTable.Dispose()
            DestTable = Nothing

            Dim _sTemp As String = ""


            For i As Integer = 0 To dv.Count - 1
                Dim index As Integer = 1
                Dim bIsRowfound As Boolean = False
                _sTemp = ""
                For index = 0 To _dtDiagnosis.Rows.Count - 1
                    If _dtDiagnosis.Rows(index)("PatientID").ToString = dv.Table.Rows(i)("nPatientID").ToString Then
                        _sTemp = _dtDiagnosis.Rows(index)("Diagnosis").ToString
                        _dtDiagnosis.Rows(index).BeginEdit()
                        bIsRowfound = True
                        Exit For
                    End If
                Next

                If _sTemp <> "" Then
                    _sTemp = _sTemp & ", " & dv.Table.Rows(i)("Diagnosis").ToString()
                Else
                    _sTemp = dv.Table.Rows(i)("Diagnosis").ToString()
                End If
                'Else
                '_sTemp = ""
                '_sTemp = dv.Table.Rows(i)("Diagnosis").ToString()
                'End If

                'If DestTable.Rows(_Rcount)("nPatientID") = DestTable.Rows(_Rcount + 1)("nPatientID") And DestTable.Rows(_Rcount)("Patient Name") = DestTable.Rows(_Rcount + 1)("Patient Name") Then
                '    ' _sTemp = DestTable.Rows(_Rcount)("nPatientID").ToString() + "   " + DestTable.Rows(_Rcount)("Diagnosis").ToString() + "," + DestTable.Rows(_Rcount + 1)("Diagnosis").ToString() + "    " + DestTable.Rows(_Rcount + 1)("Patient Name").ToString()
                'Else
                '    _sTemp = DestTable.Rows(_Rcount)("nPatientID").ToString() + "   " + DestTable.Rows(_Rcount)("Diagnosis").ToString() + "    " + DestTable.Rows(_Rcount + 1)("Patient Name").ToString()
                'End If
                If bIsRowfound = True Then
                    _dtDiagnosis.Rows.RemoveAt(index)
                Else
                    'Dim r As DataRow
                    'r = _dtDiagnosis.NewRow()
                    'r("PatientID") = dv.Table.Rows(i)("nPatientID")
                    'r("Diagnosis") = _sTemp
                    '_dtDiagnosis.Rows.Add(r)
                End If

                Dim r As DataRow
                r = _dtDiagnosis.NewRow()
                r("PatientID") = dv.Table.Rows(i)("nPatientID")
                r("Diagnosis") = _sTemp
                _dtDiagnosis.Rows.Add(r)
            Next

            Dim oGeneralInterface As New clsGeneralInterface()
            If Not IsNothing(_dtDiagnosis) Then
                If _dtDiagnosis.Rows.Count > 0 Then
                    For i As Int32 = 0 To _dtDiagnosis.Rows.Count - 1
                        oGeneralInterface.SendA28(_dtDiagnosis.Rows(i)("PatientID"), _dtDiagnosis.Rows(i)("Diagnosis"))
                    Next
                    MessageBox.Show("Patient Information is added to HL7 MessageQueue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No record to add in HL7 MessageQueue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                _dtDiagnosis.Dispose()
                _dtDiagnosis = Nothing
            End If
            oGeneralInterface.Dispose()
            oGeneralInterface = Nothing

            ''MessageBox.Show("ex.ToString", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'code end by nilesh on 20101027

    Private Function GenerateSyndromicSurveillanceHL7MessageQueue(Optional ByVal bIsUpdate As Boolean = False) As Boolean
        Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oGeneralInterface As New clsGeneralInterface()

        Dim dtReportData As DataTable = Nothing
        Dim sMessageType As String = String.Empty

        Try
            Dim dv As DataView = CType(C1grdPatients.DataSource, DataTable).DefaultView
            dv.Sort = "nPatientID"

            If dv.Count <= 0 Then
                MessageBox.Show("No record to add in HL7 MessageQueue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Try
            End If

            Dim strColsToExport() As String = {"nPatientID", "nExamID"}
            dtReportData = dv.ToTable(True, strColsToExport)

            oDBLayer.Connect(False)

            Dim dtTemp As New DataTable
            dtTemp.Columns.Add("MessageType")
            dtTemp.Columns.Add("PatientID")
            dtTemp.Columns.Add("VisitID")

            For Each drRow As DataRow In dtReportData.Rows()
                Dim bIsExamFinished As Boolean = False
                Dim sSQL As String = "SELECT bIsFinished FROM PatientExams WHERE nPatientID = " & drRow("nPatientID") & " AND nExamID = " & drRow("nExamID") & ""

                bIsExamFinished = Convert.ToBoolean(oDBLayer.ExecuteScalar_Query(sSQL))
                If bIsUpdate Then
                    sMessageType = "A08"
                ElseIf bIsExamFinished Then
                    sMessageType = "A03"
                Else
                    sMessageType = "A04"
                End If

                sSQL = "SELECT ISNULL(nVisitID,0) FROM PatientExams WHERE nPatientID = " & drRow("nPatientID") & " AND nExamID = " & drRow("nExamID") & ""

                Dim nVisitID As Int64 = Convert.ToInt64(oDBLayer.ExecuteScalar_Query(sSQL))

                If dtTemp.Select("MessageType = '" & sMessageType & "' AND PatientID = '" & drRow("nPatientID") & "' AND VisitID = '" & nVisitID & "'").Length <= 0 Then
                    oGeneralInterface.SendSyndromicSurveillance(sMessageType, drRow("nPatientID"), nVisitID)

                    Dim r As DataRow
                    r = dtTemp.NewRow()
                    r("MessageType") = sMessageType
                    r("PatientID") = drRow("nPatientID")
                    r("VisitID") = nVisitID
                    dtTemp.Rows.Add(r)
                End If
            Next

            If Not IsNothing(dtTemp) Then
                dtTemp.Dispose()
                dtTemp = Nothing
            End If

            oDBLayer.Disconnect()
            MessageBox.Show("Patient Information is added to HL7 MessageQueue", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If

            If Not IsNothing(oGeneralInterface) Then
                oGeneralInterface.Dispose()
                oGeneralInterface = Nothing
            End If

            If Not IsNothing(dtReportData) Then
                dtReportData.Dispose()
                dtReportData = Nothing
            End If
        End Try
        Return Nothing
    End Function


    Private Sub tlbbtnmenuItm_HL7_MU2_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnmenuItm_HL7_MU2.Click
        GenerateSyndromicSurveillanceHL7MessageQueue()
    End Sub

    Private Sub tlbbtnmenuItm_HL7_MU1_Click(sender As System.Object, e As System.EventArgs) Handles tlbbtnmenuItm_HL7_MU1.Click
        GeneratePublicSurveillanceHL7MessageQueue()
    End Sub

    Private Sub rbICD9_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbICD9.CheckedChanged
        If (rbICD9.Checked = True) Then
            trvDiagnosis.Nodes.Clear()
            trvselecteddia.Nodes.Clear()
            oColSelectedNodes.Clear()
            arrSelectedNodeIDs.Clear()
            FillDiagnosisTree("9")
            rbICD9.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD9.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbICD10_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbICD10.CheckedChanged
        If (rbICD10.Checked = True) Then
            trvDiagnosis.Nodes.Clear()
            trvselecteddia.Nodes.Clear()
            oColSelectedNodes.Clear()
            arrSelectedNodeIDs.Clear()
            FillDiagnosisTree("10")
            rbICD10.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbICD10.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbALL_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbALL.CheckedChanged
        If (rbALL.Checked = True) Then
            trvDiagnosis.Nodes.Clear()
            trvselecteddia.Nodes.Clear()
            oColSelectedNodes.Clear()
            arrSelectedNodeIDs.Clear()
            FillDiagnosisTree("0")
            rbALL.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbALL.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub frmRpt_PatientICD9CPT_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        If (IsNothing(objPatient) = False) Then
            objPatient = Nothing
        End If
        If (IsNothing(oProcedureListControl) = False) Then
            CloseProcedureControl()
        End If
        If (IsNothing(_dtICD) = False) Then
            _dtICD.Dispose()
            _dtICD = Nothing
        End If
    End Sub

    Private Sub tlbmenuItm_HL7_MU3_A04_Click(sender As System.Object, e As System.EventArgs) Handles tlbmenuItm_HL7_MU3_A04.Click
        GenerateSyndromicSurveillanceHL7MessageQueue()
    End Sub

    Private Sub tlbmenuItm_HL7_MU3_A08_Click(sender As System.Object, e As System.EventArgs) Handles tlbmenuItm_HL7_MU3_A08.Click
        GenerateSyndromicSurveillanceHL7MessageQueue(True)
    End Sub

    Private Sub RbDxbyProc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RbDxbyProc.CheckedChanged
        If RbDxbyProc.Checked = True Then
            RbDxbyProc.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RbDxbyProc.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub RbDrDxProc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RbDrDxProc.CheckedChanged
        If RbDrDxProc.Checked = True Then
            RbDrDxProc.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RbDrDxProc.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub RbDrDx_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RbDrDx.CheckedChanged
        If RbDrDx.Checked = True Then
            RbDrDx.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RbDrDx.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub RbDrDxLocation_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RbDrDxLocation.CheckedChanged
        If RbDrDxLocation.Checked = True Then
            RbDrDxLocation.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            RbDrDxLocation.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
End Class