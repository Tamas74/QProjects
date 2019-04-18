Imports gloEMRGeneralLibrary
Imports System.Runtime.InteropServices

Public Class gloDrugListRevised

    Private nClinicID As Int64
    Public Property ClinicID() As Int64
        Get
            Return nClinicID
        End Get
        Set(ByVal value As Int64)
            nClinicID = value
        End Set
    End Property


    Private nUserID As Int64
    Public Property UserID() As Int64
        Get
            Return nUserID
        End Get
        Set(ByVal value As Int64)
            nUserID = value
        End Set
    End Property
    Private nPatientID As Int64 = 0
    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property

    Public Enum SearchType
        StartsWith = 0
        Contains = 1
    End Enum

    Public Event DrugListClicked(ByVal Row As DataRow, ByVal DrugRoutes As List(Of String))
    Dim searchTimer As Timer = Nothing
    Public enmselectedbutton As Selectedbutton

    Private eSearchType As SearchType
    Public Property enumSearchType() As SearchType
        Get
            Return eSearchType
        End Get
        Set(ByVal value As SearchType)
            eSearchType = value
        End Set
    End Property
    Dim sCallingform As String = ""

    <DllImport("user32.dll")> _
    Private Shared Function LockWindowUpdate(hWnd As IntPtr) As Boolean
    End Function
    Public Event cntListmenuStripClick(ByVal IsProviderFavorites As Boolean)
    Public Event cntListmenuStripAddtoPlannedMedClick(ByVal drRow As DataRow, ByVal sDrugSerach As String, ByVal EnumSelectedButton As Integer)
    Enum Selectedbutton
        AllDrugs = 11
        PracticeFavorites = 12 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"
        FrequentlyUsed = 13
        ProviderFrequentlyUsed = 20
        ProviderDrugs = 21
        ClassfiedDrugs = 22
        PlannedDrugs = 23
    End Enum

    Private bAllowAddDrugs As Boolean
    Public Property AddDrugsAllow As Boolean
        Get
            Return bAllowAddDrugs
        End Get
        Set(ByVal value As Boolean)
            bAllowAddDrugs = value
        End Set
    End Property

    Private bAllowDrugsConfig As Boolean
    Public Property AllowDrugsConfig As Boolean
        Get
            Return bAllowDrugsConfig
        End Get
        Set(ByVal value As Boolean)
            bAllowDrugsConfig = value
        End Set
    End Property

    Private nProviderID As Int64
    Public Property ProviderID() As Int64
        Get
            Return nProviderID
        End Get
        Set(ByVal value As Int64)
            nProviderID = value
        End Set
    End Property

    Public Property CurrentList() As Selectedbutton
        Get
            Return enmselectedbutton
        End Get
        Set(ByVal value As Selectedbutton)
            enmselectedbutton = value
            SetCurrentDrugList()
        End Set
    End Property

    Public Sub New(ByVal rxDrugSelectedBtn As Int16, ByVal _AddDrugsAllow As Boolean, ByVal _bAllowDrugsConfig As Boolean, ByVal ProviderID As Int64, ByVal ClinicID As Int64, ByVal UserID As Int64, Optional ByVal nPatientID As Int64 = 0, Optional ByVal sDrugSearch As String = "", Optional ByVal sCallingForm As String = "")

        ' This call is required by the designer.
        InitializeComponent()
        Dim nRxSearchType As Int32 = 0

        Me.ProviderID = ProviderID
        Me.ClinicID = ClinicID
        Me.UserID = UserID
        Me.PatientID = nPatientID
        Using prescriptionLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
            nRxSearchType = prescriptionLayer.GetSearchTypeSetting(Me.ClinicID, Me.UserID)
        End Using

        If nRxSearchType = 1 Then
            Me.enumSearchType = gloDrugListRevised.SearchType.Contains
            Me.rbContains.Checked = True
        Else
            Me.enumSearchType = gloDrugListRevised.SearchType.StartsWith
            Me.rbStartswith.Checked = True
        End If

        If rxDrugSelectedBtn = 11 Then
            CurrentList = Selectedbutton.AllDrugs
        ElseIf rxDrugSelectedBtn = 12 Then
            CurrentList = Selectedbutton.PracticeFavorites
        ElseIf rxDrugSelectedBtn = 13 Then
            CurrentList = Selectedbutton.FrequentlyUsed
        ElseIf rxDrugSelectedBtn = 20 Then
            CurrentList = Selectedbutton.ProviderFrequentlyUsed
        ElseIf rxDrugSelectedBtn = 21 Then
            CurrentList = Selectedbutton.ProviderDrugs
        ElseIf rxDrugSelectedBtn = 22 Then
            CurrentList = Selectedbutton.ClassfiedDrugs
        Else
            CurrentList = Selectedbutton.ProviderDrugs
        End If

        Me.AddDrugsAllow = _AddDrugsAllow
        Me.AllowDrugsConfig = _bAllowDrugsConfig
        AddMenu(AddDrugsAllow, AllowDrugsConfig)
        txtSearch.Text = sDrugSearch
        Me.sCallingform = sCallingForm
        If sCallingForm = "POT" Then
            pnl_PlannedDrugs.Visible = False
        End If
    End Sub

    Public Sub AddMenu(ByRef bAllowAddDrugs As Boolean, ByRef bAllowDrugsConfig As Boolean)
        cntListmenuStrip.Items(0).Visible = bAllowAddDrugs
        cntListmenuStrip.Items(1).Visible = bAllowDrugsConfig
    End Sub

    Public Sub SetCurrentDrugList()

        pnl_Frequent.Visible = True
        pnl_ProviderFrequent.Visible = True
        pnl_PracticeFav.Visible = True
        pnl_AllDrugs.Visible = True
        pnl_ProviderFav.Visible = True
        pnl_Classified.Visible = True
        pnl_PlannedDrugs.Visible = True
        txtSearch.Enabled = True
        btnClear.Enabled = True

        pnlMainSearch.Enabled = True

        Select Case enmselectedbutton
            Case Selectedbutton.AllDrugs
                pnl_AllDrugs.Visible = False
                btnDrugList.Text = "All Drugs"
            Case Selectedbutton.PracticeFavorites
                pnl_PracticeFav.Visible = False
                btnDrugList.Text = "Practice Favorites"
            Case Selectedbutton.FrequentlyUsed
                pnl_Frequent.Visible = False
                btnDrugList.Text = "Frequently Used Drugs"
            Case Selectedbutton.ProviderFrequentlyUsed
                pnl_ProviderFrequent.Visible = False
                btnDrugList.Text = "Provider Frequently Used"
            Case Selectedbutton.ProviderDrugs
                pnl_ProviderFav.Visible = False
                btnDrugList.Text = "Provider Favorites"
            Case Selectedbutton.ClassfiedDrugs
                pnl_Classified.Visible = False
                btnDrugList.Text = "Classified Drugs"
                txtSearch.Enabled = False
                btnClear.Enabled = False

                pnlMainSearch.Enabled = False
                pnlSearchFilter.Visible = False
            Case Selectedbutton.PlannedDrugs
                pnl_PlannedDrugs.Visible = False
                btnDrugList.Text = "Planned Drugs"

               

        End Select

        DisplayDrugList()

    End Sub

    Private Function GetDrugList() As DataSet
        Dim helper As New PrescriptionBusinessLayer
        Dim ds As New DataSet()

        Try
            Select Case enmselectedbutton
                Case Selectedbutton.AllDrugs
                    ds.Tables.Add(helper.GetAllDrugs(txtSearch.Text, enumSearchType, 0))
                Case Selectedbutton.ClassfiedDrugs
                    ds.Tables.Add(helper.GetClassifiedDrugs())
                Case Selectedbutton.FrequentlyUsed
                    ds.Tables.Add(helper.GetFrequentlyUsedDrugs(txtSearch.Text, enumSearchType))

                Case Selectedbutton.PracticeFavorites
                    ds.Tables.Add(helper.GetAllDrugs(txtSearch.Text, enumSearchType, 1))

                Case Selectedbutton.ProviderDrugs
                    ds = helper.GetProviderDrugs(txtSearch.Text, enumSearchType, ProviderID)

                Case Selectedbutton.ProviderFrequentlyUsed
                    ds.Tables.Add(helper.GetFrequentlyUsedDrugs(txtSearch.Text, enumSearchType, ProviderID))
                Case Selectedbutton.PlannedDrugs
                    ds.Tables.Add(helper.GetPlannedDrugs(txtSearch.Text, enumSearchType, 0, PatientID))


            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                    gloAuditTrail.ActivityCategory.CreatePrescription,
                                                    gloAuditTrail.ActivityType.Add,
                                                    ex.ToString(),
                                                    gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If helper IsNot Nothing Then
                helper.Dispose()
                helper = Nothing
            End If
        End Try

        Return ds
    End Function

    Public Sub DisplayDrugList()

        Dim rootNode As New myTreeNode
        Dim ds As DataSet = GetDrugList()
        Dim dt As DataTable = Nothing

        Try
            trvMain.BeginUpdate()
            trvMain.Nodes.Clear()

            rootNode.Text = "Drugs"
            rootNode.Key = -1
            rootNode.ImageIndex = 2
            rootNode.SelectedImageIndex = 2

            If ds.Tables.Count = 0 Then
                Return
            End If

            dt = ds.Tables(0)
            LockWindowUpdate(Me.Handle)

            Dim drugNode As TreeNode = Nothing
            Dim value As String = ""
            Dim isNarcotic As Int16?

            For Each row As DataRow In dt.Rows

                If Me.CurrentList = Selectedbutton.ClassfiedDrugs Then
                    value = row("ConceptName")
                Else
                    If Convert.ToString(row("RxType")) <> "" Then
                        value = Convert.ToString(row("DrugName")) & " - " & Convert.ToString(row("RxType"))
                    Else
                        value = Convert.ToString(row("DrugName"))
                    End If
                    isNarcotic = Convert.ToInt16(row("IsNarcotic"))
                End If

                drugNode = New TreeNode(value)
                drugNode.Tag = row

                If isNarcotic.HasValue Then
                    If isNarcotic > 0 Then
                        drugNode.ForeColor = Color.Red
                        drugNode.ImageIndex = 1
                        drugNode.SelectedImageIndex = 1
                    Else
                        drugNode.ForeColor = Color.Black
                        drugNode.ImageIndex = 0
                        drugNode.SelectedImageIndex = 0
                    End If
                End If

                rootNode.Nodes.Add(drugNode)

                If Me.CurrentList = Selectedbutton.ProviderDrugs AndAlso ds.Tables.Count() > 1 Then
                    Dim nMPID As Int32 = Convert.ToInt32(DirectCast(drugNode.Tag, DataRow)("MPID"))
                    Dim sNDCCode As String = Convert.ToString(DirectCast(drugNode.Tag, DataRow)("NDCCode"))
                    Dim dtSIGDrugs As DataTable = ds.Tables(1)

                    For Each element As DataRow In dtSIGDrugs.AsEnumerable().Where(Function(p) p("MPID") = nMPID OrElse p("NDCCode") = sNDCCode)
                        Dim sDrugDisplayName As String = ""
                        Dim provNode As TreeNode = New TreeNode(Convert.ToString(element("Route")) & " " & Convert.ToString(element("Frequency")) & " " & Convert.ToString(element("Duration")) & " " & Convert.ToString(element("Refills")) & " " & Convert.ToString(element("Amount")))
                        provNode.Tag = element
                        drugNode.Nodes.Add(provNode)
                        provNode = Nothing
                    Next
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                     gloAuditTrail.ActivityCategory.CreatePrescription,
                                                     gloAuditTrail.ActivityType.Add,
                                                     ex.ToString(),
                                                     gloAuditTrail.ActivityOutCome.Failure)
        Finally
            trvMain.Nodes.Add(rootNode)
            trvMain.EndUpdate()
            trvMain.ExpandAll()

            If Me.CurrentList <> Selectedbutton.ClassfiedDrugs AndAlso trvMain.Nodes.Count > 0 Then
                trvMain.SelectedNode = trvMain.Nodes(0)
                If trvMain.Nodes(0).Nodes.Count > 0 Then
                    trvMain.SelectedNode = trvMain.Nodes(0).Nodes(0)
                End If
            End If

            LockWindowUpdate(IntPtr.Zero)
        End Try

    End Sub

    Private Sub ResetSearch()
        txtSearch.ResetText()
        txtSearch.Tag = String.Empty
        txtSearch.Focus()
    End Sub

    Private Sub ButtonClick(sender As System.Object, e As System.EventArgs) Handles btnFrequent.Click, btnProviderFrequent.Click, btnProviderFav.Click, btnPracticeFav.Click, btnClassified.Click, btnAllDrugs.Click, btnDrugList.Click, btnPlannedDrugs.Click
        If Convert.ToInt32(CType(sender, Button).Tag) <> 0 Then
            CurrentList = CType(sender, Button).Tag
        End If
        ResetSearch()
    End Sub

    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        searchTimer.Stop()
        searchTimer.Start()
    End Sub

    Private Sub gloDrugListRevised_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        searchTimer = New Timer()
        searchTimer.Interval = 400
        AddHandler searchTimer.Tick, AddressOf SearchFunction
    End Sub

    Private Sub SearchFunction(sender As Object, e As EventArgs)
        searchTimer.Stop()
        picProgress.Visible = True
        Application.DoEvents()
        DisplayDrugList()
        picProgress.Visible = False
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
    End Sub

    Private Sub LoadDrugList(ByVal Row As DataRow)
        Try
            Dim helper As New PrescriptionBusinessLayer
            Dim rowReturned As DataRow = Nothing

            If Row.Table.Columns.Contains("TherapeuticConceptTreeID") Then
                Return
            End If

            Dim mpid As Int32 = Convert.ToInt32(Row("MPID"))
            Dim ndc As String = Convert.ToString(Row("NDCCode"))

            Select Case enmselectedbutton
                Case Selectedbutton.AllDrugs, Selectedbutton.PracticeFavorites, Selectedbutton.FrequentlyUsed, Selectedbutton.ProviderFrequentlyUsed, Selectedbutton.ClassfiedDrugs, Selectedbutton.PlannedDrugs
                    rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                    If IsNothing(rowReturned) Then
                        Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                            mpid = oDIBHelper.GetMarketedProductId(ndc)
                        End Using
                        rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                    End If
                Case Selectedbutton.ProviderDrugs
                    If Row.Table.Columns.Contains("SIGID") Then
                        Dim sigid As Int64 = Convert.ToInt64(Row("SIGID"))
                        rowReturned = helper.GetDrugDetailsBySig(sigid)
                    Else
                        rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                    End If
            End Select

            Dim RoutesList As New List(Of String)
            Using objPrescriptioLayer As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                RoutesList = objPrescriptioLayer.GetDrugRoutes(mpid)
            End Using

            If rowReturned IsNot Nothing Then
                RaiseEvent DrugListClicked(rowReturned, RoutesList)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                     gloAuditTrail.ActivityCategory.CreatePrescription,
                                                     gloAuditTrail.ActivityType.Add,
                                                     ex.ToString(),
                                                     gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvMain_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseDoubleClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Return
            End If

            If e.Node IsNot Nothing AndAlso e.Node.Tag IsNot Nothing AndAlso TypeOf e.Node.Tag Is DataRow Then
                Me.LoadDrugList(DirectCast(e.Node.Tag, DataRow))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                     gloAuditTrail.ActivityCategory.CreatePrescription,
                                                     gloAuditTrail.ActivityType.Add,
                                                     ex.ToString(),
                                                     gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub AddDrugToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddDrugToolStripMenuItem.Click
        Try
            RaiseEvent cntListmenuStripClick(False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub AddProviderFavoriteDrugToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddProviderFavoriteDrugToolStripMenuItem.Click
        Try
            RaiseEvent cntListmenuStripClick(True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub AddtoPlanMedMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddtoPlanMedMenuItem.Click
        Try
            Try
                Dim helper As New PrescriptionBusinessLayer
                Dim rowReturned As DataRow = Nothing
                Dim Row As DataRow = Nothing
                If trvMain IsNot Nothing Then
                    If trvMain.SelectedNode.Tag IsNot Nothing Then
                        Row = trvMain.SelectedNode.Tag
                    End If
                End If

                If Row.Table.Columns.Contains("TherapeuticConceptTreeID") Then
                    Return
                End If

                Dim mpid As Int32 = Convert.ToInt32(Row("MPID"))
                Dim ndc As String = Convert.ToString(Row("NDCCode"))

                Select Case enmselectedbutton
                    Case Selectedbutton.AllDrugs, Selectedbutton.PracticeFavorites, Selectedbutton.FrequentlyUsed, Selectedbutton.ProviderFrequentlyUsed, Selectedbutton.ClassfiedDrugs, Selectedbutton.PlannedDrugs
                        rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                        If IsNothing(rowReturned) Then
                            Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL)
                                mpid = oDIBHelper.GetMarketedProductId(ndc)
                            End Using
                            rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                        End If
                    Case Selectedbutton.ProviderDrugs
                        If Row.Table.Columns.Contains("SIGID") Then
                            Dim sigid As Int64 = Convert.ToInt64(Row("SIGID"))
                            rowReturned = helper.GetDrugDetailsBySig(sigid)
                        Else
                            rowReturned = helper.GetDrugDetailsByID(mpid, ndc)
                        End If
                End Select

                If rowReturned IsNot Nothing Then
                    ''RaiseEvent DrugListClicked(rowReturned)
                    RaiseEvent cntListmenuStripAddtoPlannedMedClick(rowReturned, txtSearch.Text, enmselectedbutton)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                         gloAuditTrail.ActivityCategory.CreatePrescription,
                                                         gloAuditTrail.ActivityType.Add,
                                                         ex.ToString(),
                                                         gloAuditTrail.ActivityOutCome.Failure)
            End Try



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub gloDrugListRevised_trvMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvMain.MouseDown
        Try
            If sCallingform <> "POT" Then
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    If enmselectedbutton = Selectedbutton.ProviderDrugs Then
                        cntListmenuStrip.Items(1).Visible = Me.AllowDrugsConfig
                    Else
                        cntListmenuStrip.Items(1).Visible = False
                    End If
                    If enmselectedbutton = Selectedbutton.PlannedDrugs Or trvMain.Nodes(0).Nodes.Count <= 0 Then
                        cntListmenuStrip.Items(2).Visible = False
                    Else
                        cntListmenuStrip.Items(2).Visible = True
                    End If
                End If
            Else
                cntListmenuStrip.Items(0).Visible = False
                cntListmenuStrip.Items(1).Visible = False
                cntListmenuStrip.Items(2).Visible = False
            End If
           
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvMain_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvMain.AfterSelect
        Try
            If enmselectedbutton = Selectedbutton.ClassfiedDrugs Then
                Dim treeNode As TreeNode = DirectCast(trvMain.SelectedNode, TreeNode)

                If treeNode.ImageIndex <> 1 And treeNode.ImageIndex <> 6 Then
                    If treeNode.Tag IsNot Nothing AndAlso TypeOf (treeNode.Tag) Is DataRow Then
                        Dim dRow As DataRow = DirectCast(treeNode.Tag, DataRow)

                        If treeNode.IsExpanded Then
                            treeNode.Collapse()
                            Return
                        Else
                            Me.LoadDrugList(treeNode)
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub LoadDrugList(rootNode As TreeNode)
        Dim dtDrugList As DataTable = Nothing
        Try
            Cursor = Cursors.WaitCursor
            Dim helper As New PrescriptionBusinessLayer

            Dim drugNode As TreeNode = Nothing
            Dim key As Long = 0
            Dim value As String = ""
            Dim isNarcotic As Int16?
            Dim dataRow As DataRow = DirectCast(rootNode.Tag, DataRow)

            dtDrugList = helper.GetClassifiedDrugs(Convert.ToInt32(dataRow("TherapeuticConceptTreeID")))

            If dtDrugList IsNot Nothing AndAlso dtDrugList.Rows.Count > 0 Then
                For Each dr As DataRow In dtDrugList.Rows
                    value = Convert.ToString(dr("ConceptName"))
                    drugNode = New TreeNode(value)
                    drugNode.Tag = dr
                    rootNode.Nodes.Add(drugNode)
                Next
            Else
                Dim mpids As List(Of Int32) = helper.GetClassifiedDrugsByMPID(Convert.ToInt32(dataRow("TherapeuticConceptTreeID")))

                If Not IsNothing(mpids) Then
                    If mpids.Count > 0 Then
                        Using dtMPIDs As New DataTable()
                            dtMPIDs.Columns.Add(New DataColumn("MPID", Type.[GetType]("System.Int32")))
                            For Each node As Int32 In mpids
                                Dim dRow As DataRow = dtMPIDs.NewRow()
                                dRow("MPID") = node
                                dtMPIDs.Rows.Add(dRow)
                            Next
                            dtDrugList = helper.GetDrugDetails(dtMPIDs)
                        End Using

                        If Not IsNothing(dtDrugList) Then
                            For Each row As DataRow In dtDrugList.Rows
                                value = Convert.ToString(row("DrugName"))
                                isNarcotic = Convert.ToInt32(row("IsNarcotic"))
                                drugNode = New TreeNode(value)
                                drugNode.Tag = row

                                If isNarcotic.HasValue Then
                                    If isNarcotic > 0 Then
                                        drugNode.ForeColor = Color.Red
                                    Else
                                        drugNode.ForeColor = Color.Black
                                    End If
                                End If

                                drugNode.ImageIndex = 1
                                drugNode.SelectedImageIndex = 1

                                rootNode.Nodes.Add(drugNode)
                            Next
                        End If
                    End If
                End If
            End If
            rootNode.ExpandAll()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            Cursor = Cursors.Default
            If Not IsNothing(dtDrugList) Then
                dtDrugList.Dispose()
                dtDrugList = Nothing
            End If
        End Try
    End Sub

    Private Sub trvMain_BeforeCollapse(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvMain.BeforeCollapse
        If enmselectedbutton = Selectedbutton.ProviderDrugs Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtSearch_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If trvMain.Nodes.Count > 0 Then
                    If trvMain.Nodes(0).Nodes.Count > 0 Then
                        trvMain.SelectedNode = trvMain.Nodes(0).Nodes(0)
                        trvMain.Select()
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvMain_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles trvMain.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If trvMain.SelectedNode IsNot Nothing AndAlso trvMain.SelectedNode.Tag IsNot Nothing AndAlso TypeOf trvMain.SelectedNode.Tag Is DataRow Then
                    Me.LoadDrugList(DirectCast(trvMain.SelectedNode.Tag, DataRow))
                    txtSearch.Focus()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvMain_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvMain.NodeMouseClick
        Try
            If e.Button = Windows.Forms.MouseButtons.Right AndAlso e.Node IsNot Nothing Then
                trvMain.SelectedNode = e.Node
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub SaveSearchType()
        Try
            Using dbLayer As New PrescriptionBusinessLayer()
                dbLayer.SaveSearchTypeSetting(Me.ClinicID, Me.UserID, Me.enumSearchType)
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                     gloAuditTrail.ActivityCategory.CreatePrescription,
                                                     gloAuditTrail.ActivityType.Save,
                                                     ex.ToString(),
                                                     gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Dim FontBold As Font = New Font("Tahoma", 9, FontStyle.Bold)
    Dim FontRegular As Font = New Font("Tahoma", 9, FontStyle.Regular)

    Private Sub btnDrugStartContent_Click(sender As System.Object, e As System.EventArgs) Handles btnDrugStartContent.Click
        pnlSearchFilter.Visible = Not pnlSearchFilter.Visible
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbStartswith.CheckedChanged, rbContains.CheckedChanged

        Try
            If TypeOf (sender) Is RadioButton Then
                Dim rbSelected As RadioButton = DirectCast(sender, RadioButton)

                If rbSelected.Checked Then
                    rbSelected.Font = FontBold

                    If rbSelected.Name.Length > 0 Then
                        If rbSelected.Name = "rbStartswith" Then
                            enumSearchType = SearchType.StartsWith
                        ElseIf rbSelected.Name = "rbContains" Then
                            enumSearchType = SearchType.Contains
                        End If

                        If txtSearch.Text.Length > 0 Then
                            Me.SearchFunction(Me, New EventArgs())
                        End If

                        Me.SaveSearchType()
                    End If
                Else
                    rbSelected.Font = FontRegular
                End If

                rbSelected = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription,
                                                    gloAuditTrail.ActivityCategory.CreatePrescription,
                                                    gloAuditTrail.ActivityType.Save,
                                                    ex.ToString(),
                                                    gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
End Class
