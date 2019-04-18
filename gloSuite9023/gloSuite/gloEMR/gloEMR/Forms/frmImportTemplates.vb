Imports System.IO
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Text.RegularExpressions
Imports System.Text

Public Class frmImportTemplates
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
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                If (IsNothing(FolderBrowserDialog1) = False) Then
                    FolderBrowserDialog1.Dispose()
                    FolderBrowserDialog1 = Nothing
                End If
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
    Friend WithEvents pnlDirectorySettings1 As System.Windows.Forms.Panel
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtDirectoryPath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents pnlFiles As System.Windows.Forms.Panel
    Friend WithEvents pnlCategoryCommands As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvFiles As System.Windows.Forms.TreeView
    Friend WithEvents pnltrvCategories As System.Windows.Forms.Panel
    Friend WithEvents pnlProvider As System.Windows.Forms.Panel
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents trvCategories As System.Windows.Forms.TreeView
    Friend WithEvents pnl_TemplateCategoriesHeader As System.Windows.Forms.Panel
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsImportTemplate As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_ImportTemplates As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_CategorySelectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_CategoryClearAll As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBorder As System.Windows.Forms.Label
    Private WithEvents lbl_TopBorder2 As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBorder As System.Windows.Forms.Label
    Private WithEvents lbl_RightBorder As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlDirectorySettings As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ImgImportTemplate As System.Windows.Forms.ImageList
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents chkImportLiquidDt As System.Windows.Forms.CheckBox
    Friend WithEvents PnlOptions As System.Windows.Forms.Panel
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents rdbLiquidData As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTemplates As System.Windows.Forms.RadioButton
    Friend WithEvents lblTotalTemplates As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportTemplates))
        Me.pnlDirectorySettings1 = New System.Windows.Forms.Panel()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtDirectoryPath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkImportLiquidDt = New System.Windows.Forms.CheckBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlFiles = New System.Windows.Forms.Panel()
        Me.lbl_BottomBorder = New System.Windows.Forms.Label()
        Me.trvFiles = New System.Windows.Forms.TreeView()
        Me.pnlCategoryCommands = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblTotalTemplates = New System.Windows.Forms.Label()
        Me.lbl_TopBorder2 = New System.Windows.Forms.Label()
        Me.lbl_LeftBorder = New System.Windows.Forms.Label()
        Me.lbl_RightBorder = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnltrvCategories = New System.Windows.Forms.Panel()
        Me.trvCategories = New System.Windows.Forms.TreeView()
        Me.ImgImportTemplate = New System.Windows.Forms.ImageList(Me.components)
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.pnlProvider = New System.Windows.Forms.Panel()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnl_TemplateCategoriesHeader = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel()
        Me.tlsImportTemplate = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_CategorySelectAll = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_CategoryClearAll = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_ImportTemplates = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnlDirectorySettings = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.PnlOptions = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.rdbLiquidData = New System.Windows.Forms.RadioButton()
        Me.rdbTemplates = New System.Windows.Forms.RadioButton()
        Me.pnlDirectorySettings1.SuspendLayout()
        Me.pnlFiles.SuspendLayout()
        Me.pnlCategoryCommands.SuspendLayout()
        Me.pnltrvCategories.SuspendLayout()
        Me.pnlProvider.SuspendLayout()
        Me.pnl_TemplateCategoriesHeader.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsImportTemplate.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlDirectorySettings.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.PnlOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlDirectorySettings1
        '
        Me.pnlDirectorySettings1.BackColor = System.Drawing.Color.Transparent
        Me.pnlDirectorySettings1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDirectorySettings1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDirectorySettings1.Controls.Add(Me.btnBrowse)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label26)
        Me.pnlDirectorySettings1.Controls.Add(Me.txtDirectoryPath)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label3)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label5)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label6)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label7)
        Me.pnlDirectorySettings1.Controls.Add(Me.Label8)
        Me.pnlDirectorySettings1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDirectorySettings1.Location = New System.Drawing.Point(3, 3)
        Me.pnlDirectorySettings1.Name = "pnlDirectorySettings1"
        Me.pnlDirectorySettings1.Size = New System.Drawing.Size(1020, 24)
        Me.pnlDirectorySettings1.TabIndex = 3
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(729, 1)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 2
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(719, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(10, 22)
        Me.Label26.TabIndex = 9
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDirectoryPath
        '
        Me.txtDirectoryPath.BackColor = System.Drawing.Color.White
        Me.txtDirectoryPath.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtDirectoryPath.ForeColor = System.Drawing.Color.Black
        Me.txtDirectoryPath.Location = New System.Drawing.Point(108, 1)
        Me.txtDirectoryPath.Name = "txtDirectoryPath"
        Me.txtDirectoryPath.Size = New System.Drawing.Size(611, 22)
        Me.txtDirectoryPath.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 22)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "  Folder Path :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1018, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1019, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
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
        Me.Label8.Size = New System.Drawing.Size(1020, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'chkImportLiquidDt
        '
        Me.chkImportLiquidDt.AutoSize = True
        Me.chkImportLiquidDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkImportLiquidDt.Location = New System.Drawing.Point(324, 10)
        Me.chkImportLiquidDt.Name = "chkImportLiquidDt"
        Me.chkImportLiquidDt.Size = New System.Drawing.Size(176, 18)
        Me.chkImportLiquidDt.TabIndex = 13
        Me.chkImportLiquidDt.Text = "Import With Liquid Data"
        Me.chkImportLiquidDt.UseVisualStyleBackColor = True
        '
        'pnlFiles
        '
        Me.pnlFiles.BackColor = System.Drawing.Color.Transparent
        Me.pnlFiles.Controls.Add(Me.lbl_BottomBorder)
        Me.pnlFiles.Controls.Add(Me.trvFiles)
        Me.pnlFiles.Controls.Add(Me.pnlCategoryCommands)
        Me.pnlFiles.Controls.Add(Me.lbl_TopBorder2)
        Me.pnlFiles.Controls.Add(Me.lbl_LeftBorder)
        Me.pnlFiles.Controls.Add(Me.lbl_RightBorder)
        Me.pnlFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFiles.Location = New System.Drawing.Point(0, 30)
        Me.pnlFiles.Name = "pnlFiles"
        Me.pnlFiles.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlFiles.Size = New System.Drawing.Size(250, 596)
        Me.pnlFiles.TabIndex = 4
        '
        'lbl_BottomBorder
        '
        Me.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBorder.Location = New System.Drawing.Point(4, 567)
        Me.lbl_BottomBorder.Name = "lbl_BottomBorder"
        Me.lbl_BottomBorder.Size = New System.Drawing.Size(245, 1)
        Me.lbl_BottomBorder.TabIndex = 8
        Me.lbl_BottomBorder.Text = "label2"
        '
        'trvFiles
        '
        Me.trvFiles.BackColor = System.Drawing.Color.White
        Me.trvFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvFiles.CheckBoxes = True
        Me.trvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFiles.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvFiles.ForeColor = System.Drawing.Color.Black
        Me.trvFiles.Indent = 20
        Me.trvFiles.ItemHeight = 20
        Me.trvFiles.Location = New System.Drawing.Point(4, 1)
        Me.trvFiles.Name = "trvFiles"
        Me.trvFiles.ShowLines = False
        Me.trvFiles.ShowPlusMinus = False
        Me.trvFiles.ShowRootLines = False
        Me.trvFiles.Size = New System.Drawing.Size(245, 567)
        Me.trvFiles.TabIndex = 2
        '
        'pnlCategoryCommands
        '
        Me.pnlCategoryCommands.BackColor = System.Drawing.Color.Transparent
        Me.pnlCategoryCommands.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlCategoryCommands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCategoryCommands.Controls.Add(Me.Label21)
        Me.pnlCategoryCommands.Controls.Add(Me.lblTotalTemplates)
        Me.pnlCategoryCommands.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlCategoryCommands.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlCategoryCommands.Location = New System.Drawing.Point(4, 568)
        Me.pnlCategoryCommands.Name = "pnlCategoryCommands"
        Me.pnlCategoryCommands.Size = New System.Drawing.Size(245, 25)
        Me.pnlCategoryCommands.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(245, 1)
        Me.Label21.TabIndex = 10
        Me.Label21.Text = "label1"
        '
        'lblTotalTemplates
        '
        Me.lblTotalTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTemplates.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalTemplates.Name = "lblTotalTemplates"
        Me.lblTotalTemplates.Size = New System.Drawing.Size(245, 25)
        Me.lblTotalTemplates.TabIndex = 2
        Me.lblTotalTemplates.Text = "  Total Templates = 0"
        Me.lblTotalTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TopBorder2
        '
        Me.lbl_TopBorder2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBorder2.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBorder2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBorder2.Location = New System.Drawing.Point(4, 0)
        Me.lbl_TopBorder2.Name = "lbl_TopBorder2"
        Me.lbl_TopBorder2.Size = New System.Drawing.Size(245, 1)
        Me.lbl_TopBorder2.TabIndex = 9
        Me.lbl_TopBorder2.Text = "label1"
        '
        'lbl_LeftBorder
        '
        Me.lbl_LeftBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBorder.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBorder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBorder.Location = New System.Drawing.Point(3, 0)
        Me.lbl_LeftBorder.Name = "lbl_LeftBorder"
        Me.lbl_LeftBorder.Size = New System.Drawing.Size(1, 593)
        Me.lbl_LeftBorder.TabIndex = 7
        Me.lbl_LeftBorder.Text = "label4"
        '
        'lbl_RightBorder
        '
        Me.lbl_RightBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBorder.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBorder.Location = New System.Drawing.Point(249, 0)
        Me.lbl_RightBorder.Name = "lbl_RightBorder"
        Me.lbl_RightBorder.Size = New System.Drawing.Size(1, 593)
        Me.lbl_RightBorder.TabIndex = 6
        Me.lbl_RightBorder.Text = "label3"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(245, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Templates in Directory"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnltrvCategories
        '
        Me.pnltrvCategories.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvCategories.Controls.Add(Me.trvCategories)
        Me.pnltrvCategories.Controls.Add(Me.Label28)
        Me.pnltrvCategories.Controls.Add(Me.Label27)
        Me.pnltrvCategories.Controls.Add(Me.Label17)
        Me.pnltrvCategories.Controls.Add(Me.Label18)
        Me.pnltrvCategories.Controls.Add(Me.Label19)
        Me.pnltrvCategories.Controls.Add(Me.Label20)
        Me.pnltrvCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvCategories.Location = New System.Drawing.Point(253, 175)
        Me.pnltrvCategories.Name = "pnltrvCategories"
        Me.pnltrvCategories.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvCategories.Size = New System.Drawing.Size(773, 569)
        Me.pnltrvCategories.TabIndex = 5
        '
        'trvCategories
        '
        Me.trvCategories.BackColor = System.Drawing.Color.White
        Me.trvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCategories.ForeColor = System.Drawing.Color.Black
        Me.trvCategories.HideSelection = False
        Me.trvCategories.ImageIndex = 0
        Me.trvCategories.ImageList = Me.ImgImportTemplate
        Me.trvCategories.Indent = 20
        Me.trvCategories.ItemHeight = 20
        Me.trvCategories.Location = New System.Drawing.Point(4, 4)
        Me.trvCategories.Name = "trvCategories"
        Me.trvCategories.SelectedImageIndex = 0
        Me.trvCategories.ShowLines = False
        Me.trvCategories.ShowPlusMinus = False
        Me.trvCategories.ShowRootLines = False
        Me.trvCategories.Size = New System.Drawing.Size(765, 561)
        Me.trvCategories.TabIndex = 9
        '
        'ImgImportTemplate
        '
        Me.ImgImportTemplate.ImageStream = CType(resources.GetObject("ImgImportTemplate.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgImportTemplate.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgImportTemplate.Images.SetKeyName(0, "Bullet06.ico")
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(1, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(3, 561)
        Me.Label28.TabIndex = 15
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(1, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(768, 3)
        Me.Label27.TabIndex = 14
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 565)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(768, 1)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 565)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(769, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 565)
        Me.Label19.TabIndex = 11
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(770, 1)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "label1"
        '
        'pnlProvider
        '
        Me.pnlProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProvider.Controls.Add(Me.cmbProvider)
        Me.pnlProvider.Controls.Add(Me.Label2)
        Me.pnlProvider.Controls.Add(Me.Label9)
        Me.pnlProvider.Controls.Add(Me.Label10)
        Me.pnlProvider.Controls.Add(Me.Label11)
        Me.pnlProvider.Controls.Add(Me.Label12)
        Me.pnlProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProvider.Location = New System.Drawing.Point(0, 0)
        Me.pnlProvider.Name = "pnlProvider"
        Me.pnlProvider.Size = New System.Drawing.Size(770, 24)
        Me.pnlProvider.TabIndex = 6
        '
        'cmbProvider
        '
        Me.cmbProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.Location = New System.Drawing.Point(117, 1)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(283, 22)
        Me.cmbProvider.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 22)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "  Provider Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(768, 1)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(769, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(770, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(770, 27)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "  Template Categories"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnl_TemplateCategoriesHeader
        '
        Me.pnl_TemplateCategoriesHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnl_TemplateCategoriesHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnl_TemplateCategoriesHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_TemplateCategoriesHeader.Controls.Add(Me.Label13)
        Me.pnl_TemplateCategoriesHeader.Controls.Add(Me.Label14)
        Me.pnl_TemplateCategoriesHeader.Controls.Add(Me.Label15)
        Me.pnl_TemplateCategoriesHeader.Controls.Add(Me.Label16)
        Me.pnl_TemplateCategoriesHeader.Controls.Add(Me.Label4)
        Me.pnl_TemplateCategoriesHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_TemplateCategoriesHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnl_TemplateCategoriesHeader.Name = "pnl_TemplateCategoriesHeader"
        Me.pnl_TemplateCategoriesHeader.Size = New System.Drawing.Size(770, 27)
        Me.pnl_TemplateCategoriesHeader.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1, 26)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(768, 1)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 26)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(769, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 26)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(770, 1)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label1"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsImportTemplate)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(1026, 53)
        Me.pnl_tlspTOP.TabIndex = 11
        '
        'tlsImportTemplate
        '
        Me.tlsImportTemplate.BackColor = System.Drawing.Color.Transparent
        Me.tlsImportTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsImportTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsImportTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsImportTemplate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsImportTemplate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_CategorySelectAll, Me.btn_tls_CategoryClearAll, Me.btn_tls_ImportTemplates, Me.btn_tls_Close})
        Me.tlsImportTemplate.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsImportTemplate.Location = New System.Drawing.Point(0, 0)
        Me.tlsImportTemplate.Name = "tlsImportTemplate"
        Me.tlsImportTemplate.Size = New System.Drawing.Size(1026, 53)
        Me.tlsImportTemplate.TabIndex = 0
        Me.tlsImportTemplate.Text = "toolStrip1"
        '
        'btn_tls_CategorySelectAll
        '
        Me.btn_tls_CategorySelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_CategorySelectAll.Image = CType(resources.GetObject("btn_tls_CategorySelectAll.Image"), System.Drawing.Image)
        Me.btn_tls_CategorySelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_CategorySelectAll.Name = "btn_tls_CategorySelectAll"
        Me.btn_tls_CategorySelectAll.Size = New System.Drawing.Size(108, 50)
        Me.btn_tls_CategorySelectAll.Tag = "CategorySelectAll"
        Me.btn_tls_CategorySelectAll.Text = "&Select All Temp."
        Me.btn_tls_CategorySelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_CategorySelectAll.ToolTipText = "Select All Templates"
        '
        'btn_tls_CategoryClearAll
        '
        Me.btn_tls_CategoryClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_CategoryClearAll.Image = CType(resources.GetObject("btn_tls_CategoryClearAll.Image"), System.Drawing.Image)
        Me.btn_tls_CategoryClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_CategoryClearAll.Name = "btn_tls_CategoryClearAll"
        Me.btn_tls_CategoryClearAll.Size = New System.Drawing.Size(101, 50)
        Me.btn_tls_CategoryClearAll.Tag = "CategoryClearAll"
        Me.btn_tls_CategoryClearAll.Text = "&Clear All Temp."
        Me.btn_tls_CategoryClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_CategoryClearAll.ToolTipText = "Clear All Templates"
        Me.btn_tls_CategoryClearAll.Visible = False
        '
        'btn_tls_ImportTemplates
        '
        Me.btn_tls_ImportTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_ImportTemplates.Image = CType(resources.GetObject("btn_tls_ImportTemplates.Image"), System.Drawing.Image)
        Me.btn_tls_ImportTemplates.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_ImportTemplates.Name = "btn_tls_ImportTemplates"
        Me.btn_tls_ImportTemplates.Size = New System.Drawing.Size(120, 50)
        Me.btn_tls_ImportTemplates.Tag = "ImportTemplates"
        Me.btn_tls_ImportTemplates.Text = "&Import Templates"
        Me.btn_tls_ImportTemplates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(250, 30)
        Me.Panel4.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(247, 27)
        Me.Panel2.TabIndex = 19
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 26)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(245, 1)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 26)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(246, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 26)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(247, 1)
        Me.Label25.TabIndex = 5
        Me.Label25.Text = "label1"
        '
        'pnlDirectorySettings
        '
        Me.pnlDirectorySettings.Controls.Add(Me.pnlDirectorySettings1)
        Me.pnlDirectorySettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDirectorySettings.Location = New System.Drawing.Point(0, 88)
        Me.pnlDirectorySettings.Name = "pnlDirectorySettings"
        Me.pnlDirectorySettings.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDirectorySettings.Size = New System.Drawing.Size(1026, 30)
        Me.pnlDirectorySettings.TabIndex = 22
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlFiles)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 118)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 626)
        Me.Panel1.TabIndex = 23
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(250, 118)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 626)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnl_TemplateCategoriesHeader)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(253, 145)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(773, 30)
        Me.Panel3.TabIndex = 25
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.pnlProvider)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(253, 118)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(773, 27)
        Me.Panel5.TabIndex = 25
        '
        'PnlOptions
        '
        Me.PnlOptions.Controls.Add(Me.chkImportLiquidDt)
        Me.PnlOptions.Controls.Add(Me.Label29)
        Me.PnlOptions.Controls.Add(Me.Label30)
        Me.PnlOptions.Controls.Add(Me.Label31)
        Me.PnlOptions.Controls.Add(Me.Label32)
        Me.PnlOptions.Controls.Add(Me.rdbLiquidData)
        Me.PnlOptions.Controls.Add(Me.rdbTemplates)
        Me.PnlOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlOptions.Location = New System.Drawing.Point(0, 53)
        Me.PnlOptions.Name = "PnlOptions"
        Me.PnlOptions.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.PnlOptions.Size = New System.Drawing.Size(1026, 35)
        Me.PnlOptions.TabIndex = 27
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(4, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1018, 1)
        Me.Label29.TabIndex = 19
        Me.Label29.Text = "label1"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(4, 34)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1018, 1)
        Me.Label30.TabIndex = 18
        Me.Label30.Text = "label1"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(1022, 3)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 32)
        Me.Label31.TabIndex = 17
        Me.Label31.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(3, 3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 32)
        Me.Label32.TabIndex = 16
        Me.Label32.Text = "label4"
        '
        'rdbLiquidData
        '
        Me.rdbLiquidData.AutoSize = True
        Me.rdbLiquidData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbLiquidData.Location = New System.Drawing.Point(175, 9)
        Me.rdbLiquidData.Name = "rdbLiquidData"
        Me.rdbLiquidData.Size = New System.Drawing.Size(142, 18)
        Me.rdbLiquidData.TabIndex = 15
        Me.rdbLiquidData.Text = "Import &Liquid Data"
        Me.rdbLiquidData.UseVisualStyleBackColor = True
        '
        'rdbTemplates
        '
        Me.rdbTemplates.AutoSize = True
        Me.rdbTemplates.Checked = True
        Me.rdbTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbTemplates.Location = New System.Drawing.Point(34, 9)
        Me.rdbTemplates.Name = "rdbTemplates"
        Me.rdbTemplates.Size = New System.Drawing.Size(134, 18)
        Me.rdbTemplates.TabIndex = 14
        Me.rdbTemplates.TabStop = True
        Me.rdbTemplates.Text = "Import &Templates"
        Me.rdbTemplates.UseVisualStyleBackColor = True
        '
        'frmImportTemplates
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1026, 744)
        Me.Controls.Add(Me.pnltrvCategories)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlDirectorySettings)
        Me.Controls.Add(Me.PnlOptions)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmImportTemplates"
        Me.Text = "Import Templates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlDirectorySettings1.ResumeLayout(False)
        Me.pnlDirectorySettings1.PerformLayout()
        Me.pnlFiles.ResumeLayout(False)
        Me.pnlCategoryCommands.ResumeLayout(False)
        Me.pnltrvCategories.ResumeLayout(False)
        Me.pnlProvider.ResumeLayout(False)
        Me.pnl_TemplateCategoriesHeader.ResumeLayout(False)
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsImportTemplate.ResumeLayout(False)
        Me.tlsImportTemplate.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlDirectorySettings.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.PnlOptions.ResumeLayout(False)
        Me.PnlOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmImportTemplates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call Fill_Categories()
            Call FillProviders()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Import templates form due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With FolderBrowserDialog1

                ''''''''Integrated by Mayuri:20100730- to Import Liquid Data from XML 
                If rdbLiquidData.Checked = True Then
                    .Description = "Select Directory from which liquid data to Import"
                Else
                    .Description = "Select Directory from which Templates to Import"
                End If
                '''''''' Integrated by Mayuri:20100730- to Import Liquid Data from XML 

                .ShowNewFolderButton = False
            End With
            If FolderBrowserDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                txtDirectoryPath.Text = FolderBrowserDialog1.SelectedPath
                Dim objDirectory As New DirectoryInfo(txtDirectoryPath.Text)
                Dim objFile As FileInfo
                With trvFiles
                    .BeginUpdate()
                    .Nodes.Clear()
                    'Dim nCount As Int16
                    '' COMMENT BY SUDHIR 20090523 '' FETCH ONLY .doc, .docx FILES
                    ''For Each objFile In objDirectory.GetFiles("*.doc*") 'nCount = 0 To objFiles.GetUpperBound(0)
                    For Each objFile In objDirectory.GetFiles  'nCount = 0 To objFiles.GetUpperBound(0)
                        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
                        If rdbLiquidData.Checked = True Then
                            If objFile.Extension = ".XML" Then
                                Dim myNode As New TreeNode
                                myNode.Text = Replace(objFile.Name, objFile.Extension, "")
                                myNode.Tag = objFile.FullName
                                trvFiles.Nodes.Add(myNode) ')
                            End If
                            lblTotalTemplates.Text = "Total Liquid Data=" & trvFiles.GetNodeCount(True)
                        Else
                            If objFile.Extension = ".doc" Or objFile.Extension = ".docx" Then
                                Dim myNode As New TreeNode
                                myNode.Text = Replace(objFile.Name, objFile.Extension, "")
                                myNode.Tag = objFile.FullName
                                trvFiles.Nodes.Add(myNode) ')
                            End If
                            lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
                        End If
                        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
                    Next
                    'lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
                    .ExpandAll()
                    .EndUpdate()
                End With
                objDirectory = Nothing
                objFile = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to browse Import folder path due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub ImportSelectAll()
        Try
            Dim nCount As Int16
            For nCount = 0 To trvFiles.GetNodeCount(True) - 1
                trvFiles.Nodes(nCount).Checked = True
            Next
            If trvFiles.GetNodeCount(True) > 0 Then
                btn_tls_CategorySelectAll.Visible = False
                btn_tls_CategoryClearAll.Visible = True
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to select all the templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub ImportClearAll()
        Try
            Dim nCount As Int16
            For nCount = 0 To trvFiles.GetNodeCount(True) - 1
                trvFiles.Nodes(nCount).Checked = False
            Next
            If trvFiles.GetNodeCount(True) > 0 Then
                btn_tls_CategorySelectAll.Visible = True
                btn_tls_CategoryClearAll.Visible = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to deselect all templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Fill_Categories()
        With trvCategories
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvNde As TreeNode

            Dim objTemplateGallery As New clsTemplateGallery
            Dim dtTemplates As DataTable = objTemplateGallery.GetAllCategory
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing
            Dim nCount As Int16
            For nCount = 0 To dtTemplates.Rows.Count - 1
                trvNde = New TreeNode
                With trvNde
                    .Tag = dtTemplates.Rows(nCount).Item(0)
                    .Text = dtTemplates.Rows(nCount).Item(1)
                    .ImageIndex = 0
                    .SelectedImageIndex = 0
                End With
                .Nodes.Add(trvNde)
            Next
            .ExpandAll()
            .EndUpdate()
            dtTemplates.Dispose()
            dtTemplates = Nothing
        End With
    End Sub
    Private Sub FillProviders()

        Dim objTemplateGallery As New clsTemplateGallery
        Dim dtProviders As DataTable = objTemplateGallery.GetAllProvider()
        objTemplateGallery.Dispose()
        objTemplateGallery = Nothing
        If Not IsNothing(dtProviders) Then
            '' Here we add "All"(indicating All Doctors / Providers) 
            '' To datatable dt which contains Provider Name & ID's 

            Dim objrow As DataRow
            objrow = dtProviders.NewRow
            objrow.Item(0) = 0
            objrow.Item(1) = "All"
            dtProviders.Rows.Add(objrow)

            '' Attach DataSource to  CmbProvider 
            cmbProvider.DataSource = dtProviders
            cmbProvider.DisplayMember = dtProviders.Columns(1).ColumnName 'Provider Name
            cmbProvider.ValueMember = dtProviders.Columns(0).ColumnName 'Provider ID
            ''cmbProviders.SelectedValue = 0
            '20080922 cmbProvider.Text = gstrDoctorName
            'cmbProvider.Text = gstrPatientProviderName
            'Sandip Darade 20090505
            ''Select login provider
            cmbProvider.SelectedValue = gnLoginProviderID

        End If
    End Sub

    Private Sub CloseImportTemplates()

        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ImportTemplate()
        Dim strinvalidtemplatelist As New StringBuilder

        Try
            If Trim(txtDirectoryPath.Text) = "" Then
                MessageBox.Show("Select the Import directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            If Directory.Exists(Trim(txtDirectoryPath.Text)) = False Then
                MessageBox.Show("Select the valid Import directory path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            If IsNothing(trvCategories.SelectedNode) Then
                MessageBox.Show("Select the category in which you want to Import the templates.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                trvCategories.Focus()
                Exit Sub
            End If
            Dim nCount As Int16
            Dim nTotalTemplatesSelected As Int16
            For nCount = 0 To trvFiles.GetNodeCount(True) - 1
                If trvFiles.Nodes(nCount).Checked Then
                    nTotalTemplatesSelected = nTotalTemplatesSelected + 1
                End If
            Next
            If nTotalTemplatesSelected <= 0 Then
                MessageBox.Show("Select at least one template which you want to Import.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                trvFiles.Focus()
                Exit Sub
            End If
            'If MessageBox.Show("Are you sure, you want to Import the selected templates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            Dim objclsTemplateGallery As New clsTemplateGallery
            Dim strTemplateName As String
            Dim strTemplatesWhichAreNotImported As String = String.Empty
            Dim dir As DirectoryInfo = New DirectoryInfo(txtDirectoryPath.Text)
          
            For nCount = 0 To trvFiles.GetNodeCount(True) - 1
                If trvFiles.Nodes(nCount).Checked Then
                    strTemplateName = trvFiles.Nodes(nCount).Text
                    ''''''''Integrated by Mayuri  as on 20100729 - to Import Liquid Data from XML 
                    Dim sFnm As String = trvFiles.Nodes(nCount).Text
                    ''''''''Integrated by Mayuri  as on 20100729 - to Import Liquid Data from XML 
                    If (strTemplateName.Contains("Pre Op for Laparoscopic robotic assisted Myoectom")) Then
                        strTemplateName = strTemplateName
                    End If
                    If strTemplateName.Trim().Length >= 45 Then ''added condition for bugid 73816 to not import templates having more than 45 characters
                        strinvalidtemplatelist.Append(strTemplateName & vbNewLine)
                        Continue For

                    End If
                    strTemplateName = objclsTemplateGallery.ImportTemplateName(strTemplateName, trvCategories.SelectedNode.Tag, cmbProvider.SelectedValue)

                    'strTemplateName = objclsTemplateGallery.ImportTemplateName(strTemplateName, trvCategories.SelectedNode.Tag, cmbProvider.SelectedValue)
                    Try

                        '' SUDHIR 20090521 '' CHECK FOR DUPLICATE TEMPLATE NAME '' 
                        Dim cnt As Int16 = 1
                        Dim tempName As String = strTemplateName
                        While objclsTemplateGallery.CheckDuplicate(0, tempName, trvCategories.SelectedNode.Tag, cmbProvider.SelectedValue) = True
                            tempName = strTemplateName
                            '' THIS CODE IS DUE TO LENGTH LIMIT OF TEMPLATENAME(50) ''
                            '' REMOVING LAST 3 CHARACTERS IF LIMIT CROSSING ''
                            '' THEN CONCAT NUMBER SERIES ''
                            If tempName.Length >= 48 Then
                                tempName = tempName.Remove(tempName.Length - 4, 4)
                            End If
                            tempName = tempName & "-" & cnt
                            cnt += 1
                        End While
                        strTemplateName = tempName
                        '' END SUDHIR '' 

                        objclsTemplateGallery.AddNewTemplateGallery(0, strTemplateName, trvCategories.SelectedNode.Tag, trvCategories.SelectedNode.Text, cmbProvider.SelectedValue, trvFiles.Nodes(nCount).Tag) 'txtDirectoryPath.Text & "\" & trvFiles.Nodes(nCount).Text & ".docx")
                        ''''''''Integrated by Mayuri as on 20100729 - to Import Liquid Data from XML 
                        If chkImportLiquidDt.Checked = True Then
                            If sFnm <> "" Then
                                Try
                                    ImportFromXML(txtDirectoryPath.Text.ToString.Trim & "\" & sFnm & ".xml")
                                Catch ex As Exception
                                    MessageBox.Show("Unable to Import all the Templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                End Try

                            End If
                        End If
                        ''''''''Added by Ujwala Atre as on 07282010 - to Import Liquid Data from XML 
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        strTemplatesWhichAreNotImported = strTemplatesWhichAreNotImported & strTemplateName & ","
                    End Try
                End If
            Next
            If Trim(strTemplatesWhichAreNotImported) = "" And strinvalidtemplatelist.ToString() = String.Empty Then
                MessageBox.Show("All templates have been successfully imported.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            If strTemplatesWhichAreNotImported.ToString().Trim() <> "" Then
                MessageBox.Show("Following templates are not imported." & vbCrLf & strTemplatesWhichAreNotImported.Substring(0, strTemplatesWhichAreNotImported.Length - 1), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            If strinvalidtemplatelist.ToString().Trim() <> "" Then ''added condition for bugid 73816 to not import templates having more than 45 characters
                MessageBox.Show("Following templates are not loaded as the length of their names is more than 45 characters. Please give smaller names to the template documents and reload again." & vbCrLf & vbCrLf & strinvalidtemplatelist.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            End If
            objclsTemplateGallery.Dispose()
            objclsTemplateGallery = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to Import all the templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            strinvalidtemplatelist = Nothing
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    'Private Sub ImportFromXML(ByVal TemplateNm As String)
    '    ''''''''Integrated by Mayuri as on 20100729 - to Import Liquid Data from XML 
    '    Dim oDB As New DataBaseLayer
    '    Dim Ds As New DataSet
    '    Dim dt As New DataTable
    '    Dim dtChk As New DataTable
    '    Dim sSQL As String
    '    Dim sfx As Int32 = 1
    '    Dim cnt As Int32 = 0
    '    '' Create a new Connection and SqlDataAdapter
    '    Dim sqlCon As SqlConnection
    '    Dim sqlDA As SqlDataAdapter
    '    Dim DsNew As New DataSet
    '    Dim DtRw As DataRow

    '    Try
    '        If File.Exists(TemplateNm) Then
    '            Ds.ReadXml(TemplateNm)
    '            dt = Ds.Tables(0)

    '            sqlCon = New SqlConnection(oDB.ConnectionString)
    '            sqlDA = New SqlDataAdapter("select * from LiquidData_MST", sqlCon)
    '            sqlDA.Fill(DsNew, "LiquidData_MST")

    '            '''''''''''''''''chk for duplicates
    '            For i As Int32 = 0 To dt.Rows.Count - 1
    '                sSQL = "select count(*) FROM LiquidData_MST where selementname = '" & dt.Rows(i).Item("selementname").ToString & "' and ngroupid=0  "
    '                dtChk = oDB.GetDataTable_Query(sSQL)
    '                If dtChk.Rows.Count > 0 Then
    '                    If dtChk.Rows(0).Item(0) > 0 Then
    '                        If dt.Rows(i).Item("selementname").ToString.Contains("-") Then
    '                            sSQL = "select isnull(max(right(selementname,len(selementname)-charindex('-',selementname))),0) FROM LiquidData_MST where selementname like '" & dt.Rows(i).Item("selementname").ToString & "%' and ngroupid=0  "
    '                        Else
    '                            sSQL = "select isnull(max(right(selementname,len(selementname)-charindex('-',selementname))),0) FROM LiquidData_MST where selementname like '" & dt.Rows(i).Item("selementname").ToString & "-%' and ngroupid=0  "
    '                        End If
    '                        dtChk = oDB.GetDataTable_Query(sSQL)
    '                        If dtChk.Rows.Count > 0 Then
    '                            If dtChk.Rows(0).Item(0) > 0 Then
    '                                If dt.Rows(i).Item("selementname").ToString.Contains("-") Then
    '                                    Dim sval As String = "-" & (dtChk.Rows(0).Item(0) + 1)
    '                                    Dim schk As String = dt.Rows(i).Item("selementname").ToString.Remove(0, dt.Rows(i).Item("selementname").ToString.IndexOf("-"))
    '                                    dt.Rows(i).Item("selementname") = Regex.Replace(dt.Rows(i).Item("selementname").ToString, schk, sval)
    '                                Else
    '                                    dt.Rows(i).Item("selementname") = dt.Rows(i).Item("selementname").ToString & "-" & (dtChk.Rows(0).Item(0) + 1)
    '                                End If
    '                            Else
    '                                dt.Rows(i).Item("selementname") = dt.Rows(i).Item("selementname").ToString & "-" & sfx
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            '''''''''''''''''

    '            For i As Int32 = 0 To Ds.Tables(0).Rows.Count - 1
    '                DtRw = DsNew.Tables(0).NewRow()
    '                For j As Int32 = 0 To Ds.Tables(0).Columns.Count - 1
    '                    DtRw(j) = Ds.Tables(0).Rows(i).Item(j).ToString
    '                Next
    '                DsNew.Tables(0).Rows.Add(DtRw)
    '            Next

    '            Dim cmdBldr As SqlCommandBuilder = New SqlCommandBuilder(sqlDA)
    '            sqlDA.Update(DsNew, "LiquidData_MST")
    '        End If
    '    Catch ex As Exception
    '    Finally
    '        oDB = Nothing
    '        sqlCon = Nothing
    '    End Try
    '    ''''''''Integrated by Mayuri as on 20100729 - to Import Liquid Data from XML 
    'End Sub
    Private Sub ImportFromXML(ByVal TemplateNm As String)
        ''''''''Integrated by Mayuri as on 20100803 - to Import Liquid Data from XML 
        Dim oDB As New DataBaseLayer

        Dim sSQL As String
        Dim sfx As Int32 = 1
        Dim cnt As Int32 = 0
        Dim eID As Long = 0
        Dim subID As Long = 0
        '' Create a new Connection and SqlDataAdapter

        Try
            If File.Exists(TemplateNm) Then
                Dim Ds As New DataSet
                Dim dt As DataTable = Nothing

                Ds.ReadXml(TemplateNm)
                dt = Ds.Tables(0)

                'Problem : 00000163
                'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                'Change : Column nSequenceNo added in below select query. Created the new column nSequenceNo in datatable containing liquid data imported from XML file and assigned the sequence number.
                Dim sqlCon As SqlConnection
                Dim sqlDA As SqlDataAdapter

                sqlCon = New SqlConnection(DataBaseLayer.ConnectionString)

                sqlDA = New SqlDataAdapter("select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty, nSequenceNo from LiquidData_MST", sqlCon)
                Dim DsNew As New DataSet

                sqlDA.Fill(DsNew, "LiquidData_MST")

                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 AndAlso dt.Columns.Contains("nSequenceNo") = False Then
                    dt.Columns.Add("nSequenceNo", System.Type.GetType("System.Int32"))
                End If

                '''''''''''''''''chk for duplicates
                For i As Int32 = 0 To dt.Rows.Count - 1
                    sSQL = "select count(nElementID) FROM LiquidData_MST where selementname = '" & dt.Rows(i).Item("selementname").ToString & "' and ngroupid=0  "
                    Dim dtChk As DataTable = Nothing
                    dtChk = oDB.GetDataTable_Query(sSQL)
                    If dtChk.Rows.Count > 0 Then
                        If dtChk.Rows(0).Item(0) > 0 Then
                            If dt.Rows(i).Item("selementname").ToString.Contains("-") Then
                                sSQL = "select isnull(max(convert(decimal,right(selementname,len(selementname)-charindex('-',selementname)))),0) FROM LiquidData_MST where selementname like '" & dt.Rows(i).Item("selementname").ToString & "%' and ngroupid=0  "
                            Else
                                sSQL = "select isnull(max(convert(decimal,right(selementname,len(selementname)-charindex('-',selementname)))),0) FROM LiquidData_MST where selementname like '" & dt.Rows(i).Item("selementname").ToString & "-%' and ngroupid=0  "
                            End If
                            dtChk.Dispose()
                            dtChk = Nothing
                            dtChk = oDB.GetDataTable_Query(sSQL)
                            If dtChk.Rows.Count > 0 Then
                                If dtChk.Rows(0).Item(0) > 0 Then
                                    If dt.Rows(i).Item("selementname").ToString.Contains("-") Then
                                        Dim sval As String = "-" & (dtChk.Rows(0).Item(0) + 1)
                                        Dim schk As String = dt.Rows(i).Item("selementname").ToString.Remove(0, dt.Rows(i).Item("selementname").ToString.IndexOf("-"))
                                        dt.Rows(i).Item("selementname") = Regex.Replace(dt.Rows(i).Item("selementname").ToString, schk, sval)
                                    Else
                                        dt.Rows(i).Item("selementname") = dt.Rows(i).Item("selementname").ToString & "-" & (dtChk.Rows(0).Item(0) + 1)
                                    End If
                                Else
                                    dt.Rows(i).Item("selementname") = dt.Rows(i).Item("selementname").ToString & "-" & sfx
                                End If
                                '''''''''''''''''
                                If dt.Rows(i).Item("ngroupid") = 0 Then
                                    eID = oDB.GetDataValue("select dbo.GetPrefixTransactionID(0)", False)
                                    dt.Rows(i).Item("nelementid") = eID
                                End If
                                '''''''''''''''''
                            End If
                        End If
                    End If
                    '''''''''''''''''
                    If eID <> 0 And dt.Rows(i).Item("ngroupid") <> 0 Then
                        If subID = 0 Then
                            subID = oDB.GetDataValue("select dbo.GetPrefixTransactionID(" & eID & ")", False)
                        Else
                            subID = oDB.GetDataValue("select dbo.GetPrefixTransactionID(" & subID & ")", False)
                        End If
                        dt.Rows(i).Item("nelementid") = subID
                        dt.Rows(i).Item("ngroupid") = eID
                    End If
                    '''''''''''''''''
                    dt.Rows(i).Item("nSequenceNo") = i
                    dtChk.Dispose()
                    dtChk = Nothing

                Next
                '''''''''''''''''

                For i As Int32 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim DtRw As DataRow

                    DtRw = DsNew.Tables(0).NewRow()
                    For j As Int32 = 0 To Ds.Tables(0).Columns.Count - 1
                        DtRw(j) = Ds.Tables(0).Rows(i).Item(j).ToString
                    Next
                    DsNew.Tables(0).Rows.Add(DtRw)
                Next

                Dim cmdBldr As SqlCommandBuilder = New SqlCommandBuilder(sqlDA)
                sqlDA.Update(DsNew, "LiquidData_MST")

                If cmdBldr IsNot Nothing Then
                    cmdBldr.Dispose()
                    cmdBldr = Nothing
                End If
                sqlDA.Dispose()
                sqlDA = Nothing

                Ds.Dispose()
                Ds = Nothing
                dt.Dispose()
                dt = Nothing
                sqlCon.Close()
                sqlCon.Dispose()
                sqlCon = Nothing
                DsNew.Dispose()
                DsNew = Nothing
            End If
        Catch ex As Exception
        Finally
            oDB.Dispose()
            oDB = Nothing

        End Try
        ''''''''Integrated by Mayuri as on 20100803 - to Import Liquid Data from XML 
    End Sub

    Private Sub tlsImportTemplate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsImportTemplate.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "ImportTemplates"
                If rdbTemplates.Checked = True Then
                    ImportTemplate()
                Else
                    ImportLiquidData()
                End If

            Case "Close"
                CloseImportTemplates()

            Case "CategorySelectAll"
                ImportSelectAll()

            Case "CategoryClearAll"
                ImportClearAll()



        End Select
    End Sub
    Private Sub ImportLiquidData()
        ''''''''Integrated by Mayuri:20100730- to Import Liquid Data from XML to DB
        'Dim oDB As New DataBaseLayer
        'Dim Ds As New DataSet
        'Dim dt As New DataTable
        '' Create a new Connection and SqlDataAdapter
        'Dim sqlCon As SqlConnection
        'Dim sqlDA As SqlDataAdapter = Nothing
        'Dim DsNew As New DataSet
        'Dim DtRw As DataRow = Nothing

        Try

            If Trim(txtDirectoryPath.Text) = "" Then
                MessageBox.Show("Please select the Import Directory", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            If Directory.Exists(txtDirectoryPath.Text) = False Then
                MessageBox.Show("Please select the valid Import Directory Path", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            Dim nCount As Int16
            Dim nTotalTemplatesSelected As Int16
            For nCount = 0 To trvFiles.GetNodeCount(True) - 1
                If trvFiles.Nodes(nCount).Checked Then
                    nTotalTemplatesSelected = nTotalTemplatesSelected + 1
                End If
            Next
            If nTotalTemplatesSelected <= 0 Then
                MessageBox.Show("Please select atleast one Liquid Data which you want to import", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                trvFiles.Focus()
                Exit Sub
            End If

            Dim FullPath As String = Trim(txtDirectoryPath.Text)

            For Each nd As TreeNode In trvFiles.Nodes
                If nd.Checked = True Then
                    ImportFromXML(FullPath & "\" & nd.Text & ".XML")
                End If
            Next
            MessageBox.Show("Liquid Data successfully imported.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
        Finally
            '   oDB = Nothing
            '  sqlCon = Nothing
        End Try
        ''''''''Integrated by Mayuri:20100730- to Import Liquid Data from XML to DB
    End Sub


    Private Sub rdbLiquidData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbLiquidData.CheckedChanged
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
        '''''''''''''''        
        If rdbLiquidData.Checked = True Then
            Panel5.Visible = False
            pnltrvCategories.Visible = False
            chkImportLiquidDt.Visible = False
            Panel3.Visible = False
            Label1.Text = "Liquid Data in Directory"
            lblTotalTemplates.Text = "Total Liquid Data=" & trvFiles.GetNodeCount(True)
        Else
            Panel5.Visible = True
            pnltrvCategories.Visible = True
            chkImportLiquidDt.Visible = True
            Panel3.Visible = True
            Label1.Text = "Templates in Directory"
            lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
            ''''''''''''''''''''''''
            Call Fill_Categories()
            Call FillProviders()
        End If
        trvFiles.Nodes.Clear()
        '''''''''''''''        
        ''''''''''''''''''''''''
        getFiles()
        ''''''''''''''''''''''''
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
    End Sub

    Private Sub rdbLiquidData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbLiquidData.Click
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
        Panel5.Visible = False
        pnltrvCategories.Visible = False
        chkImportLiquidDt.Visible = False
        Panel3.Visible = False
        Label1.Text = "Liquid Data in Directory"
        lblTotalTemplates.Text = "Total Liquid Data=" & trvFiles.GetNodeCount(True)
        trvFiles.Nodes.Clear()
        ''''''''''''''''''''''''
        getFiles()
        ''''''''''''''''''''''''
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
    End Sub

    Private Sub rdbTemplates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTemplates.CheckedChanged
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
        If rdbTemplates.Checked = True Then
            Panel5.Visible = True
            pnltrvCategories.Visible = True
            chkImportLiquidDt.Visible = True
            Panel3.Visible = True
            Label1.Text = "Templates in Directory"
            lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
            ''''''''''''''''''''''''
            Call Fill_Categories()
            Call FillProviders()
        Else
            Panel5.Visible = False
            pnltrvCategories.Visible = False
            chkImportLiquidDt.Visible = False
            Panel3.Visible = False
            Label1.Text = "Liquid Data in Directory"
            lblTotalTemplates.Text = "Total Liquid Data=" & trvFiles.GetNodeCount(True)
        End If
        trvFiles.Nodes.Clear()
        ''''''''''''''''''''''''
        getFiles()
        ''''''''''''''''''''''''
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
    End Sub

    Private Sub rdbTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTemplates.Click
        ''''''''Added by Ujwala Atre as on 07262010 - to Import Liquid Data from XML 
        Panel5.Visible = True
        pnltrvCategories.Visible = True
        chkImportLiquidDt.Visible = True
        Panel3.Visible = True
        Label1.Text = "Templates in Directory"
        lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
        '''''''''''''''''''''''''
        Call Fill_Categories()
        Call FillProviders()
        trvFiles.Nodes.Clear()
        ''''''''''''''''''''''''
        getFiles()
        ''''''''''''''''''''''''
        ''''''''Integrated by Mayuri:20100730 - to Import Liquid Data from XML 
    End Sub
    Private Sub getFiles()
        ''''''''Added by Ujwala Atre as on 08052010 - to Import Liquid Data from XML 
        If txtDirectoryPath.Text.Trim() <> "" Then
            Dim objDirectory As New DirectoryInfo(txtDirectoryPath.Text)
            Dim objFile As FileInfo
            With trvFiles
                .BeginUpdate()
                .Nodes.Clear()
                'Dim nCount As Int16

                For Each objFile In objDirectory.GetFiles  'nCount = 0 To objFiles.GetUpperBound(0)

                    If rdbLiquidData.Checked = True Then
                        If objFile.Extension = ".XML" Then
                            Dim myNode As New TreeNode
                            myNode.Text = Replace(objFile.Name, objFile.Extension, "")
                            myNode.Tag = objFile.FullName
                            trvFiles.Nodes.Add(myNode) ')
                        End If
                        lblTotalTemplates.Text = "Total Liquid Data=" & trvFiles.GetNodeCount(True)
                    Else
                        If objFile.Extension = ".doc" Or objFile.Extension = ".docx" Then
                            Dim myNode As New TreeNode
                            myNode.Text = Replace(objFile.Name, objFile.Extension, "")
                            myNode.Tag = objFile.FullName
                            trvFiles.Nodes.Add(myNode) ')
                        End If
                        lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
                    End If

                Next
                .ExpandAll()
                .EndUpdate()
            End With
            objDirectory = Nothing
            objFile = Nothing
        End If

        ''''''''Added by Ujwala Atre as on 08052010 - to Import Liquid Data from XML 
    End Sub
    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub txtDirectoryPath_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtDirectoryPath.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Trim(txtDirectoryPath.Text) = "" Then
                    MessageBox.Show("Select the Import directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDirectoryPath.Focus()
                    Exit Sub
                End If
                If Directory.Exists(Trim(txtDirectoryPath.Text)) = False Then
                    MessageBox.Show("Select the valid Import directory path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDirectoryPath.Focus()
                    Exit Sub
                Else
                    getFiles()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class
