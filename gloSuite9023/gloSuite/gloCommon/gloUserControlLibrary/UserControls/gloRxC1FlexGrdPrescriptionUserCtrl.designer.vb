<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloRxC1FlexGrdPrescriptionUserCtrl
    Inherits gloUserControlLibrary.gloC1FlexgridUserCtrl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloRxC1FlexGrdPrescriptionUserCtrl))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_Flex
        '
        Me._Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me._Flex.BackColor = System.Drawing.Color.White
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(1, 1)
        Me._Flex.Rows.Count = 1
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(805, 392)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        '
        'ImageFlex
        '
        Me.ImageFlex.ImageStream = CType(resources.GetObject("ImageFlex.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageFlex.Images.SetKeyName(0, "Not Reimbursable.png")
        Me.ImageFlex.Images.SetKeyName(1, "Off Formulary.png")
        Me.ImageFlex.Images.SetKeyName(2, "On Formulary.png")
        Me.ImageFlex.Images.SetKeyName(3, "Preferred.png")
        Me.ImageFlex.Images.SetKeyName(4, "Unknown.png")
        Me.ImageFlex.Images.SetKeyName(5, "Delete RX01.ico")
        Me.ImageFlex.Images.SetKeyName(6, "Delete All RX01.ico")
        Me.ImageFlex.Images.SetKeyName(7, "Rx Status.ico")
        Me.ImageFlex.Images.SetKeyName(8, "Bibliography.png")
        Me.ImageFlex.Images.SetKeyName(9, "Patient reference material.ico")
        Me.ImageFlex.Images.SetKeyName(10, "Provider reference material.ico")
        Me.ImageFlex.Images.SetKeyName(11, "infobutton.ico")
        Me.ImageFlex.Images.SetKeyName(12, "Alternate drugs.ico")
        Me.ImageFlex.Images.SetKeyName(13, "ePARequestView.ico")
        Me.ImageFlex.Images.SetKeyName(14, "CancelRx.png")
        '
        'pnlMain
        '
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(1, 1)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(805, 392)
        Me.pnlMain.TabIndex = 0
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'gloRxC1FlexGrdPrescriptionUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "gloRxC1FlexGrdPrescriptionUserCtrl"
        Me.RowsCol = 1
        Me.Size = New System.Drawing.Size(807, 394)
        Me.Controls.SetChildIndex(Me.pnlMain, 0)
        Me.Controls.SetChildIndex(Me._Flex, 0)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    'Friend WithEvents DeletePrescriptionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents DeletePrescriptionItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
