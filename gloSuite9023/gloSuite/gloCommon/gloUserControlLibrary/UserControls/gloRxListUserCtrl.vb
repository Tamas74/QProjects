Imports gloEMRGeneralLibrary.gloEMRActors
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports gloEMRGeneralLibrary.gloGeneral
Imports gloEMRGeneralLibrary.gloEMRMedication
Imports System.Runtime.InteropServices

Public Class gloRxListUserCtrl

    <DllImport("user32.dll")> _
    Private Shared Function LockWindowUpdate(hWnd As IntPtr) As Boolean
    End Function


    'Public Event CheckDrugInteraction(ByVal DDID As Int64)
    Public Event cntListmenuStripClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Private _ValidDrug As Boolean
    Public ReadOnly Property ValidDrug() As Boolean
        Get
            Return _ValidDrug
        End Get

    End Property

    Enum Selectedbutton
        AllDrugs = 11
        PracticeFavorites = 12 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"
        FrequentlyUsed = 13
        ProviderFrequentlyUsed = 20
        ProviderDrugs
        ClassfiedDrugs = 22 'added by chetan to get beta blocker on 18 june 2010
    End Enum

    Private VisitID As Long
    Public enmselectedbutton As Selectedbutton
    Private _C1FormularyGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Private _rtfFormularyDescription As RichTextBox
    Private _pnlFormularyProgress As Panel
    Public _RxHSenderId As String = ""
    Public _RxHFormularyID As String = ""
    Private _PatientID As Long
    Private _nLoginProviderID As Long
    Private _IsRxC1FlexClick As Boolean
    'Developer: Pradeep()
    'Date:12/22/2011
    'Bug ID/PRD Name/Salesforce Case: 17290
    'Reason:shared variable replaced by private
    Public Property IsRxC1FlexClick() As String
        Get
            Return _IsRxC1FlexClick
        End Get
        Set(ByVal value As String)
            _IsRxC1FlexClick = value
        End Set
    End Property


    Public Sub New(ByVal PatientID As Long, ByRef objRxBusinessLayer As RxBusinesslayer, ByVal rxDrugSelectedBtn As Int16, ByRef objMedBusinessLayer As MedicationBusinessLayer, ByVal nLoginProviderID As Long, ByVal bAllowAddDrugs As Boolean, Optional ByVal RxHSenderID As String = "", Optional ByVal RxHFormularyID As String = "", Optional ByRef C1FormularyGrid As C1.Win.C1FlexGrid.C1FlexGrid = Nothing, Optional ByRef rtfFormularyDesciption As RichTextBox = Nothing, Optional ByRef pnlFormularyProgress As Panel = Nothing)
        MyBase.New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
        _nLoginProviderID = nLoginProviderID
        MyBase.trvList.Controls.Clear()
        SetButtonStyle()
        ' Add any initialization after the InitializeComponent() call.

        _C1FormularyGrid = C1FormularyGrid
        _rtfFormularyDescription = rtfFormularyDesciption
        _pnlFormularyProgress = pnlFormularyProgress
        _RxHSenderId = RxHSenderID
        _RxHFormularyID = RxHFormularyID
        _RxBusinessLayer = objRxBusinessLayer

        _MedBusinessLayer = objMedBusinessLayer

        Dim objsender As Object = Nothing
        Dim e As System.EventArgs = Nothing
        'select the Drug button for which the user want to be on top, added in the setting_new  form othersettings tab 
        'If rxDrugSelectedBtn <> 0 Then
        If rxDrugSelectedBtn = 13 Then '0 is frequently used drug in MDI general
            enmselectedbutton = Selectedbutton.FrequentlyUsed
            Call btnFreqDrugs_Click(objsender, e)
        ElseIf rxDrugSelectedBtn = 11 Then '1 is All Drugs in MDI general
            enmselectedbutton = Selectedbutton.AllDrugs
            Call btnAllDrugs_Click(objsender, e)
        ElseIf rxDrugSelectedBtn = 12 Then '2 is Practice Favorites in MDI general
            enmselectedbutton = Selectedbutton.PracticeFavorites 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"
            Call btnClinicalDrugs_Click(objsender, e)
        Else '3 is for Provider specific drug in MDI general
            enmselectedbutton = Selectedbutton.ProviderDrugs
            Call btnProvider_Click(objsender, e)
        End If
        'Else 'if nothing is selected then by default keep the selected button as frequently used drug 
        'enmselectedbutton = Selectedbutton.FrequentlyUsed
        'Call btnFreqDrugs_Click(objsender, e)
        'End If
        AddMenu(bAllowAddDrugs) '' Resolved Bug #81223
        AddEventHandlers()

    End Sub

    Private _RxBusinessLayer As RxBusinesslayer
    Private _MedBusinessLayer As MedicationBusinessLayer

    Public Property RxBusinessLayerObject() As RxBusinesslayer
        Get
            Return _RxBusinessLayer
        End Get
        Set(ByVal value As RxBusinesslayer)
            _RxBusinessLayer = value
        End Set
    End Property


    Public Property ObjMedBusinessLayer() As MedicationBusinessLayer
        Get
            Return _MedBusinessLayer
        End Get
        Set(ByVal value As MedicationBusinessLayer)
            _MedBusinessLayer = value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub gloRxListUserCtrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If enmselectedbutton = Selectedbutton.FrequentlyUsed Then
            '    btnFreqDrugs.Dock = DockStyle.Top
            '    btnAllDrugs.Dock = DockStyle.Bottom
            '    btnClinicalDrugs.Dock = DockStyle.Bottom
            '    btnProvider.Dock = DockStyle.Bottom

            'ElseIf enmselectedbutton = Selectedbutton.AllDrugs Then

            '    btnAllDrugs.Dock = DockStyle.Top
            '    btnFreqDrugs.Dock = DockStyle.Bottom
            '    btnClinicalDrugs.Dock = DockStyle.Bottom
            '    btnProvider.Dock = DockStyle.Bottom

            'ElseIf enmselectedbutton = Selectedbutton.ClinicalDrugs Then

            '    btnClinicalDrugs.Dock = DockStyle.Top
            '    btnFreqDrugs.Dock = DockStyle.Bottom
            '    btnAllDrugs.Dock = DockStyle.Bottom
            '    btnProvider.Dock = DockStyle.Bottom

            'Else 'if it is provider  drug enum

            '    btnProvider.Dock = DockStyle.Top
            '    btnFreqDrugs.Dock = DockStyle.Bottom
            '    btnAllDrugs.Dock = DockStyle.Bottom
            '    btnClinicalDrugs.Dock = DockStyle.Bottom

            'End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub AddEventHandlers()
        'AddHandler btnAllDrugs.Click, AddressOf ButtonClick
        'AddHandler btnClinicalDrugs.Click, AddressOf ButtonClick ''''''clinical drugs are now changed to Practice Favorites
        'AddHandler btnFreqDrugs.Click, AddressOf ButtonClick
        'AddHandler btnProvider.Click, AddressOf ButtonClick
        'AddHandler btnProvFreqDrugs.Click, AddressOf ButtonClick '''''bug 5246
    End Sub


    Private Sub SetButtonStyle()
        Try
            With btnFreqDrugs
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 27
                '.Width = 200
                .Text = "Frequently Used Drugs"

            End With

            With btnClinicalDrugs
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 27
                '.Width = 200
                .Text = "Practice Favorites" 'change caption from Clinical drugs to Practice Favorites. email dated 30 sep 2009 "Issue 54 Information"

            End With
            With btnAllDrugs
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 27
                '.Width = 200
                .Text = "All Drugs"
            End With
            With btnProvider
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 26
                '.Width = 200
                .Text = "Provider Favorites"
            End With
            With btnProvFreqDrugs '''''bug 5246
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 26
                '.Width = 200
                .Text = "Provider Frequently Used Drugs"
            End With

            With btnClassifiedDrugs  '''''bug 5246
                .Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                .BackColor = Color.FromArgb(207, 224, 248)
                .Dock = DockStyle.Fill
                .FlatStyle = FlatStyle.Flat
                .ForeColor = Color.FromArgb(31, 73, 125)
                .UseVisualStyleBackColor = False
                '.Height = 26
                '.Width = 200
                .Text = "Classified Drugs"
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Initializing Button Control"
            Throw objex
        End Try

    End Sub

    Public Sub AddMenu(ByRef bAllowAddDrugs As Boolean)
        Dim tlstripitem As ToolStripMenuItem
        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Add Drug"
        tlstripitem.Tag = 1
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = ImageList.Images(11)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf cntListmenuStrip_Click
        cntListmenuStrip.Items(0).Visible = bAllowAddDrugs  '' Resolved Bug #81223
        'tlstripitem.Dispose()
        tlstripitem = Nothing


        'CCHIT 08
        tlstripitem = New ToolStripMenuItem
        tlstripitem.Text = "Add Provider Favorite drugs"
        tlstripitem.Tag = 2
        tlstripitem.ForeColor = Color.FromArgb(31, 73, 125)
        tlstripitem.Image = ImageList.Images(10)
        cntListmenuStrip.Items.Add(tlstripitem)
        AddHandler tlstripitem.Click, AddressOf cntListmenuStrip_Click
        'tlstripitem.Dispose()
        tlstripitem = Nothing
        'CCHIT 08


    End Sub
    Public Sub cntListmenuStrip_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent cntListmenuStripClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Sub StripItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim objfrmPrescription As New 
        'Try
        '    objfrmMSTDrugs.Text = "Add New Drugs "
        '    objfrmMSTDrugs.ShowDialog()

        '    'If Mid(txtsearchDrug.Tag, 1, 1) <> Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1) Then
        '    If objfrmMSTDrugs.DrugID <> 0 Then
        '        If btnAllDrugs.Dock = DockStyle.Top Then
        '            AddDrugs(Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1))
        '        ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then

        '            If Not objfrmMSTDrugs.IsClinicalDrug Then
        '                btnClinicalDrugs.Dock = DockStyle.Bottom
        '                btnFrequentDrugs.Dock = DockStyle.Bottom
        '                btnAllDrugs.Dock = DockStyle.Top
        '                AddDrugs(1, Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1))
        '            Else
        '                AddDrugs(2, Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1))
        '            End If
        '        ElseIf btnFrequentDrugs.Dock = DockStyle.Top Then
        '            AddDrugs(1, Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1))
        '            btnClinicalDrugs.Dock = DockStyle.Bottom
        '            btnFrequentDrugs.Dock = DockStyle.Bottom
        '            btnAllDrugs.Dock = DockStyle.Top
        '        End If
        '        txtsearchDrug.Tag = Mid(Trim(objfrmMSTDrugs._DrugName), 1, 1)


        '        Dim drugnode As myTreeNode
        '        For Each drugnode In trDrugs.Nodes.Item(0).Nodes
        '            If drugnode.Key = objfrmMSTDrugs.DrugID Then
        '                trDrugs.SelectedNode = drugnode
        '                trDrugs.Select()
        '                Exit Sub
        '            End If
        '        Next
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub gloRxListUserCtrl_trvAfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles Me.trvAfterSelect
        Try
            If enmselectedbutton = Selectedbutton.ClassfiedDrugs Then
                Dim mynode As myTreeNode = CType(trvList.SelectedNode, myTreeNode)
                If mynode.ImageIndex <> 1 And mynode.ImageIndex <> 6 Then
                    Dim IsExpanded = mynode.IsExpanded
                    If (IsExpanded) Then
                        mynode.Collapse()
                        Return
                    End If
                    LoadDrugList(mynode)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            'bug list sent on saagar email - 15 Jan 2009
            arrstring = Split(strsplittext, ":")
            'bug list sent on saagar email - 15 Jan 2009
            Return arrstring(0)
        Else
            Return ""
        End If
    End Function

    Private Sub gloRxListUserCtrl_trvDoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.trvDoubleClick
        Dim oSelectedNode As TreeNode = trvList.SelectedNode
        Dim mynode As myTreeNode

        Try
            If IsRxC1FlexClick = True Then
                mynode = CType(trvList.SelectedNode, myTreeNode)
                If enmselectedbutton = Selectedbutton.ProviderDrugs Then
                    If Not mynode.Key = -1 Then
                        _RxBusinessLayer.FetchDrugDetailsForProviderFav(mynode.Key, mynode.NDCCode, mynode.SIGID)
                        _ValidDrug = True
                    Else
                        _ValidDrug = False
                    End If
                Else
                    If Not mynode.Key = -1 Then
                        If enmselectedbutton = Selectedbutton.ClassfiedDrugs Then
                            If mynode.ImageIndex = 1 Or mynode.ImageIndex = 6 Then
                                _RxBusinessLayer.FetchDrugDetails(mynode.NDCCode)
                                _ValidDrug = True
                            Else
                                _ValidDrug = False
                            End If
                        Else
                            _RxBusinessLayer.FetchDrugDetails(mynode.NDCCode)
                            _ValidDrug = True
                        End If
                    Else
                        _ValidDrug = False
                    End If
                End If
            Else
                Try
                    mynode = CType(trvList.SelectedNode, myTreeNode)
                    If Not mynode.Key = -1 Then

                        If enmselectedbutton = Selectedbutton.ClassfiedDrugs Then
                            If mynode.ImageIndex = 1 Or mynode.ImageIndex = 6 Then
                                If mynode.NDCCode <> "" Then
                                    _MedBusinessLayer.FetchDrugDetails(mynode.NDCCode)
                                    _ValidDrug = True
                                Else ''''if NDCCode is blank then return false
                                    _ValidDrug = False
                                End If
                            Else
                                _ValidDrug = False
                            End If

                        ElseIf enmselectedbutton = Selectedbutton.ProviderDrugs Then
                            If mynode.SIGID <> 0 Then
                                _MedBusinessLayer.FetchDrugDetails(mynode.NDCCode, mynode.SIGID)
                            Else
                                _MedBusinessLayer.FetchDrugDetails(mynode.NDCCode)
                            End If
                            _ValidDrug = True
                        Else
                            _MedBusinessLayer.FetchDrugDetails(mynode.NDCCode)
                            _ValidDrug = True
                        End If
                    Else
                        _ValidDrug = False
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.CreateMedication, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Dim objex As New gloUserControlExceptions
                    objex.ErrorMessage = "Error Adding Drugs"
                    Throw objex
                End Try
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlExceptions
            objex.ErrorMessage = "Error Adding Drugs"
            Throw objex
        Finally
            trvList.SelectedNode = oSelectedNode
            trvList.ExpandAll()
            mynode = Nothing
        End Try
    End Sub


    Private Sub LoadDrugList(Optional ByVal strsearch As String = "")

        Dim rootNode As New myTreeNode
        Dim dtDrugList As DataTable = Nothing
        Try

            Select Case enmselectedbutton
                Case Selectedbutton.AllDrugs
                    dtDrugList = _RxBusinessLayer.GetAllDrugs(strsearch)
                Case Selectedbutton.PracticeFavorites
                    dtDrugList = _RxBusinessLayer.GetPracticeFavoritesDrugs(strsearch)
                Case Selectedbutton.FrequentlyUsed
                    dtDrugList = _RxBusinessLayer.GetFrequentlyUsedDrugs(strsearch)
                Case Selectedbutton.ProviderFrequentlyUsed
                    Dim nProviderId As Int64
                    If _nLoginProviderID = 0 Then
                        nProviderId = _RxBusinessLayer.GetProviderID(_PatientID)
                    Else
                        nProviderId = _nLoginProviderID
                    End If
                    dtDrugList = _RxBusinessLayer.GetProviderFrequentlyUsedDrugs(nProviderId, strsearch)
                Case Selectedbutton.ProviderDrugs
                    dtDrugList = _RxBusinessLayer.FillProviderControls(_PatientID, _nLoginProviderID, strsearch)
                Case Selectedbutton.ClassfiedDrugs
                    dtDrugList = _RxBusinessLayer.GetClassifiedTree(0)

                    '''' dtDrugList = _RxBusinessLayer.GetAllDrugs(strsearch)

            End Select

            trvList.BeginUpdate()

            If trvList.GetNodeCount(False) > 0 Then
                trvList.Nodes.Item(0).Remove()
                rootNode.Text = "Drugs"
                rootNode.Key = -1
                rootNode.ImageIndex = 5
                rootNode.SelectedImageIndex = 5
            End If

            Dim drugNode As myTreeNode = Nothing
            Dim key As Long = 0
            Dim value As String = ""
            Dim isNarcotic As Int16?

            If Not IsNothing(dtDrugList) Then
                If dtDrugList.Rows.Count > 0 Then
                    If enmselectedbutton = Selectedbutton.ClassfiedDrugs Then
                        For Each dr As DataRow In dtDrugList.Rows
                            key = dr("TherapeuticConceptTreeID")
                            value = dr("ConceptName")
                            drugNode = New myTreeNode(key, value)
                            rootNode.Nodes.Add(drugNode)
                        Next
                    ElseIf enmselectedbutton = Selectedbutton.ProviderDrugs Then
                        Dim dtUniqdrug As DataTable = Nothing
                        Try
                            Using dtview = New DataView(dtDrugList)
                                dtUniqdrug = dtview.ToTable(True, "mpid", "NDCCode", "DrugName", "RxType", "IsNarcotic")
                                For Each row As DataRow In dtUniqdrug.Rows
                                    If Convert.ToString(row("RxType")) <> "" Then
                                        value = Convert.ToString(row("DrugName")) & " - " & Convert.ToString(row("RxType"))
                                    Else
                                        value = Convert.ToString(row("DrugName"))
                                    End If
                                    key = row("mpid") 'Convert.ToInt64(row("nDrugsID"))
                                    isNarcotic = Convert.ToInt16(row("IsNarcotic"))
                                    drugNode = New myTreeNode(key, value)
                                    drugNode.NDCCode = Convert.ToString(row("NDCCode"))
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
                                    LoadProviderDrugs(drugNode, dtDrugList)
                                   
                                Next
                            End Using
                        Catch ex As Exception
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        Finally
                            If Not IsNothing(dtUniqdrug) Then
                                dtUniqdrug.Dispose()
                                dtUniqdrug = Nothing
                            End If
                        End Try
                    Else
                        For Each row As DataRow In dtDrugList.Rows
                            If Convert.ToString(row("RxType")) <> "" Then
                                value = Convert.ToString(row("DrugName")) & " - " & Convert.ToString(row("RxType"))
                            Else
                                value = Convert.ToString(row("DrugName"))
                            End If
                            key = Convert.ToInt64(row("nDrugsID"))
                            isNarcotic = Convert.ToInt16(row("IsNarcotic"))
                            drugNode = New myTreeNode(key, value)
                            drugNode.NDCCode = row("NDCCode")
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
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex

        Finally
            trvList.EndUpdate()
            trvList.Nodes.Add(rootNode)
            If Not IsNothing(dtDrugList) Then
                dtDrugList.Dispose()
                dtDrugList = Nothing
            End If
        End Try

    End Sub

    Private Sub LoadProviderDrugs(rootNode As myTreeNode, ByVal dtdrugs As DataTable)
        Try

            Dim drugNode As myTreeNode = Nothing
            Dim key As Long = 0
            Dim value As String = ""
            Dim isNarcotic As Int16?
            Using dataview = New DataView(dtdrugs)
                If rootNode.Key <> 0 Then
                    dataview.RowFilter = "mpid = " & rootNode.Key
                Else
                    dataview.RowFilter = "NDCCode = '" & rootNode.NDCCode & "'"
                End If

                Dim row As DataRow = Nothing

                For Each rowView As DataRowView In dataview
                    row = rowView.Row
                    If dataview.Count = 1 Then
                        rootNode.SIGID = CType(row("SIGid"), Long)
                        Exit For
                    End If
                    If Not (Convert.ToString(row("Frequency")).Trim = "" And Convert.ToString(row("Refills")).Trim = "" And Convert.ToString(row("Duration")).Trim = "" And Convert.ToString(row("Amount")).Trim = "") Then ''And Convert.ToString(row("Dosage")).Trim = ""
                        isNarcotic = Convert.ToInt16(row("IsNarcotic"))
                        drugNode = New myTreeNode(Convert.ToString(row("Route")) & " " & Convert.ToString(row("Frequency")) & " " & Convert.ToString(row("Duration")) & " " & Convert.ToString(row("Refills")) & " " & Convert.ToString(row("Amount")), row("nDrugsID"), CType(row("Dosage"), String), CType(row("NDCCode"), String), CType(row("SIGid"), Long))
                        drugNode.NDCCode = Convert.ToString(row("NDCCode"))
                        drugNode.Key = row("mpid")
                        If isNarcotic.HasValue Then
                            If isNarcotic > 0 Then
                                drugNode.ForeColor = Color.Red
                                drugNode.ImageIndex = 1
                                drugNode.SelectedImageIndex = 1
                            Else
                                drugNode.ForeColor = Color.Black
                                drugNode.ImageIndex = 1
                                drugNode.SelectedImageIndex = 1
                            End If

                        End If
                        rootNode.Nodes.Add(drugNode)
                    End If
                Next
                rootNode.ExpandAll()
            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub LoadDrugList(rootNode As myTreeNode)
        Dim dtDrugList As DataTable = Nothing
        Try
            Cursor = Cursors.WaitCursor

            Dim drugNode As myTreeNode = Nothing
            Dim key As Long = 0
            Dim value As String = ""
            Dim isNarcotic As Int16?

            dtDrugList = _RxBusinessLayer.GetClassifiedTree(rootNode.Key)

            If Not IsNothing(dtDrugList) Then
                For Each dr As DataRow In dtDrugList.Rows
                    key = Convert.ToInt32(dr("TherapeuticConceptTreeID"))
                    value = Convert.ToString(dr("ConceptName"))
                    drugNode = New myTreeNode(key, value)
                    rootNode.Nodes.Add(drugNode)
                Next
                rootNode.ExpandAll()
            Else
                Dim mpids As List(Of Int32) = _RxBusinessLayer.GetClassifiedDrugsByMPID(rootNode.Key)

                If Not IsNothing(mpids) Then
                    If mpids.Count > 0 Then
                        Using dtTVP As New DataTable()
                            dtTVP.Columns.Add(New DataColumn("MPID", Type.[GetType]("System.Int32")))
                            For Each node As Int32 In mpids
                                Dim dRow As DataRow = dtTVP.NewRow()
                                dRow("MPID") = node
                                dtTVP.Rows.Add(dRow)
                            Next
                            dtDrugList = _RxBusinessLayer.Get_DrugDetails(dtTVP)
                        End Using

                        If Not IsNothing(dtDrugList) Then
                            For Each row As DataRow In dtDrugList.Rows
                                key = Convert.ToInt64(row("nDrugsID"))
                                value = Convert.ToString(row("sDrugName"))
                                isNarcotic = Convert.ToInt32(row("nIsNarcotics"))
                                drugNode = New myTreeNode(key, value)
                                drugNode.NDCCode = Convert.ToString(row("sNDCCode"))

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
                        rootNode.ExpandAll()
                    End If
                End If
            End If
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

    'Private Function GetFormularyStatus(ByVal DrugRxType As String, ByVal _RxHSenderId As String, ByVal _RxHFormularyID As String) As String
    '    Dim _strQuery As String = ""
    '    Try
    '        _strQuery = ""
    '    Catch ex As Exception

    '    End Try
    'End Function


    ''event handler for btnFreqDrugs, btnClinicalDrugs, btnAllDrugs 
    Public Sub ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim btn As Button
            btn = CType(sender, Button)
            Select Case btn.Name

                Case "btnFreqDrugs"
                    enmselectedbutton = Selectedbutton.FrequentlyUsed
                    'pnl_btnClinicalDrugs.Dock = DockStyle.Top
                    'pnl_btnAllDrugs.Dock = DockStyle.Bottom
                    'pnl_btnProvider.Dock = DockStyle.Bottom
                    btn.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)


                    Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                Case "btnClinicalDrugs" ''''''clinical drugs are now called as Practice Favorites
                    enmselectedbutton = Selectedbutton.PracticeFavorites
                    'pnl_btnFreqDrugs.Dock = DockStyle.Bottom
                    'pnl_btnAllDrugs.Dock = DockStyle.Bottom
                    'pnl_btnProvider.Dock = DockStyle.Bottom
                    btn.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                Case "btnAllDrugs"
                    enmselectedbutton = Selectedbutton.AllDrugs
                    'pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
                    'pnl_btnFreqDrugs.Dock = DockStyle.Bottom
                    'pnl_btnProvider.Dock = DockStyle.Bottom
                    btn.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                Case "btnProvider"
                    enmselectedbutton = Selectedbutton.ProviderDrugs
                    'pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
                    'pnl_btnFreqDrugs.Dock = DockStyle.Bottom
                    'pnl_btnAllDrugs.Dock = DockStyle.Bottom
                    btn.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                Case "btnProvFreqDrugs"
                    enmselectedbutton = Selectedbutton.ProviderFrequentlyUsed
                    'pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
                    'pnl_btnFreqDrugs.Dock = DockStyle.Bottom
                    'pnl_btnAllDrugs.Dock = DockStyle.Bottom
                    btn.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
                    btn.BackgroundImageLayout = ImageLayout.Stretch
                    btn.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

                    Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
                    Me.BackgroundImageLayout = ImageLayout.Stretch

                    Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
                    Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
                    Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            End Select
            'btn.BackgroundImage = Image.FromFile(Application.StartupPath & "\control\buttons\btnnormal.gif")


            LoadDrugList()
            btn.Dock = DockStyle.Top
            trvList.ExpandAll()
            trvList.Select()
            trvList.SelectedNode = trvList.Nodes(0)
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""
            Call ResetSearch()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        End Try

    End Sub



    Public Sub btnFreqDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFreqDrugs.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = False ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.FrequentlyUsed
            LoadDrugList()

            pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
            pnl_btnAllDrugs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnClassifiedDurgs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Top
            trvList.BringToFront()
            trvList.ExpandAll()
            trvList.Select()
            txtsearchDrug.Enabled = True
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""

            Call ResetSearch()


            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)


            Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
    End Sub


    Private Sub ResetSearch()
        txtsearchDrug.Text = ""
        txtsearchDrug.Focus()
    End Sub

    Public Sub btnClinicalDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClinicalDrugs.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = False ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.PracticeFavorites
            LoadDrugList()

            pnl_btnAllDrugs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnClassifiedDurgs.Dock = DockStyle.Bottom
            pnl_btnClinicalDrugs.Dock = DockStyle.Top 'clinical drugs are now called as Practice Favorites drugs
            trvList.ExpandAll()
            trvList.Select()
            txtsearchDrug.Enabled = True
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""

            Call ResetSearch()


            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub btnAllDrugs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAllDrugs.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = False ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.AllDrugs
            LoadDrugList()
            pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnClassifiedDurgs.Dock = DockStyle.Bottom
            pnl_btnAllDrugs.Dock = DockStyle.Top
            trvList.BringToFront()
            trvList.ExpandAll()
            trvList.Select()
            txtsearchDrug.Enabled = True
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""
            Call ResetSearch()


            Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
    End Sub

    Public Sub btnProvider_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvider.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = True ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.ProviderDrugs
            LoadDrugList()
            pnl_btnClinicalDrugs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnAllDrugs.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnClassifiedDurgs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Top
            trvList.BringToFront()
            trvList.ExpandAll()
            trvList.Select()
            txtsearchDrug.Enabled = True
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""
            Call ResetSearch()


            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
        End Try
    End Sub

    Public Sub btnProvFreqDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProvFreqDrugs.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = False ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.ProviderFrequentlyUsed
            LoadDrugList()

            pnl_btnAllDrugs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Bottom
            pnl_btnClinicalDrugs.Dock = DockStyle.Bottom 'clinical drugs are now called as Practice Favorites drugs
            pnl_btnClassifiedDurgs.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Top
            trvList.ExpandAll()
            trvList.Select()
            txtsearchDrug.Enabled = True
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""

            Call ResetSearch()


            Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtsearchDrug_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchDrug.TextChanged
        'Try
        '    If Len(Trim(txtsearchDrug.Text)) <= 1 Then
        '        If txtsearchDrug.Tag <> Trim(txtsearchDrug.Text) Then
        '            If btnAllDrugs.Dock = DockStyle.Top Then
        '                'AddDrugs(1, Trim(txtsearchDrug.Text))''commented by sagar for OOPs on 7 may 2007
        '                AddDrugs(Trim(txtsearchDrug.Text))
        '            ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
        '                'AddDrugs(2, Trim(txtsearchDrug.Text)) ''commented by sagar for OOPs on 7 may 2007
        '                AddDrugs(Trim(txtsearchDrug.Text))
        '            ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
        '                'AddDrugs(3, Trim(txtsearchDrug.Text)) ''commented by sagar for OOPs on 7 may 2007
        '                AddDrugs(Trim(txtsearchDrug.Text))
        '            End If
        '            'If Len(Trim(txtsearchDrug.Text)) = 1 Then
        '            txtsearchDrug.Tag = Trim(txtsearchDrug.Text)
        '            'End If
        '        End If
        '    End If

        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trvList.Nodes.Item(0).Nodes
        '        Dim str As String
        '        str = UCase(Splittext(mychildnode.Tag))
        '        If Mid(str, 1, Len(Trim(txtsearchDrug.Text))) = UCase(Trim(txtsearchDrug.Text)) Then
        '            trvList.SelectedNode = mychildnode
        '            txtsearchDrug.Focus()
        '            Exit Sub
        '        End If
        '    Next
        '    trvList.ExpandAll() ''code added vy sagar on 7 may 2007
        'Catch ex As Exception
        '    Dim objex As New gloUserControlLibrary.gloUserControlExceptions
        '    objex.ErrorMessage = ""
        '    Throw objex
        '    'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    trvList.EndUpdate()
        'End Try
    End Sub
    Private Sub txtsearchDrug_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchDrug.KeyPress

        Try
            If (e.KeyChar = ChrW(13)) Then
                trvList.Select()
            Else
                trvList.SelectedNode = trvList.Nodes.Item(0)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub
    Public Sub RefreshDrugList()
        Try
            Dim btn As Button = Nothing
            Select Case enmselectedbutton
                Case Selectedbutton.AllDrugs
                    btn = btnAllDrugs
                Case Selectedbutton.PracticeFavorites 'clinical drugs are now called as Practice Favorites
                    btn = btnClinicalDrugs
                Case Selectedbutton.FrequentlyUsed
                    btn = btnFreqDrugs
                Case Selectedbutton.ProviderDrugs
                    btn = btnProvider
                Case Selectedbutton.ProviderFrequentlyUsed
                    btn = btnProvFreqDrugs
            End Select
            Dim e As System.EventArgs = Nothing
            ButtonClick(btn, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        End Try

    End Sub

    Private Sub gloRxListUserCtrl_trvMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.trvMouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                If enmselectedbutton = Selectedbutton.AllDrugs Then
                    'hide the Add Provider Specific drug context menu strip
                    cntListmenuStrip.Items(1).Visible = False
                ElseIf enmselectedbutton = Selectedbutton.PracticeFavorites Then
                    'hide the Add Provider Specific drug context menu strip
                    cntListmenuStrip.Items(1).Visible = False
                ElseIf enmselectedbutton = Selectedbutton.FrequentlyUsed Then
                    'hide the Add Provider Specific drug context menu strip
                    cntListmenuStrip.Items(1).Visible = False
                ElseIf enmselectedbutton = Selectedbutton.ProviderFrequentlyUsed Then
                    'hide the Add Provider Specific drug context menu strip
                    cntListmenuStrip.Items(1).Visible = False
                Else 'this means provider specific button was click so enable the context menu again
                    cntListmenuStrip.Items(1).Visible = True
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub gloRxListUserCtrl_txtchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.txtchanged

        Try
            If Len(Trim(txtsearchDrug.Text)) <= 1 Then
                If txtsearchDrug.Tag <> Trim(txtsearchDrug.Text) Then
                    If pnl_btnAllDrugs.Dock = DockStyle.Top Then
                        LoadDrugList(Trim(txtsearchDrug.Text))
                    ElseIf pnl_btnClinicalDrugs.Dock = DockStyle.Top Then
                        LoadDrugList(Trim(txtsearchDrug.Text))
                    ElseIf pnl_btnFreqDrugs.Dock = DockStyle.Top Then
                        LoadDrugList(Trim(txtsearchDrug.Text))
                    ElseIf pnl_btnPrvFreqDrugs.Dock = DockStyle.Top Then
                        LoadDrugList(Trim(txtsearchDrug.Text))
                        'Code added By Rahul Patel on 18-11-2010
                        'For Resolving case no :GLO2010-0007153 i.e for Fetching  Provider Favoriate Drug list
                    ElseIf pnl_btnProvider.Dock = DockStyle.Top Then
                        LoadDrugList(Trim(txtsearchDrug.Text))
                        'End of coed Added by Rahul patel.
                    End If
                    'If Len(Trim(txtsearchDrug.Text)) = 1 Then
                    txtsearchDrug.Tag = Trim(txtsearchDrug.Text)
                    'End If
                End If
            End If

            Dim mychildnode As myTreeNode
            'child node collection
            For Each mychildnode In trvList.Nodes.Item(0).Nodes
                Dim str As String
                str = UCase(Splittext(mychildnode.Text))
                If Mid(str, 1, Len(Trim(txtsearchDrug.Text))) = UCase(Trim(txtsearchDrug.Text)) Then
                    If Not IsNothing(trvList.SelectedNode) Then
                        If Not IsNothing(trvList.SelectedNode.LastNode) Then
                            trvList.SelectedNode = trvList.SelectedNode.LastNode
                        End If
                    End If

                    trvList.SelectedNode = mychildnode
                    txtsearchDrug.Focus()
                    Exit Sub
                End If
            Next

            trvList.ExpandAll() ''code added vy sagar on 7 may 2007
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvList.EndUpdate()
        End Try

    End Sub
    'C


    Private Sub btnClassifiedDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClassifiedDrugs.Click
        Try
            clsgeneral.blnIsProviderSpecificDrugsBtnSelected = False ''''''''if provider specific btn is selected then make it true
            LockWindowUpdate(Me.Handle)
            enmselectedbutton = Selectedbutton.ClassfiedDrugs
            LoadDrugList()
            pnl_btnClassifiedDurgs.Dock = DockStyle.Top
            pnl_btnAllDrugs.Dock = DockStyle.Bottom
            pnl_btnFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnProvider.Dock = DockStyle.Bottom
            pnl_btnPrvFreqDrugs.Dock = DockStyle.Bottom
            pnl_btnClinicalDrugs.Dock = DockStyle.Bottom

            trvList.ExpandAll()
            'trvList.Select()
            txtsearchDrug.Enabled = False
            txtsearchDrug.Text = ""
            txtsearchDrug.Tag = ""

            Call ResetSearch()

            Me.btnClassifiedDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongOrange
            Me.btnClassifiedDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClassifiedDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnClinicalDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnClinicalDrugs.BackgroundImageLayout = ImageLayout.Stretch
            Me.btnClinicalDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)

            Me.btnFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnAllDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnAllDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnAllDrugs.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvider.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvider.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnProvider.BackgroundImageLayout = ImageLayout.Stretch

            Me.btnProvFreqDrugs.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Img_LongButton
            Me.btnProvFreqDrugs.BackColor = System.Drawing.Color.FromArgb(207, 224, 248)
            Me.btnProvFreqDrugs.BackgroundImageLayout = ImageLayout.Stretch




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            Throw objex
        Finally
            LockWindowUpdate(IntPtr.Zero)
            'MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnProvFreqDrugs.MouseHover, btnProvider.MouseHover, btnClinicalDrugs.MouseHover, btnClassifiedDrugs.MouseHover, btnAllDrugs.MouseHover, pnl_btnFreqDrugs.MouseLeave, btnFreqDrugs.MouseHover
        Try
            If sender IsNot Nothing Then
                Dim btn As Button = Nothing
                ' Dim btn As Button = DirectCast(sender, Button)
                Try
                    btn = DirectCast(sender, Button)
                Catch ex As Exception

                End Try

                If btn IsNot Nothing Then
                    Dim parentPanel As System.Windows.Forms.Panel = Nothing
                    If (IsNothing(btn.Parent) = False) Then
                        Try
                            parentPanel = DirectCast(btn.Parent, System.Windows.Forms.Panel)
                        Catch ex As Exception

                        End Try

                    End If
                    If (IsNothing(parentPanel) = False) Then
                        If parentPanel.Dock = DockStyle.Top Then
                            btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongYellow
                            btn.BackgroundImageLayout = ImageLayout.Stretch

                        Else
                            btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongYellow
                            btn.BackgroundImageLayout = ImageLayout.Stretch
                        End If
                    Else
                        btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongYellow
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    End If


                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return
        End Try

    End Sub

    Private Sub btn_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnProvFreqDrugs.MouseLeave, btnProvider.MouseLeave, btnClinicalDrugs.MouseLeave, btnClassifiedDrugs.MouseLeave, btnAllDrugs.MouseLeave, pnl_btnFreqDrugs.MouseHover, btnFreqDrugs.MouseLeave
        Try
            If sender IsNot Nothing Then
                ' Dim btn As Button = DirectCast(sender, Button)
                Dim btn As Button = Nothing
                Try
                    btn = DirectCast(sender, Button)
                Catch ex As Exception

                End Try

                If btn IsNot Nothing Then

                    ' If DirectCast(btn.Parent, System.Windows.Forms.Panel).Dock = DockStyle.Top Then
                    Dim parentPanel As System.Windows.Forms.Panel = Nothing
                    If (IsNothing(btn.Parent) = False) Then
                        Try
                            parentPanel = DirectCast(btn.Parent, System.Windows.Forms.Panel)
                        Catch ex As Exception

                        End Try

                    End If
                    If (IsNothing(parentPanel) = False) Then
                        If parentPanel.Dock = DockStyle.Top Then
                            btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongOrange
                            btn.BackgroundImageLayout = ImageLayout.Stretch
                        Else
                            btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongButton
                            btn.BackgroundImageLayout = ImageLayout.Stretch
                        End If
                    Else
                        btn.BackgroundImage = gloUserControlLibrary.My.Resources.Img_LongButton
                        btn.BackgroundImageLayout = ImageLayout.Stretch
                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return
        End Try
    End Sub

End Class

