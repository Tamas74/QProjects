<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloMedicationC1FlexGrdUserCtrl
    Inherits gloUserControlLibrary.gloC1FlexgridUserCtrl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            components.Dispose()
            If (IsNothing(_my_C1Med_DataTable) = False) Then
                _my_C1Med_DataTable.Dispose()
                _my_C1Med_DataTable = Nothing
            End If
            If (IsNothing(dvNext) = False) Then
                dvNext.Dispose()
                dvNext = Nothing
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloMedicationC1FlexGrdUserCtrl))
        Me.cmbMedStatus = New System.Windows.Forms.ComboBox()
        Me.pnlFlexGrid = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlCombo = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnQuickStatus = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnrechist = New System.Windows.Forms.Button()
        Me.lblMedsReconciliation = New System.Windows.Forms.Label()
        Me.btnScanViewDocument = New System.Windows.Forms.Button()
        Me.BtnMedRec = New System.Windows.Forms.Button()
        Me.brnSignedAgreement = New System.Windows.Forms.Button()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblSearchOn = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Label13 = New System.Windows.Forms.Label()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFlexGrid.SuspendLayout()
        Me.pnlCombo.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        '_Flex
        '
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(1, 33)
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(677, 356)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        '
        'ImageFlex
        '
        Me.ImageFlex.ImageStream = CType(resources.GetObject("ImageFlex.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageFlex.Images.SetKeyName(0, "Delete Drugs.ico")
        Me.ImageFlex.Images.SetKeyName(1, "Refill.ico")
        Me.ImageFlex.Images.SetKeyName(2, "Delete RX.ico")
        Me.ImageFlex.Images.SetKeyName(3, "Bibliography.png")
        Me.ImageFlex.Images.SetKeyName(4, "Patient reference material.ico")
        Me.ImageFlex.Images.SetKeyName(5, "Provider reference material.ico")
        Me.ImageFlex.Images.SetKeyName(6, "infobutton.ico")
        Me.ImageFlex.Images.SetKeyName(7, "Bullet06.ico")
        Me.ImageFlex.Images.SetKeyName(8, "Modify MX.ico")
        '
        'cmbMedStatus
        '
        Me.cmbMedStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMedStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbMedStatus.FormattingEnabled = True
        Me.cmbMedStatus.Location = New System.Drawing.Point(3, 0)
        Me.cmbMedStatus.Name = "cmbMedStatus"
        Me.cmbMedStatus.Size = New System.Drawing.Size(195, 22)
        Me.cmbMedStatus.TabIndex = 0
        '
        'pnlFlexGrid
        '
        Me.pnlFlexGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlFlexGrid.Controls.Add(Me.Label4)
        Me.pnlFlexGrid.Controls.Add(Me.Label3)
        Me.pnlFlexGrid.Controls.Add(Me.Label2)
        Me.pnlFlexGrid.Controls.Add(Me.Label1)
        Me.pnlFlexGrid.Location = New System.Drawing.Point(55, 60)
        Me.pnlFlexGrid.Name = "pnlFlexGrid"
        Me.pnlFlexGrid.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.pnlFlexGrid.Size = New System.Drawing.Size(437, 195)
        Me.pnlFlexGrid.TabIndex = 3
        Me.pnlFlexGrid.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(4, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(429, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(433, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 189)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 189)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 191)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(431, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'pnlCombo
        '
        Me.pnlCombo.BackColor = System.Drawing.Color.Transparent
        Me.pnlCombo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCombo.Controls.Add(Me.Panel2)
        Me.pnlCombo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCombo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCombo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlCombo.Location = New System.Drawing.Point(1, 1)
        Me.pnlCombo.Name = "pnlCombo"
        Me.pnlCombo.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlCombo.Size = New System.Drawing.Size(677, 32)
        Me.pnlCombo.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.btnQuickStatus)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.btnrechist)
        Me.Panel2.Controls.Add(Me.lblMedsReconciliation)
        Me.Panel2.Controls.Add(Me.btnScanViewDocument)
        Me.Panel2.Controls.Add(Me.BtnMedRec)
        Me.Panel2.Controls.Add(Me.brnSignedAgreement)
        Me.Panel2.Controls.Add(Me.pnlSearch)
        Me.Panel2.Controls.Add(Me.lblSearchOn)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(677, 29)
        Me.Panel2.TabIndex = 20
        '
        'btnQuickStatus
        '
        Me.btnQuickStatus.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnQuickStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnQuickStatus.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnQuickStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuickStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickStatus.Image = CType(resources.GetObject("btnQuickStatus.Image"), System.Drawing.Image)
        Me.btnQuickStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuickStatus.Location = New System.Drawing.Point(292, 2)
        Me.btnQuickStatus.Name = "btnQuickStatus"
        Me.btnQuickStatus.Size = New System.Drawing.Size(116, 24)
        Me.btnQuickStatus.TabIndex = 15
        Me.btnQuickStatus.Text = "Quick Status"
        Me.btnQuickStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnQuickStatus.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(408, 2)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(5, 24)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "label2"
        '
        'btnrechist
        '
        Me.btnrechist.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnrechist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnrechist.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrechist.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnrechist.FlatAppearance.BorderSize = 0
        Me.btnrechist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnrechist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnrechist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrechist.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrechist.Image = CType(resources.GetObject("btnrechist.Image"), System.Drawing.Image)
        Me.btnrechist.Location = New System.Drawing.Point(420, 2)
        Me.btnrechist.Name = "btnrechist"
        Me.btnrechist.Size = New System.Drawing.Size(29, 24)
        Me.btnrechist.TabIndex = 49
        Me.C1SuperTooltip1.SetToolTip(Me.btnrechist, "View Reconciliation History")
        Me.btnrechist.UseVisualStyleBackColor = True
        '
        'lblMedsReconciliation
        '
        Me.lblMedsReconciliation.BackColor = System.Drawing.Color.Transparent
        Me.lblMedsReconciliation.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblMedsReconciliation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedsReconciliation.Location = New System.Drawing.Point(305, 2)
        Me.lblMedsReconciliation.Name = "lblMedsReconciliation"
        Me.lblMedsReconciliation.Size = New System.Drawing.Size(115, 24)
        Me.lblMedsReconciliation.TabIndex = 16
        Me.lblMedsReconciliation.Text = "Meds. Reconciled"
        Me.lblMedsReconciliation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnScanViewDocument
        '
        Me.btnScanViewDocument.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.btnScanViewDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnScanViewDocument.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnScanViewDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScanViewDocument.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnScanViewDocument.Image = CType(resources.GetObject("btnScanViewDocument.Image"), System.Drawing.Image)
        Me.btnScanViewDocument.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnScanViewDocument.Location = New System.Drawing.Point(413, 2)
        Me.btnScanViewDocument.Name = "btnScanViewDocument"
        Me.btnScanViewDocument.Size = New System.Drawing.Size(63, 24)
        Me.btnScanViewDocument.TabIndex = 14
        Me.btnScanViewDocument.Text = "Scan"
        Me.btnScanViewDocument.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnScanViewDocument.UseVisualStyleBackColor = True
        '
        'BtnMedRec
        '
        Me.BtnMedRec.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.BtnMedRec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnMedRec.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnMedRec.Dock = System.Windows.Forms.DockStyle.Left
        Me.BtnMedRec.FlatAppearance.BorderSize = 0
        Me.BtnMedRec.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.BtnMedRec.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.BtnMedRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMedRec.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMedRec.Image = CType(resources.GetObject("BtnMedRec.Image"), System.Drawing.Image)
        Me.BtnMedRec.Location = New System.Drawing.Point(276, 2)
        Me.BtnMedRec.Name = "BtnMedRec"
        Me.BtnMedRec.Size = New System.Drawing.Size(29, 24)
        Me.BtnMedRec.TabIndex = 15
        Me.C1SuperTooltip1.SetToolTip(Me.BtnMedRec, "Medication Reconciliation")
        Me.BtnMedRec.UseVisualStyleBackColor = True
        '
        'brnSignedAgreement
        '
        Me.brnSignedAgreement.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.brnSignedAgreement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.brnSignedAgreement.Cursor = System.Windows.Forms.Cursors.Hand
        Me.brnSignedAgreement.Dock = System.Windows.Forms.DockStyle.Left
        Me.brnSignedAgreement.FlatAppearance.BorderSize = 0
        Me.brnSignedAgreement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.brnSignedAgreement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.brnSignedAgreement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.brnSignedAgreement.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brnSignedAgreement.Image = CType(resources.GetObject("brnSignedAgreement.Image"), System.Drawing.Image)
        Me.brnSignedAgreement.Location = New System.Drawing.Point(247, 2)
        Me.brnSignedAgreement.Name = "brnSignedAgreement"
        Me.brnSignedAgreement.Size = New System.Drawing.Size(29, 24)
        Me.brnSignedAgreement.TabIndex = 50
        Me.C1SuperTooltip1.SetToolTip(Me.brnSignedAgreement, "Signature on File")
        Me.brnSignedAgreement.UseVisualStyleBackColor = True
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.btnClear)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.Label12)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(64, 2)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(183, 24)
        Me.pnlSearch.TabIndex = 44
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ShortcutsEnabled = False
        Me.txtSearch.Size = New System.Drawing.Size(156, 15)
        Me.txtSearch.TabIndex = 12
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 18)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(156, 5)
        Me.Label77.TabIndex = 43
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(156, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 22)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(161, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(182, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(183, 1)
        Me.Label11.TabIndex = 44
        Me.Label11.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(183, 1)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "label1"
        '
        'lblSearchOn
        '
        Me.lblSearchOn.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchOn.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearchOn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchOn.Location = New System.Drawing.Point(0, 2)
        Me.lblSearchOn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearchOn.Name = "lblSearchOn"
        Me.lblSearchOn.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearchOn.Size = New System.Drawing.Size(64, 24)
        Me.lblSearchOn.TabIndex = 45
        Me.lblSearchOn.Text = "  Search :"
        Me.lblSearchOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmbMedStatus)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(476, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(201, 24)
        Me.Panel1.TabIndex = 10
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(0, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(677, 2)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(0, 28)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(677, 1)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(677, 2)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "label2"
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(281, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(233, 22)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "   Medication status :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(1, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(675, 1)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(1, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(675, 1)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 24)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label4"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 33)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(677, 1)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "label2"
        '
        'gloMedicationC1FlexGrdUserCtrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.pnlCombo)
        Me.Controls.Add(Me.pnlFlexGrid)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloMedicationC1FlexGrdUserCtrl"
        Me.Size = New System.Drawing.Size(679, 390)
        Me.Controls.SetChildIndex(Me.pnlFlexGrid, 0)
        Me.Controls.SetChildIndex(Me.pnlCombo, 0)
        Me.Controls.SetChildIndex(Me._Flex, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFlexGrid.ResumeLayout(False)
        Me.pnlCombo.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlFlexGrid As System.Windows.Forms.Panel
    Public WithEvents cmbMedStatus As System.Windows.Forms.ComboBox
    Private WithEvents pnlCombo As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnScanViewDocument As System.Windows.Forms.Button
    Friend WithEvents BtnMedRec As System.Windows.Forms.Button
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Public WithEvents lblMedsReconciliation As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Protected WithEvents lblSearchOn As System.Windows.Forms.Label
    Friend WithEvents btnQuickStatus As System.Windows.Forms.Button
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnrechist As System.Windows.Forms.Button
    Friend WithEvents brnSignedAgreement As System.Windows.Forms.Button

End Class
