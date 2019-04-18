<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloMedRefillC1FlexGridUserCtrl
    Inherits gloUserControlLibrary.gloSearchC1FlexgridUserCtrl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            If (IsNothing(dvNext) = False) Then
                dvNext.Dispose()
                dvNext = Nothing
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloMedRefillC1FlexGridUserCtrl))
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImagSearchFlex
        '
        Me.ImagSearchFlex.ImageStream = CType(resources.GetObject("ImagSearchFlex.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImagSearchFlex.Images.SetKeyName(0, "Refill.ico")
        '
        'lblSearchString
        '
        Me.lblSearchString.Location = New System.Drawing.Point(298, 4)
        '
        'lblSearchOn
        '
        Me.lblSearchOn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchOn.Size = New System.Drawing.Size(64, 20)
        '
        '_Flex
        '
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(677, 385)
        '
        'btnCloseRefill
        '
        Me.btnCloseRefill.FlatAppearance.BorderSize = 0
        Me.btnCloseRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(269, 1)
        '
        'txtSearchFlexGrid
        '
        Me.txtSearchFlexGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchFlexGrid.Size = New System.Drawing.Size(156, 15)
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'gloMedRefillC1FlexGridUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.Name = "gloMedRefillC1FlexGridUserCtrl"
        Me.Controls.SetChildIndex(Me.pnlheader, 0)
        Me.Controls.SetChildIndex(Me.pnlSearch, 0)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip

End Class
