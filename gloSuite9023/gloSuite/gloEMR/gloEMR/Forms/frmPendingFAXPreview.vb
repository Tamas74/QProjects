Imports System.Data
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO
'Code Added on 20091015
Imports pdftron.PDF
'End code Added on 20091015
Imports pdftron.Common
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMR.gloEMRWord
Imports gloEDocumentV3
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Public Class frmPendingFAXPreview
    Inherits System.Windows.Forms.Form
    Implements IPatientContext


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Code Added on 20091015
        ConnectToPDFTron()
        ImagXpress1.Licensing.UnlockRuntime(1908208815, 373700144, 1341181380, 19197)

        'End Code Added on 20091015
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

                If (IsNothing(dgAttemptDetails) = False) Then
                    dgAttemptDetails.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgAttemptDetails)
                    dgAttemptDetails.Dispose()
                    dgAttemptDetails = Nothing
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
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblFAXTo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFAXNo As System.Windows.Forms.Label
    Friend WithEvents lblFAXType As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblFAXDate As System.Windows.Forms.Label
    Friend WithEvents lblFAXBy As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNoOfAttempt As System.Windows.Forms.Label
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    'Removed PegausImageXpress7 -> Dhruv
    'Friend WithEvents picPreview As PegasusImaging.WinForms.ImagXpress7.PICImagXpress
    Friend WithEvents pnlPreviewCommand As System.Windows.Forms.Panel
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents lblPreviewStatus As System.Windows.Forms.Label
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuFirst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPrevious As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNext As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLast As System.Windows.Forms.MenuItem
    Friend WithEvents pnlAttemptsDetailsBody As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dgAttemptDetails As clsDataGrid
    Friend WithEvents pnlAttemptsDetailsHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlFaxAttemptDetailsBody As System.Windows.Forms.Panel
    Friend WithEvents pnlFaxAttemptDetailsHeader As System.Windows.Forms.Panel
    Friend WithEvents tblBtn As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtnNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tblbtnClose As System.Windows.Forms.ToolStripButton
    'Friend WithEvents AxPdfVw As AxXpdfViewer.AxXpdfViewer
    Friend WithEvents wdPendingFAX As AxDSOFramer.AxFramerControl
    Friend WithEvents tmrDocProtect As System.Windows.Forms.Timer
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pic_Image As PegasusImaging.WinForms.ImagXpress9.ImageXView
    Friend WithEvents ImagXpress1 As PegasusImaging.WinForms.ImagXpress9.ImagXpress
    'Friend WithEvents ImagXpress1 As PegasusImaging.WinForms.ImagXpress9.ImagXpress
    Friend WithEvents lblFAXID As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingFAXPreview))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblBtn = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtnBack = New System.Windows.Forms.ToolStripButton()
        Me.tblbtnNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tblbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlAttemptsDetailsBody = New System.Windows.Forms.Panel()
        Me.dgAttemptDetails = New gloEMR.clsDataGrid()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlAttemptsDetailsHeader = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlFaxAttemptDetailsBody = New System.Windows.Forms.Panel()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFAXID = New System.Windows.Forms.Label()
        Me.lblFAXType = New System.Windows.Forms.Label()
        Me.lblFAXNo = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblNoOfAttempt = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblFAXDate = New System.Windows.Forms.Label()
        Me.lblFAXTo = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblFAXBy = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlFaxAttemptDetailsHeader = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.wdPendingFAX = New AxDSOFramer.AxFramerControl()
        Me.pic_Image = New PegasusImaging.WinForms.ImagXpress9.ImageXView(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnuFirst = New System.Windows.Forms.MenuItem()
        Me.mnuPrevious = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuNext = New System.Windows.Forms.MenuItem()
        Me.mnuLast = New System.Windows.Forms.MenuItem()
        Me.pnlPreviewCommand = New System.Windows.Forms.Panel()
        Me.lblPreviewStatus = New System.Windows.Forms.Label()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tmrDocProtect = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ImagXpress1 = New PegasusImaging.WinForms.ImagXpress9.ImagXpress(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.tblBtn.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlAttemptsDetailsBody.SuspendLayout()
        CType(Me.dgAttemptDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.pnlAttemptsDetailsHeader.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlFaxAttemptDetailsBody.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlFaxAttemptDetailsHeader.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        CType(Me.wdPendingFAX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPreviewCommand.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tblBtn)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 53)
        Me.pnlToolStrip.TabIndex = 0
        '
        'tblBtn
        '
        Me.tblBtn.BackColor = System.Drawing.Color.Transparent
        Me.tblBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblBtn.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblBtn.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtnBack, Me.tblbtnNext, Me.ToolStripSeparator1, Me.tblbtnClose})
        Me.tblBtn.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblBtn.Location = New System.Drawing.Point(0, 0)
        Me.tblBtn.Name = "tblBtn"
        Me.tblBtn.Size = New System.Drawing.Size(1028, 53)
        Me.tblBtn.TabIndex = 0
        Me.tblBtn.Text = "ToolStrip1"
        '
        'tblbtnBack
        '
        Me.tblbtnBack.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnBack.Image = CType(resources.GetObject("tblbtnBack.Image"), System.Drawing.Image)
        Me.tblbtnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnBack.Name = "tblbtnBack"
        Me.tblbtnBack.Size = New System.Drawing.Size(63, 50)
        Me.tblbtnBack.Tag = "Back"
        Me.tblbtnBack.Text = "&Previous"
        Me.tblbtnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtnNext
        '
        Me.tblbtnNext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnNext.Image = CType(resources.GetObject("tblbtnNext.Image"), System.Drawing.Image)
        Me.tblbtnNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnNext.Name = "tblbtnNext"
        Me.tblbtnNext.Size = New System.Drawing.Size(39, 50)
        Me.tblbtnNext.Tag = "Next"
        Me.tblbtnNext.Text = "&Next"
        Me.tblbtnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.AutoSize = False
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 53)
        Me.ToolStripSeparator1.Visible = False
        '
        'tblbtnClose
        '
        Me.tblbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnClose.Image = CType(resources.GetObject("tblbtnClose.Image"), System.Drawing.Image)
        Me.tblbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnClose.Name = "tblbtnClose"
        Me.tblbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tblbtnClose.Tag = "Close"
        Me.tblbtnClose.Text = "&Close"
        Me.tblbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlAttemptsDetailsBody)
        Me.pnlMain.Controls.Add(Me.Panel4)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(297, 693)
        Me.pnlMain.TabIndex = 2
        '
        'pnlAttemptsDetailsBody
        '
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.dgAttemptDetails)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label18)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label19)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label20)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label21)
        Me.pnlAttemptsDetailsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttemptsDetailsBody.Location = New System.Drawing.Point(0, 265)
        Me.pnlAttemptsDetailsBody.Name = "pnlAttemptsDetailsBody"
        Me.pnlAttemptsDetailsBody.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlAttemptsDetailsBody.Size = New System.Drawing.Size(297, 428)
        Me.pnlAttemptsDetailsBody.TabIndex = 19
        '
        'dgAttemptDetails
        '
        Me.dgAttemptDetails.BackgroundColor = System.Drawing.Color.White
        Me.dgAttemptDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgAttemptDetails.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgAttemptDetails.CaptionForeColor = System.Drawing.Color.Black
        Me.dgAttemptDetails.CaptionVisible = False
        Me.dgAttemptDetails.DataMember = ""
        Me.dgAttemptDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgAttemptDetails.FullRowSelect = True
        Me.dgAttemptDetails.GridLineColor = System.Drawing.Color.Black
        Me.dgAttemptDetails.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgAttemptDetails.HeaderForeColor = System.Drawing.Color.Black
        Me.dgAttemptDetails.LinkColor = System.Drawing.Color.Brown
        Me.dgAttemptDetails.Location = New System.Drawing.Point(4, 1)
        Me.dgAttemptDetails.Name = "dgAttemptDetails"
        Me.dgAttemptDetails.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.dgAttemptDetails.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgAttemptDetails.ReadOnly = True
        Me.dgAttemptDetails.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.dgAttemptDetails.SelectionForeColor = System.Drawing.Color.Black
        Me.dgAttemptDetails.Size = New System.Drawing.Size(292, 423)
        Me.dgAttemptDetails.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(4, 424)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(292, 1)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 424)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(296, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 424)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(294, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.pnlAttemptsDetailsHeader)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 235)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(297, 30)
        Me.Panel4.TabIndex = 23
        '
        'pnlAttemptsDetailsHeader
        '
        Me.pnlAttemptsDetailsHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlAttemptsDetailsHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlAttemptsDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_RightBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_TopBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.Label1)
        Me.pnlAttemptsDetailsHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttemptsDetailsHeader.Location = New System.Drawing.Point(3, 0)
        Me.pnlAttemptsDetailsHeader.Name = "pnlAttemptsDetailsHeader"
        Me.pnlAttemptsDetailsHeader.Size = New System.Drawing.Size(294, 27)
        Me.pnlAttemptsDetailsHeader.TabIndex = 21
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 26)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(292, 1)
        Me.lbl_BottomBrd.TabIndex = 13
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_LeftBrd.TabIndex = 12
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(293, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_RightBrd.TabIndex = 11
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(294, 1)
        Me.lbl_TopBrd.TabIndex = 10
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(294, 27)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "  Pending Fax Details"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlFaxAttemptDetailsBody)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 32)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(297, 203)
        Me.Panel3.TabIndex = 23
        '
        'pnlFaxAttemptDetailsBody
        '
        Me.pnlFaxAttemptDetailsBody.AutoScroll = True
        Me.pnlFaxAttemptDetailsBody.BackColor = System.Drawing.Color.Transparent
        Me.pnlFaxAttemptDetailsBody.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Gradient
        Me.pnlFaxAttemptDetailsBody.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFileName)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label6)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label11)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label12)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label13)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblPatientName)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label2)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXID)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXType)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXNo)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label5)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label3)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label7)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblNoOfAttempt)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label8)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXDate)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXTo)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label10)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.Label4)
        Me.pnlFaxAttemptDetailsBody.Controls.Add(Me.lblFAXBy)
        Me.pnlFaxAttemptDetailsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFaxAttemptDetailsBody.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlFaxAttemptDetailsBody.Location = New System.Drawing.Point(3, 0)
        Me.pnlFaxAttemptDetailsBody.Name = "pnlFaxAttemptDetailsBody"
        Me.pnlFaxAttemptDetailsBody.Size = New System.Drawing.Size(294, 200)
        Me.pnlFaxAttemptDetailsBody.TabIndex = 22
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.BackColor = System.Drawing.Color.Transparent
        Me.lblFileName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileName.Location = New System.Drawing.Point(186, 93)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(59, 14)
        Me.lblFileName.TabIndex = 25
        Me.lblFileName.Text = "File Name"
        Me.lblFileName.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 199)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(292, 1)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 199)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(293, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 199)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(294, 1)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "label1"
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(125, 12)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(81, 14)
        Me.lblPatientName.TabIndex = 10
        Me.lblPatientName.Text = "Patient Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(67, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fax To :"
        '
        'lblFAXID
        '
        Me.lblFAXID.AutoSize = True
        Me.lblFAXID.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXID.Location = New System.Drawing.Point(20, 147)
        Me.lblFAXID.Name = "lblFAXID"
        Me.lblFAXID.Size = New System.Drawing.Size(41, 14)
        Me.lblFAXID.TabIndex = 20
        Me.lblFAXID.Text = "FaxID"
        Me.lblFAXID.Visible = False
        '
        'lblFAXType
        '
        Me.lblFAXType.AutoSize = True
        Me.lblFAXType.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXType.Location = New System.Drawing.Point(125, 120)
        Me.lblFAXType.Name = "lblFAXType"
        Me.lblFAXType.Size = New System.Drawing.Size(57, 14)
        Me.lblFAXType.TabIndex = 2
        Me.lblFAXType.Text = "Fax Type"
        '
        'lblFAXNo
        '
        Me.lblFAXNo.AutoSize = True
        Me.lblFAXNo.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXNo.Location = New System.Drawing.Point(125, 66)
        Me.lblFAXNo.Name = "lblFAXNo"
        Me.lblFAXNo.Size = New System.Drawing.Size(44, 14)
        Me.lblFAXNo.TabIndex = 3
        Me.lblFAXNo.Text = "Fax No"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(53, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Fax Date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 14)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "No Of Attempts :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(53, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 14)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Fax Type :"
        '
        'lblNoOfAttempt
        '
        Me.lblNoOfAttempt.AutoSize = True
        Me.lblNoOfAttempt.BackColor = System.Drawing.Color.Transparent
        Me.lblNoOfAttempt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoOfAttempt.Location = New System.Drawing.Point(125, 174)
        Me.lblNoOfAttempt.Name = "lblNoOfAttempt"
        Me.lblNoOfAttempt.Size = New System.Drawing.Size(90, 14)
        Me.lblNoOfAttempt.TabIndex = 16
        Me.lblNoOfAttempt.Text = "No Of Attempt"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(66, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Fax No :"
        '
        'lblFAXDate
        '
        Me.lblFAXDate.AutoSize = True
        Me.lblFAXDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXDate.Location = New System.Drawing.Point(125, 93)
        Me.lblFAXDate.Name = "lblFAXDate"
        Me.lblFAXDate.Size = New System.Drawing.Size(55, 14)
        Me.lblFAXDate.TabIndex = 15
        Me.lblFAXDate.Text = "Fax Date"
        '
        'lblFAXTo
        '
        Me.lblFAXTo.AutoSize = True
        Me.lblFAXTo.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXTo.Location = New System.Drawing.Point(125, 39)
        Me.lblFAXTo.Name = "lblFAXTo"
        Me.lblFAXTo.Size = New System.Drawing.Size(44, 14)
        Me.lblFAXTo.TabIndex = 8
        Me.lblFAXTo.Text = "Fax To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(67, 147)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 14)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Fax by :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Patient Name :"
        '
        'lblFAXBy
        '
        Me.lblFAXBy.AutoSize = True
        Me.lblFAXBy.BackColor = System.Drawing.Color.Transparent
        Me.lblFAXBy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFAXBy.Location = New System.Drawing.Point(125, 147)
        Me.lblFAXBy.Name = "lblFAXBy"
        Me.lblFAXBy.Size = New System.Drawing.Size(42, 14)
        Me.lblFAXBy.TabIndex = 12
        Me.lblFAXBy.Text = "Fax By"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlFaxAttemptDetailsHeader)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(297, 32)
        Me.Panel1.TabIndex = 23
        '
        'pnlFaxAttemptDetailsHeader
        '
        Me.pnlFaxAttemptDetailsHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlFaxAttemptDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFaxAttemptDetailsHeader.Controls.Add(Me.Label14)
        Me.pnlFaxAttemptDetailsHeader.Controls.Add(Me.Label15)
        Me.pnlFaxAttemptDetailsHeader.Controls.Add(Me.Label16)
        Me.pnlFaxAttemptDetailsHeader.Controls.Add(Me.Label17)
        Me.pnlFaxAttemptDetailsHeader.Controls.Add(Me.Label9)
        Me.pnlFaxAttemptDetailsHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFaxAttemptDetailsHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlFaxAttemptDetailsHeader.Name = "pnlFaxAttemptDetailsHeader"
        Me.pnlFaxAttemptDetailsHeader.Size = New System.Drawing.Size(294, 26)
        Me.pnlFaxAttemptDetailsHeader.TabIndex = 2
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 25)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(292, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 25)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(293, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 25)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(294, 1)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(294, 26)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "  Fax Attempt Details"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPreview
        '
        Me.pnlPreview.BackColor = System.Drawing.Color.White
        Me.pnlPreview.Controls.Add(Me.Label26)
        Me.pnlPreview.Controls.Add(Me.Label27)
        Me.pnlPreview.Controls.Add(Me.Label28)
        Me.pnlPreview.Controls.Add(Me.Label29)
        Me.pnlPreview.Controls.Add(Me.wdPendingFAX)
        Me.pnlPreview.Controls.Add(Me.pic_Image)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPreview.ForeColor = System.Drawing.Color.White
        Me.pnlPreview.Location = New System.Drawing.Point(301, 85)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlPreview.Size = New System.Drawing.Size(727, 661)
        Me.pnlPreview.TabIndex = 18
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 657)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(722, 1)
        Me.Label26.TabIndex = 26
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 657)
        Me.Label27.TabIndex = 25
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(723, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 657)
        Me.Label28.TabIndex = 24
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(724, 1)
        Me.Label29.TabIndex = 23
        Me.Label29.Text = "label1"
        '
        'wdPendingFAX
        '
        Me.wdPendingFAX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdPendingFAX.Enabled = True
        Me.wdPendingFAX.Location = New System.Drawing.Point(0, 0)
        Me.wdPendingFAX.Name = "wdPendingFAX"
        Me.wdPendingFAX.OcxState = CType(resources.GetObject("wdPendingFAX.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdPendingFAX.Size = New System.Drawing.Size(724, 658)
        Me.wdPendingFAX.TabIndex = 22
        '
        'pic_Image
        '
        Me.pic_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pic_Image.Location = New System.Drawing.Point(0, 0)
        Me.pic_Image.MouseWheelCapture = False
        Me.pic_Image.Name = "pic_Image"
        Me.pic_Image.Size = New System.Drawing.Size(724, 658)
        Me.pic_Image.TabIndex = 27
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFirst, Me.mnuPrevious, Me.MenuItem3, Me.mnuNext, Me.mnuLast})
        '
        'mnuFirst
        '
        Me.mnuFirst.Index = 0
        Me.mnuFirst.Text = "First"
        '
        'mnuPrevious
        '
        Me.mnuPrevious.Index = 1
        Me.mnuPrevious.Text = "Previous"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuNext
        '
        Me.mnuNext.Index = 3
        Me.mnuNext.Text = "Next"
        '
        'mnuLast
        '
        Me.mnuLast.Index = 4
        Me.mnuLast.Text = "Last"
        '
        'pnlPreviewCommand
        '
        Me.pnlPreviewCommand.BackColor = System.Drawing.Color.Transparent
        Me.pnlPreviewCommand.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlPreviewCommand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPreviewCommand.Controls.Add(Me.lblPreviewStatus)
        Me.pnlPreviewCommand.Controls.Add(Me.btnLast)
        Me.pnlPreviewCommand.Controls.Add(Me.btnNext)
        Me.pnlPreviewCommand.Controls.Add(Me.btnPrevious)
        Me.pnlPreviewCommand.Controls.Add(Me.btnFirst)
        Me.pnlPreviewCommand.Controls.Add(Me.Label22)
        Me.pnlPreviewCommand.Controls.Add(Me.Label23)
        Me.pnlPreviewCommand.Controls.Add(Me.Label24)
        Me.pnlPreviewCommand.Controls.Add(Me.Label25)
        Me.pnlPreviewCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPreviewCommand.Location = New System.Drawing.Point(0, 3)
        Me.pnlPreviewCommand.Name = "pnlPreviewCommand"
        Me.pnlPreviewCommand.Size = New System.Drawing.Size(724, 26)
        Me.pnlPreviewCommand.TabIndex = 0
        '
        'lblPreviewStatus
        '
        Me.lblPreviewStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblPreviewStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPreviewStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreviewStatus.Location = New System.Drawing.Point(97, 1)
        Me.lblPreviewStatus.Name = "lblPreviewStatus"
        Me.lblPreviewStatus.Size = New System.Drawing.Size(626, 24)
        Me.lblPreviewStatus.TabIndex = 20
        Me.lblPreviewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnLast
        '
        Me.btnLast.BackColor = System.Drawing.Color.Transparent
        Me.btnLast.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnLast.FlatAppearance.BorderSize = 0
        Me.btnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLast.Image = CType(resources.GetObject("btnLast.Image"), System.Drawing.Image)
        Me.btnLast.Location = New System.Drawing.Point(73, 1)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(24, 24)
        Me.btnLast.TabIndex = 3
        Me.btnLast.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.Transparent
        Me.btnNext.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(49, 1)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(24, 24)
        Me.btnNext.TabIndex = 2
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnPrevious
        '
        Me.btnPrevious.BackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnPrevious.FlatAppearance.BorderSize = 0
        Me.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), System.Drawing.Image)
        Me.btnPrevious.Location = New System.Drawing.Point(25, 1)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(24, 24)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.UseVisualStyleBackColor = False
        '
        'btnFirst
        '
        Me.btnFirst.BackColor = System.Drawing.Color.Transparent
        Me.btnFirst.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnFirst.FlatAppearance.BorderSize = 0
        Me.btnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), System.Drawing.Image)
        Me.btnFirst.Location = New System.Drawing.Point(1, 1)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(24, 24)
        Me.btnFirst.TabIndex = 0
        Me.btnFirst.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 25)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(722, 1)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 25)
        Me.Label23.TabIndex = 23
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(723, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 25)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(724, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'tmrDocProtect
        '
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlPreviewCommand)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(301, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(727, 32)
        Me.Panel2.TabIndex = 23
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(297, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 693)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'frmPendingFAXPreview
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnlPreview)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPendingFAXPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pending Fax Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblBtn.ResumeLayout(False)
        Me.tblBtn.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlAttemptsDetailsBody.ResumeLayout(False)
        CType(Me.dgAttemptDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.pnlAttemptsDetailsHeader.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlFaxAttemptDetailsBody.ResumeLayout(False)
        Me.pnlFaxAttemptDetailsBody.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlFaxAttemptDetailsHeader.ResumeLayout(False)
        Me.pnlPreview.ResumeLayout(False)
        CType(Me.wdPendingFAX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPreviewCommand.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    'Added the the new code against ImageXpress9->  dhruv
    Private recentImages As ArrayList
    Private recentImagesIdentify As ArrayList
    Private loadOptions As PegasusImaging.WinForms.ImagXpress9.LoadOptions
    ''--
    Dim dtFAXes As DataTable = Nothing
    Dim nCurrentRowNo As Int16

    Dim nPageNo As Byte
    Dim _fileExtension As String = ""
    Dim _FaxFileBinaryData As String = ""
    'Dim strFaxFileName As String = ""

    Dim strFileName As String = ""
    Dim nTotalPages As Byte
    Private WithEvents oCurDoc1 As Wd.Document
    'Code Added on 20091015
    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oPDFDoc As pdftron.PDF.PDFDoc
    'End Code Added on 20091015

    'Added the the new code against ImageXpress9->  dhruv
    Dim _strFileName As String = ""
    ''-
    Dim _PatientID As Int64


    Private Sub FillFAXDetails()
        nPageNo = 1
        'Dim strFileName As String
        Dim currentIndex As Long = 0
        Dim bytesRead As Integer = 0
        Dim myMaxLength As Integer = (CInt(gloEDocV3Admin.GetMemoryBuferSetting() / 7) * 4) ''10240 ''(1048576 / 7) * 3
        myMaxLength = (CInt(myMaxLength / 12) * 12)

        'reset the controls to clear previous fax data
        pic_Image.Image = Nothing
        lblPreviewStatus.Text = ""

        If pnlPreview.Controls.Contains(oPDFView) Then
            pnlPreview.Controls.Remove(oPDFView)
        End If

        btnPrevious.Enabled = False
        btnFirst.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
        'reset the controls to clear previous fax data
        If (IsNothing(dtFAXes)) Then
            Exit Sub
        End If
        Dim buffer(myMaxLength) As Char
        Dim objFAX As New clsFAX()
        Try
            lblFAXID.Text = dtFAXes.Rows(nCurrentRowNo).Item("FAXID")
            lblPatientName.Text = dtFAXes.Rows(nCurrentRowNo).Item("PatientName")
            lblFAXTo.Text = dtFAXes.Rows(nCurrentRowNo).Item("FAXTo")
            lblFAXType.Text = dtFAXes.Rows(nCurrentRowNo).Item("FAXType")
            lblFAXNo.Text = dtFAXes.Rows(nCurrentRowNo).Item("FAXNo")
            lblFAXBy.Text = dtFAXes.Rows(nCurrentRowNo).Item("LoginUser")
            lblFAXDate.Text = dtFAXes.Rows(nCurrentRowNo).Item("FAXDate")
            lblNoOfAttempt.Text = dtFAXes.Rows(nCurrentRowNo).Item("NoOfAttempts")
            strFileName = dtFAXes.Rows(nCurrentRowNo).Item("FAXFileName")
            _fileExtension = dtFAXes.Rows(nCurrentRowNo).Item("EFax_DocumentExtension")

            Dim _result As Boolean = True
            Dim _ErrorMessage As String


            bytesRead = GetContainerStream(System.Convert.ToInt64(dtFAXes.Rows(nCurrentRowNo).Item("PatientID")), System.Convert.ToInt64(dtFAXes.Rows(nCurrentRowNo).Item("FAXID")), currentIndex, myMaxLength, buffer)
            Dim sTempFile As String = String.Empty

            If bytesRead > 0 Then
                Dim oFile As FileStream = Nothing
                Dim bw As BinaryWriter = Nothing

                sTempFile = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, "." & _fileExtension, "yyyyMMddHHmmssffff") 'gloSettings.FolderSettings.AppTempFolderPath & "\" & getUniqueID() & "." & _fileExtension

                If (File.Exists(sTempFile)) Then
                    File.Delete(sTempFile)
                End If

                oFile = New FileStream(sTempFile, FileMode.Create)
                bw = New BinaryWriter(oFile)

                While (bytesRead > 0)
                    Try
                        If oFile Is Nothing Then
                            _result = False
                            _ErrorMessage = "Error file object is null"
                        End If
                        If _result = True Then

                            bw.Write(System.Convert.FromBase64CharArray(buffer, 0, bytesRead))

                        End If
                    Catch ex As Exception
                    End Try
                    currentIndex += bytesRead
                    If (bytesRead = myMaxLength) Then
                        bytesRead = GetContainerStream(System.Convert.ToInt64(dtFAXes.Rows(nCurrentRowNo).Item("PatientID")), System.Convert.ToInt64(dtFAXes.Rows(nCurrentRowNo).Item("FAXID")), currentIndex, myMaxLength, buffer)
                    Else
                        bytesRead = 0
                    End If
                End While

                bw.Flush()
                bw.Close()
                oFile.Close()
                bw.Dispose()
                bw = Nothing
                oFile.Dispose()
                oFile = Nothing
            End If

            lblFileName.Text = strFileName

            If currentIndex > 1 And sTempFile <> "" Then  '' eFax ON

                If _fileExtension.ToUpper = "DOCX" Then
                    wdPendingFAX.Visible = True
                    pnlPreviewCommand.Visible = False
                    '   wdPendingFAX.Open(sTempFile) 'myStrFileName
                    Dim thisApplication As Wd.Application = Nothing
                    Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdPendingFAX, sTempFile, oCurDoc1, thisApplication)
                    If (strError <> String.Empty) Then
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, strError, gloAuditTrail.ActivityOutCome.Failure)
                    Else
                        wdPendingFAX.BringToFront()
                        wdPendingFAX.Update()
                        oCurDoc1 = wdPendingFAX.ActiveDocument
                        ProtectDocument()
                    End If
                   

                ElseIf _fileExtension.ToUpper = "PDF" Or _fileExtension.ToUpper = ".PDF" Then
                    wdPendingFAX.Visible = False
                    pnlPreviewCommand.Visible = True
                    pic_Image.Visible = False
                    nPageNo = 1

                    If oPDFView Is Nothing Then
                        oPDFView = New pdftron.PDF.PDFViewCtrl()
                    End If

                    oPDFDoc = New pdftron.PDF.PDFDoc(sTempFile)  'myStrFileName
                    If oPDFView Is Nothing Then
                        oPDFView = New pdftron.PDF.PDFViewCtrl()
                    End If

                    oPDFView.Show()
                    oPDFView.SetDoc(oPDFDoc)
                    pnlPreview.Controls.Add(oPDFView)
                    oPDFView.Dock = DockStyle.Fill
                    oPDFView.BringToFront()
                    oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page)

                    oPDFView.SetCaching(True)
                    oPDFView.SetProgressiveRendering(True)
                    oPDFView.Visible = True
                    oPDFView.Refresh()
                    oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page)
                    oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width)

                    If (oPDFView.GotoFirstPage() = True) Then
                        oPDFView.GetSelectionBeginPage()
                    End If

                    lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
                    btnPrevious.Enabled = False
                    btnFirst.Enabled = False
                    If oPDFView.GetPageCount() > 1 Then
                        btnNext.Enabled = True
                        btnLast.Enabled = True
                    Else
                        btnNext.Enabled = False
                        btnLast.Enabled = False
                    End If
                    oPDFView.EnableInteractiveForms(False)
                ElseIf _fileExtension.ToUpper = "TIF" Or _fileExtension.ToUpper = "TIFF" Then '' Seems to be dead code
                    wdPendingFAX.Visible = False
                    pic_Image.Visible = True
                    pnlPreviewCommand.Visible = True

                    nPageNo = 1
                    LoadImage()

                    lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
                    btnPrevious.Enabled = False
                    btnFirst.Enabled = False
                    If nTotalPages > 1 Then
                        btnNext.Enabled = True
                        btnLast.Enabled = True
                    Else
                        btnNext.Enabled = False
                        btnLast.Enabled = False
                    End If

                End If
            Else  '' Normal FAX

                nPageNo = 1
                pic_Image.Visible = True
                pic_Image.BringToFront()
                pnlPreview.Visible = True
                pnlPreview.BringToFront()
                pnlPreviewCommand.Visible = True
                pnlPreviewCommand.Visible = True
                wdPendingFAX.Visible = False

                LoadImage()

                lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
                btnPrevious.Enabled = False
                btnFirst.Enabled = False
                If nTotalPages > 1 Then
                    btnNext.Enabled = True
                    btnLast.Enabled = True
                Else
                    btnNext.Enabled = False
                    btnLast.Enabled = False
                End If

            End If  'normal fax

            Dim dtAttempts As DataTable = objFAX.RetrieveFAXAttemptsDetails(lblFAXID.Text)
            dgAttemptDetails.DataSource = dtAttempts

            Dim grdTableStyle As New clsDataGridTableStyle(dtAttempts.TableName)

            Dim grdColStyleFAXDTLID As New DataGridTextBoxColumn
            With grdColStyleFAXDTLID
                .HeaderText = "DTLID"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAttempts.Columns("FAXDTLID").ColumnName
                .NullText = ""
                .Width = 0
            End With

            Dim grdColStyleAttemptDate As New DataGridTextBoxColumn
            With grdColStyleAttemptDate
                .HeaderText = "Attempt Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAttempts.Columns("AttemptDate").ColumnName
                .NullText = ""
                .Width = 0.25 * dgAttemptDetails.Width
            End With


            Dim grdColStyleFAXResponse As New DataGridTextBoxColumn
            With grdColStyleFAXResponse
                .HeaderText = "Reason"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAttempts.Columns("FAXResponse").ColumnName
                .NullText = ""
                .Width = 0.75 * dgAttemptDetails.Width - 5
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleFAXDTLID, grdColStyleAttemptDate, grdColStyleFAXResponse})
            dgAttemptDetails.TableStyles.Clear()
            dgAttemptDetails.TableStyles.Add(grdTableStyle)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Public Function GetContainerStream(ByVal nPatientID As Long, ByVal nFaxID As Long, ByVal myCurrentIndex As Int64, ByVal myLength As Int64, ByRef myBuffer() As Char) As Long

        Dim _result As Object = Nothing
        Dim byteRead As [Byte]() = Nothing
        Dim oDBParameters As SqlParameter = Nothing
        Dim objCmd As New SqlCommand()
        Dim oReader As SqlDataReader = Nothing
        Dim bytesRead As Long = 0
        Dim objConn As New SqlClient.SqlConnection()
        objConn.ConnectionString = GetConnectionString()
        objConn.Open()
        'new gloEDocumentV3.Database.DBParameters();


        Try
            If objConn IsNot Nothing Then
                objCmd.CommandType = CommandType.StoredProcedure
                objCmd.CommandText = "getFaxBinaryData"
                objCmd.Connection = objConn


                oDBParameters = New SqlParameter()
                If oDBParameters IsNot Nothing Then

                    oDBParameters = New SqlParameter()
                    oDBParameters.ParameterName = "@nPatientID"
                    oDBParameters.Value = nPatientID
                    oDBParameters.Direction = ParameterDirection.Input
                    oDBParameters.SqlDbType = SqlDbType.BigInt
                    objCmd.Parameters.Add(oDBParameters)


                    oDBParameters = New SqlParameter()
                    oDBParameters.ParameterName = "@FAXID"
                    oDBParameters.Value = nFaxID
                    oDBParameters.Direction = ParameterDirection.Input
                    oDBParameters.SqlDbType = SqlDbType.BigInt
                    objCmd.Parameters.Add(oDBParameters)

                    oDBParameters = New SqlParameter()
                    oDBParameters.ParameterName = "@nCurrentIndex"
                    oDBParameters.Value = myCurrentIndex
                    oDBParameters.Direction = ParameterDirection.Input
                    oDBParameters.SqlDbType = SqlDbType.BigInt
                    objCmd.Parameters.Add(oDBParameters)

                    oDBParameters = New SqlParameter()
                    oDBParameters.ParameterName = "@nLength"
                    oDBParameters.Value = myLength
                    oDBParameters.Direction = ParameterDirection.Input
                    oDBParameters.SqlDbType = SqlDbType.BigInt
                    objCmd.Parameters.Add(oDBParameters)

                    objCmd.Connection = objConn

                    oReader = objCmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess)

                    ''
                    While (oReader.Read())

                        'Dim instance As SqlDataReader
                        'Dim i As Integer
                        'Dim cReader As SqlChars = oReader.GetSqlChars(0)

                        'returnValue = instance.GetSqlChars(i)

                        bytesRead = oReader.GetChars(0, 0, myBuffer, 0, myLength)

                    End While


                    ''

                    Return bytesRead
                End If
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        Finally
            If oReader.IsClosed Then
            Else
                oReader.Close()
                oReader = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objConn) Then
                If objConn.State = ConnectionState.Open Then
                    objConn.Close()
                End If
                objConn.Dispose()
                objConn = Nothing
            End If
            If IsNothing(oDBParameters) = False Then
                '  oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

        End Try


    End Function
    'Function getUniqueID() As String
    '    Static firstTime As Boolean = True
    '    Static myWatch As New Stopwatch()
    '    Static myTime As DateTime
    '    If firstTime = True Then
    '        firstTime = False
    '        myTime = Now()
    '        myWatch.Start()
    '    End If
    '    Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
    '    getUniqueID = TmSp.Ticks.ToString()


    'End Function
    ''End' Huge Pending Fax from DMS

    Public Sub LoadImage() ', ByVal strFileExtention As String
        ' nPageNo = 1
        If (gstrFAXOutputDirectory <> "" And strFileName <> "") Then
            Dim strtmpFilepath As String = gstrFAXOutputDirectory & "\" & strFileName & "." & "tif"
            If (File.Exists(strtmpFilepath) = True) Then

                nTotalPages = PegasusImaging.WinForms.ImagXpress9.ImageX.NumPages(ImagXpress1, strtmpFilepath)
                pic_Image.AllowUpdate = False

                If Not IsNothing(pic_Image) Then
                    If Not IsNothing(pic_Image.Image) Then
                        pic_Image.Image.Dispose()
                        pic_Image.Image = Nothing
                    End If
                End If
                loadOptions = New PegasusImaging.WinForms.ImagXpress9.LoadOptions()
                'pic_Image.Image = PegasusImaging.WinForms.ImagXpress9.ImageX.FromFile(ImagXpress1, _strFileName, nPageNo, loadOptions)
                pic_Image.Image = PegasusImaging.WinForms.ImagXpress9.ImageX.FromFile(ImagXpress1, strtmpFilepath, nPageNo, loadOptions)

                pic_Image.Image.ImageXData.Resolution.Units = GraphicsUnit.Inch
                pic_Image.ZoomToFit(PegasusImaging.WinForms.ImagXpress9.ZoomToFitType.FitBest)
                pic_Image.AllowUpdate = True
            End If
        End If
    End Sub

    Private Sub tblFAX_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)

    End Sub

    ''Start' Huge Pending Fax from DMS
    'Public Sub ShowPendingFAXDetails(ByVal nFAXID As Integer, ByVal PatientID As Long, ByVal dummy As Boolean)
    '    'Retrieve Selected Patients FAXes & store it in DataTable
    '    Dim objFAX As New clsFAX
    '    Try
    '        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
    '        'dtFAXes = objFAX.Fill_PendingFAXes(gnPatientID)
    '        dtFAXes = objFAX.Fill_PendingFAXes(PatientID)
    '        'end modification
    '        objFAX = Nothing

    '        Dim nCount As Int16
    '        For nCount = 0 To dtFAXes.Rows.Count - 1
    '            If dtFAXes.Rows(nCount).Item(0) = nFAXID Then
    '                nCurrentRowNo = nCount
    '                Call FillFAXDetails()
    '                If dtFAXes.Rows.Count <= 1 Then
    '                    tblbtnBack.Enabled = False
    '                    tblbtnNext.Enabled = False
    '                Else
    '                    If nCurrentRowNo <= 0 Then
    '                        tblbtnBack.Enabled = False
    '                    ElseIf nCurrentRowNo >= dtFAXes.Rows.Count - 1 Then
    '                        tblbtnNext.Enabled = False
    '                    End If
    '                End If
    '                Exit Sub
    '            End If
    '        Next
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

    '    End Try
    'End Sub
    ''''Start' Huge Pending Fax from DMS

    Public Sub ShowPendingFAXDetails(ByVal nFAXID As Integer, ByVal PatientID As Long, ByVal dummy As Boolean)
        _PatientID = PatientID
        Dim objFAX As New clsFAX
        Try
            dtFAXes = objFAX.Fill_RetrieveAllPendingFaxesWithoutBinary(PatientID)
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
            objFAX.Dispose()
            objFAX = Nothing
            If (IsNothing(dtFAXes)) Then
                Exit Sub
            End If
            Dim nCount As Int16
            For nCount = 0 To dtFAXes.Rows.Count - 1
                If dtFAXes.Rows(nCount).Item(0) = nFAXID Then
                    nCurrentRowNo = nCount
                    Call FillFAXDetails()
                    If dtFAXes.Rows.Count <= 1 Then
                        tblbtnBack.Enabled = False
                        tblbtnNext.Enabled = False
                    Else
                        If nCurrentRowNo <= 0 Then
                            tblbtnBack.Enabled = False
                        ElseIf nCurrentRowNo >= dtFAXes.Rows.Count - 1 Then
                            tblbtnNext.Enabled = False
                        End If
                    End If
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    ''End'Huge Pending Fax from DMS

    'Changed by Shweta 20091021
    'New parameter added to send the patientID of selected row from the grid

    Public Sub ShowPendingFAXDetails(ByVal nFAXID As String, ByVal nPatientID As String)

        Dim objFAX As New clsFAX

        Try
            Dim strFaxID As String()
            strFaxID = nFAXID.Split(",")

            Dim i As Int32

            '00000136 : out of memory errors when there are a large number of pending faxes
            'take only those faxes from database which user wants to preview instead of taking all faxes for a patient. 
            For i = 0 To strFaxID.Length - 1

                Dim FaxID As Int64 = System.Convert.ToInt64(strFaxID(i))

                Dim dtPending As DataTable = objFAX.RetrievePendingFAX(FaxID)

                If Not IsNothing(dtPending) AndAlso dtPending.Rows.Count > 0 Then
                    If i = 0 Then
                        dtFAXes = dtPending.Copy()
                    Else

                        dtFAXes.Merge(dtPending, False)

                    End If
                End If

                dtPending.Dispose()
                dtPending = Nothing
            Next

            objFAX.Dispose()
            objFAX = Nothing

            ''to get the array of patientID so that can pass patintID to retrive pending fax
            'Dim strPatientID As String()
            'strPatientID = nPatientID.Split(",")
            'Dim i As Integer
            'For i = 0 To strPatientID.Length - 1
            '    'Temporary table to retrive pending fax data
            '    Dim dtPending As New DataTable
            '    Dim PatientID As Int64 = System.Convert.ToInt64(strPatientID(i))
            '    dtPending = objFAX.Fill_PendingFAXes(PatientID)
            '    ' dtFAXes = objFAX.Fill_PendingFAXes(True, str[i])
            '    'Copy temporary table to main table at the first time
            '    If i = 0 Then
            '        dtFAXes = dtPending.Copy()
            '    Else
            '        'merge the new retrive data into main table
            '        dtFAXes.Merge(dtPending, False)
            '    End If

            'Next
            'objFAX = Nothing

            ' ''To get only selected fax in to the main datatable
            'Dim nCount As Int16
            'Dim strFaxID As String()
            'strFaxID = nFAXID.Split(",")
            'For nCount = dtFAXes.Rows.Count - 1 To 0 Step -1
            '    'If faxId doesn't match with seleted fax then remove that from the table
            '    Dim faxID As String = dtFAXes.Rows(nCount).Item(0).ToString().Trim()

            '    Dim bResult As Boolean
            '    Dim fDelete As Boolean = True
            '    For j As Integer = 0 To strFaxID.Length - 1
            '        bResult = (strFaxID.GetValue(j) = (dtFAXes.Rows(nCount).Item(0).ToString().Trim()))
            '        'if the faxID is present then don't delete it in faxID string
            '        If bResult = True Then
            '            fDelete = False
            '        End If
            '    Next
            '    If fDelete = True Then
            '        dtFAXes.Rows.Remove(dtFAXes.Rows(nCount))
            '        'rowCount = rowCount - 1
            '        'nCount = nCount - 1
            '    End If
            'Next

            'To fill fax details
            If IsNothing(dtFAXes) Then Exit Sub
            Dim nCount As Int32

            For nCount = 0 To dtFAXes.Rows.Count - 1
                Dim faxID As String = dtFAXes.Rows(nCount).Item(0).ToString().Trim()
                If nFAXID.Contains(faxID) Then
                    'If dtFAXes.Rows(nCount).Item(0).ToString().Trim() = nFAXID Then
                    nCurrentRowNo = nCount
                    'Fill the fax details  in preview
                    Call FillFAXDetails()
                    If dtFAXes.Rows.Count <= 1 Then
                        tblbtnBack.Enabled = False
                        tblbtnNext.Enabled = False
                    Else
                        If nCurrentRowNo <= 0 Then
                            tblbtnBack.Enabled = False
                        ElseIf nCurrentRowNo >= dtFAXes.Rows.Count - 1 Then
                            tblbtnNext.Enabled = False
                        End If
                    End If
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub
    'End 20091021
    Private Sub btnbtnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        'Code Added on 20091015
        Try
            btnNext.Enabled = True
            btnLast.Enabled = True
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            nPageNo = 1

            If _fileExtension.ToUpper() = "PDF" Or _fileExtension.ToUpper() = ".PDF" Then
                '  If  axpdfVw.getFileName() <> "" Then
                'AxPdfVw.gotoFirstPage()
                'End If
                If IsNothing(oPDFView.GetDoc) = False Then
                    oPDFView.GotoFirstPage()
                End If

                lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
                'lblPreviewStatus.Text = " Page " & nPageNo & " of " '&  axpdfVw.numPages

                'End Code Added on 20091015
            Else
                'Added the the new code against ImageXpress9->  dhruv
                LoadImage()
                ''--

                'Removed PegausImageXpress7 -> Dhruv
                'picPreview.PageNbr = nPageNo
                'picPreview.FileName = strFaxFileName
                'picPreview.ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
                ''---
                lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        Try
            'Code Added on 20091015
            btnNext.Enabled = True
            btnLast.Enabled = True



            If _fileExtension.ToUpper() = "PDF" Or _fileExtension.ToUpper() = ".PDF" Then

                If IsNothing(oPDFView.GetDoc) = False Then
                    oPDFView.GotoPreviousPage()
                End If

                If oPDFView.GetCurrentPage() = 1 Then
                    btnPrevious.Enabled = False
                    btnFirst.Enabled = False
                End If

                lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
                'End Code Added on 20091015
                '' 20091014
                'If AxPdfVw.getFileName() <> "" Then
                '    If AxPdfVw.currentPage = 1 Then
                '    Else
                '        AxPdfVw.gotoPreviousPage()


                '    End If

                'End If


                'If AxPdfVw.currentPage <= 1 Then
                '    btnPrevious.Enabled = False
                '    btnFirst.Enabled = False
                'End If
                '' END 20091014

                'lblPreviewStatus.Text = " Page " & nPageNo & " of " & AxPdfVw.numPages

                ''  20091014
                ' lblPreviewStatus.Text = " Page " & AxPdfVw.currentPage & " of " & AxPdfVw.numPages
                '' END 20091014

            Else

                nPageNo = nPageNo - 1
                'Added the the new code against ImageXpress9->  dhruv
                LoadImage()
                ''--

                If nPageNo <= 1 Then
                    btnPrevious.Enabled = False
                    btnFirst.Enabled = False
                End If
                'Removed PegausImageXpress7 -> Dhruv
                'picPreview.PageNbr = nPageNo
                'picPreview.FileName = strFaxFileName
                'picPreview.ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
                ''--
                lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)


            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try


    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            'Code Added on 20091015
            btnPrevious.Enabled = True
            btnFirst.Enabled = True

            If _fileExtension.ToUpper() = "PDF" Or _fileExtension.ToUpper() = ".PDF" Then
                If IsNothing(oPDFView.GetDoc) = False Then
                    oPDFView.GotoNextPage()
                End If

                If oPDFView.GetCurrentPage() >= oPDFView.GetPageCount() Then
                    btnNext.Enabled = False
                    btnLast.Enabled = False
                End If

                lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()

                '' 20091014
                'If AxPdfVw.getFileName() <> "" Then
                '    If AxPdfVw.currentPage = AxPdfVw.numPages Then
                '    Else
                '        AxPdfVw.gotoNextPage()
                '    End If

                'End If

                'If AxPdfVw.currentPage >= AxPdfVw.numPages Then
                '    btnNext.Enabled = False
                '    btnLast.Enabled = False
                'End If


                '' END 20091014
                'End Code Added on 20091015
            Else

                nPageNo = nPageNo + 1
                LoadImage()
                'Removed PegausImageXpress7 -> Dhruv
                'picPreview.PageNbr = nPageNo
                'picPreview.FileName = strFaxFileName
                'picPreview.ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
                ''--
                If nPageNo >= nTotalPages Then
                    btnNext.Enabled = False
                    btnLast.Enabled = False
                End If
                lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Try
            'Code Added on 20091015
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
            btnNext.Enabled = False
            btnLast.Enabled = False

            If _fileExtension.ToUpper() = "PDF" Or _fileExtension.ToUpper() = ".PDF" Then
                If IsNothing(oPDFView.GetDoc) = False Then
                    oPDFView.GotoLastPage()
                End If

                lblPreviewStatus.Text = " Page " & oPDFView.GetCurrentPage() & " of " & oPDFView.GetPageCount()
                '' 20091014
                'If AxPdfVw.getFileName() <> "" Then
                '    AxPdfVw.currentPage = AxPdfVw.numPages

                '    If AxPdfVw.currentPage >= AxPdfVw.numPages Then
                '        btnNext.Enabled = False
                '        btnLast.Enabled = False
                '    End If

                '    lblPreviewStatus.Text = " Page " & AxPdfVw.currentPage & " of " & AxPdfVw.numPages
                'End If
                '' END 20091014
                'End Code Added on 20091015
            Else

                nPageNo = nTotalPages
                LoadImage()
                'Removed PegausImageXpress7 -> Dhruv
                'picPreview.PageNbr = nPageNo
                'picPreview.FileName = strFaxFileName
                'picPreview.ZoomToFit(PegasusImaging.WinForms.ImagXpress7.enumZoomFit.ZOOMFIT_BEST)
                ''---------
                lblPreviewStatus.Text = " Page " & nPageNo & " of " & nTotalPages
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub


    Private Sub tblBtn_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblBtn.ItemClicked
        Try
            Select Case Trim(e.ClickedItem.Tag)
                Case "Back"
                    nCurrentRowNo = nCurrentRowNo - 1
                    FillFAXDetails()
                    If nCurrentRowNo <= 0 Then
                        tblbtnBack.Enabled = False
                    End If
                    tblbtnNext.Enabled = True
                Case "Next"
                    nCurrentRowNo = nCurrentRowNo + 1
                    FillFAXDetails()
                    If nCurrentRowNo >= dtFAXes.Rows.Count - 1 Then
                        tblbtnNext.Enabled = False
                    End If
                    tblbtnBack.Enabled = True
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmPendingFAXPreview_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPendingFAXPreview_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If IsNothing(wdPendingFAX) = False Then
            wdPendingFAX.Close()
        End If

        If IsNothing(oPDFView) = False Then
            DisconnectToPDFTron()
        End If
    End Sub

    Private Sub tmrDocProtect_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDocProtect.Tick
        tmrDocProtect.Enabled = False
        If Not oCurDoc1 Is Nothing Then
            Dim protectPane As Wd.TaskPane = oCurDoc1.ActiveWindow.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection)
            If (IsNothing(protectPane) = False) Then
                protectPane.Visible = False
                Marshal.ReleaseComObject(protectPane)
                protectPane = Nothing
            End If
            'oCurDoc1.Application.TaskPanes(Wd.WdTaskPanes.wdTaskPaneDocumentProtection).Visible = False
        End If
        tmrDocProtect.Enabled = True
    End Sub

    Private Sub ProtectDocument()
        tmrDocProtect.Enabled = False
        If IsNothing(oCurDoc1) = False Then
            oCurDoc1.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments)
        End If
        tmrDocProtect.Enabled = True
        tmrDocProtect.Interval = 10
    End Sub
    'Code Added on 20091015
    Public Shared Sub ConnectToPDFTron()
        If gloEDocumentV3.gloEDocV3Admin.gIsPDFTronConnected = True Then
            DisconnectToPDFTron()
        End If

        Try
            '' New Licenece Added on 20120926
            ' pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20110602):7DE6A118A47A49B951EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA")
            pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20130603):4DE63118A4FA49B931EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA")
            ''

            '' Commented on 20100907 Licence Expired
            ' pdftron.PDFNet.Initialize("gloStream Inc.(glostream.com):CPU:1:E:W:AMC(20091008):69F4EAB1559D7A21E3C841109C92CA1D944C4B5A192BD15C49D07AF0FA")
            ''
            'pdftron.PDFNet.Initialize("gloStream Inc. (glostream.com):CPU:1:E:W:AMC(20081108):3CF4F371559DFA2163C846109C92BA7DE4C54B5A192BD15C49D07AF0FA");
            Dim strResourcePath As String = Application.StartupPath & "\\pdfnet.res"
            pdftron.PDFNet.SetResourcesPath(strResourcePath)
        Catch ex As pdftron.Common.PDFNetException

        End Try
        'End Code Added on 20091015
    End Sub
    'Code Added on 20091015
    Public Shared Sub DisconnectToPDFTron()
        Try
            pdftron.PDFNet.Terminate()
            gloEDocumentV3.gloEDocV3Admin.gIsPDFTronConnected = False
        Catch ex As pdftron.Common.PDFNetException

        End Try
    End Sub
    'End Code Added on 20091015

    Private Sub frmPendingFAXPreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
