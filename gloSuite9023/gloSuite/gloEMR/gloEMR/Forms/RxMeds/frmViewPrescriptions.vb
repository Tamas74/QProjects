Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary
Imports System.Linq
Imports System.IO
Imports gloEMRGeneralLibrary.gloGeneral

Public Class frmViewPrescriptions

    Private Property ProviderID As Long
    Private _patientInfo As DataRow = Nothing
    Public Property PatientInfo() As DataRow
        Get
            Return _patientInfo
        End Get
        Set(ByVal value As DataRow)
            _patientInfo = value
        End Set
    End Property

    Private _pharmacyInfo As DataRow = Nothing
    Public Property PharmacyInfo() As DataRow
        Get
            Return _pharmacyInfo
        End Get
        Set(ByVal value As DataRow)
            _pharmacyInfo = value
        End Set
    End Property

    Private _providerInfo As DataRow = Nothing
    Public Property ProviderInfo() As DataRow
        Get
            Return _providerInfo
        End Get
        Set(ByVal value As DataRow)
            _providerInfo = value
        End Set
    End Property

    Private sXML As String
    Public Property XMLData() As String
        Get
            Return sXML
        End Get
        Set(ByVal value As String)
            sXML = value
        End Set
    End Property

    Private nPrescriberID As Int64
    Public Property PrescriberID() As Int64
        Get
            Return nPrescriberID
        End Get
        Set(ByVal value As Int64)
            nPrescriberID = value
        End Set
    End Property


#Region "Constructors and Form Events"

    Private Sub New()
        InitializeComponent()
        dProviders = New Dictionary(Of Int64, Int64)
    End Sub

    Private Sub New(ByVal ProviderID As Int64, ByVal PrescriberID As Int64)
        Me.New()
        Me.ProviderID = ProviderID
        Me.PrescriberID = PrescriberID

        If PrescriberID <> 0 Then
            Me.chkCancelled.Checked = True
            pnlwbBrowser.Visible = chkCancelled.Checked
            Me.chkDateFilter.Checked = False
        End If

    End Sub

    Private Sub frmViewPrescriptions_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Me.Dispose()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmViewPrescriptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFrom.Value = DateTime.Now
        dtpToDate.Value = DateTime.Now
        SetFlexgridColumns()

        LoadPrescribers()
        cmbMethod.DataSource = Nothing
        cmbMethod.Items.Clear()
        cmbMethod.Items.Add("All")
        cmbMethod.Items.Add("Fax")
        cmbMethod.Items.Add("Print")
        cmbMethod.Items.Add("Phone")
        cmbMethod.Items.Add("Sample")
        cmbMethod.Items.Add("Handwritten")
        cmbMethod.Items.Add("eRx")
        cmbMethod.SelectedIndex = 0

        AddMenu()

    End Sub

    Private Sub frmViewPrescriptions_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        Try
            gloC1FlexStyle.Style(_Flex)

            AddHandler chkDateFilter.CheckedChanged, AddressOf chkDateFilter_CheckedChanged
            AddHandler cmbMethod.SelectedValueChanged, AddressOf cmbMethod_SelectedValueChanged
            AddHandler chkCancelled.CheckedChanged, AddressOf chkCancelled_CheckStateChanged

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Dim dProviders As Dictionary(Of Int64, Int64)

    Private Sub LoadProvider(ByVal dt As DataTable)
        Dim mynode As TreeNode = Nothing
        Dim selectedNode As TreeNode = Nothing

        Try
            If dt IsNot Nothing AndAlso dt.Rows.Count() > 0 Then
                trvPrescribers.BeginUpdate()
                trvPrescribers.Nodes.Clear()
                trvPrescribers.Nodes.Add("Prescribers")
                trvPrescribers.Nodes.Item(0).ImageIndex = 3
                trvPrescribers.Nodes.Item(0).SelectedImageIndex = 3

                For icnt As Int32 = 0 To dt.Rows.Count - 1
                    mynode = New TreeNode
                    mynode.Tag = dt.Rows(icnt)("nProviderID")
                    mynode.Text = dt.Rows(icnt)(1)
                    mynode.ImageIndex = 6
                    mynode.SelectedImageIndex = 6
                    trvPrescribers.Nodes.Item(0).Nodes.Add(mynode)

                    If Not dProviders.ContainsKey(Convert.ToString(dt.Rows(icnt)("PrescriberID"))) Then
                        dProviders.Add(Convert.ToString(dt.Rows(icnt)("PrescriberID")), Convert.ToInt64(dt.Rows(icnt)("nProviderID")))
                    End If
                Next
            End If

            trvPrescribers.ExpandAll()

            For i As Int16 = 0 To trvPrescribers.Nodes(0).GetNodeCount(True) - 1

                If dProviders.ContainsKey(Me.PrescriberID) Then
                    Me.ProviderID = dProviders(Me.PrescriberID)
                End If

                If Me.ProviderID = trvPrescribers.Nodes(0).Nodes(i).Tag Then
                    trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(i)
                    Exit For
                End If

            Next

            If IsNothing(trvPrescribers.SelectedNode) Then
                trvPrescribers.SelectedNode = trvPrescribers.Nodes.Item(0).Nodes.Item(0)
                Me.ProviderID = trvPrescribers.SelectedNode.Tag
            End If
            trvPrescribers.Focus()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            trvPrescribers.EndUpdate()
        End Try
    End Sub

