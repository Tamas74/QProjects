<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloFormularlyToolBarUserCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Protected Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloFormularlyToolBarUserCtrl))
        Me.pnlToolBar = New System.Windows.Forms.Panel()
        Me.GeneralToolBar = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlStrpPBMCombo = New System.Windows.Forms.ToolStripComboBox()
        Me.tStrpMedicationHistory = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_RxHub = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_PDR = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_ePA = New System.Windows.Forms.ToolStripButton()
        Me.tStrpFormularly = New System.Windows.Forms.ToolStripButton()
        Me.tlsPDMP = New System.Windows.Forms.ToolStripButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlToolBar.SuspendLayout()
        Me.GeneralToolBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Controls.Add(Me.GeneralToolBar)
        Me.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlToolBar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolBar.Margin = New System.Windows.Forms.Padding(41, 21, 41, 21)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(380, 53)
        Me.pnlToolBar.TabIndex = 0
        '
        'GeneralToolBar
        '
        Me.GeneralToolBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.GeneralToolBar.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.GeneralToolBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GeneralToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GeneralToolBar.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GeneralToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.GeneralToolBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.GeneralToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlStrpPBMCombo, Me.tStrpMedicationHistory, Me.tlbbtn_RxHub, Me.tlbbtn_PDR, Me.tlbbtn_ePA, Me.tStrpFormularly, Me.tlsPDMP})
        Me.GeneralToolBar.Location = New System.Drawing.Point(0, 0)
        Me.GeneralToolBar.Name = "GeneralToolBar"
        Me.GeneralToolBar.Padding = New System.Windows.Forms.Padding(0, 0, 14, 0)
        Me.GeneralToolBar.Size = New System.Drawing.Size(380, 53)
        Me.GeneralToolBar.TabIndex = 0
        Me.GeneralToolBar.Text = "ToolStrip1"
        '
        'tlStrpPBMCombo
        '
        Me.tlStrpPBMCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tlStrpPBMCombo.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.tlStrpPBMCombo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlStrpPBMCombo.Name = "tlStrpPBMCombo"
        Me.tlStrpPBMCombo.Size = New System.Drawing.Size(130, 53)
        Me.tlStrpPBMCombo.Tag = "Select Coverage"
        Me.tlStrpPBMCombo.ToolTipText = "Select Coverage"
        '
        'tStrpMedicationHistory
        '
        Me.tStrpMedicationHistory.BackColor = System.Drawing.Color.Transparent
        Me.tStrpMedicationHistory.BackgroundImage = CType(resources.GetObject("tStrpMedicationHistory.BackgroundImage"), System.Drawing.Image)
        Me.tStrpMedicationHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpMedicationHistory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tStrpMedicationHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpMedicationHistory.Image = CType(resources.GetObject("tStrpMedicationHistory.Image"), System.Drawing.Image)
        Me.tStrpMedicationHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpMedicationHistory.Name = "tStrpMedicationHistory"
        Me.tStrpMedicationHistory.Size = New System.Drawing.Size(53, 50)
        Me.tStrpMedicationHistory.Text = "Med &Hx"
        Me.tStrpMedicationHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpMedicationHistory.ToolTipText = "Medication History"
        '
        'tlbbtn_RxHub
        '
        Me.tlbbtn_RxHub.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_RxHub.BackgroundImage = CType(resources.GetObject("tlbbtn_RxHub.BackgroundImage"), System.Drawing.Image)
        Me.tlbbtn_RxHub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_RxHub.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_RxHub.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_RxHub.Image = CType(resources.GetObject("tlbbtn_RxHub.Image"), System.Drawing.Image)
        Me.tlbbtn_RxHub.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_RxHub.Name = "tlbbtn_RxHub"
        Me.tlbbtn_RxHub.Size = New System.Drawing.Size(45, 50)
        Me.tlbbtn_RxHub.Tag = "RxElig"
        Me.tlbbtn_RxHub.Text = "R&xElig"
        Me.tlbbtn_RxHub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_RxHub.ToolTipText = "Rx Eligiblity"
        '
        'tlbbtn_PDR
        '
        Me.tlbbtn_PDR.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_PDR.BackgroundImage = CType(resources.GetObject("tlbbtn_PDR.BackgroundImage"), System.Drawing.Image)
        Me.tlbbtn_PDR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_PDR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_PDR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_PDR.Image = CType(resources.GetObject("tlbbtn_PDR.Image"), System.Drawing.Image)
        Me.tlbbtn_PDR.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_PDR.Name = "tlbbtn_PDR"
        Me.tlbbtn_PDR.Size = New System.Drawing.Size(37, 50)
        Me.tlbbtn_PDR.Tag = "PDR"
        Me.tlbbtn_PDR.Text = "&PDR "
        Me.tlbbtn_PDR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_PDR.ToolTipText = "PDR (Physicians Desk Reference)  "
        Me.tlbbtn_PDR.Visible = False
        '
        'tlbbtn_ePA
        '
        Me.tlbbtn_ePA.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtn_ePA.BackgroundImage = CType(resources.GetObject("tlbbtn_ePA.BackgroundImage"), System.Drawing.Image)
        Me.tlbbtn_ePA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlbbtn_ePA.Enabled = False
        Me.tlbbtn_ePA.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlbbtn_ePA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_ePA.Image = CType(resources.GetObject("tlbbtn_ePA.Image"), System.Drawing.Image)
        Me.tlbbtn_ePA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_ePA.Name = "tlbbtn_ePA"
        Me.tlbbtn_ePA.Size = New System.Drawing.Size(36, 50)
        Me.tlbbtn_ePA.Tag = "RxElig"
        Me.tlbbtn_ePA.Text = "ePA"
        Me.tlbbtn_ePA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_ePA.ToolTipText = "Electronic Prior Authorization"
        '
        'tStrpFormularly
        '
        Me.tStrpFormularly.BackColor = System.Drawing.Color.Transparent
        Me.tStrpFormularly.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.tStrpFormularly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tStrpFormularly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tStrpFormularly.Image = CType(resources.GetObject("tStrpFormularly.Image"), System.Drawing.Image)
        Me.tStrpFormularly.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tStrpFormularly.Name = "tStrpFormularly"
        Me.tStrpFormularly.Size = New System.Drawing.Size(74, 50)
        Me.tStrpFormularly.Text = "&Formularly"
        Me.tStrpFormularly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tStrpFormularly.ToolTipText = "Formularly"
        Me.tStrpFormularly.Visible = False
        '
        'tlsPDMP
        '
        Me.tlsPDMP.BackColor = System.Drawing.Color.Transparent
        Me.tlsPDMP.BackgroundImage = CType(resources.GetObject("tlsPDMP.BackgroundImage"), System.Drawing.Image)
        Me.tlsPDMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPDMP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlsPDMP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsPDMP.Image = CType(resources.GetObject("tlsPDMP.Image"), System.Drawing.Image)
        Me.tlsPDMP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsPDMP.Name = "tlsPDMP"
        Me.tlsPDMP.Size = New System.Drawing.Size(43, 49)
        Me.tlsPDMP.Tag = "PDMP"
        Me.tlsPDMP.Text = "P&DMP"
        Me.tlsPDMP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsPDMP.ToolTipText = "Prescription drug monitoring program"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'gloFormularlyToolBarUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Margin = New System.Windows.Forms.Padding(41, 21, 41, 21)
        Me.Name = "gloFormularlyToolBarUserCtrl"
        Me.Size = New System.Drawing.Size(380, 53)
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlToolBar.PerformLayout()
        Me.GeneralToolBar.ResumeLayout(False)
        Me.GeneralToolBar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents pnlToolBar As System.Windows.Forms.Panel
    Public WithEvents GeneralToolBar As gloGlobal.gloToolStripIgnoreFocus
    Public WithEvents tStrpMedicationHistory As System.Windows.Forms.ToolStripButton
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents tStrpFormularly As System.Windows.Forms.ToolStripButton
    Public WithEvents tlStrpPBMCombo As System.Windows.Forms.ToolStripComboBox
    Public WithEvents tlbbtn_RxHub As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_ePA As System.Windows.Forms.ToolStripButton
    Public WithEvents tlbbtn_PDR As System.Windows.Forms.ToolStripButton
    Public WithEvents tlsPDMP As System.Windows.Forms.ToolStripButton

End Class
