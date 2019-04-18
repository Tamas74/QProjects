<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientCarePlan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientCarePlan))
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.txtGoal = New System.Windows.Forms.RichTextBox()
        Me.btnMst = New System.Windows.Forms.Button()
        Me.txtNotes = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtInstruction = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtProblem = New System.Windows.Forms.TextBox()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsp_PatientInjuryDate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlMiddle.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_PatientInjuryDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.Transparent
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.Controls.Add(Me.txtGoal)
        Me.pnlMiddle.Controls.Add(Me.btnMst)
        Me.pnlMiddle.Controls.Add(Me.txtNotes)
        Me.pnlMiddle.Controls.Add(Me.Label5)
        Me.pnlMiddle.Controls.Add(Me.Label3)
        Me.pnlMiddle.Controls.Add(Me.Label4)
        Me.pnlMiddle.Controls.Add(Me.txtInstruction)
        Me.pnlMiddle.Controls.Add(Me.Label2)
        Me.pnlMiddle.Controls.Add(Me.Label1)
        Me.pnlMiddle.Controls.Add(Me.txtProblem)
        Me.pnlMiddle.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 56)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMiddle.Size = New System.Drawing.Size(794, 510)
        Me.pnlMiddle.TabIndex = 0
        '
        'txtGoal
        '
        Me.txtGoal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoal.Location = New System.Drawing.Point(131, 45)
        Me.txtGoal.MaxLength = 32767
        Me.txtGoal.Name = "txtGoal"
        Me.txtGoal.Size = New System.Drawing.Size(617, 142)
        Me.txtGoal.TabIndex = 2
        Me.txtGoal.Text = ""
        '
        'btnMst
        '
        Me.btnMst.BackColor = System.Drawing.Color.Transparent
        Me.btnMst.BackgroundImage = CType(resources.GetObject("btnMst.BackgroundImage"), System.Drawing.Image)
        Me.btnMst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMst.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMst.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMst.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMst.Image = CType(resources.GetObject("btnMst.Image"), System.Drawing.Image)
        Me.btnMst.Location = New System.Drawing.Point(752, 16)
        Me.btnMst.Name = "btnMst"
        Me.btnMst.Size = New System.Drawing.Size(22, 22)
        Me.btnMst.TabIndex = 0
        Me.btnMst.Text = "          "
        Me.btnMst.UseVisualStyleBackColor = False
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNotes.Location = New System.Drawing.Point(131, 194)
        Me.txtNotes.MaxLength = 32767
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(617, 142)
        Me.txtNotes.TabIndex = 3
        Me.txtNotes.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(35, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 14)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Internal Notes :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(90, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Goal :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(19, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 14)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "*"
        '
        'txtInstruction
        '
        Me.txtInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstruction.Location = New System.Drawing.Point(131, 343)
        Me.txtInstruction.MaxLength = 32767
        Me.txtInstruction.Name = "txtInstruction"
        Me.txtInstruction.Size = New System.Drawing.Size(617, 142)
        Me.txtInstruction.TabIndex = 4
        Me.txtInstruction.Text = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 346)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Patient Instruction :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Problem/Focus :"
        '
        'txtProblem
        '
        Me.txtProblem.Location = New System.Drawing.Point(131, 16)
        Me.txtProblem.Name = "txtProblem"
        Me.txtProblem.Size = New System.Drawing.Size(617, 22)
        Me.txtProblem.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 506)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(786, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 503)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(790, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 503)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(788, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
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
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(794, 56)
        Me.pnl_tlspTOP.TabIndex = 1
        Me.pnl_tlspTOP.TabStop = True
        '
        'tlsp_PatientInjuryDate
        '
        Me.tlsp_PatientInjuryDate.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PatientInjuryDate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PatientInjuryDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PatientInjuryDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PatientInjuryDate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PatientInjuryDate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_PatientInjuryDate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PatientInjuryDate.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PatientInjuryDate.Name = "tlsp_PatientInjuryDate"
        Me.tlsp_PatientInjuryDate.Size = New System.Drawing.Size(794, 53)
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
        'frmPatientCarePlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(794, 566)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientCarePlan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Care Plan"
        Me.pnlMiddle.ResumeLayout(False)
        Me.pnlMiddle.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_PatientInjuryDate.ResumeLayout(False)
        Me.tlsp_PatientInjuryDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtInstruction As System.Windows.Forms.RichTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProblem As System.Windows.Forms.TextBox
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_PatientInjuryDate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.RichTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnMst As System.Windows.Forms.Button
    Friend WithEvents txtGoal As System.Windows.Forms.RichTextBox
End Class
