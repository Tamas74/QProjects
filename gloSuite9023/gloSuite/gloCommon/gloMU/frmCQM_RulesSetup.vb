
Imports System.Windows.Forms.ListView
'Imports System.Windows
Imports System.Windows.Forms
Imports System.Threading.Tasks
Partial Public Class frmCQM_RulesSetup

    Inherits System.Windows.Forms.Form

#Region " Contants, Variables Declaration "

    Private m_CriteriaId As Int64
    Private m_CriteriaName As String
    Private blsModify As Boolean = False
    Private blsCopyRule As Boolean = False
    Private blnIsPatientCriteria As Boolean = False
    Private m_PatientID As Int64
    Private _sMessageBoxString As String = "gloEMR"

    Private COL_NAME As Integer = 0
    Private COL_ID As Integer = 1
    Private COL_TESTGROUPFLAG As Integer = 2
    Private COL_LEVELNO As Integer = 3
    Private COL_GROUPNO As Integer = 4
    Private COL_MINVALUE As Integer = 5
    Private COL_MAXVALUE As Integer = 6
    Private COL_IDENTITY As Integer = 7
    Private COL_COUNT As Integer = 8

    Private _selectednode As TreeNode
    'Private _selectedmynode As myTreeNode

    Private COL_TestID As Integer = 0
    Private COL_TestName As Integer = 1
    Private COL_ResultID As Integer = 2
    Private COL_Operator As Integer = 3
    Private COL_ResultValue1 As Integer = 4
    Private COL_ResultValue2 As Integer = 5
    Private COL_IDENTITYModule As Integer = 6
    Private COL_CountLab As Integer = 7
    Public _databaseConnectionString As String = String.Empty
    Dim dt As New DataTable
    Dim dtdrugs As New DataTable
    Private blnhistory As Boolean = False
    'Private _DMNode As myTreeNode = Nothing
    '   Dim objICD9AssociationDBLayer As ClsICD9AssociationDBLayer
    Dim dtICD9 As DataTable
    Dim dtCPT As DataTable
    Private Const strSortByCode As String = "CODE"
    Private Const strSortByDesc As String = "DESC"
    Dim strParentToAssociate As String = "Labs"
    Dim LoadFirst As Boolean = False
    Dim _IsValid As Boolean = True
    Dim _IsValidate As Boolean = True
    Dim blnloadIm As Boolean = False '' use when loading immunization to add all value in IM node
    Dim gstrMessageBoxCaption As String = "gloEMR"
    Public CMSNO As String = ""
    Public Delegate Sub DelShowHelp()
    Public Event EvtShowHelp As DelShowHelp
#End Region 'Contants, Variables Declaration '

#Region " Enumerations "

    Public Enum TabType
        None = 0
        Trigger = 1
        Exception = 2
        QuickOrders = 3
        ReferenceInfo = 4
    End Enum

    Private Enum ItemType
        None = 0
        ICD9 = 1
        CPT = 2
        Drugs = 3
        History = 4
        Lab = 5
        Order = 6
        Snomed = 7
        ICD10 = 8
        Insurance = 9
    End Enum

    Public Enum QuickOrderType
        None = 0
        LabOrders = 1
        RadiologyOrders = 2
        Referrals = 3
        RxDrugs = 4
        Guidelines = 5
        IM = 6
    End Enum

#End Region ' Enumerations '

#Region " Property Procedures "
    Private _ReportingYear As String = ""
    'Public Delegate Sub DelShowHelp(ByVal obj As Object)
    'Public Event EvtShowHelp As DelShowHelp
    Public Property ReportingYear As String
        Get
            Return _ReportingYear
        End Get
        Set(ByVal value As String)
            _ReportingYear = value
        End Set
    End Property
    'Public Property DMSelectedNode() As myTreeNode
    '    Get
    '        Return _DMNode
    '    End Get
    '    Set(ByVal value As myTreeNode)
    '        _DMNode = value
    '    End Set
    'End Property

    Dim _UserID As Int64 = 0

    Private _strUsername As String = ""
    Private _PopulationNo As Int16 = 0
    Public Property PopulationNo As Int16
        Get
            Return _PopulationNo
        End Get
        Set(value As Int16)
            _PopulationNo = value
        End Set
    End Property

    Public Property UserName As String
        Get
            Return _strUsername
        End Get
        Set(value As String)
            _strUsername = value
        End Set
    End Property
    Public Property UserID As Int64
        Get
            Return _UserID
        End Get
        Set(value As Int64)
            _UserID = value
        End Set
    End Property
#End Region ' Property Procedures ' 

#Region " Constructor Methods "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal IsModify As Boolean, ByVal CriteriaID As Int64, ByVal CriteriaName As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        blsModify = IsModify
        m_CriteriaId = CriteriaID
        m_CriteriaName = CriteriaName
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal IsCopy As Boolean, ByVal CriteriaID As Int64)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        blsCopyRule = IsCopy
        m_CriteriaId = CriteriaID
        m_CriteriaName = ""
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal IsPatientCriteria As Boolean)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        m_PatientID = PatientID
        blnIsPatientCriteria = IsPatientCriteria
    End Sub

#End Region ' Constructor Methods '

#Region " From laod and closing events "

    Private Sub frmDiseaseManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

      

        gloC1FlexStyle.Style(c1Labs)
        gloC1FlexStyle.Style(C1LabResult)

        '   btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
        btnDemographics.BackgroundImageLayout = ImageLayout.Stretch
        btnDemographics.Tag = "Selected"

     

        Try
            'Fill_Age()
            'fill_Maritalst()
            'fill_gender()
            'fill_race()
            'fill_state()
            'fill_EmpState()
            'FillCriteriaRefInfo()
            'FillAllCriteria()

          

            ' changeHeightAsPerResolution()







       
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CQMDetails()
        clsCQMMeasure._databaseConnectionString = _databaseConnectionString
        '  RemoveHandler cmb_measure.SelectedIndexChanged, AddressOf cmb_measure_SelectedIndexChanged
        CMSNO = GetCMSNo(CMSNO)
        Dim oParameters As New gloDatabaseLayer.DBParameters
        oParameters.Add("@cmsID", CMSNO, ParameterDirection.Input, SqlDbType.VarChar)
        'If ReportingYear = "2017" Then
        'oParameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
        'Else
        oParameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
        'End If

        Dim dtMeas As DataTable = clsCQMMeasure.GetdataWithParam(oParameters, "gsp_getAllCMSMeasure")
        If (Not IsNothing(dtMeas)) Then



            cmb_measure.DataSource = dtMeas
            cmb_measure.ValueMember = "CMSNumber"
            cmb_measure.DisplayMember = "Measure"
            '  reterivedata()
            '  AddHandler cmb_measure.SelectedIndexChanged, AddressOf cmb_measure_SelectedIndexChanged

            SetComboIndex(cmb_measure.DataSource)
            reterivedata()
        End If
    End Sub
    Private Sub ClearVitalsInfo()
        cmbAgeMin.Items.Clear()
        cmbAgeMax.Items.Clear()
        cmbAgeMaxMnth.Items.Clear()
        cmbAgeMinMnth.Items.Clear()
        txtBPsettingMax.Text = ""

        txtBPsettingMin.Text = ""
        txtBPsettingMaxTo.Text = ""

        txtBPsettingMinTo.Text = ""
        txtBPstandingMax.Text = ""
        txtBPstandingMin.Text = ""

        txtBPstandingMaxTo.Text = ""
        txtBPstandingMinTo.Text = ""
        txtBMImin.Text = ""
        txtBMImax.Text = ""
    End Sub
    Private Sub SetVitalsInfo(ByVal DtVitals As DataTable)

        If (DtVitals.Rows.Count > 0) Then
            Dim splagestr As String()

            splagestr = Convert.ToString(DtVitals.Rows(0)("MinAge")).Split(".")
            If (splagestr.Length > 0) Then
                cmbAgeMin.Items.Add(splagestr(0))
                cmbAgeMin.SelectedIndex = 0
            End If
            If (splagestr.Length > 1) Then
                If (splagestr(1).Replace("0", "").Trim().Length > 0) Then
                    cmbAgeMinMnth.Items.Add(splagestr(1))
                    cmbAgeMinMnth.SelectedIndex = 0
                End If
            End If
            Array.Resize(splagestr, 0)
            splagestr = Convert.ToString(DtVitals.Rows(0)("MaxAge")).Split(".")

            If (splagestr.Length > 0) Then
                'Dim cnt As Integer = Convert.ToInt32(splagestr(0))
                'If (cnt > 0) Then
                cmbAgeMax.Items.Add(splagestr(0))
                cmbAgeMax.SelectedIndex = 0
                'End If
            End If
            If (splagestr.Length > 1) Then
                If (splagestr(1).Replace("0", "").Trim().Length > 0) Then
                    cmbAgeMaxMnth.Items.Add(splagestr(1).Replace("0.00", ""))
                    cmbAgeMaxMnth.SelectedIndex = 0
                End If
            End If

            txtBPsettingMax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBPSitting"))

            txtBPsettingMin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBPSitting"))
            txtBPsettingMaxTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxTOBPSitting")).Replace("0.00", "")

            txtBPsettingMinTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinTOBPSitting")).Replace("0.00", "")
            txtBPstandingMax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBPStanding"))
            txtBPstandingMin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBPStanding"))

            txtBPstandingMaxTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxtoBPStanding")).Replace("0.00", "")
            txtBPstandingMinTo.Text = ChangeDefaultValues(DtVitals.Rows(0)("MintoBPStanding")).Replace("0.00", "")
            txtBMImin.Text = ChangeDefaultValues(DtVitals.Rows(0)("MinBMI"))
            txtBMImax.Text = ChangeDefaultValues(DtVitals.Rows(0)("MaxBMI"))

        End If
    End Sub
    Private Function ChangeDefaultValues(ByVal strvalue As Object) As String
        If (Convert.ToString(strvalue) = "-1.00") Then
            Return ""
        Else
            Return Convert.ToString(strvalue)
        End If
    End Function
    Private Sub SetComboIndex(ByVal dtdata As DataTable)

        'Dim splstr As String()
        For i As Integer = 0 To dtdata.Rows.Count - 1
            'splstr = Convert.ToString(dtdata.Rows(i)("CMSNumber")).Split("v")
            'If (splstr.Length > 1) Then
            '    If (splstr(0).Replace("CMS", "").Replace("NQF", "") = CMSNO) Then
            If Convert.ToString(dtdata.Rows(i)("CMSNumber")) = CMSNO Then
                cmb_measure.SelectedIndex = i
                Exit For
            End If
          
            '    End If
            'End If
        Next
    End Sub
    Private Function GetCMSNo(ByVal CMSNO As String) As String
        Dim splstr As String()
       
        Dim indcms As Integer = CMSNO.IndexOf("CMS")
        If (indcms = -1) Then
            indcms = CMSNO.IndexOf("NQF")
        End If
        CMSNO = CMSNO.Substring(indcms, CMSNO.Length - indcms)
        splstr = CMSNO.Split("/")
        If (splstr.Length = 1) Then
            splstr = CMSNO.Split(",")
        End If
        If (splstr.Length = 1) Then
            splstr = CMSNO.Split(":")
        End If
        If (splstr.Length > 1) Then
            If ((splstr(0).ToUpper() = "CMS") Or (splstr(0).ToUpper() = "NQF")) Then
                CMSNO = splstr(0) + splstr(1)
            Else
                CMSNO = splstr(0)
            End If
        End If
        CMSNO = CMSNO.Replace(":", "").Replace(" ", "").Replace(",", "").Trim()
        Return CMSNO.Replace("CMS", "").Replace("NQF", "")
    End Function
    'Private Sub changeHeightAsPerResolution()

    '    Dim myScreenHeight As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.99)
    '    If myScreenHeight < Me.Height Then
    '        Me.Height = myScreenHeight
    '    End If
    '    Dim myScreenWidth As Int32 = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.63)
    '    If myScreenWidth > Me.Width Then
    '        Me.Width = myScreenWidth
    '    End If
    'End Sub
    Private Sub frmDM_RulesSetup_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'Releasing all form level variables
           
            '_selectednode = Nothing
            ' _selectedmynode = Nothing

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            If Not IsNothing(dtdrugs) Then
                dtdrugs.Dispose()
                dtdrugs = Nothing
            End If

            '  _DMNode = Nothing

          

            If Not IsNothing(dtICD9) Then
                dtICD9.Dispose()
                dtICD9 = Nothing
            End If

            If Not IsNothing(dtCPT) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            lstVw_CPT.Items.Clear()
            lstVw_CVX.Items.Clear()
            lstVw_ICD9.Items.Clear()
            lstVw_ICD10.Items.Clear()
            lstVw_Drugs.Items.Clear()
            lstVw_Lab.Items.Clear()
            lstVw_SnoMed.Items.Clear()
        Catch ex As Exception
            'blank catch
        End Try
    End Sub

#End Region ' From laod and closing events '

#Region " Form main toolstrip item click event "

    Private Sub tlsDM_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDM.ItemClicked

        Select Case e.ClickedItem.Tag
            Case "Save"
                DialogResult = Windows.Forms.DialogResult.OK
                Call SaveCQMCriteria()
                Me.Close()
                '    tlsDM.Select()
                '    If _IsValid Then
                '        Call SaveCriteria()
                '    End If

                '    If (_IsValid) Then
                '        ' Me.Close()
                '        '  gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                '    Else
                '        _IsValid = True

                '    End If
            Case "Close"
                Me.Close()
                '  gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select

    End Sub

#End Region ' Form main toolstrip item click event '
    Private Sub SaveCQMCriteria()
        If (Not IsNothing(cmb_measure.DataSource)) Then
            Dim dtCqmMeasures = getallcodes()
            Dim oParameters As New gloDatabaseLayer.DBParameters
            oParameters.Add("@cmsID", cmb_measure.SelectedValue, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@nCreatedUseID", UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@sCreatedUserName", UserName, ParameterDirection.Input, SqlDbType.VarChar)
            oParameters.Add("@tvpCriteriaDetails", dtCqmMeasures, ParameterDirection.Input, SqlDbType.Structured)
            'If ReportingYear = "2017" Then
            oParameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
            'Else
            '    oParameters.Add("@ReportingYear", "", ParameterDirection.Input, SqlDbType.VarChar)
            'End If



            clsCQMMeasure.InsertdataWithParam(oParameters, "gsp_InupCQMCriteriaDetails")
        End If
    End Sub
    Private Function getallcodes() As DataTable
        Dim dtcqmCriteria = New DataTable()
        dtcqmCriteria.Columns.Add("CodeType", GetType(Int32))
        dtcqmCriteria.Columns.Add("CodeDescription", GetType(String))
        dtcqmCriteria.Columns.Add("Code", GetType(String))
        dtcqmCriteria.Columns.Add("CodeValue", GetType(String))


        AddItemsWithCodes(lstVw_ICD9, gloListControl.gloListControlType.CQMICD9, dtcqmCriteria)
        AddItemsWithCodes(lstVw_ICD10, gloListControl.gloListControlType.CQMICD10, dtcqmCriteria)



        AddItemsWithCodes(lstVw_SnoMed, gloListControl.gloListControlType.CQMSnomed, dtcqmCriteria)

        AddItemsWithCodes(lstVw_Lab, gloListControl.gloListControlType.CQMOrders, dtcqmCriteria)

        AddItemsWithCodes(lstVw_Drugs, gloListControl.gloListControlType.CQMRxNorm, dtcqmCriteria)

        AddItemsWithCodes(lstVw_CPT, gloListControl.gloListControlType.CQMHCPCS, dtcqmCriteria)
        AddItemsWithCodes(lstVw_CVX, gloListControl.gloListControlType.CQMCVX, dtcqmCriteria)
        Return dtcqmCriteria
    End Function
    Dim splstr As String()
    Private Sub AddItemsWithCodes(ByVal lst As ListView, ByVal Codetype As Integer, ByVal dtdata As DataTable)
        For Each item As ListViewItem In lst.Items
            splstr = item.Text.Split(":")
            If (splstr.Length > 1) Then
                Dim dr As DataRow = dtdata.NewRow()
                dr("CodeType") = Codetype
                dr("Code") = splstr(0)
                dr("CodeDescription") = splstr(1)
                dr("CodeValue") = ""
                dtdata.Rows.Add(dr)
            End If

        Next
    End Sub
