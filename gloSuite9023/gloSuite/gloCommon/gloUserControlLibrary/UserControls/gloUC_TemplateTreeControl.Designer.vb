<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_TemplateTreeControl
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_TemplateTreeControl))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlTree = New System.Windows.Forms.Panel()
        Me.trvMain = New System.Windows.Forms.TreeView()
        Me.ImgTreecontrol = New System.Windows.Forms.ImageList(Me.components)
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.pnlTree.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlTree)
        Me.pnlMain.Controls.Add(Me.pnlSearch)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(150, 150)
        Me.pnlMain.TabIndex = 1
        '
        'pnlTree
        '
        Me.pnlTree.Controls.Add(Me.trvMain)
        Me.pnlTree.Controls.Add(Me.Label33)
        Me.pnlTree.Controls.Add(Me.Label31)
        Me.pnlTree.Controls.Add(Me.Label5)
        Me.pnlTree.Controls.Add(Me.Label6)
        Me.pnlTree.Controls.Add(Me.Label7)
        Me.pnlTree.Controls.Add(Me.Label8)
        Me.pnlTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTree.Location = New System.Drawing.Point(0, 26)
        Me.pnlTree.Name = "pnlTree"
        Me.pnlTree.Size = New System.Drawing.Size(150, 124)
        Me.pnlTree.TabIndex = 1
        '
        'trvMain
        '
        Me.trvMain.BackColor = System.Drawing.Color.White
        Me.trvMain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvMain.ForeColor = System.Drawing.Color.Black
        Me.trvMain.HideSelection = False
        Me.trvMain.ImageIndex = 0
        Me.trvMain.ImageList = Me.ImgTreecontrol
        Me.trvMain.Indent = 20
        Me.trvMain.ItemHeight = 20
        Me.trvMain.Location = New System.Drawing.Point(5, 5)
        Me.trvMain.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trvMain.Name = "trvMain"
        Me.trvMain.SelectedImageIndex = 0
        Me.trvMain.ShowLines = False
        Me.trvMain.ShowNodeToolTips = True
        Me.trvMain.Size = New System.Drawing.Size(144, 118)
        Me.trvMain.TabIndex = 1
        '
        'ImgTreecontrol
        '
        Me.ImgTreecontrol.ImageStream = CType(resources.GetObject("ImgTreecontrol.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgTreecontrol.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgTreecontrol.Images.SetKeyName(0, "My template.ico")
        Me.ImgTreecontrol.Images.SetKeyName(1, "Template Gallery.ico")
        Me.ImgTreecontrol.Images.SetKeyName(2, "Tempate Category_new.ico")
        Me.ImgTreecontrol.Images.SetKeyName(3, "Bullet05.ico")
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Location = New System.Drawing.Point(1, 5)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(4, 118)
        Me.Label33.TabIndex = 39
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Location = New System.Drawing.Point(1, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(148, 4)
        Me.Label31.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(148, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 123)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(149, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 123)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtsearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(150, 26)
        Me.pnlSearch.TabIndex = 0
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.ForeColor = System.Drawing.Color.Black
        Me.txtsearch.Location = New System.Drawing.Point(29, 4)
        Me.txtsearch.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(99, 15)
        Me.txtsearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(29, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(99, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(29, 17)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(99, 5)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(128, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(1, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(1, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(148, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(148, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(149, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'gloUC_TemplateTreeControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "gloUC_TemplateTreeControl"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlTree.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlTree As System.Windows.Forms.Panel
    Friend WithEvents trvMain As System.Windows.Forms.TreeView
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Public WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents ImgTreecontrol As System.Windows.Forms.ImageList

End Class
