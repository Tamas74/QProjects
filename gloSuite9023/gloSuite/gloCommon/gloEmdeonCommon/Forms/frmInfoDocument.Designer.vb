<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInfoDocument
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
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch ex As Exception

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInfoDocument))
        Me.pnlWord = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.wdPatientEducation = New AxDSOFramer.AxFramerControl()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PnlToolStrip = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtSelectedTemplates = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlSelectedTemplate = New System.Windows.Forms.Panel()
        Me.pnlWord.SuspendLayout()
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlToolStrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlSelectedTemplate.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlWord
        '
        Me.pnlWord.BackColor = System.Drawing.Color.Transparent
        Me.pnlWord.Controls.Add(Me.Label3)
        Me.pnlWord.Controls.Add(Me.Label2)
        Me.pnlWord.Controls.Add(Me.Label4)
        Me.pnlWord.Controls.Add(Me.Label5)
        Me.pnlWord.Controls.Add(Me.wdPatientEducation)
        Me.pnlWord.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWord.ForeColor = System.Drawing.Color.Black
        Me.pnlWord.Location = New System.Drawing.Point(0, 56)
        Me.pnlWord.Name = "pnlWord"
        Me.pnlWord.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWord.Size = New System.Drawing.Size(772, 482)
        Me.pnlWord.TabIndex = 42
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 474)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 478)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(765, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(768, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 475)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(766, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'wdPatientEducation
        '
        Me.wdPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPatientEducation.Enabled = True
        Me.wdPatientEducation.Location = New System.Drawing.Point(3, 3)
        Me.wdPatientEducation.Name = "wdPatientEducation"
        Me.wdPatientEducation.OcxState = CType(resources.GetObject("wdPatientEducation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPatientEducation.Size = New System.Drawing.Size(766, 476)
        Me.wdPatientEducation.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(765, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 25)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'PnlToolStrip
        '
        Me.PnlToolStrip.Controls.Add(Me.Label16)
        Me.PnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.PnlToolStrip.Name = "PnlToolStrip"
        Me.PnlToolStrip.Size = New System.Drawing.Size(772, 26)
        Me.PnlToolStrip.TabIndex = 44
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(14, 5)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 14)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "ToolStrip"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label16.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.txtSelectedTemplates)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(766, 27)
        Me.Panel2.TabIndex = 9
        '
        'txtSelectedTemplates
        '
        Me.txtSelectedTemplates.BackColor = System.Drawing.Color.White
        Me.txtSelectedTemplates.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSelectedTemplates.ForeColor = System.Drawing.Color.Black
        Me.txtSelectedTemplates.Location = New System.Drawing.Point(189, 1)
        Me.txtSelectedTemplates.Multiline = True
        Me.txtSelectedTemplates.Name = "txtSelectedTemplates"
        Me.txtSelectedTemplates.ReadOnly = True
        Me.txtSelectedTemplates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSelectedTemplates.Size = New System.Drawing.Size(615, 25)
        Me.txtSelectedTemplates.TabIndex = 0
        Me.txtSelectedTemplates.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(188, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Selected Template's :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(765, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 26)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(0, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(766, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'pnlSelectedTemplate
        '
        Me.pnlSelectedTemplate.Controls.Add(Me.Panel2)
        Me.pnlSelectedTemplate.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedTemplate.Location = New System.Drawing.Point(0, 26)
        Me.pnlSelectedTemplate.Name = "pnlSelectedTemplate"
        Me.pnlSelectedTemplate.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlSelectedTemplate.Size = New System.Drawing.Size(772, 30)
        Me.pnlSelectedTemplate.TabIndex = 43
        '
        'frmInfoDocument
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(772, 538)
        Me.Controls.Add(Me.pnlWord)
        Me.Controls.Add(Me.pnlSelectedTemplate)
        Me.Controls.Add(Me.PnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "frmInfoDocument"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Education Material"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlWord.ResumeLayout(False)
        CType(Me.wdPatientEducation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlToolStrip.ResumeLayout(False)
        Me.PnlToolStrip.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlSelectedTemplate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlWord As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents wdPatientEducation As AxDSOFramer.AxFramerControl
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtSelectedTemplates As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents pnlSelectedTemplate As System.Windows.Forms.Panel
End Class
