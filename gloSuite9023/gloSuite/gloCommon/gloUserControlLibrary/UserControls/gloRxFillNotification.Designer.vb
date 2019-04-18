<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloRxFillNotification
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloRxFillNotification))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlwbBrowser = New System.Windows.Forms.Panel()
        Me.requestViewer = New System.Windows.Forms.WebBrowser()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.pnl_Grid = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.C1RxFillList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlwbBrowser.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.pnl_Grid.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.C1RxFillList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlwbBrowser)
        Me.pnlMain.Controls.Add(Me.Splitter3)
        Me.pnlMain.Controls.Add(Me.pnl_Grid)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1000, 532)
        Me.pnlMain.TabIndex = 243
        '
        'pnlwbBrowser
        '
        Me.pnlwbBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlwbBrowser.Controls.Add(Me.requestViewer)
        Me.pnlwbBrowser.Controls.Add(Me.Panel14)
        Me.pnlwbBrowser.Controls.Add(Me.Label96)
        Me.pnlwbBrowser.Controls.Add(Me.Label95)
        Me.pnlwbBrowser.Controls.Add(Me.Label90)
        Me.pnlwbBrowser.Controls.Add(Me.Label1)
        Me.pnlwbBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlwbBrowser.Location = New System.Drawing.Point(302, 0)
        Me.pnlwbBrowser.Name = "pnlwbBrowser"
        Me.pnlwbBrowser.Size = New System.Drawing.Size(698, 532)
        Me.pnlwbBrowser.TabIndex = 237
        '
        'requestViewer
        '
        Me.requestViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.requestViewer.Location = New System.Drawing.Point(1, 26)
        Me.requestViewer.MinimumSize = New System.Drawing.Size(20, 20)
        Me.requestViewer.Name = "requestViewer"
        Me.requestViewer.Size = New System.Drawing.Size(696, 505)
        Me.requestViewer.TabIndex = 15
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.Transparent
        Me.Panel14.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_2007Header
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Label33)
        Me.Panel14.Controls.Add(Me.Label34)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(1, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(696, 25)
        Me.Panel14.TabIndex = 237
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(0, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(696, 1)
        Me.Label33.TabIndex = 9
        Me.Label33.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(696, 25)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "   Notification Details"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(697, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 530)
        Me.Label96.TabIndex = 14
        Me.Label96.Text = "label4"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(0, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 530)
        Me.Label95.TabIndex = 13
        Me.Label95.Text = "label4"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label90.Location = New System.Drawing.Point(0, 531)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(698, 1)
        Me.Label90.TabIndex = 11
        Me.Label90.Text = "label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(698, 1)
        Me.Label1.TabIndex = 238
        Me.Label1.Text = "label2"
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter3.Enabled = False
        Me.Splitter3.Location = New System.Drawing.Point(299, 0)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(3, 532)
        Me.Splitter3.TabIndex = 240
        Me.Splitter3.TabStop = False
        '
        'pnl_Grid
        '
        Me.pnl_Grid.Controls.Add(Me.Panel4)
        Me.pnl_Grid.Controls.Add(Me.Panel1)
        Me.pnl_Grid.Controls.Add(Me.Label8)
        Me.pnl_Grid.Controls.Add(Me.Label6)
        Me.pnl_Grid.Controls.Add(Me.Label9)
        Me.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_Grid.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Grid.Name = "pnl_Grid"
        Me.pnl_Grid.Size = New System.Drawing.Size(299, 532)
        Me.pnl_Grid.TabIndex = 236
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.C1RxFillList)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(1, 26)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(297, 506)
        Me.Panel4.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(297, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(0, 505)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(297, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'C1RxFillList
        '
        Me.C1RxFillList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1RxFillList.ColumnInfo = "3,0,0,0,0,105,Columns:1{Width:32;Style:""TextAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Style:""TextA" & _
    "lign:GeneralCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1RxFillList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1RxFillList.ExtendLastCol = True
        Me.C1RxFillList.Location = New System.Drawing.Point(0, 0)
        Me.C1RxFillList.Name = "C1RxFillList"
        Me.C1RxFillList.Rows.DefaultSize = 21
        Me.C1RxFillList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1RxFillList.Size = New System.Drawing.Size(297, 506)
        Me.C1RxFillList.StyleInfo = resources.GetString("C1RxFillList.StyleInfo")
        Me.C1RxFillList.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_2007Header
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 25)
        Me.Panel1.TabIndex = 237
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(297, 25)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "   RxFill Notifications"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 531)
        Me.Label8.TabIndex = 238
        Me.Label8.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(298, 1)
        Me.Label6.TabIndex = 240
        Me.Label6.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(298, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 532)
        Me.Label9.TabIndex = 239
        Me.Label9.Text = "label3"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'gloRxFillNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "gloRxFillNotification"
        Me.Size = New System.Drawing.Size(1000, 532)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlwbBrowser.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.pnl_Grid.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.C1RxFillList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents pnl_Grid As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents C1RxFillList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlwbBrowser As System.Windows.Forms.Panel
    Friend WithEvents requestViewer As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip

End Class
