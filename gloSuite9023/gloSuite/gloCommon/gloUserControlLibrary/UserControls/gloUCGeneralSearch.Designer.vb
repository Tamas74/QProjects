<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUCGeneralSearch
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
                If (IsNothing(dvNext) = False) Then
                    dvNext.Dispose()
                    dvNext = Nothing
                End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUCGeneralSearch))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pnlFill = New System.Windows.Forms.Panel()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlLeft.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFill.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.PictureBox1)
        Me.pnlLeft.Location = New System.Drawing.Point(1, 1)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(28, 22)
        Me.pnlLeft.TabIndex = 0
        Me.pnlLeft.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'pnlFill
        '
        Me.pnlFill.BackColor = System.Drawing.Color.Transparent
        Me.pnlFill.Controls.Add(Me.Label167)
        Me.pnlFill.Controls.Add(Me.pnlSearch)
        Me.pnlFill.Controls.Add(Me.Label9)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 0)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(307, 24)
        Me.pnlFill.TabIndex = 1
        '
        'Label167
        '
        Me.Label167.AutoSize = True
        Me.Label167.BackColor = System.Drawing.Color.Transparent
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label167.Location = New System.Drawing.Point(6, 0)
        Me.Label167.MaximumSize = New System.Drawing.Size(60, 18)
        Me.Label167.MinimumSize = New System.Drawing.Size(60, 18)
        Me.Label167.Name = "Label167"
        Me.Label167.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label167.Size = New System.Drawing.Size(60, 18)
        Me.Label167.TabIndex = 55
        Me.Label167.Text = "Search : "
        Me.Label167.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(66, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(241, 24)
        Me.pnlSearch.TabIndex = 50
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtSearch.MaxLength = 100
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ShortcutsEnabled = False
        Me.txtSearch.Size = New System.Drawing.Size(215, 15)
        Me.txtSearch.TabIndex = 0
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 18)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(215, 6)
        Me.Label77.TabIndex = 43
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 0)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(215, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 0)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 24)
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
        Me.btnClear.Location = New System.Drawing.Point(220, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 24)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(172, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 49
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gloUCGeneralSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnlFill)
        Me.Controls.Add(Me.pnlLeft)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUCGeneralSearch"
        Me.Size = New System.Drawing.Size(307, 24)
        Me.pnlLeft.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFill.ResumeLayout(False)
        Me.pnlFill.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Friend WithEvents Label167 As System.Windows.Forms.Label

End Class
