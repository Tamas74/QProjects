<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVo_Transcribe
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
                    If (IsNothing(OpenFileDialog1) = False) Then
                        OpenFileDialog1.Dispose()
                        OpenFileDialog1 = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVo_Transcribe))
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbTranscribeDialog = New System.Windows.Forms.RadioButton
        Me.rbTranscribeFileToFile = New System.Windows.Forms.RadioButton
        Me.rbTranscribeToText = New System.Windows.Forms.RadioButton
        Me.axDgnDictEdit1 = New AxDNSTools.AxDgnDictEdit
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_VoceCenter = New gloGlobal.gloToolStripIgnoreFocus
        Me.tls_btnTranscribe = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.axDgnDictEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_VoceCenter.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 1)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(542, 179)
        Me.RichTextBox1.TabIndex = 27
        Me.RichTextBox1.Text = ""
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(587, 26)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 24)
        Me.Button2.TabIndex = 26
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(215, 418)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(218, 48)
        Me.Button1.TabIndex = 25
        Me.Button1.Text = "Transcribe"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(37, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 14)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Output Text File File : "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Location of Wave File :"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(166, 26)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(415, 22)
        Me.TextBox1.TabIndex = 22
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbTranscribeDialog)
        Me.GroupBox1.Controls.Add(Me.rbTranscribeFileToFile)
        Me.GroupBox1.Controls.Add(Me.rbTranscribeToText)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(39, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(572, 75)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transcription Mode"
        '
        'rbTranscribeDialog
        '
        Me.rbTranscribeDialog.AutoSize = True
        Me.rbTranscribeDialog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTranscribeDialog.Location = New System.Drawing.Point(386, 30)
        Me.rbTranscribeDialog.Name = "rbTranscribeDialog"
        Me.rbTranscribeDialog.Size = New System.Drawing.Size(155, 18)
        Me.rbTranscribeDialog.TabIndex = 2
        Me.rbTranscribeDialog.TabStop = True
        Me.rbTranscribeDialog.Text = "Use Transcription Dialog"
        Me.rbTranscribeDialog.UseVisualStyleBackColor = True
        '
        'rbTranscribeFileToFile
        '
        Me.rbTranscribeFileToFile.AutoSize = True
        Me.rbTranscribeFileToFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTranscribeFileToFile.Location = New System.Drawing.Point(222, 30)
        Me.rbTranscribeFileToFile.Name = "rbTranscribeFileToFile"
        Me.rbTranscribeFileToFile.Size = New System.Drawing.Size(139, 18)
        Me.rbTranscribeFileToFile.TabIndex = 1
        Me.rbTranscribeFileToFile.TabStop = True
        Me.rbTranscribeFileToFile.Text = "Transcribe File to File"
        Me.rbTranscribeFileToFile.UseVisualStyleBackColor = True
        '
        'rbTranscribeToText
        '
        Me.rbTranscribeToText.AutoSize = True
        Me.rbTranscribeToText.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTranscribeToText.Location = New System.Drawing.Point(13, 30)
        Me.rbTranscribeToText.Name = "rbTranscribeToText"
        Me.rbTranscribeToText.Size = New System.Drawing.Size(179, 18)
        Me.rbTranscribeToText.TabIndex = 0
        Me.rbTranscribeToText.TabStop = True
        Me.rbTranscribeToText.Text = "Transcribe into Text Control"
        Me.rbTranscribeToText.UseVisualStyleBackColor = True
        '
        'axDgnDictEdit1
        '
        Me.axDgnDictEdit1.Enabled = True
        Me.axDgnDictEdit1.Location = New System.Drawing.Point(84, 152)
        Me.axDgnDictEdit1.Name = "axDgnDictEdit1"
        Me.axDgnDictEdit1.OcxState = CType(resources.GetObject("axDgnDictEdit1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axDgnDictEdit1.Size = New System.Drawing.Size(16, 15)
        Me.axDgnDictEdit1.TabIndex = 29
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(166, 65)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(415, 22)
        Me.TextBox2.TabIndex = 30
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_VoceCenter)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(628, 54)
        Me.pnl_tlsp.TabIndex = 31
        '
        'tlsp_VoceCenter
        '
        Me.tlsp_VoceCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsp_VoceCenter.BackgroundImage = CType(resources.GetObject("tlsp_VoceCenter.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_VoceCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_VoceCenter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_VoceCenter.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_VoceCenter.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_btnTranscribe, Me.ts_btnClose})
        Me.tlsp_VoceCenter.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_VoceCenter.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_VoceCenter.Name = "tlsp_VoceCenter"
        Me.tlsp_VoceCenter.Size = New System.Drawing.Size(628, 53)
        Me.tlsp_VoceCenter.TabIndex = 0
        Me.tlsp_VoceCenter.Text = "toolStrip1"
        '
        'tls_btnTranscribe
        '
        Me.tls_btnTranscribe.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnTranscribe.Image = CType(resources.GetObject("tls_btnTranscribe.Image"), System.Drawing.Image)
        Me.tls_btnTranscribe.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnTranscribe.Name = "tls_btnTranscribe"
        Me.tls_btnTranscribe.Size = New System.Drawing.Size(73, 50)
        Me.tls_btnTranscribe.Tag = "Transcribe"
        Me.tls_btnTranscribe.Text = "&Transcribe"
        Me.tls_btnTranscribe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.TextBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.axDgnDictEdit1)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(628, 410)
        Me.Panel2.TabIndex = 32
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.RichTextBox1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Location = New System.Drawing.Point(39, 210)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(542, 180)
        Me.Panel1.TabIndex = 31
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 179)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(540, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 179)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(541, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 179)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(542, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(4, 406)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(620, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 4)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 403)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(624, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 403)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 3)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(622, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'frmVo_Transcribe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(628, 464)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVo_Transcribe"
        Me.Text = "Transcribe Voice"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.axDgnDictEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_VoceCenter.ResumeLayout(False)
        Me.tlsp_VoceCenter.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbTranscribeDialog As System.Windows.Forms.RadioButton
    Friend WithEvents rbTranscribeFileToFile As System.Windows.Forms.RadioButton
    Friend WithEvents rbTranscribeToText As System.Windows.Forms.RadioButton
    Friend WithEvents axDgnDictEdit1 As AxDNSTools.AxDgnDictEdit
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_VoceCenter As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnTranscribe As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label

End Class
