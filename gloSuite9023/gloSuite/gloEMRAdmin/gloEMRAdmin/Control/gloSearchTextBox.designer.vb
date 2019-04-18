<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloSearchTextBox
    Inherits System.Windows.Forms.TextBox

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                If (IsNothing(oTimer) = False) Then
                    oTimer.Stop()
                    oTimer.Dispose()
                    oTimer = Nothing
                End If
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
        Me.components = New System.ComponentModel.Container
        Me.oTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'oTimer
        '
        '
        'gloSearchTextBox
        '
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oTimer As System.Windows.Forms.Timer

End Class
