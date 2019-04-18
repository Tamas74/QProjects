<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloListUsrCtrl
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
            If (IsNothing(ToolTip) = False) Then
                ToolTip.Dispose()
                ToolTip = Nothing
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
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloListUsrCtrl))
Me.pnlMain = New System.Windows.Forms.Panel
Me.pnlTreeView = New System.Windows.Forms.Panel
Me.lbl_pnlTop = New System.Windows.Forms.Label
Me.lbl_pnlBottom = New System.Windows.Forms.Label
Me.lbl_pnlLeft = New System.Windows.Forms.Label
Me.lbl_pnlRight = New System.Windows.Forms.Label
Me.trvList = New System.Windows.Forms.TreeView
Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
Me.pnlText = New System.Windows.Forms.Panel
Me.txtsearchDrug = New System.Windows.Forms.TextBox
Me.btnSearchClose = New System.Windows.Forms.Button
Me.Label20 = New System.Windows.Forms.Label
Me.Label21 = New System.Windows.Forms.Label
Me.PictureBox1 = New System.Windows.Forms.PictureBox
Me.label9 = New System.Windows.Forms.Label
Me.Label12 = New System.Windows.Forms.Label
Me.label10 = New System.Windows.Forms.Label
Me.label11 = New System.Windows.Forms.Label
Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
Me.pnlMain.SuspendLayout
Me.pnlTreeView.SuspendLayout
Me.pnlText.SuspendLayout
CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
Me.SuspendLayout
'
'pnlMain
'
Me.pnlMain.Controls.Add(Me.pnlTreeView)
Me.pnlMain.Controls.Add(Me.pnlText)
Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnlMain.Location = New System.Drawing.Point(0, 0)
Me.pnlMain.Name = "pnlMain"
Me.pnlMain.Size = New System.Drawing.Size(233, 778)
Me.pnlMain.TabIndex = 0
'
'pnlTreeView
'
Me.pnlTreeView.Controls.Add(Me.lbl_pnlTop)
Me.pnlTreeView.Controls.Add(Me.lbl_pnlBottom)
Me.pnlTreeView.Controls.Add(Me.lbl_pnlLeft)
Me.pnlTreeView.Controls.Add(Me.lbl_pnlRight)
Me.pnlTreeView.Controls.Add(Me.trvList)
Me.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnlTreeView.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.pnlTreeView.Location = New System.Drawing.Point(0, 30)
Me.pnlTreeView.Name = "pnlTreeView"
Me.pnlTreeView.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
Me.pnlTreeView.Size = New System.Drawing.Size(233, 748)
Me.pnlTreeView.TabIndex = 1
'
'lbl_pnlTop
'
Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
Me.lbl_pnlTop.Location = New System.Drawing.Point(4, 0)
Me.lbl_pnlTop.Name = "lbl_pnlTop"
Me.lbl_pnlTop.Size = New System.Drawing.Size(228, 1)
Me.lbl_pnlTop.TabIndex = 37
Me.lbl_pnlTop.Text = "label1"
'
'lbl_pnlBottom
'
Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 744)
Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
Me.lbl_pnlBottom.Size = New System.Drawing.Size(228, 1)
Me.lbl_pnlBottom.TabIndex = 8
Me.lbl_pnlBottom.Text = "label2"
'
'lbl_pnlLeft
'
Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 0)
Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 745)
Me.lbl_pnlLeft.TabIndex = 7
Me.lbl_pnlLeft.Text = "label4"
'
'lbl_pnlRight
'
Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
Me.lbl_pnlRight.Location = New System.Drawing.Point(232, 0)
Me.lbl_pnlRight.Name = "lbl_pnlRight"
Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 745)
Me.lbl_pnlRight.TabIndex = 6
Me.lbl_pnlRight.Text = "label3"
'
'trvList
'
Me.trvList.BackColor = System.Drawing.Color.White
Me.trvList.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.trvList.Dock = System.Windows.Forms.DockStyle.Fill
Me.trvList.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.trvList.ForeColor = System.Drawing.Color.Black
Me.trvList.ImageIndex = 0
Me.trvList.ImageList = Me.ImageList
Me.trvList.Indent = 20
Me.trvList.ItemHeight = 20
Me.trvList.Location = New System.Drawing.Point(3, 0)
Me.trvList.Name = "trvList"
Me.trvList.SelectedImageIndex = 0
Me.trvList.ShowNodeToolTips = true
Me.trvList.Size = New System.Drawing.Size(230, 745)
Me.trvList.TabIndex = 4
'
'ImageList
'
Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"),System.Windows.Forms.ImageListStreamer)
Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
Me.ImageList.Images.SetKeyName(0, "Bullet06.ico")
Me.ImageList.Images.SetKeyName(1, "arrow_01.ico")
'
'pnlText
'
Me.pnlText.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.pnlText.Controls.Add(Me.txtsearchDrug)
Me.pnlText.Controls.Add(Me.btnSearchClose)
Me.pnlText.Controls.Add(Me.Label20)
Me.pnlText.Controls.Add(Me.Label21)
Me.pnlText.Controls.Add(Me.PictureBox1)
Me.pnlText.Controls.Add(Me.label9)
Me.pnlText.Controls.Add(Me.Label12)
Me.pnlText.Controls.Add(Me.label10)
Me.pnlText.Controls.Add(Me.label11)
Me.pnlText.Dock = System.Windows.Forms.DockStyle.Top
Me.pnlText.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.pnlText.ForeColor = System.Drawing.Color.Black
Me.pnlText.Location = New System.Drawing.Point(0, 0)
Me.pnlText.Name = "pnlText"
Me.pnlText.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
Me.pnlText.Size = New System.Drawing.Size(233, 30)
Me.pnlText.TabIndex = 16
'
'txtsearchDrug
'
Me.txtsearchDrug.BackColor = System.Drawing.Color.White
Me.txtsearchDrug.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.txtsearchDrug.Dock = System.Windows.Forms.DockStyle.Fill
Me.txtsearchDrug.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.txtsearchDrug.ForeColor = System.Drawing.Color.Black
Me.txtsearchDrug.Location = New System.Drawing.Point(32, 8)
Me.txtsearchDrug.Name = "txtsearchDrug"
Me.txtsearchDrug.Size = New System.Drawing.Size(182, 15)
Me.txtsearchDrug.TabIndex = 0
'
'btnSearchClose
'
Me.btnSearchClose.BackColor = System.Drawing.Color.White
Me.btnSearchClose.Dock = System.Windows.Forms.DockStyle.Right
Me.btnSearchClose.FlatAppearance.BorderSize = 0
Me.btnSearchClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
Me.btnSearchClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
Me.btnSearchClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
Me.btnSearchClose.Image = CType(resources.GetObject("btnSearchClose.Image"),System.Drawing.Image)
Me.btnSearchClose.Location = New System.Drawing.Point(214, 8)
Me.btnSearchClose.Name = "btnSearchClose"
Me.btnSearchClose.Size = New System.Drawing.Size(18, 15)
Me.btnSearchClose.TabIndex = 41
Me.btnSearchClose.UseVisualStyleBackColor = false
'
'Label20
'
Me.Label20.BackColor = System.Drawing.Color.White
Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
Me.Label20.Location = New System.Drawing.Point(32, 4)
Me.Label20.Name = "Label20"
Me.Label20.Size = New System.Drawing.Size(200, 4)
Me.Label20.TabIndex = 37
'
'Label21
'
Me.Label21.BackColor = System.Drawing.Color.White
Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Label21.Location = New System.Drawing.Point(32, 23)
Me.Label21.Name = "Label21"
Me.Label21.Size = New System.Drawing.Size(200, 3)
Me.Label21.TabIndex = 38
'
'PictureBox1
'
Me.PictureBox1.BackColor = System.Drawing.Color.White
Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
Me.PictureBox1.Location = New System.Drawing.Point(4, 4)
Me.PictureBox1.Name = "PictureBox1"
Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
Me.PictureBox1.TabIndex = 9
Me.PictureBox1.TabStop = false
'
'label9
'
Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
Me.label9.Location = New System.Drawing.Point(4, 26)
Me.label9.Name = "label9"
Me.label9.Size = New System.Drawing.Size(228, 1)
Me.label9.TabIndex = 35
Me.label9.Text = "label1"
'
'Label12
'
Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
Me.Label12.Location = New System.Drawing.Point(4, 3)
Me.Label12.Name = "Label12"
Me.Label12.Size = New System.Drawing.Size(228, 1)
Me.Label12.TabIndex = 36
Me.Label12.Text = "label1"
'
'label10
'
Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.label10.Dock = System.Windows.Forms.DockStyle.Left
Me.label10.Location = New System.Drawing.Point(3, 3)
Me.label10.Name = "label10"
Me.label10.Size = New System.Drawing.Size(1, 24)
Me.label10.TabIndex = 39
Me.label10.Text = "label4"
'
'label11
'
Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.label11.Dock = System.Windows.Forms.DockStyle.Right
Me.label11.Location = New System.Drawing.Point(232, 3)
Me.label11.Name = "label11"
Me.label11.Size = New System.Drawing.Size(1, 24)
Me.label11.TabIndex = 40
Me.label11.Text = "label4"
'
'cntListmenuStrip
'
Me.cntListmenuStrip.Name = "cntListmenuStrip"
Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
'
'gloListUsrCtrl
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 14!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.Controls.Add(Me.pnlMain)
Me.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Name = "gloListUsrCtrl"
Me.Size = New System.Drawing.Size(233, 778)
Me.pnlMain.ResumeLayout(false)
Me.pnlTreeView.ResumeLayout(false)
Me.pnlText.ResumeLayout(false)
Me.pnlText.PerformLayout
CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
Me.ResumeLayout(false)

End Sub
    Protected WithEvents pnlMain As System.Windows.Forms.Panel
    Protected WithEvents txtsearchDrug As System.Windows.Forms.TextBox
    Protected WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Protected WithEvents ImageList As System.Windows.Forms.ImageList
    Protected WithEvents pnlTreeView As System.Windows.Forms.Panel
    Protected WithEvents trvList As System.Windows.Forms.TreeView
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Friend WithEvents pnlText As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents btnSearchClose As System.Windows.Forms.Button
End Class
