<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloHistoryUserCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloHistoryUserCtrl))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlHistory = New System.Windows.Forms.Panel
        Me.trvHistory = New System.Windows.Forms.TreeView
        Me.imgHistory = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlHistory)
        Me.pnlMain.Controls.Add(Me.pnlTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(246, 624)
        Me.pnlMain.TabIndex = 0
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.trvHistory)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHistory.Location = New System.Drawing.Point(0, 56)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlHistory.Size = New System.Drawing.Size(246, 568)
        Me.pnlHistory.TabIndex = 1
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHistory.ForeColor = System.Drawing.Color.Black
        Me.trvHistory.ImageIndex = 0
        Me.trvHistory.ImageList = Me.imgHistory
        Me.trvHistory.Indent = 20
        Me.trvHistory.ItemHeight = 20
        Me.trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.trvHistory.Name = "trvHistory"
        Me.trvHistory.SelectedImageIndex = 1
        Me.trvHistory.ShowLines = False
        Me.trvHistory.ShowNodeToolTips = True
        Me.trvHistory.Size = New System.Drawing.Size(243, 565)
        Me.trvHistory.TabIndex = 1
        '
        'imgHistory
        '
        Me.imgHistory.ImageStream = CType(resources.GetObject("imgHistory.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgHistory.TransparentColor = System.Drawing.Color.Transparent
        Me.imgHistory.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgHistory.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgHistory.Images.SetKeyName(2, "Current.ico")
        Me.imgHistory.Images.SetKeyName(3, "Current_Disable.ico")
        Me.imgHistory.Images.SetKeyName(4, "Yesterdays.ico")
        Me.imgHistory.Images.SetKeyName(5, "Yesterdays_Disable.ico")
        Me.imgHistory.Images.SetKeyName(6, "Last Week.ico")
        Me.imgHistory.Images.SetKeyName(7, "Last Week_Disable.ico")
        Me.imgHistory.Images.SetKeyName(8, "LastMonth.ico")
        Me.imgHistory.Images.SetKeyName(9, "LastMonth_Disable.ico")
        Me.imgHistory.Images.SetKeyName(10, "Older.ico")
        Me.imgHistory.Images.SetKeyName(11, "Older_Disable.ico")
        Me.imgHistory.Images.SetKeyName(12, "Template Category.ico")
        '
        'pnlTop
        '
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(246, 56)
        Me.pnlTop.TabIndex = 0
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Name = "ContextMenuStrip1"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'gloHistoryUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloHistoryUserCtrl"
        Me.Size = New System.Drawing.Size(246, 624)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents pnlMain As System.Windows.Forms.Panel
    Protected WithEvents pnlHistory As System.Windows.Forms.Panel
    Protected WithEvents pnlTop As System.Windows.Forms.Panel
    Protected WithEvents trvHistory As System.Windows.Forms.TreeView
    Protected WithEvents imgHistory As System.Windows.Forms.ImageList
    Protected WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip

End Class
