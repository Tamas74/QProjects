<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEMTimeSpend
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEMTimeSpend))
        Me.lblexamStartTime = New System.Windows.Forms.Label
        Me.lblexamEndTime = New System.Windows.Forms.Label
        Me.lblstarttimetext = New System.Windows.Forms.Label
        Me.lblendtimetext = New System.Windows.Forms.Label
        Me.lbltimeSpend = New System.Windows.Forms.Label
        Me.txttimeSpend = New System.Windows.Forms.TextBox
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstripDiagnosis = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlTimeSpend = New System.Windows.Forms.Panel
        Me.chkdefaultvalue = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblmin = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlLabs = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLabs = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.pnlOrders = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtOrders = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.pnlOtherDiagnostictest = New System.Windows.Forms.Panel
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtOtherDiagonsticTest = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripDiagnosis.SuspendLayout()
        Me.pnlTimeSpend.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.pnlOrders.SuspendLayout()
        Me.pnlOtherDiagnostictest.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblexamStartTime
        '
        Me.lblexamStartTime.AutoSize = True
        Me.lblexamStartTime.Location = New System.Drawing.Point(72, 17)
        Me.lblexamStartTime.Name = "lblexamStartTime"
        Me.lblexamStartTime.Size = New System.Drawing.Size(106, 14)
        Me.lblexamStartTime.TabIndex = 0
        Me.lblexamStartTime.Text = "Exam Start Time :"
        '
        'lblexamEndTime
        '
        Me.lblexamEndTime.AutoSize = True
        Me.lblexamEndTime.Location = New System.Drawing.Point(78, 40)
        Me.lblexamEndTime.Name = "lblexamEndTime"
        Me.lblexamEndTime.Size = New System.Drawing.Size(100, 14)
        Me.lblexamEndTime.TabIndex = 0
        Me.lblexamEndTime.Text = "Exam End Time :"
        '
        'lblstarttimetext
        '
        Me.lblstarttimetext.AutoSize = True
        Me.lblstarttimetext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstarttimetext.Location = New System.Drawing.Point(181, 17)
        Me.lblstarttimetext.Name = "lblstarttimetext"
        Me.lblstarttimetext.Size = New System.Drawing.Size(107, 14)
        Me.lblstarttimetext.TabIndex = 0
        Me.lblstarttimetext.Text = "Exam Start Time"
        Me.lblstarttimetext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblendtimetext
        '
        Me.lblendtimetext.AutoSize = True
        Me.lblendtimetext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblendtimetext.Location = New System.Drawing.Point(181, 40)
        Me.lblendtimetext.Name = "lblendtimetext"
        Me.lblendtimetext.Size = New System.Drawing.Size(98, 14)
        Me.lblendtimetext.TabIndex = 0
        Me.lblendtimetext.Text = "Exam End Time"
        Me.lblendtimetext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbltimeSpend
        '
        Me.lbltimeSpend.AutoSize = True
        Me.lbltimeSpend.Location = New System.Drawing.Point(26, 94)
        Me.lbltimeSpend.Name = "lbltimeSpend"
        Me.lbltimeSpend.Size = New System.Drawing.Size(150, 14)
        Me.lbltimeSpend.TabIndex = 0
        Me.lbltimeSpend.Text = "Time spend with Patient :"
        '
        'txttimeSpend
        '
        Me.txttimeSpend.Location = New System.Drawing.Point(181, 90)
        Me.txttimeSpend.Name = "txttimeSpend"
        Me.txttimeSpend.Size = New System.Drawing.Size(39, 22)
        Me.txttimeSpend.TabIndex = 1
        Me.txttimeSpend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(325, 53)
        Me.pnl_tlsp_Top.TabIndex = 17
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
        Me.tstripDiagnosis.Size = New System.Drawing.Size(325, 53)
        Me.tstripDiagnosis.TabIndex = 0
        Me.tstripDiagnosis.Text = "ToolStrip1"
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Tag = "OK"
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
        Me.tlsbtnClose.Tag = "Cancel"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'pnlTimeSpend
        '
        Me.pnlTimeSpend.Controls.Add(Me.chkdefaultvalue)
        Me.pnlTimeSpend.Controls.Add(Me.Label5)
        Me.pnlTimeSpend.Controls.Add(Me.Label6)
        Me.pnlTimeSpend.Controls.Add(Me.txttimeSpend)
        Me.pnlTimeSpend.Controls.Add(Me.Label7)
        Me.pnlTimeSpend.Controls.Add(Me.lblmin)
        Me.pnlTimeSpend.Controls.Add(Me.lblendtimetext)
        Me.pnlTimeSpend.Controls.Add(Me.lbltimeSpend)
        Me.pnlTimeSpend.Controls.Add(Me.Label8)
        Me.pnlTimeSpend.Controls.Add(Me.lblexamEndTime)
        Me.pnlTimeSpend.Controls.Add(Me.lblexamStartTime)
        Me.pnlTimeSpend.Controls.Add(Me.lblstarttimetext)
        Me.pnlTimeSpend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTimeSpend.Location = New System.Drawing.Point(0, 53)
        Me.pnlTimeSpend.Name = "pnlTimeSpend"
        Me.pnlTimeSpend.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTimeSpend.Size = New System.Drawing.Size(325, 129)
        Me.pnlTimeSpend.TabIndex = 18
        Me.pnlTimeSpend.Visible = False
        '
        'chkdefaultvalue
        '
        Me.chkdefaultvalue.AutoSize = True
        Me.chkdefaultvalue.Location = New System.Drawing.Point(181, 63)
        Me.chkdefaultvalue.Name = "chkdefaultvalue"
        Me.chkdefaultvalue.Size = New System.Drawing.Size(116, 18)
        Me.chkdefaultvalue.TabIndex = 9
        Me.chkdefaultvalue.Text = "Use default time"
        Me.chkdefaultvalue.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(317, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 122)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(321, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 122)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'lblmin
        '
        Me.lblmin.AutoSize = True
        Me.lblmin.Location = New System.Drawing.Point(223, 94)
        Me.lblmin.Name = "lblmin"
        Me.lblmin.Size = New System.Drawing.Size(30, 14)
        Me.lblmin.TabIndex = 0
        Me.lblmin.Text = "min."
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(319, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlLabs
        '
        Me.pnlLabs.Controls.Add(Me.Label1)
        Me.pnlLabs.Controls.Add(Me.Label2)
        Me.pnlLabs.Controls.Add(Me.txtLabs)
        Me.pnlLabs.Controls.Add(Me.Label3)
        Me.pnlLabs.Controls.Add(Me.Label10)
        Me.pnlLabs.Controls.Add(Me.Label11)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabs.Location = New System.Drawing.Point(0, 53)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLabs.Size = New System.Drawing.Size(325, 129)
        Me.pnlLabs.TabIndex = 19
        Me.pnlLabs.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(317, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 122)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'txtLabs
        '
        Me.txtLabs.Location = New System.Drawing.Point(205, 68)
        Me.txtLabs.MaxLength = 9
        Me.txtLabs.Name = "txtLabs"
        Me.txtLabs.Size = New System.Drawing.Size(39, 22)
        Me.txtLabs.TabIndex = 1
        Me.txtLabs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(321, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 122)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(100, 72)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Other labs (0 -9):"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(319, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'pnlOrders
        '
        Me.pnlOrders.Controls.Add(Me.Label15)
        Me.pnlOrders.Controls.Add(Me.Label16)
        Me.pnlOrders.Controls.Add(Me.txtOrders)
        Me.pnlOrders.Controls.Add(Me.Label17)
        Me.pnlOrders.Controls.Add(Me.Label20)
        Me.pnlOrders.Controls.Add(Me.Label21)
        Me.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOrders.Location = New System.Drawing.Point(0, 53)
        Me.pnlOrders.Name = "pnlOrders"
        Me.pnlOrders.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlOrders.Size = New System.Drawing.Size(325, 129)
        Me.pnlOrders.TabIndex = 20
        Me.pnlOrders.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(4, 125)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(317, 1)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 122)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label4"
        '
        'txtOrders
        '
        Me.txtOrders.Location = New System.Drawing.Point(212, 68)
        Me.txtOrders.MaxLength = 9
        Me.txtOrders.Name = "txtOrders"
        Me.txtOrders.Size = New System.Drawing.Size(39, 22)
        Me.txtOrders.TabIndex = 1
        Me.txtOrders.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(321, 4)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 122)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label3"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(93, 72)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(115, 14)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Other Orders(0-9) :"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(319, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'pnlOtherDiagnostictest
        '
        Me.pnlOtherDiagnostictest.Controls.Add(Me.Label25)
        Me.pnlOtherDiagnostictest.Controls.Add(Me.Label26)
        Me.pnlOtherDiagnostictest.Controls.Add(Me.txtOtherDiagonsticTest)
        Me.pnlOtherDiagnostictest.Controls.Add(Me.Label27)
        Me.pnlOtherDiagnostictest.Controls.Add(Me.Label30)
        Me.pnlOtherDiagnostictest.Controls.Add(Me.Label31)
        Me.pnlOtherDiagnostictest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOtherDiagnostictest.Location = New System.Drawing.Point(0, 53)
        Me.pnlOtherDiagnostictest.Name = "pnlOtherDiagnostictest"
        Me.pnlOtherDiagnostictest.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlOtherDiagnostictest.Size = New System.Drawing.Size(325, 129)
        Me.pnlOtherDiagnostictest.TabIndex = 21
        Me.pnlOtherDiagnostictest.Visible = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(4, 125)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(317, 1)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 122)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "label4"
        '
        'txtOtherDiagonsticTest
        '
        Me.txtOtherDiagonsticTest.Location = New System.Drawing.Point(263, 68)
        Me.txtOtherDiagonsticTest.MaxLength = 9
        Me.txtOtherDiagonsticTest.Name = "txtOtherDiagonsticTest"
        Me.txtOtherDiagonsticTest.Size = New System.Drawing.Size(39, 22)
        Me.txtOtherDiagonsticTest.TabIndex = 1
        Me.txtOtherDiagonsticTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(321, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 122)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "label3"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(25, 72)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(234, 14)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Other additional diagnostic studies (0-9) :"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(3, 3)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(319, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'frmEMTimeSpend
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(325, 182)
        Me.Controls.Add(Me.pnlTimeSpend)
        Me.Controls.Add(Me.pnlOtherDiagnostictest)
        Me.Controls.Add(Me.pnlOrders)
        Me.Controls.Add(Me.pnlLabs)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEMTimeSpend"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Time spent with Patient"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripDiagnosis.ResumeLayout(False)
        Me.tstripDiagnosis.PerformLayout()
        Me.pnlTimeSpend.ResumeLayout(False)
        Me.pnlTimeSpend.PerformLayout()
        Me.pnlLabs.ResumeLayout(False)
        Me.pnlLabs.PerformLayout()
        Me.pnlOrders.ResumeLayout(False)
        Me.pnlOrders.PerformLayout()
        Me.pnlOtherDiagnostictest.ResumeLayout(False)
        Me.pnlOtherDiagnostictest.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblexamStartTime As System.Windows.Forms.Label
    Friend WithEvents lblexamEndTime As System.Windows.Forms.Label
    Friend WithEvents lblstarttimetext As System.Windows.Forms.Label
    Friend WithEvents lblendtimetext As System.Windows.Forms.Label
    Friend WithEvents lbltimeSpend As System.Windows.Forms.Label
    Friend WithEvents txttimeSpend As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstripDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTimeSpend As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblmin As System.Windows.Forms.Label
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLabs As System.Windows.Forms.TextBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlOrders As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtOrders As System.Windows.Forms.TextBox
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlOtherDiagnostictest As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtOtherDiagonsticTest As System.Windows.Forms.TextBox
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents chkdefaultvalue As System.Windows.Forms.CheckBox
End Class