#End Region

#Region "Events"

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub _Flex_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

#End Region

#Region "C1 Code"

    Private Sub SetFlexgridColumns()
        With _Flex
            .AllowDrop = True
            .AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            .Cols(0).Width = 30
            .ExtendLastCol = True
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = 21
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            Dim _Width As Single = .Width - 10

            .Visible = True
            .BringToFront()
            .Rows.Count = 1
            .SetData(0, 0, "Date")
            .SetData(0, 1, "Patient Name")
            .SetData(0, 2, "Drug")
            .SetData(0, 3, "Prescriber")
            .SetData(0, 4, "Patient Directions")
            .SetData(0, 5, "Duration")
            .SetData(0, 6, "Quantity")
            .SetData(0, 7, "Refills")
            .SetData(0, 8, "Issue Method")
            .SetData(0, 9, "Status")
            .SetData(0, 10, "eRx Message")
            .SetData(0, 11, "User")
            .SetData(0, 12, "Allow Substitution")
            .SetData(0, 13, "Pharmacy")
            .SetData(0, 14, "Pharmacy Note")
            .SetData(0, 15, "Prescription Id")
            .SetData(0, 16, "IseRxed")
            .SetData(0, 17, "Cancelled Rx")
            .SetData(0, 18, "PatientId")
            .SetData(0, 18, "PharmacyId")
            .SetData(0, 18, "ProviderId")

            .Cols(0).Width = _Width * 0.08
            .Cols(1).Width = _Width * 0.1
            .Cols(2).Width = _Width * 0.2
            .Cols(3).Width = _Width * 0.12
            .Cols(4).Width = _Width * 0.09
            .Cols(5).Width = _Width * 0.07
            .Cols(6).Width = _Width * 0.05
            .Cols(7).Width = _Width * 0.095
            .Cols(8).Width = _Width * 0.1
            .Cols(9).Width = _Width * 0.0
            .Cols(10).Width = _Width * 0.08
            .Cols(11).Width = _Width * 0.14
            .Cols(12).Width = _Width * 0.2
            .Cols(13).Width = _Width * 0.1
            .Cols(14).Width = 0
            .Cols(15).Width = 0
            .Cols(16).Width = 0
            .Cols(17).Width = 0
            .Cols(18).Width = 0
            .Cols(19).Width = 0
            .Cols(20).Width = 0

            _Width = Nothing

            .Cols(0).TextAlign = TextAlignEnum.LeftCenter
            .Cols(1).TextAlign = TextAlignEnum.LeftCenter
            .Cols(2).TextAlign = TextAlignEnum.LeftCenter
            .Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .Cols(4).TextAlign = TextAlignEnum.LeftCenter
            .Cols(5).TextAlign = TextAlignEnum.LeftCenter
            .Cols(6).TextAlign = TextAlignEnum.LeftCenter
            .Cols(7).TextAlign = TextAlignEnum.LeftCenter
            .Cols(8).TextAlign = TextAlignEnum.LeftCenter
            .Cols(9).TextAlign = TextAlignEnum.LeftCenter
            .Cols(10).TextAlign = TextAlignEnum.LeftCenter
            .Cols(11).TextAlign = TextAlignEnum.LeftCenter
            .Cols(12).TextAlign = TextAlignEnum.LeftCenter
            .Cols(13).TextAlign = TextAlignEnum.LeftCenter
            .Cols(14).TextAlign = TextAlignEnum.LeftCenter
            .Cols(15).TextAlign = TextAlignEnum.LeftCenter
            .Cols(16).TextAlign = TextAlignEnum.LeftCenter
            .Cols(17).TextAlign = TextAlignEnum.LeftCenter
            .Cols(18).TextAlign = TextAlignEnum.LeftCenter
            .Cols(19).TextAlign = TextAlignEnum.LeftCenter
            .Cols(20).TextAlign = TextAlignEnum.LeftCenter

            .Cols(18).Visible = False
            .Cols(19).Visible = False
            .Cols(20).Visible = False
        End With

        For Each Column As C1.Win.C1FlexGrid.Column In _Flex.Cols
            Column.AllowEditing = False
        Next

    End Sub

