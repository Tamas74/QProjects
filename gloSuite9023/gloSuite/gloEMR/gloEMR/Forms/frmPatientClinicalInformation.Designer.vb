<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientClinicalInformation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientClinicalInformation))
        Me.pnlClinicalInfo = New System.Windows.Forms.Panel()
        Me.pnlBrowser = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlPreviewCommand = New System.Windows.Forms.Panel()
        Me.lblPreviewStatus = New System.Windows.Forms.Label()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pnlUnsentlabel = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblUnmatchMessage = New System.Windows.Forms.Label()
        Me.tlsToolstripTop = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsSave = New System.Windows.Forms.ToolStripButton()
        Me.tlsClose = New System.Windows.Forms.ToolStripButton()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.pnlClinicalInfo.SuspendLayout()
        Me.pnlBrowser.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlPreviewCommand.SuspendLayout()
        Me.pnlUnsentlabel.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlTitle.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.tlsToolstripTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlClinicalInfo
        '
        Me.pnlClinicalInfo.Controls.Add(Me.pnlBrowser)
        Me.pnlClinicalInfo.Controls.Add(Me.Panel3)
        Me.pnlClinicalInfo.Controls.Add(Me.Label2)
        Me.pnlClinicalInfo.Controls.Add(Me.Label3)
        Me.pnlClinicalInfo.Controls.Add(Me.Label4)
        Me.pnlClinicalInfo.Controls.Add(Me.Label9)
        Me.pnlClinicalInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClinicalInfo.Location = New System.Drawing.Point(0, 53)
        Me.pnlClinicalInfo.Name = "pnlClinicalInfo"
        Me.pnlClinicalInfo.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlClinicalInfo.Size = New System.Drawing.Size(948, 291)
        Me.pnlClinicalInfo.TabIndex = 2
        '
        'pnlBrowser
        '
        Me.pnlBrowser.Controls.Add(Me.WebBrowser1)
        Me.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBrowser.Location = New System.Drawing.Point(4, 43)
        Me.pnlBrowser.Name = "pnlBrowser"
        Me.pnlBrowser.Size = New System.Drawing.Size(940, 247)
        Me.pnlBrowser.TabIndex = 14
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(940, 247)
        Me.WebBrowser1.TabIndex = 14
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.txtSource)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(940, 39)
        Me.Panel3.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(0, 38)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(940, 1)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "label2"
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(67, 8)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(396, 22)
        Me.txtSource.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Source :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 290)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(940, 1)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 287)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(944, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 287)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(942, 1)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label1"
        '
        'pnlPreview
        '
        Me.pnlPreview.BackColor = System.Drawing.Color.White
        Me.pnlPreview.Controls.Add(Me.Panel4)
        Me.pnlPreview.Controls.Add(Me.Label26)
        Me.pnlPreview.Controls.Add(Me.Label27)
        Me.pnlPreview.Controls.Add(Me.Label28)
        Me.pnlPreview.Controls.Add(Me.Label29)
        Me.pnlPreview.Controls.Add(Me.pnlUnsentlabel)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPreview.ForeColor = System.Drawing.Color.White
        Me.pnlPreview.Location = New System.Drawing.Point(0, 348)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPreview.Size = New System.Drawing.Size(948, 355)
        Me.pnlPreview.TabIndex = 25
        Me.pnlPreview.Visible = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.pnlPreviewCommand)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(4, 43)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(940, 28)
        Me.Panel4.TabIndex = 24
        '
        'pnlPreviewCommand
        '
        Me.pnlPreviewCommand.BackColor = System.Drawing.Color.Transparent
        Me.pnlPreviewCommand.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlPreviewCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPreviewCommand.Controls.Add(Me.lblPreviewStatus)
        Me.pnlPreviewCommand.Controls.Add(Me.btnLast)
        Me.pnlPreviewCommand.Controls.Add(Me.btnNext)
        Me.pnlPreviewCommand.Controls.Add(Me.btnPrevious)
        Me.pnlPreviewCommand.Controls.Add(Me.btnFirst)
        Me.pnlPreviewCommand.Controls.Add(Me.Label22)
        Me.pnlPreviewCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPreviewCommand.Location = New System.Drawing.Point(0, 0)
        Me.pnlPreviewCommand.Name = "pnlPreviewCommand"
        Me.pnlPreviewCommand.Size = New System.Drawing.Size(940, 28)
        Me.pnlPreviewCommand.TabIndex = 0
        '
        'lblPreviewStatus
        '
        Me.lblPreviewStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblPreviewStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPreviewStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreviewStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPreviewStatus.Location = New System.Drawing.Point(96, 0)
        Me.lblPreviewStatus.Name = "lblPreviewStatus"
        Me.lblPreviewStatus.Size = New System.Drawing.Size(844, 27)
        Me.lblPreviewStatus.TabIndex = 20
        Me.lblPreviewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnLast
        '
        Me.btnLast.BackColor = System.Drawing.Color.Transparent
        Me.btnLast.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnLast.FlatAppearance.BorderSize = 0
        Me.btnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLast.Image = CType(resources.GetObject("btnLast.Image"), System.Drawing.Image)
        Me.btnLast.Location = New System.Drawing.Point(72, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(24, 27)
        Me.btnLast.TabIndex = 3
        Me.btnLast.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Transparent
        Me.btnNext.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(48, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(24, 27)
        Me.btnNext.TabIndex = 2
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnPrevious
        '
        Me.btnPrevious.BackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPrevious.FlatAppearance.BorderSize = 0
        Me.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), System.Drawing.Image)
        Me.btnPrevious.Location = New System.Drawing.Point(24, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(24, 27)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.UseVisualStyleBackColor = False
        '
        'btnFirst
        '
        Me.btnFirst.BackColor = System.Drawing.Color.Transparent
        Me.btnFirst.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnFirst.FlatAppearance.BorderSize = 0
        Me.btnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), System.Drawing.Image)
        Me.btnFirst.Location = New System.Drawing.Point(0, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(24, 27)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(0, 27)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(940, 1)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(4, 351)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(940, 1)
        Me.Label26.TabIndex = 26
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 43)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 309)
        Me.Label27.TabIndex = 25
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(944, 43)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 309)
        Me.Label28.TabIndex = 24
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(3, 42)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(942, 1)
        Me.Label29.TabIndex = 23
        Me.Label29.Text = "label1"
        '
        'pnlUnsentlabel
        '
        Me.pnlUnsentlabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUnsentlabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnsentlabel.Controls.Add(Me.Panel7)
        Me.pnlUnsentlabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnsentlabel.Location = New System.Drawing.Point(3, 0)
        Me.pnlUnsentlabel.Name = "pnlUnsentlabel"
        Me.pnlUnsentlabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlUnsentlabel.Size = New System.Drawing.Size(942, 42)
        Me.pnlUnsentlabel.TabIndex = 27
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel7.Controls.Add(Me.Label112)
        Me.Panel7.Controls.Add(Me.Label113)
        Me.Panel7.Controls.Add(Me.Label115)
        Me.Panel7.Controls.Add(Me.Label114)
        Me.Panel7.Controls.Add(Me.Label106)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(942, 39)
        Me.Panel7.TabIndex = 31
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label112.Enabled = False
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Location = New System.Drawing.Point(1, 38)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(940, 1)
        Me.Label112.TabIndex = 27
        Me.Label112.Text = "From"
        Me.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label113.Enabled = False
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Location = New System.Drawing.Point(1, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(940, 1)
        Me.Label113.TabIndex = 28
        Me.Label113.Text = "From"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label115.Enabled = False
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Location = New System.Drawing.Point(0, 0)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 39)
        Me.Label115.TabIndex = 30
        Me.Label115.Text = "From"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label114.Enabled = False
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Location = New System.Drawing.Point(941, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1, 39)
        Me.Label114.TabIndex = 29
        Me.Label114.Text = "From"
        Me.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.BackColor = System.Drawing.Color.Transparent
        Me.Label106.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label106.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label106.Location = New System.Drawing.Point(7, 10)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(939, 14)
        Me.Label106.TabIndex = 15
        Me.Label106.Text = "There is no discrete clinical data available for this patient. Instead the follow" & _
    "ing file in PDF format is present which can be imported in the DMS module."
        Me.Label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlTitle
        '
        Me.pnlTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTitle.Controls.Add(Me.Panel2)
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTitle.Size = New System.Drawing.Size(948, 30)
        Me.pnlTitle.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(942, 24)
        Me.Panel2.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(940, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(940, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Clinical Information "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(941, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(942, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblUnmatchMessage)
        Me.pnlTop.Controls.Add(Me.tlsToolstripTop)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(948, 53)
        Me.pnlTop.TabIndex = 2
        '
        'lblUnmatchMessage
        '
        Me.lblUnmatchMessage.AutoSize = True
        Me.lblUnmatchMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblUnmatchMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnmatchMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblUnmatchMessage.Location = New System.Drawing.Point(229, 23)
        Me.lblUnmatchMessage.Name = "lblUnmatchMessage"
        Me.lblUnmatchMessage.Size = New System.Drawing.Size(568, 14)
        Me.lblUnmatchMessage.TabIndex = 4
        Me.lblUnmatchMessage.Text = "This patient has not been found in the system.  A task will be sent to the CCD de" & _
    "fault user"
        Me.lblUnmatchMessage.Visible = False
        '
        'tlsToolstripTop
        '
        Me.tlsToolstripTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsToolstripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsToolstripTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsToolstripTop.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsToolstripTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsSave, Me.tlsClose})
        Me.tlsToolstripTop.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsToolstripTop.Location = New System.Drawing.Point(0, 0)
        Me.tlsToolstripTop.Name = "tlsToolstripTop"
        Me.tlsToolstripTop.Size = New System.Drawing.Size(948, 53)
        Me.tlsToolstripTop.TabIndex = 3
        Me.tlsToolstripTop.Text = "ToolStrip1"
        '
        'tlsSave
        '
        Me.tlsSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsSave.Image = CType(resources.GetObject("tlsSave.Image"), System.Drawing.Image)
        Me.tlsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsSave.Name = "tlsSave"
        Me.tlsSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsSave.Tag = "Save"
        Me.tlsSave.Text = "&Save&&Cls"
        Me.tlsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsSave.ToolTipText = "Save and Close"
        Me.tlsSave.Visible = False
        '
        'tlsClose
        '
        Me.tlsClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsClose.Image = CType(resources.GetObject("tlsClose.Image"), System.Drawing.Image)
        Me.tlsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsClose.Name = "tlsClose"
        Me.tlsClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsClose.Tag = "Close"
        Me.tlsClose.Text = "&Close"
        Me.tlsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(0, 344)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(948, 4)
        Me.Splitter3.TabIndex = 27
        Me.Splitter3.TabStop = False
        '
        'frmPatientClinicalInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(948, 703)
        Me.Controls.Add(Me.pnlClinicalInfo)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.pnlPreview)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlTitle)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientClinicalInformation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinical Information"
        Me.pnlClinicalInfo.ResumeLayout(False)
        Me.pnlBrowser.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlPreview.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlPreviewCommand.ResumeLayout(False)
        Me.pnlUnsentlabel.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.pnlTitle.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tlsToolstripTop.ResumeLayout(False)
        Me.tlsToolstripTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tlsToolstripTop As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlClinicalInfo As System.Windows.Forms.Panel
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tlsSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents lblUnmatchMessage As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlBrowser As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents pnlPreviewCommand As System.Windows.Forms.Panel
    Friend WithEvents lblPreviewStatus As System.Windows.Forms.Label
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents pnlUnsentlabel As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
End Class