#Region " Tree View Events "

    Private Sub trvHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistory.KeyPress

        If trvHistory.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvHistory.Select()
            End If
        End If

    End Sub

    Private Sub trvHistory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvHistory.KeyUp
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvLabs_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for back to search textbox.
            If e.KeyCode = Keys.Back Then
                txtLabsSearch.Select()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvGuideLineLeft_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
           
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvHistoryRight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvHistoryRight.KeyPress


        Try
            If e.KeyChar = Chr(13) Then


             
                ' ''
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If oNode IsNot Nothing Then
            '    oNode = Nothing
            'End If
            'If SelectedHistoryNode IsNot Nothing Then
            '    SelectedHistoryNode = Nothing
            'End If
        End Try

    End Sub

    Private Sub trvHistoryRight_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvHistoryRight.NodeMouseDoubleClick
        trvHistoryRight.SelectedNode = e.Node


        Try



            Dim CategoryFound As Boolean = False
            Dim HistoryFound As Boolean = False

            ''Selected Current Criteria
            'Dim thisNode As myTreeNode = CType(trvHistoryRight.SelectedNode, myTreeNode)
          
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If oNode IsNot Nothing Then
            '    oNode = Nothing
            'End If
            'If SelectedHistoryNode IsNot Nothing Then
            '    SelectedHistoryNode = Nothing
            'End If
        End Try

        ''
    End Sub

    Private Sub trOrderInfo_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
    
    End Sub

    Private Sub trvsubprb_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvsubprb.NodeMouseDoubleClick
        trvsubprb.SelectedNode = e.Node
        trvselectedprob.BeginUpdate()

    End Sub

    Private Sub trvfinprob_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvfinprob.NodeMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                With trvfinprob
                    Dim r As Integer = .HitTest(e.X, e.Y).Node.Index
                    If r >= 0 Then
                        trvfinprob.SelectedNode = trvfinprob.GetNodeAt(e.X, e.Y)
                        'Try
                        '    If (IsNothing(trvfinprob.ContextMenuStrip) = False) Then
                        '        trvfinprob.ContextMenuStrip.Dispose()
                        '        trvfinprob.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvfinprob.ContextMenuStrip = cntFindings
                    Else
                        'Try
                        '    If (IsNothing(trvfinprob.ContextMenuStrip) = False) Then
                        '        trvfinprob.ContextMenuStrip.Dispose()
                        '        trvfinprob.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvfinprob.ContextMenuStrip = Nothing
                    End If
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvSnowmedOff_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSnowmedOff.NodeMouseDoubleClick
        Dim SelectedProbNode As New TreeNode
        Dim oNode As TreeNode = Nothing
        
    End Sub

    Private Sub trvselectedprob_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedprob.MouseDown
        Dim trvnode As TreeNode = Nothing
        Try
            If Not IsNothing(trvselectedprob) Then
                trvnode = trvselectedprob.GetNodeAt(e.X, e.Y)
                trvselectedprob.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvnode) = False Then
                        If Not IsNothing(ContextMenuProblem) Then
                            If trvnode.Level > 0 Then
                                'Try
                                '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                                '        trvselectedprob.ContextMenuStrip.Dispose()
                                '        trvselectedprob.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedprob.ContextMenuStrip = ContextMenuProblem
                            Else
                                'Try
                                '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                                '        trvselectedprob.ContextMenuStrip.Dispose()
                                '        trvselectedprob.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedprob.ContextMenuStrip = Nothing
                            End If
                        Else
                            'Try
                            '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                            '        trvselectedprob.ContextMenuStrip.Dispose()
                            '        trvselectedprob.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselectedprob.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselectedprob.ContextMenuStrip) = False) Then
                        '        trvselectedprob.ContextMenuStrip.Dispose()
                        '        trvselectedprob.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedprob.ContextMenuStrip = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If trvnode IsNot Nothing Then
                trvnode = Nothing
            End If
        End Try
    End Sub

    Private Sub trvselectedhist_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedhist.MouseDown
        Dim trvnode As TreeNode = Nothing
        Try
            If Not IsNothing(trvselectedhist) Then
                trvnode = trvselectedhist.GetNodeAt(e.X, e.Y)
                trvselectedhist.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvnode) = False Then
                        If Not IsNothing(ContextMenuProblem) Then
                            If trvnode.Level > 0 Then
                                'Try
                                '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                                '        trvselectedhist.ContextMenuStrip.Dispose()
                                '        trvselectedhist.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedhist.ContextMenuStrip = ContextMenuHistory
                            Else
                                'Try
                                '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                                '        trvselectedhist.ContextMenuStrip.Dispose()
                                '        trvselectedhist.ContextMenuStrip = Nothing
                                '    End If
                                'Catch ex As Exception

                                'End Try
                                trvselectedhist.ContextMenuStrip = Nothing
                            End If
                        Else
                            'Try
                            '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                            '        trvselectedhist.ContextMenuStrip.Dispose()
                            '        trvselectedhist.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselectedhist.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselectedhist.ContextMenuStrip) = False) Then
                        '        trvselectedhist.ContextMenuStrip.Dispose()
                        '        trvselectedhist.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedhist.ContextMenuStrip = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If trvnode IsNot Nothing Then
                trvnode = Nothing
            End If
        End Try
    End Sub

#End Region ' Tree View Events '

#Region " Textbox Events "

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'Try

        '    If txtSearch.Text.Trim <> "" Then
        '        If cmbHistoryCategory.Text = "Allergies" Then
        '            For Each oNode As TreeNode In trvHistoryRight.Nodes
        '                Dim NodeText As String = UCase(oNode.Text)
        '                If NodeText.Contains(UCase(txtSearch.Text.Trim)) Then
        '                    trvHistoryRight.SelectedNode = oNode
        '                    txtSearch.Focus()
        '                    Exit Sub
        '                Else
        '                    trvHistoryRight.SelectedNode = Nothing
        '                End If
        '            Next

        '            'ElseIf gblnCodedHistory = True Then
        '            '    Search(txtSearch.Text, dt)
        '            ''if Flag  gblnCodedHistory is false
        '        Else
        '            For Each oNode As TreeNode In trvHistoryRight.Nodes
        '                Dim NodeText As String = UCase(oNode.Text)
        '                If NodeText.Contains(UCase(txtSearch.Text.Trim)) Then
        '                    trvHistoryRight.SelectedNode = oNode
        '                    txtSearch.Focus()
        '                    Exit Sub
        '                Else
        '                    trvHistoryRight.SelectedNode = Nothing
        '                End If
        '            Next
        '        End If
        '    End If

        '    'If search text box is empty 
        '    If (txtSearch.Text.Trim = "") Then
        '        Fill_Histories(cmbHistoryCategory.Text)
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp

    End Sub

    Private Sub txtLabsSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabsSearch.KeyUp
        'Try
        '    ' for focus select tree view
        '    If e.KeyCode = Keys.Enter Then
        '        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
        '            c1Labs.Select()
        '        End If
        '        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '            c1Labs_Ex.Select()
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtGuideLineSeach_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            ' for focus select tree view
            'If e.KeyCode = Keys.Enter Then
            '    trOrderInfo.Select()
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtLabResultSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLabResultSearch.KeyUp
        Try
            ' for focus select tree view
            'If e.KeyCode = Keys.Enter Then
            '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            '        C1LabResult.Select()
            '    End If
            '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '        C1LabResult_Ex.Select()
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region ' Textbox Events '

#Region " Combobox events "

    Private Sub cmbHistoryCategory_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHistoryCategory.SelectionChangeCommitted
        Fill_Histories_1(cmbHistoryCategory.Text)
    End Sub

    Private Sub cmbHistoryCategory_Ex_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''  Fill_Histories_1_Ex(cmbHistoryCategory_Ex.Text)
    End Sub

    Private Sub cmbhistsnomed_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbhistsnomed.SelectionChangeCommitted
        txtsrchprb.Text = ""
        txtsrchprb.Focus()
        trvfinprob.Nodes.Clear()
        trvsubprb.Nodes.Clear()
    End Sub

#End Region ' Combobox events '

#Region " Context menu events "

    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        'Dim oCurrentMenu As MenuItem = CType(sender, MenuItem)
        '' Dim TemplateID As Int64
        'Try
        '    If (trOrderInfo.SelectedNode.Level <> 0) Then ''Sandip Darade 20090309


        '        If oCurrentMenu.Tag = "DeleteItem" Then
        '            Dim mychildnode As myTreeNode
        '            mychildnode = CType(trOrderInfo.SelectedNode, myTreeNode)
        '            If Not IsNothing(mychildnode) Then
        '                'If child nodes are more than one delete only the selected item
        '                If mychildnode.Parent.Nodes.Count > 0 Then
        '                    mychildnode.Remove()
        '                End If
        '                If (trOrderInfo.SelectedNode.Level = 0) Then
        '                    'Try
        '                    '    If (IsNothing(trOrderInfo.ContextMenu) = False) Then
        '                    '        trOrderInfo.ContextMenu.Dispose()
        '                    '        trOrderInfo.ContextMenu = Nothing
        '                    '    End If
        '                    'Catch ex As Exception

        '                    'End Try
        '                    trOrderInfo.ContextMenu = Nothing
        '                End If
        '            End If
        '        ElseIf oCurrentMenu.Tag = "EditTemplate" Then
        '            'UpdateTemplate(CType(trOrderInfo.SelectedNode, myTreeNode))
        '        ElseIf oCurrentMenu.Tag = "EditTemplateTrigger" Then
        '            ' UpdateTemplate(CType(trOrderInfo.SelectedNode, myTreeNode))
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "DM Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oCurrentMenu = Nothing
        'End Try

    End Sub

    Private Sub mnu_AddFindings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnu_AddFindings.Click
        Dim oNode As TreeNode = trvfinprob.SelectedNode
        If Not IsNothing(oNode) Then
            If blnhistory = False Then '' for problem it is checked  
                Try
                    trvselectedprob.Visible = True
                    trvselectedhist.Visible = False
                    trvselectedprob.BeginUpdate()
                    Dim SelectedHistoryNode As New TreeNode

                    SelectedHistoryNode = oNode.Clone
                    SelectedHistoryNode.ImageIndex = 11
                    SelectedHistoryNode.SelectedImageIndex = 11
                    Dim CategoryFound As Boolean = False
                    Dim HistoryFound As Boolean = False

                    'Selected Current Criteria
                    For Each CategoryNode As TreeNode In trvselectedprob.Nodes
                        For Each HistoryNode As TreeNode In CategoryNode.Nodes
                            If HistoryNode.Text = SelectedHistoryNode.Text Then
                                HistoryFound = True
                                Exit For
                            End If
                        Next
                        If Not HistoryFound Then
                            CategoryNode.ImageIndex = 0
                            CategoryNode.SelectedImageIndex = 0
                            CategoryNode.Nodes.Add(SelectedHistoryNode)
                            CategoryNode.Expand()
                            trvselectedprob.Sort()
                        End If
                        CategoryFound = True
                        Exit For
                    Next

                    If Not CategoryFound Then
                        oNode = New TreeNode
                        oNode.Text = cmbHistoryCategory.Text
                        oNode.Tag = cmbHistoryCategory.SelectedValue
                        oNode.ImageIndex = 0
                        oNode.SelectedImageIndex = 0
                        oNode.Nodes.Add(SelectedHistoryNode)
                        trvselectedprob.Nodes.Add(oNode)
                        trvselectedprob.ExpandAll()
                        oNode = Nothing
                        trvselectedprob.Sort()
                    End If
                Catch ex As Exception

                Finally
                    trvselectedprob.EndUpdate()
                End Try

            Else
                'for history 
                trvselectedprob.Visible = False
                trvselectedhist.Visible = True
                Try
                    trvselectedhist.BeginUpdate()
                    Dim SelectedHistoryNode As New TreeNode

                    SelectedHistoryNode = oNode.Clone
                    SelectedHistoryNode.ImageIndex = 11
                    SelectedHistoryNode.SelectedImageIndex = 11
                    Dim CategoryFound As Boolean = False
                    Dim HistoryFound As Boolean = False

                    'Selected Current Criteria
                    For Each CategoryNode As TreeNode In trvselectedhist.Nodes
                        If CategoryNode.Text = cmbhistsnomed.Text Then
                            For Each HistoryNode As TreeNode In CategoryNode.Nodes
                                If HistoryNode.Text = SelectedHistoryNode.Text Then
                                    HistoryFound = True
                                    Exit For
                                End If
                            Next
                            If Not HistoryFound Then
                                CategoryNode.ImageIndex = 0
                                CategoryNode.SelectedImageIndex = 0
                                CategoryNode.Nodes.Add(SelectedHistoryNode)
                                CategoryNode.Expand()
                                trvselectedhist.Sort()
                            End If
                            CategoryFound = True
                            Exit For
                        End If
                    Next

                    If Not CategoryFound Then
                        oNode = New TreeNode
                        oNode.Text = cmbhistsnomed.Text
                        oNode.Tag = cmbhistsnomed.SelectedValue
                        oNode.ImageIndex = 0
                        oNode.SelectedImageIndex = 0
                        oNode.Nodes.Add(SelectedHistoryNode)
                        trvselectedhist.Nodes.Add(oNode)
                        trvselectedhist.ExpandAll()
                        oNode = Nothing
                        trvselectedhist.Sort()
                    End If
                Catch ex As Exception

                Finally
                    trvselectedhist.EndUpdate()
                End Try
            End If
        End If
    End Sub

#End Region ' Context menu events '

#Region " Checkbox control events "

    Private Sub chckRecurring_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '   pnlloadcqmdata.Enabled = chckRecurring.Checked
    End Sub

#End Region ' Checkbox control events '

#Region " Public and Private Methods "

    Public Sub Search(ByVal strsearch As String, ByVal dt As DataTable)
        'Try
        '    Dim i As Integer
        '    Dim tdt As DataTable
        '    Dim dv As DataView

        '    If (IsNothing(dt)) Then
        '        Exit Sub
        '    End If
        '    Dim strsearchHistory As String
        '    If Trim(strsearch) <> "" Then
        '        strsearchHistory = Replace(strsearch, "'", "''")
        '        strsearchHistory = Replace(strsearchHistory, "[", "") & ""
        '        '  strsearchHistory = mdlGeneral.ReplaceSpecialCharacters(strsearchHistory)
        '    Else
        '        strsearchHistory = ""
        '    End If
        '    'tdt = New DataTable
        '    'dv = New DataView

        '    dv = New DataView(dt)
        '    strsearch = trvHistoryRight.Text.Trim
        '    ''Appply filter to the dataview
        '    dv.RowFilter = dt.Columns(1).ColumnName & " Like '%" & strsearchHistory & "%'"


        '    tdt = dv.ToTable
        '    trvHistoryRight.Nodes.Clear()

        '    Dim oNode As myTreeNode
        '    If strsearch <> "" Then
        '        For Each dtrow As DataRow In tdt.Rows
        '            oNode = New myTreeNode
        '            oNode.Text = tdt.Rows(i)(0)
        '            oNode.Tag = cmbHistoryCategory.Text
        '            oNode.Key = tdt.Rows(i)(1)
        '            trvHistoryRight.Nodes.Add(oNode)
        '            oNode = Nothing
        '        Next dtrow
        '    Else
        '        For i = 0 To tdt.Rows.Count - 1
        '            oNode = New myTreeNode
        '            oNode.Text = tdt.Rows(i)("Column1")
        '            oNode.Tag = cmbHistoryCategory.Text
        '            oNode.Key = tdt.Rows(i)("ICD9ID")
        '            trvHistoryRight.Nodes.Add(oNode)
        '            oNode = Nothing
        '        Next
        '    End If
        '    dv.Dispose()
        '    dv = Nothing
        '    tdt.Dispose()
        '    tdt = Nothing
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub SetCombiIndex(ByVal ControlCombo As ComboBox)
        For i As Integer = 0 To ControlCombo.Items.Count - 1
            If ControlCombo.Text = ControlCombo.Items(i) Then
                ControlCombo.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub c1Labs_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1Labs.AfterEdit
        Try
            With c1Labs
                'If .GetCellCheck(.Row, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                '    .SetData(.Row, COL_MINVALUE, Nothing)
                '    .SetData(.Row, COL_MAXVALUE, Nothing)
                'End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateTemplate(ByVal ID As Int64)

        ' Dim objTemplateGallery As New clsTemplateGallery
        'Dim objfrmTemplateGallery As frmTemplateGallery
        'Try

        '    blsModify = True

        '    objfrmTemplateGallery = New frmTemplateGallery(ID)
        '    With objfrmTemplateGallery
        '        .Text = "Modify Template"
        '        .MdiParent = Me.ParentForm
        '        .Show()
        '        .WindowState = FormWindowState.Maximized
        '        .BringToFront()
        '    End With

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
        '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
        'End Try
    End Sub

    Private Sub UpdateTemplate(ByVal TemplateName As String)

        'Dim objfrmTemplateGallery As frmTemplateGallery

        'Try
        '    blsModify = True

        '    objfrmTemplateGallery = New frmTemplateGallery(m_CriteriaId, TemplateName)
        '    With objfrmTemplateGallery
        '        .Text = "Modify Template"
        '        .MdiParent = Me.ParentForm
        '        .Show()
        '        .WindowState = FormWindowState.Maximized
        '        .BringToFront()
        '    End With

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
        '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
        'End Try
    End Sub

    'Private Sub UpdateTemplate(ByVal mySelectedNode As myTreeNode)

    'Dim objfrmTemplateGallery As frmTemplateGallery

    'Try
    '    blsModify = True
    '    objfrmTemplateGallery = New frmTemplateGallery(True)
    '    Me.DMSelectedNode = mySelectedNode
    '    With objfrmTemplateGallery
    '        .DMSelectedNode = mySelectedNode
    '        .Text = "Modify Template"
    '        .MdiParent = Me.ParentForm
    '        .Show()
    '        .WindowState = FormWindowState.Maximized
    '        .BringToFront()
    '        mySelectedNode = .DMSelectedNode
    '    End With

    'Catch ex As Exception
    '    MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
    '    CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
    'End Try
    '  End Sub

    Public Function GetValues(ByVal id As Int64, ByVal min As Boolean) As String
        'Dim _strSQL As String = ""
        'Dim _Result As String = ""
        'Dim oDB As New gloStream.gloDataBase.gloDataBase
        'Try
        '    'Criteria Master Record
        '    If (min) Then
        '        _strSQL = " select dm_mst_ageMin from DM_Criteria_MST Where dm_mst_id = " & id & ""
        '    Else
        '        _strSQL = " select dm_mst_AgeMax from DM_Criteria_MST Where dm_mst_id = " & id & ""
        '    End If

        '    oDB.Connect(GetConnectionString)
        '    _Result = oDB.ExecuteQueryScaler(_strSQL)
        '    oDB.Disconnect()
        '    Return _Result
        'Catch
        Return Nothing
        'End Try
    End Function

