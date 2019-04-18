<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloToolStrip
    Inherits gloGlobal.gloToolStripIgnoreFocus

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    components.Dispose()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Try
                    Dim cntstripControls As System.Windows.Forms.ContextMenuStrip() = {Me.ContextMenuStrip, oContextMenu}
                    Dim cntControls As System.Windows.Forms.Control() = {Me.ContextMenuStrip, oContextMenu}
                    If Not IsNothing(cntstripControls) Then

                        If cntstripControls.Length > 0 Then

                            gloGlobal.cEventHelper.RemoveAllEventHandlers(cntstripControls)

                        End If
                    End If
                    If Not IsNothing(cntControls) Then
                        If (cntControls.Length > 0) Then
                            gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                        End If
                    End If

                    
                Catch

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
        Me.SuspendLayout()
        '
        'gloToolStrip
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImage = Global.gloToolStrip.My.Resources.Resources.Img_Toolstrip
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.Size = New System.Drawing.Size(375, 59)
        Me.ResumeLayout(False)

    End Sub

    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub
End Class
