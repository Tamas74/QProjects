<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloRxHistoryUserCtrl
    Inherits gloUserControlLibrary.gloHistoryUserCtrl

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloRxHistoryUserCtrl))
        Me.txtHistorySearch = New System.Windows.Forms.TextBox
        Me.NormalPriorityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SendImmediatelyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnltext = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.pnlMain.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnltext.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Location = New System.Drawing.Point(1, 0)
        Me.pnlMain.Size = New System.Drawing.Size(245, 759)
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Label4)
        Me.pnlHistory.Controls.Add(Me.Label3)
        Me.pnlHistory.Controls.Add(Me.Label2)
        Me.pnlHistory.Controls.Add(Me.Label1)
        Me.pnlHistory.Location = New System.Drawing.Point(0, 24)
        Me.pnlHistory.Padding = New System.Windows.Forms.Padding(0)
        Me.pnlHistory.Size = New System.Drawing.Size(245, 735)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label1, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label2, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label3, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label4, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.trvHistory, 0)
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.pnltext)
        Me.pnlTop.Size = New System.Drawing.Size(245, 24)
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.LineColor = System.Drawing.Color.Black
        Me.trvHistory.Location = New System.Drawing.Point(1, 1)
        Me.trvHistory.Size = New System.Drawing.Size(243, 733)
        '
        'imgHistory
        '
        Me.imgHistory.ImageStream = CType(resources.GetObject("imgHistory.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgHistory.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgHistory.Images.SetKeyName(1, "Small Arrow.ico")
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
        Me.imgHistory.Images.SetKeyName(13, "Delete RX01.ico")
        Me.imgHistory.Images.SetKeyName(14, "Print RX.ico")
        Me.imgHistory.Images.SetKeyName(15, "Fax RX.ico")
        Me.imgHistory.Images.SetKeyName(16, "Fax _01.ico")
        Me.imgHistory.Images.SetKeyName(17, "Fax High Priority.ico")
        Me.imgHistory.Images.SetKeyName(18, "e RX.ico")
        Me.imgHistory.Images.SetKeyName(19, "Edit RX.ico")
        '
        'txtHistorySearch
        '
        Me.txtHistorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHistorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHistorySearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHistorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtHistorySearch.Location = New System.Drawing.Point(29, 5)
        Me.txtHistorySearch.Name = "txtHistorySearch"
        Me.txtHistorySearch.Size = New System.Drawing.Size(215, 15)
        Me.txtHistorySearch.TabIndex = 0
        '
        'NormalPriorityToolStripMenuItem
        '
        Me.NormalPriorityToolStripMenuItem.Name = "NormalPriorityToolStripMenuItem"
        Me.NormalPriorityToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.NormalPriorityToolStripMenuItem.Text = "Normal Priority"
        '
        'SendImmediatelyToolStripMenuItem
        '
        Me.SendImmediatelyToolStripMenuItem.Name = "SendImmediatelyToolStripMenuItem"
        Me.SendImmediatelyToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.SendImmediatelyToolStripMenuItem.Text = "Send Immediately"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 735)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(244, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 735)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(243, 1)
        Me.Label3.TabIndex = 38
        Me.Label3.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(1, 734)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(243, 1)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "label1"
        '
        'pnltext
        '
        Me.pnltext.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltext.Controls.Add(Me.txtHistorySearch)
        Me.pnltext.Controls.Add(Me.Label20)
        Me.pnltext.Controls.Add(Me.Label21)
        Me.pnltext.Controls.Add(Me.PictureBox1)
        Me.pnltext.Controls.Add(Me.label9)
        Me.pnltext.Controls.Add(Me.Label12)
        Me.pnltext.Controls.Add(Me.label10)
        Me.pnltext.Controls.Add(Me.label11)
        Me.pnltext.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltext.ForeColor = System.Drawing.Color.Black
        Me.pnltext.Location = New System.Drawing.Point(0, 0)
        Me.pnltext.Name = "pnltext"
        Me.pnltext.Size = New System.Drawing.Size(245, 23)
        Me.pnltext.TabIndex = 16
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(215, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(215, 2)
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
        Me.label9.Size = New System.Drawing.Size(243, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(243, 1)
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
        Me.label11.Location = New System.Drawing.Point(244, 0)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 23)
        Me.label11.TabIndex = 40
        Me.label11.Text = "label4"
        '
        'gloRxHistoryUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.Name = "gloRxHistoryUserCtrl"
        Me.Padding = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.Size = New System.Drawing.Size(246, 759)
        Me.Controls.SetChildIndex(Me.pnlMain, 0)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnltext.ResumeLayout(False)
        Me.pnltext.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtHistorySearch As System.Windows.Forms.TextBox
    Friend WithEvents NormalPriorityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendImmediatelyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnltext As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label

End Class
