<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmATNALog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmATNALog))
        Me.ATNAWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'ATNAWebBrowser
        '
        Me.ATNAWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ATNAWebBrowser.Location = New System.Drawing.Point(0, 0)
        Me.ATNAWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.ATNAWebBrowser.Name = "ATNAWebBrowser"
        Me.ATNAWebBrowser.Size = New System.Drawing.Size(584, 516)
        Me.ATNAWebBrowser.TabIndex = 0
        '
        'frmATNALog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(584, 516)
        Me.Controls.Add(Me.ATNAWebBrowser)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmATNALog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ATNA Log"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ATNAWebBrowser As System.Windows.Forms.WebBrowser
End Class