#End Region ' Public and Private Methods '

#Region " Start: ICD9, CPT, History, Orders, Lab, Drugs, SNOMED select button click Events "

    'Quick Orders tab

    Private Sub btnRadiologyTest_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(4)
        'strParentToAssociate = btnRadiologyTest.Text
    End Sub

    Private Sub btnGuideline_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(5)
        'strParentToAssociate = btnGuideline.Text
    End Sub

    Private Sub btnRx_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(3)
        'strParentToAssociate = btnRx.Text
    End Sub

    Private Sub btnReferrals_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(2)
        'strParentToAssociate = btnReferrals.Text
    End Sub

    Private Sub btnLab_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(1)
        'strParentToAssociate = btnLab.Text
    End Sub

    Private Sub btnIM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Integrated by chetan as on 11 oct 2010  - for IM in DM Setup
        'txtSearchOrder.Text = ""
        'PopulateAssocaitedInfo(6)
        'strParentToAssociate = btnIM.Text
    End Sub

    Private Sub btnLabResultClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabResultClear.Click
        'USE TO CLEAR SEARCH TEXT BOX
        txtLabResultSearch.ResetText()
        txtLabResultSearch.Focus()
        C1LabResult.Select(1, 0, 1, 0, True)
    End Sub

    Private Sub btnLabClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabClear.Click
        'USE TO CLEAR SEARCH TEXT BOX
        txtLabsSearch.ResetText()
        txtLabsSearch.Focus()
        c1Labs.Select(1, 0, 1, 0, True)
    End Sub

    Private Sub btnLabResultClear_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'USE TO CLEAR SEARCH TEXT BOX
        'txtLabResultSearch_Ex.ResetText()
        'txtLabResultSearch_Ex.Focus()
        'C1LabResult_Ex.Select(1, 0, 1, 0, True)
    End Sub

    Private Sub btnLabClear_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'USE TO CLEAR SEARCH TEXT BOX
        'txtLabsSearch_Ex.ResetText()
        'txtLabsSearch_Ex.Focus()
        'c1Labs_Ex.Select(1, 0, 1, 0, True)
    End Sub

#End Region ' End: ICD9, CPT, History, Orders, Lab, Drugs, SNOMED select button click Events '

#Region " Start: Internal Toolstrip Save/Done Events "

    Private Sub tsBtn_SaveRadiology_Click_OLD()

    End Sub

    Private Sub tsBtn_SaveLab_Click_OLD()
        Dim _listviewItem As ListViewItem = Nothing
        Try
            lstVw_Lab.Items.Clear()
            txtLabResultSearch.Text = ""
            For i As Integer = 1 To C1LabResult.Rows.Count - 1
                If C1LabResult.GetCellCheck(i, COL_TestName) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    With C1LabResult
                        _listviewItem = New ListViewItem(C1LabResult.GetData(i, COL_TestName).ToString() + " " + Convert.ToString(C1LabResult.GetData(i, COL_ResultValue1)) + " " + Convert.ToString(C1LabResult.GetData(i, COL_Operator)) + " " + Convert.ToString(C1LabResult.GetData(i, COL_ResultValue1)))
                        lstVw_Lab.Items.Add(_listviewItem)
                        _listviewItem = Nothing
                    End With
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlLab.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

    Private Sub ts_Btn_SaveRadiology_Ex_Click_OLD()

    End Sub

    Private Sub tsBtn_SaveLab_Ex_Click_OLD()

    End Sub

#End Region 'End: Internal Toolstrip Save/Done Events'

#Region " Start: ICD9, CPT, History, Orders, Lab, Drugs, Insurance remove selected item events and method "


#End Region ' Start: ICD9, CPT, History, Orders, Lab, Drugs remove selected item events and method '

#Region "Start: TreeView/C1FlexGrid internal selection list user control events (mouse double click\key press) "

    'CPT - Trigger's Tab
    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick
        CPT_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress
        If e.KeyChar = Chr(13) Then
            CPT_Select_Event(TabType.Trigger, CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode))
        End If
    End Sub

    'Insurance - Trigger's Tab
    Private Sub GloUC_trvInsurance_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvInsurance.KeyPress
        Insurance_Select_Event(TabType.Trigger, CType(GloUC_trvInsurance.SelectedNode, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvInsurance_NodeMouseDoubleClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvInsurance.NodeMouseDoubleClick
        Insurance_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub
    'Drugs - Trigger's Tab
    Private Sub GloUC_trvDrugs_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvDrugs.NodeMouseDoubleClick
        Drugs_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvDrugs.KeyPress
        If e.KeyChar = Chr(13) Then
            Drugs_Select_Event(TabType.Trigger, CType(GloUC_trvDrugs.SelectedNode, gloUserControlLibrary.myTreeNode))
        End If
    End Sub

    'ICD9 - Trigger's Tab
    Private Sub GloUC_trvICD9_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvICD9.NodeMouseDoubleClick
        ICD9_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvICD10_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvICD10.NodeMouseDoubleClick
        ICD10_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvICD9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvICD9.KeyPress
        If e.KeyChar = Chr(13) Then
            ICD9_Select_Event(TabType.Trigger, CType(GloUC_trvICD9.SelectedNode, gloUserControlLibrary.myTreeNode))
        End If
    End Sub

    Private Sub GloUC_trvICD10_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvICD10.KeyPress
        If e.KeyChar = Chr(13) Then
            ICD10_Select_Event(TabType.Trigger, CType(GloUC_trvICD10.SelectedNode, gloUserControlLibrary.myTreeNode))
        End If
    End Sub


    'History - Trigger's Tab
    Private Sub GloUC_trvHistory_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvHistory.NodeMouseDoubleClick
        History_Select_Event(TabType.Trigger, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvHistory_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvHistory.KeyPress
        If e.KeyChar = Chr(13) Then
            History_Select_Event(TabType.Trigger, CType(GloUC_trvHistory.SelectedNode, gloUserControlLibrary.myTreeNode))
        End If
    End Sub

    'CPT - Exceptions Tab
    Private Sub GloUC_trvCPT_Ex_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        CPT_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvCPT_Ex_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then
        '    CPT_Select_Event(TabType.Exception, CType(GloUC_trvCPT_Ex.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub
    'Insurance - Exceptions Tab
    Private Sub GloUC_trvInsurance_Ex_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        ' Insurance_Select_Event(TabType.Exception, CType(GloUC_trvInsurance_Ex.SelectedNode, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvInsurance_Ex_NodeMouseDoubleClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        ' Insurance_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    'Drugs - Exceptions Tab
    Private Sub GloUC_trvDrugs_Ex_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        Drugs_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvDrugs_Ex_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then
        '    Drugs_Select_Event(TabType.Exception, CType(GloUC_trvDrugs_Ex.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub

    'ICD9 - Exceptions Tab
    Private Sub GloUC_trvICD9_Ex_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        ICD9_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvICD9_Ex_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If e.KeyChar = Chr(13) Then
        '    ICD9_Select_Event(TabType.Exception, CType(GloUC_trvICD9_Ex.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub

    'ICD10 - Exceptions Tab
    Private Sub GloUC_trvICD10_Ex_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        ICD10_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvICD10_Ex_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then
        '    ICD10_Select_Event(TabType.Exception, CType(GloUC_trvICD10_Ex.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub

    'History - Exceptions Tab
    Private Sub GloUC_TrvHistoryEx_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        History_Select_Event(TabType.Exception, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_TrvHistoryEx_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then
        '    History_Select_Event(TabType.Exception, CType(GloUC_TrvHistoryEx.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub

    'Associates - Trigger's Tab
    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        Associates_Select_Event(TabType.QuickOrders, CType(e.Node, gloUserControlLibrary.myTreeNode))
    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Then
        '    Associates_Select_Event(TabType.QuickOrders, CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode))
        'End If
    End Sub

#End Region 'End: TreeView/C1FlexGrid internal selection list user control events (mouse double click\key press)'

#Region " CPT, Drugs, Insurance, ICD9, History, Associates (QuickOrder) selection functions "

    Private Function CPT_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)

        ' Dim _myTreeNode As New myTreeNode
        'Dim Ispresent As Boolean = False

        'Try

        '    If Not IsNothing(oNode) AndAlso Tab <> TabType.None Then

        '        ' _myTreeNode.Text = oNode.Text
        '        ' _myTreeNode.Tag = oNode.Code
        '        ' _myTreeNode.DrugName = oNode.Description

        '        If TabType.Trigger = Tab Then

        '            For Each myDNode As TreeNode In trvselectedCPT.Nodes
        '                If myDNode.Text.Replace(" ", "") = _myTreeNode.Text.Replace(" ", "") Then
        '                    Ispresent = True
        '                    Exit For
        '                End If
        '            Next

        '            If Ispresent = False Then
        '                trvselectedCPT.Nodes.Add(_myTreeNode)
        '                _myTreeNode.ImageIndex = 0
        '                _myTreeNode.SelectedImageIndex = 0
        '            End If
        '            trvselectedCPT.ExpandAll()

        '        ElseIf TabType.Exception = Tab Then

        '            'For Each myDNode As TreeNode In trvselectedCPT_Ex.Nodes
        '            '    If myDNode.Text.Replace(" ", "") = _myTreeNode.Text.Replace(" ", "") Then
        '            '        Ispresent = True
        '            '        Exit For
        '            '    End If
        '            'Next

        '            'If Ispresent = False Then
        '            '    trvselectedCPT_Ex.Nodes.Add(_myTreeNode)
        '            '    _myTreeNode.ImageIndex = 0
        '            '    _myTreeNode.SelectedImageIndex = 0
        '            'End If
        '            'trvselectedCPT_Ex.ExpandAll()

        '        End If

        '    End If 'Ending ...If Not IsNothing(oNode) Then

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    _myTreeNode = Nothing
        'End Try
        Return Nothing
    End Function

    Private Function Insurance_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)

        'Dim oNodeToAdd As New myTreeNode
        'Dim Ispresent As Boolean = False

        'Try
        '    If Not IsNothing(oNode) AndAlso Tab <> TabType.None Then

        '        oNodeToAdd.Key = oNode.ID
        '        oNodeToAdd.Tag = oNode.ID
        '        oNodeToAdd.Text = oNode.Text
        '        oNodeToAdd.DrugName = oNode.Code
        '        oNodeToAdd.Dosage = oNode.Description
        '        oNodeToAdd.DrugForm = oNode.DrugForm
        '        oNodeToAdd.Route = oNode.Route
        '        oNodeToAdd.Frequency = oNode.Frequency
        '        oNodeToAdd.NDCCode = oNode.NDCCode
        '        oNodeToAdd.IsNarcotics = oNode.IsNarcotics
        '        oNodeToAdd.Duration = oNode.Duration

        '        oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier


        '        If Tab = TabType.Trigger Then

        '            For Each myDNode As TreeNode In trvSelectedInsurance.Nodes
        '                If myDNode.Text = oNodeToAdd.Text Then
        '                    Ispresent = True
        '                    Exit For
        '                End If
        '            Next
        '            If Ispresent = False Then
        '                trvSelectedInsurance.Nodes.Add(oNodeToAdd)
        '                oNodeToAdd.ImageIndex = 0
        '                oNodeToAdd.SelectedImageIndex = 0
        '            End If
        '            trvSelectedInsurance.ExpandAll()

        '        ElseIf Tab = TabType.Exception Then

        '            'For Each myDNode As TreeNode In trvSelectedInsurance_Ex.Nodes
        '            '    If myDNode.Text = oNodeToAdd.Text Then
        '            '        Ispresent = True
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If Ispresent = False Then
        '            '    trvSelectedInsurance_Ex.Nodes.Add(oNodeToAdd)
        '            '    oNodeToAdd.ImageIndex = 0
        '            '    oNodeToAdd.SelectedImageIndex = 0
        '            'End If
        '            'trvSelectedInsurance_Ex.ExpandAll()

        '        End If

        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    oNodeToAdd = Nothing
        'End Try
        Return Nothing
    End Function

    Private Function Drugs_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)

        'Dim oNodeToAdd As New myTreeNode
        'Dim Ispresent As Boolean = False

        'Try
        '    If Not IsNothing(oNode) AndAlso Tab <> TabType.None Then

        '        oNodeToAdd.Key = oNode.ID
        '        oNodeToAdd.Tag = oNode.ID
        '        oNodeToAdd.Text = oNode.Text
        '        oNodeToAdd.DrugName = oNode.Code
        '        oNodeToAdd.Dosage = oNode.Description
        '        oNodeToAdd.DrugForm = oNode.DrugForm
        '        oNodeToAdd.Route = oNode.Route
        '        oNodeToAdd.Frequency = oNode.Frequency
        '        oNodeToAdd.NDCCode = oNode.NDCCode
        '        oNodeToAdd.IsNarcotics = oNode.IsNarcotics
        '        oNodeToAdd.Duration = oNode.Duration
        '        oNodeToAdd.mpid = oNode.mpid
        '        oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier


        '        If Tab = TabType.Trigger Then

        '            For Each myDNode As TreeNode In trvSelectedDrugs.Nodes
        '                If myDNode.Text = oNodeToAdd.Text Then
        '                    Ispresent = True
        '                    Exit For
        '                End If
        '            Next
        '            If Ispresent = False Then
        '                trvSelectedDrugs.Nodes.Add(oNodeToAdd)
        '                oNodeToAdd.ImageIndex = 0
        '                oNodeToAdd.SelectedImageIndex = 0
        '            End If
        '            trvSelectedDrugs.ExpandAll()

        '        ElseIf Tab = TabType.Exception Then

        '            'For Each myDNode As TreeNode In trvSelectedDrugs_Ex.Nodes
        '            '    If myDNode.Text = oNodeToAdd.Text Then
        '            '        Ispresent = True
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If Ispresent = False Then
        '            '    trvSelectedDrugs_Ex.Nodes.Add(oNodeToAdd)
        '            '    oNodeToAdd.ImageIndex = 0
        '            '    oNodeToAdd.SelectedImageIndex = 0
        '            'End If
        '            'trvSelectedDrugs_Ex.ExpandAll()

        '        End If

        '    End If  'Ending ...If Not IsNothing(oNode) Then

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    oNodeToAdd = Nothing
        'End Try
        Return Nothing
    End Function

    Private Function ICD9_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)

        'Dim oNodeToAdd As New myTreeNode
        'Dim Ispresent As Boolean = False

        'Try

        '    If Not IsNothing(oNode) AndAlso Tab <> TabType.None Then

        '        oNodeToAdd.Key = oNode.ID
        '        oNodeToAdd.Text = oNode.Text
        '        oNodeToAdd.Tag = oNode.Code.Trim
        '        oNodeToAdd.DrugName = oNode.Description.Trim

        '        If Tab = TabType.Trigger Then

        '            For Each myDNode As TreeNode In trvselecteICDs.Nodes
        '                If myDNode.Text = oNodeToAdd.Text Then
        '                    Ispresent = True
        '                    Exit For
        '                End If
        '            Next
        '            If Ispresent = False Then
        '                trvselecteICDs.Nodes.Add(oNodeToAdd)
        '                oNodeToAdd.ImageIndex = 0
        '                oNodeToAdd.SelectedImageIndex = 0
        '            End If
        '            trvselecteICDs.ExpandAll()

        '        ElseIf Tab = TabType.Exception Then

        '            'For Each myDNode As TreeNode In trvselectedICDs_Ex.Nodes
        '            '    If myDNode.Text = oNodeToAdd.Text Then
        '            '        Ispresent = True
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If Ispresent = False Then
        '            '    trvselectedICDs_Ex.Nodes.Add(oNodeToAdd)
        '            '    oNodeToAdd.ImageIndex = 0
        '            '    oNodeToAdd.SelectedImageIndex = 0
        '            'End If
        '            'trvselectedICDs_Ex.ExpandAll()

        '        End If

        '    End If 'Ending...If Not IsNothing(oNode) Then

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    oNodeToAdd = Nothing
        'End Try
        Return Nothing
    End Function

    Private Function ICD10_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)

        'Dim oNodeToAdd As New myTreeNode
        'Dim Ispresent As Boolean = False

        'Try

        '    If Not IsNothing(oNode) AndAlso Tab <> TabType.None Then

        '        oNodeToAdd.Key = oNode.ID
        '        oNodeToAdd.Text = oNode.Text
        '        oNodeToAdd.Tag = oNode.Code.Trim
        '        oNodeToAdd.DrugName = oNode.Description.Trim

        '        If Tab = TabType.Trigger Then

        '            For Each myDNode As TreeNode In trvselecteICD10s.Nodes
        '                If myDNode.Text = oNodeToAdd.Text Then
        '                    Ispresent = True
        '                    Exit For
        '                End If
        '            Next
        '            If Ispresent = False Then
        '                trvselecteICD10s.Nodes.Add(oNodeToAdd)
        '                oNodeToAdd.ImageIndex = 0
        '                oNodeToAdd.SelectedImageIndex = 0
        '            End If
        '            trvselecteICD10s.ExpandAll()

        '        ElseIf Tab = TabType.Exception Then

        '            'For Each myDNode As TreeNode In trvselectedICD10s_Ex.Nodes
        '            '    If myDNode.Text = oNodeToAdd.Text Then
        '            '        Ispresent = True
        '            '        Exit For
        '            '    End If
        '            'Next
        '            'If Ispresent = False Then
        '            '    trvselectedICD10s_Ex.Nodes.Add(oNodeToAdd)
        '            '    oNodeToAdd.ImageIndex = 0
        '            '    oNodeToAdd.SelectedImageIndex = 0
        '            'End If
        '            'trvselectedICD10s_Ex.ExpandAll()

        '        End If

        '    End If 'Ending...If Not IsNothing(oNode) Then

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    oNodeToAdd = Nothing
        'End Try
        Return Nothing
    End Function


    Private Function History_Select_Event(ByVal Tab As TabType, ByVal oNode1 As gloUserControlLibrary.myTreeNode)


        'Dim oNode As TreeNode
        'Dim CategoryFound As Boolean = False
        'Dim HistoryFound As Boolean = False

        'Try
        '    If Not IsNothing(oNode1) Then


        '        If Tab = TabType.Trigger Then

        '            For Each CategoryNode As TreeNode In trvSelectedHistory.Nodes
        '                If CategoryNode.Text = cmbHistoryCategory.Text Then
        '                    For Each HistoryNode As TreeNode In CategoryNode.Nodes
        '                        If HistoryNode.Text = oNode1.Text Then
        '                            HistoryFound = True
        '                            Exit For
        '                        End If
        '                    Next
        '                    If Not HistoryFound Then
        '                        Dim SelectedHistoryNode As New myTreeNode
        '                        SelectedHistoryNode.Text = oNode1.Text
        '                        SelectedHistoryNode.Tag = cmbHistoryCategory.Text
        '                        SelectedHistoryNode.Key = oNode1.ID
        '                        CategoryNode.Nodes.Add(SelectedHistoryNode)
        '                        CategoryNode.Expand()
        '                        trvSelectedHistory.Sort()
        '                    End If
        '                    CategoryFound = True
        '                    Exit For
        '                End If
        '            Next

        '            If Not CategoryFound Then
        '                oNode = New TreeNode
        '                oNode.Text = cmbHistoryCategory.Text
        '                oNode.Tag = cmbHistoryCategory.SelectedValue
        '                oNode.ImageIndex = 0
        '                oNode.SelectedImageIndex = 0
        '                Dim SelectedHistoryNode As New myTreeNode
        '                SelectedHistoryNode.Text = oNode1.Text
        '                SelectedHistoryNode.Tag = cmbHistoryCategory.Text
        '                SelectedHistoryNode.Key = oNode1.ID
        '                oNode.Nodes.Add(SelectedHistoryNode)
        '                trvSelectedHistory.Nodes.Add(oNode)
        '                trvSelectedHistory.ExpandAll()
        '                oNode = Nothing
        '                trvSelectedHistory.Sort()

        '            End If

        '        ElseIf Tab = TabType.Exception And Tab <> TabType.None Then

        '            'For Each CategoryNode As TreeNode In trvSelectedHistory_Ex.Nodes
        '            '    If CategoryNode.Text = cmbHistoryCategory_Ex.Text Then
        '            '        For Each HistoryNode As TreeNode In CategoryNode.Nodes
        '            '            If HistoryNode.Text = oNode1.Text Then
        '            '                HistoryFound = True
        '            '                Exit For
        '            '            End If
        '            '        Next
        '            '        If Not HistoryFound Then
        '            '            Dim SelectedHistoryNode As New myTreeNode
        '            '            SelectedHistoryNode.Text = oNode1.Text
        '            '            SelectedHistoryNode.Tag = cmbHistoryCategory_Ex.Text
        '            '            SelectedHistoryNode.Key = oNode1.ID
        '            '            CategoryNode.Nodes.Add(SelectedHistoryNode)
        '            '            CategoryNode.Expand()
        '            '            trvSelectedHistory_Ex.Sort()
        '            '        End If
        '            '        CategoryFound = True
        '            '        Exit For
        '            '    End If
        '            'Next

        '            'If Not CategoryFound Then
        '            '    oNode = New TreeNode
        '            '    oNode.Text = cmbHistoryCategory_Ex.Text
        '            '    oNode.Tag = cmbHistoryCategory_Ex.SelectedValue
        '            '    oNode.ImageIndex = 0
        '            '    oNode.SelectedImageIndex = 0
        '            '    Dim SelectedHistoryNode As New myTreeNode
        '            '    SelectedHistoryNode.Text = oNode1.Text
        '            '    SelectedHistoryNode.Tag = cmbHistoryCategory_Ex.Text
        '            '    SelectedHistoryNode.Key = oNode1.ID
        '            '    oNode.Nodes.Add(SelectedHistoryNode)
        '            '    trvSelectedHistory_Ex.Nodes.Add(oNode)
        '            '    trvSelectedHistory_Ex.ExpandAll()
        '            '    oNode = Nothing
        '            '    trvSelectedHistory_Ex.Sort()
        '            'End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    oNode = Nothing
        '    oNode1 = Nothing
        'End Try
        Return Nothing
    End Function

    Private Function Associates_Select_Event(ByVal Tab As TabType, ByVal oNode As gloUserControlLibrary.myTreeNode)



        Try
            If Not IsNothing(oNode) AndAlso Tab = TabType.QuickOrders Then
                'Dim oNodeToAdd As New myTreeNode(oNode.Text, oNode.ID, oNode.NDCCode, oNode.mpid)

                'oNodeToAdd.Key = oNode.ID
                'oNodeToAdd.Text = oNode.Text
                'oNodeToAdd.DrugName = oNode.Code ''Vaccine name
                'oNodeToAdd.Dosage = oNode.Description
                'oNodeToAdd.DrugForm = oNode.DrugForm
                'oNodeToAdd.Route = oNode.Route ''SKU
                'oNodeToAdd.Frequency = oNode.Frequency '' TradEName IM
                'oNodeToAdd.NDCCode = oNode.NDCCode ''Manufacturer IM
                'oNodeToAdd.IsNarcotics = oNode.IsNarcotics
                'oNodeToAdd.Duration = oNode.Duration
                'oNodeToAdd.Duration = oNode.Duration
                'oNodeToAdd.DrugQtyQualifier = oNode.DrugQtyQualifier
                ' oNodeToAdd.TemplateResult = oNode.TemplateResult

                'If Not oNodeToAdd Is Nothing Then
                '    AddAssociates(oNodeToAdd, strParentToAssociate, True)
                '    oNodeToAdd.Dispose()
                '    oNodeToAdd = Nothing
                'End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oNode = Nothing
            ' oNodeToAdd = Nothing
        End Try
        Return Nothing
    End Function

