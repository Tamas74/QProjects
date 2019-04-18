Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Text
Imports gloTaskMail
Imports gloEMRReports
Imports System.Data.SqlClient


Public Class frmLab_RequestOrder
    'code added by supriya --HL7 15/7/2009
    Private WithEvents oGeneralInterface As clsGeneralInterface
    Private blnIsInvalidHL7FilePath As Boolean
    'code added by supriya --HL7 15/7/2009

    Private Const COL_ID As Int16 = 0
    Private Const COL_CODE As Int16 = 1
    Private Const COL_NAME As Int16 = 2
    Private Const COL_DIGNOSISCODE As Int16 = 3
    Private Const COL_CPTCODE As Int16 = 4
    Private Const COL_COMMENT As Int16 = 5
    Private Const COL_PRECAUTION As Int16 = 6
    Private Const COL_INSTRUCTION As Int16 = 7
    Private Const COL_COUNT As Int16 = 8

    ''by sudhir 20081114
    Public Event OrderConfirmed()
    ''Sudhir 20090207
    Public Event On_LabClosed()

    Private PendingLabOrderId As Int64
    Private PendingLabPatientId As Int64
    ''End Sudhir

    'Order Information
    Private _OrderNumberPrefix As String = ""
    Private _OrderNumberID As Int16 = 0
    Private _OrderID As Int64 = 0

    Private _OrderParamter As LabRequestOrderParameter
    Public blnOpenFromExam As Boolean = False
    Public myCaller As frmPatientExam
    'variable added by dipak 20090921 for use to call GetdataFromOtherForms() of calling form in frmLab_RequestOrder
    Public myCaller1 As Object
    Public _strLabTestName As String
    Public _strLabResultName As String

    Public _UserName As String
    Public _ProviderName As String
    Public _UserID As Int64

    Public _LoginProviderID As Int64 ' from emr login
    Public _PatientProviderID As Int64 ' default from patient master
    Public _LabProviderID As Int64 ' from saved lab order
    Dim _blnRecordLock As Boolean

    Public _arrLabs As New ArrayList
    Public OrderID As Int64
    Public VisitID As Int64
    Public ProviderID As Int64
    Public OrderPrefix As String
    ''Dhruv 20091207 -------------------------
    Dim _CloseClicked As Boolean = False
    ''----------------------------------------
    'sarika 28th sept 07
    'flag to check whether the order is saved or not
    Dim blnSaved As Boolean = True
    '-----------------------------------------
    Public bIsOpenfrmOutstanding As Boolean = False

    Dim oLabActor_Order1 As New LabActor.LabOrder

    '' By Mahesh 20080528
    '' To Check if Labs is Opened from Tasks
    Public blnOpenFromTask As Boolean = False
    ''

    ''20091026
    Dim reportViewer As gloEMRReportViewer


    Dim WithEvents oViewDocument As gloEDocumentV3.gloEDocV3Management
    '#Region " TO Check the Multiple instances Of Form "

    '    '' TO Keep track that the Form's Instance is Disposed or not
    '    Private blnDisposed As Boolean
    '    '' Private Shared _mu As New Mutex
    '    Private Shared frm As frmLab_RequestOrder

    '    ''Form overrides dispose to clean up the component list.
    '    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
    '        ' Check to see if Dispose has already been called.
    '        If Not (Me.blnDisposed) Then
    '            ' If disposing equals true, dispose all managed
    '            ' and unmanaged resources.
    '            If (disposing) Then
    '                ' Dispose managed resources.
    '                If Not (components Is Nothing) Then
    '                    components.Dispose()
    '                End If
    '                'frm = Nothing
    '            End If
    '            ' Release unmanaged resources. If disposing is false,
    '            ' only the following code is executed.

    '            ' Note that this is not thread safe.
    '            ' Another thread could start disposing the object
    '            ' after the managed resources are disposed,
    '            ' but before the disposed flag is set to true.
    '            ' If thread safety is necessary, it must be
    '            ' implemented by the client.
    '        End If
    '        frm = Nothing
    '        Me.blnDisposed = True

    '    End Sub

    '    Public Overloads Sub Dispose()
    '        Dispose(True)
    '        ' Take yourself off of the finalization queue
    '        ' to prevent finalization code for this object
    '        ' from executing a second time.
    '        GC.SuppressFinalize(Me)
    '    End Sub

    '    'Protected Overrides Sub Finalize()
    '    '    Dispose(False)
    '    'End Sub

    '    Public Shared Function GetInstance() As frmLab_RequestOrder
    '        '_mu.WaitOne()
    '        Try
    '            If frm Is Nothing Then
    '                frm = New frmLab_RequestOrder
    '            End If
    '        Finally
    '            '_mu.ReleaseMutex()
    '        End Try
    '        Return frm
    '    End Function

    '#End Region

    Public Property LabOrderParameter() As LabRequestOrderParameter
        Get
            Return _OrderParamter
        End Get
        Set(ByVal value As LabRequestOrderParameter)
            _OrderParamter = value
        End Set
    End Property



#Region "Test Related Functionality"
    'Fill Tests
    Private Sub FillTests()
        Dim oLabTests As New LabActor.Tests
        Dim oLabTest As New LabActor.Test
        Dim oTest As New gloEMRLabTest
        trvList.Visible = False
        Try

            trvList.Nodes.Clear()
            trvList.BeginUpdate()
            oLabTests = oTest.GetTests(False)
            With trvList
                If oLabTests.Count > 0 Then
                    For i As Integer = 0 To oLabTests.Count - 1
                        Dim trvnode As New TreeNode
                        oLabTest = oLabTests.Item(i)
                        trvnode.Text = oLabTest.Name
                        trvnode.Tag = oLabTest.TestID
                        trvnode.ImageIndex = 0
                        trvnode.SelectedImageIndex = 0
                        .Nodes.Add(trvnode)
                    Next
                End If
            End With
            trvList.EndUpdate()
        Catch ex As Exception
            Throw ex
        End Try
        trvList.Visible = True
    End Sub
    ''sandip darade 20090520
    ''Using tree view user control
    Private Sub FillTests_NEW()
        Dim oLabTests As New LabActor.Tests
        Dim oLabTest As New LabActor.Test
        Dim oTest As New gloEMRLabTest

        Dim dt As New DataTable
        Dim Col2 As New DataColumn("TestID")
        Col2.DataType = System.Type.GetType("System.Decimal")
        dt.Columns.Add(Col2)
        Dim Col3 As New DataColumn("TestName")
        Col3.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(Col3)
        Try

            oLabTests = oTest.GetTests(False)

            If oLabTests.Count > 0 Then
                Dim row As DataRow = Nothing
                ''Add data from the object to a datatable 
                For i As Integer = 0 To oLabTests.Count - 1
                    Dim trvnode As New TreeNode
                    oLabTest = oLabTests.Item(i)
                    row = dt.NewRow

                    row("TestName") = oLabTest.Name
                    row("TestID") = oLabTest.TestID
                    dt.Rows.Add(row)
                Next
            End If
            GloUC_trvTest.ParentMember = Nothing
            If Not IsNothing(dt) Then
                GloUC_trvTest.ImageIndex = 0
                GloUC_trvTest.SelectedImageIndex = 0
                GloUC_trvTest.DataSource = dt
                GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvTest.CodeMember = Convert.ToString(dt.Columns("TestName").ColumnName)
                GloUC_trvTest.ValueMember = Convert.ToString(dt.Columns("TestID").ColumnName)
                GloUC_trvTest.DescriptionMember = Convert.ToString(dt.Columns("TestName").ColumnName)
                GloUC_trvTest.FillTreeView()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Fill Groups
    Private Sub FillGroups()
        Dim oLabGroups As New LabActor.LabGroups
        Dim oGroup As New gloEMRLabGroup

        Dim ndGroup As TreeNode, ndTest As TreeNode
        trvList.Visible = True
        Try
            trvList.BeginUpdate()
            trvList.Nodes.Clear()
            oLabGroups = oGroup.GetGroups()
            With trvList
                If oLabGroups.Count > 0 Then
                    For i As Integer = 0 To oLabGroups.Count - 1
                        ndGroup = New TreeNode
                        With ndGroup
                            .Text = oLabGroups.Item(i).LabGroupName
                            .Tag = oLabGroups.Item(i).LabGroupID
                            .ImageIndex = 1
                            .SelectedImageIndex = 1
                            For j As Integer = 0 To oLabGroups.Item(i).Tests.Count - 1
                                ndTest = New TreeNode
                                With ndTest
                                    .Text = oLabGroups.Item(i).Tests.Item(j).Name
                                    .Tag = oLabGroups.Item(i).Tests.Item(j).TestID
                                    ''sarika Add Groups 20090418
                                    '.Name = "Group"
                                    ''--
                                    .ImageIndex = 0
                                    .SelectedImageIndex = 0
                                    ndGroup.Nodes.Add(ndTest)
                                End With
                                ndTest = Nothing
                            Next
                        End With
                        .Nodes.Add(ndGroup)
                        ndGroup = Nothing
                    Next
                End If
                .ExpandAll()
            End With
            trvList.EndUpdate()
        Catch ex As Exception
            Throw ex
        End Try
        trvList.Visible = True
    End Sub
    ''sandip darade 20090520
    ''Using tree view user control
    Private Sub FillGroups_NEW()
        Dim oLabGroups As New LabActor.LabGroups
        Dim oGroup As New gloEMRLabGroup
        Dim dt As New DataTable
        Dim Col0 As New DataColumn("GroupID")
        Col0.DataType = System.Type.GetType("System.Decimal")
        dt.Columns.Add(Col0)
        Dim Col1 As New DataColumn("GroupName")
        Col1.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(Col1)
        Dim Col2 As New DataColumn("TestID")
        Col2.DataType = System.Type.GetType("System.Decimal")
        dt.Columns.Add(Col2)
        Dim Col3 As New DataColumn("TestName")
        Col3.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(Col3)

        Try

            oLabGroups = oGroup.GetGroups()
            Dim row As DataRow = Nothing
            If oLabGroups.Count > 0 Then
                For i As Integer = 0 To oLabGroups.Count - 1
                    Dim _groupName As String
                    Dim _groupID As Int64

                    _groupName = oLabGroups.Item(i).LabGroupName
                    _groupID = oLabGroups.Item(i).LabGroupID

                    For j As Integer = 0 To oLabGroups.Item(i).Tests.Count - 1

                        row = dt.NewRow
                        row("GroupName") = _groupName
                        row("GroupID") = _groupID
                        row("TestName") = oLabGroups.Item(i).Tests.Item(j).Name
                        row("TestID") = oLabGroups.Item(i).Tests.Item(j).TestID
                        dt.Rows.Add(row)
                    Next
                Next
            End If
            If Not IsNothing(dt) Then
                GloUC_trvTest.ImageIndex = 0
                GloUC_trvTest.SelectedImageIndex = 0
                GloUC_trvTest.ParentImageIndex = 1
                GloUC_trvTest.SelectedParentImageIndex = 1
                GloUC_trvTest.DataSource = dt
                GloUC_trvTest.ParentMember = "GroupName"
                GloUC_trvTest.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
                GloUC_trvTest.CodeMember = Convert.ToString(dt.Columns("TestName").ColumnName)
                GloUC_trvTest.ValueMember = Convert.ToString(dt.Columns("TestID").ColumnName)
                GloUC_trvTest.DescriptionMember = Convert.ToString(dt.Columns("TestName").ColumnName)
                GloUC_trvTest.FillTreeView()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Test Click
    Private Sub btnTests_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTests.Click
        'sarika Add Groups 20090418
        'If btnTests.Dock = DockStyle.Bottom Then
        '    btnTests.Dock = DockStyle.Top
        '    btnGroups.Dock = DockStyle.Bottom
        'End If

        pnl_btnTests.Dock = DockStyle.Top
        btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnTests.BackgroundImageLayout = ImageLayout.Stretch


        pnl_btnGroups.Dock = DockStyle.Bottom
        btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnTests.BackgroundImageLayout = ImageLayout.Stretch

        '--


        'fill the trvList treeview with the Lab Tests
        'create a Test object collection for storing all the Lab tests
        Try
            ' FillTests()
            FillTests_NEW()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try


    End Sub

    'Group Click
    Private Sub btnGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroups.Click

        pnl_btnGroups.Dock = DockStyle.Top
        btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnTests.BackgroundImageLayout = ImageLayout.Stretch

        pnl_btnTests.Dock = DockStyle.Bottom
        btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnTests.BackgroundImageLayout = ImageLayout.Stretch


        'fill the trvList treeview with the Lab Tests

        Try
            ' FillGroups()
            FillGroups_NEW()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


    End Sub

    'Add Test or Group
    Private Sub trvList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvList.DoubleClick
        If Not trvList.SelectedNode Is Nothing Then
            Dim oNode As TreeNode = trvList.SelectedNode
            If pnl_btnTests.Dock = DockStyle.Top Then
                ' ''  Add Test TO The Orders
                gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Lab Test Added ", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)
            ElseIf pnl_btnGroups.Dock = DockStyle.Top Then
                ' ''  Add Test from Groups TO The Orders
                If IsNothing(oNode.Parent) Then
                    '' Selected Node is Group
                    Dim oChildNode As TreeNode
                    For Each oChildNode In oNode.Nodes
                        ' ''  Add Test from Groups TO The Orders
                        gloUCLab_Transaction.AddTest(0, oChildNode.Tag, oChildNode.Text, 1, "")
                        ' gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Lab Test Added ", gstrLoginName, gstrClientMachineName, gnPatientID)
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)

                    Next
                Else
                    '' Selected Node is Test Under Some Group
                    ' ''  Add Test TO The Orders
                    gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                    ' gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Lab Test Added ", gstrLoginName, gstrClientMachineName, gnPatientID)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)

                End If
            End If
        End If
    End Sub

    Private Sub txtListSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtListSearch.KeyPress
        If trvList.GetNodeCount(False) > 0 Then
            If (e.KeyChar = ChrW(13)) Then
                trvList.Select()
                'Else
                '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
            End If
        End If
    End Sub

    'Search Test Or Group
    Private Sub txtListSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtListSearch.TextChanged
        Try
            If Trim(txtListSearch.Text) <> "" Then
                If trvList.GetNodeCount(False) > 0 Then
                    Dim mychildnode As TreeNode
                    'child node collection

                    For Each mychildnode In trvList.Nodes
                        Dim str As String
                        str = UCase(Trim(mychildnode.Text))
                        If Mid(str, 1, Len(Trim(txtListSearch.Text))) = UCase(Trim(txtListSearch.Text)) Then
                            '***************code added by sagar to select the node at the top on 5 july 2007
                            trvList.SelectedNode = trvList.Nodes(trvList.Nodes.Count - 1)

                            '***************
                            trvList.SelectedNode = mychildnode
                            txtListSearch.Select()
                            Exit Sub
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trvList_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvList.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Not trvList.SelectedNode Is Nothing Then
                Dim oNode As TreeNode = trvList.SelectedNode
                If btnTests.Dock = DockStyle.Top Then
                    ' ''  Add Test TO The Orders
                    gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                ElseIf btnGroups.Dock = DockStyle.Top Then
                    ' ''  Add Test from Groups TO The Orders
                    If IsNothing(oNode.Parent) Then
                        '' Selected Node is Group
                        Dim oChildNode As TreeNode
                        For Each oChildNode In oNode.Nodes
                            ' ''  Add Test from Groups TO The Orders
                            gloUCLab_Transaction.AddTest(0, oChildNode.Tag, oChildNode.Text, 1, "")
                        Next
                    Else
                        '' Selected Node is Test Under Some Group
                        ' ''  Add Test TO The Orders
                        gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                    End If
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub frmLab_RequestOrder_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmLab_RequestOrder_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try

            If blnOpenFromExam = True Then
                myCaller.GetdataFromOtherForms(gloEMRWord.enumDocType.LabOrders) '' ("Lab Request ")
                blnOpenFromExam = False
            End If
			'code is added by dipak 20090921 to reflect chanes to liquid field word document.
            If Not IsNothing(myCaller1) Then
                myCaller1.GetdataFromOtherForms(gloEMRWord.enumDocType.LabOrders) '' ("Lab Request ")
            End If
			'end code is added by dipak 20090921
            's' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            If _blnRecordLock = False Then
                '' if the Locked by by the Current User & on Current Machine only
                UnLock_Transaction(TrnType.Labs, _OrderParamter.OrderID, 0, Now)
            End If

            '' <><><> Unlock the Record <><><>
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Lab Request Orders Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab Request Orders Closed", gloAuditTrail.ActivityOutCome.Success)


            RaiseEvent On_LabClosed()

            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, "Patient Lab Request Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
		'line is commented by dipak 20090921 "me" is already disposeed 
        'Me.Dispose()
    End Sub


