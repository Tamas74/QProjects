<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LM_SpecimenSetup
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDividerSetup As System.Windows.Forms.Label
    Friend WithEvents pnlToolStripView As System.Windows.Forms.Panel
    Friend WithEvents ToolStripView As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlCommandView As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    Me.lblDescription = New System.Windows.Forms.Label
    '    Me.txtDescription = New System.Windows.Forms.TextBox
    '    Me.pnlSetup = New System.Windows.Forms.Panel
    '    Me.lblDividerSetup = New System.Windows.Forms.Label
    '    Me.pnlCommandSetup = New System.Windows.Forms.Panel
    '    Me.btnCancel = New System.Windows.Forms.Button
    '    Me.btnOK = New System.Windows.Forms.Button
    '    Me.pnlView = New System.Windows.Forms.Panel
    '    Me.pnlCommandView = New System.Windows.Forms.Panel
    '    Me.btnClose = New System.Windows.Forms.Button
    '    Me.btnRefresh = New System.Windows.Forms.Button
    '    Me.btnDelete = New System.Windows.Forms.Button
    '    Me.btnModify = New System.Windows.Forms.Button
    '    Me.btnNew = New System.Windows.Forms.Button
    '    Me.lstSpecimens = New System.Windows.Forms.ListBox
    '    Me.lblDividerView = New System.Windows.Forms.Label
    '    Me.pnlSetup.SuspendLayout()
    '    Me.pnlCommandSetup.SuspendLayout()
    '    Me.pnlView.SuspendLayout()
    '    Me.pnlCommandView.SuspendLayout()
    '    Me.SuspendLayout()
    '    '
    '    'lblDescription
    '    '
    '    Me.lblDescription.Location = New System.Drawing.Point(72, 92)
    '    Me.lblDescription.Name = "lblDescription"
    '    Me.lblDescription.Size = New System.Drawing.Size(84, 23)
    '    Me.lblDescription.TabIndex = 3
    '    Me.lblDescription.Text = "Description :"
    '    Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '    '
    '    'txtDescription
    '    '
    '    Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.txtDescription.Location = New System.Drawing.Point(160, 94)
    '    Me.txtDescription.Name = "txtDescription"
    '    Me.txtDescription.Size = New System.Drawing.Size(194, 21)
    '    Me.txtDescription.TabIndex = 1
    '    '
    '    'pnlSetup
    '    '
    '    Me.pnlSetup.BackColor = System.Drawing.Color.Transparent
    '    Me.pnlSetup.Controls.Add(Me.lblDividerSetup)
    '    Me.pnlSetup.Controls.Add(Me.pnlCommandSetup)
    '    Me.pnlSetup.Controls.Add(Me.lblDescription)
    '    Me.pnlSetup.Controls.Add(Me.txtDescription)
    '    Me.pnlSetup.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.pnlSetup.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlSetup.Name = "pnlSetup"
    '    Me.pnlSetup.Size = New System.Drawing.Size(424, 234)
    '    Me.pnlSetup.TabIndex = 5
    '    '
    '    'lblDividerSetup
    '    '
    '    Me.lblDividerSetup.BackColor = System.Drawing.Color.CornflowerBlue
    '    Me.lblDividerSetup.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.lblDividerSetup.Location = New System.Drawing.Point(0, 203)
    '    Me.lblDividerSetup.Name = "lblDividerSetup"
    '    Me.lblDividerSetup.Size = New System.Drawing.Size(424, 1)
    '    Me.lblDividerSetup.TabIndex = 4
    '    '
    '    'pnlCommandSetup
    '    '
    '    Me.pnlCommandSetup.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    '    Me.pnlCommandSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.pnlCommandSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.pnlCommandSetup.Controls.Add(Me.btnCancel)
    '    Me.pnlCommandSetup.Controls.Add(Me.btnOK)
    '    Me.pnlCommandSetup.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.pnlCommandSetup.Location = New System.Drawing.Point(0, 204)
    '    Me.pnlCommandSetup.Name = "pnlCommandSetup"
    '    Me.pnlCommandSetup.Size = New System.Drawing.Size(424, 30)
    '    Me.pnlCommandSetup.TabIndex = 2
    '    '
    '    'btnCancel
    '    '
    '    Me.btnCancel.BackColor = System.Drawing.Color.Transparent
    '    Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnCancel.Location = New System.Drawing.Point(272, 0)
    '    Me.btnCancel.Name = "btnCancel"
    '    Me.btnCancel.Size = New System.Drawing.Size(75, 28)
    '    Me.btnCancel.TabIndex = 6
    '    Me.btnCancel.Text = "&Cancel"
    '    Me.btnCancel.UseVisualStyleBackColor = False
    '    '
    '    'btnOK
    '    '
    '    Me.btnOK.BackColor = System.Drawing.Color.Transparent
    '    Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnOK.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnOK.Location = New System.Drawing.Point(347, 0)
    '    Me.btnOK.Name = "btnOK"
    '    Me.btnOK.Size = New System.Drawing.Size(75, 28)
    '    Me.btnOK.TabIndex = 5
    '    Me.btnOK.Text = "&OK"
    '    Me.btnOK.UseVisualStyleBackColor = False
    '    '
    '    'pnlView
    '    '
    '    Me.pnlView.BackColor = System.Drawing.Color.Transparent
    '    Me.pnlView.Controls.Add(Me.pnlCommandView)
    '    Me.pnlView.Controls.Add(Me.lstSpecimens)
    '    Me.pnlView.Controls.Add(Me.lblDividerView)
    '    Me.pnlView.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.pnlView.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlView.Name = "pnlView"
    '    Me.pnlView.Size = New System.Drawing.Size(424, 234)
    '    Me.pnlView.TabIndex = 6
    '    '
    '    'pnlCommandView
    '    '
    '    Me.pnlCommandView.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    '    Me.pnlCommandView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.pnlCommandView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.pnlCommandView.Controls.Add(Me.btnClose)
    '    Me.pnlCommandView.Controls.Add(Me.btnRefresh)
    '    Me.pnlCommandView.Controls.Add(Me.btnDelete)
    '    Me.pnlCommandView.Controls.Add(Me.btnModify)
    '    Me.pnlCommandView.Controls.Add(Me.btnNew)
    '    Me.pnlCommandView.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.pnlCommandView.Location = New System.Drawing.Point(0, 203)
    '    Me.pnlCommandView.Name = "pnlCommandView"
    '    Me.pnlCommandView.Size = New System.Drawing.Size(424, 30)
    '    Me.pnlCommandView.TabIndex = 1
    '    '
    '    'btnClose
    '    '
    '    Me.btnClose.BackColor = System.Drawing.Color.Transparent
    '    Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnClose.Location = New System.Drawing.Point(338, 1)
    '    Me.btnClose.Name = "btnClose"
    '    Me.btnClose.Size = New System.Drawing.Size(75, 25)
    '    Me.btnClose.TabIndex = 8
    '    Me.btnClose.Text = "&Close"
    '    Me.btnClose.UseVisualStyleBackColor = False
    '    '
    '    'btnRefresh
    '    '
    '    Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
    '    Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnRefresh.Location = New System.Drawing.Point(254, 1)
    '    Me.btnRefresh.Name = "btnRefresh"
    '    Me.btnRefresh.Size = New System.Drawing.Size(75, 25)
    '    Me.btnRefresh.TabIndex = 7
    '    Me.btnRefresh.Text = "&Refresh"
    '    Me.btnRefresh.UseVisualStyleBackColor = False
    '    '
    '    'btnDelete
    '    '
    '    Me.btnDelete.BackColor = System.Drawing.Color.Transparent
    '    Me.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnDelete.Location = New System.Drawing.Point(172, 1)
    '    Me.btnDelete.Name = "btnDelete"
    '    Me.btnDelete.Size = New System.Drawing.Size(75, 25)
    '    Me.btnDelete.TabIndex = 6
    '    Me.btnDelete.Text = "&Delete"
    '    Me.btnDelete.UseVisualStyleBackColor = False
    '    '
    '    'btnModify
    '    '
    '    Me.btnModify.BackColor = System.Drawing.Color.Transparent
    '    Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnModify.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnModify.Location = New System.Drawing.Point(88, 1)
    '    Me.btnModify.Name = "btnModify"
    '    Me.btnModify.Size = New System.Drawing.Size(75, 25)
    '    Me.btnModify.TabIndex = 5
    '    Me.btnModify.Text = "&Modify"
    '    Me.btnModify.UseVisualStyleBackColor = False
    '    '
    '    'btnNew
    '    '
    '    Me.btnNew.BackColor = System.Drawing.Color.Transparent
    '    Me.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.btnNew.Location = New System.Drawing.Point(6, 1)
    '    Me.btnNew.Name = "btnNew"
    '    Me.btnNew.Size = New System.Drawing.Size(75, 25)
    '    Me.btnNew.TabIndex = 4
    '    Me.btnNew.Text = "&New"
    '    Me.btnNew.UseVisualStyleBackColor = False
    '    '
    '    'lstSpecimens
    '    '
    '    Me.lstSpecimens.BackColor = System.Drawing.Color.GhostWhite
    '    Me.lstSpecimens.BorderStyle = System.Windows.Forms.BorderStyle.None
    '    Me.lstSpecimens.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.lstSpecimens.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.lstSpecimens.ItemHeight = 14
    '    Me.lstSpecimens.Location = New System.Drawing.Point(0, 0)
    '    Me.lstSpecimens.Name = "lstSpecimens"
    '    Me.lstSpecimens.Size = New System.Drawing.Size(424, 224)
    '    Me.lstSpecimens.TabIndex = 4
    '    '
    '    'lblDividerView
    '    '
    '    Me.lblDividerView.BackColor = System.Drawing.Color.CornflowerBlue
    '    Me.lblDividerView.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.lblDividerView.Location = New System.Drawing.Point(0, 233)
    '    Me.lblDividerView.Name = "lblDividerView"
    '    Me.lblDividerView.Size = New System.Drawing.Size(424, 1)
    '    Me.lblDividerView.TabIndex = 3
    '    '
    '    'frm_LM_SpecimenSetup
    '    '
    '    Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
    '    Me.BackgroundImage = Global.gloEMR.My.Resources.Resources.control
    '    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.ClientSize = New System.Drawing.Size(424, 234)
    '    Me.Controls.Add(Me.pnlSetup)
    '    Me.Controls.Add(Me.pnlView)
    '    Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    '    Me.MaximizeBox = False
    '    Me.MinimizeBox = False
    '    Me.Name = "frm_LM_SpecimenSetup"
    '    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    '    Me.Text = "Specimen"
    '    Me.pnlSetup.ResumeLayout(False)
    '    Me.pnlSetup.PerformLayout()
    '    Me.pnlCommandSetup.ResumeLayout(False)
    '    Me.pnlView.ResumeLayout(False)
    '    Me.pnlCommandView.ResumeLayout(False)
    '    Me.ResumeLayout(False)

    'End Sub
End Class
