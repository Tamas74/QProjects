<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_TransactionHistory
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then

            Try
                If (IsNothing(dtpToDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                    Catch ex As Exception

                    End Try


                    dtpToDate.Dispose()
                    dtpToDate = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtpFrom) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFrom)
                    Catch ex As Exception

                    End Try


                    dtpFrom.Dispose()
                    dtpFrom = Nothing
                End If
            Catch
            End Try

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If (IsNothing(_Flex.ContextMenuStrip) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenuStrip)
                    If (IsNothing(_Flex.ContextMenuStrip.Items) = False) Then
                        _Flex.ContextMenuStrip.Items.Clear()
                    End If
                    _Flex.ContextMenuStrip.Dispose()
                    _Flex.ContextMenuStrip = Nothing
                End If
            Catch

            End Try
            Try
                If IsNothing(_Flex.ContextMenu) = False Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(_Flex.ContextMenu)
                    If (IsNothing(_Flex.ContextMenu.MenuItems) = False) Then
                        _Flex.ContextMenu.MenuItems.Clear()
                    End If
                    _Flex.ContextMenu.Dispose()
                    _Flex.ContextMenu = Nothing
                End If
            Catch ex As Exception

            End Try
            components.Dispose()
        End If
        ClearGlobalVariables()
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_TransactionHistory))
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlFlexGrid = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ImgTest = New System.Windows.Forms.PictureBox()
        Me.ImgResult = New System.Windows.Forms.PictureBox()
        Me.ImgResultHeader = New System.Windows.Forms.PictureBox()
        Me.ImgResult_Comment = New System.Windows.Forms.PictureBox()
        Me.ImgAttachment = New System.Windows.Forms.PictureBox()
        Me.ImgResult_Flask = New System.Windows.Forms.PictureBox()
        Me.ImgResultHeader_Flask = New System.Windows.Forms.PictureBox()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearchResultGrdText = New System.Windows.Forms.TextBox()
        Me.lblSearchBy = New System.Windows.Forms.Label()
        Me.lblSearchColumnName = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlSelectCriteria1 = New System.Windows.Forms.Panel()
        Me.ChkPrior = New System.Windows.Forms.CheckBox()
        Me.btnShowGraph = New System.Windows.Forms.Button()
        Me.pnlselectType = New System.Windows.Forms.Panel()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkDate = New System.Windows.Forms.CheckBox()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnCloseRefill = New System.Windows.Forms.Button()
        Me.cmbCriteria = New System.Windows.Forms.ComboBox()
        Me.lblSelectCrieteria = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlSelectCriteria = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.txtTestResultComment = New System.Windows.Forms.RichTextBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ImgContextMenu = New System.Windows.Forms.ImageList(Me.components)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFlexGrid.SuspendLayout()
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgAttachment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResult_Flask, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImgResultHeader_Flask, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlSelectCriteria1.SuspendLayout()
        Me.pnlselectType.SuspendLayout()
        Me.pnlSelectCriteria.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        '_Flex
        '
        Me._Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.ColumnInfo = "10,1,0,0,0,105,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.ExtendLastCol = True
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(0, 0)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 21
        Me._Flex.ShowCellLabels = True
        Me._Flex.Size = New System.Drawing.Size(974, 296)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 29
        '
        'pnlFlexGrid
        '
        Me.pnlFlexGrid.Controls.Add(Me.Label5)
        Me.pnlFlexGrid.Controls.Add(Me.Label6)
        Me.pnlFlexGrid.Controls.Add(Me.Label7)
        Me.pnlFlexGrid.Controls.Add(Me.Label8)
        Me.pnlFlexGrid.Controls.Add(Me._Flex)
        Me.pnlFlexGrid.Controls.Add(Me.ImgTest)
        Me.pnlFlexGrid.Controls.Add(Me.ImgResult)
        Me.pnlFlexGrid.Controls.Add(Me.ImgResultHeader)
        Me.pnlFlexGrid.Controls.Add(Me.ImgResult_Comment)
        Me.pnlFlexGrid.Controls.Add(Me.ImgAttachment)
        Me.pnlFlexGrid.Controls.Add(Me.ImgResult_Flask)
        Me.pnlFlexGrid.Controls.Add(Me.ImgResultHeader_Flask)
        Me.pnlFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFlexGrid.Location = New System.Drawing.Point(0, 54)
        Me.pnlFlexGrid.Name = "pnlFlexGrid"
        Me.pnlFlexGrid.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlFlexGrid.Size = New System.Drawing.Size(974, 299)
        Me.pnlFlexGrid.TabIndex = 33
        Me.pnlFlexGrid.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(1, 295)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(972, 1)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 295)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(973, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 295)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(974, 1)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "label1"
        '
        'ImgTest
        '
        Me.ImgTest.BackColor = System.Drawing.Color.Transparent
        Me.ImgTest.Image = CType(resources.GetObject("ImgTest.Image"), System.Drawing.Image)
        Me.ImgTest.Location = New System.Drawing.Point(525, 329)
        Me.ImgTest.Name = "ImgTest"
        Me.ImgTest.Size = New System.Drawing.Size(21, 24)
        Me.ImgTest.TabIndex = 30
        Me.ImgTest.TabStop = False
        Me.ImgTest.Visible = False
        '
        'ImgResult
        '
        Me.ImgResult.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult.Image = CType(resources.GetObject("ImgResult.Image"), System.Drawing.Image)
        Me.ImgResult.Location = New System.Drawing.Point(555, 332)
        Me.ImgResult.Name = "ImgResult"
        Me.ImgResult.Size = New System.Drawing.Size(23, 20)
        Me.ImgResult.TabIndex = 32
        Me.ImgResult.TabStop = False
        Me.ImgResult.Visible = False
        '
        'ImgResultHeader
        '
        Me.ImgResultHeader.BackColor = System.Drawing.Color.Transparent
        Me.ImgResultHeader.Image = CType(resources.GetObject("ImgResultHeader.Image"), System.Drawing.Image)
        Me.ImgResultHeader.Location = New System.Drawing.Point(592, 332)
        Me.ImgResultHeader.Name = "ImgResultHeader"
        Me.ImgResultHeader.Size = New System.Drawing.Size(23, 20)
        Me.ImgResultHeader.TabIndex = 31
        Me.ImgResultHeader.TabStop = False
        Me.ImgResultHeader.Visible = False
        '
        'ImgResult_Comment
        '
        Me.ImgResult_Comment.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult_Comment.Image = CType(resources.GetObject("ImgResult_Comment.Image"), System.Drawing.Image)
        Me.ImgResult_Comment.Location = New System.Drawing.Point(410, 139)
        Me.ImgResult_Comment.Name = "ImgResult_Comment"
        Me.ImgResult_Comment.Size = New System.Drawing.Size(26, 20)
        Me.ImgResult_Comment.TabIndex = 48
        Me.ImgResult_Comment.TabStop = False
        Me.ImgResult_Comment.Visible = False
        '
        'ImgAttachment
        '
        Me.ImgAttachment.BackColor = System.Drawing.Color.Transparent
        Me.ImgAttachment.Image = CType(resources.GetObject("ImgAttachment.Image"), System.Drawing.Image)
        Me.ImgAttachment.Location = New System.Drawing.Point(413, 137)
        Me.ImgAttachment.Name = "ImgAttachment"
        Me.ImgAttachment.Size = New System.Drawing.Size(21, 24)
        Me.ImgAttachment.TabIndex = 47
        Me.ImgAttachment.TabStop = False
        Me.ImgAttachment.Visible = False
        '
        'ImgResult_Flask
        '
        Me.ImgResult_Flask.BackColor = System.Drawing.Color.Transparent
        Me.ImgResult_Flask.Image = CType(resources.GetObject("ImgResult_Flask.Image"), System.Drawing.Image)
        Me.ImgResult_Flask.Location = New System.Drawing.Point(421, 145)
        Me.ImgResult_Flask.Name = "ImgResult_Flask"
        Me.ImgResult_Flask.Size = New System.Drawing.Size(21, 24)
        Me.ImgResult_Flask.TabIndex = 49
        Me.ImgResult_Flask.TabStop = False
        Me.ImgResult_Flask.Visible = False
        '
        'ImgResultHeader_Flask
        '
        Me.ImgResultHeader_Flask.BackColor = System.Drawing.Color.Transparent
        Me.ImgResultHeader_Flask.Image = CType(resources.GetObject("ImgResultHeader_Flask.Image"), System.Drawing.Image)
        Me.ImgResultHeader_Flask.Location = New System.Drawing.Point(429, 153)
        Me.ImgResultHeader_Flask.Name = "ImgResultHeader_Flask"
        Me.ImgResultHeader_Flask.Size = New System.Drawing.Size(21, 24)
        Me.ImgResultHeader_Flask.TabIndex = 50
        Me.ImgResultHeader_Flask.TabStop = False
        Me.ImgResultHeader_Flask.Visible = False
        '
        'pnlSearch
        '
        Me.pnlSearch.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.txtSearchResultGrdText)
        Me.pnlSearch.Controls.Add(Me.lblSearchBy)
        Me.pnlSearch.Controls.Add(Me.lblSearchColumnName)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlRight)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlTop)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(974, 24)
        Me.pnlSearch.TabIndex = 33
        '
        'txtSearchResultGrdText
        '
        Me.txtSearchResultGrdText.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearchResultGrdText.ForeColor = System.Drawing.Color.Black
        Me.txtSearchResultGrdText.Location = New System.Drawing.Point(92, 1)
        Me.txtSearchResultGrdText.Name = "txtSearchResultGrdText"
        Me.txtSearchResultGrdText.Size = New System.Drawing.Size(158, 22)
        Me.txtSearchResultGrdText.TabIndex = 1
        '
        'lblSearchBy
        '
        Me.lblSearchBy.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchBy.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearchBy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchBy.Location = New System.Drawing.Point(1, 1)
        Me.lblSearchBy.Name = "lblSearchBy"
        Me.lblSearchBy.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblSearchBy.Size = New System.Drawing.Size(91, 22)
        Me.lblSearchBy.TabIndex = 0
        Me.lblSearchBy.Text = "  Search by : "
        Me.lblSearchBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSearchColumnName
        '
        Me.lblSearchColumnName.AutoSize = True
        Me.lblSearchColumnName.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchColumnName.Location = New System.Drawing.Point(80, 6)
        Me.lblSearchColumnName.Name = "lblSearchColumnName"
        Me.lblSearchColumnName.Size = New System.Drawing.Size(0, 14)
        Me.lblSearchColumnName.TabIndex = 8
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 23)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(972, 1)
        Me.lbl_pnlBottom.TabIndex = 12
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlLeft.TabIndex = 11
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(973, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlRight.TabIndex = 10
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(974, 1)
        Me.lbl_pnlTop.TabIndex = 9
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlSelectCriteria1
        '
        Me.pnlSelectCriteria1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlSelectCriteria1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectCriteria1.Controls.Add(Me.ChkPrior)
        Me.pnlSelectCriteria1.Controls.Add(Me.btnShowGraph)
        Me.pnlSelectCriteria1.Controls.Add(Me.pnlselectType)
        Me.pnlSelectCriteria1.Controls.Add(Me.dtpToDate)
        Me.pnlSelectCriteria1.Controls.Add(Me.lblToDate)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label13)
        Me.pnlSelectCriteria1.Controls.Add(Me.dtpFrom)
        Me.pnlSelectCriteria1.Controls.Add(Me.chkDate)
        Me.pnlSelectCriteria1.Controls.Add(Me.lblFromDate)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label12)
        Me.pnlSelectCriteria1.Controls.Add(Me.btnRefresh)
        Me.pnlSelectCriteria1.Controls.Add(Me.btnCloseRefill)
        Me.pnlSelectCriteria1.Controls.Add(Me.cmbCriteria)
        Me.pnlSelectCriteria1.Controls.Add(Me.lblSelectCrieteria)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label1)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label2)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label3)
        Me.pnlSelectCriteria1.Controls.Add(Me.Label4)
        Me.pnlSelectCriteria1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectCriteria1.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelectCriteria1.Name = "pnlSelectCriteria1"
        Me.pnlSelectCriteria1.Size = New System.Drawing.Size(974, 24)
        Me.pnlSelectCriteria1.TabIndex = 29
        '
        'ChkPrior
        '
        Me.ChkPrior.AutoSize = True
        Me.ChkPrior.BackColor = System.Drawing.Color.Transparent
        Me.ChkPrior.Dock = System.Windows.Forms.DockStyle.Right
        Me.ChkPrior.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ChkPrior.Location = New System.Drawing.Point(762, 1)
        Me.ChkPrior.Name = "ChkPrior"
        Me.ChkPrior.Size = New System.Drawing.Size(139, 22)
        Me.ChkPrior.TabIndex = 41
        Me.ChkPrior.Text = "Show prior results"
        Me.ChkPrior.UseVisualStyleBackColor = False
        '
        'btnShowGraph
        '
        Me.btnShowGraph.BackColor = System.Drawing.Color.Transparent
        Me.btnShowGraph.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnShowGraph.FlatAppearance.BorderSize = 0
        Me.btnShowGraph.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnShowGraph.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnShowGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowGraph.Image = CType(resources.GetObject("btnShowGraph.Image"), System.Drawing.Image)
        Me.btnShowGraph.Location = New System.Drawing.Point(901, 1)
        Me.btnShowGraph.Name = "btnShowGraph"
        Me.btnShowGraph.Size = New System.Drawing.Size(24, 22)
        Me.btnShowGraph.TabIndex = 46
        Me.btnShowGraph.UseVisualStyleBackColor = False
        Me.btnShowGraph.Visible = False
        '
        'pnlselectType
        '
        Me.pnlselectType.BackColor = System.Drawing.Color.Transparent
        Me.pnlselectType.Controls.Add(Me.cmbType)
        Me.pnlselectType.Controls.Add(Me.lblType)
        Me.pnlselectType.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlselectType.Location = New System.Drawing.Point(541, 1)
        Me.pnlselectType.Name = "pnlselectType"
        Me.pnlselectType.Size = New System.Drawing.Size(215, 22)
        Me.pnlselectType.TabIndex = 38
        '
        'cmbType
        '
        Me.cmbType.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.ForeColor = System.Drawing.Color.Black
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(100, 0)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(112, 22)
        Me.cmbType.TabIndex = 39
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(0, 0)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(100, 22)
        Me.lblType.TabIndex = 38
        Me.lblType.Text = "Type :"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(455, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(86, 22)
        Me.dtpToDate.TabIndex = 35
        '
        'lblToDate
        '
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(359, 1)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(96, 22)
        Me.lblToDate.TabIndex = 34
        Me.lblToDate.Text = "Reported To :"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(354, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(5, 22)
        Me.Label13.TabIndex = 45
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFrom.Enabled = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(268, 1)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(86, 22)
        Me.dtpFrom.TabIndex = 33
        '
        'chkDate
        '
        Me.chkDate.AutoSize = True
        Me.chkDate.BackColor = System.Drawing.Color.Transparent
        Me.chkDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDate.Location = New System.Drawing.Point(253, 1)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.chkDate.Size = New System.Drawing.Size(15, 22)
        Me.chkDate.TabIndex = 47
        Me.chkDate.UseVisualStyleBackColor = False
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(144, 1)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(109, 22)
        Me.lblFromDate.TabIndex = 32
        Me.lblFromDate.Text = "Reported From :"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(134, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 22)
        Me.Label12.TabIndex = 44
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(925, 1)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(24, 22)
        Me.btnRefresh.TabIndex = 39
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnCloseRefill
        '
        Me.btnCloseRefill.BackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCloseRefill.FlatAppearance.BorderSize = 0
        Me.btnCloseRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseRefill.Image = CType(resources.GetObject("btnCloseRefill.Image"), System.Drawing.Image)
        Me.btnCloseRefill.Location = New System.Drawing.Point(949, 1)
        Me.btnCloseRefill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCloseRefill.Name = "btnCloseRefill"
        Me.btnCloseRefill.Size = New System.Drawing.Size(24, 22)
        Me.btnCloseRefill.TabIndex = 29
        Me.btnCloseRefill.UseVisualStyleBackColor = False
        Me.btnCloseRefill.Visible = False
        '
        'cmbCriteria
        '
        Me.cmbCriteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbCriteria.FormattingEnabled = True
        Me.cmbCriteria.Location = New System.Drawing.Point(73, 1)
        Me.cmbCriteria.Name = "cmbCriteria"
        Me.cmbCriteria.Size = New System.Drawing.Size(61, 22)
        Me.cmbCriteria.TabIndex = 9
        '
        'lblSelectCrieteria
        '
        Me.lblSelectCrieteria.BackColor = System.Drawing.Color.Transparent
        Me.lblSelectCrieteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSelectCrieteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectCrieteria.Location = New System.Drawing.Point(1, 1)
        Me.lblSelectCrieteria.Name = "lblSelectCrieteria"
        Me.lblSelectCrieteria.Size = New System.Drawing.Size(72, 22)
        Me.lblSelectCrieteria.TabIndex = 8
        Me.lblSelectCrieteria.Text = "  Sort by :"
        Me.lblSelectCrieteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(972, 1)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(973, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(974, 1)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "label1"
        '
        'pnlSelectCriteria
        '
        Me.pnlSelectCriteria.Controls.Add(Me.pnlSelectCriteria1)
        Me.pnlSelectCriteria.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectCriteria.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelectCriteria.Name = "pnlSelectCriteria"
        Me.pnlSelectCriteria.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectCriteria.Size = New System.Drawing.Size(974, 27)
        Me.pnlSelectCriteria.TabIndex = 34
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(369, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(20, 28)
        Me.Label9.TabIndex = 35
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(296, 77)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 28)
        Me.Label10.TabIndex = 35
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(434, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 28)
        Me.Label11.TabIndex = 35
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 27)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(974, 27)
        Me.Panel2.TabIndex = 36
        Me.Panel2.Visible = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'txtTestResultComment
        '
        Me.txtTestResultComment.BackColor = System.Drawing.Color.White
        Me.txtTestResultComment.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtTestResultComment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestResultComment.ForeColor = System.Drawing.Color.Black
        Me.txtTestResultComment.Location = New System.Drawing.Point(0, 356)
        Me.txtTestResultComment.Name = "txtTestResultComment"
        Me.txtTestResultComment.ReadOnly = True
        Me.txtTestResultComment.Size = New System.Drawing.Size(974, 71)
        Me.txtTestResultComment.TabIndex = 37
        Me.txtTestResultComment.Text = ""
        Me.txtTestResultComment.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 353)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(974, 3)
        Me.Splitter1.TabIndex = 39
        Me.Splitter1.TabStop = False
        Me.Splitter1.Visible = False
        '
        'ImgContextMenu
        '
        Me.ImgContextMenu.ImageStream = CType(resources.GetObject("ImgContextMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgContextMenu.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgContextMenu.Images.SetKeyName(0, "Add Result.ico")
        Me.ImgContextMenu.Images.SetKeyName(1, "Add Result Document.ico")
        Me.ImgContextMenu.Images.SetKeyName(2, "View Result Document.ico")
        Me.ImgContextMenu.Images.SetKeyName(3, "Remove Result Document.ico")
        Me.ImgContextMenu.Images.SetKeyName(4, "Add Result URL Doc.ico")
        Me.ImgContextMenu.Images.SetKeyName(5, "View Result URL Doc.ico")
        Me.ImgContextMenu.Images.SetKeyName(6, "Edit View URL Doc.ico")
        Me.ImgContextMenu.Images.SetKeyName(7, "Remove Result URL Doc.ico")
        Me.ImgContextMenu.Images.SetKeyName(8, "Remove Test.ico")
        Me.ImgContextMenu.Images.SetKeyName(9, "Bullet06.ico")
        Me.ImgContextMenu.Images.SetKeyName(10, "Modify Order.ico")
        Me.ImgContextMenu.Images.SetKeyName(11, "View Order.ico")
        Me.ImgContextMenu.Images.SetKeyName(12, "Generate CDA.ico")
        '
        'gloUC_TransactionHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlFlexGrid)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.txtTestResultComment)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.pnlSelectCriteria)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUC_TransactionHistory"
        Me.Size = New System.Drawing.Size(974, 427)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFlexGrid.ResumeLayout(False)
        CType(Me.ImgTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResultHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult_Comment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgAttachment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResult_Flask, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImgResultHeader_Flask, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlSelectCriteria1.ResumeLayout(False)
        Me.pnlSelectCriteria1.PerformLayout()
        Me.pnlselectType.ResumeLayout(False)
        Me.pnlSelectCriteria.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSearchResultGrdText As System.Windows.Forms.TextBox
    Friend WithEvents lblSearchColumnName As System.Windows.Forms.Label
    Friend WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImgResult As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResultHeader As System.Windows.Forms.PictureBox
    Friend WithEvents ImgTest As System.Windows.Forms.PictureBox
    Friend WithEvents pnlSelectCriteria1 As System.Windows.Forms.Panel
    Friend WithEvents lblSelectCrieteria As System.Windows.Forms.Label
    Public WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents pnlFlexGrid As System.Windows.Forms.Panel
    Protected WithEvents btnCloseRefill As System.Windows.Forms.Button
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Public WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents pnlselectType As System.Windows.Forms.Panel
    Protected WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblSearchBy As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectCriteria As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnShowGraph As System.Windows.Forms.Button
    Friend WithEvents txtTestResultComment As System.Windows.Forms.RichTextBox
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ChkPrior As System.Windows.Forms.CheckBox
    Friend WithEvents chkDate As System.Windows.Forms.CheckBox
    Friend WithEvents ImgResult_Comment As System.Windows.Forms.PictureBox
    Friend WithEvents ImgAttachment As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResult_Flask As System.Windows.Forms.PictureBox
    Friend WithEvents ImgResultHeader_Flask As System.Windows.Forms.PictureBox
    Friend WithEvents ImgContextMenu As System.Windows.Forms.ImageList
End Class
