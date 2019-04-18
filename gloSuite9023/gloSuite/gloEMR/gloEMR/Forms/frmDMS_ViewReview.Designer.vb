<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDMS_ViewReview
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDMS_ViewReview))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tls_ViewReview = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_Close = New System.Windows.Forms.ToolStripButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.C1viewReview = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.lblselectuser = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.tls_ViewReview.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.C1viewReview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tls_ViewReview)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(621, 54)
        Me.Panel1.TabIndex = 9
        '
        'tls_ViewReview
        '
        Me.tls_ViewReview.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_ViewReview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ViewReview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ViewReview.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ViewReview.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Close})
        Me.tls_ViewReview.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_ViewReview.Location = New System.Drawing.Point(0, 0)
        Me.tls_ViewReview.Name = "tls_ViewReview"
        Me.tls_ViewReview.Size = New System.Drawing.Size(621, 53)
        Me.tls_ViewReview.TabIndex = 7
        Me.tls_ViewReview.Text = "ToolStrip1"
        '
        'tlb_Close
        '
        Me.tlb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Close.Image = CType(resources.GetObject("tlb_Close.Image"), System.Drawing.Image)
        Me.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Close.Name = "tlb_Close"
        Me.tlb_Close.Size = New System.Drawing.Size(59, 50)
        Me.tlb_Close.Tag = "Close"
        Me.tlb_Close.Text = "  &Close  "
        Me.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Close.ToolTipText = "Close"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.C1viewReview)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 84)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(621, 272)
        Me.Panel3.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 268)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(613, 1)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 267)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(617, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 267)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(615, 1)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label1"
        '
        'C1viewReview
        '
        Me.C1viewReview.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1viewReview.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1viewReview.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1viewReview.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1viewReview.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
            ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1viewReview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1viewReview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1viewReview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1viewReview.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1viewReview.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1viewReview.Location = New System.Drawing.Point(3, 1)
        Me.C1viewReview.Name = "C1viewReview"
        Me.C1viewReview.Rows.Count = 1
        Me.C1viewReview.Rows.DefaultSize = 19
        Me.C1viewReview.Rows.Fixed = 0
        Me.C1viewReview.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1viewReview.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1viewReview.ShowSort = False
        Me.C1viewReview.Size = New System.Drawing.Size(615, 268)
        Me.C1viewReview.StyleInfo = resources.GetString("C1viewReview.StyleInfo")
        Me.C1viewReview.TabIndex = 6
        Me.C1viewReview.Tree.NodeImageCollapsed = CType(resources.GetObject("C1viewReview.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1viewReview.Tree.NodeImageExpanded = CType(resources.GetObject("C1viewReview.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.cmbUsers)
        Me.Panel2.Controls.Add(Me.lblselectuser)
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(615, 24)
        Me.Panel2.TabIndex = 10
        '
        'cmbUsers
        '
        Me.cmbUsers.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsers.ForeColor = System.Drawing.Color.Black
        Me.cmbUsers.FormattingEnabled = True
        Me.cmbUsers.Location = New System.Drawing.Point(123, 1)
        Me.cmbUsers.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(222, 22)
        Me.cmbUsers.TabIndex = 0
        '
        'lblselectuser
        '
        Me.lblselectuser.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblselectuser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblselectuser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblselectuser.Location = New System.Drawing.Point(1, 1)
        Me.lblselectuser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblselectuser.Name = "lblselectuser"
        Me.lblselectuser.Size = New System.Drawing.Size(122, 22)
        Me.lblselectuser.TabIndex = 0
        Me.lblselectuser.Text = "Select User Name :"
        Me.lblselectuser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(613, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(614, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(615, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel4.Size = New System.Drawing.Size(621, 30)
        Me.Panel4.TabIndex = 12
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDMS_ViewReview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(621, 356)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDMS_ViewReview"
        Me.ShowInTaskbar = False
        Me.Text = "View Review"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tls_ViewReview.ResumeLayout(False)
        Me.tls_ViewReview.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.C1viewReview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblselectuser As System.Windows.Forms.Label
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents C1viewReview As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tls_ViewReview As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlb_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
