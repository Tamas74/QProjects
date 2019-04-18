'Imports System.IO
Public Class frmCategoryMst

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
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
    Friend WithEvents lstCategory As System.Windows.Forms.ListBox
    Friend WithEvents txtCategory As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Private WithEvents pnl_tls_Category As System.Windows.Forms.Panel
    Private WithEvents tlsCategory As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_New As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Modify As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Delete As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Clear As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCategoryMst))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lstCategory = New System.Windows.Forms.ListBox
        Me.txtCategory = New System.Windows.Forms.TextBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnl_tls_Category = New System.Windows.Forms.Panel
        Me.tlsCategory = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_New = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Modify = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Delete = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Clear = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel2.SuspendLayout()
        Me.pnl_tls_Category.SuspendLayout()
        Me.tlsCategory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "new.ico")
        Me.ImageList1.Images.SetKeyName(1, "edit.ico")
        Me.ImageList1.Images.SetKeyName(2, "Delet.ico")
        Me.ImageList1.Images.SetKeyName(3, "clear.ico")
        Me.ImageList1.Images.SetKeyName(4, "update.ico")
        Me.ImageList1.Images.SetKeyName(5, "Exit.ico")
        '
        'lstCategory
        '
        Me.lstCategory.BackColor = System.Drawing.Color.White
        Me.lstCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCategory.ForeColor = System.Drawing.Color.Black
        Me.lstCategory.ItemHeight = 14
        Me.lstCategory.Items.AddRange(New Object() {"Vinayak", "Manohar", "Gadekar"})
        Me.lstCategory.Location = New System.Drawing.Point(8, 7)
        Me.lstCategory.Name = "lstCategory"
        Me.lstCategory.Size = New System.Drawing.Size(432, 196)
        Me.lstCategory.TabIndex = 0
        '
        'txtCategory
        '
        Me.txtCategory.BackColor = System.Drawing.Color.White
        Me.txtCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.Color.Black
        Me.txtCategory.Location = New System.Drawing.Point(86, 1)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(351, 22)
        Me.txtCategory.TabIndex = 3
        '
        'lblCategory
        '
        Me.lblCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.Color.White
        Me.lblCategory.Location = New System.Drawing.Point(1, 1)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(85, 22)
        Me.lblCategory.TabIndex = 2
        Me.lblCategory.Text = "  Category :"
        Me.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.txtCategory)
        Me.Panel2.Controls.Add(Me.lblCategory)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(438, 24)
        Me.Panel2.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(436, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
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
        Me.Label3.Location = New System.Drawing.Point(437, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(438, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnl_tls_Category
        '
        Me.pnl_tls_Category.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_Category.Controls.Add(Me.tlsCategory)
        Me.pnl_tls_Category.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_Category.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_Category.Name = "pnl_tls_Category"
        Me.pnl_tls_Category.Size = New System.Drawing.Size(444, 53)
        Me.pnl_tls_Category.TabIndex = 12
        '
        'tlsCategory
        '
        Me.tlsCategory.BackColor = System.Drawing.Color.Transparent
        Me.tlsCategory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsCategory.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsCategory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_New, Me.btn_tls_Modify, Me.btn_tls_Delete, Me.btn_tls_Clear, Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tlsCategory.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsCategory.Location = New System.Drawing.Point(0, 0)
        Me.tlsCategory.Name = "tlsCategory"
        Me.tlsCategory.Size = New System.Drawing.Size(444, 53)
        Me.tlsCategory.TabIndex = 0
        Me.tlsCategory.Text = "toolStrip1"
        '
        'btn_tls_New
        '
        Me.btn_tls_New.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_New.Image = CType(resources.GetObject("btn_tls_New.Image"), System.Drawing.Image)
        Me.btn_tls_New.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_New.Name = "btn_tls_New"
        Me.btn_tls_New.Size = New System.Drawing.Size(37, 50)
        Me.btn_tls_New.Text = "&New"
        Me.btn_tls_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Modify
        '
        Me.btn_tls_Modify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Modify.Image = CType(resources.GetObject("btn_tls_Modify.Image"), System.Drawing.Image)
        Me.btn_tls_Modify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Modify.Name = "btn_tls_Modify"
        Me.btn_tls_Modify.Size = New System.Drawing.Size(53, 50)
        Me.btn_tls_Modify.Tag = "Modify"
        Me.btn_tls_Modify.Text = "&Modify"
        Me.btn_tls_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Delete
        '
        Me.btn_tls_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Delete.Image = CType(resources.GetObject("btn_tls_Delete.Image"), System.Drawing.Image)
        Me.btn_tls_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Delete.Name = "btn_tls_Delete"
        Me.btn_tls_Delete.Size = New System.Drawing.Size(50, 50)
        Me.btn_tls_Delete.Text = "&Delete"
        Me.btn_tls_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Clear
        '
        Me.btn_tls_Clear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Clear.Image = CType(resources.GetObject("btn_tls_Clear.Image"), System.Drawing.Image)
        Me.btn_tls_Clear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Clear.Name = "btn_tls_Clear"
        Me.btn_tls_Clear.Size = New System.Drawing.Size(41, 50)
        Me.btn_tls_Clear.Text = "&Clear"
        Me.btn_tls_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Save
        '
        Me.btn_tls_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Save.Image = CType(resources.GetObject("btn_tls_Save.Image"), System.Drawing.Image)
        Me.btn_tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Save.Name = "btn_tls_Save"
        Me.btn_tls_Save.Size = New System.Drawing.Size(40, 50)
        Me.btn_tls_Save.Tag = "Save"
        Me.btn_tls_Save.Text = "&Save"
        Me.btn_tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lstCategory)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(444, 208)
        Me.Panel1.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(8, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(432, 4)
        Me.Label5.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(4, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(4, 201)
        Me.Label6.TabIndex = 10
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 204)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(436, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 202)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(440, 3)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 202)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 2)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(438, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 53)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(444, 30)
        Me.Panel3.TabIndex = 11
        '
        'frmCategoryMst
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(444, 291)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.pnl_tls_Category)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCategoryMst"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tls_Category.ResumeLayout(False)
        Me.pnl_tls_Category.PerformLayout()
        Me.tlsCategory.ResumeLayout(False)
        Me.tlsCategory.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim blnAddEdit As Boolean = False

    Private Enum MR ' Menu Rights
        NewCommand
        EditCommand
        DeleteCommand
        ClearCommand
        SaveCommand
        CloseCommand
    End Enum

    Private Sub SaveCategory()
        On Error GoTo Trap
        Me.Cursor = Cursors.WaitCursor

        '---Check Validation---
        '1. Check Blanck Name
        If Trim(txtCategory.Text) = "" Then
            MsgBox("Please enter category", MsgBoxStyle.Information, "Save")
            Me.Cursor = Cursors.Default
            Exit Sub
        End If
        '2. Check Duplicate Name
        Dim oCategory As New clsDMSCategory
        If blnAddEdit = True Then
            If oCategory.IsExists(Trim(txtCategory.Text)) = True Then
                MsgBox("Category with same name already exists", MsgBoxStyle.Information, "Save")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            If UCase(Trim(txtCategory.Text)) <> UCase(Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) Then
                If oCategory.IsExists(Trim(txtCategory.Text)) = True Then
                    MsgBox("Category with same name already exists", MsgBoxStyle.Information, "Save")
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
            End If
        End If

        'Set Value to Objetc & Save category

        If blnAddEdit = True Then
            oCategory.NewCategory()
        Else
            oCategory.OpenCategory(Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)))
        End If
        oCategory.CategoryName = Trim(txtCategory.Text)
        If oCategory.Save() = True Then
            Dim strContainerPath As String = DMSRootPath & "\" & DMSRootFolder & "\" & DMSGeneralContainer
            Dim strRecyclePath As String = DMSRootPath & "\" & DMSRootFolder & "\" & DMSRecycleBinContainer
            If blnAddEdit = True Then   ' Create New Folder
                'General Container
                If System.IO.Directory.Exists(strContainerPath & "\" & Trim(txtCategory.Text)) = False Then
                    MkDir(strContainerPath & "\" & Trim(txtCategory.Text))
                Else
                    Rename(strContainerPath & "\" & Trim(txtCategory.Text), Get_NextFileName(strContainerPath & "\" & Trim(txtCategory.Text) & "_Old"))
                    MkDir(strContainerPath & "\" & Trim(txtCategory.Text))
                End If
                'Recycle Container
                If System.IO.Directory.Exists(strRecyclePath & "\" & Trim(txtCategory.Text)) = False Then
                    MkDir(strRecyclePath & "\" & Trim(txtCategory.Text))
                Else
                    Rename(strRecyclePath & "\" & Trim(txtCategory.Text), Get_NextFileName(strRecyclePath & "\" & Trim(txtCategory.Text) & "_Old"))
                    MkDir(strRecyclePath & "\" & Trim(txtCategory.Text))
                End If
            Else                        ' Rename Folder
                'General Container
                If System.IO.Directory.Exists(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) = True Then
                    If System.IO.Directory.Exists(strContainerPath & "\" & Trim(txtCategory.Text)) = False Then
                        Rename(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), strContainerPath & "\" & Trim(txtCategory.Text))
                    Else
                        Rename(strContainerPath & "\" & Trim(txtCategory.Text), Get_NextFileName(strContainerPath & "\" & Trim(txtCategory.Text) & "_Old"))
                        Rename(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), strContainerPath & "\" & Trim(txtCategory.Text))
                    End If
                Else
                    If System.IO.Directory.Exists(strContainerPath & "\" & Trim(txtCategory.Text)) = False Then
                        MkDir(strContainerPath & "\" & Trim(txtCategory.Text))
                    Else
                        Rename(strContainerPath & "\" & Trim(txtCategory.Text), Get_NextFileName(strContainerPath & "\" & Trim(txtCategory.Text) & "_Old"))
                        MkDir(strContainerPath & "\" & Trim(txtCategory.Text))
                    End If
                End If
                'Recycle Container
                If System.IO.Directory.Exists(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) = True Then
                    If System.IO.Directory.Exists(strRecyclePath & "\" & Trim(txtCategory.Text)) = False Then
                        Rename(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), strRecyclePath & "\" & Trim(txtCategory.Text))
                    Else
                        Rename(strRecyclePath & "\" & Trim(txtCategory.Text), Get_NextFileName(strRecyclePath & "\" & Trim(txtCategory.Text) & "_Old"))
                        Rename(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), strRecyclePath & "\" & Trim(txtCategory.Text))
                    End If
                Else
                    If System.IO.Directory.Exists(strRecyclePath & "\" & Trim(txtCategory.Text)) = False Then
                        MkDir(strRecyclePath & "\" & Trim(txtCategory.Text))
                    Else
                        Rename(strRecyclePath & "\" & Trim(txtCategory.Text), Get_NextFileName(strRecyclePath & "\" & Trim(txtCategory.Text) & "_Old"))
                        MkDir(strRecyclePath & "\" & Trim(txtCategory.Text))
                    End If
                End If
            End If
            'Maintain Audit Log
            'Dim objAudit As New clsAudit
            If blnAddEdit = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Category added", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, ActivityType.Add, "Category added", ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "Category Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, ActivityType.Add, "Category Modified", ActivityOutCome.Success)
            End If
            'objAudit = Nothing
        End If

        Fill_CategoryList()

        Set_MenuRights(MR.SaveCommand)
        If blnAddEdit = True Then
            Set_MenuRights(MR.NewCommand)
        End If

        oCategory = Nothing
        Me.Cursor = Cursors.Default
        Exit Sub
Trap:
        Me.Cursor = Cursors.WaitCursor
        MsgBox(Err.Description, MsgBoxStyle.Information, "Error")
        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, Err.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub frmCategoryMst_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Fill_CategoryList()
        Set_MenuRights(MR.CloseCommand)
    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        txtCategory.Text = lstCategory.Items.Item(lstCategory.SelectedIndex)
        If blnAddEdit = False Then
            txtCategory.Enabled = True
        Else
            txtCategory.Enabled = False
        End If
    End Sub
    Private Sub Set_MenuRights(ByVal MnuRht As MR)
        Select Case MnuRht
            Case MR.NewCommand
                btn_tls_New.Enabled = False
                btn_tls_Modify.Enabled = False
                btn_tls_Delete.Enabled = False
                btn_tls_Clear.Enabled = True
                btn_tls_Save.Enabled = True
                btn_tls_Save.Text = "Save"
                btn_tls_Close.Enabled = True

                lstCategory.Enabled = False
                txtCategory.Enabled = True
                txtCategory.Text = ""
                txtCategory.Select()
                blnAddEdit = True
            Case MR.EditCommand
                btn_tls_New.Enabled = False
                btn_tls_Modify.Enabled = False
                btn_tls_Delete.Enabled = True
                btn_tls_Clear.Enabled = True
                btn_tls_Save.Enabled = True
                btn_tls_Save.Text = "Update"
                btn_tls_Close.Enabled = True
                lstCategory.Enabled = True
                txtCategory.Enabled = False
                If Not lstCategory.Items.Count = 0 Then
                    lstCategory.SelectedIndex = 0
                End If
                lstCategory.Select()
                blnAddEdit = False
            Case MR.DeleteCommand
                btn_tls_New.Enabled = True
                btn_tls_Modify.Enabled = True
                btn_tls_Delete.Enabled = False
                btn_tls_Clear.Enabled = False
                btn_tls_Save.Enabled = False
                btn_tls_Close.Enabled = True
            Case MR.ClearCommand
                btn_tls_New.Enabled = True
                btn_tls_Modify.Enabled = True
                btn_tls_Delete.Enabled = False
                btn_tls_Clear.Enabled = False
                btn_tls_Save.Enabled = False
                btn_tls_Close.Enabled = True
                txtCategory.Text = ""
                btn_tls_New.Select()
                lstCategory.Enabled = False
                txtCategory.Enabled = False
            Case MR.SaveCommand
                btn_tls_New.Enabled = True
                btn_tls_Modify.Enabled = True
                btn_tls_Delete.Enabled = False
                btn_tls_Clear.Enabled = False
                btn_tls_Save.Enabled = False
                btn_tls_Save.Text = "Save"
                btn_tls_Close.Enabled = True
                lstCategory.Enabled = False
                txtCategory.Enabled = False
                txtCategory.Text = ""
                btn_tls_New.Select()
            Case MR.CloseCommand
                btn_tls_New.Enabled = True
                btn_tls_Modify.Enabled = True
                btn_tls_Delete.Enabled = False
                btn_tls_Clear.Enabled = False
                btn_tls_Save.Enabled = False
                btn_tls_Close.Enabled = True
                btn_tls_Save.Text = "Save"
                txtCategory.Text = ""
                btn_tls_New.Select()
                lstCategory.Enabled = False
                txtCategory.Enabled = False
        End Select
    End Sub

    Private Sub Fill_CategoryList()
        Dim oList As Collection
        Dim oCategoryList As New clsDMSCategory
        Dim i As Integer

        oList = oCategoryList.CategoryList
        lstCategory.Items.Clear()

        For i = 1 To oList.Count
            lstCategory.Items.Add(oList.Item(i))
        Next

        oList = Nothing

        oCategoryList = Nothing
    End Sub

    Private Function Get_NextFileName(ByVal sFileName As String) As String
        Dim i As Integer
        Get_NextFileName = ""
        For i = 1 To 32000
            If System.IO.Directory.Exists(sFileName & i) = False Then
                Get_NextFileName = sFileName & i
                Exit For
            End If
        Next
    End Function

    Private Sub txtCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    btnSave_Click(sender, e)
        'End If
    End Sub

    Private Sub lstCategory_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstCategory.KeyUp
        If e.KeyCode = Keys.Enter Then
            txtCategory.Select()
        ElseIf e.KeyCode = Keys.Delete Then
            DeleteCategory()
        End If
    End Sub

    Private Sub txtCategory_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCategory.TextChanged

    End Sub

    Private Sub tlsCategory_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsCategory.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "New"
                Set_MenuRights(MR.NewCommand)
            Case "Modify"
                Set_MenuRights(MR.EditCommand)
            Case "Delete"
                DeleteCategory()
            Case "Clear"
                Set_MenuRights(MR.ClearCommand)
            Case "Save"
                SaveCategory()
            Case "Close"
                'Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub
    Private Sub DeleteCategory()
        On Error GoTo Trap
        Me.Cursor = Cursors.WaitCursor

        '---Check Validation---
        '1. Check Duplicate Name
        Dim oCategory As New clsDMSCategory
        If oCategory.IsDelete(Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) = True Then
            MsgBox("Can not delete this category because its already used", MsgBoxStyle.Information, "Save")
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        If MsgBox("Do you want to delete the selected category?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'Set Value to Objetc & Save category
            oCategory.OpenCategory(Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)))
            If oCategory.Delete() = True Then
                Dim strContainerPath As String = DMSRootPath & "\" & DMSRootFolder & "\" & DMSGeneralContainer
                Dim strRecyclePath As String = DMSRootPath & "\" & DMSRootFolder & "\" & DMSRecycleBinContainer
                'General Container
                If System.IO.Directory.Exists(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) = True Then
                    Rename(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), Get_NextFileName(strContainerPath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)) & "_Del"))
                End If
                'Recycle Container
                If System.IO.Directory.Exists(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex))) = True Then
                    Rename(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)), Get_NextFileName(strRecyclePath & "\" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)) & "_Del"))
                End If

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Category, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "'" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)) & "' Category Deleted", gloAuditTrail.ActivityOutCome.Success)
                'Dim objAudit As New clsAudit
                'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & Trim(lstCategory.Items.Item(lstCategory.SelectedIndex)) & "' Category Deleted", gstrLoginName, gstrClientMachineName)
                'objAudit = Nothing

            End If

            Set_MenuRights(MR.DeleteCommand)
            Fill_CategoryList()

            lstCategory.Enabled = False
            txtCategory.Enabled = False
            txtCategory.Text = ""
            btn_tls_New.Select()
        End If

        oCategory = Nothing
        Me.Cursor = Cursors.Default
        Exit Sub
Trap:
        Me.Cursor = Cursors.WaitCursor
        MsgBox(Err.Description, MsgBoxStyle.Information, "Error")
        Me.Cursor = Cursors.Default
    End Sub

End Class
