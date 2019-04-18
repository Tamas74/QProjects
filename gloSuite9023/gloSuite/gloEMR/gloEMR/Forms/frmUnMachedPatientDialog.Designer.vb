<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUnMachedPatientDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnMachedPatientDialog))
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.BtnSelectDiff = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblCCDPatient = New System.Windows.Forms.Label()
        Me.lblDashboardPatient = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStop
        '
        Me.btnStop.BackgroundImage = CType(resources.GetObject("btnStop.BackgroundImage"), System.Drawing.Image)
        Me.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.Location = New System.Drawing.Point(131, 112)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(87, 25)
        Me.btnStop.TabIndex = 0
        Me.btnStop.Text = "Stop Import"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnContinue
        '
        Me.btnContinue.BackgroundImage = CType(resources.GetObject("btnContinue.BackgroundImage"), System.Drawing.Image)
        Me.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContinue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinue.Location = New System.Drawing.Point(233, 112)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(116, 25)
        Me.btnContinue.TabIndex = 1
        Me.btnContinue.Text = "Continue Import"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'BtnSelectDiff
        '
        Me.BtnSelectDiff.BackgroundImage = CType(resources.GetObject("BtnSelectDiff.BackgroundImage"), System.Drawing.Image)
        Me.BtnSelectDiff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnSelectDiff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BtnSelectDiff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BtnSelectDiff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectDiff.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectDiff.Location = New System.Drawing.Point(319, 75)
        Me.BtnSelectDiff.Name = "BtnSelectDiff"
        Me.BtnSelectDiff.Size = New System.Drawing.Size(151, 25)
        Me.BtnSelectDiff.TabIndex = 2
        Me.BtnSelectDiff.Text = "Select Different Patient"
        Me.BtnSelectDiff.UseVisualStyleBackColor = True
        Me.BtnSelectDiff.Visible = False
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(29, 22)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(146, 14)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "Possible Patient Mismatch"
        '
        'lblCCDPatient
        '
        Me.lblCCDPatient.AutoSize = True
        Me.lblCCDPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCDPatient.Location = New System.Drawing.Point(155, 51)
        Me.lblCCDPatient.Name = "lblCCDPatient"
        Me.lblCCDPatient.Size = New System.Drawing.Size(89, 14)
        Me.lblCCDPatient.TabIndex = 4
        Me.lblCCDPatient.Text = "Patient Name"
        '
        'lblDashboardPatient
        '
        Me.lblDashboardPatient.AutoSize = True
        Me.lblDashboardPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDashboardPatient.Location = New System.Drawing.Point(155, 75)
        Me.lblDashboardPatient.Name = "lblDashboardPatient"
        Me.lblDashboardPatient.Size = New System.Drawing.Size(89, 14)
        Me.lblDashboardPatient.TabIndex = 5
        Me.lblDashboardPatient.Text = "Patient Name"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblDashboardPatient)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblCCDPatient)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblMessage)
        Me.Panel1.Controls.Add(Me.BtnSelectDiff)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnContinue)
        Me.Panel1.Controls.Add(Me.btnStop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(480, 154)
        Me.Panel1.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 14)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Selected Patient :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(37, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 14)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Import File Patient :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(476, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 146)
        Me.Label4.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 146)
        Me.Label3.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(474, 1)
        Me.Label2.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(474, 1)
        Me.Label1.TabIndex = 7
        '
        'frmUnMachedPatientDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(480, 154)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUnMachedPatientDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Patient Mismatch"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents BtnSelectDiff As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents lblCCDPatient As System.Windows.Forms.Label
    Friend WithEvents lblDashboardPatient As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
