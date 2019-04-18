<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmeRxSummary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeRxSummary))
        Me.pnlToostrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnNewRx = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnApprove = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnApprovewithChanges = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnDTNF = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnComparedata = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnPDR = New System.Windows.Forms.ToolStripButton()
        Me.pnlDrugSummary = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lbl_PharamcyCity = New System.Windows.Forms.Label()
        Me.lbl_PharmCity = New System.Windows.Forms.Label()
        Me.lbl_PharmacyPhoneNo = New System.Windows.Forms.Label()
        Me.pbl_PharmacyPhone = New System.Windows.Forms.Label()
        Me.lblPhNpi = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblPhAddress2 = New System.Windows.Forms.Label()
        Me.lbl_PharmacyZip = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lbl_Pharmacy = New System.Windows.Forms.Label()
        Me.lbl_PharmacyName = New System.Windows.Forms.Label()
        Me.lbl_PharmacyAdd = New System.Windows.Forms.Label()
        Me.lbl_PharmacyState = New System.Windows.Forms.Label()
        Me.lblPhFax = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lbl_PharmacyAddress = New System.Windows.Forms.Label()
        Me.lbl_PharmState = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_DOB = New System.Windows.Forms.Label()
        Me.lbl_PatDOB = New System.Windows.Forms.Label()
        Me.lbl_Gender = New System.Windows.Forms.Label()
        Me.lbl_PatGender = New System.Windows.Forms.Label()
        Me.lbl_City = New System.Windows.Forms.Label()
        Me.lbl_CityName = New System.Windows.Forms.Label()
        Me.lblPatientFax = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.lbl_PatAdd = New System.Windows.Forms.Label()
        Me.lbl_Patient = New System.Windows.Forms.Label()
        Me.lbl_ZIP = New System.Windows.Forms.Label()
        Me.lbl_PatientName = New System.Windows.Forms.Label()
        Me.lbl_ZIPCode = New System.Windows.Forms.Label()
        Me.lbl_State = New System.Windows.Forms.Label()
        Me.lblPatAddress2 = New System.Windows.Forms.Label()
        Me.lbl_PatientAddress = New System.Windows.Forms.Label()
        Me.lbl_PatientPhone = New System.Windows.Forms.Label()
        Me.lbl_StateName = New System.Windows.Forms.Label()
        Me.lbl_PatientPhoneNo = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblProviderFax = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblProviderNPI = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblProviderAddressline2 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lbl_PrPhone = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
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
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lblDrugSubstitution = New System.Windows.Forms.Label()
        Me.lblDrugWrittenDate = New System.Windows.Forms.Label()
        Me.lblDrugDuration = New System.Windows.Forms.Label()
        Me.lblDrugQuantitiyInformation = New System.Windows.Forms.Label()
        Me.lblSubstitution = New System.Windows.Forms.Label()
        Me.lblDateWritten = New System.Windows.Forms.Label()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.lblDrugQuantity = New System.Windows.Forms.Label()
        Me.lblRefillQtyInformation = New System.Windows.Forms.Label()
        Me.lblRefillQty = New System.Windows.Forms.Label()
        Me.lblRefillQualifier = New System.Windows.Forms.Label()
        Me.lblRefillQualifierInformation = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.lblDrugDescription = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lblDrugSummaryDetail = New System.Windows.Forms.Label()
        Me.lblSIG = New System.Windows.Forms.Label()
        Me.lblSigInformation = New System.Windows.Forms.Label()
        Me.lblDrugDescriptionInformation = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.pnlButton = New System.Windows.Forms.Panel()
        Me.pnlcode_Name = New System.Windows.Forms.Panel()
        Me.lblPtName = New System.Windows.Forms.Label()
        Me.lbl_BorderTOP = New System.Windows.Forms.Label()
        Me.lbl_BorderLEFT = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnlWebBrowser = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPDRListView = New System.Windows.Forms.Panel()
        Me.lstPDRPrograms = New System.Windows.Forms.ListView()
        Me.colPDRProgram = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.label48 = New System.Windows.Forms.Label()
        Me.label71 = New System.Windows.Forms.Label()
        Me.pnlPDRListViewHeader = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlDrugSummary.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlButton.SuspendLayout()
        Me.pnlcode_Name.SuspendLayout()
        Me.pnlWebBrowser.SuspendLayout()
        Me.pnlPDRListView.SuspendLayout()
        Me.pnlPDRListViewHeader.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(972, 54)
        Me.pnlToostrip.TabIndex = 16
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = CType(resources.GetObject("ts_ViewButtons.BackgroundImage"), System.Drawing.Image)
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnNewRx, Me.tlbbtnApprove, Me.tlbbtnApprovewithChanges, Me.tlbbtnDTNF, Me.tlbbtnComparedata, Me.tlbbtnClose, Me.tlbbtnPDR})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(972, 54)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlbbtnNewRx
        '
        Me.tlbbtnNewRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnNewRx.Image = CType(resources.GetObject("tlbbtnNewRx.Image"), System.Drawing.Image)
        Me.tlbbtnNewRx.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnNewRx.Name = "tlbbtnNewRx"
        Me.tlbbtnNewRx.Size = New System.Drawing.Size(57, 51)
        Me.tlbbtnNewRx.Text = "&New Rx"
        Me.tlbbtnNewRx.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnNewRx.Visible = False
        '
        'tlbbtnApprove
        '
        Me.tlbbtnApprove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnApprove.Image = CType(resources.GetObject("tlbbtnApprove.Image"), System.Drawing.Image)
        Me.tlbbtnApprove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnApprove.Name = "tlbbtnApprove"
        Me.tlbbtnApprove.Size = New System.Drawing.Size(63, 51)
        Me.tlbbtnApprove.Text = "&Approve"
        Me.tlbbtnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnApprove.Visible = False
        '
        'tlbbtnApprovewithChanges
        '
        Me.tlbbtnApprovewithChanges.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnApprovewithChanges.Image = CType(resources.GetObject("tlbbtnApprovewithChanges.Image"), System.Drawing.Image)
        Me.tlbbtnApprovewithChanges.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnApprovewithChanges.Name = "tlbbtnApprovewithChanges"
        Me.tlbbtnApprovewithChanges.Size = New System.Drawing.Size(151, 51)
        Me.tlbbtnApprovewithChanges.Text = "Approve with Changes"
        Me.tlbbtnApprovewithChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnApprovewithChanges.Visible = False
        '
        'tlbbtnDTNF
        '
        Me.tlbbtnDTNF.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnDTNF.Image = CType(resources.GetObject("tlbbtnDTNF.Image"), System.Drawing.Image)
        Me.tlbbtnDTNF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnDTNF.Name = "tlbbtnDTNF"
        Me.tlbbtnDTNF.Size = New System.Drawing.Size(41, 51)
        Me.tlbbtnDTNF.Text = "&DNTF"
        Me.tlbbtnDTNF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnDTNF.ToolTipText = "Deny with New Rx to Follow"
        Me.tlbbtnDTNF.Visible = False
        '
        'tlbbtnComparedata
        '
        Me.tlbbtnComparedata.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnComparedata.Image = CType(resources.GetObject("tlbbtnComparedata.Image"), System.Drawing.Image)
        Me.tlbbtnComparedata.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnComparedata.Name = "tlbbtnComparedata"
        Me.tlbbtnComparedata.Size = New System.Drawing.Size(98, 51)
        Me.tlbbtnComparedata.Text = "&Compare Data"
        Me.tlbbtnComparedata.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnComparedata.ToolTipText = "Compare Request Data"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(123, 51)
        Me.tlbbtnClose.Text = "&Stop Transmission"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnPDR
        '
        Me.tlbbtnPDR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnPDR.Image = CType(resources.GetObject("tlbbtnPDR.Image"), System.Drawing.Image)
        Me.tlbbtnPDR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnPDR.Name = "tlbbtnPDR"
        Me.tlbbtnPDR.Size = New System.Drawing.Size(99, 51)
        Me.tlbbtnPDR.Text = "&PDR Programs"
        Me.tlbbtnPDR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnPDR.Visible = False
        '
        'pnlDrugSummary
        '
        Me.pnlDrugSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDrugSummary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDrugSummary.Controls.Add(Me.Panel6)
        Me.pnlDrugSummary.Controls.Add(Me.Panel11)
        Me.pnlDrugSummary.Controls.Add(Me.Panel1)
        Me.pnlDrugSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDrugSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDrugSummary.Location = New System.Drawing.Point(0, 140)
        Me.pnlDrugSummary.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlDrugSummary.Name = "pnlDrugSummary"
        Me.pnlDrugSummary.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDrugSummary.Size = New System.Drawing.Size(972, 607)
        Me.pnlDrugSummary.TabIndex = 20
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.Label10)
        Me.Panel6.Controls.Add(Me.Label11)
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 391)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(966, 213)
        Me.Panel6.TabIndex = 37
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.lbl_PharamcyCity)
        Me.Panel7.Controls.Add(Me.lbl_PharmCity)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyPhoneNo)
        Me.Panel7.Controls.Add(Me.pbl_PharmacyPhone)
        Me.Panel7.Controls.Add(Me.lblPhNpi)
        Me.Panel7.Controls.Add(Me.Label36)
        Me.Panel7.Controls.Add(Me.Label30)
        Me.Panel7.Controls.Add(Me.lblPhAddress2)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyZip)
        Me.Panel7.Controls.Add(Me.Label18)
        Me.Panel7.Controls.Add(Me.lbl_Pharmacy)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyName)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyAdd)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyState)
        Me.Panel7.Controls.Add(Me.lblPhFax)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.lbl_PharmacyAddress)
        Me.Panel7.Controls.Add(Me.lbl_PharmState)
        Me.Panel7.Controls.Add(Me.Label4)
        Me.Panel7.Controls.Add(Me.Label1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel7.Location = New System.Drawing.Point(1, 193)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(964, 20)
        Me.Panel7.TabIndex = 32
        '
        'lbl_PharamcyCity
        '
        Me.lbl_PharamcyCity.AutoEllipsis = True
        Me.lbl_PharamcyCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharamcyCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharamcyCity.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharamcyCity.Location = New System.Drawing.Point(139, 64)
        Me.lbl_PharamcyCity.Name = "lbl_PharamcyCity"
        Me.lbl_PharamcyCity.Size = New System.Drawing.Size(315, 14)
        Me.lbl_PharamcyCity.TabIndex = 30
        Me.lbl_PharamcyCity.Text = "Label3"
        Me.lbl_PharamcyCity.UseMnemonic = False
        '
        'lbl_PharmCity
        '
        Me.lbl_PharmCity.AutoSize = True
        Me.lbl_PharmCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmCity.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmCity.Location = New System.Drawing.Point(97, 64)
        Me.lbl_PharmCity.Name = "lbl_PharmCity"
        Me.lbl_PharmCity.Size = New System.Drawing.Size(35, 14)
        Me.lbl_PharmCity.TabIndex = 29
        Me.lbl_PharmCity.Text = "City :"
        '
        'lbl_PharmacyPhoneNo
        '
        Me.lbl_PharmacyPhoneNo.AutoSize = True
        Me.lbl_PharmacyPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyPhoneNo.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyPhoneNo.Location = New System.Drawing.Point(139, 93)
        Me.lbl_PharmacyPhoneNo.Name = "lbl_PharmacyPhoneNo"
        Me.lbl_PharmacyPhoneNo.Size = New System.Drawing.Size(109, 14)
        Me.lbl_PharmacyPhoneNo.TabIndex = 28
        Me.lbl_PharmacyPhoneNo.Text = "Pharmacy Phone"
        Me.lbl_PharmacyPhoneNo.UseMnemonic = False
        '
        'pbl_PharmacyPhone
        '
        Me.pbl_PharmacyPhone.AutoSize = True
        Me.pbl_PharmacyPhone.BackColor = System.Drawing.Color.Transparent
        Me.pbl_PharmacyPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pbl_PharmacyPhone.ForeColor = System.Drawing.Color.Black
        Me.pbl_PharmacyPhone.Location = New System.Drawing.Point(81, 93)
        Me.pbl_PharmacyPhone.Name = "pbl_PharmacyPhone"
        Me.pbl_PharmacyPhone.Size = New System.Drawing.Size(50, 14)
        Me.pbl_PharmacyPhone.TabIndex = 27
        Me.pbl_PharmacyPhone.Text = "Phone :"
        '
        'lblPhNpi
        '
        Me.lblPhNpi.AutoSize = True
        Me.lblPhNpi.BackColor = System.Drawing.Color.Transparent
        Me.lblPhNpi.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhNpi.ForeColor = System.Drawing.Color.Black
        Me.lblPhNpi.Location = New System.Drawing.Point(555, 6)
        Me.lblPhNpi.Name = "lblPhNpi"
        Me.lblPhNpi.Size = New System.Drawing.Size(47, 14)
        Me.lblPhNpi.TabIndex = 26
        Me.lblPhNpi.Text = "Label3"
        Me.lblPhNpi.UseMnemonic = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(517, 6)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(34, 14)
        Me.Label36.TabIndex = 25
        Me.Label36.Text = "NPI :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(478, 36)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(73, 14)
        Me.Label30.TabIndex = 23
        Me.Label30.Text = " Address 2 :"
        '
        'lblPhAddress2
        '
        Me.lblPhAddress2.AutoEllipsis = True
        Me.lblPhAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblPhAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblPhAddress2.Location = New System.Drawing.Point(555, 36)
        Me.lblPhAddress2.Name = "lblPhAddress2"
        Me.lblPhAddress2.Size = New System.Drawing.Size(315, 14)
        Me.lblPhAddress2.TabIndex = 24
        Me.lblPhAddress2.Text = "Ph Address2"
        Me.lblPhAddress2.UseMnemonic = False
        '
        'lbl_PharmacyZip
        '
        Me.lbl_PharmacyZip.AutoSize = True
        Me.lbl_PharmacyZip.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyZip.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyZip.Location = New System.Drawing.Point(774, 64)
        Me.lbl_PharmacyZip.Name = "lbl_PharmacyZip"
        Me.lbl_PharmacyZip.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PharmacyZip.TabIndex = 22
        Me.lbl_PharmacyZip.Text = "Label3"
        Me.lbl_PharmacyZip.UseMnemonic = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(742, 64)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(33, 14)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "ZIP :"
        '
        'lbl_Pharmacy
        '
        Me.lbl_Pharmacy.AutoEllipsis = True
        Me.lbl_Pharmacy.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Pharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Pharmacy.ForeColor = System.Drawing.Color.Black
        Me.lbl_Pharmacy.Location = New System.Drawing.Point(139, 6)
        Me.lbl_Pharmacy.Name = "lbl_Pharmacy"
        Me.lbl_Pharmacy.Size = New System.Drawing.Size(315, 14)
        Me.lbl_Pharmacy.TabIndex = 20
        Me.lbl_Pharmacy.Text = "Pharmacy Name"
        Me.lbl_Pharmacy.UseMnemonic = False
        '
        'lbl_PharmacyName
        '
        Me.lbl_PharmacyName.AutoSize = True
        Me.lbl_PharmacyName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyName.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyName.Location = New System.Drawing.Point(86, 6)
        Me.lbl_PharmacyName.Name = "lbl_PharmacyName"
        Me.lbl_PharmacyName.Size = New System.Drawing.Size(46, 14)
        Me.lbl_PharmacyName.TabIndex = 12
        Me.lbl_PharmacyName.Text = "Name :"
        '
        'lbl_PharmacyAdd
        '
        Me.lbl_PharmacyAdd.AutoSize = True
        Me.lbl_PharmacyAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyAdd.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyAdd.Location = New System.Drawing.Point(63, 36)
        Me.lbl_PharmacyAdd.Name = "lbl_PharmacyAdd"
        Me.lbl_PharmacyAdd.Size = New System.Drawing.Size(69, 14)
        Me.lbl_PharmacyAdd.TabIndex = 10
        Me.lbl_PharmacyAdd.Text = "Address 1 :"
        '
        'lbl_PharmacyState
        '
        Me.lbl_PharmacyState.AutoSize = True
        Me.lbl_PharmacyState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyState.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyState.Location = New System.Drawing.Point(555, 64)
        Me.lbl_PharmacyState.Name = "lbl_PharmacyState"
        Me.lbl_PharmacyState.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PharmacyState.TabIndex = 18
        Me.lbl_PharmacyState.Text = "Label3"
        Me.lbl_PharmacyState.UseMnemonic = False
        '
        'lblPhFax
        '
        Me.lblPhFax.AutoSize = True
        Me.lblPhFax.BackColor = System.Drawing.Color.Transparent
        Me.lblPhFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhFax.ForeColor = System.Drawing.Color.Black
        Me.lblPhFax.Location = New System.Drawing.Point(139, 122)
        Me.lblPhFax.Name = "lblPhFax"
        Me.lblPhFax.Size = New System.Drawing.Size(47, 14)
        Me.lblPhFax.TabIndex = 19
        Me.lblPhFax.Text = "Label3"
        Me.lblPhFax.UseMnemonic = False
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(98, 122)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(33, 14)
        Me.Label31.TabIndex = 11
        Me.Label31.Text = "Fax :"
        '
        'lbl_PharmacyAddress
        '
        Me.lbl_PharmacyAddress.AutoEllipsis = True
        Me.lbl_PharmacyAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmacyAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmacyAddress.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmacyAddress.Location = New System.Drawing.Point(139, 36)
        Me.lbl_PharmacyAddress.Name = "lbl_PharmacyAddress"
        Me.lbl_PharmacyAddress.Size = New System.Drawing.Size(315, 14)
        Me.lbl_PharmacyAddress.TabIndex = 16
        Me.lbl_PharmacyAddress.Text = "Ph Address1"
        Me.lbl_PharmacyAddress.UseMnemonic = False
        '
        'lbl_PharmState
        '
        Me.lbl_PharmState.AutoSize = True
        Me.lbl_PharmState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PharmState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PharmState.ForeColor = System.Drawing.Color.Black
        Me.lbl_PharmState.Location = New System.Drawing.Point(506, 64)
        Me.lbl_PharmState.Name = "lbl_PharmState"
        Me.lbl_PharmState.Size = New System.Drawing.Size(45, 14)
        Me.lbl_PharmState.TabIndex = 14
        Me.lbl_PharmState.Text = "State :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(0, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(964, 1)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(964, 1)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "label1"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Panel10)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(1, 170)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(964, 23)
        Me.Panel9.TabIndex = 82
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.Controls.Add(Me.Label2)
        Me.Panel10.Controls.Add(Me.Label9)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(964, 23)
        Me.Panel10.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 22)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "    Pharmacy Information"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(964, 1)
        Me.Label9.TabIndex = 90
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 170)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 43)
        Me.Label10.TabIndex = 93
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(965, 170)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 43)
        Me.Label11.TabIndex = 94
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(966, 170)
        Me.Panel2.TabIndex = 95
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lbl_DOB)
        Me.Panel3.Controls.Add(Me.lbl_PatDOB)
        Me.Panel3.Controls.Add(Me.lbl_Gender)
        Me.Panel3.Controls.Add(Me.lbl_PatGender)
        Me.Panel3.Controls.Add(Me.lbl_City)
        Me.Panel3.Controls.Add(Me.lbl_CityName)
        Me.Panel3.Controls.Add(Me.lblPatientFax)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.lbl_PatAdd)
        Me.Panel3.Controls.Add(Me.lbl_Patient)
        Me.Panel3.Controls.Add(Me.lbl_ZIP)
        Me.Panel3.Controls.Add(Me.lbl_PatientName)
        Me.Panel3.Controls.Add(Me.lbl_ZIPCode)
        Me.Panel3.Controls.Add(Me.lbl_State)
        Me.Panel3.Controls.Add(Me.lblPatAddress2)
        Me.Panel3.Controls.Add(Me.lbl_PatientAddress)
        Me.Panel3.Controls.Add(Me.lbl_PatientPhone)
        Me.Panel3.Controls.Add(Me.lbl_StateName)
        Me.Panel3.Controls.Add(Me.lbl_PatientPhoneNo)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(1, 23)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(964, 144)
        Me.Panel3.TabIndex = 32
        '
        'lbl_DOB
        '
        Me.lbl_DOB.AutoSize = True
        Me.lbl_DOB.BackColor = System.Drawing.Color.Transparent
        Me.lbl_DOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DOB.ForeColor = System.Drawing.Color.Black
        Me.lbl_DOB.Location = New System.Drawing.Point(745, 8)
        Me.lbl_DOB.Name = "lbl_DOB"
        Me.lbl_DOB.Size = New System.Drawing.Size(87, 14)
        Me.lbl_DOB.TabIndex = 28
        Me.lbl_DOB.Text = "Date Of Birth :"
        '
        'lbl_PatDOB
        '
        Me.lbl_PatDOB.AutoSize = True
        Me.lbl_PatDOB.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatDOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatDOB.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatDOB.Location = New System.Drawing.Point(834, 8)
        Me.lbl_PatDOB.Name = "lbl_PatDOB"
        Me.lbl_PatDOB.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PatDOB.TabIndex = 29
        Me.lbl_PatDOB.Text = "Label3"
        Me.lbl_PatDOB.UseMnemonic = False
        '
        'lbl_Gender
        '
        Me.lbl_Gender.AutoSize = True
        Me.lbl_Gender.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Gender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Gender.ForeColor = System.Drawing.Color.Black
        Me.lbl_Gender.Location = New System.Drawing.Point(777, 94)
        Me.lbl_Gender.Name = "lbl_Gender"
        Me.lbl_Gender.Size = New System.Drawing.Size(55, 14)
        Me.lbl_Gender.TabIndex = 24
        Me.lbl_Gender.Text = "Gender :"
        '
        'lbl_PatGender
        '
        Me.lbl_PatGender.AutoSize = True
        Me.lbl_PatGender.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatGender.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatGender.Location = New System.Drawing.Point(834, 94)
        Me.lbl_PatGender.Name = "lbl_PatGender"
        Me.lbl_PatGender.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PatGender.TabIndex = 27
        Me.lbl_PatGender.Text = "Label3"
        Me.lbl_PatGender.UseMnemonic = False
        '
        'lbl_City
        '
        Me.lbl_City.AutoSize = True
        Me.lbl_City.BackColor = System.Drawing.Color.Transparent
        Me.lbl_City.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_City.ForeColor = System.Drawing.Color.Black
        Me.lbl_City.Location = New System.Drawing.Point(97, 65)
        Me.lbl_City.Name = "lbl_City"
        Me.lbl_City.Size = New System.Drawing.Size(35, 14)
        Me.lbl_City.TabIndex = 22
        Me.lbl_City.Text = "City :"
        '
        'lbl_CityName
        '
        Me.lbl_CityName.AutoEllipsis = True
        Me.lbl_CityName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_CityName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CityName.ForeColor = System.Drawing.Color.Black
        Me.lbl_CityName.Location = New System.Drawing.Point(139, 65)
        Me.lbl_CityName.Name = "lbl_CityName"
        Me.lbl_CityName.Size = New System.Drawing.Size(315, 14)
        Me.lbl_CityName.TabIndex = 25
        Me.lbl_CityName.Text = "Label3"
        Me.lbl_CityName.UseMnemonic = False
        '
        'lblPatientFax
        '
        Me.lblPatientFax.AutoSize = True
        Me.lblPatientFax.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientFax.ForeColor = System.Drawing.Color.Black
        Me.lblPatientFax.Location = New System.Drawing.Point(139, 120)
        Me.lblPatientFax.Name = "lblPatientFax"
        Me.lblPatientFax.Size = New System.Drawing.Size(23, 14)
        Me.lblPatientFax.TabIndex = 21
        Me.lblPatientFax.Text = "alc"
        Me.lblPatientFax.UseMnemonic = False
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(99, 120)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(33, 14)
        Me.Label35.TabIndex = 20
        Me.Label35.Text = "Fax :"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(964, 1)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(964, 1)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "label1"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(480, 36)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(69, 14)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Address 2 :"
        '
        'lbl_PatAdd
        '
        Me.lbl_PatAdd.AutoSize = True
        Me.lbl_PatAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatAdd.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatAdd.Location = New System.Drawing.Point(63, 36)
        Me.lbl_PatAdd.Name = "lbl_PatAdd"
        Me.lbl_PatAdd.Size = New System.Drawing.Size(69, 14)
        Me.lbl_PatAdd.TabIndex = 0
        Me.lbl_PatAdd.Text = "Address 1 :"
        '
        'lbl_Patient
        '
        Me.lbl_Patient.AutoEllipsis = True
        Me.lbl_Patient.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Patient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Patient.ForeColor = System.Drawing.Color.Black
        Me.lbl_Patient.Location = New System.Drawing.Point(139, 7)
        Me.lbl_Patient.Name = "lbl_Patient"
        Me.lbl_Patient.Size = New System.Drawing.Size(571, 17)
        Me.lbl_Patient.TabIndex = 6
        Me.lbl_Patient.Text = "PatientName"
        Me.lbl_Patient.UseMnemonic = False
        '
        'lbl_ZIP
        '
        Me.lbl_ZIP.AutoSize = True
        Me.lbl_ZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ZIP.ForeColor = System.Drawing.Color.Black
        Me.lbl_ZIP.Location = New System.Drawing.Point(799, 65)
        Me.lbl_ZIP.Name = "lbl_ZIP"
        Me.lbl_ZIP.Size = New System.Drawing.Size(33, 14)
        Me.lbl_ZIP.TabIndex = 0
        Me.lbl_ZIP.Text = "ZIP :"
        '
        'lbl_PatientName
        '
        Me.lbl_PatientName.AutoSize = True
        Me.lbl_PatientName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientName.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatientName.Location = New System.Drawing.Point(86, 8)
        Me.lbl_PatientName.Name = "lbl_PatientName"
        Me.lbl_PatientName.Size = New System.Drawing.Size(46, 14)
        Me.lbl_PatientName.TabIndex = 0
        Me.lbl_PatientName.Text = "Name :"
        '
        'lbl_ZIPCode
        '
        Me.lbl_ZIPCode.AutoSize = True
        Me.lbl_ZIPCode.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ZIPCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ZIPCode.ForeColor = System.Drawing.Color.Black
        Me.lbl_ZIPCode.Location = New System.Drawing.Point(834, 65)
        Me.lbl_ZIPCode.Name = "lbl_ZIPCode"
        Me.lbl_ZIPCode.Size = New System.Drawing.Size(47, 14)
        Me.lbl_ZIPCode.TabIndex = 5
        Me.lbl_ZIPCode.Text = "Label3"
        Me.lbl_ZIPCode.UseMnemonic = False
        '
        'lbl_State
        '
        Me.lbl_State.AutoSize = True
        Me.lbl_State.BackColor = System.Drawing.Color.Transparent
        Me.lbl_State.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_State.ForeColor = System.Drawing.Color.Black
        Me.lbl_State.Location = New System.Drawing.Point(504, 65)
        Me.lbl_State.Name = "lbl_State"
        Me.lbl_State.Size = New System.Drawing.Size(45, 14)
        Me.lbl_State.TabIndex = 0
        Me.lbl_State.Text = "State :"
        '
        'lblPatAddress2
        '
        Me.lblPatAddress2.AutoEllipsis = True
        Me.lblPatAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblPatAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblPatAddress2.Location = New System.Drawing.Point(558, 36)
        Me.lblPatAddress2.Name = "lblPatAddress2"
        Me.lblPatAddress2.Size = New System.Drawing.Size(315, 14)
        Me.lblPatAddress2.TabIndex = 5
        Me.lblPatAddress2.Text = "Addressline2"
        Me.lblPatAddress2.UseMnemonic = False
        '
        'lbl_PatientAddress
        '
        Me.lbl_PatientAddress.AutoEllipsis = True
        Me.lbl_PatientAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientAddress.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatientAddress.Location = New System.Drawing.Point(139, 36)
        Me.lbl_PatientAddress.Name = "lbl_PatientAddress"
        Me.lbl_PatientAddress.Size = New System.Drawing.Size(315, 14)
        Me.lbl_PatientAddress.TabIndex = 5
        Me.lbl_PatientAddress.Text = "Addressline1"
        Me.lbl_PatientAddress.UseMnemonic = False
        '
        'lbl_PatientPhone
        '
        Me.lbl_PatientPhone.AutoSize = True
        Me.lbl_PatientPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientPhone.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatientPhone.Location = New System.Drawing.Point(82, 94)
        Me.lbl_PatientPhone.Name = "lbl_PatientPhone"
        Me.lbl_PatientPhone.Size = New System.Drawing.Size(50, 14)
        Me.lbl_PatientPhone.TabIndex = 0
        Me.lbl_PatientPhone.Text = "Phone :"
        '
        'lbl_StateName
        '
        Me.lbl_StateName.AutoSize = True
        Me.lbl_StateName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_StateName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_StateName.ForeColor = System.Drawing.Color.Black
        Me.lbl_StateName.Location = New System.Drawing.Point(558, 65)
        Me.lbl_StateName.Name = "lbl_StateName"
        Me.lbl_StateName.Size = New System.Drawing.Size(47, 14)
        Me.lbl_StateName.TabIndex = 5
        Me.lbl_StateName.Text = "Label3"
        Me.lbl_StateName.UseMnemonic = False
        '
        'lbl_PatientPhoneNo
        '
        Me.lbl_PatientPhoneNo.AutoSize = True
        Me.lbl_PatientPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientPhoneNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientPhoneNo.ForeColor = System.Drawing.Color.Black
        Me.lbl_PatientPhoneNo.Location = New System.Drawing.Point(139, 94)
        Me.lbl_PatientPhoneNo.Name = "lbl_PatientPhoneNo"
        Me.lbl_PatientPhoneNo.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PatientPhoneNo.TabIndex = 5
        Me.lbl_PatientPhoneNo.Text = "Label3"
        Me.lbl_PatientPhoneNo.UseMnemonic = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(1, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(964, 23)
        Me.Panel4.TabIndex = 82
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(964, 23)
        Me.Panel5.TabIndex = 5
        '
        'Label20
        '
        Me.Label20.AutoEllipsis = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(0, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(200, 22)
        Me.Label20.TabIndex = 50
        Me.Label20.Text = "    Patient Information"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(964, 1)
        Me.Label21.TabIndex = 90
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 167)
        Me.Label22.TabIndex = 93
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Location = New System.Drawing.Point(965, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 167)
        Me.Label25.TabIndex = 94
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel12)
        Me.Panel11.Controls.Add(Me.Panel13)
        Me.Panel11.Controls.Add(Me.Label16)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(3, 235)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(966, 156)
        Me.Panel11.TabIndex = 38
        '
        'Panel12
        '
        Me.Panel12.BackgroundImage = CType(resources.GetObject("Panel12.BackgroundImage"), System.Drawing.Image)
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.lblProviderFax)
        Me.Panel12.Controls.Add(Me.Label37)
        Me.Panel12.Controls.Add(Me.lblProviderNPI)
        Me.Panel12.Controls.Add(Me.Label33)
        Me.Panel12.Controls.Add(Me.Label29)
        Me.Panel12.Controls.Add(Me.lblProviderAddressline2)
        Me.Panel12.Controls.Add(Me.Label23)
        Me.Panel12.Controls.Add(Me.lbl_PrPhone)
        Me.Panel12.Controls.Add(Me.Label24)
        Me.Panel12.Controls.Add(Me.lbl_Provider)
        Me.Panel12.Controls.Add(Me.lbl_ProviderName)
        Me.Panel12.Controls.Add(Me.lbl_ProviderZIP)
        Me.Panel12.Controls.Add(Me.lbl_ProviderAdd)
        Me.Panel12.Controls.Add(Me.lbl_ProviderState)
        Me.Panel12.Controls.Add(Me.lbl_ProviderCity)
        Me.Panel12.Controls.Add(Me.lbl_ProvidCity)
        Me.Panel12.Controls.Add(Me.lbl_ProviderAddress)
        Me.Panel12.Controls.Add(Me.lbl_ProvidState)
        Me.Panel12.Controls.Add(Me.lbl_ProvidZIP)
        Me.Panel12.Controls.Add(Me.Label12)
        Me.Panel12.Controls.Add(Me.Label13)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel12.Location = New System.Drawing.Point(1, 23)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(965, 130)
        Me.Panel12.TabIndex = 32
        '
        'lblProviderFax
        '
        Me.lblProviderFax.AutoSize = True
        Me.lblProviderFax.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderFax.ForeColor = System.Drawing.Color.Black
        Me.lblProviderFax.Location = New System.Drawing.Point(139, 109)
        Me.lblProviderFax.Name = "lblProviderFax"
        Me.lblProviderFax.Size = New System.Drawing.Size(92, 14)
        Me.lblProviderFax.TabIndex = 41
        Me.lblProviderFax.Text = "lblProviderFax"
        Me.lblProviderFax.UseMnemonic = False
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(102, 109)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(33, 14)
        Me.Label37.TabIndex = 40
        Me.Label37.Text = "Fax :"
        '
        'lblProviderNPI
        '
        Me.lblProviderNPI.AutoSize = True
        Me.lblProviderNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderNPI.ForeColor = System.Drawing.Color.Black
        Me.lblProviderNPI.Location = New System.Drawing.Point(834, 9)
        Me.lblProviderNPI.Name = "lblProviderNPI"
        Me.lblProviderNPI.Size = New System.Drawing.Size(47, 14)
        Me.lblProviderNPI.TabIndex = 22
        Me.lblProviderNPI.Text = "Label3"
        Me.lblProviderNPI.UseMnemonic = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(798, 9)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(34, 14)
        Me.Label33.TabIndex = 21
        Me.Label33.Text = "NPI :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(476, 34)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(73, 14)
        Me.Label29.TabIndex = 38
        Me.Label29.Text = " Address 2 :"
        '
        'lblProviderAddressline2
        '
        Me.lblProviderAddressline2.AutoEllipsis = True
        Me.lblProviderAddressline2.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderAddressline2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderAddressline2.ForeColor = System.Drawing.Color.Black
        Me.lblProviderAddressline2.Location = New System.Drawing.Point(558, 34)
        Me.lblProviderAddressline2.Name = "lblProviderAddressline2"
        Me.lblProviderAddressline2.Size = New System.Drawing.Size(315, 14)
        Me.lblProviderAddressline2.TabIndex = 39
        Me.lblProviderAddressline2.Text = "Addressline2"
        Me.lblProviderAddressline2.UseMnemonic = False
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(964, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 128)
        Me.Label23.TabIndex = 37
        Me.Label23.Text = "label3"
        '
        'lbl_PrPhone
        '
        Me.lbl_PrPhone.AutoSize = True
        Me.lbl_PrPhone.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PrPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PrPhone.ForeColor = System.Drawing.Color.Black
        Me.lbl_PrPhone.Location = New System.Drawing.Point(139, 84)
        Me.lbl_PrPhone.Name = "lbl_PrPhone"
        Me.lbl_PrPhone.Size = New System.Drawing.Size(47, 14)
        Me.lbl_PrPhone.TabIndex = 36
        Me.lbl_PrPhone.Text = "Label3"
        Me.lbl_PrPhone.UseMnemonic = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(85, 84)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(50, 14)
        Me.Label24.TabIndex = 35
        Me.Label24.Text = "Phone :"
        '
        'lbl_Provider
        '
        Me.lbl_Provider.AutoEllipsis = True
        Me.lbl_Provider.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Provider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Provider.ForeColor = System.Drawing.Color.Black
        Me.lbl_Provider.Location = New System.Drawing.Point(139, 7)
        Me.lbl_Provider.Name = "lbl_Provider"
        Me.lbl_Provider.Size = New System.Drawing.Size(571, 17)
        Me.lbl_Provider.TabIndex = 26
        Me.lbl_Provider.Text = "Provider Name"
        Me.lbl_Provider.UseMnemonic = False
        '
        'lbl_ProviderName
        '
        Me.lbl_ProviderName.AutoSize = True
        Me.lbl_ProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderName.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderName.Location = New System.Drawing.Point(89, 9)
        Me.lbl_ProviderName.Name = "lbl_ProviderName"
        Me.lbl_ProviderName.Size = New System.Drawing.Size(46, 14)
        Me.lbl_ProviderName.TabIndex = 30
        Me.lbl_ProviderName.Text = "Name :"
        '
        'lbl_ProviderZIP
        '
        Me.lbl_ProviderZIP.AutoSize = True
        Me.lbl_ProviderZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderZIP.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderZIP.Location = New System.Drawing.Point(834, 59)
        Me.lbl_ProviderZIP.Name = "lbl_ProviderZIP"
        Me.lbl_ProviderZIP.Size = New System.Drawing.Size(47, 14)
        Me.lbl_ProviderZIP.TabIndex = 32
        Me.lbl_ProviderZIP.Text = "Label3"
        Me.lbl_ProviderZIP.UseMnemonic = False
        '
        'lbl_ProviderAdd
        '
        Me.lbl_ProviderAdd.AutoSize = True
        Me.lbl_ProviderAdd.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderAdd.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderAdd.Location = New System.Drawing.Point(62, 34)
        Me.lbl_ProviderAdd.Name = "lbl_ProviderAdd"
        Me.lbl_ProviderAdd.Size = New System.Drawing.Size(73, 14)
        Me.lbl_ProviderAdd.TabIndex = 29
        Me.lbl_ProviderAdd.Text = " Address 1 :"
        '
        'lbl_ProviderState
        '
        Me.lbl_ProviderState.AutoSize = True
        Me.lbl_ProviderState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderState.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderState.Location = New System.Drawing.Point(558, 59)
        Me.lbl_ProviderState.Name = "lbl_ProviderState"
        Me.lbl_ProviderState.Size = New System.Drawing.Size(47, 14)
        Me.lbl_ProviderState.TabIndex = 31
        Me.lbl_ProviderState.Text = "Label3"
        Me.lbl_ProviderState.UseMnemonic = False
        '
        'lbl_ProviderCity
        '
        Me.lbl_ProviderCity.AutoEllipsis = True
        Me.lbl_ProviderCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderCity.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderCity.Location = New System.Drawing.Point(139, 59)
        Me.lbl_ProviderCity.Name = "lbl_ProviderCity"
        Me.lbl_ProviderCity.Size = New System.Drawing.Size(315, 14)
        Me.lbl_ProviderCity.TabIndex = 33
        Me.lbl_ProviderCity.Text = "Label3"
        Me.lbl_ProviderCity.UseMnemonic = False
        '
        'lbl_ProvidCity
        '
        Me.lbl_ProvidCity.AutoSize = True
        Me.lbl_ProvidCity.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidCity.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProvidCity.Location = New System.Drawing.Point(100, 59)
        Me.lbl_ProvidCity.Name = "lbl_ProvidCity"
        Me.lbl_ProvidCity.Size = New System.Drawing.Size(35, 14)
        Me.lbl_ProvidCity.TabIndex = 25
        Me.lbl_ProvidCity.Text = "City :"
        '
        'lbl_ProviderAddress
        '
        Me.lbl_ProviderAddress.AutoEllipsis = True
        Me.lbl_ProviderAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderAddress.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProviderAddress.Location = New System.Drawing.Point(139, 34)
        Me.lbl_ProviderAddress.Name = "lbl_ProviderAddress"
        Me.lbl_ProviderAddress.Size = New System.Drawing.Size(315, 14)
        Me.lbl_ProviderAddress.TabIndex = 34
        Me.lbl_ProviderAddress.Text = "Addressline1"
        Me.lbl_ProviderAddress.UseMnemonic = False
        '
        'lbl_ProvidState
        '
        Me.lbl_ProvidState.AutoSize = True
        Me.lbl_ProvidState.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidState.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProvidState.Location = New System.Drawing.Point(504, 59)
        Me.lbl_ProvidState.Name = "lbl_ProvidState"
        Me.lbl_ProvidState.Size = New System.Drawing.Size(45, 14)
        Me.lbl_ProvidState.TabIndex = 27
        Me.lbl_ProvidState.Text = "State :"
        '
        'lbl_ProvidZIP
        '
        Me.lbl_ProvidZIP.AutoSize = True
        Me.lbl_ProvidZIP.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProvidZIP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProvidZIP.ForeColor = System.Drawing.Color.Black
        Me.lbl_ProvidZIP.Location = New System.Drawing.Point(799, 59)
        Me.lbl_ProvidZIP.Name = "lbl_ProvidZIP"
        Me.lbl_ProvidZIP.Size = New System.Drawing.Size(33, 14)
        Me.lbl_ProvidZIP.TabIndex = 28
        Me.lbl_ProvidZIP.Text = "ZIP :"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Location = New System.Drawing.Point(0, 129)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(965, 1)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(965, 1)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "label1"
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel13.BackgroundImage = CType(resources.GetObject("Panel13.BackgroundImage"), System.Drawing.Image)
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(1, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(965, 23)
        Me.Panel13.TabIndex = 82
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.Controls.Add(Me.Label14)
        Me.Panel14.Controls.Add(Me.Label15)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(965, 23)
        Me.Panel14.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.AutoEllipsis = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(200, 22)
        Me.Label14.TabIndex = 50
        Me.Label14.Text = "    Provider Information"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(965, 1)
        Me.Label15.TabIndex = 90
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 153)
        Me.Label16.TabIndex = 93
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnl_Base)
        Me.Panel1.Controls.Add(Me.pnlButton)
        Me.Panel1.Controls.Add(Me.lbl_BorderLEFT)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(966, 232)
        Me.Panel1.TabIndex = 35
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.White
        Me.pnl_Base.BackgroundImage = CType(resources.GetObject("pnl_Base.BackgroundImage"), System.Drawing.Image)
        Me.pnl_Base.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.lblDrugSubstitution)
        Me.pnl_Base.Controls.Add(Me.lblDrugWrittenDate)
        Me.pnl_Base.Controls.Add(Me.lblDrugDuration)
        Me.pnl_Base.Controls.Add(Me.lblDrugQuantitiyInformation)
        Me.pnl_Base.Controls.Add(Me.lblSubstitution)
        Me.pnl_Base.Controls.Add(Me.lblDateWritten)
        Me.pnl_Base.Controls.Add(Me.lblDuration)
        Me.pnl_Base.Controls.Add(Me.lblDrugQuantity)
        Me.pnl_Base.Controls.Add(Me.lblRefillQtyInformation)
        Me.pnl_Base.Controls.Add(Me.lblRefillQty)
        Me.pnl_Base.Controls.Add(Me.lblRefillQualifier)
        Me.pnl_Base.Controls.Add(Me.lblRefillQualifierInformation)
        Me.pnl_Base.Controls.Add(Me.Label17)
        Me.pnl_Base.Controls.Add(Me.lblNotes)
        Me.pnl_Base.Controls.Add(Me.lblDrugDescription)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Controls.Add(Me.lblDrugSummaryDetail)
        Me.pnl_Base.Controls.Add(Me.lblSIG)
        Me.pnl_Base.Controls.Add(Me.lblSigInformation)
        Me.pnl_Base.Controls.Add(Me.lblDrugDescriptionInformation)
        Me.pnl_Base.Controls.Add(Me.Label26)
        Me.pnl_Base.Controls.Add(Me.Label27)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(1, 23)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Size = New System.Drawing.Size(964, 206)
        Me.pnl_Base.TabIndex = 32
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(0, 205)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(964, 1)
        Me.lbl_pnlBottom.TabIndex = 38
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lblDrugSubstitution
        '
        Me.lblDrugSubstitution.AutoSize = True
        Me.lblDrugSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugSubstitution.ForeColor = System.Drawing.Color.Black
        Me.lblDrugSubstitution.Location = New System.Drawing.Point(888, 126)
        Me.lblDrugSubstitution.Name = "lblDrugSubstitution"
        Me.lblDrugSubstitution.Size = New System.Drawing.Size(28, 14)
        Me.lblDrugSubstitution.TabIndex = 40
        Me.lblDrugSubstitution.Text = "abc"
        Me.lblDrugSubstitution.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDrugSubstitution.UseMnemonic = False
        '
        'lblDrugWrittenDate
        '
        Me.lblDrugWrittenDate.AutoSize = True
        Me.lblDrugWrittenDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugWrittenDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugWrittenDate.ForeColor = System.Drawing.Color.Black
        Me.lblDrugWrittenDate.Location = New System.Drawing.Point(636, 126)
        Me.lblDrugWrittenDate.Name = "lblDrugWrittenDate"
        Me.lblDrugWrittenDate.Size = New System.Drawing.Size(28, 14)
        Me.lblDrugWrittenDate.TabIndex = 41
        Me.lblDrugWrittenDate.Text = "abc"
        Me.lblDrugWrittenDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDrugWrittenDate.UseMnemonic = False
        '
        'lblDrugDuration
        '
        Me.lblDrugDuration.AutoSize = True
        Me.lblDrugDuration.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugDuration.ForeColor = System.Drawing.Color.Black
        Me.lblDrugDuration.Location = New System.Drawing.Point(139, 126)
        Me.lblDrugDuration.Name = "lblDrugDuration"
        Me.lblDrugDuration.Size = New System.Drawing.Size(28, 14)
        Me.lblDrugDuration.TabIndex = 43
        Me.lblDrugDuration.Text = "abc"
        Me.lblDrugDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDrugDuration.UseMnemonic = False
        '
        'lblDrugQuantitiyInformation
        '
        Me.lblDrugQuantitiyInformation.AutoSize = True
        Me.lblDrugQuantitiyInformation.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugQuantitiyInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugQuantitiyInformation.ForeColor = System.Drawing.Color.Black
        Me.lblDrugQuantitiyInformation.Location = New System.Drawing.Point(139, 98)
        Me.lblDrugQuantitiyInformation.Name = "lblDrugQuantitiyInformation"
        Me.lblDrugQuantitiyInformation.Size = New System.Drawing.Size(28, 14)
        Me.lblDrugQuantitiyInformation.TabIndex = 42
        Me.lblDrugQuantitiyInformation.Text = "abc"
        Me.lblDrugQuantitiyInformation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblDrugQuantitiyInformation.UseMnemonic = False
        '
        'lblSubstitution
        '
        Me.lblSubstitution.AutoSize = True
        Me.lblSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubstitution.ForeColor = System.Drawing.Color.Black
        Me.lblSubstitution.Location = New System.Drawing.Point(805, 126)
        Me.lblSubstitution.Name = "lblSubstitution"
        Me.lblSubstitution.Size = New System.Drawing.Size(81, 14)
        Me.lblSubstitution.TabIndex = 34
        Me.lblSubstitution.Text = "Substitution :"
        Me.lblSubstitution.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDateWritten
        '
        Me.lblDateWritten.AutoSize = True
        Me.lblDateWritten.BackColor = System.Drawing.Color.Transparent
        Me.lblDateWritten.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateWritten.ForeColor = System.Drawing.Color.Black
        Me.lblDateWritten.Location = New System.Drawing.Point(547, 126)
        Me.lblDateWritten.Name = "lblDateWritten"
        Me.lblDateWritten.Size = New System.Drawing.Size(87, 14)
        Me.lblDateWritten.TabIndex = 36
        Me.lblDateWritten.Text = "Written Date :"
        Me.lblDateWritten.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = True
        Me.lblDuration.BackColor = System.Drawing.Color.Transparent
        Me.lblDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDuration.ForeColor = System.Drawing.Color.Black
        Me.lblDuration.Location = New System.Drawing.Point(41, 126)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(91, 14)
        Me.lblDuration.TabIndex = 33
        Me.lblDuration.Text = "Drug Duration :"
        Me.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDrugQuantity
        '
        Me.lblDrugQuantity.AutoSize = True
        Me.lblDrugQuantity.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugQuantity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugQuantity.ForeColor = System.Drawing.Color.Black
        Me.lblDrugQuantity.Location = New System.Drawing.Point(40, 98)
        Me.lblDrugQuantity.Name = "lblDrugQuantity"
        Me.lblDrugQuantity.Size = New System.Drawing.Size(92, 14)
        Me.lblDrugQuantity.TabIndex = 35
        Me.lblDrugQuantity.Text = "Drug Quantity :"
        Me.lblDrugQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRefillQtyInformation
        '
        Me.lblRefillQtyInformation.AutoSize = True
        Me.lblRefillQtyInformation.BackColor = System.Drawing.Color.Transparent
        Me.lblRefillQtyInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefillQtyInformation.ForeColor = System.Drawing.Color.Black
        Me.lblRefillQtyInformation.Location = New System.Drawing.Point(667, 98)
        Me.lblRefillQtyInformation.Name = "lblRefillQtyInformation"
        Me.lblRefillQtyInformation.Size = New System.Drawing.Size(28, 14)
        Me.lblRefillQtyInformation.TabIndex = 44
        Me.lblRefillQtyInformation.Text = "abc"
        Me.lblRefillQtyInformation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRefillQtyInformation.UseMnemonic = False
        '
        'lblRefillQty
        '
        Me.lblRefillQty.AutoSize = True
        Me.lblRefillQty.BackColor = System.Drawing.Color.Transparent
        Me.lblRefillQty.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefillQty.ForeColor = System.Drawing.Color.Black
        Me.lblRefillQty.Location = New System.Drawing.Point(590, 98)
        Me.lblRefillQty.Name = "lblRefillQty"
        Me.lblRefillQty.Size = New System.Drawing.Size(44, 14)
        Me.lblRefillQty.TabIndex = 37
        Me.lblRefillQty.Text = "Refills :"
        Me.lblRefillQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRefillQualifier
        '
        Me.lblRefillQualifier.AutoSize = True
        Me.lblRefillQualifier.BackColor = System.Drawing.Color.Transparent
        Me.lblRefillQualifier.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefillQualifier.ForeColor = System.Drawing.Color.Black
        Me.lblRefillQualifier.Location = New System.Drawing.Point(800, 98)
        Me.lblRefillQualifier.Name = "lblRefillQualifier"
        Me.lblRefillQualifier.Size = New System.Drawing.Size(86, 14)
        Me.lblRefillQualifier.TabIndex = 39
        Me.lblRefillQualifier.Text = "Refill Qualifier :"
        Me.lblRefillQualifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRefillQualifierInformation
        '
        Me.lblRefillQualifierInformation.AutoSize = True
        Me.lblRefillQualifierInformation.BackColor = System.Drawing.Color.Transparent
        Me.lblRefillQualifierInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefillQualifierInformation.ForeColor = System.Drawing.Color.Black
        Me.lblRefillQualifierInformation.Location = New System.Drawing.Point(888, 98)
        Me.lblRefillQualifierInformation.Name = "lblRefillQualifierInformation"
        Me.lblRefillQualifierInformation.Size = New System.Drawing.Size(28, 14)
        Me.lblRefillQualifierInformation.TabIndex = 45
        Me.lblRefillQualifierInformation.Text = "abc"
        Me.lblRefillQualifierInformation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRefillQualifierInformation.UseMnemonic = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(29, 154)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(103, 14)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Pharmacy Notes :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNotes
        '
        Me.lblNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.ForeColor = System.Drawing.Color.Black
        Me.lblNotes.Location = New System.Drawing.Point(139, 154)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(794, 45)
        Me.lblNotes.TabIndex = 31
        Me.lblNotes.Text = "abc"
        Me.lblNotes.UseMnemonic = False
        '
        'lblDrugDescription
        '
        Me.lblDrugDescription.AutoSize = True
        Me.lblDrugDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugDescription.ForeColor = System.Drawing.Color.Black
        Me.lblDrugDescription.Location = New System.Drawing.Point(27, 37)
        Me.lblDrugDescription.Name = "lblDrugDescription"
        Me.lblDrugDescription.Size = New System.Drawing.Size(105, 14)
        Me.lblDrugDescription.TabIndex = 0
        Me.lblDrugDescription.Text = "Drug Description :"
        Me.lblDrugDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(964, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'lblDrugSummaryDetail
        '
        Me.lblDrugSummaryDetail.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugSummaryDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugSummaryDetail.ForeColor = System.Drawing.Color.Black
        Me.lblDrugSummaryDetail.Location = New System.Drawing.Point(139, 7)
        Me.lblDrugSummaryDetail.Name = "lblDrugSummaryDetail"
        Me.lblDrugSummaryDetail.Size = New System.Drawing.Size(792, 30)
        Me.lblDrugSummaryDetail.TabIndex = 28
        Me.lblDrugSummaryDetail.Text = "abc"
        Me.lblDrugSummaryDetail.UseMnemonic = False
        '
        'lblSIG
        '
        Me.lblSIG.AutoSize = True
        Me.lblSIG.BackColor = System.Drawing.Color.Transparent
        Me.lblSIG.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSIG.ForeColor = System.Drawing.Color.Black
        Me.lblSIG.Location = New System.Drawing.Point(21, 70)
        Me.lblSIG.Name = "lblSIG"
        Me.lblSIG.Size = New System.Drawing.Size(111, 14)
        Me.lblSIG.TabIndex = 2
        Me.lblSIG.Text = "Patient Directions :"
        Me.lblSIG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSigInformation
        '
        Me.lblSigInformation.BackColor = System.Drawing.Color.Transparent
        Me.lblSigInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSigInformation.ForeColor = System.Drawing.Color.Black
        Me.lblSigInformation.Location = New System.Drawing.Point(139, 71)
        Me.lblSigInformation.Name = "lblSigInformation"
        Me.lblSigInformation.Size = New System.Drawing.Size(794, 30)
        Me.lblSigInformation.TabIndex = 22
        Me.lblSigInformation.Text = "Directions"
        Me.lblSigInformation.UseMnemonic = False
        '
        'lblDrugDescriptionInformation
        '
        Me.lblDrugDescriptionInformation.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugDescriptionInformation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugDescriptionInformation.ForeColor = System.Drawing.Color.Black
        Me.lblDrugDescriptionInformation.Location = New System.Drawing.Point(139, 39)
        Me.lblDrugDescriptionInformation.Name = "lblDrugDescriptionInformation"
        Me.lblDrugDescriptionInformation.Size = New System.Drawing.Size(792, 30)
        Me.lblDrugDescriptionInformation.TabIndex = 21
        Me.lblDrugDescriptionInformation.Text = "Drug Description"
        Me.lblDrugDescriptionInformation.UseMnemonic = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(3, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(129, 16)
        Me.Label26.TabIndex = 27
        Me.Label26.Text = "(For Internal Use Only)"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(3, 7)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(129, 14)
        Me.Label27.TabIndex = 27
        Me.Label27.Text = "Drug Summary Detail :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pnlButton
        '
        Me.pnlButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlButton.BackgroundImage = CType(resources.GetObject("pnlButton.BackgroundImage"), System.Drawing.Image)
        Me.pnlButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlButton.Controls.Add(Me.pnlcode_Name)
        Me.pnlButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButton.Location = New System.Drawing.Point(1, 0)
        Me.pnlButton.Name = "pnlButton"
        Me.pnlButton.Size = New System.Drawing.Size(964, 23)
        Me.pnlButton.TabIndex = 82
        '
        'pnlcode_Name
        '
        Me.pnlcode_Name.BackColor = System.Drawing.Color.Transparent
        Me.pnlcode_Name.Controls.Add(Me.lblPtName)
        Me.pnlcode_Name.Controls.Add(Me.lbl_BorderTOP)
        Me.pnlcode_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlcode_Name.Location = New System.Drawing.Point(0, 0)
        Me.pnlcode_Name.Name = "pnlcode_Name"
        Me.pnlcode_Name.Size = New System.Drawing.Size(964, 23)
        Me.pnlcode_Name.TabIndex = 5
        '
        'lblPtName
        '
        Me.lblPtName.AutoEllipsis = True
        Me.lblPtName.BackColor = System.Drawing.Color.Transparent
        Me.lblPtName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPtName.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPtName.ForeColor = System.Drawing.Color.Black
        Me.lblPtName.Location = New System.Drawing.Point(0, 1)
        Me.lblPtName.Name = "lblPtName"
        Me.lblPtName.Size = New System.Drawing.Size(200, 22)
        Me.lblPtName.TabIndex = 50
        Me.lblPtName.Text = "    Drug Summary"
        Me.lblPtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_BorderTOP
        '
        Me.lbl_BorderTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lbl_BorderTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_BorderTOP.Location = New System.Drawing.Point(0, 0)
        Me.lbl_BorderTOP.Name = "lbl_BorderTOP"
        Me.lbl_BorderTOP.Size = New System.Drawing.Size(964, 1)
        Me.lbl_BorderTOP.TabIndex = 90
        '
        'lbl_BorderLEFT
        '
        Me.lbl_BorderLEFT.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lbl_BorderLEFT.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_BorderLEFT.Location = New System.Drawing.Point(0, 0)
        Me.lbl_BorderLEFT.Name = "lbl_BorderLEFT"
        Me.lbl_BorderLEFT.Size = New System.Drawing.Size(1, 229)
        Me.lbl_BorderLEFT.TabIndex = 93
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Location = New System.Drawing.Point(965, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 229)
        Me.Label28.TabIndex = 94
        '
        'pnlWebBrowser
        '
        Me.pnlWebBrowser.Controls.Add(Me.Label8)
        Me.pnlWebBrowser.Controls.Add(Me.Label7)
        Me.pnlWebBrowser.Controls.Add(Me.Label6)
        Me.pnlWebBrowser.Controls.Add(Me.Label5)
        Me.pnlWebBrowser.Controls.Add(Me.WebBrowser1)
        Me.pnlWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWebBrowser.Location = New System.Drawing.Point(0, 54)
        Me.pnlWebBrowser.Name = "pnlWebBrowser"
        Me.pnlWebBrowser.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWebBrowser.Size = New System.Drawing.Size(972, 693)
        Me.pnlWebBrowser.TabIndex = 46
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(968, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 685)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 685)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(966, 1)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(3, 689)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(966, 1)
        Me.Label5.TabIndex = 39
        Me.Label5.Text = "label2"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(966, 687)
        Me.WebBrowser1.TabIndex = 0
        '
        'pnlPDRListView
        '
        Me.pnlPDRListView.Controls.Add(Me.lstPDRPrograms)
        Me.pnlPDRListView.Controls.Add(Me.Label38)
        Me.pnlPDRListView.Controls.Add(Me.Label34)
        Me.pnlPDRListView.Controls.Add(Me.Label41)
        Me.pnlPDRListView.Controls.Add(Me.Label42)
        Me.pnlPDRListView.Controls.Add(Me.label48)
        Me.pnlPDRListView.Controls.Add(Me.label71)
        Me.pnlPDRListView.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPDRListView.Location = New System.Drawing.Point(0, 82)
        Me.pnlPDRListView.Name = "pnlPDRListView"
        Me.pnlPDRListView.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlPDRListView.Size = New System.Drawing.Size(972, 58)
        Me.pnlPDRListView.TabIndex = 10
        Me.pnlPDRListView.Visible = False
        '
        'lstPDRPrograms
        '
        Me.lstPDRPrograms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstPDRPrograms.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPDRProgram})
        Me.lstPDRPrograms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPDRPrograms.LabelWrap = False
        Me.lstPDRPrograms.Location = New System.Drawing.Point(9, 9)
        Me.lstPDRPrograms.Name = "lstPDRPrograms"
        Me.lstPDRPrograms.Size = New System.Drawing.Size(959, 48)
        Me.lstPDRPrograms.SmallImageList = Me.ImgList
        Me.lstPDRPrograms.TabIndex = 14
        Me.lstPDRPrograms.UseCompatibleStateImageBehavior = False
        Me.lstPDRPrograms.View = System.Windows.Forms.View.Tile
        '
        'colPDRProgram
        '
        Me.colPDRProgram.Text = "Program"
        '
        'ImgList
        '
        Me.ImgList.ImageStream = CType(resources.GetObject("ImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList.Images.SetKeyName(0, "PDR.ico")
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.White
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(9, 4)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(959, 5)
        Me.Label38.TabIndex = 16
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.White
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(4, 4)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(5, 53)
        Me.Label34.TabIndex = 15
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(4, 57)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(964, 1)
        Me.Label41.TabIndex = 13
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 4)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 54)
        Me.Label42.TabIndex = 12
        Me.Label42.Text = "label4"
        '
        'label48
        '
        Me.label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label48.Location = New System.Drawing.Point(968, 4)
        Me.label48.Name = "label48"
        Me.label48.Size = New System.Drawing.Size(1, 54)
        Me.label48.TabIndex = 11
        Me.label48.Text = "label3"
        '
        'label71
        '
        Me.label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label71.Dock = System.Windows.Forms.DockStyle.Top
        Me.label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label71.Location = New System.Drawing.Point(3, 3)
        Me.label71.Name = "label71"
        Me.label71.Size = New System.Drawing.Size(966, 1)
        Me.label71.TabIndex = 10
        Me.label71.Text = "label1"
        '
        'pnlPDRListViewHeader
        '
        Me.pnlPDRListViewHeader.Controls.Add(Me.Panel16)
        Me.pnlPDRListViewHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPDRListViewHeader.Location = New System.Drawing.Point(0, 54)
        Me.pnlPDRListViewHeader.Name = "pnlPDRListViewHeader"
        Me.pnlPDRListViewHeader.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlPDRListViewHeader.Size = New System.Drawing.Size(972, 28)
        Me.pnlPDRListViewHeader.TabIndex = 16
        Me.pnlPDRListViewHeader.Visible = False
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Transparent
        Me.Panel16.BackgroundImage = CType(resources.GetObject("Panel16.BackgroundImage"), System.Drawing.Image)
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Label43)
        Me.Panel16.Controls.Add(Me.btnUp)
        Me.Panel16.Controls.Add(Me.btnDown)
        Me.Panel16.Controls.Add(Me.Label44)
        Me.Panel16.Controls.Add(Me.Label45)
        Me.Panel16.Controls.Add(Me.Label46)
        Me.Panel16.Controls.Add(Me.Label47)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(3, 3)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(966, 25)
        Me.Panel16.TabIndex = 0
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.White
        Me.Label43.Location = New System.Drawing.Point(1, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(916, 23)
        Me.Label43.TabIndex = 2
        Me.Label43.Text = "  PDR Programs"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(917, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 23)
        Me.btnUp.TabIndex = 1
        Me.btnUp.UseVisualStyleBackColor = False
        Me.btnUp.Visible = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(941, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 23)
        Me.btnDown.TabIndex = 0
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(1, 24)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(964, 1)
        Me.Label44.TabIndex = 12
        Me.Label44.Text = "label2"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 24)
        Me.Label45.TabIndex = 11
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(965, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 24)
        Me.Label46.TabIndex = 10
        Me.Label46.Text = "label3"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(0, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(966, 1)
        Me.Label47.TabIndex = 9
        Me.Label47.Text = "label1"
        '
        'frmeRxSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(972, 747)
        Me.Controls.Add(Me.pnlDrugSummary)
        Me.Controls.Add(Me.pnlPDRListView)
        Me.Controls.Add(Me.pnlPDRListViewHeader)
        Me.Controls.Add(Me.pnlWebBrowser)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmeRxSummary"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Electronic Rx Review"
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlDrugSummary.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.pnlButton.ResumeLayout(False)
        Me.pnlcode_Name.ResumeLayout(False)
        Me.pnlWebBrowser.ResumeLayout(False)
        Me.pnlPDRListView.ResumeLayout(False)
        Me.pnlPDRListViewHeader.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents pnlDrugSummary As System.Windows.Forms.Panel
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Panel7 As System.Windows.Forms.Panel
    Public WithEvents lbl_PharmacyZip As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lbl_Pharmacy As System.Windows.Forms.Label
    Public WithEvents lbl_PharmacyName As System.Windows.Forms.Label
    Public WithEvents lbl_PharmacyAdd As System.Windows.Forms.Label
    Public WithEvents lbl_PharmacyState As System.Windows.Forms.Label
    Public WithEvents lbl_PharmacyAddress As System.Windows.Forms.Label
    Public WithEvents lbl_PharmState As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Panel12 As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents lbl_PrPhone As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents lbl_Provider As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderName As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderZIP As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderAdd As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderState As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderCity As System.Windows.Forms.Label
    Public WithEvents lbl_ProvidCity As System.Windows.Forms.Label
    Public WithEvents lbl_ProviderAddress As System.Windows.Forms.Label
    Public WithEvents lbl_ProvidState As System.Windows.Forms.Label
    Public WithEvents lbl_ProvidZIP As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblNotes As System.Windows.Forms.Label
    Public WithEvents lblDrugDescription As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Public WithEvents lblDrugSummaryDetail As System.Windows.Forms.Label
    Public WithEvents lblSIG As System.Windows.Forms.Label
    Public WithEvents lblSigInformation As System.Windows.Forms.Label
    Public WithEvents lblDrugDescriptionInformation As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents pnlButton As System.Windows.Forms.Panel
    Friend WithEvents pnlcode_Name As System.Windows.Forms.Panel
    Friend WithEvents lblPtName As System.Windows.Forms.Label
    Private WithEvents lbl_BorderTOP As System.Windows.Forms.Label
    Private WithEvents lbl_BorderLEFT As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lbl_PatAdd As System.Windows.Forms.Label
    Public WithEvents lbl_Patient As System.Windows.Forms.Label
    Public WithEvents lbl_ZIP As System.Windows.Forms.Label
    Public WithEvents lbl_PatientName As System.Windows.Forms.Label
    Public WithEvents lbl_ZIPCode As System.Windows.Forms.Label
    Public WithEvents lbl_State As System.Windows.Forms.Label
    Public WithEvents lbl_PatientAddress As System.Windows.Forms.Label
    Public WithEvents lbl_PatientPhone As System.Windows.Forms.Label
    Public WithEvents lbl_StateName As System.Windows.Forms.Label
    Public WithEvents lbl_PatientPhoneNo As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents lblPhAddress2 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents lblPatAddress2 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblProviderAddressline2 As System.Windows.Forms.Label
    Public WithEvents lblPhNpi As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents lblPhFax As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents lbl_DOB As System.Windows.Forms.Label
    Public WithEvents lbl_PatDOB As System.Windows.Forms.Label
    Public WithEvents lbl_Gender As System.Windows.Forms.Label
    Public WithEvents lbl_PatGender As System.Windows.Forms.Label
    Public WithEvents lbl_City As System.Windows.Forms.Label
    Public WithEvents lbl_CityName As System.Windows.Forms.Label
    Public WithEvents lblPatientFax As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents lblProviderFax As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents lblProviderNPI As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Public WithEvents lblDrugSubstitution As System.Windows.Forms.Label
    Public WithEvents lblDrugWrittenDate As System.Windows.Forms.Label
    Public WithEvents lblDrugDuration As System.Windows.Forms.Label
    Public WithEvents lblDrugQuantitiyInformation As System.Windows.Forms.Label
    Public WithEvents lblSubstitution As System.Windows.Forms.Label
    Public WithEvents lblDateWritten As System.Windows.Forms.Label
    Public WithEvents lblDuration As System.Windows.Forms.Label
    Public WithEvents lblDrugQuantity As System.Windows.Forms.Label
    Public WithEvents lblRefillQtyInformation As System.Windows.Forms.Label
    Public WithEvents lblRefillQty As System.Windows.Forms.Label
    Public WithEvents lblRefillQualifier As System.Windows.Forms.Label
    Public WithEvents lblRefillQualifierInformation As System.Windows.Forms.Label
    Public WithEvents lbl_PharamcyCity As System.Windows.Forms.Label
    Public WithEvents lbl_PharmCity As System.Windows.Forms.Label
    Public WithEvents lbl_PharmacyPhoneNo As System.Windows.Forms.Label
    Public WithEvents pbl_PharmacyPhone As System.Windows.Forms.Label
    Friend WithEvents pnlWebBrowser As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents tlbbtnNewRx As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtnApprove As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtnApprovewithChanges As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtnDTNF As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtnComparedata As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlPDRListView As System.Windows.Forms.Panel
    Friend WithEvents lstPDRPrograms As System.Windows.Forms.ListView
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents label48 As System.Windows.Forms.Label
    Private WithEvents label71 As System.Windows.Forms.Label
    Private WithEvents pnlPDRListViewHeader As System.Windows.Forms.Panel
    Private WithEvents Panel16 As System.Windows.Forms.Panel
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents btnUp As System.Windows.Forms.Button
    Private WithEvents btnDown As System.Windows.Forms.Button
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents ImgList As System.Windows.Forms.ImageList
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents colPDRProgram As System.Windows.Forms.ColumnHeader
    Friend WithEvents tlbbtnPDR As System.Windows.Forms.ToolStripButton

End Class
