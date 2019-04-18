<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

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
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        AxBlackIceDEVMODE1 = New AxBLACKICEDEVMODELib.AxBlackIceDEVMODE
        CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AxBlackIceDEVMODE1
        '
        AxBlackIceDEVMODE1.Enabled = True
        AxBlackIceDEVMODE1.Location = New System.Drawing.Point(352, 213)
        AxBlackIceDEVMODE1.Name = "AxBlackIceDEVMODE1"
        AxBlackIceDEVMODE1.OcxState = CType(resources.GetObject("AxBlackIceDEVMODE1.OcxState"), System.Windows.Forms.AxHost.State)
        AxBlackIceDEVMODE1.Size = New System.Drawing.Size(16, 16)
        AxBlackIceDEVMODE1.TabIndex = 0
        ''
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 266)
        Me.Controls.Add(AxBlackIceDEVMODE1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(AxBlackIceDEVMODE1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Public Shared WithEvents AxBlackIceDEVMODE1 As AxBLACKICEDEVMODELib.AxBlackIceDEVMODE
End Class
