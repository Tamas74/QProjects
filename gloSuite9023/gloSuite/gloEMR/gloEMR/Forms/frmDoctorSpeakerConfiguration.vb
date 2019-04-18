Imports gloAuditTrail

Public Class frmDoctorSpeakerConfiguration
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(dgConfiguration) = False) Then
                    dgConfiguration.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgConfiguration)
                    dgConfiguration.Dispose()
                    dgConfiguration = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMachineName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDoctor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSpeaker As System.Windows.Forms.ComboBox
    Friend WithEvents dgConfiguration As System.Windows.Forms.DataGrid
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsSpeaker As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnRefreshSpeakers As System.Windows.Forms.Button
    Public WithEvents DgnEngineControl1 As AxDNSTools.AxDgnEngineControl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDoctorSpeakerConfiguration))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnRefreshSpeakers = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.cmbSpeaker = New System.Windows.Forms.ComboBox()
        Me.cmbDoctor = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.DgnEngineControl1 = New AxDNSTools.AxDgnEngineControl()
        Me.dgConfiguration = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tlsSpeaker = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        CType(Me.DgnEngineControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgConfiguration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsSpeaker.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.btnRefreshSpeakers)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTop.Controls.Add(Me.cmbSpeaker)
        Me.pnlTop.Controls.Add(Me.cmbDoctor)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.lblMachineName)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 53)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(550, 66)
        Me.pnlTop.TabIndex = 1
        '
        'btnRefreshSpeakers
        '
        Me.btnRefreshSpeakers.Location = New System.Drawing.Point(295, 6)
        Me.btnRefreshSpeakers.Name = "btnRefreshSpeakers"
        Me.btnRefreshSpeakers.Size = New System.Drawing.Size(146, 23)
        Me.btnRefreshSpeakers.TabIndex = 10
        Me.btnRefreshSpeakers.Text = "Refresh Speakers"
        Me.btnRefreshSpeakers.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Machine Name :"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 62)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(542, 1)
        Me.lbl_BottomBrd.TabIndex = 9
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 59)
        Me.lbl_LeftBrd.TabIndex = 8
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(546, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 59)
        Me.lbl_RightBrd.TabIndex = 7
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(544, 1)
        Me.lbl_TopBrd.TabIndex = 6
        Me.lbl_TopBrd.Text = "label1"
        '
        'cmbSpeaker
        '
        Me.cmbSpeaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpeaker.ForeColor = System.Drawing.Color.Black
        Me.cmbSpeaker.Location = New System.Drawing.Point(295, 32)
        Me.cmbSpeaker.Name = "cmbSpeaker"
        Me.cmbSpeaker.Size = New System.Drawing.Size(148, 22)
        Me.cmbSpeaker.TabIndex = 5
        '
        'cmbDoctor
        '
        Me.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoctor.Location = New System.Drawing.Point(69, 32)
        Me.cmbDoctor.Name = "cmbDoctor"
        Me.cmbDoctor.Size = New System.Drawing.Size(156, 22)
        Me.cmbDoctor.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(235, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Speaker :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Doctor :"
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.Location = New System.Drawing.Point(120, 11)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(0, 14)
        Me.lblMachineName.TabIndex = 1
        '
        'DgnEngineControl1
        '
        Me.DgnEngineControl1.Enabled = True
        Me.DgnEngineControl1.Location = New System.Drawing.Point(425, 12)
        Me.DgnEngineControl1.Name = "DgnEngineControl1"
        Me.DgnEngineControl1.OcxState = CType(resources.GetObject("DgnEngineControl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.DgnEngineControl1.Size = New System.Drawing.Size(16, 15)
        Me.DgnEngineControl1.TabIndex = 27
        '
        'dgConfiguration
        '
        Me.dgConfiguration.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgConfiguration.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgConfiguration.BackgroundColor = System.Drawing.Color.White
        Me.dgConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgConfiguration.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgConfiguration.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgConfiguration.CaptionForeColor = System.Drawing.Color.White
        Me.dgConfiguration.CaptionVisible = False
        Me.dgConfiguration.DataMember = ""
        Me.dgConfiguration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgConfiguration.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgConfiguration.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgConfiguration.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgConfiguration.HeaderForeColor = System.Drawing.Color.White
        Me.dgConfiguration.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgConfiguration.Location = New System.Drawing.Point(4, 2)
        Me.dgConfiguration.Name = "dgConfiguration"
        Me.dgConfiguration.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgConfiguration.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgConfiguration.ReadOnly = True
        Me.dgConfiguration.RowHeadersVisible = False
        Me.dgConfiguration.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgConfiguration.SelectionForeColor = System.Drawing.Color.Black
        Me.dgConfiguration.Size = New System.Drawing.Size(536, 197)
        Me.dgConfiguration.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.DgnEngineControl1)
        Me.pnlToolStrip.Controls.Add(Me.tlsSpeaker)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(550, 53)
        Me.pnlToolStrip.TabIndex = 13
        '
        'tlsSpeaker
        '
        Me.tlsSpeaker.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsSpeaker.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsSpeaker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsSpeaker.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsSpeaker.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsSpeaker.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsSpeaker.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnUpdate, Me.ts_btnDelete, Me.ts_btnClose})
        Me.tlsSpeaker.Location = New System.Drawing.Point(0, 0)
        Me.tlsSpeaker.Name = "tlsSpeaker"
        Me.tlsSpeaker.Size = New System.Drawing.Size(550, 53)
        Me.tlsSpeaker.TabIndex = 0
        Me.tlsSpeaker.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnUpdate
        '
        Me.ts_btnUpdate.Image = CType(resources.GetObject("ts_btnUpdate.Image"), System.Drawing.Image)
        Me.ts_btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnUpdate.Name = "ts_btnUpdate"
        Me.ts_btnUpdate.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnUpdate.Tag = "Modify"
        Me.ts_btnUpdate.Text = "&Modify"
        Me.ts_btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
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
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dgConfiguration)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 119)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(550, 225)
        Me.Panel1.TabIndex = 14
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(16, 202)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(506, 14)
        Me.lblStatus.TabIndex = 9
        Me.lblStatus.Text = "Note: If you are not able to see your profile in the Speaker list, click on 'Refr" & _
    "esh Speakers'."
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(4, 221)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(542, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 220)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(546, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 220)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(544, 1)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "label1"
        '
        'frmDoctorSpeakerConfiguration
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(550, 344)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDoctorSpeakerConfiguration"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Doctor Speaker Configuration"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.DgnEngineControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgConfiguration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsSpeaker.ResumeLayout(False)
        Me.tlsSpeaker.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private dtSpeaker As DataTable
    Private strProfileMessage As String = "Note: If you are not able to see your profile in the Speaker list, click on 'Refresh Speakers'."

    Private Sub frmDoctorSpeakerConfiguration_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If IsNothing(dtSpeaker) = False Then
            dtSpeaker.Dispose()
            dtSpeaker = Nothing
        End If

    End Sub

    Private Sub frmDoctorSpeakerConfiguration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Cursor = Cursors.WaitCursor
            lblMachineName.Text = gstrClientMachineName
            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.View, "Load Doctor speaker configuration started", ActivityOutCome.Success)

            Call Fill_Providers()
            Call Fill_Speakers()
            Call Fill_DoctorSpeakerConfiguration()

            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.View, "Load Doctor speaker configuration completed", ActivityOutCome.Success)

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.View, objErr.ToString, ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            lblStatus.Text = strProfileMessage
        End Try

    End Sub

    Private Sub Fill_Providers()

        With cmbDoctor
            .Items.Clear()
            Dim clProviders As New Collection
            Dim objProvider As New clsProvider
            clProviders = objProvider.Fill_Providers
            objProvider.Dispose()
            objProvider = Nothing
            Dim nCount As Int16

            For nCount = 1 To clProviders.Count
                .Items.Add(clProviders.Item(nCount))
            Next

            If .Items.Count >= 1 Then .SelectedIndex = 0

            clProviders = Nothing
            objProvider = Nothing

        End With


    End Sub

    Private Sub Fill_Speakers(Optional RefreshSpeakers As Boolean = False)

        Dim nCount As Integer
        Dim objConfiguration As New clsDoctorSpeakerConfiguration
        Dim intTotalSpeakerCount As Integer

        Try
            lblStatus.Text = "Please Wait..."
            Application.DoEvents()

            If RefreshSpeakers = False Then
                dtSpeaker = objConfiguration.FillSpeakerList(gstrClientMachineName)
            Else
                dtSpeaker.Rows.Clear()
            End If


            If RefreshSpeakers = True OrElse dtSpeaker.Rows.Count = 0 Then

                intTotalSpeakerCount = DgnEngineControl1.Speakers.Count
                With cmbSpeaker
                    '.Items.Clear()

                    If intTotalSpeakerCount >= 1 Then
                        For nCount = 1 To intTotalSpeakerCount
                            lblStatus.Text = "Loading Speaker: " & nCount & " of " & intTotalSpeakerCount
                            Application.DoEvents()
                            '.Items.Add(DgnEngineControl1.Speakers.Item(nCount))
                            dtSpeaker.Rows.Add(dtSpeaker.NewRow)
                            dtSpeaker.Rows(nCount - 1)("SpeakerName") = DgnEngineControl1.Speakers.Item(nCount)
                        Next
                    End If

                    'If .Items.Count >= 1 Then .SelectedIndex = 0
                End With

                objConfiguration.SaveSpeakerList(dtSpeaker, gstrClientMachineName)

            End If

            cmbSpeaker.DisplayMember = "SpeakerName"
            cmbSpeaker.ValueMember = "SpeakerName"
            cmbSpeaker.DataSource = dtSpeaker

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            lblStatus.Text = strProfileMessage
            If IsNothing(objConfiguration) = False Then
                objConfiguration = Nothing
            End If

        End Try

    End Sub

    Private Sub AddSpeaker()

        Try
            If cmbDoctor.SelectedIndex = -1 Then
                MessageBox.Show("Please select the Doctor.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDoctor.Focus()
                Exit Sub
            End If
            If cmbSpeaker.SelectedIndex = -1 Then
                MessageBox.Show("Please select the Speaker.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSpeaker.Focus()
                Exit Sub
            End If
            Dim objConfiguration As New clsDoctorSpeakerConfiguration
            If objConfiguration.CheckConfiguratuionExists(cmbDoctor.Text, cmbSpeaker.Text, Trim(lblMachineName.Text)) = True Then
                MessageBox.Show("This Configuration is already added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDoctor.Focus()
                objConfiguration = Nothing
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            objConfiguration.Doctor = cmbDoctor.Text
            objConfiguration.Speaker = cmbSpeaker.Text
            objConfiguration.MachineName = Trim(lblMachineName.Text)
            objConfiguration.AddDoctorSpeakerConfiguration()

            objConfiguration = Nothing
            Call Fill_DoctorSpeakerConfiguration()
            ts_btnUpdate.Enabled = False
            ts_btnDelete.Enabled = False
            Me.Cursor = Cursors.Default

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.Add, objErr.ToString, ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DeleteSpeaker()
        Try
            ''Sandip Darade  20090701
            ''check that datasource for the grid is not null
            If Not IsNothing(dgConfiguration.DataSource) Then
                If CType(dgConfiguration.DataSource, DataTable).Rows.Count >= 1 Then
                    If MessageBox.Show("Are you sure you want to delete?" & dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 1) & "-" & dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 2) & " Configuration?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Me.Cursor = Cursors.WaitCursor
                        Dim objConfiguration As New clsDoctorSpeakerConfiguration
                        objConfiguration.DeleteDoctorSpeakerConfiguration(dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 0))
                        objConfiguration = Nothing
                        Call Fill_DoctorSpeakerConfiguration()
                        MessageBox.Show("Doctor-Speaker Configuration Deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ts_btnUpdate.Enabled = False
                        ts_btnDelete.Enabled = False
                        Me.Cursor = Cursors.Default
                    End If
                End If
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.Delete, objErr.ToString, ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateSpeaker()
        Try
            If cmbDoctor.SelectedIndex = -1 Then
                MessageBox.Show("Please select the Doctor.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDoctor.Focus()
                Exit Sub
            End If
            If cmbSpeaker.SelectedIndex = -1 Then
                MessageBox.Show("Please select the Speaker.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSpeaker.Focus()
                Exit Sub
            End If


            Dim objConfiguration As New clsDoctorSpeakerConfiguration
            If objConfiguration.CheckConfiguratuionExists(cmbDoctor.Text, cmbSpeaker.Text, Trim(lblMachineName.Text), cmbDoctor.Tag) = True Then
                MessageBox.Show("This Configuration is already added.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDoctor.Focus()
                objConfiguration = Nothing
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            objConfiguration.Doctor = cmbDoctor.Text
            objConfiguration.Speaker = cmbSpeaker.Text
            objConfiguration.MachineName = Trim(lblMachineName.Text)
            objConfiguration.UpdateDoctorSpeakerConfiguration(cmbDoctor.Tag)
            objConfiguration = Nothing
            Call Fill_DoctorSpeakerConfiguration()
            MessageBox.Show("Doctor-Speaker Configuration Modified.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            ts_btnUpdate.Enabled = False
            ts_btnDelete.Enabled = False
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.Modify, objErr.ToString, ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_DoctorSpeakerConfiguration()
        Dim dtConfiguration As DataTable
        Dim objConfiguration As New clsDoctorSpeakerConfiguration
        dtConfiguration = objConfiguration.Fill_DeleteDoctorSpeakerConfiguration(gstrClientMachineName)
        objConfiguration = Nothing
        dgConfiguration.DataSource = dtConfiguration

        Dim grdTableStyle As New clsDataGridTableStyle(dtConfiguration.TableName)

        Dim grdColStyleID As New DataGridTextBoxColumn
        With grdColStyleID
            .HeaderText = "ID"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtConfiguration.Columns(0).ColumnName
            .NullText = ""
            .Width = 0
        End With

        Dim grdColStyleDoctor As New DataGridTextBoxColumn
        With grdColStyleDoctor
            .HeaderText = "Doctor"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtConfiguration.Columns(1).ColumnName
            .NullText = ""
            .Width = 0.5 * dgConfiguration.Width
        End With

        Dim grdColStyleSpeaker As New DataGridTextBoxColumn
        With grdColStyleSpeaker
            .HeaderText = "Speaker"
            .Alignment = HorizontalAlignment.Left
            .MappingName = dtConfiguration.Columns(2).ColumnName
            .NullText = ""
            .Width = 0.5 * dgConfiguration.Width - 5
        End With

        grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleID, grdColStyleDoctor, grdColStyleSpeaker})
        dgConfiguration.TableStyles.Clear()
        dgConfiguration.TableStyles.Add(grdTableStyle)

    End Sub



    Private Sub dgConfiguration_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgConfiguration.MouseUp

        Try
            ''commented Sandip Darade 20090701
            ''Me.Cursor = Cursors.WaitCursor

            If dgConfiguration.CurrentRowIndex >= 0 Then

                dgConfiguration.Select(dgConfiguration.CurrentRowIndex)
                cmbDoctor.Tag = dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 0)

                'If cmbDoctor.Items.Contains(dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 1)) = True Then
                cmbDoctor.Text = dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 1)
                'End If

                'If cmbSpeaker.Items.Contains(dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 2)) = True Then
                cmbSpeaker.Text = dgConfiguration.Item(dgConfiguration.CurrentRowIndex, 2)
                'End If

                ts_btnUpdate.Enabled = True
                ts_btnDelete.Enabled = True
                Me.Cursor = Cursors.Default
            End If

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.Select, objErr.ToString, ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub tlsSpeaker_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsSpeaker.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                AddSpeaker()
            Case "Modify"
                UpdateSpeaker()
            Case "Delete"
                DeleteSpeaker()
            Case "Close"
                Me.Close()
        End Select
    End Sub


    Private Sub btnRefreshSpeakers_Click(sender As System.Object, e As System.EventArgs) Handles btnRefreshSpeakers.Click

        Try

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()

            btnRefreshSpeakers.Enabled = False
            Fill_Speakers(True)
            btnRefreshSpeakers.Enabled = True

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ActivityModule.Voice, ActivityCategory.DoctorSpeakerConfiguration, ActivityType.Select, ex.ToString, ActivityOutCome.Failure)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

End Class
