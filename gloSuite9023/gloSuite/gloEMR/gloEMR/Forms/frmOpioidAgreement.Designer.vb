<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpioidAgreement
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try

            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtSignedVerifiedDate}
                Dim cntControls() As System.Windows.Forms.Control = {dtSignedVerifiedDate}
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOpioidAgreement))
        Me.pnl_btnmodifier = New System.Windows.Forms.Panel()
        Me.lnkSignedAgreement = New System.Windows.Forms.LinkLabel()
        Me.dtSignedVerifiedDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.chkSignedAgreement = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstripDiagnosis = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.pnl_btnmodifier.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripDiagnosis.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_btnmodifier
        '
        Me.pnl_btnmodifier.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnmodifier.Controls.Add(Me.lnkSignedAgreement)
        Me.pnl_btnmodifier.Controls.Add(Me.dtSignedVerifiedDate)
        Me.pnl_btnmodifier.Controls.Add(Me.Label3)
        Me.pnl_btnmodifier.Controls.Add(Me.Label2)
        Me.pnl_btnmodifier.Controls.Add(Me.txtNotes)
        Me.pnl_btnmodifier.Controls.Add(Me.chkSignedAgreement)
        Me.pnl_btnmodifier.Controls.Add(Me.Label1)
        Me.pnl_btnmodifier.Controls.Add(Me.Label19)
        Me.pnl_btnmodifier.Controls.Add(Me.Label20)
        Me.pnl_btnmodifier.Controls.Add(Me.Label25)
        Me.pnl_btnmodifier.Controls.Add(Me.Label26)
        Me.pnl_btnmodifier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_btnmodifier.Location = New System.Drawing.Point(0, 53)
        Me.pnl_btnmodifier.Name = "pnl_btnmodifier"
        Me.pnl_btnmodifier.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_btnmodifier.Size = New System.Drawing.Size(591, 202)
        Me.pnl_btnmodifier.TabIndex = 0
        '
        'lnkSignedAgreement
        '
        Me.lnkSignedAgreement.AutoSize = True
        Me.lnkSignedAgreement.LinkColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lnkSignedAgreement.Location = New System.Drawing.Point(240, 17)
        Me.lnkSignedAgreement.Name = "lnkSignedAgreement"
        Me.lnkSignedAgreement.Size = New System.Drawing.Size(141, 14)
        Me.lnkSignedAgreement.TabIndex = 18
        Me.lnkSignedAgreement.TabStop = True
        Me.lnkSignedAgreement.Text = "View Signed Agreement"
        Me.lnkSignedAgreement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtSignedVerifiedDate
        '
        Me.dtSignedVerifiedDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtSignedVerifiedDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtSignedVerifiedDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtSignedVerifiedDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtSignedVerifiedDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtSignedVerifiedDate.CustomFormat = "MM/dd/yyyy"
        Me.dtSignedVerifiedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtSignedVerifiedDate.Location = New System.Drawing.Point(219, 39)
        Me.dtSignedVerifiedDate.Name = "dtSignedVerifiedDate"
        Me.dtSignedVerifiedDate.Size = New System.Drawing.Size(108, 22)
        Me.dtSignedVerifiedDate.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(195, 14)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Signed Opioid Agreement on File :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(173, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Date :"
        '
        'txtNotes
        '
        Me.txtNotes.Location = New System.Drawing.Point(219, 68)
        Me.txtNotes.MaxLength = 500
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(342, 117)
        Me.txtNotes.TabIndex = 3
        '
        'chkSignedAgreement
        '
        Me.chkSignedAgreement.AutoSize = True
        Me.chkSignedAgreement.Location = New System.Drawing.Point(219, 18)
        Me.chkSignedAgreement.Name = "chkSignedAgreement"
        Me.chkSignedAgreement.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkSignedAgreement.Size = New System.Drawing.Size(15, 14)
        Me.chkSignedAgreement.TabIndex = 1
        Me.chkSignedAgreement.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(126, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Internal Note :"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(4, 198)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(583, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 195)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(587, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 195)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(585, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlsp_Top.Controls.Add(Me.tstripDiagnosis)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(591, 53)
        Me.pnl_tlsp_Top.TabIndex = 1
        '
        'tstripDiagnosis
        '
        Me.tstripDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.tstripDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstripDiagnosis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstripDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstripDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstripDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSave, Me.tlsbtnClose})
        Me.tstripDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.tstripDiagnosis.Name = "tstripDiagnosis"
        Me.tstripDiagnosis.Size = New System.Drawing.Size(591, 53)
        Me.tstripDiagnosis.TabIndex = 0
        Me.tstripDiagnosis.TabStop = True
        Me.tstripDiagnosis.Text = "ToolStrip1"
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Text = "&Save&&Cls"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'frmOpioidAgreement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(591, 255)
        Me.Controls.Add(Me.pnl_btnmodifier)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpioidAgreement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Opioid Agreement"
        Me.pnl_btnmodifier.ResumeLayout(False)
        Me.pnl_btnmodifier.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripDiagnosis.ResumeLayout(False)
        Me.tstripDiagnosis.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_btnmodifier As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstripDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents chkSignedAgreement As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents dtSignedVerifiedDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lnkSignedAgreement As System.Windows.Forms.LinkLabel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
End Class
