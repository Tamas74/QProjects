<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFamilyMemberSettings
    Inherits System.Windows.Forms.Form

    ''Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFamilyMemberSettings))
        Me.tlsp_SettingList = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnNew = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.PnlCustomTask = New System.Windows.Forms.Panel()
        Me.txtsnodesc = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSnomedcode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btn_Delete = New System.Windows.Forms.Button()
        Me.txt_ConceptID = New System.Windows.Forms.TextBox()
        Me.lbl_SnomedDescription = New System.Windows.Forms.Label()
        Me.lbl_ConceptID = New System.Windows.Forms.Label()
        Me.btn_SnomedCode = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRelation = New System.Windows.Forms.TextBox()
        Me.tlsp_SettingList.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlsp_SettingList
        '
        Me.tlsp_SettingList.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_SettingList.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_SettingList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_SettingList.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_SettingList.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_SettingList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnNew, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_SettingList.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_SettingList.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_SettingList.Name = "tlsp_SettingList"
        Me.tlsp_SettingList.Size = New System.Drawing.Size(524, 53)
        Me.tlsp_SettingList.TabIndex = 1
        Me.tlsp_SettingList.TabStop = True
        Me.tlsp_SettingList.Text = "toolStrip1"
        '
        'ts_btnNew
        '
        Me.ts_btnNew.Image = CType(resources.GetObject("ts_btnNew.Image"), System.Drawing.Image)
        Me.ts_btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnNew.Name = "ts_btnNew"
        Me.ts_btnNew.Size = New System.Drawing.Size(36, 49)
        Me.ts_btnNew.Tag = "New"
        Me.ts_btnNew.Text = "&New"
        Me.ts_btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnNew.ToolTipText = "New"
        Me.ts_btnNew.Visible = False
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnModify.Visible = False
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDelete.Visible = False
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlsp_SettingList)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(524, 56)
        Me.Panel1.TabIndex = 1
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.PnlCustomTask)
        Me.pnlSettings.Controls.Add(Me.txtsnodesc)
        Me.pnlSettings.Controls.Add(Me.Label9)
        Me.pnlSettings.Controls.Add(Me.Label8)
        Me.pnlSettings.Controls.Add(Me.Label5)
        Me.pnlSettings.Controls.Add(Me.Label4)
        Me.pnlSettings.Controls.Add(Me.txtSnomedcode)
        Me.pnlSettings.Controls.Add(Me.Label7)
        Me.pnlSettings.Controls.Add(Me.Label6)
        Me.pnlSettings.Controls.Add(Me.btn_Delete)
        Me.pnlSettings.Controls.Add(Me.txt_ConceptID)
        Me.pnlSettings.Controls.Add(Me.lbl_SnomedDescription)
        Me.pnlSettings.Controls.Add(Me.lbl_ConceptID)
        Me.pnlSettings.Controls.Add(Me.btn_SnomedCode)
        Me.pnlSettings.Controls.Add(Me.Label3)
        Me.pnlSettings.Controls.Add(Me.Label1)
        Me.pnlSettings.Controls.Add(Me.txtRelation)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 56)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSettings.Size = New System.Drawing.Size(524, 162)
        Me.pnlSettings.TabIndex = 0
        '
        'PnlCustomTask
        '
        Me.PnlCustomTask.Location = New System.Drawing.Point(223, 70)
        Me.PnlCustomTask.Name = "PnlCustomTask"
        Me.PnlCustomTask.Size = New System.Drawing.Size(294, 87)
        Me.PnlCustomTask.TabIndex = 259
        Me.PnlCustomTask.Visible = False
        '
        'txtsnodesc
        '
        Me.txtsnodesc.Enabled = False
        Me.txtsnodesc.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtsnodesc.Location = New System.Drawing.Point(148, 118)
        Me.txtsnodesc.Name = "txtsnodesc"
        Me.txtsnodesc.Size = New System.Drawing.Size(294, 22)
        Me.txtsnodesc.TabIndex = 273
        Me.txtsnodesc.Visible = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(520, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 154)
        Me.Label9.TabIndex = 272
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 154)
        Me.Label8.TabIndex = 271
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(518, 1)
        Me.Label5.TabIndex = 270
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(518, 1)
        Me.Label4.TabIndex = 269
        '
        'txtSnomedcode
        '
        Me.txtSnomedcode.BackColor = System.Drawing.Color.White
        Me.txtSnomedcode.Enabled = False
        Me.txtSnomedcode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSnomedcode.ForeColor = System.Drawing.Color.Black
        Me.txtSnomedcode.Location = New System.Drawing.Point(148, 84)
        Me.txtSnomedcode.MaxLength = 255
        Me.txtSnomedcode.Name = "txtSnomedcode"
        Me.txtSnomedcode.ReadOnly = True
        Me.txtSnomedcode.Size = New System.Drawing.Size(294, 22)
        Me.txtSnomedcode.TabIndex = 5
        Me.txtSnomedcode.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(70, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 14)
        Me.Label7.TabIndex = 268
        Me.Label7.Text = "SnoMed ID :"
        Me.Label7.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(71, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 13)
        Me.Label6.TabIndex = 266
        Me.Label6.Text = "*"
        '
        'btn_Delete
        '
        Me.btn_Delete.BackColor = System.Drawing.Color.Transparent
        Me.btn_Delete.BackgroundImage = CType(resources.GetObject("btn_Delete.BackgroundImage"), System.Drawing.Image)
        Me.btn_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Delete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Delete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Delete.Image = CType(resources.GetObject("btn_Delete.Image"), System.Drawing.Image)
        Me.btn_Delete.Location = New System.Drawing.Point(469, 49)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(21, 21)
        Me.btn_Delete.TabIndex = 4
        Me.btn_Delete.UseVisualStyleBackColor = False
        '
        'txt_ConceptID
        '
        Me.txt_ConceptID.BackColor = System.Drawing.Color.White
        Me.txt_ConceptID.Enabled = False
        Me.txt_ConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ConceptID.ForeColor = System.Drawing.Color.Black
        Me.txt_ConceptID.Location = New System.Drawing.Point(148, 50)
        Me.txt_ConceptID.MaxLength = 255
        Me.txt_ConceptID.Name = "txt_ConceptID"
        Me.txt_ConceptID.ReadOnly = True
        Me.txt_ConceptID.Size = New System.Drawing.Size(294, 22)
        Me.txt_ConceptID.TabIndex = 2
        '
        'lbl_SnomedDescription
        '
        Me.lbl_SnomedDescription.AutoSize = True
        Me.lbl_SnomedDescription.BackColor = System.Drawing.Color.Transparent
        Me.lbl_SnomedDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SnomedDescription.Location = New System.Drawing.Point(18, 121)
        Me.lbl_SnomedDescription.Name = "lbl_SnomedDescription"
        Me.lbl_SnomedDescription.Size = New System.Drawing.Size(127, 14)
        Me.lbl_SnomedDescription.TabIndex = 256
        Me.lbl_SnomedDescription.Text = "SNOMED Description :"
        Me.lbl_SnomedDescription.Visible = False
        '
        'lbl_ConceptID
        '
        Me.lbl_ConceptID.AutoSize = True
        Me.lbl_ConceptID.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ConceptID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ConceptID.Location = New System.Drawing.Point(82, 53)
        Me.lbl_ConceptID.Name = "lbl_ConceptID"
        Me.lbl_ConceptID.Size = New System.Drawing.Size(63, 14)
        Me.lbl_ConceptID.TabIndex = 255
        Me.lbl_ConceptID.Text = "SNOMED :"
        '
        'btn_SnomedCode
        '
        Me.btn_SnomedCode.BackColor = System.Drawing.Color.Transparent
        Me.btn_SnomedCode.BackgroundImage = CType(resources.GetObject("btn_SnomedCode.BackgroundImage"), System.Drawing.Image)
        Me.btn_SnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_SnomedCode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_SnomedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_SnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_SnomedCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_SnomedCode.Image = CType(resources.GetObject("btn_SnomedCode.Image"), System.Drawing.Image)
        Me.btn_SnomedCode.Location = New System.Drawing.Point(445, 49)
        Me.btn_SnomedCode.Name = "btn_SnomedCode"
        Me.btn_SnomedCode.Size = New System.Drawing.Size(21, 21)
        Me.btn_SnomedCode.TabIndex = 3
        Me.btn_SnomedCode.Text = "strSnomedDescription"
        Me.btn_SnomedCode.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(75, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 252
        Me.Label3.Text = "*"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(87, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 14)
        Me.Label1.TabIndex = 251
        Me.Label1.Text = "Relation :"
        '
        'txtRelation
        '
        Me.txtRelation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRelation.ForeColor = System.Drawing.Color.Black
        Me.txtRelation.Location = New System.Drawing.Point(148, 16)
        Me.txtRelation.MaxLength = 255
        Me.txtRelation.Name = "txtRelation"
        Me.txtRelation.Size = New System.Drawing.Size(294, 22)
        Me.txtRelation.TabIndex = 1
        '
        'frmFamilyMemberSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(524, 218)
        Me.Controls.Add(Me.pnlSettings)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFamilyMemberSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configure Family Member"
        Me.tlsp_SettingList.ResumeLayout(False)
        Me.tlsp_SettingList.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tlsp_SettingList As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents PnlCustomTask As System.Windows.Forms.Panel
    Friend WithEvents btn_Delete As System.Windows.Forms.Button
    Friend WithEvents txt_ConceptID As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SnomedDescription As System.Windows.Forms.Label
    Friend WithEvents lbl_ConceptID As System.Windows.Forms.Label
    Friend WithEvents btn_SnomedCode As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRelation As System.Windows.Forms.TextBox
    Friend WithEvents ts_btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSnomedcode As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtsnodesc As System.Windows.Forms.TextBox
End Class
