<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVWErrorMessage
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
    Private Shared frm As frmVWErrorMessage

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            Try

                If (IsNothing(dgRefillList) = False) Then
                    dgRefillList.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgRefillList)
                    dgRefillList.Dispose()
                    dgRefillList = Nothing
                End If
            Catch ex As Exception

            End Try
            If (disposing) Then
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpContextMenuStrip As ContextMenuStrip() = {cntListmenuStrip}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
                Catch ex As Exception

                End Try
               
                
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

    Public Shared Function GetInstance(Optional ByVal PrescriberID As String = "") As frmVWErrorMessage
        '_mu.WaitOne()
        Try
            
            If Not frm Is Nothing Then
                '    'Dim ecls As EventArgs
                '    'Dim obj As Object
                '    'obj = CType(Me, Object)
                '    'frm.frmVWErrorMessage_FormClosed(obj, ecls)
                frm.Close()
                '    frm = Nothing
            End If

            If frm Is Nothing Then

                frm = New frmVWErrorMessage(PrescriberID)

            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWErrorMessage))
        Me.pnl_toolstrip = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuReview = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReloadRx = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnl_trv = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.trvPrescribers = New System.Windows.Forms.TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbl_Prescriber = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnl_Grid = New System.Windows.Forms.Panel()
        Me.pnl_RefillInfo = New System.Windows.Forms.Panel()
        Me.pnl_ProviderInfo = New System.Windows.Forms.Panel()
        Me.lbl_ProviderFax = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lbl_ProviderNPI = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.lbl_ProviderAddress2 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.lbl_PrPhone = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbl_Provider = New System.Windows.Forms.Label()
        Me.lbl_ProviderName = New System.Windows.Forms.Label()
        Me.lbl_ProviderZIP = New System.Windows.Forms.Label()
        Me.lbl_ProviderAdd = New System.Windows.Forms.Label()
        Me.lbl_ProviderState = New System.Windows.Forms.Label()
        Me.lbl_ProviderCity = New System.Windows.Forms.Label()
        Me.lbl_ProvidCity = New System.Windows.Forms.Label()
        Me.lbl_ProviderAddress = New System.Windows.Forms.Label()
        Me.lbl_ProvidState = New System.Windows.Forms.Label()
        Me.lbl_ProvidZIP = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.pnl_PharmacyInfo = New System.Windows.Forms.Panel()
        Me.lbl_PharmacyFax = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.lbl_PharmacyNPI = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.lbl_PharmacyAddress2 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.lbl_PharmacyZip = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_Pharmacy = New System.Windows.Forms.Label()
        Me.lbl_PharmacyName = New System.Windows.Forms.Label()
        Me.lbl_PharmacyZIPCode = New System.Windows.Forms.Label()
        Me.lbl_PharmacyAdd = New System.Windows.Forms.Label()
        Me.lbl_PharmacyState = New System.Windows.Forms.Label()
        Me.lbl_PharmacyPhoneNo = New System.Windows.Forms.Label()
        Me.lbl_PharamcyCity = New System.Windows.Forms.Label()
        Me.pbl_PharmacyPhone = New System.Windows.Forms.Label()
        Me.lbl_PharmCity = New System.Windows.Forms.Label()
        Me.lbl_PharmacyAddress = New System.Windows.Forms.Label()
        Me.lbl_PharmState = New System.Windows.Forms.Label()
        Me.lbl_PharmZIPCode = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.pnl_PatientInfo = New System.Windows.Forms.Panel()
        Me.lbl_PatientAddress2 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.lbl_Patient = New System.Windows.Forms.Label()
        Me.lbl_PatientName = New System.Windows.Forms.Label()
        Me.lbl_ZIPCode = New System.Windows.Forms.Label()
        Me.lbl_PatAdd = New System.Windows.Forms.Label()
        Me.lbl_PatGender = New System.Windows.Forms.Label()
        Me.lbl_PatDOB = New System.Windows.Forms.Label()
        Me.lbl_StateName = New System.Windows.Forms.Label()
        Me.lbl_PatientPhoneNo = New System.Windows.Forms.Label()
        Me.lbl_CityName = New System.Windows.Forms.Label()
        Me.lbl_PatientPhone = New System.Windows.Forms.Label()
        Me.lbl_City = New System.Windows.Forms.Label()
        Me.lbl_PatientAddress = New System.Windows.Forms.Label()
        Me.lbl_State = New System.Windows.Forms.Label()
        Me.lbl_Gender = New System.Windows.Forms.Label()
        Me.lbl_ZIP = New System.Windows.Forms.Label()
        Me.lbl_DOB = New System.Windows.Forms.Label()
        Me.pnlRefillinfotitle = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnl_MedicalDispanced = New System.Windows.Forms.Panel()
        Me.lbl_MDRef_Qlfr = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.lbl_MDDuration = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.lbl_MDLastFillDate = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.lblDrugName_Strength_Dosageform = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.lblSubstitution = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.lblDrugQuantity = New System.Windows.Forms.Label()
        Me.lblWrittenDate = New System.Windows.Forms.Label()
        Me.lblRefillQuantity = New System.Windows.Forms.Label()
        Me.lblDrugNotes = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.lblDirection = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.pnl_MedicalPrescribe = New System.Windows.Forms.Panel()
        Me.lbl_Ref_Qlfr = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_Duration = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.lbl_LastFillDate = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.lbl_MPDirection = New System.Windows.Forms.Label()
        Me.lbl_MPSubstitution = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.lbl_MPDrugName_Strength_Dosageform = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.lbl_MPDrugQuantity = New System.Windows.Forms.Label()
        Me.lbl_MPWrittenDate = New System.Windows.Forms.Label()
        Me.lbl_MPRefillQuantity = New System.Windows.Forms.Label()
        Me.lbl_MPDrugnotes = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Pnl_MedicalPrescribedHeader = New System.Windows.Forms.Panel()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.pnlErrorMessage = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblErrMsgDtl_Name = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblRelatesMsgId = New System.Windows.Forms.Label()
        Me.lblDateSent = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblMessageId = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlError = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnupdown = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgRefillList = New gloEMR.clsDataGrid()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.pnl_toolstrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.cntListmenuStrip.SuspendLayout()
        Me.pnl_trv.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_Grid.SuspendLayout()
        Me.pnl_RefillInfo.SuspendLayout()
        Me.pnl_ProviderInfo.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.pnl_PharmacyInfo.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnl_PatientInfo.SuspendLayout()
        Me.pnlRefillinfotitle.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnl_MedicalDispanced.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.pnl_MedicalPrescribe.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Pnl_MedicalPrescribedHeader.SuspendLayout()
        Me.pnlErrorMessage.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlError.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dgRefillList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTopRight.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_toolstrip
        '
        Me.pnl_toolstrip.AutoSize = True
        Me.pnl_toolstrip.Controls.Add(Me.ToolStrip1)
        Me.pnl_toolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_toolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_toolstrip.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_toolstrip.Name = "pnl_toolstrip"
        Me.pnl_toolstrip.Size = New System.Drawing.Size(1481, 57)
        Me.pnl_toolstrip.TabIndex = 10
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1481, 57)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(71, 54)
        Me.ts_btnRefresh.Text = "Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Refresh Pending Refill Request List"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(53, 54)
        Me.ts_btnClose.Text = "Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Approved Rx.ico")
        Me.ImageList1.Images.SetKeyName(1, "Denied Prescription.ico")
        Me.ImageList1.Images.SetKeyName(2, "Denied Rx to Follow New Rx.ico")
        Me.ImageList1.Images.SetKeyName(3, "Provider.ico")
        Me.ImageList1.Images.SetKeyName(4, "Bullet06.ico")
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReview, Me.mnuReloadRx})
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(146, 52)
        '
        'mnuReview
        '
        Me.mnuReview.Name = "mnuReview"
        Me.mnuReview.Size = New System.Drawing.Size(145, 24)
        Me.mnuReview.Text = "Dismiss"
        '
        'mnuReloadRx
        '
        Me.mnuReloadRx.Name = "mnuReloadRx"
        Me.mnuReloadRx.Size = New System.Drawing.Size(145, 24)
        Me.mnuReloadRx.Text = "Reload Rx"
        '
        'pnl_trv
        '
        Me.pnl_trv.Controls.Add(Me.Panel3)
        Me.pnl_trv.Controls.Add(Me.Panel2)
        Me.pnl_trv.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_trv.Location = New System.Drawing.Point(0, 57)
        Me.pnl_trv.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_trv.Name = "pnl_trv"
        Me.pnl_trv.Size = New System.Drawing.Size(281, 871)
        Me.pnl_trv.TabIndex = 8
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel3.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_RightBrd)
        Me.Panel3.Controls.Add(Me.lbl_TopBrd)
        Me.Panel3.Controls.Add(Me.trvPrescribers)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 36)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(4, 0, 0, 4)
        Me.Panel3.Size = New System.Drawing.Size(281, 835)
        Me.Panel3.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(5, 830)
        Me.lbl_BottomBrd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(275, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(4, 1)
        Me.lbl_LeftBrd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 830)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(280, 1)
        Me.lbl_RightBrd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 830)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_TopBrd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(277, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'trvPrescribers
        '
        Me.trvPrescribers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvPrescribers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvPrescribers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvPrescribers.ForeColor = System.Drawing.Color.Black
        Me.trvPrescribers.ImageIndex = 3
        Me.trvPrescribers.ImageList = Me.ImageList1
        Me.trvPrescribers.ItemHeight = 20
        Me.trvPrescribers.Location = New System.Drawing.Point(4, 0)
        Me.trvPrescribers.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.trvPrescribers.Name = "trvPrescribers"
        Me.trvPrescribers.SelectedImageIndex = 3
        Me.trvPrescribers.Size = New System.Drawing.Size(277, 831)
        Me.trvPrescribers.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Panel9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(4, 4, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(281, 36)
        Me.Panel2.TabIndex = 1
        '
        'Panel9
        '
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label2)
        Me.Panel9.Controls.Add(Me.Label10)
        Me.Panel9.Controls.Add(Me.Label12)
        Me.Panel9.Controls.Add(Me.lbl_Prescriber)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(4, 4)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(277, 28)
        Me.Panel9.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 27)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(275, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(276, 1)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 27)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(276, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label1"
        '
        'lbl_Prescriber
        '
        Me.lbl_Prescriber.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Prescriber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Prescriber.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Prescriber.Location = New System.Drawing.Point(1, 0)
        Me.lbl_Prescriber.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Prescriber.Name = "lbl_Prescriber"
        Me.lbl_Prescriber.Size = New System.Drawing.Size(276, 28)
        Me.lbl_Prescriber.TabIndex = 0
        Me.lbl_Prescriber.Text = " Prescriber "
        Me.lbl_Prescriber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 28)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(281, 57)
        Me.Splitter1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 871)
        Me.Splitter1.TabIndex = 9
        Me.Splitter1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.pnl_Grid)
        Me.Panel1.Controls.Add(Me.Panel18)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(285, 57)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1196, 871)
        Me.Panel1.TabIndex = 12
        '
        'pnl_Grid
        '
        Me.pnl_Grid.AutoScroll = True
        Me.pnl_Grid.Controls.Add(Me.pnl_RefillInfo)
        Me.pnl_Grid.Controls.Add(Me.Panel4)
        Me.pnl_Grid.Controls.Add(Me.pnlTopRight)
        Me.pnl_Grid.Controls.Add(Me.Panel8)
        Me.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Grid.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Grid.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_Grid.Name = "pnl_Grid"
        Me.pnl_Grid.Size = New System.Drawing.Size(1175, 1525)
        Me.pnl_Grid.TabIndex = 6
        '
        'pnl_RefillInfo
        '
        Me.pnl_RefillInfo.Controls.Add(Me.pnl_ProviderInfo)
        Me.pnl_RefillInfo.Controls.Add(Me.Panel7)
        Me.pnl_RefillInfo.Controls.Add(Me.pnl_PharmacyInfo)
        Me.pnl_RefillInfo.Controls.Add(Me.Panel6)
        Me.pnl_RefillInfo.Controls.Add(Me.pnl_PatientInfo)
        Me.pnl_RefillInfo.Controls.Add(Me.pnlRefillinfotitle)
        Me.pnl_RefillInfo.Controls.Add(Me.pnl_MedicalDispanced)
        Me.pnl_RefillInfo.Controls.Add(Me.Panel16)
        Me.pnl_RefillInfo.Controls.Add(Me.pnl_MedicalPrescribe)
        Me.pnl_RefillInfo.Controls.Add(Me.Panel15)
        Me.pnl_RefillInfo.Controls.Add(Me.pnlErrorMessage)
        Me.pnl_RefillInfo.Controls.Add(Me.pnlError)
        Me.pnl_RefillInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_RefillInfo.Location = New System.Drawing.Point(0, 252)
        Me.pnl_RefillInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_RefillInfo.Name = "pnl_RefillInfo"
        Me.pnl_RefillInfo.Size = New System.Drawing.Size(1175, 1273)
        Me.pnl_RefillInfo.TabIndex = 3
        '
        'pnl_ProviderInfo
        '
        Me.pnl_ProviderInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnl_ProviderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderFax)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label66)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderNPI)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label68)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderAddress2)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label65)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_PrPhone)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label7)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_Provider)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderName)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderZIP)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderAdd)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderState)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderCity)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProvidCity)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProviderAddress)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProvidState)
        Me.pnl_ProviderInfo.Controls.Add(Me.lbl_ProvidZIP)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label53)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label54)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label55)
        Me.pnl_ProviderInfo.Controls.Add(Me.Label56)
        Me.pnl_ProviderInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_ProviderInfo.Location = New System.Drawing.Point(0, 1097)
        Me.pnl_ProviderInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_ProviderInfo.Name = "pnl_ProviderInfo"
        Me.pnl_ProviderInfo.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnl_ProviderInfo.Size = New System.Drawing.Size(1175, 176)
        Me.pnl_ProviderInfo.TabIndex = 13
        '
        'lbl_ProviderFax
        '
        Me.lbl_ProviderFax.AutoSize = True
        Me.lbl_ProviderFax.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderFax.Location = New System.Drawing.Point(626, 139)
        Me.lbl_ProviderFax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderFax.Name = "lbl_ProviderFax"
        Me.lbl_ProviderFax.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderFax.TabIndex = 30
        Me.lbl_ProviderFax.Text = "Label3"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.Color.Transparent
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(584, 139)
        Me.Label66.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(44, 18)
        Me.Label66.TabIndex = 29
        Me.Label66.Text = "Fax :"
        '
        'lbl_ProviderNPI
        '
        Me.lbl_ProviderNPI.AutoSize = True
        Me.lbl_ProviderNPI.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderNPI.Location = New System.Drawing.Point(180, 139)
        Me.lbl_ProviderNPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderNPI.Name = "lbl_ProviderNPI"
        Me.lbl_ProviderNPI.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderNPI.TabIndex = 28
        Me.lbl_ProviderNPI.Text = "Label3"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.Transparent
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(52, 136)
        Me.Label68.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(129, 22)
        Me.Label68.TabIndex = 27
        Me.Label68.Text = "NPI :"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ProviderAddress2
        '
        Me.lbl_ProviderAddress2.AutoSize = True
        Me.lbl_ProviderAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderAddress2.Location = New System.Drawing.Point(180, 74)
        Me.lbl_ProviderAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderAddress2.Name = "lbl_ProviderAddress2"
        Me.lbl_ProviderAddress2.Size = New System.Drawing.Size(68, 18)
        Me.lbl_ProviderAddress2.TabIndex = 26
        Me.lbl_ProviderAddress2.Text = "Address2"
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.Transparent
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(52, 71)
        Me.Label65.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(129, 22)
        Me.Label65.TabIndex = 25
        Me.Label65.Text = "Address 2 :"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PrPhone
        '
        Me.lbl_PrPhone.AutoSize = True
        Me.lbl_PrPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PrPhone.Location = New System.Drawing.Point(1026, 74)
        Me.lbl_PrPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PrPhone.Name = "lbl_PrPhone"
        Me.lbl_PrPhone.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PrPhone.TabIndex = 20
        Me.lbl_PrPhone.Text = "Label3"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(960, 74)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 18)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Phone :"
        '
        'lbl_Provider
        '
        Me.lbl_Provider.AutoSize = True
        Me.lbl_Provider.Location = New System.Drawing.Point(180, 9)
        Me.lbl_Provider.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Provider.Name = "lbl_Provider"
        Me.lbl_Provider.Size = New System.Drawing.Size(49, 18)
        Me.lbl_Provider.TabIndex = 6
        Me.lbl_Provider.Text = "Label8"
        '
        'lbl_ProviderName
        '
        Me.lbl_ProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderName.Location = New System.Drawing.Point(52, 6)
        Me.lbl_ProviderName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderName.Name = "lbl_ProviderName"
        Me.lbl_ProviderName.Size = New System.Drawing.Size(129, 22)
        Me.lbl_ProviderName.TabIndex = 10
        Me.lbl_ProviderName.Text = "Provider Name :"
        Me.lbl_ProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ProviderZIP
        '
        Me.lbl_ProviderZIP.AutoSize = True
        Me.lbl_ProviderZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderZIP.Location = New System.Drawing.Point(1026, 106)
        Me.lbl_ProviderZIP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderZIP.Name = "lbl_ProviderZIP"
        Me.lbl_ProviderZIP.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderZIP.TabIndex = 15
        Me.lbl_ProviderZIP.Text = "Label3"
        '
        'lbl_ProviderAdd
        '
        Me.lbl_ProviderAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderAdd.Location = New System.Drawing.Point(52, 39)
        Me.lbl_ProviderAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderAdd.Name = "lbl_ProviderAdd"
        Me.lbl_ProviderAdd.Size = New System.Drawing.Size(129, 22)
        Me.lbl_ProviderAdd.TabIndex = 9
        Me.lbl_ProviderAdd.Text = "Address 1 :"
        Me.lbl_ProviderAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ProviderState
        '
        Me.lbl_ProviderState.AutoSize = True
        Me.lbl_ProviderState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderState.Location = New System.Drawing.Point(626, 106)
        Me.lbl_ProviderState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderState.Name = "lbl_ProviderState"
        Me.lbl_ProviderState.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderState.TabIndex = 14
        Me.lbl_ProviderState.Text = "Label3"
        '
        'lbl_ProviderCity
        '
        Me.lbl_ProviderCity.AutoSize = True
        Me.lbl_ProviderCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderCity.Location = New System.Drawing.Point(180, 106)
        Me.lbl_ProviderCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderCity.Name = "lbl_ProviderCity"
        Me.lbl_ProviderCity.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderCity.TabIndex = 16
        Me.lbl_ProviderCity.Text = "Label3"
        '
        'lbl_ProvidCity
        '
        Me.lbl_ProvidCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidCity.Location = New System.Drawing.Point(52, 104)
        Me.lbl_ProvidCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProvidCity.Name = "lbl_ProvidCity"
        Me.lbl_ProvidCity.Size = New System.Drawing.Size(129, 22)
        Me.lbl_ProvidCity.TabIndex = 6
        Me.lbl_ProvidCity.Text = "City :"
        Me.lbl_ProvidCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ProviderAddress
        '
        Me.lbl_ProviderAddress.AutoSize = True
        Me.lbl_ProviderAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderAddress.Location = New System.Drawing.Point(180, 41)
        Me.lbl_ProviderAddress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProviderAddress.Name = "lbl_ProviderAddress"
        Me.lbl_ProviderAddress.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ProviderAddress.TabIndex = 17
        Me.lbl_ProviderAddress.Text = "Label3"
        '
        'lbl_ProvidState
        '
        Me.lbl_ProvidState.AutoSize = True
        Me.lbl_ProvidState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidState.Location = New System.Drawing.Point(566, 106)
        Me.lbl_ProvidState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProvidState.Name = "lbl_ProvidState"
        Me.lbl_ProvidState.Size = New System.Drawing.Size(57, 18)
        Me.lbl_ProvidState.TabIndex = 7
        Me.lbl_ProvidState.Text = "State :"
        '
        'lbl_ProvidZIP
        '
        Me.lbl_ProvidZIP.AutoSize = True
        Me.lbl_ProvidZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidZIP.Location = New System.Drawing.Point(984, 106)
        Me.lbl_ProvidZIP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ProvidZIP.Name = "lbl_ProvidZIP"
        Me.lbl_ProvidZIP.Size = New System.Drawing.Size(43, 18)
        Me.lbl_ProvidZIP.TabIndex = 8
        Me.lbl_ProvidZIP.Text = "ZIP :"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label53.Location = New System.Drawing.Point(1, 171)
        Me.Label53.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1169, 1)
        Me.Label53.TabIndex = 24
        Me.Label53.Text = "label2"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(0, 1)
        Me.Label54.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 171)
        Me.Label54.TabIndex = 23
        Me.Label54.Text = "label4"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label55.Location = New System.Drawing.Point(1170, 1)
        Me.Label55.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1, 171)
        Me.Label55.TabIndex = 22
        Me.Label55.Text = "label3"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(0, 0)
        Me.Label56.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1171, 1)
        Me.Label56.TabIndex = 21
        Me.Label56.Text = "label1"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Panel13)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 1066)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.Panel7.Size = New System.Drawing.Size(1175, 31)
        Me.Panel7.TabIndex = 18
        '
        'Panel13
        '
        Me.Panel13.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.Label5)
        Me.Panel13.Controls.Add(Me.Label57)
        Me.Panel13.Controls.Add(Me.Label59)
        Me.Panel13.Controls.Add(Me.Label60)
        Me.Panel13.Controls.Add(Me.Label58)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1171, 27)
        Me.Panel13.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Image = CType(resources.GetObject("Label5.Image"), System.Drawing.Image)
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1169, 25)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "      Provider Information"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(1, 26)
        Me.Label57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1169, 1)
        Me.Label57.TabIndex = 8
        Me.Label57.Text = "label2"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label59.Location = New System.Drawing.Point(1170, 1)
        Me.Label59.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(1, 26)
        Me.Label59.TabIndex = 6
        Me.Label59.Text = "label3"
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(1, 0)
        Me.Label60.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1170, 1)
        Me.Label60.TabIndex = 5
        Me.Label60.Text = "label1"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(0, 0)
        Me.Label58.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 27)
        Me.Label58.TabIndex = 7
        Me.Label58.Text = "label4"
        '
        'pnl_PharmacyInfo
        '
        Me.pnl_PharmacyInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnl_PharmacyInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyFax)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label63)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyNPI)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label62)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyAddress2)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label61)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyZip)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label1)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_Pharmacy)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyName)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyZIPCode)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyAdd)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyState)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyPhoneNo)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharamcyCity)
        Me.pnl_PharmacyInfo.Controls.Add(Me.pbl_PharmacyPhone)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmCity)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmacyAddress)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmState)
        Me.pnl_PharmacyInfo.Controls.Add(Me.lbl_PharmZIPCode)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label49)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label50)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label51)
        Me.pnl_PharmacyInfo.Controls.Add(Me.Label52)
        Me.pnl_PharmacyInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_PharmacyInfo.Location = New System.Drawing.Point(0, 897)
        Me.pnl_PharmacyInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_PharmacyInfo.Name = "pnl_PharmacyInfo"
        Me.pnl_PharmacyInfo.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnl_PharmacyInfo.Size = New System.Drawing.Size(1175, 169)
        Me.pnl_PharmacyInfo.TabIndex = 12
        '
        'lbl_PharmacyFax
        '
        Me.lbl_PharmacyFax.AutoSize = True
        Me.lbl_PharmacyFax.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyFax.Location = New System.Drawing.Point(626, 138)
        Me.lbl_PharmacyFax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyFax.Name = "lbl_PharmacyFax"
        Me.lbl_PharmacyFax.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyFax.TabIndex = 19
        Me.lbl_PharmacyFax.Text = "Label3"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.Color.Transparent
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(584, 138)
        Me.Label63.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(44, 18)
        Me.Label63.TabIndex = 18
        Me.Label63.Text = "Fax :"
        '
        'lbl_PharmacyNPI
        '
        Me.lbl_PharmacyNPI.AutoSize = True
        Me.lbl_PharmacyNPI.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyNPI.Location = New System.Drawing.Point(180, 138)
        Me.lbl_PharmacyNPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyNPI.Name = "lbl_PharmacyNPI"
        Me.lbl_PharmacyNPI.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyNPI.TabIndex = 17
        Me.lbl_PharmacyNPI.Text = "Label3"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(136, 135)
        Me.Label62.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(45, 22)
        Me.Label62.TabIndex = 16
        Me.Label62.Text = "NPI :"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PharmacyAddress2
        '
        Me.lbl_PharmacyAddress2.AutoSize = True
        Me.lbl_PharmacyAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyAddress2.Location = New System.Drawing.Point(180, 75)
        Me.lbl_PharmacyAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyAddress2.Name = "lbl_PharmacyAddress2"
        Me.lbl_PharmacyAddress2.Size = New System.Drawing.Size(130, 18)
        Me.lbl_PharmacyAddress2.TabIndex = 15
        Me.lbl_PharmacyAddress2.Text = "Pharmacy Address"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(86, 72)
        Me.Label61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(95, 22)
        Me.Label61.TabIndex = 14
        Me.Label61.Text = "Address 2 :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PharmacyZip
        '
        Me.lbl_PharmacyZip.AutoSize = True
        Me.lbl_PharmacyZip.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyZip.Location = New System.Drawing.Point(1026, 106)
        Me.lbl_PharmacyZip.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyZip.Name = "lbl_PharmacyZip"
        Me.lbl_PharmacyZip.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyZip.TabIndex = 8
        Me.lbl_PharmacyZip.Text = "Label3"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(984, 106)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 18)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "ZIP :"
        '
        'lbl_Pharmacy
        '
        Me.lbl_Pharmacy.AutoSize = True
        Me.lbl_Pharmacy.Location = New System.Drawing.Point(180, 12)
        Me.lbl_Pharmacy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Pharmacy.Name = "lbl_Pharmacy"
        Me.lbl_Pharmacy.Size = New System.Drawing.Size(117, 18)
        Me.lbl_Pharmacy.TabIndex = 6
        Me.lbl_Pharmacy.Text = "Pharmacy Name"
        '
        'lbl_PharmacyName
        '
        Me.lbl_PharmacyName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyName.Location = New System.Drawing.Point(42, 10)
        Me.lbl_PharmacyName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyName.Name = "lbl_PharmacyName"
        Me.lbl_PharmacyName.Size = New System.Drawing.Size(139, 22)
        Me.lbl_PharmacyName.TabIndex = 0
        Me.lbl_PharmacyName.Text = "Pharmacy Name :"
        Me.lbl_PharmacyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PharmacyZIPCode
        '
        Me.lbl_PharmacyZIPCode.AutoSize = True
        Me.lbl_PharmacyZIPCode.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyZIPCode.Location = New System.Drawing.Point(1334, 14)
        Me.lbl_PharmacyZIPCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyZIPCode.Name = "lbl_PharmacyZIPCode"
        Me.lbl_PharmacyZIPCode.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyZIPCode.TabIndex = 5
        Me.lbl_PharmacyZIPCode.Text = "Label3"
        Me.lbl_PharmacyZIPCode.Visible = False
        '
        'lbl_PharmacyAdd
        '
        Me.lbl_PharmacyAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyAdd.Location = New System.Drawing.Point(86, 41)
        Me.lbl_PharmacyAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyAdd.Name = "lbl_PharmacyAdd"
        Me.lbl_PharmacyAdd.Size = New System.Drawing.Size(95, 22)
        Me.lbl_PharmacyAdd.TabIndex = 0
        Me.lbl_PharmacyAdd.Text = "Address 1 :"
        Me.lbl_PharmacyAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PharmacyState
        '
        Me.lbl_PharmacyState.AutoSize = True
        Me.lbl_PharmacyState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyState.Location = New System.Drawing.Point(626, 106)
        Me.lbl_PharmacyState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyState.Name = "lbl_PharmacyState"
        Me.lbl_PharmacyState.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyState.TabIndex = 5
        Me.lbl_PharmacyState.Text = "Label3"
        '
        'lbl_PharmacyPhoneNo
        '
        Me.lbl_PharmacyPhoneNo.AutoSize = True
        Me.lbl_PharmacyPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyPhoneNo.Location = New System.Drawing.Point(1026, 75)
        Me.lbl_PharmacyPhoneNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyPhoneNo.Name = "lbl_PharmacyPhoneNo"
        Me.lbl_PharmacyPhoneNo.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharmacyPhoneNo.TabIndex = 5
        Me.lbl_PharmacyPhoneNo.Text = "Label3"
        '
        'lbl_PharamcyCity
        '
        Me.lbl_PharamcyCity.AutoSize = True
        Me.lbl_PharamcyCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharamcyCity.Location = New System.Drawing.Point(180, 106)
        Me.lbl_PharamcyCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharamcyCity.Name = "lbl_PharamcyCity"
        Me.lbl_PharamcyCity.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PharamcyCity.TabIndex = 5
        Me.lbl_PharamcyCity.Text = "Label3"
        '
        'pbl_PharmacyPhone
        '
        Me.pbl_PharmacyPhone.AutoSize = True
        Me.pbl_PharmacyPhone.BackColor = System.Drawing.Color.Transparent
        Me.pbl_PharmacyPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pbl_PharmacyPhone.Location = New System.Drawing.Point(960, 75)
        Me.pbl_PharmacyPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.pbl_PharmacyPhone.Name = "pbl_PharmacyPhone"
        Me.pbl_PharmacyPhone.Size = New System.Drawing.Size(63, 18)
        Me.pbl_PharmacyPhone.TabIndex = 0
        Me.pbl_PharmacyPhone.Text = "Phone :"
        '
        'lbl_PharmCity
        '
        Me.lbl_PharmCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmCity.Location = New System.Drawing.Point(132, 104)
        Me.lbl_PharmCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmCity.Name = "lbl_PharmCity"
        Me.lbl_PharmCity.Size = New System.Drawing.Size(49, 22)
        Me.lbl_PharmCity.TabIndex = 0
        Me.lbl_PharmCity.Text = "City :"
        Me.lbl_PharmCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_PharmacyAddress
        '
        Me.lbl_PharmacyAddress.AutoSize = True
        Me.lbl_PharmacyAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyAddress.Location = New System.Drawing.Point(180, 44)
        Me.lbl_PharmacyAddress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmacyAddress.Name = "lbl_PharmacyAddress"
        Me.lbl_PharmacyAddress.Size = New System.Drawing.Size(130, 18)
        Me.lbl_PharmacyAddress.TabIndex = 5
        Me.lbl_PharmacyAddress.Text = "Pharmacy Address"
        '
        'lbl_PharmState
        '
        Me.lbl_PharmState.AutoSize = True
        Me.lbl_PharmState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmState.Location = New System.Drawing.Point(566, 106)
        Me.lbl_PharmState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmState.Name = "lbl_PharmState"
        Me.lbl_PharmState.Size = New System.Drawing.Size(57, 18)
        Me.lbl_PharmState.TabIndex = 0
        Me.lbl_PharmState.Text = "State :"
        '
        'lbl_PharmZIPCode
        '
        Me.lbl_PharmZIPCode.AutoSize = True
        Me.lbl_PharmZIPCode.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmZIPCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmZIPCode.Location = New System.Drawing.Point(1281, 14)
        Me.lbl_PharmZIPCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PharmZIPCode.Name = "lbl_PharmZIPCode"
        Me.lbl_PharmZIPCode.Size = New System.Drawing.Size(47, 17)
        Me.lbl_PharmZIPCode.TabIndex = 0
        Me.lbl_PharmZIPCode.Text = "ZIP :"
        Me.lbl_PharmZIPCode.Visible = False
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label49.Location = New System.Drawing.Point(1, 164)
        Me.Label49.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1169, 1)
        Me.Label49.TabIndex = 13
        Me.Label49.Text = "label2"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(0, 1)
        Me.Label50.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 164)
        Me.Label50.TabIndex = 12
        Me.Label50.Text = "label4"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label51.Location = New System.Drawing.Point(1170, 1)
        Me.Label51.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 164)
        Me.Label51.TabIndex = 11
        Me.Label51.Text = "label3"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(0, 0)
        Me.Label52.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1171, 1)
        Me.Label52.TabIndex = 10
        Me.Label52.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Panel12)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 866)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.Panel6.Size = New System.Drawing.Size(1175, 31)
        Me.Panel6.TabIndex = 9
        '
        'Panel12
        '
        Me.Panel12.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label45)
        Me.Panel12.Controls.Add(Me.Label46)
        Me.Panel12.Controls.Add(Me.Label47)
        Me.Panel12.Controls.Add(Me.Label4)
        Me.Panel12.Controls.Add(Me.Label48)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(1171, 27)
        Me.Panel12.TabIndex = 9
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(1, 26)
        Me.Label45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1169, 1)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 26)
        Me.Label46.TabIndex = 7
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(1170, 1)
        Me.Label47.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 26)
        Me.Label47.TabIndex = 6
        Me.Label47.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1171, 26)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "      Pharmacy Information"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1171, 1)
        Me.Label48.TabIndex = 5
        Me.Label48.Text = "label1"
        '
        'pnl_PatientInfo
        '
        Me.pnl_PatientInfo.BackColor = System.Drawing.Color.Transparent
        Me.pnl_PatientInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatientAddress2)
        Me.pnl_PatientInfo.Controls.Add(Me.Label28)
        Me.pnl_PatientInfo.Controls.Add(Me.Label37)
        Me.pnl_PatientInfo.Controls.Add(Me.Label38)
        Me.pnl_PatientInfo.Controls.Add(Me.Label39)
        Me.pnl_PatientInfo.Controls.Add(Me.Label40)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_Patient)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatientName)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_ZIPCode)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatAdd)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatGender)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatDOB)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_StateName)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatientPhoneNo)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_CityName)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatientPhone)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_City)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_PatientAddress)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_State)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_Gender)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_ZIP)
        Me.pnl_PatientInfo.Controls.Add(Me.lbl_DOB)
        Me.pnl_PatientInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_PatientInfo.Location = New System.Drawing.Point(0, 725)
        Me.pnl_PatientInfo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_PatientInfo.Name = "pnl_PatientInfo"
        Me.pnl_PatientInfo.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnl_PatientInfo.Size = New System.Drawing.Size(1175, 141)
        Me.pnl_PatientInfo.TabIndex = 11
        '
        'lbl_PatientAddress2
        '
        Me.lbl_PatientAddress2.AutoSize = True
        Me.lbl_PatientAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientAddress2.Location = New System.Drawing.Point(180, 76)
        Me.lbl_PatientAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatientAddress2.Name = "lbl_PatientAddress2"
        Me.lbl_PatientAddress2.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PatientAddress2.TabIndex = 12
        Me.lbl_PatientAddress2.Text = "Label3"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(86, 74)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(95, 22)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "Address 2 :"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(1, 136)
        Me.Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1169, 1)
        Me.Label37.TabIndex = 10
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 1)
        Me.Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 136)
        Me.Label38.TabIndex = 9
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(1170, 1)
        Me.Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 136)
        Me.Label39.TabIndex = 8
        Me.Label39.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1171, 1)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "label1"
        '
        'lbl_Patient
        '
        Me.lbl_Patient.AutoSize = True
        Me.lbl_Patient.Location = New System.Drawing.Point(180, 14)
        Me.lbl_Patient.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Patient.Name = "lbl_Patient"
        Me.lbl_Patient.Size = New System.Drawing.Size(49, 18)
        Me.lbl_Patient.TabIndex = 6
        Me.lbl_Patient.Text = "Label8"
        '
        'lbl_PatientName
        '
        Me.lbl_PatientName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientName.Location = New System.Drawing.Point(60, 11)
        Me.lbl_PatientName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatientName.Name = "lbl_PatientName"
        Me.lbl_PatientName.Size = New System.Drawing.Size(121, 22)
        Me.lbl_PatientName.TabIndex = 0
        Me.lbl_PatientName.Text = "Patient Name :"
        '
        'lbl_ZIPCode
        '
        Me.lbl_ZIPCode.AutoSize = True
        Me.lbl_ZIPCode.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ZIPCode.Location = New System.Drawing.Point(1026, 108)
        Me.lbl_ZIPCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ZIPCode.Name = "lbl_ZIPCode"
        Me.lbl_ZIPCode.Size = New System.Drawing.Size(49, 18)
        Me.lbl_ZIPCode.TabIndex = 5
        Me.lbl_ZIPCode.Text = "Label3"
        '
        'lbl_PatAdd
        '
        Me.lbl_PatAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatAdd.Location = New System.Drawing.Point(86, 42)
        Me.lbl_PatAdd.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatAdd.Name = "lbl_PatAdd"
        Me.lbl_PatAdd.Size = New System.Drawing.Size(95, 22)
        Me.lbl_PatAdd.TabIndex = 0
        Me.lbl_PatAdd.Text = "Address 1 :"
        '
        'lbl_PatGender
        '
        Me.lbl_PatGender.AutoSize = True
        Me.lbl_PatGender.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatGender.Location = New System.Drawing.Point(1026, 14)
        Me.lbl_PatGender.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatGender.Name = "lbl_PatGender"
        Me.lbl_PatGender.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PatGender.TabIndex = 5
        Me.lbl_PatGender.Text = "Label3"
        '
        'lbl_PatDOB
        '
        Me.lbl_PatDOB.AutoSize = True
        Me.lbl_PatDOB.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatDOB.Location = New System.Drawing.Point(626, 14)
        Me.lbl_PatDOB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatDOB.Name = "lbl_PatDOB"
        Me.lbl_PatDOB.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PatDOB.TabIndex = 5
        Me.lbl_PatDOB.Text = "Label3"
        '
        'lbl_StateName
        '
        Me.lbl_StateName.AutoSize = True
        Me.lbl_StateName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_StateName.Location = New System.Drawing.Point(626, 108)
        Me.lbl_StateName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_StateName.Name = "lbl_StateName"
        Me.lbl_StateName.Size = New System.Drawing.Size(49, 18)
        Me.lbl_StateName.TabIndex = 5
        Me.lbl_StateName.Text = "Label3"
        '
        'lbl_PatientPhoneNo
        '
        Me.lbl_PatientPhoneNo.AutoSize = True
        Me.lbl_PatientPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientPhoneNo.Location = New System.Drawing.Point(1026, 76)
        Me.lbl_PatientPhoneNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatientPhoneNo.Name = "lbl_PatientPhoneNo"
        Me.lbl_PatientPhoneNo.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PatientPhoneNo.TabIndex = 5
        Me.lbl_PatientPhoneNo.Text = "Label3"
        '
        'lbl_CityName
        '
        Me.lbl_CityName.AutoSize = True
        Me.lbl_CityName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_CityName.Location = New System.Drawing.Point(180, 108)
        Me.lbl_CityName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_CityName.Name = "lbl_CityName"
        Me.lbl_CityName.Size = New System.Drawing.Size(49, 18)
        Me.lbl_CityName.TabIndex = 5
        Me.lbl_CityName.Text = "Label3"
        '
        'lbl_PatientPhone
        '
        Me.lbl_PatientPhone.AutoSize = True
        Me.lbl_PatientPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientPhone.Location = New System.Drawing.Point(960, 76)
        Me.lbl_PatientPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatientPhone.Name = "lbl_PatientPhone"
        Me.lbl_PatientPhone.Size = New System.Drawing.Size(63, 18)
        Me.lbl_PatientPhone.TabIndex = 0
        Me.lbl_PatientPhone.Text = "Phone :"
        '
        'lbl_City
        '
        Me.lbl_City.BackColor = System.Drawing.Color.Transparent
        Me.lbl_City.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_City.Location = New System.Drawing.Point(132, 105)
        Me.lbl_City.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_City.Name = "lbl_City"
        Me.lbl_City.Size = New System.Drawing.Size(49, 22)
        Me.lbl_City.TabIndex = 0
        Me.lbl_City.Text = "City :"
        '
        'lbl_PatientAddress
        '
        Me.lbl_PatientAddress.AutoSize = True
        Me.lbl_PatientAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientAddress.Location = New System.Drawing.Point(180, 45)
        Me.lbl_PatientAddress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_PatientAddress.Name = "lbl_PatientAddress"
        Me.lbl_PatientAddress.Size = New System.Drawing.Size(49, 18)
        Me.lbl_PatientAddress.TabIndex = 5
        Me.lbl_PatientAddress.Text = "Label3"
        '
        'lbl_State
        '
        Me.lbl_State.AutoSize = True
        Me.lbl_State.BackColor = System.Drawing.Color.Transparent
        Me.lbl_State.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_State.Location = New System.Drawing.Point(566, 108)
        Me.lbl_State.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_State.Name = "lbl_State"
        Me.lbl_State.Size = New System.Drawing.Size(57, 18)
        Me.lbl_State.TabIndex = 0
        Me.lbl_State.Text = "State :"
        '
        'lbl_Gender
        '
        Me.lbl_Gender.AutoSize = True
        Me.lbl_Gender.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Gender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Gender.Location = New System.Drawing.Point(955, 14)
        Me.lbl_Gender.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Gender.Name = "lbl_Gender"
        Me.lbl_Gender.Size = New System.Drawing.Size(71, 18)
        Me.lbl_Gender.TabIndex = 3
        Me.lbl_Gender.Text = "Gender :"
        '
        'lbl_ZIP
        '
        Me.lbl_ZIP.AutoSize = True
        Me.lbl_ZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_ZIP.Location = New System.Drawing.Point(984, 108)
        Me.lbl_ZIP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ZIP.Name = "lbl_ZIP"
        Me.lbl_ZIP.Size = New System.Drawing.Size(43, 18)
        Me.lbl_ZIP.TabIndex = 0
        Me.lbl_ZIP.Text = "ZIP :"
        '
        'lbl_DOB
        '
        Me.lbl_DOB.AutoSize = True
        Me.lbl_DOB.BackColor = System.Drawing.Color.Transparent
        Me.lbl_DOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DOB.Location = New System.Drawing.Point(508, 14)
        Me.lbl_DOB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_DOB.Name = "lbl_DOB"
        Me.lbl_DOB.Size = New System.Drawing.Size(115, 18)
        Me.lbl_DOB.TabIndex = 3
        Me.lbl_DOB.Text = "Date Of Birth :"
        '
        'pnlRefillinfotitle
        '
        Me.pnlRefillinfotitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRefillinfotitle.Controls.Add(Me.Panel11)
        Me.pnlRefillinfotitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRefillinfotitle.Location = New System.Drawing.Point(0, 694)
        Me.pnlRefillinfotitle.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlRefillinfotitle.Name = "pnlRefillinfotitle"
        Me.pnlRefillinfotitle.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnlRefillinfotitle.Size = New System.Drawing.Size(1175, 31)
        Me.pnlRefillinfotitle.TabIndex = 0
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label41)
        Me.Panel11.Controls.Add(Me.Label42)
        Me.Panel11.Controls.Add(Me.Label43)
        Me.Panel11.Controls.Add(Me.Label44)
        Me.Panel11.Controls.Add(Me.Label3)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1171, 27)
        Me.Panel11.TabIndex = 9
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(1, 26)
        Me.Label41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1169, 1)
        Me.Label41.TabIndex = 8
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 26)
        Me.Label42.TabIndex = 7
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1170, 1)
        Me.Label43.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 26)
        Me.Label43.TabIndex = 6
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1171, 1)
        Me.Label44.TabIndex = 5
        Me.Label44.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1171, 27)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "      Patient Information"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_MedicalDispanced
        '
        Me.pnl_MedicalDispanced.BackColor = System.Drawing.Color.Transparent
        Me.pnl_MedicalDispanced.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_MedicalDispanced.Controls.Add(Me.lbl_MDRef_Qlfr)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label93)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lbl_MDDuration)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label92)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lbl_MDLastFillDate)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label91)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label94)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label95)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label96)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label97)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblDrugName_Strength_Dosageform)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label98)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblSubstitution)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label99)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblDrugQuantity)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblWrittenDate)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblRefillQuantity)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblDrugNotes)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label100)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label101)
        Me.pnl_MedicalDispanced.Controls.Add(Me.lblDirection)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label102)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label103)
        Me.pnl_MedicalDispanced.Controls.Add(Me.Label104)
        Me.pnl_MedicalDispanced.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_MedicalDispanced.Location = New System.Drawing.Point(0, 434)
        Me.pnl_MedicalDispanced.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_MedicalDispanced.Name = "pnl_MedicalDispanced"
        Me.pnl_MedicalDispanced.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnl_MedicalDispanced.Size = New System.Drawing.Size(1175, 260)
        Me.pnl_MedicalDispanced.TabIndex = 26
        '
        'lbl_MDRef_Qlfr
        '
        Me.lbl_MDRef_Qlfr.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MDRef_Qlfr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MDRef_Qlfr.Location = New System.Drawing.Point(626, 105)
        Me.lbl_MDRef_Qlfr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MDRef_Qlfr.Name = "lbl_MDRef_Qlfr"
        Me.lbl_MDRef_Qlfr.Size = New System.Drawing.Size(38, 18)
        Me.lbl_MDRef_Qlfr.TabIndex = 20
        Me.lbl_MDRef_Qlfr.Text = "Label3"
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.BackColor = System.Drawing.Color.Transparent
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.Location = New System.Drawing.Point(545, 105)
        Me.Label93.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(82, 18)
        Me.Label93.TabIndex = 19
        Me.Label93.Text = "Ref. Qlfr :"
        '
        'lbl_MDDuration
        '
        Me.lbl_MDDuration.AutoSize = True
        Me.lbl_MDDuration.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MDDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MDDuration.Location = New System.Drawing.Point(626, 135)
        Me.lbl_MDDuration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MDDuration.Name = "lbl_MDDuration"
        Me.lbl_MDDuration.Size = New System.Drawing.Size(49, 18)
        Me.lbl_MDDuration.TabIndex = 18
        Me.lbl_MDDuration.Text = "Label3"
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.BackColor = System.Drawing.Color.Transparent
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.Location = New System.Drawing.Point(541, 135)
        Me.Label92.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(82, 18)
        Me.Label92.TabIndex = 17
        Me.Label92.Text = "Duration :"
        '
        'lbl_MDLastFillDate
        '
        Me.lbl_MDLastFillDate.AutoSize = True
        Me.lbl_MDLastFillDate.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MDLastFillDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MDLastFillDate.Location = New System.Drawing.Point(626, 75)
        Me.lbl_MDLastFillDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MDLastFillDate.Name = "lbl_MDLastFillDate"
        Me.lbl_MDLastFillDate.Size = New System.Drawing.Size(49, 18)
        Me.lbl_MDLastFillDate.TabIndex = 16
        Me.lbl_MDLastFillDate.Text = "Label3"
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.BackColor = System.Drawing.Color.Transparent
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.Location = New System.Drawing.Point(511, 75)
        Me.Label91.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(116, 18)
        Me.Label91.TabIndex = 15
        Me.Label91.Text = "Last Fill Date :"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label94.Location = New System.Drawing.Point(1, 255)
        Me.Label94.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1169, 1)
        Me.Label94.TabIndex = 10
        Me.Label94.Text = "label2"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(0, 1)
        Me.Label95.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 255)
        Me.Label95.TabIndex = 9
        Me.Label95.Text = "label4"
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label96.Location = New System.Drawing.Point(1170, 1)
        Me.Label96.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 255)
        Me.Label96.TabIndex = 8
        Me.Label96.Text = "label3"
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(0, 0)
        Me.Label97.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1171, 1)
        Me.Label97.TabIndex = 7
        Me.Label97.Text = "label1"
        '
        'lblDrugName_Strength_Dosageform
        '
        Me.lblDrugName_Strength_Dosageform.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugName_Strength_Dosageform.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugName_Strength_Dosageform.Location = New System.Drawing.Point(180, 11)
        Me.lblDrugName_Strength_Dosageform.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDrugName_Strength_Dosageform.Name = "lblDrugName_Strength_Dosageform"
        Me.lblDrugName_Strength_Dosageform.Size = New System.Drawing.Size(1088, 51)
        Me.lblDrugName_Strength_Dosageform.TabIndex = 6
        Me.lblDrugName_Strength_Dosageform.Text = "Label8"
        Me.lblDrugName_Strength_Dosageform.UseMnemonic = False
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.Transparent
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(125, 11)
        Me.Label98.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(56, 22)
        Me.Label98.TabIndex = 0
        Me.Label98.Text = "Drug :"
        '
        'lblSubstitution
        '
        Me.lblSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubstitution.Location = New System.Drawing.Point(626, 165)
        Me.lblSubstitution.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubstitution.Name = "lblSubstitution"
        Me.lblSubstitution.Size = New System.Drawing.Size(34, 18)
        Me.lblSubstitution.TabIndex = 5
        Me.lblSubstitution.Text = "Label3"
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.Transparent
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.Location = New System.Drawing.Point(25, 162)
        Me.Label99.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(156, 22)
        Me.Label99.TabIndex = 0
        Me.Label99.Text = "Patient Directions :"
        '
        'lblDrugQuantity
        '
        Me.lblDrugQuantity.AutoSize = True
        Me.lblDrugQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugQuantity.Location = New System.Drawing.Point(180, 135)
        Me.lblDrugQuantity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDrugQuantity.Name = "lblDrugQuantity"
        Me.lblDrugQuantity.Size = New System.Drawing.Size(49, 18)
        Me.lblDrugQuantity.TabIndex = 5
        Me.lblDrugQuantity.Text = "Label3"
        '
        'lblWrittenDate
        '
        Me.lblWrittenDate.AutoSize = True
        Me.lblWrittenDate.BackColor = System.Drawing.Color.Transparent
        Me.lblWrittenDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWrittenDate.Location = New System.Drawing.Point(180, 75)
        Me.lblWrittenDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWrittenDate.Name = "lblWrittenDate"
        Me.lblWrittenDate.Size = New System.Drawing.Size(49, 18)
        Me.lblWrittenDate.TabIndex = 5
        Me.lblWrittenDate.Text = "Label3"
        '
        'lblRefillQuantity
        '
        Me.lblRefillQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lblRefillQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefillQuantity.Location = New System.Drawing.Point(180, 105)
        Me.lblRefillQuantity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefillQuantity.Name = "lblRefillQuantity"
        Me.lblRefillQuantity.Size = New System.Drawing.Size(46, 18)
        Me.lblRefillQuantity.TabIndex = 5
        Me.lblRefillQuantity.Text = "Label3"
        '
        'lblDrugNotes
        '
        Me.lblDrugNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugNotes.Location = New System.Drawing.Point(180, 195)
        Me.lblDrugNotes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDrugNotes.Name = "lblDrugNotes"
        Me.lblDrugNotes.Size = New System.Drawing.Size(1089, 50)
        Me.lblDrugNotes.TabIndex = 5
        Me.lblDrugNotes.Text = "Label3"
        Me.lblDrugNotes.UseMnemonic = False
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.Transparent
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.Location = New System.Drawing.Point(118, 102)
        Me.Label100.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(64, 22)
        Me.Label100.TabIndex = 0
        Me.Label100.Text = "Refills :"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.Transparent
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.Location = New System.Drawing.Point(40, 195)
        Me.Label101.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(141, 22)
        Me.Label101.TabIndex = 0
        Me.Label101.Text = "Pharmacy Notes :"
        '
        'lblDirection
        '
        Me.lblDirection.AutoSize = True
        Me.lblDirection.BackColor = System.Drawing.Color.Transparent
        Me.lblDirection.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirection.Location = New System.Drawing.Point(180, 165)
        Me.lblDirection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDirection.Name = "lblDirection"
        Me.lblDirection.Size = New System.Drawing.Size(49, 18)
        Me.lblDirection.TabIndex = 5
        Me.lblDirection.Text = "Label3"
        Me.lblDirection.UseMnemonic = False
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.Transparent
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.Location = New System.Drawing.Point(62, 72)
        Me.Label102.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(119, 22)
        Me.Label102.TabIndex = 0
        Me.Label102.Text = "Written Date :"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.Transparent
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.Location = New System.Drawing.Point(96, 132)
        Me.Label103.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(89, 22)
        Me.Label103.TabIndex = 3
        Me.Label103.Text = "Drug Qty :"
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.BackColor = System.Drawing.Color.Transparent
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.Location = New System.Drawing.Point(511, 165)
        Me.Label104.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(108, 18)
        Me.Label104.TabIndex = 0
        Me.Label104.Text = "Substitution :"
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Panel17)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 403)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.Panel16.Size = New System.Drawing.Size(1175, 31)
        Me.Panel16.TabIndex = 25
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel17.Controls.Add(Me.Label81)
        Me.Panel17.Controls.Add(Me.Label85)
        Me.Panel17.Controls.Add(Me.Label86)
        Me.Panel17.Controls.Add(Me.Label87)
        Me.Panel17.Controls.Add(Me.Label90)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(1171, 27)
        Me.Panel17.TabIndex = 0
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.Transparent
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Image = CType(resources.GetObject("Label81.Image"), System.Drawing.Image)
        Me.Label81.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label81.Location = New System.Drawing.Point(1, 1)
        Me.Label81.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1169, 25)
        Me.Label81.TabIndex = 1
        Me.Label81.Text = "      Medication Dispensed"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label85.Location = New System.Drawing.Point(1, 26)
        Me.Label85.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1169, 1)
        Me.Label85.TabIndex = 8
        Me.Label85.Text = "label2"
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.Location = New System.Drawing.Point(0, 1)
        Me.Label86.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1, 26)
        Me.Label86.TabIndex = 7
        Me.Label86.Text = "label4"
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label87.Location = New System.Drawing.Point(1170, 1)
        Me.Label87.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 26)
        Me.Label87.TabIndex = 6
        Me.Label87.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1171, 1)
        Me.Label90.TabIndex = 5
        Me.Label90.Text = "label1"
        '
        'pnl_MedicalPrescribe
        '
        Me.pnl_MedicalPrescribe.BackColor = System.Drawing.Color.Transparent
        Me.pnl_MedicalPrescribe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_Ref_Qlfr)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label77)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_Duration)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label89)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_LastFillDate)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label88)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPDirection)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPSubstitution)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label72)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label73)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label74)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label75)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPDrugName_Strength_Dosageform)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label76)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label78)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPDrugQuantity)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPWrittenDate)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPRefillQuantity)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.lbl_MPDrugnotes)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label79)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label80)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label82)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label83)
        Me.pnl_MedicalPrescribe.Controls.Add(Me.Label84)
        Me.pnl_MedicalPrescribe.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_MedicalPrescribe.Location = New System.Drawing.Point(0, 143)
        Me.pnl_MedicalPrescribe.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnl_MedicalPrescribe.Name = "pnl_MedicalPrescribe"
        Me.pnl_MedicalPrescribe.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnl_MedicalPrescribe.Size = New System.Drawing.Size(1175, 260)
        Me.pnl_MedicalPrescribe.TabIndex = 24
        '
        'lbl_Ref_Qlfr
        '
        Me.lbl_Ref_Qlfr.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Ref_Qlfr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Ref_Qlfr.Location = New System.Drawing.Point(626, 106)
        Me.lbl_Ref_Qlfr.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Ref_Qlfr.Name = "lbl_Ref_Qlfr"
        Me.lbl_Ref_Qlfr.Size = New System.Drawing.Size(38, 18)
        Me.lbl_Ref_Qlfr.TabIndex = 18
        Me.lbl_Ref_Qlfr.Text = "Label3"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.BackColor = System.Drawing.Color.Transparent
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(545, 106)
        Me.Label77.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(82, 18)
        Me.Label77.TabIndex = 17
        Me.Label77.Text = "Ref. Qlfr :"
        '
        'lbl_Duration
        '
        Me.lbl_Duration.AutoSize = True
        Me.lbl_Duration.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Duration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Duration.Location = New System.Drawing.Point(626, 136)
        Me.lbl_Duration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Duration.Name = "lbl_Duration"
        Me.lbl_Duration.Size = New System.Drawing.Size(49, 18)
        Me.lbl_Duration.TabIndex = 16
        Me.lbl_Duration.Text = "Label3"
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.BackColor = System.Drawing.Color.Transparent
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.Location = New System.Drawing.Point(541, 136)
        Me.Label89.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(82, 18)
        Me.Label89.TabIndex = 15
        Me.Label89.Text = "Duration :"
        '
        'lbl_LastFillDate
        '
        Me.lbl_LastFillDate.AutoSize = True
        Me.lbl_LastFillDate.BackColor = System.Drawing.Color.Transparent
        Me.lbl_LastFillDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LastFillDate.Location = New System.Drawing.Point(626, 76)
        Me.lbl_LastFillDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_LastFillDate.Name = "lbl_LastFillDate"
        Me.lbl_LastFillDate.Size = New System.Drawing.Size(49, 18)
        Me.lbl_LastFillDate.TabIndex = 14
        Me.lbl_LastFillDate.Text = "Label3"
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.BackColor = System.Drawing.Color.Transparent
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(511, 76)
        Me.Label88.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(116, 18)
        Me.Label88.TabIndex = 13
        Me.Label88.Text = "Last Fill Date :"
        '
        'lbl_MPDirection
        '
        Me.lbl_MPDirection.AutoSize = True
        Me.lbl_MPDirection.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPDirection.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPDirection.Location = New System.Drawing.Point(180, 166)
        Me.lbl_MPDirection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPDirection.Name = "lbl_MPDirection"
        Me.lbl_MPDirection.Size = New System.Drawing.Size(49, 18)
        Me.lbl_MPDirection.TabIndex = 12
        Me.lbl_MPDirection.Text = "Label3"
        Me.lbl_MPDirection.UseMnemonic = False
        '
        'lbl_MPSubstitution
        '
        Me.lbl_MPSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPSubstitution.Location = New System.Drawing.Point(626, 166)
        Me.lbl_MPSubstitution.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPSubstitution.Name = "lbl_MPSubstitution"
        Me.lbl_MPSubstitution.Size = New System.Drawing.Size(34, 18)
        Me.lbl_MPSubstitution.TabIndex = 11
        Me.lbl_MPSubstitution.Text = "Label3"
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label72.Location = New System.Drawing.Point(1, 255)
        Me.Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1169, 1)
        Me.Label72.TabIndex = 10
        Me.Label72.Text = "label2"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(0, 1)
        Me.Label73.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1, 255)
        Me.Label73.TabIndex = 9
        Me.Label73.Text = "label4"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label74.Location = New System.Drawing.Point(1170, 1)
        Me.Label74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1, 255)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "label3"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 0)
        Me.Label75.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1171, 1)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "label1"
        '
        'lbl_MPDrugName_Strength_Dosageform
        '
        Me.lbl_MPDrugName_Strength_Dosageform.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPDrugName_Strength_Dosageform.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPDrugName_Strength_Dosageform.Location = New System.Drawing.Point(180, 12)
        Me.lbl_MPDrugName_Strength_Dosageform.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPDrugName_Strength_Dosageform.Name = "lbl_MPDrugName_Strength_Dosageform"
        Me.lbl_MPDrugName_Strength_Dosageform.Size = New System.Drawing.Size(1088, 51)
        Me.lbl_MPDrugName_Strength_Dosageform.TabIndex = 6
        Me.lbl_MPDrugName_Strength_Dosageform.Text = "Label8"
        Me.lbl_MPDrugName_Strength_Dosageform.UseMnemonic = False
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.Transparent
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(25, 10)
        Me.Label76.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(156, 22)
        Me.Label76.TabIndex = 0
        Me.Label76.Text = "Drug :"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.Transparent
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(25, 164)
        Me.Label78.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(156, 22)
        Me.Label78.TabIndex = 0
        Me.Label78.Text = "Patient Directions :"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_MPDrugQuantity
        '
        Me.lbl_MPDrugQuantity.AutoSize = True
        Me.lbl_MPDrugQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPDrugQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPDrugQuantity.Location = New System.Drawing.Point(180, 136)
        Me.lbl_MPDrugQuantity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPDrugQuantity.Name = "lbl_MPDrugQuantity"
        Me.lbl_MPDrugQuantity.Size = New System.Drawing.Size(49, 18)
        Me.lbl_MPDrugQuantity.TabIndex = 5
        Me.lbl_MPDrugQuantity.Text = "Label3"
        '
        'lbl_MPWrittenDate
        '
        Me.lbl_MPWrittenDate.AutoSize = True
        Me.lbl_MPWrittenDate.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPWrittenDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPWrittenDate.Location = New System.Drawing.Point(180, 76)
        Me.lbl_MPWrittenDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPWrittenDate.Name = "lbl_MPWrittenDate"
        Me.lbl_MPWrittenDate.Size = New System.Drawing.Size(49, 18)
        Me.lbl_MPWrittenDate.TabIndex = 5
        Me.lbl_MPWrittenDate.Text = "Label3"
        '
        'lbl_MPRefillQuantity
        '
        Me.lbl_MPRefillQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPRefillQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPRefillQuantity.Location = New System.Drawing.Point(180, 106)
        Me.lbl_MPRefillQuantity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPRefillQuantity.Name = "lbl_MPRefillQuantity"
        Me.lbl_MPRefillQuantity.Size = New System.Drawing.Size(46, 18)
        Me.lbl_MPRefillQuantity.TabIndex = 5
        Me.lbl_MPRefillQuantity.Text = "Label3"
        '
        'lbl_MPDrugnotes
        '
        Me.lbl_MPDrugnotes.BackColor = System.Drawing.Color.Transparent
        Me.lbl_MPDrugnotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MPDrugnotes.Location = New System.Drawing.Point(180, 196)
        Me.lbl_MPDrugnotes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_MPDrugnotes.Name = "lbl_MPDrugnotes"
        Me.lbl_MPDrugnotes.Size = New System.Drawing.Size(1089, 50)
        Me.lbl_MPDrugnotes.TabIndex = 5
        Me.lbl_MPDrugnotes.Text = "Label3"
        Me.lbl_MPDrugnotes.UseMnemonic = False
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(25, 104)
        Me.Label79.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(156, 22)
        Me.Label79.TabIndex = 0
        Me.Label79.Text = "Refills :"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.Transparent
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(25, 196)
        Me.Label80.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(156, 22)
        Me.Label80.TabIndex = 0
        Me.Label80.Text = "Pharmacy Notes :"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.Transparent
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(25, 74)
        Me.Label82.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(156, 22)
        Me.Label82.TabIndex = 0
        Me.Label82.Text = "Written Date :"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.Transparent
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(25, 134)
        Me.Label83.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(156, 22)
        Me.Label83.TabIndex = 3
        Me.Label83.Text = "Drug Qty :"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.Color.Transparent
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(511, 166)
        Me.Label84.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(108, 18)
        Me.Label84.TabIndex = 0
        Me.Label84.Text = "Substitution :"
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Pnl_MedicalPrescribedHeader)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Location = New System.Drawing.Point(0, 112)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.Panel15.Size = New System.Drawing.Size(1175, 31)
        Me.Panel15.TabIndex = 23
        '
        'Pnl_MedicalPrescribedHeader
        '
        Me.Pnl_MedicalPrescribedHeader.BackColor = System.Drawing.Color.Transparent
        Me.Pnl_MedicalPrescribedHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Pnl_MedicalPrescribedHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Label64)
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Button1)
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Label67)
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Label69)
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Label70)
        Me.Pnl_MedicalPrescribedHeader.Controls.Add(Me.Label71)
        Me.Pnl_MedicalPrescribedHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnl_MedicalPrescribedHeader.Location = New System.Drawing.Point(0, 0)
        Me.Pnl_MedicalPrescribedHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Pnl_MedicalPrescribedHeader.Name = "Pnl_MedicalPrescribedHeader"
        Me.Pnl_MedicalPrescribedHeader.Size = New System.Drawing.Size(1171, 27)
        Me.Pnl_MedicalPrescribedHeader.TabIndex = 0
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.Transparent
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Image = CType(resources.GetObject("Label64.Image"), System.Drawing.Image)
        Me.Label64.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label64.Location = New System.Drawing.Point(1, 1)
        Me.Label64.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1147, 25)
        Me.Label64.TabIndex = 1
        Me.Label64.Text = "      Medication Prescribed"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImage = Global.gloEMR.My.Resources.Resources.UP
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(1148, 1)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 25)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label67.Location = New System.Drawing.Point(1, 26)
        Me.Label67.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1169, 1)
        Me.Label67.TabIndex = 8
        Me.Label67.Text = "label2"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 1)
        Me.Label69.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 26)
        Me.Label69.TabIndex = 7
        Me.Label69.Text = "label4"
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label70.Location = New System.Drawing.Point(1170, 1)
        Me.Label70.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(1, 26)
        Me.Label70.TabIndex = 6
        Me.Label70.Text = "label3"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(0, 0)
        Me.Label71.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1171, 1)
        Me.Label71.TabIndex = 5
        Me.Label71.Text = "label1"
        '
        'pnlErrorMessage
        '
        Me.pnlErrorMessage.Controls.Add(Me.Label33)
        Me.pnlErrorMessage.Controls.Add(Me.Label34)
        Me.pnlErrorMessage.Controls.Add(Me.Label35)
        Me.pnlErrorMessage.Controls.Add(Me.Label36)
        Me.pnlErrorMessage.Controls.Add(Me.Panel5)
        Me.pnlErrorMessage.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlErrorMessage.Location = New System.Drawing.Point(0, 31)
        Me.pnlErrorMessage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlErrorMessage.Name = "pnlErrorMessage"
        Me.pnlErrorMessage.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnlErrorMessage.Size = New System.Drawing.Size(1175, 81)
        Me.pnlErrorMessage.TabIndex = 14
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(1, 76)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1169, 1)
        Me.Label33.TabIndex = 17
        Me.Label33.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 1)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 76)
        Me.Label34.TabIndex = 16
        Me.Label34.Text = "label4"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label35.Location = New System.Drawing.Point(1170, 1)
        Me.Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 76)
        Me.Label35.TabIndex = 15
        Me.Label35.Text = "label3"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(0, 0)
        Me.Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1171, 1)
        Me.Label36.TabIndex = 14
        Me.Label36.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.lblErrMsgDtl_Name)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.lblRelatesMsgId)
        Me.Panel5.Controls.Add(Me.lblDateSent)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.lblMessageId)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1171, 77)
        Me.Panel5.TabIndex = 13
        '
        'lblErrMsgDtl_Name
        '
        Me.lblErrMsgDtl_Name.AutoSize = True
        Me.lblErrMsgDtl_Name.Location = New System.Drawing.Point(180, 14)
        Me.lblErrMsgDtl_Name.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblErrMsgDtl_Name.Name = "lblErrMsgDtl_Name"
        Me.lblErrMsgDtl_Name.Size = New System.Drawing.Size(49, 18)
        Me.lblErrMsgDtl_Name.TabIndex = 6
        Me.lblErrMsgDtl_Name.Text = "Label8"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(51, 14)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(129, 18)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Message Name :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(75, 41)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 18)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Message ID :"
        '
        'lblRelatesMsgId
        '
        Me.lblRelatesMsgId.AutoSize = True
        Me.lblRelatesMsgId.BackColor = System.Drawing.Color.Transparent
        Me.lblRelatesMsgId.Location = New System.Drawing.Point(626, 14)
        Me.lblRelatesMsgId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRelatesMsgId.Name = "lblRelatesMsgId"
        Me.lblRelatesMsgId.Size = New System.Drawing.Size(49, 18)
        Me.lblRelatesMsgId.TabIndex = 5
        Me.lblRelatesMsgId.Text = "Label3"
        '
        'lblDateSent
        '
        Me.lblDateSent.AutoSize = True
        Me.lblDateSent.BackColor = System.Drawing.Color.Transparent
        Me.lblDateSent.Location = New System.Drawing.Point(626, 41)
        Me.lblDateSent.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDateSent.Name = "lblDateSent"
        Me.lblDateSent.Size = New System.Drawing.Size(49, 18)
        Me.lblDateSent.TabIndex = 5
        Me.lblDateSent.Text = "Label3"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(531, 41)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(90, 18)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Date Sent :"
        '
        'lblMessageId
        '
        Me.lblMessageId.AutoSize = True
        Me.lblMessageId.BackColor = System.Drawing.Color.Transparent
        Me.lblMessageId.Location = New System.Drawing.Point(180, 41)
        Me.lblMessageId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessageId.Name = "lblMessageId"
        Me.lblMessageId.Size = New System.Drawing.Size(49, 18)
        Me.lblMessageId.TabIndex = 5
        Me.lblMessageId.Text = "Label3"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(438, 14)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(184, 18)
        Me.Label23.TabIndex = 3
        Me.Label23.Text = "Relates to Message ID :"
        '
        'pnlError
        '
        Me.pnlError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlError.Controls.Add(Me.Panel10)
        Me.pnlError.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlError.Location = New System.Drawing.Point(0, 0)
        Me.pnlError.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlError.Name = "pnlError"
        Me.pnlError.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnlError.Size = New System.Drawing.Size(1175, 31)
        Me.pnlError.TabIndex = 12
        '
        'Panel10
        '
        Me.Panel10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label24)
        Me.Panel10.Controls.Add(Me.btnupdown)
        Me.Panel10.Controls.Add(Me.Label26)
        Me.Panel10.Controls.Add(Me.Label27)
        Me.Panel10.Controls.Add(Me.Label32)
        Me.Panel10.Controls.Add(Me.Label25)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1171, 27)
        Me.Panel10.TabIndex = 9
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Image = CType(resources.GetObject("Label24.Image"), System.Drawing.Image)
        Me.Label24.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label24.Location = New System.Drawing.Point(1, 1)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1143, 25)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "      Error Message Details"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnupdown
        '
        Me.btnupdown.BackColor = System.Drawing.Color.Transparent
        Me.btnupdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnupdown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnupdown.FlatAppearance.BorderSize = 0
        Me.btnupdown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnupdown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnupdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdown.Location = New System.Drawing.Point(1144, 1)
        Me.btnupdown.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnupdown.Name = "btnupdown"
        Me.btnupdown.Size = New System.Drawing.Size(26, 25)
        Me.btnupdown.TabIndex = 0
        Me.btnupdown.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 1)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 25)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1170, 1)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 25)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "label3"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1171, 1)
        Me.Label32.TabIndex = 5
        Me.Label32.Text = "label1"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(0, 26)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1171, 1)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "label2"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.dgRefillList)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 70)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.Panel4.Size = New System.Drawing.Size(1175, 182)
        Me.Panel4.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 177)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1169, 1)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 177)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1170, 1)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 177)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1171, 1)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label1"
        '
        'dgRefillList
        '
        Me.dgRefillList.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgRefillList.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgRefillList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgRefillList.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgRefillList.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgRefillList.CaptionForeColor = System.Drawing.Color.White
        Me.dgRefillList.CaptionVisible = False
        Me.dgRefillList.ContextMenuStrip = Me.cntListmenuStrip
        Me.dgRefillList.DataMember = ""
        Me.dgRefillList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgRefillList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgRefillList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgRefillList.FullRowSelect = True
        Me.dgRefillList.GridLineColor = System.Drawing.Color.Black
        Me.dgRefillList.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgRefillList.HeaderForeColor = System.Drawing.Color.White
        Me.dgRefillList.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgRefillList.Location = New System.Drawing.Point(0, 0)
        Me.dgRefillList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgRefillList.Name = "dgRefillList"
        Me.dgRefillList.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgRefillList.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgRefillList.ReadOnly = True
        Me.dgRefillList.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgRefillList.SelectionForeColor = System.Drawing.Color.Black
        Me.dgRefillList.Size = New System.Drawing.Size(1171, 178)
        Me.dgRefillList.TabIndex = 6
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.Panel14)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 36)
        Me.pnlTopRight.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Padding = New System.Windows.Forms.Padding(0, 0, 4, 4)
        Me.pnlTopRight.Size = New System.Drawing.Size(1175, 34)
        Me.pnlTopRight.TabIndex = 1
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Panel19)
        Me.Panel14.Controls.Add(Me.lblSearch)
        Me.Panel14.Controls.Add(Me.Label20)
        Me.Panel14.Controls.Add(Me.Label21)
        Me.Panel14.Controls.Add(Me.Label19)
        Me.Panel14.Controls.Add(Me.Label18)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1171, 30)
        Me.Panel14.TabIndex = 9
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.White
        Me.Panel19.Controls.Add(Me.txtSearch)
        Me.Panel19.Controls.Add(Me.Label105)
        Me.Panel19.Controls.Add(Me.Label106)
        Me.Panel19.Controls.Add(Me.btnClear)
        Me.Panel19.Controls.Add(Me.Label107)
        Me.Panel19.Controls.Add(Me.Label108)
        Me.Panel19.Controls.Add(Me.Label109)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.ForeColor = System.Drawing.Color.Black
        Me.Panel19.Location = New System.Drawing.Point(130, 1)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(301, 28)
        Me.Panel19.TabIndex = 47
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(6, 4)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(268, 19)
        Me.txtSearch.TabIndex = 0
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.White
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label105.Location = New System.Drawing.Point(6, 22)
        Me.Label105.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(268, 6)
        Me.Label105.TabIndex = 43
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.White
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label106.Location = New System.Drawing.Point(6, 0)
        Me.Label106.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(268, 4)
        Me.Label106.TabIndex = 37
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
        Me.btnClear.Location = New System.Drawing.Point(274, 0)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(26, 28)
        Me.btnClear.TabIndex = 44
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.White
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label107.Location = New System.Drawing.Point(1, 0)
        Me.Label107.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(5, 28)
        Me.Label107.TabIndex = 38
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label108.Location = New System.Drawing.Point(0, 0)
        Me.Label108.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(1, 28)
        Me.Label108.TabIndex = 39
        Me.Label108.Text = "label4"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label109.Location = New System.Drawing.Point(300, 0)
        Me.Label109.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 28)
        Me.Label109.TabIndex = 40
        Me.Label109.Text = "label4"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(129, 28)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Middle Name :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1170, 1)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 28)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(1, 0)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1170, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 29)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(0, 29)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1171, 1)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label2"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.pnlTop)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 4, 4, 4)
        Me.Panel8.Size = New System.Drawing.Size(1175, 36)
        Me.Panel8.TabIndex = 11
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.Label31)
        Me.pnlTop.Controls.Add(Me.Label30)
        Me.pnlTop.Controls.Add(Me.Label29)
        Me.pnlTop.Controls.Add(Me.Label22)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 4)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1171, 28)
        Me.pnlTop.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(1, 1)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1169, 26)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = " Error Messages"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(1, 0)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1169, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(1170, 0)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 27)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 27)
        Me.Label29.TabIndex = 7
        Me.Label29.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(0, 27)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1171, 1)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "label2"
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(1175, 1525)
        Me.Panel18.TabIndex = 7
        Me.Panel18.Visible = False
        '
        'frmVWErrorMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1481, 928)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl_trv)
        Me.Controls.Add(Me.pnl_toolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmVWErrorMessage"
        Me.Text = "Error Messages"
        Me.pnl_toolstrip.ResumeLayout(False)
        Me.pnl_toolstrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.cntListmenuStrip.ResumeLayout(False)
        Me.pnl_trv.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnl_Grid.ResumeLayout(False)
        Me.pnl_RefillInfo.ResumeLayout(False)
        Me.pnl_ProviderInfo.ResumeLayout(False)
        Me.pnl_ProviderInfo.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.pnl_PharmacyInfo.ResumeLayout(False)
        Me.pnl_PharmacyInfo.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.pnl_PatientInfo.ResumeLayout(False)
        Me.pnl_PatientInfo.PerformLayout()
        Me.pnlRefillinfotitle.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnl_MedicalDispanced.ResumeLayout(False)
        Me.pnl_MedicalDispanced.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.pnl_MedicalPrescribe.ResumeLayout(False)
        Me.pnl_MedicalPrescribe.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Pnl_MedicalPrescribedHeader.ResumeLayout(False)
        Me.pnlErrorMessage.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlError.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.dgRefillList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTopRight.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnl_toolstrip As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents pnl_trv As System.Windows.Forms.Panel
    Friend WithEvents trvPrescribers As System.Windows.Forms.TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Prescriber As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnl_Grid As System.Windows.Forms.Panel
    'Friend WithEvents dgRefillList As System.Windows.Forms.DataGrid
    Friend WithEvents dgRefillList As clsDataGrid
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnl_RefillInfo As System.Windows.Forms.Panel
    Friend WithEvents pnl_ProviderInfo As System.Windows.Forms.Panel
    Friend WithEvents lbl_PrPhone As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbl_Provider As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderName As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderZIP As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderAdd As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderState As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderCity As System.Windows.Forms.Label
    Friend WithEvents lbl_ProvidCity As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderAddress As System.Windows.Forms.Label
    Friend WithEvents lbl_ProvidState As System.Windows.Forms.Label
    Friend WithEvents lbl_ProvidZIP As System.Windows.Forms.Label
    Friend WithEvents pnl_PharmacyInfo As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyZip As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_Pharmacy As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyName As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyZIPCode As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyAdd As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyState As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lbl_PharamcyCity As System.Windows.Forms.Label
    Friend WithEvents pbl_PharmacyPhone As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmCity As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyAddress As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmState As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmZIPCode As System.Windows.Forms.Label
    Friend WithEvents pnl_PatientInfo As System.Windows.Forms.Panel
    Friend WithEvents lbl_Patient As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientName As System.Windows.Forms.Label
    Friend WithEvents lbl_ZIPCode As System.Windows.Forms.Label
    Friend WithEvents lbl_PatAdd As System.Windows.Forms.Label
    Friend WithEvents lbl_PatGender As System.Windows.Forms.Label
    Friend WithEvents lbl_PatDOB As System.Windows.Forms.Label
    Friend WithEvents lbl_StateName As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientPhoneNo As System.Windows.Forms.Label
    Friend WithEvents lbl_CityName As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientPhone As System.Windows.Forms.Label
    Friend WithEvents lbl_City As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientAddress As System.Windows.Forms.Label
    Friend WithEvents lbl_State As System.Windows.Forms.Label
    Friend WithEvents lbl_Gender As System.Windows.Forms.Label
    Friend WithEvents lbl_ZIP As System.Windows.Forms.Label
    Friend WithEvents lbl_DOB As System.Windows.Forms.Label
    Friend WithEvents pnlRefillinfotitle As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlErrorMessage As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblErrMsgDtl_Name As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblRelatesMsgId As System.Windows.Forms.Label
    Friend WithEvents lblDateSent As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblMessageId As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents pnlError As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btnupdown As System.Windows.Forms.Button
    Friend WithEvents mnuReview As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReloadRx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents lbl_PharmacyAddress2 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientAddress2 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyNPI As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderAddress2 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents lbl_PharmacyFax As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderFax As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents lbl_ProviderNPI As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Pnl_MedicalPrescribedHeader As System.Windows.Forms.Panel
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents pnl_MedicalPrescribe As System.Windows.Forms.Panel
    Friend WithEvents lbl_Ref_Qlfr As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_Duration As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents lbl_LastFillDate As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents lbl_MPDirection As System.Windows.Forms.Label
    Friend WithEvents lbl_MPSubstitution As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents lbl_MPDrugName_Strength_Dosageform As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents lbl_MPDrugQuantity As System.Windows.Forms.Label
    Friend WithEvents lbl_MPWrittenDate As System.Windows.Forms.Label
    Friend WithEvents lbl_MPRefillQuantity As System.Windows.Forms.Label
    Friend WithEvents lbl_MPDrugnotes As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents pnl_MedicalDispanced As System.Windows.Forms.Panel
    Friend WithEvents lbl_MDRef_Qlfr As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents lbl_MDDuration As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents lbl_MDLastFillDate As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents lblDrugName_Strength_Dosageform As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents lblSubstitution As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents lblDrugQuantity As System.Windows.Forms.Label
    Friend WithEvents lblWrittenDate As System.Windows.Forms.Label
    Friend WithEvents lblRefillQuantity As System.Windows.Forms.Label
    Friend WithEvents lblDrugNotes As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents lblDirection As System.Windows.Forms.Label
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
End Class
