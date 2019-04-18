<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmApproove
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirmApproove))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlPharmacyInformation = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.label100 = New System.Windows.Forms.Label()
        Me.lblPharmacyName = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.lblNCPDPId = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlPharmacy = New System.Windows.Forms.Panel()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblMDPharmacyName = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.lblMDNCPDPId = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.pnlProviderInformation = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.lblFax = New System.Windows.Forms.Label()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.lblDEA = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.lblAddress2 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.lblAddress1 = New System.Windows.Forms.Label()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.lblNPI = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlProviderInfo = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblMDZip = New System.Windows.Forms.Label()
        Me.lblMDSSN = New System.Windows.Forms.Label()
        Me.lblSSN = New System.Windows.Forms.Label()
        Me.lblMDFax = New System.Windows.Forms.Label()
        Me.lblMDPhone = New System.Windows.Forms.Label()
        Me.lblMDState = New System.Windows.Forms.Label()
        Me.lblMDDEA = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblMDCity = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.lblMDAddress2 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.lblMDAddress1 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.lblMDNPI = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.pnlDrugSummary = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblRefills = New System.Windows.Forms.Label()
        Me.lblPotencyCode = New System.Windows.Forms.Label()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.lblSubstitution = New System.Windows.Forms.Label()
        Me.lblRefQlf = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNDCCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDirection = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDrug = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlflexgrid = New System.Windows.Forms.Panel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblMDRefills = New System.Windows.Forms.Label()
        Me.lblMDPotencyCode = New System.Windows.Forms.Label()
        Me.lblMDDuration = New System.Windows.Forms.Label()
        Me.lblMDSubstitution = New System.Windows.Forms.Label()
        Me.lblMDRefQlf = New System.Windows.Forms.Label()
        Me.lblMDQty = New System.Windows.Forms.Label()
        Me.lblMDNotes = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.lblMDNDCCode = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblMDDirection = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblMDDrug = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblGridTop = New System.Windows.Forms.Label()
        Me.pnlMids = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblLightHeader = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.pnlheader = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.lblResponse = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblRequest = New System.Windows.Forms.Label()
        Me.tlsp_Approve = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtnApprovewithChanges = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtnDTNF = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.pnlMain.SuspendLayout()
        Me.pnlPharmacyInformation.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.pnlPharmacy.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlProviderInformation.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnlProviderInfo.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlDrugSummary.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlflexgrid.SuspendLayout()
        Me.pnlMids.SuspendLayout()
        Me.pnlheader.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.tlsp_Approve.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.pnlPharmacyInformation)
        Me.pnlMain.Controls.Add(Me.pnlProviderInformation)
        Me.pnlMain.Controls.Add(Me.pnlDrugSummary)
        Me.pnlMain.Controls.Add(Me.pnlheader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMain.Size = New System.Drawing.Size(1109, 477)
        Me.pnlMain.TabIndex = 17
        '
        'pnlPharmacyInformation
        '
        Me.pnlPharmacyInformation.Controls.Add(Me.Panel16)
        Me.pnlPharmacyInformation.Controls.Add(Me.Label23)
        Me.pnlPharmacyInformation.Controls.Add(Me.pnlPharmacy)
        Me.pnlPharmacyInformation.Controls.Add(Me.Label71)
        Me.pnlPharmacyInformation.Controls.Add(Me.Panel9)
        Me.pnlPharmacyInformation.Controls.Add(Me.Label75)
        Me.pnlPharmacyInformation.Controls.Add(Me.Label76)
        Me.pnlPharmacyInformation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPharmacyInformation.Location = New System.Drawing.Point(10, 400)
        Me.pnlPharmacyInformation.Name = "pnlPharmacyInformation"
        Me.pnlPharmacyInformation.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlPharmacyInformation.Size = New System.Drawing.Size(1089, 65)
        Me.pnlPharmacyInformation.TabIndex = 48
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Label42)
        Me.Panel16.Controls.Add(Me.label100)
        Me.Panel16.Controls.Add(Me.lblPharmacyName)
        Me.Panel16.Controls.Add(Me.Label133)
        Me.Panel16.Controls.Add(Me.lblNCPDPId)
        Me.Panel16.Controls.Add(Me.Label137)
        Me.Panel16.Controls.Add(Me.Label9)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel16.ForeColor = System.Drawing.Color.Black
        Me.Panel16.Location = New System.Drawing.Point(546, 30)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(542, 34)
        Me.Panel16.TabIndex = 56
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 34)
        Me.Label42.TabIndex = 73
        Me.Label42.Text = "label4"
        '
        'label100
        '
        Me.label100.AutoSize = True
        Me.label100.BackColor = System.Drawing.Color.Transparent
        Me.label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label100.ForeColor = System.Drawing.Color.Black
        Me.label100.Location = New System.Drawing.Point(17, 10)
        Me.label100.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label100.Name = "label100"
        Me.label100.Size = New System.Drawing.Size(46, 14)
        Me.label100.TabIndex = 70
        Me.label100.Text = "Name :"
        Me.label100.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPharmacyName
        '
        Me.lblPharmacyName.AutoSize = True
        Me.lblPharmacyName.BackColor = System.Drawing.Color.Transparent
        Me.lblPharmacyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPharmacyName.ForeColor = System.Drawing.Color.Black
        Me.lblPharmacyName.Location = New System.Drawing.Point(118, 10)
        Me.lblPharmacyName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPharmacyName.Name = "lblPharmacyName"
        Me.lblPharmacyName.Size = New System.Drawing.Size(40, 14)
        Me.lblPharmacyName.TabIndex = 72
        Me.lblPharmacyName.Text = "Name"
        Me.lblPharmacyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label133
        '
        Me.Label133.AutoSize = True
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Location = New System.Drawing.Point(143, 343)
        Me.Label133.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(57, 14)
        Me.Label133.TabIndex = 55
        Me.Label133.Text = "Potency"
        Me.Label133.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNCPDPId
        '
        Me.lblNCPDPId.AutoSize = True
        Me.lblNCPDPId.BackColor = System.Drawing.Color.Transparent
        Me.lblNCPDPId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNCPDPId.ForeColor = System.Drawing.Color.Black
        Me.lblNCPDPId.Location = New System.Drawing.Point(442, 10)
        Me.lblNCPDPId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNCPDPId.Name = "lblNCPDPId"
        Me.lblNCPDPId.Size = New System.Drawing.Size(61, 14)
        Me.lblNCPDPId.TabIndex = 31
        Me.lblNCPDPId.Text = "NCPDPId"
        Me.lblNCPDPId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label137
        '
        Me.Label137.AutoSize = True
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label137.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Location = New System.Drawing.Point(15, 343)
        Me.Label137.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(122, 14)
        Me.Label137.TabIndex = 45
        Me.Label137.Text = "Drug Potency Code :"
        Me.Label137.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(374, 10)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 14)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "NCPDP ID :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(543, 30)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(3, 34)
        Me.Label23.TabIndex = 62
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlPharmacy
        '
        Me.pnlPharmacy.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.pnlPharmacy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPharmacy.Controls.Add(Me.Label44)
        Me.pnlPharmacy.Controls.Add(Me.Label15)
        Me.pnlPharmacy.Controls.Add(Me.lblMDPharmacyName)
        Me.pnlPharmacy.Controls.Add(Me.Label47)
        Me.pnlPharmacy.Controls.Add(Me.lblMDNCPDPId)
        Me.pnlPharmacy.Controls.Add(Me.Label54)
        Me.pnlPharmacy.Controls.Add(Me.Label32)
        Me.pnlPharmacy.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPharmacy.ForeColor = System.Drawing.Color.Black
        Me.pnlPharmacy.Location = New System.Drawing.Point(1, 30)
        Me.pnlPharmacy.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlPharmacy.Name = "pnlPharmacy"
        Me.pnlPharmacy.Size = New System.Drawing.Size(542, 34)
        Me.pnlPharmacy.TabIndex = 56
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(541, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 34)
        Me.Label44.TabIndex = 76
        Me.Label44.Text = "label4"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(14, 10)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(46, 14)
        Me.Label15.TabIndex = 74
        Me.Label15.Text = "Name :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDPharmacyName
        '
        Me.lblMDPharmacyName.AutoSize = True
        Me.lblMDPharmacyName.BackColor = System.Drawing.Color.Transparent
        Me.lblMDPharmacyName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDPharmacyName.ForeColor = System.Drawing.Color.Black
        Me.lblMDPharmacyName.Location = New System.Drawing.Point(114, 10)
        Me.lblMDPharmacyName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDPharmacyName.Name = "lblMDPharmacyName"
        Me.lblMDPharmacyName.Size = New System.Drawing.Size(40, 14)
        Me.lblMDPharmacyName.TabIndex = 75
        Me.lblMDPharmacyName.Text = "Name"
        Me.lblMDPharmacyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Location = New System.Drawing.Point(143, 343)
        Me.Label47.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(57, 14)
        Me.Label47.TabIndex = 55
        Me.Label47.Text = "Potency"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDNCPDPId
        '
        Me.lblMDNCPDPId.AutoSize = True
        Me.lblMDNCPDPId.BackColor = System.Drawing.Color.Transparent
        Me.lblMDNCPDPId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDNCPDPId.ForeColor = System.Drawing.Color.Black
        Me.lblMDNCPDPId.Location = New System.Drawing.Point(435, 10)
        Me.lblMDNCPDPId.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDNCPDPId.Name = "lblMDNCPDPId"
        Me.lblMDNCPDPId.Size = New System.Drawing.Size(61, 14)
        Me.lblMDNCPDPId.TabIndex = 53
        Me.lblMDNCPDPId.Text = "NCPDPId"
        Me.lblMDNCPDPId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Location = New System.Drawing.Point(15, 343)
        Me.Label54.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(122, 14)
        Me.Label54.TabIndex = 45
        Me.Label54.Text = "Drug Potency Code :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(364, 10)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(68, 14)
        Me.Label32.TabIndex = 43
        Me.Label32.Text = "NCPDP ID :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(1, 64)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1087, 1)
        Me.Label71.TabIndex = 60
        Me.Label71.Text = "label1"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label72)
        Me.Panel9.Controls.Add(Me.Label73)
        Me.Panel9.Controls.Add(Me.Label74)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(1, 5)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1087, 25)
        Me.Panel9.TabIndex = 45
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(0, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1087, 1)
        Me.Label72.TabIndex = 14
        Me.Label72.Text = "label1"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label73.Location = New System.Drawing.Point(0, 24)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1087, 1)
        Me.Label73.TabIndex = 13
        Me.Label73.Text = "label2"
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label74.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.White
        Me.Label74.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label74.Location = New System.Drawing.Point(0, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1087, 25)
        Me.Label74.TabIndex = 9
        Me.Label74.Text = "   Pharmacy Information"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 5)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 60)
        Me.Label75.TabIndex = 58
        Me.Label75.Text = "label4"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(1088, 5)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 60)
        Me.Label76.TabIndex = 59
        Me.Label76.Text = "label3"
        '
        'pnlProviderInformation
        '
        Me.pnlProviderInformation.Controls.Add(Me.Panel14)
        Me.pnlProviderInformation.Controls.Add(Me.Label22)
        Me.pnlProviderInformation.Controls.Add(Me.pnlProviderInfo)
        Me.pnlProviderInformation.Controls.Add(Me.Label94)
        Me.pnlProviderInformation.Controls.Add(Me.Panel7)
        Me.pnlProviderInformation.Controls.Add(Me.Label105)
        Me.pnlProviderInformation.Controls.Add(Me.Label95)
        Me.pnlProviderInformation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProviderInformation.Location = New System.Drawing.Point(10, 234)
        Me.pnlProviderInformation.Name = "pnlProviderInformation"
        Me.pnlProviderInformation.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlProviderInformation.Size = New System.Drawing.Size(1089, 166)
        Me.pnlProviderInformation.TabIndex = 47
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label25)
        Me.Panel14.Controls.Add(Me.lblZip)
        Me.Panel14.Controls.Add(Me.Label103)
        Me.Panel14.Controls.Add(Me.lblFax)
        Me.Panel14.Controls.Add(Me.lblPhone)
        Me.Panel14.Controls.Add(Me.lblState)
        Me.Panel14.Controls.Add(Me.Label109)
        Me.Panel14.Controls.Add(Me.lblDEA)
        Me.Panel14.Controls.Add(Me.Label111)
        Me.Panel14.Controls.Add(Me.lblCity)
        Me.Panel14.Controls.Add(Me.Label113)
        Me.Panel14.Controls.Add(Me.Label114)
        Me.Panel14.Controls.Add(Me.Label115)
        Me.Panel14.Controls.Add(Me.lblAddress2)
        Me.Panel14.Controls.Add(Me.Label117)
        Me.Panel14.Controls.Add(Me.Label118)
        Me.Panel14.Controls.Add(Me.lblAddress1)
        Me.Panel14.Controls.Add(Me.Label120)
        Me.Panel14.Controls.Add(Me.Label121)
        Me.Panel14.Controls.Add(Me.Label122)
        Me.Panel14.Controls.Add(Me.lblNPI)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel14.ForeColor = System.Drawing.Color.Black
        Me.Panel14.Location = New System.Drawing.Point(546, 30)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(542, 135)
        Me.Panel14.TabIndex = 15
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 135)
        Me.Label25.TabIndex = 79
        Me.Label25.Text = "label4"
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.Black
        Me.lblZip.Location = New System.Drawing.Point(442, 82)
        Me.lblZip.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(25, 14)
        Me.lblZip.TabIndex = 78
        Me.lblZip.Text = "Zip"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Location = New System.Drawing.Point(143, 343)
        Me.Label103.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(57, 14)
        Me.Label103.TabIndex = 55
        Me.Label103.Text = "Potency"
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFax
        '
        Me.lblFax.AutoSize = True
        Me.lblFax.BackColor = System.Drawing.Color.Transparent
        Me.lblFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFax.ForeColor = System.Drawing.Color.Black
        Me.lblFax.Location = New System.Drawing.Point(118, 106)
        Me.lblFax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFax.Name = "lblFax"
        Me.lblFax.Size = New System.Drawing.Size(26, 14)
        Me.lblFax.TabIndex = 75
        Me.lblFax.Text = "fax"
        Me.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.BackColor = System.Drawing.Color.Transparent
        Me.lblPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhone.ForeColor = System.Drawing.Color.Black
        Me.lblPhone.Location = New System.Drawing.Point(277, 106)
        Me.lblPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(46, 14)
        Me.lblPhone.TabIndex = 74
        Me.lblPhone.Text = "Phone"
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.Black
        Me.lblState.Location = New System.Drawing.Point(277, 82)
        Me.lblState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(41, 14)
        Me.lblState.TabIndex = 73
        Me.lblState.Text = "State"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Location = New System.Drawing.Point(15, 343)
        Me.Label109.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(122, 14)
        Me.Label109.TabIndex = 45
        Me.Label109.Text = "Drug Potency Code :"
        Me.Label109.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDEA
        '
        Me.lblDEA.AutoSize = True
        Me.lblDEA.BackColor = System.Drawing.Color.Transparent
        Me.lblDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDEA.ForeColor = System.Drawing.Color.Black
        Me.lblDEA.Location = New System.Drawing.Point(442, 10)
        Me.lblDEA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDEA.Name = "lblDEA"
        Me.lblDEA.Size = New System.Drawing.Size(32, 14)
        Me.lblDEA.TabIndex = 69
        Me.lblDEA.Text = "DEA"
        Me.lblDEA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.Color.Transparent
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.ForeColor = System.Drawing.Color.Black
        Me.Label111.Location = New System.Drawing.Point(404, 10)
        Me.Label111.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(38, 14)
        Me.Label111.TabIndex = 58
        Me.Label111.Text = "DEA :"
        Me.Label111.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.Black
        Me.lblCity.Location = New System.Drawing.Point(118, 82)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(31, 14)
        Me.lblCity.TabIndex = 72
        Me.lblCity.Text = "City"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label113
        '
        Me.Label113.AutoSize = True
        Me.Label113.BackColor = System.Drawing.Color.Transparent
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.Black
        Me.Label113.Location = New System.Drawing.Point(227, 106)
        Me.Label113.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(50, 14)
        Me.Label113.TabIndex = 64
        Me.Label113.Text = "Phone :"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label114
        '
        Me.Label114.AutoSize = True
        Me.Label114.BackColor = System.Drawing.Color.Transparent
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.ForeColor = System.Drawing.Color.Black
        Me.Label114.Location = New System.Drawing.Point(17, 10)
        Me.Label114.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(34, 14)
        Me.Label114.TabIndex = 57
        Me.Label114.Text = "NPI :"
        Me.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label115
        '
        Me.Label115.AutoSize = True
        Me.Label115.BackColor = System.Drawing.Color.Transparent
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.Black
        Me.Label115.Location = New System.Drawing.Point(17, 106)
        Me.Label115.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(33, 14)
        Me.Label115.TabIndex = 65
        Me.Label115.Text = "Fax :"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddress2
        '
        Me.lblAddress2.AutoSize = True
        Me.lblAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblAddress2.Location = New System.Drawing.Point(118, 58)
        Me.lblAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress2.Name = "lblAddress2"
        Me.lblAddress2.Size = New System.Drawing.Size(64, 14)
        Me.lblAddress2.TabIndex = 71
        Me.lblAddress2.Text = "Address2"
        Me.lblAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.BackColor = System.Drawing.Color.Transparent
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.ForeColor = System.Drawing.Color.Black
        Me.Label117.Location = New System.Drawing.Point(411, 82)
        Me.Label117.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(31, 14)
        Me.Label117.TabIndex = 63
        Me.Label117.Text = "Zip :"
        Me.Label117.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label118
        '
        Me.Label118.AutoSize = True
        Me.Label118.BackColor = System.Drawing.Color.Transparent
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label118.ForeColor = System.Drawing.Color.Black
        Me.Label118.Location = New System.Drawing.Point(17, 58)
        Me.Label118.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(69, 14)
        Me.Label118.TabIndex = 66
        Me.Label118.Text = "Address 2 :"
        Me.Label118.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddress1
        '
        Me.lblAddress1.AutoSize = True
        Me.lblAddress1.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress1.ForeColor = System.Drawing.Color.Black
        Me.lblAddress1.Location = New System.Drawing.Point(118, 34)
        Me.lblAddress1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAddress1.Name = "lblAddress1"
        Me.lblAddress1.Size = New System.Drawing.Size(64, 14)
        Me.lblAddress1.TabIndex = 70
        Me.lblAddress1.Text = "Address1"
        Me.lblAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label120
        '
        Me.Label120.AutoSize = True
        Me.Label120.BackColor = System.Drawing.Color.Transparent
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label120.ForeColor = System.Drawing.Color.Black
        Me.Label120.Location = New System.Drawing.Point(232, 82)
        Me.Label120.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(45, 14)
        Me.Label120.TabIndex = 62
        Me.Label120.Text = "State :"
        Me.Label120.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label121
        '
        Me.Label121.AutoSize = True
        Me.Label121.BackColor = System.Drawing.Color.Transparent
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.ForeColor = System.Drawing.Color.Black
        Me.Label121.Location = New System.Drawing.Point(17, 34)
        Me.Label121.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(69, 14)
        Me.Label121.TabIndex = 59
        Me.Label121.Text = "Address 1 :"
        Me.Label121.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label122
        '
        Me.Label122.AutoSize = True
        Me.Label122.BackColor = System.Drawing.Color.Transparent
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label122.ForeColor = System.Drawing.Color.Black
        Me.Label122.Location = New System.Drawing.Point(17, 82)
        Me.Label122.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(35, 14)
        Me.Label122.TabIndex = 60
        Me.Label122.Text = "City :"
        Me.Label122.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNPI
        '
        Me.lblNPI.AutoSize = True
        Me.lblNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPI.ForeColor = System.Drawing.Color.Black
        Me.lblNPI.Location = New System.Drawing.Point(118, 10)
        Me.lblNPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNPI.Name = "lblNPI"
        Me.lblNPI.Size = New System.Drawing.Size(28, 14)
        Me.lblNPI.TabIndex = 68
        Me.lblNPI.Text = "NPI"
        Me.lblNPI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(543, 30)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(3, 135)
        Me.Label22.TabIndex = 62
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlProviderInfo
        '
        Me.pnlProviderInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.pnlProviderInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProviderInfo.Controls.Add(Me.Label24)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDZip)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDSSN)
        Me.pnlProviderInfo.Controls.Add(Me.lblSSN)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDFax)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDPhone)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDState)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDDEA)
        Me.pnlProviderInfo.Controls.Add(Me.Label19)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDCity)
        Me.pnlProviderInfo.Controls.Add(Me.Label26)
        Me.pnlProviderInfo.Controls.Add(Me.Label28)
        Me.pnlProviderInfo.Controls.Add(Me.Label41)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDAddress2)
        Me.pnlProviderInfo.Controls.Add(Me.Label57)
        Me.pnlProviderInfo.Controls.Add(Me.Label58)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDAddress1)
        Me.pnlProviderInfo.Controls.Add(Me.Label60)
        Me.pnlProviderInfo.Controls.Add(Me.Label61)
        Me.pnlProviderInfo.Controls.Add(Me.Label62)
        Me.pnlProviderInfo.Controls.Add(Me.Label63)
        Me.pnlProviderInfo.Controls.Add(Me.Label123)
        Me.pnlProviderInfo.Controls.Add(Me.lblMDNPI)
        Me.pnlProviderInfo.Controls.Add(Me.Label45)
        Me.pnlProviderInfo.Controls.Add(Me.Label64)
        Me.pnlProviderInfo.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProviderInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlProviderInfo.ForeColor = System.Drawing.Color.Black
        Me.pnlProviderInfo.Location = New System.Drawing.Point(1, 30)
        Me.pnlProviderInfo.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlProviderInfo.Name = "pnlProviderInfo"
        Me.pnlProviderInfo.Size = New System.Drawing.Size(542, 135)
        Me.pnlProviderInfo.TabIndex = 15
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(541, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 135)
        Me.Label24.TabIndex = 101
        Me.Label24.Text = "label4"
        '
        'lblMDZip
        '
        Me.lblMDZip.AutoSize = True
        Me.lblMDZip.BackColor = System.Drawing.Color.Transparent
        Me.lblMDZip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDZip.ForeColor = System.Drawing.Color.Black
        Me.lblMDZip.Location = New System.Drawing.Point(435, 82)
        Me.lblMDZip.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDZip.Name = "lblMDZip"
        Me.lblMDZip.Size = New System.Drawing.Size(25, 14)
        Me.lblMDZip.TabIndex = 100
        Me.lblMDZip.Text = "Zip"
        Me.lblMDZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDSSN
        '
        Me.lblMDSSN.AutoSize = True
        Me.lblMDSSN.BackColor = System.Drawing.Color.Transparent
        Me.lblMDSSN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDSSN.ForeColor = System.Drawing.Color.Black
        Me.lblMDSSN.Location = New System.Drawing.Point(550, 164)
        Me.lblMDSSN.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDSSN.Name = "lblMDSSN"
        Me.lblMDSSN.Size = New System.Drawing.Size(31, 14)
        Me.lblMDSSN.TabIndex = 99
        Me.lblMDSSN.Text = "SSN"
        Me.lblMDSSN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSSN
        '
        Me.lblSSN.AutoSize = True
        Me.lblSSN.BackColor = System.Drawing.Color.Transparent
        Me.lblSSN.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSSN.ForeColor = System.Drawing.Color.Black
        Me.lblSSN.Location = New System.Drawing.Point(550, 186)
        Me.lblSSN.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSSN.Name = "lblSSN"
        Me.lblSSN.Size = New System.Drawing.Size(31, 14)
        Me.lblSSN.TabIndex = 76
        Me.lblSSN.Text = "SSN"
        Me.lblSSN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDFax
        '
        Me.lblMDFax.AutoSize = True
        Me.lblMDFax.BackColor = System.Drawing.Color.Transparent
        Me.lblMDFax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDFax.ForeColor = System.Drawing.Color.Black
        Me.lblMDFax.Location = New System.Drawing.Point(114, 106)
        Me.lblMDFax.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDFax.Name = "lblMDFax"
        Me.lblMDFax.Size = New System.Drawing.Size(26, 14)
        Me.lblMDFax.TabIndex = 98
        Me.lblMDFax.Text = "fax"
        Me.lblMDFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDPhone
        '
        Me.lblMDPhone.AutoSize = True
        Me.lblMDPhone.BackColor = System.Drawing.Color.Transparent
        Me.lblMDPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDPhone.ForeColor = System.Drawing.Color.Black
        Me.lblMDPhone.Location = New System.Drawing.Point(268, 106)
        Me.lblMDPhone.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDPhone.Name = "lblMDPhone"
        Me.lblMDPhone.Size = New System.Drawing.Size(46, 14)
        Me.lblMDPhone.TabIndex = 97
        Me.lblMDPhone.Text = "Phone"
        Me.lblMDPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDState
        '
        Me.lblMDState.AutoSize = True
        Me.lblMDState.BackColor = System.Drawing.Color.Transparent
        Me.lblMDState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDState.ForeColor = System.Drawing.Color.Black
        Me.lblMDState.Location = New System.Drawing.Point(268, 82)
        Me.lblMDState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDState.Name = "lblMDState"
        Me.lblMDState.Size = New System.Drawing.Size(41, 14)
        Me.lblMDState.TabIndex = 96
        Me.lblMDState.Text = "State"
        Me.lblMDState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDDEA
        '
        Me.lblMDDEA.AutoSize = True
        Me.lblMDDEA.BackColor = System.Drawing.Color.Transparent
        Me.lblMDDEA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDDEA.ForeColor = System.Drawing.Color.Black
        Me.lblMDDEA.Location = New System.Drawing.Point(268, 10)
        Me.lblMDDEA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDDEA.Name = "lblMDDEA"
        Me.lblMDDEA.Size = New System.Drawing.Size(32, 14)
        Me.lblMDDEA.TabIndex = 92
        Me.lblMDDEA.Text = "DEA"
        Me.lblMDDEA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(229, 10)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(38, 14)
        Me.Label19.TabIndex = 82
        Me.Label19.Text = "DEA :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDCity
        '
        Me.lblMDCity.AutoSize = True
        Me.lblMDCity.BackColor = System.Drawing.Color.Transparent
        Me.lblMDCity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDCity.ForeColor = System.Drawing.Color.Black
        Me.lblMDCity.Location = New System.Drawing.Point(114, 82)
        Me.lblMDCity.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDCity.Name = "lblMDCity"
        Me.lblMDCity.Size = New System.Drawing.Size(31, 14)
        Me.lblMDCity.TabIndex = 95
        Me.lblMDCity.Text = "City"
        Me.lblMDCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(217, 106)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(50, 14)
        Me.Label26.TabIndex = 88
        Me.Label26.Text = "Phone :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(14, 10)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(34, 14)
        Me.Label28.TabIndex = 81
        Me.Label28.Text = "NPI :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.Black
        Me.Label41.Location = New System.Drawing.Point(14, 106)
        Me.Label41.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(33, 14)
        Me.Label41.TabIndex = 89
        Me.Label41.Text = "Fax :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDAddress2
        '
        Me.lblMDAddress2.AutoSize = True
        Me.lblMDAddress2.BackColor = System.Drawing.Color.Transparent
        Me.lblMDAddress2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDAddress2.ForeColor = System.Drawing.Color.Black
        Me.lblMDAddress2.Location = New System.Drawing.Point(114, 58)
        Me.lblMDAddress2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDAddress2.Name = "lblMDAddress2"
        Me.lblMDAddress2.Size = New System.Drawing.Size(64, 14)
        Me.lblMDAddress2.TabIndex = 94
        Me.lblMDAddress2.Text = "Address2"
        Me.lblMDAddress2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.Black
        Me.Label57.Location = New System.Drawing.Point(401, 82)
        Me.Label57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(31, 14)
        Me.Label57.TabIndex = 87
        Me.Label57.Text = "Zip :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.Color.Black
        Me.Label58.Location = New System.Drawing.Point(14, 58)
        Me.Label58.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(69, 14)
        Me.Label58.TabIndex = 90
        Me.Label58.Text = "Address 2 :"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDAddress1
        '
        Me.lblMDAddress1.AutoSize = True
        Me.lblMDAddress1.BackColor = System.Drawing.Color.Transparent
        Me.lblMDAddress1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDAddress1.ForeColor = System.Drawing.Color.Black
        Me.lblMDAddress1.Location = New System.Drawing.Point(114, 34)
        Me.lblMDAddress1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDAddress1.Name = "lblMDAddress1"
        Me.lblMDAddress1.Size = New System.Drawing.Size(64, 14)
        Me.lblMDAddress1.TabIndex = 93
        Me.lblMDAddress1.Text = "Address1"
        Me.lblMDAddress1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.BackColor = System.Drawing.Color.Transparent
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.Color.Black
        Me.Label60.Location = New System.Drawing.Point(222, 82)
        Me.Label60.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(45, 14)
        Me.Label60.TabIndex = 86
        Me.Label60.Text = "State :"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.Black
        Me.Label61.Location = New System.Drawing.Point(14, 34)
        Me.Label61.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(69, 14)
        Me.Label61.TabIndex = 83
        Me.Label61.Text = "Address 1 :"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.BackColor = System.Drawing.Color.Transparent
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.Black
        Me.Label62.Location = New System.Drawing.Point(14, 82)
        Me.Label62.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(35, 14)
        Me.Label62.TabIndex = 84
        Me.Label62.Text = "City :"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.Color.LightCoral
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.Color.Black
        Me.Label63.Location = New System.Drawing.Point(505, 164)
        Me.Label63.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(37, 14)
        Me.Label63.TabIndex = 85
        Me.Label63.Text = "SSN :"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label123
        '
        Me.Label123.AutoSize = True
        Me.Label123.BackColor = System.Drawing.Color.LightCoral
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.ForeColor = System.Drawing.Color.Black
        Me.Label123.Location = New System.Drawing.Point(505, 186)
        Me.Label123.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(37, 14)
        Me.Label123.TabIndex = 61
        Me.Label123.Text = "SSN :"
        Me.Label123.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDNPI
        '
        Me.lblMDNPI.AutoSize = True
        Me.lblMDNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblMDNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDNPI.ForeColor = System.Drawing.Color.Black
        Me.lblMDNPI.Location = New System.Drawing.Point(114, 10)
        Me.lblMDNPI.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDNPI.Name = "lblMDNPI"
        Me.lblMDNPI.Size = New System.Drawing.Size(28, 14)
        Me.lblMDNPI.TabIndex = 91
        Me.lblMDNPI.Text = "NPI"
        Me.lblMDNPI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Location = New System.Drawing.Point(143, 343)
        Me.Label45.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(57, 14)
        Me.Label45.TabIndex = 55
        Me.Label45.Text = "Potency"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Location = New System.Drawing.Point(15, 343)
        Me.Label64.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(122, 14)
        Me.Label64.TabIndex = 45
        Me.Label64.Text = "Drug Potency Code :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(142, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(1, 165)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1087, 1)
        Me.Label94.TabIndex = 60
        Me.Label94.Text = "label1"
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label96)
        Me.Panel7.Controls.Add(Me.Label97)
        Me.Panel7.Controls.Add(Me.Label104)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(1, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1087, 25)
        Me.Panel7.TabIndex = 45
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(0, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1087, 1)
        Me.Label96.TabIndex = 14
        Me.Label96.Text = "label1"
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label97.Location = New System.Drawing.Point(0, 24)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1087, 1)
        Me.Label97.TabIndex = 13
        Me.Label97.Text = "label2"
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.Transparent
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label104.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.ForeColor = System.Drawing.Color.White
        Me.Label104.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label104.Location = New System.Drawing.Point(0, 0)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1087, 25)
        Me.Label104.TabIndex = 9
        Me.Label104.Text = "   Provider Information"
        Me.Label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.Location = New System.Drawing.Point(0, 5)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(1, 161)
        Me.Label105.TabIndex = 58
        Me.Label105.Text = "label4"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label95.Location = New System.Drawing.Point(1088, 5)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 161)
        Me.Label95.TabIndex = 59
        Me.Label95.Text = "label3"
        '
        'pnlDrugSummary
        '
        Me.pnlDrugSummary.Controls.Add(Me.Panel8)
        Me.pnlDrugSummary.Controls.Add(Me.Label18)
        Me.pnlDrugSummary.Controls.Add(Me.pnlflexgrid)
        Me.pnlDrugSummary.Controls.Add(Me.lblGridTop)
        Me.pnlDrugSummary.Controls.Add(Me.pnlMids)
        Me.pnlDrugSummary.Controls.Add(Me.Label98)
        Me.pnlDrugSummary.Controls.Add(Me.lblGridRight)
        Me.pnlDrugSummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDrugSummary.Location = New System.Drawing.Point(10, 38)
        Me.pnlDrugSummary.Name = "pnlDrugSummary"
        Me.pnlDrugSummary.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.pnlDrugSummary.Size = New System.Drawing.Size(1089, 196)
        Me.pnlDrugSummary.TabIndex = 46
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label27)
        Me.Panel8.Controls.Add(Me.lblRefills)
        Me.Panel8.Controls.Add(Me.lblPotencyCode)
        Me.Panel8.Controls.Add(Me.lblDuration)
        Me.Panel8.Controls.Add(Me.lblSubstitution)
        Me.Panel8.Controls.Add(Me.lblRefQlf)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.lblNotes)
        Me.Panel8.Controls.Add(Me.Label1)
        Me.Panel8.Controls.Add(Me.lblNDCCode)
        Me.Panel8.Controls.Add(Me.Label2)
        Me.Panel8.Controls.Add(Me.lblDirection)
        Me.Panel8.Controls.Add(Me.Label4)
        Me.Panel8.Controls.Add(Me.lblQty)
        Me.Panel8.Controls.Add(Me.Label5)
        Me.Panel8.Controls.Add(Me.lblDrug)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Controls.Add(Me.Label10)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(546, 30)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(542, 165)
        Me.Panel8.TabIndex = 15
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 165)
        Me.Label27.TabIndex = 59
        Me.Label27.Text = "label4"
        '
        'lblRefills
        '
        Me.lblRefills.AutoSize = True
        Me.lblRefills.BackColor = System.Drawing.Color.Transparent
        Me.lblRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefills.ForeColor = System.Drawing.Color.Black
        Me.lblRefills.Location = New System.Drawing.Point(277, 107)
        Me.lblRefills.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefills.Name = "lblRefills"
        Me.lblRefills.Size = New System.Drawing.Size(43, 14)
        Me.lblRefills.TabIndex = 34
        Me.lblRefills.Text = "Refills"
        Me.lblRefills.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPotencyCode
        '
        Me.lblPotencyCode.AutoSize = True
        Me.lblPotencyCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPotencyCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPotencyCode.ForeColor = System.Drawing.Color.Black
        Me.lblPotencyCode.Location = New System.Drawing.Point(442, 48)
        Me.lblPotencyCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPotencyCode.Name = "lblPotencyCode"
        Me.lblPotencyCode.Size = New System.Drawing.Size(57, 14)
        Me.lblPotencyCode.TabIndex = 33
        Me.lblPotencyCode.Text = "Potency"
        Me.lblPotencyCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = True
        Me.lblDuration.BackColor = System.Drawing.Color.Transparent
        Me.lblDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDuration.ForeColor = System.Drawing.Color.Black
        Me.lblDuration.Location = New System.Drawing.Point(442, 27)
        Me.lblDuration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(61, 14)
        Me.lblDuration.TabIndex = 32
        Me.lblDuration.Text = "Duration"
        Me.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubstitution
        '
        Me.lblSubstitution.AutoSize = True
        Me.lblSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubstitution.ForeColor = System.Drawing.Color.Black
        Me.lblSubstitution.Location = New System.Drawing.Point(442, 107)
        Me.lblSubstitution.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubstitution.Name = "lblSubstitution"
        Me.lblSubstitution.Size = New System.Drawing.Size(85, 14)
        Me.lblSubstitution.TabIndex = 30
        Me.lblSubstitution.Text = "Substitution"
        Me.lblSubstitution.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRefQlf
        '
        Me.lblRefQlf.AutoSize = True
        Me.lblRefQlf.BackColor = System.Drawing.Color.Transparent
        Me.lblRefQlf.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefQlf.ForeColor = System.Drawing.Color.Black
        Me.lblRefQlf.Location = New System.Drawing.Point(118, 107)
        Me.lblRefQlf.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRefQlf.Name = "lblRefQlf"
        Me.lblRefQlf.Size = New System.Drawing.Size(45, 14)
        Me.lblRefQlf.TabIndex = 29
        Me.lblRefQlf.Text = "RefQlf"
        Me.lblRefQlf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(17, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Drug :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNotes
        '
        Me.lblNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.ForeColor = System.Drawing.Color.Black
        Me.lblNotes.Location = New System.Drawing.Point(118, 128)
        Me.lblNotes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(419, 31)
        Me.lblNotes.TabIndex = 28
        Me.lblNotes.Text = "Notes"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(17, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Drug Qty :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNDCCode
        '
        Me.lblNDCCode.AutoSize = True
        Me.lblNDCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCCode.ForeColor = System.Drawing.Color.Black
        Me.lblNDCCode.Location = New System.Drawing.Point(118, 48)
        Me.lblNDCCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNDCCode.Name = "lblNDCCode"
        Me.lblNDCCode.Size = New System.Drawing.Size(71, 14)
        Me.lblNDCCode.TabIndex = 27
        Me.lblNDCCode.Text = "NDCCode :"
        Me.lblNDCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(17, 69)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 14)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Drug Direction :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDirection
        '
        Me.lblDirection.BackColor = System.Drawing.Color.Transparent
        Me.lblDirection.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDirection.ForeColor = System.Drawing.Color.Black
        Me.lblDirection.Location = New System.Drawing.Point(118, 69)
        Me.lblDirection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDirection.Name = "lblDirection"
        Me.lblDirection.Size = New System.Drawing.Size(419, 31)
        Me.lblDirection.TabIndex = 26
        Me.lblDirection.Text = "Drug Direction :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(17, 128)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 14)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Notes :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.BackColor = System.Drawing.Color.Transparent
        Me.lblQty.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.Color.Black
        Me.lblQty.Location = New System.Drawing.Point(118, 27)
        Me.lblQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(71, 14)
        Me.lblQty.TabIndex = 25
        Me.lblQty.Text = "Drug Qty :"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(381, 27)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Duration :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDrug
        '
        Me.lblDrug.AutoSize = True
        Me.lblDrug.BackColor = System.Drawing.Color.Transparent
        Me.lblDrug.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrug.ForeColor = System.Drawing.Color.Black
        Me.lblDrug.Location = New System.Drawing.Point(118, 6)
        Me.lblDrug.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDrug.Name = "lblDrug"
        Me.lblDrug.Size = New System.Drawing.Size(45, 14)
        Me.lblDrug.TabIndex = 24
        Me.lblDrug.Text = "Drug :"
        Me.lblDrug.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(17, 107)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 14)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Ref. Qlf :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(320, 48)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(122, 14)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Drug Potency Code :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(233, 107)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Refills :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(17, 48)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 14)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Drug NDC Code :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(361, 107)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 14)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Substitution :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(543, 30)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(3, 165)
        Me.Label18.TabIndex = 61
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlflexgrid
        '
        Me.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.pnlflexgrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlflexgrid.Controls.Add(Me.Label43)
        Me.pnlflexgrid.Controls.Add(Me.lblMDRefills)
        Me.pnlflexgrid.Controls.Add(Me.lblMDPotencyCode)
        Me.pnlflexgrid.Controls.Add(Me.lblMDDuration)
        Me.pnlflexgrid.Controls.Add(Me.lblMDSubstitution)
        Me.pnlflexgrid.Controls.Add(Me.lblMDRefQlf)
        Me.pnlflexgrid.Controls.Add(Me.lblMDQty)
        Me.pnlflexgrid.Controls.Add(Me.lblMDNotes)
        Me.pnlflexgrid.Controls.Add(Me.Label40)
        Me.pnlflexgrid.Controls.Add(Me.lblMDNDCCode)
        Me.pnlflexgrid.Controls.Add(Me.Label39)
        Me.pnlflexgrid.Controls.Add(Me.lblMDDirection)
        Me.pnlflexgrid.Controls.Add(Me.Label38)
        Me.pnlflexgrid.Controls.Add(Me.Label37)
        Me.pnlflexgrid.Controls.Add(Me.lblMDDrug)
        Me.pnlflexgrid.Controls.Add(Me.Label36)
        Me.pnlflexgrid.Controls.Add(Me.Label30)
        Me.pnlflexgrid.Controls.Add(Me.Label35)
        Me.pnlflexgrid.Controls.Add(Me.Label31)
        Me.pnlflexgrid.Controls.Add(Me.Label34)
        Me.pnlflexgrid.Controls.Add(Me.Label33)
        Me.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlflexgrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlflexgrid.Location = New System.Drawing.Point(1, 30)
        Me.pnlflexgrid.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlflexgrid.Name = "pnlflexgrid"
        Me.pnlflexgrid.Size = New System.Drawing.Size(542, 165)
        Me.pnlflexgrid.TabIndex = 15
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(541, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 165)
        Me.Label43.TabIndex = 59
        Me.Label43.Text = "label4"
        '
        'lblMDRefills
        '
        Me.lblMDRefills.AutoSize = True
        Me.lblMDRefills.BackColor = System.Drawing.Color.Transparent
        Me.lblMDRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDRefills.ForeColor = System.Drawing.Color.Black
        Me.lblMDRefills.Location = New System.Drawing.Point(268, 107)
        Me.lblMDRefills.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDRefills.Name = "lblMDRefills"
        Me.lblMDRefills.Size = New System.Drawing.Size(43, 14)
        Me.lblMDRefills.TabIndex = 56
        Me.lblMDRefills.Text = "Refills"
        Me.lblMDRefills.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDPotencyCode
        '
        Me.lblMDPotencyCode.AutoSize = True
        Me.lblMDPotencyCode.BackColor = System.Drawing.Color.Transparent
        Me.lblMDPotencyCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDPotencyCode.ForeColor = System.Drawing.Color.Black
        Me.lblMDPotencyCode.Location = New System.Drawing.Point(435, 48)
        Me.lblMDPotencyCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDPotencyCode.Name = "lblMDPotencyCode"
        Me.lblMDPotencyCode.Size = New System.Drawing.Size(57, 14)
        Me.lblMDPotencyCode.TabIndex = 55
        Me.lblMDPotencyCode.Text = "Potency"
        Me.lblMDPotencyCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDDuration
        '
        Me.lblMDDuration.AutoSize = True
        Me.lblMDDuration.BackColor = System.Drawing.Color.Transparent
        Me.lblMDDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDDuration.ForeColor = System.Drawing.Color.Black
        Me.lblMDDuration.Location = New System.Drawing.Point(435, 27)
        Me.lblMDDuration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDDuration.Name = "lblMDDuration"
        Me.lblMDDuration.Size = New System.Drawing.Size(61, 14)
        Me.lblMDDuration.TabIndex = 54
        Me.lblMDDuration.Text = "Duration"
        Me.lblMDDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDSubstitution
        '
        Me.lblMDSubstitution.AutoSize = True
        Me.lblMDSubstitution.BackColor = System.Drawing.Color.Transparent
        Me.lblMDSubstitution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDSubstitution.ForeColor = System.Drawing.Color.Black
        Me.lblMDSubstitution.Location = New System.Drawing.Point(435, 107)
        Me.lblMDSubstitution.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDSubstitution.Name = "lblMDSubstitution"
        Me.lblMDSubstitution.Size = New System.Drawing.Size(85, 14)
        Me.lblMDSubstitution.TabIndex = 52
        Me.lblMDSubstitution.Text = "Substitution"
        Me.lblMDSubstitution.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDRefQlf
        '
        Me.lblMDRefQlf.AutoSize = True
        Me.lblMDRefQlf.BackColor = System.Drawing.Color.Transparent
        Me.lblMDRefQlf.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDRefQlf.ForeColor = System.Drawing.Color.Black
        Me.lblMDRefQlf.Location = New System.Drawing.Point(114, 107)
        Me.lblMDRefQlf.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDRefQlf.Name = "lblMDRefQlf"
        Me.lblMDRefQlf.Size = New System.Drawing.Size(45, 14)
        Me.lblMDRefQlf.TabIndex = 51
        Me.lblMDRefQlf.Text = "RefQlf"
        Me.lblMDRefQlf.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDQty
        '
        Me.lblMDQty.AutoSize = True
        Me.lblMDQty.BackColor = System.Drawing.Color.Transparent
        Me.lblMDQty.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDQty.ForeColor = System.Drawing.Color.Black
        Me.lblMDQty.Location = New System.Drawing.Point(114, 27)
        Me.lblMDQty.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDQty.Name = "lblMDQty"
        Me.lblMDQty.Size = New System.Drawing.Size(71, 14)
        Me.lblMDQty.TabIndex = 47
        Me.lblMDQty.Text = "Drug Qty :"
        Me.lblMDQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDNotes
        '
        Me.lblMDNotes.BackColor = System.Drawing.Color.Transparent
        Me.lblMDNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDNotes.ForeColor = System.Drawing.Color.Black
        Me.lblMDNotes.Location = New System.Drawing.Point(114, 128)
        Me.lblMDNotes.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDNotes.Name = "lblMDNotes"
        Me.lblMDNotes.Size = New System.Drawing.Size(419, 31)
        Me.lblMDNotes.TabIndex = 50
        Me.lblMDNotes.Text = "Notes"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(14, 6)
        Me.Label40.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(41, 14)
        Me.Label40.TabIndex = 35
        Me.Label40.Text = "Drug :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDNDCCode
        '
        Me.lblMDNDCCode.AutoSize = True
        Me.lblMDNDCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblMDNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDNDCCode.ForeColor = System.Drawing.Color.Black
        Me.lblMDNDCCode.Location = New System.Drawing.Point(114, 48)
        Me.lblMDNDCCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDNDCCode.Name = "lblMDNDCCode"
        Me.lblMDNDCCode.Size = New System.Drawing.Size(71, 14)
        Me.lblMDNDCCode.TabIndex = 49
        Me.lblMDNDCCode.Text = "NDCCode :"
        Me.lblMDNDCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(14, 27)
        Me.Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(65, 14)
        Me.Label39.TabIndex = 36
        Me.Label39.Text = "Drug Qty :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDDirection
        '
        Me.lblMDDirection.BackColor = System.Drawing.Color.Transparent
        Me.lblMDDirection.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDDirection.ForeColor = System.Drawing.Color.Black
        Me.lblMDDirection.Location = New System.Drawing.Point(114, 69)
        Me.lblMDDirection.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDDirection.Name = "lblMDDirection"
        Me.lblMDDirection.Size = New System.Drawing.Size(424, 31)
        Me.lblMDDirection.TabIndex = 48
        Me.lblMDDirection.Text = "Drug Direction :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(14, 69)
        Me.Label38.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(93, 14)
        Me.Label38.TabIndex = 37
        Me.Label38.Text = "Drug Direction :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(14, 128)
        Me.Label37.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(47, 14)
        Me.Label37.TabIndex = 38
        Me.Label37.Text = "Notes :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMDDrug
        '
        Me.lblMDDrug.AutoSize = True
        Me.lblMDDrug.BackColor = System.Drawing.Color.Transparent
        Me.lblMDDrug.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMDDrug.ForeColor = System.Drawing.Color.Black
        Me.lblMDDrug.Location = New System.Drawing.Point(114, 6)
        Me.lblMDDrug.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMDDrug.Name = "lblMDDrug"
        Me.lblMDDrug.Size = New System.Drawing.Size(45, 14)
        Me.lblMDDrug.TabIndex = 46
        Me.lblMDDrug.Text = "Drug :"
        Me.lblMDDrug.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(371, 27)
        Me.Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(61, 14)
        Me.Label36.TabIndex = 39
        Me.Label36.Text = "Duration :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(310, 48)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(122, 14)
        Me.Label30.TabIndex = 45
        Me.Label30.Text = "Drug Potency Code :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(14, 107)
        Me.Label35.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(56, 14)
        Me.Label35.TabIndex = 40
        Me.Label35.Text = "Ref. Qlf :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(14, 48)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(100, 14)
        Me.Label31.TabIndex = 44
        Me.Label31.Text = "Drug NDC Code :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(223, 107)
        Me.Label34.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(44, 14)
        Me.Label34.TabIndex = 41
        Me.Label34.Text = "Refills :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(351, 107)
        Me.Label33.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(81, 14)
        Me.Label33.TabIndex = 42
        Me.Label33.Text = "Substitution :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGridTop
        '
        Me.lblGridTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblGridTop.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridTop.Location = New System.Drawing.Point(1, 195)
        Me.lblGridTop.Name = "lblGridTop"
        Me.lblGridTop.Size = New System.Drawing.Size(1087, 1)
        Me.lblGridTop.TabIndex = 60
        Me.lblGridTop.Text = "label1"
        '
        'pnlMids
        '
        Me.pnlMids.BackgroundImage = CType(resources.GetObject("pnlMids.BackgroundImage"), System.Drawing.Image)
        Me.pnlMids.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMids.Controls.Add(Me.Label29)
        Me.pnlMids.Controls.Add(Me.Label12)
        Me.pnlMids.Controls.Add(Me.lblLightHeader)
        Me.pnlMids.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMids.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMids.Location = New System.Drawing.Point(1, 5)
        Me.pnlMids.Name = "pnlMids"
        Me.pnlMids.Size = New System.Drawing.Size(1087, 25)
        Me.pnlMids.TabIndex = 45
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1087, 1)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(0, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1087, 1)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "label2"
        '
        'lblLightHeader
        '
        Me.lblLightHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblLightHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLightHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblLightHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLightHeader.ForeColor = System.Drawing.Color.White
        Me.lblLightHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLightHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblLightHeader.Name = "lblLightHeader"
        Me.lblLightHeader.Size = New System.Drawing.Size(1087, 25)
        Me.lblLightHeader.TabIndex = 9
        Me.lblLightHeader.Text = "   Drug Summary"
        Me.lblLightHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 5)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(1, 191)
        Me.Label98.TabIndex = 58
        Me.Label98.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(1088, 5)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 191)
        Me.lblGridRight.TabIndex = 59
        Me.lblGridRight.Text = "label3"
        '
        'pnlheader
        '
        Me.pnlheader.Controls.Add(Me.Panel18)
        Me.pnlheader.Controls.Add(Me.Panel6)
        Me.pnlheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlheader.Location = New System.Drawing.Point(10, 10)
        Me.pnlheader.Name = "pnlheader"
        Me.pnlheader.Size = New System.Drawing.Size(1089, 28)
        Me.pnlheader.TabIndex = 49
        '
        'Panel18
        '
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Panel11)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.Location = New System.Drawing.Point(543, 0)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel18.Size = New System.Drawing.Size(546, 28)
        Me.Panel18.TabIndex = 45
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = CType(resources.GetObject("Panel11.BackgroundImage"), System.Drawing.Image)
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label17)
        Me.Panel11.Controls.Add(Me.Label14)
        Me.Panel11.Controls.Add(Me.Label138)
        Me.Panel11.Controls.Add(Me.lblResponse)
        Me.Panel11.Controls.Add(Me.Label139)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(3, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(543, 28)
        Me.Panel11.TabIndex = 61
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(541, 1)
        Me.Label17.TabIndex = 61
        Me.Label17.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(1, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(541, 1)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "label1"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label138.Location = New System.Drawing.Point(542, 0)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 28)
        Me.Label138.TabIndex = 59
        Me.Label138.Text = "label4"
        '
        'lblResponse
        '
        Me.lblResponse.BackColor = System.Drawing.Color.Transparent
        Me.lblResponse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblResponse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblResponse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResponse.ForeColor = System.Drawing.Color.Black
        Me.lblResponse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblResponse.Location = New System.Drawing.Point(1, 0)
        Me.lblResponse.Name = "lblResponse"
        Me.lblResponse.Size = New System.Drawing.Size(542, 28)
        Me.lblResponse.TabIndex = 9
        Me.lblResponse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.Location = New System.Drawing.Point(0, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1, 28)
        Me.Label139.TabIndex = 58
        Me.Label139.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Panel10)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(543, 28)
        Me.Panel6.TabIndex = 45
        '
        'Panel10
        '
        Me.Panel10.BackgroundImage = CType(resources.GetObject("Panel10.BackgroundImage"), System.Drawing.Image)
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label16)
        Me.Panel10.Controls.Add(Me.Label13)
        Me.Panel10.Controls.Add(Me.Label50)
        Me.Panel10.Controls.Add(Me.Label20)
        Me.Panel10.Controls.Add(Me.lblRequest)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(543, 28)
        Me.Panel10.TabIndex = 60
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(541, 1)
        Me.Label16.TabIndex = 61
        Me.Label16.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(541, 1)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "label1"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(542, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 28)
        Me.Label50.TabIndex = 59
        Me.Label50.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(203, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 28)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "label4"
        '
        'lblReqest
        '
        Me.lblRequest.BackColor = System.Drawing.Color.Transparent
        Me.lblRequest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRequest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequest.ForeColor = System.Drawing.Color.Black
        Me.lblRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRequest.Location = New System.Drawing.Point(0, 0)
        Me.lblRequest.Name = "lblReqest"
        Me.lblRequest.Size = New System.Drawing.Size(543, 28)
        Me.lblRequest.TabIndex = 9
        Me.lblRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tlsp_Approve
        '
        Me.tlsp_Approve.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Approve.BackgroundImage = CType(resources.GetObject("tlsp_Approve.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_Approve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Approve.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Approve.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Approve.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnApprovewithChanges, Me.tlbbtnDTNF, Me.ts_btnClose})
        Me.tlsp_Approve.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Approve.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Approve.Name = "tlsp_Approve"
        Me.tlsp_Approve.Size = New System.Drawing.Size(1109, 53)
        Me.tlsp_Approve.TabIndex = 0
        Me.tlsp_Approve.Text = "toolStrip1"
        '
        'tlbbtnApprovewithChanges
        '
        Me.tlbbtnApprovewithChanges.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnApprovewithChanges.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnApprovewithChanges.Image = CType(resources.GetObject("tlbbtnApprovewithChanges.Image"), System.Drawing.Image)
        Me.tlbbtnApprovewithChanges.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnApprovewithChanges.Name = "tlbbtnApprovewithChanges"
        Me.tlbbtnApprovewithChanges.Size = New System.Drawing.Size(151, 50)
        Me.tlbbtnApprovewithChanges.Text = "Approve with Changes"
        Me.tlbbtnApprovewithChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnApprovewithChanges.Visible = False
        '
        'tlbbtnDTNF
        '
        Me.tlbbtnDTNF.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnDTNF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnDTNF.Image = CType(resources.GetObject("tlbbtnDTNF.Image"), System.Drawing.Image)
        Me.tlbbtnDTNF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnDTNF.Name = "tlbbtnDTNF"
        Me.tlbbtnDTNF.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtnDTNF.Text = "&DNTF"
        Me.tlbbtnDTNF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnDTNF.ToolTipText = "Deny with New Rx to Follow"
        Me.tlbbtnDTNF.Visible = False
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_Approve)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(1109, 53)
        Me.pnl_tlsp.TabIndex = 16
        '
        'frmConfirmApproove
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1109, 530)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfirmApproove"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlMain.ResumeLayout(False)
        Me.pnlPharmacyInformation.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.pnlPharmacy.ResumeLayout(False)
        Me.pnlPharmacy.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.pnlProviderInformation.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.pnlProviderInfo.ResumeLayout(False)
        Me.pnlProviderInfo.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.pnlDrugSummary.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlflexgrid.ResumeLayout(False)
        Me.pnlflexgrid.PerformLayout()
        Me.pnlMids.ResumeLayout(False)
        Me.pnlheader.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.tlsp_Approve.ResumeLayout(False)
        Me.tlsp_Approve.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents tlsp_Approve As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Public WithEvents tlbbtnApprovewithChanges As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtnDTNF As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblRefills As System.Windows.Forms.Label
    Friend WithEvents lblPotencyCode As System.Windows.Forms.Label
    Friend WithEvents lblDuration As System.Windows.Forms.Label
    Friend WithEvents lblNCPDPId As System.Windows.Forms.Label
    Friend WithEvents lblSubstitution As System.Windows.Forms.Label
    Friend WithEvents lblRefQlf As System.Windows.Forms.Label
    Friend WithEvents lblNotes As System.Windows.Forms.Label
    Friend WithEvents lblNDCCode As System.Windows.Forms.Label
    Friend WithEvents lblDirection As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents lblDrug As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlflexgrid As System.Windows.Forms.Panel
    Friend WithEvents pnlMids As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblLightHeader As System.Windows.Forms.Label
    Friend WithEvents lblMDRefills As System.Windows.Forms.Label
    Friend WithEvents lblMDPotencyCode As System.Windows.Forms.Label
    Friend WithEvents lblMDDuration As System.Windows.Forms.Label
    Friend WithEvents lblMDNCPDPId As System.Windows.Forms.Label
    Friend WithEvents lblMDSubstitution As System.Windows.Forms.Label
    Friend WithEvents lblMDRefQlf As System.Windows.Forms.Label
    Friend WithEvents lblMDQty As System.Windows.Forms.Label
    Friend WithEvents lblMDNotes As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents lblMDNDCCode As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblMDDirection As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblMDDrug As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents pnlProviderInfo As System.Windows.Forms.Panel
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents pnlPharmacy As System.Windows.Forms.Panel
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblRequest As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents label100 As System.Windows.Forms.Label
    Friend WithEvents lblPharmacyName As System.Windows.Forms.Label
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Label137 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents lblResponse As System.Windows.Forms.Label
    Friend WithEvents lblMDZip As System.Windows.Forms.Label
    Friend WithEvents lblMDSSN As System.Windows.Forms.Label
    Friend WithEvents lblMDFax As System.Windows.Forms.Label
    Friend WithEvents lblMDPhone As System.Windows.Forms.Label
    Friend WithEvents lblMDState As System.Windows.Forms.Label
    Friend WithEvents lblMDDEA As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblMDCity As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblMDAddress2 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents lblMDAddress1 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents lblMDNPI As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblMDPharmacyName As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlDrugSummary As System.Windows.Forms.Panel
    Private WithEvents lblGridTop As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents pnlPharmacyInformation As System.Windows.Forms.Panel
    Private WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents pnlProviderInformation As System.Windows.Forms.Panel
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents lblSSN As System.Windows.Forms.Label
    Friend WithEvents lblFax As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblDEA As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents lblAddress2 As System.Windows.Forms.Label
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents lblAddress1 As System.Windows.Forms.Label
    Friend WithEvents Label120 As System.Windows.Forms.Label
    Friend WithEvents Label121 As System.Windows.Forms.Label
    Friend WithEvents Label122 As System.Windows.Forms.Label
    Friend WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents lblNPI As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents pnlheader As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
End Class
