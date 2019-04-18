Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class frmImportVitalGraphData
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
                Try
                    If (IsNothing(OpenFileDialog1) = False) Then
                        OpenFileDialog1.Dispose()
                        OpenFileDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDirectoryPath As System.Windows.Forms.TextBox
    Friend WithEvents btnImportdata As System.Windows.Forms.Button
    Friend WithEvents btnImportdataWtHt As System.Windows.Forms.Button
    Friend WithEvents txtDirectoryPathWtHt As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseWtHt As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_ImportVitalGraphData As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ts_btn_Close As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportVitalGraphData))
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnImportdataWtHt = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtDirectoryPathWtHt = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnBrowseWtHt = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDirectoryPath = New System.Windows.Forms.TextBox
        Me.btnImportdata = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_ImportVitalGraphData = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_ImportVitalGraphData.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.Transparent
        Me.pnlBottom.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlBottom.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlBottom.Controls.Add(Me.lbl_pnlRight)
        Me.pnlBottom.Controls.Add(Me.lbl_pnlTop)
        Me.pnlBottom.Controls.Add(Me.ProgressBar1)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.ForeColor = System.Drawing.Color.Black
        Me.pnlBottom.Location = New System.Drawing.Point(0, 244)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlBottom.Size = New System.Drawing.Size(448, 28)
        Me.pnlBottom.TabIndex = 10
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 24)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(440, 1)
        Me.lbl_pnlBottom.TabIndex = 11
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlLeft.TabIndex = 10
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(444, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlRight.TabIndex = 9
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(442, 1)
        Me.lbl_pnlTop.TabIndex = 8
        Me.lbl_pnlTop.Text = "label1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 7)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(426, 14)
        Me.ProgressBar1.TabIndex = 7
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Controls.Add(Me.pnl_Base)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(448, 191)
        Me.pnlMain.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnImportdataWtHt)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtDirectoryPathWtHt)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.btnBrowseWtHt)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 94)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(448, 97)
        Me.Panel1.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(4, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(440, 1)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(15, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(298, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "File Path for Standard Vitals Height Vs Weight :"
        '
        'btnImportdataWtHt
        '
        Me.btnImportdataWtHt.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnImportdataWtHt.BackgroundImage = CType(resources.GetObject("btnImportdataWtHt.BackgroundImage"), System.Drawing.Image)
        Me.btnImportdataWtHt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnImportdataWtHt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnImportdataWtHt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnImportdataWtHt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportdataWtHt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportdataWtHt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnImportdataWtHt.Location = New System.Drawing.Point(139, 61)
        Me.btnImportdataWtHt.Name = "btnImportdataWtHt"
        Me.btnImportdataWtHt.Size = New System.Drawing.Size(90, 27)
        Me.btnImportdataWtHt.TabIndex = 7
        Me.btnImportdataWtHt.Text = "Import Data"
        Me.btnImportdataWtHt.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(3, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 90)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "label4"
        '
        'txtDirectoryPathWtHt
        '
        Me.txtDirectoryPathWtHt.BackColor = System.Drawing.Color.White
        Me.txtDirectoryPathWtHt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirectoryPathWtHt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtDirectoryPathWtHt.Location = New System.Drawing.Point(14, 33)
        Me.txtDirectoryPathWtHt.Name = "txtDirectoryPathWtHt"
        Me.txtDirectoryPathWtHt.ReadOnly = True
        Me.txtDirectoryPathWtHt.Size = New System.Drawing.Size(387, 22)
        Me.txtDirectoryPathWtHt.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(444, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 90)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "label3"
        '
        'btnBrowseWtHt
        '
        Me.btnBrowseWtHt.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnBrowseWtHt.BackgroundImage = CType(resources.GetObject("btnBrowseWtHt.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseWtHt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseWtHt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowseWtHt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowseWtHt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseWtHt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseWtHt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseWtHt.Image = CType(resources.GetObject("btnBrowseWtHt.Image"), System.Drawing.Image)
        Me.btnBrowseWtHt.Location = New System.Drawing.Point(407, 32)
        Me.btnBrowseWtHt.Name = "btnBrowseWtHt"
        Me.btnBrowseWtHt.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseWtHt.TabIndex = 10
        Me.btnBrowseWtHt.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(442, 1)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "label1"
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.Label2)
        Me.pnl_Base.Controls.Add(Me.btnBrowse)
        Me.pnl_Base.Controls.Add(Me.Label4)
        Me.pnl_Base.Controls.Add(Me.Label5)
        Me.pnl_Base.Controls.Add(Me.txtDirectoryPath)
        Me.pnl_Base.Controls.Add(Me.btnImportdata)
        Me.pnl_Base.Controls.Add(Me.Label6)
        Me.pnl_Base.Controls.Add(Me.Label3)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 0)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(448, 94)
        Me.pnl_Base.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(4, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(440, 1)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "label2"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnBrowse.BackgroundImage = CType(resources.GetObject("btnBrowse.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(407, 30)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 5
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 87)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(444, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 87)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "label3"
        '
        'txtDirectoryPath
        '
        Me.txtDirectoryPath.BackColor = System.Drawing.Color.White
        Me.txtDirectoryPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirectoryPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.txtDirectoryPath.Location = New System.Drawing.Point(14, 30)
        Me.txtDirectoryPath.Name = "txtDirectoryPath"
        Me.txtDirectoryPath.ReadOnly = True
        Me.txtDirectoryPath.Size = New System.Drawing.Size(387, 22)
        Me.txtDirectoryPath.TabIndex = 4
        '
        'btnImportdata
        '
        Me.btnImportdata.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnImportdata.BackgroundImage = CType(resources.GetObject("btnImportdata.BackgroundImage"), System.Drawing.Image)
        Me.btnImportdata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnImportdata.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnImportdata.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnImportdata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImportdata.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportdata.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnImportdata.Location = New System.Drawing.Point(139, 58)
        Me.btnImportdata.Name = "btnImportdata"
        Me.btnImportdata.Size = New System.Drawing.Size(90, 27)
        Me.btnImportdata.TabIndex = 1
        Me.btnImportdata.Text = "Import Data"
        Me.btnImportdata.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(442, 1)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "label1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(15, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(187, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "File Path for Standard Vitals :"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_ImportVitalGraphData)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(448, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_ImportVitalGraphData
        '
        Me.tlsp_ImportVitalGraphData.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_ImportVitalGraphData.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_ImportVitalGraphData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_ImportVitalGraphData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_ImportVitalGraphData.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_ImportVitalGraphData.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btn_Close})
        Me.tlsp_ImportVitalGraphData.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_ImportVitalGraphData.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_ImportVitalGraphData.Name = "tlsp_ImportVitalGraphData"
        Me.tlsp_ImportVitalGraphData.Size = New System.Drawing.Size(448, 53)
        Me.tlsp_ImportVitalGraphData.TabIndex = 0
        Me.tlsp_ImportVitalGraphData.Text = "toolStrip1"
        '
        'ts_btn_Close
        '
        Me.ts_btn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btn_Close.Image = CType(resources.GetObject("ts_btn_Close.Image"), System.Drawing.Image)
        Me.ts_btn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btn_Close.Name = "ts_btn_Close"
        Me.ts_btn_Close.Size = New System.Drawing.Size(43, 50)
        Me.ts_btn_Close.Tag = "Close"
        Me.ts_btn_Close.Text = "&Close"
        Me.ts_btn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmImportVitalGraphData
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(448, 272)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportVitalGraphData"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Graph Data"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_ImportVitalGraphData.ResumeLayout(False)
        Me.tlsp_ImportVitalGraphData.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            OpenFileDialog1.Filter = "*.csv|"
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.RestoreDirectory = True

            If OpenFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                Dim oFile As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                txtDirectoryPath.Text = oFile.Name
                txtDirectoryPath.Tag = oFile.FullName
                oFile = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to browse Import Folder Path due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnImportdata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportdata.Click
        If File.Exists(txtDirectoryPath.Tag) = True Then
            Dim filenameCheck As String
            Dim oFileInfo As FileInfo = New FileInfo(txtDirectoryPath.Text)
            filenameCheck = Replace(oFileInfo.Name, oFileInfo.Extension, "")

            If filenameCheck.ToUpper = UCase("StandardVitals") Then
                Import_TXT(",", txtDirectoryPath.Tag)
                btnImportdata.Enabled = False
            Else
                MessageBox.Show("Please select valid standard vital file for import the data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Please select the text file for import the data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function Import_TXT(ByVal Delimiter As String, ByVal strFileName As String)
        If FileInUse(strFileName) = True Then
            MessageBox.Show("File is being used by another process, please close the file", "gloIEWizard", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            'Dim oFile As FileStream = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            'Dim oReader As StreamReader = New StreamReader(oFile)
            'Dim objcon As New SqlConnection
            'Dim objCmd As New SqlCommand
            'objcon.ConnectionString = strConnection
            'objCmd.Connection = objcon
            'objcon.Open()
            Dim oFile As FileStream = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            Dim oReader As StreamReader = New StreamReader(oFile)
            Dim objcon As New SqlConnection
            Dim objCmd As New SqlCommand
            objcon.ConnectionString = GetConnectionString()

            Dim nProgressMax As Integer = 100 '// Temporary Vinayak 15 Feb 2007
            Dim nCounter As Int16 = 1

            '   Dim objSQLDataReader As SqlDataReader
            objCmd.Connection = objcon
            objcon.Open()

            Dim strQuery As String = ""
            Dim strInsertSQL As String = ""
            Dim strSQL As String = ""
            Dim strUpdateSQL As String = ""

            Dim strTableName As String = Path.GetFileName(strFileName)
            strTableName = strTableName.Substring(0, strTableName.Length - 4)
            Dim strColumns As String = ""
            Dim strDataType As String = ""
            strQuery = "select column_name,data_type from information_schema.columns where table_name='" & strTableName & "'"
            objCmd.CommandText = strQuery
            Dim drColumns As SqlDataReader
            drColumns = objCmd.ExecuteReader
            ''''Retreiving the column names of the table
            If Not (drColumns Is Nothing) Then
                If drColumns.HasRows = True Then
                    While drColumns.Read
                        strColumns &= drColumns(0) & ","
                        strDataType &= drColumns(1) & ","
                    End While
                    strColumns = strColumns.Substring(0, strColumns.Length - 1)
                    strDataType = strDataType.Substring(0, strDataType.Length - 1)
                End If
            End If
            drColumns.Close()
            drColumns = Nothing

            objCmd.Cancel()
            strQuery = ""

            Dim strTXT As String = ""

            Try
                '''' Reading to end of the file line by line

                If strTableName.Trim.ToUpper = "STANDARDVITALS" Then
                    strInsertSQL = "truncate table standardvitals " ' & strTableName
                    nProgressMax = 1070
                ElseIf strTableName.Trim.ToUpper = "STANDARDVITALSWTHT" Then
                    strInsertSQL = "truncate table standardvitalsWtHt " ' & strTableName
                    nProgressMax = 120
                End If

                objCmd.CommandText = strInsertSQL
                objCmd.ExecuteNonQuery()


                If strColumns <> "" Then

                    ProgressBar1.Minimum = 1
                    ProgressBar1.Maximum = nProgressMax

                    Do While oReader.Peek() <> -1

                        nCounter = nCounter + 1
                        If nCounter <= nProgressMax Then
                            Application.DoEvents()
                            ProgressBar1.Value = nCounter
                        End If


                        strTXT = oReader.ReadLine()
                        Dim strRecords = Split(strTXT, ",")
                        '''' Get the no of fields in the specified line in an array
                        strInsertSQL = "Insert into " & strTableName & "(" & strColumns & ") values('"
                        Dim strColNames = Split(strColumns, ",")
                        Dim strDatatypeNames = Split(strDataType, ",")
                        'strUpdateSQL = "update " & strTableName & " set "
                        'For noCol As Int16 = 0 To UBound(strColNames) - 2
                        '    strUpdateSQL &= strColNames(noCol) & "='"

                        '    ''''For replicated data tables we are not inserting the data
                        '    'For j As Int16 = 0 To UBound(strRecords) - 2

                        '    strUpdateSQL &= Trim(strRecords(noCol)) & "',"
                        '    ' Next
                        'Next

                        For j As Int16 = 0 To UBound(strRecords) - 1
                            If strDatatypeNames(j) = "numeric" Or strDatatypeNames(j) = "int" Or strDatatypeNames(j) = "decimal" Or strDatatypeNames(j) = "float" Then
                                If Trim(strRecords(j)) <> "" Then
                                    strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                    strInsertSQL &= Trim(strRecords(j)) & ",'"
                                Else
                                    strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                    strInsertSQL &= "-1" & ",'"
                                End If
                            ElseIf strDatatypeNames(j) = "bit" Then
                                If Trim(strRecords(j)) <> "True" Then
                                    strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                    strInsertSQL &= "1" & ",'"
                                Else
                                    strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                    strInsertSQL &= "0" & ",'"
                                End If
                            Else
                                strInsertSQL &= Trim(strRecords(j)) & "','"
                            End If

                        Next

                        '''' check for existence of the record 
                        strQuery = "Select count(" & strColNames(0) & ") from " & strTableName & " where " & strColNames(0) & " = " & Trim(strRecords(0))
                        objCmd.CommandText = strQuery

                        ' If objCmd.ExecuteScalar = 0 Then
                        'objCmd.Cancel()
                        If strDatatypeNames(UBound(strRecords)) = "numeric" Or strDatatypeNames(UBound(strRecords) - 1) = "int" Or strDatatypeNames(UBound(strRecords) - 1) = "decimal" Or strDatatypeNames(UBound(strRecords) - 1) = "float" Then
                            If Trim(strRecords(UBound(strRecords))) <> "" Then
                                strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                strInsertSQL &= Trim(strRecords(UBound(strRecords))) & ")"
                            Else
                                strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                strInsertSQL &= "-1" & ")"
                            End If
                        ElseIf strDatatypeNames(UBound(strRecords) - 1) = "bit" Then
                            If Trim(strRecords(UBound(strRecords) - 1)) <> "True" Then
                                strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                strInsertSQL &= "1" & ",'"
                            Else
                                strInsertSQL = strInsertSQL.Substring(0, strInsertSQL.Length - 1)
                                strInsertSQL &= "0" & ",'"
                            End If
                        Else
                            strInsertSQL &= Trim(strRecords(UBound(strRecords) - 1)) & "')"
                        End If


                        ''''  MsgBox(strInsertSQL)

                        objCmd.CommandText = strInsertSQL
                        objCmd.ExecuteNonQuery()
                        objCmd.Cancel()

                        'Else

                        '    strUpdateSQL &= strColNames((UBound(strColNames) - 1)) & "='" & Trim(strRecords(UBound(strRecords) - 1)) & "' where " & strColNames(0) & "=" & Trim(strRecords(0))

                        '    ''''  MsgBox(strUpdateSQL)

                        '    objCmd.CommandText = strUpdateSQL
                        '    objCmd.ExecuteNonQuery()
                        '    objCmd.Cancel()
                        '    strUpdateSQL = Nothing
                        'End If

                    Loop
                    MessageBox.Show("Succesfully Imported " & strTableName & " from text file to database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            Catch err As Exception
                MessageBox.Show(err.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            oReader.Close()
            oReader.Dispose()
            oReader = Nothing
            oFile.Close()
            oFile.Dispose()
            oFile = Nothing
            objcon.Close()
            objcon.Dispose()
            objcon = Nothing


            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End If
        Return Nothing
    End Function

    Private Function FileInUse(ByVal FN As String) As Boolean
        Dim fs As IO.FileStream = Nothing
        Try
            fs = IO.File.Open(FN, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None)
            Return False
        Catch ex As IO.IOException
            Return True
        Finally
            If Not fs Is Nothing Then
                fs.Close()
                fs.Dispose()
                fs = Nothing
            End If
        End Try
    End Function

    Private Sub btnBrowseWtHt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseWtHt.Click
        Try
            'With OpenFileDialog1
            '    .Description = "Select Directory from which Templates to Import"
            '    .ShowNewFolderButton = False
            'End With
            OpenFileDialog1.Filter = "*.csv|"
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.RestoreDirectory = True

            If OpenFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                Dim oFile As FileInfo = New FileInfo(OpenFileDialog1.FileName)
                txtDirectoryPathWtHt.Text = oFile.Name
                txtDirectoryPathWtHt.Tag = oFile.FullName
                oFile = Nothing
                'Dim objDirectory As New DirectoryInfo(txtDirectoryPath.Text)
                'Dim objFiles() As FileInfo
                'objFiles = objDirectory.GetFiles("*.csv")

                'With trvFiles
                '    .BeginUpdate()
                '    .Nodes.Clear()
                '    Dim nCount As Int16
                '    For nCount = 0 To objFiles.GetUpperBound(0)
                '        trvFiles.Nodes.Add(Replace(objFiles(nCount).Name, objFiles(nCount).Extension, ""))
                '    Next
                '    ' lblTotalTemplates.Text = "Total Templates=" & trvFiles.GetNodeCount(True)
                '    .ExpandAll()
                '    .EndUpdate()
                'End With
                'objDirectory = Nothing
                'objFiles = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnImportdataWtHt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportdataWtHt.Click
        If File.Exists(txtDirectoryPathWtHt.Tag) = True Then
            Dim filenameCheck As String
            Dim oFileInfo As FileInfo = New FileInfo(txtDirectoryPathWtHt.Text)
            filenameCheck = Replace(oFileInfo.Name, oFileInfo.Extension, "")

            If filenameCheck.ToUpper = UCase("StandardVitalsWtHt") Then
                'Import_TXT(",", txtDirectoryPath.Text)
                Import_TXT(",", txtDirectoryPathWtHt.Tag)
                btnImportdataWtHt.Enabled = False
            Else
                MessageBox.Show("Please select valid standard vital file for of height and Weight to import the data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Please select the text file for import the data.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub tlsp_ImportVitalGraphData_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_ImportVitalGraphData.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                Me.Close()

        End Select
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportdata.MouseHover, btnBrowse.MouseHover, btnBrowseWtHt.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportdata.MouseLeave, btnBrowse.MouseLeave, btnBrowseWtHt.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub
End Class
