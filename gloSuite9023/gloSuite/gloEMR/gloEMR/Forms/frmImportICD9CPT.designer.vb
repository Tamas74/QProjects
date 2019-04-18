<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportICD9CPT
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
                Try
                    If (IsNothing(openfileICD9CPT) = False) Then
                        openfileICD9CPT.Dispose()
                        openfileICD9CPT = Nothing
                    End If
                Catch ex As Exception

                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportICD9CPT))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.tbImportICD9CPT = New System.Windows.Forms.TabControl
        Me.tbpageICD9 = New System.Windows.Forms.TabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.chkDeleteICD9 = New System.Windows.Forms.CheckBox
        Me.lblFilePathICD9 = New System.Windows.Forms.Label
        Me.btnbrowsefileICD9 = New System.Windows.Forms.Button
        Me.txtFilePathICD9 = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.tbpageCPT = New System.Windows.Forms.TabPage
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkDeleteCPT = New System.Windows.Forms.CheckBox
        Me.btnbrowsefileCPT = New System.Windows.Forms.Button
        Me.txtFilePathCPT = New System.Windows.Forms.TextBox
        Me.lblCPT = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlbottomICD9 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.prgsBar = New System.Windows.Forms.ProgressBar
        Me.openfileICD9CPT = New System.Windows.Forms.OpenFileDialog
        Me.Button1 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.pnl_tls_ = New System.Windows.Forms.Panel
        Me.tlsImportICD9CPT = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Import = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.tbImportICD9CPT.SuspendLayout()
        Me.tbpageICD9.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.tbpageCPT.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlbottomICD9.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsImportICD9CPT.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tbImportICD9CPT)
        Me.pnlMain.Controls.Add(Me.pnlbottomICD9)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(371, 164)
        Me.pnlMain.TabIndex = 0
        '
        'tbImportICD9CPT
        '
        Me.tbImportICD9CPT.Controls.Add(Me.tbpageICD9)
        Me.tbImportICD9CPT.Controls.Add(Me.tbpageCPT)
        Me.tbImportICD9CPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbImportICD9CPT.Location = New System.Drawing.Point(0, 0)
        Me.tbImportICD9CPT.Name = "tbImportICD9CPT"
        Me.tbImportICD9CPT.Padding = New System.Drawing.Point(0, 0)
        Me.tbImportICD9CPT.SelectedIndex = 0
        Me.tbImportICD9CPT.Size = New System.Drawing.Size(371, 136)
        Me.tbImportICD9CPT.TabIndex = 14
        '
        'tbpageICD9
        '
        Me.tbpageICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpageICD9.Controls.Add(Me.Panel1)
        Me.tbpageICD9.Location = New System.Drawing.Point(4, 23)
        Me.tbpageICD9.Name = "tbpageICD9"
        Me.tbpageICD9.Size = New System.Drawing.Size(363, 109)
        Me.tbpageICD9.TabIndex = 0
        Me.tbpageICD9.Text = "ICD9"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.chkDeleteICD9)
        Me.Panel1.Controls.Add(Me.lblFilePathICD9)
        Me.Panel1.Controls.Add(Me.btnbrowsefileICD9)
        Me.Panel1.Controls.Add(Me.txtFilePathICD9)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(363, 109)
        Me.Panel1.TabIndex = 16
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 105)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(355, 1)
        Me.lbl_pnlBottom.TabIndex = 19
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(4, 27)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(355, 1)
        Me.lbl_pnlTop.TabIndex = 16
        Me.lbl_pnlTop.Text = "label1"
        '
        'chkDeleteICD9
        '
        Me.chkDeleteICD9.AutoSize = True
        Me.chkDeleteICD9.Location = New System.Drawing.Point(88, 74)
        Me.chkDeleteICD9.Name = "chkDeleteICD9"
        Me.chkDeleteICD9.Size = New System.Drawing.Size(141, 18)
        Me.chkDeleteICD9.TabIndex = 15
        Me.chkDeleteICD9.Text = "Delete Previous ICD9"
        Me.chkDeleteICD9.UseVisualStyleBackColor = True
        '
        'lblFilePathICD9
        '
        Me.lblFilePathICD9.AutoSize = True
        Me.lblFilePathICD9.BackColor = System.Drawing.Color.Transparent
        Me.lblFilePathICD9.Location = New System.Drawing.Point(9, 43)
        Me.lblFilePathICD9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilePathICD9.Name = "lblFilePathICD9"
        Me.lblFilePathICD9.Size = New System.Drawing.Size(74, 14)
        Me.lblFilePathICD9.TabIndex = 12
        Me.lblFilePathICD9.Text = "Select File : "
        Me.lblFilePathICD9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnbrowsefileICD9
        '
        Me.btnbrowsefileICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnbrowsefileICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrowsefileICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnbrowsefileICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnbrowsefileICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrowsefileICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrowsefileICD9.Image = CType(resources.GetObject("btnbrowsefileICD9.Image"), System.Drawing.Image)
        Me.btnbrowsefileICD9.Location = New System.Drawing.Point(320, 38)
        Me.btnbrowsefileICD9.Name = "btnbrowsefileICD9"
        Me.btnbrowsefileICD9.Size = New System.Drawing.Size(24, 24)
        Me.btnbrowsefileICD9.TabIndex = 14
        Me.btnbrowsefileICD9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnbrowsefileICD9.UseVisualStyleBackColor = True
        '
        'txtFilePathICD9
        '
        Me.txtFilePathICD9.BackColor = System.Drawing.Color.White
        Me.txtFilePathICD9.Location = New System.Drawing.Point(85, 38)
        Me.txtFilePathICD9.Name = "txtFilePathICD9"
        Me.txtFilePathICD9.ReadOnly = True
        Me.txtFilePathICD9.Size = New System.Drawing.Size(228, 22)
        Me.txtFilePathICD9.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(355, 23)
        Me.Panel4.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(355, 23)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "  Import ICD9"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 102)
        Me.lbl_pnlLeft.TabIndex = 18
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(359, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 102)
        Me.lbl_pnlRight.TabIndex = 17
        Me.lbl_pnlRight.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(357, 1)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "label2"
        '
        'tbpageCPT
        '
        Me.tbpageCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpageCPT.Controls.Add(Me.Panel2)
        Me.tbpageCPT.Location = New System.Drawing.Point(4, 23)
        Me.tbpageCPT.Name = "tbpageCPT"
        Me.tbpageCPT.Size = New System.Drawing.Size(363, 109)
        Me.tbpageCPT.TabIndex = 1
        Me.tbpageCPT.Text = "CPT"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.chkDeleteCPT)
        Me.Panel2.Controls.Add(Me.btnbrowsefileCPT)
        Me.Panel2.Controls.Add(Me.txtFilePathCPT)
        Me.Panel2.Controls.Add(Me.lblCPT)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(363, 109)
        Me.Panel2.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(4, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(355, 1)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(4, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(355, 1)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "label2"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(355, 23)
        Me.Panel3.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(355, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "  Import CPT "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkDeleteCPT
        '
        Me.chkDeleteCPT.AutoSize = True
        Me.chkDeleteCPT.Location = New System.Drawing.Point(88, 74)
        Me.chkDeleteCPT.Name = "chkDeleteCPT"
        Me.chkDeleteCPT.Size = New System.Drawing.Size(137, 18)
        Me.chkDeleteCPT.TabIndex = 18
        Me.chkDeleteCPT.Text = "Delete Previous CPT"
        Me.chkDeleteCPT.UseVisualStyleBackColor = True
        '
        'btnbrowsefileCPT
        '
        Me.btnbrowsefileCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnbrowsefileCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrowsefileCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnbrowsefileCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnbrowsefileCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrowsefileCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrowsefileCPT.Image = CType(resources.GetObject("btnbrowsefileCPT.Image"), System.Drawing.Image)
        Me.btnbrowsefileCPT.Location = New System.Drawing.Point(320, 38)
        Me.btnbrowsefileCPT.Name = "btnbrowsefileCPT"
        Me.btnbrowsefileCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnbrowsefileCPT.TabIndex = 17
        Me.btnbrowsefileCPT.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnbrowsefileCPT.UseVisualStyleBackColor = True
        '
        'txtFilePathCPT
        '
        Me.txtFilePathCPT.BackColor = System.Drawing.Color.White
        Me.txtFilePathCPT.Location = New System.Drawing.Point(85, 38)
        Me.txtFilePathCPT.Name = "txtFilePathCPT"
        Me.txtFilePathCPT.ReadOnly = True
        Me.txtFilePathCPT.Size = New System.Drawing.Size(228, 22)
        Me.txtFilePathCPT.TabIndex = 16
        '
        'lblCPT
        '
        Me.lblCPT.AutoSize = True
        Me.lblCPT.BackColor = System.Drawing.Color.Transparent
        Me.lblCPT.Location = New System.Drawing.Point(9, 43)
        Me.lblCPT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCPT.Name = "lblCPT"
        Me.lblCPT.Size = New System.Drawing.Size(74, 14)
        Me.lblCPT.TabIndex = 15
        Me.lblCPT.Text = "Select File : "
        Me.lblCPT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(4, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(355, 1)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 103)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(359, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 103)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "label4"
        '
        'pnlbottomICD9
        '
        Me.pnlbottomICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlbottomICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbottomICD9.Controls.Add(Me.Label10)
        Me.pnlbottomICD9.Controls.Add(Me.Label11)
        Me.pnlbottomICD9.Controls.Add(Me.Label12)
        Me.pnlbottomICD9.Controls.Add(Me.Label13)
        Me.pnlbottomICD9.Controls.Add(Me.prgsBar)
        Me.pnlbottomICD9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbottomICD9.Location = New System.Drawing.Point(0, 136)
        Me.pnlbottomICD9.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlbottomICD9.Name = "pnlbottomICD9"
        Me.pnlbottomICD9.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlbottomICD9.Size = New System.Drawing.Size(371, 28)
        Me.pnlbottomICD9.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(4, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(363, 1)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 21)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(367, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 21)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(365, 1)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "label1"
        '
        'prgsBar
        '
        Me.prgsBar.BackColor = System.Drawing.Color.Silver
        Me.prgsBar.ForeColor = System.Drawing.Color.GreenYellow
        Me.prgsBar.Location = New System.Drawing.Point(7, 7)
        Me.prgsBar.Maximum = 250
        Me.prgsBar.Name = "prgsBar"
        Me.prgsBar.Size = New System.Drawing.Size(354, 14)
        Me.prgsBar.Step = 1
        Me.prgsBar.TabIndex = 14
        '
        'openfileICD9CPT
        '
        Me.openfileICD9CPT.FileName = "OpenFileDialog1"
        '
        'Button1
        '
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(386, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 22)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "..."
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(109, 20)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(271, 20)
        Me.TextBox1.TabIndex = 13
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.TabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage1.Size = New System.Drawing.Size(438, 86)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ICD9"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(24, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Select File: "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage2
        '
        Me.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TabPage2.Size = New System.Drawing.Size(438, 86)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "CPT"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsImportICD9CPT)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(371, 53)
        Me.pnl_tls_.TabIndex = 7
        '
        'tlsImportICD9CPT
        '
        Me.tlsImportICD9CPT.BackColor = System.Drawing.Color.Transparent
        Me.tlsImportICD9CPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsImportICD9CPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsImportICD9CPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsImportICD9CPT.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsImportICD9CPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Import, Me.btn_tls_Close})
        Me.tlsImportICD9CPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsImportICD9CPT.Location = New System.Drawing.Point(0, 0)
        Me.tlsImportICD9CPT.Name = "tlsImportICD9CPT"
        Me.tlsImportICD9CPT.Size = New System.Drawing.Size(371, 53)
        Me.tlsImportICD9CPT.TabIndex = 0
        Me.tlsImportICD9CPT.Text = "toolStrip1"
        '
        'btn_tls_Import
        '
        Me.btn_tls_Import.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Import.Image = CType(resources.GetObject("btn_tls_Import.Image"), System.Drawing.Image)
        Me.btn_tls_Import.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Import.Name = "btn_tls_Import"
        Me.btn_tls_Import.Size = New System.Drawing.Size(62, 50)
        Me.btn_tls_Import.Tag = "Import"
        Me.btn_tls_Import.Text = "  &Import"
        Me.btn_tls_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Import.ToolTipText = "Import"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmImportICD9CPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(371, 217)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportICD9CPT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import ICD9CPT"
        Me.pnlMain.ResumeLayout(False)
        Me.tbImportICD9CPT.ResumeLayout(False)
        Me.tbpageICD9.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.tbpageCPT.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnlbottomICD9.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsImportICD9CPT.ResumeLayout(False)
        Me.tlsImportICD9CPT.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlbottomICD9 As System.Windows.Forms.Panel
    Friend WithEvents openfileICD9CPT As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnbrowsefileCPT As System.Windows.Forms.Button
    Friend WithEvents txtFilePathCPT As System.Windows.Forms.TextBox
    Friend WithEvents lblCPT As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblFilePathICD9 As System.Windows.Forms.Label
    Friend WithEvents btnbrowsefileICD9 As System.Windows.Forms.Button
    Friend WithEvents txtFilePathICD9 As System.Windows.Forms.TextBox
    Friend WithEvents tbImportICD9CPT As System.Windows.Forms.TabControl
    Friend WithEvents tbpageICD9 As System.Windows.Forms.TabPage
    Friend WithEvents tbpageCPT As System.Windows.Forms.TabPage
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents prgsBar As System.Windows.Forms.ProgressBar
    Friend WithEvents chkDeleteICD9 As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeleteCPT As System.Windows.Forms.CheckBox
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsImportICD9CPT As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Import As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
End Class