#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gloUC_PatientStrip1_Date_Validated() Handles gloUC_PatientStrip1.Date_Validated
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
        Me.Controls.Add(gloUC_PatientStrip1)
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        pnlToolStrip.SendToBack()
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            '' Pass Paarameters Type of Form
            .ShowDetail(_OrderParamter.PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder)
            '.SendToBack()
            .DTPValue = Format(_OrderParamter.TransactionDate, "MM/dd/yyyy hh:mm tt")
        End With

        ' ''
        ' ''
    End Sub

#End Region

    Private Sub frmLab_RequestOrder_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ''dhruv 20091207 --------------------------------------------
        Dim oResult As DialogResult
        Try
            If _CloseClicked And (gloUCLab_Transaction.LabModified Or gloUCLab_OrderDetail.OrderModified) Then
                oResult = MessageBox.Show("Do you want to save lab order?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If oResult = Windows.Forms.DialogResult.Yes Then
                    MenuEvent_Save(0)
                    'Code added by dipak 20090921 to fix "Dispose object exception- for Icon" 
                    Me.Hide()
                    Me.WindowState = FormWindowState.Normal
                ElseIf oResult = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                End If

            End If

            ''--------------------------------------------------------------

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
        End Try



    End Sub
    Private Sub InitializeToolStrip()
        ts_LabMain.ConnectionString = GetConnectionString()
        ts_LabMain.ModuleName = Me.Name
        ts_LabMain.UserID = gnLoginID

    End Sub

    Private Sub frmLab_RequestOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ''--------code added by sagaar kumbhar on 01092008 for showing the previous labs orders
            'dont show the previous labs control unless the btnPrvLabs order is clicked. therefore make the panel TransactionHistory visible false
            If spltTransactionHistory.Visible = True Then
                spltTransactionHistory.Visible = False
            End If
            If pnlTransactionHistory.Visible = True Then
                pnlTransactionHistory.Visible = False
            End If

            ''-------------------------------------------------------------------------------------

            '_OrderParamter.ProviderID = ProviderID
            If bIsOpenfrmOutstanding Then
                _OrderParamter.PatientID = gnPatientID
            End If


            Call Set_PatientDetailStrip()

            ' Fill_Toolbar()

            'Show Test
            '''''''''FillTests()
            FillTests_NEW()

            'Clear All Tests
            gloUCLab_Transaction.ClearTest()



            gDMSCategory_Labs = ""
            Dim objSettings As New clsSettings
            objSettings.GetSettings()
            If IsNothing(objSettings.DMSCategory_Labs) = False Then
                gDMSCategory_Labs = objSettings.DMSCategory_Labs
            End If
            objSettings = Nothing

            ''code commented by
            ''sarika 29th may 07
            ''------------------------
            ''Show Order Detail
            'If _OrderParamter.IsEditMode = False Then
            '    gloUCLab_OrderDetail.SetNewOrderNumber()
            'Else
            '    'Fill Order Details
            'End If
            '------------------------

            ''sarika 29th  may 07
            ' ''Load the latest order of that visit for the patient
            ''If _OrderParamter.OrderID = 0 Then
            ''    'open the load order in new mode
            ''    gloUCLab_OrderDetail.SetNewOrderNumber()
            ''Else
            ''    LoadOrder()
            ''End If
            ''-----------------------------


            'Show First Test Detail
            gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "")

            ''''Get UseID and providerID at formload

            _UserName = gstrLoginName
            _ProviderName = gstrLoginProviderName
            _UserID = gnLoginID
            _LoginProviderID = GetProviderFromLoginID(gnLoginID) ' check login user is provider

            ''''Get Patient Provider ID
            _OrderParamter.ProviderID = gloUC_PatientStrip1.ProviderID() ' get default patient provider
            _PatientProviderID = _OrderParamter.ProviderID ' it will be from Visit or either Patient Master

            'sarika 29th may 07
            Dim oLabOrder As New gloEMRLabOrder

            '' By Mahesh 20080528  For CCHIT2007
            '' Condition Added if Labs Open from Tasks the dont Fetch OrderID  
            If blnOpenFromTask = False Then
                If _LoginProviderID > 0 Then
                    _OrderParamter.OrderID = oLabOrder.GetOrderFromVisitID(_OrderParamter.VisitID, _LoginProviderID)
                Else
                    _OrderParamter.OrderID = oLabOrder.GetOrderFromVisitID(_OrderParamter.VisitID, _PatientProviderID)
                End If
            End If

            If bIsOpenfrmOutstanding Then
                _OrderParamter.OrderID = OrderID
                _OrderParamter.VisitID = VisitID
                _OrderParamter.OrderNumberPrefix = OrderPrefix
            End If


            If _OrderParamter.OrderID = 0 Then
                gloUCLab_OrderDetail.SetNewOrderNumber()
                If _LoginProviderID > 0 Then
                    _LabProviderID = _LoginProviderID
                Else
                    _LabProviderID = _PatientProviderID
                End If
            Else
                _OrderParamter.IsEditMode = True
                Call LoadOrder()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders viewed", gloAuditTrail.ActivityOutCome.Success)
            End If

            If _LoginProviderID > 0 Then
                gloUC_PatientStrip1.SetProviderName(gstrLoginProviderName, _LoginProviderID)
            End If
            '----------------------------------


            gloUCLab_Transaction.TransactionType = _OrderParamter.TransactionType
            
            gloUCLab_Transaction.PatientID = gnPatientID

            If gblnRecordLocking = True And _OrderParamter.OrderID <> 0 Then
                Dim mydt As New mytable
                mydt = Scan_n_Lock_Transaction(TrnType.Labs, _OrderParamter.OrderID, 0, Now)
                If mydt.Description <> gstrClientMachineName Then
                    MessageBox.Show("This Patient Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''Record open only for view.
                    _blnRecordLock = True
                End If
            End If
            '''' <><><> Record Level Locking <><><><> 


            Dim IsAcknoledgement As Boolean = False
            If IsAcknoledgement = CheckAcknoledgement(_OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID) Then
                tlbbtn_Acknowledgment.Visible = True
                tlbbtn_VWAcknowledgment.Visible = False
                If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_VWAcknowledgment.Name)) Then
                    ts_LabMain.ButtonsToHide.Add(tlbbtn_VWAcknowledgment.Name)
                End If
                ts_LabMain.ButtonsToHide.Remove(tlbbtn_Acknowledgment.Name)
            Else
                tlbbtn_Acknowledgment.Visible = False
                tlbbtn_VWAcknowledgment.Visible = True
                If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_Acknowledgment.Name)) Then
                    ts_LabMain.ButtonsToHide.Add(tlbbtn_Acknowledgment.Name)
                End If
                ts_LabMain.ButtonsToHide.Remove(tlbbtn_VWAcknowledgment.Name)
            End If

            '//Instruction//
            '//Instruction//
            'pnlInstruction_Detail.Height = 96
            'pnlInstruction.Height = 130
            'btnInstruction_Up.Visible = False
            'btnInstruction_Down.Visible = True

            '//Precuation//
            'pnlPrecuation_Detail.Height = 96
            'pnlPreuation.Height = 130
            'btnPreuation_Up.Visible = False
            'btnPreuation_Down.Visible = True

            'If _OrderParamter.TransactionType = LabRequestOrderParameter.enumTransactionType.LabOrder Or _OrderParamter.TransactionType = LabRequestOrderParameter.enumTransactionType.LabExternalResult Then
            pnlLeft.Visible = True
            splLeft.Visible = True
            MenuEvent_ShowHide(False)
            'Else
            '    pnlLeft.Visible = False
            '    splLeft.Visible = False
            '    MenuEvent_Previous(sender, e)
            'End If
            ''''Pramod Add the selected Lab Test to FlexGrid which is form Smart Order Form Start
            If _arrLabs.Count > 0 Then
                For i As Integer = 0 To _arrLabs.Count - 1
                    Dim lst As New myList
                    lst = CType(_arrLabs(i), myList)
                    gloUCLab_Transaction.AddTest(0, lst.ID, lst.Value, 1, "")
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders viewed", gloAuditTrail.ActivityOutCome.Success)
                Next
            End If
            ''''Pramod Add the selected Lab Test to FlexGrid which is form Smart Order Form End
            InitializeToolStrip()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Call Set_RecordLock(_blnRecordLock)
        End Try

    End Sub

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then

            tlbbtn_Save.Enabled = False
            tlbbtn_Finish.Enabled = False
            tlbbtn_HL7.Enabled = False
            tlbbtn_Acknowledgment.Enabled = False
            tlbbtn_VWAcknowledgment.Enabled = False
        Else

            tlbbtn_Save.Enabled = True
            tlbbtn_Finish.Enabled = True
            tlbbtn_HL7.Enabled = True
            tlbbtn_Acknowledgment.Enabled = True
            tlbbtn_VWAcknowledgment.Enabled = True
        End If
    End Sub

    Public Function LoadOrder() As Boolean
        '#---LOAD DETAILS---#
        Dim oLabOrderRequest As New gloEMRLabOrder
        Dim oLabActor_Order As New LabActor.LabOrder
        Dim oLabActorContact As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo

        'Assign Actor to Object
        oLabActor_Order = oLabOrderRequest.GetOrder(_OrderParamter.OrderID)

        If Not oLabActor_Order Is Nothing Then
            'Clear All Tests
            gloUCLab_Transaction.ClearTest()

            ''Show Patient Detail
            'gloUCLab_PatientDetail.ShowDetail(oLabActor_Order.PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder)

            'Show Order Detail
            gloUCLab_OrderDetail.SetData(oLabActor_Order.OrderNoPrefix, oLabActor_Order.OrderNoID, oLabActor_Order.PreferredLab, oLabActor_Order.ReferredBy, oLabActor_Order.SampledBy, oLabActor_Order.Users, oLabActor_Order.PreferredLabID, oLabActor_Order.ReferredByID, oLabActor_Order.SampledByID, oLabActor_Order.TaskDescription, oLabActor_Order.TaskDueDate)

            'Show First Test Detail
            '//--Remark--//
            '-----------------------------------------------------------
            'Assign Values to Order Object

            With oLabActor_Order
                '       gloUCLab_PatientDetail.TransactionDate = .TransactionDate ' remaining -- change in user user control to affect transaction date in user control
                Me.gloUC_PatientStrip1.TransactionDate = .TransactionDate
                Me.gloUC_PatientStrip1.Provider = .Provider
                '.ProviderID = 1 - Remaining
                _OrderParamter.ProviderID = .ProviderID
                _LabProviderID = .ProviderID

                Dim oDataTable As New DataTable
                Dim _Provider As String
                oDataTable = oLabActorContact.GetProviderName(_OrderParamter.ProviderID)
                _Provider = ""
                If Not IsNothing(oDataTable) Then
                    If oDataTable.Rows.Count > 0 Then
                        If Not IsDBNull(oDataTable.Rows(0).Item("sFirstName")) Then
                            _Provider = oDataTable.Rows(0).Item("sFirstName") & ""
                        End If
                        If Not IsDBNull(oDataTable.Rows(0).Item("sMiddleName")) Then
                            _Provider = _Provider & " " & oDataTable.Rows(0).Item("sMiddleName") & ""
                        End If
                        If Not IsDBNull(oDataTable.Rows(0).Item("sLastName")) Then
                            _Provider = _Provider & " " & oDataTable.Rows(0).Item("sLastName") & ""
                        End If
                    End If
                End If

                gloUC_PatientStrip1.SetProviderName(_Provider, _OrderParamter.ProviderID)
                gloUCLab_Transaction.SetData(oLabActor_Order)

            End With


            '-----------------------------------------------------------

        End If

        oLabActor_Order = Nothing
        oLabOrderRequest = Nothing

    End Function

#Region "User Control Events"
    Private Sub gloUCLab_TestDetail_gUC_InstructionChanged(ByVal TestID As Long, ByVal sData As String) Handles gloUCLab_TestDetail.gUC_InstructionChanged
        gloUCLab_Transaction.AddInstruction(TestID, sData)
    End Sub

    Private Sub gloUCLab_TestDetail_gUC_PrecuationChanged(ByVal TestID As Long, ByVal sData As String) Handles gloUCLab_TestDetail.gUC_PrecuationChanged
        gloUCLab_Transaction.AddPrecuation(TestID, sData)
    End Sub
