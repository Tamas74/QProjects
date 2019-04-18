Imports gloGallery
Imports gloEMRGeneralLibrary
Imports gloGlobal.gloICD
Imports System.Xml
Imports System.Windows.Data
Imports System.Xml.Linq
Imports gloUIControlLibrary.WPFForms.PQRS


Public Class frm_Diagnosis
    Dim _PatientID As Long
    Dim _blnICDTransition As Boolean = False
    'Dim frmICD9To10Mapping As gloUIControlLibrary.WPFForms.frmICD9To10Mapping = Nothing
    Dim ICD9To10MappingControl As gloUIControlLibrary.WPFUserControl.ICD10.ICD9To10MappingTextTreeView = Nothing
    Dim ICD9To10MappingDBLayer As gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping.clsICD9To10MappingDBLayer = Nothing
    Dim dtActiveCPTTable As DataTable
    Dim diag_activewindow As IntPtr = IntPtr.Zero



    Public Sub DisposeGlobalVariables()


        If (IsNothing(dtOrderbyCode) = False) Then
            dtOrderbyCode.Dispose()
            dtOrderbyCode = Nothing
        End If
        If (IsNothing(dtOrderbyCode_CPT) = False) Then
            dtOrderbyCode_CPT.Dispose()
            dtOrderbyCode_CPT = Nothing
        End If
        If (IsNothing(dtOrderbyDesc) = False) Then
            dtOrderbyDesc.Dispose()
            dtOrderbyDesc = Nothing
        End If
        If (IsNothing(dtOrderbyDesc_CPT) = False) Then
            dtOrderbyDesc_CPT.Dispose()
            dtOrderbyDesc_CPT = Nothing
        End If
        If (IsNothing(dtOrderbyDesc_MOD) = False) Then
            dtOrderbyDesc_MOD.Dispose()
            dtOrderbyDesc_MOD = Nothing
        End If
        If (IsNothing(dtOrderbyCode_MOD) = False) Then
            dtOrderbyCode_MOD.Dispose()
            dtOrderbyCode_MOD = Nothing
        End If


    End Sub

    Public Sub New(ByVal m_VisitID, ByVal m_ExamID, ByVal PatientID, Optional ByVal viewdiagnosis = False, Optional ByVal IsFromExam = False, Optional ByVal _activewindow = 0)
        MyBase.New()
        VisitId = m_VisitID
        ExamID = m_ExamID
        ''Sandip Darade 20090712 
        ''view diagnosis for the exam only
        _IsViewDiagnosis = viewdiagnosis
        _IsFromExam = IsFromExam
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
        If _activewindow = 0 Then
            diag_activewindow = IntPtr.Zero
        Else
            diag_activewindow = _activewindow
        End If
        'Bug #47406: 00000415 : Word Modules
        'Primary Diagnosis reset when Diagnosis Screen Initialised
        frmPatientExam._PrimaryDiaForICD9Driven = ""
    End Sub
    'Added Code for DiagnosisScreenWidth
    Private _DiagnosisWidth As String = "DiagnosisScreenWidth"
    'Added Code for CPTScreenWidth
    Private _CPTWidth As String = "CPTScreenWidth"

    Public ReadOnly Property CPTWidth() As String
        Get
            Return _CPTWidth
        End Get
    End Property

    Public ReadOnly Property DiagnosisWidth() As String
        Get
            Return _DiagnosisWidth
        End Get
    End Property

    Public ReadOnly Property SelectedICD() As CodeRevision
        Get
            If RbICD9.Checked Then
                Return CodeRevision.ICD9
            ElseIf RbICD10.Checked Then
                Return CodeRevision.ICD10
            End If

            Return CodeRevision.ICD9
        End Get
    End Property

    Public Property blnICDTransition() As Boolean
        Get
            Return _blnICDTransition
        End Get
        Set(ByVal Value As Boolean)
            _blnICDTransition = Value
        End Set
    End Property

    Public Property ShowICD10Codes() As Boolean

#Region "Local Variables"
    Dim rootNode As myTreeNode
    Dim ExisingNode As myTreeNode
    Private VisitId As Long
    Private ExamID As Long
    Dim _IsViewDiagnosis As Boolean
    Dim _IsFromExam As Boolean
    Dim dtOrderbyCode As DataTable = Nothing
    Dim dtOrderbyCode_CPT As DataTable = Nothing
    Dim dtOrderbyDesc As DataTable = Nothing
    Dim dtOrderbyDesc_CPT As DataTable = Nothing
    Dim dtOrderbyDesc_MOD As DataTable = Nothing
    Dim dtOrderbyCode_MOD As DataTable = Nothing
    Dim _CPTClick As Boolean = False
    Dim _ModifierClick As Boolean = False
    Dim ICD9Count As Int64 = 0
    Dim CPTCount As Int64 = 0
    Dim ModifierCount As Int64 = 0




    Dim strICD9Code As String
    Dim strICD9Description As String
    Dim strCPTCode As String
    Dim strCPTDescription As String
    Dim strModCode As String
    Dim strModDescription As String
    Dim nICDRevision As Int16
#End Region
    'Added Code for Getting UserID
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim _UserID As Long = Convert.ToInt64(appSettings("UserID"))
#Region "Class Initializers"
    '  Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
    ' Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
#End Region

#Region "FlexGrid Column variables"
    Private Col_ICD9Code_Description As Integer = 0
    Private Col_ICD9Code As Integer = 1
    Private Col_ICD9Desc As Integer = 2
    Private COl_CPTCode As Integer = 3
    Private Col_CPTDesc As Integer = 4
    Private Col_ModCode As Integer = 5
    Private Col_ModDesc As Integer = 6

    Private Col_Timed_PT As Integer = 7
    Private Col_UnTimed_PT As Integer = 8

    Private Col_Units As Integer = 9
    Private Col_ICD9Count As Integer = 10
    Private Col_CPTCount As Integer = 11
    Private Col_ModCount As Integer = 12
    Private Col_SnomedCode As Integer = 13
    Private Col_SnomedDesc As Integer = 14
    Private Col_nICDRevision As Integer = 15
    Private Col_nConceptId As Integer = 16
    Private Col_sConceptDescription As Integer = 17

    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
    Private Col_nIsOneToOneMappingSnoMed As Integer = 18

    Private Col_ReasonConceptId As Integer = 19
    Private Col_ReasonConceptDescription As Integer = 20
    Private Col_Count = 21

