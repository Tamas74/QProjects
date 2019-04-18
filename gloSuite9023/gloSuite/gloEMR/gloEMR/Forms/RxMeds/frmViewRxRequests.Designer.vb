<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewRxRequests
    Inherits System.Windows.Forms.Form



    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub


#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmViewRxRequests

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                ' Dispose managed resources.
                Try

                    If (IsNothing(dgRefillList) = False) Then
                        dgRefillList.TableStyles.Clear()
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dgRefillList)
                        dgRefillList.Dispose()
                        dgRefillList = Nothing
                    End If
                Catch ex As Exception

                End Try
                If Not (components Is Nothing) Then
                    components.Dispose()                   
                End If
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal PatientID As Long, ByVal PrescriberID As String) As frmViewRxRequests
        If frm Is Nothing Then
            frm = New frmViewRxRequests(PatientID, PrescriberID)
        End If

        Return frm
    End Function


    'Public Shared Function GetExistingInstance() As frmVWErrorMessage
    '    '_mu.WaitOne()
    '    Try

    '        If Not frm Is Nothing Then
    '            'Dim ecls As EventArgs
    '            'Dim obj As Object
    '            'obj = CType(Me, Object)
    '            'frm.frmVWErrorMessage_FormClosed(obj, ecls)
    '            frm.Close()
    '            ' Me.Finalize()
    '            frm = Nothing
    '        End If

    '        'If frm Is Nothing Then

    '        '    frm = New frmVWErrorMessage(PrescriberID)

    '        'End If
    '    Finally
    '        '_mu.ReleaseMutex()
    '    End Try
    '    Return frm
    'End Function