#End Region

#Region "Load and Bind data"

    Public Function GetProviderPrescriptions(ByVal ProviderID As Int64, ByVal FromDate As Date, ByVal ToDate As Date, ByVal IsCancelled As Boolean) As DataTable
        Dim dtReturned As New DataTable()
        Dim dvFilter As New DataView()
        Dim strFilter As String = ""

        Try

            Using p As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                dtReturned = p.GetProviderPrescriptions(ProviderID, chkCancelled.Checked)
                dvFilter = dtReturned.DefaultView

                If (chkDateFilter.Checked) Then
                    strFilter = "dtPrescriptionDate >=  '" & dtpFrom.Value.Date & " 12:00:00 AM" & "' AND dtPrescriptionDate <= '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
                    If cmbMethod.Text <> "All" AndAlso cmbMethod.Text <> "" Then
                        strFilter = strFilter & " AND sMethod = '" & cmbMethod.Text & "' "
                    End If
                Else
                    If cmbMethod.Text <> "All" AndAlso cmbMethod.Text <> "" Then
                        strFilter = " sMethod = '" & cmbMethod.Text & "' "
                    End If
                End If

                dvFilter.RowFilter = strFilter
            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return dvFilter.ToTable()
    End Function

    Private Sub LoadPrescribers()
        Dim dtProviders As New DataTable()
        Try
            Using p As New PrescriptionBusinessLayer()
                dtProviders = p.GetPrescriberList()
            End Using

            LoadProvider(dtProviders)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub RefreshData()
        Dim oDatatable As New DataTable
        Try
            If trvPrescribers.SelectedNode IsNot Nothing Then
                SetFlexgridColumns()
                oDatatable = GetProviderPrescriptions(trvPrescribers.SelectedNode.Tag, dtpFrom.Value, dtpToDate.Value, chkCancelled.Checked)
                BindData(oDatatable)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

    Private Sub BindData(ByVal oDatatable As DataTable)
        Try
            If Not IsNothing(oDatatable) Then
                If oDatatable.Rows.Count > 0 Then
                    _Flex.Rows.Count = 1
                    For i As Integer = 0 To oDatatable.Rows.Count - 1
                        With _Flex
                            .Rows.Add()

                            .SetData(i + 1, 0, CType(oDatatable.Rows(i)("dtPrescriptionDate"), Date).ToShortDateString())
                            .SetData(i + 1, 1, oDatatable.Rows(i)("sPatientName"))
                            .SetData(i + 1, 2, oDatatable.Rows(i)("sMedication"))
                            .SetData(i + 1, 3, oDatatable.Rows(i)("Prescriber"))
                            .SetData(i + 1, 4, oDatatable.Rows(i)("sFrequency"))
                            .SetData(i + 1, 5, oDatatable.Rows(i)("sDuration"))
                            .SetData(i + 1, 6, oDatatable.Rows(i)("sAmount"))
                            .SetData(i + 1, 7, oDatatable.Rows(i)("sRefills"))
                            .SetData(i + 1, 8, oDatatable.Rows(i)("sMethod"))
                            .SetData(i + 1, 9, oDatatable.Rows(i)("eRxStatus"))
                            .SetData(i + 1, 10, oDatatable.Rows(i)("eRxStatusMessage"))
                            .SetData(i + 1, 11, oDatatable.Rows(i)("UserName"))
                            .SetData(i + 1, 12, oDatatable.Rows(i)("AllowSubstitue"))
                            .SetData(i + 1, 13, oDatatable.Rows(i)("sName"))
                            Dim str As String = oDatatable.Rows(i)("sNotes")
                            .SetData(i + 1, 14, str.Replace(Environment.NewLine, " "))
                            .SetData(i + 1, 15, oDatatable.Rows(i)("PrescriptionID"))
                            .SetData(i + 1, 16, oDatatable.Rows(i)("IseRxed"))
                            .SetUserData(i + 1, 17, oDatatable.Rows(i)("IsCancelled"))
                            .SetData(i + 1, 18, oDatatable.Rows(i)("PatientID"))
                            .SetData(i + 1, 19, oDatatable.Rows(i)("PharmacyId"))
                            .SetData(i + 1, 20, oDatatable.Rows(i)("ProviderId"))
                        End With
                    Next
                Else
                    _Flex.Rows.Count = 1
                End If
            Else
                _Flex.Rows.Count = 1
            End If

            LoadDetails()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            oDatatable = Nothing
        Finally
            oDatatable = Nothing
        End Try
    End Sub