#End Region



    Private Sub btnmodifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodifier.Click
        Try


            btnCPT.Enabled = False
            btnmodifier.Enabled = False
            '''' Set Modifier button on top
            pnl_btnmodifier.Dock = DockStyle.Top
            btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnmodifier.BackgroundImageLayout = ImageLayout.Stretch
            btnmodifier.Tag = "Selected"
            pnl_btnCPT.Dock = DockStyle.Bottom
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
            btnCPT.Tag = "UnSelected"
            _ModifierClick = True
            '''' Fill Modifer Data in grid
            Call FillModifiers()
            '---------------------
            txtsearchrht.Text = ""
            txtsearchrht.Focus()

            ''''For Modifier Radio Button Click
            _ModifierClick = True
            _CPTClick = False
            btnCPT.Enabled = True
            btnmodifier.Enabled = True
        Finally
            _ModifierClick = True
        End Try
    End Sub
    Private Sub btnAllDiagnosis_Click(sender As System.Object, e As System.EventArgs) Handles btnAllDiagnosis.Click
        If btnAllDiagnosis.Tag = "UnSelected" Then
            LayoutAllDiagnosis()
            FillICD9ICD10()
        End If
    End Sub

    Private Sub btnPatientProblemDiagnosis_Click(sender As System.Object, e As System.EventArgs) Handles btnPatientProblemDiagnosis.Click
        If btnPatientProblemDiagnosis.Tag = "UnSelected" Then
            LayoutPatientProblem()
            FillICD9ICD10()
        End If
    End Sub

    Private Sub LayoutAllDiagnosis()

        pnlAllDiagnosis.Dock = DockStyle.Top
        btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnAllDiagnosis.Tag = "Selected"

        pnlPatientProblemDiagnosis.Dock = DockStyle.Bottom
        btnPatientProblemDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnPatientProblemDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnPatientProblemDiagnosis.Tag = "UnSelected"

    End Sub

    Private Sub LayoutPatientProblem()
        pnlPatientProblemDiagnosis.Dock = DockStyle.Top
        btnPatientProblemDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnPatientProblemDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnPatientProblemDiagnosis.Tag = "Selected"

        pnlAllDiagnosis.Dock = DockStyle.Bottom
        btnAllDiagnosis.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnAllDiagnosis.BackgroundImageLayout = ImageLayout.Stretch
        btnAllDiagnosis.Tag = "UnSelected"
    End Sub

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try


            ''''set CPT Button Top
            btnCPT.Enabled = False
            btnmodifier.Enabled = False
            pnl_btnCPT.Dock = DockStyle.Top
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch


            pnl_btnmodifier.Dock = DockStyle.Bottom
            btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnmodifier.BackgroundImageLayout = ImageLayout.Stretch


            trvCPT.Nodes.Clear()

            rdoDescriptionrht.Checked = True

            '''' Fill CPT Data In grid
            _ModifierClick = False
            Call FillCPT()

            'for radio button click
            _CPTClick = True
            _ModifierClick = False

            txtsearchrht.Text = ""
            txtsearchrht.Focus()
            btnCPT.Enabled = True
            btnmodifier.Enabled = True
        Finally
            _ModifierClick = False
        End Try
    End Sub

    Private Sub frm_Diagnosis_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''So set Diagnosis form on Top when right click on form
        ''Resolved Bug No.67223::Diagnosis -  when diagnosis screen is open and user want to open another application then that application open at the back of diagnosis screen.
        'Me.TopMost = True
    End Sub

    Private Sub frm_Diagnosis_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        'Me.TopMost = False
    End Sub

    Private Sub frm_Diagnosis_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        gloWord.WordDialogBoxBackgroundCloser.ForceWindowIntoForeground(diag_activewindow)
    End Sub

    Private Sub frm_Diagnosis_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            'Added code for Saving the Width as per USER ID
            Dim oclsDiagnosis As New ClsDiagnosisDBLayer
            Dim _Width As Long = pnlleft.Width
            Dim _CWidth As Long = pnltrvrht.Width
            oclsDiagnosis.AddMyWidthSetting(_DiagnosisWidth, _Width, gClinicID, _UserID, gloSettings.SettingFlag.Clinic)
            oclsDiagnosis.AddMyWidthSetting(_CPTWidth, _CWidth, gClinicID, _UserID, gloSettings.SettingFlag.Clinic)
            If ICD9To10MappingControl IsNot Nothing AndAlso ICD9To10MappingControl.DataContext IsNot Nothing AndAlso TypeOf (ICD9To10MappingControl.DataContext) Is gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping.ICD9To10MappingContainer Then

                DirectCast(ICD9To10MappingControl.DataContext, gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping.ICD9To10MappingContainer).Dispose()
                ICD9To10MappingControl.DataContext = Nothing

                RemoveHandler ICD9To10MappingControl.CodeSelected, AddressOf ICD10CodeSelected
            End If

            If ICD9To10MappingDBLayer IsNot Nothing Then
                ICD9To10MappingDBLayer = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub



    Private Sub frm_Diagnosis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''gloC1FlexStyle.Style(C1Dignosis)
        'Added Code to get the Width as per userID

        lblCopyRight.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain

        Dim dtMyWidth As DataTable
        Dim dtCPTWidth As DataTable
        Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
        objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
        Dim MyWidth As Object = Nothing
        Dim MyCPTWidth As Object = Nothing

        dtMyWidth = objDiagnosisDBLayer.GetMyWidthSetting(_DiagnosisWidth)
        If (dtMyWidth.Rows.Count > 0) Then
            MyWidth = Convert.ToString(dtMyWidth.Rows(0)("sSettingsValue"))
            pnlleft.Width = MyWidth

        End If

        If Not IsNothing(dtMyWidth) Then
            dtMyWidth.Dispose()
            dtMyWidth = Nothing
        End If

        dtCPTWidth = objDiagnosisDBLayer.GetMyWidthSetting(_CPTWidth)
        If (dtCPTWidth.Rows.Count > 0) Then
            MyCPTWidth = Convert.ToString(dtCPTWidth.Rows(0)("sSettingsValue"))
            pnltrvrht.Width = MyCPTWidth
        End If
        If Not IsNothing(dtCPTWidth) Then
            dtCPTWidth.Dispose()
            dtCPTWidth = Nothing
        End If


        dtActiveCPTTable = New DataTable()
        Dim colCPTCode As New DataColumn("sCPTCode")
        Dim colFromDate As New DataColumn("dtFromDate")
        Dim colToDate As New DataColumn("dtToDate")

        dtActiveCPTTable.Columns.Add(colCPTCode)
        dtActiveCPTTable.Columns.Add(colFromDate)
        dtActiveCPTTable.Columns.Add(colToDate)




        C1Dignosis.Styles.Normal.WordWrap = True

        Application.DoEvents()
        Try
            Dim dbSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(GetConnectionString())
            Dim oSettingValue As Object = Nothing
            dbSettings.GetSetting("DefaultDxCPTPatientProblemDiagnosis", oSettingValue)
            If Not IsNothing(oSettingValue) AndAlso Convert.ToString(oSettingValue).Trim() <> "" Then
                If Convert.ToInt32(oSettingValue) = 1 Then LayoutPatientProblem()
            End If

            oSettingValue = Nothing
            If dbSettings IsNot Nothing Then
                dbSettings.Dispose()
                dbSettings = Nothing
            End If

            ''Sandip Darade 20090713
            ''Open existing  diagnosis against respected exam to vew
            If _IsViewDiagnosis = True Then



                'C1Dignosis.Enabled = False
                pnltrvrht.Visible = False
                pnlleft.Visible = False
                Splitter1.Visible = False
                Splitter2.Visible = False
                tlsbtnFinish.Visible = False
                tlsbtnSave.Visible = False
                lblCopyRight.Left = pnl_tlsp_Top.Right - lblCopyRight.Width
                pnlUpdown.Visible = False
                RemoveHandler RbICD10.CheckedChanged, AddressOf RbICD10_CheckedChanged
                RemoveHandler RbICD9.CheckedChanged, AddressOf RbICD9_CheckedChanged
                tlsShowICD10Codes.Visible = False
                tlsbtnCodingRule.Visible = False
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, "Patient diagnosis viewed only.  ", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, "Patient diagnosis viewed only.  ", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''


            Else
                'Dim dtCompare As Integer = DateTime.Compare(System.DateTime.Now.Date, gdtIcd10Transition.Date)
                'If dtCompare < 0 Then
                '    RbICD9.Checked = True
                'ElseIf dtCompare = 0 Or dtCompare > 0 Then
                '    RbICD10.Checked = True
                'End If



                Call RefreshSearch()

                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, "Patient Diagnosis Opened", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, "Patient Diagnosis Opened", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''

                Call FillCPT()
                _CPTClick = True
            End If

            Call SetGridStyle()

            Call FillICDCPTMOD()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            'Bug No 67222::Diagnosis - Application not showing modifier label Properly
            'Added Code for getting PQRS Button visible only when the DxCPT Screen Opens from Exam 
            If ExamID = 0 Then
                pnl_btnPQRSCodes.Visible = False
                btnPQRSCodes.Visible = False
            End If

            panel9.Visible = False
            pnl_txtsearchrht.Visible = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '' solving sales force case- 0009512
        Finally
            GloUC_trvICD.SetFocus()
            If _IsViewDiagnosis = False Then
                Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            End If
        End Try
    End Sub

    Private Sub FillICD9_0ld(ByVal dtICD As DataTable, ByVal Flag As Integer)
        Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
        objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
        Try
            Dim i As Integer
            If IsNothing(dtICD) Then
                dtICD = New DataTable
            End If

            If dtICD.Rows.Count = 0 Then
                dtICD = objDiagnosisDBLayer.FillICD9(Flag)
            End If

            If Flag = 0 Then
                'If IsNothing(dtOrderbyCode) = True Then
                '    dtOrderbyCode = New DataTable
                'End If
                If (IsNothing(dtOrderbyCode) = False) Then
                    dtOrderbyCode.Dispose()
                    dtOrderbyCode = Nothing
                End If
                dtOrderbyCode = dtICD.Copy()
            Else
                'If IsNothing(dtOrderbyDesc) = True Then
                '    dtOrderbyDesc = New DataTable
                'End If
                If (IsNothing(dtOrderbyDesc) = False) Then
                    dtOrderbyDesc.Dispose()
                    dtOrderbyDesc = Nothing
                End If
                dtOrderbyDesc = dtICD.Copy()
            End If

            trICD9.Hide()
            trICD9.Nodes.Clear()

            rootNode = New myTreeNode
            rootNode.Text = "ICD9"
            rootNode.Key = -1
            rootNode.ImageIndex = 0
            rootNode.SelectedImageIndex = 0
            trICD9.Nodes.Add(rootNode)

            trICD9.BeginUpdate()
            'fill the treeview with ICD9 data
            If dtICD.Rows.Count < 400 Then
                ICD9Count = dtICD.Rows.Count - 1
            Else
                ICD9Count = 400
            End If

            For i = 0 To ICD9Count

                'ExisingNode = New myTreeNode
                ExisingNode = Searchnode(CType(dtICD.Rows(i)(3), String))
                If IsNothing(ExisingNode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode
                    specialtynode.Text = CType(dtICD.Rows(i)(3), String)
                    specialtynode.Key = -1
                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1

                    rootNode.Nodes.Add(specialtynode)
                    ExisingNode = specialtynode
                End If
                Dim myNode As myTreeNode

                myNode = New myTreeNode(dtICD.Rows(i)(1), CType(dtICD.Rows(i)(4), Long), CType(dtICD.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                ExisingNode.Nodes.Add(myNode)
                ExisingNode.Expand()
            Next

            trICD9.Show()
            trICD9.Nodes.Item(0).Expand()
            trICD9.EndUpdate()
            trICD9.SelectedNode = trICD9.Nodes.Item(0)

        Catch ex As Exception

        Finally
            If Not IsNothing(objDiagnosisDBLayer) Then
                objDiagnosisDBLayer.Dispose()
                objDiagnosisDBLayer = Nothing
            End If
        End Try
    End Sub

    'Private Sub FillICD9()
    '    objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
    '    Dim dtICD As DataTable

    '    Try


    '        dtICD = objDiagnosisDBLayer.GetAllICD9


    '        GloUC_trvICD.DataSource = dtICD

    '        GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
    '        GloUC_trvICD.CodeMember = Convert.ToString(dtICD.Columns("sICD9Code").ColumnName)
    '        GloUC_trvICD.ValueMember = Convert.ToString(dtICD.Columns("nICD9ID").ColumnName)
    '        GloUC_trvICD.DescriptionMember = Convert.ToString(dtICD.Columns("sDescription").ColumnName)
    '        GloUC_trvICD.MaximumNodes = 200
    '        GloUC_trvICD.FillTreeView()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    Finally
    '        If Not IsNothing(objDiagnosisDBLayer) Then
    '            objDiagnosisDBLayer = Nothing
    '        End If
    '    End Try
    'End Sub
    ''Added on 20140207-To Retrieve icd9 and icd10 
    Private Sub FillICD9ICD10(Optional ByVal dtBinding As DataTable = Nothing)
        Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
        objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
        Dim dtICD As DataTable = Nothing
        Me.Cursor = Cursors.WaitCursor
        Dim clsICD As New gloGallery.clsICD9()
        Try

            If btnAllDiagnosis.Tag = "Selected" Then
                If RbICD9.Checked Then
                    dtICD = objDiagnosisDBLayer.GetAllICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD9, GloUC_trvICD.txtsearch.Text.Trim)
                ElseIf RbICD10.Checked Then
                    If ShowICD10Codes Then
                        If GloUC_trvICD.SelectedNode IsNot Nothing AndAlso dtBinding IsNot Nothing Then
                            dtICD = dtBinding
                        End If
                    Else
                        dtICD = objDiagnosisDBLayer.GetAllICD9ICD10(gloGlobal.gloICD.CodeRevision.ICD10, GloUC_trvICD.txtsearch.Text.Trim)
                    End If

                End If
            ElseIf btnPatientProblemDiagnosis.Tag = "Selected" Then
                If RbICD9.Checked Then
                    dtICD = objDiagnosisDBLayer.FillPatientProblemDiagnosis(9, GloUC_trvICD.txtsearch.Text.Trim, _PatientID)
                ElseIf RbICD10.Checked Then
                    If ShowICD10Codes Then
                        If GloUC_trvICD.SelectedNode IsNot Nothing AndAlso dtBinding IsNot Nothing Then
                            dtICD = dtBinding
                        End If
                    Else
                        dtICD = objDiagnosisDBLayer.FillPatientProblemDiagnosis(10, GloUC_trvICD.txtsearch.Text.Trim, _PatientID)
                    End If
                End If
            End If



            GloUC_trvICD.DataSource = dtICD
            GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
            GloUC_trvICD.CodeMember = Convert.ToString(dtICD.Columns("sICD9Code").ColumnName)
            GloUC_trvICD.ValueMember = Convert.ToString(dtICD.Columns("nICD9ID").ColumnName)
            GloUC_trvICD.DescriptionMember = Convert.ToString(dtICD.Columns("sDescription").ColumnName)
            GloUC_trvICD.ICDRevision = Convert.ToString(dtICD.Columns("nICDRevision").ColumnName)
            GloUC_trvICD.MaximumNodes = 200
            GloUC_trvICD.IsDiagnosisSearch = True
            GloUC_trvICD.FillTreeView()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

            Me.Cursor = Cursors.Default
            If Not IsNothing(objDiagnosisDBLayer) Then
                objDiagnosisDBLayer = Nothing
            End If
            If Not IsNothing(clsICD) Then
                clsICD.Dispose()
                clsICD = Nothing
            End If
        End Try
    End Sub

    Private Sub trICD9_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trICD9.DoubleClick
        Dim i As Integer
        If IsNothing(trICD9.SelectedNode) = True Then
            Exit Sub
        End If

        If IsNothing(trICD9.SelectedNode.FirstNode) = False Then
            Exit Sub
        End If

        Dim mynode As myTreeNode
        If Not IsNothing(trvCPT.SelectedNode) Then
            mynode = CType(trvCPT.SelectedNode, myTreeNode)
            AddNode_CPT(mynode)
        End If
        Dim nMaxICD9Count As Integer
        With C1Dignosis
            Dim _Row As Integer = 0
            'selected row in flexgrid 
            Dim strDesription As String = trICD9.SelectedNode.Text

            ' if the ICD9 item is alreay present then exit
            For i = 0 To .Rows.Count - 1
                If .GetData(i, Col_ICD9Code_Description) = strDesription Then
                    '' TO Insert the New Item At the END of the CAtegory
                    Exit Sub
                End If
                If i = .Rows.Count - 1 Then
                    If i <> 0 Then
                        If Not IsNothing(.GetData(i, Col_ICD9Count)) Then
                            nMaxICD9Count = .GetData(i, Col_ICD9Count)
                        Else
                            nMaxICD9Count = 0
                        End If
                    Else
                        nMaxICD9Count = 0
                    End If
                End If
            Next

            'set the propery of treeview in the flexgrid
            '''''
            .Tree.Column = Col_ICD9Code_Description
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid

            .Tree.Indent = 15

            '''''

            ' add the data to treeview in flexgrid     
            ' if there is no data in flexgrid
            If .Rows.Count - 1 = 0 Then
                .Rows.Add()
                _Row = .Rows.Count - 1
                'set the properties for newly added row
                With .Rows(_Row)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 0
                    .Node.Data = trICD9.SelectedNode.Text
                    .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                    '.Node.Key = strSelectedICD9
                End With
                Dim strConctICD9 As String
                Dim arrstrConctICD9() As String
                Dim strICDCode As String
                Dim strICDDesc As String = ""
                strConctICD9 = trICD9.SelectedNode.Text
                ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                ' is row its current row it is used for removing data from flexgrid
                arrstrConctICD9 = Split(strConctICD9, "-", 2)
                strICDCode = arrstrConctICD9.GetValue(0)
                If (arrstrConctICD9.Length > 1) Then
                    strICDDesc = arrstrConctICD9.GetValue(1)
                End If

                .SetData(_Row, Col_ICD9Code, strICDCode)
                .SetData(_Row, Col_ICD9Desc, strICDDesc)
                .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)
                .Row = _Row
                _Row = _Row + 1
            Else
                .Rows.Add()
                _Row = .Rows.Count - 1
                With .Rows(.Rows.Count - 1)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 0
                    .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                    .Node.Data = trICD9.SelectedNode.Text
                End With

                ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                ' is row its current row it is used for removing data from flexgrid
                '.SetData(_Row, Col_sICD9Code, _Row)

                Dim strConctICD9 As String
                Dim arrstrConctICD9() As String
                Dim strICDCode As String
                Dim strICDDesc As String = ""
                strConctICD9 = trICD9.SelectedNode.Text
                arrstrConctICD9 = Split(strConctICD9, "-", 2)
                strICDCode = arrstrConctICD9.GetValue(0)
                If (arrstrConctICD9.Length > 1) Then
                    strICDDesc = arrstrConctICD9.GetValue(1)
                End If

                .SetData(_Row, Col_ICD9Code, strICDCode)
                .SetData(_Row, Col_ICD9Desc, strICDDesc)
                .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)
                .Row = _Row
                _Row = _Row + 1
            End If
        End With
    End Sub
    Private Sub trICD9_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trICD9.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            Dim selectedTreeview As TreeView = CType(sender, TreeView)
            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)
            AddNode(dropNode)
            dropNode.EnsureVisible()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Searchnode(ByVal strspecialty As String) As myTreeNode
        Dim mynode As myTreeNode
        For Each mynode In trICD9.Nodes.Item(0).Nodes
            If mynode.Text = strspecialty Then
                Return mynode
                Exit For
            End If
        Next
        Return Nothing
    End Function

    Private Sub AddNode(ByVal mynode As myTreeNode)
        If Not mynode Is trICD9.Nodes.Item(0) Then
            If Not mynode.Parent Is trICD9.Nodes.Item(0) Then

                'Dim mytragetnode As myTreeNode

                'Add the selected node to target treeview
                Dim associatenode As myTreeNode

                associatenode = mynode.Clone
                associatenode.Key = mynode.Key
                associatenode.Text = mynode.Text
                associatenode.Tag = mynode.Tag

                associatenode.ImageIndex = 3
                associatenode.SelectedImageIndex = 3

                associatenode.EnsureVisible()


                ''to Reset Search
                Call RefreshSearch()

            End If
        End If
    End Sub

    Private Sub trICD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trICD9.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                Try
                    Dim mynode As myTreeNode
                    If Not IsNothing(trICD9.SelectedNode) Then
                        mynode = CType(trICD9.SelectedNode, myTreeNode)
                        If Not IsNothing(mynode) Then
                            AddNode(mynode)
                        End If
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub RefreshICD9()
        txtsearch.Text = ""

        ''to Reset Search
        Call RefreshSearch()
        'trICD9.Nodes.Item(0).Nodes.Clear()

    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, "-", 0)
            If (arrstring.Length > 1) Then
                Return arrstring(1)
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Private Sub txtsearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearch.KeyPress
        Try

            If (e.KeyChar = ChrW(13)) Then
                trICD9.Select()
            Else
                trICD9.SelectedNode = trICD9.Nodes.Item(0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'radio button one click event of left side

    Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try


            If IsNothing(dtOrderbyCode) = True Then
                dtOrderbyCode = New DataTable
            End If

            Dim i As Integer
            'Dim dt As DataTable

            If dtOrderbyCode.Rows.Count = 0 Then
                If (IsNothing(dtOrderbyCode) = False) Then
                    dtOrderbyCode.Dispose()
                    dtOrderbyCode = Nothing
                End If
                Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                dtOrderbyCode = objDiagnosisDBLayer.FillICD9(0)
                objDiagnosisDBLayer.Dispose()
                objDiagnosisDBLayer = Nothing
            End If

            trICD9.Hide()

            trICD9.Nodes(0).Nodes.Clear()
            trICD9.BeginUpdate()

            If dtOrderbyCode.Rows.Count < 400 Then
                ICD9Count = dtOrderbyCode.Rows.Count - 1
            Else
                ICD9Count = 400
            End If

            For i = 0 To ICD9Count 'dtOrderbyCode.Rows.Count - 1


                Dim rootnode As myTreeNode
                rootnode = Searchnode(CType(dtOrderbyCode.Rows(i)(3), String))
                If IsNothing(rootnode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode(CType(dtOrderbyCode.Rows(i)(3), String), -1)

                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1
                    trICD9.Nodes.Item(0).Nodes.Add(specialtynode)
                    rootnode = specialtynode
                End If

                Dim myNode As myTreeNode
                myNode = New myTreeNode(dtOrderbyCode.Rows(i)(1), CType(dtOrderbyCode.Rows(i)(4), Long), CType(dtOrderbyCode.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                rootnode.Nodes.Add(myNode)

            Next

            trICD9.ExpandAll()
            trICD9.Show()
            trICD9.SelectedNode = trICD9.Nodes.Item(0)
            trICD9.Select()
            trICD9.EndUpdate()
            txtsearch.Text = ""
            txtsearch.Focus()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'radio button2 click event of right side

    Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Try

            If IsNothing(dtOrderbyDesc) = True Then
                dtOrderbyDesc = New DataTable
            End If

            Dim i As Integer
            'Dim dt As DataTable

            If dtOrderbyDesc.Rows.Count = 0 Then
                If (IsNothing(dtOrderbyDesc) = False) Then
                    dtOrderbyDesc.Dispose()
                    dtOrderbyDesc = Nothing
                End If
                Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
                objDiagnosisDBLayer = New ClsDiagnosisDBLayer
                dtOrderbyDesc = objDiagnosisDBLayer.FillICD9(1)
                objDiagnosisDBLayer.Dispose()
                objDiagnosisDBLayer = Nothing
            End If


            trICD9.Hide()
            'fill the treeview for decription
            trICD9.Nodes(0).Nodes.Clear()
            trICD9.BeginUpdate()

            If dtOrderbyDesc.Rows.Count < 400 Then
                ICD9Count = dtOrderbyDesc.Rows.Count - 1
            Else
                ICD9Count = 400
            End If

            For i = 0 To ICD9Count 'dtOrderbyDesc.Rows.Count - 1


                Dim rootnode As myTreeNode
                rootnode = Searchnode(CType(dtOrderbyDesc.Rows(i)(3), String))
                If IsNothing(rootnode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode(CType(dtOrderbyDesc.Rows(i)(3), String), -1)

                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1
                    trICD9.Nodes.Item(0).Nodes.Add(specialtynode)
                    rootnode = specialtynode
                End If
                Dim myNode As myTreeNode
                myNode = New myTreeNode(dtOrderbyDesc.Rows(i)(1), CType(dtOrderbyDesc.Rows(i)(4), Long), CType(dtOrderbyDesc.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                rootnode.Nodes.Add(myNode)

            Next

            trICD9.ExpandAll()
            trICD9.Show()
            trICD9.SelectedNode = trICD9.Nodes.Item(0)
            trICD9.Select()

            trICD9.EndUpdate()
            txtsearch.Text = ""
            txtsearch.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try

            'sarika 26th sept 07
            If txtsearch.Tag <> Trim(txtsearch.Text) Then
                ''If rbSearch2.Checked = True Then
                ''    AddSrICD9(Trim(txtsearch.Text), dtOrderbyDesc)
                ''Else
                ''    AddSrICD9(Trim(txtsearch.Text), dtOrderbyCode)
                ''End If


                'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'End If
                'If Len(Trim(txtsearchDrug.Text)) = 1 Then
                txtsearch.Tag = Trim(txtsearch.Text)
                txtsearch.Focus()

                'End If
            End If

            Exit Sub
            '-------------------------------------------------------------------------


            If Trim(txtsearch.Text) <> "" Then
                If trICD9.Nodes.Count > 0 Then


                    If trICD9.Nodes.Item(0).GetNodeCount(False) > 0 Then
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In trICD9.Nodes.Item(0).Nodes
                            Dim ICD9node As myTreeNode
                            For Each ICD9node In mychildnode.Nodes
                                ''If rbSearch1.Checked = True Then
                                If Mid(UCase(ICD9node.Tag), 1, Len(UCase(Trim(txtsearch.Text)))) = UCase(Trim(txtsearch.Text)) Then
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                    '*************
                                    trICD9.SelectedNode = ICD9node
                                    txtsearch.Focus()
                                    Exit Sub
                                End If
                                ''Else
                                'Dim str As String
                                'str = Mid(UCase(ICD9node.Text), Len(UCase(ICD9node.Tag)) + 2, Len(UCase(Trim(txtsearch.Text))))
                                If Mid(UCase(ICD9node.Text), Len(UCase(ICD9node.Tag)) + 2, Len(UCase(Trim(txtsearch.Text)))) = UCase(Trim(txtsearch.Text)) Then
                                    '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                    trICD9.SelectedNode = trICD9.SelectedNode.LastNode
                                    '*************
                                    trICD9.SelectedNode = ICD9node
                                    txtsearch.Focus()
                                    Exit Sub
                                End If

                                'End If
                            Next
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'sarika 26th sept 07

    Public Sub AddSrICD9(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            'i.sICD9code,i.sICD9code+'-'+i.sDescription,i.sdescription,s.sdescription,i.nICD9ID 

            ''If rbSearch2.Checked = True Then
            ''description 
            dv.RowFilter = dt.Columns(2).ColumnName & " Like '%" & strsearch & "%'"
            ''Else
            ''code
            dv.RowFilter = dt.Columns(0).ColumnName & " Like '%" & strsearch & "%'"
            ''End If
            tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trICD9.Hide()

            trICD9.Nodes(0).Nodes.Clear()
            trICD9.BeginUpdate()
            'fill the treeview with ICD9 data

            If tdt.Rows.Count < 400 Then
                ICD9Count = tdt.Rows.Count - 1
            Else
                ICD9Count = 400
            End If


            For i = 0 To ICD9Count 'tdt.Rows.Count - 1


                Dim rootnode As myTreeNode
                rootnode = Searchnode(CType(tdt.Rows(i)(3), String))
                If IsNothing(rootnode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode(CType(tdt.Rows(i)(3), String), -1)

                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1
                    trICD9.Nodes.Item(0).Nodes.Add(specialtynode)
                    rootnode = specialtynode
                End If
                Dim myNode As myTreeNode

                myNode = New myTreeNode(tdt.Rows(i)(1), CType(tdt.Rows(i)(4), Long), CType(tdt.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                rootnode.Nodes.Add(myNode)
                rootnode.Expand()
            Next

            'trICD9.ExpandAll()
            trICD9.Show()
            trICD9.Nodes.Item(0).Expand()
            'trICD9.SelectedNode = trICD9.Nodes.Item(0)

            'trICD9.Select()
            trICD9.EndUpdate()

            trICD9.SelectedNode = trICD9.Nodes.Item(0)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    '---------------------------------------------------

    Private Sub RefreshSearch()
        txtsearch.Text = ""
        txtsearch.Focus()
    End Sub

    Private Sub trICD9_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trICD9.MouseDown

        Try


            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trICD9.GetNodeAt(e.X, e.Y)
                trICD9.SelectedNode = trvNode
                If IsNothing(trvNode) = True Then
                    'Try
                    '    If (IsNothing(trICD9.ContextMenuStrip) = False) Then
                    '        trICD9.ContextMenuStrip.Dispose()
                    '        trICD9.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trICD9.ContextMenuStrip = Nothing
                    Exit Sub
                End If
                If trICD9.Nodes.Item(0).Text = trICD9.SelectedNode.Text Or (CType(trICD9.SelectedNode, myTreeNode).Key = -1) Then
                    'Try
                    '    If (IsNothing(trICD9.ContextMenuStrip) = False) Then
                    '        trICD9.ContextMenuStrip.Dispose()
                    '        trICD9.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trICD9.ContextMenuStrip = Nothing
                    Exit Sub
                End If

                If IsNothing(trvNode) = False Then

                    trICD9.SelectedNode = trvNode
                    ContextMenuDiagnosis.Items(0).Visible = False
                    ContextMenuDiagnosis.Items(1).Visible = True
                    ContextMenuDiagnosis.Items(2).Visible = False
                    ContextMenuDiagnosis.Items(3).Visible = False
                    ContextMenuDiagnosis.Items(4).Visible = True
                    ContextMenuDiagnosis.Items(5).Visible = False
                    ContextMenuDiagnosis.Items(6).Visible = False
                    ContextMenuDiagnosis.Items(7).Visible = False
                    ContextMenuDiagnosis.Items(8).Visible = False
                    ContextMenuDiagnosis.Items(9).Visible = False
                    'Try
                    '    If (IsNothing(trICD9.ContextMenuStrip) = False) Then
                    '        trICD9.ContextMenuStrip.Dispose()
                    '        trICD9.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trICD9.ContextMenuStrip = ContextMenuDiagnosis

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillCPT_old(ByVal dtCPT As DataTable, ByVal Flag As Integer)
        '''''Flag=0 if Order by CPTCODE
        '''''Flag=1 if Order by CPTDESCRIPTION
        Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
        objTreatmentDBLayer = New ClsTreatmentDBLayer()
        Dim i As Integer
        Try
            If IsNothing(dtCPT) Then
                dtCPT = New DataTable
            End If

            If dtCPT.Rows.Count = 0 Then
                dtCPT = objTreatmentDBLayer.FillCPT(Flag)
            End If

            If Flag = 0 Then        'order by code
                If IsNothing(dtOrderbyCode_CPT) = True Then
                    dtOrderbyCode_CPT = New DataTable
                End If
                If dtOrderbyCode_CPT.Rows.Count = 0 Then
                    If (IsNothing(dtOrderbyCode_CPT) = False) Then
                        dtOrderbyCode_CPT.Dispose()
                        dtOrderbyCode_CPT = Nothing
                    End If
                    dtOrderbyCode_CPT = dtCPT
                End If

            Else   'order by desc
                'If IsNothing(dtOrderbyDesc_CPT) = True Then
                '    dtOrderbyDesc_CPT = New DataTable
                'End If
                If (IsNothing(dtOrderbyDesc_CPT) = False) Then
                    dtOrderbyDesc_CPT.Dispose()
                    dtOrderbyDesc_CPT = Nothing
                End If
                dtOrderbyDesc_CPT = dtCPT
            End If
            trvCPT.ImageList = imglistTrvModifier
            trvCPT.Hide()
            trvCPT.Nodes.Clear()
            rootNode = New myTreeNode
            rootNode.Text = "CPT"
            rootNode.Key = -1
            rootNode.ImageIndex = 0
            rootNode.SelectedImageIndex = 0
            trvCPT.Nodes.Add(rootNode)
            trvCPT.BeginUpdate()
            'fill treeview with CPT data
            If dtCPT.Rows.Count < 400 Then
                CPTCount = dtCPT.Rows.Count - 1
            Else
                CPTCount = 400
            End If

            For i = 0 To CPTCount 'dtCPT.Rows.Count - 1



                ExisingNode = Searchnode_CPT(CType(dtCPT.Rows(i)(3), String))
                If IsNothing(ExisingNode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode
                    specialtynode.Text = CType(dtCPT.Rows(i)(3), String)
                    specialtynode.Key = -1
                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1
                    rootNode.Nodes.Add(specialtynode)
                    ExisingNode = specialtynode
                End If
                Dim myNode As myTreeNode
                myNode = New myTreeNode(dtCPT.Rows(i)(1), CType(dtCPT.Rows(i)(4), Long), CType(dtCPT.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                ExisingNode.Nodes.Add(myNode)
                ExisingNode.Expand()
            Next
            trvCPT.Nodes.Item(0).Expand()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.EndUpdate()
        Catch ex As Exception

        Finally
            If Not IsNothing(objTreatmentDBLayer) Then
                objTreatmentDBLayer.Dispose()
                objTreatmentDBLayer = Nothing
            End If
        End Try
    End Sub
    Private Sub FillCPT()
        Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
        objTreatmentDBLayer = New ClsTreatmentDBLayer()
        Try
            Dim dtCPT As New DataTable
            dtCPT = objTreatmentDBLayer.GetAllCPT

            If IsNothing(dtCPT) = False Then
                GloUC_trvAssociates.ImageList = imglistTrvCPT
                GloUC_trvAssociates.DataSource = dtCPT
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvAssociates.CodeMember = Convert.ToString(dtCPT.Columns("sCPTCode").ColumnName)
                GloUC_trvAssociates.ValueMember = Convert.ToString(dtCPT.Columns("nCPTID").ColumnName)
                GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtCPT.Columns("sDescription").ColumnName)
                GloUC_trvAssociates.MaximumNodes = 200
                GloUC_trvAssociates.FillTreeView()
            End If
        Catch ex As Exception

        Finally
            If Not IsNothing(objTreatmentDBLayer) Then
                objTreatmentDBLayer.Dispose()
                objTreatmentDBLayer = Nothing
            End If
        End Try
    End Sub

    'Added Code for getting PQRS Code from History
    Private Sub AddCPTsfromHistory()
        Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
        objTreatmentDBLayer = New ClsTreatmentDBLayer()
        Dim i As Integer
        Dim j As Integer
        Dim dtCPT As New DataTable
        Dim intSelectedrow As Integer = 0
        Dim strSelectedICD9 As String = ""
        Dim strSelectedMod As String = ""
        Dim NewRow As Integer = 0
        Dim _isCPTFilled As Boolean = False
        Dim ofNode As C1.Win.C1FlexGrid.Node
        Dim nMaxCPTCount As Integer
        Dim nMaxICD9Count As Integer

        With C1Dignosis

            'FOR LOOP TO SET CPT TO ALL ICD9s ''
            Dim iRow As Integer = 1
            While iRow <= C1Dignosis.Rows.Count - 1
                If gblnSetCPTtoAllICD9 Then
                    intSelectedrow = iRow '' CPT TO ALL ''
                    If C1Dignosis.Rows(iRow).Node.Level <> 0 Then
                        iRow = iRow + 1
                        Continue While
                    End If
                Else
                    intSelectedrow = .Row '' CPT TO SELECTED ROW ''
                End If
                'get PQRS Codes 

                dtCPT = objTreatmentDBLayer.GetCPTsFromHistory(_PatientID)

                If (_isCPTFilled = False) Then
                    If Not IsNothing(dtCPT) Then
                        frmPQRS._dtpqrs = dtCPT
                        Dim objReview As frmPQRS = gloUIControlLibrary.WPFForms.PQRS.frmPQRS.GetInstance()
                        Dim interopHelper As New System.Windows.Interop.WindowInteropHelper(objReview)
                        interopHelper.Owner = Me.Handle
                        objReview.ShowInTaskbar = False
                        gloUIControlLibrary.WPFForms.PQRS.frmPQRS.PropfrmPQrsHandle = Me.Handle
                        objReview.ShowDialog()
                        If IsNothing(interopHelper) = False Then
                            interopHelper = Nothing
                        End If
                       
                        If (objReview._IsSave) Then
                            dtCPT = frmPQRS._dtpqrs
                        Else
                            If Not IsNothing(dtCPT) Then
                                dtCPT.Rows.Clear()
                            End If
                            End If
                            objReview.Close()
                            objReview = Nothing
                            _isCPTFilled = True
                            If dtCPT.Rows.Count = 0 Then
                                Exit Sub
                            End If
                    Else
                            Exit Sub
                    End If
                End If


                Dim strDesriptionCPT As String = String.Empty
                Dim strCodeCPT As String = String.Empty
                Dim strCodeDescriptionCPT As String = String.Empty

                For j = 0 To dtCPT.Rows.Count - 1
                    strDesriptionCPT = dtCPT.Rows(j)(1).ToString
                    strCodeCPT = dtCPT.Rows(j)(0).ToString
                    strCodeDescriptionCPT = strCodeCPT & " - " & strDesriptionCPT

                    NewRow = 0

                    Dim ICD9LineNo As Integer = CType(.GetData(intSelectedrow, Col_ICD9Count), Integer)
                    ofNode = Nothing
                    ofNode = .Rows(intSelectedrow).Node

                    If ofNode.Level = 2 Then
                        If gblnSetCPTtoAllICD9 Then
                            iRow = iRow + 1
                            Continue While
                        Else
                            Exit Sub
                        End If
                    End If


                    If ofNode.Level = 1 Then
                        intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                        C1Dignosis.Select(intSelectedrow, Col_ICD9Code_Description)
                        C1Dignosis.Row = intSelectedrow
                    End If
                    ofNode = Nothing
                    ofNode = .Rows(intSelectedrow).Node

                    If ofNode.Children > 0 Then
                        For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            If .GetData(i, Col_ICD9Code_Description) = strCodeDescriptionCPT Then
                                If gblnSetCPTtoAllICD9 Then
                                    j = j + 1
                                    If (j = dtCPT.Rows.Count) Then
                                        iRow = iRow + 1
                                        Continue While
                                    Else
                                        If (j < dtCPT.Rows.Count) Then
                                            strDesriptionCPT = dtCPT.Rows(j)(1).ToString
                                            strCodeCPT = dtCPT.Rows(j)(0).ToString
                                            strCodeDescriptionCPT = strCodeCPT & " - " & strDesriptionCPT
                                            i = intSelectedrow

                                        End If
                                    End If
                                Else
                                    j = j + 1
                                    If (j = dtCPT.Rows.Count) Then
                                        Exit Sub
                                    Else
                                        If (j < dtCPT.Rows.Count) Then
                                            strDesriptionCPT = dtCPT.Rows(j)(1).ToString
                                            strCodeCPT = dtCPT.Rows(j)(0).ToString
                                            strCodeDescriptionCPT = strCodeCPT & " - " & strDesriptionCPT
                                            i = intSelectedrow
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        If ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index = 0 Then
                            nMaxCPTCount = 0
                            nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                        Else
                            nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                            nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                        End If
                    Else
                        nMaxCPTCount = 0
                        nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                    End If
                    'Adding new row 
                    NewRow = ofNode.GetCellRange().BottomRow

                    If NewRow = 0 Then
                        If gblnSetCPTtoAllICD9 Then
                            iRow = iRow + 1
                            Continue While
                        Else
                            Exit Sub
                        End If
                    Else
                        .Rows.Insert(NewRow + 1)
                        With .Rows(NewRow + 1)
                            .AllowEditing = True
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                            .Node.Data = strCodeDescriptionCPT
                        End With
                        .Row = NewRow + 1
                        'set the concated string which has ICD and CPT code in the col_sICD9Code field


                        Dim strICDCode As String
                        Dim strICDDesc As String
                        Dim strSnomedCode As String
                        Dim strSnomedDesc As String

                        strICDCode = .GetData(NewRow, Col_ICD9Code)
                        strICDDesc = .GetData(NewRow, Col_ICD9Desc)
                        strSnomedCode = Convert.ToString(.GetData(NewRow, Col_SnomedCode))
                        strSnomedDesc = Convert.ToString(.GetData(NewRow, Col_SnomedDesc))

                        Dim strConctCPT As String
                        Dim arrstrConctCPT() As String
                        Dim strCPTCode As String = ""
                        Dim strCPTDesc As String = ""

                        strConctCPT = strCodeDescriptionCPT
                        If Not IsNothing(strConctCPT) AndAlso strConctCPT <> "" Then
                            arrstrConctCPT = Split(strConctCPT, "-", 2)
                            strCPTCode = arrstrConctCPT.GetValue(0)
                            If arrstrConctCPT.Length > 1 Then
                                strCPTDesc = arrstrConctCPT.GetValue(1)
                            End If
                        End If

                        'set value for ICDCODE,ICDDesc,ICDCODE,ICDDesc, 
                        .SetData(NewRow + 1, Col_ICD9Code, strICDCode)
                        .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc)
                        .SetData(NewRow + 1, COl_CPTCode, strCPTCode)
                        .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc)
                        .SetData(NewRow + 1, Col_SnomedCode, Convert.ToString(strSnomedCode))
                        .SetData(NewRow + 1, Col_SnomedDesc, Convert.ToString(strSnomedDesc))
                        ''Line edited by dipak 20090819 to set default valu to 1 insted of 0 for units in C1Dignosis for CPT node

                        .SetData(NewRow + 1, Col_Units, 1)
                        .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount + 1)
                        .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                        .SetData(NewRow + 1, Col_nICDRevision, .GetData(NewRow, Col_nICDRevision))
                    End If

                Next
                iRow = iRow + 1
                dtCPT.Dispose()
                dtCPT = Nothing

                If gblnSetCPTtoAllICD9 = False Then
                    Exit Sub
                End If

            End While

        End With

    End Sub

   Private Function Searchnode_CPT(ByVal strspecialty As String) As myTreeNode
        Dim mynode As myTreeNode
        For Each mynode In trvCPT.Nodes.Item(0).Nodes
            If mynode.Text = strspecialty Then

                Return mynode
                Exit For
            End If
        Next
        Return Nothing
    End Function



    Private Sub trvCPT_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPT.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'Get the TreeNode being dragged


            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

            'The target node should be selected from the DragOver event



            Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

            'Remove the drop node from its current location

            'If there is no targetNode add dropNode to the bottom of the TreeView root
            'nodes, otherwise add it to the end of the dropNode child nodes


            AddNode_CPT(dropNode)
            dropNode.EnsureVisible()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddNode_CPT(ByVal mynode As myTreeNode)
        If Not IsNothing(mynode) Then
            If Not mynode Is trvCPT.Nodes.Item(0) Then
                If Not mynode.Parent Is trvCPT.Nodes.Item(0) Then

                    'Dim mytragetnode As myTreeNode


                    Dim mynodeClone As myTreeNode
                    mynodeClone = mynode.Clone


                    mynodeClone.ImageIndex = 3
                    mynodeClone.SelectedImageIndex = 3

                    mynodeClone.EnsureVisible()

                End If
            End If
        End If
    End Sub


    Private Sub trvCPT_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvCPT.DoubleClick
        Try

            Dim i As Integer
            Dim intSelectedrow As Integer = 0
            Dim strSelectedICD9 As String = ""
            Dim strSelectedMod As String = ""
            Dim NewRow As Integer = 0

            Dim ofNode As C1.Win.C1FlexGrid.Node
            Dim nMaxCPTCount As Integer
            Dim nMaxICD9Count As Integer
            Dim nMaxModifierCount As Integer
            If IsNothing(trvCPT.SelectedNode) = True Then
                Exit Sub
            End If

            If IsNothing(trvCPT.SelectedNode.FirstNode) = False Then
                Exit Sub
            End If

            Dim mynode As myTreeNode
            If Not IsNothing(trvCPT.SelectedNode) Then
                mynode = CType(trvCPT.SelectedNode, myTreeNode)
                AddNode_CPT(mynode)
            End If

            If C1Dignosis.Rows.Count - 1 = 0 Then
                Exit Sub
            End If


            'if modifier is present in treeview then insert this data   
            If _ModifierClick = True Then
                With C1Dignosis
                    NewRow = 0
                    intSelectedrow = .Row
                    ofNode = .Rows(intSelectedrow).Node

                    If ofNode.Level = 0 Then '''' Selected Item is ICD9 exit functionality
                        Exit Sub
                    Else
                        If ofNode.Level = 2 Then ''''Seleted Item is CPT 
                            .Select(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, Col_ICD9Code_Description)
                            intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                            ofNode = Nothing
                            ofNode = .Rows(intSelectedrow).Node
                        End If

                        Dim strDesriptionMod As String = trvCPT.SelectedNode.Text
                        If ofNode.Children > 0 Then  '''' CPT Node has child then check the modifer is present and  get last Child
                            For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If .GetData(i, Col_ICD9Code_Description) = strDesriptionMod Then
                                    Exit Sub
                                End If
                            Next
                            nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                            nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                            nMaxModifierCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ModCount)
                        Else
                            nMaxICD9Count = .GetData(.Row, Col_ICD9Count)
                            nMaxCPTCount = .GetData(.Row, Col_CPTCount)
                            nMaxModifierCount = 0
                        End If
                        NewRow = ofNode.GetCellRange().BottomRow

                        If NewRow = 0 Then
                            Exit Sub
                        Else '''' Insert Modifer
                            .Rows.Insert(NewRow + 1)
                            With .Rows(NewRow + 1)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 2
                                .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                .Node.Data = trvCPT.SelectedNode.Text
                            End With

                            .Row = NewRow + 1
                            Dim strConctMOD As String
                            Dim arrstrConctMOD() As String
                            Dim strMODCode As String
                            Dim strMODDesc As String = ""

                            Dim strICDCode_new As String
                            Dim strICDDesc_new As String = ""
                            Dim strCPTCode_new As String
                            Dim strCPTDesc_new As String = ""


                            strICDCode_new = .GetData(NewRow, Col_ICD9Code)
                            strICDDesc_new = .GetData(NewRow, Col_ICD9Desc)

                            strCPTCode_new = .GetData(NewRow, COl_CPTCode)
                            strCPTDesc_new = .GetData(NewRow, Col_CPTDesc)

                            strConctMOD = trvCPT.SelectedNode.Text
                            arrstrConctMOD = Split(strConctMOD, "-", 2)
                            strMODCode = arrstrConctMOD.GetValue(0)
                            If (arrstrConctMOD.Length > 1) Then
                                strMODDesc = arrstrConctMOD.GetValue(1)
                            End If




                            .SetData(NewRow + 1, Col_ICD9Code, strICDCode_new)
                            .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc_new)
                            .SetData(NewRow + 1, COl_CPTCode, strCPTCode_new)
                            .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc_new)
                            .SetData(NewRow + 1, Col_ModCode, strMODCode)
                            .SetData(NewRow + 1, Col_ModDesc, strMODDesc)
                            .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                            .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount)
                            .SetData(NewRow + 1, Col_ModCount, nMaxModifierCount + 1)
                        End If
                    End If
                End With

            Else
                With C1Dignosis
                    intSelectedrow = .Row
                    Dim strDesriptionCPT As String = trvCPT.SelectedNode.Text
                    NewRow = 0
                    Dim ICD9LineNo As Integer = CType(.GetData(intSelectedrow, Col_ICD9Count), Integer)
                    ofNode = Nothing
                    ofNode = .Rows(intSelectedrow).Node
                    If ofNode.Level = 2 Then '''' If modifier is selected then exit sub
                        Exit Sub
                    End If


                    If ofNode.Level = 1 Then ''''If ICD9 selected
                        intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                        C1Dignosis.Select(intSelectedrow, Col_ICD9Code_Description)
                        C1Dignosis.Row = intSelectedrow
                    End If
                    ofNode = Nothing
                    ofNode = .Rows(intSelectedrow).Node

                    If ofNode.Children > 0 Then '''' Check the CPT is alredy exits and get last Child index to add CPT
                        For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            If .GetData(i, Col_ICD9Code_Description) = strDesriptionCPT Then
                                '.Select(i, Col_ICD9Code_Description)
                                Exit Sub

                            End If
                        Next
                        If ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index = 0 Then
                            nMaxCPTCount = 0
                            nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                        Else
                            nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                            nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                        End If
                    Else
                        nMaxCPTCount = 0
                        nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                    End If
                    NewRow = ofNode.GetCellRange().BottomRow

                    If NewRow = 0 Then
                        Exit Sub
                    Else
                        .Rows.Insert(NewRow + 1)
                        With .Rows(NewRow + 1)
                            .AllowEditing = True
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                            .Node.Data = trvCPT.SelectedNode.Text
                        End With
                        .Row = NewRow + 1
                        'set the concated string which has ICD and CPT code in the col_sICD9Code field

                        '    Dim arrstrConctICD9() As String
                        Dim strICDCode As String
                        Dim strICDDesc As String

                        strICDCode = .GetData(NewRow, Col_ICD9Code)
                        strICDDesc = .GetData(NewRow, Col_ICD9Desc)


                        Dim strConctCPT As String
                        Dim arrstrConctCPT() As String
                        Dim strCPTCode As String
                        Dim strCPTDesc As String = ""
                        strConctCPT = trvCPT.SelectedNode.Text
                        arrstrConctCPT = Split(strConctCPT, "-", 2)
                        strCPTCode = arrstrConctCPT.GetValue(0)
                        If (arrstrConctCPT.Length > 1) Then
                            strCPTDesc = arrstrConctCPT.GetValue(1)
                        End If


                        'set value for ICDCODE,ICDDesc,ICDCODE,ICDDesc, 
                        .SetData(NewRow + 1, Col_ICD9Code, strICDCode)
                        .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc)
                        .SetData(NewRow + 1, COl_CPTCode, strCPTCode)
                        .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc)
                        .SetData(NewRow + 1, Col_Units, 0)
                        .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount + 1)
                        .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                    End If
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvCPT.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trvCPT.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub txtsearchrht_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchrht.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                trvCPT.Select()
            Else
                trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub rdocode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdocode.Click
        Try



            ' fill modifier data in treeview
            If _ModifierClick = True Then
                Call FillModifiers()
            Else
                ' fill CPT data in treeview 
                trvCPT.BeginUpdate()
                If IsNothing(dtOrderbyCode_CPT) = True Then
                    dtOrderbyCode_CPT = New DataTable
                End If

                Dim i As Integer

                'Dim dt As DataTable
                If dtOrderbyCode_CPT.Rows.Count = 0 Then
                    If (IsNothing(dtOrderbyCode_CPT) = False) Then
                        dtOrderbyCode_CPT.Dispose()
                        dtOrderbyCode_CPT = Nothing
                    End If
                    Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
                    objTreatmentDBLayer = New ClsTreatmentDBLayer()
                    dtOrderbyCode_CPT = objTreatmentDBLayer.FillCPT(0)
                    objTreatmentDBLayer.Dispose()
                    objTreatmentDBLayer = Nothing
                End If


                trvCPT.Hide()

                trvCPT.Nodes(0).Nodes.Clear()
                If dtOrderbyCode_CPT.Rows.Count < 400 Then
                    CPTCount = dtOrderbyCode_CPT.Rows.Count - 1
                Else
                    CPTCount = 400
                End If
                ' fill treeview with modifier
                For i = 0 To CPTCount 'dtOrderbyCode_CPT.Rows.Count - 1


                    Dim rootnode As myTreeNode
                    rootnode = Searchnode_CPT(CType(dtOrderbyCode_CPT.Rows(i)(3), String))
                    If IsNothing(rootnode) Then
                        Dim specialtynode As myTreeNode
                        specialtynode = New myTreeNode(CType(dtOrderbyCode_CPT.Rows(i)(3), String), -1)
                        specialtynode.ImageIndex = 1
                        specialtynode.SelectedImageIndex = 1
                        trvCPT.Nodes.Item(0).Nodes.Add(specialtynode)
                        rootnode = specialtynode
                    End If
                    Dim myNode As myTreeNode
                    myNode = New myTreeNode(dtOrderbyCode_CPT.Rows(i)(1), dtOrderbyCode_CPT.Rows(i)(4), CType(dtOrderbyCode_CPT.Rows(i)(0), String))
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2
                    rootnode.Nodes.Add(myNode)

                Next
            End If

            trvCPT.ExpandAll()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.Select()
            trvCPT.EndUpdate()
            txtsearchrht.Text = ""
            txtsearchrht.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub rdoDescriptionrht_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDescriptionrht.Click
        Try


            'Fill modifier data
            If _ModifierClick = True Then
                Call FillModifiers()
            Else
                'fill CPT data
                If IsNothing(dtOrderbyDesc_CPT) = True Then
                    dtOrderbyDesc_CPT = New DataTable
                End If

                Dim i As Integer
                'Dim dt As DataTable

                If dtOrderbyDesc_CPT.Rows.Count = 0 Then
                    Dim objTreatmentDBLayer As ClsTreatmentDBLayer = Nothing
                    objTreatmentDBLayer = New ClsTreatmentDBLayer()
                    If (IsNothing(dtOrderbyDesc_CPT) = False) Then
                        dtOrderbyDesc_CPT.Dispose()
                        dtOrderbyDesc_CPT = Nothing
                    End If
                    dtOrderbyDesc_CPT = objTreatmentDBLayer.FillCPT(1)
                    objTreatmentDBLayer.Dispose()
                    objTreatmentDBLayer = Nothing
                End If


                trvCPT.Hide()

                trvCPT.Nodes(0).Nodes.Clear()
                trvCPT.BeginUpdate()
                If dtOrderbyDesc_CPT.Rows.Count < 400 Then
                    CPTCount = dtOrderbyDesc_CPT.Rows.Count - 1
                Else
                    CPTCount = 400
                End If
                For i = 0 To CPTCount 'dtOrderbyDesc_CPT.Rows.Count - 1


                    Dim rootnode As myTreeNode
                    rootnode = Searchnode_CPT(CType(dtOrderbyDesc_CPT.Rows(i)(3), String))
                    If IsNothing(rootnode) Then
                        Dim specialtynode As myTreeNode
                        specialtynode = New myTreeNode(CType(dtOrderbyDesc_CPT.Rows(i)(3), String), -1)
                        specialtynode.ImageIndex = 1
                        specialtynode.SelectedImageIndex = 1
                        trvCPT.Nodes.Item(0).Nodes.Add(specialtynode)
                        rootnode = specialtynode
                    End If
                    Dim myNode As myTreeNode
                    myNode = New myTreeNode(dtOrderbyDesc_CPT.Rows(i)(1), dtOrderbyDesc_CPT.Rows(i)(4), CType(dtOrderbyDesc_CPT.Rows(i)(0), String))
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2
                    rootnode.Nodes.Add(myNode)

                Next
            End If
            trvCPT.ExpandAll()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)

            trvCPT.Select()
            trvCPT.EndUpdate()
            txtsearchrht.Text = ""
            txtsearchrht.Focus()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtsearchrht_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchrht.TextChanged
        Try
            'sarika 26th sept 07
            If txtsearchrht.Tag <> Trim(txtsearchrht.Text) Then
                'If _ModifierClick = True Then
                If btnmodifier.Dock = DockStyle.Top Then

                    'Modifiers
                    If rdoDescriptionrht.Checked = True Then
                        AddSrMod(Trim(txtsearchrht.Text), dtOrderbyDesc_MOD)
                    Else
                        AddSrMod(Trim(txtsearchrht.Text), dtOrderbyCode_MOD)
                    End If
                ElseIf btnCPT.Dock = DockStyle.Top Then
                    'cpt
                    If rdoDescriptionrht.Checked = True Then
                        AddSrCPT(Trim(txtsearchrht.Text), dtOrderbyDesc_CPT)
                    Else
                        AddSrCPT(Trim(txtsearchrht.Text), dtOrderbyCode_CPT)
                    End If
                End If

                'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'End If
                'If Len(Trim(txtsearchDrug.Text)) = 1 Then
                txtsearchrht.Tag = Trim(txtsearchrht.Text)
                txtsearchrht.Focus()
                'End If
            End If

            'Exit Sub ''COMMENT BY SUDHIR 20090131 - LINE WAS NOT LETTING SEARCH THE TEXT ''
            '------------------------------------------------------------


            If Trim(txtsearchrht.Text) <> "" Then
                If trvCPT.Nodes.Count > 0 Then


                    If trvCPT.Nodes.Item(0).GetNodeCount(False) Then
                        Dim mychildnode As myTreeNode
                        'child node collection
                        For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
                            If _ModifierClick = True Then
                                '' seach On Modifier
                                If rdocode.Checked = True Then
                                    '' seach On ModifierCode
                                    If Mid(UCase(mychildnode.Tag), 1, Len(UCase(Trim(txtsearchrht.Text)))) = UCase(Trim(txtsearchrht.Text)) Then
                                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                        '*************
                                        trvCPT.SelectedNode = mychildnode
                                        txtsearchrht.Focus()
                                        Exit Sub
                                    End If
                                Else
                                    '' seach On ModifierDecs
                                    Dim str As String
                                    str = Mid(UCase(mychildnode.Text), Len(UCase(mychildnode.Tag)) + 2, Len(UCase(Trim(txtsearchrht.Text))))
                                    If str = UCase(Trim(txtsearchrht.Text)) Then
                                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                        '*************
                                        trvCPT.SelectedNode = mychildnode
                                        txtsearchrht.Focus()
                                        Exit Sub
                                    End If

                                End If
                            Else
                                '' seach On CPT
                                Dim CPTnode As myTreeNode
                                For Each CPTnode In mychildnode.Nodes
                                    If rdocode.Checked = True Then
                                        '' seach On CPTCode
                                        If Mid(UCase(CPTnode.Tag), 1, Len(UCase(Trim(txtsearchrht.Text)))) = UCase(Trim(txtsearchrht.Text)) Then
                                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                            trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                            '*************
                                            trvCPT.SelectedNode = CPTnode
                                            txtsearchrht.Focus()
                                            Exit Sub
                                        End If
                                    Else
                                        '' seach On CPTDecs
                                        Dim str As String
                                        str = Mid(UCase(CPTnode.Text), Len(UCase(CPTnode.Tag)) + 2, Len(UCase(Trim(txtsearchrht.Text))))
                                        If str = UCase(Trim(txtsearchrht.Text)) Then
                                            '*************code added by sagar for showing the selected drug at the top on 4 july 2007
                                            trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
                                            '*************
                                            trvCPT.SelectedNode = CPTnode
                                            txtsearchrht.Focus()
                                            Exit Sub
                                        End If
                                    End If
                                Next
                            End If

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagonosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddSrMod(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            'nModifierID,             'sModifierCode'=sModifierCode,           'sDescription'=sDescription

            If rdocode.Checked = True Then
                ''Code 
                dv.RowFilter = dt.Columns(1).ToString & " Like '%" & strsearch & "%'"
            Else
                ''Description
                dv.RowFilter = dt.Columns(2).ToString & " Like '%" & strsearch & "%'"
            End If
            tdt = New DataTable
            tdt = dv.ToTable

            trvCPT.Nodes.Clear()

            Dim rootNode As New TreeNode
            rootNode = New myTreeNode("Modifier", -1)
            trvCPT.ImageList = imglistTrvModifier
            rootNode.ImageIndex = 0
            rootNode.SelectedImageIndex = 0
            trvCPT.Nodes.Add(rootNode)

            If IsNothing(tdt) = False Then
                trvCPT.Nodes.Item(0).ImageIndex = 0
                trvCPT.Nodes.Item(0).SelectedImageIndex = 0
                trvCPT.Nodes.Item(0).Nodes.Clear()
                If tdt.Rows.Count < 400 Then
                    ModifierCount = tdt.Rows.Count - 1
                Else
                    ModifierCount = 400
                End If
                For i = 0 To ModifierCount 'tdt.Rows.Count - 1
                    Dim myNode As myTreeNode
                    myNode = New myTreeNode(tdt.Rows(i)("sModifierCode") & "-" & tdt.Rows(i)("sDescription"), -1, CType(tdt.Rows(i)("sModifierCode"), String))
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2

                    trvCPT.Nodes.Item(0).Nodes.Add(myNode)
                Next
            End If
            trvCPT.ExpandAll()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddSrCPT(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            'select c.sCPTcode,c.sCPTcode+'-'+c.sDescription,c.sdescription,s.sdescription,c.nCPTID  

            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            If rdocode.Checked = True Then
                ''Code 
                dv.RowFilter = dt.Columns(0).ToString & " Like '%" & strsearch & "%'"
            Else
                ''Description
                dv.RowFilter = dt.Columns(2).ToString & " Like '%" & strsearch & "%'"
            End If
            tdt = New DataTable
            tdt = dv.ToTable

            trvCPT.Nodes(0).Nodes.Clear()

            trvCPT.Hide()
            trvCPT.BeginUpdate()
            'fill treeview with CPT data
            If tdt.Rows.Count < 400 Then
                CPTCount = tdt.Rows.Count - 1
            Else
                CPTCount = 400
            End If
            For i = 0 To CPTCount 'tdt.Rows.Count - 1


                Dim rootnode As myTreeNode
                rootnode = Searchnode_CPT(CType(tdt.Rows(i)(3), String))
                If IsNothing(rootnode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode(CType(tdt.Rows(i)(3), String), -1)

                    specialtynode.ImageIndex = 1
                    specialtynode.SelectedImageIndex = 1
                    trvCPT.Nodes.Item(0).Nodes.Add(specialtynode)
                    rootnode = specialtynode
                End If
                Dim myNode As myTreeNode
                myNode = New myTreeNode(tdt.Rows(i)(1), CType(tdt.Rows(i)(4), Long), CType(tdt.Rows(i)(0), String))
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                rootnode.Nodes.Add(myNode)
                rootnode.Expand()
            Next
            trvCPT.Nodes.Item(0).Expand()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)

            'trvCPT.Select()
            trvCPT.EndUpdate()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillModifiers_old(ByVal dtModifier As DataTable, ByVal Flag As Int16)
        Try

            Dim objclsModifier As New clsModifiers
            Dim dv As DataView
            dv = objclsModifier.GetAllModifiers(Flag)
            objclsModifier.Dispose()
            objclsModifier = Nothing

            Dim dtModifierTable As DataTable = Nothing

            If IsNothing(dv.Table) = False Then
                dtModifierTable = dv.ToTable
            Else
                If (IsNothing(dtModifier) = False) Then
                    dtModifierTable = dtModifier.Copy()
                    dv = dtModifier.DefaultView
                Else
                    dtModifierTable = Nothing
                End If

            End If

            If Flag = 0 Then
                'search by code
                If IsNothing(dtModifierTable) = False Then
                    'If IsNothing(dtOrderbyCode_MOD) Then
                    '    dtOrderbyCode_MOD = New DataTable
                    'End If
                    If (IsNothing(dtOrderbyCode_MOD) = False) Then
                        dtOrderbyCode_MOD.Dispose()
                        dtOrderbyCode_MOD = Nothing
                    End If
                    dtOrderbyCode_MOD = dtModifierTable
                End If
            Else
                'search by desc
                If IsNothing(dtModifierTable) = False Then
                    'If IsNothing(dtOrderbyCode_MOD) Then
                    '    dtOrderbyCode_MOD = New DataTable
                    'End If
                    If (IsNothing(dtOrderbyDesc_MOD) = False) Then
                        dtOrderbyDesc_MOD.Dispose()
                        dtOrderbyDesc_MOD = Nothing
                    End If
                    dtOrderbyDesc_MOD = dtModifierTable
                End If
            End If




            Dim i As Int16

            trvCPT.Nodes.Clear()

            Dim rootNode As New TreeNode
            rootNode = New myTreeNode("Modifier", -1)
            trvCPT.ImageList = imglistTrvModifier
            rootNode.ImageIndex = 0
            rootNode.SelectedImageIndex = 0
            trvCPT.Nodes.Add(rootNode)

            If IsNothing(dv) = False Then
                trvCPT.Nodes.Item(0).ImageIndex = 0
                trvCPT.Nodes.Item(0).SelectedImageIndex = 0
                trvCPT.Nodes.Item(0).Nodes.Clear()
                If dv.Table.Rows.Count < 400 Then
                    ModifierCount = dv.Table.Rows.Count - 1
                Else
                    ModifierCount = 400
                End If
                For i = 0 To ModifierCount 'dv.Table.Rows.Count - 1
                    Dim myNode As myTreeNode
                    myNode = New myTreeNode(dv.Table.Rows(i)("sModifierCode") & "-" & dv.Table.Rows(i)("sDescription"), dv.Table.Rows(i)("nModifierID"), CType(dv.Table.Rows(i)("sModifierCode"), String))
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2

                    trvCPT.Nodes.Item(0).Nodes.Add(myNode)
                Next
            End If
            trvCPT.ExpandAll()
            trvCPT.Show()
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub FillModifiers()
        Try
            Dim objclsModifier As New clsModifiers
            Dim dtModifier As DataTable
            dtModifier = objclsModifier.GetAllModifier
            objclsModifier.Dispose()
            objclsModifier = Nothing

            If IsNothing(dtModifier) = False Then
                GloUC_trvAssociates.ImageList = imglistTrvModifier
                GloUC_trvAssociates.DataSource = dtModifier
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvAssociates.CodeMember = Convert.ToString(dtModifier.Columns("sModifierCode").ColumnName)
                GloUC_trvAssociates.ValueMember = Convert.ToString(dtModifier.Columns("nModifierID").ColumnName)
                GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtModifier.Columns("sDescription").ColumnName)
                GloUC_trvAssociates.FillTreeView()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub SetGridStyle()
        Try
            'gloC1FlexStyle.Style(C1Dignosis)
            With C1Dignosis
                Dim _TotalWidth As Single = .Width - 5
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = Col_Count

                'for ICD9

                ' If RbICD9.Checked Then
                .SetData(0, Col_ICD9Code_Description, "Diagnosis")
                ' ElseIf RbICD10.Checked Then
                '.SetData(0, Col_ICD9Code_Description, "ICD10")
                ' End If

                .Cols(Col_ICD9Code_Description).AllowEditing = False
                .Cols(Col_ICD9Code_Description).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Code).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Code, "ICD9CODE")
                .Cols(Col_ICD9Code).AllowEditing = True
                .Cols(Col_ICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Desc).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Desc, "ICD9Description")
                .Cols(Col_ICD9Desc).AllowEditing = True
                .Cols(Col_ICD9Desc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COl_CPTCode).Width = _TotalWidth * 0
                .SetData(0, COl_CPTCode, "CPTCODE")
                .Cols(COl_CPTCode).AllowEditing = True
                .Cols(COl_CPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTDesc).Width = _TotalWidth * 0
                .SetData(0, Col_CPTDesc, "CPTDescription")
                .Cols(Col_CPTDesc).AllowEditing = True
                .Cols(Col_CPTDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCode).Width = _TotalWidth * 0
                .SetData(0, Col_ModCode, "MODCODE")
                .Cols(Col_ModCode).AllowEditing = True
                .Cols(Col_ModCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModDesc).Width = _TotalWidth * 0
                .SetData(0, Col_ModDesc, "MODDescription")
                .Cols(Col_ModDesc).AllowEditing = True
                .Cols(Col_ModDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(Col_Units).Width = 59
                .SetData(0, Col_Units, "Units")
                .Cols(Col_Units).DataType = GetType(System.Decimal)
                .Cols(Col_Units).AllowEditing = True
                .Cols(Col_Units).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_Timed_PT).Width = 79
                .SetData(0, Col_Timed_PT, "Timed PT")
                .Cols(Col_Timed_PT).DataType = GetType(Integer)
                .Cols(Col_Timed_PT).AllowEditing = True
                .Cols(Col_Timed_PT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_UnTimed_PT).Width = 81
                .SetData(0, Col_UnTimed_PT, "Untimed PT")
                .Cols(Col_UnTimed_PT).DataType = GetType(Integer)
                .Cols(Col_UnTimed_PT).AllowEditing = True
                .Cols(Col_UnTimed_PT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Count).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Count, "ICD9 Count")
                .Cols(Col_ICD9Count).DataType = GetType(System.Int64)
                .Cols(Col_ICD9Count).AllowEditing = True
                .Cols(Col_ICD9Count).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTCount).Width = _TotalWidth * 0
                .SetData(0, Col_CPTCount, "CPT Count")
                .Cols(Col_CPTCount).DataType = GetType(System.Int64)
                .Cols(Col_CPTCount).AllowEditing = True
                .Cols(Col_CPTCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCount).Width = _TotalWidth * 0
                .SetData(0, Col_ModCount, "Mod Count")
                .Cols(Col_ModCount).DataType = GetType(System.Int64)
                .Cols(Col_ModCount).AllowEditing = True
                .Cols(Col_ModCount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_SnomedCode).Width = _TotalWidth * 0
                .SetData(0, Col_SnomedCode, "SnomedCode")
                .Cols(Col_SnomedCode).AllowEditing = False
                .Cols(Col_SnomedCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_SnomedDesc).Width = _TotalWidth * 0
                .SetData(0, Col_SnomedDesc, "SnomedDesc")
                .Cols(Col_SnomedDesc).AllowEditing = False
                .Cols(Col_SnomedDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_nICDRevision).Width = _TotalWidth * 0
                .Cols(Col_nICDRevision).AllowEditing = False
                .Cols(Col_nICDRevision).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_nConceptId).Width = _TotalWidth * 0
                .Cols(Col_nConceptId).AllowEditing = False
                .Cols(Col_nConceptId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_sConceptDescription).Width = _TotalWidth * 0
                .Cols(Col_sConceptDescription).AllowEditing = False
                .Cols(Col_sConceptDescription).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                .Cols(Col_nIsOneToOneMappingSnoMed).Width = _TotalWidth * 0
                .Cols(Col_nIsOneToOneMappingSnoMed).AllowEditing = False
                .Cols(Col_nIsOneToOneMappingSnoMed).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ReasonConceptId).Width = _TotalWidth * 0
                .SetData(0, Col_ReasonConceptId, "ReasonconceptID")
                .Cols(Col_ReasonConceptId).AllowEditing = False
                .Cols(Col_ReasonConceptId).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ReasonConceptDescription).Width = _TotalWidth * 0
                .SetData(0, Col_ReasonConceptDescription, "ReasonconceptDesc")
                .Cols(Col_ReasonConceptDescription).AllowEditing = False
                .Cols(Col_ReasonConceptDescription).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                If gblnIsExamPTBillingEnabled AndAlso Not _IsViewDiagnosis AndAlso ExamID <> 0 Then
                    ' .Cols(Col_ICD9Code_Description).Width = _TotalWidth - (40 + 81)
                    .Cols(Col_ICD9Code_Description).Width = (_TotalWidth * 1.6 - (59 + 79 + 81))
                    .Cols(Col_Timed_PT).Visible = True
                    .Cols(Col_UnTimed_PT).Visible = True
                Else
                    .Cols(Col_ICD9Code_Description).Width = _TotalWidth + 59
                    .Cols(Col_Timed_PT).Visible = False
                    .Cols(Col_UnTimed_PT).Visible = False
                End If
                If (.Cols(Col_ICD9Code_Description).Width < 270) Then ''integrated from 8071  for incident 00065807 diagnosis column not displaying properly
                    .Cols(Col_ICD9Code_Description).Width = 270
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblNew.Click
        C1Dignosis.Rows.Count = 1
    End Sub

    Private Sub tblsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsave.Click
        If C1Dignosis.Rows.Count - 1 = 0 Then
            Exit Sub
        End If
        Call saveDiagnosis()

    End Sub

    Private Sub tblFinish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblFinish.Click
        If C1Dignosis.Rows.Count - 1 = 0 Then
            Exit Sub
        End If
        If saveDiagnosis() = True Then
            Me.Close()
        End If

    End Sub

    Private Sub tblClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblClose.Click
        Me.Close()
        'Dim objmig As New frmMigrateDignosis
        'objmig.ShowDialog(Me)
        'Me.Show()
    End Sub


    Private Sub RemoveDiagnosisToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveSelectedDiagnosis.Click
        Try
            With C1Dignosis
                Dim RemoveRowIndex As Integer
                Dim ParentRowIndex As Integer
                Dim LastRowIndex As Integer
                Dim cnt As Integer = 0
                If .Row > 0 Then
                    Dim ofNode As C1.Win.C1FlexGrid.Node
                    ofNode = .Rows(.Row).Node

                    If ofNode.Level = 2 Then '''' Remove Modifier
                        RemoveRowIndex = .Row
                        ParentRowIndex = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                        ofNode = Nothing
                        .Rows.Remove(RemoveRowIndex)
                        ofNode = .Rows(ParentRowIndex).Node
                        If ofNode.Children > 0 Then '''' set The count index of modifier
                            LastRowIndex = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            For i As Integer = ParentRowIndex + 1 To LastRowIndex
                                cnt = cnt + 1
                                .SetData(i, Col_ModCount, cnt)
                            Next
                        End If
                    ElseIf ofNode.Level = 1 Then ''''Remove CPT
                        RemoveRowIndex = .Row
                        ParentRowIndex = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                        ofNode.RemoveNode()
                        '.Select(ParentRowIndex, Col_ICD9Code_Description)

                        For i As Integer = .Row To .Rows.Count - 1
                            If Not IsNothing(.GetData(i, Col_CPTCount)) Then
                                If CType(.GetData(i, Col_CPTCount), String) <> "" Then
                                    .SetData(i, Col_CPTCount, (CType(.GetData(i, Col_CPTCount), Integer) - 1))
                                Else
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If

                        Next

                    ElseIf ofNode.Level = 0 Then '''' Remove ICD9
                        ofNode.RemoveNode()
                        If .Rows.Count > 1 Then
                            For i As Integer = 1 To .Rows.Count - 1
                                ofNode = Nothing
                                ofNode = .Rows(i).Node
                                cnt = cnt + 1
                                .SetData(i, Col_ICD9Count, cnt)
                                If ofNode.Children > 0 Then

                                    'Dim CPTCount As Integer
                                    '  Dim MODCount As Integer
                                    Dim ofModNode As C1.Win.C1FlexGrid.Node
                                    For j As Integer = 1 To ofNode.Children
                                        i = i + 1
                                        .SetData(i, Col_ICD9Count, cnt)

                                        ofModNode = .Rows(i).Node
                                        If ofModNode.Children > 0 Then
                                            For k As Integer = 1 To ofModNode.Children
                                                i = i + 1
                                                .SetData(i, Col_ICD9Count, cnt)

                                            Next
                                        End If
                                    Next

                                End If
                            Next
                        End If

                    End If
                End If

            End With
            frmPatientExam.blnChangesMade = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Dignosis_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1Dignosis.AfterEdit
        'code added by dipak 20090819 for fixing bug 2650 ':New Exam >  Click DxCPT -- Its accepting Units in -ve 
        'for validating Units value for -ve no
        If (C1Dignosis.GetData(C1Dignosis.Row, C1Dignosis.Col) < 0) Then
            C1Dignosis.SetData(C1Dignosis.Row, C1Dignosis.Col, Math.Abs(C1Dignosis.GetData(C1Dignosis.Row, C1Dignosis.Col)))
        End If
        If (C1Dignosis.GetData(C1Dignosis.Row, C1Dignosis.Col) = 0) Then
            C1Dignosis.SetData(C1Dignosis.Row, C1Dignosis.Col, 1)
        End If
        Dim _unit As Decimal = FormatNumber(Convert.ToDecimal(C1Dignosis.GetData(C1Dignosis.RowSel, Col_Units)))
        C1Dignosis.SetData(C1Dignosis.RowSel, Col_Units, 0)
        C1Dignosis.SetData(C1Dignosis.RowSel, Col_Units, _unit)
        'code added by dipak is end
    End Sub

    Private Sub C1Dignosis_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1Dignosis.KeyPressEdit
        If e.Col = Col_Units Or e.Col = Col_Timed_PT Or e.Col = Col_UnTimed_PT Then
            'Dim ch As String = .ToString()
            If (Char.IsDigit(e.KeyChar) = False) Then
                If (e.KeyChar.ToString() = "-") Then
                    e.Handled = True
                End If
            End If

            If e.Col = Col_Timed_PT Or e.Col = Col_UnTimed_PT Then
                If (Char.IsDigit(e.KeyChar) = False) Then
                    If (e.KeyChar.ToString() = ".") Then
                        e.Handled = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub C1Dignosis_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles C1Dignosis.KeyUp
        Try
            If e.KeyCode = Keys.Delete Then
                Dim oNode As C1.Win.C1FlexGrid.Node = C1Dignosis.Rows(C1Dignosis.RowSel).Node
                If oNode.Level = 1 Then
                    If C1Dignosis.ColSel = Col_Units Then
                        C1Dignosis.SetData(C1Dignosis.RowSel, C1Dignosis.ColSel, 1)
                    ElseIf C1Dignosis.ColSel = Col_Timed_PT Or C1Dignosis.ColSel = Col_UnTimed_PT Then
                        C1Dignosis.SetData(C1Dignosis.RowSel, C1Dignosis.ColSel, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.AddTransactionLine, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    '' COMMENT BY SUDHIR 20090901 ''
    'Private Sub C1Dignosis_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Dignosis.MouseDown
    '    Try
    '        If e.Button = Windows.Forms.MouseButtons.Right Then

    '            With C1Dignosis
    '                Dim r As Integer = .HitTest(e.X, e.Y).Row
    '                If r > 0 Then
    '                    .Select(r, True)

    '                    If .GetData(r, Col_ICD9Code_Description) = "" Then
    '                        C1Dignosis.ContextMenuStrip = Nothing
    '                    Else
    '                        C1Dignosis.ContextMenuStrip = ContextMenuDiagnosis
    '                        Dim intSlectedrow As Integer = C1Dignosis.Row
    '                        ContextMenuDiagnosis.Items(0).Visible = True
    '                        ContextMenuDiagnosis.Items(1).Visible = False
    '                        ContextMenuDiagnosis.Items(2).Visible = False
    '                        ContextMenuDiagnosis.Items(3).Visible = False
    '                        ContextMenuDiagnosis.Items(4).Visible = False
    '                        ContextMenuDiagnosis.Items(5).Visible = False
    '                        ContextMenuDiagnosis.Items(6).Visible = False
    '                        ContextMenuDiagnosis.Items(7).Visible = False
    '                        ContextMenuDiagnosis.Items(8).Visible = False
    '                        ContextMenuDiagnosis.Items(9).Visible = False
    '                        Dim ofNode As C1.Win.C1FlexGrid.Node
    '                        ofNode = .Rows(intSlectedrow).Node

    '                        If ofNode.Level = 0 Then
    '                            mnuRemoveSelectedDiagnosis.Text = "Remove Diagnosis"
    '                            'RemoveDiagnosisToolStripMenuItem.BackgroundImage = Global.gloEMR.My.Resources.Remove_Diagnosis
    '                            'RemoveDiagnosisToolStripMenuItem.BackgroundImageLayout = ImageLayout.Center
    '                            mnuRemoveSelectedDiagnosis.Image = Global.gloEMR.My.Resources.Remove_Diagnosis01
    '                            mnuRemoveSelectedDiagnosis.ImageAlign = ContentAlignment.MiddleLeft
    '                            ContextMenuDiagnosis.Items(9).Visible = True
    '                            ContextMenuDiagnosis.Items(9).Image = Global.gloEMR.My.Resources.Set_as_primary
    '                            ContextMenuDiagnosis.Items(9).ImageAlign = ContentAlignment.MiddleLeft
    '                        ElseIf ofNode.Level = 1 Then
    '                            mnuRemoveSelectedDiagnosis.Text = "Remove Treatment"
    '                            mnuRemoveSelectedDiagnosis.Image = Global.gloEMR.My.Resources.Remove_Treatment
    '                            mnuRemoveSelectedDiagnosis.ImageAlign = ContentAlignment.MiddleLeft

    '                            ContextMenuDiagnosis.Items(7).Visible = True
    '                            ContextMenuDiagnosis.Items(7).Image = Global.gloEMR.My.Resources.Associate_CPT_to_all_Dx01
    '                            ContextMenuDiagnosis.Items(7).ImageAlign = ContentAlignment.MiddleLeft

    '                            ContextMenuDiagnosis.Items(8).Visible = True
    '                            ContextMenuDiagnosis.Items(8).Image = Global.gloEMR.My.Resources.Associate_CPT_to_all_unassociate_ICD901
    '                            ContextMenuDiagnosis.Items(8).ImageAlign = ContentAlignment.MiddleLeft

    '                        ElseIf ofNode.Level = 2 Then
    '                            mnuRemoveSelectedDiagnosis.Text = "Remove Modifier"
    '                            mnuRemoveSelectedDiagnosis.Image = Global.gloEMR.My.Resources.Remove_Modify
    '                            mnuRemoveSelectedDiagnosis.ImageAlign = ContentAlignment.MiddleLeft
    '                        End If

    '                    End If
    '                Else
    '                    C1Dignosis.ContextMenuStrip = Nothing
    '                End If
    '            End With

    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub C1Dignosis_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Dignosis.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Me.TopMost = True  '' 8071 integrated  changes done for incident CAS-02726-B4X7D1

                With C1Dignosis
                    Dim r As Integer = .HitTest(e.X, e.Y).Row
                    If r > 0 AndAlso Not _IsViewDiagnosis Then
                        Dim oNode As C1.Win.C1FlexGrid.Node = C1Dignosis.Rows(r).Node
                        .Select(r, True)

                        If .GetData(r, Col_ICD9Code_Description) = "" Then
                            'Try
                            '    If (IsNothing(C1Dignosis.ContextMenuStrip) = False) Then
                            '        C1Dignosis.ContextMenuStrip.Dispose()
                            '        C1Dignosis.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            C1Dignosis.ContextMenuStrip = Nothing
                        Else
                            'Try
                            '    If (IsNothing(C1Dignosis.ContextMenuStrip) = False) Then
                            '        C1Dignosis.ContextMenuStrip.Dispose()
                            '        C1Dignosis.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            ChangeZOrderAndContextMenuStrip(C1Dignosis, ContextMenuDiagnosis)
                            ' Dim arr() As String
                            ' Dim strICDcode As String
                            Dim intSlectedrow As Integer = C1Dignosis.Row
                            ContextMenuDiagnosis.Items(0).Visible = False
                            ContextMenuDiagnosis.Items(1).Visible = True
                            ContextMenuDiagnosis.Items(2).Visible = False
                            ContextMenuDiagnosis.Items(3).Visible = False
                            ContextMenuDiagnosis.Items(4).Visible = False
                            ContextMenuDiagnosis.Items(5).Visible = False
                            ContextMenuDiagnosis.Items(6).Visible = False
                            ContextMenuDiagnosis.Items(7).Visible = False
                            ContextMenuDiagnosis.Items(8).Visible = False
                            ContextMenuDiagnosis.Items(9).Visible = False
                            ContextMenuDiagnosis.Items(10).Visible = False
                            ContextMenuDiagnosis.Items(11).Visible = False
                            ContextMenuDiagnosis.Items(12).Visible = False
                            ContextMenuDiagnosis.Items(13).Visible = False
                            ContextMenuDiagnosis.Items(14).Visible = False
                            ContextMenuDiagnosis.Items(15).Visible = False
                            ContextMenuDiagnosis.Items(16).Visible = False
                            ContextMenuDiagnosis.Items(17).Visible = False
                            Select Case oNode.Level
                                Case 0
                                    mnuRemoveSelectedDiagnosis.Text = "Remove Selected Diagnosis"
                                    mnuRemoveAllDiagnosis.Text = "Remove all Diagnosis"
                                    ContextMenuDiagnosis.Items(0).Visible = True
                                    ContextMenuDiagnosis.Items(10).Visible = True

                                Case 1
                                    mnuRemoveSelectedDiagnosis.Text = "Remove Selected Treatment"
                                    mnuRemoveAllDiagnosis.Text = "Remove all Treatment"
                                    ContextMenuDiagnosis.Items(0).Visible = True
                                    ContextMenuDiagnosis.Items(8).Visible = True
                                    ContextMenuDiagnosis.Items(9).Visible = True
                                    ContextMenuDiagnosis.Items(11).Visible = True
                                    ContextMenuDiagnosis.Items(12).Visible = True
                                    ContextMenuDiagnosis.Items(17).Visible = True

                                Case 2
                                    mnuRemoveAllDiagnosis.Text = "Remove All modifier"
                                    ContextMenuDiagnosis.Items(0).Visible = True
                                    ContextMenuDiagnosis.Items(13).Visible = True
                                    ContextMenuDiagnosis.Items(14).Visible = True
                                    mnuRemoveSelectedDiagnosis.Text = "Remove Selected Modifier"
                            End Select

                        End If
                    Else
                        'Try
                        '    If (IsNothing(C1Dignosis.ContextMenuStrip) = False) Then
                        '        C1Dignosis.ContextMenuStrip.Dispose()
                        '        C1Dignosis.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        C1Dignosis.ContextMenuStrip = Nothing
                    End If
                End With

            End If
            ''Resolved issue #90791-gloEMR: Dx-CPT- Application gives exception
            tstripDiagnosis.Select()
            tstripDiagnosis.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.TopMost = False '' 8071 integrated  changes done for incident CAS-02726-B4X7D1
        End Try
    End Sub
    '' END SUDHIR ''
    Public Function saveDiagnosis() As Boolean
        Try
            Application.DoEvents()
            Dim arrExam As New ArrayList

            With C1Dignosis
                .Col = Col_ICD9Code
                .Select()
                Dim i As Integer
                Dim lst As myList
                'Dim lstExam As myList

                Dim arrList As New ArrayList

                Dim strICD9Code As String = ""
                Dim strICD9Desc As String = ""
                Dim strSnomedCode As String = ""
                Dim strSnomedDesc As String = ""
                Dim strCPTCode As String = ""
                Dim strCPTDesc As String = ""
                Dim strMODCode As String = ""
                Dim strMODDesc As String = ""
                Dim nICD9Count As Integer = 0
                Dim nCPTCount As Integer = 0
                Dim nModCount As Integer = 0
                Dim intUnits As System.Double
                Dim timed_therapy As String = Nothing
                Dim untimed_therapy As String = Nothing

                For i = 1 To .Rows.Count - 1

                    ''

                    If RbICD9.Checked Then
                        If .GetData(i, Col_nICDRevision) = 10 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If


                    ElseIf RbICD10.Checked Then
                        If .GetData(i, Col_nICDRevision) = 9 Then
                            MessageBox.Show("ICD Type Mismatch. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                        End If

                    End If



                    ''
                    lst = New myList
                    Dim _Node As C1.Win.C1FlexGrid.Node


                    _Node = .Rows(i).Node


                    If _Node.Level = 1 Then
                        intUnits = C1Dignosis.GetData(i, Col_Units)
                        timed_therapy = C1Dignosis.GetData(i, Col_Timed_PT)
                        untimed_therapy = C1Dignosis.GetData(i, Col_UnTimed_PT)
                    End If
                    strICD9Code = .GetData(_Node.Row.Index, Col_ICD9Code).ToString().Trim()
                    strICD9Desc = .GetData(_Node.Row.Index, Col_ICD9Desc)
                    strSnomedCode = Convert.ToString(.GetData(_Node.Row.Index, Col_SnomedCode))
                    strSnomedDesc = Convert.ToString(.GetData(_Node.Row.Index, Col_SnomedDesc))

                    If _Node.Level = 0 Then
                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        arrExam.Add(New mytable(strICD9Desc, strICD9Code, strSnomedDesc, strSnomedCode, Convert.ToInt16(.GetData(_Node.Row.Index, Col_nICDRevision)), .GetData(_Node.Row.Index, Col_nIsOneToOneMappingSnoMed)))
                    End If
                    'if current row don't have any child means it is leaf and save to database  
                    If _Node.Children = 0 Then
                        _Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        Dim rowno As Integer = _Node.Row.Index

                        strICD9Code = .GetData(rowno, Col_ICD9Code)
                        strICD9Desc = .GetData(rowno, Col_ICD9Desc)

                        strSnomedCode = Convert.ToString(.GetData(rowno, Col_SnomedCode))
                        strSnomedDesc = Convert.ToString(.GetData(rowno, Col_SnomedDesc))

                        nICD9Count = .GetData(rowno, Col_ICD9Count)

                        If Not IsNothing(.GetData(rowno, COl_CPTCode)) Then
                            strCPTCode = .GetData(rowno, COl_CPTCode).ToString().Trim()
                        Else
                            strCPTCode = .GetData(rowno, COl_CPTCode)
                        End If
                        strCPTDesc = .GetData(rowno, Col_CPTDesc)
                        nCPTCount = .GetData(rowno, Col_CPTCount)

                        strMODCode = .GetData(rowno, Col_ModCode)
                        strMODDesc = .GetData(rowno, Col_ModDesc)
                        nModCount = .GetData(rowno, Col_ModCount)

                        'list for ICD9,CPT and Modifier in ExamICD9CPT Table
                        lst.Code = strICD9Code
                        lst.Description = strICD9Desc
                        lst.HistoryCategory = strCPTCode
                        lst.HistoryItem = strCPTDesc
                        lst.Value = strMODCode
                        lst.ParameterName = strMODDesc
                        lst.TemplateResult = intUnits
                        lst.ICD9Count = nICD9Count
                        lst.CPTCount = nCPTCount
                        lst.ModCount = nModCount
                        lst.SnowMadeID = Convert.ToString(.GetData(rowno, Col_SnomedCode))
                        lst.SnoDescription = Convert.ToString(.GetData(rowno, Col_SnomedDesc))
                        lst.nICDRevision = Convert.ToInt16(.GetData(rowno, Col_nICDRevision))
                        lst.ConceptId = Convert.ToString(.GetData(rowno, Col_nConceptId))
                        lst.ReasonConceptID = Convert.ToString(.GetData(rowno, Col_ReasonConceptId))
                        lst.ReasonConceptDesc = Convert.ToString(.GetData(rowno, Col_ReasonConceptDescription))

                        If Not String.IsNullOrEmpty(timed_therapy) Then
                            lst.TimedTherapy = timed_therapy
                        End If

                        If Not String.IsNullOrEmpty(untimed_therapy) Then
                            lst.UnTimedTherapy = untimed_therapy
                        End If

                        If frmAddProblemList.htIcdSnomed.ContainsKey(strICD9Code.Trim()) = False Then
                            frmAddProblemList.htIcdSnomed.Add(strICD9Code.Trim(), Convert.ToString(.GetData(rowno, Col_SnomedCode)))
                        Else
                            frmAddProblemList.htIcdSnomed.Item(strICD9Code.Trim()) = Convert.ToString(.GetData(rowno, Col_SnomedCode))
                        End If

                        arrList.Add(lst)


                        Dim drRow As DataRow = dtActiveCPTTable.NewRow()
                        drRow("sCPTCode") = strCPTCode
                        drRow("dtFromDate") = gloDateMaster.gloDate.DateAsNumber(GetVisitdate(VisitId))
                        drRow("dtToDate") = 0
                        dtActiveCPTTable.Rows.Add(drRow)
                        drRow = Nothing
                    End If


                Next
                Dim oclsDiagnosis As ClsDiagnosisDBLayer
                oclsDiagnosis = New ClsDiagnosisDBLayer


                'Check Active CPT
                Dim CPTAlert As String = gloGlobal.gloPMGlobal.getCPTDeativatedCPT(dtActiveCPTTable)
                dtActiveCPTTable.Clear()
                If (CPTAlert <> "") Then
                    Dim dResult As DialogResult = MessageBox.Show(CPTAlert, "Diagnosis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                    If dResult.ToString() = "Cancel" Then
                        oclsDiagnosis.Dispose()
                        oclsDiagnosis = Nothing
                        Return False
                    End If
                End If

                'save data in ExamICDCPT Table
                oclsDiagnosis.SaveDiagTreatmentAssociation(ExamID, _PatientID, VisitId, arrList, Me, , True)
                ''Added by Mayuri:20100212-Case No:GLO2010-0004374
                ''if user removes all diagnosis and saves data then system should save 
                If arrList.Count > 0 Then
                    strICD9Code = .GetData(.Row, Col_ICD9Code)
                    strICD9Desc = .GetData(.Row, Col_ICD9Desc)
                    frmProblemList.Diagonsis = strICD9Code & "-" & strICD9Desc

                End If
                oclsDiagnosis.Dispose()
                oclsDiagnosis = Nothing
                If (IsNothing(arrList) = False) Then
                    arrList.Clear()
                End If
            End With
            Dim _icd9 As Integer
            Dim _stricd9 As String = ""
            Dim _strdesc As String = ""
            frmPatientExam.Arrlist = arrExam

            If IsNothing(arrExam) = False Then
                If arrExam.Count > 0 Then
                    For _icd9 = 0 To arrExam.Count - 1
                        frmPatientExam._PrimaryDiaForICD9Driven = CType(arrExam.Item(_icd9), mytable).Code.ToString.Trim() & " " & CType(arrExam.Item(_icd9), mytable).Description.ToString.Trim()

                        Exit For
                    Next

                End If
            End If
            'Added by Rahul patel on 25-11-2010
            'For Resolving Case no : GLO2010-0007091 i.e for Problem list Not get Populated 
            frmSummaryofVisit.ArrlistDiagonsis = arrExam
            'End of Code Added by rahul patel 25-11-2010
            If IsNothing(arrExam) = False Then
                If arrExam.Count > 0 Then
                    frmPatientExam.blnChangesMade = True
                End If
            End If



            Me.Close()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.AddTransactionLine, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub CheckActiveCPT(ByVal arrlist As ArrayList)

    End Sub


    Private Sub FillICDCPTMOD()

        Try

            Dim _Row As Integer
            'set properties of treeview in flexgrid
            With C1Dignosis
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With


            Dim dtICD9 As DataTable
            Dim dtCPT As DataTable
            Dim dtMOD As DataTable
            Dim dvICD9 As DataView
            Dim dvCPT As DataView

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16

            'Dim strSelectQry As String
            'Dim strselecedICD9Qry As String
            'Dim strselectedCPTQry As String
            'Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""

            Dim nextICD As Integer
            Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
            objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
            ' flag = 0 - ICD9   flag = 1 - CPT flag = 2 -MOD
            dtICD9 = objDiagnosisDBLayer.FetchICD9CPTMod(ExamID, VisitId, "", "", "", 5)
            ''Addedby mayuri :20111130- ''To avoid duplicate diagnosis add in DxCPT from other modules like smartDx and smartCPT
            dtICD9.Columns.Remove("nLineNo")
            objDiagnosisDBLayer.Dispose()
            objDiagnosisDBLayer = Nothing
            If Not IsNothing(dtICD9) Then
                If dtICD9.Rows.Count > 0 Then
                    If dtICD9.Rows(0)("nICDRevision") = 10 Then
                        RbICD10.Checked = True
                    ElseIf dtICD9.Rows(0)("nICDRevision") = 9 Then
                        RbICD9.Checked = True
                    End If
                Else
                    If _IsFromExam Then
                        If _blnICDTransition = True Then
                            RbICD10.Checked = True
                        Else
                            RbICD9.Checked = True
                        End If
                    Else
                        If gblnIcd10Transition = True Then
                            RbICD10.Checked = True
                        Else
                            RbICD9.Checked = True
                        End If
                    End If
                End If
                dvICD9 = New DataView(dtICD9)
                If Not IsNothing(dtICD9) Then
                    dtICD9 = Nothing
                End If
                dtICD9 = New DataTable()
                dtICD9 = New DataTable

                Dim strICD9(dtICD9.Columns.Count - 1) As String

                For i As Integer = 0 To dtICD9.Columns.Count - 1

                    strICD9.SetValue(dtICD9.Columns(i).ColumnName, i)


                Next

                dtICD9 = dvICD9.ToTable(True, strICD9)
                ''''Pramod 04232009 End

                With dtICD9
                    If IsNothing(dtICD9) = False Then
                        For nICD9 = 0 To .Rows.Count - 1
                            Dim count As Integer = nICD9 + 1
                            If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                C1Dignosis.Rows.Add()
                                _Row = C1Dignosis.Rows.Count - 1
                                'set the properties for newly added row
                                With C1Dignosis.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                    If Not IsNothing(dtICD9.Rows(nICD9)("nICDRevision")) AndAlso Convert.ToString(dtICD9.Rows(nICD9)("nICDRevision")).Trim() <> "" Then
                                        If Convert.ToInt16(dtICD9.Rows(nICD9)("nICDRevision")) = 9 Then
                                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                                        ElseIf Convert.ToInt16(dtICD9.Rows(nICD9)("nICDRevision")) = 10 Then
                                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                                        End If
                                    End If
                                End With
                                nextICD = _Row
                                With C1Dignosis
                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                    .SetData(_Row, Col_nICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                    .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                    .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                End With
                                Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")
                                ''Sanjog Added on 20101030 to fetch the CPT with ICD Description
                                Dim strCurrentICD9Desc As String = dtICD9.Rows(nICD9)("sICD9Description")
                                ''Sanjog Added on 20101030 to fetch the CPT with ICD Description
                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                dtCPT = objDiagnosisDBLayer.FetchICD9CPTMod(ExamID, VisitId, strCurrentICD9, "", "", 3, strCurrentICD9Desc)
                                dtCPT.Columns.Remove("nLineNo")
                                objDiagnosisDBLayer = Nothing
                                If Not IsNothing(dtCPT) Then
                                    dvCPT = New DataView(dtCPT)
                                    If Not IsNothing(dtCPT) Then
                                        dtCPT = Nothing
                                    End If
                                    dtCPT = New DataTable
                                    dtCPT = New DataTable
                                    Dim strCPT(dtCPT.Columns.Count - 1) As String
                                    For i As Integer = 0 To dtCPT.Columns.Count - 1
                                        strCPT.SetValue(dtCPT.Columns(i).ColumnName, i)
                                    Next
                                    dtCPT = dvCPT.ToTable(True, strCPT)
                                    With dtCPT
                                        If IsNothing(dtCPT) = False Then
                                            For nCPT = 0 To .Rows.Count - 1
                                                Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                                If strCurrentCPT.Trim <> "" Then
                                                    C1Dignosis.Rows.Add()
                                                    _Row = C1Dignosis.Rows.Count - 1
                                                    'set the properties for newly added row
                                                    With C1Dignosis.Rows(_Row)
                                                        .AllowEditing = True
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 1
                                                        .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & " -" & dtCPT.Rows(nCPT)("sCPTDescription")
                                                        .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                                    End With

                                                    With C1Dignosis
                                                        .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                        .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                        .SetData(_Row, Col_nICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                        .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                        .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                        .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                                        .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                                        '.SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))

                                                        If Not String.IsNullOrEmpty(dtCPT.Rows(nCPT)("nTimedTherapy")) Then
                                                            .SetData(_Row, Col_Timed_PT, CType(dtCPT.Rows(nCPT)("nTimedTherapy"), Integer))
                                                        End If

                                                        If Not String.IsNullOrEmpty(dtCPT.Rows(nCPT)("nUnTimedTherapy")) Then
                                                            .SetData(_Row, Col_UnTimed_PT, CType(dtCPT.Rows(nCPT)("nUnTimedTherapy"), Integer))
                                                        End If

                                                        .SetData(_Row, Col_Units, FormatNumber(Convert.ToDecimal(dtCPT.Rows(nCPT)("nUnit"))))
                                                        .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                        .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                        .SetData(_Row, Col_ReasonConceptId, Convert.ToString(dtCPT.Rows(nCPT)("sReasonConceptID")))
                                                        .SetData(_Row, Col_ReasonConceptDescription, Convert.ToString(dtCPT.Rows(nCPT)("sReasonConceptDesc")))
                                                    End With


                                                End If

                                                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                                                dtMOD = objDiagnosisDBLayer.FetchICD9CPTMod(ExamID, VisitId, strCurrentICD9, strCurrentCPT, "", 2)
                                                objDiagnosisDBLayer = Nothing

                                                With dtMOD
                                                    If IsNothing(dtMOD) = False Then
                                                        For nMOD = 0 To .Rows.Count - 1

                                                            Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

                                                            If strCurrentMod.Trim <> "" Then
                                                                C1Dignosis.Rows.Add()
                                                                _Row = C1Dignosis.Rows.Count - 1
                                                                'set the properties for newly added row
                                                                With C1Dignosis.Rows(_Row)
                                                                    .AllowEditing = False
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 2
                                                                    .Node.Data = dtMOD.Rows(nMOD)("sModCode") & " -" & dtMOD.Rows(nMOD)("sModDescription")
                                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                                                End With

                                                                With C1Dignosis
                                                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                    .SetData(_Row, Col_nICDRevision, dtICD9.Rows(nICD9)("nICDRevision"))
                                                                    .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                    .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                    .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                    .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                    .SetData(_Row, Col_SnomedCode, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedCode")))
                                                                    .SetData(_Row, Col_SnomedDesc, Convert.ToString(dtICD9.Rows(nICD9)("sSnomedDesc")))
                                                                    .SetData(_Row, Col_ICD9Count, nICD9 + 1)
                                                                    .SetData(_Row, Col_CPTCount, nCPT + 1)
                                                                    .SetData(_Row, Col_ModCount, nMOD + 1)
                                                                End With
                                                            End If
                                                        Next
                                                    End If
                                                End With '' With dtMOD
                                            Next '' For nCPT = 0 To .Rows.Count - 1
                                        End If
                                    End With '' With dtCPT
                                End If
                            End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                        Next ''For nICD9 = 0 To .Rows.Count - 1
                    End If  '' If IsNothing(dtICD9) = False Then
                End With '' With dtICD9
            Else
                If _IsFromExam Then
                    If _blnICDTransition = True Then
                        RbICD10.Checked = True
                    Else
                        RbICD9.Checked = True
                    End If
                Else
                    If gblnIcd10Transition = True Then
                        RbICD10.Checked = True
                    Else
                        RbICD9.Checked = True
                    End If
                End If
            End If


            dtICD9 = Nothing
            dtCPT = Nothing
            dtMOD = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnCPT_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseEnter
        'If pnl_btnCPT.Dock <> DockStyle.Top Then
        btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        'End If
    End Sub

    Private Sub btnmodifier_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodifier.MouseEnter
        'If pnl_btnmodifier.Dock <> DockStyle.Top Then
        btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnmodifier.BackgroundImageLayout = ImageLayout.Stretch
        'End If
    End Sub

    Private Sub btnCPT_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCPT.MouseLeave
        If pnl_btnCPT.Dock = DockStyle.Top Then
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnCPT.BackgroundImageLayout = ImageLayout.Stretch
        End If


    End Sub

    Private Sub btnmodifier_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodifier.MouseLeave

        If pnl_btnmodifier.Dock = DockStyle.Top Then
            btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnmodifier.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnmodifier.BackgroundImageLayout = ImageLayout.Stretch
        End If


    End Sub

    Private Sub tlsbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnNew.Click
        C1Dignosis.Rows.Count = 1
    End Sub

    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click
        ''Commented by Mayuri:20100212-Case No:GLO2010-0004374
        ''if user removes all diagnosis and saves data then system should save and close form
        'If C1Dignosis.Rows.Count - 1 = 0 Then
        '    Exit Sub
        'End If
        C1Dignosis.Select()
        If (ValidateDiagnosis() = False) Then
            MessageBox.Show("Units should be less than 999.9999", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If saveDiagnosis() = True Then
            Me.DialogResult = Windows.Forms.DialogResult.Yes
            Me.Focus()
            Me.ActiveControl = tstripDiagnosis
        End If

    End Sub

    Private Sub tlsbtnFinish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnFinish.Click
        If C1Dignosis.Rows.Count - 1 = 0 Then
            Exit Sub
        End If
        If saveDiagnosis() = True Then
            Me.Close()
        End If

    End Sub

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        frmPatientExam.blnChangesMade = False ''added for unnecessary refreshing treatment liquid link issue 80994
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.No
        'Dim objmig As New frmMigrateDignosis
        'objmig.ShowDialog(Me)
        'Me.Show()

    End Sub

    Private Sub trvCPT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPT.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trvCPT.GetNodeAt(e.X, e.Y)
                trvCPT.SelectedNode = trvNode
                If IsNothing(trvNode) = True Then
                    'Try
                    '    If (IsNothing(trvCPT.ContextMenuStrip) = False) Then
                    '        trvCPT.ContextMenuStrip.Dispose()
                    '        trvCPT.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCPT.ContextMenuStrip = Nothing
                    Exit Sub
                End If
                If trvCPT.Nodes.Item(0).Text = trvCPT.SelectedNode.Text Or (CType(trvCPT.SelectedNode, myTreeNode).Key = -1) Then
                    'Try
                    '    If (IsNothing(trvCPT.ContextMenuStrip) = False) Then
                    '        trvCPT.ContextMenuStrip.Dispose()
                    '        trvCPT.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvCPT.ContextMenuStrip = Nothing
                    Exit Sub
                End If

                If _ModifierClick = True Then
                    If IsNothing(trvNode) = False Then

                        ContextMenuDiagnosis.Items(0).Visible = False
                        ContextMenuDiagnosis.Items(1).Visible = False
                        ContextMenuDiagnosis.Items(2).Visible = False
                        ContextMenuDiagnosis.Items(3).Visible = True
                        ContextMenuDiagnosis.Items(4).Visible = False
                        ContextMenuDiagnosis.Items(5).Visible = False
                        ContextMenuDiagnosis.Items(6).Visible = True
                        ContextMenuDiagnosis.Items(7).Visible = False
                        ContextMenuDiagnosis.Items(8).Visible = False
                        ContextMenuDiagnosis.Items(9).Visible = False
                        'Try
                        '    If (IsNothing(trvCPT.ContextMenuStrip) = False) Then
                        '        trvCPT.ContextMenuStrip.Dispose()
                        '        trvCPT.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPT.ContextMenuStrip = ContextMenuDiagnosis
                    End If
                Else
                    If IsNothing(trvNode) = False Then
                        trvCPT.SelectedNode = trvNode
                        ContextMenuDiagnosis.Items(0).Visible = False
                        ContextMenuDiagnosis.Items(1).Visible = False
                        ContextMenuDiagnosis.Items(2).Visible = True
                        ContextMenuDiagnosis.Items(3).Visible = False
                        ContextMenuDiagnosis.Items(4).Visible = False
                        ContextMenuDiagnosis.Items(5).Visible = True
                        ContextMenuDiagnosis.Items(6).Visible = False
                        ContextMenuDiagnosis.Items(7).Visible = False
                        ContextMenuDiagnosis.Items(8).Visible = False
                        ContextMenuDiagnosis.Items(9).Visible = False
                        'Try
                        '    If (IsNothing(trvCPT.ContextMenuStrip) = False) Then
                        '        trvCPT.ContextMenuStrip.Dispose()
                        '        trvCPT.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPT.ContextMenuStrip = ContextMenuDiagnosis
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub mnuAddICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles mnuAddICD9.Click
        'If Not trvSpeciality.SelectedNode Is trvSpeciality.Nodes.Item(0) Then
        Dim objfrmMSTICD9 As New frmMSTICD9("") 'SpecialityName)
        'Dim objclsICD9 As New clsICD9
        Try
            Me.TopMost = False
            'blnModify = False
            objfrmMSTICD9.Text = "Add New ICD9"
            objfrmMSTICD9.ShowDialog(IIf(IsNothing(objfrmMSTICD9.Parent), Me, objfrmMSTICD9.Parent))
            trICD9.Nodes.Clear()
            trICD9.BeginUpdate()
            'set the properties of treeview to show ICD9
            Dim rootNode As myTreeNode
            rootNode = New myTreeNode("ICD9", -1)
            rootNode.ImageIndex = 0
            rootNode.SelectedImageIndex = 0

            trICD9.Nodes.Add(rootNode)

            Call FillICD9ICD10()


            trICD9.EndUpdate()

            '' To Reset Search
            Call RefreshSearch()
            objfrmMSTICD9.Dispose()
            objfrmMSTICD9 = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub mnuAddCPT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) ' Handles mnuAddCPT.MouseDown
        Dim objfrmCPT As New frmMSTCPT() '(CategoryID, CategoryType)
        Try
            Me.TopMost = False
            'blnModify = False
            objfrmCPT.Text = "Add New CPT"
            objfrmCPT.ShowDialog(IIf(IsNothing(objfrmCPT.Parent), Me, objfrmCPT.Parent))
            If objfrmCPT.CancelClick = False Then
                trvCPT.Nodes.Clear()
                txtsearchrht.Text = ""
                Call FillCPT()
                trvCPT.GetNodeAt(0, 0)
            End If
            objfrmCPT.Dispose()
            objfrmCPT = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub mnuAddModifier_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) ' Handles mnuAddModifier.MouseDown
        Dim objfrmMSTModifier As New frmMSTModifier
        Try
            Me.TopMost = False
            objfrmMSTModifier.Text = "Add New Modifier"
            objfrmMSTModifier.ShowDialog(IIf(IsNothing(objfrmMSTModifier.Parent), Me, objfrmMSTModifier.Parent))
            trvCPT.Nodes.Clear()
            Dim rootNode As myTreeNode
            rootNode = New myTreeNode("Modifiers", -1)
            rootNode.ImageIndex = 2
            rootNode.SelectedImageIndex = 2
            trvCPT.Nodes.Add(rootNode)
            rdocode.Checked = True
            'Show Modifier
            Call FillModifiers()
            txtsearchrht.Text = ""
            objfrmMSTModifier.Dispose()
            objfrmMSTModifier = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub mnuEditICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles mnuEditICD9.Click
        Dim ID As Long
        Dim objfrmMSTICD9 As frmMSTICD9

        Try

            Me.TopMost = False
            Dim mytreeNode As myTreeNode
            mytreeNode = CType(trICD9.SelectedNode, myTreeNode)
            ID = mytreeNode.Key

            objfrmMSTICD9 = New frmMSTICD9(ID)
            objfrmMSTICD9.Text = "Modify ICD9"
            objfrmMSTICD9.ShowDialog(IIf(IsNothing(objfrmMSTICD9.Parent), Me, objfrmMSTICD9.Parent))
            objfrmMSTICD9.Dispose()
            objfrmMSTICD9 = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Edit ICD9", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub mnuEditCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles mnuEditCPT.Click
        Dim ID As Long
        Dim objfrmCPT As frmMSTCPT

        Try
            Me.TopMost = False
            Dim mytreeNode As myTreeNode
            mytreeNode = CType(trvCPT.SelectedNode, myTreeNode)
            ID = mytreeNode.Key
            objfrmCPT = New frmMSTCPT(ID)
            objfrmCPT.Text = "Modify CPT"
            objfrmCPT.ShowDialog(IIf(IsNothing(objfrmCPT.Parent), Me, objfrmCPT.Parent))
            objfrmCPT.Dispose()
            objfrmCPT = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Modify CPT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        End Try


    End Sub

    Private Sub mnuEditModifier_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles mnuEditModifier.Click
        Dim ID As Long
        Dim objfrmMSTModifier As frmMSTModifier
        Try
            Me.TopMost = False
            Dim mytreenode As myTreeNode
            mytreenode = CType(trvCPT.SelectedNode, myTreeNode)
            ID = mytreenode.Key


            objfrmMSTModifier = New frmMSTModifier(ID)
            objfrmMSTModifier.Text = "Modify Modifier"
            objfrmMSTModifier.ShowDialog(IIf(IsNothing(objfrmMSTModifier.Parent), Me, objfrmMSTModifier.Parent))
            objfrmMSTModifier.Dispose()
            objfrmMSTModifier = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Edit Modifier", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub trvCPT_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPT.MouseClick
        'If trvCPT.Nodes.Item(0).Text = trvCPT.SelectedNode.Text Or (CType(trvCPT.SelectedNode, myTreeNode).Key = -1) Then ' Or trvCPT.SelectedNode.PrevNode Is trvCPT.Parent Then
        '    trvCPT.ContextMenuStrip = Nothing
        '    Exit Sub
        'End If
    End Sub

    'Private Sub rbSearch1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If rbSearch1.Checked = True Then
    '        rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '        rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '    Else
    '        rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '        rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    End If

    'End Sub

    'Private Sub rbSearch2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If rbSearch2.Checked = True Then
    '        rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '        rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Regular)

    '    Else
    '        rbSearch2.Font = New Font("Tahoma", 9, FontStyle.Regular)
    '        rbSearch1.Font = New Font("Tahoma", 9, FontStyle.Bold)
    '    End If


    'End Sub

    Private Sub rdocode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdocode.CheckedChanged

        If rdocode.Checked = True Then
            rdocode.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rdoDescriptionrht.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        Else
            rdocode.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rdoDescriptionrht.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        End If
    End Sub

    Private Sub rdoDescriptionrht_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDescriptionrht.CheckedChanged

        If rdoDescriptionrht.Checked = True Then
            rdoDescriptionrht.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            rdocode.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        Else
            rdoDescriptionrht.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            rdocode.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        End If
    End Sub

    Private Sub btnmodifier_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmodifier.MouseHover

        'pnl_btnmodifier.Dock = DockStyle.Top
        'btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        'btnmodifier.BackgroundImageLayout = ImageLayout.Stretch

        'btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange        pnl_btnmodifier.Tag = "Selected"

        'btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        'btnCPT.Tag = "UnSelected"

        ''btnmodifier.SendToBack()
        ''btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_ButtonHover
        ''btnmodifier.BackgroundImageLayout = ImageLayout.Stretch

        'btnCPT.Dock = DockStyle.Bottom
        'btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        'btnCPT.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnCPT_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.MouseHover

        'pnl_btnCPT.Dock = DockStyle.Top
        'btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        'btnCPT.BackgroundImageLayout = ImageLayout.Stretch

        'btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        'btnCPT.Tag = "Selected"

        'btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        'btnmodifier.Tag = "UnSelected"

        'pnl_btnmodifier.Dock = DockStyle.Bottom
        'btnmodifier.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        'btnmodifier.BackgroundImageLayout = ImageLayout.Stretch

    End Sub

   

    Private Sub C1Dignosis_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Dignosis.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        'C1Dignosis.ShowCellLabels = True
    End Sub

    Private Sub mnusetasPrimary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetAsPrimary.Click
        C1Dignosis.Rows(C1Dignosis.Row).Node.Move(C1.Win.C1FlexGrid.NodeMoveEnum.First, C1Dignosis.Rows(0).Node)
        Dim ICD9Count As Integer = 0
        For i As Integer = 1 To C1Dignosis.Rows.Count - 1
            If IsNothing(C1Dignosis.GetData(i, Col_CPTCount)) Then
                ICD9Count = ICD9Count + 1
                C1Dignosis.SetData(i, Col_ICD9Count, ICD9Count)
            Else
                C1Dignosis.SetData(i, Col_ICD9Count, ICD9Count)
            End If
        Next
    End Sub



    Private Sub mnuAssociateCPTWithAllICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssociateCPTWithAllICD9.Click

        With C1Dignosis

            Dim strICD9code As String
            Dim strICD9description As String = ""
            Dim strCPTCode As String
            Dim strCPTDescription As String = ""
            Dim strModcode As String
            Dim strModdescription As String = ""
            Dim ICD9() As String
            Dim CPT() As String
            'Dim _Mod() As String
            Dim oCPTNode As C1.Win.C1FlexGrid.Node
            Dim IsAssociateMod As Boolean = False
            oCPTNode = .Rows(.Row).Node

            Dim CPTRange As C1.Win.C1FlexGrid.CellRange
            Dim arrlist As New ArrayList
            Dim mylist As myList
            Dim arrmod() As String

            If oCPTNode.Children > 0 Then
                If MessageBox.Show("Do you want to associate Modifier?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    IsAssociateMod = True

                    If oCPTNode.Children > 0 Then
                        CPTRange = oCPTNode.GetCellRange()
                        arrlist.Clear()
                        arrlist = Nothing
                        arrlist = New ArrayList
                        For cnt As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                            mylist = New myList
                            mylist.Code = oCPTNode.Data '''' Parent Node i.e. cpt
                            mylist.Description = .Rows(cnt).Node.Data ''''child Node i.e. modifier
                            arrlist.Add(mylist)
                        Next
                    End If

                End If
            End If

            For i As Integer = .Rows.Count - 1 To 1 Step -1
                Dim ofNode As C1.Win.C1FlexGrid.Node
                ofNode = .Rows(i).Node

                If ofNode.Level = 0 Then
                    Dim IsFound As Boolean = False
                    If ofNode.Children > 0 Then '''' Check CPT alredy exits
                        For j As Integer = ofNode.Row.Index To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                            If .GetData(j, Col_ICD9Code_Description) = .GetData(.Row, Col_ICD9Code_Description) Then
                                IsFound = True
                                Exit For
                            End If
                        Next
                    End If
                    If IsFound = False Then
                        Dim NewRow As C1.Win.C1FlexGrid.Row

                        Dim oModNode As C1.Win.C1FlexGrid.Node

                        If ofNode.Children > 0 Then '''' Get the last child index to add node
                            oModNode = .Rows(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index).Node
                            If oModNode.Children > 0 Then
                                NewRow = .Rows.Insert(oModNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1)
                            Else
                                NewRow = .Rows.Insert(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1)
                            End If

                        Else
                            'oModNode = .Rows(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index).Node
                            'If oModNode.Children > 0 Then
                            '    NewRow = .Rows.Insert(oModNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1)
                            'Else
                            NewRow = .Rows.Insert(ofNode.Row.Index + 1)
                            'End If
                        End If

                        With .Rows(NewRow.Index)
                            .AllowEditing = True
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                            .Node.Data = C1Dignosis.GetData(C1Dignosis.Row, Col_ICD9Code_Description)
                        End With

                        '.SetData(NewRow.Index, Col_ICD9Code_Description, .GetData(.Row, Col_ICD9Code_Description))
                        ICD9 = Split(ofNode.Data, "-", 2)
                        strICD9code = ICD9.GetValue(0)
                        If (ICD9.Length > 1) Then
                            strICD9description = ICD9.GetValue(1)
                        End If


                        CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                        strCPTCode = CPT.GetValue(0)
                        If (CPT.Length > 1) Then
                            strCPTDescription = CPT.GetValue(1)
                        End If


                        .SetData(NewRow.Index, Col_Units, .GetData(.Row, Col_Units))
                        .SetData(NewRow.Index, Col_ICD9Count, .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
                        .SetData(NewRow.Index, Col_CPTCount, CType(.GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
                        .SetData(NewRow.Index, Col_ICD9Code, strICD9code)
                        .SetData(NewRow.Index, Col_ICD9Desc, strICD9description)
                        .SetData(NewRow.Index, COl_CPTCode, strCPTCode)
                        .SetData(NewRow.Index, Col_CPTDesc, strCPTDescription)
                        .SetData(NewRow.Index, Col_nICDRevision, .GetData(.Row, Col_nICDRevision))

                        If IsAssociateMod = True Then '''' If user select to associate modifer

                            Dim modRowCout As Integer = oCPTNode.Children
                            Dim NewmodRow As C1.Win.C1FlexGrid.Row

                            For l As Integer = arrlist.Count - 1 To 0 Step -1
                                arrmod = Split(CType(arrlist.Item(l), myList).Description, "-", 2)
                                NewmodRow = .Rows.Insert(NewRow.Index + 1)
                                With .Rows(NewmodRow.Index)
                                    .AllowEditing = True
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 2
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                    .Node.Data = CType(arrlist.Item(l), myList).Description
                                End With

                                '.SetData(NewRow.Index, Col_ICD9Code_Description, .GetData(.Row, Col_ICD9Code_Description))
                                ICD9 = Split(ofNode.Data, "-", 2)
                                strICD9code = ICD9.GetValue(0)
                                If (ICD9.Length > 1) Then '
                                    strICD9description = ICD9.GetValue(1)
                                End If


                                CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                                strCPTCode = CPT.GetValue(0)
                                If (CPT.Length > 1) Then
                                    strCPTDescription = CPT.GetValue(1)

                                End If

                                '_Mod = oCPTNode.
                                '_Mod = Split(.GetData(l, Col_ModDesc), "-", 2)
                                strModcode = arrmod.GetValue(0)
                                If (arrmod.Length > 1) Then
                                    strModdescription = arrmod.GetValue(1)
                                End If


                                '.SetData(NewmodRow.Index, Col_Units, .GetData(.Row, Col_Units))
                                .SetData(NewmodRow.Index, Col_ICD9Count, .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
                                .SetData(NewmodRow.Index, Col_CPTCount, CType(.GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
                                .SetData(NewmodRow.Index, Col_ModCount, modRowCout)

                                .SetData(NewmodRow.Index, Col_ICD9Code, strICD9code)
                                .SetData(NewmodRow.Index, Col_ICD9Desc, strICD9description)
                                .SetData(NewmodRow.Index, COl_CPTCode, strCPTCode)
                                .SetData(NewmodRow.Index, Col_CPTDesc, strCPTDescription)
                                .SetData(NewmodRow.Index, Col_ModCode, strModcode)
                                .SetData(NewmodRow.Index, Col_ModDesc, strModdescription)
                                .SetData(NewmodRow.Index, Col_nICDRevision, .GetData(.Row, Col_nICDRevision))

                            Next

                            'Dim modRowCout As Integer = oCPTNode.Children
                            'Dim NewmodRow As C1.Win.C1FlexGrid.Row
                            'Dim _cellRng As C1.Win.C1FlexGrid.CellRange = oCPTNode.GetCellRange()
                            'Dim modcount As Integer = oCPTNode.Children + 1
                            'For cntmod As Integer = _cellRng.BottomRow To _cellRng.TopRow + 1 Step -1
                            '    modcount = modcount - 1
                            '    NewmodRow = .Rows.Insert(NewRow.Index + 1)
                            '    With .Rows(NewmodRow.Index)
                            '        .AllowEditing = True
                            '        .ImageAndText = True
                            '        .Height = 24
                            '        .IsNode = True
                            '        .Node.Level = 2
                            '        .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                            '        .Node.Data = C1Dignosis.GetData(cntmod, Col_ICD9Code_Description)
                            '    End With
                            '    '.SetData(NewRow.Index, Col_ICD9Code_Description, .GetData(.Row, Col_ICD9Code_Description))
                            '    ICD9 = Split(ofNode.Data, "-", 2)
                            '    strICD9code = ICD9.GetValue(0)
                            '    strICD9description = ICD9.GetValue(1)

                            '    CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                            '    strCPTCode = CPT.GetValue(0)
                            '    strCPTDescription = CPT.GetValue(1)

                            '    '_Mod = oCPTNode.
                            '    _Mod = Split(.GetData(cntmod, Col_ICD9Code_Description), "-", 2)
                            '    strModcode = _Mod.GetValue(0)
                            '    strModdescription = _Mod.GetValue(1)

                            '    '.SetData(NewmodRow.Index, Col_Units, .GetData(.Row, Col_Units))
                            '    .SetData(NewmodRow.Index, Col_ICD9Count, .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
                            '    .SetData(NewmodRow.Index, Col_CPTCount, CType(.GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
                            '    .SetData(NewmodRow.Index, Col_ModCount, modcount)

                            '    .SetData(NewmodRow.Index, Col_ICD9Code, strICD9code)
                            '    .SetData(NewmodRow.Index, Col_ICD9Desc, strICD9description)
                            '    .SetData(NewmodRow.Index, COl_CPTCode, strCPTCode)
                            '    .SetData(NewmodRow.Index, Col_CPTDesc, strCPTDescription)
                            '    .SetData(NewmodRow.Index, Col_ModCode, strModcode)
                            '    .SetData(NewmodRow.Index, Col_ModDesc, strModdescription)

                            'Next
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub mnuAssociateCPTWithAllUnassociatedICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssociateCPTWithAllUnassociatedICD9.Click

        Try


            With C1Dignosis

                Dim strICD9code As String
                Dim strICD9description As String = ""
                Dim strCPTCode As String
                Dim strCPTDescription As String = ""
                Dim strModcode As String
                Dim strModdescription As String = ""
                Dim ICD9() As String
                Dim CPT() As String
                'Dim _Mod() As String
                Dim oCPTNode As C1.Win.C1FlexGrid.Node
                Dim IsAssociateMod As Boolean = False
                oCPTNode = .Rows(.Row).Node

                Dim CPTRange As C1.Win.C1FlexGrid.CellRange
                Dim arrlist As New ArrayList
                Dim mylist As myList
                Dim arrmod() As String

                If oCPTNode.Children > 0 Then
                    If MessageBox.Show("Do you want to associate Modifier?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        IsAssociateMod = True

                        If oCPTNode.Children > 0 Then
                            CPTRange = oCPTNode.GetCellRange()
                            arrlist.Clear()
                            arrlist = Nothing
                            arrlist = New ArrayList
                            For cnt As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                                mylist = New myList
                                mylist.Code = oCPTNode.Data '''' Parent Node i.e. cpt
                                mylist.Description = .Rows(cnt).Node.Data ''''child Node i.e. modifier
                                arrlist.Add(mylist)
                            Next
                        End If
                    End If
                End If

                For i As Integer = .Rows.Count - 1 To 1 Step -1
                    Dim ofNode As C1.Win.C1FlexGrid.Node
                    ofNode = .Rows(i).Node
                    If ofNode.Level = 0 Then
                        If ofNode.Children = 0 Then '''' Check Already CPT is present if not the add CPT
                            Dim NewRow As C1.Win.C1FlexGrid.Row = .Rows.Insert(ofNode.Row.Index + 1)
                            With .Rows(NewRow.Index)
                                .AllowEditing = True
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 1
                                .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                .Node.Data = C1Dignosis.GetData(C1Dignosis.Row, Col_ICD9Code_Description)
                            End With

                            ICD9 = Split(ofNode.Data, "-", 2)
                            strICD9code = ICD9.GetValue(0)
                            If (ICD9.Length > 1) Then
                                strICD9description = ICD9.GetValue(1)
                            End If


                            CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                            strCPTCode = CPT.GetValue(0)
                            If (CPT.Length > 1) Then
                                strCPTDescription = CPT.GetValue(1)
                            End If


                            .SetData(NewRow.Index, Col_Units, .GetData(.Row, Col_Units))
                            .SetData(NewRow.Index, Col_ICD9Count, .GetData(ofNode.Row.Index, Col_ICD9Count))
                            .SetData(NewRow.Index, Col_CPTCount, 1)
                            .SetData(NewRow.Index, Col_ICD9Code, strICD9code)
                            .SetData(NewRow.Index, Col_ICD9Desc, strICD9description)
                            .SetData(NewRow.Index, COl_CPTCode, strCPTCode)
                            .SetData(NewRow.Index, Col_CPTDesc, strCPTDescription)
                            .SetData(NewRow.Index, Col_nICDRevision, .GetData(.Row, Col_nICDRevision))
                            If IsAssociateMod = True Then

                                Dim modRowCout As Integer = oCPTNode.Children
                                Dim NewmodRow As C1.Win.C1FlexGrid.Row

                                For l As Integer = arrlist.Count - 1 To 0 Step -1
                                    arrmod = Split(CType(arrlist.Item(l), myList).Description, "-", 2)
                                    NewmodRow = .Rows.Insert(NewRow.Index + 1)
                                    With .Rows(NewmodRow.Index)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 2
                                        .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                        .Node.Data = CType(arrlist.Item(l), myList).Description
                                    End With

                                    '.SetData(NewRow.Index, Col_ICD9Code_Description, .GetData(.Row, Col_ICD9Code_Description))
                                    ICD9 = Split(ofNode.Data, "-", 2)
                                    strICD9code = ICD9.GetValue(0)
                                    If (ICD9.Length > 1) Then
                                        strICD9description = ICD9.GetValue(1)
                                    End If


                                    CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                                    strCPTCode = CPT.GetValue(0)
                                    If (CPT.Length > 1) Then
                                        strCPTDescription = CPT.GetValue(1)
                                    End If


                                    '_Mod = oCPTNode.
                                    '_Mod = Split(.GetData(l, Col_ModDesc), "-", 2)
                                    strModcode = arrmod.GetValue(0)
                                    If (arrmod.Length > 1) Then
                                        strModdescription = arrmod.GetValue(1)
                                    End If


                                    '.SetData(NewmodRow.Index, Col_Units, .GetData(.Row, Col_Units))
                                    .SetData(NewmodRow.Index, Col_ICD9Count, .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
                                    .SetData(NewmodRow.Index, Col_CPTCount, CType(.GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
                                    .SetData(NewmodRow.Index, Col_ModCount, modRowCout)

                                    .SetData(NewmodRow.Index, Col_ICD9Code, strICD9code)
                                    .SetData(NewmodRow.Index, Col_ICD9Desc, strICD9description)
                                    .SetData(NewmodRow.Index, COl_CPTCode, strCPTCode)
                                    .SetData(NewmodRow.Index, Col_CPTDesc, strCPTDescription)
                                    .SetData(NewmodRow.Index, Col_ModCode, strModcode)
                                    .SetData(NewmodRow.Index, Col_ModDesc, strModdescription)
                                    .SetData(NewRow.Index, Col_nICDRevision, .GetData(.Row, Col_nICDRevision))

                                Next

                                'Dim modRowCout As Integer = oCPTNode.Children
                                'Dim NewmodRow As C1.Win.C1FlexGrid.Row
                                'Dim _cellRng As C1.Win.C1FlexGrid.CellRange = oCPTNode.GetCellRange()
                                'Dim modcount As Integer = oCPTNode.Children + 1
                                'For cntmod As Integer = _cellRng.BottomRow To _cellRng.TopRow + 1 Step -1
                                '    modcount = modcount - 1
                                '    NewmodRow = .Rows.Insert(NewRow.Index + 1)
                                '    With .Rows(NewmodRow.Index)
                                '        .AllowEditing = True
                                '        .ImageAndText = True
                                '        .Height = 24
                                '        .IsNode = True
                                '        .Node.Level = 2
                                '        .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                '        .Node.Data = C1Dignosis.GetData(cntmod, Col_ICD9Code_Description)
                                '    End With
                                '    '.SetData(NewRow.Index, Col_ICD9Code_Description, .GetData(.Row, Col_ICD9Code_Description))
                                '    ICD9 = Split(ofNode.Data, "-", 2)
                                '    strICD9code = ICD9.GetValue(0)
                                '    strICD9description = ICD9.GetValue(1)

                                '    CPT = Split(.GetData(.Row, Col_ICD9Code_Description), "-", 2)
                                '    strCPTCode = CPT.GetValue(0)
                                '    strCPTDescription = CPT.GetValue(1)

                                '    '_Mod = oCPTNode.
                                '    _Mod = Split(.GetData(cntmod, Col_ICD9Code_Description), "-", 2)
                                '    strModcode = _Mod.GetValue(0)
                                '    strModdescription = _Mod.GetValue(1)


                                '    .SetData(NewmodRow.Index, Col_ICD9Count, .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
                                '    .SetData(NewmodRow.Index, Col_CPTCount, CType(.GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
                                '    .SetData(NewmodRow.Index, Col_ModCount, modcount)

                                '    .SetData(NewmodRow.Index, Col_ICD9Code, strICD9code)
                                '    .SetData(NewmodRow.Index, Col_ICD9Desc, strICD9description)

                                '    .SetData(NewmodRow.Index, COl_CPTCode, strCPTCode)
                                '    .SetData(NewmodRow.Index, Col_CPTDesc, strCPTDescription)

                                '    .SetData(NewmodRow.Index, Col_ModCode, strModcode)
                                '    .SetData(NewmodRow.Index, Col_ModDesc, strModdescription)

                                'Next
                            End If


                        End If
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GloUC_trvICD_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvICD.MouseDown
        Try
            If GloUC_trvICD.SelectedNode IsNot Nothing Then
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    ContextMenuDiagnosis.Items(0).Visible = False
                    ContextMenuDiagnosis.Items(1).Visible = False
                    If RbICD9.Checked = True Then
                        ContextMenuDiagnosis.Items(2).Visible = True
                        ContextMenuDiagnosis.Items(5).Visible = True
                        ContextMenuDiagnosis.Items(15).Visible = False
                        ContextMenuDiagnosis.Items(16).Visible = False
                    Else
                        ContextMenuDiagnosis.Items(2).Visible = False
                        ContextMenuDiagnosis.Items(5).Visible = False
                        ContextMenuDiagnosis.Items(15).Visible = True
                        ContextMenuDiagnosis.Items(16).Visible = True
                    End If
                    ContextMenuDiagnosis.Items(3).Visible = False
                    ContextMenuDiagnosis.Items(4).Visible = False

                    ContextMenuDiagnosis.Items(6).Visible = False
                    ContextMenuDiagnosis.Items(7).Visible = False
                    ContextMenuDiagnosis.Items(8).Visible = False
                    ContextMenuDiagnosis.Items(9).Visible = False
                    ContextMenuDiagnosis.Items(10).Visible = False
                    ContextMenuDiagnosis.Items(11).Visible = False
                    ContextMenuDiagnosis.Items(12).Visible = False
                    ContextMenuDiagnosis.Items(13).Visible = False
                    ContextMenuDiagnosis.Items(14).Visible = False
                    'Try
                    '    If (IsNothing(GloUC_trvICD.ContextMenuStrip) = False) Then
                    '        GloUC_trvICD.ContextMenuStrip.Dispose()
                    '        GloUC_trvICD.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    GloUC_trvICD.ContextMenuStrip = Nothing
                    GloUC_trvICD.DisplayContextMenuStrip = ContextMenuDiagnosis
                    ChangeZOrder()
                End If
            Else
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    ContextMenuDiagnosis.Items(0).Visible = False
                    ContextMenuDiagnosis.Items(1).Visible = False
                    If RbICD9.Checked = True Then
                        ContextMenuDiagnosis.Items(2).Visible = True
                        ContextMenuDiagnosis.Items(15).Visible = False
                    Else
                        ContextMenuDiagnosis.Items(2).Visible = False
                        ContextMenuDiagnosis.Items(15).Visible = True
                    End If
                    ContextMenuDiagnosis.Items(3).Visible = False
                    ContextMenuDiagnosis.Items(4).Visible = False
                    ContextMenuDiagnosis.Items(5).Visible = False
                    ContextMenuDiagnosis.Items(6).Visible = False
                    ContextMenuDiagnosis.Items(7).Visible = False
                    ContextMenuDiagnosis.Items(8).Visible = False
                    ContextMenuDiagnosis.Items(9).Visible = False
                    ContextMenuDiagnosis.Items(10).Visible = False
                    ContextMenuDiagnosis.Items(11).Visible = False
                    ContextMenuDiagnosis.Items(12).Visible = False
                    ContextMenuDiagnosis.Items(13).Visible = False
                    ContextMenuDiagnosis.Items(14).Visible = False
                    'Try
                    '    If (IsNothing(GloUC_trvICD.ContextMenuStrip) = False) Then
                    '        GloUC_trvICD.ContextMenuStrip.Dispose()
                    '        GloUC_trvICD.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    GloUC_trvICD.ContextMenuStrip = Nothing
                    GloUC_trvICD.DisplayContextMenuStrip = ContextMenuDiagnosis
                    ChangeZOrder()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub GloUC_trvICD_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvICD.NodeMouseDoubleClick

        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
        Dim blnOneSnoMed As Boolean
        Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode)
        'Dim oNodeToAdd As New myTreeNode
        'oNodeToAdd.Key = oNode.ID
        'oNodeToAdd.Text = oNode.Text
        Dim i As Integer
        Dim clsDiagnosis As New ClsDiagnosisDBLayer

        Dim nMaxICD9Count As Integer
        With C1Dignosis
            Dim _Row As Integer = 0
            'selected row in flexgrid 
            Dim strDesription As String = GloUC_trvICD.SelectedNode.Text

            ' if the ICD9 item is alreay present then exit
            For i = 0 To .Rows.Count - 1
                ''To avoid duplicate diagnosis add in DxCPT
                If C1Dignosis.GetData(i, Col_ICD9Code_Description).ToString.Replace(" -", "-") = strDesription.Replace(" -", "-") Then
                    '' TO Insert the New Item At the END of the CAtegory
                    MessageBox.Show("Duplicate ICD9/10 is not allowed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '' SUDHIR 20091014 '' BUG 3743 ''
                    Exit Sub
                End If
                If i = .Rows.Count - 1 Then
                    If i <> 0 Then
                        If Not IsNothing(.GetData(i, Col_ICD9Count)) Then
                            nMaxICD9Count = .GetData(i, Col_ICD9Count)
                        Else
                            nMaxICD9Count = 0
                        End If
                    Else
                        nMaxICD9Count = 0
                    End If
                End If
            Next

            'set the propery of treeview in the flexgrid
            '''''
            .Tree.Column = Col_ICD9Code_Description
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid

            .Tree.Indent = 15

            '''''

            ' add the data to treeview in flexgrid     
            ' if there is no data in flexgrid
            If .Rows.Count - 1 = 0 Then
                .Rows.Add()
                _Row = .Rows.Count - 1
                'set the properties for newly added row
                With .Rows(_Row)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 0
                    '.Node.Data = trICD9.SelectedNode.Text
                    .Node.Data = GloUC_trvICD.SelectedNode.Text
                    If oNode.ICDRevision = 10 Then
                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                    Else
                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                    End If

                    '.Node.Key = strSelectedICD9
                End With
                Dim strConctICD9 As String
                Dim arrstrConctICD9() As String
                Dim strICDCode As String
                Dim strICDDesc As String = ""
                ' strConctICD9 = trICD9.SelectedNode.Text
                strConctICD9 = GloUC_trvICD.SelectedNode.Text
                ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                ' is row its current row it is used for removing data from flexgrid
                arrstrConctICD9 = Split(strConctICD9, "-", 2)
                strICDCode = arrstrConctICD9.GetValue(0)
                If (arrstrConctICD9.Length > 1) Then
                    strICDDesc = arrstrConctICD9.GetValue(1)
                End If

                .SetData(_Row, Col_ICD9Code, strICDCode)
                .SetData(_Row, Col_ICD9Desc, strICDDesc)
                .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)
                .SetData(_Row, Col_nICDRevision, oNode.ICDRevision)
                Dim htSno As Hashtable

                '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                htSno = clsDiagnosis.GetDefaultSnomed(strICDCode, strICDDesc, oNode.ICDRevision, blnOneSnoMed, Me)

                If Not IsNothing(htSno) Then

                    Dim key As ICollection = htSno.Keys
                    Dim k As String

                    For Each k In key
                        .SetData(_Row, Col_SnomedCode, k)
                        .SetData(_Row, Col_SnomedDesc, htSno(k))
                    Next k

                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    .SetData(_Row, Col_nIsOneToOneMappingSnoMed, blnOneSnoMed)

                End If

                htSno = Nothing
                .Row = _Row
                _Row = _Row + 1
            Else
                .Rows.Add()
                _Row = .Rows.Count - 1
                With .Rows(.Rows.Count - 1)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 0
                    If oNode.ICDRevision = 10 Then
                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                    Else
                        .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                    End If
                    '.Node.Data = trICD9.SelectedNode.Text
                    .Node.Data = GloUC_trvICD.SelectedNode.Text
                End With

                ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                ' is row its current row it is used for removing data from flexgrid
                '.SetData(_Row, Col_sICD9Code, _Row)

                Dim strConctICD9 As String = ""
                Dim arrstrConctICD9() As String = Nothing
                Dim strICDCode As String = ""
                Dim strICDDesc As String = ""
                'strConctICD9 = trICD9.SelectedNode.Text
                strConctICD9 = GloUC_trvICD.SelectedNode.Text
                arrstrConctICD9 = Split(strConctICD9, "-", 2)
                strICDCode = arrstrConctICD9.GetValue(0)
                If (arrstrConctICD9.Length > 1) Then
                    strICDDesc = arrstrConctICD9.GetValue(1)
                End If

                .SetData(_Row, Col_ICD9Code, strICDCode)
                .SetData(_Row, Col_ICD9Desc, strICDDesc)
                .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)
                .SetData(_Row, Col_nICDRevision, oNode.ICDRevision)
                Dim htSno As Hashtable
                '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                htSno = clsDiagnosis.GetDefaultSnomed(strICDCode, strICDDesc, oNode.ICDRevision, blnOneSnoMed, Me)

                If Not IsNothing(htSno) Then
                    Dim key As ICollection = htSno.Keys
                    Dim k As String

                    For Each k In key
                        .SetData(_Row, Col_SnomedCode, k)
                        .SetData(_Row, Col_SnomedDesc, htSno(k))
                    Next k

                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    .SetData(_Row, Col_nIsOneToOneMappingSnoMed, blnOneSnoMed)

                End If
                htSno = Nothing
                ''Bug : 00000814: DxCPT. Set focus on newly added ICD
                .Row = _Row
                _Row = _Row + 1
            End If
        End With
        If Not IsNothing(clsDiagnosis) Then
            clsDiagnosis.Dispose()
            clsDiagnosis = Nothing
        End If
    End Sub

    Private Sub GloUC_trvICD_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvICD.KeyPress

        If e.KeyChar = ChrW(13) AndAlso GloUC_trvICD.SelectedNode IsNot Nothing Then
            Dim blnOneSnoMed As Boolean
            Dim clsDiagnosis As New ClsDiagnosisDBLayer
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode)
            Dim i As Integer
            Dim nMaxICD9Count As Integer

            With C1Dignosis
                Dim _Row As Integer = 0
                'selected row in flexgrid 
                Dim strDesription As String = GloUC_trvICD.SelectedNode.Text

                ' if the ICD9 item is alreay present then exit
                For i = 0 To .Rows.Count - 1
                    '21-Oct-15 Aniket: Resolving Bug #90443: gloEMR: DX-CPT- application allows user to add same diagnosis twice
                    If .GetData(i, Col_ICD9Code_Description).ToString.Replace(" ", "") = strDesription.ToString.Replace(" ", "") Then
                        '' TO Insert the New Item At the END of the CAtegory
                        Exit Sub
                    End If
                    If i = .Rows.Count - 1 Then
                        If i <> 0 Then
                            If Not IsNothing(.GetData(i, Col_ICD9Count)) Then
                                nMaxICD9Count = .GetData(i, Col_ICD9Count)
                            Else
                                nMaxICD9Count = 0
                            End If
                        Else
                            nMaxICD9Count = 0
                        End If
                    End If
                Next

                'set the propery of treeview in the flexgrid
                '''''
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 15

                '''''

                ' add the data to treeview in flexgrid     
                ' if there is no data in flexgrid
                If .Rows.Count - 1 = 0 Then
                    .Rows.Add()
                    _Row = .Rows.Count - 1
                    'set the properties for newly added row
                    With .Rows(_Row)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 0
                        '.Node.Data = trICD9.SelectedNode.Text
                        .Node.Data = GloUC_trvICD.SelectedNode.Text

                        If oNode.ICDRevision = 9 Then
                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                        Else
                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                        End If
                        '.Node.Key = strSelectedICD9
                    End With
                    Dim strConctICD9 As String
                    Dim arrstrConctICD9() As String
                    Dim strICDCode As String
                    Dim strICDDesc As String = ""
                    ' strConctICD9 = trICD9.SelectedNode.Text
                    strConctICD9 = GloUC_trvICD.SelectedNode.Text
                    ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                    ' is row its current row it is used for removing data from flexgrid
                    arrstrConctICD9 = Split(strConctICD9, "-", 2)
                    strICDCode = arrstrConctICD9.GetValue(0)
                    If (arrstrConctICD9.Length > 1) Then
                        strICDDesc = arrstrConctICD9.GetValue(1)
                    End If

                    .SetData(_Row, Col_ICD9Code, strICDCode)
                    .SetData(_Row, Col_ICD9Desc, strICDDesc)
                    .SetData(_Row, Col_nICDRevision, oNode.ICDRevision)
                    .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)


                    Dim htSno As Hashtable
                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    htSno = clsDiagnosis.GetDefaultSnomed(strICDCode, strICDDesc, oNode.ICDRevision, blnOneSnoMed, Me)

                    If Not IsNothing(htSno) Then

                        Dim key As ICollection = htSno.Keys
                        Dim k As String

                        For Each k In key
                            .SetData(_Row, Col_SnomedCode, k)
                            .SetData(_Row, Col_SnomedDesc, htSno(k))
                        Next k

                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        .SetData(_Row, Col_nIsOneToOneMappingSnoMed, blnOneSnoMed)

                    End If

                    htSno = Nothing


                    .Row = _Row
                    _Row = _Row + 1
                Else
                    .Rows.Add()
                    _Row = .Rows.Count - 1
                    With .Rows(.Rows.Count - 1)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 0
                        If oNode.ICDRevision = 9 Then
                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                        Else
                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD10GalleryGreen1
                        End If
                        '.Node.Data = trICD9.SelectedNode.Text
                        .Node.Data = GloUC_trvICD.SelectedNode.Text
                    End With

                    ' To identify the selected ICD9 added on column Col_sICD9CODE and it has value row no.which
                    ' is row its current row it is used for removing data from flexgrid
                    '.SetData(_Row, Col_sICD9Code, _Row)

                    Dim strConctICD9 As String
                    Dim arrstrConctICD9() As String
                    Dim strICDCode As String
                    Dim strICDDesc As String = ""
                    'strConctICD9 = trICD9.SelectedNode.Text
                    strConctICD9 = GloUC_trvICD.SelectedNode.Text
                    arrstrConctICD9 = Split(strConctICD9, "-", 2)
                    strICDCode = arrstrConctICD9.GetValue(0)
                    If (arrstrConctICD9.Length > 1) Then
                        strICDDesc = arrstrConctICD9.GetValue(1)
                    End If

                    .SetData(_Row, Col_ICD9Code, strICDCode)
                    .SetData(_Row, Col_ICD9Desc, strICDDesc)
                    .SetData(_Row, Col_nICDRevision, oNode.ICDRevision)
                    .SetData(_Row, Col_ICD9Count, nMaxICD9Count + 1)

                    Dim htSno As Hashtable
                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    htSno = clsDiagnosis.GetDefaultSnomed(strICDCode, strICDDesc, oNode.ICDRevision, blnOneSnoMed, Me)

                    If Not IsNothing(htSno) Then
                        Dim key As ICollection = htSno.Keys
                        Dim k As String

                        For Each k In key
                            .SetData(_Row, Col_SnomedCode, k)
                            .SetData(_Row, Col_SnomedDesc, htSno(k))
                        Next k

                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        .SetData(_Row, Col_nIsOneToOneMappingSnoMed, blnOneSnoMed)

                    End If
                    htSno = Nothing
                    .Row = _Row
                    _Row = _Row + 1
                End If
            End With
            If Not IsNothing(clsDiagnosis) Then
                clsDiagnosis.Dispose()
                clsDiagnosis = Nothing
            End If
        End If

    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAssociates.KeyPress
        Try
            If e.KeyChar = ChrW(13) Then
                If IsNothing(GloUC_trvAssociates.SelectedNode) Then
                    Exit Sub
                End If

                Dim i As Integer
                Dim intSelectedrow As Integer = 0
                Dim strSelectedICD9 As String = ""
                Dim strSelectedMod As String = ""
                Dim NewRow As Integer = 0

                Dim ofNode As C1.Win.C1FlexGrid.Node
                Dim nMaxCPTCount As Integer
                Dim nMaxICD9Count As Integer
                Dim nMaxModifierCount As Integer
                'If IsNothing(trvCPT.SelectedNode) = True Then
                '    Exit Sub
                'End If

                'If IsNothing(trvCPT.SelectedNode.FirstNode) = False Then
                '    Exit Sub
                'End If

                ' Dim mynode As myTreeNode
                'If Not IsNothing(trvCPT.SelectedNode) Then
                '    mynode = CType(trvCPT.SelectedNode, myTreeNode)
                '    AddNode_CPT(mynode)
                'End If

                If C1Dignosis.Rows.Count - 1 = 0 Then
                    Exit Sub
                End If


                'if modifier is present in treeview then insert this data   
                If _ModifierClick = True Then
                    With C1Dignosis
                        NewRow = 0
                        intSelectedrow = .Row
                        ofNode = .Rows(intSelectedrow).Node

                        If ofNode.Level = 0 Then
                            Exit Sub
                        Else
                            If ofNode.Level = 2 Then
                                .Select(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, Col_ICD9Code_Description)
                                intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                                ofNode = Nothing
                                ofNode = .Rows(intSelectedrow).Node
                            End If

                            ' Dim strDesriptionMod As String = trvCPT.SelectedNode.Text
                            Dim strDesriptionMod As String = GloUC_trvAssociates.SelectedNode.Text
                            If ofNode.Children > 0 Then
                                For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                    If .GetData(i, Col_ICD9Code_Description) = strDesriptionMod Then
                                        Exit Sub
                                    End If
                                Next
                                nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                                nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                                nMaxModifierCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ModCount)
                            Else
                                nMaxICD9Count = .GetData(.Row, Col_ICD9Count)
                                nMaxCPTCount = .GetData(.Row, Col_CPTCount)
                                nMaxModifierCount = 0
                            End If
                            NewRow = ofNode.GetCellRange().BottomRow

                            If NewRow = 0 Then
                                Exit Sub
                            Else
                                .Rows.Insert(NewRow + 1)
                                With .Rows(NewRow + 1)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 2
                                    .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                    '.Node.Data = trvCPT.SelectedNode.Text
                                    .Node.Data = GloUC_trvAssociates.SelectedNode.Text
                                End With

                                .Row = NewRow + 1
                                Dim strConctMOD As String
                                Dim arrstrConctMOD() As String
                                Dim strMODCode As String
                                Dim strMODDesc As String = ""

                                Dim strICDCode_new As String
                                Dim strICDDesc_new As String = ""
                                Dim strCPTCode_new As String
                                Dim strCPTDesc_new As String = ""

                                Dim strSnomedCode_new As String
                                Dim strSnomedDesc_new As String = ""

                                strICDCode_new = .GetData(NewRow, Col_ICD9Code)
                                strICDDesc_new = .GetData(NewRow, Col_ICD9Desc)

                                strCPTCode_new = .GetData(NewRow, COl_CPTCode)
                                strCPTDesc_new = .GetData(NewRow, Col_CPTDesc)

                                'strConctMOD = trvCPT.SelectedNode.Text
                                strConctMOD = GloUC_trvAssociates.SelectedNode.Text
                                arrstrConctMOD = Split(strConctMOD, "-", 2)
                                strMODCode = arrstrConctMOD.GetValue(0)
                                If (arrstrConctMOD.Length > 1) Then
                                    strMODDesc = arrstrConctMOD.GetValue(1)
                                End If

                                strSnomedCode_new = Convert.ToString(.GetData(NewRow, Col_SnomedCode))
                                strSnomedDesc_new = Convert.ToString(.GetData(NewRow, Col_SnomedDesc))

                                .SetData(NewRow + 1, Col_SnomedCode, strSnomedCode_new)
                                .SetData(NewRow + 1, Col_SnomedDesc, strSnomedDesc_new)

                                .SetData(NewRow + 1, Col_ICD9Code, strICDCode_new)
                                .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc_new)
                                .SetData(NewRow + 1, COl_CPTCode, strCPTCode_new)
                                .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc_new)
                                .SetData(NewRow + 1, Col_ModCode, strMODCode)
                                .SetData(NewRow + 1, Col_ModDesc, strMODDesc)
                                .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                                .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount)
                                .SetData(NewRow + 1, Col_ModCount, nMaxModifierCount + 1)
                                .SetData(NewRow + 1, Col_nICDRevision, .GetData(NewRow, Col_nICDRevision))
                            End If
                        End If
                    End With

                Else
                    With C1Dignosis
                        intSelectedrow = .Row
                        ' Dim strDesriptionCPT As String = trvCPT.SelectedNode.Text
                        Dim strDesriptionCPT As String = GloUC_trvAssociates.SelectedNode.Text
                        NewRow = 0
                        Dim ICD9LineNo As Integer = CType(.GetData(intSelectedrow, Col_ICD9Count), Integer)
                        ofNode = Nothing
                        ofNode = .Rows(intSelectedrow).Node
                        If ofNode.Level = 2 Then
                            Exit Sub
                        End If


                        If ofNode.Level = 1 Then
                            intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                            C1Dignosis.Select(intSelectedrow, Col_ICD9Code_Description)
                            C1Dignosis.Row = intSelectedrow
                        End If
                        ofNode = Nothing
                        ofNode = .Rows(intSelectedrow).Node

                        If ofNode.Children > 0 Then
                            For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If .GetData(i, Col_ICD9Code_Description) = strDesriptionCPT Then
                                    '.Select(i, Col_ICD9Code_Description)
                                    Exit Sub

                                End If
                            Next
                            If ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index = 0 Then
                                nMaxCPTCount = 0
                                nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                            Else
                                nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                                nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                            End If
                        Else
                            nMaxCPTCount = 0
                            nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                        End If
                        NewRow = ofNode.GetCellRange().BottomRow

                        If NewRow = 0 Then
                            Exit Sub
                        Else
                            .Rows.Insert(NewRow + 1)
                            With .Rows(NewRow + 1)
                                .AllowEditing = True
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 1
                                .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                '.Node.Data = trvCPT.SelectedNode.Text
                                .Node.Data = GloUC_trvAssociates.SelectedNode.Text
                            End With
                            .Row = NewRow + 1
                            'set the concated string which has ICD and CPT code in the col_sICD9Code field

                            '  Dim arrstrConctICD9() As String
                            Dim strICDCode As String
                            Dim strICDDesc As String

                            Dim strSnomedCode As String
                            Dim strSnomedDesc As String

                            strICDCode = .GetData(NewRow, Col_ICD9Code)
                            strICDDesc = .GetData(NewRow, Col_ICD9Desc)
                            strSnomedCode = .GetData(NewRow, Col_SnomedCode)
                            strSnomedDesc = .GetData(NewRow, Col_SnomedDesc)

                            Dim strConctCPT As String
                            Dim arrstrConctCPT() As String
                            Dim strCPTCode As String
                            Dim strCPTDesc As String = ""
                            'strConctCPT = trvCPT.SelectedNode.Text
                            strConctCPT = GloUC_trvAssociates.SelectedNode.Text
                            arrstrConctCPT = Split(strConctCPT, "-", 2)
                            strCPTCode = arrstrConctCPT.GetValue(0)
                            If (arrstrConctCPT.Length > 1) Then
                                strCPTDesc = arrstrConctCPT.GetValue(1)
                            End If


                            'set value for ICDCODE,ICDDesc,ICDCODE,ICDDesc, 
                            .SetData(NewRow + 1, Col_ICD9Code, strICDCode)
                            .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc)
                            .SetData(NewRow + 1, Col_SnomedCode, strSnomedCode)
                            .SetData(NewRow + 1, Col_SnomedDesc, strSnomedDesc)
                            .SetData(NewRow + 1, COl_CPTCode, strCPTCode)
                            .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc)
                            .SetData(NewRow + 1, Col_Units, 1)
                            .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount + 1)
                            .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                            .SetData(NewRow + 1, Col_nICDRevision, .GetData(NewRow, Col_nICDRevision))
                        End If
                    End With
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvAssociates.MouseDown
        Try
            If GloUC_trvAssociates.SelectedNode IsNot Nothing Then
                If e.Button = Windows.Forms.MouseButtons.Right Then

                    If _CPTClick = True Then
                        ContextMenuDiagnosis.Items(0).Visible = False
                        ContextMenuDiagnosis.Items(1).Visible = False
                        ContextMenuDiagnosis.Items(2).Visible = False
                        ContextMenuDiagnosis.Items(3).Visible = True
                        ContextMenuDiagnosis.Items(4).Visible = False
                        ContextMenuDiagnosis.Items(5).Visible = False
                        ContextMenuDiagnosis.Items(6).Visible = True
                        ContextMenuDiagnosis.Items(7).Visible = False
                        ContextMenuDiagnosis.Items(8).Visible = False
                        ContextMenuDiagnosis.Items(9).Visible = False
                        ContextMenuDiagnosis.Items(10).Visible = False
                        ContextMenuDiagnosis.Items(11).Visible = False
                        ContextMenuDiagnosis.Items(12).Visible = False
                        ContextMenuDiagnosis.Items(13).Visible = False
                        ContextMenuDiagnosis.Items(14).Visible = False
                        ContextMenuDiagnosis.Items(15).Visible = False
                        ContextMenuDiagnosis.Items(16).Visible = False
                    Else
                        ContextMenuDiagnosis.Items(0).Visible = False
                        ContextMenuDiagnosis.Items(1).Visible = False
                        ContextMenuDiagnosis.Items(2).Visible = False
                        ContextMenuDiagnosis.Items(3).Visible = False
                        ContextMenuDiagnosis.Items(4).Visible = True
                        ContextMenuDiagnosis.Items(5).Visible = False
                        ContextMenuDiagnosis.Items(6).Visible = False
                        ContextMenuDiagnosis.Items(7).Visible = True
                        ContextMenuDiagnosis.Items(8).Visible = False
                        ContextMenuDiagnosis.Items(9).Visible = False
                        ContextMenuDiagnosis.Items(10).Visible = False
                        ContextMenuDiagnosis.Items(11).Visible = False
                        ContextMenuDiagnosis.Items(12).Visible = False
                        ContextMenuDiagnosis.Items(13).Visible = False
                        ContextMenuDiagnosis.Items(14).Visible = False
                        ContextMenuDiagnosis.Items(15).Visible = False
                        ContextMenuDiagnosis.Items(16).Visible = False
                    End If
                    'Try
                    '    If (IsNothing(GloUC_trvAssociates.ContextMenuStrip) = False) Then
                    '        GloUC_trvAssociates.ContextMenuStrip.Dispose()
                    '        GloUC_trvAssociates.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    ' GloUC_trvAssociates.ContextMenuStrip = ContextMenuDiagnosis
                    GloUC_trvAssociates.ContextMenuStrip = Nothing
                    GloUC_trvAssociates.DisplayContextMenuStrip = ContextMenuDiagnosis
                    ChangeZOrder()
                End If
            Else
                If _CPTClick = True Then
                    ContextMenuDiagnosis.Items(0).Visible = False
                    ContextMenuDiagnosis.Items(1).Visible = False
                    ContextMenuDiagnosis.Items(2).Visible = False
                    ContextMenuDiagnosis.Items(3).Visible = True
                    ContextMenuDiagnosis.Items(4).Visible = False
                    ContextMenuDiagnosis.Items(5).Visible = False
                    ContextMenuDiagnosis.Items(6).Visible = False
                    ContextMenuDiagnosis.Items(7).Visible = False
                    ContextMenuDiagnosis.Items(8).Visible = False
                    ContextMenuDiagnosis.Items(9).Visible = False
                    ContextMenuDiagnosis.Items(10).Visible = False
                    ContextMenuDiagnosis.Items(11).Visible = False
                    ContextMenuDiagnosis.Items(12).Visible = False
                    ContextMenuDiagnosis.Items(13).Visible = False
                    ContextMenuDiagnosis.Items(14).Visible = False
                    ContextMenuDiagnosis.Items(15).Visible = False
                    ContextMenuDiagnosis.Items(16).Visible = False
                Else
                    ContextMenuDiagnosis.Items(0).Visible = False
                    ContextMenuDiagnosis.Items(1).Visible = False
                    ContextMenuDiagnosis.Items(2).Visible = False
                    ContextMenuDiagnosis.Items(3).Visible = False
                    ContextMenuDiagnosis.Items(4).Visible = True
                    ContextMenuDiagnosis.Items(5).Visible = False
                    ContextMenuDiagnosis.Items(6).Visible = False
                    ContextMenuDiagnosis.Items(7).Visible = False
                    ContextMenuDiagnosis.Items(8).Visible = False
                    ContextMenuDiagnosis.Items(9).Visible = False
                    ContextMenuDiagnosis.Items(10).Visible = False
                    ContextMenuDiagnosis.Items(11).Visible = False
                    ContextMenuDiagnosis.Items(12).Visible = False
                    ContextMenuDiagnosis.Items(13).Visible = False
                    ContextMenuDiagnosis.Items(14).Visible = False
                    ContextMenuDiagnosis.Items(15).Visible = False
                    ContextMenuDiagnosis.Items(16).Visible = False
                End If
                'Try
                '    If (IsNothing(GloUC_trvAssociates.ContextMenuStrip) = False) Then
                '        GloUC_trvAssociates.ContextMenuStrip.Dispose()
                '        GloUC_trvAssociates.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                '   GloUC_trvAssociates.ContextMenuStrip = ContextMenuDiagnosis

                GloUC_trvAssociates.ContextMenuStrip = Nothing
                GloUC_trvAssociates.DisplayContextMenuStrip = ContextMenuDiagnosis
                ChangeZOrder()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvAssociates.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ChangeZOrderAndContextMenuStripForTreeview(GloUC_trvAssociates, e.Location)
        End If
    End Sub

    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAssociates.NodeMouseDoubleClick
        Try

            Dim i As Integer
            Dim intSelectedrow As Integer = 0
            Dim strSelectedICD9 As String = ""
            Dim strSelectedMod As String = ""
            Dim NewRow As Integer = 0

            Dim ofNode As C1.Win.C1FlexGrid.Node
            Dim nMaxCPTCount As Integer
            Dim nMaxICD9Count As Integer
            Dim nMaxModifierCount As Integer
            'If IsNothing(trvCPT.SelectedNode) = True Then
            '    Exit Sub
            'End If

            'If IsNothing(trvCPT.SelectedNode.FirstNode) = False Then
            '    Exit Sub
            'End If

            '  Dim mynode As myTreeNode
            'If Not IsNothing(trvCPT.SelectedNode) Then
            '    mynode = CType(trvCPT.SelectedNode, myTreeNode)
            '    AddNode_CPT(mynode)
            'End If

            If C1Dignosis.Rows.Count - 1 = 0 Then
                Exit Sub
            End If

            If _ModifierClick = True Then
                With C1Dignosis
                    NewRow = 0
                    intSelectedrow = .Row
                    ofNode = .Rows(intSelectedrow).Node

                    If ofNode.Level = 0 Then
                        Exit Sub
                    Else
                        If ofNode.Level = 2 Then
                            .Select(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, Col_ICD9Code_Description)
                            intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                            ofNode = Nothing
                            ofNode = .Rows(intSelectedrow).Node
                        End If

                        ' Dim strDesriptionMod As String = trvCPT.SelectedNode.Text
                        Dim strDesriptionMod As String = GloUC_trvAssociates.SelectedNode.Text
                        If ofNode.Children > 0 Then
                            For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If .GetData(i, Col_ICD9Code_Description).Replace(" -", "-").Replace("- ", "-") = strDesriptionMod.Replace(" - ", "-") Then
                                    MessageBox.Show("Duplicate Modifier is not allowed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '' SUDHIR 20091014 '' BUG 3743 ''
                                    Exit Sub
                                End If
                            Next
                            nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                            nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                            nMaxModifierCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ModCount)
                        Else
                            nMaxICD9Count = .GetData(.Row, Col_ICD9Count)
                            nMaxCPTCount = .GetData(.Row, Col_CPTCount)
                            nMaxModifierCount = 0
                        End If
                        NewRow = ofNode.GetCellRange().BottomRow

                        If NewRow = 0 Then
                            Exit Sub
                        Else
                            .Rows.Insert(NewRow + 1)
                            With .Rows(NewRow + 1)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 2
                                .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                '.Node.Data = trvCPT.SelectedNode.Text
                                .Node.Data = GloUC_trvAssociates.SelectedNode.Text
                            End With

                            .Row = NewRow + 1
                            Dim strConctMOD As String
                            Dim arrstrConctMOD() As String
                            Dim strMODCode As String
                            Dim strMODDesc As String = ""

                            Dim strICDCode_new As String
                            Dim strICDDesc_new As String = ""
                            Dim strSnomedCode_new As String
                            Dim strSnomedDesc_new As String = ""
                            Dim strCPTCode_new As String
                            Dim strCPTDesc_new As String = ""


                            strICDCode_new = .GetData(NewRow, Col_ICD9Code)
                            strICDDesc_new = .GetData(NewRow, Col_ICD9Desc)
                            strSnomedCode_new = Convert.ToString(.GetData(NewRow, Col_SnomedCode))
                            strSnomedDesc_new = Convert.ToString(.GetData(NewRow, Col_SnomedDesc))
                            strCPTCode_new = .GetData(NewRow, COl_CPTCode)
                            strCPTDesc_new = .GetData(NewRow, Col_CPTDesc)

                            'strConctMOD = trvCPT.SelectedNode.Text
                            strConctMOD = GloUC_trvAssociates.SelectedNode.Text
                            arrstrConctMOD = Split(strConctMOD, "-", 2)
                            strMODCode = arrstrConctMOD.GetValue(0)
                            If (arrstrConctMOD.Length > 1) Then
                                strMODDesc = arrstrConctMOD.GetValue(1)
                            End If




                            .SetData(NewRow + 1, Col_ICD9Code, strICDCode_new)
                            .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc_new)
                            .SetData(NewRow + 1, Col_SnomedCode, Convert.ToString(strSnomedCode_new))
                            .SetData(NewRow + 1, Col_SnomedDesc, Convert.ToString(strSnomedDesc_new))
                            .SetData(NewRow + 1, COl_CPTCode, strCPTCode_new)
                            .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc_new)
                            .SetData(NewRow + 1, Col_ModCode, strMODCode)
                            .SetData(NewRow + 1, Col_ModDesc, strMODDesc)
                            .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                            .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount)
                            .SetData(NewRow + 1, Col_ModCount, nMaxModifierCount + 1)
                            .SetData(NewRow + 1, Col_nICDRevision, .GetData(NewRow, Col_nICDRevision))
                        End If
                    End If
                End With

            Else
                With C1Dignosis

                    '' SUDHIR 20090821 '' FOR LOOP TO SET CPT TO ALL ICD9s ''
                    Dim iRow As Integer = 1
                    While iRow <= C1Dignosis.Rows.Count - 1
                        If gblnSetCPTtoAllICD9 Then
                            intSelectedrow = iRow '' CPT TO ALL ''
                            If C1Dignosis.Rows(iRow).Node.Level <> 0 Then
                                iRow = iRow + 1
                                Continue While
                            End If
                        Else
                            intSelectedrow = .Row '' CPT TO SELECTED ROW ''
                        End If
                        
                         ' Dim strDesriptionCPT As String = trvCPT.SelectedNode.Text
                        Dim strDesriptionCPT As String = GloUC_trvAssociates.SelectedNode.Text
                        NewRow = 0
                        Dim ICD9LineNo As Integer = CType(.GetData(intSelectedrow, Col_ICD9Count), Integer)
                        ofNode = Nothing
                        ofNode = .Rows(intSelectedrow).Node
                        If ofNode.Level = 2 Then
                            If gblnSetCPTtoAllICD9 Then
                                iRow = iRow + 1
                                Continue While
                            Else
                                Exit Sub
                            End If
                        End If


                        If ofNode.Level = 1 Then
                            intSelectedrow = ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index
                            C1Dignosis.Select(intSelectedrow, Col_ICD9Code_Description)
                            C1Dignosis.Row = intSelectedrow
                        End If
                        ofNode = Nothing
                        ofNode = .Rows(intSelectedrow).Node

                        If ofNode.Children > 0 Then
                            For i = intSelectedrow To ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If .GetData(i, Col_ICD9Code_Description) = strDesriptionCPT Then
                                    '.Select(i, Col_ICD9Code_Description)
                                    If gblnSetCPTtoAllICD9 Then
                                        iRow = iRow + 1
                                        Continue While
                                    Else
                                        MessageBox.Show("Duplicate CPT is not allowed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '' SUDHIR 20091014 '' BUG 3743 ''
                                        Exit Sub
                                    End If

                                End If
                            Next
                            If ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index = 0 Then
                                nMaxCPTCount = 0
                                nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                            Else
                                nMaxCPTCount = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_CPTCount)
                                nMaxICD9Count = .GetData(ofNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index, Col_ICD9Count)
                            End If
                        Else
                            nMaxCPTCount = 0
                            nMaxICD9Count = .GetData(intSelectedrow, Col_ICD9Count)
                        End If
                        NewRow = ofNode.GetCellRange().BottomRow

                        If NewRow = 0 Then
                            If gblnSetCPTtoAllICD9 Then
                                iRow = iRow + 1
                                Continue While
                            Else
                                Exit Sub
                            End If
                        Else
                            .Rows.Insert(NewRow + 1)
                            With .Rows(NewRow + 1)
                                .AllowEditing = True
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 1
                                .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                '.Node.Data = trvCPT.SelectedNode.Text
                                .Node.Data = GloUC_trvAssociates.SelectedNode.Text
                            End With
                            .Row = NewRow + 1
                            'set the concated string which has ICD and CPT code in the col_sICD9Code field

                            ' Dim arrstrConctICD9() As String
                            Dim strICDCode As String
                            Dim strICDDesc As String
                            Dim strSnomedCode As String
                            Dim strSnomedDesc As String

                            strICDCode = .GetData(NewRow, Col_ICD9Code)
                            strICDDesc = .GetData(NewRow, Col_ICD9Desc)
                            strSnomedCode = Convert.ToString(.GetData(NewRow, Col_SnomedCode))
                            strSnomedDesc = Convert.ToString(.GetData(NewRow, Col_SnomedDesc))

                            Dim strConctCPT As String
                            Dim arrstrConctCPT() As String
                            Dim strCPTCode As String = ""
                            Dim strCPTDesc As String = ""
                            'strConctCPT = trvCPT.SelectedNode.Text
                            strConctCPT = GloUC_trvAssociates.SelectedNode.Text
                            If Not IsNothing(strConctCPT) AndAlso strConctCPT <> "" Then
                                arrstrConctCPT = Split(strConctCPT, "-", 2)
                                strCPTCode = arrstrConctCPT.GetValue(0)
                                If arrstrConctCPT.Length > 1 Then
                                    strCPTDesc = arrstrConctCPT.GetValue(1)
                                End If
                            End If
                            'set value for ICDCODE,ICDDesc,ICDCODE,ICDDesc, 
                            .SetData(NewRow + 1, Col_ICD9Code, strICDCode)
                            .SetData(NewRow + 1, Col_ICD9Desc, strICDDesc)
                            .SetData(NewRow + 1, COl_CPTCode, strCPTCode)
                            .SetData(NewRow + 1, Col_CPTDesc, strCPTDesc)
                            .SetData(NewRow + 1, Col_SnomedCode, Convert.ToString(strSnomedCode))
                            .SetData(NewRow + 1, Col_SnomedDesc, Convert.ToString(strSnomedDesc))
                            ''Line edited by dipak 20090819 to set default valu to 1 insted of 0 for units in C1Dignosis for CPT node

                            .SetData(NewRow + 1, Col_Units, 1)
                            .SetData(NewRow + 1, Col_CPTCount, nMaxCPTCount + 1)
                            .SetData(NewRow + 1, Col_ICD9Count, nMaxICD9Count)
                            .SetData(NewRow + 1, Col_nICDRevision, .GetData(NewRow, Col_nICDRevision))
                        End If

                        If gblnSetCPTtoAllICD9 = False Then
                            Exit Sub
                        End If
                        iRow = iRow + 1
                    End While

                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' SUDHIR 20090716 ''
    Private Sub mnuAddICD9_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddICD9.Click
        Try
            Dim oICD9 As New gloBilling.frmSetupICD9(Convert.ToInt16(SelectedICD()), GetConnectionString)
            oICD9.BringToFront()
            Me.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            oICD9.TopMost = True
            oICD9.ShowDialog(IIf(IsNothing(oICD9.Parent), Me, oICD9.Parent))
            oICD9.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            'Me.TopMost = True
            Me.BringToFront()
            If oICD9.ICD9Code <> "" Then
                FillICD9ICD10()
            End If
            oICD9.Dispose()
            oICD9 = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuEditICD9_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditICD9.Click
        Try
            If GloUC_trvICD.SelectedNode IsNot Nothing Then
                Dim _ICD9ID As Int64 = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode).ID
                Dim oICD9 As New gloBilling.frmSetupICD9(SelectedICD(), _ICD9ID, GetConnectionString)
                Me.TopMost = False
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                oICD9.TopMost = True
                oICD9.ShowDialog(IIf(IsNothing(oICD9.Parent), Me, oICD9.Parent))
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                'Me.TopMost = True
                oICD9.TopMost = False
                FillICD9ICD10()
                oICD9.Dispose()
                oICD9 = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub mnuAddICD10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddICD10.Click
        Try
            Dim oICD10 As New gloBilling.frmSetupICD9(Convert.ToInt16(SelectedICD()), GetConnectionString)
            oICD10.BringToFront()
            Me.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            oICD10.TopMost = True
            oICD10.ShowDialog(IIf(IsNothing(oICD10.Parent), Me, oICD10.Parent))
            oICD10.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            'Me.TopMost = True
            Me.BringToFront()
            If oICD10.ICD9Code <> "" Then
                FillICD9ICD10()
            End If
            oICD10.Dispose()
            oICD10 = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub mnuEditICD10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditICD10.Click
        Try
            If GloUC_trvICD.SelectedNode IsNot Nothing Then
                Dim _ICD10ID As Int64 = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode).ID
                Dim oICD10 As New gloBilling.frmSetupICD9(Convert.ToInt16(SelectedICD()), _ICD10ID, GetConnectionString)
                Me.TopMost = False
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                oICD10.TopMost = True
                oICD10.ShowDialog(IIf(IsNothing(oICD10.Parent), Me, oICD10.Parent))
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                'Me.TopMost = True

                oICD10.TopMost = False

                If oICD10.IsSaving Then

                    Dim TreeNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode)

                    If TreeNode IsNot Nothing Then
                        TreeNode.Description = oICD10.Description
                        TreeNode.Code = oICD10.Code
                        TreeNode.Text = TreeNode.Code + " - " + TreeNode.Description
                    End If

                    TreeNode = Nothing

                End If

                oICD10.Dispose()
                oICD10 = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuAddCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddCPT.Click
        Try
            Dim oCPT As New gloBilling.frmSetupCPT(GetConnectionString)

            Me.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            oCPT.TopMost = True
            oCPT.ShowDialog(IIf(IsNothing(oCPT.Parent), Me, oCPT.Parent))
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            'Me.TopMost = True
            oCPT.TopMost = False

            If oCPT.CPTID > 0 Then
                FillCPT()
            End If
            oCPT.Dispose()
            oCPT = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuEditCPT_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCPT.Click
        Try
            If GloUC_trvAssociates.SelectedNode IsNot Nothing Then
                Dim IsEditMultiple As Boolean
                Dim _CPTID As Int64 = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode).ID
                Dim oCPT As New gloBilling.frmSetupCPT(_CPTID, IsEditMultiple, GetConnectionString)

                Me.TopMost = False
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                oCPT.TopMost = True
                oCPT.ShowDialog(IIf(IsNothing(oCPT.Parent), Me, oCPT.Parent))
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                'Me.TopMost = True
                oCPT.TopMost = False

                FillCPT()
                oCPT.Dispose()
                oCPT = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuAddModifier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddModifier.Click
        Try
            Dim oModifier As New gloBilling.frmSetupModifier(GetConnectionString)

            Me.TopMost = False
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            oModifier.TopMost = True
            oModifier.ShowDialog(IIf(IsNothing(oModifier.Parent), Me, oModifier.Parent))
            For Each myForm As Form In Application.OpenForms
                If (myForm.TopMost) Then
                    myForm.TopMost = False
                End If
            Next
            'Me.TopMost = True
            oModifier.TopMost = False

            If oModifier.ModifierID > 0 Then
                FillModifiers()
            End If
            oModifier.Dispose()
            oModifier = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuEditModifier_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditModifier.Click
        Try
            If GloUC_trvAssociates.SelectedNode IsNot Nothing Then
                Dim _ModifierID As Int64 = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode).ID
                Dim oModifier As New gloBilling.frmSetupModifier(_ModifierID, GetConnectionString)

                Me.TopMost = False
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                oModifier.TopMost = True
                oModifier.ShowDialog(IIf(IsNothing(oModifier.Parent), Me, oModifier.Parent))
                For Each myForm As Form In Application.OpenForms
                    If (myForm.TopMost) Then
                        myForm.TopMost = False
                    End If
                Next
                'Me.TopMost = True
                oModifier.TopMost = False

                FillModifiers()
                oModifier.Dispose()
                oModifier = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' SUDHIR 20090901 ''

    Private Sub mnuAssociateAllCPTtoAllICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAssociateAllCPTtoAllICD9.Click

        Dim oCPTNode As C1.Win.C1FlexGrid.Node
        Dim oICD9Node As C1.Win.C1FlexGrid.Node
        Dim ICD9Range As C1.Win.C1FlexGrid.CellRange
        Dim CPTRange As C1.Win.C1FlexGrid.CellRange
        Dim oNewNode As C1.Win.C1FlexGrid.Node
        Dim oTempCPTNode As C1.Win.C1FlexGrid.Node
        Dim arrlist As New ArrayList
        Dim mylist As myList
        Dim ounit As String
        Dim ParentIndex As Integer
        Dim CPTNodefound As Boolean = False
        Dim arrmod() As String
        With C1Dignosis
            Dim i As Integer = 1
            While (i <= .Rows.Count - 1)
                ''For i As Integer = 1 To .Rows.Count
                oCPTNode = .Rows(i).Node

                If oCPTNode.Level = 1 Then
                    ounit = .GetData(i, Col_Units)
                    GetOtherDetails(i)
                    If oCPTNode.Children > 0 Then
                        CPTRange = oCPTNode.GetCellRange()
                        arrlist.Clear()
                        arrlist = Nothing
                        arrlist = New ArrayList
                        For cnt As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                            mylist = New myList
                            mylist.Code = oCPTNode.Data '''' Parent Node i.e. cpt
                            mylist.Description = .Rows(cnt).Node.Data ''''child Node i.e. modifier
                            arrlist.Add(mylist)
                        Next
                    End If
                    For j As Integer = 1 To .Rows.Count - 1
                        oICD9Node = .Rows(j).Node
                        If oICD9Node.Level = 0 Then
                            ICD9Range = oICD9Node.GetCellRange()
                            CPTNodefound = False
                            For k As Integer = ICD9Range.TopRow + 1 To ICD9Range.BottomRow
                                If oCPTNode.Data = .Rows(k).Node.Data Then
                                    CPTNodefound = True
                                    Exit For
                                End If
                            Next
                            If CPTNodefound = False Then
                                ''.Rows.Insert(ICD9Range.BottomRow + 1
                                If oCPTNode.Children <= 0 Then
                                    oNewNode = .Rows.InsertNode(ICD9Range.BottomRow + 1, 1)
                                    oNewNode.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                    oNewNode.Data = oCPTNode.Data
                                    C1Dignosis.SetData(oNewNode.Row.Index, Col_Units, ounit)
                                    SetOtherData(oNewNode.Row.Index)
                                    ParentIndex = .Rows(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                    .SetData(oNewNode.Row.Index, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                    .SetData(oNewNode.Row.Index, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                    .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                Else

                                    CPTRange = oCPTNode.GetCellRange()
                                    Dim flexRow As Integer = 0

                                    oNewNode = .Rows.InsertNode(ICD9Range.BottomRow + 1, 1)
                                    oNewNode.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                    oNewNode.Data = oCPTNode.Data
                                    C1Dignosis.SetData(oNewNode.Row.Index, Col_Units, ounit)
                                    SetOtherData(oNewNode.Row.Index)
                                    ParentIndex = .Rows(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                    .SetData(oNewNode.Row.Index, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                    .SetData(oNewNode.Row.Index, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                    .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                    flexRow = oNewNode.Row.Index + 1
                                    For l As Integer = 0 To arrlist.Count - 1
                                        oTempCPTNode = .Rows.InsertNode(flexRow, 2)
                                        oTempCPTNode.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                        oTempCPTNode.Data = CType(arrlist.Item(l), myList).Description
                                        SetOtherData(flexRow)
                                        ParentIndex = .Rows(oTempCPTNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                        arrmod = Split(CType(arrlist.Item(l), myList).Description, "-", 2)
                                        .SetData(flexRow, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                        .SetData(flexRow, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                        .SetData(flexRow, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                        .SetData(flexRow, Col_ModCode, arrmod.GetValue(0))
                                        If (arrmod.Length > 1) Then
                                            .SetData(flexRow, Col_ModDesc, arrmod.GetValue(1))
                                        End If

                                        flexRow = flexRow + 1
                                    Next
                                    'For l As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                                    '    oTempCPTNode = .Rows.InsertNode(oNewNode.Row.Index + 1, 2)
                                    '    oTempCPTNode.Image = Global.gloEMR.My.Resources.Resources.Modifier
                                    '    oTempCPTNode.Data = .Rows(l).Node.Data
                                    'Next

                                End If

                            End If
                        End If
                    Next
                End If
                i = i + 1
            End While
            ''       Next
            RearrangeIndex()
        End With


    End Sub

    Private Sub mnuAssociateAllCPTToAllUnassociatedICD9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAssociateAllCPTToAllUnassociatedICD9.Click
        Try
            Dim oCPTNode As C1.Win.C1FlexGrid.Node
            Dim oICD9Node As C1.Win.C1FlexGrid.Node
            Dim ICD9Range As C1.Win.C1FlexGrid.CellRange
            Dim CPTRange As C1.Win.C1FlexGrid.CellRange
            Dim oNewNode As C1.Win.C1FlexGrid.Node
            Dim oTempCPTNode As C1.Win.C1FlexGrid.Node
            Dim arrlist As New ArrayList
            Dim mylist As myList
            Dim arrICD9 As New ArrayList
            Dim CPTNodefound As Boolean = False
            Dim ounit As String
            Dim ParentIndex As Integer = 0
            Dim arrmod() As String
            With C1Dignosis
                For cntICD0 As Integer = 1 To .Rows.Count - 1
                    oICD9Node = .Rows(cntICD0).Node
                    If oICD9Node.Level = 0 Then
                        If oICD9Node.Children = 0 Then
                            arrICD9.Add(oICD9Node.Data)
                        End If
                    End If
                Next
                Dim i As Integer = 1
                While (i <= .Rows.Count - 1)
                    ''For i As Integer = 1 To .Rows.Count
                    oCPTNode = .Rows(i).Node

                    If oCPTNode.Level = 1 Then
                        ounit = .GetData(i, Col_Units)
                        GetOtherDetails(i)
                        If oCPTNode.Children > 0 Then
                            CPTRange = oCPTNode.GetCellRange()
                            arrlist.Clear()
                            arrlist = Nothing
                            arrlist = New ArrayList
                            For cnt As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                                mylist = New myList
                                mylist.Code = oCPTNode.Data
                                mylist.Description = .Rows(cnt).Node.Data
                                arrlist.Add(mylist)
                            Next
                        End If
                        For j As Integer = 1 To .Rows.Count - 1
                            oICD9Node = .Rows(j).Node

                            If oICD9Node.Level = 0 Then
                                If Not IsNothing(arrICD9) Then
                                    If arrICD9.Count > 0 Then
                                        If oICD9Node.Children <= 0 Or arrICD9.Contains(oICD9Node.Data) = True Then
                                            ICD9Range = oICD9Node.GetCellRange()
                                            CPTNodefound = False
                                            For k As Integer = ICD9Range.TopRow + 1 To ICD9Range.BottomRow
                                                If oCPTNode.Data = .Rows(k).Node.Data Then
                                                    CPTNodefound = True
                                                    Exit For
                                                End If
                                            Next
                                            If CPTNodefound = False Then
                                                ''.Rows.Insert(ICD9Range.BottomRow + 1
                                                If oCPTNode.Children <= 0 Then
                                                    oNewNode = .Rows.InsertNode(ICD9Range.BottomRow + 1, 1)
                                                    oNewNode.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                                    oNewNode.Data = oCPTNode.Data
                                                    .SetData(oNewNode.Row.Index, Col_Units, ounit)
                                                    SetOtherData(oNewNode.Row.Index)
                                                    ParentIndex = .Rows(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                                    .SetData(oNewNode.Row.Index, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                                    .SetData(oNewNode.Row.Index, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                                    .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                                Else

                                                    CPTRange = oCPTNode.GetCellRange()
                                                    Dim flexRow As Integer = 0

                                                    oNewNode = .Rows.InsertNode(ICD9Range.BottomRow + 1, 1)
                                                    oNewNode.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                                    oNewNode.Data = oCPTNode.Data
                                                    .SetData(oNewNode.Row.Index, Col_Units, ounit)
                                                    SetOtherData(oNewNode.Row.Index)
                                                    ParentIndex = .Rows(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                                    .SetData(oNewNode.Row.Index, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                                    .SetData(oNewNode.Row.Index, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                                    .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                                    flexRow = oNewNode.Row.Index + 1
                                                    For l As Integer = 0 To arrlist.Count - 1
                                                        oTempCPTNode = .Rows.InsertNode(flexRow, 2)
                                                        oTempCPTNode.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                                        oTempCPTNode.Data = CType(arrlist.Item(l), myList).Description
                                                        SetOtherData(flexRow)
                                                        ParentIndex = .Rows(oTempCPTNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index).Index
                                                        arrmod = Split(CType(arrlist.Item(l), myList).Description, "-", 2)
                                                        .SetData(flexRow, Col_ICD9Code, .GetData(ParentIndex, Col_ICD9Code))
                                                        .SetData(flexRow, Col_ICD9Desc, .GetData(ParentIndex, Col_ICD9Desc))
                                                        .SetData(flexRow, Col_ModCode, arrmod.GetValue(0))
                                                        If (arrmod.Length > 1) Then
                                                            .SetData(flexRow, Col_ModDesc, arrmod.GetValue(1))
                                                        End If

                                                        .SetData(flexRow, Col_nICDRevision, .GetData(ParentIndex, Col_nICDRevision))
                                                        flexRow = flexRow + 1
                                                    Next
                                                End If

                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                    i = i + 1
                End While
                ''       Next
                RearrangeIndex()
            End With
        Catch
        End Try
    End Sub

    Private Sub mnuAssociateModifierWithAllCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAssociateModifierWithAllCPT.Click
        Try
            Dim oNode As C1.Win.C1FlexGrid.Node
            oNode = C1Dignosis.Rows(C1Dignosis.Row).Node
            Dim oModNode As C1.Win.C1FlexGrid.Node
            Dim oNewNode As C1.Win.C1FlexGrid.Node
            Dim CPTRange As C1.Win.C1FlexGrid.CellRange
            Dim IsModiferFound As Boolean = False
            Dim i As Integer = 1

            With C1Dignosis
                While (i <= C1Dignosis.Rows.Count - 1)
                    oModNode = C1Dignosis.Rows(i).Node

                    If oModNode.Level = 1 Then

                        If oModNode.Children <= 0 Then
                            oNewNode = .Rows.InsertNode(i + 1, 2)
                            oNewNode.Image = Global.gloEMR.My.Resources.Resources.Modify1
                            oNewNode.Data = oNode.Data
                            GetOtherDetails(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index)
                            SetOtherData(oNewNode.Row.Index)
                            .SetData(oNewNode.Row.Index, Col_ModCode, .GetData(oNode.Row.Index, Col_ModCode))
                            .SetData(oNewNode.Row.Index, Col_ModDesc, .GetData(oNode.Row.Index, Col_ModDesc))
                            .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(oNode.Row.Index, Col_nICDRevision))
                            .SetData(oNewNode.Row.Index, Col_SnomedCode, .GetData(oNode.Row.Index, Col_SnomedCode))
                            .SetData(oNewNode.Row.Index, Col_SnomedDesc, .GetData(oNode.Row.Index, Col_SnomedDesc))
                            '06-Mar-15 Aniket: Resolving Bug #80107 ( Modified): gloEMR: Dx-CPT(Order Template)- Application should not allow user to put digits against modifiers
                            .Rows(oNewNode.Row.Index).AllowEditing = False
                        Else
                            CPTRange = oModNode.GetCellRange()
                            IsModiferFound = False
                            For j As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
                                If .Rows(j).Node.Data = oNode.Data Then
                                    IsModiferFound = True
                                    Exit For
                                End If
                            Next
                            If IsModiferFound = False Then
                                oNewNode = .Rows.InsertNode(oModNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1, 2)
                                oNewNode.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                oNewNode.Data = oNode.Data
                                GetOtherDetails(oNewNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index)
                                SetOtherData(oNewNode.Row.Index)
                                .SetData(oNewNode.Row.Index, Col_ModCode, .GetData(oNode.Row.Index, Col_ModCode))
                                .SetData(oNewNode.Row.Index, Col_ModDesc, .GetData(oNode.Row.Index, Col_ModDesc))
                                .SetData(oNewNode.Row.Index, Col_nICDRevision, .GetData(oNode.Row.Index, Col_nICDRevision))
                                .SetData(oNewNode.Row.Index, Col_SnomedCode, .GetData(oNode.Row.Index, Col_SnomedCode))
                                .SetData(oNewNode.Row.Index, Col_SnomedDesc, .GetData(oNode.Row.Index, Col_SnomedDesc))
                                '06-Mar-15 Aniket: Resolving Bug #80107 ( Modified): gloEMR: Dx-CPT(Order Template)- Application should not allow user to put digits against modifiers
                                .Rows(oNewNode.Row.Index).AllowEditing = False
                            End If
                        End If
                    End If
                    i = i + 1
                End While
                RearrangeIndex()
            End With
        Catch
        End Try
    End Sub

    Private Sub mnuRemoveAllDiagnosis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveAllDiagnosis.Click
        Try
            Dim _SelRow As Integer = C1Dignosis.Row
            Dim oNode As C1.Win.C1FlexGrid.Node

            Select Case C1Dignosis.Rows(_SelRow).Node.Level
                Case 0 '' REMOVE ALL DIAGNOSIS ''
                    If MessageBox.Show("Are you sure you want to remove all diagnosis ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        C1Dignosis.Rows.Count = 1
                        pnl_tlsp_Top.Focus()
                        frmPatientExam.blnChangesMade = True
                    End If

                Case 1 '' REMOVE ALL ICD9s
                    If MessageBox.Show("Are you sure you want to remove all treatment ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        oNode = C1Dignosis.Rows(_SelRow).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                        While oNode.Children > 0
                            oNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).RemoveNode()
                        End While
                        pnl_tlsp_Top.Focus()
                    End If

                Case 2 '' REMOVE ALL MODIFIERS
                    If MessageBox.Show("Are you sure you want to remove all modifier ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        For iRow As Integer = C1Dignosis.Rows.Count - 1 To 1 Step -1
                            If C1Dignosis.Rows(iRow).Node.Level = 2 Then
                                C1Dignosis.Rows.Remove(iRow)
                            End If
                        Next
                        pnl_tlsp_Top.Focus()
                    End If
            End Select
        Catch
        End Try
    End Sub

    Private Sub mnuRemoveAllModifierforSelectedCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoveAllModifierforSelectedCPT.Click
        Try
            Dim _SelRow As Integer = C1Dignosis.Row
            Dim oNode As C1.Win.C1FlexGrid.Node

            If MessageBox.Show("Are you sure you want to remove all modifier ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                oNode = C1Dignosis.Rows(_SelRow).Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)
                While oNode.Children > 0
                    oNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).RemoveNode()
                End While
            End If
        Catch
        End Try
    End Sub
    '' END SUHDIR ''

    Public Sub RearrangeIndex()
        Dim nICD9Count As Integer = 0
        Dim nCPTCount As Integer = 0
        Dim nModifierCount As Integer = 0
        Dim ofleNode As C1.Win.C1FlexGrid.Node
        ' Dim strICD9CPTMod As String
        With C1Dignosis
            For i As Integer = 1 To .Rows.Count - 1
                ofleNode = C1Dignosis.Rows(i).Node
                If ofleNode.Level = 0 Then
                    nICD9Count = nICD9Count + 1
                    nCPTCount = 0
                    '.SetData(i, Col_ICD9Code, nICD9Count)
                    .SetData(i, Col_ICD9Count, nICD9Count)
                ElseIf ofleNode.Level = 1 Then
                    nCPTCount = nCPTCount + 1
                    nModifierCount = 0
                    'strICD9CPTMod = nICD9Count & "|" & nICD9Count & "CPT"

                    '.SetData(i, Col_ICD9Code, strICD9CPTMod)
                    .SetData(i, Col_ICD9Count, nICD9Count)
                    .SetData(i, Col_CPTCount, nCPTCount)
                ElseIf ofleNode.Level = 2 Then
                    nModifierCount = nModifierCount + 1
                    'strICD9CPTMod = nICD9Count & "|" & nICD9Count & "CPT" & "|" & "MOD"
                    '.SetData(i, Col_ICD9Code, strICD9CPTMod)
                    .SetData(i, Col_ICD9Count, nICD9Count)
                    .SetData(i, Col_CPTCount, nCPTCount)
                    .SetData(i, Col_ModCount, nModifierCount)
                End If
            Next
        End With
    End Sub

    Public Sub GetOtherDetails(ByVal index As Integer)
        With C1Dignosis
            strICD9Code = .GetData(index, Col_ICD9Code)
            strICD9Description = .GetData(index, Col_ICD9Desc)
            strCPTCode = .GetData(index, COl_CPTCode)
            strCPTDescription = .GetData(index, Col_CPTDesc)
            strModCode = .GetData(index, Col_ModCode)
            strModDescription = .GetData(index, Col_ModDesc)
            nICDRevision = .GetData(index, Col_nICDRevision)
        End With

    End Sub
    Public Sub SetOtherData(ByVal index As Integer)
        With C1Dignosis
            .SetData(index, Col_ICD9Code, strICD9Code)
            .SetData(index, Col_ICD9Desc, strICD9Description)
            .SetData(index, COl_CPTCode, strCPTCode)
            .SetData(index, Col_CPTDesc, strCPTDescription)
            .SetData(index, Col_ModCode, strModCode)
            .SetData(index, Col_ModDesc, strModDescription)
            .Rows(index).Height = 24
        End With
    End Sub

    '' SUDHIR 20091026 ''
    Private Sub btnUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUP.Click
        If C1Dignosis.Rows.Count > 1 Then
            Dim oNode As C1.Win.C1FlexGrid.Node
            oNode = C1Dignosis.Rows(C1Dignosis.Row).Node
            If oNode.Level = 0 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Up)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            ElseIf oNode.Level = 1 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Up)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            ElseIf oNode.Level = 2 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Up)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            End If
        End If
    End Sub

    Private Sub btndown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        If C1Dignosis.Rows.Count > 1 Then
            Dim oNode As C1.Win.C1FlexGrid.Node
            oNode = C1Dignosis.Rows(C1Dignosis.Row).Node
            If oNode.Level = 0 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Down)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            ElseIf oNode.Level = 1 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Down)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            ElseIf oNode.Level = 2 Then
                oNode.Move(C1.Win.C1FlexGrid.NodeMoveEnum.Down)
                RearrangeIndex()
                C1Dignosis.Select(oNode.Row.Index, Col_ICD9Code_Description)
            End If
        End If
    End Sub

    Private Sub btnUP_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUP.MouseHover
        btnUP.BackgroundImage = Global.gloEMR.My.Resources.YellowUp24
        btnUP.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUP_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUP.MouseLeave
        btnUP.BackgroundImage = Global.gloEMR.My.Resources.Up24
        btnUP.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btndown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.YellowDown24
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btndown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Down24
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub
    '' END SUDHIR ''

    Private Sub frm_Diagnosis_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Cursor = Cursors.WaitCursor
        'Try
        '    Application.DoEvents()
        '    FillICD9()
        '    RefreshSearch()
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, "Patient Diagnosis Opened", gloAuditTrail.ActivityOutCome.Success)
        '    FillCPT()
        'Catch ex As Exception
        'End Try
        'Cursor = Cursors.Default

    End Sub

    Private Sub tstripDiagnosis_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstripDiagnosis.MouseHover
        'Me.Focus()
        'Me.ActiveControl = tstripDiagnosis
        'GloUC_trvICD.SetFocus()
    End Sub

    'Private Sub AssociateCPT(ByVal SelectedCPT_Node As C1.Win.C1FlexGrid.Node)

    '    Dim AssociateModifiers As Boolean = False
    '    Dim ModifiersAssociated As New ArrayList
    '    Dim SelectedICD_Node As C1.Win.C1FlexGrid.Node

    '    Dim CPT() As String
    '    Dim SelectedCPT_Code As String
    '    Dim SelectedCPT_Desc As String

    '    Dim ICD() As String
    '    Dim SelectedICD_Code As String
    '    Dim SelectedICD_Desc As String

    '    SelectedICD_Node = SelectedCPT_Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent)

    '    If SelectedCPT_Node.Children > 0 Then
    '        If MessageBox.Show("Do you want to associate Modifier?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '            AssociateModifiers = True
    '            ModifiersAssociated = GetModifiers(SelectedCPT_Node)
    '        End If
    '    End If

    '    CPT = Split(SelectedCPT_Node.Data, "-", 2)
    '    SelectedCPT_Code = CPT.GetValue(0)
    '    SelectedCPT_Desc = CPT.GetValue(1)

    '    ICD = Split(SelectedICD_Node.Data, "-", 2)
    '    SelectedICD_Code = ICD.GetValue(0)
    '    SelectedICD_Desc = ICD.GetValue(1)

    '    ICD = Nothing

    '    For rowIndex As Integer = C1Dignosis.Rows.Count - 1 To 1 Step -1

    '        Dim ICDNode As C1.Win.C1FlexGrid.Node
    '        Dim CPTNode As C1.Win.C1FlexGrid.Node
    '        Dim CPT_Code As String
    '        Dim CPT_Desc As String

    '        If C1Dignosis.Rows(rowIndex).Node.Level = 0 Then
    '            ICDNode = C1Dignosis.Rows(rowIndex).Node
    '            ICD = Split(ICDNode.Data, "-", 2)

    '            Dim IsFound As Boolean = False
    '            Dim NewRowIndex As Integer = 0

    '            If (ICDNode.Children > 0) Then
    '                For j As Integer = ICDNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.FirstChild).Row.Index To ICDNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index 'Step -1

    '                    CPT_Code = C1Dignosis.GetData(j, COl_CPTCode)
    '                    CPT_Desc = C1Dignosis.GetData(j, Col_CPTDesc)

    '                    CPT.SetValue(CPT_Code, 0)
    '                    CPT.SetValue(CPT_Desc, 1)

    '                    If (CPT_Code = SelectedCPT_Code) And (CPT_Desc = SelectedCPT_Desc) Then
    '                        IsFound = True
    '                        Exit For
    '                    End If
    '                Next
    '            End If

    '            If Not IsFound Then
    '                InsertCPT(ICDNode, ICD, CPT, rowIndex)
    '            End If

    '        End If
    '    Next
    'End Sub

    'Private Sub InsertCPT(ByVal ICDNode As C1.Win.C1FlexGrid.Node, ByVal ICD() As String, ByVal CPT() As String, ByVal rowIndex As Integer)

    '    Dim NewRowIndex As Integer = 0

    '    If (ICDNode.Children > 0) Then
    '        NewRowIndex = ICDNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index + 1
    '    Else
    '        NewRowIndex = rowIndex + 1
    '    End If

    '    C1Dignosis.Rows.Insert(NewRowIndex)
    '    With C1Dignosis.Rows(NewRowIndex)
    '        .AllowEditing = True
    '        .ImageAndText = True
    '        .Height = 24
    '        .IsNode = True
    '        .Node.Level = 1
    '        .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
    '        .Node.Data = C1Dignosis.GetData(C1Dignosis.Row, Col_ICD9Code_Description)
    '    End With
    '    With C1Dignosis
    '        .SetData(NewRowIndex, Col_Units, .GetData(.Row, Col_Units))
    '        .SetData(NewRowIndex, Col_ICD9Count, .GetData(ICDNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_ICD9Count))
    '        .SetData(NewRowIndex, Col_CPTCount, CType(.GetData(ICDNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index - 1, Col_CPTCount), Integer) + 1)
    '        .SetData(NewRowIndex, Col_ICD9Code, ICD.GetValue(0))
    '        .SetData(NewRowIndex, Col_ICD9Desc, ICD.GetValue(1))
    '        .SetData(NewRowIndex, COl_CPTCode, CPT.GetValue(0))
    '        .SetData(NewRowIndex, Col_CPTDesc, CPT.GetValue(1))
    '    End With

    'End Sub

    'Private Function GetModifiers(ByVal SelectedCPT_Node As C1.Win.C1FlexGrid.Node) As ArrayList
    '    Dim Modifiers As New ArrayList
    '    Dim CPTRange As C1.Win.C1FlexGrid.CellRange
    '    Dim myListItem As myList

    '    If SelectedCPT_Node.Children > 0 Then
    '        CPTRange = SelectedCPT_Node.GetCellRange()
    '        Modifiers.Clear()
    '        Modifiers = Nothing
    '        Modifiers = New ArrayList
    '        For rowIndex As Integer = CPTRange.TopRow + 1 To CPTRange.BottomRow
    '            myListItem = New myList
    '            myListItem.Code = SelectedCPT_Node.Data '''' Parent Node i.e. cpt
    '            myListItem.Description = C1Dignosis.Rows(rowIndex).Node.Data ''''child Node i.e. modifier
    '            Modifiers.Add(myListItem)
    '        Next
    '    End If

    '    Return Modifiers

    'End Function
    Public Function FormatNumber(ByVal Number As Decimal) As Decimal
        Dim _result As [Decimal] = Number
        Try
            Dim no As [String]() = _result.ToString().Split("."c)
            If no.GetUpperBound(0) > 0 Then
                If no(1).ToString().Length > 4 Then
                    no(1) = no(1).Substring(0, 4)
                End If
                _result = Convert.ToDecimal(no(0) + "." + no(1))
            End If
            _result = Convert.ToDecimal(_result.ToString("0.####"))
        Catch
            _result = Number
        End Try
        Return _result
    End Function
    Private Function ValidateDiagnosis() As Boolean
        Dim lst As myList
        Dim i As Integer
        Dim intUnits As System.Double
        For i = 1 To C1Dignosis.Rows.Count - 1
            lst = New myList
            Dim _Node As C1.Win.C1FlexGrid.Node
            _Node = C1Dignosis.Rows(i).Node
            If _Node.Level = 1 Then
                intUnits = C1Dignosis.GetData(i, Col_Units)
                If (intUnits > 999.9999) Then
                    C1Dignosis.Select(i, Col_Units, True)
                    Return False
                End If
            End If
        Next
        Return True
    End Function




    ''Added on 20140207-Added ICD revision 10
    Private Sub RbICD10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbICD10.CheckedChanged
        If RbICD10.Checked = True Then
            tlsbtnCodingRule.Visible = True
            tlsShowICD10Codes.Visible = False
            GloUC_trvICD.ImageIndex = 10
            GloUC_trvICD.SelectedImageIndex = 10
            RbICD10.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            Call FillICD9ICD10()
        Else
            tlsbtnCodingRule.Visible = False
            RbICD10.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub RbICD9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbICD9.CheckedChanged
        If RbICD9.Checked = True Then
            RbICD9.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            tlsShowICD10Codes.Visible = True
            GloUC_trvICD.ImageIndex = 0
            GloUC_trvICD.SelectedImageIndex = 0
            Me.ShowICD10Codes = False
            tlsbtnCodingRule.Visible = False
            Call FillICD9ICD10()
            pnlElementHost.Visible = False
            pnlElementHost.Dock = DockStyle.None
            splpnlElementHost.Visible = False
        Else
            RbICD9.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub GloUC_trvICD_SearchFired() Handles GloUC_trvICD.SearchFired
        If Me.ShowICD10Codes Then
            Me.ShowICD10Codes = False
        Else
            Call FillICD9ICD10()
        End If

    End Sub


    Private Sub tlsbtnCodingRule_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnCodingRule.Click

        Dim strConctICD9 As String = ""
        Dim arrstrConctICD9() As String = Nothing
        Dim strICDCode As String = ""
        Dim strICDDesc As String = ""

        If Not IsNothing(GloUC_trvICD.SelectedNode) Then

            strConctICD9 = GloUC_trvICD.SelectedNode.Text
            arrstrConctICD9 = Split(strConctICD9, "-", 2)
            strICDCode = arrstrConctICD9.GetValue(0)
            If (arrstrConctICD9.Length > 1) Then
                strICDDesc = arrstrConctICD9.GetValue(1)
            End If

            Dim objCodeRule As New gloUIControlLibrary.WPFForms.frmShowCodingRules(strICDCode.Trim(), strICDDesc, GetConnectionString())
            Dim interopHelper As New System.Windows.Interop.WindowInteropHelper(objCodeRule)
            interopHelper.Owner = Me.Handle

            objCodeRule.LoadNotes()

            If objCodeRule.NoData Then
                MessageBox.Show("No notes available for code " + strICDCode.Trim(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                objCodeRule.ShowDialog()
            End If

            If objCodeRule IsNot Nothing Then
                objCodeRule = Nothing
            End If

            If interopHelper IsNot Nothing Then
                interopHelper = Nothing
            End If
        Else
            MessageBox.Show("Please select ICD10 code or category to view coding rules.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub



    Private Sub tlsShowICD10Codes_Click(sender As System.Object, e As System.EventArgs) Handles tlsShowICD10Codes.Click
        Dim dtICD As DataTable = Nothing
        Dim objDiagnosisDBLayer As ClsDiagnosisDBLayer = Nothing
        Dim bResetFlag As Boolean = False
        Dim sICD9Code As String = String.Empty
        Try
            If GloUC_trvICD.SelectedNode IsNot Nothing AndAlso TypeOf (GloUC_trvICD.SelectedNode) Is gloUserControlLibrary.myTreeNode Then

                objDiagnosisDBLayer = New ClsDiagnosisDBLayer()
                dtICD = objDiagnosisDBLayer.GetICD9To10Mapping(CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode).Code.Replace(".", ""))
                sICD9Code = CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode).Code

                If dtICD.Rows.Count > 0 Then
                    Me.ShowICD10Codes = True

                    If GloUC_trvICD.txtsearch.Text.Length = 0 Then
                        bResetFlag = True
                    Else
                        GloUC_trvICD.txtsearch.Clear()
                    End If

                    tlsShowICD10Codes.Visible = False
                    tlsbtnCodingRule.Visible = True

                    GloUC_trvICD.ImageIndex = 10
                    GloUC_trvICD.SelectedImageIndex = 10

                    RemoveHandler RbICD10.CheckedChanged, AddressOf RbICD10_CheckedChanged
                    RemoveHandler RbICD9.CheckedChanged, AddressOf RbICD9_CheckedChanged

                    If btnPatientProblemDiagnosis.Tag = "Selected" Then
                        LayoutAllDiagnosis()
                    End If

                    RbICD10.Checked = True

                    FillICD9ICD10(dtICD)

                    If bResetFlag Then
                        Me.ShowICD10Codes = False
                    End If

                    AddHandler RbICD10.CheckedChanged, AddressOf RbICD10_CheckedChanged
                    AddHandler RbICD9.CheckedChanged, AddressOf RbICD9_CheckedChanged

                    If ICD9To10MappingControl Is Nothing Then
                        ICD9To10MappingControl = New gloUIControlLibrary.WPFUserControl.ICD10.ICD9To10MappingTextTreeView()
                        ICD9To10MappingDBLayer = New gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping.clsICD9To10MappingDBLayer(GetConnectionString())
                        AddHandler ICD9To10MappingControl.CodeSelected, AddressOf ICD10CodeSelected
                        elementHost.Child = ICD9To10MappingControl
                    End If

                    ICD9To10MappingControl.DataContext = ICD9To10MappingDBLayer.LoadMappingCodes(sICD9Code)
                    pnlElementHost.Visible = True
                    pnlElementHost.Dock = DockStyle.Bottom
                    splpnlElementHost.Visible = True

                Else
                    MessageBox.Show("No mapping found for ICD-9 code " + CType(GloUC_trvICD.SelectedNode, gloUserControlLibrary.myTreeNode).Code, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If ICD9To10MappingControl IsNot Nothing Then
                        ICD9To10MappingControl.DataContext = Nothing
                        pnlElementHost.Visible = False
                        pnlElementHost.Dock = DockStyle.None
                        splpnlElementHost.Visible = False
                    End If

                End If
            Else
                MessageBox.Show("Please select an ICD-9 code.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If dtICD IsNot Nothing Then
                dtICD.Dispose()
                dtICD = Nothing
            End If
        End Try
    End Sub

    Private Sub ICD10CodeSelected(ByVal Code As String)
        If GloUC_trvICD.Nodes IsNot Nothing Then
            For Each element As gloUserControlLibrary.myTreeNode In GloUC_trvICD.Nodes
                If element.Code.Replace(".", "").ToLower() = Code.ToLower() Then
                    GloUC_trvICD.SelectedNode = element
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub ChangeZOrderAndContextMenuStrip(ByRef thisControl As Control, ByVal thisContextMenu As ContextMenuStrip)
        ChangeZOrder()
        thisControl.ContextMenuStrip = thisContextMenu
        ChangeZOrder()
        '20-Jun-16 Aniket: Resolving Bug #97031: Background Printing:gloEMR:Application loose the focus .
        Me.Focus()
    End Sub
    Private Sub ChangeZOrder()
        If (IsNothing(Owner) = False) Then
            BringToFront()
            Owner.SendToBack()
            '20-Jun-16 Aniket: Resolving Bug #97031: Background Printing:gloEMR:Application loose the focus .
            BringToFront()
        Else
            BringToFront()
        End If
    End Sub
    Private Sub GloUC_trvICD_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvICD.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ChangeZOrderAndContextMenuStripForTreeview(GloUC_trvICD, e.Location)
        End If
    End Sub
    Private Sub ChangeZOrderAndContextMenuStripForTreeview(ByRef thisTreeView As gloUserControlLibrary.gloUC_TreeView, ByVal thisPoint As Point)
        If (IsNothing(thisTreeView.DisplayContextMenuStrip) = False) Then
            ChangeZOrder()
            thisTreeView.DisplayContextMenuStrip.Show(thisTreeView, thisPoint)
            thisTreeView.DisplayContextMenuStrip = Nothing
            ChangeZOrder()
        End If
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click

        pnlElementHost.Dock = DockStyle.None
        pnlElementHost.Visible = False
        splpnlElementHost.Visible = False

    End Sub


    Private Sub C1Dignosis_ValidateEdit(sender As System.Object, e As C1.Win.C1FlexGrid.ValidateEditEventArgs) Handles C1Dignosis.ValidateEdit
        If (e.Col = Col_Timed_PT Or e.Col = Col_UnTimed_PT) Then
            Try
                If Not String.IsNullOrEmpty(C1Dignosis.Editor.Text) Then
                    Dim val As Integer = Integer.Parse(C1Dignosis.Editor.Text)
                    If (val > 0 AndAlso val < 1000) Then
                        Return
                    Else
                        MessageBox.Show("Timed/Untimed Therapy should be less than 999 and greater than 0.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        RemoveHandler C1Dignosis.ValidateEdit, AddressOf C1Dignosis_ValidateEdit
                        C1Dignosis.Editor.Text = Nothing
                        AddHandler C1Dignosis.ValidateEdit, AddressOf C1Dignosis_ValidateEdit
                        e.Cancel = True
                    End If
                End If
            Catch
                MessageBox.Show("Timed/Untimed Therapy should be less than 999 and greater than 0.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RemoveHandler C1Dignosis.ValidateEdit, AddressOf C1Dignosis_ValidateEdit
                C1Dignosis.Editor.Text = Nothing
                AddHandler C1Dignosis.ValidateEdit, AddressOf C1Dignosis_ValidateEdit
                e.Cancel = True
            End Try
        End If
    End Sub
    'Added Code for getting PQRS Code from History
    Private Sub btnPQRSCodes_Click(sender As Object, e As System.EventArgs) Handles btnPQRSCodes.Click
        AddCPTsfromHistory()
    End Sub

    Private oRefusalListControl As gloListControl.gloListControl
    Private ofrmRefusalList As frmViewListControl


    Private Sub mnuRefusedselectedDiagnosis_Click(sender As System.Object, e As System.EventArgs) Handles mnuRefusedselectedDiagnosis.Click
        Try
            ofrmRefusalList = New frmViewListControl
            oRefusalListControl = New gloListControl.gloListControl(GetConnectionString(), gloListControl.gloListControlType.MUCQMRefusedCode, False, Me.Width)
            oRefusalListControl.ControlHeader = "Reason Concept Codes"
            'set the property true for refused code you want 
            oRefusalListControl.bShowNotTakenCodes = True
            oRefusalListControl.bShowAttributeCodes = True
            AddHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            AddHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            ofrmRefusalList.Controls.Add(oRefusalListControl)
            oRefusalListControl.Dock = DockStyle.Fill
            oRefusalListControl.BringToFront()

            oRefusalListControl.ShowHeaderPanel(False)
            'oDiagnosisListControl.OpenControl()
            ofrmRefusalList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ofrmRefusalList.Text = "Refused Reason"
            ofrmRefusalList.ShowDialog(IIf(IsNothing(CType(ofrmRefusalList, Control).Parent), Me, CType(ofrmRefusalList, Control).Parent))
         
            RemoveHandler oRefusalListControl.ItemSelectedClick, AddressOf oRefusalListControl_ItemSelectedClick
            RemoveHandler oRefusalListControl.ItemClosedClick, AddressOf oRefusalListControl_ItemClosedClick
            oRefusalListControl.Dispose()
            oRefusalListControl = Nothing


            If IsNothing(ofrmRefusalList) = False Then
                ofrmRefusalList.Dispose()
                ofrmRefusalList = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

       
    End Sub

    Private Sub oRefusalListControl_ItemClosedClick(sender As Object, e As EventArgs)
        ofrmRefusalList.Close()
    End Sub

    Private Sub oRefusalListControl_ItemSelectedClick(sender As Object, e As EventArgs)
        Try
            If oRefusalListControl.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oRefusalListControl.SelectedItems.Count - 1
                    Dim strCurrentLateralityCode As String = Convert.ToString(oRefusalListControl.SelectedItems(i).Code)
                    Dim strCurrentLateralityDesc As String = Convert.ToString(oRefusalListControl.SelectedItems(i).Description)

                    Dim selectedRow As Int16 = 0
                    With C1Dignosis
                        If .Row > 0 Then
                            Dim ofNode As C1.Win.C1FlexGrid.Node
                            ofNode = .Rows(.Row).Node
                            If ofNode.Level = 1 Then
                                selectedRow = .Row
                                .SetData(.Row, Col_ReasonConceptId, strCurrentLateralityCode)
                                .SetData(.Row, Col_ReasonConceptDescription, strCurrentLateralityDesc)
                            End If
                        End If
                    End With

                Next
                ofrmRefusalList.Close()
            Else
                ofrmRefusalList.Close()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
