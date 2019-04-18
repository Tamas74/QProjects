<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDosageCalculator
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDosageCalculator))
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.lblWeight = New System.Windows.Forms.Label
        Me.lblDosageOrdered = New System.Windows.Forms.Label
        Me.txtDosageOrdered = New System.Windows.Forms.TextBox
        Me.cmbDosageOrderedUnit = New System.Windows.Forms.ComboBox
        Me.lblDosageAvailable = New System.Windows.Forms.Label
        Me.txtDoseavailable = New System.Windows.Forms.TextBox
        Me.cmbWeightunit = New System.Windows.Forms.ComboBox
        Me.cmbDosageavailableUnit = New System.Windows.Forms.ComboBox
        Me.lblVolume = New System.Windows.Forms.Label
        Me.txtDosageVolume = New System.Windows.Forms.TextBox
        Me.cmbVolumeUnit = New System.Windows.Forms.ComboBox
        Me.txtCalulatedDosage = New System.Windows.Forms.TextBox
        Me.lblDosagevalue = New System.Windows.Forms.Label
        Me.pnlToostrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlbbtnCalculateDosage = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnOk = New System.Windows.Forms.ToolStripButton
        Me.tlbbtnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtTotalDoseOrdered = New System.Windows.Forms.TextBox
        Me.lblTotalDoseOrdered = New System.Windows.Forms.Label
        Me.txtDrugForm = New System.Windows.Forms.TextBox
        Me.cmbDysWksMnths = New System.Windows.Forms.ComboBox
        Me.lblDosageForm = New System.Windows.Forms.Label
        Me.txtTimes = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblDosageFrequency = New System.Windows.Forms.Label
        Me.txtDosageFrequency = New System.Windows.Forms.TextBox
        Me.pnlToostrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtWeight
        '
        Me.txtWeight.ForeColor = System.Drawing.Color.Black
        Me.txtWeight.Location = New System.Drawing.Point(150, 44)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(77, 22)
        Me.txtWeight.TabIndex = 1
        '
        'lblWeight
        '
        Me.lblWeight.AutoSize = True
        Me.lblWeight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWeight.Location = New System.Drawing.Point(92, 48)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(55, 14)
        Me.lblWeight.TabIndex = 2
        Me.lblWeight.Text = "Weight :"
        '
        'lblDosageOrdered
        '
        Me.lblDosageOrdered.AutoSize = True
        Me.lblDosageOrdered.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosageOrdered.Location = New System.Drawing.Point(43, 81)
        Me.lblDosageOrdered.Name = "lblDosageOrdered"
        Me.lblDosageOrdered.Size = New System.Drawing.Size(104, 14)
        Me.lblDosageOrdered.TabIndex = 4
        Me.lblDosageOrdered.Text = "Dosage Ordered :"
        '
        'txtDosageOrdered
        '
        Me.txtDosageOrdered.ForeColor = System.Drawing.Color.Black
        Me.txtDosageOrdered.Location = New System.Drawing.Point(150, 77)
        Me.txtDosageOrdered.Name = "txtDosageOrdered"
        Me.txtDosageOrdered.Size = New System.Drawing.Size(77, 22)
        Me.txtDosageOrdered.TabIndex = 3
        '
        'cmbDosageOrderedUnit
        '
        Me.cmbDosageOrderedUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDosageOrderedUnit.ForeColor = System.Drawing.Color.Black
        Me.cmbDosageOrderedUnit.FormattingEnabled = True
        Me.cmbDosageOrderedUnit.Items.AddRange(New Object() {"mg/kg", "mcg", "mg", "gr", "g", "u", "mEq", "ml", "cc", "gtt"})
        Me.cmbDosageOrderedUnit.Location = New System.Drawing.Point(236, 77)
        Me.cmbDosageOrderedUnit.Name = "cmbDosageOrderedUnit"
        Me.cmbDosageOrderedUnit.Size = New System.Drawing.Size(75, 22)
        Me.cmbDosageOrderedUnit.TabIndex = 4
        '
        'lblDosageAvailable
        '
        Me.lblDosageAvailable.AutoSize = True
        Me.lblDosageAvailable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosageAvailable.Location = New System.Drawing.Point(42, 147)
        Me.lblDosageAvailable.Name = "lblDosageAvailable"
        Me.lblDosageAvailable.Size = New System.Drawing.Size(105, 14)
        Me.lblDosageAvailable.TabIndex = 7
        Me.lblDosageAvailable.Text = "Dosage Available :"
        '
        'txtDoseavailable
        '
        Me.txtDoseavailable.ForeColor = System.Drawing.Color.Black
        Me.txtDoseavailable.Location = New System.Drawing.Point(150, 143)
        Me.txtDoseavailable.Name = "txtDoseavailable"
        Me.txtDoseavailable.Size = New System.Drawing.Size(77, 22)
        Me.txtDoseavailable.TabIndex = 5
        '
        'cmbWeightunit
        '
        Me.cmbWeightunit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWeightunit.ForeColor = System.Drawing.Color.Black
        Me.cmbWeightunit.FormattingEnabled = True
        Me.cmbWeightunit.Location = New System.Drawing.Point(236, 44)
        Me.cmbWeightunit.Name = "cmbWeightunit"
        Me.cmbWeightunit.Size = New System.Drawing.Size(75, 22)
        Me.cmbWeightunit.TabIndex = 2
        '
        'cmbDosageavailableUnit
        '
        Me.cmbDosageavailableUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDosageavailableUnit.ForeColor = System.Drawing.Color.Black
        Me.cmbDosageavailableUnit.FormattingEnabled = True
        Me.cmbDosageavailableUnit.Items.AddRange(New Object() {"", "%", "BAU", "C", "cc", "CFU", "DAgU", "ELU", "FCCu", "FFU", "g", "gr", "gtt", "IU", "kBq", "KIU", "Lf", "mcg", "mCi", "mEq", "mg", "mg/kg", "ml", "mMole", "molar", "MU", "pfu", "TCID", "TCID50", "tu", "u", "USPu", "X"})
        Me.cmbDosageavailableUnit.Location = New System.Drawing.Point(236, 143)
        Me.cmbDosageavailableUnit.Name = "cmbDosageavailableUnit"
        Me.cmbDosageavailableUnit.Size = New System.Drawing.Size(75, 22)
        Me.cmbDosageavailableUnit.TabIndex = 6
        '
        'lblVolume
        '
        Me.lblVolume.AutoSize = True
        Me.lblVolume.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVolume.Location = New System.Drawing.Point(91, 180)
        Me.lblVolume.Name = "lblVolume"
        Me.lblVolume.Size = New System.Drawing.Size(56, 14)
        Me.lblVolume.TabIndex = 11
        Me.lblVolume.Text = "Volume :"
        '
        'txtDosageVolume
        '
        Me.txtDosageVolume.ForeColor = System.Drawing.Color.Black
        Me.txtDosageVolume.Location = New System.Drawing.Point(150, 176)
        Me.txtDosageVolume.Name = "txtDosageVolume"
        Me.txtDosageVolume.Size = New System.Drawing.Size(77, 22)
        Me.txtDosageVolume.TabIndex = 7
        '
        'cmbVolumeUnit
        '
        Me.cmbVolumeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVolumeUnit.ForeColor = System.Drawing.Color.Black
        Me.cmbVolumeUnit.FormattingEnabled = True
        Me.cmbVolumeUnit.Items.AddRange(New Object() {"", "actuation", "ampule", "capsule", "dose", "drop", "g", "gtt/cc", "gtt/min", "gtt/ml", "h", "hr", "l", "mcl", "mg", "ml", "ml/hr", "scoop", "tab", "u"})
        Me.cmbVolumeUnit.Location = New System.Drawing.Point(236, 176)
        Me.cmbVolumeUnit.Name = "cmbVolumeUnit"
        Me.cmbVolumeUnit.Size = New System.Drawing.Size(75, 22)
        Me.cmbVolumeUnit.TabIndex = 8
        '
        'txtCalulatedDosage
        '
        Me.txtCalulatedDosage.ForeColor = System.Drawing.Color.Black
        Me.txtCalulatedDosage.Location = New System.Drawing.Point(150, 233)
        Me.txtCalulatedDosage.Name = "txtCalulatedDosage"
        Me.txtCalulatedDosage.ReadOnly = True
        Me.txtCalulatedDosage.Size = New System.Drawing.Size(77, 22)
        Me.txtCalulatedDosage.TabIndex = 14
        '
        'lblDosagevalue
        '
        Me.lblDosagevalue.AutoSize = True
        Me.lblDosagevalue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosagevalue.Location = New System.Drawing.Point(60, 237)
        Me.lblDosagevalue.Name = "lblDosagevalue"
        Me.lblDosagevalue.Size = New System.Drawing.Size(87, 14)
        Me.lblDosagevalue.TabIndex = 15
        Me.lblDosagevalue.Text = "Dosage value :"
        '
        'pnlToostrip
        '
        Me.pnlToostrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToostrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToostrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToostrip.Name = "pnlToostrip"
        Me.pnlToostrip.Size = New System.Drawing.Size(409, 54)
        Me.pnlToostrip.TabIndex = 16
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnCalculateDosage, Me.tlbbtnOk, Me.tlbbtnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(409, 54)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tlbbtnCalculateDosage
        '
        Me.tlbbtnCalculateDosage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnCalculateDosage.Image = CType(resources.GetObject("tlbbtnCalculateDosage.Image"), System.Drawing.Image)
        Me.tlbbtnCalculateDosage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnCalculateDosage.Name = "tlbbtnCalculateDosage"
        Me.tlbbtnCalculateDosage.Size = New System.Drawing.Size(66, 51)
        Me.tlbbtnCalculateDosage.Text = "&Calculate"
        Me.tlbbtnCalculateDosage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtnOk
        '
        Me.tlbbtnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnOk.Image = CType(resources.GetObject("tlbbtnOk.Image"), System.Drawing.Image)
        Me.tlbbtnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnOk.Name = "tlbbtnOk"
        Me.tlbbtnOk.Size = New System.Drawing.Size(66, 51)
        Me.tlbbtnOk.Text = "&Save&&Cls"
        Me.tlbbtnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtnOk.ToolTipText = "Save and Close"
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnClose.Image = CType(resources.GetObject("tlbbtnClose.Image"), System.Drawing.Image)
        Me.tlbbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Size = New System.Drawing.Size(43, 51)
        Me.tlbbtnClose.Text = "&Close"
        Me.tlbbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblDosageFrequency)
        Me.Panel2.Controls.Add(Me.txtDosageFrequency)
        Me.Panel2.Controls.Add(Me.txtTotalDoseOrdered)
        Me.Panel2.Controls.Add(Me.lblTotalDoseOrdered)
        Me.Panel2.Controls.Add(Me.txtDrugForm)
        Me.Panel2.Controls.Add(Me.cmbDysWksMnths)
        Me.Panel2.Controls.Add(Me.lblDosageForm)
        Me.Panel2.Controls.Add(Me.txtTimes)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.lblDosagevalue)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtCalulatedDosage)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.cmbVolumeUnit)
        Me.Panel2.Controls.Add(Me.lblDosageOrdered)
        Me.Panel2.Controls.Add(Me.lblVolume)
        Me.Panel2.Controls.Add(Me.txtWeight)
        Me.Panel2.Controls.Add(Me.txtDosageVolume)
        Me.Panel2.Controls.Add(Me.lblWeight)
        Me.Panel2.Controls.Add(Me.cmbDosageavailableUnit)
        Me.Panel2.Controls.Add(Me.txtDosageOrdered)
        Me.Panel2.Controls.Add(Me.cmbWeightunit)
        Me.Panel2.Controls.Add(Me.cmbDosageOrderedUnit)
        Me.Panel2.Controls.Add(Me.lblDosageAvailable)
        Me.Panel2.Controls.Add(Me.txtDoseavailable)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(409, 266)
        Me.Panel2.TabIndex = 20
        '
        'txtTotalDoseOrdered
        '
        Me.txtTotalDoseOrdered.ForeColor = System.Drawing.Color.Black
        Me.txtTotalDoseOrdered.Location = New System.Drawing.Point(150, 110)
        Me.txtTotalDoseOrdered.Name = "txtTotalDoseOrdered"
        Me.txtTotalDoseOrdered.ReadOnly = True
        Me.txtTotalDoseOrdered.Size = New System.Drawing.Size(77, 22)
        Me.txtTotalDoseOrdered.TabIndex = 23
        '
        'lblTotalDoseOrdered
        '
        Me.lblTotalDoseOrdered.AutoSize = True
        Me.lblTotalDoseOrdered.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDoseOrdered.Location = New System.Drawing.Point(11, 114)
        Me.lblTotalDoseOrdered.Name = "lblTotalDoseOrdered"
        Me.lblTotalDoseOrdered.Size = New System.Drawing.Size(136, 14)
        Me.lblTotalDoseOrdered.TabIndex = 22
        Me.lblTotalDoseOrdered.Text = "Total Dosage Ordered :"
        '
        'txtDrugForm
        '
        Me.txtDrugForm.ForeColor = System.Drawing.Color.Black
        Me.txtDrugForm.Location = New System.Drawing.Point(150, 11)
        Me.txtDrugForm.Name = "txtDrugForm"
        Me.txtDrugForm.ReadOnly = True
        Me.txtDrugForm.Size = New System.Drawing.Size(162, 22)
        Me.txtDrugForm.TabIndex = 21
        '
        'cmbDysWksMnths
        '
        Me.cmbDysWksMnths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDysWksMnths.ForeColor = System.Drawing.Color.Black
        Me.cmbDysWksMnths.FormattingEnabled = True
        Me.cmbDysWksMnths.Items.AddRange(New Object() {"daily", "weekly", "monthly"})
        Me.cmbDysWksMnths.Location = New System.Drawing.Point(317, 233)
        Me.cmbDysWksMnths.Name = "cmbDysWksMnths"
        Me.cmbDysWksMnths.Size = New System.Drawing.Size(75, 22)
        Me.cmbDysWksMnths.TabIndex = 20
        '
        'lblDosageForm
        '
        Me.lblDosageForm.AutoSize = True
        Me.lblDosageForm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosageForm.Location = New System.Drawing.Point(61, 15)
        Me.lblDosageForm.Name = "lblDosageForm"
        Me.lblDosageForm.Size = New System.Drawing.Size(86, 14)
        Me.lblDosageForm.TabIndex = 19
        Me.lblDosageForm.Text = "Dosage Form :"
        '
        'txtTimes
        '
        Me.txtTimes.ForeColor = System.Drawing.Color.Black
        Me.txtTimes.Location = New System.Drawing.Point(236, 233)
        Me.txtTimes.Name = "txtTimes"
        Me.txtTimes.Size = New System.Drawing.Size(75, 22)
        Me.txtTimes.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 262)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(401, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 259)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(405, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 259)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(403, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'lblDosageFrequency
        '
        Me.lblDosageFrequency.AutoSize = True
        Me.lblDosageFrequency.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDosageFrequency.Location = New System.Drawing.Point(31, 209)
        Me.lblDosageFrequency.Name = "lblDosageFrequency"
        Me.lblDosageFrequency.Size = New System.Drawing.Size(116, 14)
        Me.lblDosageFrequency.TabIndex = 27
        Me.lblDosageFrequency.Text = "Dosage Frequency :"
        '
        'txtDosageFrequency
        '
        Me.txtDosageFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtDosageFrequency.Location = New System.Drawing.Point(150, 205)
        Me.txtDosageFrequency.Name = "txtDosageFrequency"
        Me.txtDosageFrequency.ReadOnly = True
        Me.txtDosageFrequency.Size = New System.Drawing.Size(242, 22)
        Me.txtDosageFrequency.TabIndex = 26
        '
        'frmDosageCalculator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(409, 320)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToostrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDosageCalculator"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dosage Calculator"
        Me.pnlToostrip.ResumeLayout(False)
        Me.pnlToostrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblDosageOrdered As System.Windows.Forms.Label
    Friend WithEvents txtDosageOrdered As System.Windows.Forms.TextBox
    Friend WithEvents cmbDosageOrderedUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lblDosageAvailable As System.Windows.Forms.Label
    Friend WithEvents txtDoseavailable As System.Windows.Forms.TextBox
    Friend WithEvents cmbWeightunit As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDosageavailableUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lblVolume As System.Windows.Forms.Label
    Friend WithEvents txtDosageVolume As System.Windows.Forms.TextBox
    Friend WithEvents cmbVolumeUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtCalulatedDosage As System.Windows.Forms.TextBox
    Friend WithEvents lblDosagevalue As System.Windows.Forms.Label
    Friend WithEvents pnlToostrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtnCalculateDosage As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtTimes As System.Windows.Forms.TextBox
    Friend WithEvents lblDosageForm As System.Windows.Forms.Label
    Friend WithEvents cmbDysWksMnths As System.Windows.Forms.ComboBox
    Friend WithEvents txtDrugForm As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalDoseOrdered As System.Windows.Forms.Label
    Friend WithEvents txtTotalDoseOrdered As System.Windows.Forms.TextBox
    Friend WithEvents lblDosageFrequency As System.Windows.Forms.Label
    Friend WithEvents txtDosageFrequency As System.Windows.Forms.TextBox

End Class
