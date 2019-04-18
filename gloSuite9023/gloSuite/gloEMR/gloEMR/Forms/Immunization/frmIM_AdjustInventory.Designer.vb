<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIM_AdjustInventory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIM_AdjustInventory))
        Me.pnl_tls_ = New System.Windows.Forms.Panel()
        Me.tlsImmunization = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_Save = New System.Windows.Forms.ToolStripButton()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.txtDosageOnHand = New System.Windows.Forms.TextBox()
        Me.txtLotNumber = New System.Windows.Forms.TextBox()
        Me.txtManufacturer = New System.Windows.Forms.TextBox()
        Me.txtTradeName = New System.Windows.Forms.TextBox()
        Me.txtVaccine = New System.Windows.Forms.TextBox()
        Me.txtSKU = New System.Windows.Forms.TextBox()
        Me.txtDosageReturnToStock = New System.Windows.Forms.TextBox()
        Me.txtCurrentInventory = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsImmunization.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsImmunization)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(518, 53)
        Me.pnl_tls_.TabIndex = 1
        Me.pnl_tls_.TabStop = True
        '
        'tlsImmunization
        '
        Me.tlsImmunization.BackColor = System.Drawing.Color.Transparent
        Me.tlsImmunization.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsImmunization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsImmunization.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsImmunization.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsImmunization.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_Save, Me.tblbtn_Close})
        Me.tlsImmunization.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsImmunization.Location = New System.Drawing.Point(0, 0)
        Me.tlsImmunization.Name = "tlsImmunization"
        Me.tlsImmunization.Size = New System.Drawing.Size(518, 53)
        Me.tlsImmunization.TabIndex = 0
        Me.tlsImmunization.TabStop = True
        Me.tlsImmunization.Text = "toolStrip1"
        '
        'btn_Save
        '
        Me.btn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Save.Image = CType(resources.GetObject("btn_Save.Image"), System.Drawing.Image)
        Me.btn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(66, 50)
        Me.btn_Save.Tag = "Save"
        Me.btn_Save.Text = "&Save&&Cls"
        Me.btn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_Save.ToolTipText = "Save and Close"
        '
        'pnlTop
        '
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.txtDosageOnHand)
        Me.pnlTop.Controls.Add(Me.txtLotNumber)
        Me.pnlTop.Controls.Add(Me.txtManufacturer)
        Me.pnlTop.Controls.Add(Me.txtTradeName)
        Me.pnlTop.Controls.Add(Me.txtVaccine)
        Me.pnlTop.Controls.Add(Me.txtSKU)
        Me.pnlTop.Controls.Add(Me.txtDosageReturnToStock)
        Me.pnlTop.Controls.Add(Me.txtCurrentInventory)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.Label24)
        Me.pnlTop.Controls.Add(Me.Label25)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.Label22)
        Me.pnlTop.Controls.Add(Me.Label21)
        Me.pnlTop.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlTop.Controls.Add(Me.lbl_pnlTop)
        Me.pnlTop.Controls.Add(Me.Label23)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(518, 347)
        Me.pnlTop.TabIndex = 0
        '
        'txtDosageOnHand
        '
        Me.txtDosageOnHand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtDosageOnHand.Location = New System.Drawing.Point(203, 64)
        Me.txtDosageOnHand.Name = "txtDosageOnHand"
        Me.txtDosageOnHand.ReadOnly = True
        Me.txtDosageOnHand.Size = New System.Drawing.Size(50, 22)
        Me.txtDosageOnHand.TabIndex = 57
        Me.txtDosageOnHand.TabStop = False
        Me.txtDosageOnHand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLotNumber
        '
        Me.txtLotNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtLotNumber.Location = New System.Drawing.Point(203, 306)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.ReadOnly = True
        Me.txtLotNumber.Size = New System.Drawing.Size(295, 22)
        Me.txtLotNumber.TabIndex = 57
        Me.txtLotNumber.TabStop = False
        '
        'txtManufacturer
        '
        Me.txtManufacturer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtManufacturer.Location = New System.Drawing.Point(203, 274)
        Me.txtManufacturer.Name = "txtManufacturer"
        Me.txtManufacturer.ReadOnly = True
        Me.txtManufacturer.Size = New System.Drawing.Size(295, 22)
        Me.txtManufacturer.TabIndex = 57
        Me.txtManufacturer.TabStop = False
        '
        'txtTradeName
        '
        Me.txtTradeName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtTradeName.Location = New System.Drawing.Point(203, 210)
        Me.txtTradeName.Name = "txtTradeName"
        Me.txtTradeName.ReadOnly = True
        Me.txtTradeName.Size = New System.Drawing.Size(295, 22)
        Me.txtTradeName.TabIndex = 57
        Me.txtTradeName.TabStop = False
        '
        'txtVaccine
        '
        Me.txtVaccine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtVaccine.Location = New System.Drawing.Point(203, 242)
        Me.txtVaccine.Name = "txtVaccine"
        Me.txtVaccine.ReadOnly = True
        Me.txtVaccine.Size = New System.Drawing.Size(295, 22)
        Me.txtVaccine.TabIndex = 57
        Me.txtVaccine.TabStop = False
        '
        'txtSKU
        '
        Me.txtSKU.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtSKU.Location = New System.Drawing.Point(203, 178)
        Me.txtSKU.Name = "txtSKU"
        Me.txtSKU.ReadOnly = True
        Me.txtSKU.Size = New System.Drawing.Size(295, 22)
        Me.txtSKU.TabIndex = 57
        Me.txtSKU.TabStop = False
        '
        'txtDosageReturnToStock
        '
        Me.txtDosageReturnToStock.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtDosageReturnToStock.Location = New System.Drawing.Point(203, 95)
        Me.txtDosageReturnToStock.Name = "txtDosageReturnToStock"
        Me.txtDosageReturnToStock.ReadOnly = True
        Me.txtDosageReturnToStock.Size = New System.Drawing.Size(50, 22)
        Me.txtDosageReturnToStock.TabIndex = 57
        Me.txtDosageReturnToStock.TabStop = False
        Me.txtDosageReturnToStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCurrentInventory
        '
        Me.txtCurrentInventory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtCurrentInventory.Location = New System.Drawing.Point(203, 125)
        Me.txtCurrentInventory.MaxLength = 5
        Me.txtCurrentInventory.Name = "txtCurrentInventory"
        Me.txtCurrentInventory.Size = New System.Drawing.Size(50, 22)
        Me.txtCurrentInventory.TabIndex = 57
        Me.txtCurrentInventory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(514, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 340)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(494, 49)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "Please indicate what adjustment must be made to the vaccine inventory to account " & _
    "for deleting this vaccine record :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(11, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(500, 2)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(112, 277)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Manufacturer :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(58, 68)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(140, 14)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Current doses on hand :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(28, 129)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(170, 14)
        Me.Label25.TabIndex = 13
        Me.Label25.Text = "Doses on hand after change :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(114, 309)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 14)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Lot Number :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(50, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(148, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Doses returned to stock :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(117, 213)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(82, 14)
        Me.Label22.TabIndex = 40
        Me.Label22.Text = "Trade Name :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(162, 181)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(37, 14)
        Me.Label21.TabIndex = 32
        Me.Label21.Text = "SKU :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 340)
        Me.lbl_pnlLeft.TabIndex = 1
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(512, 1)
        Me.lbl_pnlTop.TabIndex = 6
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(142, 245)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(57, 14)
        Me.Label23.TabIndex = 38
        Me.Label23.Text = "Vaccine :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'frmIM_AdjustInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(518, 400)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIM_AdjustInventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Adjust Inventory"
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsImmunization.ResumeLayout(False)
        Me.tlsImmunization.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsImmunization As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Protected Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Protected Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Protected Friend WithEvents Label22 As System.Windows.Forms.Label
    Protected Friend WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtCurrentInventory As System.Windows.Forms.TextBox
    Friend WithEvents txtDosageReturnToStock As System.Windows.Forms.TextBox
    Friend WithEvents txtDosageOnHand As System.Windows.Forms.TextBox
    Friend WithEvents txtLotNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtManufacturer As System.Windows.Forms.TextBox
    Friend WithEvents txtTradeName As System.Windows.Forms.TextBox
    Friend WithEvents txtVaccine As System.Windows.Forms.TextBox
    Friend WithEvents txtSKU As System.Windows.Forms.TextBox
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
End Class
