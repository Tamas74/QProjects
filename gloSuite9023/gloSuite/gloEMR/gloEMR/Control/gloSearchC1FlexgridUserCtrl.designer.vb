<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloSearchC1FlexgridUserCtrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Dim dtpControls() As System.Windows.Forms.DateTimePicker = {DateTimePicker1}
        Dim cntControls() As System.Windows.Forms.Control = {DateTimePicker1}

        If disposing AndAlso components IsNot Nothing Then
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cntListmenuStrip}

            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try


            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If
            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If



            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                End If
            End If

            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                End If
            End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloSearchC1FlexgridUserCtrl))
        Me.ImagSearchFlex = New System.Windows.Forms.ImageList(Me.components)
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.btnUC_Close = New System.Windows.Forms.Button
        Me.btnUC_OK = New System.Windows.Forms.Button
        Me.btnUC_Add = New System.Windows.Forms.Button
        Me.lblSearchOn = New System.Windows.Forms.Label
        Me.lblColName = New System.Windows.Forms.Label
        Me.txtSearchFlexGrid = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbCOlumnName = New System.Windows.Forms.ComboBox
        Me.btnCloseRefill = New System.Windows.Forms.Button
        Me.lblSearchString = New System.Windows.Forms.Label
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlSearch.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImagSearchFlex
        '
        Me.ImagSearchFlex.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImagSearchFlex.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImagSearchFlex.TransparentColor = System.Drawing.Color.Transparent
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Name = "cntListmenuStrip"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.pnlTop)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(632, 34)
        Me.pnlSearch.TabIndex = 3
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.pnlTop.BackgroundImage = CType(resources.GetObject("pnlTop.BackgroundImage"), System.Drawing.Image)
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.btnUC_Close)
        Me.pnlTop.Controls.Add(Me.btnUC_OK)
        Me.pnlTop.Controls.Add(Me.btnUC_Add)
        Me.pnlTop.Controls.Add(Me.lblSearchOn)
        Me.pnlTop.Controls.Add(Me.lblColName)
        Me.pnlTop.Controls.Add(Me.txtSearchFlexGrid)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.cmbCOlumnName)
        Me.pnlTop.Controls.Add(Me.btnCloseRefill)
        Me.pnlTop.Controls.Add(Me.lblSearchString)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.DateTimePicker1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(632, 29)
        Me.pnlTop.TabIndex = 7
        '
        'btnUC_Close
        '
        Me.btnUC_Close.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_Close.FlatAppearance.BorderSize = 0
        Me.btnUC_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_Close.Image = CType(resources.GetObject("btnUC_Close.Image"), System.Drawing.Image)
        Me.btnUC_Close.Location = New System.Drawing.Point(606, 3)
        Me.btnUC_Close.Name = "btnUC_Close"
        Me.btnUC_Close.Size = New System.Drawing.Size(22, 20)
        Me.btnUC_Close.TabIndex = 13
        Me.btnUC_Close.UseVisualStyleBackColor = False
        '
        'btnUC_OK
        '
        Me.btnUC_OK.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_OK.FlatAppearance.BorderSize = 0
        Me.btnUC_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_OK.Image = CType(resources.GetObject("btnUC_OK.Image"), System.Drawing.Image)
        Me.btnUC_OK.Location = New System.Drawing.Point(581, 4)
        Me.btnUC_OK.Name = "btnUC_OK"
        Me.btnUC_OK.Size = New System.Drawing.Size(25, 20)
        Me.btnUC_OK.TabIndex = 12
        Me.btnUC_OK.Text = "."
        Me.btnUC_OK.UseVisualStyleBackColor = False
        '
        'btnUC_Add
        '
        Me.btnUC_Add.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_Add.FlatAppearance.BorderSize = 0
        Me.btnUC_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_Add.Image = CType(resources.GetObject("btnUC_Add.Image"), System.Drawing.Image)
        Me.btnUC_Add.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnUC_Add.Location = New System.Drawing.Point(556, 5)
        Me.btnUC_Add.Name = "btnUC_Add"
        Me.btnUC_Add.Size = New System.Drawing.Size(25, 20)
        Me.btnUC_Add.TabIndex = 11
        Me.btnUC_Add.UseVisualStyleBackColor = False
        '
        'lblSearchOn
        '
        Me.lblSearchOn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSearchOn.AutoSize = True
        Me.lblSearchOn.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchOn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchOn.Location = New System.Drawing.Point(7, 8)
        Me.lblSearchOn.Name = "lblSearchOn"
        Me.lblSearchOn.Size = New System.Drawing.Size(65, 14)
        Me.lblSearchOn.TabIndex = 0
        Me.lblSearchOn.Text = "Search On :"
        '
        'lblColName
        '
        Me.lblColName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblColName.AutoSize = True
        Me.lblColName.BackColor = System.Drawing.Color.Transparent
        Me.lblColName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColName.Location = New System.Drawing.Point(78, 8)
        Me.lblColName.Name = "lblColName"
        Me.lblColName.Size = New System.Drawing.Size(60, 14)
        Me.lblColName.TabIndex = 1
        Me.lblColName.Text = "Drug Name"
        Me.lblColName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearchFlexGrid
        '
        Me.txtSearchFlexGrid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearchFlexGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchFlexGrid.Location = New System.Drawing.Point(142, 4)
        Me.txtSearchFlexGrid.Name = "txtSearchFlexGrid"
        Me.txtSearchFlexGrid.Size = New System.Drawing.Size(136, 20)
        Me.txtSearchFlexGrid.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(433, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'cmbCOlumnName
        '
        Me.cmbCOlumnName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCOlumnName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCOlumnName.FormattingEnabled = True
        Me.cmbCOlumnName.Location = New System.Drawing.Point(478, 3)
        Me.cmbCOlumnName.Name = "cmbCOlumnName"
        Me.cmbCOlumnName.Size = New System.Drawing.Size(26, 22)
        Me.cmbCOlumnName.TabIndex = 7
        Me.cmbCOlumnName.Visible = False
        '
        'btnCloseRefill
        '
        Me.btnCloseRefill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseRefill.FlatAppearance.BorderSize = 0
        Me.btnCloseRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseRefill.Image = CType(resources.GetObject("btnCloseRefill.Image"), System.Drawing.Image)
        Me.btnCloseRefill.Location = New System.Drawing.Point(605, 0)
        Me.btnCloseRefill.Name = "btnCloseRefill"
        Me.btnCloseRefill.Size = New System.Drawing.Size(22, 20)
        Me.btnCloseRefill.TabIndex = 6
        '
        'lblSearchString
        '
        Me.lblSearchString.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSearchString.AutoSize = True
        Me.lblSearchString.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchString.Location = New System.Drawing.Point(3, -16)
        Me.lblSearchString.Name = "lblSearchString"
        Me.lblSearchString.Size = New System.Drawing.Size(73, 14)
        Me.lblSearchString.TabIndex = 2
        Me.lblSearchString.Text = "Search String"
        Me.lblSearchString.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(220, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(58, 18)
        Me.btnRefresh.TabIndex = 10
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnRefresh.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(78, 3)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(136, 20)
        Me.DateTimePicker1.TabIndex = 9
        Me.DateTimePicker1.Visible = False
        '
        '_Flex
        '
        Me._Flex.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.Location = New System.Drawing.Point(0, 34)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 17
        Me._Flex.Size = New System.Drawing.Size(632, 369)
        Me._Flex.TabIndex = 4
        '
        'gloSearchC1FlexgridUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me._Flex)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "gloSearchC1FlexgridUserCtrl"
        Me.Size = New System.Drawing.Size(632, 403)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents ImagSearchFlex As System.Windows.Forms.ImageList
    Protected WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Protected WithEvents pnlSearch As System.Windows.Forms.Panel
    Protected WithEvents txtSearchFlexGrid As System.Windows.Forms.TextBox
    Protected WithEvents lblSearchString As System.Windows.Forms.Label
    Protected WithEvents lblColName As System.Windows.Forms.Label
    Protected WithEvents lblSearchOn As System.Windows.Forms.Label
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Protected WithEvents btnCloseRefill As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents cmbCOlumnName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnUC_Close As System.Windows.Forms.Button
    Friend WithEvents btnUC_OK As System.Windows.Forms.Button
    Friend WithEvents btnUC_Add As System.Windows.Forms.Button

End Class