#End Region

#Region "Filter Events"

    Private Sub dtpFrom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFrom.TextChanged
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub dtptoDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub chkDateFilter_CheckedChanged(sender As System.Object, e As System.EventArgs) 'Handles chkDateFilter.CheckedChanged
        Try
            dtpFrom.Enabled = chkDateFilter.Checked
            dtpToDate.Enabled = chkDateFilter.Checked
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub chkCancelled_CheckStateChanged(sender As System.Object, e As System.EventArgs) 'Handles chkCancelled.CheckStateChanged
        Try
            pnlwbBrowser.Visible = chkCancelled.Checked
            Me.RefreshData()

            If mdlGeneral.IsCancelRxEnabledForProvider Then
                If cntListmenuStrip.Items.OfType(Of ToolStripItem).Any(Function(p) p.Tag = "Cancel Prescription") Then
                    Dim mnuCancelPrescription As ToolStripItem = cntListmenuStrip.Items.OfType(Of ToolStripItem).FirstOrDefault(Function(p) p.Tag = "Cancel Prescription")

                    If mnuCancelPrescription IsNot Nothing Then
                        mnuCancelPrescription.Visible = Not chkCancelled.Checked
                        mnuCancelPrescription = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub cmbMethod_SelectedValueChanged(sender As System.Object, e As System.EventArgs) 'Handles cmbMethod.SelectedValueChanged
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region

    Private Sub trvPrescribers_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvPrescribers.AfterSelect
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxChangeRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub AddMenu()
        Dim toolStripItem As ToolStripMenuItem = Nothing

        Try
            If mdlGeneral.IsCancelRxEnabledForProvider Then
                cntListmenuStrip.ImageList = ImgLstFlex

                toolStripItem = New ToolStripMenuItem
                toolStripItem.Text = "Cancel Prescription"
                toolStripItem.Tag = "Cancel Prescription"
                toolStripItem.ForeColor = Color.FromArgb(31, 73, 125)
                toolStripItem.ImageIndex = 1
                toolStripItem.Visible = Not chkCancelled.Checked

                cntListmenuStrip.Items.Add(toolStripItem)
                'AddHandler toolStripItem.Click, AddressOf CanceleRx_Click

                Dim item1 As New System.Windows.Forms.ToolStripMenuItem()
                item1.Text = "Cancel Prescription"
                item1.Tag = "C"
                item1.ForeColor = Color.FromArgb(31, 73, 125)
                item1.Image = cntListmenuStrip.ImageList.Images(1)

                AddHandler item1.Click, AddressOf CanceleRx_Click

                toolStripItem.DropDownItems.Add(item1)

                Dim item2 As New System.Windows.Forms.ToolStripMenuItem()
                item2.Text = "Discontinue Prescription"
                item2.Tag = "D"
                item2.ForeColor = Color.FromArgb(31, 73, 125)
                item2.Image = cntListmenuStrip.ImageList.Images(1)

                AddHandler item2.Click, AddressOf CanceleRx_Click

                toolStripItem.DropDownItems.Add(item2)
                toolStripItem = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Get_PatientDetails(ByVal PatientId As Int64)
        Try
            Using dtPatient As DataTable = GetPatientInfo(PatientId)
                If Not IsNothing(dtPatient) AndAlso dtPatient.Rows.Count > 0 Then
                    _patientInfo = dtPatient.Rows(0)
                End If

            End Using
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub



    Private Sub CanceleRx_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim RetMsg As String = Nothing
            Dim ss_helper As gloSureScript.gloSurescriptsHelper = Nothing
            ss_helper = New gloSureScript.gloSurescriptsHelper(gstrSurescriptServiceURL)

            Dim flag As String = CType(sender, ToolStripMenuItem).Tag

            gloSureScript.gloSurescriptGeneral.ServerName = gstrSQLServerName
            gloSureScript.gloSurescriptGeneral.DatabaseName = gstrDatabaseName
            gloSureScript.gloSurescriptGeneral.sUserName = gstrSQLUserEMR
            gloSureScript.gloSurescriptGeneral.sPassword = gstrSQLPasswordEMR
            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = gblnSQLAuthentication

            Dim sPrescriptionNo As String = _Flex.GetData(_Flex.RowSel, 15)
            Dim sDrugName As String = _Flex.GetData(_Flex.RowSel, 2)

            Dim PatientId As Int64 = Convert.ToInt64(_Flex.GetData(_Flex.RowSel, 18))
            Dim PharmacyID As Int64 = Convert.ToInt64(_Flex.GetData(_Flex.RowSel, 19))
            Dim ProviderID As Int64 = Convert.ToInt64(_Flex.GetData(_Flex.RowSel, 20))

            Get_PatientDetails(PatientId)

            Dim dtMessages As DataTable = Nothing

            Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                dtMessages = helper.GeteRxDetailsForCancelRx(sPrescriptionNo)

                Using dtProvider As DataTable = helper.GetPatientProviderDetails(ProviderID)
                    If Not IsNothing(dtProvider) AndAlso dtProvider.Rows.Count > 0 Then
                        _providerInfo = dtProvider.Rows(0)
                    End If

                End Using

                Using dtPharmacy As DataTable = helper.GetPharmacyDetails(PharmacyID)
                    If dtPharmacy IsNot Nothing Then
                        If dtPharmacy.Rows.Count > 0 Then
                            _pharmacyInfo = dtPharmacy.Rows(0)
                        End If
                    End If
                End Using
            End Using

            Dim sMsg As String = ""

            If flag = "C" Then
                sMsg = "You are about to cancel this prescription : " & sDrugName & System.Environment.NewLine() & "Are you sure ? "
                Label35.Text = "Sending Rx cancellation request..."
            ElseIf flag = "D" Then
                sMsg = "You are about to discontinue this prescription : " & sDrugName & System.Environment.NewLine() & "Are you sure ? "
                Label35.Text = "Sending Rx discontinue request..."
            End If

            If MessageBox.Show(sMsg, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            pnlCancelProgress.Visible = True
            pnlCancelProgress.BringToFront()
            Application.DoEvents()

            Dim PrescriptionId As Int64 = 0
            Int64.TryParse(sPrescriptionNo, PrescriptionId)

            Dim medPrescribed As gloGlobal.Common.ServiceObjectBase.MedPrescribed = Nothing
            Using helper As New gloEMRGeneralLibrary.PrescriptionBusinessLayer()
                medPrescribed = helper.FillMedPrescribed(PrescriptionId)
            End Using

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.CancelOperation, "Sending CancelRx request", PatientId, PrescriptionId, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)

            If dtMessages IsNot Nothing Then
                If dtMessages.Rows.Count > 0 Then
                    RetMsg = ss_helper.SendCancelRxMessage(sPrescriptionNo, medPrescribed, _patientInfo, dtMessages, flag, globalSecurity.gstrLoginName)
                Else
                    RetMsg = ss_helper.SendCancelRxMessage(sPrescriptionNo, medPrescribed, _patientInfo, _providerInfo, _pharmacyInfo, flag, globalSecurity.gstrLoginName)
                End If
            Else
                RetMsg = ss_helper.SendCancelRxMessage(sPrescriptionNo, medPrescribed, _patientInfo, _providerInfo, _pharmacyInfo, flag, globalSecurity.gstrLoginName)
            End If

            pnlCancelProgress.Visible = False

            If Not String.IsNullOrEmpty(RetMsg) Then
                MessageBox.Show(RetMsg, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Using p As New gloSureScript.gloSureScriptDBLayer()
                    If flag = "C" Then
                        p.UpdateMedicationStatus(PatientId, sPrescriptionNo, "Cancelled")
                    ElseIf flag = "D" Then
                        p.UpdateMedicationStatus(PatientId, sPrescriptionNo, "Discontinued")
                    End If
                End Using

                RefreshData()
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            pnlCancelProgress.Visible = False
        End Try
    End Sub

    Private Sub cntListmenuStrip_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cntListmenuStrip.Opening
        Try
            If _Flex.RowSel <= 0 Then
                e.Cancel = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub _Flex_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles _Flex.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim _nRow As Integer = _Flex.HitTest(e.X, e.Y).Row
                If _nRow >= 0 Then
                    _Flex.Row = _nRow
                Else
                    _Flex.RowSel = 0
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub _Flex_SelChange(sender As System.Object, e As System.EventArgs) Handles _Flex.SelChange
        LoadDetails()
    End Sub

    Private Sub LoadDetails()
        Dim dtCancelResponses As DataTable = Nothing
        Dim sPrescriberOrderNumber As String = ""
        Dim nPrescriberOrderNumber As Int64 = 0
        Dim dRow As DataRow = Nothing
        Dim sMessageID As String = ""

        Dim sPatientID As String = ""
        Dim nPatientID As Int64 = 0
        Try
            Me.XMLData = String.Empty

            If _Flex.RowSel > 0 AndAlso _Flex.Cols.Count = 21 Then
                If Not IsNothing(_Flex.GetData(_Flex.RowSel, 15)) Then
                    sPrescriberOrderNumber = _Flex.GetData(_Flex.RowSel, 15)
                Else
                    sPrescriberOrderNumber = ""
                End If

                If Not IsNothing(_Flex.GetData(_Flex.RowSel, 18)) Then
                    sPatientID = _Flex.GetData(_Flex.RowSel, 18)
                Else
                    sPatientID = ""
                End If

                Int64.TryParse(sPatientID, nPatientID)

                If Int64.TryParse(sPrescriberOrderNumber, nPrescriberOrderNumber) Then
                    Using p As New PrescriptionBusinessLayer()
                        dRow = p.GetRxMessageByID(nPrescriberOrderNumber, "CancelRxResponse")

                        If dRow IsNot Nothing AndAlso dRow.Table.Rows.Count > 0 Then
                            Me.XMLData = dRow("FileXML")
                            sMessageID = dRow("nMessageID")
                        End If

                        If Not String.IsNullOrWhiteSpace(XMLData) Then
                            lblNoResponses.Visible = False
                            requestViewer.Visible = True
                            XMLtoHTMLFileLoad(XMLData)

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CancelRx, gloAuditTrail.ActivityType.View, "CancelRx response viewed", nPatientID, nPrescriberOrderNumber, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)

                            If Not String.IsNullOrWhiteSpace(sMessageID) Then
                                Using DB As New gloEMRGeneralLibrary.SurescriptsBusinessLayer()
                                    DB.UpdateRxMessageStatus(sMessageID, "Viewed", "CancelRxResponse")
                                End Using
                            End If
                        End If
                    End Using
                End If
            End If

            If String.IsNullOrWhiteSpace(XMLData) Then
                lblNoResponses.Visible = True
                requestViewer.Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub XMLtoHTMLFileLoad(ByVal strContent As String)

        Dim _firstTransformation As String = ""
        Dim _secondTransforamtion As String = ""
        Dim UniqueFileName As String = gloGlobal.clsFileExtensions.GetUniqueDateString()
        Dim _strfileName1 As String = ""
        Dim oglointerface As New gloSureScript.gloSureScriptInterface

        Try

            If (Not String.IsNullOrEmpty(strContent)) Then
                _firstTransformation = oglointerface.Transform(strContent, System.Windows.Forms.Application.StartupPath & "\namespaceremoval.xsl")
                _secondTransforamtion = oglointerface.Transform(_firstTransformation, System.Windows.Forms.Application.StartupPath & "\RxRequestSummary.xsl")
                _strfileName1 = gloSettings.FolderSettings.AppTempFolderPath & UniqueFileName & ".html"

                requestViewer.Navigate("about:blank")

                If _strfileName1 <> "" Then

                    DeleteHTMLFile(requestViewer.Tag)

                    File.WriteAllText(_strfileName1, _secondTransforamtion)

                    requestViewer.Navigate(_strfileName1)
                    requestViewer.Tag = _strfileName1
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oglointerface) Then
                oglointerface.Dispose()
                oglointerface = Nothing
            End If
        End Try

    End Sub

    Private Sub DeleteHTMLFile(ByVal filetodelete As String)
        Try
            Dim sFileToDelete As String = Convert.ToString(filetodelete)
            If Not String.IsNullOrEmpty(sFileToDelete) Then
                If File.Exists(sFileToDelete) Then
                    File.Delete(sFileToDelete)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RxRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            Me.RefreshData()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.RefillRequest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class