<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStressTest
    Inherits System.Windows.Forms.Form
    Private components As System.ComponentModel.IContainer
    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    'Required by the Windows Form Designer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStressTest))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tsStressTest = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlPatientStrip = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlStressTest = New System.Windows.Forms.Panel
        Me.C1StressList = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlTop.SuspendLayout()
        Me.tsStressTest.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlStressTest.SuspendLayout()
        CType(Me.C1StressList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tsStressTest)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(959, 53)
        Me.pnlTop.TabIndex = 0
        '
        'tsStressTest
        '
        Me.tsStressTest.BackgroundImage = CType(resources.GetObject("tsStressTest.BackgroundImage"), System.Drawing.Image)
        Me.tsStressTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsStressTest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsStressTest.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsStressTest.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tsStressTest.Location = New System.Drawing.Point(0, 0)
        Me.tsStressTest.Name = "tsStressTest"
        Me.tsStressTest.Size = New System.Drawing.Size(959, 53)
        Me.tsStressTest.TabIndex = 0
        Me.tsStressTest.Text = "ToolStrip1"
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(40, 50)
        Me.tblbtn_Save.Tag = "Save"
        Me.tblbtn_Save.Text = "&Save"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlPatientStrip
        '
        Me.pnlPatientStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientStrip.Location = New System.Drawing.Point(0, 53)
        Me.pnlPatientStrip.Name = "pnlPatientStrip"
        Me.pnlPatientStrip.Size = New System.Drawing.Size(959, 163)
        Me.pnlPatientStrip.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(117, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Patient Search :"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlStressTest)
        Me.pnlMain.Controls.Add(Me.pnlSearch)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 216)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(959, 408)
        Me.pnlMain.TabIndex = 0
        '
        'pnlStressTest
        '
        Me.pnlStressTest.Controls.Add(Me.C1StressList)
        Me.pnlStressTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStressTest.Location = New System.Drawing.Point(0, 30)
        Me.pnlStressTest.Name = "pnlStressTest"
        Me.pnlStressTest.Size = New System.Drawing.Size(959, 378)
        Me.pnlStressTest.TabIndex = 0
        '
        'C1StressList
        '
        Me.C1StressList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1StressList.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.C1StressList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1StressList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1StressList.Location = New System.Drawing.Point(0, 0)
        Me.C1StressList.Name = "C1StressList"
        Me.C1StressList.Rows.DefaultSize = 19
        Me.C1StressList.Size = New System.Drawing.Size(959, 378)
        Me.C1StressList.StyleInfo = resources.GetString("C1StressList.StyleInfo")
        Me.C1StressList.TabIndex = 23
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.Panel6)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(959, 30)
        Me.pnlSearch.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = CType(resources.GetObject("Panel6.BackgroundImage"), System.Drawing.Image)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.TextBox1)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(953, 24)
        Me.Panel6.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TextBox1.Location = New System.Drawing.Point(118, 1)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(259, 22)
        Me.TextBox1.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(118, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(834, 1)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Patient Strip"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(951, 1)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Patient Strip"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(952, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 24)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Patient Strip"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Patient Strip"
        '
        'frmStressTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(959, 624)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlPatientStrip)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmStressTest"
        Me.Text = "Stress Test"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tsStressTest.ResumeLayout(False)
        Me.tsStressTest.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlStressTest.ResumeLayout(False)
        CType(Me.C1StressList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlPatientStrip As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlStressTest As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tsStressTest As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1StressList As C1.Win.C1FlexGrid.C1FlexGrid

End Class