#Region "Commented By Pramod For DMSV2"

    'Private Sub gloUCLab_Transaction_gUC_ScanDocument(ByVal TestID As Long) Handles gloUCLab_Transaction.gUC_ScanDocument
    '    Dim _ScanDocumentID As Int64 = 0
    '    Dim _ScanDocFlag As Boolean = True

    '    '// Vinayak - 5 Feb 2007 //

    '    '//Check for DMS Path & Patient Directive Category
    '    Dim oDMSPath As New gloStream.gloDMS.Supporting.Supporting
    '    'Check DMS System Path is Correct
    '    If oDMSPath.IsDMSSystem(DMSRootPath) = False Then
    '        _ScanDocFlag = False
    '        MessageBox.Show("Document Management System Path not set to scan lab document, please use Tools->Setting command to set DMS path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Else
    '        Dim oChk As New gloStream.gloDMS.DocumentCategory.DocumentCategory
    '        Dim oCat As New gloStream.gloDMS.DocumentCategory.Category
    '        oCat.Name = gDMSCategory_Labs '// Assign here Global Lab Category Name
    '        oCat.IsDeleted = False
    '        If oChk.IsExists(oCat) = False Then
    '            _ScanDocFlag = False
    '            MessageBox.Show("DMS Category for lab order has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If
    '        oChk = Nothing
    '        oCat = Nothing
    '    End If
    '    oDMSPath = Nothing
    '    '//Check for DMS Path & Patient Directive Category


    '    If _ScanDocFlag = True Then
    '        _ScanDocumentID = Set_ScanDocumentEvent(gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _OrderParamter.PatientID)
    '        'If intId = 0 Then
    '        '    Set_ScanDocumentEvent(gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _OrderParamter.PatientID)
    '        'Else
    '        '    If _PatientDirectiveStatus = True Then
    '        '        If MessageBox.Show("Do you want to add more documents against patient directive?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
    '        '            Set_ScanDocumentEvent(gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _OrderParamter.PatientID)
    '        '        End If
    '        '    Else
    '        '        Set_ScanDocumentEvent(gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument, _OrderParamter.PatientID)
    '        '    End If
    '        'End If
    '    End If

    '    '// Vinayak - 5 Feb 2007 //

    '    gloUCLab_Transaction.AddScanDocument(TestID, _ScanDocumentID)
    'End Sub
#End Region

    Private Sub gloUCLab_Transaction_gUC_ScanDocument(ByVal TestID As Long) Handles gloUCLab_Transaction.gUC_ScanDocument
        Dim _ScanContainerID As Int64 = 0
        Dim _ScanDocumentID As Int64 = 0
        Dim _ScanDocFlag As Boolean = True
        Dim _result As Boolean = False

        If gloEDocumentV3.eDocManager.eDocValidator.IsCategoryExists(0, gDMSCategory_Labs, gClinicID) = False Then
            MessageBox.Show("DMS Category for lab order has not been set, Please set the category", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            _ScanDocFlag = False
        End If

        If _ScanDocFlag = True Then
            _result = Set_ScanDocumentEvent(_OrderParamter.PatientID, gDMSCategory_Labs, _ScanContainerID, _ScanDocumentID)
        End If

        If _result = True Then
            gloUCLab_Transaction.AddScanDocument(TestID, _ScanDocumentID)
        End If
    End Sub

#Region "Commented By Pramod For DMSV2"
    'Private Function Set_ScanDocumentEvent(ByVal DocumentType As gloStream.gloDMS.Supporting.enumDocumentType, ByVal PatID As Int64) As Int64
    '    Dim oChk As New gloStream.gloDMS.DocumentCategory.DocumentCategory
    '    Dim oCat As New gloStream.gloDMS.DocumentCategory.Category
    '    oCat.Name = gDMSCategory_Labs
    '    oCat.IsDeleted = False
    '    If oChk.IsExists(oCat) = False Then
    '        MessageBox.Show("DMS Category not set for Labs, please add document from Scan Document functionality")
    '        Exit Function
    '    End If

    '    'Dim oImportDocumentEvent As New frmDMS_ScannedDocumentEvent ' ABBYY
    '    Dim _ScanDoucmentID As Int64 = 0

    '    Dim oImportDocumentEvent As New frmDMS_PatientDirective
    '    Dim oDocument As New gloStream.gloDMS.Document.Document                             ' DMS Document
    '    Dim _PatientID As Long = 0                                                      ' Patient ID
    '    Dim _Category As String = ""                                                    ' Destination Category
    '    Dim _Month As String = ""                                                       ' Destination Month
    '    Dim _Year As String = ""
    '    Dim _DoucmentName As String = ""
    '    Dim _CurrentDocument As String = ""
    '    Dim _ImportDocument As String = ""
    '    Dim i As Integer = 0


    '    Try
    '        _PatientID = PatID 'CLng(txtPatientID.Text)
    '        _Month = MonthName(Month(Date.Now))
    '        _DoucmentName = ""
    '        If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '            _Year = Trim(Year(Date.Now))
    '            _Category = gDMSCategory_Labs
    '            _CurrentDocument = ""
    '        Else
    '            Exit Function
    '        End If

    '        'Process
    '        If _PatientID > 0 Then
    '            'Check Month
    '            If Trim(_Month) = "" Then
    '                MessageBox.Show("Month not found to import document", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                Exit Function
    '            End If

    '            'Category
    '            If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                If Trim(_Category) = "" Then
    '                    MessageBox.Show("Category not selected to import document", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '                    Exit Function
    '                End If
    '            End If

    '            'Set Values to Form Object
    '            With oImportDocumentEvent
    '                With .ProcessParameter
    '                    .PatientID = _PatientID
    '                    .Category = _Category
    '                    .Month = _Month
    '                    .Year = _Year
    '                    .DocumentName = _DoucmentName
    '                    .DocumentType = DocumentType
    '                End With

    '                'Show Form
    '                .Left = ((Me.Width - 492) / 2) + 100
    '                .Top = ((Me.Height - 284) / 2) + 150
    '                .FormName = "Lab Document"
    '                .ShowDialog()
    '                _ImportDocument = .sImportDocumentPath ' Document path which is imported

    '                'Audit Trial
    '                If File.Exists(_ImportDocument) = True Then
    '                    Dim objAudit As New clsAudit
    '                    Dim sMessage As String = ""
    '                    If DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                        sMessage = "Document Imported from file with the name " & _ImportDocument & " in " & _Category
    '                    ElseIf DocumentType = gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument Then
    '                        sMessage = "Document Imported from file into General Bin with the name " & _ImportDocument
    '                    End If
    '                    objAudit.CreateLog(clsAudit.enmActivityType.ImportDocument_FromFile, sMessage, gstrLoginName, gstrClientMachineName, _PatientID)
    '                    objAudit = Nothing
    '                End If

    '                If File.Exists(_ImportDocument) = True Then
    '                    Dim oFileInfo As FileInfo = New FileInfo(_ImportDocument)
    '                    _ScanDoucmentID = Replace(oFileInfo.Name, oFileInfo.Extension, "")
    '                End If

    '            End With ' With oPrintDeleteEvent.ProcessParameter

    '        End If ' If _PatientID > 0 Then

    '        Return _ScanDoucmentID

    '    Catch objError As Exception
    '        MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Exit Function
    '    Finally
    '        oImportDocumentEvent = Nothing
    '        oDocument = Nothing
    '        _PatientID = Nothing
    '        _Category = Nothing
    '        _Month = Nothing
    '        _Year = Nothing
    '        _DoucmentName = Nothing
    '        _CurrentDocument = Nothing
    '        _ImportDocument = Nothing
    '    End Try
    'End Function
#End Region

    Private Function Set_ScanDocumentEvent(ByVal PatientID As Int64, ByVal LabCategory As String, ByRef ScanContainerID As Int64, ByRef ScanDocumentID As Int64) As Boolean
        Dim oScanDocument As New gloEDocumentV3.gloEDocV3Management()
        Dim _result As Boolean = False
        Try
            '_result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocument.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
            _result = oScanDocument.ShowEScanner(PatientID, LabCategory, DateTime.Now.Year.ToString(), MonthName(Month(Date.Now)), gClinicID, gloEDocumentV3.Enumeration.enum_DocumentEventType.ScanDocument, ScanContainerID, ScanDocumentID)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oScanDocument.Dispose()
        End Try
        Return _result
    End Function


    Private Sub gloUCLab_Transaction_gUC_TestSelected(ByVal TestID As Long, ByVal Specimen As String, ByVal CollectionContainer As String, ByVal StorageTemperature As String, ByVal LOINCCode As String, ByVal Instructionas As String, ByVal Precuation As String, ByVal Comments As String) Handles gloUCLab_Transaction.gUC_TestSelected
        gloUCLab_TestDetail.SetData(TestID, Specimen, CollectionContainer, StorageTemperature, LOINCCode, Instructionas, Precuation, Comments)

        'txtInstruction.Text = Instructionas
        'txtInstruction.Tag = TestID
        'txtPreuation.Text = Precuation
        'txtPreuation.Tag = TestID
    End Sub

    Private Sub btnInstruction_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' gloUCLab_Transaction.AddInstruction(txtInstruction.Tag, txtInstruction.Text)
    End Sub

    Private Sub btnPrecaution_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'gloUCLab_Transaction.AddPrecuation(txtPreuation.Tag, txtPreuation.Text)
    End Sub
#End Region

#Region "Menu Events"

    'Private Sub Fill_Toolbar()
    '    Dim oToolBars As New gloToolBars
    '    With ts_LabMain
    '        .Items.Clear()
    '        'New
    '        .Items.Add(oToolBars.NewItem.MenuName, Global.gloEMR.My.Resources._New, AddressOf MenuEvent_New)
    '        'Save
    '        .Items.Add(oToolBars.Save.MenuName, Global.gloEMR.My.Resources.SAVE, AddressOf MenuEvent_Save)
    '        'Close
    '        .Items.Add(oToolBars.Close.MenuName, Global.gloEMR.My.Resources.CLOSE, AddressOf MenuEvent_Close)
    '        'Print
    '        .Items.Add(oToolBars.Print.MenuName, Global.gloEMR.My.Resources.Print1, AddressOf MenuEvent_Print)
    '        'Fax
    '        .Items.Add(oToolBars.Fax.MenuName, Global.gloEMR.My.Resources.Faxs, AddressOf MenuEvent_Fax)
    '        'Previous
    '        .Items.Add(oToolBars.PreviousRecord.MenuName, Global.gloEMR.My.Resources.SHOW, AddressOf MenuEvent_Previous)
    '        'Hide
    '        .Items.Add(oToolBars.HideRecord.MenuName, Global.gloEMR.My.Resources.SHOW, AddressOf MenuEvent_Hide)

    '        For i As Integer = 0 To .Items.Count - 1
    '            .Items.Item(i).TextImageRelation = TextImageRelation.ImageAboveText
    '        Next
    '    End With
    '    oToolBars = Nothing
    'End Sub


    Private Sub MenuEvent_New()

        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            If ShowAssociateProvider(gnPatientID) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        If _blnRecordLock = False Then
            ''When open new Order Unlock previous Order.
            UnLock_Transaction(TrnType.Labs, _OrderParamter.OrderID, 0, Now)
        End If
        _blnRecordLock = False

        With _OrderParamter
            .IsEditMode = False
            .OrderID = 0
            .OrderNumberID = 0
            .OrderNumberPrefix = "ORD"
            .PatientID = _OrderParamter.PatientID
            .VisitID = GenerateVisitID()
            .TransactionDate = Now
            .CloseAfterSave = True
        End With

        MenuEvent_ShowHide(False)
        'Show Patient Detail
        Me.gloUC_PatientStrip1.ShowDetail(_OrderParamter.PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder)
        '' Set Tranasaction Date
        Me.gloUC_PatientStrip1.DTPValue = Now
        ''   'Show Order Detail
        gloUCLab_OrderDetail.SetData("", 0, "", "", "", Nothing, 0, 0, 0, "", Now.Date)
        'Show Order Detail
        gloUCLab_OrderDetail.SetNewOrderNumber()
        'Show Test Detail
        gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "")
        'Clear All Tests
        gloUCLab_Transaction.ClearTest()
        tlbbtn_Acknowledgment.Visible = True
        tlbbtn_VWAcknowledgment.Visible = False
        If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_VWAcknowledgment.Name)) Then
            ts_LabMain.ButtonsToHide.Add(tlbbtn_VWAcknowledgment.Name)
        End If
        ts_LabMain.ButtonsToHide.Remove(tlbbtn_Acknowledgment.Name)

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders viewed", gloAuditTrail.ActivityOutCome.Success)

    End Sub

    Private Sub MenuEvent_Save(ByVal IsFinished As Int16)
        ''check Provider of patient and order is same
        Dim _IsFinished As Int16 = IsFinished

        If _LoginProviderID <= 0 Then
            If _LabProviderID <> _PatientProviderID Then
                If MessageBox.Show("Provider for this order is different do you want to continue", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                    SaveOrder(_IsFinished, _PatientProviderID)
                End If
            Else
                SaveOrder(_IsFinished)
            End If
        Else
            If _LabProviderID <> _LoginProviderID Then
                If MessageBox.Show("Provider for this order is different do you want to continue", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                    SaveOrder(_IsFinished, _LoginProviderID)
                End If
            Else
                If _LabProviderID <> _PatientProviderID Then
                    If MessageBox.Show("Provider for this order is different do you want to continue", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                        SaveOrder(_IsFinished, _PatientProviderID)
                    End If
                Else
                    SaveOrder(_IsFinished)
                End If
                'SaveOrder(_IsFinished)
            End If
        End If

    End Sub

    'Private Sub GetData(ByRef Arrlist As ArrayList)
    '    '@nTaskId	numeric(18,0),
    '    '@nFromId	numeric(18,0),
    '    '@nToId		numeric(18,0),
    '    '@dtTaskDate	numeric(18,0),
    '    '@sSubject	varchar(100),
    '    '@dtDuedate	datetime,
    '    '@sPriority	varchar(50),
    '    '@sStatus	varchar(50),
    '    '@sNotes	
    '    LoginId = ObjTasksDBLayer.GetLoginId
    '    ''Arrlist.Add(0)
    '    Arrlist.Add(TaskID)
    '    Arrlist.Add(LoginId)
    '    Arrlist.Add(_VisitDate)   ''dtTaskDate

    '    'sarika 21st june 07

    '    '' Orders Radiology Chenged to "Radiology Orders" on 20071003 - Anil 
    '    '' Ref BugNo - 83
    '    'Dim strSubject As String = "Radiology Orders"
    '    Dim strSubject As String = "Labs"
    '    '---------

    '    'Dim mynode1 As myTreeNode

    '    '''''Arrlist.Add("Labs Ordered")   '' sSubject
    '    Arrlist.Add(strSubject)   '' sSubject
    '    Arrlist.Add(gloUCLab_OrderDetail.TaskDueDate)    '' dtDuedate
    '    Arrlist.Add("High")   '' sPriority
    '    Arrlist.Add("Not Started")   '' sStatus
    '    _strTasksNotes = Trim(gloUCLab_OrderDetail.TaskDescription)
    '    Arrlist.Add(_strTasksNotes)   '' sNotes
    '    Arrlist.Add(gnPatientID)
    'End Sub

    Private Sub AddTasks(ByVal Users As gloEMRGeneralLibrary.gloEMRActors.LabActor.ItemDetails, ByVal TaskDate As Date, ByVal PatientID As Long, ByVal TaskId As Long, ByVal TaskDesc As String, ByVal TaskDueDate As Date)

        'Dim arrlist As New ArrayList
        ' '' fill Value to arraylist 
        ' '' Like Taskid ,Date, etc Task Related Values
        'Call GetData(arrlist)

        Dim ArrTasks(Users.Count - 1) As Int64
        'Dim mlist As myList
        Dim i As Integer

        For i = 0 To Users.Count - 1
            ArrTasks(i) = Users.Item(i).ID
        Next
        'End If

        '' ''ObjTasksDBLayer.AddData(arrlist, ArrTasks)
        ''If ArrTasks.Length > 0 Then
        ''    ObjTasksDBLayer.UpdateData(arrlist, ArrTasks, ClsTasksDBLayer.TaskType.OrderRadiology)
        ''End If

        Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString)
        Dim oTask As New Task()
        Dim oTaskProgress As New gloTaskMail.TaskProgress

        For i = 0 To ArrTasks.Length - 1

            Dim oTaskAssign As New TaskAssign

            oTaskAssign.AssignFromID = gnLoginID
            oTaskAssign.AssignFromName = gstrLoginName
            oTaskAssign.AssignToID = Users.Item(i).ID
            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
            Else
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
            End If
            oTaskAssign.AssignToName = Users.Item(i).Description
            ''Sandip Darade 20091027
            oTaskAssign.ClinicID = gnClinicID
            oTask.Assignment.Add(oTaskAssign)

        Next

        oTaskProgress.ClinicID = gnClinicID
        oTaskProgress.Complete = 0
        oTaskProgress.DateTime = TaskDate
        oTaskProgress.Description = TaskDesc
        oTaskProgress.StatusID = 1 '' Not Started
        oTaskProgress.TaskID = TaskId

        '' 
        oTask.TaskID = TaskId
        oTask.UserID = gnLoginID
        oTask.TaskType = gloTaskMail.TaskType.LabOrder
        oTask.PatientID = PatientID
        '' oTask.Subject = TaskDesc'' Task Subject is same as Task Description in LabOrders
        oTask.Subject = "Lab order assigned "
        oTask.ClinicID = gnClinicID
        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(TaskDueDate)
        'oTask.FaxTiffFileName = FaxTiffFileName
        oTask.IsPrivate = False
        oTask.MachineName = gstrClientMachineName
        oTask.Progress = oTaskProgress
        oTask.ReferenceID1 = _OrderParamter.OrderID ''LabOrder ID for referance
        oTask.ProviderID = gnPatientProviderID '' Passing the Provider Id 'Dhruv'

        ''Sandip Darade 20091027
        oTask.PriorityID = 1

        If TaskId = 0 Then
            ogloTask.Add(oTask)
        Else
            ogloTask.Modify(oTask)
        End If


        'Dim objTasksDBLayer As New ClsTasksDBLayer

        ''    Dim arrlist As New ArrayList
        ' '' fill Value to arraylist 
        ' '' Like Taskid ,Date, etc Task Related Values
        ''Call GetData(arrlist)

        ''  Dim ArrTasks(chkLstUsers.CheckedItems.Count - 1) As Int64

        ''Dim mlist As myList
        ''Dim i As Integer

        '' ''ObjTasksDBLayer.AddData(arrlist, ArrTasks)
        'Dim objgloEMRLab As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder

        'objgloEMRLab.UpdateTaskData(Users, "Lab Order assigned", gnLoginID, TaskDate, PatientID, objTasksDBLayer.TaskType.LabOrder, TaskId, TaskDesc, TaskDueDate)

    End Sub

    Private Sub MenuEvent_Close()
        Me.Close()
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Close, "Lab Request Orders closed without saving", gloAuditTrail.ActivityOutCome.Success)
    End Sub

    Private Sub MenuEvent_Print()
        Try
            Dim dt As New DataTable
            _OrderParamter.CloseAfterSave = False
            blnSaved = True
            MenuEvent_Save(0)
            _OrderParamter.IsEditMode = True
            If blnSaved = False Then
                Exit Sub
            End If


            'sarika Labs Denormalization 20090323
            'PrintLabOrderReport(_OrderParamter.OrderID, oLabActor_Order1.ArrTestID)
            PrintLabOrderReport(_OrderParamter.OrderID, oLabActor_Order1.ArrTestName)
            '----
            _OrderParamter.CloseAfterSave = True
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Print, "Lab Request Orders printed", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub MenuEvent_Fax()
        Try
            _OrderParamter.CloseAfterSave = False
            blnSaved = True
            MenuEvent_Save(0)
            If blnSaved = False Then
                'order not saved 
                Exit Sub
            End If
            _OrderParamter.IsEditMode = True
            'sarika Labs Denormalization 20090323
            '            FaxLabOrder(_OrderParamter.OrderID, oLabActor_Order1.ArrTestID)
            FaxLabOrder(_OrderParamter.OrderID, oLabActor_Order1.ArrTestName)

            '--
            _OrderParamter.CloseAfterSave = True
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Fax, "Lab Request Orders fax", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuEvent_PrintPreview()
        Try
            blnSaved = True
            MenuEvent_Save(0)
            If blnSaved = False Then
                Exit Sub
            End If

            Dim frm As New frmViewLabReport

            frm.OrderID = _OrderParamter.OrderID

            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR-Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Try

    '          blnIsInvalidHL7FilePath = False
    '          oGeneralInterface = New clsGeneralInterface()
    '          oGeneralInterface.SendHL7PatientDetails(patientID, blnIsUpdate)
    '          If blnIsInvalidHL7FilePath = True Then
    '              MessageBox.Show("Please Set a valid HL7 File Path from gloEMRAdmin", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '          End If


    '      Catch ex As Exception
    '          MessageBox.Show("Unable to generate HL7 file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '          UpdateLog(ex.ToString())
    '      Finally
    '          If Not IsNothing(oGeneralInterface) Then
    '              oGeneralInterface.Dispose()
    '              oGeneralInterface = Nothing
    '          End If
    '      End Try
    'Private Sub MenuEvent_HL7()
    '    _OrderParamter.CloseAfterSave = False
    '    blnSaved = True
    '    MenuEvent_Save(0)
    '    _OrderParamter.IsEditMode = True
    '    If blnSaved = False Then
    '        Exit Sub
    '    End If
    '    Dim ogloHL7 As New gloHl7Interface.HL7Library.gloHL7
    '    Try

    '        With ogloHL7
    '            .DatabaseName = gstrDatabaseName
    '            .ServerName = gstrSQLServerName
    '            .Connect(System.Windows.Forms.Application.StartupPath, gloHl7Interface.HL7Library.enumHL7ConnectionType.Create, "2.3")
    '            ogloHL7.CreateMessageforLabs(_OrderParamter.OrderID, _OrderParamter.PatientID, oLabActor_Order1.ArrTestName)
    '            ogloHL7.Disconnect(gstrgloEMRStartupPath)
    '            MessageBox.Show("HL7 Lab order generated successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End With

    '    Catch ex As gloHl7Interface.HL7DatabaseLibrary.HL7DatabaseException
    '        MsgBox(ex.ErrMessage)
    '    Catch ex As gloHl7Interface.HL7Library.HL7Exception
    '        MsgBox(ex.ErrMessage)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        If Not IsNothing(ogloHL7) Then
    '            ogloHL7.Dispose()
    '            ogloHL7 = Nothing
    '        End If

    '    End Try

    '    _OrderParamter.CloseAfterSave = True
    'End Sub

    Private Sub MenuEvent_HL7()
        _OrderParamter.CloseAfterSave = False
        blnSaved = True
        MenuEvent_Save(0)
        _OrderParamter.IsEditMode = True
        If blnSaved = False Then
            Exit Sub
        End If
        Dim oGeneralInterface As New clsGeneralInterface()
        Try

            'blnIsInvalidHL7FilePath = False
            If gblnSendChargesToHL7 = True Then
                oGeneralInterface.SendLabs(_OrderParamter.OrderID, _OrderParamter.PatientID, oLabActor_Order1.ArrTestName)
            End If
            'code commented by kanchan on 20091202 for genius & Hl7 work simultaneously as case3176
            '    If gstrHL7MessagePath = "" Then
            '        MessageBox.Show("Please set HL7 File Path from gloEMRAdmin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            '    If System.IO.Directory.Exists(gstrHL7MessagePath) = False Then
            '        MessageBox.Show("Please set valid HL7 File Path from gloEMRAdmin", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End If
            'MessageBox.Show("Hl7 File Sucessfully Generated", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            'If blnIsInvalidHL7FilePath = True Then
            '    MessageBox.Show("Please Set a valid HL7 File Path from gloEMRAdmin", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If


        Catch ex As Exception
            MessageBox.Show("Unable to generate HL7 file", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog(ex.ToString())
        Finally
            If Not IsNothing(oGeneralInterface) Then
                oGeneralInterface.Dispose()
                oGeneralInterface = Nothing
            End If
        End Try

        _OrderParamter.CloseAfterSave = True
    End Sub

#End Region

#Region "History User Control Events"
    Private Sub gloUCLab_History_gUC_DeleteOrder(ByVal OrderID As Long) Handles gloUCLab_History.gUC_DeleteOrder
        Dim oLabOrder As New gloEMRLabOrder
        Try
            If MessageBox.Show("Are you sure you want to delete this lab order?", "Lab Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                oLabOrder.Delete(OrderID)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Delete, "Lab Request Orders deleted", gloAuditTrail.ActivityOutCome.Success)
                If _OrderParamter.OrderID = OrderID Then
                    'gloUCLab_OrderDetail.SetNewOrderNumber()
                    'gloUCLab_TestDetail.SetData(0, "", "", "", "", "", "", "")
                    MenuEvent_New()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gloUCLab_History_gUC_FaxOrder(ByVal OrderID As Long) Handles gloUCLab_History.gUC_FaxOrder

        Try
            _OrderParamter.CloseAfterSave = False
            'sarika Labs Denormalization 20090323
            '            FaxLabOrder(OrderID, oLabActor_Order1.ArrTestID)

            'sarika Lab Order History PrintFax Fix 20090511
            'get the testnames for the selected order
            Dim oLabOrder As New gloEMRLabOrder
            Dim arrTests As ArrayList

            arrTests = oLabOrder.GetOrderTests(OrderID)
            '---

            FaxLabOrder(OrderID, arrTests)


            oLabOrder = Nothing
            '---
            _OrderParamter.CloseAfterSave = True
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Fax, "Lab Request Orders fax", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function RetrieveFAXDocumentName() As String
        'Set FAX Settings
        Dim strTIFFFileName As String = ""
        Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
        strTIFFFileName = gnClientMachineID & "-" & Format(_dtCurrentDateTime, "yyyyMMddhhmmss") & _dtCurrentDateTime.Millisecond
        Return strTIFFFileName
    End Function


    'sarika internet fax
    Public Function FaxLabOrderOLD(ByVal OrderID As Long)
        'sarika 12th oct 07
        'Check FAX Printer settings are set or not
        If isPrinterSettingsSet(True) = False Then
            Exit Function
        End If
        '--------

        Dim oRpt As ReportDocument

        Try
            Dim _strPath As String = gstrgloEMRStartupPath & "\Reports\rptLabOrderReport.rpt"
            '  Dim _strPath As String = "D:\gloEMR4.0\gloEMR\gloEMR\bin\Debug\Reports\rptLabOrderReport.rpt"
            'Dim _strPath As String = "E:\gloEMR Working Folder\gloEMR\bin\Reports\rptLabOrderReport.rpt"

            oRpt = New ReportDocument
            oRpt.Load(_strPath)
            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            Dim TableCounter

            With crConnectionInfo
                .AllowCustomConnection = True
                .ServerName = gstrSQLServerName
                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 
                .DatabaseName = gstrDatabaseName
                '.UserID = "Your User ID"
                '.Password = "Your Password"
                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

            Next
            'oRpt.Load(_strPath)
            oRpt.SetParameterValue("OrderID", OrderID.ToString)

            ' Call SetFAXPrinterDefaultSettings()

            Try
                MainMenu.SetFAXPrinterDefaultSettings1()
            Catch ex As Exception
                MessageBox.Show("Error in setting Default Printer settings", "Lab Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'Retrieve the FAX Cover Page details
            'Find FAX Parameters
            'Get Pharmacy FAX No
            Dim strFAXTo As String
            Dim strFAXNo As String
            Dim objmytable As mytable
            Dim objFAX As New clsFAX
            'objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

            'If Not IsNothing(objmytable) Then
            '    gstrFAXContactPersonFAXNo = objmytable.Description
            '    gstrFAXContactPerson = objmytable.Code
            'End If

            'If Trim(gstrFAXContactPerson) = "" Then
            '    gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
            'End If

            ' If gblnFAXCoverPage Then
            mdlFAX.Owner = Me
            If RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
                Exit Function
            End If
            'Else
            'If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '    gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
            'End If
            'End If


            'code commented by sarika 13th nov 07 -- for 1 fax to multiple recipients

            'If Trim(gstrFAXContactPersonFAXNo) = "" Then
            '    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    'sarika 3rd oct 07
            '    'the fax is send even then the fax no. is not entered.
            '    Exit Function
            '    '----------------------------------
            'End If

            ''Retrieve FAX Document Name
            'Dim strFAXDocumentName As String
            'strFAXDocumentName = RetrieveFAXDocumentName()

            ''If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Function
            'If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

            'objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
            'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
            'oRpt.PrintToPrinter(1, False, 0, 0)
            'objFAX = Nothing
            '--------


            'code added by sarika 13th nov 07 -- for 1 fax to multiple recipients
            If multipleRecipients = False Then


                If Trim(gstrFAXContactPersonFAXNo) = "" Then
                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'sarika 3rd oct 07
                    'the fax is send even then the fax no. is not entered.
                    Exit Function
                    '----------------------------------
                End If

                'Retrieve FAX Document Name
                Dim strFAXDocumentName As String
                strFAXDocumentName = RetrieveFAXDocumentName()
                If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function
                objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                oRpt.PrintToPrinter(1, False, 0, 0)

            Else

                If gstrFAXContacts.Count = 0 Then
                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'sarika 3rd oct 07
                    'the fax is send even then the fax no. is not entered.
                    Exit Function
                    '----------------------------------
                End If

                'Retrieve FAX Document Name
                Dim strFAXDocumentName As String
                strFAXDocumentName = RetrieveFAXDocumentName()

                Dim strFAXDocumentName1 As String = ""
                strFAXDocumentName1 = strFAXDocumentName

                For i As Integer = 0 To gstrFAXContacts.Count - 1
                    strFAXDocumentName = strFAXDocumentName1 & i.ToString
                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

                    Dim mynode As myTreeNode

                    mynode = New myTreeNode

                    mynode = CType(gstrFAXContacts.Item(i + 1), myTreeNode)

                    objFAX.AddPendingFAX(gnPatientID, mynode.Text, "Labs", mynode.Tag, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                    oRpt.PrintToPrinter(1, False, 0, 0)
                    mynode = Nothing
                Next



            End If
            objFAX = Nothing
            '------





        Catch ex As Exception

        End Try
    End Function

    'Commented By Shweta 20091028
    'against PER with Case No :GLO2008-0001765
    'In this function we are using the old report as report has change 
    'Public Function FaxLabOrder(ByVal OrderID As Long, ByVal arrTests As ArrayList)


    '    Dim oRpt As ReportDocument

    '    Try
    '        Dim _strPath As String = gstrgloEMRStartupPath & "\Reports\rptLabOrderReport.rpt"
    '        '  Dim _strPath As String = "D:\gloEMR4.0\gloEMR\gloEMR\bin\Debug\Reports\rptLabOrderReport.rpt"
    '        'Dim _strPath As String = "E:\gloEMR Working Folder\gloEMR\bin\Reports\rptLabOrderReport.rpt"

    '        oRpt = New ReportDocument
    '        oRpt.Load(_strPath)
    '        Dim crtableLogoninfos As New TableLogOnInfos
    '        Dim crtableLogoninfo As New TableLogOnInfo
    '        Dim crConnectionInfo As New ConnectionInfo
    '        Dim CrTables As Tables
    '        Dim CrTable As Table
    '        Dim TableCounter

    '        With crConnectionInfo
    '            .AllowCustomConnection = True
    '            .ServerName = gstrSQLServerName
    '            'If you are connecting to Oracle there is no 
    '            'DatabaseName. Use an empty string. 
    '            'For example, .DatabaseName = "" 
    '            .DatabaseName = gstrDatabaseName
    '            '.UserID = "Your User ID"
    '            '.Password = "Your Password"
    '            .IntegratedSecurity = True
    '        End With

    '        'This code works for both user tables and stored 
    '        'procedures. Set the CrTables to the Tables collection 
    '        'of the report 

    '        CrTables = oRpt.Database.Tables

    '        'Loop through each table in the report and apply the 
    '        'LogonInfo information 

    '        For Each CrTable In CrTables
    '            crtableLogoninfo = CrTable.LogOnInfo
    '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '            CrTable.ApplyLogOnInfo(crtableLogoninfo)

    '            'If your DatabaseName is changing at runtime, specify 
    '            'the table location. 
    '            'For example, when you are reporting off of a 
    '            'Northwind database on SQL server you 
    '            'should have the following line of code: 

    '            CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

    '        Next
    '        'oRpt.Load(_strPath)
    '        '   oRpt.SetParameterValue("OrderID", OrderID.ToString)

    '        Dim prm As ParameterValues
    '        Dim discreteval As ParameterDiscreteValue

    '        oRpt.Refresh()

    '        ''//patientid
    '        prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
    '        prm.Clear()
    '        discreteval = New ParameterDiscreteValue
    '        discreteval.Value = CType(OrderID, String)
    '        prm.Add(discreteval)
    '        oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)


    '        prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
    '        prm.Clear()

    '        Dim valcnt As Integer = 0
    '        For valcnt = 0 To arrTests.Count - 1
    '            discreteval = New ParameterDiscreteValue
    '            discreteval.Value = CType(arrTests.Item(valcnt), String)
    '            prm.Add(discreteval)
    '            discreteval = Nothing
    '        Next
    '        oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)


    '        ' Call SetFAXPrinterDefaultSettings()

    '        mdlFAX.gstrFAXContactPerson = ""
    '        mdlFAX.gstrFAXContactPersonFAXNo = ""
    '        mdlFAX.multipleRecipients = False
    '        mdlFAX.gstrFAXContacts = Nothing


    '        'sarika internet fax
    '        If gblnInternetFax = False Then

    '            'sarika 12th oct 07
    '            'Check FAX Printer settings are set or not
    '            If isPrinterSettingsSet(True) = False Then
    '                Exit Function
    '            End If
    '            '--------

    '            Try
    '                MainMenu.SetFAXPrinterDefaultSettings1()
    '            Catch ex As Exception
    '                MessageBox.Show("Error in setting Default Printer settings", "Lab Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            End Try

    '            'Retrieve the FAX Cover Page details
    '            'Find FAX Parameters
    '            'Get Pharmacy FAX No
    '            Dim strFAXTo As String
    '            Dim strFAXNo As String
    '            Dim objmytable As mytable
    '            Dim objFAX As New clsFAX
    '            'objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

    '            'If Not IsNothing(objmytable) Then
    '            '    gstrFAXContactPersonFAXNo = objmytable.Description
    '            '    gstrFAXContactPerson = objmytable.Code
    '            'End If

    '            'If Trim(gstrFAXContactPerson) = "" Then
    '            '    gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
    '            'End If

    '            ' If gblnFAXCoverPage Then
    '            mdlFAX.Owner = Me
    '            If RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
    '                Exit Function
    '            End If
    '            'Else
    '            'If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '            '    gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
    '            'End If
    '            'End If


    '            'code commented by sarika 13th nov 07 -- for 1 fax to multiple recipients

    '            'If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '            '    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            '    'sarika 3rd oct 07
    '            '    'the fax is send even then the fax no. is not entered.
    '            '    Exit Function
    '            '    '----------------------------------
    '            'End If

    '            ''Retrieve FAX Document Name
    '            'Dim strFAXDocumentName As String
    '            'strFAXDocumentName = RetrieveFAXDocumentName()

    '            ''If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Function
    '            'If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

    '            'objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
    '            'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
    '            'oRpt.PrintToPrinter(1, False, 0, 0)
    '            'objFAX = Nothing
    '            '--------


    '            'code added by sarika 13th nov 07 -- for 1 fax to multiple recipients
    '            If multipleRecipients = False Then


    '                If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    'sarika 3rd oct 07
    '                    'the fax is send even then the fax no. is not entered.
    '                    Exit Function
    '                    '----------------------------------
    '                End If

    '                'Retrieve FAX Document Name
    '                Dim strFAXDocumentName As String
    '                strFAXDocumentName = RetrieveFAXDocumentName()
    '                If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function
    '                objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
    '                oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
    '                oRpt.PrintToPrinter(1, False, 0, 0)

    '            Else

    '                If gstrFAXContacts.Count = 0 Then
    '                    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    'sarika 3rd oct 07
    '                    'the fax is send even then the fax no. is not entered.
    '                    Exit Function
    '                    '----------------------------------
    '                End If

    '                'Retrieve FAX Document Name
    '                Dim strFAXDocumentName As String
    '                strFAXDocumentName = RetrieveFAXDocumentName()

    '                Dim strFAXDocumentName1 As String = ""
    '                strFAXDocumentName1 = strFAXDocumentName

    '                For i As Integer = 0 To gstrFAXContacts.Count - 1
    '                    strFAXDocumentName = strFAXDocumentName1 & i.ToString
    '                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

    '                    Dim mynode As myTreeNode

    '                    mynode = New myTreeNode

    '                    mynode = CType(gstrFAXContacts.Item(i + 1), myTreeNode)

    '                    objFAX.AddPendingFAX(gnPatientID, mynode.Text, "Labs", mynode.Tag, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
    '                    oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
    '                    oRpt.PrintToPrinter(1, False, 0, 0)
    '                    mynode = Nothing
    '                Next



    '            End If
    '            objFAX = Nothing
    '            '------
    '        Else
    '            'sarika internet fax
    '            mdlFAX.Owner = Me
    '            If RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
    '                Exit Function
    '            End If

    '            Dim objclsFaxReport As New clsPrintFaxReport

    '            objclsFaxReport.FaxReport(oRpt)
    '        End If



    '    Catch ex As Exception

    '    End Try
    'End Function
    'End Commenting 20091028

    'Added by Shweta 20091028
    'against PER with Case No :GLO2008-0001765
    'For preparing report to print or to send fax 
    'Here we are using report obj from gloEMRReports project
    Public Function CreateReport(ByVal OrderID As Long, ByVal arrTests As ArrayList) As Rpt_LabOrder

        'Create the object for report
        Dim oLabs As Rpt_LabOrder = New Rpt_LabOrder()
        'Create the object for dataset i.e dsgloEMRReports.xsd
        Dim dsReports As dsgloEMRReports = New dsgloEMRReports()

        'Commented by Shweta 20091028
        'Added as sub report in main report but not needed as all functionality has done on main form
        'Dim oLabResult As Rpt_LabOrderResult = New Rpt_LabOrderResult()
        'And commenting 20091028

        Dim oConnection As SqlConnection = New SqlConnection
        Dim sqlCmd As SqlCommand = New SqlCommand
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Try
            oConnection.ConnectionString = GetConnectionString()
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.CommandText = " SELECT sClinicName, sAddress1, sAddress2, sStreet, sCity, sState, " _
                                 & " sZIP, sPhoneNo,sMobileNo, sFAX, sEmail, sURL, imgClinicLogo FROM  Clinic_MST where nclinicId = 1"
            sqlCmd.Connection = oConnection
            da = New SqlDataAdapter(sqlCmd)
            da.Fill(dsReports, "dt_Clinic_MST")
            If OrderID <> 0 Then
                sqlCmd.CommandText = ""
                sqlCmd.CommandText = "SELECT Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                               & "Lab_Order_MST.labom_OrderNoPrefix + '-' + CONVERT(varchar(100), " _
                               & "Lab_Order_MST.labom_OrderNoID) AS OrderNumber, " _
                               & "Lab_Order_TestDtl.labotd_Instruction AS Instruction, " _
                               & "Lab_Order_TestDtl.labotd_Precaution AS Precaution, " _
                               & "Lab_Order_TestDtl.labotd_DateTime AS TestDate, " _
                               & "CONVERT(varchar(100),Lab_Order_MST.labom_OrderID) AS OrderID, " _
                               & " Lab_Order_TestDtl.labotd_TestID AS TestID, " _
                               & " CONVERT(varchar(100), Lab_Order_MST.labom_PatientID) AS PatientID, " _
                               & "User_MST.sLoginName AS SampledBy, " _
                               & "contacts_mst.sName AS ReferredBy, " _
                               & "ISNULL(Provider_MST.sFirstName, '') + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, " _
                               & "ISNULL(dbo.Lab_Order_Test_Result.labotr_TestResultNumber, 0) AS TestResultNumber, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultLineNo,0) AS ResultLineNo, " _
                               & "case len(ISNUll(dbo.Lab_Order_Test_Result.labotr_TestResultName,'')) when 0 then '-' when null then '-' else dbo.Lab_Order_Test_Result.labotr_TestResultName  END AS  TestResultName, " _
                               & "dbo.Lab_Order_Test_Result.labotr_TestResultDateTime AS TestResultDateTime, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultName, '')  AS ResultName, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultValue, '') AS ResultValue, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultUnit, '') AS ResultUnit, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultRange, '')  AS ResultRange, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultType, '') AS ResultType, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag, '') AS AbnormalFlag, " _
                               & "ISNULL(dbo.Lab_Order_Test_ResultDtl.labotrd_ResultComment, '')  AS ResultComment, " _
                               & "dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTime AS ResultDateTime, " _
                               & "dbo.Lab_Order_TestDtl.labotd_TestName AS TestName, " _
                               & "dbo.Lab_Order_TestDtl.labotd_SpecimenName AS Speciman, " _
                               & "ISNULL(Lab_Collection_Mst.labcm_Name, '') AS CollectionContainer, " _
                               & "dbo.Lab_Order_TestDtl.labotd_StorageName AS StorageTemperature " _
                               & "FROM Lab_Order_MST INNER JOIN " _
                               & "Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID LEFT OUTER JOIN " _
                               & "Lab_Order_Test_Result ON Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_Result.labotr_TestID AND " _
                               & " Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN " _
                               & "Lab_Order_Test_ResultDtl ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID AND " _
                               & "Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND  " _
                               & " Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber LEFT OUTER JOIN " _
                               & "User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                               & "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                               & "contacts_mst ON Lab_Order_MST.labom_ReferredByID = contacts_mst.nContactID LEFT OUTER JOIN " _
                               & "Lab_Specimen_Mst ON Lab_Order_TestDtl.labotd_SpecimenID = Lab_Specimen_Mst.labsm_ID LEFT OUTER JOIN " _
                               & "Lab_Collection_Mst ON Lab_Order_TestDtl.labotd_CollectionID = Lab_Collection_Mst.labcm_ID " _
                               & "WHERE Lab_Order_MST.labom_OrderID = " & OrderID & " order by Lab_Order_TestDtl.labotd_LineNo" ' Lab_Order_TestDtl.labotd_TestID ,Lab_Order_Test_ResultDtl.labotrd_TestResultNumber desc"


                sqlCmd.Connection = oConnection
                da = New SqlDataAdapter(sqlCmd)
                da.Fill(dsReports, "dt_LabOrderMainReport")

                'Added by Shweta 20090104
                'Agains the bug Id:5379
                sqlCmd.CommandText = ""
                sqlCmd.CommandText = " SELECT Patient.sPatientCode AS PatientCode,ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'')+ SPACE(1) + ISNULL(Patient.sLastName,'') AS PatientName,ISNULL(Patient.sGender,'') as Gender," _
                                    & " ISNULL(Patient.SAddressLine1,'') + SPACE(1)+ ISNULL(Patient.sAddressLine2,'') AS PatientAddress,ISNULL(Patient.sPhone,'') AS PatientPhone," _
                                    & " ISNULL(Patient.sCity,'') AS PatientCity,ISNULL(Patient.sState,'') AS PatientState,ISNULL(Patient.sZIP, '') AS PatientZip,ISNULL(Patient.sCounty,'') AS PatientCounty," _
                                    & " Patient.dtDOB AS DateOfBirth, Lab_Order_MST.labom_PatientAgeYear AS AgeInYrs, " _
                                    & " Lab_Order_MST.labom_PatientAgeMonth AS AgeInMnths, Lab_Order_MST.labom_PatientAgeDay AS AgeInDays, ISNULL(Provider_MST.sFirstName, '') " _
                                    & " + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS Provider, ISNULL(User_MST.sLoginName, '') " _
                                    & " AS SampledBy, ISNULL(Contacts_MST.sFirstName, '') + ' ' + ISNULL(Contacts_MST.sMiddleName, '') + ' ' + ISNULL(Contacts_MST.sLastName, '') " _
                                    & " AS ReferredBy, CONVERT(varchar(100), Lab_Order_MST.labom_OrderID) AS OrderID, Lab_Order_MST.labom_TransactionDate AS TransDate, " _
                                    & " Lab_Order_MST.labom_OrderNoPrefix + ' ' + CONVERT(varchar(100), Lab_Order_MST.labom_OrderNoID) AS OrderNumber" _
                                    & " FROM Lab_Order_MST INNER JOIN " _
                                    & " Patient ON Lab_Order_MST.labom_PatientID = Patient.nPatientID LEFT OUTER JOIN " _
                                    & " User_MST ON Lab_Order_MST.labom_SampledByID = User_MST.nUserID LEFT OUTER JOIN " _
                                    & " Lab_ContactInfo ON Lab_Order_MST.labom_PreferredLabID = Lab_ContactInfo.labci_Id LEFT OUTER JOIN " _
                                    & " Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " _
                                    & " Contacts_MST ON Lab_Order_MST.labom_ReferredByID = Contacts_MST.nContactID " _
                                    & " WHERE Patient.nPatientID =" & gnPatientID
                sqlCmd.Connection = oConnection
                da = New SqlDataAdapter(sqlCmd)
                da.Fill(dsReports, "dt_PatientInfo")

                sqlCmd.CommandText = ""
                sqlCmd.CommandText = "select isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') + '-' + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as description, " _
                                      & " convert(varchar(1), dbo.lab_order_testdtl_diagcpt.labodtl_type) + isnull(dbo.lab_order_testdtl_diagcpt.labodtl_code, '') " _
                                      & "+ isnull(dbo.lab_order_testdtl_diagcpt.labodtl_description, '') as diagcpttype, dbo.lab_order_testdtl_diagcpt.labodtl_type as type," _
                                      & " convert(varchar(100), dbo.lab_order_mst.labom_orderid) as orderid,  dbo.lab_order_testdtl.labotd_testid as testid, " _
                                      & "dbo.lab_order_mst.labom_patientid as patientid, dbo.lab_order_testdtl_diagcpt.labodtl_testname as testname" _
                                      & " from  dbo.lab_order_mst inner join " _
                                      & "dbo.lab_order_testdtl on dbo.lab_order_mst.labom_orderid = dbo.lab_order_testdtl.labotd_orderid inner join " _
                                      & "dbo.lab_order_testdtl_diagcpt on dbo.lab_order_testdtl.labotd_orderid = dbo.lab_order_testdtl_diagcpt.labodtl_orderid and " _
                                      & "dbo.lab_order_testdtl.labotd_testname = dbo.lab_order_testdtl_diagcpt.labodtl_testname " _
                                       & " where lab_order_mst.labom_orderid= " & OrderID
                sqlCmd.Connection = oConnection
                da = New SqlDataAdapter(sqlCmd)
                da.Fill(dsReports, "dt_LabOrderReportCPTICD9")

                sqlCmd.CommandText = ""
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.CommandText = " SELECT CONVERT(VARCHAR(50), nPatientID) AS Ins_PatientID, nInsuranceID, CONVERT(VARCHAR(50), nInsuranceID) AS Ins_ID, " _
                                     & " sSubscriberPolicy# AS Ins_SubscriberPolicyNo, sSubscriberID AS Ins_SubscriberID, sGroup AS Ins_Group, sEmployer AS Ins_Employer, " _
                                     & " dtDOB AS Ins_DOB, ISNULL(sSubFName, '') + ' ' + ISNULL(sSubMName, '') + ' ' + ISNULL(sSubLName, '') AS Ins_Subscribername, " _
                                     & " CASE nInsuranceFlag WHEN 1 THEN 'Primary' WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary' ELSE 'InActive' END AS Ins_PrimaryFlag, " _
                                     & " sInsurancePhone AS Ins_InsurancePhone, sInsuranceName AS InsuranceName " _
                                     & " FROM PatientInsurance_DTL"
                sqlCmd.Connection = oConnection
                da = New SqlDataAdapter(sqlCmd)
                da.Fill(dsReports, "dt_PatientInsDtl")
                'End Code 20090104
                da.Dispose()
                oLabs.SetDataSource(dsReports)
            End If
            Return oLabs
        Catch ex As Exception
            oLabs = Nothing
            Return oLabs
        Finally
            oConnection = Nothing
            sqlCmd = Nothing
            da.Dispose()
            da = Nothing
            oLabs = Nothing
            dsReports = Nothing
        End Try
    End Function
    'End code Adding 20091028


    'Changed by Shweta 20091028
    'against PER with Case No :GLO2008-0001765
    Public Function FaxLabOrder(ByVal OrderID As Long, ByVal arrTests As ArrayList)
        Try

            'Create report object
            Dim oLabs As Rpt_LabOrder = New Rpt_LabOrder()

            'Call create report function to prepare report for sending fax
            oLabs = CreateReport(OrderID, arrTests)

            mdlFAX.gstrFAXContactPerson = ""
            mdlFAX.gstrFAXContactPersonFAXNo = ""
            mdlFAX.multipleRecipients = False
            mdlFAX.gstrFAXContacts = Nothing


            'sarika internet fax
            If gblnInternetFax = False Then

                'sarika 12th oct 07
                'Check FAX Printer settings are set or not
                If isPrinterSettingsSet(True) = False Then
                    Exit Function
                End If
                '--------

                Try
                    MainMenu.SetFAXPrinterDefaultSettings1()
                Catch ex As Exception
                    MessageBox.Show("Error in setting Default Printer settings", "Lab Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

                'Retrieve the FAX Cover Page details
                'Find FAX Parameters
                'Get Pharmacy FAX No
                Dim strFAXTo As String
                Dim strFAXNo As String
                Dim objmytable As mytable
                Dim objFAX As New clsFAX
                'objmytable = objFAX.GetPharmacyFAXNo(gnPatientID)

                'If Not IsNothing(objmytable) Then
                '    gstrFAXContactPersonFAXNo = objmytable.Description
                '    gstrFAXContactPerson = objmytable.Code
                'End If

                'If Trim(gstrFAXContactPerson) = "" Then
                '    gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
                'End If

                ' If gblnFAXCoverPage Then
                mdlFAX.Owner = Me
                If RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
                    Exit Function
                End If
                'Else
                'If Trim(gstrFAXContactPersonFAXNo) = "" Then
                '    gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
                'End If
                'End If


                'code commented by sarika 13th nov 07 -- for 1 fax to multiple recipients

                'If Trim(gstrFAXContactPersonFAXNo) = "" Then
                '    MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    'sarika 3rd oct 07
                '    'the fax is send even then the fax no. is not entered.
                '    Exit Function
                '    '----------------------------------
                'End If

                ''Retrieve FAX Document Name
                'Dim strFAXDocumentName As String
                'strFAXDocumentName = RetrieveFAXDocumentName()

                ''If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Function
                'If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

                'objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                'oRpt.PrintOptions.PrinterName = gstrFAXPrinterName
                'oRpt.PrintToPrinter(1, False, 0, 0)
                'objFAX = Nothing
                '--------


                'code added by sarika 13th nov 07 -- for 1 fax to multiple recipients
                If multipleRecipients = False Then


                    If Trim(gstrFAXContactPersonFAXNo) = "" Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'sarika 3rd oct 07
                        'the fax is send even then the fax no. is not entered.
                        Exit Function
                        '----------------------------------
                    End If

                    'Retrieve FAX Document Name
                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()
                    If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function
                    objFAX.AddPendingFAX(gnPatientID, gstrFAXContactPerson, "Labs", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                    oLabs.PrintOptions.PrinterName = gstrFAXPrinterName
                    oLabs.PrintToPrinter(1, False, 0, 0)

                Else

                    If gstrFAXContacts.Count = 0 Then
                        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'sarika 3rd oct 07
                        'the fax is send even then the fax no. is not entered.
                        Exit Function
                        '----------------------------------
                    End If

                    'Retrieve FAX Document Name
                    Dim strFAXDocumentName As String
                    strFAXDocumentName = RetrieveFAXDocumentName()

                    Dim strFAXDocumentName1 As String = ""
                    strFAXDocumentName1 = strFAXDocumentName

                    For i As Integer = 0 To gstrFAXContacts.Count - 1
                        strFAXDocumentName = strFAXDocumentName1 & i.ToString
                        If MainMenu.SetFAXPrinterDocumentSettings1(strFAXDocumentName) = False Then Exit Function

                        Dim mynode As myTreeNode

                        mynode = New myTreeNode

                        mynode = CType(gstrFAXContacts.Item(i + 1), myTreeNode)

                        objFAX.AddPendingFAX(gnPatientID, mynode.Text, "Labs", mynode.Tag, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
                        oLabs.PrintOptions.PrinterName = gstrFAXPrinterName
                        oLabs.PrintToPrinter(1, False, 0, 0)
                        mynode = Nothing
                    Next



                End If
                objFAX = Nothing
                '------
           
            Else
                'sarika internet fax
                mdlFAX.Owner = Me


                If RetrieveFAXDetails(mdlFAX.enmFAXType.Labs, gnPatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Labs", 0, 0, 0) = False Then
                    Exit Function
                End If

                Dim objclsFaxReport As New clsPrintFaxReport

                objclsFaxReport.FaxReport(oLabs)
            End If



        Catch ex As Exception

        End Try
    End Function
    'End Code Adding 20091028

    'sarika internet fax
    Private Sub gloUCLab_History_gUC_FillOrder(ByVal CriteriaNumber As Short) Handles gloUCLab_History.gUC_FillOrder
        Try
            Dim oLabOrders As New LabActor.LabOrders
            Dim oLabOrder As New gloEMRLabOrder
            oLabOrders = oLabOrder.GetOrders(_OrderParamter.PatientID, CriteriaNumber, False)
            If Not oLabOrders Is Nothing Then
                gloUCLab_History.FillOrder(CriteriaNumber, oLabOrders)
            End If
            oLabOrders = Nothing
            oLabOrder = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gloUCLab_History_gUC_OpenLabForModify(ByVal OrderID As Long) Handles gloUCLab_History.gUC_OpenLabForModify
        Try
            With _OrderParamter
                .IsEditMode = True
                .OrderID = OrderID
                .OrderNumberID = 0
                .OrderNumberPrefix = "ORD"
                .PatientID = _OrderParamter.PatientID
                .VisitID = _OrderParamter.VisitID
                .TransactionDate = _OrderParamter.TransactionDate
                .CloseAfterSave = True

            End With
            _blnRecordLock = False
            If gblnRecordLocking = True Then
                Dim mydt As New mytable
                mydt = Scan_n_Lock_Transaction(TrnType.Labs, _OrderParamter.OrderID, 0, Now)
                If mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This Patient Order is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                        ''Record open only for view.
                        _blnRecordLock = True

                    Else
                        'Return False
                        Exit Sub
                    End If

                End If
                Call Set_RecordLock(_blnRecordLock)
            End If


            Call LoadOrder()
            Dim IsAcknoledgement As Boolean = False
            If IsAcknoledgement = CheckAcknoledgement(_OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID) Then
                tlbbtn_Acknowledgment.Visible = True
                tlbbtn_VWAcknowledgment.Visible = False
                If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_VWAcknowledgment.Name)) Then
                    ts_LabMain.ButtonsToHide.Add(tlbbtn_VWAcknowledgment.Name)
                End If
                ts_LabMain.ButtonsToHide.Remove(tlbbtn_Acknowledgment.Name)

            Else
                tlbbtn_Acknowledgment.Visible = False
                tlbbtn_VWAcknowledgment.Visible = True
                If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_Acknowledgment.Name)) Then
                    ts_LabMain.ButtonsToHide.Add(tlbbtn_Acknowledgment.Name)
                End If
                ts_LabMain.ButtonsToHide.Remove(tlbbtn_VWAcknowledgment.Name)
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Lab Request Orders opened to modify.  ", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gloUCLab_History_gUC_PreviewOrder(ByVal OrderID As Long) Handles gloUCLab_History.gUC_PreviewOrder
        Try
            Dim frm As New frmViewLabReport
            frm.OrderID = OrderID

            'sarika Lab Order History PrintFax Fix 20090511
            'get the testnames for the selected order
            Dim oLabOrder As New gloEMRLabOrder
            frm.arrTestNames = oLabOrder.GetOrderTests(OrderID)
            '---
            oLabOrder = Nothing
            frm.ShowDialog()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders  viewed.  ", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub gloUCLab_History_gUC_PrintOrder(ByVal OrderID As Long) Handles gloUCLab_History.gUC_PrintOrder
        Try
            _OrderParamter.CloseAfterSave = False
            'sarika Labs Denormalization 20090323
            'PrintLabOrderReport(OrderID, oLabActor_Order1.ArrTestID)

            'sarika Lab Order History PrintFax Fix 20090511
            'get the testnames for the selected order
            Dim oLabOrder As New gloEMRLabOrder
            Dim arrTests As ArrayList

            arrTests = oLabOrder.GetOrderTests(OrderID)
            '---


            PrintLabOrderReport(OrderID, arrTests)

            '--
            _OrderParamter.CloseAfterSave = True
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Print, "Lab Request Orders printed", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

    Public Sub New(Optional ByVal blnRecordLock As Boolean = False)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _blnRecordLock = blnRecordLock
        ' Add any initialization after the InitializeComponent() call.
        _OrderParamter = New LabRequestOrderParameter
    End Sub

    Public Sub New(ByVal _PatientID As Int64, ByVal _OrderID As Int64)  ''by sudhir 20081114
        PendingLabPatientId = _PatientID
        PendingLabOrderId = _OrderID
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _OrderParamter = New LabRequestOrderParameter
    End Sub

    Protected Overrides Sub Finalize()
        _OrderParamter = Nothing
        MyBase.Finalize()
    End Sub

#Region "Instruction & Precuation Button CLick"
    'Private Sub btnInstruction_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '//Instruction//
    '    pnlInstruction_Detail.Height = 0
    '    pnlInstruction.Height = 34
    '    btnInstruction_Up.Visible = True
    '    btnInstruction_Down.Visible = False
    'End Sub

    'Private Sub btnPreuation_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '//Precuation//
    '    pnlPrecuation_Detail.Height = 0
    '    pnlPreuation.Height = 34
    '    btnPreuation_Up.Visible = True
    '    btnPreuation_Down.Visible = False
    'End Sub

    'Private Sub btnPreuation_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '//Precuation//
    '    pnlPrecuation_Detail.Height = 96
    '    pnlPreuation.Height = 130
    '    btnPreuation_Up.Visible = False
    '    btnPreuation_Down.Visible = True
    'End Sub

    'Private Sub btnInstruction_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '//Instruction//
    '    pnlInstruction_Detail.Height = 96
    '    pnlInstruction.Height = 130
    '    btnInstruction_Up.Visible = False
    '    btnInstruction_Down.Visible = True
    'End Sub
#End Region

    Private Sub ts_LabMain_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_LabMain.ItemClicked
        Try
            Select Case e.ClickedItem.Name
                Case tlbbtn_New.Name
                    MenuEvent_New()
                Case tlbbtn_Save.Name
                    MenuEvent_Save(0)
                    If _OrderParamter.CloseAfterSave = True Then
                        Me.Close()
                    End If
                Case tlbbtn_Finish.Name
                    MenuEvent_Save(1)
                    If _OrderParamter.CloseAfterSave = True Then
                        Me.Close()
                    End If
                Case tlbbtn_Print.Name
                    MenuEvent_Print()
                Case tlbbtn_Fax.Name
                    MenuEvent_Fax()
                Case tlbbtn_Close.Name
                    _CloseClicked = True
                    MenuEvent_Close()
                Case tlbbtn_Previous.Name
                    If tlbbtn_Previous.Text.Trim = "Sh&ow" Then
                        MenuEvent_ShowHide(True)
                    ElseIf tlbbtn_Previous.Text.Trim = "Hi&de" Then
                        MenuEvent_ShowHide(False)
                    End If
                Case tlbbtn_HL7.Name
                    MenuEvent_HL7()
                Case tlbbtn_Acknowledgment.Name
                    AddAcknowledgment()
                Case tlbbtn_VWAcknowledgment.Name
                    ViewAcknowledgment()
                Case tlbbtn_PrvLabs.Name

                    GloUC_TransactionHistory.LoadPreviousLabs(gnPatientID, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"))
                    If pnlTransactionHistory.Visible = False Then
                        pnlTransactionHistory.Visible = True
                        spltTransactionHistory.Visible = True
                        GloUC_TransactionHistory.DesignTestGrid()
                        GloUC_TransactionHistory.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                        GloUC_TransactionHistory.cmbCriteria.Text = "Date"
                    Else

                        pnlTransactionHistory.Visible = False
                        spltTransactionHistory.Visible = False
                        GloUC_TransactionHistory.DesignTestGrid()
                        GloUC_TransactionHistory.cmbCriteria.Text = ""
                        GloUC_TransactionHistory.dtpFrom.Text = DateTime.Now.Date
                        GloUC_TransactionHistory.dtpToDate.Text = DateTime.Now.Date
                        GloUC_TransactionHistory.cmbType.Text = ""
                        GloUC_TransactionHistory.cmbType.Items.Clear()
                    End If

                    '''' 
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SaveOrder(ByVal IsFinished As Int16, Optional ByVal NewProviderID As Int64 = 0)
        Dim oLabOrderRequest As New gloEMRLabOrder
        'Dim oLabActor_Order As New LabActor.LabOrder

        Try


            '#---CHECK VALIDATION---#

            '#---SAVE DETAILS---#

            'Assign Actor to Object
            oLabOrderRequest.LabOrder = oLabActor_Order1
            _OrderParamter.CloseAfterSave = True

            'Assign Values to Order Object
            With oLabActor_Order1
                '<<<---1. Lab Order Master Record--->>>
                If _OrderParamter.IsEditMode = False Then
                    .OrderID = 0
                Else
                    .OrderID = _OrderParamter.OrderID
                End If

                'Data From Order Detail User Control
                gloUCLab_OrderDetail.ReadData()
                .OrderNoPrefix = gloUCLab_OrderDetail.OrderNumberPrefix
                .OrderNoID = gloUCLab_OrderDetail.OrderNumberID
                .PreferredLab = gloUCLab_OrderDetail.PreferredLab
                .PreferredLabID = gloUCLab_OrderDetail.PreferredLabID
                .SampledBy = gloUCLab_OrderDetail.SampledBy
                .SampledByID = gloUCLab_OrderDetail.SampledByID
                .ReferredBy = gloUCLab_OrderDetail.ReferredBy
                .ReferredByID = gloUCLab_OrderDetail.ReferredByID
                .Users = gloUCLab_OrderDetail.Users

                .TaskDescription = gloUCLab_OrderDetail.TaskDescription
                .TaskDueDate = gloUCLab_OrderDetail.TaskDueDate


                'sarika Labs Denormalization 20090318
                .ReferredByFName = gloUCLab_OrderDetail.ReferredByFName
                .ReferredByMName = gloUCLab_OrderDetail.ReferredByMName
                .ReferredByLName = gloUCLab_OrderDetail.ReferredByLName

                '-------

                'Data From Patient Detail User Control

                .TransactionDate = Me.gloUC_PatientStrip1.TransactionDate
                .PatientID = _OrderParamter.PatientID
                .VisitID = GenerateVisitID(.TransactionDate) ' _OrderParamter.VisitID
                .PatientAge.Years = Me.gloUC_PatientStrip1.PatientAge.Years
                .PatientAge.Months = Me.gloUC_PatientStrip1.PatientAge.Months
                .PatientAge.Days = Me.gloUC_PatientStrip1.PatientAge.Days
                .PatientAge.Age = Me.gloUC_PatientStrip1.PatientAge.Age
                .Provider = Me.gloUC_PatientStrip1.Provider
                If NewProviderID = 0 Then
                    .ProviderID = Me.gloUC_PatientStrip1.ProviderID '  1 'gnProviderID
                Else
                    .ProviderID = NewProviderID '  1 'gnProviderID
                End If

                .DMSID = 0

                '<<<---2. Lab Order Master - Test Details--->>>
                oLabActor_Order1.OrderTests = gloUCLab_Transaction.GetData
                'sarika Labs Denormalization 20090323
                'oLabActor_Order1.ArrTestID = gloUCLab_Transaction.arrTestID
                oLabActor_Order1.ArrTestName = gloUCLab_Transaction.arrTestNames
                '----
                If IsNothing(oLabActor_Order1.OrderTests) = True Then
                    MessageBox.Show("There must be at least one test.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _OrderParamter.CloseAfterSave = False
                    blnSaved = False
                    Exit Sub
                End If
            End With


            'Save/Modify
            If _OrderParamter.IsEditMode = False Then
                _OrderParamter.OrderID = oLabOrderRequest.Add(IsFinished)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Request Orders added.  ", gloAuditTrail.ActivityOutCome.Success)
            Else
                oLabOrderRequest.Modify(_OrderParamter.OrderID, IsFinished)

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Request Orders modified.  ", gloAuditTrail.ActivityOutCome.Success)
                ' AddTasks(oLabActor_Order.Users, oLabActor_Order.TransactionDate)
            End If
            ''Fpr disease mangement
            If Not _arrLabs Is Nothing Then
                If _arrLabs.Count > 0 Then
                    For i As Integer = 0 To _arrLabs.Count - 1
                        Dim blnIsPresent As Boolean = False
                        Dim lst As New myList
                        lst = CType(_arrLabs(i), myList)
                        For j As Integer = 0 To oLabActor_Order1.OrderTests.Count - 1
                            Dim objTest As LabActor.OrderTest
                            objTest = oLabActor_Order1.OrderTests.Item(j)
                            If lst.ID = objTest.TestID Then
                                lst.IsFinished = True
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If

            Dim objclsPatientOrders As New clsLM_Orders
            Dim TaskID As Long
            'Comment by sudhir 20090207
            'TaskID = objclsPatientOrders.GetTaskID(oLabActor_Order1.PatientID, oLabActor_Order1.TransactionDate, ClsTasksDBLayer.TaskType.LabOrder)
            TaskID = objclsPatientOrders.GetTaskID_OfLab(_OrderParamter.OrderID)
            objclsPatientOrders = Nothing

            If oLabActor_Order1.Users.Count > 0 Then
                AddTasks(oLabActor_Order1.Users, oLabActor_Order1.TransactionDate, oLabActor_Order1.PatientID, TaskID, oLabActor_Order1.TaskDescription, oLabActor_Order1.TaskDueDate)
            End If

            If Me.IsMdiChild = True Then
                Dim frm As MainMenu
                frm = Me.MdiParent
                frm.Fill_Tasks()
            End If

            ''oLabActor_Order = Nothing
            oLabOrderRequest = Nothing

            ''#---AFTER SAVE---#
            'If _OrderParamter.CloseAfterSave = True Then
            '    Me.Close()
            'Else
            '    '' Temporary Commented By Mahesh For HL7 Labs Testing
            '    '' 20070606
            '    ' MenuEvent_New()
            'End If
            blnSaved = True

            ''Sudhir 20081114
            If PendingLabOrderId <> 0 Then
                Dim Connection As New SqlClient.SqlConnection(GetConnectionString)
                Try

                    Connection.Open()
                    Dim cmd As New SqlClient.SqlCommand("Delete from dbo.HL7_PendingLabOrders where nLabOrderID=" & PendingLabOrderId & "", Connection)
                    cmd.ExecuteNonQuery()
                    PendingLabOrderId = 0
                    RaiseEvent OrderConfirmed()
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
            ''End Sudhir

        Catch ex As Exception
            MessageBox.Show("Error while saving lab order : " & ex.ToString, "Lab Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
            blnSaved = False
        End Try
    End Sub

    Private Sub MenuEvent_ShowHide(ByVal IsShow As Boolean)

        pnlPrevious.Visible = IsShow
        splRight.Visible = IsShow

        If IsShow = False Then
            tlbbtn_Previous.Text = " Sh&ow "
        Else
            tlbbtn_Previous.Text = " Hi&de "
        End If


    End Sub

    'Commented by Shweta 20091028
    'against PER with Case No :GLO2008-0001765
    'As we are using new report from gloEMRReport project
    'Function to print the Crystal report
    'Public Sub PrintLabOrderReport(ByVal OrderID As Long, ByVal arrTests As ArrayList)
    '    Dim oRpt As ReportDocument
    '    Dim _strPath As String = gstrgloEMRStartupPath & "\Reports\rptLabOrderReport.rpt"
    '    '  Dim _strPath As String = "D:\gloEMR4.0\gloEMR\gloEMR\bin\Debug\Reports\rptLabOrderReport.rpt"
    '    'Dim _strPath As String = "E:\gloEMR Working Folder\gloEMR\bin\Reports\rptLabOrderReport.rpt"
    '    Try
    '        oRpt = New ReportDocument
    '        oRpt.Load(_strPath)
    '        Dim crtableLogoninfos As New TableLogOnInfos
    '        Dim crtableLogoninfo As New TableLogOnInfo
    '        Dim crConnectionInfo As New ConnectionInfo
    '        Dim CrTables As Tables
    '        Dim CrTable As Table
    '        Dim TableCounter

    '        With crConnectionInfo
    '            .AllowCustomConnection = True
    '            .ServerName = gstrSQLServerName
    '            'If you are connecting to Oracle there is no 
    '            'DatabaseName. Use an empty string. 
    '            'For example, .DatabaseName = "" 
    '            .DatabaseName = gstrDatabaseName
    '            '.UserID = "Your User ID"
    '            '.Password = "Your Password"
    '            .IntegratedSecurity = True
    '        End With

    '        'This code works for both user tables and stored 
    '        'procedures. Set the CrTables to the Tables collection 
    '        'of the report 

    '        CrTables = oRpt.Database.Tables

    '        'Loop through each table in the report and apply the 
    '        'LogonInfo information 

    '        For Each CrTable In CrTables
    '            crtableLogoninfo = CrTable.LogOnInfo
    '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '            CrTable.ApplyLogOnInfo(crtableLogoninfo)

    '            'If your DatabaseName is changing at runtime, specify 
    '            'the table location. 
    '            'For example, when you are reporting off of a 
    '            'Northwind database on SQL server you 
    '            'should have the following line of code: 

    '            CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

    '        Next
    '        Dim prm As ParameterValues
    '        Dim discreteval As ParameterDiscreteValue

    '        oRpt.Refresh()

    '        ''//patientid
    '        prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
    '        prm.Clear()
    '        discreteval = New ParameterDiscreteValue
    '        discreteval.Value = CType(OrderID, String)
    '        prm.Add(discreteval)
    '        oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)


    '        prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
    '        prm.Clear()

    '        Dim valcnt As Integer = 0
    '        For valcnt = 0 To arrTests.Count - 1
    '            discreteval = New ParameterDiscreteValue
    '            discreteval.Value = CType(arrTests.Item(valcnt), String)
    '            prm.Add(discreteval)
    '            discreteval = Nothing
    '        Next
    '        oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

    '        If gblnUseDefaultPrinter = False Then
    '            'sarika Show Print Dialog 20080923
    '            '                        oRpt.PrintToPrinter(1, False, 0, 0)
    '            'PrintDialog1.UseEXDialog = True
    '            PrintDialog1 = New PrintDialog()
    '            '            PrintDialog1.ShowDialog()
    '            'If PrintDialog1.Then Then
    '            'If Not IsNothing(oRpt) Then
    '            '    oRpt.Close()
    '            'End If
    '            If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

    '                oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
    '                '     ApplyRptParameters(oRpt, sCheckstate, sPrescriptionDate, blnIsFax, dtPrescriptiondate, strPrescriptiondate)
    '                oRpt.Load(Application.StartupPath & "\Reports\rptLabOrderReport.rpt")


    '                oRpt.PrintToPrinter(1, False, 0, 0)

    '                If Not IsNothing(oRpt) Then
    '                    oRpt.Close()
    '                End If

    '            End If
    '        Else
    '            oRpt.PrintToPrinter(1, False, 0, 0)

    '        End If
    '        '----------------------


    '    Catch ex As Exception

    '    End Try

    'End Sub
    'End Commenting


    'Added By Shweta 20091024
    'Function to print the Crystal report for labs
    'Function t

    'Added by Shweta 20091028
    'Print lab order 
    Public Sub PrintLabOrderReport(ByVal OrderID As Long, ByVal arrTests As ArrayList)

        'Create the object for report
        Dim oLabs As Rpt_LabOrder = New Rpt_LabOrder()
        
        If OrderID <> 0 Then

            oLabs = CreateReport(OrderID, arrTests)

            If gblnUseDefaultPrinter = False Then
                PrintDialog1 = New PrintDialog()
                If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then

                    oLabs.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName

                    'ApplyRptParameters(oLabs, sCheckstate, sPrescriptionDate, blnIsFax, dtPrescriptiondate, strPrescriptiondate)
                    oLabs.PrintToPrinter(1, False, 0, 0)

                    If Not IsNothing(oLabs) Then
                        oLabs.Close()
                    End If

                End If
            Else
                oLabs.PrintToPrinter(1, False, 0, 0)
            End If

        End If
    End Sub
    'End code adding 20091028


    Private Sub btnGroups_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGroups.MouseLeave
        If pnl_btnGroups.Dock = DockStyle.Top Then
            btnGroups.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnGroups.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnGroups.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnGroups.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnTests_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTests.MouseLeave
        If pnl_btnTests.Dock = DockStyle.Top Then
            btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
            btnTests.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
            btnTests.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub


#Region "Commented by Pramod for DMSV2"
    'Private Sub gloUCLab_Transaction_gUC_ViewDocument(ByVal TestID As Long, ByVal DocumentID As Long) Handles gloUCLab_Transaction.gUC_ViewDocument
    '    'View Doucment

    '    Dim oDMSPath As New gloStream.gloDMS.Supporting.Supporting
    '    'Check DMS System Path is Correct
    '    If oDMSPath.IsDMSSystem(DMSRootPath) = False Then
    '        MessageBox.Show("Document Management System Path not set", gstrMessageBoxCaption, MessageBoxButtons.OK)
    '        Exit Sub
    '    End If
    '    oDMSPath = Nothing

    '    If gnPatientID = 0 Then
    '        MessageBox.Show("Please select patient", "Pull Chart", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    End If

    '    '''''<><><><><> Check Patient Status <><><><><><>''''
    '    ''''' 20070125 -Mahesh 
    '    If CheckPatientStatus(gnPatientID) = False Then
    '        Exit Sub
    '    End If
    '    '''''<><><><><> Check Patient Status <><><><><><>''''

    '    Dim frm As New frmDMS_ViewDocument
    '    Dim _DocNode As TreeNode
    '    Dim _blnLoadForm As Boolean = False
    '    With frm
    '        'pnlLeft.Visible = False
    '        '.MdiParent = Me
    '        '.WindowState = FormWindowState.Maximized
    '        '.Show()
    '        Dim oDocument As New gloStream.gloDMS.Document.Document
    '        If Val(oDocument.DocumentCount(gnPatientID, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument)) > 0 Then
    '            _blnLoadForm = True
    '        End If
    '        oDocument = Nothing

    '        If _blnLoadForm = True Then
    '            'pnlLeft.Visible = False
    '            '.MdiParent = Me
    '            '._ViewPatientDirective = True
    '            .txtPatientID.Tag = gnPatientID
    '            ._DMSDocumentFileName = DocumentID
    '            .WindowState = FormWindowState.Maximized
    '            .Show()
    '        Else
    '            If gnPatientID <> 0 Then
    '                frm = Nothing
    '                MessageBox.Show("No Scan Documents present for: " & gstrPatientFirstName & " " & gstrPatientLastName, "Pull Chart", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '        End If
    '    End With
    'End Sub
#End Region

    Private Sub gloUCLab_Transaction_gUC_ViewDocument(ByVal TestID As Long, ByVal DocumentID As Long) Handles gloUCLab_Transaction.gUC_ViewDocument
        If gnPatientID = 0 Then
            MessageBox.Show("Please select patient", "Pull Chart", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        '''''<><><><><> Check Patient Status <><><><><><>''''
        ''''' 20070125 -Mahesh 
        If CheckPatientStatus(gnPatientID) = False Then
            Exit Sub
        End If
        '''''<><><><><> Check Patient Status <><><><><><>''''

        'If (DocumentID > 0) Then
        '    Dim oViewDocument As New gloEDocument.gloEDocumentManagement()
        '    oViewDocument.ShowEDocument(gnPatientID, True, "", DocumentID, True, Me.ParentForm)
        '    oViewDocument.Dispose()
        'End If
        If (DocumentID > 0) Then
            ''Dim oViewDocument As New gloEDocumentV3.gloEDocV3Management()
            If IsNothing(oViewDocument) Then
                oViewDocument = New gloEDocumentV3.gloEDocV3Management()
            End If

            oViewDocument.ShowEDocument(gnPatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Me.ParentForm, gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder, DocumentID)
            oViewDocument.Dispose()

        End If
    End Sub

    'Private Sub gloUCLab_Transaction_ShowGraph(ByVal sender As Object, ByVal e As System.EventArgs) Handles gloUCLab_Transaction.ShowGraph
    '    'Dim oGraphs As New frmLab_Graphs(True, System.DateTime.Today, System.DateTime.Today, gloUCLab_Transaction.LabTestName, gloUCLab_Transaction.LabResultName)
    '    ''oGraphs.ShowDialog()
    '    'With oGraphs
    '    '    .ShowInTaskbar = False
    '    '    .ShowDialog(Me)
    '    'End With
    '    'Me.Close()
    '    'MessageBox.Show("Under construvtion")
    '    Dim dt As DataTable
    '    Dim TestId As Int64
    '    TestId = gloUCLab_Transaction.LabTestId

    '    Dim ResultId As Int64
    '    ResultId = gloUCLab_Transaction.LabResultId

    '    Dim dtSelectedResultToDate As DateTime
    '    dtSelectedResultToDate = gloUCLab_Transaction.dtSelectedToDt

    '    Dim testName As String = ""
    '    testName = gloUCLab_Transaction.LabTestName

    '    Dim resultName As String = ""
    '    resultName = gloUCLab_Transaction.LabResultName

    '    'Dim dtSelectedResultFromDate As DateTime
    '    'dtSelectedResultFromDate = Format(gloUCLab_Transaction.dtSelectedFromDt, "MM/dd/yyyy")
    '    'dtSelectedResultFromDate = CType(dtSelectedResultFromDate & " 23:59:00.00 PM", DateTime)
    '    'CType(dtFrom.Text & " 12:01:00.00 AM", DateTime)

    '    Dim olab_graphs As New frmLab_Graphs
    '    dt = olab_graphs.fillData(System.DateTime.Today, dtSelectedResultToDate, True, TestId, ResultId)

    '    '-----------------------------------------
    '    Dim dt_MINMAX As New DataTable
    '    Dim dt_OnlyMinMax As DataTable

    '    ''''''' new data table req. for the data fill for the ranges
    '    dt_OnlyMinMax = New DataTable

    '    Dim clmnMin As New DataColumn
    '    With clmnMin
    '        .ColumnName = "Min"
    '        '.DataType = System.Type.GetType("System.integer")
    '    End With
    '    dt_OnlyMinMax.Columns.Add(clmnMin)

    '    Dim clmnMax As New DataColumn
    '    With clmnMax
    '        .ColumnName = "Max"
    '        '.DataType = System.Type.GetType("System.Integer")
    '        '.DefaultValue = strVal(1)
    '    End With
    '    dt_OnlyMinMax.Columns.Add(clmnMax)
    '    ''''''''

    '    Dim j As Integer = 0
    '    Dim nActualValue = 0

    '    For j = 0 To dt.Rows.Count - 1
    '        'dt_MINMAX = getMinMAxRanges(dt.Rows(j)(0))  'Split(dt.Rows(j)("labotrd_ResultRange"), "-")
    '        'nActualValue = getMinMAxRanges()
    '        Dim strVal() As String
    '        If IsDBNull(dt.Rows(j)(0)) = False Then
    '            strVal = Split(dt.Rows(j)(0), "-")
    '        End If

    '        If strVal.Length <= 1 Then
    '            MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Exit Sub
    '        End If

    '        Dim r As DataRow
    '        r = dt_OnlyMinMax.NewRow()
    '        If IsNothing(strVal) = False Then
    '            r.Item(0) = CType(strVal(0), Integer)
    '            r.Item(1) = CType(strVal(1), Integer)
    '            dt_OnlyMinMax.Rows.Add(r)
    '        End If
    '    Next

    '    dt_MINMAX = dt_OnlyMinMax

    '    Dim dtStartdate As DateTime
    '    dtStartdate = dt.Rows.Item(0)(3) ' Take from date for Display 
    '    dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

    '    Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, TestId, ResultId, gnPatientID, testName, resultName, dt, dt_MINMAX, False)
    '    'oGraphResult.ShowDialog()
    '    With oGraphResult
    '        '.MdiParent = Me
    '        .MdiParent = Me.MdiParent
    '        .WindowState = FormWindowState.Maximized
    '        .ShowInTaskbar = False
    '        .Show()
    '    End With
    '    'Me.Close()

    '    Exit Sub
    'End Sub


    Private Sub gloUCLab_Transaction_ShowGraph(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oGraphs As New frmLab_Graphs(True, System.DateTime.Today, System.DateTime.Today, gloUCLab_Transaction.LabTestName, gloUCLab_Transaction.LabResultName)
        ''oGraphs.ShowDialog()
        'With oGraphs
        '    .ShowInTaskbar = False
        '    .ShowDialog(Me)
        'End With
        'Me.Close()

        ' get data from the properties of UC_transaction
        Dim dt As DataTable
        Dim TestId As Int64
        TestId = gloUCLab_Transaction.LabTestId

        Dim ResultId As Int64
        ResultId = gloUCLab_Transaction.LabResultId

        Dim dtSelectedResultToDate As DateTime
        dtSelectedResultToDate = gloUCLab_Transaction.dtSelectedToDt

        Dim testName As String = ""
        testName = gloUCLab_Transaction.LabTestName

        Dim resultName As String = ""
        resultName = gloUCLab_Transaction.LabResultName

        'Dim dtSelectedResultFromDate As DateTime
        'dtSelectedResultFromDate = Format(gloUCLab_Transaction.dtSelectedFromDt, "MM/dd/yyyy")
        'dtSelectedResultFromDate = CType(dtSelectedResultFromDate & " 23:59:00.00 PM", DateTime)
        'CType(dtFrom.Text & " 12:01:00.00 AM", DateTime)

        ' Function return all the data from the given dates
        'Dim olab_graphs As New frmLab_Graphs
        'Add the gnPatientID coz constructor is changed.20100925
        Dim olab_graphs As New frmLab_Graphs(gnPatientID)
        dt = olab_graphs.fillData(System.DateTime.Today, dtSelectedResultToDate, True, TestId, ResultId)

        If Not dt Is Nothing Then
            If dt.Rows.Count <= 0 Then
                If MessageBox.Show("Test is not save do You want to save Test", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
                    blnSaved = True
                    MenuEvent_Save(0)

                    If blnSaved = True Then
                        _OrderParamter.IsEditMode = True
                    Else
                        Exit Sub
                    End If

                    dt = olab_graphs.fillData(System.DateTime.Today, dtSelectedResultToDate, True, TestId, ResultId)

                Else
                    Exit Sub
                End If
            End If
        End If
        ' datatables use for the separate data available
        Dim dt_MINMAX As New DataTable
        Dim dt_OnlyMinMax As DataTable

        ''''''' new data table req. for the data fill for the ranges
        dt_OnlyMinMax = New DataTable

        ' add column into data table for identifications
        Dim clmnMin As New DataColumn
        With clmnMin
            .ColumnName = "Min"
            '.DataType = System.Type.GetType("System.integer")
        End With
        dt_OnlyMinMax.Columns.Add(clmnMin)

        Dim clmnMax As New DataColumn
        With clmnMax
            .ColumnName = "Max"
            '.DataType = System.Type.GetType("System.Integer")
            '.DefaultValue = strVal(1)
        End With
        dt_OnlyMinMax.Columns.Add(clmnMax)
        ''''''''

        Dim j As Integer = 0
        Dim nActualValue = 0

        For j = 0 To dt.Rows.Count - 1
            'dt_MINMAX = getMinMAxRanges(dt.Rows(j)(0))  'Split(dt.Rows(j)("labotrd_ResultRange"), "-")
            'nActualValue = getMinMAxRanges()
            Dim strVal() As String
            If IsDBNull(dt.Rows(j)(0)) = False Then
                strVal = Split(dt.Rows(j)(0), "-")
            End If

            If strVal.Length <= 1 Then
                MessageBox.Show("Please Enter Range for selected Test", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'MessageBox.Show("No data available against selected Test ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim r As DataRow
            r = dt_OnlyMinMax.NewRow()
            If IsNothing(strVal) = False Then
                ' loop for aloow user first value as negative values
                If strVal(0) = "" Then
                    strVal(1) = "-" & strVal(1)
                    r.Item(0) = CType(strVal(1), Integer)
                    r.Item(1) = CType(strVal(2), Integer)
                Else
                    r.Item(0) = CType(strVal(0), Integer)
                    r.Item(1) = CType(strVal(1), Integer)
                End If

                dt_OnlyMinMax.Rows.Add(r)
            End If
        Next

        ' set data to other datatable
        dt_MINMAX = dt_OnlyMinMax

        ' lines for get the first results data and show it into the label as From-date
        Dim dtStartdate As DateTime
        dtStartdate = dt.Rows.Item(0)(3) ' Take from date for Display 
        dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

        ' view the graphs for the provided values as a parameters provided
        Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, TestId, ResultId, gnPatientID, testName, resultName, dt, dt_MINMAX, False)
        With oGraphResult
            .MdiParent = Me.MdiParent
            .WindowState = FormWindowState.Maximized
            .ShowInTaskbar = False
            .Show()
        End With
        'Me.Close()
        Exit Sub
    End Sub

    Private Sub gloUCLab_Transaction_ShowGraph_crieteria(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable
        'Dim olab_graphs As New frmLab_Graphs

        Dim TestId As Int64
        TestId = gloUCLab_Transaction.LabTestId

        Dim ResultId As Int64
        ResultId = gloUCLab_Transaction.LabResultId

        Dim dtSelectedResultToDate As DateTime
        dtSelectedResultToDate = gloUCLab_Transaction.dtSelectedToDt


        'If Not dt Is Nothing Then
        '    If dt.Rows.Count <= 0 Then

        'Dim oGraphs As New frmLab_Graphs(True, System.DateTime.Today, System.DateTime.Today, gloUCLab_Transaction.LabTestName, gloUCLab_Transaction.LabResultName)
        Dim oGraphs As New frmLab_Graphs(gnPatientID, True, System.DateTime.Today, System.DateTime.Today, gloUCLab_Transaction.LabTestName, gloUCLab_Transaction.LabResultName)
        dt = oGraphs.fillData(System.DateTime.Today, dtSelectedResultToDate, True, TestId, ResultId)
        If Not dt Is Nothing Then
            If dt.Rows.Count <= 0 Then
                If MessageBox.Show("Test is not save do You want to save Test", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
                    blnSaved = True
                    MenuEvent_Save(0)
                    If blnSaved = True Then
                        _OrderParamter.IsEditMode = True
                    Else
                        Exit Sub
                    End If
                    dt = oGraphs.fillData(System.DateTime.Today, dtSelectedResultToDate, True, gloUCLab_Transaction.LabTestId, gloUCLab_Transaction.LabResultId)
                Else
                    Exit Sub
                End If
            End If
        End If
        'oGraphs.ShowDialog()
        With oGraphs
            .ShowInTaskbar = False
            .ShowDialog(Me.MdiParent)
        End With
        'Me.Close()
    End Sub

    Public Sub AddAcknowledgment()
        If _OrderParamter.OrderID <> 0 Then
            Dim objAcw As New frmLab_Acknoledgement(_OrderParamter.OrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID) ' _OrderParamter.OrderNumberID)
            With objAcw
                .ShowInTaskbar = False
                If .ShowDialog(Me.MdiParent) = Windows.Forms.DialogResult.OK Then

                    tlbbtn_Acknowledgment.Visible = False
                    tlbbtn_VWAcknowledgment.Visible = True
                    If Not (ts_LabMain.ButtonsToHide.Contains(tlbbtn_Acknowledgment.Name)) Then
                        ts_LabMain.ButtonsToHide.Add(tlbbtn_Acknowledgment.Name)
                    End If
                    ts_LabMain.ButtonsToHide.Remove(tlbbtn_VWAcknowledgment.Name)

                End If
            End With

        End If
    End Sub

    Public Sub ViewAcknowledgment()
        If _OrderParamter.OrderID <> 0 Then
            Dim objviewAcw As New frmLab_ViewAcknoledgement(_OrderParamter.OrderID, _OrderParamter.OrderNumberPrefix, gloUCLab_OrderDetail.OrderNumberID)
            With objviewAcw
                .ShowInTaskbar = False
                .ShowDialog(Me.MdiParent)
            End With
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.View, "Lab Request Orders cvknowledgement viewed.  ", gloAuditTrail.ActivityOutCome.Success)
        End If
    End Sub

    Public Function CheckAcknoledgement(ByVal OrederNumberPrifix As String, ByVal OrderNumberID As Long) As Boolean
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        'Dim dt As New DataTable
        Dim strquery As String
        oDB.Connect(GetConnectionString)
        strquery = oDB.ExecuteQueryScaler("select * FROM Lab_Acknowledgment Where nOrderNumberPrefix = '" & OrederNumberPrifix & "' and nOrderNumberID = " & OrderNumberID & " ")
        oDB.Disconnect()
        If strquery = "" Then
            Return False
        Else
            Return True
        End If

    End Function


    ''' <summary>
    ''' this function will be fill all the past labs against that patient on the click event of the PrvLabs button from toolbar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Sub gloUCLab_OrderDetail_Lab_btnUC_ADDclick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal intStatus As Int16) Handles gloUCLab_OrderDetail.Lab_btnUC_ADDclick
        Try



            'If intStatus = 3 Then
            '    frm = New frmContactMst(False, "Insurance")
            '    frm.Text = "Add Contacts for Insurance"
            '    colno = 1
            '    frm.FillControls()
            'Elseif
            If intStatus = 1 Then
                Dim frm As frmLab_ContactInformation
                frm = New frmLab_ContactInformation()
                frm.blnContactType = LabActor.enumContactType.PreferredLab
                frm.nEditID = 0
                frm.blnIsModify = True
                '                frm.Text = "Add Contacts for Pharmacy"

                '                frm.FillControls()
                frm.ShowDialog()
                gloUCLab_OrderDetail.searchdt = frm.ContactID
            ElseIf intStatus = 2 Then
                '' Commented by Sandip Darade 20090306
                'Dim frm As frmContactMst
                'frm = New frmContactMst(True, "Physician")
                'frm.Text = "Add Contacts for Physician"
                'frm.FillControls()
                'frm.ShowDialog()
                'gloUCLab_OrderDetail.searchdt = frm.ContactID

                ''Sandip Darade 20090306
                '' New Physician
                Dim ofrm As New gloContacts.frmSetupPhysician(GetConnectionString())
                ofrm.ShowDialog()
                gloUCLab_OrderDetail.searchdt = ofrm.ContactID
            End If


            '' ButtonX14_Click(sender, e)
            '' modify cod eon 20070613 to refresh the c1Grid
            'Call loadC1flexgrid()

            '' default selection of new added row
            'Dim searchdt = frm.strData

            ''Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
            ''c1Uc.SortDataview(searchdt)

            'Dim searchrow = oC1flex._UCflex.FindRow(searchdt, 0, colno, True)
            'oC1flex._UCflex.Select(searchrow, 1)

            ''code commented on 20070524 Bipin
            ''BindGrid()
            'colno = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Contacts", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub gloUCLab_OrderDetail_Lab_btnUC_Modifyclick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal intStatus As Int16) Handles gloUCLab_OrderDetail.Lab_btnUC_Modifyclick
    '    Dim ID As Int64

    '    Try

    '        If oC1flex._UCflex.Row > 0 Then
    '            ID = CType(oC1flex._UCflex.GetData(oC1flex._UCflex.Row, "nContactID"), System.Int64)
    '            'txtPharmacy.Text = CType(dgCustomGrid.CurrentName, System.String)
    '            'txtPharmacy.Tag = CType(dgCustomGrid.CurrentID, Long)
    '            btnPharmacyBrowse.Focus()
    '            'Else
    '            '    oC1flex_btnUC_ADDclick(sender, e)
    '            '    Exit Sub
    '        End If

    '        Dim frm As frmContactMst

    '        If intStatus = 1 Then
    '            frm = New frmContactMst(False, ID, "Pharmacy")
    '            frm.Text = "Add Contacts for Pharmacy"
    '            frm.FillControls()
    '        ElseIf intStatus = 2 Then
    '            frm = New frmContactMst(True, ID, "Physician")
    '            frm.Text = "Add Contacts for Physician"
    '            frm.FillControls()
    '        ElseIf intStatus = 3 Then
    '            frm = New frmContactMst(False, ID, "Insurance")
    '            frm.Text = "Add Contacts for Insurance"
    '            frm.FillControls()
    '        ElseIf intStatus = 4 Then
    '            frm = New frmContactMst(True, ID, "Physician")
    '            frm.Text = "Add Contacts for Referrals"
    '            frm.FillControls()
    '        End If


    '        frm.ShowDialog()

    '        ' ButtonX14_Click(sender, e)
    '        ' modify cod eon 20070613 to refresh the c1Grid
    '        Call loadC1flexgrid()

    '        ' default selection of new added row
    '        Dim searchdt = frm.strData

    '        'Dim c1Uc As New gloUC_CustomSearchInC1Flexgrid
    '        'c1Uc.SortDataview(searchdt)
    '        Dim searchrow As String = ""
    '        If intStatus = 1 Then
    '            'Pharmacy 
    '            searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
    '        ElseIf intStatus = 2 Then
    '            'Physician
    '            searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 2, True)
    '        ElseIf intStatus = 3 Then
    '            'Insurance
    '            searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
    '        ElseIf intStatus = 4 Then
    '            'Physician ---referrals
    '            searchrow = oC1flex._UCflex.FindRow(searchdt, 0, 1, True)
    '        End If

    '        oC1flex._UCflex.Select(searchrow, 1)

    '        'code commented on 20070524 Bipin
    '        'BindGrid()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub gloUCLab_OrderDetail_Lab_btnUC_Modifyclick(ByVal sender As Object, ByVal e As System.EventArgs, ByVal intStatus As Short, ByVal nEditID As Long) Handles gloUCLab_OrderDetail.Lab_btnUC_Modifyclick
        Dim ID As Int64

        Try

            If intStatus = 1 Then  'clicked on preferred lab
                Dim frm As frmLab_ContactInformation
                frm = New frmLab_ContactInformation()
                frm.blnContactType = LabActor.enumContactType.PreferredLab
                frm.blnIsModify = False
                frm.nEditID = nEditID

                frm.ShowDialog()

                '  gloUCLab_OrderDetail.searchdt = frm.strData

            ElseIf intStatus = 2 Then  'clicked on refereed by
                '' Commented by Sandip Darade 20090306
                'Dim frm As frmContactMst
                'frm = New frmContactMst(True, nEditID, "Physician")
                'frm.Text = "Add Contacts for Physician"
                'frm.FillControls()
                'frm.ShowDialog()
                'gloUCLab_OrderDetail.searchdt = nEditID

                ''Sandip Darade 20090306
                ''Modify Physician
                Dim ofrm As New gloContacts.frmSetupPhysician(nEditID, GetConnectionString())
                ofrm.ShowDialog()
                gloUCLab_OrderDetail.searchdt = nEditID
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "PatientRegistration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub GloUC_TransactionHistory_btnCloseRefillClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloUC_TransactionHistory.btnCloseRefillClick
        Try

            'dont show the previous labs control unless the btnPrvLabs order is clicked. therefore make the panel TransactionHistory visible false
            If spltTransactionHistory.Visible = True Then
                spltTransactionHistory.Visible = False
            End If
            If pnlTransactionHistory.Visible = True Then
                pnlTransactionHistory.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub



    Private Sub GloUC_TransactionHistory_btnShowGraphClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloUC_TransactionHistory.btnShowGraphClick
        Try

            Dim dt_selectedResult As New DataTable
            '''' Get selected Result From Grid
            dt_selectedResult = GloUC_TransactionHistory.SelectResult()

            '''' If DataTable is empty then exit from Procedure.
            'If dt_selectedResult Is Nothing Then
            If dt_selectedResult.Rows.Count = 0 Then
                Exit Sub
            End If
            ' End If


            ''''Get Min and Max Value from DataTable

            Dim dv As DataView
            dv = New DataView(dt_selectedResult)
            dv.Sort = "Value"

            Dim max As Integer = dv.Item(dv.Count - 1)("Value").ToString()
            Dim min As Integer = dv.Item(0)("Value").ToString() '' dv.Table.Rows.Count - 1)("Value")



            Dim dtSelectedResultToDate As DateTime = CType(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0), DateTime)


            ' lines for get the first results data and show it into the label as From-date
            Dim dtStartdate As DateTime
            dtStartdate = dt_selectedResult.Rows.Item(0)(0) ' Take from date for Display 
            dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

            ' view the graphs for the provided values as a parameters provided
            Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, 0, 0, gnPatientID, dt_selectedResult.Rows(0)(1), dt_selectedResult.Rows(0)(2), dt_selectedResult, , False, , min, max)
            With oGraphResult
                .MdiParent = Me.MdiParent
                .WindowState = FormWindowState.Maximized
                .ShowInTaskbar = False
                .Show()
            End With
            'Me.Close()
            Exit Sub
        Catch ex As Exception

        End Try
    End Sub

    Private Sub oViewDocument_EvnRefreshDocuments() Handles oViewDocument.EvnRefreshDocuments
        '''' Add Code here if you want to do any operation after close view document form
    End Sub


    Private Sub btnTests_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTests.MouseEnter
        btnTests.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnTests.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnGroups_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroups.MouseEnter
        btnGroups.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnGroups.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub gloUC_TransactionBtnDiagnCPTclicked() Handles gloUCLab_Transaction.gUC_ButtonDiagnCPTClicked
        Dim _VisitID As Int64 = 0
        _VisitID = GetVisitID(Now.Date, gnPatientID)

        If gblnICD9Driven Then
            Dim frm As New frm_Diagnosis(_VisitID, 0)
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.ShowDialog(Me)
        Else
            Dim oTreatment As New frm_Treatment(0, _VisitID, Now.Date, "")
            oTreatment.ShowDialog()
            oTreatment.Dispose()
        End If

    End Sub

    Private Sub GloUC_trvTest_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvTest.KeyPress
        Dim mynode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvTest.SelectedNode, gloUserControlLibrary.myTreeNode)
        If Not mynode Is Nothing Then
            Dim oNode As New TreeNode
            oNode.Tag = mynode.ID
            oNode.Text = mynode.Text
            Dim _Isgroup As Boolean = False
            If IsNothing(mynode.Parent) Then
                _Isgroup = True
            End If

            If pnl_btnTests.Dock = DockStyle.Top Then
                ' ''  Add Test TO The Orders
                gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)
            ElseIf pnl_btnGroups.Dock = DockStyle.Top Then
                ' ''  Add Test from Groups TO The Orders
                If _Isgroup Then
                    '' Selected Node is Group
                    ''Add all tests from that group 
                    Dim oTestNode As gloUserControlLibrary.myTreeNode
                    For Each oTestNode In GloUC_trvTest.SelectedNode.Nodes
                        gloUCLab_Transaction.AddTest(0, oTestNode.ID, oTestNode.Text, 1, "")

                    Next
                    GloUC_trvTest.SelectedNode.ExpandAll()
                Else
                    '' Selected Node is Test Under Some Group
                    ' ''  Add Test TO The Orders
                    gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")


                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)

            End If

        End If
    End Sub

    Private Sub GloUC_trvTest_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvTest.NodeMouseDoubleClick
        Dim mynode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
        If Not mynode Is Nothing Then
            Dim oNode As New TreeNode
            oNode.Tag = mynode.ID
            oNode.Text = mynode.Text
            Dim _Isgroup As Boolean = False
            If IsNothing(mynode.Parent) Then
                _Isgroup = True
            End If

            If pnl_btnTests.Dock = DockStyle.Top Then
                ' ''  Add Test TO The Orders
                gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                'Shubhangi 20091209
                'Check the setting Reset search text box after assiging category
                If gblnResetSearchTextBox = True Then
                    GloUC_trvTest.txtsearch.ResetText()
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)
            ElseIf pnl_btnGroups.Dock = DockStyle.Top Then
                ' ''  Add Test from Groups TO The Orders
                If _Isgroup Then
                    '' Selected Node is Group
                    ''Add all tests from that group 

                    Dim oTestNode As gloUserControlLibrary.myTreeNode
                    For Each oTestNode In GloUC_trvTest.SelectedNode.Nodes
                        gloUCLab_Transaction.AddTest(0, oTestNode.ID, oTestNode.Text, 1, "")

                    Next
                    GloUC_trvTest.SelectedNode.ExpandAll()
                Else
                    '' Selected Node is Test Under Some Group
                    ' ''  Add Test TO The Orders
                    gloUCLab_Transaction.AddTest(0, oNode.Tag, oNode.Text, 1, "")
                End If
                'Shubhangi 20091209
                'Check the setting Reset search text box after assiging category
                If gblnResetSearchTextBox = True Then
                    GloUC_trvTest.txtsearch.ResetText()
                End If
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Add, "Lab Test Added", gloAuditTrail.ActivityOutCome.Success)

            End If

        End If
    End Sub

    '' SUDHIR 20090626 ''
    Private Sub gloUCLab_Transaction_OnSubWindow_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles gloUCLab_Transaction.OnSubWindow_Closed
        pnlToolStrip.Visible = True
    End Sub

    Private Sub gloUCLab_Transaction_OnSubWindow_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles gloUCLab_Transaction.OnSubWindow_Opened
        pnlToolStrip.Visible = False
    End Sub
    '' END SUDHIR ''

    Private Sub oGeneralInterface_InvalidFilePath() Handles oGeneralInterface.InvalidFilePath
        blnIsInvalidHL7FilePath = True
    End Sub
End Class


Public Class LabRequestOrderParameter
    Private _OrderNumberPrefix As String = ""
    Private _OrderNumberID As Int16 = 0
    Private _OrderID As Int64 = 0

    'Order Transaction Information
    Private _IsEditMode As Boolean = False
    Private _PatientID As Int64 = 0
    Private _VisitID As Int64 = 0
    Private _CloseAfterSave As Boolean = True
    Private _TransactionDate As Date = Now  ''Added on 20070606
    Private _TransactionType As enumTransactionType
    Private _ProviderID As Long ''Added on 20071907


    Enum enumTransactionType
        None = 0
        LabOrder = 1
        LabResult = 2
        LabExternalResult = 3
    End Enum

    Public Property TransactionType() As enumTransactionType
        Get
            Return _TransactionType
        End Get
        Set(ByVal value As enumTransactionType)
            _TransactionType = value
        End Set
    End Property

    Public Property OrderNumberPrefix() As String
        Get
            Return _OrderNumberPrefix
        End Get
        Set(ByVal value As String)
            _OrderNumberPrefix = value
        End Set
    End Property

    Public Property OrderNumberID() As Int16
        Get
            Return _OrderNumberID
        End Get
        Set(ByVal value As Int16)
            _OrderNumberID = value
        End Set
    End Property

    Public Property OrderID() As Int64
        Get
            Return _OrderID
        End Get
        Set(ByVal value As Int64)
            _OrderID = value
        End Set
    End Property

    Public Property PatientID() As Int64
        Get
            Return _PatientID
        End Get
        Set(ByVal value As Int64)
            _PatientID = value
        End Set
    End Property

    Public Property VisitID() As Int64
        Get
            Return _VisitID
        End Get
        Set(ByVal value As Int64)
            _VisitID = value
        End Set
    End Property

    Public Property IsEditMode() As Boolean
        Get
            Return _IsEditMode
        End Get
        Set(ByVal value As Boolean)
            _IsEditMode = value
        End Set
    End Property

    Public Property CloseAfterSave() As Boolean
        Get
            Return _CloseAfterSave
        End Get
        Set(ByVal value As Boolean)
            _CloseAfterSave = value
        End Set
    End Property

    Public Property TransactionDate() As Date
        Get
            Return _TransactionDate
        End Get
        Set(ByVal value As Date)
            _TransactionDate = value
        End Set
    End Property
    Public Property ProviderID() As Long
        Get
            Return _ProviderID
        End Get
        Set(ByVal value As Long)
            _ProviderID = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


End Class

