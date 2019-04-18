<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_ImplantDevice
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DtProcDate}
                Dim cntControls() As System.Windows.Forms.Control = {DtProcDate}
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

             

                If (IsNothing(dtpControls) = False) Then
                    If dtpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    End If
                End If


                If (IsNothing(cntControls) = False) Then
                    If cntControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                    End If
                End If


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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_ImplantDevice))
        Me.tls_Implant = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlstrip_Save = New System.Windows.Forms.ToolStripButton
        Me.tlstrip_DeleteRow = New System.Windows.Forms.ToolStripButton
        Me.tlstrip_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlPatientDetails = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label175 = New System.Windows.Forms.Label
        Me.Label177 = New System.Windows.Forms.Label
        Me.C1ImplantDevice = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtImpVen = New System.Windows.Forms.TextBox
        Me.txtImpAtr = New System.Windows.Forms.TextBox
        Me.txtSenVen = New System.Windows.Forms.TextBox
        Me.txtSenAtr = New System.Windows.Forms.TextBox
        Me.txtThVen = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtThAtr = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtLeadLoc = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtPhLoc = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.pnlSelectFromList = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCPTCode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnClearAllCPT = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnClearCPT = New System.Windows.Forms.Button
        Me.btnBrowseCPT = New System.Windows.Forms.Button
        Me.lstCPTcode = New System.Windows.Forms.ListBox
        Me.lstProcedures = New System.Windows.Forms.ListBox
        Me.btnBrowseProc = New System.Windows.Forms.Button
        Me.btnClearProc = New System.Windows.Forms.Button
        Me.BtnClearAllProc = New System.Windows.Forms.Button
        Me.lblProc = New System.Windows.Forms.Label
        Me.pnlDateProcedure = New System.Windows.Forms.Panel
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.DtProcDate = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPatientStrip = New System.Windows.Forms.Panel
        Me.pnlCustomTask = New System.Windows.Forms.Panel
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.tls_Implant.SuspendLayout()
        Me.pnlPatientDetails.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1ImplantDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlSelectFromList.SuspendLayout()
        Me.pnlDateProcedure.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.pnlCustomTask.SuspendLayout()
        Me.SuspendLayout()
        '
        'tls_Implant
        '
        Me.tls_Implant.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_Implant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Implant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Implant.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Implant.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Implant.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlstrip_Save, Me.tlstrip_DeleteRow, Me.tlstrip_Close})
        Me.tls_Implant.Location = New System.Drawing.Point(0, 0)
        Me.tls_Implant.Name = "tls_Implant"
        Me.tls_Implant.Size = New System.Drawing.Size(943, 53)
        Me.tls_Implant.TabIndex = 0
        Me.tls_Implant.TabStop = True
        Me.tls_Implant.Text = "ToolStrip1"
        '
        'tlstrip_Save
        '
        Me.tlstrip_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Save.Image = CType(resources.GetObject("tlstrip_Save.Image"), System.Drawing.Image)
        Me.tlstrip_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Save.Name = "tlstrip_Save"
        Me.tlstrip_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlstrip_Save.Text = "&Save&&Cls"
        Me.tlstrip_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlstrip_Save.ToolTipText = "Save and Close"
        '
        'tlstrip_DeleteRow
        '
        Me.tlstrip_DeleteRow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlstrip_DeleteRow.Image = CType(resources.GetObject("tlstrip_DeleteRow.Image"), System.Drawing.Image)
        Me.tlstrip_DeleteRow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_DeleteRow.Name = "tlstrip_DeleteRow"
        Me.tlstrip_DeleteRow.Size = New System.Drawing.Size(50, 50)
        Me.tlstrip_DeleteRow.Text = "&Delete"
        Me.tlstrip_DeleteRow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlstrip_Close
        '
        Me.tlstrip_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlstrip_Close.Image = CType(resources.GetObject("tlstrip_Close.Image"), System.Drawing.Image)
        Me.tlstrip_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlstrip_Close.Name = "tlstrip_Close"
        Me.tlstrip_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlstrip_Close.Text = "&Close"
        Me.tlstrip_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlPatientDetails
        '
        Me.pnlPatientDetails.Controls.Add(Me.Panel1)
        Me.pnlPatientDetails.Controls.Add(Me.Panel2)
        Me.pnlPatientDetails.Controls.Add(Me.pnlSelectFromList)
        Me.pnlPatientDetails.Controls.Add(Me.pnlDateProcedure)
        Me.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientDetails.Location = New System.Drawing.Point(0, 56)
        Me.pnlPatientDetails.Name = "pnlPatientDetails"
        Me.pnlPatientDetails.Size = New System.Drawing.Size(943, 592)
        Me.pnlPatientDetails.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label175)
        Me.Panel1.Controls.Add(Me.Label177)
        Me.Panel1.Controls.Add(Me.C1ImplantDevice)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 219)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(943, 373)
        Me.Panel1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(939, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 368)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 369)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(936, 1)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "label1"
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label175.Location = New System.Drawing.Point(3, 1)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(1, 369)
        Me.Label175.TabIndex = 9
        Me.Label175.Text = "label4"
        '
        'Label177
        '
        Me.Label177.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label177.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label177.Location = New System.Drawing.Point(3, 0)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(937, 1)
        Me.Label177.TabIndex = 8
        Me.Label177.Text = "label1"
        '
        'C1ImplantDevice
        '
        Me.C1ImplantDevice.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1ImplantDevice.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ImplantDevice.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1ImplantDevice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ImplantDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ImplantDevice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ImplantDevice.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ImplantDevice.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ImplantDevice.Location = New System.Drawing.Point(3, 0)
        Me.C1ImplantDevice.Name = "C1ImplantDevice"
        Me.C1ImplantDevice.Rows.DefaultSize = 19
        Me.C1ImplantDevice.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ImplantDevice.Size = New System.Drawing.Size(937, 370)
        Me.C1ImplantDevice.StyleInfo = resources.GetString("C1ImplantDevice.StyleInfo")
        Me.C1ImplantDevice.TabIndex = 0
        Me.C1ImplantDevice.TabStop = False
        Me.C1ImplantDevice.Tree.NodeImageCollapsed = CType(resources.GetObject("C1ImplantDevice.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1ImplantDevice.Tree.NodeImageExpanded = CType(resources.GetObject("C1ImplantDevice.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtImpVen)
        Me.Panel2.Controls.Add(Me.txtImpAtr)
        Me.Panel2.Controls.Add(Me.txtSenVen)
        Me.Panel2.Controls.Add(Me.txtSenAtr)
        Me.Panel2.Controls.Add(Me.txtThVen)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.txtThAtr)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.txtLeadLoc)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.txtPhLoc)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 141)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(943, 78)
        Me.Panel2.TabIndex = 2
        '
        'txtImpVen
        '
        Me.txtImpVen.Location = New System.Drawing.Point(830, 40)
        Me.txtImpVen.Name = "txtImpVen"
        Me.txtImpVen.Size = New System.Drawing.Size(100, 22)
        Me.txtImpVen.TabIndex = 7
        '
        'txtImpAtr
        '
        Me.txtImpAtr.Location = New System.Drawing.Point(830, 13)
        Me.txtImpAtr.Name = "txtImpAtr"
        Me.txtImpAtr.Size = New System.Drawing.Size(100, 22)
        Me.txtImpAtr.TabIndex = 3
        '
        'txtSenVen
        '
        Me.txtSenVen.Location = New System.Drawing.Point(583, 40)
        Me.txtSenVen.Name = "txtSenVen"
        Me.txtSenVen.Size = New System.Drawing.Size(100, 22)
        Me.txtSenVen.TabIndex = 6
        '
        'txtSenAtr
        '
        Me.txtSenAtr.Location = New System.Drawing.Point(583, 13)
        Me.txtSenAtr.Name = "txtSenAtr"
        Me.txtSenAtr.Size = New System.Drawing.Size(100, 22)
        Me.txtSenAtr.TabIndex = 2
        '
        'txtThVen
        '
        Me.txtThVen.Location = New System.Drawing.Point(356, 40)
        Me.txtThVen.Name = "txtThVen"
        Me.txtThVen.Size = New System.Drawing.Size(100, 22)
        Me.txtThVen.TabIndex = 5
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(688, 44)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(139, 14)
        Me.Label22.TabIndex = 12
        Me.Label22.Text = "Impedence Ventricular :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtThAtr
        '
        Me.txtThAtr.Location = New System.Drawing.Point(356, 13)
        Me.txtThAtr.Name = "txtThAtr"
        Me.txtThAtr.Size = New System.Drawing.Size(100, 22)
        Me.txtThAtr.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(719, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(108, 14)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "Impedence Atrial :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLeadLoc
        '
        Me.txtLeadLoc.Location = New System.Drawing.Point(118, 40)
        Me.txtLeadLoc.Name = "txtLeadLoc"
        Me.txtLeadLoc.Size = New System.Drawing.Size(100, 22)
        Me.txtLeadLoc.TabIndex = 4
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(461, 44)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(119, 14)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "Sensing Ventricular :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPhLoc
        '
        Me.txtPhLoc.Location = New System.Drawing.Point(118, 13)
        Me.txtPhLoc.Name = "txtPhLoc"
        Me.txtPhLoc.Size = New System.Drawing.Size(100, 22)
        Me.txtPhLoc.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(492, 17)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(88, 14)
        Me.Label19.TabIndex = 12
        Me.Label19.Text = "Sensing Atrial :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(935, 1)
        Me.Label11.TabIndex = 301
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(223, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(131, 14)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "Threshold Ventricular :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(935, 1)
        Me.Label12.TabIndex = 300
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(254, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 14)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Threshold Atrial :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(939, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 75)
        Me.Label13.TabIndex = 299
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(20, 44)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 14)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "Leads Location :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 75)
        Me.Label14.TabIndex = 298
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(10, 17)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(106, 14)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Physical Location :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSelectFromList
        '
        Me.pnlSelectFromList.Controls.Add(Me.Label4)
        Me.pnlSelectFromList.Controls.Add(Me.Label3)
        Me.pnlSelectFromList.Controls.Add(Me.lblCPTCode)
        Me.pnlSelectFromList.Controls.Add(Me.Label5)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllCPT)
        Me.pnlSelectFromList.Controls.Add(Me.Label6)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearCPT)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseCPT)
        Me.pnlSelectFromList.Controls.Add(Me.lstCPTcode)
        Me.pnlSelectFromList.Controls.Add(Me.lstProcedures)
        Me.pnlSelectFromList.Controls.Add(Me.btnBrowseProc)
        Me.pnlSelectFromList.Controls.Add(Me.btnClearProc)
        Me.pnlSelectFromList.Controls.Add(Me.BtnClearAllProc)
        Me.pnlSelectFromList.Controls.Add(Me.lblProc)
        Me.pnlSelectFromList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectFromList.Location = New System.Drawing.Point(0, 47)
        Me.pnlSelectFromList.Name = "pnlSelectFromList"
        Me.pnlSelectFromList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSelectFromList.Size = New System.Drawing.Size(943, 94)
        Me.pnlSelectFromList.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(935, 1)
        Me.Label4.TabIndex = 296
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(935, 1)
        Me.Label3.TabIndex = 295
        '
        'lblCPTCode
        '
        Me.lblCPTCode.AutoSize = True
        Me.lblCPTCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCPTCode.Location = New System.Drawing.Point(79, 9)
        Me.lblCPTCode.Name = "lblCPTCode"
        Me.lblCPTCode.Size = New System.Drawing.Size(37, 14)
        Me.lblCPTCode.TabIndex = 294
        Me.lblCPTCode.Text = "CPT :"
        Me.lblCPTCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 91)
        Me.Label5.TabIndex = 297
        '
        'BtnClearAllCPT
        '
        Me.BtnClearAllCPT.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllCPT.BackgroundImage = CType(resources.GetObject("BtnClearAllCPT.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllCPT.Image = CType(resources.GetObject("BtnClearAllCPT.Image"), System.Drawing.Image)
        Me.BtnClearAllCPT.Location = New System.Drawing.Point(276, 58)
        Me.BtnClearAllCPT.Name = "BtnClearAllCPT"
        Me.BtnClearAllCPT.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllCPT.TabIndex = 3
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllCPT, "Clear All CPT")
        Me.BtnClearAllCPT.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(939, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 91)
        Me.Label6.TabIndex = 298
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(276, 32)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnClearCPT.TabIndex = 2
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearCPT, "Clear CPT")
        Me.btnClearCPT.UseVisualStyleBackColor = False
        '
        'btnBrowseCPT
        '
        Me.btnBrowseCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseCPT.BackgroundImage = CType(resources.GetObject("btnBrowseCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCPT.Image = CType(resources.GetObject("btnBrowseCPT.Image"), System.Drawing.Image)
        Me.btnBrowseCPT.Location = New System.Drawing.Point(276, 6)
        Me.btnBrowseCPT.Name = "btnBrowseCPT"
        Me.btnBrowseCPT.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseCPT.TabIndex = 1
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseCPT, "Browse CPT")
        Me.btnBrowseCPT.UseVisualStyleBackColor = False
        '
        'lstCPTcode
        '
        Me.lstCPTcode.FormattingEnabled = True
        Me.lstCPTcode.ItemHeight = 14
        Me.lstCPTcode.Location = New System.Drawing.Point(118, 6)
        Me.lstCPTcode.Name = "lstCPTcode"
        Me.lstCPTcode.Size = New System.Drawing.Size(152, 74)
        Me.lstCPTcode.TabIndex = 0
        '
        'lstProcedures
        '
        Me.lstProcedures.FormattingEnabled = True
        Me.lstProcedures.ItemHeight = 14
        Me.lstProcedures.Location = New System.Drawing.Point(583, 9)
        Me.lstProcedures.Name = "lstProcedures"
        Me.lstProcedures.Size = New System.Drawing.Size(152, 74)
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
        Me.btnBrowseProc.Location = New System.Drawing.Point(742, 9)
        Me.btnBrowseProc.Name = "btnBrowseProc"
        Me.btnBrowseProc.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseProc.TabIndex = 5
        Me.C1SuperTooltip1.SetToolTip(Me.btnBrowseProc, "Browse Procedure")
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
        Me.btnClearProc.Location = New System.Drawing.Point(742, 35)
        Me.btnClearProc.Name = "btnClearProc"
        Me.btnClearProc.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProc.TabIndex = 6
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearProc, "Clear Procedure")
        Me.btnClearProc.UseVisualStyleBackColor = False
        '
        'BtnClearAllProc
        '
        Me.BtnClearAllProc.BackColor = System.Drawing.Color.Transparent
        Me.BtnClearAllProc.BackgroundImage = CType(resources.GetObject("BtnClearAllProc.BackgroundImage"), System.Drawing.Image)
        Me.BtnClearAllProc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnClearAllProc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BtnClearAllProc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClearAllProc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAllProc.Image = CType(resources.GetObject("BtnClearAllProc.Image"), System.Drawing.Image)
        Me.BtnClearAllProc.Location = New System.Drawing.Point(742, 61)
        Me.BtnClearAllProc.Name = "BtnClearAllProc"
        Me.BtnClearAllProc.Size = New System.Drawing.Size(22, 22)
        Me.BtnClearAllProc.TabIndex = 7
        Me.C1SuperTooltip1.SetToolTip(Me.BtnClearAllProc, "Clear All Procedures")
        Me.BtnClearAllProc.UseVisualStyleBackColor = False
        '
        'lblProc
        '
        Me.lblProc.AutoSize = True
        Me.lblProc.BackColor = System.Drawing.Color.Transparent
        Me.lblProc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProc.Location = New System.Drawing.Point(494, 12)
        Me.lblProc.Name = "lblProc"
        Me.lblProc.Size = New System.Drawing.Size(86, 14)
        Me.lblProc.TabIndex = 233
        Me.lblProc.Text = "Procedure(s) :"
        Me.lblProc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlDateProcedure
        '
        Me.pnlDateProcedure.Controls.Add(Me.Label67)
        Me.pnlDateProcedure.Controls.Add(Me.Label66)
        Me.pnlDateProcedure.Controls.Add(Me.Label65)
        Me.pnlDateProcedure.Controls.Add(Me.Label64)
        Me.pnlDateProcedure.Controls.Add(Me.DtProcDate)
        Me.pnlDateProcedure.Controls.Add(Me.lblDate)
        Me.pnlDateProcedure.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDateProcedure.Location = New System.Drawing.Point(0, 0)
        Me.pnlDateProcedure.Name = "pnlDateProcedure"
        Me.pnlDateProcedure.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDateProcedure.Size = New System.Drawing.Size(943, 47)
        Me.pnlDateProcedure.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(4, 43)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(935, 1)
        Me.Label67.TabIndex = 301
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(4, 3)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(935, 1)
        Me.Label66.TabIndex = 300
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(939, 3)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 41)
        Me.Label65.TabIndex = 299
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(3, 3)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 41)
        Me.Label64.TabIndex = 298
        '
        'DtProcDate
        '
        Me.DtProcDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DtProcDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.DtProcDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DtProcDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DtProcDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DtProcDate.CustomFormat = "MM/dd/yyyy"
        Me.DtProcDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtProcDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtProcDate.Location = New System.Drawing.Point(118, 12)
        Me.DtProcDate.Name = "DtProcDate"
        Me.DtProcDate.Size = New System.Drawing.Size(121, 22)
        Me.DtProcDate.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(15, 16)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(101, 14)
        Me.lblDate.TabIndex = 12
        Me.lblDate.Text = "Date of Implant :"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tls_Implant)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(943, 56)
        Me.pnlToolStrip.TabIndex = 1
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlPatientStrip
        '
        Me.pnlPatientStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientStrip.Location = New System.Drawing.Point(52, 297)
        Me.pnlPatientStrip.Name = "pnlPatientStrip"
        Me.pnlPatientStrip.Size = New System.Drawing.Size(765, 54)
        Me.pnlPatientStrip.TabIndex = 301
        Me.pnlPatientStrip.Visible = False
        '
        'pnlCustomTask
        '
        Me.pnlCustomTask.Controls.Add(Me.Label8)
        Me.pnlCustomTask.Controls.Add(Me.Label7)
        Me.pnlCustomTask.Controls.Add(Me.Label9)
        Me.pnlCustomTask.Controls.Add(Me.Label10)
        Me.pnlCustomTask.Location = New System.Drawing.Point(311, 134)
        Me.pnlCustomTask.Name = "pnlCustomTask"
        Me.pnlCustomTask.Size = New System.Drawing.Size(311, 125)
        Me.pnlCustomTask.TabIndex = 302
        Me.pnlCustomTask.TabStop = True
        Me.pnlCustomTask.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(309, 1)
        Me.Label8.TabIndex = 301
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(309, 1)
        Me.Label7.TabIndex = 300
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(310, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 125)
        Me.Label9.TabIndex = 299
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 125)
        Me.Label10.TabIndex = 298
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(118, 13)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 0
        '
        'frmCV_ImplantDevice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(943, 648)
        Me.Controls.Add(Me.pnlCustomTask)
        Me.Controls.Add(Me.pnlPatientDetails)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.pnlPatientStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_ImplantDevice"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Implant Device"
        Me.tls_Implant.ResumeLayout(False)
        Me.tls_Implant.PerformLayout()
        Me.pnlPatientDetails.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1ImplantDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlSelectFromList.ResumeLayout(False)
        Me.pnlSelectFromList.PerformLayout()
        Me.pnlDateProcedure.ResumeLayout(False)
        Me.pnlDateProcedure.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.pnlCustomTask.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tls_Implant As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents pnlPatientDetails As System.Windows.Forms.Panel
    Friend WithEvents tlstrip_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlstrip_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1ImplantDevice As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label175 As System.Windows.Forms.Label
    Private WithEvents Label177 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlPatientStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlDateProcedure As System.Windows.Forms.Panel
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents DtProcDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents pnlSelectFromList As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCPTCode As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BtnClearAllCPT As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button
    Friend WithEvents btnBrowseCPT As System.Windows.Forms.Button
    Friend WithEvents lstCPTcode As System.Windows.Forms.ListBox
    Friend WithEvents lstProcedures As System.Windows.Forms.ListBox
    Friend WithEvents btnBrowseProc As System.Windows.Forms.Button
    Friend WithEvents btnClearProc As System.Windows.Forms.Button
    Friend WithEvents BtnClearAllProc As System.Windows.Forms.Button
    Friend WithEvents lblProc As System.Windows.Forms.Label
    Friend WithEvents pnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tlstrip_DeleteRow As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtImpVen As System.Windows.Forms.TextBox
    Friend WithEvents txtImpAtr As System.Windows.Forms.TextBox
    Friend WithEvents txtSenVen As System.Windows.Forms.TextBox
    Friend WithEvents txtSenAtr As System.Windows.Forms.TextBox
    Friend WithEvents txtThVen As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtThAtr As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtLeadLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtPhLoc As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
