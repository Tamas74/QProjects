Imports gloEMR.gloEMRWord

Public Class frmVWPatientEducation
    Inherits System.Windows.Forms.Form
    Implements IPatientContext

    Private objclsPatientEducation As New clsPatientEducation

    Dim _PatientID As Long

    Dim _VisitDate As Date
    Dim _VisitID As Int64
    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
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
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ts_btnNew As System.Windows.Forms.ToolStripButton
    Dim strsortorder As String
    Friend WithEvents ts_btnView As System.Windows.Forms.ToolStripButton

    'Shubhangi 20100105
    Dim _GridWidth As Integer
    Public Shared blnView As Boolean
    Dim dvPatientEducationList As DataView


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientID = PatientID
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

                If (IsNothing(grdPatienEducation) = False) Then
                    grdPatienEducation.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdPatienEducation)
                    grdPatienEducation.Dispose()
                    grdPatienEducation = Nothing
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
    Friend WithEvents pnlTopRightMain As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents grdPatienEducation As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWPatientEducation))
        Me.pnlTopRightMain = New System.Windows.Forms.Panel
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grdPatienEducation = New System.Windows.Forms.DataGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnNew = New System.Windows.Forms.ToolStripButton
        Me.ts_btnView = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.pnlTopRightMain.SuspendLayout()
        CType(Me.grdPatienEducation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRightMain
        '
        Me.pnlTopRightMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRightMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRightMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRightMain.Controls.Add(Me.btnClear)
        Me.pnlTopRightMain.Controls.Add(Me.Label9)
        Me.pnlTopRightMain.Controls.Add(Me.txtSearch)
        Me.pnlTopRightMain.Controls.Add(Me.lblSearch)
        Me.pnlTopRightMain.Controls.Add(Me.Label1)
        Me.pnlTopRightMain.Controls.Add(Me.Label2)
        Me.pnlTopRightMain.Controls.Add(Me.Label3)
        Me.pnlTopRightMain.Controls.Add(Me.Label4)
        Me.pnlTopRightMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRightMain.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRightMain.Name = "pnlTopRightMain"
        Me.pnlTopRightMain.Size = New System.Drawing.Size(726, 24)
        Me.pnlTopRightMain.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(495, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 48
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(491, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 49
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(91, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(90, 22)
        Me.lblSearch.TabIndex = 1
        Me.lblSearch.Text = "       Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(724, 1)
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
        Me.Label3.Location = New System.Drawing.Point(725, 1)
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
        Me.Label4.Size = New System.Drawing.Size(726, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'grdPatienEducation
        '
        Me.grdPatienEducation.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdPatienEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdPatienEducation.BackgroundColor = System.Drawing.Color.White
        Me.grdPatienEducation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdPatienEducation.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdPatienEducation.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatienEducation.CaptionForeColor = System.Drawing.Color.White
        Me.grdPatienEducation.CaptionVisible = False
        Me.grdPatienEducation.DataMember = ""
        Me.grdPatienEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPatienEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatienEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdPatienEducation.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdPatienEducation.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdPatienEducation.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatienEducation.HeaderForeColor = System.Drawing.Color.White
        Me.grdPatienEducation.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdPatienEducation.Location = New System.Drawing.Point(3, 0)
        Me.grdPatienEducation.Name = "grdPatienEducation"
        Me.grdPatienEducation.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdPatienEducation.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPatienEducation.ReadOnly = True
        Me.grdPatienEducation.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdPatienEducation.SelectionForeColor = System.Drawing.Color.Black
        Me.grdPatienEducation.Size = New System.Drawing.Size(726, 431)
        Me.grdPatienEducation.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(732, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnNew, Me.ts_btnView, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(732, 54)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnNew
        '
        Me.ts_btnNew.Image = CType(resources.GetObject("ts_btnNew.Image"), System.Drawing.Image)
        Me.ts_btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnNew.Name = "ts_btnNew"
        Me.ts_btnNew.Size = New System.Drawing.Size(37, 51)
        Me.ts_btnNew.Tag = "New"
        Me.ts_btnNew.Text = "&New"
        Me.ts_btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnView
        '
        Me.ts_btnView.Image = CType(resources.GetObject("ts_btnView.Image"), System.Drawing.Image)
        Me.ts_btnView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnView.Name = "ts_btnView"
        Me.ts_btnView.Size = New System.Drawing.Size(53, 51)
        Me.ts_btnView.Tag = "Modify"
        Me.ts_btnView.Text = "&Modify"
        Me.ts_btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
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
        Me.Panel1.Controls.Add(Me.grdPatienEducation)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(732, 434)
        Me.Panel1.TabIndex = 12
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 430)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(724, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(728, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(726, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlTopRight
        '
        Me.pnlTopRight.Controls.Add(Me.pnlTopRightMain)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 54)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTopRight.Size = New System.Drawing.Size(732, 30)
        Me.pnlTopRight.TabIndex = 24
        '
        'frmVWPatientEducation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(732, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlTopRight)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWPatientEducation"
        Me.Text = "View Patient Education"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRightMain.ResumeLayout(False)
        Me.pnlTopRightMain.PerformLayout()
        CType(Me.grdPatienEducation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWPatientEducation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub frmVWPatientEducation_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

    End Sub

    Private Sub frmVWPatientEducation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '26-Apr-13 Aniket: Resolving Memory Leaks
        If IsNothing(dvPatientEducationList) = False Then
            dvPatientEducationList.Dispose()
            dvPatientEducationList = Nothing
        End If
        If (IsNothing(objclsPatientEducation) = False) Then
            objclsPatientEducation.Dispose()
            objclsPatientEducation = Nothing
        End If
    End Sub


    Private Sub frmVWPatientEducation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtSearch.Focus()

        Try



            dvPatientEducationList = objclsPatientEducation.GetAllEducations(_PatientID)
            grdPatienEducation.Enabled = False
            grdPatienEducation.DataSource = dvPatientEducationList
            grdPatienEducation.Enabled = True
            CustomGridStyle()

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''sudhir 20081205
#Region "Commented - > Dhruv -> To not to show View Button"
    'Private Sub ViewEducation()
    '    Dim EducationID As Long
    '    Dim objfrmPatEduView As frmPatientEducationView

    '    If grdPatienEducation.VisibleRowCount > 0 Then
    '        Dim grdIndex As Integer = grdPatienEducation.CurrentRowIndex

    '        EducationID = grdPatienEducation.Item(grdIndex, 0).ToString

    '  objfrmPatEduView = New frmPatientEducationView(EducationID, _PatientID)
    '        blnView = True
    '        With objfrmPatEduView
    '            '.Text = "Modify Patient Letters"
    '            '.MdiParent = CType(Me.MdiParent, MainMenu)
    '            'CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
    '            'CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

    '            '.WindowState = FormWindowState.Maximized
    '            .Show()
    '            '.BringToFront()
    '            '.wdPatientEducation.Open("E:\Developer Working Folder\Documents\Daily Task Sheet\2008 Dec\Internal_TaskListtemplate 01Dec2008.doc")
    '        End With
    '    End If
    '    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
    'End Sub
#End Region



    


    Public Sub RefreshEducation()
        Dim dv As DataView
        dv = objclsPatientEducation.GetAllEducations(_PatientID)
        grdPatienEducation.Enabled = False
        grdPatienEducation.DataSource = dv
        grdPatienEducation.Enabled = True
        CustomGridStyle()
        txtSearch.Text = ""

    End Sub

    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        dv = objclsPatientEducation.DsDataview
        '' nEducationID, sTemplateName, nVisitID, dtVisitDate
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName())

        Dim colEduID As New DataGridTextBoxColumn
        colEduID.Width = 0
        colEduID.MappingName = dv.Table.Columns(0).ColumnName
        colEduID.HeaderText = "Education ID"

        Dim colVisitID As New DataGridTextBoxColumn
        With colVisitID
            .Width = 0
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "VisitID"
            .NullText = ""
        End With

        Dim colVisitDate As New DataGridTextBoxColumn
        With colVisitDate
            ' .Width = 0.3 * grdPatienEducation.Width
            .Width = 0.2 * _GridWidth
            .MappingName = dv.Table.Columns(2).ColumnName
            .HeaderText = "Visit Date"
            .NullText = ""
        End With

        Dim colTemplateName As New DataGridTextBoxColumn
        With colTemplateName
            '.Width = 0.7 * grdPatienEducation.Width
            .Width = 0.6 * _GridWidth
            .MappingName = dv.Table.Columns(3).ColumnName
            .HeaderText = "Educations"
            .NullText = ""
        End With

        Dim colSource As New DataGridTextBoxColumn
        With colSource
            '.Width = 0.7 * grdPatienEducation.Width
            .Width = 0.2 * _GridWidth
            .MappingName = dv.Table.Columns(4).ColumnName
            .HeaderText = "Source"
            .NullText = ""
        End With

        Dim colResourceCategory As New DataGridTextBoxColumn
        With colResourceCategory
            '.Width = 0.7 * grdPatienEducation.Width
            .Width = 0 * _GridWidth
            .MappingName = dv.Table.Columns(5).ColumnName
            .HeaderText = "Resource Category"
            .NullText = ""
        End With

        Dim colResourceType As New DataGridTextBoxColumn
        With colResourceType
            '.Width = 0.7 * grdPatienEducation.Width
            .Width = 0 * _GridWidth
            .MappingName = dv.Table.Columns(6).ColumnName
            .HeaderText = "Resource Type"
            .NullText = ""
        End With


        Dim colDocumentUrl As New DataGridTextBoxColumn
        With colDocumentUrl
            '.Width = 0.7 * grdPatienEducation.Width
            .Width = 0 * _GridWidth
            .MappingName = dv.Table.Columns(7).ColumnName
            .HeaderText = "Document URL"
            .NullText = ""
        End With



        '''''''Code is added by Anil on 20071105
        txtSearch.Text = ""
        txtSearch.Text = strsearchtxt
        If IsNothing(strcolumnName) OrElse strcolumnName = "" Then
            dv.Sort = "[" & dv.Table.Columns(1).ColumnName & "]" & strsortorder
        Else
            Dim strColumn As String = Replace(strcolumnName, "[", "")
            dv.Sort = "[" & strColumn & "]" & strSortBy
        End If
        ''''''''''''''''''''''''''''''''
        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {colEduID, colVisitID, colVisitDate, colTemplateName, colSource, colResourceCategory, colResourceType, colDocumentUrl})
        grdPatienEducation.TableStyles.Clear()
        grdPatienEducation.TableStyles.Add(ts)

        Dim dt As New DataTable
        dt = dv.ToTable()
        If (dt.Rows.Count >= 1) Then
            grdPatienEducation.Select(0)
        End If


    End Sub
    'Commented by Shubhangi
    'Private Sub grdPatienEducation_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPatienEducation.CurrentCellChanged
    '    Try
    '        Select Case grdPatienEducation.CurrentCell.ColumnNumber
    '            Case 3
    '                lblSearch.Text = "  Educations :"
    '        End Select
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub txtSearch_TextAlignChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextAlignChanged

    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dv As DataView
                dv = CType(grdPatienEducation.DataSource(), DataView)
                If IsNothing(dv) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                grdPatienEducation.Enabled = False
                grdPatienEducation.DataSource = dv
                grdPatienEducation.Enabled = True
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                'Commented by Shubhangi 20100121 Coz to implement Generalised In string search

                'Select Case Trim(lblSearch.Text)
                '    Case "Educations"
                '        If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '            dv.RowFilter = dv.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        Else
                '            'Commented by shubhangi 20091006
                '            'dv.RowFilter = dv.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '            'Shubhangi 20091006 Use in string search
                '            dv.RowFilter = dv.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"


                '        End If
                'End Select

                'Shubhangi 20100121 Search is now Generalized & instring
                dv.RowFilter = dv.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                grdPatienEducation.DataSource = dv

                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub grdPatienEducation_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPatienEducation.MouseDoubleClick
        ''sudhir 
        Try

            '******Shweta 20090828 *********'
            'To check exeception related to word
            If CheckWordForException() = False Then
                Exit Sub
            End If
            'End Shweta

            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdPatienEducation.HitTest(ptPoint)
            ''''''''''''Code is Added by Anil on 20071105
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                'dhruv -> Not to Show the View
                ''ViewEducation()
                'Dhruv - Changed the flow
                ModifyCategory()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgReferrals_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPatienEducation.MouseUp
        'If grdPatienEducation.CurrentRowIndex >= 0 Then
        '    grdPatienEducation.Select(grdPatienEducation.CurrentRowIndex)
        'End If
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdPatienEducation.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                grdPatienEducation.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdPatienEducation.CurrentRowIndex >= 0 Then
                    grdPatienEducation.Select(0)
                    grdPatienEducation.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Code uncommented by Shubhangi 20091228 referring the bug no: 4956
    'Commented by shubhangi 20091014 refering to Bug No:4133

    ''Code Added by Shilpa for adding the new buttons on 14th Nov 2007

    Private Sub DeleteCategory()
        Try
            If grdPatienEducation.VisibleRowCount >= 1 Then

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                'If CheckPatientStatus(_PatientID) = False Then
                '    Exit Sub
                'End If
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''
                If grdPatienEducation.IsSelected(grdPatienEducation.CurrentRowIndex) = False Then
                    Exit Sub
                End If

                If MessageBox.Show("Are you sure you want to delete this Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    Dim ID As Long
                    Dim VisitDate As String
                    ID = CType(grdPatienEducation.Item(grdPatienEducation.CurrentRowIndex, 0), Long)
                    VisitDate = CType(grdPatienEducation.Item(grdPatienEducation.CurrentRowIndex, 3), String)
                    objclsPatientEducation.DeleteEducations(ID, VisitDate, _PatientID)
                    grdPatienEducation.Enabled = False
                    grdPatienEducation.DataSource = objclsPatientEducation.GetAllEducations(_PatientID)
                    grdPatienEducation.Enabled = True
                    '''''''''''Code is Added by Anil on 20071105
                    Dim myDataView As DataView = CType(grdPatienEducation.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If

                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                    End If
                End If
            End If
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshCategory()
        Try
            Me.Cursor = Cursors.WaitCursor
            Call RefreshEducation()
            If grdPatienEducation.VisibleRowCount > 0 Then
                grdPatienEducation.CurrentRowIndex = 0
                grdPatienEducation.Select(0)
            End If
            _blnSearch = True
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            'Code uncommented by Shubhangi 20091228 referring the bug no: 4956
            'Comented by Shubhangi 20091014 by refering Bug no: 4133 
            'Shubhangi 20100105
            'Add new Category
            Case "New"
                NewCategory()
                'Case "View"
            Case "Modify"
                ''ViewEducation()
                ModifyCategory()
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub
    Private Sub NewCategory()

        'If CheckPatientStatus(_PatientID) = False Then
        '    Exit Sub
        'End If
        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If

        'dtWord = New DataTable

        Dim objWord As New clsWordDocument
        Dim dtPtEducation As New DataTable
        dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
        'dtWord = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
        If dtPtEducation.Rows.Count = 0 Then
            ''''If not present then exit from sub
            MessageBox.Show("No Template is associated for Patient Education. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            objWord = Nothing
            dtPtEducation = Nothing
            Exit Sub
        Else
            'line commented and modified by dipak 
            'gnVisitID = GenerateVisitID(_PatientID)
            _VisitID = GenerateVisitID(_PatientID)
            'end modification by dipak
            'Line commented by dipak 20100907 for case UC5070.003 - replace gnPatientID by local variable 
            'Dim ofrmPatientEducation As New frmPatientEducation(gnVisitID, gnPatientID, 0)
            Dim ofrmPatientEducation As New frmPatientEducation(_VisitID, _PatientID, 0)
            AddHandler ofrmPatientEducation.FormClosed, AddressOf On_PatientEducation_Closed
            Try
                ofrmPatientEducation.Text = "New Patient Education"
                ' .MdiParent = Me.ParentForm
                '.MyCaller = Me
                ofrmPatientEducation.MdiParent = Me.ParentForm
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False


                ofrmPatientEducation.Show()

                ofrmPatientEducation.WindowState = FormWindowState.Maximized
                ofrmPatientEducation.BringToFront()


            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True

            Finally
                ofrmPatientEducation = Nothing
            End Try
        End If

    End Sub


#Region "Dhruv  -> Opening for the Modification"
    Private Sub ModifyCategory()
        'If CheckPatientStatus(_PatientID) = False Then
        '    Exit Sub
        'End If
        If MainMenu.IsAccess(False, _PatientID) = False Then
            Exit Sub
        End If

        '' SUDHIR 20090521 '' CHECK PROVIDER ''
        If gblnProviderDisable = True Then
            If ShowAssociateProvider(_PatientID, Me) = True Then
                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
            End If
        End If
        '' END SUDHIR

        'To check exeception related to word
        If CheckWordForException() = False Then
            Exit Sub
        End If

        'dtWord = New DataTable

        Dim objWord As New clsWordDocument
        Dim dtPtEducation As New DataTable
        dtPtEducation = objWord.FillTemplates(enumTemplateFlag.PatientEducation)
        If Not IsNothing(dtPtEducation) Then
            Dim grdEducationID As Int64
            Dim grdDocumentName As String
            Dim grdSource As String
            Dim grdResourceCategory As String
            Dim grdResourcetype As String
            Dim grdDocumentUrl As String


            If dtPtEducation.Rows.Count = 0 Then
                ''''If not present then exit from sub
                MessageBox.Show("No Template is associated for Patient Education. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                objWord = Nothing
                dtPtEducation = Nothing
                Exit Sub
            Else
                _VisitID = GenerateVisitID(_PatientID) ''gn_VisitID replaced by _VisitID
                If grdPatienEducation.VisibleRowCount > 0 Then
                    Dim grdIndex As Integer = grdPatienEducation.CurrentRowIndex
                    grdEducationID = grdPatienEducation.Item(grdIndex, 0).ToString
                    grdDocumentName = grdPatienEducation.Item(grdIndex, 3).ToString()
                    grdSource = grdPatienEducation.Item(grdIndex, 4).ToString()
                    grdResourceCategory = grdPatienEducation.Item(grdIndex, 5).ToString()
                    grdResourcetype = grdPatienEducation.Item(grdIndex, 6).ToString()
                    grdDocumentUrl = grdPatienEducation.Item(grdIndex, 7).ToString()
                    _VisitID = grdPatienEducation.Item(grdIndex, 1).ToString
                    _VisitDate = grdPatienEducation.Item(grdIndex, 2).ToString
                Else
                    MessageBox.Show("No Template is associated for Patient Education. Please associate any template first", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not IsNothing(objWord) Then
                        objWord = Nothing
                    End If
                    If Not IsNothing(dtPtEducation) Then
                        dtPtEducation.Dispose()
                        dtPtEducation = Nothing
                    End If
                    Return
                End If

                If grdResourceCategory = "Online Library" Then


                    Dim InfoButtonForm As gloEMRGeneralLibrary.frmInfoButtonBrowser = gloEMRGeneralLibrary.frmInfoButtonBrowser.GetInstance
                    Try
                        RemoveHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
                    Catch ex As Exception

                    End Try

                    AddHandler InfoButtonForm.FormClosed, AddressOf On_InfoButton_Closed
                    With InfoButtonForm
                        .LoginProviderID = gnLoginProviderID
                        .PatientId = _PatientID
                        .VisitID = _VisitID

                        If grdSource = "Problem List" Then
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                        ElseIf grdSource = "Medication" Then
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                        ElseIf grdSource = "Orders" Then
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                        ElseIf grdSource = "" Then
                            .Source = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
                        End If

                        If grdResourceCategory = "Internal Library" Then
                            .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.InternalLibrary
                        ElseIf grdResourceCategory = "Online Library" Then
                            .ResourceCategory = gloEMRGeneralLibrary.clsInfobutton.enumResourceCategory.OnlineLibrary
                        End If


                        If grdResourcetype = "Patient Education" Then
                            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientEducation
                        ElseIf grdResourcetype = "Patient Reference Material" Then
                            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.PatientReferenceMaterial
                        ElseIf grdResourcetype = "Provider Reference Material" Then
                            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.ProviderReferenceMaterial
                        ElseIf grdResourcetype = "General" Then
                            .ResourceType = gloEMRGeneralLibrary.clsInfobutton.enumResourceType.General
                        End If
                        .EducationID = grdEducationID
                        .isViewed = True
                        .Show()
                        .ValidatePortalFeatures()
                        .NavigateTo(grdDocumentUrl)
                        .Visible = True
                        .Activate()
                    End With



                Else


                    'Line commented and modified by dipak for case UC5070.003 replace gnPatientID by local variable
                    'Dim ofrmPatientEducation As New frmPatientEducation(False, grdEducationID, grdDocumentName, _VisitID, gnPatientID, 0)
                    Dim src As Integer = 0
                    If grdSource = "Problem List" Then
                        src = gloEMRGeneralLibrary.clsInfobutton.enumSource.ProblemList
                    ElseIf grdSource = "Medication" Then
                        src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Medication
                    ElseIf grdSource = "Orders" Then
                        src = gloEMRGeneralLibrary.clsInfobutton.enumSource.Orders
                    ElseIf grdSource = "" Then
                        src = gloEMRGeneralLibrary.clsInfobutton.enumSource.None
                    End If


                    Dim ofrmPatientEducation As New frmPatientEducation(False, grdEducationID, grdDocumentName, _VisitID, _PatientID, 0, src)
                    'end modification by dipak
                    AddHandler ofrmPatientEducation.FormClosed, AddressOf On_PatientEducation_Closed
                    Try
                        If grdResourcetype = "Patient Education" Then
                            ofrmPatientEducation.Text = "Modify Patient Education"
                        ElseIf grdResourcetype = "Patient Reference Material" Then
                            ofrmPatientEducation.Text = "Modify Patient Reference Material"
                        ElseIf grdResourcetype = "Provider Reference Material" Then
                            ofrmPatientEducation.Text = "Modify Provider Reference Material"
                        End If

                        'ofrmPatientEducation.Text = "Modify Patient Education"
                        ' .MdiParent = Me.ParentForm
                        '.MyCaller = Me
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                        ofrmPatientEducation.MdiParent = Me.ParentForm
                        'Me.Hide()
                        'ofrmPatientEducation.BringToFront()
                        ofrmPatientEducation.Show()
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
                        ofrmPatientEducation.WindowState = FormWindowState.Maximized

                        Dim myDataView As DataView = CType(grdPatienEducation.DataSource, DataView)
                        If (IsNothing(myDataView) = False) Then


                            'SHUBHANGI 20110331
                            Dim i As Integer
                            For i = 0 To myDataView.Table.Rows.Count - 1
                                '''' when ID Found select that matching Row
                                If grdEducationID = grdPatienEducation.Item(i, 0) Then
                                    grdPatienEducation.CurrentRowIndex = i
                                    grdPatienEducation.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CType(Me.MdiParent, MainMenu).ShowHideMainMenu(True, True)
                        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
                        If Not IsNothing(dtPtEducation) Then
                            ofrmPatientEducation.Dispose()
                            ofrmPatientEducation = Nothing
                        End If
                    Finally
                        'If Not IsNothing(dtPtEducation) Then
                        '    ofrmPatientEducation.Dispose()
                        '    ofrmPatientEducation = Nothing
                        'End If
                    End Try
                End If

            End If '' Checking for the nothing
        End If
    End Sub
#End Region
    Private Sub On_PatientEducation_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        
        Dim ofrmPatientEducation As frmPatientEducation = Nothing
        Try
            ofrmPatientEducation = DirectCast(sender, frmPatientEducation)
        Catch ex As Exception

        End Try
       
        Try
            If (IsNothing(ofrmPatientEducation) = False) Then
                RemoveHandler ofrmPatientEducation.FormClosed, AddressOf On_PatientEducation_Closed
            End If

        Catch ex As Exception

        End Try

        Try
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Close()
            End If

        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmPatientEducation) = False) Then
                ofrmPatientEducation.Dispose()
                ofrmPatientEducation = Nothing
            End If
        Catch ex As Exception

        End Try
        Try
            RefreshCategory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub On_InfoButton_Closed(ByVal sender As Object, ByVal e As FormClosedEventArgs)

        Dim ofrmInfoButton As gloEMRGeneralLibrary.frmInfoButtonBrowser = Nothing
        Try
            ofrmInfoButton = DirectCast(sender, gloEMRGeneralLibrary.frmInfoButtonBrowser)
        Catch ex As Exception

        End Try

        Try
            If (IsNothing(ofrmInfoButton) = False) Then
                RemoveHandler ofrmInfoButton.FormClosed, AddressOf On_InfoButton_Closed
            End If

        Catch ex As Exception

        End Try

        Try
            If (IsNothing(ofrmInfoButton) = False) Then
                ofrmInfoButton.Close()
            End If

        Catch ex As Exception

        End Try
        Try
            If (IsNothing(ofrmInfoButton) = False) Then
                ofrmInfoButton.Dispose()
                ofrmInfoButton = Nothing
            End If
        Catch ex As Exception

        End Try
        Try
            RefreshCategory()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Shubhangi 20091223
    'Add this clear search text box
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub frmVWPatientEducation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        _GridWidth = grdPatienEducation.Width
        CustomGridStyle()
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property


End Class
