<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_LabHistory
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
                If (IsNothing(trvHistory.ContextMenuStrip) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(trvHistory.ContextMenuStrip)
                    If (IsNothing(trvHistory.ContextMenuStrip.Items) = False) Then
                        trvHistory.ContextMenuStrip.Items.Clear()
                    End If
                    trvHistory.ContextMenuStrip.Dispose()
                    trvHistory.ContextMenuStrip = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_LabHistory))
        Me.cmbSearchCriteria = New System.Windows.Forms.ComboBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.trvHistory = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImgPreview = New System.Windows.Forms.PictureBox
        Me.ImgFax = New System.Windows.Forms.PictureBox
        Me.ImgPrint = New System.Windows.Forms.PictureBox
        Me.ImgDelete = New System.Windows.Forms.PictureBox
        Me.ImgModify = New System.Windows.Forms.PictureBox
        Me.ImgNew = New System.Windows.Forms.PictureBox
        Me.pnl_trvHistory = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.pnlCmbSearchCriteria = New System.Windows.Forms.Panel
        Me.pnltextSearch = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        CType(Me.ImgPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgFax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgModify, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_trvHistory.SuspendLayout()
        Me.pnlCmbSearchCriteria.SuspendLayout()
        Me.pnltextSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbSearchCriteria
        '
        Me.cmbSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSearchCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchCriteria.FormattingEnabled = True
        Me.cmbSearchCriteria.Location = New System.Drawing.Point(0, 0)
        Me.cmbSearchCriteria.Name = "cmbSearchCriteria"
        Me.cmbSearchCriteria.Size = New System.Drawing.Size(232, 22)
        Me.cmbSearchCriteria.TabIndex = 1
        Me.cmbSearchCriteria.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(202, 15)
        Me.txtSearch.TabIndex = 11
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHistory.ForeColor = System.Drawing.Color.Black
        Me.trvHistory.ImageIndex = 0
        Me.trvHistory.ImageList = Me.ImageList1
        Me.trvHistory.Indent = 20
        Me.trvHistory.ItemHeight = 20
        Me.trvHistory.Location = New System.Drawing.Point(1, 1)
        Me.trvHistory.Name = "trvHistory"
        Me.trvHistory.SelectedImageIndex = 0
        Me.trvHistory.ShowNodeToolTips = True
        Me.trvHistory.Size = New System.Drawing.Size(230, 484)
        Me.trvHistory.TabIndex = 18
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "breakPoint.png")
        Me.ImageList1.Images.SetKeyName(1, "Current.ico")
        Me.ImageList1.Images.SetKeyName(2, "Yesterdays.ico")
        Me.ImageList1.Images.SetKeyName(3, "Last Week.ico")
        Me.ImageList1.Images.SetKeyName(4, "LastMonth.ico")
        Me.ImageList1.Images.SetKeyName(5, "Olders.ico")
        Me.ImageList1.Images.SetKeyName(6, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(7, "Table Align.ico")
        Me.ImageList1.Images.SetKeyName(8, "BreakpointHS.png")
        Me.ImageList1.Images.SetKeyName(9, "Calendar_scheduleHS.png")
        Me.ImageList1.Images.SetKeyName(10, "compareversionsHS.png")
        Me.ImageList1.Images.SetKeyName(11, "book_reportHS.png")
        Me.ImageList1.Images.SetKeyName(12, "MonthlyViewHS.png")
        Me.ImageList1.Images.SetKeyName(13, "ActualSizeHS.png")
        Me.ImageList1.Images.SetKeyName(14, "AlignTableCellMiddleLeftJustHS.png")
        '
        'ImgPreview
        '
        Me.ImgPreview.Image = CType(resources.GetObject("ImgPreview.Image"), System.Drawing.Image)
        Me.ImgPreview.Location = New System.Drawing.Point(154, 39)
        Me.ImgPreview.Name = "ImgPreview"
        Me.ImgPreview.Size = New System.Drawing.Size(20, 20)
        Me.ImgPreview.TabIndex = 28
        Me.ImgPreview.TabStop = False
        Me.ImgPreview.Visible = False
        '
        'ImgFax
        '
        Me.ImgFax.Image = CType(resources.GetObject("ImgFax.Image"), System.Drawing.Image)
        Me.ImgFax.Location = New System.Drawing.Point(128, 39)
        Me.ImgFax.Name = "ImgFax"
        Me.ImgFax.Size = New System.Drawing.Size(20, 20)
        Me.ImgFax.TabIndex = 27
        Me.ImgFax.TabStop = False
        Me.ImgFax.Visible = False
        '
        'ImgPrint
        '
        Me.ImgPrint.Image = CType(resources.GetObject("ImgPrint.Image"), System.Drawing.Image)
        Me.ImgPrint.Location = New System.Drawing.Point(102, 39)
        Me.ImgPrint.Name = "ImgPrint"
        Me.ImgPrint.Size = New System.Drawing.Size(20, 20)
        Me.ImgPrint.TabIndex = 26
        Me.ImgPrint.TabStop = False
        Me.ImgPrint.Visible = False
        '
        'ImgDelete
        '
        Me.ImgDelete.Image = CType(resources.GetObject("ImgDelete.Image"), System.Drawing.Image)
        Me.ImgDelete.Location = New System.Drawing.Point(76, 39)
        Me.ImgDelete.Name = "ImgDelete"
        Me.ImgDelete.Size = New System.Drawing.Size(20, 20)
        Me.ImgDelete.TabIndex = 25
        Me.ImgDelete.TabStop = False
        Me.ImgDelete.Visible = False
        '
        'ImgModify
        '
        Me.ImgModify.Image = CType(resources.GetObject("ImgModify.Image"), System.Drawing.Image)
        Me.ImgModify.Location = New System.Drawing.Point(50, 39)
        Me.ImgModify.Name = "ImgModify"
        Me.ImgModify.Size = New System.Drawing.Size(20, 20)
        Me.ImgModify.TabIndex = 24
        Me.ImgModify.TabStop = False
        Me.ImgModify.Visible = False
        '
        'ImgNew
        '
        Me.ImgNew.Image = CType(resources.GetObject("ImgNew.Image"), System.Drawing.Image)
        Me.ImgNew.Location = New System.Drawing.Point(24, 39)
        Me.ImgNew.Name = "ImgNew"
        Me.ImgNew.Size = New System.Drawing.Size(20, 20)
        Me.ImgNew.TabIndex = 23
        Me.ImgNew.TabStop = False
        Me.ImgNew.Visible = False
        '
        'pnl_trvHistory
        '
        Me.pnl_trvHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_trvHistory.Controls.Add(Me.trvHistory)
        Me.pnl_trvHistory.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_trvHistory.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_trvHistory.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_trvHistory.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_trvHistory.Controls.Add(Me.ImgPreview)
        Me.pnl_trvHistory.Controls.Add(Me.ImgFax)
        Me.pnl_trvHistory.Controls.Add(Me.ImgNew)
        Me.pnl_trvHistory.Controls.Add(Me.ImgPrint)
        Me.pnl_trvHistory.Controls.Add(Me.ImgModify)
        Me.pnl_trvHistory.Controls.Add(Me.ImgDelete)
        Me.pnl_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_trvHistory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_trvHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_trvHistory.Location = New System.Drawing.Point(0, 51)
        Me.pnl_trvHistory.Name = "pnl_trvHistory"
        Me.pnl_trvHistory.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_trvHistory.Size = New System.Drawing.Size(235, 489)
        Me.pnl_trvHistory.TabIndex = 29
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 485)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(230, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 485)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(231, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 485)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(232, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlCmbSearchCriteria
        '
        Me.pnlCmbSearchCriteria.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCmbSearchCriteria.Controls.Add(Me.cmbSearchCriteria)
        Me.pnlCmbSearchCriteria.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCmbSearchCriteria.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCmbSearchCriteria.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlCmbSearchCriteria.Location = New System.Drawing.Point(0, 0)
        Me.pnlCmbSearchCriteria.Name = "pnlCmbSearchCriteria"
        Me.pnlCmbSearchCriteria.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlCmbSearchCriteria.Size = New System.Drawing.Size(235, 25)
        Me.pnlCmbSearchCriteria.TabIndex = 29
        Me.pnlCmbSearchCriteria.Visible = False
        '
        'pnltextSearch
        '
        Me.pnltextSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltextSearch.Controls.Add(Me.txtSearch)
        Me.pnltextSearch.Controls.Add(Me.Label20)
        Me.pnltextSearch.Controls.Add(Me.Label21)
        Me.pnltextSearch.Controls.Add(Me.PictureBox1)
        Me.pnltextSearch.Controls.Add(Me.label9)
        Me.pnltextSearch.Controls.Add(Me.Label12)
        Me.pnltextSearch.Controls.Add(Me.label10)
        Me.pnltextSearch.Controls.Add(Me.label11)
        Me.pnltextSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltextSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltextSearch.ForeColor = System.Drawing.Color.Black
        Me.pnltextSearch.Location = New System.Drawing.Point(0, 25)
        Me.pnltextSearch.Name = "pnltextSearch"
        Me.pnltextSearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltextSearch.Size = New System.Drawing.Size(235, 26)
        Me.pnltextSearch.TabIndex = 30
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(202, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(202, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(1, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(230, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(230, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.label10.Location = New System.Drawing.Point(0, 0)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(1, 23)
        Me.label10.TabIndex = 39
        Me.label10.Text = "label4"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.label11.Location = New System.Drawing.Point(231, 0)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 23)
        Me.label11.TabIndex = 40
        Me.label11.Text = "label4"
        '
        'gloUC_LabHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnl_trvHistory)
        Me.Controls.Add(Me.pnltextSearch)
        Me.Controls.Add(Me.pnlCmbSearchCriteria)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_LabHistory"
        Me.Size = New System.Drawing.Size(235, 540)
        CType(Me.ImgPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgFax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgModify, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_trvHistory.ResumeLayout(False)
        Me.pnlCmbSearchCriteria.ResumeLayout(False)
        Me.pnltextSearch.ResumeLayout(False)
        Me.pnltextSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbSearchCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents trvHistory As System.Windows.Forms.TreeView
    Friend WithEvents ImgPreview As System.Windows.Forms.PictureBox
    Friend WithEvents ImgFax As System.Windows.Forms.PictureBox
    Friend WithEvents ImgPrint As System.Windows.Forms.PictureBox
    Friend WithEvents ImgDelete As System.Windows.Forms.PictureBox
    Friend WithEvents ImgModify As System.Windows.Forms.PictureBox
    Friend WithEvents ImgNew As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents pnl_trvHistory As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents pnlCmbSearchCriteria As System.Windows.Forms.Panel
    Friend WithEvents pnltextSearch As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label

End Class
