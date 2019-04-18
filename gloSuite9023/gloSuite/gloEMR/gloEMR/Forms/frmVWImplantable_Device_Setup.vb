Public Class frmVWImplantable_Device_Setup
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(dgImplantableDevice) = False) Then
                    dgImplantableDevice.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgImplantableDevice)
                    dgImplantableDevice.Dispose()
                    dgImplantableDevice = Nothing
                End If
            Catch ex As Exception

            End Try
            Dim dtpContextMenuStrip As ContextMenuStrip() = {cmnuAddCategory}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)
                gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
            Catch ex As Exception

            End Try


        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents trvIssueAgency As System.Windows.Forms.TreeView
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgImplantableDevice As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cmnuAddCategory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents imgHistory As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWImplantable_Device_Setup))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.trvIssueAgency = New System.Windows.Forms.TreeView()
        Me.imgHistory = New System.Windows.Forms.ImageList(Me.components)
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.dgImplantableDevice = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.cmnuAddCategory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        CType(Me.dgImplantableDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.Panel3)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(842, 23)
        Me.pnlTopRight.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.txtSearch)
        Me.Panel3.Controls.Add(Me.Label77)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(66, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(241, 23)
        Me.Panel3.TabIndex = 44
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 1
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(214, 5)
        Me.Label77.TabIndex = 43
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(5, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(214, 3)
        Me.Label10.TabIndex = 37
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(1, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(4, 21)
        Me.Label11.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 21)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 21)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Location = New System.Drawing.Point(240, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 21)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(241, 1)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(241, 1)
        Me.Label15.TabIndex = 45
        Me.Label15.Text = "label1"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(65, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 47
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(64, 20)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(840, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 22)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(841, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 22)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(842, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 84)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(245, 434)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftTop.Controls.Add(Me.trvIssueAgency)
        Me.pnlLeftTop.Controls.Add(Me.Label16)
        Me.pnlLeftTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlLeftTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(245, 434)
        Me.pnlLeftTop.TabIndex = 1
        '
        'trvIssueAgency
        '
        Me.trvIssueAgency.BackColor = System.Drawing.Color.White
        Me.trvIssueAgency.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvIssueAgency.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvIssueAgency.ForeColor = System.Drawing.Color.Black
        Me.trvIssueAgency.HideSelection = False
        Me.trvIssueAgency.ImageIndex = 1
        Me.trvIssueAgency.ImageList = Me.imgHistory
        Me.trvIssueAgency.ItemHeight = 19
        Me.trvIssueAgency.Location = New System.Drawing.Point(4, 6)
        Me.trvIssueAgency.Name = "trvIssueAgency"
        Me.trvIssueAgency.SelectedImageIndex = 1
        Me.trvIssueAgency.ShowLines = False
        Me.trvIssueAgency.Size = New System.Drawing.Size(240, 424)
        Me.trvIssueAgency.TabIndex = 3
        '
        'imgHistory
        '
        Me.imgHistory.ImageStream = CType(resources.GetObject("imgHistory.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgHistory.TransparentColor = System.Drawing.Color.Transparent
        Me.imgHistory.Images.SetKeyName(0, "History.ico")
        Me.imgHistory.Images.SetKeyName(1, "Bullet06.ico")
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(4, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(240, 5)
        Me.Label16.TabIndex = 44
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 430)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(240, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(244, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(242, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'dgImplantableDevice
        '
        Me.dgImplantableDevice.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgImplantableDevice.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgImplantableDevice.BackgroundColor = System.Drawing.Color.White
        Me.dgImplantableDevice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgImplantableDevice.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgImplantableDevice.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImplantableDevice.CaptionForeColor = System.Drawing.Color.White
        Me.dgImplantableDevice.CaptionVisible = False
        Me.dgImplantableDevice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgImplantableDevice.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImplantableDevice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgImplantableDevice.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgImplantableDevice.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgImplantableDevice.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImplantableDevice.HeaderForeColor = System.Drawing.Color.White
        Me.dgImplantableDevice.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgImplantableDevice.Location = New System.Drawing.Point(0, 1)
        Me.dgImplantableDevice.Name = "dgImplantableDevice"
        Me.dgImplantableDevice.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgImplantableDevice.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgImplantableDevice.ReadOnly = True
        Me.dgImplantableDevice.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgImplantableDevice.SelectionForeColor = System.Drawing.Color.Black
        Me.dgImplantableDevice.Size = New System.Drawing.Size(597, 430)
        Me.dgImplantableDevice.TabIndex = 4
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(848, 55)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(848, 55)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 52)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnAdd.Visible = False
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 52)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnModify.Visible = False
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 52)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDelete.Visible = False
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.Image = CType(resources.GetObject("ts_gloCommunityDownload.Image"), System.Drawing.Image)
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 52)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 52)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.Visible = False
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 52)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.dgImplantableDevice)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(248, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(600, 434)
        Me.Panel1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(595, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 430)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(596, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 430)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(597, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 55)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(848, 29)
        Me.Panel2.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(245, 84)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 434)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'cmnuAddCategory
        '
        Me.cmnuAddCategory.Name = "cmnuAddCategory"
        Me.cmnuAddCategory.Size = New System.Drawing.Size(61, 4)
        '
        'frmVWImplantable_Device_Setup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWImplantable_Device_Setup"
        Me.Text = "Implantable Devices"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        CType(Me.dgImplantableDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private MyNodename As String = ""
    Private ImplantDeviceDBLayer As New ClsImplantDeviceDBLayer    
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

    Private Sub frmVWImplantable_Device_Setup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            FillTreeView()
            dgImplantableDevice.AllowSorting = True
            trvIssueAgency.ExpandAll()
            Dim mynode As myTreeNode
            If trvIssueAgency.Nodes.Item(0).GetNodeCount(False) > 0 Then
                mynode = trvIssueAgency.Nodes.Item(0).Nodes.Item(0)
                MyNodename = mynode.NodeName
                trvIssueAgency.SelectedNode = mynode                
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BindGrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            If MyNodename <> "" Then
                ImplantDeviceDBLayer.FetchData(MyNodename, strsearchtxt)
                dgImplantableDevice.SetDataBinding(ImplantDeviceDBLayer.DsDataview, "")

                ImplantDeviceDBLayer.SortDataview(ImplantDeviceDBLayer.DsDataview.Table.Columns(1).ColumnName)

                If strcolumnName = "" Then
                    ImplantDeviceDBLayer.SortDataview(ImplantDeviceDBLayer.DsDataview.Table.Columns(1).ColumnName)
                Else
                    Dim strColumn As String = Replace(strcolumnName, "[", "")

                    ImplantDeviceDBLayer.SortDataview(strColumn, strSortBy)
                End If
                If ImplantDeviceDBLayer.DsDataview.Count > 0 Then
                    dgImplantableDevice.Select(0)
                End If
            End If

        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub UpdateImplantdeviceSetup()
        If MyNodename <> "" Then
            Try
                Me.Cursor = Cursors.WaitCursor

                If dgImplantableDevice.VisibleRowCount >= 1 Then
                    Dim frm As frmImplantableDeviceMaster
                    Dim DeviceID As String
                    DeviceID = CType(dgImplantableDevice.Item(dgImplantableDevice.CurrentRowIndex, 1), String)
                    Dim DeviceMSTID As Long
                    DeviceMSTID = CType(dgImplantableDevice.Item(dgImplantableDevice.CurrentRowIndex, 0), Long)
                    Dim myDataView As DataView = CType(dgImplantableDevice.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then
                        sortOrder = CType(dgImplantableDevice.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                    End If

                    ''''''''''''''''''
                    frm = New frmImplantableDeviceMaster(DeviceMSTID, DeviceID)
                    frm.Text = "Update Implantable Device Setup"

                    frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                    If frm._DialogResult = Windows.Forms.DialogResult.OK Then
                        BindGrid(strcolumnName, strsortorder, strSearchstring)
                        '''' 
                        Dim i As Integer

                        For i = 0 To trvIssueAgency.Nodes(0).GetNodeCount(False) - 1
                            Dim CategoryNode As myTreeNode
                            CategoryNode = trvIssueAgency.Nodes(0).Nodes(i)
                            If frm._IssuingAgency = CategoryNode.NodeName Then
                                trvIssueAgency.SelectedNode = CategoryNode
                                Exit For
                            End If
                        Next
                        Dim myDatagView As DataView = CType(dgImplantableDevice.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then
                            For i = 0 To CType(dgImplantableDevice.DataSource, DataView).Count - 1
                                If frm._DeviceID = dgImplantableDevice.Item(i, 1) Then
                                    dgImplantableDevice.CurrentRowIndex = i
                                    dgImplantableDevice.UnSelect(0)
                                    dgImplantableDevice.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    frm.Dispose()
                    frm = Nothing
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(ImplantDeviceDBLayer.DsDataview.Table.TableName)

        Dim dgID As New DataGridTextBoxColumn

        With dgID
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .NullText = ""
            .Width = 0
        End With

        Dim dgCol1 As New DataGridTextBoxColumn
        With dgCol1
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(1).ColumnName
            .HeaderText = "Device ID"
            .NullText = ""
            .Width = 0.15 * dgImplantableDevice.Width
        End With

        Dim dgCol2 As New DataGridTextBoxColumn
        With dgCol2
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(2).ColumnName
            .HeaderText = "Issuing Agency"
            .NullText = ""
            .Width = 0 ' 0.2 * dgImplantableDevice.Width
        End With

        Dim dgCol3 As New DataGridTextBoxColumn
        With dgCol3
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(3).ColumnName
            .HeaderText = "Brand Name"
            .NullText = ""
            .Width = 0.25 * dgImplantableDevice.Width
        End With
        
        Dim dgCol4 As New DataGridTextBoxColumn
        With dgCol4
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(4).ColumnName
            .HeaderText = "Company Name"
            .NullText = ""
            .Width = 0.25 * dgImplantableDevice.Width
        End With
        
        Dim dgCol5 As New DataGridTextBoxColumn
        With dgCol5
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(5).ColumnName
            .HeaderText = "Version or Model"
            .NullText = ""
            .Width = 0.25 * dgImplantableDevice.Width
        End With
        Dim dgCol6 As New DataGridTextBoxColumn
        With dgCol6
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(6).ColumnName
            .HeaderText = "MRI Safety Status"
            .NullText = ""
            .Width = 0.25 * dgImplantableDevice.Width
        End With
        Dim dgCol7 As New DataGridTextBoxColumn
        With dgCol7
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(7).ColumnName
            .HeaderText = "Labeled Contains NRL"
            .NullText = ""
            .Width = 0.15 * dgImplantableDevice.Width
        End With
        Dim dgCol8 As New DataGridTextBoxColumn
        With dgCol8
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(8).ColumnName
            .HeaderText = "GMDN Terms"
            .NullText = ""
            .Width = 0
        End With
        Dim dgCol9 As New DataGridTextBoxColumn
        With dgCol9
            .MappingName = ImplantDeviceDBLayer.DsDataview.Table.Columns(9).ColumnName
            .HeaderText = "IsSystemDevice"
            .NullText = ""
            .Width = 0
        End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3, dgCol4, dgCol5, dgCol6, dgCol7, dgCol8, dgCol9})
        dgImplantableDevice.TableStyles.Clear()
        dgImplantableDevice.TableStyles.Add(ts)

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                BindGrid(strsearchtxt:=txtSearch.Text.Trim)
            Catch objErr As Exception                
                MessageBox.Show(objErr.ToString, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Cursor = Cursors.Default
            End Try
        End If
    End Sub

    Private Sub trvIssueAgency_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvIssueAgency.AfterSelect
        Try

            txtSearch.Text = ""
            Dim mynode As myTreeNode = Nothing
            If Not IsNothing(e.Node) Then
                If Not mynode Is trvIssueAgency.Nodes.Item(0) Then
                    mynode = CType(e.Node, myTreeNode)
                    MyNodename = mynode.NodeName
                    If MyNodename <> "" Then
                        BindGrid()
                    Else

                        dgImplantableDevice.DataSource = Nothing
                    End If
                Else
                    dgImplantableDevice.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgImplantableDevice.MouseUp
        If dgImplantableDevice.CurrentRowIndex >= 0 Then
            dgImplantableDevice.Select(dgImplantableDevice.CurrentRowIndex)
        End If
    End Sub

    Private Sub FillTreeView()
        Dim dc As DataTable
        Try
            dc = ImplantDeviceDBLayer.FillControls
            Dim mynode As myTreeNode
            mynode = New myTreeNode("Issuing Agencies", -1)
            mynode.ImageIndex = 0
            mynode.SelectedImageIndex = 0
            trvIssueAgency.Nodes.Add(mynode)

            Dim mychildnode As myTreeNode
            Dim i As Integer
            Dim key As Int64
            Dim strname As String
            For i = 0 To dc.Rows.Count - 1
                key = dc.Rows.Item(i)(0)
                strname = dc.Rows.Item(i)(1)
                mychildnode = New myTreeNode(strname, key)
                mychildnode.ImageIndex = 1
                mychildnode.SelectedImageIndex = 1
                mynode.Nodes.Add(mychildnode)
            Next
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub
    
    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If dgImplantableDevice.CurrentRowIndex >= 0 Then
                    dgImplantableDevice.Select(0)
                    dgImplantableDevice.CurrentRowIndex = 0
                End If
            End If
            mdlGeneral.ValidateText(txtSearch.Text, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgHistory_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgImplantableDevice.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgImplantableDevice.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If

            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                'UpdateImplantdeviceSetup()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddCategory()
        Try
            If MyNodename <> "" Then
                Me.Cursor = Cursors.WaitCursor
                Dim frm As frmImplantableDeviceMaster
                frm = New frmImplantableDeviceMaster(0, "")
                frm.Text = "Add Implant Device Setup"
                frm._IssuingAgency = MyNodename
                frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                If frm._DialogResult = Windows.Forms.DialogResult.OK Then
                    BindGrid(strsearchtxt:=txtSearch.Text.Trim)
                    Dim myDataView As DataView = CType(dgImplantableDevice.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then
                        Dim i As Integer
                        For i = 0 To myDataView.Table.Rows.Count - 1
                            If frm._DeviceID = dgImplantableDevice.Item(i, 1) Then
                                dgImplantableDevice.CurrentRowIndex = i
                                dgImplantableDevice.Select(i)
                                Exit For
                            End If
                        Next
                    End If
                End If
                frm.Dispose()
                frm = Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DeleteCategory()
        Try
            If Convert.ToBoolean(dgImplantableDevice.Item(dgImplantableDevice.CurrentRowIndex, 9)) = False Then

                If MyNodename <> "" Then
                    Me.Cursor = Cursors.WaitCursor
                    If dgImplantableDevice.VisibleRowCount >= 1 Then
                        If MessageBox.Show("Are you sure you want to delete this Record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                            Dim ID As Long
                            ID = CType(dgImplantableDevice.Item(dgImplantableDevice.CurrentRowIndex, 0), Long)
                            Try
                                ImplantDeviceDBLayer.DeleteData(ID)
                                ''''''Code is Added by Anil 0n 20071102
                                Dim myDataView As DataView = CType(dgImplantableDevice.DataSource, DataView)
                                If (IsNothing(myDataView) = False) Then
                                    sortOrder = CType(dgImplantableDevice.DataSource, DataView).Sort
                                    strSearchstring = txtSearch.Text.Trim
                                    arrcolumnsort = Split(sortOrder, "]")
                                    If arrcolumnsort.Length > 1 Then
                                        strcolumnName = arrcolumnsort.GetValue(0)
                                        strsortorder = arrcolumnsort.GetValue(1)
                                    End If

                                    BindGrid(strcolumnName, strsortorder, strSearchstring)
                                End If
                                ''''''''''''''''''
                            Catch ex As SqlClient.SqlException
                                MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End Try

                        End If
                    End If
                End If
            Else
                MessageBox.Show("You cannot Delete System defined Items", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RefreshCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            txtSearch.Clear()
            txtSearch.Focus()
            dgImplantableDevice.DataSource = Nothing
            _blnSearch = True
            If MyNodename <> "" Then
                BindGrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Implantable Device Setup", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddCategory()
            Case "Modify"
                Call UpdateImplantdeviceSetup()
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmVWImplantable_Device_Setup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        HideColumn()
    End Sub
  
End Class
