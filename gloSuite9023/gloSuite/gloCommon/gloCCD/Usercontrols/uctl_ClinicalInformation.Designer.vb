<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uctl_ClinicalInformation
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
        Me.rtbClinicalInformation = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'rtbClinicalInformation
        '
        Me.rtbClinicalInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbClinicalInformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbClinicalInformation.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbClinicalInformation.Location = New System.Drawing.Point(0, 0)
        Me.rtbClinicalInformation.Name = "rtbClinicalInformation"
        Me.rtbClinicalInformation.Size = New System.Drawing.Size(961, 702)
        Me.rtbClinicalInformation.TabIndex = 0
        Me.rtbClinicalInformation.Text = ""
        '
        'uctl_ClinicalInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rtbClinicalInformation)
        Me.Name = "uctl_ClinicalInformation"
        Me.Size = New System.Drawing.Size(961, 702)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbClinicalInformation As System.Windows.Forms.RichTextBox

End Class
