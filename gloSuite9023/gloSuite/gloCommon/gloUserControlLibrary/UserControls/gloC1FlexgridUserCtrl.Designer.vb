<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloC1FlexgridUserCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If (IsNothing(cntListmenuStrip) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(cntListmenuStrip)
                    If (IsNothing(cntListmenuStrip.Items) = False) Then
                        cntListmenuStrip.Items.Clear()
                    End If
                    cntListmenuStrip.Dispose()
                    cntListmenuStrip = Nothing
                End If
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloC1FlexgridUserCtrl))
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.ImageFlex = New System.Windows.Forms.ImageList(Me.components)
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_Flex
        '
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(1, 0)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(580, 361)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 0
        '
        'ImageFlex
        '
        Me.ImageFlex.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageFlex.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageFlex.TransparentColor = System.Drawing.Color.Transparent
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 361)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(580, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 362)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(581, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 362)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(580, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'gloC1FlexgridUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me._Flex)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Name = "gloC1FlexgridUserCtrl"
        Me.Size = New System.Drawing.Size(582, 362)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Protected WithEvents ImageFlex As System.Windows.Forms.ImageList
    Protected WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label

End Class
