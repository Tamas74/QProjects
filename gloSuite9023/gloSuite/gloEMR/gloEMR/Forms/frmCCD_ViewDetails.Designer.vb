<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCD_ViewDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCD_ViewDetails))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.pnlFill = New System.Windows.Forms.Panel
        Me.wdMessages = New AxDSOFramer.AxFramerControl
        Me.pnlFill.SuspendLayout()
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(799, 57)
        Me.pnlTop.TabIndex = 0
        '
        'pnlFill
        '
        Me.pnlFill.Controls.Add(Me.wdMessages)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 57)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(799, 597)
        Me.pnlFill.TabIndex = 1
        '
        'wdMessages
        '
        Me.wdMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdMessages.Enabled = True
        Me.wdMessages.Location = New System.Drawing.Point(0, 0)
        Me.wdMessages.Name = "wdMessages"
        Me.wdMessages.OcxState = CType(resources.GetObject("wdMessages.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdMessages.Size = New System.Drawing.Size(799, 597)
        Me.wdMessages.TabIndex = 5
        '
        'frmCCD_ViewDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 654)
        Me.Controls.Add(Me.pnlFill)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCCD_ViewDetails"
        Me.Text = "CCD Details"
        Me.pnlFill.ResumeLayout(False)
        CType(Me.wdMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents wdMessages As AxDSOFramer.AxFramerControl
End Class
