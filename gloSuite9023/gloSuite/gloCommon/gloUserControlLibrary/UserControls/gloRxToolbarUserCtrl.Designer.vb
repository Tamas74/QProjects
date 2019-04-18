<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloRxToolbarUserCtrl
    Inherits gloUserControlLibrary.gloToolBarUserCtrl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Overloads Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'pnlToolBar
        '
        Me.pnlToolBar.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolBar.Size = New System.Drawing.Size(735, 53)
        '
        'gloRxToolbarUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "gloRxToolbarUserCtrl"
        Me.Size = New System.Drawing.Size(735, 53)
        Me.ResumeLayout(False)

    End Sub

End Class
