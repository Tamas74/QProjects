<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOBVital
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpControls As DateTimePicker() = {dtVitals, dtpConDueDate, dtpConEstDate, dtpUltraDueDate, dtpLMPDueDate, dtpLMPEstDate, dtpEstDueDate}

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                Catch ex As Exception

                End Try
                Try
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOBVital))
        Me.tblStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Assign_Task = New System.Windows.Forms.ToolStripButton()
        Me.tls_ObHistory = New System.Windows.Forms.ToolStripButton()
        Me.tls_OBPlan = New System.Windows.Forms.ToolStripButton()
        Me.tls_OBVitals = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Ok_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlPreferedGesAge = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnInternalBr = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtobcomm = New System.Windows.Forms.TextBox()
        Me.chkobrsk = New System.Windows.Forms.CheckBox()
        Me.btnDelUltraDueDate = New System.Windows.Forms.Button()
        Me.btnDelEstDueDate = New System.Windows.Forms.Button()
        Me.btnDelConception = New System.Windows.Forms.Button()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.chkUltraSound = New System.Windows.Forms.CheckBox()
        Me.chkLMP = New System.Windows.Forms.CheckBox()
        Me.chkConception = New System.Windows.Forms.CheckBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.dtpEstDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpUltraDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.dtpLMPDueDate = New System.Windows.Forms.DateTimePicker()
        Me.cbGADays = New System.Windows.Forms.ComboBox()
        Me.dtpConDueDate = New System.Windows.Forms.DateTimePicker()
        Me.cbGAWeeks = New System.Windows.Forms.ComboBox()
        Me.dtpLMPEstDate = New System.Windows.Forms.DateTimePicker()
        Me.txtNextAppointment = New System.Windows.Forms.TextBox()
        Me.dtpConEstDate = New System.Windows.Forms.DateTimePicker()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtBPSittingMax = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblbpsidia = New System.Windows.Forms.Label()
        Me.txtBPSittingMin = New System.Windows.Forms.TextBox()
        Me.lblbpsisys = New System.Windows.Forms.Label()
        Me.label52 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.label51 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtVitals = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtUrineGlucose = New System.Windows.Forms.TextBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.pnlLiving = New System.Windows.Forms.Panel()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.pnlPainLevelCurrent = New System.Windows.Forms.Panel()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.label36 = New System.Windows.Forms.Label()
        Me.label35 = New System.Windows.Forms.Label()
        Me.label34 = New System.Windows.Forms.Label()
        Me.label33 = New System.Windows.Forms.Label()
        Me.label32 = New System.Windows.Forms.Label()
        Me.label31 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.trbPainLevel = New System.Windows.Forms.TrackBar()
        Me.chkpain = New System.Windows.Forms.CheckBox()
        Me.txtWeightChange = New System.Windows.Forms.TextBox()
        Me.txtUrineAlbumin = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtPrePregWeight = New System.Windows.Forms.TextBox()
        Me.txtEdema = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.pnlEffacement = New System.Windows.Forms.Panel()
        Me.txtStation = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtDilation = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtEffacement = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cbPreTermLabor = New System.Windows.Forms.ComboBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.cbFetalMovement = New System.Windows.Forms.ComboBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtFetalheartrate = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtPresentation = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtPreferredGesAge = New System.Windows.Forms.TextBox()
        Me.txtFundalHeight = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTradeName = New System.Windows.Forms.Label()
        Me.btnDelLMPEstDate = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnltblStrip = New System.Windows.Forms.Panel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tblStrip.SuspendLayout()
        Me.pnlPreferedGesAge.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlLiving.SuspendLayout()
        Me.pnlPainLevelCurrent.SuspendLayout()
        CType(Me.trbPainLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEffacement.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnltblStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Assign_Task, Me.tls_ObHistory, Me.tls_OBPlan, Me.tls_OBVitals, Me.tblbtn_Ok_32, Me.tblbtn_Close_32})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(979, 53)
        Me.tblStrip.TabIndex = 0
        Me.tblStrip.TabStop = True
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Assign_Task
        '
        Me.tblbtn_Assign_Task.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_Assign_Task.Image = CType(resources.GetObject("tblbtn_Assign_Task.Image"), System.Drawing.Image)
        Me.tblbtn_Assign_Task.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Assign_Task.Name = "tblbtn_Assign_Task"
        Me.tblbtn_Assign_Task.Size = New System.Drawing.Size(82, 50)
        Me.tblbtn_Assign_Task.Tag = "Assign Task"
        Me.tblbtn_Assign_Task.Text = "Assign Task"
        Me.tblbtn_Assign_Task.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Assign_Task.Visible = False
        '
        'tls_ObHistory
        '
        Me.tls_ObHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ObHistory.Image = CType(resources.GetObject("tls_ObHistory.Image"), System.Drawing.Image)
        Me.tls_ObHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_ObHistory.Name = "tls_ObHistory"
        Me.tls_ObHistory.Size = New System.Drawing.Size(76, 50)
        Me.tls_ObHistory.Tag = "History"
        Me.tls_ObHistory.Text = "OB &History"
        Me.tls_ObHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_ObHistory.ToolTipText = "OB History"
        '
        'tls_OBPlan
        '
        Me.tls_OBPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_OBPlan.Image = CType(resources.GetObject("tls_OBPlan.Image"), System.Drawing.Image)
        Me.tls_OBPlan.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_OBPlan.Name = "tls_OBPlan"
        Me.tls_OBPlan.Size = New System.Drawing.Size(58, 50)
        Me.tls_OBPlan.Tag = "OB Plan"
        Me.tls_OBPlan.Text = "OB &Plan"
        Me.tls_OBPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_OBPlan.ToolTipText = "OB Plan"
        '
        'tls_OBVitals
        '
        Me.tls_OBVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_OBVitals.Image = CType(resources.GetObject("tls_OBVitals.Image"), System.Drawing.Image)
        Me.tls_OBVitals.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_OBVitals.Name = "tls_OBVitals"
        Me.tls_OBVitals.Size = New System.Drawing.Size(98, 50)
        Me.tls_OBVitals.Tag = "OBVitals"
        Me.tls_OBVitals.Text = "&View OB Vitals"
        Me.tls_OBVitals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_OBVitals.ToolTipText = "View OB Vitals"
        '
        'tblbtn_Ok_32
        '
        Me.tblbtn_Ok_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Ok_32.Image = CType(resources.GetObject("tblbtn_Ok_32.Image"), System.Drawing.Image)
        Me.tblbtn_Ok_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Ok_32.Name = "tblbtn_Ok_32"
        Me.tblbtn_Ok_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Ok_32.Tag = "Ok"
        Me.tblbtn_Ok_32.Text = "&Save&&Cls"
        Me.tblbtn_Ok_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Ok_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlPreferedGesAge
        '
        Me.pnlPreferedGesAge.Controls.Add(Me.Label10)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label7)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label9)
        Me.pnlPreferedGesAge.Controls.Add(Me.btnInternalBr)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label6)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtobcomm)
        Me.pnlPreferedGesAge.Controls.Add(Me.chkobrsk)
        Me.pnlPreferedGesAge.Controls.Add(Me.btnDelUltraDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.btnDelEstDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.btnDelConception)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label74)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label73)
        Me.pnlPreferedGesAge.Controls.Add(Me.chkUltraSound)
        Me.pnlPreferedGesAge.Controls.Add(Me.chkLMP)
        Me.pnlPreferedGesAge.Controls.Add(Me.chkConception)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label72)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpEstDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label71)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label69)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label68)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label1)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpUltraDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label67)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpLMPDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.cbGADays)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpConDueDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.cbGAWeeks)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpLMPEstDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtNextAppointment)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtpConEstDate)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label62)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label2)
        Me.pnlPreferedGesAge.Controls.Add(Me.Panel1)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label3)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label50)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label28)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label16)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label15)
        Me.pnlPreferedGesAge.Controls.Add(Me.dtVitals)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label18)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label8)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label21)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtUrineGlucose)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtComment)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label4)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label53)
        Me.pnlPreferedGesAge.Controls.Add(Me.pnlLiving)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtWeightChange)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtUrineAlbumin)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label49)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label47)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label29)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label61)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtPrePregWeight)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtEdema)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label30)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtWeight)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label48)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label55)
        Me.pnlPreferedGesAge.Controls.Add(Me.pnlEffacement)
        Me.pnlPreferedGesAge.Controls.Add(Me.cbPreTermLabor)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label59)
        Me.pnlPreferedGesAge.Controls.Add(Me.cbFetalMovement)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label56)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtFetalheartrate)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label54)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtPresentation)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label60)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label22)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtPreferredGesAge)
        Me.pnlPreferedGesAge.Controls.Add(Me.txtFundalHeight)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label26)
        Me.pnlPreferedGesAge.Controls.Add(Me.Label5)
        Me.pnlPreferedGesAge.Controls.Add(Me.lblTradeName)
        Me.pnlPreferedGesAge.Controls.Add(Me.btnDelLMPEstDate)
        Me.pnlPreferedGesAge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPreferedGesAge.Location = New System.Drawing.Point(0, 0)
        Me.pnlPreferedGesAge.Name = "pnlPreferedGesAge"
        Me.pnlPreferedGesAge.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPreferedGesAge.Size = New System.Drawing.Size(979, 624)
        Me.pnlPreferedGesAge.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(604, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(14, 14)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "*"
        '
        'btnInternalBr
        '
        Me.btnInternalBr.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnInternalBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInternalBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInternalBr.Image = CType(resources.GetObject("btnInternalBr.Image"), System.Drawing.Image)
        Me.btnInternalBr.Location = New System.Drawing.Point(945, 336)
        Me.btnInternalBr.Name = "btnInternalBr"
        Me.btnInternalBr.Size = New System.Drawing.Size(22, 21)
        Me.btnInternalBr.TabIndex = 56
        Me.btnInternalBr.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(542, 507)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 14)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Risk Comments :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtobcomm
        '
        Me.txtobcomm.ForeColor = System.Drawing.Color.Black
        Me.txtobcomm.Location = New System.Drawing.Point(646, 501)
        Me.txtobcomm.MaxLength = 500
        Me.txtobcomm.Multiline = True
        Me.txtobcomm.Name = "txtobcomm"
        Me.txtobcomm.Size = New System.Drawing.Size(296, 109)
        Me.txtobcomm.TabIndex = 54
        '
        'chkobrsk
        '
        Me.chkobrsk.AutoSize = True
        Me.chkobrsk.Location = New System.Drawing.Point(648, 474)
        Me.chkobrsk.Name = "chkobrsk"
        Me.chkobrsk.Size = New System.Drawing.Size(74, 18)
        Me.chkobrsk.TabIndex = 53
        Me.chkobrsk.Text = "High Risk"
        Me.chkobrsk.UseVisualStyleBackColor = True
        '
        'btnDelUltraDueDate
        '
        Me.btnDelUltraDueDate.AutoEllipsis = True
        Me.btnDelUltraDueDate.BackColor = System.Drawing.Color.Transparent
        Me.btnDelUltraDueDate.BackgroundImage = CType(resources.GetObject("btnDelUltraDueDate.BackgroundImage"), System.Drawing.Image)
        Me.btnDelUltraDueDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelUltraDueDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnDelUltraDueDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnDelUltraDueDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDelUltraDueDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelUltraDueDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelUltraDueDate.Image = CType(resources.GetObject("btnDelUltraDueDate.Image"), System.Drawing.Image)
        Me.btnDelUltraDueDate.Location = New System.Drawing.Point(509, 147)
        Me.btnDelUltraDueDate.Name = "btnDelUltraDueDate"
        Me.btnDelUltraDueDate.Size = New System.Drawing.Size(24, 22)
        Me.btnDelUltraDueDate.TabIndex = 12
        Me.ToolTip.SetToolTip(Me.btnDelUltraDueDate, "Delete Ultrasound")
        Me.btnDelUltraDueDate.UseVisualStyleBackColor = False
        '
        'btnDelEstDueDate
        '
        Me.btnDelEstDueDate.AutoEllipsis = True
        Me.btnDelEstDueDate.BackColor = System.Drawing.Color.Transparent
        Me.btnDelEstDueDate.BackgroundImage = CType(resources.GetObject("btnDelEstDueDate.BackgroundImage"), System.Drawing.Image)
        Me.btnDelEstDueDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelEstDueDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnDelEstDueDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnDelEstDueDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDelEstDueDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelEstDueDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelEstDueDate.Image = CType(resources.GetObject("btnDelEstDueDate.Image"), System.Drawing.Image)
        Me.btnDelEstDueDate.Location = New System.Drawing.Point(867, 118)
        Me.btnDelEstDueDate.Name = "btnDelEstDueDate"
        Me.btnDelEstDueDate.Size = New System.Drawing.Size(22, 22)
        Me.btnDelEstDueDate.TabIndex = 15
        Me.ToolTip.SetToolTip(Me.btnDelEstDueDate, "Delete Estimated Due Date")
        Me.btnDelEstDueDate.UseVisualStyleBackColor = False
        '
        'btnDelConception
        '
        Me.btnDelConception.AutoEllipsis = True
        Me.btnDelConception.BackColor = System.Drawing.Color.Transparent
        Me.btnDelConception.BackgroundImage = CType(resources.GetObject("btnDelConception.BackgroundImage"), System.Drawing.Image)
        Me.btnDelConception.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelConception.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnDelConception.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnDelConception.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDelConception.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelConception.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelConception.Image = CType(resources.GetObject("btnDelConception.Image"), System.Drawing.Image)
        Me.btnDelConception.Location = New System.Drawing.Point(287, 90)
        Me.btnDelConception.Name = "btnDelConception"
        Me.btnDelConception.Size = New System.Drawing.Size(22, 22)
        Me.btnDelConception.TabIndex = 4
        Me.ToolTip.SetToolTip(Me.btnDelConception, "Delete Conception")
        Me.btnDelConception.UseVisualStyleBackColor = False
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Location = New System.Drawing.Point(4, 41)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(971, 1)
        Me.Label74.TabIndex = 52
        Me.Label74.Text = "Dilation :"
        '
        'Label73
        '
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(526, 50)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(65, 34)
        Me.Label73.TabIndex = 51
        Me.Label73.Text = "Select Method"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkUltraSound
        '
        Me.chkUltraSound.AutoSize = True
        Me.chkUltraSound.Location = New System.Drawing.Point(548, 150)
        Me.chkUltraSound.Name = "chkUltraSound"
        Me.chkUltraSound.Size = New System.Drawing.Size(15, 14)
        Me.chkUltraSound.TabIndex = 13
        Me.chkUltraSound.UseVisualStyleBackColor = True
        '
        'chkLMP
        '
        Me.chkLMP.AutoSize = True
        Me.chkLMP.Location = New System.Drawing.Point(548, 122)
        Me.chkLMP.Name = "chkLMP"
        Me.chkLMP.Size = New System.Drawing.Size(15, 14)
        Me.chkLMP.TabIndex = 10
        Me.chkLMP.UseVisualStyleBackColor = True
        '
        'chkConception
        '
        Me.chkConception.AutoSize = True
        Me.chkConception.Location = New System.Drawing.Point(548, 94)
        Me.chkConception.Name = "chkConception"
        Me.chkConception.Size = New System.Drawing.Size(15, 14)
        Me.chkConception.TabIndex = 6
        Me.chkConception.UseVisualStyleBackColor = True
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(596, 122)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(125, 14)
        Me.Label72.TabIndex = 47
        Me.Label72.Text = "Estimated Due Date :"
        '
        'dtpEstDueDate
        '
        Me.dtpEstDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpEstDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpEstDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpEstDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpEstDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpEstDueDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpEstDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEstDueDate.Location = New System.Drawing.Point(728, 118)
        Me.dtpEstDueDate.Name = "dtpEstDueDate"
        Me.dtpEstDueDate.Size = New System.Drawing.Size(136, 22)
        Me.dtpEstDueDate.TabIndex = 14
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(104, 150)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(73, 14)
        Me.Label71.TabIndex = 45
        Me.Label71.Text = "Ultrasound :"
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(46, 122)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(131, 14)
        Me.Label69.TabIndex = 44
        Me.Label69.Text = "Last Menstrual Period :"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(866, 150)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(32, 14)
        Me.Label68.TabIndex = 41
        Me.Label68.Text = "Days"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(100, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 14)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Conception :"
        '
        'dtpUltraDueDate
        '
        Me.dtpUltraDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpUltraDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpUltraDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpUltraDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpUltraDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpUltraDueDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpUltraDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUltraDueDate.Location = New System.Drawing.Point(402, 147)
        Me.dtpUltraDueDate.Name = "dtpUltraDueDate"
        Me.dtpUltraDueDate.Size = New System.Drawing.Size(104, 22)
        Me.dtpUltraDueDate.TabIndex = 11
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(774, 150)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(44, 14)
        Me.Label67.TabIndex = 40
        Me.Label67.Text = "Weeks"
        '
        'dtpLMPDueDate
        '
        Me.dtpLMPDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpLMPDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpLMPDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpLMPDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpLMPDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpLMPDueDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpLMPDueDate.Enabled = False
        Me.dtpLMPDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLMPDueDate.Location = New System.Drawing.Point(402, 118)
        Me.dtpLMPDueDate.Name = "dtpLMPDueDate"
        Me.dtpLMPDueDate.Size = New System.Drawing.Size(104, 22)
        Me.dtpLMPDueDate.TabIndex = 9
        '
        'cbGADays
        '
        Me.cbGADays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGADays.FormattingEnabled = True
        Me.cbGADays.Items.AddRange(New Object() {"", "0", "1", "2", "3", "4", "5", "6"})
        Me.cbGADays.Location = New System.Drawing.Point(827, 146)
        Me.cbGADays.Name = "cbGADays"
        Me.cbGADays.Size = New System.Drawing.Size(37, 22)
        Me.cbGADays.TabIndex = 17
        '
        'dtpConDueDate
        '
        Me.dtpConDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpConDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpConDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpConDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpConDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpConDueDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpConDueDate.Enabled = False
        Me.dtpConDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConDueDate.Location = New System.Drawing.Point(402, 90)
        Me.dtpConDueDate.Name = "dtpConDueDate"
        Me.dtpConDueDate.Size = New System.Drawing.Size(104, 22)
        Me.dtpConDueDate.TabIndex = 5
        '
        'cbGAWeeks
        '
        Me.cbGAWeeks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGAWeeks.FormattingEnabled = True
        Me.cbGAWeeks.Items.AddRange(New Object() {"", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42"})
        Me.cbGAWeeks.Location = New System.Drawing.Point(728, 146)
        Me.cbGAWeeks.Name = "cbGAWeeks"
        Me.cbGAWeeks.Size = New System.Drawing.Size(43, 22)
        Me.cbGAWeeks.TabIndex = 16
        '
        'dtpLMPEstDate
        '
        Me.dtpLMPEstDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpLMPEstDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpLMPEstDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpLMPEstDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpLMPEstDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpLMPEstDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpLMPEstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLMPEstDate.Location = New System.Drawing.Point(179, 118)
        Me.dtpLMPEstDate.Name = "dtpLMPEstDate"
        Me.dtpLMPEstDate.Size = New System.Drawing.Size(104, 22)
        Me.dtpLMPEstDate.TabIndex = 7
        '
        'txtNextAppointment
        '
        Me.txtNextAppointment.Location = New System.Drawing.Point(646, 194)
        Me.txtNextAppointment.MaxLength = 500
        Me.txtNextAppointment.Name = "txtNextAppointment"
        Me.txtNextAppointment.Size = New System.Drawing.Size(296, 22)
        Me.txtNextAppointment.TabIndex = 31
        '
        'dtpConEstDate
        '
        Me.dtpConEstDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpConEstDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpConEstDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpConEstDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpConEstDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpConEstDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpConEstDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpConEstDate.Location = New System.Drawing.Point(179, 90)
        Me.dtpConEstDate.Name = "dtpConEstDate"
        Me.dtpConEstDate.Size = New System.Drawing.Size(104, 22)
        Me.dtpConEstDate.TabIndex = 3
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(522, 198)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(117, 14)
        Me.Label62.TabIndex = 37
        Me.Label62.Text = "Next Appointment :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(202, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtBPSittingMax)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.lblbpsidia)
        Me.Panel1.Controls.Add(Me.txtBPSittingMin)
        Me.Panel1.Controls.Add(Me.lblbpsisys)
        Me.Panel1.Controls.Add(Me.label52)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.label51)
        Me.Panel1.Location = New System.Drawing.Point(19, 397)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(463, 68)
        Me.Panel1.TabIndex = 24
        '
        'txtBPSittingMax
        '
        Me.txtBPSittingMax.ForeColor = System.Drawing.Color.Black
        Me.txtBPSittingMax.Location = New System.Drawing.Point(157, 7)
        Me.txtBPSittingMax.MaxLength = 3
        Me.txtBPSittingMax.Name = "txtBPSittingMax"
        Me.txtBPSittingMax.Size = New System.Drawing.Size(52, 22)
        Me.txtBPSittingMax.TabIndex = 0
        Me.txtBPSittingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(215, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(17, 26)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "/"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblbpsidia
        '
        Me.lblbpsidia.AutoSize = True
        Me.lblbpsidia.BackColor = System.Drawing.Color.Transparent
        Me.lblbpsidia.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpsidia.Location = New System.Drawing.Point(231, 49)
        Me.lblbpsidia.Name = "lblbpsidia"
        Me.lblbpsidia.Size = New System.Drawing.Size(70, 11)
        Me.lblbpsidia.TabIndex = 1
        Me.lblbpsidia.Text = "(129.54-177.80)"
        Me.lblbpsidia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBPSittingMin
        '
        Me.txtBPSittingMin.ForeColor = System.Drawing.Color.Black
        Me.txtBPSittingMin.Location = New System.Drawing.Point(240, 6)
        Me.txtBPSittingMin.MaxLength = 3
        Me.txtBPSittingMin.Name = "txtBPSittingMin"
        Me.txtBPSittingMin.Size = New System.Drawing.Size(52, 22)
        Me.txtBPSittingMin.TabIndex = 1
        Me.txtBPSittingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblbpsisys
        '
        Me.lblbpsisys.AutoSize = True
        Me.lblbpsisys.BackColor = System.Drawing.Color.Transparent
        Me.lblbpsisys.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbpsisys.Location = New System.Drawing.Point(148, 50)
        Me.lblbpsisys.Name = "lblbpsisys"
        Me.lblbpsisys.Size = New System.Drawing.Size(70, 11)
        Me.lblbpsisys.TabIndex = 0
        Me.lblbpsisys.Text = "(129.54-177.80)"
        Me.lblbpsisys.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label52
        '
        Me.label52.AutoSize = True
        Me.label52.BackColor = System.Drawing.Color.Transparent
        Me.label52.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label52.Location = New System.Drawing.Point(162, 32)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(43, 11)
        Me.label52.TabIndex = 3
        Me.label52.Text = "(Systolic)"
        Me.label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(86, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 14)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "BP Sitting :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label51
        '
        Me.label51.AutoSize = True
        Me.label51.BackColor = System.Drawing.Color.Transparent
        Me.label51.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label51.Location = New System.Drawing.Point(244, 31)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(45, 11)
        Me.label51.TabIndex = 4
        Me.label51.Text = "(Diastolic)"
        Me.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(415, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 34)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Suggested Due Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Location = New System.Drawing.Point(975, 4)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 616)
        Me.Label50.TabIndex = 27
        Me.Label50.Text = "Dilation :"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Location = New System.Drawing.Point(3, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 616)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Dilation :"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(3, 620)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(973, 1)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Dilation :"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(3, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(973, 1)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Dilation :"
        '
        'dtVitals
        '
        Me.dtVitals.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtVitals.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtVitals.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtVitals.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtVitals.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtVitals.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtVitals.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVitals.Location = New System.Drawing.Point(167, 11)
        Me.dtVitals.Name = "dtVitals"
        Me.dtVitals.Size = New System.Drawing.Size(190, 22)
        Me.dtVitals.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(97, 15)
        Me.Label18.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(68, 14)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Vital Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(4, 183)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(971, 1)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Dilation :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(566, 338)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(73, 14)
        Me.Label21.TabIndex = 35
        Me.Label21.Text = "Comments :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUrineGlucose
        '
        Me.txtUrineGlucose.Location = New System.Drawing.Point(424, 589)
        Me.txtUrineGlucose.MaxLength = 30
        Me.txtUrineGlucose.Name = "txtUrineGlucose"
        Me.txtUrineGlucose.Size = New System.Drawing.Size(70, 22)
        Me.txtUrineGlucose.TabIndex = 30
        '
        'txtComment
        '
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(646, 334)
        Me.txtComment.MaxLength = 255
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(296, 129)
        Me.txtComment.TabIndex = 33
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(274, 567)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "(lbs)"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(333, 593)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(89, 14)
        Me.Label53.TabIndex = 32
        Me.Label53.Text = "Urine Glucose :"
        '
        'pnlLiving
        '
        Me.pnlLiving.Controls.Add(Me.Label58)
        Me.pnlLiving.Controls.Add(Me.pnlPainLevelCurrent)
        Me.pnlLiving.Controls.Add(Me.chkpain)
        Me.pnlLiving.Location = New System.Drawing.Point(502, 223)
        Me.pnlLiving.Name = "pnlLiving"
        Me.pnlLiving.Size = New System.Drawing.Size(465, 101)
        Me.pnlLiving.TabIndex = 32
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(6, 11)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(110, 14)
        Me.Label58.TabIndex = 0
        Me.Label58.Text = "Pain level : Current"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPainLevelCurrent
        '
        Me.pnlPainLevelCurrent.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label63)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label64)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label65)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label66)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label36)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label35)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label34)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label33)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label32)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label31)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label37)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label38)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label39)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label40)
        Me.pnlPainLevelCurrent.Controls.Add(Me.label42)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label41)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label43)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label44)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label45)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label46)
        Me.pnlPainLevelCurrent.Controls.Add(Me.Label57)
        Me.pnlPainLevelCurrent.Controls.Add(Me.trbPainLevel)
        Me.pnlPainLevelCurrent.Location = New System.Drawing.Point(143, 11)
        Me.pnlPainLevelCurrent.Name = "pnlPainLevelCurrent"
        Me.pnlPainLevelCurrent.Size = New System.Drawing.Size(316, 84)
        Me.pnlPainLevelCurrent.TabIndex = 2
        Me.pnlPainLevelCurrent.TabStop = True
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(1, 83)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(314, 1)
        Me.Label63.TabIndex = 4
        Me.Label63.Text = "label2"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(0, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(1, 83)
        Me.Label64.TabIndex = 1
        Me.Label64.Text = "label4"
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(315, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 83)
        Me.Label65.TabIndex = 0
        Me.Label65.Text = "label3"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(316, 1)
        Me.Label66.TabIndex = 0
        Me.Label66.Text = "label1"
        '
        'label36
        '
        Me.label36.AutoSize = True
        Me.label36.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label36.Location = New System.Drawing.Point(281, 66)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(19, 12)
        Me.label36.TabIndex = 0
        Me.label36.Text = "10"
        '
        'label35
        '
        Me.label35.AutoSize = True
        Me.label35.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label35.Location = New System.Drawing.Point(254, 66)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(12, 12)
        Me.label35.TabIndex = 0
        Me.label35.Text = "9"
        '
        'label34
        '
        Me.label34.AutoSize = True
        Me.label34.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label34.Location = New System.Drawing.Point(227, 66)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(12, 12)
        Me.label34.TabIndex = 0
        Me.label34.Text = "8"
        '
        'label33
        '
        Me.label33.AutoSize = True
        Me.label33.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label33.Location = New System.Drawing.Point(201, 66)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(12, 12)
        Me.label33.TabIndex = 0
        Me.label33.Text = "7"
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.Location = New System.Drawing.Point(174, 66)
        Me.label32.Name = "label32"
        Me.label32.Size = New System.Drawing.Size(12, 12)
        Me.label32.TabIndex = 0
        Me.label32.Text = "6"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.Location = New System.Drawing.Point(147, 66)
        Me.label31.Name = "label31"
        Me.label31.Size = New System.Drawing.Size(12, 12)
        Me.label31.TabIndex = 0
        Me.label31.Text = "5"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(120, 66)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(12, 12)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "4"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(93, 66)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(12, 12)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "3"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(66, 66)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(12, 12)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "2"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(40, 66)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(12, 12)
        Me.Label40.TabIndex = 0
        Me.Label40.Text = "1"
        '
        'label42
        '
        Me.label42.AutoSize = True
        Me.label42.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label42.Location = New System.Drawing.Point(254, 18)
        Me.label42.Name = "label42"
        Me.label42.Size = New System.Drawing.Size(23, 11)
        Me.label42.TabIndex = 0
        Me.label42.Text = "Pain"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(237, 6)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(52, 11)
        Me.Label41.TabIndex = 0
        Me.Label41.Text = "Unbearable"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(135, 18)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(23, 11)
        Me.Label43.TabIndex = 0
        Me.Label43.Text = "Pain"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(122, 6)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(44, 11)
        Me.Label44.TabIndex = 0
        Me.Label44.Text = "Moderate"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(5, 18)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(23, 11)
        Me.Label45.TabIndex = 2
        Me.Label45.Text = "Pain"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(8, 6)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(17, 11)
        Me.Label46.TabIndex = 1
        Me.Label46.Text = "No"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(13, 66)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(12, 12)
        Me.Label57.TabIndex = 0
        Me.Label57.Text = "0"
        '
        'trbPainLevel
        '
        Me.trbPainLevel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.trbPainLevel.LargeChange = 1
        Me.trbPainLevel.Location = New System.Drawing.Point(3, 33)
        Me.trbPainLevel.Name = "trbPainLevel"
        Me.trbPainLevel.Size = New System.Drawing.Size(301, 45)
        Me.trbPainLevel.TabIndex = 1
        '
        'chkpain
        '
        Me.chkpain.Location = New System.Drawing.Point(117, 11)
        Me.chkpain.Name = "chkpain"
        Me.chkpain.Padding = New System.Windows.Forms.Padding(3)
        Me.chkpain.Size = New System.Drawing.Size(20, 15)
        Me.chkpain.TabIndex = 0
        Me.chkpain.Text = "Allow"
        Me.chkpain.UseVisualStyleBackColor = True
        '
        'txtWeightChange
        '
        Me.txtWeightChange.Enabled = False
        Me.txtWeightChange.Location = New System.Drawing.Point(179, 559)
        Me.txtWeightChange.MaxLength = 6
        Me.txtWeightChange.Name = "txtWeightChange"
        Me.txtWeightChange.Size = New System.Drawing.Size(90, 22)
        Me.txtWeightChange.TabIndex = 28
        '
        'txtUrineAlbumin
        '
        Me.txtUrineAlbumin.Location = New System.Drawing.Point(179, 589)
        Me.txtUrineAlbumin.MaxLength = 30
        Me.txtUrineAlbumin.Name = "txtUrineAlbumin"
        Me.txtUrineAlbumin.Size = New System.Drawing.Size(90, 22)
        Me.txtUrineAlbumin.TabIndex = 29
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(87, 594)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(86, 14)
        Me.Label49.TabIndex = 30
        Me.Label49.Text = "Urine Protein :"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(77, 564)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(100, 14)
        Me.Label47.TabIndex = 27
        Me.Label47.Text = "Weight Change :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(274, 537)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(28, 13)
        Me.Label29.TabIndex = 26
        Me.Label29.Text = "(lbs)"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(274, 507)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(28, 13)
        Me.Label61.TabIndex = 23
        Me.Label61.Text = "(lbs)"
        '
        'txtPrePregWeight
        '
        Me.txtPrePregWeight.Location = New System.Drawing.Point(179, 529)
        Me.txtPrePregWeight.MaxLength = 6
        Me.txtPrePregWeight.Name = "txtPrePregWeight"
        Me.txtPrePregWeight.Size = New System.Drawing.Size(90, 22)
        Me.txtPrePregWeight.TabIndex = 27
        '
        'txtEdema
        '
        Me.txtEdema.Location = New System.Drawing.Point(179, 469)
        Me.txtEdema.MaxLength = 30
        Me.txtEdema.Name = "txtEdema"
        Me.txtEdema.Size = New System.Drawing.Size(90, 22)
        Me.txtEdema.TabIndex = 25
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(39, 534)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(138, 14)
        Me.Label30.TabIndex = 24
        Me.Label30.Text = "Pre Pregnancy Weight :"
        '
        'txtWeight
        '
        Me.txtWeight.Location = New System.Drawing.Point(179, 499)
        Me.txtWeight.MaxLength = 6
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(90, 22)
        Me.txtWeight.TabIndex = 26
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(77, 504)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(100, 14)
        Me.Label48.TabIndex = 21
        Me.Label48.Text = "Current Weight :"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(125, 474)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(52, 14)
        Me.Label55.TabIndex = 19
        Me.Label55.Text = "Edema :"
        '
        'pnlEffacement
        '
        Me.pnlEffacement.Controls.Add(Me.txtStation)
        Me.pnlEffacement.Controls.Add(Me.Label27)
        Me.pnlEffacement.Controls.Add(Me.Label70)
        Me.pnlEffacement.Controls.Add(Me.Label78)
        Me.pnlEffacement.Controls.Add(Me.txtDilation)
        Me.pnlEffacement.Controls.Add(Me.Label23)
        Me.pnlEffacement.Controls.Add(Me.Label25)
        Me.pnlEffacement.Controls.Add(Me.txtEffacement)
        Me.pnlEffacement.Controls.Add(Me.Label24)
        Me.pnlEffacement.Location = New System.Drawing.Point(18, 338)
        Me.pnlEffacement.Name = "pnlEffacement"
        Me.pnlEffacement.Size = New System.Drawing.Size(464, 58)
        Me.pnlEffacement.TabIndex = 23
        '
        'txtStation
        '
        Me.txtStation.Location = New System.Drawing.Point(405, 29)
        Me.txtStation.MaxLength = 30
        Me.txtStation.Name = "txtStation"
        Me.txtStation.Size = New System.Drawing.Size(44, 22)
        Me.txtStation.TabIndex = 2
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(348, 33)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(54, 14)
        Me.Label27.TabIndex = 7
        Me.Label27.Text = "Station :"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(122, 34)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(28, 13)
        Me.Label70.TabIndex = 3
        Me.Label70.Text = "(cm)"
        '
        'Label78
        '
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(0, 0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(464, 21)
        Me.Label78.TabIndex = 0
        Me.Label78.Text = "Cervical Exam"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDilation
        '
        Me.txtDilation.Location = New System.Drawing.Point(77, 29)
        Me.txtDilation.MaxLength = 6
        Me.txtDilation.Name = "txtDilation"
        Me.txtDilation.Size = New System.Drawing.Size(44, 22)
        Me.txtDilation.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(21, 33)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(54, 14)
        Me.Label23.TabIndex = 1
        Me.Label23.Text = "Dilation :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(303, 34)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(18, 13)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "%"
        '
        'txtEffacement
        '
        Me.txtEffacement.Location = New System.Drawing.Point(257, 29)
        Me.txtEffacement.MaxLength = 6
        Me.txtEffacement.Name = "txtEffacement"
        Me.txtEffacement.Size = New System.Drawing.Size(44, 22)
        Me.txtEffacement.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(176, 33)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(78, 14)
        Me.Label24.TabIndex = 4
        Me.Label24.Text = "Effacement :"
        '
        'cbPreTermLabor
        '
        Me.cbPreTermLabor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPreTermLabor.FormattingEnabled = True
        Me.cbPreTermLabor.Items.AddRange(New Object() {"Present", "Absent"})
        Me.cbPreTermLabor.Location = New System.Drawing.Point(179, 309)
        Me.cbPreTermLabor.Name = "cbPreTermLabor"
        Me.cbPreTermLabor.Size = New System.Drawing.Size(244, 22)
        Me.cbPreTermLabor.TabIndex = 22
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(77, 314)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(100, 14)
        Me.Label59.TabIndex = 16
        Me.Label59.Text = "Pre-Term Labor :"
        '
        'cbFetalMovement
        '
        Me.cbFetalMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFetalMovement.FormattingEnabled = True
        Me.cbFetalMovement.Items.AddRange(New Object() {"Present", "Absent", "Reduced"})
        Me.cbFetalMovement.Location = New System.Drawing.Point(179, 280)
        Me.cbFetalMovement.Name = "cbFetalMovement"
        Me.cbFetalMovement.Size = New System.Drawing.Size(244, 22)
        Me.cbFetalMovement.TabIndex = 21
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(74, 284)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(103, 14)
        Me.Label56.TabIndex = 14
        Me.Label56.Text = "Fetal Movement :"
        '
        'txtFetalheartrate
        '
        Me.txtFetalheartrate.Location = New System.Drawing.Point(179, 251)
        Me.txtFetalheartrate.MaxLength = 30
        Me.txtFetalheartrate.Name = "txtFetalheartrate"
        Me.txtFetalheartrate.Size = New System.Drawing.Size(244, 22)
        Me.txtFetalheartrate.TabIndex = 20
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(73, 255)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(104, 14)
        Me.Label54.TabIndex = 12
        Me.Label54.Text = "Fetal Heart Rate :"
        '
        'txtPresentation
        '
        Me.txtPresentation.Location = New System.Drawing.Point(179, 222)
        Me.txtPresentation.MaxLength = 60
        Me.txtPresentation.Name = "txtPresentation"
        Me.txtPresentation.Size = New System.Drawing.Size(244, 22)
        Me.txtPresentation.TabIndex = 19
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(93, 226)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(84, 14)
        Me.Label60.TabIndex = 10
        Me.Label60.Text = "Presentation :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(273, 198)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(28, 13)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "(cm)"
        '
        'txtPreferredGesAge
        '
        Me.txtPreferredGesAge.Location = New System.Drawing.Point(363, 11)
        Me.txtPreferredGesAge.MaxLength = 100
        Me.txtPreferredGesAge.Name = "txtPreferredGesAge"
        Me.txtPreferredGesAge.Size = New System.Drawing.Size(143, 22)
        Me.txtPreferredGesAge.TabIndex = 2
        Me.txtPreferredGesAge.TabStop = False
        Me.txtPreferredGesAge.Visible = False
        '
        'txtFundalHeight
        '
        Me.txtFundalHeight.Location = New System.Drawing.Point(179, 193)
        Me.txtFundalHeight.MaxLength = 5
        Me.txtFundalHeight.Name = "txtFundalHeight"
        Me.txtFundalHeight.Size = New System.Drawing.Size(90, 22)
        Me.txtFundalHeight.TabIndex = 18
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(87, 197)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(90, 14)
        Me.Label26.TabIndex = 7
        Me.Label26.Text = "Fundal Height :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(620, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 14)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Gestational Age :"
        '
        'lblTradeName
        '
        Me.lblTradeName.AutoSize = True
        Me.lblTradeName.ForeColor = System.Drawing.Color.Red
        Me.lblTradeName.Location = New System.Drawing.Point(619, 150)
        Me.lblTradeName.Name = "lblTradeName"
        Me.lblTradeName.Size = New System.Drawing.Size(14, 14)
        Me.lblTradeName.TabIndex = 42
        Me.lblTradeName.Text = "*"
        '
        'btnDelLMPEstDate
        '
        Me.btnDelLMPEstDate.AutoEllipsis = True
        Me.btnDelLMPEstDate.BackColor = System.Drawing.Color.Transparent
        Me.btnDelLMPEstDate.BackgroundImage = CType(resources.GetObject("btnDelLMPEstDate.BackgroundImage"), System.Drawing.Image)
        Me.btnDelLMPEstDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelLMPEstDate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnDelLMPEstDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnDelLMPEstDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDelLMPEstDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelLMPEstDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelLMPEstDate.Image = CType(resources.GetObject("btnDelLMPEstDate.Image"), System.Drawing.Image)
        Me.btnDelLMPEstDate.Location = New System.Drawing.Point(287, 118)
        Me.btnDelLMPEstDate.Name = "btnDelLMPEstDate"
        Me.btnDelLMPEstDate.Size = New System.Drawing.Size(22, 22)
        Me.btnDelLMPEstDate.TabIndex = 8
        Me.ToolTip.SetToolTip(Me.btnDelLMPEstDate, "Delete Last Menstrual Period")
        Me.btnDelLMPEstDate.UseVisualStyleBackColor = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlPreferedGesAge)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(979, 624)
        Me.pnlMain.TabIndex = 0
        '
        'pnltblStrip
        '
        Me.pnltblStrip.Controls.Add(Me.tblStrip)
        Me.pnltblStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltblStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnltblStrip.Name = "pnltblStrip"
        Me.pnltblStrip.Size = New System.Drawing.Size(979, 54)
        Me.pnltblStrip.TabIndex = 1
        Me.pnltblStrip.TabStop = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(315, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 14)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "+ 266 Days ="
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(315, 122)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "+ 280 Days ="
        '
        'frmOBVital
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(979, 678)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnltblStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOBVital"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OB Vitals"
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnlPreferedGesAge.ResumeLayout(False)
        Me.pnlPreferedGesAge.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlLiving.ResumeLayout(False)
        Me.pnlLiving.PerformLayout()
        Me.pnlPainLevelCurrent.ResumeLayout(False)
        Me.pnlPainLevelCurrent.PerformLayout()
        CType(Me.trbPainLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEffacement.ResumeLayout(False)
        Me.pnlEffacement.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnltblStrip.ResumeLayout(False)
        Me.pnltblStrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tblStrip As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Ok_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtVitals As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtBPSittingMax As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtBPSittingMin As System.Windows.Forms.TextBox
    Friend WithEvents label52 As System.Windows.Forms.Label
    Friend WithEvents label51 As System.Windows.Forms.Label
    Friend WithEvents lblbpsisys As System.Windows.Forms.Label
    Friend WithEvents lblbpsidia As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpConDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpConEstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUltraDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpLMPDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpLMPEstDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlPreferedGesAge As System.Windows.Forms.Panel
    Friend WithEvents txtPreferredGesAge As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlLiving As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents chkpain As System.Windows.Forms.CheckBox
    Private WithEvents pnlPainLevelCurrent As System.Windows.Forms.Panel
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents label36 As System.Windows.Forms.Label
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents label34 As System.Windows.Forms.Label
    Private WithEvents label33 As System.Windows.Forms.Label
    Private WithEvents label32 As System.Windows.Forms.Label
    Private WithEvents label31 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents label42 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents trbPainLevel As System.Windows.Forms.TrackBar
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPrePregWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtFundalHeight As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtDilation As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtStation As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents pnlEffacement As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtEffacement As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtWeightChange As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtFetalheartrate As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtUrineGlucose As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents txtUrineAlbumin As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents cbPreTermLabor As System.Windows.Forms.ComboBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents cbFetalMovement As System.Windows.Forms.ComboBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtEdema As System.Windows.Forms.TextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents txtPresentation As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnltblStrip As System.Windows.Forms.Panel
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents tblbtn_Assign_Task As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtNextAppointment As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents cbGADays As System.Windows.Forms.ComboBox
    Friend WithEvents cbGAWeeks As System.Windows.Forms.ComboBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents lblTradeName As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents chkUltraSound As System.Windows.Forms.CheckBox
    Friend WithEvents chkLMP As System.Windows.Forms.CheckBox
    Friend WithEvents chkConception As System.Windows.Forms.CheckBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents dtpEstDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents btnDelUltraDueDate As System.Windows.Forms.Button
    Private WithEvents btnDelEstDueDate As System.Windows.Forms.Button
    Private WithEvents btnDelConception As System.Windows.Forms.Button
    Private WithEvents btnDelLMPEstDate As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtobcomm As System.Windows.Forms.TextBox
    Friend WithEvents chkobrsk As System.Windows.Forms.CheckBox
    Friend WithEvents tls_ObHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_OBVitals As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_OBPlan As System.Windows.Forms.ToolStripButton
    Private WithEvents btnInternalBr As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
