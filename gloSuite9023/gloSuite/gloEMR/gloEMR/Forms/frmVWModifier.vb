Public Class frmVWModifier
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(grdModifiers) = False) Then
                    grdModifiers.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdModifiers)
                    grdModifiers.Dispose()
                    grdModifiers = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grdModifiers As System.Windows.Forms.DataGrid
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWModifier))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grdModifiers = New System.Windows.Forms.DataGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlTopRight.SuspendLayout()
        CType(Me.grdModifiers, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(618, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(97, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(96, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "  Description :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(616, 1)
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
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(617, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
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
        Me.Label4.Size = New System.Drawing.Size(618, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'grdModifiers
        '
        Me.grdModifiers.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdModifiers.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdModifiers.BackgroundColor = System.Drawing.Color.White
        Me.grdModifiers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdModifiers.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdModifiers.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdModifiers.CaptionForeColor = System.Drawing.Color.White
        Me.grdModifiers.CaptionVisible = False
        Me.grdModifiers.DataMember = ""
        Me.grdModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdModifiers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdModifiers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdModifiers.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdModifiers.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdModifiers.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdModifiers.HeaderForeColor = System.Drawing.Color.White
        Me.grdModifiers.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdModifiers.Location = New System.Drawing.Point(3, 0)
        Me.grdModifiers.Name = "grdModifiers"
        Me.grdModifiers.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdModifiers.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdModifiers.ReadOnly = True
        Me.grdModifiers.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdModifiers.SelectionForeColor = System.Drawing.Color.Black
        Me.grdModifiers.Size = New System.Drawing.Size(618, 431)
        Me.grdModifiers.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(624, 54)
        Me.pnlToolStrip.TabIndex = 11
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(624, 54)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 51)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 51)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 51)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 51)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.BackColor = System.Drawing.Color.Transparent
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 51)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.grdModifiers)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(624, 434)
        Me.Panel1.TabIndex = 12
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 430)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(616, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(620, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(618, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(624, 30)
        Me.Panel2.TabIndex = 13
        '
        'frmVWModifier
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(624, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWModifier"
        Me.ShowInTaskbar = False
        Me.Text = "Modifier"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        CType(Me.grdModifiers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public Shared blnModify As Boolean
    Dim objclsModifier As New clsModifiers
    Dim _blnSearch As Boolean = True
    Dim strcolumnName As String
    Dim strsortorder As String
    Private Sub frmVWModifier_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdModifiers.DataSource = objclsModifier.GetAllModifiers
            If Not IsNothing(objclsModifier.GetDataview) Then
                objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
            End If
            CustomGridStyle()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UpdateModifiers()
        Dim ID As Long

        Dim objfrmMSTModifier As frmMSTModifier
        Try
            If grdModifiers.VisibleRowCount >= 1 Then
                blnModify = True
                ID = grdModifiers.Item(grdModifiers.CurrentRowIndex, 0).ToString
                Dim grdIndex As Integer = grdModifiers.CurrentRowIndex

                ''''''Code is Added by Anil 0n 20071102
                Dim myDataView As DataView = CType(grdModifiers.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim sortOrder As String = CType(grdModifiers.DataSource, DataView).Sort
                    Dim strSearchstring As String = txtSearch.Text.Trim
                    Dim arrcolumnsort() As String = Split(sortOrder, "]")
                    If arrcolumnsort.Length > 1 Then
                        strcolumnName = arrcolumnsort.GetValue(0)
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If

                    ''''''''''''''''''
                    objfrmMSTModifier = New frmMSTModifier(ID)
                    objfrmMSTModifier.Text = "Modify Modifier"
                    objfrmMSTModifier.ShowDialog(IIf(IsNothing(objfrmMSTModifier.Parent), Me, objfrmMSTModifier.Parent))
                    objfrmMSTModifier.BringToFront()

                    If objfrmMSTModifier.CancelClick = False Then
                        grdModifiers.DataSource = objclsModifier.GetAllModifiers

                        objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
                        ''''''Code Line is Added by Anil on 20071102
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        '''' To Remember the Selection of Row 
                        Dim myDatagView As DataView = CType(grdModifiers.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then


                            Dim i As Integer
                            For i = 0 To myDatagView.Count - 1
                                '''' when ID Found select that matching Row
                                If ID = grdModifiers.Item(i, 0) Then
                                    grdModifiers.CurrentRowIndex = i
                                    grdModifiers.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                        'Else
                        '    grdModifiers.Select(grdIndex)
                    End If
                    objfrmMSTModifier.Dispose()
                    objfrmMSTModifier = Nothing
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTModifier = Nothing
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim ts As New clsDataGridTableStyle(objclsModifier.GetDataview.Table.TableName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = objclsModifier.GetDataview.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim CodeCol As New DataGridTextBoxColumn
        With CodeCol
            .Width = 0.5 * grdModifiers.Width
            .MappingName = objclsModifier.GetDataview.Table.Columns(1).ColumnName
            .HeaderText = "Code"
            .NullText = ""
        End With

        Dim DescriptionCol As New DataGridTextBoxColumn
        With DescriptionCol
            .Width = 0.5 * grdModifiers.Width - 10
            .MappingName = objclsModifier.GetDataview.Table.Columns(2).ColumnName
            .HeaderText = "Description"
            .NullText = ""
        End With
        '''''''Code is added by Anil on 20071102
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If strcolumnName = "" Then
            objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")

            objclsModifier.SortDataview(strColumn, strSortBy)
        End If
        ''''''''''''''''''''''''''''''''
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, CodeCol, DescriptionCol})
        grdModifiers.TableStyles.Clear()
        grdModifiers.TableStyles.Add(ts)

    End Sub
    Private Sub grdModifiers_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdModifiers.CurrentCellChanged
        ''''''''Code Commented by Anil on 20071102
        ''Try
        ''    Select Case grdModifiers.CurrentCell.ColumnNumber
        ''        Case 1
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "Code"
        ''        Case 2
        ''            txtSearch.Text = ""
        ''            lblSearch.Text = "Description"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdModifiers.DataSource(), DataView)
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                grdModifiers.DataSource = dvPatient
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                Select Case Trim(lblSearch.Text)
                    Case "Code"

                        '''''Code Modified by Anil on 24/09/2007 at 12:35 p.m.
                        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                    Case "Description"
                        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        ''Else
                        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        ''End If
                        '''''Upto here the changes are made by Anil on 24/09/2007 at 12:35 p.m.
                End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    '    objclsModifier.Search(grdModifiers.DataSource, 1, txtSearch.Text)
    'End Sub

    Private Sub grdModifiers_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdModifiers.MouseUp
        If grdModifiers.CurrentRowIndex >= 0 Then
            grdModifiers.Select(grdModifiers.CurrentRowIndex)
        End If
    End Sub

    Private Sub grdModifiers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdModifiers.DoubleClick
        ''Try
        ''    UpdateModifiers()
        ''Catch ex As Exception

        ''End Try

        ''Dim ID As Long
        ''Dim objfrmMSTModifier As frmMSTModifier
        ''Try
        ''    If grdModifiers.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdModifiers.Item(grdModifiers.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdModifiers.CurrentRowIndex
        ''        objfrmMSTModifier = New frmMSTModifier(ID)
        ''        objfrmMSTModifier.Text = "Modify Modifier"
        ''        objfrmMSTModifier.ShowDialog(Me)
        ''        objfrmMSTModifier.BringToFront()
        ''        grdModifiers.DataSource = objclsModifier.GetAllModifiers
        ''        If Not IsNothing(objclsModifier.GetDataview) Then
        ''            objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
        ''        End If
        ''        grdModifiers.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMSTModifier = Nothing
        ''End Try
    End Sub

    ''Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    Try
    ''        If Not IsNothing(objclsModifier.GetDataview) Then
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")

    ''            objclsModifier.SetRowFilter(Trim(Search))
    ''            'HideColumn()
    ''        End If

    ''        'If Not IsNothing(objclsModifier.GetDataview) Then
    ''        '    objclsModifier.SetRowFilter(Trim(txtSearch.Text), grdModifiers.DataSource)
    ''        '    'HideColumn()
    ''        'End If
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.Message, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdModifiers.CurrentRowIndex >= 0 Then
                    grdModifiers.Select(0)
                    grdModifiers.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmVWModifier_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub
    '#''''''''''''Code/Event is added by Anil on 20071102
    Private Sub grdModifiers_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdModifiers.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdModifiers.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                Select Case htInfo.Column
                    Case 1
                        txtSearch.Text = ""
                        lblSearch.Text = "Code"
                    Case 2
                        txtSearch.Text = ""
                        lblSearch.Text = "Description"
                End Select
                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If

            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateModifiers()
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "Modifier", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''''''''''''''''''''
    Private Sub AddCategory()
        Dim objfrmMSTModifier As New frmMSTModifier
        Try
            blnModify = False
            objfrmMSTModifier.Text = "Add New Modifier"
            objfrmMSTModifier.ShowDialog(IIf(IsNothing(objfrmMSTModifier.Parent), Me, objfrmMSTModifier.Parent))

            If objfrmMSTModifier.CancelClick = False Then
                grdModifiers.DataSource = objclsModifier.GetAllModifiers

                '''' To Remember the Selection of Row 
                Dim myDataView As DataView = CType(grdModifiers.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        '''' when Modifier Code Found select that matching Row
                        If objfrmMSTModifier._ModifierCode = grdModifiers.Item(i, 1) Then
                            grdModifiers.CurrentRowIndex = i
                            grdModifiers.Select(i)
                            Exit For
                        End If
                    Next
                End If
            End If
            objfrmMSTModifier.Dispose()
            objfrmMSTModifier = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            objfrmMSTModifier = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        Try
            UpdateModifiers()
        Catch ex As Exception

        End Try

        ''Dim ID As Long
        ''Dim objfrmMSTModifier As frmMSTModifier
        ''Try
        ''    If grdModifiers.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdModifiers.Item(grdModifiers.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdModifiers.CurrentRowIndex

        ''        objfrmMSTModifier = New frmMSTModifier(ID)
        ''        objfrmMSTModifier.Text = "Modify Modifier"
        ''        objfrmMSTModifier.ShowDialog(Me)
        ''        objfrmMSTModifier.BringToFront()

        ''        If objfrmMSTModifier.CancelClick = False Then
        ''            grdModifiers.DataSource = objclsModifier.GetAllModifiers
        ''            If Not IsNothing(objclsModifier.GetDataview) Then
        ''                objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
        ''            End If
        ''        End If
        ''        grdModifiers.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMSTModifier = Nothing
        ''End Try
    End Sub
    Private Sub DeleteCategory()
        Dim ID As Long
        Dim Code As String
        Try
            If grdModifiers.VisibleRowCount >= 1 Then
                'blnModify = True
                If MessageBox.Show("Are you sure to Delete this Modifier's Detail", "Modifiers", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    ID = grdModifiers.Item(grdModifiers.CurrentRowIndex, 0).ToString
                    Code = grdModifiers.Item(grdModifiers.CurrentRowIndex, 1).ToString
                    objclsModifier.DeleteModifier(ID, Code)
                    grdModifiers.DataSource = objclsModifier.GetAllModifiers
                    If Not IsNothing(objclsModifier.GetDataview) Then
                        objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
                    End If
                    ''''''Code is Added by Anil 0n 20071102
                    Dim myDataView As DataView = CType(grdModifiers.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        Dim sortOrder As String = CType(grdModifiers.DataSource, DataView).Sort
                        Dim strSearchstring As String = txtSearch.Text.Trim
                        Dim arrcolumnsort() As String = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                    End If ''''''''''''''''''
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            grdModifiers.DataSource = objclsModifier.GetAllModifiers
            If Not IsNothing(objclsModifier.GetDataview) Then
                objclsModifier.SortDataview(objclsModifier.GetDataview.Table.Columns(1).ColumnName)
            End If
            CustomGridStyle()
            '''''Following 3 code lines are addded by Anil 0n 26/09/07 at 1:05 p.m.
            '''''This code clears the search textbox and gets the focus on the first row of the grid on click of Refresh button.
            txtSearch.Clear()
            txtSearch.Text = ""
            _blnSearch = True
            If grdModifiers.VisibleRowCount = 0 Then
                Exit Sub
            Else
                grdModifiers.CurrentRowIndex = 0
                grdModifiers.Select(grdModifiers.CurrentRowIndex)
            End If
            '''''upto here code is added
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Modifiers", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Call UpdateCategory()
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub
End Class