#End Region ' CPT, Drugs, ICD9, History, Associates (QuickOrder) selection functions '

#Region " MouseHover/MouseLeave Events "



    'This are common events written for MouseHove/MouseLeave any button on form which needs the highligth effect should 
    'attached to this event

    Private Sub Button_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.MouseHover, btnICD9.MouseHover, btnDrugs.MouseHover, btnCPT.MouseHover, btnClearLab.MouseHover, btnClearICD.MouseHover, btnClearICD10.MouseHover, btnClearDrugs.MouseHover, btnClearCPT.MouseHover
        Try
            'If Not IsNothing(sender) AndAlso Not IsNothing(TryCast(sender, Button)) Then
            '    '   sender.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
            '    btnGuideline.BackgroundImageLayout = ImageLayout.Stretch
            'End If
        Catch ex As Exception
            'Blank catch 
        End Try
    End Sub

    Private Sub Button_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.MouseLeave, btnICD9.MouseLeave, btnDrugs.MouseLeave, btnCPT.MouseLeave, btnClearLab.MouseLeave, btnClearICD.MouseLeave, btnClearICD10.MouseHover, btnClearDrugs.MouseLeave, btnClearCPT.MouseLeave
        Try
            If Not IsNothing(sender) AndAlso Not IsNothing(TryCast(sender, Button)) Then
                If sender.Tag = "Selected" Then
                    '   sender.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
                    sender.BackgroundImageLayout = ImageLayout.Stretch
                Else
                    '  sender.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
                    sender.BackgroundImageLayout = ImageLayout.Stretch
                End If
            End If
        Catch ex As Exception
            'Blank catch
        End Try
    End Sub

#End Region ' MouseHover/MouseLeave Events '

