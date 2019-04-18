<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKnoosJR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmKnoosJR))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chknon = New System.Windows.Forms.CheckBox()
        Me.chkmild = New System.Windows.Forms.CheckBox()
        Me.chkmod = New System.Windows.Forms.CheckBox()
        Me.chksev = New System.Windows.Forms.CheckBox()
        Me.chkext = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(136, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(400, 75)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(136, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(400, 41)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "What amount of knee pain have you experienced the last week during the following " & _
    "activities?"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Instructions"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Pain"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(136, 187)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(400, 41)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "1 :Twisting/Pivoting on your knee"
        '
        'chknon
        '
        Me.chknon.AutoSize = True
        Me.chknon.Location = New System.Drawing.Point(15, 236)
        Me.chknon.Name = "chknon"
        Me.chknon.Size = New System.Drawing.Size(55, 18)
        Me.chknon.TabIndex = 5
        Me.chknon.Text = "None"
        Me.chknon.UseVisualStyleBackColor = True
        '
        'chkmild
        '
        Me.chkmild.AutoSize = True
        Me.chkmild.Location = New System.Drawing.Point(117, 236)
        Me.chkmild.Name = "chkmild"
        Me.chkmild.Size = New System.Drawing.Size(46, 18)
        Me.chkmild.TabIndex = 6
        Me.chkmild.Text = "Mild"
        Me.chkmild.UseVisualStyleBackColor = True
        '
        'chkmod
        '
        Me.chkmod.AutoSize = True
        Me.chkmod.Location = New System.Drawing.Point(239, 236)
        Me.chkmod.Name = "chkmod"
        Me.chkmod.Size = New System.Drawing.Size(78, 18)
        Me.chkmod.TabIndex = 7
        Me.chkmod.Text = "Moderate"
        Me.chkmod.UseVisualStyleBackColor = True
        '
        'chksev
        '
        Me.chksev.AutoSize = True
        Me.chksev.Location = New System.Drawing.Point(343, 236)
        Me.chksev.Name = "chksev"
        Me.chksev.Size = New System.Drawing.Size(64, 18)
        Me.chksev.TabIndex = 8
        Me.chksev.Text = "Severe"
        Me.chksev.UseVisualStyleBackColor = True
        '
        'chkext
        '
        Me.chkext.AutoSize = True
        Me.chkext.Location = New System.Drawing.Point(450, 236)
        Me.chkext.Name = "chkext"
        Me.chkext.Size = New System.Drawing.Size(72, 18)
        Me.chkext.TabIndex = 9
        Me.chkext.Text = "Extreme"
        Me.chkext.UseVisualStyleBackColor = True
        '
        'frmKnoosJR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(743, 607)
        Me.Controls.Add(Me.chkext)
        Me.Controls.Add(Me.chksev)
        Me.Controls.Add(Me.chkmod)
        Me.Controls.Add(Me.chkmild)
        Me.Controls.Add(Me.chknon)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmKnoosJR"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Screening Test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chknon As System.Windows.Forms.CheckBox
    Friend WithEvents chkmild As System.Windows.Forms.CheckBox
    Friend WithEvents chkmod As System.Windows.Forms.CheckBox
    Friend WithEvents chksev As System.Windows.Forms.CheckBox
    Friend WithEvents chkext As System.Windows.Forms.CheckBox
End Class
