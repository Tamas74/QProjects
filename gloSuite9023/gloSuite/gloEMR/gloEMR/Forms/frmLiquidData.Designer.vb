<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiquidData
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cmnuDelete}

                Dim CmpMControls() As System.Windows.Forms.ContextMenu = {CntData}
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try


 

                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                    End If
                End If



                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                    End If
                End If
                



                If (IsNothing(CmpMControls) = False) Then
                    If CmpMControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpMControls)
                    End If
                End If

                If (IsNothing(CmpMControls) = False) Then
                    If CmpMControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenu(CmpMControls)
                    End If
                End If

                

                If (IsNothing(objTemplate) = False) Then
                    objTemplate.Dispose()
                    objTemplate = Nothing
                End If
                If (IsNothing(objclsPatientROS) = False) Then
                    objclsPatientROS.Dispose()
                    objclsPatientROS = Nothing
                End If
                If (IsNothing(dtElement) = False) Then
                    dtElement.Dispose()
                    dtElement = Nothing
                End If
                If (IsNothing(dtSubElement) = False) Then
                    dtSubElement.Dispose()
                    dtSubElement = Nothing
                End If
                If (IsNothing(dtSubElemetGroup) = False) Then
                    dtSubElemetGroup.Dispose()
                    dtSubElemetGroup = Nothing
                End If
                If (IsNothing(dv) = False) Then
                    dv.Dispose()
                    dv = Nothing
                End If
                If (IsNothing(mylist) = False) Then
                    mylist.Dispose()
                    mylist = Nothing
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLiquidData))
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlEdit = New System.Windows.Forms.Panel()
        Me.pnlTableEntry = New System.Windows.Forms.Panel()
        Me.pnldgTableField = New System.Windows.Forms.Panel()
        Me.dgTableField = New System.Windows.Forms.DataGridView()
        Me.Col_Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Item = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HiddenCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sHiddenControlType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sColumnType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_AssociatedCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col_AssociatedItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HiddenAssociatedCategory = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_AssociatedPropertyName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnTableUp = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnTableDown = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlAddCategory = New System.Windows.Forms.Panel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.chkAssociateStd = New System.Windows.Forms.CheckBox()
        Me.pnlStandardEM = New System.Windows.Forms.Panel()
        Me.grbEM = New System.Windows.Forms.GroupBox()
        Me.btnaddcategory = New System.Windows.Forms.Button()
        Me.btnaddstandreddata = New System.Windows.Forms.Button()
        Me.cmbAssociateSubItem = New System.Windows.Forms.ComboBox()
        Me.cmbAssoicatedItem = New System.Windows.Forms.ComboBox()
        Me.cmbAssociatedCategory = New System.Windows.Forms.ComboBox()
        Me.lblAssociateSubItem = New System.Windows.Forms.Label()
        Me.lblAssociatedCategory = New System.Windows.Forms.Label()
        Me.lblAssoicatedItem = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblControl = New System.Windows.Forms.Label()
        Me.CmbControl = New System.Windows.Forms.ComboBox()
        Me.lblcaption = New System.Windows.Forms.Label()
        Me.txtCaption = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnCatModify = New System.Windows.Forms.Button()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.trvstd = New System.Windows.Forms.TreeView()
        Me.txtcategory = New System.Windows.Forms.TextBox()
        Me.btncatAdd = New System.Windows.Forms.Button()
        Me.txtCatItem = New System.Windows.Forms.TextBox()
        Me.lblitem = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlFieldValues = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgItemList = New System.Windows.Forms.DataGridView()
        Me.Col_HiddenID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CotrolType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HiddenAssociatedItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.btnItemUp = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnItemDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkAssociatestddata = New System.Windows.Forms.CheckBox()
        Me.pnlHPIExtended = New System.Windows.Forms.Panel()
        Me.RdbtnExtended = New System.Windows.Forms.RadioButton()
        Me.RdbtnBrief = New System.Windows.Forms.RadioButton()
        Me.chckRequired = New System.Windows.Forms.CheckBox()
        Me.pnlassociateStdItem = New System.Windows.Forms.Panel()
        Me.btnaddassociated = New System.Windows.Forms.Button()
        Me.btnaddfieldvalue = New System.Windows.Forms.Button()
        Me.cmbstddata = New System.Windows.Forms.ComboBox()
        Me.lblstdData = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFieldcategory = New System.Windows.Forms.Label()
        Me.cmbFieldCategory = New System.Windows.Forms.ComboBox()
        Me.pnlBtns = New System.Windows.Forms.Panel()
        Me.btn_Refresh = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnModify = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtField = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnltrvDiscrete = New System.Windows.Forms.Panel()
        Me.trvDiscrete = New System.Windows.Forms.TreeView()
        Me.imgTemplate = New System.Windows.Forms.ImageList(Me.components)
        Me.Label33 = New System.Windows.Forms.Label()
        Me.wdTemp = New AxDSOFramer.AxFramerControl()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlLiquidDataDictionaryHeader = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblLiquidDataDictionary = New System.Windows.Forms.Label()
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel()
        Me.tlsLiquidData = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.CntData = New System.Windows.Forms.ContextMenu()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmnuDelete = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.pnlMain.SuspendLayout()
        Me.pnlEdit.SuspendLayout()
        Me.pnlTableEntry.SuspendLayout()
        Me.pnldgTableField.SuspendLayout()
        CType(Me.dgTableField, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlAddCategory.SuspendLayout()
        Me.pnlStandardEM.SuspendLayout()
        Me.grbEM.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlFieldValues.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlHPIExtended.SuspendLayout()
        Me.pnlassociateStdItem.SuspendLayout()
        Me.pnlBtns.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnltrvDiscrete.SuspendLayout()
        CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLiquidDataDictionaryHeader.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tlsLiquidData.SuspendLayout()
        Me.cmnuDelete.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlEdit)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Controls.Add(Me.pnl_ToolStrip)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1192, 767)
        Me.pnlMain.TabIndex = 0
        '
        'pnlEdit
        '
        Me.pnlEdit.BackColor = System.Drawing.Color.Transparent
        Me.pnlEdit.Controls.Add(Me.pnlTableEntry)
        Me.pnlEdit.Controls.Add(Me.pnlFieldValues)
        Me.pnlEdit.Controls.Add(Me.Panel2)
        Me.pnlEdit.Controls.Add(Me.pnlFooter)
        Me.pnlEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEdit.Location = New System.Drawing.Point(262, 56)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Size = New System.Drawing.Size(930, 711)
        Me.pnlEdit.TabIndex = 12
        '
        'pnlTableEntry
        '
        Me.pnlTableEntry.Controls.Add(Me.pnldgTableField)
        Me.pnlTableEntry.Controls.Add(Me.Panel1)
        Me.pnlTableEntry.Controls.Add(Me.pnlAddCategory)
        Me.pnlTableEntry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTableEntry.Location = New System.Drawing.Point(0, 405)
        Me.pnlTableEntry.Name = "pnlTableEntry"
        Me.pnlTableEntry.Size = New System.Drawing.Size(930, 306)
        Me.pnlTableEntry.TabIndex = 17
        Me.pnlTableEntry.Visible = False
        '
        'pnldgTableField
        '
        Me.pnldgTableField.Controls.Add(Me.dgTableField)
        Me.pnldgTableField.Controls.Add(Me.Label23)
        Me.pnldgTableField.Controls.Add(Me.Label24)
        Me.pnldgTableField.Controls.Add(Me.Label25)
        Me.pnldgTableField.Controls.Add(Me.Label26)
        Me.pnldgTableField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnldgTableField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnldgTableField.Location = New System.Drawing.Point(0, 191)
        Me.pnldgTableField.Name = "pnldgTableField"
        Me.pnldgTableField.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnldgTableField.Size = New System.Drawing.Size(930, 115)
        Me.pnldgTableField.TabIndex = 11
        '
        'dgTableField
        '
        Me.dgTableField.AllowUserToAddRows = False
        Me.dgTableField.AllowUserToDeleteRows = False
        Me.dgTableField.AllowUserToResizeColumns = False
        Me.dgTableField.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dgTableField.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgTableField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgTableField.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgTableField.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgTableField.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgTableField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgTableField.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Category, Me.Col_Item, Me.Col_HiddenCategory, Me.sHiddenControlType, Me.sColumnType, Me.col_AssociatedCategory, Me.col_AssociatedItem, Me.Col_HiddenAssociatedCategory, Me.Col_AssociatedPropertyName})
        Me.dgTableField.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgTableField.EnableHeadersVisualStyles = False
        Me.dgTableField.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgTableField.Location = New System.Drawing.Point(1, 4)
        Me.dgTableField.MultiSelect = False
        Me.dgTableField.Name = "dgTableField"
        Me.dgTableField.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.dgTableField.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgTableField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgTableField.Size = New System.Drawing.Size(928, 107)
        Me.dgTableField.TabIndex = 0
        '
        'Col_Category
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.Col_Category.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_Category.HeaderText = "Category"
        Me.Col_Category.Name = "Col_Category"
        Me.Col_Category.ReadOnly = True
        Me.Col_Category.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_Item
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.Col_Item.DefaultCellStyle = DataGridViewCellStyle4
        Me.Col_Item.HeaderText = "Item"
        Me.Col_Item.Name = "Col_Item"
        Me.Col_Item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_HiddenCategory
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.Col_HiddenCategory.DefaultCellStyle = DataGridViewCellStyle5
        Me.Col_HiddenCategory.HeaderText = "Hiddent Category"
        Me.Col_HiddenCategory.Name = "Col_HiddenCategory"
        Me.Col_HiddenCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_HiddenCategory.Visible = False
        '
        'sHiddenControlType
        '
        Me.sHiddenControlType.HeaderText = "Hidden Column Type"
        Me.sHiddenControlType.Name = "sHiddenControlType"
        Me.sHiddenControlType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.sHiddenControlType.Visible = False
        '
        'sColumnType
        '
        Me.sColumnType.HeaderText = "Control Type"
        Me.sColumnType.Name = "sColumnType"
        Me.sColumnType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'col_AssociatedCategory
        '
        Me.col_AssociatedCategory.HeaderText = "Associated Category"
        Me.col_AssociatedCategory.Name = "col_AssociatedCategory"
        Me.col_AssociatedCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_AssociatedCategory.Visible = False
        '
        'col_AssociatedItem
        '
        Me.col_AssociatedItem.HeaderText = "AssociatedItem"
        Me.col_AssociatedItem.Name = "col_AssociatedItem"
        Me.col_AssociatedItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_AssociatedItem.Visible = False
        '
        'Col_HiddenAssociatedCategory
        '
        Me.Col_HiddenAssociatedCategory.HeaderText = "Associated Hidden Category"
        Me.Col_HiddenAssociatedCategory.Name = "Col_HiddenAssociatedCategory"
        Me.Col_HiddenAssociatedCategory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_HiddenAssociatedCategory.Visible = False
        '
        'Col_AssociatedPropertyName
        '
        Me.Col_AssociatedPropertyName.HeaderText = "AssociatedPropertyName"
        Me.Col_AssociatedPropertyName.Name = "Col_AssociatedPropertyName"
        Me.Col_AssociatedPropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(1, 111)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(928, 1)
        Me.Label23.TabIndex = 52
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1, 3)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(928, 1)
        Me.Label24.TabIndex = 51
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(929, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 109)
        Me.Label25.TabIndex = 50
        Me.Label25.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 109)
        Me.Label26.TabIndex = 49
        Me.Label26.Text = "label4"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btnTableUp)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.btnTableDown)
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label40)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 166)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(930, 25)
        Me.Panel1.TabIndex = 59
        '
        'btnTableUp
        '
        Me.btnTableUp.BackColor = System.Drawing.Color.Transparent
        Me.btnTableUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnTableUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTableUp.FlatAppearance.BorderSize = 0
        Me.btnTableUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTableUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTableUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTableUp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTableUp.Image = CType(resources.GetObject("btnTableUp.Image"), System.Drawing.Image)
        Me.btnTableUp.Location = New System.Drawing.Point(885, 1)
        Me.btnTableUp.Name = "btnTableUp"
        Me.btnTableUp.Size = New System.Drawing.Size(22, 23)
        Me.btnTableUp.TabIndex = 10
        Me.btnTableUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnTableUp.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(1, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 23)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Fields :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button4
        '
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(803, 0)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(24, 24)
        Me.Button4.TabIndex = 4
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'btnTableDown
        '
        Me.btnTableDown.BackColor = System.Drawing.Color.Transparent
        Me.btnTableDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnTableDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTableDown.FlatAppearance.BorderSize = 0
        Me.btnTableDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTableDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTableDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTableDown.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTableDown.Image = CType(resources.GetObject("btnTableDown.Image"), System.Drawing.Image)
        Me.btnTableDown.Location = New System.Drawing.Point(907, 1)
        Me.btnTableDown.Name = "btnTableDown"
        Me.btnTableDown.Size = New System.Drawing.Size(22, 23)
        Me.btnTableDown.TabIndex = 9
        Me.btnTableDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnTableDown.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.AutoSize = True
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Button6.Location = New System.Drawing.Point(833, 0)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(24, 24)
        Me.Button6.TabIndex = 3
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1, 24)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(928, 1)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 24)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(929, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 24)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(930, 1)
        Me.Label40.TabIndex = 5
        Me.Label40.Text = "label1"
        '
        'pnlAddCategory
        '
        Me.pnlAddCategory.Controls.Add(Me.Label47)
        Me.pnlAddCategory.Controls.Add(Me.Label46)
        Me.pnlAddCategory.Controls.Add(Me.chkAssociateStd)
        Me.pnlAddCategory.Controls.Add(Me.pnlStandardEM)
        Me.pnlAddCategory.Controls.Add(Me.btnRefresh)
        Me.pnlAddCategory.Controls.Add(Me.Panel5)
        Me.pnlAddCategory.Controls.Add(Me.lblcaption)
        Me.pnlAddCategory.Controls.Add(Me.txtCaption)
        Me.pnlAddCategory.Controls.Add(Me.btnDelete)
        Me.pnlAddCategory.Controls.Add(Me.btnCatModify)
        Me.pnlAddCategory.Controls.Add(Me.lblCategory)
        Me.pnlAddCategory.Controls.Add(Me.trvstd)
        Me.pnlAddCategory.Controls.Add(Me.txtcategory)
        Me.pnlAddCategory.Controls.Add(Me.btncatAdd)
        Me.pnlAddCategory.Controls.Add(Me.txtCatItem)
        Me.pnlAddCategory.Controls.Add(Me.lblitem)
        Me.pnlAddCategory.Controls.Add(Me.Label27)
        Me.pnlAddCategory.Controls.Add(Me.Label28)
        Me.pnlAddCategory.Controls.Add(Me.Label29)
        Me.pnlAddCategory.Controls.Add(Me.Label30)
        Me.pnlAddCategory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAddCategory.Location = New System.Drawing.Point(0, 0)
        Me.pnlAddCategory.Name = "pnlAddCategory"
        Me.pnlAddCategory.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlAddCategory.Size = New System.Drawing.Size(930, 166)
        Me.pnlAddCategory.TabIndex = 10
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.Color.Red
        Me.Label47.Location = New System.Drawing.Point(76, 68)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(14, 14)
        Me.Label47.TabIndex = 70
        Me.Label47.Text = "*"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Red
        Me.Label46.Location = New System.Drawing.Point(54, 39)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(14, 14)
        Me.Label46.TabIndex = 69
        Me.Label46.Text = "*"
        '
        'chkAssociateStd
        '
        Me.chkAssociateStd.AutoSize = True
        Me.chkAssociateStd.Location = New System.Drawing.Point(391, 7)
        Me.chkAssociateStd.Name = "chkAssociateStd"
        Me.chkAssociateStd.Size = New System.Drawing.Size(242, 18)
        Me.chkAssociateStd.TabIndex = 12
        Me.chkAssociateStd.Text = "Associate standard Physical Examination"
        Me.chkAssociateStd.UseVisualStyleBackColor = True
        Me.chkAssociateStd.Visible = False
        '
        'pnlStandardEM
        '
        Me.pnlStandardEM.Controls.Add(Me.grbEM)
        Me.pnlStandardEM.Location = New System.Drawing.Point(386, 25)
        Me.pnlStandardEM.Name = "pnlStandardEM"
        Me.pnlStandardEM.Size = New System.Drawing.Size(455, 96)
        Me.pnlStandardEM.TabIndex = 59
        Me.pnlStandardEM.Visible = False
        '
        'grbEM
        '
        Me.grbEM.Controls.Add(Me.btnaddcategory)
        Me.grbEM.Controls.Add(Me.btnaddstandreddata)
        Me.grbEM.Controls.Add(Me.cmbAssociateSubItem)
        Me.grbEM.Controls.Add(Me.cmbAssoicatedItem)
        Me.grbEM.Controls.Add(Me.cmbAssociatedCategory)
        Me.grbEM.Controls.Add(Me.lblAssociateSubItem)
        Me.grbEM.Controls.Add(Me.lblAssociatedCategory)
        Me.grbEM.Controls.Add(Me.lblAssoicatedItem)
        Me.grbEM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grbEM.Location = New System.Drawing.Point(0, 0)
        Me.grbEM.Name = "grbEM"
        Me.grbEM.Size = New System.Drawing.Size(455, 96)
        Me.grbEM.TabIndex = 16
        Me.grbEM.TabStop = False
        '
        'btnaddcategory
        '
        Me.btnaddcategory.BackgroundImage = CType(resources.GetObject("btnaddcategory.BackgroundImage"), System.Drawing.Image)
        Me.btnaddcategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnaddcategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddcategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddcategory.Image = CType(resources.GetObject("btnaddcategory.Image"), System.Drawing.Image)
        Me.btnaddcategory.Location = New System.Drawing.Point(391, 68)
        Me.btnaddcategory.Name = "btnaddcategory"
        Me.btnaddcategory.Size = New System.Drawing.Size(21, 21)
        Me.btnaddcategory.TabIndex = 3
        Me.btnaddcategory.Text = "  &Add to category "
        Me.btnaddcategory.UseVisualStyleBackColor = True
        '
        'btnaddstandreddata
        '
        Me.btnaddstandreddata.BackgroundImage = CType(resources.GetObject("btnaddstandreddata.BackgroundImage"), System.Drawing.Image)
        Me.btnaddstandreddata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnaddstandreddata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddstandreddata.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddstandreddata.Image = CType(resources.GetObject("btnaddstandreddata.Image"), System.Drawing.Image)
        Me.btnaddstandreddata.Location = New System.Drawing.Point(415, 68)
        Me.btnaddstandreddata.Name = "btnaddstandreddata"
        Me.btnaddstandreddata.Size = New System.Drawing.Size(21, 21)
        Me.btnaddstandreddata.TabIndex = 4
        Me.btnaddstandreddata.Text = "  &Insert Standard data"
        Me.btnaddstandreddata.UseVisualStyleBackColor = True
        '
        'cmbAssociateSubItem
        '
        Me.cmbAssociateSubItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssociateSubItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAssociateSubItem.ForeColor = System.Drawing.Color.Black
        Me.cmbAssociateSubItem.FormattingEnabled = True
        Me.cmbAssociateSubItem.Location = New System.Drawing.Point(144, 67)
        Me.cmbAssociateSubItem.Name = "cmbAssociateSubItem"
        Me.cmbAssociateSubItem.Size = New System.Drawing.Size(244, 22)
        Me.cmbAssociateSubItem.TabIndex = 2
        '
        'cmbAssoicatedItem
        '
        Me.cmbAssoicatedItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssoicatedItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAssoicatedItem.ForeColor = System.Drawing.Color.Black
        Me.cmbAssoicatedItem.FormattingEnabled = True
        Me.cmbAssoicatedItem.Location = New System.Drawing.Point(144, 40)
        Me.cmbAssoicatedItem.Name = "cmbAssoicatedItem"
        Me.cmbAssoicatedItem.Size = New System.Drawing.Size(244, 22)
        Me.cmbAssoicatedItem.TabIndex = 1
        '
        'cmbAssociatedCategory
        '
        Me.cmbAssociatedCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAssociatedCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAssociatedCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbAssociatedCategory.FormattingEnabled = True
        Me.cmbAssociatedCategory.Location = New System.Drawing.Point(144, 14)
        Me.cmbAssociatedCategory.Name = "cmbAssociatedCategory"
        Me.cmbAssociatedCategory.Size = New System.Drawing.Size(244, 22)
        Me.cmbAssociatedCategory.TabIndex = 0
        '
        'lblAssociateSubItem
        '
        Me.lblAssociateSubItem.AutoSize = True
        Me.lblAssociateSubItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAssociateSubItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssociateSubItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAssociateSubItem.Location = New System.Drawing.Point(15, 71)
        Me.lblAssociateSubItem.Name = "lblAssociateSubItem"
        Me.lblAssociateSubItem.Size = New System.Drawing.Size(126, 14)
        Me.lblAssociateSubItem.TabIndex = 11
        Me.lblAssociateSubItem.Text = "Associated sub Item :"
        '
        'lblAssociatedCategory
        '
        Me.lblAssociatedCategory.AutoSize = True
        Me.lblAssociatedCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAssociatedCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssociatedCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAssociatedCategory.Location = New System.Drawing.Point(15, 18)
        Me.lblAssociatedCategory.Name = "lblAssociatedCategory"
        Me.lblAssociatedCategory.Size = New System.Drawing.Size(126, 14)
        Me.lblAssociatedCategory.TabIndex = 9
        Me.lblAssociatedCategory.Text = "Associated Category :"
        '
        'lblAssoicatedItem
        '
        Me.lblAssoicatedItem.AutoSize = True
        Me.lblAssoicatedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAssoicatedItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssoicatedItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAssoicatedItem.Location = New System.Drawing.Point(38, 44)
        Me.lblAssoicatedItem.Name = "lblAssoicatedItem"
        Me.lblAssoicatedItem.Size = New System.Drawing.Size(103, 14)
        Me.lblAssoicatedItem.TabIndex = 11
        Me.lblAssoicatedItem.Text = "Associated Item :"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(273, 125)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(69, 26)
        Me.btnRefresh.TabIndex = 19
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label48)
        Me.Panel5.Controls.Add(Me.lblControl)
        Me.Panel5.Controls.Add(Me.CmbControl)
        Me.Panel5.Location = New System.Drawing.Point(61, 92)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(299, 22)
        Me.Panel5.TabIndex = 14
        Me.Panel5.Visible = False
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.Red
        Me.Label48.Location = New System.Drawing.Point(1, 5)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(14, 14)
        Me.Label48.TabIndex = 68
        Me.Label48.Text = "*"
        '
        'lblControl
        '
        Me.lblControl.AutoSize = True
        Me.lblControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblControl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControl.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblControl.Location = New System.Drawing.Point(14, 4)
        Me.lblControl.Name = "lblControl"
        Me.lblControl.Size = New System.Drawing.Size(54, 14)
        Me.lblControl.TabIndex = 52
        Me.lblControl.Text = "Control :"
        '
        'CmbControl
        '
        Me.CmbControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbControl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbControl.ForeColor = System.Drawing.Color.Black
        Me.CmbControl.FormattingEnabled = True
        Me.CmbControl.Location = New System.Drawing.Point(70, 0)
        Me.CmbControl.Name = "CmbControl"
        Me.CmbControl.Size = New System.Drawing.Size(228, 22)
        Me.CmbControl.TabIndex = 0
        '
        'lblcaption
        '
        Me.lblcaption.AutoSize = True
        Me.lblcaption.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblcaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblcaption.Location = New System.Drawing.Point(73, 12)
        Me.lblcaption.Name = "lblcaption"
        Me.lblcaption.Size = New System.Drawing.Size(56, 14)
        Me.lblcaption.TabIndex = 12
        Me.lblcaption.Text = "Caption :"
        '
        'txtCaption
        '
        Me.txtCaption.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCaption.Location = New System.Drawing.Point(132, 8)
        Me.txtCaption.Name = "txtCaption"
        Me.txtCaption.Size = New System.Drawing.Size(227, 22)
        Me.txtCaption.TabIndex = 10
        '
        'btnDelete
        '
        Me.btnDelete.BackgroundImage = CType(resources.GetObject("btnDelete.BackgroundImage"), System.Drawing.Image)
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(199, 125)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(69, 26)
        Me.btnDelete.TabIndex = 18
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnCatModify
        '
        Me.btnCatModify.BackgroundImage = CType(resources.GetObject("btnCatModify.BackgroundImage"), System.Drawing.Image)
        Me.btnCatModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCatModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatModify.Location = New System.Drawing.Point(348, 125)
        Me.btnCatModify.Name = "btnCatModify"
        Me.btnCatModify.Size = New System.Drawing.Size(69, 26)
        Me.btnCatModify.TabIndex = 21
        Me.btnCatModify.Text = "&Modify"
        Me.btnCatModify.UseVisualStyleBackColor = True
        Me.btnCatModify.Visible = False
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCategory.Location = New System.Drawing.Point(65, 40)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(64, 14)
        Me.lblCategory.TabIndex = 5
        Me.lblCategory.Text = "Category :"
        '
        'trvstd
        '
        Me.trvstd.CheckBoxes = True
        Me.trvstd.Indent = 21
        Me.trvstd.Location = New System.Drawing.Point(882, 6)
        Me.trvstd.Name = "trvstd"
        Me.trvstd.ShowNodeToolTips = True
        Me.trvstd.ShowRootLines = False
        Me.trvstd.Size = New System.Drawing.Size(21, 27)
        Me.trvstd.TabIndex = 0
        Me.trvstd.Visible = False
        '
        'txtcategory
        '
        Me.txtcategory.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcategory.Location = New System.Drawing.Point(132, 36)
        Me.txtcategory.Name = "txtcategory"
        Me.txtcategory.Size = New System.Drawing.Size(227, 22)
        Me.txtcategory.TabIndex = 11
        '
        'btncatAdd
        '
        Me.btncatAdd.BackgroundImage = CType(resources.GetObject("btncatAdd.BackgroundImage"), System.Drawing.Image)
        Me.btncatAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncatAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncatAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncatAdd.Location = New System.Drawing.Point(130, 125)
        Me.btncatAdd.Name = "btncatAdd"
        Me.btncatAdd.Size = New System.Drawing.Size(64, 26)
        Me.btncatAdd.TabIndex = 17
        Me.btncatAdd.Text = "&Add"
        Me.btncatAdd.UseVisualStyleBackColor = True
        '
        'txtCatItem
        '
        Me.txtCatItem.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatItem.Location = New System.Drawing.Point(132, 64)
        Me.txtCatItem.Name = "txtCatItem"
        Me.txtCatItem.Size = New System.Drawing.Size(227, 22)
        Me.txtCatItem.TabIndex = 13
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblitem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblitem.Location = New System.Drawing.Point(88, 68)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(41, 14)
        Me.lblitem.TabIndex = 7
        Me.lblitem.Text = "Item :"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(1, 162)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(928, 1)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(1, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(928, 1)
        Me.Label28.TabIndex = 51
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(929, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 163)
        Me.Label29.TabIndex = 50
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 163)
        Me.Label30.TabIndex = 49
        Me.Label30.Text = "label4"
        '
        'pnlFieldValues
        '
        Me.pnlFieldValues.Controls.Add(Me.Panel3)
        Me.pnlFieldValues.Controls.Add(Me.Panel8)
        Me.pnlFieldValues.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFieldValues.Location = New System.Drawing.Point(0, 225)
        Me.pnlFieldValues.Name = "pnlFieldValues"
        Me.pnlFieldValues.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlFieldValues.Size = New System.Drawing.Size(930, 180)
        Me.pnlFieldValues.TabIndex = 16
        Me.pnlFieldValues.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgItemList)
        Me.Panel3.Controls.Add(Me.Label41)
        Me.Panel3.Controls.Add(Me.Label42)
        Me.Panel3.Controls.Add(Me.Label43)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 25)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(930, 152)
        Me.Panel3.TabIndex = 53
        '
        'dgItemList
        '
        Me.dgItemList.AllowUserToAddRows = False
        Me.dgItemList.AllowUserToDeleteRows = False
        Me.dgItemList.AllowUserToResizeColumns = False
        Me.dgItemList.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.dgItemList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgItemList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgItemList.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgItemList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_HiddenID, Me.sItem, Me.Col_CotrolType, Me.Col_HiddenAssociatedItem})
        Me.dgItemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgItemList.EnableHeadersVisualStyles = False
        Me.dgItemList.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgItemList.Location = New System.Drawing.Point(1, 4)
        Me.dgItemList.MultiSelect = False
        Me.dgItemList.Name = "dgItemList"
        Me.dgItemList.ReadOnly = True
        Me.dgItemList.RowHeadersVisible = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.dgItemList.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgItemList.Size = New System.Drawing.Size(928, 147)
        Me.dgItemList.TabIndex = 0
        '
        'Col_HiddenID
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.Col_HiddenID.DefaultCellStyle = DataGridViewCellStyle9
        Me.Col_HiddenID.HeaderText = "HiddenID"
        Me.Col_HiddenID.Name = "Col_HiddenID"
        Me.Col_HiddenID.ReadOnly = True
        Me.Col_HiddenID.Visible = False
        '
        'sItem
        '
        Me.sItem.HeaderText = "Item"
        Me.sItem.Name = "sItem"
        Me.sItem.ReadOnly = True
        Me.sItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_CotrolType
        '
        Me.Col_CotrolType.HeaderText = "Control Type"
        Me.Col_CotrolType.Name = "Col_CotrolType"
        Me.Col_CotrolType.ReadOnly = True
        Me.Col_CotrolType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_HiddenAssociatedItem
        '
        Me.Col_HiddenAssociatedItem.HeaderText = "Associated Item"
        Me.Col_HiddenAssociatedItem.Name = "Col_HiddenAssociatedItem"
        Me.Col_HiddenAssociatedItem.ReadOnly = True
        Me.Col_HiddenAssociatedItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_HiddenAssociatedItem.Visible = False
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 4)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 147)
        Me.Label41.TabIndex = 51
        Me.Label41.Text = "label4"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(929, 4)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 147)
        Me.Label42.TabIndex = 52
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 151)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(930, 1)
        Me.Label43.TabIndex = 53
        Me.Label43.Text = "label4"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 3)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(930, 1)
        Me.Label44.TabIndex = 54
        Me.Label44.Text = "label4"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.btnItemUp)
        Me.Panel8.Controls.Add(Me.Label2)
        Me.Panel8.Controls.Add(Me.btnDown)
        Me.Panel8.Controls.Add(Me.btnItemDown)
        Me.Panel8.Controls.Add(Me.btnUp)
        Me.Panel8.Controls.Add(Me.Label37)
        Me.Panel8.Controls.Add(Me.Label38)
        Me.Panel8.Controls.Add(Me.Label39)
        Me.Panel8.Controls.Add(Me.Label45)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(930, 25)
        Me.Panel8.TabIndex = 0
        '
        'btnItemUp
        '
        Me.btnItemUp.BackColor = System.Drawing.Color.Transparent
        Me.btnItemUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnItemUp.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnItemUp.FlatAppearance.BorderSize = 0
        Me.btnItemUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnItemUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnItemUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnItemUp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnItemUp.Image = CType(resources.GetObject("btnItemUp.Image"), System.Drawing.Image)
        Me.btnItemUp.Location = New System.Drawing.Point(885, 1)
        Me.btnItemUp.Name = "btnItemUp"
        Me.btnItemUp.Size = New System.Drawing.Size(22, 23)
        Me.btnItemUp.TabIndex = 10
        Me.btnItemUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnItemUp.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Fields :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = CType(resources.GetObject("btnDown.BackgroundImage"), System.Drawing.Image)
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(803, 0)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(24, 24)
        Me.btnDown.TabIndex = 4
        Me.btnDown.UseVisualStyleBackColor = True
        Me.btnDown.Visible = False
        '
        'btnItemDown
        '
        Me.btnItemDown.BackColor = System.Drawing.Color.Transparent
        Me.btnItemDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnItemDown.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnItemDown.FlatAppearance.BorderSize = 0
        Me.btnItemDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnItemDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnItemDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnItemDown.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnItemDown.Image = CType(resources.GetObject("btnItemDown.Image"), System.Drawing.Image)
        Me.btnItemDown.Location = New System.Drawing.Point(907, 1)
        Me.btnItemDown.Name = "btnItemDown"
        Me.btnItemDown.Size = New System.Drawing.Size(22, 23)
        Me.btnItemDown.TabIndex = 9
        Me.btnItemDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnItemDown.UseVisualStyleBackColor = False
        '
        'btnUp
        '
        Me.btnUp.AutoSize = True
        Me.btnUp.BackgroundImage = CType(resources.GetObject("btnUp.BackgroundImage"), System.Drawing.Image)
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.btnUp.Location = New System.Drawing.Point(833, 0)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(24, 24)
        Me.btnUp.TabIndex = 3
        Me.btnUp.UseVisualStyleBackColor = True
        Me.btnUp.Visible = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(1, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(928, 1)
        Me.Label37.TabIndex = 8
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 24)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(929, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 24)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "label3"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(930, 1)
        Me.Label45.TabIndex = 5
        Me.Label45.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.chckRequired)
        Me.Panel2.Controls.Add(Me.pnlassociateStdItem)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lblFieldcategory)
        Me.Panel2.Controls.Add(Me.cmbFieldCategory)
        Me.Panel2.Controls.Add(Me.pnlBtns)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.txtField)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmbDataType)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtItem)
        Me.Panel2.Controls.Add(Me.Label34)
        Me.Panel2.Controls.Add(Me.Label35)
        Me.Panel2.Controls.Add(Me.Label36)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 56)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(930, 169)
        Me.Panel2.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.chkAssociatestddata)
        Me.Panel6.Controls.Add(Me.pnlHPIExtended)
        Me.Panel6.Location = New System.Drawing.Point(365, 71)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(437, 26)
        Me.Panel6.TabIndex = 70
        '
        'chkAssociatestddata
        '
        Me.chkAssociatestddata.AutoSize = True
        Me.chkAssociatestddata.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkAssociatestddata.Location = New System.Drawing.Point(178, 0)
        Me.chkAssociatestddata.Name = "chkAssociatestddata"
        Me.chkAssociatestddata.Size = New System.Drawing.Size(235, 26)
        Me.chkAssociatestddata.TabIndex = 5
        Me.chkAssociatestddata.Text = "Associate standard Managment option"
        Me.chkAssociatestddata.UseVisualStyleBackColor = True
        Me.chkAssociatestddata.Visible = False
        '
        'pnlHPIExtended
        '
        Me.pnlHPIExtended.Controls.Add(Me.RdbtnExtended)
        Me.pnlHPIExtended.Controls.Add(Me.RdbtnBrief)
        Me.pnlHPIExtended.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHPIExtended.Location = New System.Drawing.Point(0, 0)
        Me.pnlHPIExtended.Name = "pnlHPIExtended"
        Me.pnlHPIExtended.Size = New System.Drawing.Size(178, 26)
        Me.pnlHPIExtended.TabIndex = 6
        '
        'RdbtnExtended
        '
        Me.RdbtnExtended.AutoSize = True
        Me.RdbtnExtended.Location = New System.Drawing.Point(80, 4)
        Me.RdbtnExtended.Name = "RdbtnExtended"
        Me.RdbtnExtended.Size = New System.Drawing.Size(78, 18)
        Me.RdbtnExtended.TabIndex = 68
        Me.RdbtnExtended.TabStop = True
        Me.RdbtnExtended.Text = "Extended"
        Me.RdbtnExtended.UseVisualStyleBackColor = True
        '
        'RdbtnBrief
        '
        Me.RdbtnBrief.AutoSize = True
        Me.RdbtnBrief.Location = New System.Drawing.Point(7, 4)
        Me.RdbtnBrief.Name = "RdbtnBrief"
        Me.RdbtnBrief.Size = New System.Drawing.Size(49, 18)
        Me.RdbtnBrief.TabIndex = 68
        Me.RdbtnBrief.TabStop = True
        Me.RdbtnBrief.Text = "Brief"
        Me.RdbtnBrief.UseVisualStyleBackColor = True
        '
        'chckRequired
        '
        Me.chckRequired.AutoSize = True
        Me.chckRequired.Location = New System.Drawing.Point(376, 14)
        Me.chckRequired.Name = "chckRequired"
        Me.chckRequired.Size = New System.Drawing.Size(96, 18)
        Me.chckRequired.TabIndex = 2
        Me.chckRequired.Text = "Is Mandatory"
        Me.chckRequired.UseVisualStyleBackColor = True
        '
        'pnlassociateStdItem
        '
        Me.pnlassociateStdItem.Controls.Add(Me.btnaddassociated)
        Me.pnlassociateStdItem.Controls.Add(Me.btnaddfieldvalue)
        Me.pnlassociateStdItem.Controls.Add(Me.cmbstddata)
        Me.pnlassociateStdItem.Controls.Add(Me.lblstdData)
        Me.pnlassociateStdItem.Location = New System.Drawing.Point(367, 90)
        Me.pnlassociateStdItem.Name = "pnlassociateStdItem"
        Me.pnlassociateStdItem.Size = New System.Drawing.Size(531, 42)
        Me.pnlassociateStdItem.TabIndex = 62
        Me.pnlassociateStdItem.Visible = False
        '
        'btnaddassociated
        '
        Me.btnaddassociated.BackgroundImage = CType(resources.GetObject("btnaddassociated.BackgroundImage"), System.Drawing.Image)
        Me.btnaddassociated.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnaddassociated.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddassociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddassociated.Image = CType(resources.GetObject("btnaddassociated.Image"), System.Drawing.Image)
        Me.btnaddassociated.Location = New System.Drawing.Point(497, 10)
        Me.btnaddassociated.Name = "btnaddassociated"
        Me.btnaddassociated.Size = New System.Drawing.Size(22, 22)
        Me.btnaddassociated.TabIndex = 9
        Me.btnaddassociated.Text = "  &Insert Associated Items"
        Me.btnaddassociated.UseVisualStyleBackColor = True
        '
        'btnaddfieldvalue
        '
        Me.btnaddfieldvalue.BackgroundImage = CType(resources.GetObject("btnaddfieldvalue.BackgroundImage"), System.Drawing.Image)
        Me.btnaddfieldvalue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnaddfieldvalue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddfieldvalue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddfieldvalue.Image = CType(resources.GetObject("btnaddfieldvalue.Image"), System.Drawing.Image)
        Me.btnaddfieldvalue.Location = New System.Drawing.Point(470, 10)
        Me.btnaddfieldvalue.Name = "btnaddfieldvalue"
        Me.btnaddfieldvalue.Size = New System.Drawing.Size(22, 22)
        Me.btnaddfieldvalue.TabIndex = 8
        Me.btnaddfieldvalue.Text = "  &Add to field"
        Me.btnaddfieldvalue.UseVisualStyleBackColor = True
        '
        'cmbstddata
        '
        Me.cmbstddata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstddata.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstddata.ForeColor = System.Drawing.Color.Black
        Me.cmbstddata.FormattingEnabled = True
        Me.cmbstddata.Location = New System.Drawing.Point(111, 10)
        Me.cmbstddata.Name = "cmbstddata"
        Me.cmbstddata.Size = New System.Drawing.Size(353, 22)
        Me.cmbstddata.TabIndex = 7
        '
        'lblstdData
        '
        Me.lblstdData.AutoSize = True
        Me.lblstdData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblstdData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstdData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblstdData.Location = New System.Drawing.Point(7, 14)
        Me.lblstdData.Name = "lblstdData"
        Me.lblstdData.Size = New System.Drawing.Size(103, 14)
        Me.lblstdData.TabIndex = 9
        Me.lblstdData.Text = "Associated Item :"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(1, 165)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(928, 1)
        Me.Label22.TabIndex = 48
        Me.Label22.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(1, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(928, 1)
        Me.Label21.TabIndex = 47
        Me.Label21.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(929, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 166)
        Me.Label20.TabIndex = 46
        Me.Label20.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 166)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "label4"
        '
        'lblFieldcategory
        '
        Me.lblFieldcategory.AutoSize = True
        Me.lblFieldcategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblFieldcategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFieldcategory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFieldcategory.Location = New System.Drawing.Point(36, 78)
        Me.lblFieldcategory.Name = "lblFieldcategory"
        Me.lblFieldcategory.Size = New System.Drawing.Size(92, 14)
        Me.lblFieldcategory.TabIndex = 9
        Me.lblFieldcategory.Text = "Field Category :"
        '
        'cmbFieldCategory
        '
        Me.cmbFieldCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFieldCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFieldCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbFieldCategory.FormattingEnabled = True
        Me.cmbFieldCategory.Location = New System.Drawing.Point(130, 74)
        Me.cmbFieldCategory.Name = "cmbFieldCategory"
        Me.cmbFieldCategory.Size = New System.Drawing.Size(229, 22)
        Me.cmbFieldCategory.TabIndex = 4
        '
        'pnlBtns
        '
        Me.pnlBtns.Controls.Add(Me.btn_Refresh)
        Me.pnlBtns.Controls.Add(Me.btnAdd)
        Me.pnlBtns.Controls.Add(Me.btnRemove)
        Me.pnlBtns.Controls.Add(Me.btnModify)
        Me.pnlBtns.Location = New System.Drawing.Point(366, 35)
        Me.pnlBtns.Name = "pnlBtns"
        Me.pnlBtns.Size = New System.Drawing.Size(169, 33)
        Me.pnlBtns.TabIndex = 7
        '
        'btn_Refresh
        '
        Me.btn_Refresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Refresh.BackgroundImage = CType(resources.GetObject("btn_Refresh.BackgroundImage"), System.Drawing.Image)
        Me.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Refresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_Refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Refresh.Image = CType(resources.GetObject("btn_Refresh.Image"), System.Drawing.Image)
        Me.btn_Refresh.Location = New System.Drawing.Point(61, 5)
        Me.btn_Refresh.Name = "btn_Refresh"
        Me.btn_Refresh.Size = New System.Drawing.Size(21, 21)
        Me.btn_Refresh.TabIndex = 7
        Me.btn_Refresh.UseVisualStyleBackColor = True
        Me.btn_Refresh.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.Location = New System.Drawing.Point(11, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(21, 21)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.Visible = False
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(36, 5)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(21, 21)
        Me.btnRemove.TabIndex = 5
        Me.btnRemove.UseVisualStyleBackColor = True
        Me.btnRemove.Visible = False
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackgroundImage = CType(resources.GetObject("btnModify.BackgroundImage"), System.Drawing.Image)
        Me.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Image = CType(resources.GetObject("btnModify.Image"), System.Drawing.Image)
        Me.btnModify.Location = New System.Drawing.Point(138, 5)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(21, 21)
        Me.btnModify.TabIndex = 6
        Me.btnModify.UseVisualStyleBackColor = True
        Me.btnModify.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(25, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Field Description :"
        '
        'txtField
        '
        Me.txtField.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtField.ForeColor = System.Drawing.Color.Black
        Me.txtField.Location = New System.Drawing.Point(130, 12)
        Me.txtField.Name = "txtField"
        Me.txtField.Size = New System.Drawing.Size(229, 22)
        Me.txtField.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(57, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Field Type :"
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDataType.ForeColor = System.Drawing.Color.Black
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(130, 43)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(229, 22)
        Me.cmbDataType.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(55, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 14)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Field Value :"
        '
        'txtItem
        '
        Me.txtItem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.Color.Black
        Me.txtItem.Location = New System.Drawing.Point(130, 105)
        Me.txtItem.MaxLength = 100
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(229, 22)
        Me.txtItem.TabIndex = 6
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Red
        Me.Label34.Location = New System.Drawing.Point(45, 47)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(14, 14)
        Me.Label34.TabIndex = 65
        Me.Label34.Text = "*"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Red
        Me.Label35.Location = New System.Drawing.Point(24, 78)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(14, 14)
        Me.Label35.TabIndex = 66
        Me.Label35.Text = "*"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Red
        Me.Label36.Location = New System.Drawing.Point(44, 109)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(14, 14)
        Me.Label36.TabIndex = 67
        Me.Label36.Text = "*"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(13, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "*"
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.ToolStrip1)
        Me.pnlFooter.Controls.Add(Me.Label19)
        Me.pnlFooter.Controls.Add(Me.Label32)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFooter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlFooter.Size = New System.Drawing.Size(930, 56)
        Me.pnlFooter.TabIndex = 14
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripButton6, Me.ToolStripButton4, Me.ToolStripButton5})
        Me.ToolStrip1.Location = New System.Drawing.Point(1, 1)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(926, 53)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 50)
        Me.ToolStripButton1.Tag = "Add"
        Me.ToolStripButton1.Text = "&Add"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton2.Tag = "Remove"
        Me.ToolStripButton2.Text = "&Delete"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(58, 50)
        Me.ToolStripButton3.Tag = "Refresh"
        Me.ToolStripButton3.Text = "&Refresh"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton3.Visible = False
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(66, 50)
        Me.ToolStripButton4.Tag = "Save"
        Me.ToolStripButton4.Text = "&Save&&Cls"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.ToolTipText = "Save and Close"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton5.Tag = "Close"
        Me.ToolStripButton5.Text = "&Close"
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 55)
        Me.Label19.TabIndex = 46
        Me.Label19.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(927, 1)
        Me.Label32.TabIndex = 49
        Me.Label32.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(259, 56)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 711)
        Me.Splitter1.TabIndex = 15
        Me.Splitter1.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnltrvDiscrete)
        Me.pnlLeft.Controls.Add(Me.pnlLiquidDataDictionaryHeader)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 56)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(259, 711)
        Me.pnlLeft.TabIndex = 14
        '
        'pnltrvDiscrete
        '
        Me.pnltrvDiscrete.Controls.Add(Me.trvDiscrete)
        Me.pnltrvDiscrete.Controls.Add(Me.Label33)
        Me.pnltrvDiscrete.Controls.Add(Me.wdTemp)
        Me.pnltrvDiscrete.Controls.Add(Me.Label15)
        Me.pnltrvDiscrete.Controls.Add(Me.Label14)
        Me.pnltrvDiscrete.Controls.Add(Me.Label12)
        Me.pnltrvDiscrete.Controls.Add(Me.Label13)
        Me.pnltrvDiscrete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvDiscrete.Location = New System.Drawing.Point(0, 28)
        Me.pnltrvDiscrete.Name = "pnltrvDiscrete"
        Me.pnltrvDiscrete.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvDiscrete.Size = New System.Drawing.Size(259, 683)
        Me.pnltrvDiscrete.TabIndex = 1
        '
        'trvDiscrete
        '
        Me.trvDiscrete.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDiscrete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvDiscrete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDiscrete.ForeColor = System.Drawing.Color.Black
        Me.trvDiscrete.ImageIndex = 0
        Me.trvDiscrete.ImageList = Me.imgTemplate
        Me.trvDiscrete.ItemHeight = 21
        Me.trvDiscrete.Location = New System.Drawing.Point(4, 5)
        Me.trvDiscrete.Name = "trvDiscrete"
        Me.trvDiscrete.SelectedImageIndex = 0
        Me.trvDiscrete.Size = New System.Drawing.Size(254, 674)
        Me.trvDiscrete.TabIndex = 0
        '
        'imgTemplate
        '
        Me.imgTemplate.ImageStream = CType(resources.GetObject("imgTemplate.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTemplate.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTemplate.Images.SetKeyName(0, "Liduid Data.ico")
        Me.imgTemplate.Images.SetKeyName(1, "SubTemplate.ico")
        Me.imgTemplate.Images.SetKeyName(2, "Bullet06.ico")
        Me.imgTemplate.Images.SetKeyName(3, "datadictionary.ico")
        Me.imgTemplate.Images.SetKeyName(4, "Table_04.ico")
        Me.imgTemplate.Images.SetKeyName(5, "Arrow_02.ico")
        Me.imgTemplate.Images.SetKeyName(6, "Small Arrow.ico")
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(4, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(254, 4)
        Me.Label33.TabIndex = 48
        '
        'wdTemp
        '
        Me.wdTemp.Enabled = True
        Me.wdTemp.Location = New System.Drawing.Point(339, 390)
        Me.wdTemp.Name = "wdTemp"
        Me.wdTemp.OcxState = CType(resources.GetObject("wdTemp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdTemp.Size = New System.Drawing.Size(34, 33)
        Me.wdTemp.TabIndex = 49
        Me.wdTemp.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 679)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(254, 1)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(258, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 679)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(255, 1)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 680)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "label4"
        '
        'pnlLiquidDataDictionaryHeader
        '
        Me.pnlLiquidDataDictionaryHeader.Controls.Add(Me.Panel4)
        Me.pnlLiquidDataDictionaryHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLiquidDataDictionaryHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlLiquidDataDictionaryHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlLiquidDataDictionaryHeader.Name = "pnlLiquidDataDictionaryHeader"
        Me.pnlLiquidDataDictionaryHeader.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLiquidDataDictionaryHeader.Size = New System.Drawing.Size(259, 28)
        Me.pnlLiquidDataDictionaryHeader.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.lblLiquidDataDictionary)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(256, 25)
        Me.Panel4.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(254, 1)
        Me.Label11.TabIndex = 44
        Me.Label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(254, 1)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(255, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 25)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 25)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "label4"
        '
        'lblLiquidDataDictionary
        '
        Me.lblLiquidDataDictionary.BackColor = System.Drawing.Color.Transparent
        Me.lblLiquidDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLiquidDataDictionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblLiquidDataDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLiquidDataDictionary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLiquidDataDictionary.Location = New System.Drawing.Point(0, 0)
        Me.lblLiquidDataDictionary.Name = "lblLiquidDataDictionary"
        Me.lblLiquidDataDictionary.Size = New System.Drawing.Size(256, 25)
        Me.lblLiquidDataDictionary.TabIndex = 2
        Me.lblLiquidDataDictionary.Text = "  Liquid Data "
        Me.lblLiquidDataDictionary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.Controls.Add(Me.tlsLiquidData)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.ForeColor = System.Drawing.Color.Black
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(1192, 56)
        Me.pnl_ToolStrip.TabIndex = 9
        '
        'tlsLiquidData
        '
        Me.tlsLiquidData.BackColor = System.Drawing.Color.Transparent
        Me.tlsLiquidData.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsLiquidData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsLiquidData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsLiquidData.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsLiquidData.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsLiquidData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.tlsLiquidData.Location = New System.Drawing.Point(0, 0)
        Me.tlsLiquidData.Name = "tlsLiquidData"
        Me.tlsLiquidData.Padding = New System.Windows.Forms.Padding(0)
        Me.tlsLiquidData.Size = New System.Drawing.Size(1192, 53)
        Me.tlsLiquidData.TabIndex = 2
        Me.tlsLiquidData.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.Image = CType(resources.GetObject("ts_gloCommunityDownload.Image"), System.Drawing.Image)
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 50)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cmnuDelete
        '
        Me.cmnuDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmnuDelete.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator10})
        Me.cmnuDelete.Name = "cmnu_Appointment"
        Me.cmnuDelete.Size = New System.Drawing.Size(61, 10)
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(57, 6)
        '
        'frmLiquidData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1192, 767)
        Me.Controls.Add(Me.pnlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLiquidData"
        Me.ShowInTaskbar = False
        Me.Text = "Liquid Data"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlEdit.ResumeLayout(False)
        Me.pnlTableEntry.ResumeLayout(False)
        Me.pnldgTableField.ResumeLayout(False)
        CType(Me.dgTableField, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlAddCategory.ResumeLayout(False)
        Me.pnlAddCategory.PerformLayout()
        Me.pnlStandardEM.ResumeLayout(False)
        Me.grbEM.ResumeLayout(False)
        Me.grbEM.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlFieldValues.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlHPIExtended.ResumeLayout(False)
        Me.pnlHPIExtended.PerformLayout()
        Me.pnlassociateStdItem.ResumeLayout(False)
        Me.pnlassociateStdItem.PerformLayout()
        Me.pnlBtns.ResumeLayout(False)
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnltrvDiscrete.ResumeLayout(False)
        CType(Me.wdTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLiquidDataDictionaryHeader.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tlsLiquidData.ResumeLayout(False)
        Me.tlsLiquidData.PerformLayout()
        Me.cmnuDelete.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents trvDiscrete As System.Windows.Forms.TreeView
    Friend WithEvents tlsLiquidData As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtField As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlFieldValues As System.Windows.Forms.Panel
    Friend WithEvents pnlBtns As System.Windows.Forms.Panel
    Friend WithEvents CntData As System.Windows.Forms.ContextMenu
    Friend WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblLiquidDataDictionary As System.Windows.Forms.Label
    Friend WithEvents imgTemplate As System.Windows.Forms.ImageList
    Friend WithEvents pnlTableEntry As System.Windows.Forms.Panel
    Friend WithEvents btncatAdd As System.Windows.Forms.Button
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents txtCatItem As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents txtcategory As System.Windows.Forms.TextBox
    Friend WithEvents pnlAddCategory As System.Windows.Forms.Panel
    Friend WithEvents dgTableField As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnldgTableField As System.Windows.Forms.Panel
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnCatModify As System.Windows.Forms.Button
    Friend WithEvents lblcaption As System.Windows.Forms.Label
    Friend WithEvents txtCaption As System.Windows.Forms.TextBox
    Friend WithEvents pnlEdit As System.Windows.Forms.Panel
    Friend WithEvents lblFieldcategory As System.Windows.Forms.Label
    Friend WithEvents cmbFieldCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlLiquidDataDictionaryHeader As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnltrvDiscrete As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblControl As System.Windows.Forms.Label
    Friend WithEvents CmbControl As System.Windows.Forms.ComboBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgItemList As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents pnlStandardEM As System.Windows.Forms.Panel
    Friend WithEvents grbEM As System.Windows.Forms.GroupBox
    Friend WithEvents trvstd As System.Windows.Forms.TreeView
    Friend WithEvents chkAssociateStd As System.Windows.Forms.CheckBox
    Friend WithEvents cmbAssociateSubItem As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAssoicatedItem As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAssociatedCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblAssociateSubItem As System.Windows.Forms.Label
    Friend WithEvents lblAssociatedCategory As System.Windows.Forms.Label
    Friend WithEvents lblAssoicatedItem As System.Windows.Forms.Label
    Friend WithEvents chkAssociatestddata As System.Windows.Forms.CheckBox
    Friend WithEvents cmbstddata As System.Windows.Forms.ComboBox
    Friend WithEvents lblstdData As System.Windows.Forms.Label
    Friend WithEvents pnlassociateStdItem As System.Windows.Forms.Panel
    Friend WithEvents btnaddstandreddata As System.Windows.Forms.Button
    Friend WithEvents btnaddassociated As System.Windows.Forms.Button
    Friend WithEvents btnaddfieldvalue As System.Windows.Forms.Button
    Friend WithEvents btnaddcategory As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents chckRequired As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents cmnuDelete As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Col_HiddenID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_CotrolType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_HiddenAssociatedItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Item As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_HiddenCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sHiddenControlType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sColumnType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AssociatedCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_AssociatedItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_HiddenAssociatedCategory As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_AssociatedPropertyName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents btnItemUp As System.Windows.Forms.Button
    Friend WithEvents btnItemDown As System.Windows.Forms.Button
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnTableUp As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnTableDown As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pnlHPIExtended As System.Windows.Forms.Panel
    Friend WithEvents RdbtnExtended As System.Windows.Forms.RadioButton
    Friend WithEvents RdbtnBrief As System.Windows.Forms.RadioButton
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
End Class