#Region " Validations Events"

    Private Sub TemperatureMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtTemperatureMax.Text)) = False Then
                '    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    CType(sender, TextBox).Focus()
                '    Exit Sub
                'End If
            End If


            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtTemperatureMax_Ex.Text)) = False Then
                '    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    CType(sender, TextBox).Focus()
                '    Exit Sub
                'End If
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub TemperatureMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If CType(sender, TextBox).Text <> "" Then

                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(txtTemperatureMin.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(txtTemperatureMin_Ex.Text, Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Temperature.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PulseMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then

                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtPulseMax.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtPulseMax_Ex.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function MinMaxValidator(ByVal MinVal As String, ByVal MaxVal As String) As Boolean
        Dim blnIsValid As Boolean = False

        If MaxVal = "" Then
            Return True
        End If

        If MinVal = "" Then
            Return True
        End If

        If Val(MinVal) > Val(MaxVal) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub PulseMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(txtPulseMin.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(txtPulseMin_Ex.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PulseOXmax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(Trim(CType(sender, TextBox).Text)) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(txtPulseOXmin.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(txtPulseOXmin_Ex.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If

            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PulseOXmin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtPulseOXmax.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtPulseOXmax_Ex.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Pulse OX.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(txtWeightMin.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(txtWeightMin_Ex.Text), Trim(CType(sender, TextBox).Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtWeightMax.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtWeightMax_Ex.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for Weight.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HeightMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                'If Val(CType(sender, TextBox).Text) > 0 And Val(txtHeightMax.Text) <> 0 Then
                '    'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
                '    If Val(CType(sender, TextBox).Text) > Val(txtHeightMax.Text) Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'Else
                '    txtHeightMinInch.Focus()
                '    Exit Sub
                'End If
            End If

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                ' If Val(CType(sender, TextBox).Text) > 0 And Val(txtHeightMax_Ex.Text) <> 0 Then
                'If Val(CType(sender, TextBox).Text) > Val(txtHeightMax_Ex.Text) Then
                '    MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    CType(sender, TextBox).Focus()
                '    _IsValid = False
                '    Exit Sub
                'End If
                'Else
                'txtHeightMinInch_Ex.Focus()
                '  Exit Sub
                'End If
            End If

            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HeightMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then

                'If Val(txtHeightMin.Text) > 0 And Val(CType(sender, TextBox).Text) > 0 Then
                '    If Val(txtHeightMin.Text) > Val(CType(sender, TextBox).Text) Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).ResetText()
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            End If
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(txtHeightMin_Ex.Text) > 0 And Val(CType(sender, TextBox).Text) > 0 Then
                '    If Val(txtHeightMin_Ex.Text) > Val(CType(sender, TextBox).Text) Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).ResetText()
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BMImin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImin.Validating
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                If Val(CType(sender, TextBox).Text) > 0 Then
                    If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtBMImax.Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                Else
                    CType(sender, TextBox).Text = ""
                End If
            End If

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(CType(sender, TextBox).Text) > 0 Then
                '    If MinMaxValidator(Trim(CType(sender, TextBox).Text), Trim(txtBMImax_Ex.Text)) = False Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'Else
                '    CType(sender, TextBox).Text = ""
                'End If
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BMImax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBMImax.Validating
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                If Val(CType(sender, TextBox).Text) > 0 Then
                    If MinMaxValidator(Trim(txtBMImin.Text), Trim(CType(sender, TextBox).Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                Else
                    CType(sender, TextBox).Text = ""
                End If
            End If
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(CType(sender, TextBox).Text) > 0 Then
                '    If MinMaxValidator(Trim(txtBMImin_Ex.Text), Trim(CType(sender, TextBox).Text)) = False Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for BMI.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'Else
                '    CType(sender, TextBox).Text = ""
                'End If
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BPsettingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMax.Validating, txtBPsettingMaxTo.Validating
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    If MinMaxValidator(Trim(txtBPsettingMax.Text), Trim(txtBPsettingMaxTo.Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
                'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                '    If MinMaxValidator(Trim(txtBPsettingMax_Ex.Text), Trim(txtBPsettingMax_Ex_To.Text)) = False Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BPsettingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPsettingMin.Validating, txtBPsettingMinTo.Validating
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    If MinMaxValidator(Trim(txtBPsettingMin.Text), Trim(txtBPsettingMinTo.Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
                'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                '    If MinMaxValidator(Trim(txtBPsettingMin_Ex.Text), Trim(txtBPsettingMin_Ex_To.Text)) = False Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for BPSitting.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BPstandingMax_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMax.Validating, txtBPstandingMaxTo.Validating
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    If MinMaxValidator(Trim(txtBPstandingMax.Text), Trim(txtBPstandingMaxTo.Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
                'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                '    If MinMaxValidator(Trim(txtBPstandingMax_Ex.Text), Trim(txtBPstandingMax_Ex_To.Text)) = False Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BPstandingMin_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBPstandingMin.Validating, txtBPstandingMinTo.Validating
        Try
            If Val(CType(sender, TextBox).Text) > 0 Then
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                    If MinMaxValidator(Trim(txtBPstandingMin.Text), Trim(txtBPstandingMinTo.Text)) = False Then
                        MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CType(sender, TextBox).Focus()
                        _IsValid = False
                        Exit Sub
                    End If
                End If
                If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                    'If MinMaxValidator(Trim(txtBPstandingMin_Ex.Text), Trim(txtBPstandingMin_Ex_To.Text)) = False Then
                    '    MessageBox.Show("Please check the Minimum and Maximum values for BPStanding.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    CType(sender, TextBox).Focus()
                    '    _IsValid = False
                    '    Exit Sub
                    'End If
                End If
            Else
                CType(sender, TextBox).Text = ""
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AllowDecimal(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBMImin.KeyPress, txtBMImax.KeyPress, txtBPsettingMin.KeyPress, txtBPsettingMax.KeyPress, txtBPstandingMin.KeyPress, txtBPstandingMax.KeyPress, txtBPstandingMinTo.KeyPress, txtBPstandingMaxTo.KeyPress, txtBPsettingMinTo.KeyPress, txtBPsettingMaxTo.KeyPress
        Try
            AllowDecimal(CType(sender, TextBox).Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Disease Management", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TabControl1_Deselecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tbCntrl_RuleSetup.Deselecting
        If Not _IsValid Then
            e.Cancel = True
        End If
    End Sub

    Private Sub HeightMinInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                'If Val(txtHeightMin.Text) <= 0 Then
                '    If Val(CType(sender, TextBox).Text) >= 12 And Val(CType(sender, TextBox).Text) <= 84 Then
                '        Dim _Ft As Decimal
                '        Dim _Inches As Decimal
                '        Dim _TotalInches As Decimal = Val(CType(sender, TextBox).Text)

                '        _Ft = Math.Floor(_TotalInches / 12)
                '        _Inches = Math.Round(_TotalInches Mod 12, 2)
                '        txtHeightMin.Text = _Ft
                '        CType(sender, TextBox).Text = _Inches
                '        ' Exit Sub
                '    ElseIf Val(CType(sender, TextBox).Text) > 84 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
            Else
                If Val(CType(sender, TextBox).Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CType(sender, TextBox).Focus()
                    _IsValid = False
                End If
            End If

            'If Val(CType(sender, TextBox).Text) > 0 And Val(txtHeightMaxInch.Text) <> 0 Then
            'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
            '    If Val(CType(sender, TextBox).Text) > Val(txtHeightMaxInch.Text) Then
            '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        CType(sender, TextBox).Focus()
            '        _IsValid = False
            '        Exit Sub
            '    End If
            'End If

            ' End If
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(txtHeightMin_Ex.Text) <= 0 Then
                '    If Val(CType(sender, TextBox).Text) >= 12 And Val(CType(sender, TextBox).Text) <= 84 Then
                '        Dim _Ft As Decimal
                '        Dim _Inches As Decimal
                '        Dim _TotalInches As Decimal = Val(CType(sender, TextBox).Text)

                '        _Ft = Math.Floor(_TotalInches / 12)
                '        _Inches = Math.Round(_TotalInches Mod 12, 2)
                '        txtHeightMin_Ex.Text = _Ft
                '        CType(sender, TextBox).Text = _Inches
                '        ' Exit Sub
                '    ElseIf Val(CType(sender, TextBox).Text) > 84 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
                'Else
                '    If Val(CType(sender, TextBox).Text) >= 12 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
                'End If
                'If Val(CType(sender, TextBox).Text) > 0 And Val(txtHeightMaxInch_Ex.Text) <> 0 Then
                '    'If MinMaxValidator(Trim(txtHeightMin.Text), Trim(txtHeightMax.Text)) = False Then
                '    If Val(CType(sender, TextBox).Text) > Val(txtHeightMaxInch_Ex.Text) Then
                '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '        Exit Sub
                '    End If
                'End If
            End If
            _IsValid = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HeightMaxInch_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                'If Val(txtHeightMax.Text) <= 0 Then
                '    If Val(CType(sender, TextBox).Text) >= 12 And Val(CType(sender, TextBox).Text) <= 84 Then
                '        Dim _Ft As Decimal
                '        Dim _Inches As Decimal
                '        Dim _TotalInches As Decimal = Val(CType(sender, TextBox).Text)

                '        _Ft = Math.Floor(_TotalInches / 12)
                '        _Inches = Math.Round(_TotalInches Mod 12, 2)
                '        txtHeightMax.Text = _Ft
                '        CType(sender, TextBox).Text = _Inches
                '        'Exit Sub
                '    ElseIf Val(CType(sender, TextBox).Text) > 84 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
            Else
                If Val(CType(sender, TextBox).Text) >= 12 Then
                    MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CType(sender, TextBox).Focus()
                    _IsValid = False
                End If
                '' End If
            End If
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(txtHeightMax_Ex.Text) <= 0 Then
                '    If Val(CType(sender, TextBox).Text) >= 12 And Val(CType(sender, TextBox).Text) <= 84 Then
                '        Dim _Ft As Decimal
                '        Dim _Inches As Decimal
                '        Dim _TotalInches As Decimal = Val(CType(sender, TextBox).Text)

                '        _Ft = Math.Floor(_TotalInches / 12)
                '        _Inches = Math.Round(_TotalInches Mod 12, 2)
                '        txtHeightMax_Ex.Text = _Ft
                '        CType(sender, TextBox).Text = _Inches
                '        'Exit Sub
                '    ElseIf Val(CType(sender, TextBox).Text) > 84 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
                'Else
                '    If Val(CType(sender, TextBox).Text) >= 12 Then
                '        MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        CType(sender, TextBox).Focus()
                '        _IsValid = False
                '    End If
                'End If
            End If

            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            '    If Val(txtHeightMin.Text) > Val(txtHeightMax.Text) Then
            '        MessageBox.Show("Invalid value of Ft ", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        txtHeightMax.Focus()
            '        _IsValid = False
            '    ElseIf Val(txtHeightMin.Text) = Val(txtHeightMax.Text) And (Val(txtHeightMinInch.Text) > Val(CType(sender, TextBox).Text)) Then
            '        'MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        CType(sender, TextBox).ResetText()
            '        CType(sender, TextBox).Focus()
            '        _IsValid = False
            '    End If
            'End If
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If Val(txtHeightMin_Ex.Text) > Val(txtHeightMax_Ex.Text) Then
                '    MessageBox.Show("Invalid value of Ft ", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    txtHeightMax_Ex.Focus()
                '    _IsValid = False
                'ElseIf Val(txtHeightMin_Ex.Text) = Val(txtHeightMax_Ex.Text) And (Val(txtHeightMinInch.Text) > Val(CType(sender, TextBox).Text)) Then
                '    'MessageBox.Show("Invalid value of Inches", "Patient Vitals", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    MessageBox.Show("Please check the Minimum and Maximum values for Height.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    CType(sender, TextBox).ResetText()
                '    CType(sender, TextBox).Focus()
                '    _IsValid = False
                'End If
                'If Val(CType(sender, TextBox).Text) = 0 Then
                '    CType(sender, TextBox).Text = ""
                '    'Exit Sub
                'End If
            End If
            _IsValid = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub IsLetterOrDigit(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsLetterOrDigit(e.KeyChar) Or (e.KeyChar = ChrW(8)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    '----------------------------------------------------------------END vijay Changes------------------------------------------------------------------------------------------
#End Region

#Region "changes for Mouse Down and Context Menu Item "

    Private Sub mnuDeleteHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteHistory.Click
        Try
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                If trvSelectedHistory.SelectedNode.Parent.Nodes.Count > 1 Then
                    trvSelectedHistory.SelectedNode.Remove()
                Else
                    trvSelectedHistory.SelectedNode.Parent.Remove()
                End If
            End If

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
                'If trvSelectedHistory_Ex.SelectedNode.Parent.Nodes.Count > 1 Then
                '    trvSelectedHistory_Ex.SelectedNode.Remove()
                'Else
                '    trvSelectedHistory_Ex.SelectedNode.Parent.Remove()
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub trvSelectedHistory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedHistory.MouseDown
        Dim trvNode As TreeNode
        Try

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                trvNode = trvSelectedHistory.GetNodeAt(e.X, e.Y)

                trvSelectedHistory.SelectedNode = trvNode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvNode) = False Then
                        If trvNode.Level = 1 Then
                            'Try
                            '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                            '        trvSelectedHistory.ContextMenuStrip.Dispose()
                            '        trvSelectedHistory.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvSelectedHistory.ContextMenuStrip = ContextMenuHistory
                        Else
                            'Try
                            '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                            '        trvSelectedHistory.ContextMenuStrip.Dispose()
                            '        trvSelectedHistory.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvSelectedHistory.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvSelectedHistory.ContextMenuStrip) = False) Then
                        '        trvSelectedHistory.ContextMenuStrip.Dispose()
                        '        trvSelectedHistory.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedHistory.ContextMenuStrip = Nothing
                    End If
                End If
            End If

            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '    trvNode = trvSelectedHistory_Ex.GetNodeAt(e.X, e.Y)

            '    trvSelectedHistory_Ex.SelectedNode = trvNode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then
            '        If IsNothing(trvNode) = False Then
            '            If trvNode.Level = 1 Then
            '                'Try
            '                '    If (IsNothing(trvSelectedHistory_Ex.ContextMenuStrip) = False) Then
            '                '        trvSelectedHistory_Ex.ContextMenuStrip.Dispose()
            '                '        trvSelectedHistory_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvSelectedHistory_Ex.ContextMenuStrip = ContextMenuHistory
            '            Else
            '                'Try
            '                '    If (IsNothing(trvSelectedHistory_Ex.ContextMenuStrip) = False) Then
            '                '        trvSelectedHistory_Ex.ContextMenuStrip.Dispose()
            '                '        trvSelectedHistory_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvSelectedHistory_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            'Try
            '            '    If (IsNothing(trvSelectedHistory_Ex.ContextMenuStrip) = False) Then
            '            '        trvSelectedHistory_Ex.ContextMenuStrip.Dispose()
            '            '        trvSelectedHistory_Ex.ContextMenuStrip = Nothing
            '            '    End If
            '            'Catch ex As Exception

            '            'End Try
            '            trvSelectedHistory_Ex.ContextMenuStrip = Nothing
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvNode = Nothing
        End Try
    End Sub

    Private Sub mnuDeleteInsurance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteInsurance.Click
        'Dim trvnode As New TreeNode
        'Try

        '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
        '        trvnode = trvSelectedInsurance.SelectedNode
        '        If trvnode.Level = 0 Then
        '            trvSelectedInsurance.SelectedNode.Remove()
        '        End If
        '    End If
        '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '        trvnode = trvSelectedInsurance_Ex.SelectedNode
        '        If trvnode.Level = 0 Then
        '            trvSelectedInsurance_Ex.SelectedNode.Remove()
        '        End If
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    trvnode = Nothing
        'End Try
    End Sub

    Private Sub mnuDeleteDrugs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteDrugs.Click
        'Dim trvnode As New TreeNode
        'Try

        '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
        '        trvnode = trvSelectedDrugs.SelectedNode
        '        If trvnode.Level = 0 Then
        '            trvSelectedDrugs.SelectedNode.Remove()
        '        End If
        '    End If
        '    If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '        trvnode = trvSelectedDrugs_Ex.SelectedNode
        '        If trvnode.Level = 0 Then
        '            trvSelectedDrugs_Ex.SelectedNode.Remove()
        '        End If
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    trvnode = Nothing
        'End Try
    End Sub

    Private Sub trvSelectedInsurance_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedInsurance.MouseDown
        Dim trvnode As TreeNode

        Try

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                trvnode = trvSelectedInsurance.GetNodeAt(e.X, e.Y)
                trvSelectedInsurance.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then

                    If IsNothing(trvnode) = False Then
                        If trvnode.Level = 0 Then
                            trvSelectedInsurance.ContextMenuStrip = ContextMenuStrip2
                        Else
                            trvSelectedInsurance.ContextMenuStrip = Nothing
                        End If
                    Else
                        trvSelectedInsurance.ContextMenuStrip = Nothing
                    End If
                Else
                    trvSelectedInsurance.ContextMenuStrip = Nothing
                End If
            End If
            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '    trvnode = trvSelectedInsurance_Ex.GetNodeAt(e.X, e.Y)
            '    trvSelectedInsurance_Ex.SelectedNode = trvnode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then

            '        If IsNothing(trvnode) = False Then
            '            If trvnode.Level = 0 Then
            '                trvSelectedInsurance_Ex.ContextMenuStrip = ContextMenuStrip2
            '            Else
            '                trvSelectedInsurance_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            trvSelectedInsurance_Ex.ContextMenuStrip = Nothing
            '        End If
            '    Else
            '        trvSelectedInsurance_Ex.ContextMenuStrip = Nothing
            '    End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvnode = Nothing
        End Try
    End Sub

    Private Sub trvSelectedDrugs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSelectedDrugs.MouseDown
        Dim trvnode As TreeNode

        Try

            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                trvnode = trvSelectedDrugs.GetNodeAt(e.X, e.Y)
                trvSelectedDrugs.SelectedNode = trvnode
                If e.Button = Windows.Forms.MouseButtons.Right Then

                    If IsNothing(trvnode) = False Then
                        If trvnode.Level = 0 Then
                            'Try
                            '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                            '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                            '        trvSelectedDrugs.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvSelectedDrugs.ContextMenuStrip = ContextMenuStrip1
                        Else
                            'Try
                            '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                            '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                            '        trvSelectedDrugs.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvSelectedDrugs.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                        '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                        '        trvSelectedDrugs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvSelectedDrugs.ContextMenuStrip = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trvSelectedDrugs.ContextMenuStrip) = False) Then
                    '        trvSelectedDrugs.ContextMenuStrip.Dispose()
                    '        trvSelectedDrugs.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trvSelectedDrugs.ContextMenuStrip = Nothing
                End If
            End If
            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '    trvnode = trvSelectedDrugs_Ex.GetNodeAt(e.X, e.Y)
            '    trvSelectedDrugs_Ex.SelectedNode = trvnode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then

            '        If IsNothing(trvnode) = False Then
            '            If trvnode.Level = 0 Then
            '                'Try
            '                '    If (IsNothing(trvSelectedDrugs_Ex.ContextMenuStrip) = False) Then
            '                '        trvSelectedDrugs_Ex.ContextMenuStrip.Dispose()
            '                '        trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvSelectedDrugs_Ex.ContextMenuStrip = ContextMenuStrip1
            '            Else
            '                'Try
            '                '    If (IsNothing(trvSelectedDrugs_Ex.ContextMenuStrip) = False) Then
            '                '        trvSelectedDrugs_Ex.ContextMenuStrip.Dispose()
            '                '        trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            'Try
            '            '    If (IsNothing(trvSelectedDrugs_Ex.ContextMenuStrip) = False) Then
            '            '        trvSelectedDrugs_Ex.ContextMenuStrip.Dispose()
            '            '        trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '            '    End If
            '            'Catch ex As Exception

            '            'End Try
            '            trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '        End If
            '    Else
            '        'Try
            '        '    If (IsNothing(trvSelectedDrugs_Ex.ContextMenuStrip) = False) Then
            '        '        trvSelectedDrugs_Ex.ContextMenuStrip.Dispose()
            '        '        trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '        '    End If
            '        'Catch ex As Exception

            '        'End Try
            '        trvSelectedDrugs_Ex.ContextMenuStrip = Nothing
            '    End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvnode = Nothing
        End Try
    End Sub

    Private Sub mnuItem_DeleteCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_DeleteCPT.Click
        Dim trvnode As New TreeNode
        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            trvnode = trvselectedCPT.SelectedNode
            If trvnode.Text <> "" Then
                trvselectedCPT.SelectedNode.Remove()
            End If
        End If
        'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '    trvnode = trvselectedCPT_Ex.SelectedNode
        '    If trvnode.Text <> "" Then
        '        trvselectedCPT_Ex.SelectedNode.Remove()
        '    End If
        'End If

    End Sub

    Private Sub trvselectedCPT_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselectedCPT.MouseDown
        Try
            Dim trvNode As TreeNode
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                trvNode = trvselectedCPT.GetNodeAt(e.X, e.Y)
                trvselectedCPT.SelectedNode = trvNode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvNode) = False Then
                        If trvNode.Text <> "" Then
                            'Try
                            '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                            '        trvselectedCPT.ContextMenuStrip.Dispose()
                            '        trvselectedCPT.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselectedCPT.ContextMenuStrip = CmnuStripCPT
                        Else
                            'Try
                            '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                            '        trvselectedCPT.ContextMenuStrip.Dispose()
                            '        trvselectedCPT.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselectedCPT.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselectedCPT.ContextMenuStrip) = False) Then
                        '        trvselectedCPT.ContextMenuStrip.Dispose()
                        '        trvselectedCPT.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselectedCPT.ContextMenuStrip = Nothing
                    End If
                End If
            End If
            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '    trvNode = trvselectedCPT_Ex.GetNodeAt(e.X, e.Y)
            '    trvselectedCPT_Ex.SelectedNode = trvNode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then
            '        If IsNothing(trvNode) = False Then
            '            If trvNode.Text <> "" Then
            '                'Try
            '                '    If (IsNothing(trvselectedCPT_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedCPT_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedCPT_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedCPT_Ex.ContextMenuStrip = CmnuStripCPT
            '            Else
            '                'Try
            '                '    If (IsNothing(trvselectedCPT_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedCPT_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedCPT_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedCPT_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            'Try
            '            '    If (IsNothing(trvselectedCPT_Ex.ContextMenuStrip) = False) Then
            '            '        trvselectedCPT_Ex.ContextMenuStrip.Dispose()
            '            '        trvselectedCPT_Ex.ContextMenuStrip = Nothing
            '            '    End If
            '            'Catch ex As Exception

            '            'End Try
            '            trvselectedCPT_Ex.ContextMenuStrip = Nothing
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvselecteICDs_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvselecteICDs.MouseDown, trvselecteICD10s.MouseDown
        Try
            Dim trvNode As TreeNode
            If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
                trvNode = trvselecteICDs.GetNodeAt(e.X, e.Y)
                trvselecteICDs.SelectedNode = trvNode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvNode) = False Then
                        If trvNode.Text <> "" Then
                            'Try
                            '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                            '        trvselecteICDs.ContextMenuStrip.Dispose()
                            '        trvselecteICDs.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselecteICDs.ContextMenuStrip = CmnustripICD
                        Else
                            'Try
                            '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                            '        trvselecteICDs.ContextMenuStrip.Dispose()
                            '        trvselecteICDs.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselecteICDs.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselecteICDs.ContextMenuStrip) = False) Then
                        '        trvselecteICDs.ContextMenuStrip.Dispose()
                        '        trvselecteICDs.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselecteICDs.ContextMenuStrip = Nothing
                    End If
                End If

                trvNode = trvselecteICD10s.GetNodeAt(e.X, e.Y)
                trvselecteICD10s.SelectedNode = trvNode
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If IsNothing(trvNode) = False Then
                        If trvNode.Text <> "" Then
                            'Try
                            '    If (IsNothing(trvselecteICD10s.ContextMenuStrip) = False) Then
                            '        trvselecteICD10s.ContextMenuStrip.Dispose()
                            '        trvselecteICD10s.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselecteICD10s.ContextMenuStrip = CmnustripICD
                        Else
                            'Try
                            '    If (IsNothing(trvselecteICD10s.ContextMenuStrip) = False) Then
                            '        trvselecteICD10s.ContextMenuStrip.Dispose()
                            '        trvselecteICD10s.ContextMenuStrip = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvselecteICD10s.ContextMenuStrip = Nothing
                        End If
                    Else
                        'Try
                        '    If (IsNothing(trvselecteICD10s.ContextMenuStrip) = False) Then
                        '        trvselecteICD10s.ContextMenuStrip.Dispose()
                        '        trvselecteICD10s.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvselecteICD10s.ContextMenuStrip = Nothing
                    End If
                End If

            End If
            'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
            '    trvNode = trvselectedICDs_Ex.GetNodeAt(e.X, e.Y)
            '    trvselectedICDs_Ex.SelectedNode = trvNode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then
            '        If IsNothing(trvNode) = False Then
            '            If trvNode.Text <> "" Then
            '                'Try
            '                '    If (IsNothing(trvselectedICDs_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedICDs_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedICDs_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedICDs_Ex.ContextMenuStrip = CmnustripICD
            '            Else
            '                'Try
            '                '    If (IsNothing(trvselectedICDs_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedICDs_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedICDs_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedICDs_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            'Try
            '            '    If (IsNothing(trvselectedICDs_Ex.ContextMenuStrip) = False) Then
            '            '        trvselectedICDs_Ex.ContextMenuStrip.Dispose()
            '            '        trvselectedICDs_Ex.ContextMenuStrip = Nothing
            '            '    End If
            '            'Catch ex As Exception

            '            'End Try
            '            trvselectedICDs_Ex.ContextMenuStrip = Nothing
            '        End If
            '    End If

            '    trvNode = trvselectedICD10s_Ex.GetNodeAt(e.X, e.Y)
            '    trvselectedICD10s_Ex.SelectedNode = trvNode
            '    If e.Button = Windows.Forms.MouseButtons.Right Then
            '        If IsNothing(trvNode) = False Then
            '            If trvNode.Text <> "" Then
            '                'Try
            '                '    If (IsNothing(trvselectedICD10s_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedICD10s_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedICD10s_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedICD10s_Ex.ContextMenuStrip = CmnustripICD
            '            Else
            '                'Try
            '                '    If (IsNothing(trvselectedICD10s_Ex.ContextMenuStrip) = False) Then
            '                '        trvselectedICD10s_Ex.ContextMenuStrip.Dispose()
            '                '        trvselectedICD10s_Ex.ContextMenuStrip = Nothing
            '                '    End If
            '                'Catch ex As Exception

            '                'End Try
            '                trvselectedICD10s_Ex.ContextMenuStrip = Nothing
            '            End If
            '        Else
            '            'Try
            '            '    If (IsNothing(trvselectedICD10s_Ex.ContextMenuStrip) = False) Then
            '            '        trvselectedICD10s_Ex.ContextMenuStrip.Dispose()
            '            '        trvselectedICD10s_Ex.ContextMenuStrip = Nothing
            '            '    End If
            '            'Catch ex As Exception

            '            'End Try
            '            trvselectedICD10s_Ex.ContextMenuStrip = Nothing
            '        End If
            '    End If

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub mnuItem_DeleteICD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItem_DeleteICD.Click
        Dim trvnode As New TreeNode
        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            If sender.Name = "btnRemoveSelectedICD10" Then
                If IsNothing(trvselecteICD10s.SelectedNode) = False Then
                    trvnode = trvselecteICD10s.SelectedNode
                    If trvnode.Text <> "" Then
                        trvselecteICD10s.SelectedNode.Remove()
                    End If
                End If
            ElseIf sender.Name = "btnRemoveSelectedICD9" Then
                If IsNothing(trvselecteICDs.SelectedNode) = False Then
                    trvnode = trvselecteICDs.SelectedNode
                    If trvnode.Text <> "" Then
                        trvselecteICDs.SelectedNode.Remove()
                    End If
                End If
            ElseIf sender.name = "mnuItem_DeleteICD" Then
                If trvselecteICD10s.Focused Then
                    If IsNothing(trvselecteICD10s.SelectedNode) = False Then
                        trvnode = trvselecteICD10s.SelectedNode
                        If trvnode.Text <> "" Then
                            trvselecteICD10s.SelectedNode.Remove()
                        End If
                    End If
                End If

                If trvselecteICDs.Focused Then
                    If IsNothing(trvselecteICDs.SelectedNode) = False Then
                        trvnode = trvselecteICDs.SelectedNode
                        If trvnode.Text <> "" Then
                            trvselecteICDs.SelectedNode.Remove()
                        End If
                    End If
                End If

            End If
        End If

        'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '    If sender.Name = "btnRemoveSelectedICD10_Ex" Then
        '        If IsNothing(trvselectedICD10s_Ex.SelectedNode) = False Then
        '            trvnode = trvselectedICD10s_Ex.SelectedNode
        '            If trvnode.Text <> "" Then
        '                trvselectedICD10s_Ex.SelectedNode.Remove()
        '            End If
        '        End If
        '    ElseIf sender.Name = "btnRemoveSelectedICD9_Ex" Then
        '        If IsNothing(trvselectedICDs_Ex.SelectedNode) = False Then
        '            trvnode = trvselectedICDs_Ex.SelectedNode
        '            If trvnode.Text <> "" Then
        '                trvselectedICDs_Ex.SelectedNode.Remove()
        '            End If
        '        End If
        '    ElseIf sender.name = "mnuItem_DeleteICD" Then

        '        If trvselectedICD10s_Ex.Focused Then
        '            If IsNothing(trvselectedICD10s_Ex.SelectedNode) = False Then
        '                trvnode = trvselectedICD10s_Ex.SelectedNode
        '                If trvnode.Text <> "" Then
        '                    trvselectedICD10s_Ex.SelectedNode.Remove()
        '                End If
        '            End If
        '        End If

        '        If trvselectedICDs_Ex.Focused Then
        '            If IsNothing(trvselectedICDs_Ex.SelectedNode) = False Then
        '                trvnode = trvselectedICDs_Ex.SelectedNode
        '                If trvnode.Text <> "" Then
        '                    trvselectedICDs_Ex.SelectedNode.Remove()
        '                End If
        '            End If
        '        End If

        '    End If
        'End If

    End Sub

#End Region

#Region "changes for Tab Selection change"
    Private Sub OrdersTobeGivenTabSelected(ByVal sender As System.Object, ByVal e As TabControlEventArgs) Handles tbCntrl_RuleSetup.Selected
        'If tbCntrl_RuleSetup.SelectedTab.Name = "tbPg_QuickOrders" Then
        '    txtSearchOrder.Text = ""
        '    PopulateAssocaitedInfo(1)
        '    strParentToAssociate = btnLab.Text
        'End If
    End Sub
#End Region

#Region "Changes for Search functionality for Lab And Order"
    Private Sub txtLabsSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLabsSearch.TextChanged


        Dim strSearch As String = ""
        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            strSearch = txtLabsSearch.Text.Trim()
            Search_Order_TextChanged(c1Labs, strSearch)
            If c1Labs IsNot Nothing AndAlso c1Labs.Rows.Count > 1 Then
                DesignRadiologyGridByTable(c1Labs)
            End If

        End If
        'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '    strSearch = txtLabsSearch_Ex.Text.Trim()
        '    Search_Order_TextChanged(c1Labs_Ex, strSearch)
        '    If c1Labs_Ex IsNot Nothing AndAlso c1Labs_Ex.Rows.Count > 1 Then
        '        DesignRadiologyGridByTable(c1Labs_Ex)
        '    End If
        'End If

    End Sub

    Private Sub txtLabResultSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLabResultSearch.TextChanged

        Dim strSearch As String = ""
        If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(0).Name Then
            strSearch = txtLabResultSearch.Text.Trim()
            Search_TextChanged(C1LabResult, strSearch)
            If C1LabResult IsNot Nothing AndAlso C1LabResult.Rows.Count > 1 Then
                DesignLabsGridByTable(C1LabResult)
            End If

        End If
        'If tbCntrl_RuleSetup.SelectedTab.Name = tbCntrl_RuleSetup.TabPages(1).Name Then
        '    strSearch = txtLabResultSearch_Ex.Text.Trim()
        '    Search_TextChanged(C1LabResult_Ex, strSearch)
        '    If C1LabResult_Ex IsNot Nothing AndAlso C1LabResult_Ex.Rows.Count > 1 Then
        '        DesignLabsGridByTable(C1LabResult_Ex)
        '    End If
        'End If

    End Sub
#End Region


#Region "History Button Click"

    Private Sub btnHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor

            FillAllCriteria()
            trvselectedhist.Visible = True
            trvselectedprob.Visible = False
            trvselectedhist.BringToFront()
            txtsrchprb.Text = ""

            trvfinprob.Nodes.Clear()
            trvsubprb.Nodes.Clear()
            cmbhistsnomed.Visible = True
            lblsnohistcat.Visible = True
            ' sptRight.Visible = False
            ' pnlRight.Visible = True
            ' pnlRight.SendToBack()
            Label230.Text = "     Selected History"
            pnlHistory.Visible = True
            pnlHistory.BringToFront()

            Fill_Histories_1()
            If cmbHistoryCategory.Items.Count >= 1 Then
                cmbHistoryCategory.SelectedIndex = 0
            End If
            cmbHistoryCategory_SelectionChangeCommitted(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        trvSelectedHistory.Nodes.Clear()
        ' lstVw_History.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveHistory.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            'lstVw_History.Items.Clear()
            GloUC_trvHistory.txtsearch.Text = ""

            If trvSelectedHistory.Nodes.Count > 0 Then
                For i As Integer = 0 To trvSelectedHistory.GetNodeCount(False) - 1
                    For j As Integer = 0 To trvSelectedHistory.Nodes(i).GetNodeCount(False) - 1
                        _listviewItem = New ListViewItem(vbTab & trvSelectedHistory.Nodes(i).Text & " - " & vbTab & trvSelectedHistory.Nodes(i).Nodes(j).Text)
                        _listviewItem.Tag = trvSelectedHistory.Nodes(i).Nodes(j)
                        ' lstVw_History.Items.Add(_listviewItem)
                        _listviewItem = Nothing
                    Next
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlHistory.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "History Exclusion Button Click"

    Private Sub btnHistoryEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            '     FillAllCriteria()
            'trvSelectedHistory_Ex.Visible = True
            'trvSelectedHistory_Ex.BringToFront()
            'txtsrchprb.Text = ""

            'lblsnohistcat.Visible = True
            '' sptRight.Visible = False
            'pnlRight.Visible = True
            'pnlRight.SendToBack()
            'Label230.Text = "     Selected History"
            'pnlExceptionsHistory.Visible = True
            'pnlExceptionsHistory.BringToFront()

            ''  Fill_Histories_1_Ex()
            'If cmbHistoryCategory_Ex.Items.Count >= 1 Then
            '    cmbHistoryCategory_Ex.SelectedIndex = 0
            'End If
            cmbHistoryCategory_SelectionChangeCommitted(Nothing, Nothing)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearHistoryEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  trvSelectedHistory_Ex.Nodes.Clear()
        '  lstExVw_History.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveHistory_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_History.Items.Clear()
        '    GloUC_TrvHistoryEx.txtsearch.Text = ""
        '    If trvSelectedHistory_Ex.Nodes.Count > 0 Then
        '        For i As Integer = 0 To trvSelectedHistory_Ex.GetNodeCount(False) - 1
        '            For j As Integer = 0 To trvSelectedHistory_Ex.Nodes(i).GetNodeCount(False) - 1
        '                _listviewItem = New ListViewItem(vbTab & trvSelectedHistory_Ex.Nodes(i).Text & " - " & vbTab & trvSelectedHistory_Ex.Nodes(i).Nodes(j).Text)
        '                _listviewItem.Tag = trvSelectedHistory_Ex.Nodes(i).Nodes(j)
        '                lstExVw_History.Items.Add(_listviewItem)
        '                _listviewItem = Nothing
        '            Next
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsHistory.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        'End Try
    End Sub

#End Region


#Region "CPT Button Click"

    Private Sub btnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCPT.Click
        Try




            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMHCPCS, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "CPT"
            oListControl.ReportingYear = _ReportingYear
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)
            AddSelectedItemstoListControl(lstVw_CPT)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCPT.Click
        trvselectedCPT.Nodes.Clear()
        lstVw_CPT.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveCPT.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            lstVw_CPT.Items.Clear()
            GloUC_trvCPT.txtsearch.Text = ""

            If trvselectedCPT.GetNodeCount(False) > 0 Then
                For i As Integer = 0 To trvselectedCPT.GetNodeCount(False) - 1
                    _listviewItem = New ListViewItem(trvselectedCPT.Nodes(i).Text)
                    _listviewItem.SubItems.Add(trvselectedCPT.Nodes(i).Text)
                    _listviewItem.Tag = trvselectedCPT.Nodes(i)
                    lstVw_CPT.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlCPT.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "CPT Exclusion Button Click"

    Private Sub btnCPT_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            '    FillAllCriteria()
            '  sptRight.Visible = False
            'pnlRight.Visible = True

            ''   sptRight.SendToBack()
            'pnlRight.SendToBack()

            'pnlExceptionsCPT.Visible = True
            'pnlExceptionsCPT.BringToFront()

            '   fill_CPTs("")
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearCPTEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '   trvselectedCPT_Ex.Nodes.Clear()
        ' lstExVw_CPT.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveCPT_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_CPT.Items.Clear()
        '    GloUC_trvCPT_Ex.txtsearch.Text = ""
        '    If trvselectedCPT_Ex.GetNodeCount(False) > 0 Then
        '        For i As Integer = 0 To trvselectedCPT_Ex.GetNodeCount(False) - 1
        '            _listviewItem = New ListViewItem(trvselectedCPT_Ex.Nodes(i).Text)
        '            _listviewItem.SubItems.Add(trvselectedCPT_Ex.Nodes(i).Text)
        '            _listviewItem.Tag = trvselectedCPT_Ex.Nodes(i)
        '            lstExVw_CPT.Items.Add(_listviewItem)
        '            _listviewItem = Nothing
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsCPT.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        'End Try
    End Sub

#End Region

#Region "Insurance Button Click"

    Private Sub btnInsurance_Click(sender As Object, e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            '  FillAllCriteria()
            'pnlRight.Visible = True
            'pnlRight.SendToBack()
            'pnlInsurance.Visible = True
            'pnlInsurance.BringToFront()
            ' fill_Insurance()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearInsurance_Click(sender As Object, e As System.EventArgs)
        trvSelectedInsurance.Nodes.Clear()
        'lstVw_Insurance.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveInsurance_Click(sender As Object, e As System.EventArgs) Handles tsBtn_SaveInsurance.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            '  lstVw_Insurance.Items.Clear()
            GloUC_trvInsurance.txtsearch.Text = ""
            For i As Integer = 0 To trvSelectedInsurance.GetNodeCount(False) - 1
                _listviewItem = New ListViewItem(trvSelectedInsurance.Nodes(i).Text)
                _listviewItem.Tag = trvSelectedInsurance.Nodes(i)
                '    lstVw_Insurance.Items.Add(_listviewItem)
                _listviewItem = Nothing
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlInsurance.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Insurance Exclusion Button Click"

    Private Sub btnInsuranceEx_Click(sender As Object, e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            '  FillAllCriteria()
            'pnlRight.Visible = True
            'pnlRight.SendToBack()
            'pnlExceptionsInsurance.Visible = True
            'pnlExceptionsInsurance.BringToFront()
            ' fill_Insurance()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearInsuranceEx_Click(sender As Object, e As System.EventArgs)
        '  trvSelectedInsurance_Ex.Nodes.Clear()
        ' lstExVw_Insurance.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveInsurance_Ex_Click(sender As Object, e As System.EventArgs)
        Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_Insurance.Items.Clear()
        '    GloUC_trvInsurance_Ex.txtsearch.Text = ""
        '    For i As Integer = 0 To trvSelectedInsurance_Ex.GetNodeCount(False) - 1
        '        _listviewItem = New ListViewItem(trvSelectedInsurance_Ex.Nodes(i).Text)
        '        _listviewItem.Tag = trvSelectedInsurance_Ex.Nodes(i)
        '        lstExVw_Insurance.Items.Add(_listviewItem)
        '        _listviewItem = Nothing
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsInsurance.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        'End Try
    End Sub

#End Region

#Region "Drug Button Click"

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    FillAllCriteria()
        '    sptRight.Visible = False
        '    pnlRight.Visible = True
        '    pnlRight.SendToBack()
        '    sptRight.SendToBack()
        '    pnlDrugs.Visible = True
        '    pnlDrugs.BringToFront()
        '    fill_Drugs()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try


        Try




            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMRxNorm, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "RXNORM"
            oListControl.ReportingYear = _ReportingYear

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)
            AddSelectedItemstoListControl(lstVw_Drugs)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearDrugs.Click
        trvSelectedDrugs.Nodes.Clear()
        lstVw_Drugs.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveDrugs.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            lstVw_Drugs.Items.Clear()
            GloUC_trvDrugs.txtsearch.Text = ""
            For i As Integer = 0 To trvSelectedDrugs.GetNodeCount(False) - 1
                _listviewItem = New ListViewItem(trvSelectedDrugs.Nodes(i).Text)
                _listviewItem.Tag = trvSelectedDrugs.Nodes(i)
                lstVw_Drugs.Items.Add(_listviewItem)
                _listviewItem = Nothing
            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlDrugs.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Drug Exclusion Button Click"

    Private Sub btnDrugsEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            FillAllCriteria()
            '   sptRight.Visible = False
            'pnlRight.Visible = True

            'pnlRight.SendToBack()
            ''   sptRight.SendToBack()

            'pnlExceptionsDrugs.Visible = True
            'pnlExceptionsDrugs.BringToFront()

            'fill_Drugs()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearDrugsEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'trvSelectedDrugs_Ex.Nodes.Clear()
        'lstExVw_Drugs.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveDrugs_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_Drugs.Items.Clear()
        '    GloUC_trvDrugs_Ex.txtsearch.Text = ""
        '    For i As Integer = 0 To trvSelectedDrugs_Ex.GetNodeCount(False) - 1
        '        _listviewItem = New ListViewItem(trvSelectedDrugs_Ex.Nodes(i).Text)
        '        _listviewItem.Tag = trvSelectedDrugs_Ex.Nodes(i)
        '        lstExVw_Drugs.Items.Add(_listviewItem)
        '        _listviewItem = Nothing
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsDrugs.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        'End Try
    End Sub

#End Region


#Region "Order/Lab Button Click"

    Private Sub btnLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        'Try
        'Me.Cursor = Cursors.WaitCursor
        'FillAllCriteria()
        '' sptRight.Visible = False
        'pnlRight.Visible = True
        'pnlRight.SendToBack()

        'pnlLab.Visible = True
        'pnlLab.BringToFront()
        'txtLabResultSearch.Focus()

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try

        Try
            '    Me.Cursor = Cursors.WaitCursor
            '    FillAllCriteria()
            '    ' sptRight.Visible = False
            '    pnlRight.Visible = True
            '    pnlRight.SendToBack()

            '    pnlICD9.Visible = True

            '    tsBtn_SaveICD.Visible = True
            '    tsBtn_SaveICD10.Visible = False

            '    GloUC_trvICD9.Visible = True
            '    GloUC_trvICD10.Visible = False

            '    trvselecteICD10s.Visible = False
            '    trvselecteICDs.Visible = True

            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMOrders, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "Orders"
            oListControl.ReportingYear = _ReportingYear
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)
            '    pnlICD9.Visible = True
            '    pnlICD9.BringToFront()
            'if (cmbInsCompany.DataSource != null)
            '{
            '    for (int i = 0; i < cmbInsCompany.Items.Count; i++)
            '    {
            '        cmbInsCompany.SelectedIndex = i;
            '        cmbInsCompany.Refresh();
            '        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsCompany.SelectedValue), cmbInsCompany.Text);
            '    }
            '}
            AddSelectedItemstoListControl(lstVw_Lab)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLab.Click

        For j As Integer = 1 To C1LabResult.Rows.Count - 1

            If C1LabResult.GetCellCheck(j, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                C1LabResult.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                C1LabResult.SetData(j, 5, "")
                C1LabResult.SetData(j, 6, "")
                C1LabResult.SetData(j, 7, "")
            End If
        Next

        lstVw_Lab.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveLab.Click
        Try
            C1LabResult.Select()
            pnl_tlstrip.Select()
            txtLabResultSearch.Text = ""
            lstVw_Lab.Items.Clear()
            SetLabs_ByTable()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlLab.Visible = False
        End Try
    End Sub

#End Region

#Region "Order/Lab Exclusion Button Click"

    Private Sub btnLabEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Me.Cursor = Cursors.WaitCursor
            FillAllCriteria()
            ' sptRight.Visible = False
            'pnlRight.Visible = True
            'pnlRight.SendToBack()

            'pnlExceptionsLab.Visible = True
            'pnlExceptionsLab.BringToFront()
            'txtLabResultSearch_Ex.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearLabEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'For j As Integer = 1 To C1LabResult_Ex.Rows.Count - 1
        '    If C1LabResult_Ex.GetCellCheck(j, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
        '        C1LabResult_Ex.SetCellCheck(j, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        '        C1LabResult_Ex.SetData(j, 5, "")
        '        C1LabResult_Ex.SetData(j, 6, "")
        '        C1LabResult_Ex.SetData(j, 7, "")
        '    End If
        'Next
        'lstExVw_Lab.Items.Clear()
    End Sub

    Private Sub tsBtn_SaveLab_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    C1LabResult_Ex.Select()
        '    pnl_tlstrip.Select()
        '    txtLabResultSearch_Ex.Text = ""
        '    lstExVw_Lab.Items.Clear()
        '    SetEXLabs_ByTable()
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsLab.Visible = False
        'End Try
    End Sub

#End Region


#Region "Order Template Button Click"

    Private Sub btnRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Me.Cursor = Cursors.WaitCursor
            FillAllCriteria()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnClearOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub tsBtn_SaveRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveRadiology.Click

    End Sub

#End Region

#Region "Order Template Exclusion Button Click"

    Private Sub btnRadiologyEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            FillAllCriteria()
            ' sptRight.Visible = False
            'pnlRight.Visible = True
            'pnlRight.SendToBack()

            'pnlExceptionsRadiology.Visible = True
            'pnlExceptionsRadiology.BringToFront()
            'txtLabsSearch_Ex.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClearOrdersEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'For i As Integer = 1 To c1Labs_Ex.Rows.Count - 1
        '    If c1Labs_Ex.GetCellCheck(i, COL_NAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
        '        c1Labs_Ex.SetCellCheck(i, COL_NAME, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        '    End If
        'Next
        'lstExVw_Orders.Items.Clear()
    End Sub

    Private Sub ts_Btn_SaveRadiology_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Try
        '    pnl_tlstrip.Select()
        '    txtLabsSearch_Ex.Text = ""
        '    lstExVw_Orders.Items.Clear()
        '    SetExOrders_ByTable()

        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsRadiology.Visible = False
        'End Try
    End Sub

#End Region


#Region "SNOMED Button Click"

    Private Sub btnSnomed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSnomed.Click
        ''  ShowSnoMedSelector(TabType.Trigger)
        Try
            '    Me.Cursor = Cursors.WaitCursor
            '    FillAllCriteria()
            '    ' sptRight.Visible = False
            '    pnlRight.Visible = True
            '    pnlRight.SendToBack()

            '    pnlICD9.Visible = True

            '    tsBtn_SaveICD.Visible = True
            '    tsBtn_SaveICD10.Visible = False

            '    GloUC_trvICD9.Visible = True
            '    GloUC_trvICD10.Visible = False

            '    trvselecteICD10s.Visible = False
            '    trvselecteICDs.Visible = True

            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMSnomed, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "Snomed"
            oListControl.ReportingYear = _ReportingYear

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)
            '    pnlICD9.Visible = True
            '    pnlICD9.BringToFront()
            'if (cmbInsCompany.DataSource != null)
            '{
            '    for (int i = 0; i < cmbInsCompany.Items.Count; i++)
            '    {
            '        cmbInsCompany.SelectedIndex = i;
            '        cmbInsCompany.Refresh();
            '        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsCompany.SelectedValue), cmbInsCompany.Text);
            '    }
            '}
            AddSelectedItemstoListControl(lstVw_SnoMed)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnClearSnomed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSnomed.Click
        lstVw_SnoMed.Items.Clear()
    End Sub

    Private Sub btnRemoveSelectedSnomedCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSelectedSnomedCode.Click
        RemoveSelectedItemFromList(lstVw_SnoMed)
    End Sub

#End Region

#Region "SNOMED Exclusion Button Click"

    Private Sub btnSnomedEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowSnoMedSelector(TabType.Exception)
    End Sub

    Private Sub btnClearSnomedEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lstExVw_SnoMed.Items.Clear()
    End Sub

    Private Sub btnRemoveSelectedSnomedCodeEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  RemoveSelectedItemFromList(lstExVw_SnoMed)
    End Sub

#End Region


#Region "ICD 9 Button Click"

    Private Sub btnICD9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnICD9.Click
        Try


            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMICD9, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "ICD9"
            oListControl.ReportingYear = _ReportingYear

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)

            AddSelectedItemstoListControl(lstVw_ICD9)
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnClearICD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearICD.Click
        GloUC_trvICD9.Nodes.Clear()
        lstVw_ICD9.Items.Clear()
        trvselecteICDs.Nodes.Clear()
    End Sub

    Private Sub tsBtn_SaveICD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveICD.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            lstVw_ICD9.Items.Clear()
            GloUC_trvICD9.txtsearch.Text = ""
            If trvselecteICDs.Nodes.Count > 0 Then

                For cptNodeIndex As Integer = 0 To trvselecteICDs.Nodes.Count - 1

                    _listviewItem = New ListViewItem
                    _listviewItem.Text = trvselecteICDs.Nodes(cptNodeIndex).Text
                    _listviewItem.Tag = trvselecteICDs.Nodes(cptNodeIndex)
                    lstVw_ICD9.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            pnlICD9.Visible = False

            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "ICD 9 Exclusion Button Click"

    Private Sub btnICD9Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    FillAllCriteria()
        '    'sptRight.Visible = False
        '    pnlRight.Visible = True
        '    pnlRight.SendToBack()

        '    pnlExceptionsICD9.Visible = True

        '    tsBtn_SaveICD_Ex.Visible = True
        '    tsBtn_SaveICD10_Ex.Visible = False

        '    GloUC_trvICD9_Ex.Visible = True
        '    GloUC_trvICD10_Ex.Visible = False

        '    trvselectedICDs_Ex.Visible = True
        '    trvselectedICD10s_Ex.Visible = False

        '  Label397.Text = "      Selected ICD9"

        '    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCQM_RulesSetup))
        '    Label397.Image = CType(resources.GetObject("btnICD9Ex.Image"), System.Drawing.Image)
        '    resources = Nothing
        '    pnlExceptionsICD9.BringToFront()

        '    Fill_ICD9s("")
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub

    Private Sub btnClearICD9Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'GloUC_trvICD9_Ex.Nodes.Clear()
        'lstExVw_ICD.Items.Clear()
        'trvselectedICDs_Ex.Nodes.Clear()
    End Sub

    Private Sub tsBtn_SaveICD_Ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_ICD.Items.Clear()
        '    GloUC_trvICD9_Ex.txtsearch.Text = ""
        '    If trvselectedICDs_Ex.Nodes.Count > 0 Then
        '        For cptNodeIndex As Integer = 0 To trvselectedICDs_Ex.Nodes.Count - 1
        '            _listviewItem = New ListViewItem
        '            _listviewItem.Text = trvselectedICDs_Ex.Nodes(cptNodeIndex).Text
        '            _listviewItem.Tag = trvselectedICDs_Ex.Nodes(cptNodeIndex)
        '            lstExVw_ICD.Items.Add(_listviewItem)
        '            _listviewItem = Nothing
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsICD9.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        'End Try
    End Sub

#End Region


#Region "ICD 10 Button Click"
    Dim oListControl As gloListControl.gloListControl
    Private Sub btnICD10_Click(sender As System.Object, e As System.EventArgs) Handles btnICD10.Click
        Try


            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMICD10, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "ICD10"
            oListControl.ReportingYear = _ReportingYear

            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)

            AddSelectedItemstoListControl(lstVw_ICD10)
            '
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub oListControl_ItemSelectedClick(sender As System.Object, e As System.EventArgs)

        '  If (oListControl.SelectedItems.Count > 0) Then
        Select Case oListControl.ControlHeader
            Case "ICD10"
                AddItems(lstVw_ICD10)
                RemoveUnwantedScrollbar(lstVw_ICD10)
            Case "ICD9"
                AddItems(lstVw_ICD9)
                RemoveUnwantedScrollbar(lstVw_ICD9)
            Case "Snomed"
                AddItems(lstVw_SnoMed)
                RemoveUnwantedScrollbar(lstVw_SnoMed)
            Case "Orders"
                AddItems(lstVw_Lab)
                RemoveUnwantedScrollbar(lstVw_Lab)
            Case "RXNORM"
                AddItems(lstVw_Drugs)
                RemoveUnwantedScrollbar(lstVw_Drugs)
            Case "CPT"
                AddItems(lstVw_CPT)
                RemoveUnwantedScrollbar(lstVw_CPT)


            Case "CVX"
                AddItems(lstVw_CVX)
                RemoveUnwantedScrollbar(lstVw_CVX)
        End Select

        ' End If
    End Sub
    Private Sub RemoveUnwantedScrollbar(ByRef lv As ListView)
        'lv.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.None)


        lv.View = Windows.Forms.View.Details
        lv.Columns(0).Width = lv.Width - 5
        '  lv.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub
    Dim strdata As String() = {}
    Private Sub AddSelectedItemstoListControl(ByRef listcontrol As ListView)

        For Each item As ListViewItem In listcontrol.Items
            strdata = Convert.ToString(item.Text).Split(":")
            If (strdata.Length > 1) Then
                oListControl.SelectedItems.Add(strdata(0), strdata(1))
            End If
        Next
       

    End Sub

    Private Sub AddItems(ByRef listcontrol As ListView)
        listcontrol.Items.Clear()
        For i As Integer = 0 To oListControl.SelectedItems.Count - 1
            listcontrol.Items.Add(oListControl.SelectedItems(i).Code & ": " & oListControl.SelectedItems(i).Description)

        Next

    End Sub
    Private Sub oListControl_ItemClosedClick(sender As System.Object, e As System.EventArgs)
        If Not IsNothing(oListControl) Then
            Me.Controls.Remove(oListControl)
            RemoveHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            RemoveHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick
            oListControl.Dispose()
            oListControl = Nothing
        End If
    End Sub


    Private Sub btnClearICD10_Click(sender As System.Object, e As System.EventArgs) Handles btnClearICD10.Click
        GloUC_trvICD10.Nodes.Clear()
        lstVw_ICD10.Items.Clear()
        trvselecteICD10s.Nodes.Clear()
    End Sub

    Private Sub tsBtn_SaveICD10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsBtn_SaveICD10.Click
        Dim _listviewItem As ListViewItem = Nothing
        Try
            lstVw_ICD10.Items.Clear()
            GloUC_trvICD10.txtsearch.Text = ""
            If trvselecteICD10s.Nodes.Count > 0 Then
                For cptNodeIndex As Integer = 0 To trvselecteICD10s.Nodes.Count - 1
                    _listviewItem = New ListViewItem
                    _listviewItem.Text = trvselecteICD10s.Nodes(cptNodeIndex).Text
                    _listviewItem.Tag = trvselecteICD10s.Nodes(cptNodeIndex)
                    lstVw_ICD10.Items.Add(_listviewItem)
                    _listviewItem = Nothing
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            pnlICD9.Visible = False
            If Not IsNothing(_listviewItem) Then
                _listviewItem = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "ICD 10 Exclusion Button Click"

    Private Sub btnICD10Ex_Click(sender As System.Object, e As System.EventArgs)
        'Try
        '    Me.Cursor = Cursors.WaitCursor
        '    FillAllCriteria()
        '    '   sptRight.Visible = False
        '    pnlRight.Visible = True
        '    pnlRight.SendToBack()

        '    pnlExceptionsICD9.Visible = True

        '    tsBtn_SaveICD_Ex.Visible = False
        '    tsBtn_SaveICD10_Ex.Visible = True

        '    GloUC_trvICD9_Ex.Visible = False
        '    GloUC_trvICD10_Ex.Visible = True

        '    trvselectedICDs_Ex.Visible = False
        '    trvselectedICD10s_Ex.Visible = True

        '  Label397.Text = "      Selected ICD10"

        '    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCQM_RulesSetup))
        '    Label397.Image = CType(resources.GetObject("btnICD10Ex.Image"), System.Drawing.Image)
        '    resources = Nothing

        '    pnlExceptionsICD9.BringToFront()

        '    Fill_ICD10s("")
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub

    Private Sub btnClearICD10Ex_Click(sender As System.Object, e As System.EventArgs)
        'GloUC_trvICD10_Ex.Nodes.Clear()
        'lstExVw_ICD10.Items.Clear()
        'trvselectedICD10s_Ex.Nodes.Clear()
    End Sub

    Private Sub tsBtn_SaveICD10_Ex_Click(sender As System.Object, e As System.EventArgs)
        'Dim _listviewItem As ListViewItem = Nothing
        'Try
        '    lstExVw_ICD10.Items.Clear()
        '    GloUC_trvICD10_Ex.txtsearch.Text = ""
        '    If trvselectedICD10s_Ex.Nodes.Count > 0 Then
        '        For cptNodeIndex As Integer = 0 To trvselectedICD10s_Ex.Nodes.Count - 1
        '            _listviewItem = New ListViewItem
        '            _listviewItem.Text = trvselectedICD10s_Ex.Nodes(cptNodeIndex).Text
        '            _listviewItem.Tag = trvselectedICD10s_Ex.Nodes(cptNodeIndex)
        '            lstExVw_ICD10.Items.Add(_listviewItem)
        '            _listviewItem = Nothing
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString, _sMessageBoxString, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    pnlExceptionsICD9.Visible = False
        '    If Not IsNothing(_listviewItem) Then
        '        _listviewItem = Nothing
        '    End If
        '   End Try
    End Sub

#End Region

    Private Sub GloUC_trvICD9_SearchFired() Handles GloUC_trvICD9.SearchFired

    End Sub

    Private Sub GloUC_trvICD9_Ex_SearchFired()

    End Sub

    Private Sub GloUC_trvICD10_SearchFired() Handles GloUC_trvICD10.SearchFired

    End Sub

    Private Sub GloUC_trvICD10_Ex_SearchFired()

    End Sub

    Private Sub btn_MouseHover(sender As System.Object, e As System.EventArgs)
        Try
            If sender IsNot Nothing Then
                Dim btn As Button = DirectCast(sender, Button)
                If btn IsNot Nothing Then
                    If DirectCast(btn.Parent, System.Windows.Forms.Panel).Dock = DockStyle.Top Then
                        '  btn.BackgroundImage = My.Resources.Img_LongYellow
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    Else
                        '  btn.BackgroundImage = My.Resources.Img_LongYellow
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return
        End Try

    End Sub

    Private Sub btn_MouseLeave(sender As System.Object, e As System.EventArgs)
        Try
            If sender IsNot Nothing Then
                Dim btn As Button = DirectCast(sender, Button)
                If btn IsNot Nothing Then
                    If DirectCast(btn.Parent, System.Windows.Forms.Panel).Dock = DockStyle.Top Then
                        '  btn.BackgroundImage = My.Resources.Img_LongOrange
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    Else
                        ' btn.BackgroundImage = My.Resources.Img_LongButton
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return
        End Try
    End Sub

    Private Sub btnRemoveSelectedICD10_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedICD10.Click
        RemoveSelectedItemFromList(lstVw_ICD10)
    End Sub

    Private Sub btnRemoveSelectedLab_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedLab.Click
        RemoveSelectedItemFromList(lstVw_Lab)
    End Sub

    Private Sub btnRemoveSelectedICD9_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedICD9.Click
        RemoveSelectedItemFromList(lstVw_ICD9)
    End Sub

    Private Sub btnRemoveSelectedCPT_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedCPT.Click
        RemoveSelectedItemFromList(lstVw_CPT)
    End Sub

    Private Sub btnRemoveSelectedDrug_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedDrug.Click
        RemoveSelectedItemFromList(lstVw_Drugs)
    End Sub

    Private Sub cmb_measure_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        '  reterivedata()

    End Sub
    Dim dsdata As DataSet = Nothing
    Private dtdatalstview As DataTable = Nothing
    '  Private datacode As DataTable = Nothing
    Private drrx As DataRow()
    Private drhcp As DataRow()
    Dim drsno As DataRow()
    Dim drord As DataRow()
    Dim drICD9 As DataRow()
    Dim drICD10 As DataRow()
    Dim drCVX As DataRow()
    Private Sub reterivedata()
        Dim oParameters As New gloDatabaseLayer.DBParameters
        Dim str As String = cmb_measure.SelectedValue
        '  Dim cmsno = str.Substring(0, str.IndexOf("v")).Replace("CMS", "").Replace("NQF", "")
        Dim cmsno = str
        oParameters.Add("@cmsID", cmsno, ParameterDirection.Input, SqlDbType.VarChar)
        oParameters.Add("@PopulationNo", _PopulationNo, ParameterDirection.Input, SqlDbType.Int)
        oParameters.Add("@ReportingYear", _ReportingYear, ParameterDirection.Input, SqlDbType.VarChar)
        If (Not IsNothing(dsdata)) Then
            dsdata.Tables.Clear()
            dsdata.Dispose()
            dsdata = Nothing
        End If
        dsdata = clsCQMMeasure.GetdataWithParamDataset(oParameters, "gsp_getCQMCriteriaDetails")
        oParameters.Clear()
        oParameters.Dispose()
        oParameters = Nothing

        lstVw_ICD9.Items.Clear()
        lstVw_ICD10.Items.Clear()
        lstVw_SnoMed.Items.Clear()
        lstVw_Lab.Items.Clear()
        lstVw_Drugs.Items.Clear()
        lstVw_CPT.Items.Clear()
        lstVw_CVX.Items.Clear()
        If (dsdata.Tables.Count > 0) Then
            dtdatalstview = dsdata.Tables(0)
            Try

                If Not IsNothing(drrx) Then
                    If (drrx.Length > 1) Then
                        Array.Clear(drrx, 0, drrx.Length - 1)
                    End If
                End If
                If Not IsNothing(drhcp) Then
                    If (drhcp.Length > 1) Then
                        Array.Clear(drhcp, 0, drhcp.Length - 1)
                    End If
                End If
                If Not IsNothing(drsno) Then
                    If (drsno.Length > 1) Then
                        Array.Clear(drsno, 0, drsno.Length - 1)
                    End If
                End If
                If Not IsNothing(drord) Then
                    If (drord.Length > 1) Then
                        Array.Clear(drord, 0, drord.Length - 1)
                    End If
                End If

                If Not IsNothing(drICD9) Then
                    If (drICD9.Length > 1) Then
                        Array.Clear(drICD9, 0, drICD9.Length - 1)
                    End If
                End If
                If Not IsNothing(drICD10) Then
                    If (drICD10.Length > 1) Then
                        Array.Clear(drICD10, 0, drICD10.Length - 1)
                    End If
                End If
                If Not IsNothing(drCVX) Then
                    If (drCVX.Length > 1) Then
                        Array.Clear(drCVX, 0, drCVX.Length - 1)
                    End If
                End If

                drrx = dtdatalstview.Select("CodeType = " & gloListControl.gloListControlType.CQMRxNorm)
                drhcp = dtdatalstview.Select("CodeType= " & gloListControl.gloListControlType.CQMHCPCS)

                drsno = dtdatalstview.Select("CodeType = " & gloListControl.gloListControlType.CQMSnomed)
                drord = dtdatalstview.Select("CodeType= " & gloListControl.gloListControlType.CQMOrders)

                drICD9 = dtdatalstview.Select("CodeType = " & gloListControl.gloListControlType.CQMICD9)
                drICD10 = dtdatalstview.Select("CodeType = " & gloListControl.gloListControlType.CQMICD10)
                drCVX = dtdatalstview.Select("CodeType = " & gloListControl.gloListControlType.CQMCVX)
                ' Dim syn As WindowsFormsSynchronizationContext
                ' Me.syn = WindowsFormsSynchronizationContext.Current
                ' Dim ui = TaskScheduler.FromCurrentSynchronizationContext()
                'Parallel.Invoke(AddressOf SetDatatoListViewICD9, AddressOf SetDatatoListViewICD10, AddressOf SetDatatoListViewSno, AddressOf SetDatatoListViewOrd, AddressOf SetDatatoListViewrx, AddressOf SetDatatoListViewHcpcs, AddressOf SetDatatoListViewcvx)

                'ListView.CheckForIllegalCrossThreadCalls = True
                '   Task.Factory.StartNew(AddressOf SetdataListView, TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskICD9 = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewICD9(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskICD10 = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewICD10(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskHcpcs = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewHcpcs(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskord = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewOrd(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskrx = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewrx(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim tasksno = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewSno(), TaskScheduler.FromCurrentSynchronizationContext())
                'Dim taskcvx = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewcvx(), TaskScheduler.FromCurrentSynchronizationContext())


                Dim taskICD9 = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewICD9())
                Dim taskICD10 = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewICD10())
                Dim taskHcpcs = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewHcpcs())
                Dim taskord = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewOrd())
                Dim taskrx = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewrx())
                Dim tasksno = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewSno())
                Dim taskcvx = Task(Of List(Of ListViewItem)).Factory.StartNew(Function() SetDatatoListViewcvx())

                lstVw_ICD9.BeginUpdate()
                lstVw_ICD9.Items.AddRange(taskICD9.Result.ToArray())
                lstVw_ICD9.EndUpdate()

                lstVw_ICD10.BeginUpdate()
                lstVw_ICD10.Items.AddRange(taskICD10.Result.ToArray())
                lstVw_ICD10.EndUpdate()

                lstVw_CPT.BeginUpdate()
                lstVw_CPT.Items.AddRange(taskHcpcs.Result.ToArray())
                lstVw_CPT.EndUpdate()


                lstVw_Lab.BeginUpdate()
                lstVw_Lab.Items.AddRange(taskord.Result.ToArray())
                lstVw_Lab.EndUpdate()

                lstVw_Drugs.BeginUpdate()
                lstVw_Drugs.Items.AddRange(taskrx.Result.ToArray())
                lstVw_Drugs.EndUpdate()

                lstVw_SnoMed.BeginUpdate()
                lstVw_SnoMed.Items.AddRange(tasksno.Result.ToArray())
                lstVw_SnoMed.EndUpdate()

                lstVw_CVX.BeginUpdate()
                lstVw_CVX.Items.AddRange(taskcvx.Result.ToArray())
                lstVw_CVX.EndUpdate()

               
                taskICD9.Dispose()
                taskICD10.Dispose()
                taskHcpcs.Dispose()
                taskord.Dispose()
                taskrx.Dispose()
                tasksno.Dispose()
                taskcvx.Dispose()
            Catch ex As Exception
                '  MessageBox.Show(ex.Message.ToString())
            End Try

        End If
        ClearVitalsInfo()
        If (dsdata.Tables.Count > 1) Then

            SetVitalsInfo(dsdata.Tables(1))
            RemoveUnwantedScrollbar(lstVw_CPT)
            RemoveUnwantedScrollbar(lstVw_ICD10)
            RemoveUnwantedScrollbar(lstVw_ICD9)
            RemoveUnwantedScrollbar(lstVw_Drugs)
            RemoveUnwantedScrollbar(lstVw_SnoMed)
            RemoveUnwantedScrollbar(lstVw_Lab)
            RemoveUnwantedScrollbar(lstVw_CVX)
        End If
    End Sub
    'Private Function SetdataListView() As List(Of ListViewItem)
    '    Dim items As New List(Of ListViewItem)
    '    Try



    '        SetDatatoListViewICD9(Items)
    '        'SetDatatoListViewICD10()
    '        'SetDatatoListViewSno()
    '        'SetDatatoListViewOrd()
    '        'SetDatatoListViewrx()
    '        'SetDatatoListViewHcpcs()
    '        'SetDatatoListViewcvx()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message.ToString())
    '    End Try
    '    Return Items
    'End Function



    Private Function SetDatatoListViewrx() As List(Of ListViewItem)
        Dim items As New List(Of ListViewItem)
        ' lstVw_Drugs.BeginUpdate()
        Try
            For Each dr As DataRow In drrx
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            '  lstVw_Drugs.Items.AddRange(items.ToArray())
            '  lstVw_Drugs.EndUpdate()
        Catch ex As Exception

        End Try
        Return items

    End Function


    Private Function SetDatatoListViewcvx() As List(Of ListViewItem)
        Dim items As New List(Of ListViewItem)
        '  lstVw_CVX.BeginUpdate()
        Try
            For Each dr As DataRow In drCVX
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            '   lstVw_CVX.Items.AddRange(items.ToArray())
            '  lstVw_CVX.EndUpdate()
        Catch ex As Exception

        End Try
        Return items


    End Function
    Private Function SetDatatoListViewHcpcs() As List(Of ListViewItem)
        Dim items As New List(Of ListViewItem)
        ' lstVw_CPT.BeginUpdate()
        Try
            For Each dr As DataRow In drhcp
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            '  lstVw_CPT.Items.AddRange(items.ToArray())
            '  lstVw_CPT.EndUpdate()
        Catch ex As Exception

        End Try

        Return items

    End Function

    Private Function SetDatatoListViewSno() As List(Of ListViewItem)


        ' lstVw_SnoMed.BeginUpdate()
        Dim items As New List(Of ListViewItem)
        Try
            For Each dr As DataRow In drsno
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            '  lstVw_SnoMed.Items.AddRange(items.ToArray())
            '   lstVw_SnoMed.EndUpdate()

        Catch ex As Exception

        End Try

        Return items

    End Function

    Private Function SetDatatoListViewOrd() As List(Of ListViewItem)
        Dim items As New List(Of ListViewItem)
        ' lstVw_Lab.BeginUpdate()
        Try
            For Each dr As DataRow In drord
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            '   lstVw_Lab.Items.AddRange(items.ToArray())
            '  lstVw_Lab.EndUpdate()
        Catch ex As Exception

        End Try

        Return items

    End Function

    Private Function SetDatatoListViewICD9() As List(Of ListViewItem)
        Dim items As New List(Of ListViewItem)
        '  lstVw_ICD9.BeginUpdate()
        Try
            For Each dr As DataRow In drICD9
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            ' lstVw_ICD9.Items.AddRange(items.ToArray())
            ' lstVw_ICD9.EndUpdate()

        Catch ex As Exception

        End Try
        Return items

    End Function

    Private Function SetDatatoListViewICD10() As List(Of ListViewItem)

        Dim items As New List(Of ListViewItem)
        'lstVw_ICD10.BeginUpdate()
        Try
            For Each dr As DataRow In drICD10
                items.Add(New ListViewItem(Convert.ToString(dr("ConcatCodeDesc"))))
            Next
            ' lstVw_ICD10.Items.AddRange(items.ToArray())
            ' lstVw_ICD10.EndUpdate()
        Catch ex As Exception

        End Try
        Return items


    End Function

    Private Sub tlsDM_Save_Click(sender As System.Object, e As System.EventArgs) Handles tlsDM_Save.Click

    End Sub




    Private Sub chkloadcqmdata_CheckedChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub frmCQM_RulesSetup_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        '  Me.Text = "Quality_Measure_Workflow_TRIARQ-CMS117-NQF0038"
        CQMDetails()
    End Sub

    Private Sub txtWeightMin_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub txtName_TextChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub cmb_measure_SelectedIndexChanged1(sender As Object, e As System.EventArgs)
        reterivedata()
    End Sub

    Private Sub cmb_measure_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub lstVw_ICD9_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstVw_ICD9.MouseMove, lstVw_ICD10.MouseMove, lstVw_SnoMed.MouseMove, lstVw_Lab.MouseMove, lstVw_Drugs.MouseMove, lstVw_CPT.MouseMove
        'Try



        '    Dim ListIteminfo As ListViewHitTestInfo = CType(sender, ListView).HitTest(e.X, e.Y)
        '    Dim X As Integer = 0

        '    Dim CharLimit As Integer = 120
        '    If (Not IsNothing(ListIteminfo.Item)) Then
        '        If (ListIteminfo.Item.Text.Length > 70) Then
        '            If (e.X > 300) Then
        '                'X = CType(sender, ListView).Location.X
        '                'Screen.PrimaryScreen.WorkingArea.Width

        '                X = e.X - (CType(sender, ListView).Width - 200)
        '            Else
        '                X = e.X
        '            End If
        '            Dim lstname As String = CType(sender, ListView).Name
        '            If (lstname.Contains("SnoMed") Or lstname.Contains("Lab") Or lstname.Contains("Drug")) Then
        '                CharLimit = 50
        '                If (e.X > 300) Then
        '                    X = e.X
        '                End If
        '            End If
        '            SetListViewToolTip(ListIteminfo, X, e.Y, CharLimit)
        '        Else
        '            ToolTip2.Hide(CType(sender, ListView))
        '        End If
        '    Else


        '        ToolTip2.Hide(CType(sender, ListView))

        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    ' Private Sub SetListViewToolTip(ByVal Info As ListViewHitTestInfo, ByVal X As Integer, ByVal Y As Integer, ByVal CharLimit As Integer)
    'Dim str As String = Info.Item.Text
    'Dim cntchar As Integer = 0
    'Dim sb As New System.Text.StringBuilder()
    'If (str.Length > CharLimit) Then
    '    Dim splstr As String() = str.Split(" ")


    '    For Len As Integer = 0 To splstr.Length - 1
    '        cntchar += splstr(Len).Length
    '        sb.Append(splstr(Len))
    '        sb.Append(" ")
    '        If (cntchar >= CharLimit) Then
    '            sb.Append(Environment.NewLine)
    '            cntchar = 0
    '        End If
    '    Next
    'Else
    '    sb.Append(str)
    'End If
    'ToolTip2.Show(sb.ToString(), Info.Item.ListView, X, Y, 20000)
    'sb.Clear()
    'sb = Nothing
    'End Sub


    Private Sub lnkhelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkhelp.LinkClicked
        'Me.Activate()
        RaiseEvent EvtShowHelp()
        'Dim frmobjDb As Object = Application.OpenForms("MainMenu")
        'frmobjDb.ShowHelpobj(Me)
    End Sub

    Private Sub btncvx_Click(sender As System.Object, e As System.EventArgs) Handles btncvx.Click
        Try


            oListControl = New gloListControl.gloListControl(_databaseConnectionString, gloListControl.gloListControlType.CQMCVX, True, Me.Width)
            oListControl.CMSID = cmb_measure.SelectedValue.ToString()
            oListControl.ClinicID = 1
            oListControl.ControlHeader = "CVX"
            oListControl.ReportingYear = _ReportingYear
            AddHandler oListControl.ItemSelectedClick, AddressOf oListControl_ItemSelectedClick
            AddHandler oListControl.ItemClosedClick, AddressOf oListControl_ItemClosedClick

            Me.Controls.Add(oListControl)

            AddSelectedItemstoListControl(lstVw_CVX)
            '
            oListControl.OpenControl()
            oListControl.Dock = DockStyle.Fill
            oListControl.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRemoveSelectedCVX_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveSelectedCVX.Click
        RemoveSelectedItemFromList(lstVw_CVX)
    End Sub

    Private Sub btnClearCVX_Click(sender As System.Object, e As System.EventArgs) Handles btnClearCVX.Click
        'GloUC_trvC.Nodes.Clear()
        lstVw_CVX.Items.Clear()
        ' trvselecteICD10s.Nodes.Clear()
    End Sub
End Class

