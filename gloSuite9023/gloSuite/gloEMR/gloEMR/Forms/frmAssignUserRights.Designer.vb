<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssignUserRights
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssignUserRights))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.trvAvailableUsers = New System.Windows.Forms.TreeView()
        Me.imgCommon = New System.Windows.Forms.ImageList(Me.components)
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.trvAssociatedUsers = New System.Windows.Forms.TreeView()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblJunior = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.pnltls = New System.Windows.Forms.Panel()
        Me.tlsProviderAssociation = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtn_OK = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnltls.SuspendLayout()
        Me.tlsProviderAssociation.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnRemove)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(601, 419)
        Me.Panel1.TabIndex = 21
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.trvAvailableUsers)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(268, 413)
        Me.Panel3.TabIndex = 20
        '
        'trvAvailableUsers
        '
        Me.trvAvailableUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAvailableUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAvailableUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAvailableUsers.ForeColor = System.Drawing.Color.Black
        Me.trvAvailableUsers.HideSelection = False
        Me.trvAvailableUsers.ImageIndex = 0
        Me.trvAvailableUsers.ImageList = Me.imgCommon
        Me.trvAvailableUsers.Indent = 20
        Me.trvAvailableUsers.ItemHeight = 20
        Me.trvAvailableUsers.Location = New System.Drawing.Point(3, 27)
        Me.trvAvailableUsers.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.trvAvailableUsers.Name = "trvAvailableUsers"
        Me.trvAvailableUsers.SelectedImageIndex = 0
        Me.trvAvailableUsers.ShowLines = False
        Me.trvAvailableUsers.ShowNodeToolTips = True
        Me.trvAvailableUsers.ShowRootLines = False
        Me.trvAvailableUsers.Size = New System.Drawing.Size(264, 385)
        Me.trvAvailableUsers.TabIndex = 2
        '
        'imgCommon
        '
        Me.imgCommon.ImageStream = CType(resources.GetObject("imgCommon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgCommon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgCommon.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgCommon.Images.SetKeyName(1, "Small Arrow.ico")
        Me.imgCommon.Images.SetKeyName(2, "arrow_01.ico")
        Me.imgCommon.Images.SetKeyName(3, "Ornage Bullet.ico")
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 25)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(264, 2)
        Me.Label18.TabIndex = 28
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.White
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 25)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(2, 387)
        Me.Label19.TabIndex = 27
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.ForeColor = System.Drawing.Color.White
        Me.Panel5.Location = New System.Drawing.Point(1, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(266, 24)
        Me.Panel5.TabIndex = 21
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(266, 1)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "label1"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(266, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Available Users"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 412)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(266, 1)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 412)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(267, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 412)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(268, 1)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.trvAssociatedUsers)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(317, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(281, 413)
        Me.Panel2.TabIndex = 20
        '
        'trvAssociatedUsers
        '
        Me.trvAssociatedUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociatedUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssociatedUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociatedUsers.ForeColor = System.Drawing.Color.Black
        Me.trvAssociatedUsers.HideSelection = False
        Me.trvAssociatedUsers.ImageIndex = 0
        Me.trvAssociatedUsers.ImageList = Me.imgCommon
        Me.trvAssociatedUsers.Indent = 20
        Me.trvAssociatedUsers.ItemHeight = 20
        Me.trvAssociatedUsers.Location = New System.Drawing.Point(3, 27)
        Me.trvAssociatedUsers.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.trvAssociatedUsers.Name = "trvAssociatedUsers"
        Me.trvAssociatedUsers.SelectedImageIndex = 0
        Me.trvAssociatedUsers.ShowLines = False
        Me.trvAssociatedUsers.ShowNodeToolTips = True
        Me.trvAssociatedUsers.Size = New System.Drawing.Size(277, 385)
        Me.trvAssociatedUsers.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 25)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(277, 2)
        Me.Label17.TabIndex = 26
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 25)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(2, 387)
        Me.Label16.TabIndex = 25
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.lblJunior)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.ForeColor = System.Drawing.Color.White
        Me.Panel4.Location = New System.Drawing.Point(1, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(279, 24)
        Me.Panel4.TabIndex = 20
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(279, 1)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "label1"
        '
        'lblJunior
        '
        Me.lblJunior.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblJunior.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJunior.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblJunior.Location = New System.Drawing.Point(0, 0)
        Me.lblJunior.Name = "lblJunior"
        Me.lblJunior.Size = New System.Drawing.Size(279, 24)
        Me.lblJunior.TabIndex = 1
        Me.lblJunior.Text = "  Assigned Users"
        Me.lblJunior.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 412)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(279, 1)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 412)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(280, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 412)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(281, 1)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "label1"
        '
        'btnAdd
        '
        Me.btnAdd.AutoSize = True
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(283, 173)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(32, 27)
        Me.btnAdd.TabIndex = 18
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.AutoSize = True
        Me.btnRemove.BackColor = System.Drawing.Color.Transparent
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRemove.FlatAppearance.BorderSize = 0
        Me.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Location = New System.Drawing.Point(283, 206)
        Me.btnRemove.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(32, 27)
        Me.btnRemove.TabIndex = 19
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'pnltls
        '
        Me.pnltls.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltls.Controls.Add(Me.tlsProviderAssociation)
        Me.pnltls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltls.Location = New System.Drawing.Point(0, 0)
        Me.pnltls.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnltls.Name = "pnltls"
        Me.pnltls.Size = New System.Drawing.Size(601, 54)
        Me.pnltls.TabIndex = 24
        '
        'tlsProviderAssociation
        '
        Me.tlsProviderAssociation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsProviderAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsProviderAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsProviderAssociation.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsProviderAssociation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtn_OK, Me.tlsbtnCancel})
        Me.tlsProviderAssociation.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsProviderAssociation.Location = New System.Drawing.Point(0, 0)
        Me.tlsProviderAssociation.Name = "tlsProviderAssociation"
        Me.tlsProviderAssociation.Size = New System.Drawing.Size(601, 53)
        Me.tlsProviderAssociation.TabIndex = 0
        Me.tlsProviderAssociation.Text = "ToolStrip1"
        '
        'tlsbtn_OK
        '
        Me.tlsbtn_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_OK.Image = CType(resources.GetObject("tlsbtn_OK.Image"), System.Drawing.Image)
        Me.tlsbtn_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_OK.Name = "tlsbtn_OK"
        Me.tlsbtn_OK.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtn_OK.Tag = "OK"
        Me.tlsbtn_OK.Text = "&Save&&Cls"
        Me.tlsbtn_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_OK.ToolTipText = "Save and Close"
        '
        'tlsbtnCancel
        '
        Me.tlsbtnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnCancel.Image = CType(resources.GetObject("tlsbtnCancel.Image"), System.Drawing.Image)
        Me.tlsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCancel.Name = "tlsbtnCancel"
        Me.tlsbtnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnCancel.Tag = "Cancel"
        Me.tlsbtnCancel.Text = "&Close"
        Me.tlsbtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmAssignUserRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(601, 473)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnltls)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssignUserRights"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Assign User Rights"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnltls.ResumeLayout(False)
        Me.pnltls.PerformLayout()
        Me.tlsProviderAssociation.ResumeLayout(False)
        Me.tlsProviderAssociation.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents trvAvailableUsers As System.Windows.Forms.TreeView
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents trvAssociatedUsers As System.Windows.Forms.TreeView
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblJunior As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents pnltls As System.Windows.Forms.Panel
    Friend WithEvents tlsProviderAssociation As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtn_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents imgCommon As System.Windows.Forms.ImageList
End Class
