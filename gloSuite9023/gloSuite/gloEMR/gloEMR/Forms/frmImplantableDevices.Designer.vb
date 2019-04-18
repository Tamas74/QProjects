<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImplantableDevices
    Inherits gloEMR.frmBaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpControls As DateTimePicker() = {dttransaction_date, dtTransactionTime}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImplantableDevices))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tblStrip = New gloToolStrip.gloToolStrip()
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnParseUDI = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtDeviceDescription = New System.Windows.Forms.TextBox()
        Me.txtGmdnPTName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lstProcedures = New System.Windows.Forms.ListBox()
        Me.btnBrowseProc = New System.Windows.Forms.Button()
        Me.btnClearProc = New System.Windows.Forms.Button()
        Me.btnClrPlayingDevice = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblconcptid = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnConceptID = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblUDI = New System.Windows.Forms.Label()
        Me.lblIssuingAgency = New System.Windows.Forms.Label()
        Me.optActive = New System.Windows.Forms.RadioButton()
        Me.optInactive = New System.Windows.Forms.RadioButton()
        Me.txt_ManufacturingDate = New System.Windows.Forms.TextBox()
        Me.txt_ExpiryDate = New System.Windows.Forms.TextBox()
        Me.txt_HCTP = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_UDI = New System.Windows.Forms.TextBox()
        Me.txt_SerialNumber = New System.Windows.Forms.TextBox()
        Me.lblMRI = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_LotBatch = New System.Windows.Forms.TextBox()
        Me.lblManufacturer = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_VersionOrModel = New System.Windows.Forms.TextBox()
        Me.dtTransactionTime = New System.Windows.Forms.DateTimePicker()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txt_NRL = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.dttransaction_date = New System.Windows.Forms.DateTimePicker()
        Me.txt_MRIstatus = New System.Windows.Forms.TextBox()
        Me.lblAdministeredTime = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblNRL = New System.Windows.Forms.Label()
        Me.lblVersionOrModel = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblDeviceID = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txt_CompanyName = New System.Windows.Forms.TextBox()
        Me.txt_DI = New System.Windows.Forms.TextBox()
        Me.txt_IssuingAgency = New System.Windows.Forms.TextBox()
        Me.txt_BrandName = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblBrandName = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tblStrip)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 54)
        Me.Panel1.TabIndex = 18
        '
        'tblStrip
        '
        Me.tblStrip.AddSeparatorsBetweenEachButton = False
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.ButtonsToHide = CType(resources.GetObject("tblStrip.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblStrip.ConnectionString = Nothing
        Me.tblStrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.ModuleName = Nothing
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1028, 53)
        Me.tblStrip.TabIndex = 18
        Me.tblStrip.TabStop = True
        Me.tblStrip.Text = "ToolStrip1"
        Me.tblStrip.UserID = CType(0, Long)
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save.Tag = "Save and Close"
        Me.tblbtn_Save.Text = "&Save&&Cls"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save.ToolTipText = "Save and Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Close.ToolTipText = "Close"
        '
        'btnParseUDI
        '
        Me.btnParseUDI.BackColor = System.Drawing.Color.Transparent
        Me.btnParseUDI.BackgroundImage = CType(resources.GetObject("btnParseUDI.BackgroundImage"), System.Drawing.Image)
        Me.btnParseUDI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnParseUDI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnParseUDI.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnParseUDI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnParseUDI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnParseUDI.Image = CType(resources.GetObject("btnParseUDI.Image"), System.Drawing.Image)
        Me.btnParseUDI.Location = New System.Drawing.Point(471, 122)
        Me.btnParseUDI.Name = "btnParseUDI"
        Me.btnParseUDI.Size = New System.Drawing.Size(24, 23)
        Me.btnParseUDI.TabIndex = 6
        Me.btnParseUDI.Text = "          "
        Me.btnParseUDI.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnParseUDI.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtDeviceDescription)
        Me.Panel2.Controls.Add(Me.txtGmdnPTName)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.lstProcedures)
        Me.Panel2.Controls.Add(Me.btnBrowseProc)
        Me.Panel2.Controls.Add(Me.btnClearProc)
        Me.Panel2.Controls.Add(Me.btnClrPlayingDevice)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.btnConceptID)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lblUDI)
        Me.Panel2.Controls.Add(Me.lblIssuingAgency)
        Me.Panel2.Controls.Add(Me.optActive)
        Me.Panel2.Controls.Add(Me.optInactive)
        Me.Panel2.Controls.Add(Me.txt_ManufacturingDate)
        Me.Panel2.Controls.Add(Me.txt_ExpiryDate)
        Me.Panel2.Controls.Add(Me.txt_HCTP)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txt_UDI)
        Me.Panel2.Controls.Add(Me.txt_SerialNumber)
        Me.Panel2.Controls.Add(Me.lblMRI)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txt_LotBatch)
        Me.Panel2.Controls.Add(Me.lblManufacturer)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txt_VersionOrModel)
        Me.Panel2.Controls.Add(Me.btnParseUDI)
        Me.Panel2.Controls.Add(Me.dtTransactionTime)
        Me.Panel2.Controls.Add(Me.Label32)
        Me.Panel2.Controls.Add(Me.txt_NRL)
        Me.Panel2.Controls.Add(Me.Label47)
        Me.Panel2.Controls.Add(Me.cmbProvider)
        Me.Panel2.Controls.Add(Me.dttransaction_date)
        Me.Panel2.Controls.Add(Me.txt_MRIstatus)
        Me.Panel2.Controls.Add(Me.lblAdministeredTime)
        Me.Panel2.Controls.Add(Me.Label43)
        Me.Panel2.Controls.Add(Me.lblProviderName)
        Me.Panel2.Controls.Add(Me.Label41)
        Me.Panel2.Controls.Add(Me.Label39)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Controls.Add(Me.Label29)
        Me.Panel2.Controls.Add(Me.lblNRL)
        Me.Panel2.Controls.Add(Me.lblVersionOrModel)
        Me.Panel2.Controls.Add(Me.Label28)
        Me.Panel2.Controls.Add(Me.lblDeviceID)
        Me.Panel2.Controls.Add(Me.Label40)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.txt_CompanyName)
        Me.Panel2.Controls.Add(Me.txt_DI)
        Me.Panel2.Controls.Add(Me.txt_IssuingAgency)
        Me.Panel2.Controls.Add(Me.txt_BrandName)
        Me.Panel2.Controls.Add(Me.Label37)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.lblBrandName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1028, 450)
        Me.Panel2.TabIndex = 19
        '
        'txtDeviceDescription
        '
        Me.txtDeviceDescription.Location = New System.Drawing.Point(661, 176)
        Me.txtDeviceDescription.MaxLength = 80
        Me.txtDeviceDescription.Multiline = True
        Me.txtDeviceDescription.Name = "txtDeviceDescription"
        Me.txtDeviceDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDeviceDescription.Size = New System.Drawing.Size(299, 34)
        Me.txtDeviceDescription.TabIndex = 494
        '
        'txtGmdnPTName
        '
        Me.txtGmdnPTName.Location = New System.Drawing.Point(661, 221)
        Me.txtGmdnPTName.MaxLength = 255
        Me.txtGmdnPTName.Multiline = True
        Me.txtGmdnPTName.Name = "txtGmdnPTName"
        Me.txtGmdnPTName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGmdnPTName.Size = New System.Drawing.Size(299, 34)
        Me.txtGmdnPTName.TabIndex = 495
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(556, 224)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(102, 14)
        Me.Label16.TabIndex = 493
        Me.Label16.Text = "GMDN PT Name :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(543, 181)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(115, 14)
        Me.Label14.TabIndex = 490
        Me.Label14.Text = "Device Description :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(94, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 14)
        Me.Label12.TabIndex = 488
        Me.Label12.Text = "Procedures :"
        '
        'lstProcedures
        '
        Me.lstProcedures.FormattingEnabled = True
        Me.lstProcedures.ItemHeight = 14
        Me.lstProcedures.Location = New System.Drawing.Point(173, 52)
        Me.lstProcedures.Name = "lstProcedures"
        Me.lstProcedures.Size = New System.Drawing.Size(294, 60)
        Me.lstProcedures.TabIndex = 4
        '
        'btnBrowseProc
        '
        Me.btnBrowseProc.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseProc.BackgroundImage = CType(resources.GetObject("btnBrowseProc.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseProc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseProc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseProc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseProc.Image = CType(resources.GetObject("btnBrowseProc.Image"), System.Drawing.Image)
        Me.btnBrowseProc.Location = New System.Drawing.Point(472, 52)
        Me.btnBrowseProc.Name = "btnBrowseProc"
        Me.btnBrowseProc.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseProc.TabIndex = 2
        Me.btnBrowseProc.UseVisualStyleBackColor = False
        '
        'btnClearProc
        '
        Me.btnClearProc.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProc.BackgroundImage = CType(resources.GetObject("btnClearProc.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProc.Image = CType(resources.GetObject("btnClearProc.Image"), System.Drawing.Image)
        Me.btnClearProc.Location = New System.Drawing.Point(472, 78)
        Me.btnClearProc.Name = "btnClearProc"
        Me.btnClearProc.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProc.TabIndex = 3
        Me.btnClearProc.UseVisualStyleBackColor = False
        '
        'btnClrPlayingDevice
        '
        Me.btnClrPlayingDevice.BackColor = System.Drawing.Color.Transparent
        Me.btnClrPlayingDevice.BackgroundImage = CType(resources.GetObject("btnClrPlayingDevice.BackgroundImage"), System.Drawing.Image)
        Me.btnClrPlayingDevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClrPlayingDevice.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClrPlayingDevice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClrPlayingDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClrPlayingDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClrPlayingDevice.Image = CType(resources.GetObject("btnClrPlayingDevice.Image"), System.Drawing.Image)
        Me.btnClrPlayingDevice.Location = New System.Drawing.Point(989, 51)
        Me.btnClrPlayingDevice.Name = "btnClrPlayingDevice"
        Me.btnClrPlayingDevice.Size = New System.Drawing.Size(23, 23)
        Me.btnClrPlayingDevice.TabIndex = 15
        Me.btnClrPlayingDevice.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Panel5.Controls.Add(Me.lblconcptid)
        Me.Panel5.Controls.Add(Me.Label42)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.Label38)
        Me.Panel5.Location = New System.Drawing.Point(661, 52)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(299, 23)
        Me.Panel5.TabIndex = 483
        '
        'lblconcptid
        '
        Me.lblconcptid.AutoEllipsis = True
        Me.lblconcptid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblconcptid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblconcptid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblconcptid.Location = New System.Drawing.Point(1, 1)
        Me.lblconcptid.Name = "lblconcptid"
        Me.lblconcptid.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.lblconcptid.Size = New System.Drawing.Size(297, 21)
        Me.lblconcptid.TabIndex = 209
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(298, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 21)
        Me.Label42.TabIndex = 15
        Me.Label42.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(0, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 21)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(299, 1)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(0, 22)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(299, 1)
        Me.Label38.TabIndex = 12
        Me.Label38.Text = "label2"
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(566, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(92, 14)
        Me.Label8.TabIndex = 481
        Me.Label8.Text = "Playing Device :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnConceptID
        '
        Me.btnConceptID.BackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.BackgroundImage = CType(resources.GetObject("btnConceptID.BackgroundImage"), System.Drawing.Image)
        Me.btnConceptID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnConceptID.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConceptID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnConceptID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnConceptID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConceptID.Image = CType(resources.GetObject("btnConceptID.Image"), System.Drawing.Image)
        Me.btnConceptID.Location = New System.Drawing.Point(964, 51)
        Me.btnConceptID.Name = "btnConceptID"
        Me.btnConceptID.Size = New System.Drawing.Size(22, 23)
        Me.btnConceptID.TabIndex = 14
        Me.btnConceptID.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 446)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1020, 1)
        Me.Label5.TabIndex = 480
        Me.Label5.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1020, 1)
        Me.Label3.TabIndex = 479
        Me.Label3.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1024, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 444)
        Me.Label1.TabIndex = 478
        Me.Label1.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 444)
        Me.Label6.TabIndex = 477
        Me.Label6.Text = "label4"
        '
        'lblUDI
        '
        Me.lblUDI.AutoSize = True
        Me.lblUDI.ForeColor = System.Drawing.Color.Red
        Me.lblUDI.Location = New System.Drawing.Point(122, 127)
        Me.lblUDI.Name = "lblUDI"
        Me.lblUDI.Size = New System.Drawing.Size(14, 14)
        Me.lblUDI.TabIndex = 476
        Me.lblUDI.Text = "*"
        '
        'lblIssuingAgency
        '
        Me.lblIssuingAgency.AutoSize = True
        Me.lblIssuingAgency.ForeColor = System.Drawing.Color.Red
        Me.lblIssuingAgency.Location = New System.Drawing.Point(61, 193)
        Me.lblIssuingAgency.Name = "lblIssuingAgency"
        Me.lblIssuingAgency.Size = New System.Drawing.Size(14, 14)
        Me.lblIssuingAgency.TabIndex = 475
        Me.lblIssuingAgency.Text = "*"
        '
        'optActive
        '
        Me.optActive.AutoSize = True
        Me.optActive.Checked = True
        Me.optActive.Location = New System.Drawing.Point(664, 356)
        Me.optActive.Name = "optActive"
        Me.optActive.Size = New System.Drawing.Size(59, 18)
        Me.optActive.TabIndex = 22
        Me.optActive.TabStop = True
        Me.optActive.Text = "Active"
        Me.optActive.UseVisualStyleBackColor = True
        '
        'optInactive
        '
        Me.optInactive.AutoSize = True
        Me.optInactive.Location = New System.Drawing.Point(736, 356)
        Me.optInactive.Name = "optInactive"
        Me.optInactive.Size = New System.Drawing.Size(68, 18)
        Me.optInactive.TabIndex = 23
        Me.optInactive.Text = "Inactive"
        Me.optInactive.UseVisualStyleBackColor = True
        '
        'txt_ManufacturingDate
        '
        Me.txt_ManufacturingDate.Location = New System.Drawing.Point(173, 321)
        Me.txt_ManufacturingDate.MaxLength = 12
        Me.txt_ManufacturingDate.Name = "txt_ManufacturingDate"
        Me.txt_ManufacturingDate.Size = New System.Drawing.Size(294, 22)
        Me.txt_ManufacturingDate.TabIndex = 12
        '
        'txt_ExpiryDate
        '
        Me.txt_ExpiryDate.Location = New System.Drawing.Point(173, 288)
        Me.txt_ExpiryDate.MaxLength = 12
        Me.txt_ExpiryDate.Name = "txt_ExpiryDate"
        Me.txt_ExpiryDate.Size = New System.Drawing.Size(294, 22)
        Me.txt_ExpiryDate.TabIndex = 11
        '
        'txt_HCTP
        '
        Me.txt_HCTP.Location = New System.Drawing.Point(173, 387)
        Me.txt_HCTP.MaxLength = 30
        Me.txt_HCTP.Name = "txt_HCTP"
        Me.txt_HCTP.Size = New System.Drawing.Size(294, 22)
        Me.txt_HCTP.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 391)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(155, 14)
        Me.Label7.TabIndex = 469
        Me.Label7.Text = "HCT/P identification Code :"
        '
        'txt_UDI
        '
        Me.txt_UDI.Location = New System.Drawing.Point(173, 123)
        Me.txt_UDI.MaxLength = 150
        Me.txt_UDI.Name = "txt_UDI"
        Me.txt_UDI.Size = New System.Drawing.Size(294, 22)
        Me.txt_UDI.TabIndex = 5
        '
        'txt_SerialNumber
        '
        Me.txt_SerialNumber.Location = New System.Drawing.Point(173, 222)
        Me.txt_SerialNumber.MaxLength = 30
        Me.txt_SerialNumber.Name = "txt_SerialNumber"
        Me.txt_SerialNumber.Size = New System.Drawing.Size(294, 22)
        Me.txt_SerialNumber.TabIndex = 19
        '
        'lblMRI
        '
        Me.lblMRI.AutoSize = True
        Me.lblMRI.ForeColor = System.Drawing.Color.Red
        Me.lblMRI.Location = New System.Drawing.Point(532, 135)
        Me.lblMRI.Name = "lblMRI"
        Me.lblMRI.Size = New System.Drawing.Size(14, 14)
        Me.lblMRI.TabIndex = 464
        Me.lblMRI.Text = "*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(80, 226)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 14)
        Me.Label4.TabIndex = 465
        Me.Label4.Text = "Serial Number :"
        '
        'txt_LotBatch
        '
        Me.txt_LotBatch.Location = New System.Drawing.Point(173, 255)
        Me.txt_LotBatch.MaxLength = 30
        Me.txt_LotBatch.Name = "txt_LotBatch"
        Me.txt_LotBatch.Size = New System.Drawing.Size(294, 22)
        Me.txt_LotBatch.TabIndex = 20
        '
        'lblManufacturer
        '
        Me.lblManufacturer.AutoSize = True
        Me.lblManufacturer.ForeColor = System.Drawing.Color.Red
        Me.lblManufacturer.Location = New System.Drawing.Point(559, 315)
        Me.lblManufacturer.Name = "lblManufacturer"
        Me.lblManufacturer.Size = New System.Drawing.Size(14, 14)
        Me.lblManufacturer.TabIndex = 461
        Me.lblManufacturer.Text = "*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(40, 260)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 14)
        Me.Label2.TabIndex = 462
        Me.Label2.Text = "Lot or Batch Number :"
        '
        'txt_VersionOrModel
        '
        Me.txt_VersionOrModel.Location = New System.Drawing.Point(661, 86)
        Me.txt_VersionOrModel.MaxLength = 80
        Me.txt_VersionOrModel.Multiline = True
        Me.txt_VersionOrModel.Name = "txt_VersionOrModel"
        Me.txt_VersionOrModel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_VersionOrModel.Size = New System.Drawing.Size(299, 34)
        Me.txt_VersionOrModel.TabIndex = 16
        '
        'dtTransactionTime
        '
        Me.dtTransactionTime.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTransactionTime.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTransactionTime.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTransactionTime.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTransactionTime.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTransactionTime.CustomFormat = "hh:mm:tt"
        Me.dtTransactionTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTransactionTime.Location = New System.Drawing.Point(367, 19)
        Me.dtTransactionTime.Name = "dtTransactionTime"
        Me.dtTransactionTime.ShowCheckBox = True
        Me.dtTransactionTime.ShowUpDown = True
        Me.dtTransactionTime.Size = New System.Drawing.Size(100, 22)
        Me.dtTransactionTime.TabIndex = 1
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(48, 325)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(122, 14)
        Me.Label32.TabIndex = 425
        Me.Label32.Text = "Manufacturing Date :"
        '
        'txt_NRL
        '
        Me.txt_NRL.Location = New System.Drawing.Point(173, 354)
        Me.txt_NRL.MaxLength = 50
        Me.txt_NRL.Name = "txt_NRL"
        Me.txt_NRL.Size = New System.Drawing.Size(294, 22)
        Me.txt_NRL.TabIndex = 17
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(599, 23)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(59, 14)
        Me.Label47.TabIndex = 381
        Me.Label47.Text = "Provider :"
        '
        'cmbProvider
        '
        Me.cmbProvider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProvider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(661, 19)
        Me.cmbProvider.MaxLength = 1000
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(299, 22)
        Me.cmbProvider.TabIndex = 13
        '
        'dttransaction_date
        '
        Me.dttransaction_date.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dttransaction_date.CalendarMonthBackground = System.Drawing.Color.White
        Me.dttransaction_date.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dttransaction_date.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dttransaction_date.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dttransaction_date.CustomFormat = "MM/dd/yyyy  "
        Me.dttransaction_date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dttransaction_date.Location = New System.Drawing.Point(173, 19)
        Me.dttransaction_date.Name = "dttransaction_date"
        Me.dttransaction_date.ShowCheckBox = True
        Me.dttransaction_date.Size = New System.Drawing.Size(115, 22)
        Me.dttransaction_date.TabIndex = 0
        '
        'txt_MRIstatus
        '
        Me.txt_MRIstatus.Location = New System.Drawing.Point(661, 131)
        Me.txt_MRIstatus.MaxLength = 255
        Me.txt_MRIstatus.Multiline = True
        Me.txt_MRIstatus.Name = "txt_MRIstatus"
        Me.txt_MRIstatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_MRIstatus.Size = New System.Drawing.Size(299, 34)
        Me.txt_MRIstatus.TabIndex = 18
        '
        'lblAdministeredTime
        '
        Me.lblAdministeredTime.AutoSize = True
        Me.lblAdministeredTime.Location = New System.Drawing.Point(322, 23)
        Me.lblAdministeredTime.Name = "lblAdministeredTime"
        Me.lblAdministeredTime.Size = New System.Drawing.Size(42, 14)
        Me.lblAdministeredTime.TabIndex = 368
        Me.lblAdministeredTime.Text = "Time :"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(129, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(41, 14)
        Me.Label43.TabIndex = 369
        Me.Label43.Text = "Date :"
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.ForeColor = System.Drawing.Color.Red
        Me.lblProviderName.Location = New System.Drawing.Point(587, 23)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(14, 14)
        Me.lblProviderName.TabIndex = 380
        Me.lblProviderName.Text = "*"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(135, 127)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(35, 14)
        Me.Label41.TabIndex = 383
        Me.Label41.Text = "UDI :"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(571, 315)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(87, 14)
        Me.Label39.TabIndex = 398
        Me.Label39.Text = "Manufacturer :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(545, 136)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(113, 14)
        Me.Label33.TabIndex = 446
        Me.Label33.Text = "MRI Safety Status :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(103, 159)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(67, 14)
        Me.Label29.TabIndex = 392
        Me.Label29.Text = "Device ID :"
        '
        'lblNRL
        '
        Me.lblNRL.AutoSize = True
        Me.lblNRL.ForeColor = System.Drawing.Color.Red
        Me.lblNRL.Location = New System.Drawing.Point(484, 127)
        Me.lblNRL.Name = "lblNRL"
        Me.lblNRL.Size = New System.Drawing.Size(14, 14)
        Me.lblNRL.TabIndex = 432
        Me.lblNRL.Text = "*"
        '
        'lblVersionOrModel
        '
        Me.lblVersionOrModel.AutoSize = True
        Me.lblVersionOrModel.ForeColor = System.Drawing.Color.Red
        Me.lblVersionOrModel.Location = New System.Drawing.Point(540, 89)
        Me.lblVersionOrModel.Name = "lblVersionOrModel"
        Me.lblVersionOrModel.Size = New System.Drawing.Size(14, 14)
        Me.lblVersionOrModel.TabIndex = 427
        Me.lblVersionOrModel.Text = "*"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(27, 357)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(143, 14)
        Me.Label28.TabIndex = 433
        Me.Label28.Text = "Labeled Containing NRL :"
        '
        'lblDeviceID
        '
        Me.lblDeviceID.AutoSize = True
        Me.lblDeviceID.ForeColor = System.Drawing.Color.Red
        Me.lblDeviceID.Location = New System.Drawing.Point(89, 159)
        Me.lblDeviceID.Name = "lblDeviceID"
        Me.lblDeviceID.Size = New System.Drawing.Size(14, 14)
        Me.lblDeviceID.TabIndex = 391
        Me.lblDeviceID.Text = "*"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(552, 89)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(106, 14)
        Me.Label40.TabIndex = 428
        Me.Label40.Text = "Version or Model :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(93, 292)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(77, 14)
        Me.Label26.TabIndex = 408
        Me.Label26.Text = "Expiry Date :"
        '
        'txt_CompanyName
        '
        Me.txt_CompanyName.Location = New System.Drawing.Point(661, 311)
        Me.txt_CompanyName.MaxLength = 255
        Me.txt_CompanyName.Multiline = True
        Me.txt_CompanyName.Name = "txt_CompanyName"
        Me.txt_CompanyName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_CompanyName.Size = New System.Drawing.Size(299, 34)
        Me.txt_CompanyName.TabIndex = 10
        '
        'txt_DI
        '
        Me.txt_DI.Location = New System.Drawing.Point(173, 156)
        Me.txt_DI.MaxLength = 30
        Me.txt_DI.Name = "txt_DI"
        Me.txt_DI.Size = New System.Drawing.Size(294, 22)
        Me.txt_DI.TabIndex = 7
        '
        'txt_IssuingAgency
        '
        Me.txt_IssuingAgency.Location = New System.Drawing.Point(173, 189)
        Me.txt_IssuingAgency.MaxLength = 15
        Me.txt_IssuingAgency.Name = "txt_IssuingAgency"
        Me.txt_IssuingAgency.Size = New System.Drawing.Size(294, 22)
        Me.txt_IssuingAgency.TabIndex = 8
        '
        'txt_BrandName
        '
        Me.txt_BrandName.Location = New System.Drawing.Point(661, 266)
        Me.txt_BrandName.MaxLength = 100
        Me.txt_BrandName.Multiline = True
        Me.txt_BrandName.Name = "txt_BrandName"
        Me.txt_BrandName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_BrandName.Size = New System.Drawing.Size(299, 34)
        Me.txt_BrandName.TabIndex = 9
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(73, 193)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(97, 14)
        Me.Label37.TabIndex = 423
        Me.Label37.Text = "Issuing Agency :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(577, 269)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(81, 14)
        Me.Label23.TabIndex = 411
        Me.Label23.Text = "Brand Name :"
        '
        'lblBrandName
        '
        Me.lblBrandName.AutoSize = True
        Me.lblBrandName.ForeColor = System.Drawing.Color.Red
        Me.lblBrandName.Location = New System.Drawing.Point(565, 269)
        Me.lblBrandName.Name = "lblBrandName"
        Me.lblBrandName.Size = New System.Drawing.Size(14, 14)
        Me.lblBrandName.TabIndex = 410
        Me.lblBrandName.Text = "*"
        '
        'frmImplantableDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 504)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImplantableDevices"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Implantable Devices"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents tblStrip As gloToolStrip.gloToolStrip
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txt_VersionOrModel As System.Windows.Forms.TextBox
    Friend WithEvents btnParseUDI As System.Windows.Forms.Button
    Friend WithEvents dtTransactionTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txt_NRL As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents dttransaction_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_MRIstatus As System.Windows.Forms.TextBox
    Friend WithEvents lblAdministeredTime As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblProviderName As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblNRL As System.Windows.Forms.Label
    Friend WithEvents lblVersionOrModel As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblDeviceID As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txt_CompanyName As System.Windows.Forms.TextBox
    Friend WithEvents txt_DI As System.Windows.Forms.TextBox
    Friend WithEvents txt_IssuingAgency As System.Windows.Forms.TextBox
    Friend WithEvents txt_BrandName As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblBrandName As System.Windows.Forms.Label
    Friend WithEvents txt_LotBatch As System.Windows.Forms.TextBox
    Friend WithEvents lblManufacturer As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_SerialNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblMRI As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_UDI As System.Windows.Forms.TextBox
    Friend WithEvents txt_HCTP As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_ManufacturingDate As System.Windows.Forms.TextBox
    Friend WithEvents txt_ExpiryDate As System.Windows.Forms.TextBox
    Friend WithEvents optActive As System.Windows.Forms.RadioButton
    Friend WithEvents optInactive As System.Windows.Forms.RadioButton
    Friend WithEvents lblIssuingAgency As System.Windows.Forms.Label
    Friend WithEvents lblUDI As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnConceptID As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblconcptid As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents btnClrPlayingDevice As System.Windows.Forms.Button
    Friend WithEvents lstProcedures As System.Windows.Forms.ListBox
    Friend WithEvents btnBrowseProc As System.Windows.Forms.Button
    Friend WithEvents btnClearProc As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtDeviceDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtGmdnPTName As System.Windows.Forms.TextBox
End Class
