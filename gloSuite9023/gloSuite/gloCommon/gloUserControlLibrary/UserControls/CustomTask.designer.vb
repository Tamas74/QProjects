<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomTask
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomTask))
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Task = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ts_LM_Orders = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsbtn_New = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_SelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_DeSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_OK = New System.Windows.Forms.ToolStripButton()
        Me.tsbtn_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PnlBeersList = New System.Windows.Forms.Panel()
        Me.rbtnAllDrugs = New System.Windows.Forms.RadioButton()
        Me.rbtnBeersList = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.PnlICD = New System.Windows.Forms.Panel()
        Me.rbICD9 = New System.Windows.Forms.RadioButton()
        Me.rbICD10 = New System.Windows.Forms.RadioButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Task, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.ts_LM_Orders.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PnlBeersList.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.PnlICD.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.ForeColor = System.Drawing.Color.Black
        Me.txtsearch.Location = New System.Drawing.Point(73, 7)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(273, 22)
        Me.txtsearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(10, 6, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(68, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search :"
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me.C1FlexGrid1.ExtendLastCol = True
        Me.C1FlexGrid1.Location = New System.Drawing.Point(409, 345)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.DefaultSize = 21
        Me.C1FlexGrid1.Size = New System.Drawing.Size(87, 24)
        Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
        Me.C1FlexGrid1.TabIndex = 5
        '
        'C1Task
        '
        Me.C1Task.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Task.AllowEditing = False
        Me.C1Task.BackColor = System.Drawing.Color.White
        Me.C1Task.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Task.ColumnInfo = resources.GetString("C1Task.ColumnInfo")
        Me.C1Task.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Task.ExtendLastCol = True
        Me.C1Task.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Task.Location = New System.Drawing.Point(4, 1)
        Me.C1Task.Name = "C1Task"
        Me.C1Task.Rows.Count = 100
        Me.C1Task.Rows.DefaultSize = 19
        Me.C1Task.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Task.ShowCellLabels = True
        Me.C1Task.Size = New System.Drawing.Size(629, 256)
        Me.C1Task.StyleInfo = resources.GetString("C1Task.StyleInfo")
        Me.C1Task.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.ts_LM_Orders)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(637, 54)
        Me.Panel1.TabIndex = 7
        '
        'ts_LM_Orders
        '
        Me.ts_LM_Orders.BackColor = System.Drawing.Color.Transparent
        Me.ts_LM_Orders.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Toolstrip
        Me.ts_LM_Orders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_LM_Orders.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_LM_Orders.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_LM_Orders.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_LM_Orders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtn_New, Me.tsbtn_SelectAll, Me.tsbtn_DeSelectAll, Me.tsbtn_OK, Me.tsbtn_Cancel})
        Me.ts_LM_Orders.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ts_LM_Orders.Location = New System.Drawing.Point(0, 0)
        Me.ts_LM_Orders.Name = "ts_LM_Orders"
        Me.ts_LM_Orders.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ts_LM_Orders.Size = New System.Drawing.Size(637, 53)
        Me.ts_LM_Orders.TabIndex = 3
        Me.ts_LM_Orders.Text = "ToolStrip1"
        '
        'tsbtn_New
        '
        Me.tsbtn_New.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_New.Image = CType(resources.GetObject("tsbtn_New.Image"), System.Drawing.Image)
        Me.tsbtn_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_New.Name = "tsbtn_New"
        Me.tsbtn_New.Size = New System.Drawing.Size(45, 50)
        Me.tsbtn_New.Tag = "New"
        Me.tsbtn_New.Text = " &New "
        Me.tsbtn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_New.ToolTipText = "New "
        '
        'tsbtn_SelectAll
        '
        Me.tsbtn_SelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_SelectAll.Image = CType(resources.GetObject("tsbtn_SelectAll.Image"), System.Drawing.Image)
        Me.tsbtn_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_SelectAll.Name = "tsbtn_SelectAll"
        Me.tsbtn_SelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tsbtn_SelectAll.Tag = "Cancel"
        Me.tsbtn_SelectAll.Text = "&Select All"
        Me.tsbtn_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_SelectAll.ToolTipText = "Select All"
        '
        'tsbtn_DeSelectAll
        '
        Me.tsbtn_DeSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_DeSelectAll.Image = CType(resources.GetObject("tsbtn_DeSelectAll.Image"), System.Drawing.Image)
        Me.tsbtn_DeSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_DeSelectAll.Name = "tsbtn_DeSelectAll"
        Me.tsbtn_DeSelectAll.Size = New System.Drawing.Size(88, 50)
        Me.tsbtn_DeSelectAll.Tag = "Cancel"
        Me.tsbtn_DeSelectAll.Text = "&De-Select All"
        Me.tsbtn_DeSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_DeSelectAll.ToolTipText = "De-Select All"
        Me.tsbtn_DeSelectAll.Visible = False
        '
        'tsbtn_OK
        '
        Me.tsbtn_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_OK.Image = CType(resources.GetObject("tsbtn_OK.Image"), System.Drawing.Image)
        Me.tsbtn_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_OK.Name = "tsbtn_OK"
        Me.tsbtn_OK.Size = New System.Drawing.Size(40, 50)
        Me.tsbtn_OK.Tag = "OK"
        Me.tsbtn_OK.Text = "&Save"
        Me.tsbtn_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_OK.ToolTipText = "Save"
        '
        'tsbtn_Cancel
        '
        Me.tsbtn_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbtn_Cancel.Image = CType(resources.GetObject("tsbtn_Cancel.Image"), System.Drawing.Image)
        Me.tsbtn_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtn_Cancel.Name = "tsbtn_Cancel"
        Me.tsbtn_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tsbtn_Cancel.Tag = "Cancel"
        Me.tsbtn_Cancel.Text = "&Close"
        Me.tsbtn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsbtn_Cancel.ToolTipText = "Close"
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.txtsearch)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 84)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(637, 37)
        Me.Panel2.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(4, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(629, 1)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(4, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(629, 1)
        Me.Label17.TabIndex = 45
        Me.Label17.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 31)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(633, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 31)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "label3"
        '
        'PnlBeersList
        '
        Me.PnlBeersList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlBeersList.Controls.Add(Me.rbtnAllDrugs)
        Me.PnlBeersList.Controls.Add(Me.rbtnBeersList)
        Me.PnlBeersList.Controls.Add(Me.Label11)
        Me.PnlBeersList.Controls.Add(Me.Label10)
        Me.PnlBeersList.Controls.Add(Me.Label9)
        Me.PnlBeersList.Controls.Add(Me.Label4)
        Me.PnlBeersList.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlBeersList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlBeersList.Location = New System.Drawing.Point(0, 121)
        Me.PnlBeersList.Name = "PnlBeersList"
        Me.PnlBeersList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.PnlBeersList.Size = New System.Drawing.Size(637, 33)
        Me.PnlBeersList.TabIndex = 48
        Me.PnlBeersList.Visible = False
        '
        'rbtnAllDrugs
        '
        Me.rbtnAllDrugs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnAllDrugs.AutoSize = True
        Me.rbtnAllDrugs.Checked = True
        Me.rbtnAllDrugs.Location = New System.Drawing.Point(77, 7)
        Me.rbtnAllDrugs.Name = "rbtnAllDrugs"
        Me.rbtnAllDrugs.Size = New System.Drawing.Size(72, 18)
        Me.rbtnAllDrugs.TabIndex = 217
        Me.rbtnAllDrugs.TabStop = True
        Me.rbtnAllDrugs.Text = "All Drugs"
        Me.rbtnAllDrugs.UseVisualStyleBackColor = True
        '
        'rbtnBeersList
        '
        Me.rbtnBeersList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbtnBeersList.AutoSize = True
        Me.rbtnBeersList.Location = New System.Drawing.Point(149, 7)
        Me.rbtnBeersList.Name = "rbtnBeersList"
        Me.rbtnBeersList.Size = New System.Drawing.Size(77, 18)
        Me.rbtnBeersList.TabIndex = 216
        Me.rbtnBeersList.Text = "Beers List"
        Me.rbtnBeersList.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(4, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(629, 1)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "label1"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(629, 1)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(633, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 30)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 30)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "label3"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.C1Task)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.C1FlexGrid1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 154)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(637, 261)
        Me.Panel3.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 257)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(629, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 257)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(633, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 257)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(631, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'PnlICD
        '
        Me.PnlICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlICD.Controls.Add(Me.rbICD9)
        Me.PnlICD.Controls.Add(Me.rbICD10)
        Me.PnlICD.Controls.Add(Me.Label13)
        Me.PnlICD.Controls.Add(Me.Label14)
        Me.PnlICD.Controls.Add(Me.Label15)
        Me.PnlICD.Controls.Add(Me.Label16)
        Me.PnlICD.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlICD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlICD.Location = New System.Drawing.Point(0, 54)
        Me.PnlICD.Name = "PnlICD"
        Me.PnlICD.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.PnlICD.Size = New System.Drawing.Size(637, 30)
        Me.PnlICD.TabIndex = 49
        Me.PnlICD.Visible = False
        '
        'rbICD9
        '
        Me.rbICD9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbICD9.AutoSize = True
        Me.rbICD9.Checked = True
        Me.rbICD9.Location = New System.Drawing.Point(77, 7)
        Me.rbICD9.Name = "rbICD9"
        Me.rbICD9.Size = New System.Drawing.Size(51, 18)
        Me.rbICD9.TabIndex = 217
        Me.rbICD9.TabStop = True
        Me.rbICD9.Text = "ICD9"
        Me.rbICD9.UseVisualStyleBackColor = True
        '
        'rbICD10
        '
        Me.rbICD10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rbICD10.AutoSize = True
        Me.rbICD10.Location = New System.Drawing.Point(149, 7)
        Me.rbICD10.Name = "rbICD10"
        Me.rbICD10.Size = New System.Drawing.Size(58, 18)
        Me.rbICD10.TabIndex = 216
        Me.rbICD10.Text = "ICD10"
        Me.rbICD10.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(4, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(629, 1)
        Me.Label13.TabIndex = 52
        Me.Label13.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(4, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(629, 1)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(633, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 27)
        Me.Label15.TabIndex = 50
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(101, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(207, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(3, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 27)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = "label3"
        '
        'CustomTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PnlBeersList)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PnlICD)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "CustomTask"
        Me.Size = New System.Drawing.Size(637, 415)
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Task, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ts_LM_Orders.ResumeLayout(False)
        Me.ts_LM_Orders.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.PnlBeersList.ResumeLayout(False)
        Me.PnlBeersList.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.PnlICD.ResumeLayout(False)
        Me.PnlICD.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ts_LM_Orders As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsbtn_New As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_Cancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents C1Task As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents PnlBeersList As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbtnAllDrugs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnBeersList As System.Windows.Forms.RadioButton
    Friend WithEvents tsbtn_SelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtn_DeSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents PnlICD As System.Windows.Forms.Panel
    Friend WithEvents rbICD9 As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD10 As System.Windows.Forms.RadioButton
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Public Event SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event OKClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event AddClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event CloseClick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event Dblclick(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event MouseUpClick(ByVal sender, ByVal e)
    '' chetan added 14 oct 2010
    Public Event MouseDubClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) '' Chetan Added
    Public Event MouseMoveClick(ByVal sender As System.Object, ByVal e As MouseEventArgs) '' Chetan Added

    'Public Event chkBeersListClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event SelectAllClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event DeSelectAllClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event GridDoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) '' Vinayak Added
    Public Event rbtnBeersListClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event rbtnAllDrugsClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event rbtnICD9Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event rbtnICD10Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Event TextKeyPress(ByVal sender, ByVal e)
    Public Event CellChanged(ByVal sender, ByVal e)
End Class
