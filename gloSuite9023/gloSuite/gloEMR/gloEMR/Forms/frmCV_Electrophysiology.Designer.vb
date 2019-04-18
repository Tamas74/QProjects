<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCV_Electrophysiology
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DTPicker1}
                Dim cntControls() As System.Windows.Forms.Control = {DTPicker1}
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {ConMenuDeleteNode}

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


 

                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                    End If
                End If



                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                    End If
                End If


            End If
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCV_Electrophysiology))
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstripCVElectroPhysiology = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnltrvrht = New System.Windows.Forms.Panel
        Me.pnl_trvAssociates = New System.Windows.Forms.Panel
        Me.trvAssociates = New System.Windows.Forms.TreeView
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.label21 = New System.Windows.Forms.Label
        Me.label22 = New System.Windows.Forms.Label
        Me.label23 = New System.Windows.Forms.Label
        Me.label24 = New System.Windows.Forms.Label
        Me.pnl_btnProcedure = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnProcedure = New System.Windows.Forms.Button
        Me.pnl_btnProvider = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnProvider = New System.Windows.Forms.Button
        Me.pnl_btnUser = New System.Windows.Forms.Panel
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.btnUser = New System.Windows.Forms.Button
        Me.pnl_btnCPT = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnCPT = New System.Windows.Forms.Button
        Me.pnl_txtsearchrht = New System.Windows.Forms.Panel
        Me.btnsearchrhtClear = New System.Windows.Forms.Button
        Me.txtsearchrht = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnl_trvProcedureDate = New System.Windows.Forms.Panel
        Me.trvProcedureDate = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.pnl_DTPicker = New System.Windows.Forms.Panel
        Me.pnl_DTPicker1 = New System.Windows.Forms.Panel
        Me.DTPicker1 = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnl_Main = New System.Windows.Forms.Panel
        Me.ConMenuDeleteNode = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRemoveCPT = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRemoveProcedure = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRemoveUser = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripCVElectroPhysiology.SuspendLayout()
        Me.pnltrvrht.SuspendLayout()
        Me.pnl_trvAssociates.SuspendLayout()
        Me.pnl_btnProcedure.SuspendLayout()
        Me.pnl_btnProvider.SuspendLayout()
        Me.pnl_btnUser.SuspendLayout()
        Me.pnl_btnCPT.SuspendLayout()
        Me.pnl_txtsearchrht.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_trvProcedureDate.SuspendLayout()
        Me.pnl_DTPicker.SuspendLayout()
        Me.pnl_DTPicker1.SuspendLayout()
        Me.pnl_Main.SuspendLayout()
        Me.ConMenuDeleteNode.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlsp_Top.Controls.Add(Me.tstripCVElectroPhysiology)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(876, 56)
        Me.pnl_tlsp_Top.TabIndex = 17
        '
        'tstripCVElectroPhysiology
        '
        Me.tstripCVElectroPhysiology.BackColor = System.Drawing.Color.Transparent
        Me.tstripCVElectroPhysiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tstripCVElectroPhysiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstripCVElectroPhysiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstripCVElectroPhysiology.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstripCVElectroPhysiology.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstripCVElectroPhysiology.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnSave, Me.tlsbtnClose})
        Me.tstripCVElectroPhysiology.Location = New System.Drawing.Point(0, 0)
        Me.tstripCVElectroPhysiology.Name = "tstripCVElectroPhysiology"
        Me.tstripCVElectroPhysiology.Size = New System.Drawing.Size(876, 53)
        Me.tstripCVElectroPhysiology.TabIndex = 0
        Me.tstripCVElectroPhysiology.Text = "ToolStrip1"
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
        'pnltrvrht
        '
        Me.pnltrvrht.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvrht.Controls.Add(Me.pnl_trvAssociates)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnProcedure)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnProvider)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnUser)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnCPT)
        Me.pnltrvrht.Controls.Add(Me.pnl_txtsearchrht)
        Me.pnltrvrht.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnltrvrht.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvrht.Location = New System.Drawing.Point(656, 0)
        Me.pnltrvrht.Margin = New System.Windows.Forms.Padding(0)
        Me.pnltrvrht.Name = "pnltrvrht"
        Me.pnltrvrht.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrvrht.Size = New System.Drawing.Size(220, 600)
        Me.pnltrvrht.TabIndex = 18
        '
        'pnl_trvAssociates
        '
        Me.pnl_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.pnl_trvAssociates.Controls.Add(Me.trvAssociates)
        Me.pnl_trvAssociates.Controls.Add(Me.Label34)
        Me.pnl_trvAssociates.Controls.Add(Me.Label32)
        Me.pnl_trvAssociates.Controls.Add(Me.label21)
        Me.pnl_trvAssociates.Controls.Add(Me.label22)
        Me.pnl_trvAssociates.Controls.Add(Me.label23)
        Me.pnl_trvAssociates.Controls.Add(Me.label24)
        Me.pnl_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_trvAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_trvAssociates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_trvAssociates.Location = New System.Drawing.Point(0, 57)
        Me.pnl_trvAssociates.Name = "pnl_trvAssociates"
        Me.pnl_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_trvAssociates.Size = New System.Drawing.Size(220, 459)
        Me.pnl_trvAssociates.TabIndex = 17
        '
        'trvAssociates
        '
        Me.trvAssociates.BackColor = System.Drawing.Color.White
        Me.trvAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociates.ForeColor = System.Drawing.Color.Black
        Me.trvAssociates.HideSelection = False
        Me.trvAssociates.Indent = 19
        Me.trvAssociates.ItemHeight = 18
        Me.trvAssociates.Location = New System.Drawing.Point(5, 5)
        Me.trvAssociates.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trvAssociates.Name = "trvAssociates"
        Me.trvAssociates.ShowRootLines = False
        Me.trvAssociates.Size = New System.Drawing.Size(211, 450)
        Me.trvAssociates.TabIndex = 4
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.White
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Location = New System.Drawing.Point(1, 5)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(4, 450)
        Me.Label34.TabIndex = 40
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.White
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Location = New System.Drawing.Point(1, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(215, 4)
        Me.Label32.TabIndex = 38
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label21.Location = New System.Drawing.Point(1, 455)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(215, 1)
        Me.label21.TabIndex = 4
        Me.label21.Text = "label2"
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.Location = New System.Drawing.Point(0, 1)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(1, 455)
        Me.label22.TabIndex = 3
        Me.label22.Text = "label4"
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label23.Location = New System.Drawing.Point(216, 1)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 455)
        Me.label23.TabIndex = 2
        Me.label23.Text = "label3"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label24.Location = New System.Drawing.Point(0, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(217, 1)
        Me.label24.TabIndex = 0
        Me.label24.Text = "label1"
        '
        'pnl_btnProcedure
        '
        Me.pnl_btnProcedure.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnProcedure.Controls.Add(Me.Label7)
        Me.pnl_btnProcedure.Controls.Add(Me.Label8)
        Me.pnl_btnProcedure.Controls.Add(Me.Label9)
        Me.pnl_btnProcedure.Controls.Add(Me.Label10)
        Me.pnl_btnProcedure.Controls.Add(Me.btnProcedure)
        Me.pnl_btnProcedure.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnProcedure.Location = New System.Drawing.Point(0, 516)
        Me.pnl_btnProcedure.Name = "pnl_btnProcedure"
        Me.pnl_btnProcedure.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnProcedure.Size = New System.Drawing.Size(220, 28)
        Me.pnl_btnProcedure.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(215, 1)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 24)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(216, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 24)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(217, 1)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "label1"
        '
        'btnProcedure
        '
        Me.btnProcedure.BackColor = System.Drawing.Color.Transparent
        Me.btnProcedure.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnProcedure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProcedure.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProcedure.FlatAppearance.BorderSize = 0
        Me.btnProcedure.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcedure.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProcedure.Location = New System.Drawing.Point(0, 0)
        Me.btnProcedure.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnProcedure.Name = "btnProcedure"
        Me.btnProcedure.Size = New System.Drawing.Size(217, 25)
        Me.btnProcedure.TabIndex = 5
        Me.btnProcedure.Tag = "UnSelected"
        Me.btnProcedure.Text = "Procedure"
        Me.btnProcedure.UseVisualStyleBackColor = False
        '
        'pnl_btnProvider
        '
        Me.pnl_btnProvider.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnProvider.Controls.Add(Me.Label1)
        Me.pnl_btnProvider.Controls.Add(Me.Label2)
        Me.pnl_btnProvider.Controls.Add(Me.Label5)
        Me.pnl_btnProvider.Controls.Add(Me.Label6)
        Me.pnl_btnProvider.Controls.Add(Me.btnProvider)
        Me.pnl_btnProvider.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnProvider.Location = New System.Drawing.Point(0, 544)
        Me.pnl_btnProvider.Name = "pnl_btnProvider"
        Me.pnl_btnProvider.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnProvider.Size = New System.Drawing.Size(220, 28)
        Me.pnl_btnProvider.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(215, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 24)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(216, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 24)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(217, 1)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "label1"
        '
        'btnProvider
        '
        Me.btnProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProvider.FlatAppearance.BorderSize = 0
        Me.btnProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProvider.Location = New System.Drawing.Point(0, 0)
        Me.btnProvider.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnProvider.Name = "btnProvider"
        Me.btnProvider.Size = New System.Drawing.Size(217, 25)
        Me.btnProvider.TabIndex = 6
        Me.btnProvider.Tag = "UnSelected"
        Me.btnProvider.Text = "Provider"
        Me.btnProvider.UseVisualStyleBackColor = False
        '
        'pnl_btnUser
        '
        Me.pnl_btnUser.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnUser.Controls.Add(Me.Label19)
        Me.pnl_btnUser.Controls.Add(Me.Label20)
        Me.pnl_btnUser.Controls.Add(Me.Label25)
        Me.pnl_btnUser.Controls.Add(Me.Label26)
        Me.pnl_btnUser.Controls.Add(Me.btnUser)
        Me.pnl_btnUser.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnUser.Location = New System.Drawing.Point(0, 572)
        Me.pnl_btnUser.Name = "pnl_btnUser"
        Me.pnl_btnUser.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnUser.Size = New System.Drawing.Size(220, 28)
        Me.pnl_btnUser.TabIndex = 18
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(1, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(215, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 24)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(216, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 24)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(217, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'btnUser
        '
        Me.btnUser.BackColor = System.Drawing.Color.Transparent
        Me.btnUser.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnUser.FlatAppearance.BorderSize = 0
        Me.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUser.Location = New System.Drawing.Point(0, 0)
        Me.btnUser.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnUser.Name = "btnUser"
        Me.btnUser.Size = New System.Drawing.Size(217, 25)
        Me.btnUser.TabIndex = 7
        Me.btnUser.Tag = "UnSelected"
        Me.btnUser.Text = "Users"
        Me.btnUser.UseVisualStyleBackColor = False
        '
        'pnl_btnCPT
        '
        Me.pnl_btnCPT.Controls.Add(Me.Label15)
        Me.pnl_btnCPT.Controls.Add(Me.Label16)
        Me.pnl_btnCPT.Controls.Add(Me.Label17)
        Me.pnl_btnCPT.Controls.Add(Me.Label18)
        Me.pnl_btnCPT.Controls.Add(Me.btnCPT)
        Me.pnl_btnCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnCPT.Location = New System.Drawing.Point(0, 29)
        Me.pnl_btnCPT.Name = "pnl_btnCPT"
        Me.pnl_btnCPT.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnCPT.Size = New System.Drawing.Size(220, 28)
        Me.pnl_btnCPT.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(215, 1)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(216, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 24)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(217, 1)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label1"
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCPT.FlatAppearance.BorderSize = 0
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.Location = New System.Drawing.Point(0, 0)
        Me.btnCPT.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(217, 25)
        Me.btnCPT.TabIndex = 3
        Me.btnCPT.Tag = "Selected"
        Me.btnCPT.Text = "&CPT"
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'pnl_txtsearchrht
        '
        Me.pnl_txtsearchrht.BackColor = System.Drawing.Color.Transparent
        Me.pnl_txtsearchrht.Controls.Add(Me.btnsearchrhtClear)
        Me.pnl_txtsearchrht.Controls.Add(Me.txtsearchrht)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label3)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label4)
        Me.pnl_txtsearchrht.Controls.Add(Me.PictureBox1)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label11)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label12)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label13)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label14)
        Me.pnl_txtsearchrht.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_txtsearchrht.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_txtsearchrht.ForeColor = System.Drawing.Color.Black
        Me.pnl_txtsearchrht.Location = New System.Drawing.Point(0, 3)
        Me.pnl_txtsearchrht.Name = "pnl_txtsearchrht"
        Me.pnl_txtsearchrht.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_txtsearchrht.Size = New System.Drawing.Size(220, 26)
        Me.pnl_txtsearchrht.TabIndex = 16
        '
        'btnsearchrhtClear
        '
        Me.btnsearchrhtClear.BackColor = System.Drawing.Color.Transparent
        Me.btnsearchrhtClear.BackgroundImage = CType(resources.GetObject("btnsearchrhtClear.BackgroundImage"), System.Drawing.Image)
        Me.btnsearchrhtClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsearchrhtClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnsearchrhtClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnsearchrhtClear.FlatAppearance.BorderSize = 0
        Me.btnsearchrhtClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnsearchrhtClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnsearchrhtClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsearchrhtClear.Image = CType(resources.GetObject("btnsearchrhtClear.Image"), System.Drawing.Image)
        Me.btnsearchrhtClear.Location = New System.Drawing.Point(195, 5)
        Me.btnsearchrhtClear.Name = "btnsearchrhtClear"
        Me.btnsearchrhtClear.Size = New System.Drawing.Size(21, 15)
        Me.btnsearchrhtClear.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.btnsearchrhtClear, "Clear search")
        Me.btnsearchrhtClear.UseVisualStyleBackColor = False
        '
        'txtsearchrht
        '
        Me.txtsearchrht.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchrht.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchrht.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchrht.ForeColor = System.Drawing.Color.Black
        Me.txtsearchrht.Location = New System.Drawing.Point(29, 5)
        Me.txtsearchrht.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtsearchrht.Name = "txtsearchrht"
        Me.txtsearchrht.Size = New System.Drawing.Size(187, 15)
        Me.txtsearchrht.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(29, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(187, 4)
        Me.Label3.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(29, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(187, 2)
        Me.Label4.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(1, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(215, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(215, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(216, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "label4"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(652, 0)
        Me.Splitter2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(4, 600)
        Me.Splitter2.TabIndex = 19
        Me.Splitter2.TabStop = False
        '
        'pnl_trvProcedureDate
        '
        Me.pnl_trvProcedureDate.Controls.Add(Me.trvProcedureDate)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label33)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label31)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label27)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label28)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label29)
        Me.pnl_trvProcedureDate.Controls.Add(Me.Label30)
        Me.pnl_trvProcedureDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_trvProcedureDate.Location = New System.Drawing.Point(0, 30)
        Me.pnl_trvProcedureDate.Name = "pnl_trvProcedureDate"
        Me.pnl_trvProcedureDate.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_trvProcedureDate.Size = New System.Drawing.Size(652, 570)
        Me.pnl_trvProcedureDate.TabIndex = 20
        '
        'trvProcedureDate
        '
        Me.trvProcedureDate.BackColor = System.Drawing.Color.White
        Me.trvProcedureDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvProcedureDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvProcedureDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvProcedureDate.ForeColor = System.Drawing.Color.Black
        Me.trvProcedureDate.HideSelection = False
        Me.trvProcedureDate.ImageIndex = 0
        Me.trvProcedureDate.ImageList = Me.ImageList1
        Me.trvProcedureDate.Indent = 19
        Me.trvProcedureDate.ItemHeight = 20
        Me.trvProcedureDate.Location = New System.Drawing.Point(8, 6)
        Me.trvProcedureDate.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trvProcedureDate.Name = "trvProcedureDate"
        Me.trvProcedureDate.SelectedImageIndex = 0
        Me.trvProcedureDate.Size = New System.Drawing.Size(643, 560)
        Me.trvProcedureDate.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(1, "Pat control customization.ico")
        Me.ImageList1.Images.SetKeyName(2, "Due Over Guideline.ico")
        Me.ImageList1.Images.SetKeyName(3, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(4, "Procedure Date.ico")
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Location = New System.Drawing.Point(4, 6)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(4, 560)
        Me.Label33.TabIndex = 39
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Location = New System.Drawing.Point(4, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(647, 5)
        Me.Label31.TabIndex = 38
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(4, 566)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(647, 1)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(3, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 566)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(651, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 566)
        Me.Label29.TabIndex = 6
        Me.Label29.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(649, 1)
        Me.Label30.TabIndex = 5
        Me.Label30.Text = "label1"
        '
        'pnl_DTPicker
        '
        Me.pnl_DTPicker.Controls.Add(Me.pnl_DTPicker1)
        Me.pnl_DTPicker.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_DTPicker.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_DTPicker.Location = New System.Drawing.Point(0, 0)
        Me.pnl_DTPicker.Name = "pnl_DTPicker"
        Me.pnl_DTPicker.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnl_DTPicker.Size = New System.Drawing.Size(652, 30)
        Me.pnl_DTPicker.TabIndex = 21
        '
        'pnl_DTPicker1
        '
        Me.pnl_DTPicker1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_DTPicker1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnl_DTPicker1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_DTPicker1.Controls.Add(Me.DTPicker1)
        Me.pnl_DTPicker1.Controls.Add(Me.lblDate)
        Me.pnl_DTPicker1.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_DTPicker1.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_DTPicker1.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_DTPicker1.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_DTPicker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_DTPicker1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_DTPicker1.Location = New System.Drawing.Point(3, 3)
        Me.pnl_DTPicker1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_DTPicker1.Name = "pnl_DTPicker1"
        Me.pnl_DTPicker1.Size = New System.Drawing.Size(649, 24)
        Me.pnl_DTPicker1.TabIndex = 4
        '
        'DTPicker1
        '
        Me.DTPicker1.CalendarForeColor = System.Drawing.Color.Maroon
        Me.DTPicker1.CalendarMonthBackground = System.Drawing.Color.White
        Me.DTPicker1.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.DTPicker1.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.DTPicker1.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.DTPicker1.CustomFormat = "MM/dd/yyyy"
        Me.DTPicker1.Dock = System.Windows.Forms.DockStyle.Left
        Me.DTPicker1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPicker1.Location = New System.Drawing.Point(144, 1)
        Me.DTPicker1.Name = "DTPicker1"
        Me.DTPicker1.Size = New System.Drawing.Size(104, 22)
        Me.DTPicker1.TabIndex = 1
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(1, 1)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(143, 22)
        Me.lblDate.TabIndex = 10
        Me.lblDate.Text = "  Date of Procedure :"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(647, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(648, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(649, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnl_Main
        '
        Me.pnl_Main.Controls.Add(Me.pnl_trvProcedureDate)
        Me.pnl_Main.Controls.Add(Me.pnl_DTPicker)
        Me.pnl_Main.Controls.Add(Me.Splitter2)
        Me.pnl_Main.Controls.Add(Me.pnltrvrht)
        Me.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Main.Location = New System.Drawing.Point(0, 56)
        Me.pnl_Main.Name = "pnl_Main"
        Me.pnl_Main.Size = New System.Drawing.Size(876, 600)
        Me.pnl_Main.TabIndex = 40
        '
        'ConMenuDeleteNode
        '
        Me.ConMenuDeleteNode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRemoveCPT, Me.mnuRemoveProcedure, Me.mnuRemoveUser})
        Me.ConMenuDeleteNode.Name = "ContextMenuStrip1"
        Me.ConMenuDeleteNode.Size = New System.Drawing.Size(177, 70)
        '
        'mnuRemoveCPT
        '
        Me.mnuRemoveCPT.Name = "mnuRemoveCPT"
        Me.mnuRemoveCPT.Size = New System.Drawing.Size(176, 22)
        Me.mnuRemoveCPT.Text = "Remove CPT"
        '
        'mnuRemoveProcedure
        '
        Me.mnuRemoveProcedure.Name = "mnuRemoveProcedure"
        Me.mnuRemoveProcedure.Size = New System.Drawing.Size(176, 22)
        Me.mnuRemoveProcedure.Text = "Remove Procedure"
        '
        'mnuRemoveUser
        '
        Me.mnuRemoveUser.Name = "mnuRemoveUser"
        Me.mnuRemoveUser.Size = New System.Drawing.Size(176, 22)
        Me.mnuRemoveUser.Text = "Remove User"
        '
        'frmCV_Electrophysiology
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(876, 656)
        Me.Controls.Add(Me.pnl_Main)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCV_Electrophysiology"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cardiovascular Electrophysiology Details"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripCVElectroPhysiology.ResumeLayout(False)
        Me.tstripCVElectroPhysiology.PerformLayout()
        Me.pnltrvrht.ResumeLayout(False)
        Me.pnl_trvAssociates.ResumeLayout(False)
        Me.pnl_btnProcedure.ResumeLayout(False)
        Me.pnl_btnProvider.ResumeLayout(False)
        Me.pnl_btnUser.ResumeLayout(False)
        Me.pnl_btnCPT.ResumeLayout(False)
        Me.pnl_txtsearchrht.ResumeLayout(False)
        Me.pnl_txtsearchrht.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_trvProcedureDate.ResumeLayout(False)
        Me.pnl_DTPicker.ResumeLayout(False)
        Me.pnl_DTPicker1.ResumeLayout(False)
        Me.pnl_Main.ResumeLayout(False)
        Me.ConMenuDeleteNode.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstripCVElectroPhysiology As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnltrvrht As System.Windows.Forms.Panel
    Friend WithEvents pnl_btnProcedure As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnProcedure As System.Windows.Forms.Button
    Friend WithEvents pnl_btnProvider As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnProvider As System.Windows.Forms.Button
    Private WithEvents pnl_trvAssociates As System.Windows.Forms.Panel
    Friend WithEvents trvAssociates As System.Windows.Forms.TreeView
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnUser As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnUser As System.Windows.Forms.Button
    Friend WithEvents pnl_txtsearchrht As System.Windows.Forms.Panel
    Friend WithEvents txtsearchrht As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnCPT As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnl_trvProcedureDate As System.Windows.Forms.Panel
    Friend WithEvents trvProcedureDate As System.Windows.Forms.TreeView
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnl_DTPicker As System.Windows.Forms.Panel
    Friend WithEvents pnl_DTPicker1 As System.Windows.Forms.Panel
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents pnl_Main As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ConMenuDeleteNode As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRemoveCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveProcedure As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveUser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DTPicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnsearchrhtClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
