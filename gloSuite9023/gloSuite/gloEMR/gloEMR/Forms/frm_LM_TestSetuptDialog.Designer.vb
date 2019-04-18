<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LM_TestSetuptDialog
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
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnAssociatedEMItem As System.Windows.Forms.Button
    Friend WithEvents txtLoincCode As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    components = New System.ComponentModel.Container
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Text = "frm_LM_SetupTestDialog"
    'End Sub
End Class
