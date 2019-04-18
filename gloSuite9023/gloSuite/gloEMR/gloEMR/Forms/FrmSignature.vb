Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Security.Permissions
Imports System.Diagnostics


Public Class FrmSignature
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
                    If (IsNothing(dlgOpenFile) = False) Then
                        dlgOpenFile.Dispose()
                        dlgOpenFile = Nothing
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
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents picSignature As System.Windows.Forms.PictureBox
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents grpBrowse As System.Windows.Forms.GroupBox
    Friend WithEvents txtImagePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_Signature As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnCapture As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnReconnect As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SigPlusNET1 As Topaz.SigPlusNET
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ts_btnInsert As System.Windows.Forms.ToolStripButton
    Friend WithEvents AxSigPlus1 As AxSIGPLUSLib.AxSigPlus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private m_intOpenedFrom As Int16
    Friend WithEvents lblWaitMessage As System.Windows.Forms.Label


    Dim objstate As System.Windows.Forms.AxHost.State

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSignature))
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.grpBrowse = New System.Windows.Forms.GroupBox()
        Me.txtImagePath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.picSignature = New System.Windows.Forms.PictureBox()
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp_Signature = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnCapture = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnInsert = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnReconnect = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AxSigPlus1 = New AxSIGPLUSLib.AxSigPlus()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SigPlusNET1 = New Topaz.SigPlusNET()
        Me.lblWaitMessage = New System.Windows.Forms.Label()
        Me.pnlBottom.SuspendLayout()
        Me.grpBrowse.SuspendLayout()
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_Signature.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.AxSigPlus1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.Transparent
        Me.pnlBottom.Controls.Add(Me.grpBrowse)
        Me.pnlBottom.Controls.Add(Me.btnStart)
        Me.pnlBottom.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlBottom.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlBottom.Controls.Add(Me.lbl_RightBrd)
        Me.pnlBottom.Controls.Add(Me.lbl_TopBrd)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 172)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlBottom.Size = New System.Drawing.Size(397, 74)
        Me.pnlBottom.TabIndex = 0
        '
        'grpBrowse
        '
        Me.grpBrowse.Controls.Add(Me.txtImagePath)
        Me.grpBrowse.Controls.Add(Me.Label1)
        Me.grpBrowse.Controls.Add(Me.btnBrowse)
        Me.grpBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBrowse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpBrowse.Location = New System.Drawing.Point(8, 8)
        Me.grpBrowse.Name = "grpBrowse"
        Me.grpBrowse.Size = New System.Drawing.Size(375, 56)
        Me.grpBrowse.TabIndex = 12
        Me.grpBrowse.TabStop = False
        Me.grpBrowse.Text = " Browse from File "
        '
        'txtImagePath
        '
        Me.txtImagePath.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImagePath.Location = New System.Drawing.Point(79, 21)
        Me.txtImagePath.Name = "txtImagePath"
        Me.txtImagePath.Size = New System.Drawing.Size(257, 22)
        Me.txtImagePath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "File Name :"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(342, 20)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowse.TabIndex = 0
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(53, 10)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 24)
        Me.btnStart.TabIndex = 8
        Me.btnStart.Text = "&Start"
        Me.btnStart.Visible = False
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 70)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(389, 1)
        Me.lbl_BottomBrd.TabIndex = 16
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 69)
        Me.lbl_LeftBrd.TabIndex = 15
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(393, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 69)
        Me.lbl_RightBrd.TabIndex = 14
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(391, 1)
        Me.lbl_TopBrd.TabIndex = 13
        Me.lbl_TopBrd.Text = "label1"
        '
        'picSignature
        '
        Me.picSignature.Location = New System.Drawing.Point(11, 10)
        Me.picSignature.Name = "picSignature"
        Me.picSignature.Size = New System.Drawing.Size(375, 96)
        Me.picSignature.TabIndex = 11
        Me.picSignature.TabStop = False
        Me.picSignature.Visible = False
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_Signature)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(397, 53)
        Me.pnl_tlsp.TabIndex = 1
        '
        'tlsp_Signature
        '
        Me.tlsp_Signature.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Signature.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_Signature.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Signature.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Signature.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Signature.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnCapture, Me.ts_btnInsert, Me.ts_btnSave, Me.ts_btnClose, Me.ts_btnReconnect})
        Me.tlsp_Signature.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Signature.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Signature.Name = "tlsp_Signature"
        Me.tlsp_Signature.Size = New System.Drawing.Size(397, 53)
        Me.tlsp_Signature.TabIndex = 0
        Me.tlsp_Signature.TabStop = True
        Me.tlsp_Signature.Text = "toolStrip1"
        '
        'ts_btnCapture
        '
        Me.ts_btnCapture.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCapture.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnCapture.Image = CType(resources.GetObject("ts_btnCapture.Image"), System.Drawing.Image)
        Me.ts_btnCapture.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCapture.Name = "ts_btnCapture"
        Me.ts_btnCapture.Size = New System.Drawing.Size(60, 50)
        Me.ts_btnCapture.Tag = "Capture"
        Me.ts_btnCapture.Text = "C&apture"
        Me.ts_btnCapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnInsert
        '
        Me.ts_btnInsert.Image = CType(resources.GetObject("ts_btnInsert.Image"), System.Drawing.Image)
        Me.ts_btnInsert.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnInsert.Name = "ts_btnInsert"
        Me.ts_btnInsert.Size = New System.Drawing.Size(48, 50)
        Me.ts_btnInsert.Tag = "Insert"
        Me.ts_btnInsert.Text = "&Insert"
        Me.ts_btnInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnInsert.ToolTipText = "Insert Signature"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnSave.Tag = "Clear"
        Me.ts_btnSave.Text = "Cl&ear"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnReconnect
        '
        Me.ts_btnReconnect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnReconnect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnReconnect.Image = CType(resources.GetObject("ts_btnReconnect.Image"), System.Drawing.Image)
        Me.ts_btnReconnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnReconnect.Name = "ts_btnReconnect"
        Me.ts_btnReconnect.Size = New System.Drawing.Size(76, 50)
        Me.ts_btnReconnect.Tag = "Reconnect"
        Me.ts_btnReconnect.Text = "&Reconnect"
        Me.ts_btnReconnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.AxSigPlus1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.picSignature)
        Me.Panel1.Controls.Add(Me.SigPlusNET1)
        Me.Panel1.Controls.Add(Me.lblWaitMessage)
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(397, 117)
        Me.Panel1.TabIndex = 18
        '
        'AxSigPlus1
        '
        Me.AxSigPlus1.Enabled = True
        Me.AxSigPlus1.Location = New System.Drawing.Point(11, 10)
        Me.AxSigPlus1.Name = "AxSigPlus1"
        Me.AxSigPlus1.OcxState = CType(resources.GetObject("AxSigPlus1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxSigPlus1.Size = New System.Drawing.Size(375, 96)
        Me.AxSigPlus1.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(389, 1)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 110)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(393, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 110)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(391, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label1"
        '
        'SigPlusNET1
        '
        Me.SigPlusNET1.BackColor = System.Drawing.Color.White
        Me.SigPlusNET1.ForeColor = System.Drawing.Color.Black
        Me.SigPlusNET1.Location = New System.Drawing.Point(11, 11)
        Me.SigPlusNET1.Name = "SigPlusNET1"
        Me.SigPlusNET1.Size = New System.Drawing.Size(375, 96)
        Me.SigPlusNET1.TabIndex = 18
        '
        'lblWaitMessage
        '
        Me.lblWaitMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWaitMessage.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWaitMessage.Location = New System.Drawing.Point(3, 3)
        Me.lblWaitMessage.Name = "lblWaitMessage"
        Me.lblWaitMessage.Size = New System.Drawing.Size(391, 111)
        Me.lblWaitMessage.TabIndex = 19
        Me.lblWaitMessage.Text = "Inserting Signature. Please Wait..."
        Me.lblWaitMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblWaitMessage.Visible = False
        '
        'FrmSignature
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(397, 246)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "FrmSignature"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Signature"
        Me.pnlBottom.ResumeLayout(False)
        Me.grpBrowse.ResumeLayout(False)
        Me.grpBrowse.PerformLayout()
        CType(Me.picSignature, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_Signature.ResumeLayout(False)
        Me.tlsp_Signature.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.AxSigPlus1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _ImagePath As String = ""
    Private _IsSignatureOnAddendum As Boolean = False
    Public Event On_InsertSignature()
    Private strSignaturePath As String = "\\tsclient\Y\ForSignature\"
    Private intIdleCount As Integer
    Private blnShowFullSignaturePad As Boolean = False

    Public ReadOnly Property ImagePath() As String
        Get
            Return _ImagePath
        End Get
    End Property

    Public Property IsSignatureOnAddendum() As Boolean
        Get
            Return _IsSignatureOnAddendum
        End Get
        Set(ByVal value As Boolean)
            _IsSignatureOnAddendum = value
        End Set
    End Property

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If AxSigPlus1.TabletConnectQuery() Then
            AxSigPlus1.TabletState = 1
        End If
        picSignature.SendToBack()
    End Sub

    ''Dhruv 20091214 To insert the signature
    Private Sub InsertSignature()
        Try
            If System.IO.File.Exists(txtImagePath.Text.Trim) Then
                _ImagePath = txtImagePath.Text.Trim
                If (_IsSignatureOnAddendum = False) Then
                    If Me.Owner IsNot Nothing Then
                        CType(Me.Owner, ISignature).AddSignature(txtImagePath.Text.Trim)
                    End If
                End If
                Me.Close()
                RaiseEvent On_InsertSignature()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
        End Try
    End Sub

    Private Sub CaptureSignature()

        Try

            If AxSigPlus1.TabletConnectQuery() Then
                If AxSigPlus1.GetNumberOfStrokes() > 0 Then
                    AxSigPlus1.TabletState = 0 'allows JustifyMode to be set
                    AxSigPlus1.ImageXSize = 100 'sets image width in pixels
                    AxSigPlus1.ImageYSize = 50 'sets image height in pixels
                    AxSigPlus1.ImagePenWidth = 11 'sets width of pen stroke in pixels
                    AxSigPlus1.ImageFileFormat = 5
                    AxSigPlus1.JustifyMode = 6 '+expands signature to fit all of sig window
                    Dim Imagefile As String
                    Imagefile = SignatureNewImageName
                    AxSigPlus1.WriteImageFile(Imagefile)
                    _ImagePath = Imagefile
                    If _IsSignatureOnAddendum = False Then
                        CType(Me.Owner, ISignature).ImageFilePath = Imagefile
                        CType(Me.Owner, ISignature).AddSignature(Imagefile)
                    Else
                        RaiseEvent On_InsertSignature()
                    End If

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Singnature Inserted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    AxSigPlus1.Refresh()
                    AxSigPlus1.TabletState = 0
                    ts_btnCapture.Enabled = False
                    Me.Close()
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to insert Signature at the given location.", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub CaptureSignatureNew()
        Try
            If SigPlusNET1.TabletConnectQuery() Then
                If SigPlusNET1.GetNumberOfStrokes() > 0 Then
                    SigPlusNET1.SetTabletState(0)
                    SigPlusNET1.SetImageXSize(100)
                    SigPlusNET1.SetImageYSize(50)
                    SigPlusNET1.SetImagePenWidth(4)
                    SigPlusNET1.SetImageFileFormat(5)
                    SigPlusNET1.SetJustifyMode(6) '+expands signature to fit all of sig window

                    Dim Imagefile As String
                    Imagefile = SignatureNewImageName

                    Dim myimage As Image
                    myimage = SigPlusNET1.GetSigImage()
                    myimage.Save(Imagefile, System.Drawing.Imaging.ImageFormat.Tiff)
                    If _IsSignatureOnAddendum = False Then
                        CType(Me.Owner, ISignature).ImageFilePath = Imagefile
                        CType(Me.Owner, ISignature).AddSignature(Imagefile)
                    Else
                        RaiseEvent On_InsertSignature()
                    End If

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Singnature Inserted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    SigPlusNET1.Refresh()
                    SigPlusNET1.SetTabletState(0)
                    ts_btnCapture.Enabled = False
                    Me.Close()

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Dim watcher As New FileSystemWatcher()
    Dim Timer As Windows.Forms.Timer


    Private Sub HideScreen()
        Me.Text = "Waiting for Signature Capture..."
        tlsp_Signature.Visible = False
        Me.Height = 20
        Me.Opacity = 50
    End Sub


    Private Sub ShowScreen()
        Me.Text = "Signature"
        tlsp_Signature.Visible = True
        Me.Height = 275
        Me.Opacity = 100
    End Sub

    Private Sub TimerMethod()

        ts_btnCapture.Enabled = False
        ts_btnReconnect.Enabled = False

        HideScreen()

        Try

            Try

                If File.Exists(strSignaturePath & "SignatureCreated.txt") = True Then
                    File.Delete(strSignaturePath & "SignatureCreated.txt")
                End If

                If File.Exists(strSignaturePath & "StartSignature.txt") = True Then
                    File.Delete(strSignaturePath & "StartSignature.txt")
                End If
                Try
                    Dim myFile As StreamWriter = Nothing
                    myFile = File.CreateText(strSignaturePath & "StartSignature.txt")
                    Try
                        myFile.Close()
                    Catch

                    End Try
                    myFile.Dispose()
                Catch

                End Try
               

                Me.SetDesktopLocation(10, 10)

            Catch ex As Exception
                ShowScreen()
                MsgBox("Unable to find the mapped drive. Cannot use local Signature Pad.", MsgBoxStyle.Critical)
                Exit Sub
            End Try

            Timer = New Windows.Forms.Timer
            AddHandler Timer.Tick, AddressOf TimerTick
            intIdleCount = 0

            Timer.Interval = 500
            Timer.Enabled = True
            Timer.Start()

        Catch ex As Exception
            ShowScreen()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub TimerTick(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            GetSignatureFromClipboard()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub GetSignatureFromClipboard()

        Try

            'Method 2
            If File.Exists(strSignaturePath & "SignatureCreated.txt") = True Then

                Dim imgMain As Image = Nothing
                Dim sTempImportPath As String = gloSettings.FolderSettings.AppTempFolderPath & "SignatureCreated.txt"

                Timer.Enabled = False
                lblWaitMessage.BringToFront()
                lblWaitMessage.Visible = True
                Application.DoEvents()


                File.Delete(strSignaturePath & "SignatureCreated.txt")
                Try
                    Dim myFile As FileStream = Nothing
                    myFile = File.Create(sTempImportPath)
                    Try
                        myFile.Close()
                    Catch

                    End Try
                    myFile.Dispose()
                Catch

                End Try
               


                CType(Me.Owner, ISignature).AddSignature(sTempImportPath)

                File.Delete(sTempImportPath)

                Me.Close()

            Else
                intIdleCount = intIdleCount + 1
                'If there is no signature captures in 2 minutes, then ask the user if he wants to quit the application or not.
                If intIdleCount >= 240 Then '
                    Timer.Enabled = False
                    'Timer.Stop()
                    If (MessageBox.Show(Me, "No signature captured from 'Signature Pad' yet. Would you like to continue waiting?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = MsgBoxResult.No Then
                        Me.Close()
                    Else
                        Timer.Enabled = True
                        'Timer.Start()
                        intIdleCount = 0
                    End If
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub OnRenamed(source As Object, e As RenamedEventArgs)
        ' Specify what is done when a file is renamed.
        Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath)
    End Sub

    Private Sub FrmSignature_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed


        Try

            Global.gloWord.gloWord.SetClipboardData()

            RemoveHandler Me.Resize, AddressOf FrmSignature_Resize

            If gblnLocalSignaturePad = True Then
                lblWaitMessage.SendToBack()
                lblWaitMessage.Visible = False
            End If

            If IsNothing(Timer) = False Then
                Timer.Enabled = False
                Timer.Dispose()
                Timer = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FrmSignature_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            If gblnLocalSignaturePad = True Then
                Try
                    Global.gloWord.gloWord.GetClipboardData()
                Catch ex1 As Exception
                End Try

                TimerMethod()
                AddHandler Me.Resize, AddressOf FrmSignature_Resize
            Else

                If mdlGeneral.gblbUseNewSignaturePad = False Then
                    SigPlusNET1.Visible = False
                    If AxSigPlus1.Visible = False Then
                        AxSigPlus1.Visible = True
                    End If
                    Me.Cursor = Cursors.WaitCursor
                    ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server
                    If gblnSigPlusSupportTS And gloSettings.gloRegistrySetting.IsServerOS Then
                        If gblnIsSigPlusSettingsAvailable And (Not String.IsNullOrEmpty(gstrSigPlusTabletPortPath)) Then
                            AxSigPlus1.SetTabletPortPath(gstrSigPlusTabletPortPath)
                        Else
                            MessageBox.Show("Please check the SigPlus Settings (Tools > Settings > Word Settings)", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If

                    Dim attempts As Integer = 0
                    Dim connected As Boolean = False


                    While attempts < 15
                        AxSigPlus1.TabletType = gshortSigPlusTabletType
                        '----------------
                        If AxSigPlus1.TabletConnectQuery() Then
                            AxSigPlus1.DisplayPenWidth = 11
                            AxSigPlus1.ClearTablet()
                            AxSigPlus1.Refresh()
                            AxSigPlus1.TabletState = 1
                            connected = True
                            Exit While
                        Else
                            attempts += 1
                            System.Threading.Thread.Sleep(3000)
                        End If
                    End While
                    If Not connected Then
                        MessageBox.Show("Unable to connect with signature pad.", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    AxSigPlus1.Visible = False
                    If SigPlusNET1.Visible = False Then
                        SigPlusNET1.Visible = True
                    End If
                    Dim isconnected As Boolean = False
                    If SigPlusNET1.TabletConnectQuery() Then
                        isconnected = True
                    End If
                    If isconnected Then
                        SigPlusNET1.SetTabletState(1)
                        'MessageBox.Show("New signature pad Connected successfully,COM port " + Convert.ToString(SigPlusNET1.GetTabletComPort()) + "", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Unable to open port " + Convert.ToString(SigPlusNET1.GetTabletComPort()) + ". Either it does not exist, or is in use by another program", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End If

                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub ClearSignatureNew()
        Try
            ts_btnCapture.Enabled = True

            If SigPlusNET1.TabletConnectQuery() Then
                SigPlusNET1.ClearTablet()
                SigPlusNET1.Refresh()
                SigPlusNET1.SetTabletState(1)
            End If
            AxSigPlus1.Visible = False
            SigPlusNET1.Visible = True

            picSignature.Image = Nothing
            picSignature.Refresh()
            picSignature.Visible = False
            txtImagePath.Text = ""

            If (_IsSignatureOnAddendum = True) Then
                _ImagePath = ""
            Else
                If Not IsNothing(Me.Owner) Then
                    CType(Me.Owner, ISignature).ImageFilePath = ""
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearSignature()
        Try


            If gblnLocalSignaturePad = False Then
                ts_btnCapture.Enabled = True
                If AxSigPlus1.TabletConnectQuery() Then
                    AxSigPlus1.ClearTablet()
                    AxSigPlus1.Refresh()
                    AxSigPlus1.TabletState = 1
                End If
            End If

            '18-Mar-15 Aniket: Resolving Bug #80348: gloEMR: Capsign- Application does not display white screen after click on clear button
            SigPlusNET1.Visible = False
            AxSigPlus1.Visible = True

            picSignature.Image = Nothing
            picSignature.Refresh()
            picSignature.Visible = False
            txtImagePath.Text = ""

            ''If called from addendum control
            If (_IsSignatureOnAddendum = True) Then
                _ImagePath = ""
            Else
                If Not IsNothing(Me.Owner) Then
                    CType(Me.Owner, ISignature).ImageFilePath = ""
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Delete, ex, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to clear signature.", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CloseSignature()
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtImagePath_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImagePath.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            picSignature.SizeMode = PictureBoxSizeMode.StretchImage
            With dlgOpenFile
                .Title = "Select Clinic Logo"
                .Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif"
                .CheckFileExists = True
                .Multiselect = False
                .ShowHelp = False
                .ShowReadOnly = False
            End With
            If dlgOpenFile.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                picSignature.Visible = True
                AxSigPlus1.Visible = False
                SigPlusNET1.Visible = False
                picSignature.Image = Image.FromFile(dlgOpenFile.FileName)
                If (_IsSignatureOnAddendum = False) Then
                    If Me.Owner IsNot Nothing Then
                        CType(Me.Owner, ISignature).ImageFilePath = dlgOpenFile.FileName
                    End If
                End If
                _ImagePath = dlgOpenFile.FileName
                txtImagePath.Text = dlgOpenFile.FileName
                ts_btnCapture.Enabled = False
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.SignatureCreated, "Singnature Inserted", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Singnature Inserted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.Add, "Singnature Inserted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_Signature_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_Signature.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Capture"
                    If mdlGeneral.gblbUseNewSignaturePad Then
                        CaptureSignatureNew()
                    Else
                        CaptureSignature()
                    End If

                Case "Clear"
                    If mdlGeneral.gblbUseNewSignaturePad Then
                        ClearSignatureNew()
                    Else
                        ClearSignature()
                    End If
                Case "Close"
                    CloseSignature()
                    ''dhruv 20091214
                Case "Insert"
                    InsertSignature()
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    '<<<<<<<<<<<<<<<<<<<<<<<<<Ojeswini06032009>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    'Button Mouse and Leave images

    Private Sub btnBrowse_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseHover
        btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        btnBrowse.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnBrowse_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.MouseLeave
        btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnBrowse.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    'added by manoj jadhav on 20141203 Version 8033 disposing SigPlus OCX object at Form closing Event
    Private Sub FrmSignature_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not IsNothing(AxSigPlus1) Then
                AxSigPlus1.Dispose()
                AxSigPlus1 = Nothing
            End If
            If Not IsNothing(SigPlusNET1) Then
                SigPlusNET1.Dispose()
                SigPlusNET1 = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub ts_btnReconnect_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnReconnect.Click
        If mdlGeneral.gblbUseNewSignaturePad = False Then
            Try
                Dim attempts As Integer = 0
                Dim connected As Boolean = False
                While attempts < 15
                    ts_btnReconnect.Enabled = False
                    ts_btnCapture.Enabled = False
                    ts_btnInsert.Enabled = False
                    ts_btnSave.Enabled = False
                    Application.DoEvents()
                    AxSigPlus1.TabletState = 0
                    AxSigPlus1.Refresh()

                    If AxSigPlus1.TabletConnectQuery() Then
                        AxSigPlus1.DisplayPenWidth = 11
                        AxSigPlus1.ClearTablet()
                        AxSigPlus1.Refresh()
                        AxSigPlus1.TabletState = 1
                        connected = True
                        Exit While
                    Else
                        attempts += 1
                        System.Threading.Thread.Sleep(3000)
                    End If
                End While
                If Not connected Then
                    MessageBox.Show("Unable to connect with signature pad.", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                'MessageBox.Show(ex.Message.ToString())            
            Finally
                ts_btnReconnect.Enabled = True
                ts_btnCapture.Enabled = True
                ts_btnInsert.Enabled = True
                ts_btnSave.Enabled = True
                Application.DoEvents()
            End Try
        Else
            Try
                ts_btnReconnect.Enabled = False
                ts_btnCapture.Enabled = False
                ts_btnInsert.Enabled = False
                ts_btnSave.Enabled = False
                Application.DoEvents()
                SigPlusNET1.SetTabletState(0)
                SigPlusNET1.Refresh()

                Dim isconnected As Boolean = False
                If SigPlusNET1.TabletConnectQuery() Then
                    isconnected = True
                End If
                If isconnected Then
                    SigPlusNET1.ClearTablet()
                    SigPlusNET1.Refresh()
                    SigPlusNET1.SetTabletState(1)
                    'MessageBox.Show("New signature pad Connected successfully,COM port " + Convert.ToString(SigPlusNET1.GetTabletComPort()) + "", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Unable to open port " + Convert.ToString(SigPlusNET1.GetTabletComPort()) + ". Either it does not exist, or is in use by another program", "Signature", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End If
            Catch ex As Exception
                'MessageBox.Show(ex.Message.ToString())   
            Finally
                ts_btnReconnect.Enabled = True
                ts_btnCapture.Enabled = True
                ts_btnInsert.Enabled = True
                ts_btnSave.Enabled = True
                Application.DoEvents()
            End Try
        End If




    End Sub

    Private Sub FrmSignature_Resize(sender As Object, e As System.EventArgs)

        If blnShowFullSignaturePad = False Then
            blnShowFullSignaturePad = True
            If gblnLocalSignaturePad = True Then


                RemoveHandler Me.Resize, AddressOf FrmSignature_Resize

                ShowScreen()
                Me.WindowState = FormWindowState.Normal
                Me.MaximizeBox = False
            End If

        End If

    End Sub

End Class
