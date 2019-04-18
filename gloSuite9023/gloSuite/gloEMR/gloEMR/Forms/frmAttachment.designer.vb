<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttachment
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
            Try
                If (IsNothing(dlgOpenFile) = False) Then
                    dlgOpenFile.Dispose()
                    dlgOpenFile = Nothing
                End If
            Catch ex As Exception

            End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttachment))
        Me.pnl_Attach = New System.Windows.Forms.Panel()
        Me.pnlErrorMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlReconciliationType = New System.Windows.Forms.Panel()
        Me.chkSendTask = New System.Windows.Forms.CheckBox()
        Me.cmbImportnExtract = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rbExistingPatient = New System.Windows.Forms.RadioButton()
        Me.rbNewPatient = New System.Windows.Forms.RadioButton()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.txtClinicalPath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblOK = New System.Windows.Forms.ToolStripButton()
        Me.tblPreview = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.tblMapping = New System.Windows.Forms.ToolStripButton()
        Me.tblCCDMapping = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Attach.SuspendLayout()
        Me.pnlErrorMessage.SuspendLayout()
        Me.pnlReconciliationType.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Attach
        '
        Me.pnl_Attach.Controls.Add(Me.pnlErrorMessage)
        Me.pnl_Attach.Controls.Add(Me.pnlReconciliationType)
        Me.pnl_Attach.Controls.Add(Me.rbExistingPatient)
        Me.pnl_Attach.Controls.Add(Me.rbNewPatient)
        Me.pnl_Attach.Controls.Add(Me.btnOpenFile)
        Me.pnl_Attach.Controls.Add(Me.txtClinicalPath)
        Me.pnl_Attach.Controls.Add(Me.Label6)
        Me.pnl_Attach.Controls.Add(Me.Label4)
        Me.pnl_Attach.Controls.Add(Me.Label3)
        Me.pnl_Attach.Controls.Add(Me.Label2)
        Me.pnl_Attach.Controls.Add(Me.Label1)
        Me.pnl_Attach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Attach.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Attach.Name = "pnl_Attach"
        Me.pnl_Attach.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Attach.Size = New System.Drawing.Size(608, 134)
        Me.pnl_Attach.TabIndex = 0
        '
        'pnlErrorMessage
        '
        Me.pnlErrorMessage.BackColor = System.Drawing.Color.White
        Me.pnlErrorMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlErrorMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlErrorMessage.Controls.Add(Me.Label24)
        Me.pnlErrorMessage.Location = New System.Drawing.Point(214, 41)
        Me.pnlErrorMessage.Name = "pnlErrorMessage"
        Me.pnlErrorMessage.Size = New System.Drawing.Size(180, 53)
        Me.pnlErrorMessage.TabIndex = 76
        Me.pnlErrorMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(16, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(110, 18)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'pnlReconciliationType
        '
        Me.pnlReconciliationType.Controls.Add(Me.chkSendTask)
        Me.pnlReconciliationType.Controls.Add(Me.cmbImportnExtract)
        Me.pnlReconciliationType.Controls.Add(Me.Label5)
        Me.pnlReconciliationType.Location = New System.Drawing.Point(15, 63)
        Me.pnlReconciliationType.Name = "pnlReconciliationType"
        Me.pnlReconciliationType.Size = New System.Drawing.Size(561, 59)
        Me.pnlReconciliationType.TabIndex = 7
        '
        'chkSendTask
        '
        Me.chkSendTask.AutoSize = True
        Me.chkSendTask.Checked = True
        Me.chkSendTask.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSendTask.Location = New System.Drawing.Point(174, 35)
        Me.chkSendTask.Name = "chkSendTask"
        Me.chkSendTask.Size = New System.Drawing.Size(83, 18)
        Me.chkSendTask.TabIndex = 7
        Me.chkSendTask.Text = "Send Task"
        Me.chkSendTask.UseVisualStyleBackColor = True
        '
        'cmbImportnExtract
        '
        Me.cmbImportnExtract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbImportnExtract.FormattingEnabled = True
        Me.cmbImportnExtract.Location = New System.Drawing.Point(174, 6)
        Me.cmbImportnExtract.Name = "cmbImportnExtract"
        Me.cmbImportnExtract.Size = New System.Drawing.Size(378, 22)
        Me.cmbImportnExtract.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(154, 14)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Select Reconciliation Type:"
        '
        'rbExistingPatient
        '
        Me.rbExistingPatient.AutoSize = True
        Me.rbExistingPatient.Checked = True
        Me.rbExistingPatient.Location = New System.Drawing.Point(422, 9)
        Me.rbExistingPatient.Name = "rbExistingPatient"
        Me.rbExistingPatient.Size = New System.Drawing.Size(109, 18)
        Me.rbExistingPatient.TabIndex = 1
        Me.rbExistingPatient.TabStop = True
        Me.rbExistingPatient.Text = "Existing Patient"
        Me.rbExistingPatient.UseVisualStyleBackColor = True
        '
        'rbNewPatient
        '
        Me.rbNewPatient.AutoSize = True
        Me.rbNewPatient.Location = New System.Drawing.Point(189, 9)
        Me.rbNewPatient.Name = "rbNewPatient"
        Me.rbNewPatient.Size = New System.Drawing.Size(227, 18)
        Me.rbNewPatient.TabIndex = 0
        Me.rbNewPatient.Text = "New Patient [Register from File Info]"
        Me.rbNewPatient.UseVisualStyleBackColor = True
        '
        'btnOpenFile
        '
        Me.btnOpenFile.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenFile.Image = CType(resources.GetObject("btnOpenFile.Image"), System.Drawing.Image)
        Me.btnOpenFile.Location = New System.Drawing.Point(573, 35)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(22, 22)
        Me.btnOpenFile.TabIndex = 4
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'txtClinicalPath
        '
        Me.txtClinicalPath.Location = New System.Drawing.Point(189, 34)
        Me.txtClinicalPath.Name = "txtClinicalPath"
        Me.txtClinicalPath.Size = New System.Drawing.Size(381, 22)
        Me.txtClinicalPath.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(165, 14)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Select Clinical Document File:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 126)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(604, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 126)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(602, 1)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(602, 1)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.FileName = "dlgOpenFile"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.Controls.Add(Me.tblMedication)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(608, 54)
        Me.pnlToolStrip.TabIndex = 1
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblOK, Me.tblPreview, Me.tblClose, Me.tblMapping, Me.tblCCDMapping})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(608, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblOK
        '
        Me.tblOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblOK.Image = CType(resources.GetObject("tblOK.Image"), System.Drawing.Image)
        Me.tblOK.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblOK.Name = "tblOK"
        Me.tblOK.Size = New System.Drawing.Size(58, 50)
        Me.tblOK.Tag = "Import"
        Me.tblOK.Text = " &Import"
        Me.tblOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblOK.ToolTipText = "Import File "
        '
        'tblPreview
        '
        Me.tblPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblPreview.Image = CType(resources.GetObject("tblPreview.Image"), System.Drawing.Image)
        Me.tblPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblPreview.Name = "tblPreview"
        Me.tblPreview.Size = New System.Drawing.Size(89, 50)
        Me.tblPreview.Text = "&Preview CDA"
        Me.tblPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblPreview.ToolTipText = "Preview CDA"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(51, 50)
        Me.tblClose.Text = " &Close "
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'tblMapping
        '
        Me.tblMapping.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMapping.Image = CType(resources.GetObject("tblMapping.Image"), System.Drawing.Image)
        Me.tblMapping.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblMapping.Name = "tblMapping"
        Me.tblMapping.Size = New System.Drawing.Size(93, 50)
        Me.tblMapping.Tag = "CCR Mapping"
        Me.tblMapping.Text = "CC&R Mapping"
        Me.tblMapping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblMapping.ToolTipText = "CCR Mapping"
        Me.tblMapping.Visible = False
        '
        'tblCCDMapping
        '
        Me.tblCCDMapping.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCDMapping.Image = CType(resources.GetObject("tblCCDMapping.Image"), System.Drawing.Image)
        Me.tblCCDMapping.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCDMapping.Name = "tblCCDMapping"
        Me.tblCCDMapping.Size = New System.Drawing.Size(93, 50)
        Me.tblCCDMapping.Tag = "CCD Mapping"
        Me.tblCCDMapping.Text = "CC&D Mapping"
        Me.tblCCDMapping.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCDMapping.ToolTipText = "CCD Mapping"
        Me.tblCCDMapping.Visible = False
        '
        'frmAttachment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(608, 188)
        Me.Controls.Add(Me.pnl_Attach)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAttachment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import CCD-CCR-CDA Files"
        Me.pnl_Attach.ResumeLayout(False)
        Me.pnl_Attach.PerformLayout()
        Me.pnlErrorMessage.ResumeLayout(False)
        Me.pnlErrorMessage.PerformLayout()
        Me.pnlReconciliationType.ResumeLayout(False)
        Me.pnlReconciliationType.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_Attach As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents txtClinicalPath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblMapping As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblCCDMapping As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbExistingPatient As System.Windows.Forms.RadioButton
    Friend WithEvents rbNewPatient As System.Windows.Forms.RadioButton
    Friend WithEvents cmbImportnExtract As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlReconciliationType As System.Windows.Forms.Panel
    Friend WithEvents chkSendTask As System.Windows.Forms.CheckBox
    Friend WithEvents tblPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlErrorMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label

End Class
