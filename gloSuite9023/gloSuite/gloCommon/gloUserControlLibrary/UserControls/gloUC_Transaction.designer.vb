<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_Transaction
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try


            Try
                If (IsNothing(_AbnormalFlag_COL) = False) Then
                    _AbnormalFlag_COL.Dispose()
                    _AbnormalFlag_COL = Nothing
                End If
            Catch ex As Exception

            End Try

            Try
                If (IsNothing(_ObservationStatus_COL) = False) Then
                    _ObservationStatus_COL.Dispose()
                    _ObservationStatus_COL = Nothing
                End If
            Catch ex As Exception

            End Try


        End If
        MyBase.Dispose(disposing)


        Dim dtpControls As System.Windows.Forms.ContextMenu() = {_Flex.ContextMenu}


        Try
            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            gloGlobal.cEventHelper.DisposeContextMenu(dtpControls)
        Catch

        End Try

    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_Transaction))
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlDiagosisCPT = New System.Windows.Forms.Panel
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.c1DignosisCPTs = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.pnlDgnCPTSearch = New System.Windows.Forms.Panel
        Me.txtDgnCPTSearch = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.lbl_SearchColumn = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblDigCPTHeader = New System.Windows.Forms.Label
        Me.pnlDgnCPTToolStrip = New System.Windows.Forms.Panel
        Me.tlsp_DgnCPT = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsp_btnOK = New System.Windows.Forms.ToolStripButton
        Me.tlsp_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.txtTestResultComment = New System.Windows.Forms.TextBox
        Me.pnlResults = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.c1Results = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlResultSearch = New System.Windows.Forms.Panel
        Me.txtResultSearch = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.lblResultHeader = New System.Windows.Forms.Label
        Me.pnlButton = New System.Windows.Forms.Panel
        Me.ImgResult_Comment = New System.Windows.Forms.PictureBox
        Me.ImgResult = New System.Windows.Forms.PictureBox
        Me.ImgResultHeader = New System.Windows.Forms.PictureBox
        Me.ImgTest = New System.Windows.Forms.PictureBox
        Me.lblResultTestRowNo = New System.Windows.Forms.Label
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.pnlInstruction = New System.Windows.Forms.Panel
        Me.pnlInstruction_Detail = New System.Windows.Forms.Panel
        Me.txtInstruction = New System.Windows.Forms.TextBox
        Me.lblInstruction_SpliterTop = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlInstruction_Button = New System.Windows.Forms.Panel
        Me.pnlInstruction_Button1 = New System.Windows.Forms.Panel
        Me.lblTestDetails_Header = New System.Windows.Forms.Label
        Me.btnInstruction_Up = New System.Windows.Forms.Button
        Me.btnInstruction_Down = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlInstruction_ButtonBottom = New System.Windows.Forms.Panel
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDiagosisCPT.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        CType(Me.c1DignosisCPTs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.pnlDgnCPTSearch.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlDgnCPTToolStrip.SuspendLayout()
        Me.tlsp_DgnCPT.SuspendLayout()
        Me.pnlResults.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.c1Results, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlResultSearch.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlButton.SuspendLayout()
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlInstruction.SuspendLayout()
        Me.pnlInstruction_Detail.SuspendLayout()
        Me.pnlInstruction_Button.SuspendLayout()
        Me.pnlInstruction_Button1.SuspendLayout()
        Me.pnlInstruction_ButtonBottom.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.SuspendLayout()
        '
        '_Flex
        '
        Me._Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me._Flex.BackColor = System.Drawing.Color.GhostWhite
        Me._Flex.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.ForeColor = System.Drawing.Color.Black
        Me._Flex.Location = New System.Drawing.Point(0, 0)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(732, 406)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 0
        '
        'pnlDiagosisCPT
        '
        Me.pnlDiagosisCPT.Controls.Add(Me.pnl_Base)
        Me.pnlDiagosisCPT.Controls.Add(Me.Panel6)
        Me.pnlDiagosisCPT.Controls.Add(Me.Panel1)
        Me.pnlDiagosisCPT.Controls.Add(Me.pnlDgnCPTToolStrip)
        Me.pnlDiagosisCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDiagosisCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlDiagosisCPT.Name = "pnlDiagosisCPT"
        Me.pnlDiagosisCPT.Size = New System.Drawing.Size(732, 406)
        Me.pnlDiagosisCPT.TabIndex = 1
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.c1DignosisCPTs)
        Me.pnl_Base.Controls.Add(Me.Label25)
        Me.pnl_Base.Controls.Add(Me.Label26)
        Me.pnl_Base.Controls.Add(Me.Label27)
        Me.pnl_Base.Controls.Add(Me.Label28)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 112)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(732, 294)
        Me.pnl_Base.TabIndex = 17
        '
        'c1DignosisCPTs
        '
        Me.c1DignosisCPTs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1DignosisCPTs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1DignosisCPTs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DignosisCPTs.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1DignosisCPTs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DignosisCPTs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1DignosisCPTs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DignosisCPTs.Location = New System.Drawing.Point(1, 1)
        Me.c1DignosisCPTs.Name = "c1DignosisCPTs"
        Me.c1DignosisCPTs.Rows.DefaultSize = 19
        Me.c1DignosisCPTs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1DignosisCPTs.Size = New System.Drawing.Size(730, 289)
        Me.c1DignosisCPTs.StyleInfo = resources.GetString("c1DignosisCPTs.StyleInfo")
        Me.c1DignosisCPTs.TabIndex = 7
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Location = New System.Drawing.Point(1, 290)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(730, 1)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(0, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 290)
        Me.Label26.TabIndex = 3
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Location = New System.Drawing.Point(731, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 290)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "label3"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(732, 1)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.pnlDgnCPTSearch)
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 85)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(732, 27)
        Me.Panel6.TabIndex = 20
        '
        'pnlDgnCPTSearch
        '
        Me.pnlDgnCPTSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDgnCPTSearch.Controls.Add(Me.txtDgnCPTSearch)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label17)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label18)
        Me.pnlDgnCPTSearch.Controls.Add(Me.PictureBox2)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label19)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label22)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label23)
        Me.pnlDgnCPTSearch.Controls.Add(Me.Label24)
        Me.pnlDgnCPTSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDgnCPTSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDgnCPTSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlDgnCPTSearch.Location = New System.Drawing.Point(145, 0)
        Me.pnlDgnCPTSearch.Name = "pnlDgnCPTSearch"
        Me.pnlDgnCPTSearch.Padding = New System.Windows.Forms.Padding(3, 1, 0, 3)
        Me.pnlDgnCPTSearch.Size = New System.Drawing.Size(587, 27)
        Me.pnlDgnCPTSearch.TabIndex = 16
        '
        'txtDgnCPTSearch
        '
        Me.txtDgnCPTSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDgnCPTSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDgnCPTSearch.ForeColor = System.Drawing.Color.Black
        Me.txtDgnCPTSearch.Location = New System.Drawing.Point(31, 6)
        Me.txtDgnCPTSearch.Name = "txtDgnCPTSearch"
        Me.txtDgnCPTSearch.Size = New System.Drawing.Size(555, 15)
        Me.txtDgnCPTSearch.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.White
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(31, 2)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(555, 4)
        Me.Label17.TabIndex = 37
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Location = New System.Drawing.Point(31, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(555, 2)
        Me.Label18.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(4, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(582, 1)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Location = New System.Drawing.Point(4, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(582, 1)
        Me.Label22.TabIndex = 36
        Me.Label22.Text = "label1"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Location = New System.Drawing.Point(3, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 23)
        Me.Label23.TabIndex = 39
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Location = New System.Drawing.Point(586, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "label4"
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.lbl_SearchColumn)
        Me.Panel7.Controls.Add(Me.Label43)
        Me.Panel7.Controls.Add(Me.Label39)
        Me.Panel7.Controls.Add(Me.Label40)
        Me.Panel7.Controls.Add(Me.Label41)
        Me.Panel7.Controls.Add(Me.Label42)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 1, 3)
        Me.Panel7.Size = New System.Drawing.Size(145, 27)
        Me.Panel7.TabIndex = 8
        Me.Panel7.Visible = False
        '
        'lbl_SearchColumn
        '
        Me.lbl_SearchColumn.AutoSize = True
        Me.lbl_SearchColumn.BackColor = System.Drawing.Color.Transparent
        Me.lbl_SearchColumn.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_SearchColumn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SearchColumn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_SearchColumn.Location = New System.Drawing.Point(61, 1)
        Me.lbl_SearchColumn.Name = "lbl_SearchColumn"
        Me.lbl_SearchColumn.Padding = New System.Windows.Forms.Padding(2, 5, 2, 2)
        Me.lbl_SearchColumn.Size = New System.Drawing.Size(42, 21)
        Me.lbl_SearchColumn.TabIndex = 41
        Me.lbl_SearchColumn.Text = "Code"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(1, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Padding = New System.Windows.Forms.Padding(2, 5, 2, 2)
        Me.Label43.Size = New System.Drawing.Size(60, 21)
        Me.Label43.TabIndex = 48
        Me.Label43.Text = "Search :"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Location = New System.Drawing.Point(143, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 22)
        Me.Label39.TabIndex = 47
        Me.Label39.Text = "label4"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Location = New System.Drawing.Point(0, 1)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 22)
        Me.Label40.TabIndex = 46
        Me.Label40.Text = "label4"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Location = New System.Drawing.Point(0, 23)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(144, 1)
        Me.Label41.TabIndex = 45
        Me.Label41.Text = "label1"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Location = New System.Drawing.Point(0, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(144, 1)
        Me.Label42.TabIndex = 44
        Me.Label42.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 57)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(732, 28)
        Me.Panel1.TabIndex = 18
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.lblDigCPTHeader)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(732, 25)
        Me.Panel4.TabIndex = 8
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(1, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(730, 1)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 24)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(731, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 24)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(732, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'lblDigCPTHeader
        '
        Me.lblDigCPTHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblDigCPTHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDigCPTHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDigCPTHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDigCPTHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblDigCPTHeader.Name = "lblDigCPTHeader"
        Me.lblDigCPTHeader.Size = New System.Drawing.Size(732, 25)
        Me.lblDigCPTHeader.TabIndex = 3
        Me.lblDigCPTHeader.Text = "  Diagnosis/CPTs"
        Me.lblDigCPTHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDgnCPTToolStrip
        '
        Me.pnlDgnCPTToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDgnCPTToolStrip.Controls.Add(Me.tlsp_DgnCPT)
        Me.pnlDgnCPTToolStrip.Controls.Add(Me.Label30)
        Me.pnlDgnCPTToolStrip.Controls.Add(Me.Label29)
        Me.pnlDgnCPTToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDgnCPTToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlDgnCPTToolStrip.Name = "pnlDgnCPTToolStrip"
        Me.pnlDgnCPTToolStrip.Size = New System.Drawing.Size(732, 57)
        Me.pnlDgnCPTToolStrip.TabIndex = 4
        '
        'tlsp_DgnCPT
        '
        Me.tlsp_DgnCPT.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_DgnCPT.BackgroundImage = CType(resources.GetObject("tlsp_DgnCPT.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_DgnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_DgnCPT.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tlsp_DgnCPT.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_DgnCPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsp_btnOK, Me.tlsp_btnCancel})
        Me.tlsp_DgnCPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_DgnCPT.Location = New System.Drawing.Point(1, 1)
        Me.tlsp_DgnCPT.Name = "tlsp_DgnCPT"
        Me.tlsp_DgnCPT.Size = New System.Drawing.Size(731, 53)
        Me.tlsp_DgnCPT.TabIndex = 1
        Me.tlsp_DgnCPT.Text = "toolStrip1"
        '
        'tlsp_btnOK
        '
        Me.tlsp_btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_btnOK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsp_btnOK.Image = CType(resources.GetObject("tlsp_btnOK.Image"), System.Drawing.Image)
        Me.tlsp_btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsp_btnOK.Name = "tlsp_btnOK"
        Me.tlsp_btnOK.Size = New System.Drawing.Size(66, 50)
        Me.tlsp_btnOK.Tag = "OK"
        Me.tlsp_btnOK.Text = "&Save&&Cls"
        Me.tlsp_btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsp_btnOK.ToolTipText = "Save and Close"
        '
        'tlsp_btnCancel
        '
        Me.tlsp_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsp_btnCancel.Image = CType(resources.GetObject("tlsp_btnCancel.Image"), System.Drawing.Image)
        Me.tlsp_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsp_btnCancel.Name = "tlsp_btnCancel"
        Me.tlsp_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tlsp_btnCancel.Tag = "Cancel"
        Me.tlsp_btnCancel.Text = "&Close"
        Me.tlsp_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(1, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(731, 1)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "label1"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 57)
        Me.Label29.TabIndex = 10
        Me.Label29.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 406)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(732, 3)
        Me.Splitter1.TabIndex = 30
        Me.Splitter1.TabStop = False
        Me.Splitter1.Visible = False
        '
        'txtTestResultComment
        '
        Me.txtTestResultComment.BackColor = System.Drawing.Color.White
        Me.txtTestResultComment.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtTestResultComment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestResultComment.ForeColor = System.Drawing.Color.Black
        Me.txtTestResultComment.Location = New System.Drawing.Point(0, 409)
        Me.txtTestResultComment.Multiline = True
        Me.txtTestResultComment.Name = "txtTestResultComment"
        Me.txtTestResultComment.ReadOnly = True
        Me.txtTestResultComment.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtTestResultComment.Size = New System.Drawing.Size(732, 81)
        Me.txtTestResultComment.TabIndex = 29
        Me.txtTestResultComment.Visible = False
        '
        'pnlResults
        '
        Me.pnlResults.BackColor = System.Drawing.Color.Transparent
        Me.pnlResults.Controls.Add(Me.Panel5)
        Me.pnlResults.Controls.Add(Me.pnlResultSearch)
        Me.pnlResults.Controls.Add(Me.Panel2)
        Me.pnlResults.Controls.Add(Me.pnlButton)
        Me.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlResults.Location = New System.Drawing.Point(0, 0)
        Me.pnlResults.Name = "pnlResults"
        Me.pnlResults.Size = New System.Drawing.Size(732, 406)
        Me.pnlResults.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label36)
        Me.Panel5.Controls.Add(Me.Label35)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.Label33)
        Me.Panel5.Controls.Add(Me.c1Results)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 112)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(732, 294)
        Me.Panel5.TabIndex = 7
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Location = New System.Drawing.Point(731, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 289)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "label4"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label35.Location = New System.Drawing.Point(0, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 289)
        Me.Label35.TabIndex = 46
        Me.Label35.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label34.Location = New System.Drawing.Point(0, 290)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(732, 1)
        Me.Label34.TabIndex = 45
        Me.Label34.Text = "label1"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(732, 1)
        Me.Label33.TabIndex = 44
        Me.Label33.Text = "label1"
        '
        'c1Results
        '
        Me.c1Results.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Results.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Results.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Results.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Results.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1Results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Results.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Results.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Results.Location = New System.Drawing.Point(0, 0)
        Me.c1Results.Name = "c1Results"
        Me.c1Results.Rows.DefaultSize = 19
        Me.c1Results.ShowSort = False
        Me.c1Results.Size = New System.Drawing.Size(732, 291)
        Me.c1Results.StyleInfo = resources.GetString("c1Results.StyleInfo")
        Me.c1Results.TabIndex = 4
        '
        'pnlResultSearch
        '
        Me.pnlResultSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlResultSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlResultSearch.Controls.Add(Me.txtResultSearch)
        Me.pnlResultSearch.Controls.Add(Me.Label21)
        Me.pnlResultSearch.Controls.Add(Me.Label20)
        Me.pnlResultSearch.Controls.Add(Me.PictureBox1)
        Me.pnlResultSearch.Controls.Add(Me.label9)
        Me.pnlResultSearch.Controls.Add(Me.Label12)
        Me.pnlResultSearch.Controls.Add(Me.label11)
        Me.pnlResultSearch.Controls.Add(Me.Label10)
        Me.pnlResultSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlResultSearch.Location = New System.Drawing.Point(0, 86)
        Me.pnlResultSearch.Name = "pnlResultSearch"
        Me.pnlResultSearch.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlResultSearch.Size = New System.Drawing.Size(732, 26)
        Me.pnlResultSearch.TabIndex = 3
        Me.pnlResultSearch.Visible = False
        '
        'txtResultSearch
        '
        Me.txtResultSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResultSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtResultSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultSearch.ForeColor = System.Drawing.Color.Black
        Me.txtResultSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtResultSearch.Name = "txtResultSearch"
        Me.txtResultSearch.Size = New System.Drawing.Size(702, 15)
        Me.txtResultSearch.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 19)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(702, 3)
        Me.Label21.TabIndex = 46
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(702, 4)
        Me.Label20.TabIndex = 44
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
        Me.PictureBox1.TabIndex = 41
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(1, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(730, 1)
        Me.label9.TabIndex = 42
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(730, 1)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "label1"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.label11.Location = New System.Drawing.Point(731, 0)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 23)
        Me.label11.TabIndex = 45
        Me.label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "label4"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 56)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(732, 30)
        Me.Panel2.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel3.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel3.Controls.Add(Me.lbl_pnlRight)
        Me.Panel3.Controls.Add(Me.lbl_pnlTop)
        Me.Panel3.Controls.Add(Me.lblResultHeader)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(732, 27)
        Me.Panel3.TabIndex = 5
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 26)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(730, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(731, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 26)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(732, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lblResultHeader
        '
        Me.lblResultHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblResultHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblResultHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblResultHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblResultHeader.Name = "lblResultHeader"
        Me.lblResultHeader.Size = New System.Drawing.Size(732, 27)
        Me.lblResultHeader.TabIndex = 0
        Me.lblResultHeader.Text = " Results"
        Me.lblResultHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlButton
        '
        Me.pnlButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlButton.Controls.Add(Me.ImgResult_Comment)
        Me.pnlButton.Controls.Add(Me.ImgResult)
        Me.pnlButton.Controls.Add(Me.ImgResultHeader)
        Me.pnlButton.Controls.Add(Me.ImgTest)
        Me.pnlButton.Controls.Add(Me.lblResultTestRowNo)
        Me.pnlButton.Controls.Add(Me.ToolStrip1)
        Me.pnlButton.Controls.Add(Me.Label31)
        Me.pnlButton.Controls.Add(Me.Label32)
        Me.pnlButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButton.Location = New System.Drawing.Point(0, 0)
        Me.pnlButton.Name = "pnlButton"
        Me.pnlButton.Size = New System.Drawing.Size(732, 56)
        Me.pnlButton.TabIndex = 1
        '
        'ImgResult_Comment
        '
        Me.ImgResult_Comment.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult_Comment.Image = CType(resources.GetObject("ImgResult_Comment.Image"), System.Drawing.Image)
        Me.ImgResult_Comment.Location = New System.Drawing.Point(352, 16)
        Me.ImgResult_Comment.Name = "ImgResult_Comment"
        Me.ImgResult_Comment.Size = New System.Drawing.Size(26, 20)
        Me.ImgResult_Comment.TabIndex = 19
        Me.ImgResult_Comment.TabStop = False
        Me.ImgResult_Comment.Visible = False
        '
        'ImgResult
        '
        Me.ImgResult.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult.Image = CType(resources.GetObject("ImgResult.Image"), System.Drawing.Image)
        Me.ImgResult.Location = New System.Drawing.Point(604, 19)
        Me.ImgResult.Name = "ImgResult"
        Me.ImgResult.Size = New System.Drawing.Size(23, 20)
        Me.ImgResult.TabIndex = 17
        Me.ImgResult.TabStop = False
        Me.ImgResult.Visible = False
        '
        'ImgResultHeader
        '
        Me.ImgResultHeader.BackColor = System.Drawing.Color.Transparent
        Me.ImgResultHeader.Image = CType(resources.GetObject("ImgResultHeader.Image"), System.Drawing.Image)
        Me.ImgResultHeader.Location = New System.Drawing.Point(642, 19)
        Me.ImgResultHeader.Name = "ImgResultHeader"
        Me.ImgResultHeader.Size = New System.Drawing.Size(23, 20)
        Me.ImgResultHeader.TabIndex = 16
        Me.ImgResultHeader.TabStop = False
        Me.ImgResultHeader.Visible = False
        '
        'ImgTest
        '
        Me.ImgTest.BackColor = System.Drawing.Color.Transparent
        Me.ImgTest.Image = CType(resources.GetObject("ImgTest.Image"), System.Drawing.Image)
        Me.ImgTest.Location = New System.Drawing.Point(575, 17)
        Me.ImgTest.Name = "ImgTest"
        Me.ImgTest.Size = New System.Drawing.Size(21, 24)
        Me.ImgTest.TabIndex = 15
        Me.ImgTest.TabStop = False
        Me.ImgTest.Visible = False
        '
        'lblResultTestRowNo
        '
        Me.lblResultTestRowNo.AutoSize = True
        Me.lblResultTestRowNo.BackColor = System.Drawing.Color.Transparent
        Me.lblResultTestRowNo.Location = New System.Drawing.Point(555, 21)
        Me.lblResultTestRowNo.Name = "lblResultTestRowNo"
        Me.lblResultTestRowNo.Size = New System.Drawing.Size(14, 14)
        Me.lblResultTestRowNo.TabIndex = 14
        Me.lblResultTestRowNo.Text = "0"
        Me.lblResultTestRowNo.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = CType(resources.GetObject("ToolStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(1, 1)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip1.Size = New System.Drawing.Size(731, 53)
        Me.ToolStrip1.TabIndex = 18
        Me.ToolStrip1.Text = "toolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(40, 50)
        Me.ToolStripButton1.Tag = "OK"
        Me.ToolStripButton1.Text = "&Save"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton2.Tag = "Cancel"
        Me.ToolStripButton2.Text = "&Close"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Location = New System.Drawing.Point(0, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 55)
        Me.Label31.TabIndex = 20
        Me.Label31.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(732, 1)
        Me.Label32.TabIndex = 44
        Me.Label32.Text = "label1"
        '
        'pnlInstruction
        '
        Me.pnlInstruction.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlInstruction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInstruction.Controls.Add(Me.pnlInstruction_Detail)
        Me.pnlInstruction.Controls.Add(Me.pnlInstruction_Button)
        Me.pnlInstruction.Controls.Add(Me.pnlInstruction_ButtonBottom)
        Me.pnlInstruction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlInstruction.Location = New System.Drawing.Point(80, 108)
        Me.pnlInstruction.Name = "pnlInstruction"
        Me.pnlInstruction.Size = New System.Drawing.Size(479, 214)
        Me.pnlInstruction.TabIndex = 27
        Me.pnlInstruction.Visible = False
        '
        'pnlInstruction_Detail
        '
        Me.pnlInstruction_Detail.Controls.Add(Me.txtInstruction)
        Me.pnlInstruction_Detail.Controls.Add(Me.lblInstruction_SpliterTop)
        Me.pnlInstruction_Detail.Controls.Add(Me.Label1)
        Me.pnlInstruction_Detail.Controls.Add(Me.Label2)
        Me.pnlInstruction_Detail.Controls.Add(Me.Label3)
        Me.pnlInstruction_Detail.Controls.Add(Me.Label4)
        Me.pnlInstruction_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInstruction_Detail.Location = New System.Drawing.Point(0, 84)
        Me.pnlInstruction_Detail.Name = "pnlInstruction_Detail"
        Me.pnlInstruction_Detail.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInstruction_Detail.Size = New System.Drawing.Size(479, 130)
        Me.pnlInstruction_Detail.TabIndex = 85
        '
        'txtInstruction
        '
        Me.txtInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInstruction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInstruction.ForeColor = System.Drawing.Color.Black
        Me.txtInstruction.Location = New System.Drawing.Point(1, 3)
        Me.txtInstruction.MaxLength = 5000
        Me.txtInstruction.Multiline = True
        Me.txtInstruction.Name = "txtInstruction"
        Me.txtInstruction.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInstruction.Size = New System.Drawing.Size(477, 123)
        Me.txtInstruction.TabIndex = 95
        '
        'lblInstruction_SpliterTop
        '
        Me.lblInstruction_SpliterTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInstruction_SpliterTop.Location = New System.Drawing.Point(1, 1)
        Me.lblInstruction_SpliterTop.Name = "lblInstruction_SpliterTop"
        Me.lblInstruction_SpliterTop.Size = New System.Drawing.Size(477, 2)
        Me.lblInstruction_SpliterTop.TabIndex = 92
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(477, 1)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 126)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(478, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 126)
        Me.Label3.TabIndex = 97
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(479, 1)
        Me.Label4.TabIndex = 96
        Me.Label4.Text = "label1"
        '
        'pnlInstruction_Button
        '
        Me.pnlInstruction_Button.Controls.Add(Me.pnlInstruction_Button1)
        Me.pnlInstruction_Button.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInstruction_Button.Location = New System.Drawing.Point(0, 56)
        Me.pnlInstruction_Button.Name = "pnlInstruction_Button"
        Me.pnlInstruction_Button.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInstruction_Button.Size = New System.Drawing.Size(479, 28)
        Me.pnlInstruction_Button.TabIndex = 94
        '
        'pnlInstruction_Button1
        '
        Me.pnlInstruction_Button1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlInstruction_Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInstruction_Button1.Controls.Add(Me.lblTestDetails_Header)
        Me.pnlInstruction_Button1.Controls.Add(Me.btnInstruction_Up)
        Me.pnlInstruction_Button1.Controls.Add(Me.btnInstruction_Down)
        Me.pnlInstruction_Button1.Controls.Add(Me.Label5)
        Me.pnlInstruction_Button1.Controls.Add(Me.Label6)
        Me.pnlInstruction_Button1.Controls.Add(Me.Label7)
        Me.pnlInstruction_Button1.Controls.Add(Me.Label8)
        Me.pnlInstruction_Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInstruction_Button1.Location = New System.Drawing.Point(0, 0)
        Me.pnlInstruction_Button1.Name = "pnlInstruction_Button1"
        Me.pnlInstruction_Button1.Size = New System.Drawing.Size(479, 25)
        Me.pnlInstruction_Button1.TabIndex = 84
        '
        'lblTestDetails_Header
        '
        Me.lblTestDetails_Header.BackColor = System.Drawing.Color.Transparent
        Me.lblTestDetails_Header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTestDetails_Header.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestDetails_Header.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblTestDetails_Header.Location = New System.Drawing.Point(1, 1)
        Me.lblTestDetails_Header.Name = "lblTestDetails_Header"
        Me.lblTestDetails_Header.Size = New System.Drawing.Size(429, 23)
        Me.lblTestDetails_Header.TabIndex = 3
        Me.lblTestDetails_Header.Text = " Instruction :"
        Me.lblTestDetails_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnInstruction_Up
        '
        Me.btnInstruction_Up.BackColor = System.Drawing.Color.Transparent
        Me.btnInstruction_Up.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnInstruction_Up.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnInstruction_Up.FlatAppearance.BorderSize = 0
        Me.btnInstruction_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstruction_Up.Image = Global.gloUserControlLibrary.My.Resources.Resources.Img_UPBlueArrow
        Me.btnInstruction_Up.Location = New System.Drawing.Point(430, 1)
        Me.btnInstruction_Up.Name = "btnInstruction_Up"
        Me.btnInstruction_Up.Size = New System.Drawing.Size(24, 23)
        Me.btnInstruction_Up.TabIndex = 1
        Me.btnInstruction_Up.UseVisualStyleBackColor = False
        Me.btnInstruction_Up.Visible = False
        '
        'btnInstruction_Down
        '
        Me.btnInstruction_Down.BackColor = System.Drawing.Color.Transparent
        Me.btnInstruction_Down.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnInstruction_Down.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnInstruction_Down.FlatAppearance.BorderSize = 0
        Me.btnInstruction_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstruction_Down.Image = Global.gloUserControlLibrary.My.Resources.Resources.Img_DownBlueArrow
        Me.btnInstruction_Down.Location = New System.Drawing.Point(454, 1)
        Me.btnInstruction_Down.Name = "btnInstruction_Down"
        Me.btnInstruction_Down.Size = New System.Drawing.Size(24, 23)
        Me.btnInstruction_Down.TabIndex = 2
        Me.btnInstruction_Down.UseVisualStyleBackColor = False
        Me.btnInstruction_Down.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(1, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(477, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(478, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 24)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(479, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlInstruction_ButtonBottom
        '
        Me.pnlInstruction_ButtonBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInstruction_ButtonBottom.Controls.Add(Me.tls)
        Me.pnlInstruction_ButtonBottom.Controls.Add(Me.Label37)
        Me.pnlInstruction_ButtonBottom.Controls.Add(Me.Label38)
        Me.pnlInstruction_ButtonBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInstruction_ButtonBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlInstruction_ButtonBottom.Name = "pnlInstruction_ButtonBottom"
        Me.pnlInstruction_ButtonBottom.Size = New System.Drawing.Size(479, 56)
        Me.pnlInstruction_ButtonBottom.TabIndex = 93
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = CType(resources.GetObject("tls.BackgroundImage"), System.Drawing.Image)
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(1, 1)
        Me.tls.Name = "tls"
        Me.tls.Padding = New System.Windows.Forms.Padding(0)
        Me.tls.Size = New System.Drawing.Size(478, 53)
        Me.tls.TabIndex = 1
        Me.tls.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(40, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Location = New System.Drawing.Point(0, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 55)
        Me.Label37.TabIndex = 4
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label38.Location = New System.Drawing.Point(0, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(479, 1)
        Me.Label38.TabIndex = 6
        Me.Label38.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'gloUC_Transaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlInstruction)
        Me.Controls.Add(Me.pnlDiagosisCPT)
        Me.Controls.Add(Me.pnlResults)
        Me.Controls.Add(Me._Flex)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.txtTestResultComment)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_Transaction"
        Me.Size = New System.Drawing.Size(732, 490)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDiagosisCPT.ResumeLayout(False)
        Me.pnl_Base.ResumeLayout(False)
        CType(Me.c1DignosisCPTs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.pnlDgnCPTSearch.ResumeLayout(False)
        Me.pnlDgnCPTSearch.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlDgnCPTToolStrip.ResumeLayout(False)
        Me.pnlDgnCPTToolStrip.PerformLayout()
        Me.tlsp_DgnCPT.ResumeLayout(False)
        Me.tlsp_DgnCPT.PerformLayout()
        Me.pnlResults.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.c1Results, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlResultSearch.ResumeLayout(False)
        Me.pnlResultSearch.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlButton.ResumeLayout(False)
        Me.pnlButton.PerformLayout()
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlInstruction.ResumeLayout(False)
        Me.pnlInstruction_Detail.ResumeLayout(False)
        Me.pnlInstruction_Detail.PerformLayout()
        Me.pnlInstruction_Button.ResumeLayout(False)
        Me.pnlInstruction_Button1.ResumeLayout(False)
        Me.pnlInstruction_ButtonBottom.ResumeLayout(False)
        Me.pnlInstruction_ButtonBottom.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlDiagosisCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlResults As System.Windows.Forms.Panel
    Friend WithEvents lblResultHeader As System.Windows.Forms.Label
    Friend WithEvents pnlButton As System.Windows.Forms.Panel
    Friend WithEvents pnlDgnCPTToolStrip As System.Windows.Forms.Panel
    Friend WithEvents lblDigCPTHeader As System.Windows.Forms.Label
    Friend WithEvents pnlResultSearch As System.Windows.Forms.Panel
    Friend WithEvents txtDgnCPTSearch As System.Windows.Forms.TextBox
    Friend WithEvents c1DignosisCPTs As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents c1Results As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblResultTestRowNo As System.Windows.Forms.Label
    Friend WithEvents txtResultSearch As System.Windows.Forms.TextBox
    Friend WithEvents ImgTest As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResult As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResultHeader As System.Windows.Forms.PictureBox
    Friend WithEvents pnlInstruction As System.Windows.Forms.Panel
    Friend WithEvents pnlInstruction_Button1 As System.Windows.Forms.Panel
    Friend WithEvents lblTestDetails_Header As System.Windows.Forms.Label
    Friend WithEvents btnInstruction_Up As System.Windows.Forms.Button
    Friend WithEvents btnInstruction_Down As System.Windows.Forms.Button
    Friend WithEvents pnlInstruction_Detail As System.Windows.Forms.Panel
    Friend WithEvents txtInstruction As System.Windows.Forms.TextBox
    Friend WithEvents pnlInstruction_ButtonBottom As System.Windows.Forms.Panel
    Friend WithEvents lblInstruction_SpliterTop As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents pnlDgnCPTSearch As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents tlsp_DgnCPT As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlsp_btnOK As System.Windows.Forms.ToolStripButton
    Private WithEvents tlsp_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents txtTestResultComment As System.Windows.Forms.TextBox
    Friend WithEvents ImgResult_Comment As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents pnlInstruction_Button As System.Windows.Forms.Panel
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lbl_SearchColumn As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label

End Class
