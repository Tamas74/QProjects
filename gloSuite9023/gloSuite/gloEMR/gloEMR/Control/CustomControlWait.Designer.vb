<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomControlWait
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.pnlProcess = New System.Windows.Forms.Panel()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.lblProcess = New System.Windows.Forms.Label()
        Me.pnlProcess.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlProcess
        '
        Me.pnlProcess.AutoSize = True
        Me.pnlProcess.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlProcess.BackColor = System.Drawing.Color.White
        Me.pnlProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProcess.Controls.Add(Me.Label61)
        Me.pnlProcess.Controls.Add(Me.Label62)
        Me.pnlProcess.Controls.Add(Me.Label65)
        Me.pnlProcess.Controls.Add(Me.Label66)
        Me.pnlProcess.Controls.Add(Me.lblProcess)
        Me.pnlProcess.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.pnlProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProcess.Location = New System.Drawing.Point(0, 0)
        Me.pnlProcess.Name = "pnlProcess"
        Me.pnlProcess.Size = New System.Drawing.Size(206, 41)
        Me.pnlProcess.TabIndex = 28
        Me.pnlProcess.UseWaitCursor = True
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label61.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label61.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label61.Location = New System.Drawing.Point(205, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 39)
        Me.Label61.TabIndex = 23
        Me.Label61.UseWaitCursor = True
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label62.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label62.Location = New System.Drawing.Point(0, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 39)
        Me.Label62.TabIndex = 22
        Me.Label62.UseWaitCursor = True
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label65.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label65.Location = New System.Drawing.Point(0, 40)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(206, 1)
        Me.Label65.TabIndex = 21
        Me.Label65.UseWaitCursor = True
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label66.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label66.Location = New System.Drawing.Point(0, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(206, 1)
        Me.Label66.TabIndex = 19
        Me.Label66.UseWaitCursor = True
        '
        'lblProcess
        '
        Me.lblProcess.AutoEllipsis = True
        Me.lblProcess.AutoSize = True
        Me.lblProcess.BackColor = System.Drawing.Color.Transparent
        Me.lblProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblProcess.Font = New System.Drawing.Font("Georgia", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProcess.Location = New System.Drawing.Point(0, 0)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Padding = New System.Windows.Forms.Padding(5)
        Me.lblProcess.Size = New System.Drawing.Size(206, 41)
        Me.lblProcess.TabIndex = 17
        Me.lblProcess.Text = "Please wait..."
        Me.lblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProcess.UseWaitCursor = True
        '
        'CustomControlWait
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.pnlProcess)
        Me.Name = "CustomControlWait"
        Me.Size = New System.Drawing.Size(206, 41)
        Me.pnlProcess.ResumeLayout(False)
        Me.pnlProcess.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pnlProcess As System.Windows.Forms.Panel
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents lblProcess As System.Windows.Forms.Label

End Class