#End Region



    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewRxRequests))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnl_trv = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.trvPrescribers = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_Prescriber = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnl_toolstrip = New System.Windows.Forms.Panel()
        Me.lblSupplyText = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAproved = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDenied = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDeniedWithNewRxtoFollow = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.pnlwbBrowser = New System.Windows.Forms.Panel()
        Me.requestViewer = New System.Windows.Forms.WebBrowser()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.pnlProcessRequest = New System.Windows.Forms.Panel()
        Me.pnlApprove = New System.Windows.Forms.Panel()
        Me.pnlApproveDetails = New System.Windows.Forms.Panel()
        Me.pnlDataGrid = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgChangeRequests = New System.Windows.Forms.DataGridView()
        Me.colChecked = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colDrug = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDuration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDirections = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNotes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRefills = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSubstitution = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.splitterApprove = New System.Windows.Forms.Splitter()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.lblDrug = New System.Windows.Forms.Label()
        Me.lblPharmacyNotes = New System.Windows.Forms.Label()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.lblDirections = New System.Windows.Forms.Label()
        Me.lblRefills = New System.Windows.Forms.Label()
        Me.lblSubstitution = New System.Windows.Forms.Label()
        Me.lblQuantity = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlDeny = New System.Windows.Forms.Panel()
        Me.lblDenyPatientName = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.lblDenialReasoncode = New System.Windows.Forms.Label()
        Me.lblMedicationItemName = New System.Windows.Forms.Label()
        Me.lblMedicationName = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.cmbDenialReasonCode = New System.Windows.Forms.ComboBox()
        Me.pnltlstrip = New System.Windows.Forms.Panel()
        Me.tlStrpMain = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlStrpBtnOk = New System.Windows.Forms.ToolStripButton()
        Me.tlStrpBtnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblpnlHeader = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnl_Grid = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.C1RefillList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.dgRefillList = New System.Windows.Forms.DataGrid()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.txtSearch = New gloUserControlLibrary.gloSearchTextBox()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.miniToolStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnl_trv.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnl_toolstrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlwbBrowser.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnlProcessRequest.SuspendLayout()
        Me.pnlApprove.SuspendLayout()
        Me.pnlApproveDetails.SuspendLayout()
        Me.pnlDataGrid.SuspendLayout()
        CType(Me.dgChangeRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.pnlDeny.SuspendLayout()
        Me.pnltlstrip.SuspendLayout()
        Me.tlStrpMain.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnl_Grid.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.C1RefillList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgRefillList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_trv
        '
        Me.pnl_trv.Controls.Add(Me.Panel5)
        Me.pnl_trv.Controls.Add(Me.Panel6)
        Me.pnl_trv.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_trv.Location = New System.Drawing.Point(0, 56)
        Me.pnl_trv.Name = "pnl_trv"
        Me.pnl_trv.Size = New System.Drawing.Size(213, 764)
        Me.pnl_trv.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label39)
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.Label41)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.trvPrescribers)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 27)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(213, 737)
        Me.Panel5.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(4, 733)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(208, 1)
        Me.Label39.TabIndex = 8
        Me.Label39.Text = "label2"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 733)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "label4"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(212, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 733)
        Me.Label41.TabIndex = 6
        Me.Label41.Text = "label3"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(210, 1)
        Me.Label42.TabIndex = 5
        Me.Label42.Text = "label1"
        '
        'trvPrescribers
        '
        Me.trvPrescribers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPrescribers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPrescribers.ForeColor = System.Drawing.Color.Black
        Me.trvPrescribers.ImageIndex = 3
        Me.trvPrescribers.ImageList = Me.ImageList1
        Me.trvPrescribers.ItemHeight = 20
        Me.trvPrescribers.Location = New System.Drawing.Point(3, 0)
        Me.trvPrescribers.Name = "trvPrescribers"
        Me.trvPrescribers.SelectedImageIndex = 0
        Me.trvPrescribers.Size = New System.Drawing.Size(210, 734)
        Me.trvPrescribers.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Approved Rx.ico")
        Me.ImageList1.Images.SetKeyName(1, "Denied Prescription.ico")
        Me.ImageList1.Images.SetKeyName(2, "Denied Rx to Follow New Rx.ico")
        Me.ImageList1.Images.SetKeyName(3, "Provider.ico")
        Me.ImageList1.Images.SetKeyName(4, "Cancel.png")
        Me.ImageList1.Images.SetKeyName(5, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(6, "Bullet06.ico")
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(213, 27)
        Me.Panel6.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.lbl_Prescriber)
        Me.Panel2.Controls.Add(Me.Label43)
        Me.Panel2.Controls.Add(Me.Label44)
        Me.Panel2.Controls.Add(Me.Label45)
        Me.Panel2.Controls.Add(Me.Label46)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(210, 24)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(188, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(21, 22)
        Me.Panel3.TabIndex = 1
        Me.Panel3.Visible = False
        '
        'lbl_Prescriber
        '
        Me.lbl_Prescriber.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Prescriber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Prescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Prescriber.Location = New System.Drawing.Point(1, 1)
        Me.lbl_Prescriber.Name = "lbl_Prescriber"
        Me.lbl_Prescriber.Size = New System.Drawing.Size(208, 22)
        Me.lbl_Prescriber.TabIndex = 0
        Me.lbl_Prescriber.Text = " Prescriber "
        Me.lbl_Prescriber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(208, 1)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label2"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 23)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(209, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 23)
        Me.Label45.TabIndex = 6
        Me.Label45.Text = "label3"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(210, 1)
        Me.Label46.TabIndex = 5
        Me.Label46.Text = "label1"
        '
        'pnl_toolstrip
        '
        Me.pnl_toolstrip.Controls.Add(Me.lblSupplyText)
        Me.pnl_toolstrip.Controls.Add(Me.ToolStrip1)
        Me.pnl_toolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_toolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_toolstrip.Name = "pnl_toolstrip"
        Me.pnl_toolstrip.Size = New System.Drawing.Size(1232, 56)
        Me.pnl_toolstrip.TabIndex = 5
        '
        'lblSupplyText
        '
        Me.lblSupplyText.AutoSize = True
        Me.lblSupplyText.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplyText.ForeColor = System.Drawing.Color.Red
        Me.lblSupplyText.Location = New System.Drawing.Point(384, 21)
        Me.lblSupplyText.Name = "lblSupplyText"
        Me.lblSupplyText.Size = New System.Drawing.Size(42, 14)
        Me.lblSupplyText.TabIndex = 7
        Me.lblSupplyText.Text = "Label8"
        Me.lblSupplyText.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAproved, Me.ts_btnDenied, Me.ts_btnDeniedWithNewRxtoFollow, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1232, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ts_btnAproved
        '
        Me.ts_btnAproved.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAproved.Image = CType(resources.GetObject("ts_btnAproved.Image"), System.Drawing.Image)
        Me.ts_btnAproved.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAproved.Name = "ts_btnAproved"
        Me.ts_btnAproved.Size = New System.Drawing.Size(63, 50)
        Me.ts_btnAproved.Text = "&Approve"
        Me.ts_btnAproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnAproved.ToolTipText = "Approve RxChange Request"
        Me.ts_btnAproved.Visible = False
        '
        'ts_btnDenied
        '
        Me.ts_btnDenied.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDenied.Image = CType(resources.GetObject("ts_btnDenied.Image"), System.Drawing.Image)
        Me.ts_btnDenied.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDenied.Name = "ts_btnDenied"
        Me.ts_btnDenied.Size = New System.Drawing.Size(97, 50)
        Me.ts_btnDenied.Text = "Deny &Request"
        Me.ts_btnDenied.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDenied.Visible = False
        '
        'ts_btnDeniedWithNewRxtoFollow
        '
        Me.ts_btnDeniedWithNewRxtoFollow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDeniedWithNewRxtoFollow.Image = CType(resources.GetObject("ts_btnDeniedWithNewRxtoFollow.Image"), System.Drawing.Image)
        Me.ts_btnDeniedWithNewRxtoFollow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDeniedWithNewRxtoFollow.Name = "ts_btnDeniedWithNewRxtoFollow"
        Me.ts_btnDeniedWithNewRxtoFollow.Size = New System.Drawing.Size(111, 50)
        Me.ts_btnDeniedWithNewRxtoFollow.Text = "&Deny W/New Rx"
        Me.ts_btnDeniedWithNewRxtoFollow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDeniedWithNewRxtoFollow.ToolTipText = "Deny With New Rx to Follow"
        Me.ts_btnDeniedWithNewRxtoFollow.Visible = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh Pending RxChange Request List"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel9)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(217, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1015, 764)
        Me.Panel1.TabIndex = 7
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel8)
        Me.Panel9.Controls.Add(Me.Splitter2)
        Me.Panel9.Controls.Add(Me.pnl_Grid)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1015, 764)
        Me.Panel9.TabIndex = 11
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.pnlwbBrowser)
        Me.Panel8.Controls.Add(Me.pnlProcessRequest)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 304)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1015, 460)
        Me.Panel8.TabIndex = 11
        '
        'pnlwbBrowser
        '
        Me.pnlwbBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlwbBrowser.Controls.Add(Me.requestViewer)
        Me.pnlwbBrowser.Controls.Add(Me.Panel14)
        Me.pnlwbBrowser.Controls.Add(Me.Label96)
        Me.pnlwbBrowser.Controls.Add(Me.Label95)
        Me.pnlwbBrowser.Controls.Add(Me.Label94)
        Me.pnlwbBrowser.Controls.Add(Me.Label90)
        Me.pnlwbBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwbBrowser.Location = New System.Drawing.Point(0, 0)
        Me.pnlwbBrowser.Name = "pnlwbBrowser"
        Me.pnlwbBrowser.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlwbBrowser.Size = New System.Drawing.Size(1015, 460)
        Me.pnlwbBrowser.TabIndex = 11
        '
        'requestViewer
        '
        Me.requestViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.requestViewer.Location = New System.Drawing.Point(1, 26)
        Me.requestViewer.MinimumSize = New System.Drawing.Size(20, 20)
        Me.requestViewer.Name = "requestViewer"
        Me.requestViewer.Size = New System.Drawing.Size(1013, 430)
        Me.requestViewer.TabIndex = 15
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_2007Header1
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label33)
        Me.Panel14.Controls.Add(Me.Label34)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(1, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1013, 25)
        Me.Panel14.TabIndex = 237
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(0, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1013, 1)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1013, 25)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = " Request Details"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(1014, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 455)
        Me.Label96.TabIndex = 14
        Me.Label96.Text = "label4"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(0, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 455)
        Me.Label95.TabIndex = 13
        Me.Label95.Text = "label4"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label94.Location = New System.Drawing.Point(0, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1015, 1)
        Me.Label94.TabIndex = 12
        Me.Label94.Text = "label2"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label90.Location = New System.Drawing.Point(0, 456)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1015, 1)
        Me.Label90.TabIndex = 11
        Me.Label90.Text = "label2"
        '
        'pnlProcessRequest
        '
        Me.pnlProcessRequest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProcessRequest.Controls.Add(Me.pnlApprove)
        Me.pnlProcessRequest.Controls.Add(Me.pnlDeny)
        Me.pnlProcessRequest.Controls.Add(Me.pnltlstrip)
        Me.pnlProcessRequest.Controls.Add(Me.pnlHeader)
        Me.pnlProcessRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProcessRequest.Location = New System.Drawing.Point(0, 0)
        Me.pnlProcessRequest.Name = "pnlProcessRequest"
        Me.pnlProcessRequest.Size = New System.Drawing.Size(1015, 460)
        Me.pnlProcessRequest.TabIndex = 12
        Me.pnlProcessRequest.Visible = False
        '
        'pnlApprove
        '
        Me.pnlApprove.Controls.Add(Me.pnlApproveDetails)
        Me.pnlApprove.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlApprove.Location = New System.Drawing.Point(0, 83)
        Me.pnlApprove.Name = "pnlApprove"
        Me.pnlApprove.Size = New System.Drawing.Size(1015, 377)
        Me.pnlApprove.TabIndex = 5
        Me.pnlApprove.Visible = False
        '
        'pnlApproveDetails
        '
        Me.pnlApproveDetails.Controls.Add(Me.pnlDataGrid)
        Me.pnlApproveDetails.Controls.Add(Me.Panel7)
        Me.pnlApproveDetails.Controls.Add(Me.splitterApprove)
        Me.pnlApproveDetails.Controls.Add(Me.Panel10)
        Me.pnlApproveDetails.Controls.Add(Me.Label1)
        Me.pnlApproveDetails.Controls.Add(Me.Label5)
        Me.pnlApproveDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlApproveDetails.Location = New System.Drawing.Point(0, 0)
        Me.pnlApproveDetails.Name = "pnlApproveDetails"
        Me.pnlApproveDetails.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlApproveDetails.Size = New System.Drawing.Size(1015, 377)
        Me.pnlApproveDetails.TabIndex = 1
        '
        'pnlDataGrid
        '
        Me.pnlDataGrid.Controls.Add(Me.Label18)
        Me.pnlDataGrid.Controls.Add(Me.Label16)
        Me.pnlDataGrid.Controls.Add(Me.dgChangeRequests)
        Me.pnlDataGrid.Controls.Add(Me.Label17)
        Me.pnlDataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDataGrid.Location = New System.Drawing.Point(0, 199)
        Me.pnlDataGrid.Name = "pnlDataGrid"
        Me.pnlDataGrid.Size = New System.Drawing.Size(1012, 174)
        Me.pnlDataGrid.TabIndex = 229
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(1011, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 173)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1011, 1)
        Me.Label16.TabIndex = 30
        '
        'dgChangeRequests
        '
        Me.dgChangeRequests.AllowUserToAddRows = False
        Me.dgChangeRequests.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgChangeRequests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgChangeRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgChangeRequests.BackgroundColor = System.Drawing.Color.White
        Me.dgChangeRequests.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(217, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(2, 0, 0, 2)
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgChangeRequests.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgChangeRequests.ColumnHeadersHeight = 25
        Me.dgChangeRequests.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colChecked, Me.colDrug, Me.colQuantity, Me.colDuration, Me.colDirections, Me.colNotes, Me.colRefills, Me.colSubstitution})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgChangeRequests.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgChangeRequests.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgChangeRequests.EnableHeadersVisualStyles = False
        Me.dgChangeRequests.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgChangeRequests.Location = New System.Drawing.Point(1, 0)
        Me.dgChangeRequests.MultiSelect = False
        Me.dgChangeRequests.Name = "dgChangeRequests"
        Me.dgChangeRequests.RowHeadersVisible = False
        Me.dgChangeRequests.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.dgChangeRequests.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgChangeRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgChangeRequests.Size = New System.Drawing.Size(1011, 174)
        Me.dgChangeRequests.TabIndex = 17
        '
        'colChecked
        '
        Me.colChecked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.colChecked.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.colChecked.HeaderText = ""
        Me.colChecked.Name = "colChecked"
        Me.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colChecked.Width = 21
        '
        'colDrug
        '
        Me.colDrug.DataPropertyName = "DrugDescription"
        Me.colDrug.FillWeight = 103.1408!
        Me.colDrug.HeaderText = "Drug"
        Me.colDrug.Name = "colDrug"
        Me.colDrug.ReadOnly = True
        '
        'colQuantity
        '
        Me.colQuantity.DataPropertyName = "Quantity.Value"
        Me.colQuantity.FillWeight = 57.65635!
        Me.colQuantity.HeaderText = "Quantity"
        Me.colQuantity.Name = "colQuantity"
        Me.colQuantity.ReadOnly = True
        '
        'colDuration
        '
        Me.colDuration.DataPropertyName = "DaysSupply"
        Me.colDuration.FillWeight = 53.41866!
        Me.colDuration.HeaderText = "Duration"
        Me.colDuration.Name = "colDuration"
        Me.colDuration.ReadOnly = True
        '
        'colDirections
        '
        Me.colDirections.DataPropertyName = "Directions"
        Me.colDirections.FillWeight = 154.0112!
        Me.colDirections.HeaderText = "Directions"
        Me.colDirections.Name = "colDirections"
        Me.colDirections.ReadOnly = True
        '
        'colNotes
        '
        Me.colNotes.DataPropertyName = "Note"
        Me.colNotes.FillWeight = 168.9731!
        Me.colNotes.HeaderText = "Notes"
        Me.colNotes.Name = "colNotes"
        Me.colNotes.ReadOnly = True
        '
        'colRefills
        '
        Me.colRefills.DataPropertyName = "Refills.Value"
        Me.colRefills.FillWeight = 59.65909!
        Me.colRefills.HeaderText = "Refills"
        Me.colRefills.Name = "colRefills"
        Me.colRefills.ReadOnly = True
        '
        'colSubstitution
        '
        Me.colSubstitution.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSubstitution.DataPropertyName = "Substitutions"
        Me.colSubstitution.FillWeight = 103.1408!
        Me.colSubstitution.HeaderText = "Substitution"
        Me.colSubstitution.Name = "colSubstitution"
        Me.colSubstitution.ReadOnly = True
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 174)
        Me.Label17.TabIndex = 31
        Me.Label17.Text = "label4"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_2007Header
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label32)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.Label25)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 174)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1012, 25)
        Me.Panel7.TabIndex = 235
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1010, 1)
        Me.Label32.TabIndex = 34
        Me.Label32.Text = "label2"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(1011, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 25)
        Me.Label31.TabIndex = 33
        Me.Label31.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 25)
        Me.Label30.TabIndex = 32
        Me.Label30.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1012, 25)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = " Medication Requested"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'splitterApprove
        '
        Me.splitterApprove.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitterApprove.Location = New System.Drawing.Point(0, 170)
        Me.splitterApprove.Name = "splitterApprove"
        Me.splitterApprove.Size = New System.Drawing.Size(1012, 4)
        Me.splitterApprove.TabIndex = 234
        Me.splitterApprove.TabStop = False
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel13)
        Me.Panel10.Controls.Add(Me.lblPatientName)
        Me.Panel10.Controls.Add(Me.Label24)
        Me.Panel10.Controls.Add(Me.Label4)
        Me.Panel10.Controls.Add(Me.Label3)
        Me.Panel10.Controls.Add(Me.Label7)
        Me.Panel10.Controls.Add(Me.Label15)
        Me.Panel10.Controls.Add(Me.Label84)
        Me.Panel10.Controls.Add(Me.Label83)
        Me.Panel10.Controls.Add(Me.lblDrug)
        Me.Panel10.Controls.Add(Me.lblPharmacyNotes)
        Me.Panel10.Controls.Add(Me.lblDuration)
        Me.Panel10.Controls.Add(Me.Label80)
        Me.Panel10.Controls.Add(Me.Label89)
        Me.Panel10.Controls.Add(Me.Label79)
        Me.Panel10.Controls.Add(Me.lblDirections)
        Me.Panel10.Controls.Add(Me.lblRefills)
        Me.Panel10.Controls.Add(Me.lblSubstitution)
        Me.Panel10.Controls.Add(Me.lblQuantity)
        Me.Panel10.Controls.Add(Me.Label78)
        Me.Panel10.Controls.Add(Me.Label76)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1012, 166)
        Me.Panel10.TabIndex = 233
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.Transparent
        Me.Panel13.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_2007Header
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.Label27)
        Me.Panel13.Controls.Add(Me.Label28)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(1, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1010, 25)
        Me.Panel13.TabIndex = 236
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(0, 24)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1010, 1)
        Me.Label27.TabIndex = 9
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1010, 25)
        Me.Label28.TabIndex = 1
        Me.Label28.Text = " Medication Prescribed"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientName
        '
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(154, 34)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(830, 14)
        Me.lblPatientName.TabIndex = 21
        Me.lblPatientName.UseMnemonic = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(61, 34)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(89, 14)
        Me.Label24.TabIndex = 20
        Me.Label24.Text = "Patient Name :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1011, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 165)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "label3"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 165)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(0, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1012, 1)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "label2"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(152, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(0, 14)
        Me.Label15.TabIndex = 6
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(821, 103)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(81, 14)
        Me.Label84.TabIndex = 0
        Me.Label84.Text = "Substitution :"
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(85, 80)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(65, 14)
        Me.Label83.TabIndex = 3
        Me.Label83.Text = "Drug Qty :"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDrug
        '
        Me.lblDrug.BackColor = System.Drawing.Color.Transparent
        Me.lblDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDrug.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrug.Location = New System.Drawing.Point(154, 57)
        Me.lblDrug.Name = "lblDrug"
        Me.lblDrug.Size = New System.Drawing.Size(830, 14)
        Me.lblDrug.TabIndex = 6
        Me.lblDrug.UseMnemonic = False
        '
        'lblPharmacyNotes
        '
        Me.lblPharmacyNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblPharmacyNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPharmacyNotes.Location = New System.Drawing.Point(154, 126)
        Me.lblPharmacyNotes.Name = "lblPharmacyNotes"
        Me.lblPharmacyNotes.Size = New System.Drawing.Size(830, 31)
        Me.lblPharmacyNotes.TabIndex = 5
        Me.lblPharmacyNotes.UseMnemonic = False
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = True
        Me.lblDuration.BackColor = System.Drawing.Color.Transparent
        Me.lblDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDuration.Location = New System.Drawing.Point(907, 80)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(47, 14)
        Me.lblDuration.TabIndex = 16
        Me.lblDuration.Text = "Label3"
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.BackColor = System.Drawing.Color.Transparent
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(47, 126)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(103, 14)
        Me.Label80.TabIndex = 0
        Me.Label80.Text = "Pharmacy Notes :"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.BackColor = System.Drawing.Color.Transparent
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.Location = New System.Drawing.Point(841, 80)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(61, 14)
        Me.Label89.TabIndex = 15
        Me.Label89.Text = "Duration :"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(447, 78)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(125, 18)
        Me.Label79.TabIndex = 0
        Me.Label79.Text = "Refills :"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDirections
        '
        Me.lblDirections.AutoSize = True
        Me.lblDirections.BackColor = System.Drawing.Color.Transparent
        Me.lblDirections.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirections.Location = New System.Drawing.Point(154, 103)
        Me.lblDirections.Name = "lblDirections"
        Me.lblDirections.Size = New System.Drawing.Size(39, 14)
        Me.lblDirections.TabIndex = 12
        Me.lblDirections.Text = "Label"
        Me.lblDirections.UseMnemonic = False
        '
        'lblRefills
        '
        Me.lblRefills.AutoSize = True
        Me.lblRefills.BackColor = System.Drawing.Color.Transparent
        Me.lblRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefills.Location = New System.Drawing.Point(573, 80)
        Me.lblRefills.Name = "lblRefills"
        Me.lblRefills.Size = New System.Drawing.Size(47, 14)
        Me.lblRefills.TabIndex = 5
        Me.lblRefills.Text = "Label3"
        '
        'lblSubstitution
        '
        Me.lblSubstitution.AutoSize = True
        Me.lblSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubstitution.Location = New System.Drawing.Point(907, 103)
        Me.lblSubstitution.Name = "lblSubstitution"
        Me.lblSubstitution.Size = New System.Drawing.Size(47, 14)
        Me.lblSubstitution.TabIndex = 11
        Me.lblSubstitution.Text = "Label3"
        '
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lblQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQuantity.Location = New System.Drawing.Point(154, 80)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(39, 14)
        Me.lblQuantity.TabIndex = 5
        Me.lblQuantity.Text = "Label"
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(39, 103)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(111, 14)
        Me.Label78.TabIndex = 0
        Me.Label78.Text = "Patient Directions :"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.BackColor = System.Drawing.Color.Transparent
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(109, 57)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(41, 14)
        Me.Label76.TabIndex = 0
        Me.Label76.Text = "Drug :"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(0, 373)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1012, 1)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1012, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label1"
        '
        'pnlDeny
        '
        Me.pnlDeny.Controls.Add(Me.lblDenyPatientName)
        Me.pnlDeny.Controls.Add(Me.Label29)
        Me.pnlDeny.Controls.Add(Me.Label11)
        Me.pnlDeny.Controls.Add(Me.Label12)
        Me.pnlDeny.Controls.Add(Me.Label13)
        Me.pnlDeny.Controls.Add(Me.Label14)
        Me.pnlDeny.Controls.Add(Me.txtNotes)
        Me.pnlDeny.Controls.Add(Me.lblDenialReasoncode)
        Me.pnlDeny.Controls.Add(Me.lblMedicationItemName)
        Me.pnlDeny.Controls.Add(Me.lblMedicationName)
        Me.pnlDeny.Controls.Add(Me.lblNotes)
        Me.pnlDeny.Controls.Add(Me.cmbDenialReasonCode)
        Me.pnlDeny.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDeny.Location = New System.Drawing.Point(0, 83)
        Me.pnlDeny.Name = "pnlDeny"
        Me.pnlDeny.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlDeny.Size = New System.Drawing.Size(1015, 377)
        Me.pnlDeny.TabIndex = 0
        '
        'lblDenyPatientName
        '
        Me.lblDenyPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblDenyPatientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDenyPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDenyPatientName.Location = New System.Drawing.Point(139, 18)
        Me.lblDenyPatientName.Name = "lblDenyPatientName"
        Me.lblDenyPatientName.Size = New System.Drawing.Size(830, 14)
        Me.lblDenyPatientName.TabIndex = 23
        Me.lblDenyPatientName.Text = "Label8"
        Me.lblDenyPatientName.UseMnemonic = False
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(47, 18)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(89, 14)
        Me.Label29.TabIndex = 22
        Me.Label29.Text = "Patient Name :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(1, 373)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1010, 1)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 370)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1011, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 370)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1012, 1)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label1"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(139, 95)
        Me.txtNotes.MaxLength = 70
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(397, 48)
        Me.txtNotes.TabIndex = 3
        '
        'lblDenialReasoncode
        '
        Me.lblDenialReasoncode.AutoSize = True
        Me.lblDenialReasoncode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDenialReasoncode.Location = New System.Drawing.Point(15, 68)
        Me.lblDenialReasoncode.Name = "lblDenialReasoncode"
        Me.lblDenialReasoncode.Size = New System.Drawing.Size(121, 14)
        Me.lblDenialReasoncode.TabIndex = 1
        Me.lblDenialReasoncode.Text = "Denial Reason code :"
        '
        'lblMedicationItemName
        '
        Me.lblMedicationItemName.AutoSize = True
        Me.lblMedicationItemName.Location = New System.Drawing.Point(139, 41)
        Me.lblMedicationItemName.Name = "lblMedicationItemName"
        Me.lblMedicationItemName.Size = New System.Drawing.Size(0, 14)
        Me.lblMedicationItemName.TabIndex = 6
        '
        'lblMedicationName
        '
        Me.lblMedicationName.AutoSize = True
        Me.lblMedicationName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedicationName.Location = New System.Drawing.Point(28, 41)
        Me.lblMedicationName.Name = "lblMedicationName"
        Me.lblMedicationName.Size = New System.Drawing.Size(108, 14)
        Me.lblMedicationName.TabIndex = 0
        Me.lblMedicationName.Text = "Medication Name :"
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(89, 95)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(47, 14)
        Me.lblNotes.TabIndex = 2
        Me.lblNotes.Text = "Notes :"
        '
        'cmbDenialReasonCode
        '
        Me.cmbDenialReasonCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDenialReasonCode.FormattingEnabled = True
        Me.cmbDenialReasonCode.Location = New System.Drawing.Point(139, 64)
        Me.cmbDenialReasonCode.Name = "cmbDenialReasonCode"
        Me.cmbDenialReasonCode.Size = New System.Drawing.Size(397, 22)
        Me.cmbDenialReasonCode.TabIndex = 4
        '
        'pnltlstrip
        '
        Me.pnltlstrip.AutoSize = True
        Me.pnltlstrip.Controls.Add(Me.tlStrpMain)
        Me.pnltlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltlstrip.Location = New System.Drawing.Point(0, 30)
        Me.pnltlstrip.Name = "pnltlstrip"
        Me.pnltlstrip.Size = New System.Drawing.Size(1015, 53)
        Me.pnltlstrip.TabIndex = 4
        '
        'tlStrpMain
        '
        Me.tlStrpMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlStrpMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlStrpMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpMain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlStrpMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlStrpBtnOk, Me.tlStrpBtnCancel})
        Me.tlStrpMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlStrpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlStrpMain.Name = "tlStrpMain"
        Me.tlStrpMain.Size = New System.Drawing.Size(1015, 53)
        Me.tlStrpMain.TabIndex = 0
        Me.tlStrpMain.Text = "ToolStrip1"
        '
        'tlStrpBtnOk
        '
        Me.tlStrpBtnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpBtnOk.Image = CType(resources.GetObject("tlStrpBtnOk.Image"), System.Drawing.Image)
        Me.tlStrpBtnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlStrpBtnOk.Name = "tlStrpBtnOk"
        Me.tlStrpBtnOk.Size = New System.Drawing.Size(66, 50)
        Me.tlStrpBtnOk.Tag = "OK"
        Me.tlStrpBtnOk.Text = "&Save&&Cls"
        Me.tlStrpBtnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlStrpBtnOk.ToolTipText = "Save and Close"
        '
        'tlStrpBtnCancel
        '
        Me.tlStrpBtnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpBtnCancel.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.tlStrpBtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlStrpBtnCancel.Name = "tlStrpBtnCancel"
        Me.tlStrpBtnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tlStrpBtnCancel.Tag = "Cancel"
        Me.tlStrpBtnCancel.Text = "&Close"
        Me.tlStrpBtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHeader.Controls.Add(Me.Panel12)
        Me.pnlHeader.Controls.Add(Me.Label20)
        Me.pnlHeader.Controls.Add(Me.Label21)
        Me.pnlHeader.Controls.Add(Me.Label22)
        Me.pnlHeader.Controls.Add(Me.Label23)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlHeader.Size = New System.Drawing.Size(1015, 30)
        Me.pnlHeader.TabIndex = 6
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Transparent
        Me.Panel12.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue20071
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.lblpnlHeader)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(1, 1)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(1013, 25)
        Me.Panel12.TabIndex = 1
        '
        'lblpnlHeader
        '
        Me.lblpnlHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblpnlHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblpnlHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpnlHeader.ForeColor = System.Drawing.Color.White
        Me.lblpnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblpnlHeader.Name = "lblpnlHeader"
        Me.lblpnlHeader.Size = New System.Drawing.Size(1013, 25)
        Me.lblpnlHeader.TabIndex = 1
        Me.lblpnlHeader.Text = " Approve Request"
        Me.lblpnlHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 26)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1013, 1)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "label2"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 26)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1014, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 26)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1015, 1)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 300)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(1015, 4)
        Me.Splitter2.TabIndex = 13
        Me.Splitter2.TabStop = False
        '
        'pnl_Grid
        '
        Me.pnl_Grid.Controls.Add(Me.Panel4)
        Me.pnl_Grid.Controls.Add(Me.pnlTop)
        Me.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Grid.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Grid.Name = "pnl_Grid"
        Me.pnl_Grid.Size = New System.Drawing.Size(1015, 300)
        Me.pnl_Grid.TabIndex = 6
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.C1RefillList)
        Me.Panel4.Controls.Add(Me.dgRefillList)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 27)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel4.Size = New System.Drawing.Size(1015, 273)
        Me.Panel4.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 272)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1010, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'C1RefillList
        '
        Me.C1RefillList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1RefillList.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me.C1RefillList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1RefillList.Location = New System.Drawing.Point(1, 1)
        Me.C1RefillList.Name = "C1RefillList"
        Me.C1RefillList.Rows.DefaultSize = 21
        Me.C1RefillList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1RefillList.Size = New System.Drawing.Size(1010, 272)
        Me.C1RefillList.StyleInfo = resources.GetString("C1RefillList.StyleInfo")
        Me.C1RefillList.TabIndex = 10
        '
        'dgRefillList
        '
        Me.dgRefillList.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgRefillList.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgRefillList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgRefillList.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgRefillList.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgRefillList.CaptionForeColor = System.Drawing.Color.White
        Me.dgRefillList.CaptionVisible = False
        Me.dgRefillList.ContextMenuStrip = Me.cntListmenuStrip
        Me.dgRefillList.DataMember = ""
        Me.dgRefillList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgRefillList.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgRefillList.GridLineColor = System.Drawing.Color.Black
        Me.dgRefillList.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgRefillList.HeaderForeColor = System.Drawing.Color.White
        Me.dgRefillList.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgRefillList.Location = New System.Drawing.Point(1, 1)
        Me.dgRefillList.Name = "dgRefillList"
        Me.dgRefillList.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgRefillList.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgRefillList.ReadOnly = True
        Me.dgRefillList.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgRefillList.SelectionForeColor = System.Drawing.Color.Black
        Me.dgRefillList.Size = New System.Drawing.Size(1010, 272)
        Me.dgRefillList.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 272)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1011, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 272)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1012, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Panel11)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlTop.Size = New System.Drawing.Size(1015, 27)
        Me.pnlTop.TabIndex = 2
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel11.Controls.Add(Me.pnlTopRight)
        Me.Panel11.Controls.Add(Me.lbl_RightBrd)
        Me.Panel11.Controls.Add(Me.Label6)
        Me.Panel11.Controls.Add(Me.lbl_TopBrd)
        Me.Panel11.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1012, 24)
        Me.Panel11.TabIndex = 9
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'pnlTopRight
        '
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Panel15)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTopRight.Location = New System.Drawing.Point(662, 1)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(349, 22)
        Me.pnlTopRight.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(-20, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(128, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Middle Name :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.White
        Me.Panel15.Controls.Add(Me.txtSearch)
        Me.Panel15.Controls.Add(Me.Label97)
        Me.Panel15.Controls.Add(Me.Label98)
        Me.Panel15.Controls.Add(Me.btnClear)
        Me.Panel15.Controls.Add(Me.Label100)
        Me.Panel15.Controls.Add(Me.Label101)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.ForeColor = System.Drawing.Color.Black
        Me.Panel15.Location = New System.Drawing.Point(108, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(241, 22)
        Me.Panel15.TabIndex = 47
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ShortcutsEnabled = False
        Me.txtSearch.Size = New System.Drawing.Size(215, 15)
        Me.txtSearch.TabIndex = 0
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.White
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label97.Location = New System.Drawing.Point(5, 17)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(215, 5)
        Me.Label97.TabIndex = 43
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.White
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label98.Location = New System.Drawing.Point(5, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(215, 3)
        Me.Label98.TabIndex = 37
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(220, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 44
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.White
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label100.Location = New System.Drawing.Point(1, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(4, 22)
        Me.Label100.TabIndex = 38
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Location = New System.Drawing.Point(0, 0)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 22)
        Me.Label101.TabIndex = 39
        Me.Label101.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1011, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1012, 22)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "   RxChange Requests"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1012, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(0, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1012, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(213, 56)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 764)
        Me.Splitter1.TabIndex = 8
        Me.Splitter1.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.miniToolStrip.Location = New System.Drawing.Point(109, 0)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(1001, 53)
        Me.miniToolStrip.TabIndex = 0
        '
        'frmViewRxRequests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1232, 820)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl_trv)
        Me.Controls.Add(Me.pnl_toolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewRxRequests"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pending RxChange Requests"
        Me.pnl_trv.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnl_toolstrip.ResumeLayout(False)
        Me.pnl_toolstrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.pnlwbBrowser.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.pnlProcessRequest.ResumeLayout(False)
        Me.pnlProcessRequest.PerformLayout()
        Me.pnlApprove.ResumeLayout(False)
        Me.pnlApproveDetails.ResumeLayout(False)
        Me.pnlDataGrid.ResumeLayout(False)
        CType(Me.dgChangeRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.pnlDeny.ResumeLayout(False)
        Me.pnlDeny.PerformLayout()
        Me.pnltlstrip.ResumeLayout(False)
        Me.pnltlstrip.PerformLayout()
        Me.tlStrpMain.ResumeLayout(False)
        Me.tlStrpMain.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.pnl_Grid.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.C1RefillList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgRefillList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_trv As System.Windows.Forms.Panel
    Friend WithEvents pnl_toolstrip As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnDenied As System.Windows.Forms.ToolStripButton
    Friend WithEvents trvPrescribers As System.Windows.Forms.TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Prescriber As System.Windows.Forms.Label
    Friend WithEvents ts_btnAproved As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDeniedWithNewRxtoFollow As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents lblSupplyText As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents pnlProcessRequest As System.Windows.Forms.Panel
    Friend WithEvents pnlDeny As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents lblDenialReasoncode As System.Windows.Forms.Label
    Friend WithEvents lblMedicationItemName As System.Windows.Forms.Label
    Friend WithEvents lblMedicationName As System.Windows.Forms.Label
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents cmbDenialReasonCode As System.Windows.Forms.ComboBox
    Friend WithEvents pnltlstrip As System.Windows.Forms.Panel
    Friend WithEvents tlStrpMain As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlStrpBtnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlStrpBtnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlwbBrowser As System.Windows.Forms.Panel
    Friend WithEvents requestViewer As System.Windows.Forms.WebBrowser
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnl_Grid As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents C1RefillList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents dgRefillList As System.Windows.Forms.DataGrid
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Friend WithEvents miniToolStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents pnlApprove As System.Windows.Forms.Panel
    Friend WithEvents pnlApproveDetails As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblDuration As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents lblDirections As System.Windows.Forms.Label
    Friend WithEvents lblSubstitution As System.Windows.Forms.Label
    Friend WithEvents lblDrug As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents lblRefills As System.Windows.Forms.Label
    Friend WithEvents lblPharmacyNotes As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents dgChangeRequests As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDataGrid As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents toolTip As System.Windows.Forms.ToolTip
    Friend WithEvents splitterApprove As System.Windows.Forms.Splitter
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents lblpnlHeader As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblDenyPatientName As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents colChecked As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colDrug As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDuration As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDirections As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNotes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefills As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSubstitution As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
