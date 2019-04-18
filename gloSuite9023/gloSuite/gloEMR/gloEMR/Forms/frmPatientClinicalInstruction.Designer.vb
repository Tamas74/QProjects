<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientClinicalInstruction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientClinicalInstruction))
        Me.txtInstruction = New System.Windows.Forms.TextBox()
        Me.txtInstructionDtl = New System.Windows.Forms.RichTextBox()
        Me.btnInstruction = New System.Windows.Forms.Button()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.mskDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_PatientInjuryDate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlMiddle.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_PatientInjuryDate.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtInstruction
        '
        Me.txtInstruction.Location = New System.Drawing.Point(110, 15)
        Me.txtInstruction.Name = "txtInstruction"
        Me.txtInstruction.Size = New System.Drawing.Size(501, 22)
        Me.txtInstruction.TabIndex = 1
        '
        'txtInstructionDtl
        '
        Me.txtInstructionDtl.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstructionDtl.Location = New System.Drawing.Point(110, 77)
        Me.txtInstructionDtl.MaxLength = 8000
        Me.txtInstructionDtl.Name = "txtInstructionDtl"
        Me.txtInstructionDtl.Size = New System.Drawing.Size(529, 174)
        Me.txtInstructionDtl.TabIndex = 3
        Me.txtInstructionDtl.Text = ""
        '
        'btnInstruction
        '
        Me.btnInstruction.BackColor = System.Drawing.Color.Transparent
        Me.btnInstruction.BackgroundImage = CType(resources.GetObject("btnInstruction.BackgroundImage"), System.Drawing.Image)
        Me.btnInstruction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInstruction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnInstruction.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnInstruction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstruction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInstruction.Image = CType(resources.GetObject("btnInstruction.Image"), System.Drawing.Image)
        Me.btnInstruction.Location = New System.Drawing.Point(617, 14)
        Me.btnInstruction.Name = "btnInstruction"
        Me.btnInstruction.Size = New System.Drawing.Size(24, 24)
        Me.btnInstruction.TabIndex = 0
        Me.btnInstruction.Text = "          "
        Me.btnInstruction.UseVisualStyleBackColor = False
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.Transparent
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.Controls.Add(Me.mskDate)
        Me.pnlMiddle.Controls.Add(Me.Label5)
        Me.pnlMiddle.Controls.Add(Me.txtInstructionDtl)
        Me.pnlMiddle.Controls.Add(Me.Label2)
        Me.pnlMiddle.Controls.Add(Me.Label1)
        Me.pnlMiddle.Controls.Add(Me.btnInstruction)
        Me.pnlMiddle.Controls.Add(Me.txtInstruction)
        Me.pnlMiddle.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMiddle.Controls.Add(Me.Label4)
        Me.pnlMiddle.Controls.Add(Me.Label6)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 58)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMiddle.Size = New System.Drawing.Size(669, 263)
        Me.pnlMiddle.TabIndex = 0
        '
        'mskDate
        '
        Me.mskDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskDate.ForeColor = System.Drawing.Color.Black
        Me.mskDate.Location = New System.Drawing.Point(111, 46)
        Me.mskDate.Mask = "00/00/0000"
        Me.mskDate.Name = "mskDate"
        Me.mskDate.Size = New System.Drawing.Size(154, 22)
        Me.mskDate.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(64, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 14)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Date :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Description :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Instruction :"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 259)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(661, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 256)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(665, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 256)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(663, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(17, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 14)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(51, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "*"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_PatientInjuryDate)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.ForeColor = System.Drawing.Color.Transparent
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(669, 58)
        Me.pnl_tlspTOP.TabIndex = 1
        Me.pnl_tlspTOP.TabStop = True
        '
        'tlsp_PatientInjuryDate
        '
        Me.tlsp_PatientInjuryDate.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientInjuryDate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientInjuryDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientInjuryDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsp_PatientInjuryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientInjuryDate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientInjuryDate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientInjuryDate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientInjuryDate.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientInjuryDate.Name = "tlsp_PatientInjuryDate"
        Me.tlsp_PatientInjuryDate.Size = New System.Drawing.Size(669, 58)
        Me.tlsp_PatientInjuryDate.TabIndex = 0
        Me.tlsp_PatientInjuryDate.TabStop = True
        Me.tlsp_PatientInjuryDate.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlMiddle)
        Me.pnlMain.Controls.Add(Me.pnl_tlspTOP)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(669, 321)
        Me.pnlMain.TabIndex = 36
        '
        'frmPatientClinicalInstruction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(669, 321)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientClinicalInstruction"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Clinical Instruction"
        Me.pnlMiddle.ResumeLayout(False)
        Me.pnlMiddle.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_PatientInjuryDate.ResumeLayout(False)
        Me.tlsp_PatientInjuryDate.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtInstruction As System.Windows.Forms.TextBox
    Friend WithEvents txtInstructionDtl As System.Windows.Forms.RichTextBox
    Friend WithEvents btnInstruction As System.Windows.Forms.Button
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_PatientInjuryDate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents mskDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
End Class
