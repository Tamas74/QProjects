<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloAcceleratorEPA
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.webBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'webBrowser
        '
        Me.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser.Location = New System.Drawing.Point(0, 0)
        Me.webBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webBrowser.Name = "webBrowser"
        Me.webBrowser.Size = New System.Drawing.Size(570, 340)
        Me.webBrowser.TabIndex = 0
        '
        'gloRxEPAUserControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.webBrowser)
        Me.Name = "gloRxEPAUserControl"
        Me.Size = New System.Drawing.Size(570, 340)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents webBrowser As System.Windows.Forms.WebBrowser

End Class
