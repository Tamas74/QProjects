<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientletterSecureMessage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientletterSecureMessage))
        Me.tls_OverDue = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbtnSend = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkAllowReply = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblportalDisplayname = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlPrintMessage = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblFormularyTransactionMessage = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbMessageSendto = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblportaldisplay = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblfrom = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAddAttachment = New System.Windows.Forms.Button()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.c1Group = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlwdcontrol = New System.Windows.Forms.Panel()
        Me.wdPatientLetter = New AxDSOFramer.AxFramerControl()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tls_OverDue.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlPrintMessage.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.c1Group, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlwdcontrol.SuspendLayout()
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tls_OverDue
        '
        Me.tls_OverDue.BackColor = System.Drawing.Color.Transparent
        Me.tls_OverDue.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_OverDue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_OverDue.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls_OverDue.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_OverDue.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbtnSend, Me.ts_btnClose})
        Me.tls_OverDue.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_OverDue.Location = New System.Drawing.Point(0, 0)
        Me.tls_OverDue.Name = "tls_OverDue"
        Me.tls_OverDue.Size = New System.Drawing.Size(758, 53)
        Me.tls_OverDue.TabIndex = 1
        Me.tls_OverDue.Text = "toolStrip1"
        '
        'tlbtnSend
        '
        Me.tlbtnSend.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbtnSend.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbtnSend.Image = CType(resources.GetObject("tlbtnSend.Image"), System.Drawing.Image)
        Me.tlbtnSend.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbtnSend.Name = "tlbtnSend"
        Me.tlbtnSend.Size = New System.Drawing.Size(42, 50)
        Me.tlbtnSend.Tag = "Select"
        Me.tlbtnSend.Text = "&Send"
        Me.tlbtnSend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(31, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Subject :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(27, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Message :"
        '
        'txtSubject
        '
        Me.txtSubject.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.Location = New System.Drawing.Point(90, 69)
        Me.txtSubject.MaxLength = 100
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(644, 22)
        Me.txtSubject.TabIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.Location = New System.Drawing.Point(90, 102)
        Me.txtMessage.MaxLength = 8000
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(644, 173)
        Me.txtMessage.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(10, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(219, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Send Secure Message to Patient"
        '
        'chkAllowReply
        '
        Me.chkAllowReply.AutoSize = True
        Me.chkAllowReply.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllowReply.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkAllowReply.Location = New System.Drawing.Point(90, 286)
        Me.chkAllowReply.Name = "chkAllowReply"
        Me.chkAllowReply.Size = New System.Drawing.Size(144, 18)
        Me.chkAllowReply.TabIndex = 2
        Me.chkAllowReply.Text = "Allow Patient to reply"
        Me.chkAllowReply.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(90, 472)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Note:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(131, 472)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(520, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "A maximum of three files can be attached and total file size should not be greate" & _
    "r than 4 MB."
        '
        'lblportalDisplayname
        '
        Me.lblportalDisplayname.AutoSize = True
        Me.lblportalDisplayname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblportalDisplayname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblportalDisplayname.Location = New System.Drawing.Point(505, 100)
        Me.lblportalDisplayname.Name = "lblportalDisplayname"
        Me.lblportalDisplayname.Size = New System.Drawing.Size(0, 15)
        Me.lblportalDisplayname.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.pnlwdcontrol)
        Me.Panel1.Controls.Add(Me.lblportalDisplayname)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 55)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(758, 501)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlPrintMessage)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.cmbMessageSendto)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.lblportaldisplay)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.chkAllowReply)
        Me.Panel3.Controls.Add(Me.lblfrom)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.txtMessage)
        Me.Panel3.Controls.Add(Me.btnAddAttachment)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.pnlGrid)
        Me.Panel3.Controls.Add(Me.txtSubject)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(758, 501)
        Me.Panel3.TabIndex = 1
        '
        'pnlPrintMessage
        '
        Me.pnlPrintMessage.BackColor = System.Drawing.Color.White
        Me.pnlPrintMessage.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlPrintMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPrintMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrintMessage.Controls.Add(Me.Label24)
        Me.pnlPrintMessage.Controls.Add(Me.lblFormularyTransactionMessage)
        Me.pnlPrintMessage.Location = New System.Drawing.Point(265, 216)
        Me.pnlPrintMessage.Name = "pnlPrintMessage"
        Me.pnlPrintMessage.Size = New System.Drawing.Size(294, 69)
        Me.pnlPrintMessage.TabIndex = 76
        Me.pnlPrintMessage.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(18, 8)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 19)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Please wait..."
        '
        'lblFormularyTransactionMessage
        '
        Me.lblFormularyTransactionMessage.AutoSize = True
        Me.lblFormularyTransactionMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblFormularyTransactionMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormularyTransactionMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFormularyTransactionMessage.Location = New System.Drawing.Point(19, 34)
        Me.lblFormularyTransactionMessage.Name = "lblFormularyTransactionMessage"
        Me.lblFormularyTransactionMessage.Size = New System.Drawing.Size(168, 16)
        Me.lblFormularyTransactionMessage.TabIndex = 61
        Me.lblFormularyTransactionMessage.Text = "Sending secure message"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(4, 497)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(750, 1)
        Me.Label12.TabIndex = 22
        '
        'cmbMessageSendto
        '
        Me.cmbMessageSendto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMessageSendto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMessageSendto.Items.AddRange(New Object() {"Patient", "Patient Representative", "Both"})
        Me.cmbMessageSendto.Location = New System.Drawing.Point(192, 311)
        Me.cmbMessageSendto.Name = "cmbMessageSendto"
        Me.cmbMessageSendto.Size = New System.Drawing.Size(151, 22)
        Me.cmbMessageSendto.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(4, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(750, 1)
        Me.Label11.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(90, 315)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 14)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Send Message to"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(754, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 495)
        Me.Label10.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 495)
        Me.Label9.TabIndex = 19
        '
        'lblportaldisplay
        '
        Me.lblportaldisplay.AutoSize = True
        Me.lblportaldisplay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblportaldisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblportaldisplay.Location = New System.Drawing.Point(416, 44)
        Me.lblportaldisplay.Name = "lblportaldisplay"
        Me.lblportaldisplay.Size = New System.Drawing.Size(0, 14)
        Me.lblportaldisplay.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(296, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(116, 14)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Portal Diplay Name :"
        '
        'lblfrom
        '
        Me.lblfrom.AutoSize = True
        Me.lblfrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblfrom.Location = New System.Drawing.Point(90, 44)
        Me.lblfrom.Name = "lblfrom"
        Me.lblfrom.Size = New System.Drawing.Size(34, 14)
        Me.lblfrom.TabIndex = 17
        Me.lblfrom.Text = "From"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(46, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "From :"
        '
        'btnAddAttachment
        '
        Me.btnAddAttachment.BackgroundImage = CType(resources.GetObject("btnAddAttachment.BackgroundImage"), System.Drawing.Image)
        Me.btnAddAttachment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddAttachment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddAttachment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddAttachment.Location = New System.Drawing.Point(90, 343)
        Me.btnAddAttachment.Name = "btnAddAttachment"
        Me.btnAddAttachment.Size = New System.Drawing.Size(113, 25)
        Me.btnAddAttachment.TabIndex = 4
        Me.btnAddAttachment.Text = "Add Attachment"
        Me.btnAddAttachment.UseVisualStyleBackColor = True
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.c1Group)
        Me.pnlGrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGrid.Location = New System.Drawing.Point(90, 376)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(415, 85)
        Me.pnlGrid.TabIndex = 15
        '
        'c1Group
        '
        Me.c1Group.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Group.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1Group.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Group.AutoResize = True
        Me.c1Group.BackColor = System.Drawing.Color.White
        Me.c1Group.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.c1Group.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1Group.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Group.ExtendLastCol = True
        Me.c1Group.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.c1Group.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Group.Location = New System.Drawing.Point(0, 0)
        Me.c1Group.Name = "c1Group"
        Me.c1Group.Rows.Count = 0
        Me.c1Group.Rows.DefaultSize = 19
        Me.c1Group.Rows.Fixed = 0
        Me.c1Group.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell
        Me.c1Group.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Group.Size = New System.Drawing.Size(415, 85)
        Me.c1Group.StyleInfo = resources.GetString("c1Group.StyleInfo")
        Me.c1Group.TabIndex = 2
        '
        'pnlwdcontrol
        '
        Me.pnlwdcontrol.Controls.Add(Me.wdPatientLetter)
        Me.pnlwdcontrol.Location = New System.Drawing.Point(661, 12)
        Me.pnlwdcontrol.Name = "pnlwdcontrol"
        Me.pnlwdcontrol.Size = New System.Drawing.Size(19, 54)
        Me.pnlwdcontrol.TabIndex = 20
        Me.pnlwdcontrol.Visible = False
        '
        'wdPatientLetter
        '
        Me.wdPatientLetter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientLetter.Enabled = True
        Me.wdPatientLetter.Location = New System.Drawing.Point(0, 0)
        Me.wdPatientLetter.Name = "wdPatientLetter"
        Me.wdPatientLetter.OcxState = CType(resources.GetObject("wdPatientLetter.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientLetter.Size = New System.Drawing.Size(19, 54)
        Me.wdPatientLetter.TabIndex = 1
        Me.wdPatientLetter.Visible = False
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "Small Close Tick.ico")
        Me.imgList.Images.SetKeyName(1, "About us.jpg")
        Me.imgList.Images.SetKeyName(2, "close-black.ico")
        Me.imgList.Images.SetKeyName(3, "SendMessage.ico")
        Me.imgList.Images.SetKeyName(4, "Forward Email.png")
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tls_OverDue)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(758, 55)
        Me.Panel2.TabIndex = 2
        Me.Panel2.TabStop = True
        '
        'frmPatientletterSecureMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(758, 556)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientletterSecureMessage"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Send Patient Message to Patient Portal"
        Me.tls_OverDue.ResumeLayout(False)
        Me.tls_OverDue.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlPrintMessage.ResumeLayout(False)
        Me.pnlPrintMessage.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.c1Group, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlwdcontrol.ResumeLayout(False)
        CType(Me.wdPatientLetter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tls_OverDue As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlbtnSend As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkAllowReply As System.Windows.Forms.CheckBox
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lblportalDisplayname As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAddAttachment As System.Windows.Forms.Button
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Public WithEvents c1Group As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents imgList As System.Windows.Forms.ImageList
    Private WithEvents lblportaldisplay As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lblfrom As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlwdcontrol As System.Windows.Forms.Panel
    Friend WithEvents wdPatientLetter As AxDSOFramer.AxFramerControl
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbMessageSendto As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlPrintMessage As System.Windows.Forms.Panel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblFormularyTransactionMessage As System.Windows.Forms.Label
End Class
