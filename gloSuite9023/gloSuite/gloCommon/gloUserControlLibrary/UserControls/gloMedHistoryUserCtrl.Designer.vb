<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloMedHistoryUserCtrl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloMedHistoryUserCtrl))
        Me.txtHistorySearch = New System.Windows.Forms.TextBox
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlMain.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Location = New System.Drawing.Point(1, 0)
        Me.pnlMain.Size = New System.Drawing.Size(245, 624)
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Label8)
        Me.pnlHistory.Controls.Add(Me.Label7)
        Me.pnlHistory.Controls.Add(Me.Label4)
        Me.pnlHistory.Controls.Add(Me.Label3)
        Me.pnlHistory.Controls.Add(Me.Label2)
        Me.pnlHistory.Controls.Add(Me.Label1)
        Me.pnlHistory.Location = New System.Drawing.Point(0, 27)
        Me.pnlHistory.Padding = New System.Windows.Forms.Padding(0)
        Me.pnlHistory.Size = New System.Drawing.Size(245, 597)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label1, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label2, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label3, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label4, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label7, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.Label8, 0)
        Me.pnlHistory.Controls.SetChildIndex(Me.trvHistory, 0)
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.Panel6)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlTop.Size = New System.Drawing.Size(245, 27)
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHistory.LineColor = System.Drawing.Color.Black
        Me.trvHistory.Location = New System.Drawing.Point(4, 4)
        Me.trvHistory.Size = New System.Drawing.Size(240, 592)
        '
        'imgHistory
        '
        Me.imgHistory.ImageStream = CType(resources.GetObject("imgHistory.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgHistory.Images.SetKeyName(0, "Drugs.ico")
        Me.imgHistory.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgHistory.Images.SetKeyName(2, "Olders.ico")
        Me.imgHistory.Images.SetKeyName(3, "Olders_Disable.ico")
        Me.imgHistory.Images.SetKeyName(4, "Yesterdays.ico")
        Me.imgHistory.Images.SetKeyName(5, "Yesterdays_Disable.ico")
        Me.imgHistory.Images.SetKeyName(6, "Last Week.ico")
        Me.imgHistory.Images.SetKeyName(7, "Last Week_Disable.ico")
        Me.imgHistory.Images.SetKeyName(8, "LastMonth.ico")
        Me.imgHistory.Images.SetKeyName(9, "LastMonth_Disable.ico")
        Me.imgHistory.Images.SetKeyName(10, "Current.ico")
        Me.imgHistory.Images.SetKeyName(11, "Current_Disable.ico")
        Me.imgHistory.Images.SetKeyName(12, "MyTemplate.ico")
        Me.imgHistory.Images.SetKeyName(13, "Bullet06.ico")
        Me.imgHistory.Images.SetKeyName(14, "Modify MX.ico")
        Me.imgHistory.Images.SetKeyName(15, "Delete Drugs.ico")
        '
        'txtHistorySearch
        '
        Me.txtHistorySearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHistorySearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHistorySearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHistorySearch.ForeColor = System.Drawing.Color.Black
        Me.txtHistorySearch.Location = New System.Drawing.Point(28, 5)
        Me.txtHistorySearch.Multiline = True
        Me.txtHistorySearch.Name = "txtHistorySearch"
        Me.txtHistorySearch.Size = New System.Drawing.Size(215, 16)
        Me.txtHistorySearch.TabIndex = 2
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel6.Controls.Add(Me.txtHistorySearch)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Controls.Add(Me.Label21)
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Controls.Add(Me.label9)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.ForeColor = System.Drawing.Color.Black
        Me.Panel6.Location = New System.Drawing.Point(1, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(243, 24)
        Me.Panel6.TabIndex = 16
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(28, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(215, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(28, 21)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(215, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(0, 23)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(243, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(243, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(245, 1)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 596)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(245, 1)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 595)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(244, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 595)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 24)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(244, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(1, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(3, 595)
        Me.Label7.TabIndex = 41
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(4, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(240, 3)
        Me.Label8.TabIndex = 42
        '
        'gloMedHistoryUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Name = "gloMedHistoryUserCtrl"
        Me.Padding = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.Controls.SetChildIndex(Me.pnlMain, 0)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtHistorySearch As System.Windows.Forms.TextBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label

End Class
