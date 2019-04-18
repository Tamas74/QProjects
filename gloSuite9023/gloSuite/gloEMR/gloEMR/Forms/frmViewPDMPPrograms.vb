Imports QSurvey.Models
Imports pdmeds = gloGlobal.PDMP.Meds
Imports gloUIControlLibrary.WPFUserControl
Imports System.Xml.Linq
Imports System.IO
Imports System.Text
Imports gloEMRGeneralLibrary
Imports System.Linq
Imports SelectPdf
Imports gloEDocumentV3

Public Class frmViewpdmpprograms

    Inherits System.Windows.Forms.Form

    Dim bIsDocumentLoaded As Boolean = False
    Dim sFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".html", "MMddyyyyHHmmssffff")
    Dim sFilePathPDF As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".pdf", "MMddyyyyHHmmssffff")
    Dim nPatientID As Long
    Dim bytearray As String

    Dim _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

    Dim Col_HoosID As Integer = 0
    Dim Col_ProviderName As Integer = 0
    Dim Col_ResponseDate As Integer = 1
    Dim Col_ReportID As Integer = 2
    Dim Col_FormName As Integer = 4
    Dim Col_Count As Integer = 3

    Dim ind As Integer = -1
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus

    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Friend WithEvents c1Screenings As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser

    Private WithEvents PatientStripControl As gloUserControlLibrary.gloUC_PatientStrip
    Friend WithEvents pnlWebBrowser As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlWait As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents tblbtn_Save_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip


#Region " Windows Form Designer generated code "
    'constructor commnted by dipak 20100907 as not used anywhere 
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal PatientID As Long)
        InitializeComponent()
        nPatientID = PatientID
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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewpdmpprograms))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Save_32 = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.c1Screenings = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.pnlWebBrowser = New System.Windows.Forms.Panel()
        Me.pnlWait = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        CType(Me.c1Screenings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlWebBrowser.SuspendLayout()
        Me.pnlWait.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(848, 53)
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save_32, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(848, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'tblbtn_Save_32
        '
        Me.tblbtn_Save_32.AutoToolTip = False
        Me.tblbtn_Save_32.BackColor = System.Drawing.Color.Transparent
        Me.tblbtn_Save_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblbtn_Save_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Save_32.Image = CType(resources.GetObject("tblbtn_Save_32.Image"), System.Drawing.Image)
        Me.tblbtn_Save_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save_32.Name = "tblbtn_Save_32"
        Me.tblbtn_Save_32.Size = New System.Drawing.Size(96, 50)
        Me.tblbtn_Save_32.Tag = "SaveExam"
        Me.tblbtn_Save_32.Text = " Send to DMS"
        Me.tblbtn_Save_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tblbtn_Save_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save_32.ToolTipText = " Send to DMS"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(842, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(844, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 197)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 197)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 200)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(840, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'c1Screenings
        '
        Me.c1Screenings.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Screenings.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.c1Screenings.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Screenings.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Screenings.ColumnInfo = resources.GetString("c1Screenings.ColumnInfo")
        Me.c1Screenings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Screenings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Screenings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Screenings.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1Screenings.Location = New System.Drawing.Point(4, 4)
        Me.c1Screenings.Name = "c1Screenings"
        Me.c1Screenings.Rows.Count = 1
        Me.c1Screenings.Rows.DefaultSize = 19
        Me.c1Screenings.Rows.Fixed = 0
        Me.c1Screenings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Screenings.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1Screenings.ShowCellLabels = True
        Me.c1Screenings.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.c1Screenings.Size = New System.Drawing.Size(840, 196)
        Me.c1Screenings.StyleInfo = resources.GetString("c1Screenings.StyleInfo")
        Me.c1Screenings.TabIndex = 15
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.c1Screenings)
        Me.pnlMain.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMain.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMain.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMain.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlMain.Size = New System.Drawing.Size(848, 201)
        Me.pnlMain.TabIndex = 0
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(4, 1)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(840, 256)
        Me.WebBrowser1.TabIndex = 16
        '
        'pnlWebBrowser
        '
        Me.pnlWebBrowser.Controls.Add(Me.pnlWait)
        Me.pnlWebBrowser.Controls.Add(Me.WebBrowser1)
        Me.pnlWebBrowser.Controls.Add(Me.Label1)
        Me.pnlWebBrowser.Controls.Add(Me.Label2)
        Me.pnlWebBrowser.Controls.Add(Me.Label3)
        Me.pnlWebBrowser.Controls.Add(Me.Label4)
        Me.pnlWebBrowser.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlWebBrowser.Location = New System.Drawing.Point(0, 257)
        Me.pnlWebBrowser.Name = "pnlWebBrowser"
        Me.pnlWebBrowser.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlWebBrowser.Size = New System.Drawing.Size(848, 261)
        Me.pnlWebBrowser.TabIndex = 12
        Me.pnlWebBrowser.Visible = False
        '
        'pnlWait
        '
        Me.pnlWait.BackColor = System.Drawing.Color.White
        Me.pnlWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWait.Controls.Add(Me.Label12)
        Me.pnlWait.Controls.Add(Me.PictureBox1)
        Me.pnlWait.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlWait.Location = New System.Drawing.Point(4, 1)
        Me.pnlWait.Name = "pnlWait"
        Me.pnlWait.Size = New System.Drawing.Size(840, 256)
        Me.pnlWait.TabIndex = 68
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(453, 266)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(207, 19)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "Loading Narx Care Report..."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(518, 187)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(69, 69)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 62
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 257)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(840, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 257)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(844, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 257)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(842, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 254)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(848, 3)
        Me.Splitter1.TabIndex = 14
        Me.Splitter1.TabStop = False
        '
        'frmViewpdmpprograms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(848, 518)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlWebBrowser)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewpdmpprograms"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View PDMP Programs"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        CType(Me.c1Screenings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlWebBrowser.ResumeLayout(False)
        Me.pnlWait.ResumeLayout(False)
        Me.pnlWait.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub frmVWPTProtocol_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load


        'Checked for save  to DMS Setting and enable or disable  save to dms button.
        If gblPDMPSaveToDMS Then
            tblbtn_Save_32.Enabled = True
        Else
            tblbtn_Save_32.Enabled = False
            tblbtn_Save_32.ToolTipText = ""
        End If

        Me.LoadPatientStripControl()
        Me.LoadPDMPrograms()

    End Sub
    Private Sub LoadPatientStripControl()
        Try
            If IsNothing(PatientStripControl) Then
                PatientStripControl = New gloUserControlLibrary.gloUC_PatientStrip

                With PatientStripControl
                    .Dock = DockStyle.Top
                    .Padding = New Padding(3, 0, 3, 0)
                    .ShowDetail(nPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.RxFillNotifications)
                    .BringToFront()
                End With
                Me.Controls.Add(PatientStripControl)
                pnlToolStrip.SendToBack()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub LoadPDMPrograms()
        Try
            Dim dt As New DataTable
            Dim objurl As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}
            dt = objurl.GetPdmpPrograms(nPatientID, gnLoginProviderID)

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "View PDMP Programs", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, gloAuditTrail.SoftwareComponent.gloEMR)
            ' c1Screenings.Enabled = False
            c1Screenings.DataSource = dt
            c1Screenings.Enabled = True

            SetGridStyle()

            Try
                gloPatient.gloPatient.GetWindowTitle(Me, nPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try

            If TypeOf (c1Screenings.DataSource) Is DataTable Then
                Dim dt As DataTable
                Dim dv As DataView
                dt = c1Screenings.DataSource
                dv = dt.DefaultView

                c1Screenings.DataSource = dv
                With c1Screenings
                    .AllowSorting = True
                    .ExtendLastCol = True

                    .Redraw = False
                    .Dock = DockStyle.Fill
                    Dim _TotalWidth As Single = 0
                    _TotalWidth = Screen.PrimaryScreen.WorkingArea.Width - 40
                    c1Screenings.Width = _TotalWidth

                    c1Screenings.ShowCellLabels = False
                    Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                    .Cols.Count = Col_Count
                    .Rows.Fixed = 1
                    .Styles.ClearUnused()
                    .AllowResizing = True


                    .Cols(Col_ProviderName).Width = _TotalWidth * 0.33
                    .Cols(Col_ProviderName).AllowEditing = False
                    .Cols(Col_ProviderName).Visible = True
                    .Cols(Col_ProviderName).Caption = "Provider Name"
                    .Cols(Col_ProviderName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter



                    .Cols(Col_ResponseDate).Width = _TotalWidth * 0.33
                    .Cols(Col_ResponseDate).AllowEditing = False
                    .Cols(Col_ResponseDate).Visible = True
                    .Cols(Col_ResponseDate).Caption = "Response Date"
                    .Cols(Col_ResponseDate).DataType = GetType(System.DateTime)
                    .Cols(Col_ResponseDate).Format = "MM/dd/yyyy h:mm tt"


                    .Cols(Col_ReportID).Width = _TotalWidth * 0.33
                    .Cols(Col_ReportID).AllowEditing = False
                    .Cols(Col_ReportID).Visible = False
                    .Cols(Col_ReportID).Caption = "Report ID"
                    .Cols(Col_ReportID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                    .Redraw = True
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub


    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub frmViewScreenings_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetGridStyle()
    End Sub

    Private Sub c1PTProtocol_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Screenings.MouseClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo = c1Screenings.HitTest(ptPoint)

        If htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.ColumnHeader Then

            '''''''''''''''''''''''''''''''''''''
        ElseIf htInfo.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell Then
            OpenPdmprograms()
        Else
            Exit Sub
        End If
    End Sub

    Dim nReportID As Long = 0

    Private Sub OpenPdmprograms()

        Dim narcoxmlstring As String = ""
        Dim dtPDMP As DataTable

        Try
            nReportID = Convert.ToInt64(c1Screenings.Item(c1Screenings.RowSel, Me.Col_ReportID))

            Dim PDMPService As New gloRxHub.PDMP.PDMP(PDMPUsername, PDMPPassword) With {.ConnectionString = GetConnectionString(), .WebURL = PDMPServiceURL}

            dtPDMP = PDMPService.GetNarcoticsHTML(nReportID)

            If dtPDMP.Rows.Count > 0 Then
                If IsDBNull(dtPDMP.Rows(0).Item("sContent")) = False Then
                    'Dim sFilePath As String = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".html", "MMddyyyyHHmmssffff")

                    narcoxmlstring = dtPDMP.Rows(0).Item("sContent")

                    If File.Exists(sFilePath) Then
                        File.Delete(sFilePath)
                    End If
                    File.WriteAllText(sFilePath, narcoxmlstring)
                    WebBrowser1.Navigate(sFilePath)

                    pnlWebBrowser.Visible = True
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "PDMP Report View from pdmp program", nPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog("View Screening MouseClick " + ex.Message.ToString(), False)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, "Viewed pdmpprograms", nPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub WebBrowser1_Navigating(sender As System.Object, e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating
        pnlWait.Visible = True
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As System.Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        pnlWait.Visible = False
        bIsDocumentLoaded = True
    End Sub

    Private Function CheckBatchPrintProcessRunning() As Boolean
        Try
            Dim oFrm = System.Windows.Forms.Application.OpenForms.OfType(Of frmgloPrintPrintInfoController).FirstOrDefault()

            If oFrm IsNot Nothing Then
                Dim dg As DialogResult = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (dg = DialogResult.Yes) Then
                    oFrm.Close()
                    Return False
                Else
                    oFrm.Visible = True
                    Return True
                End If
            End If

            Return False
        Catch ex As Exception
            ex = Nothing
            Return False
        End Try
    End Function
    Private Function SaveToPDF() As Boolean
        Dim oDocManager As eDocManager.eDocManager = New gloEDocumentV3.eDocManager.eDocManager()
        Dim oSourceDocuments As New ArrayList()
        Dim oDialogContainerID As Long = 0
        Dim oDialogDocumentID As Long = 0
        Dim sDMSFileName As String = ""
        Dim nDocumentID As Int32 = 0
        Dim nDestinationCategoryID As Int32 = 0
        Dim dtCategories As New DataTable()
        Dim bReturned As Boolean = False
        Try
            Dim DMSConnectionstring As String = GetDMSConnectionString()

            Using eDoc As New gloEDocumentV3.eDocManager.eDocGetList()
                eDoc.GetCategories(gnClinicID, dtCategories)
            End Using

            If dtCategories IsNot Nothing AndAlso dtCategories.Rows.Count() > 0 Then
                If dtCategories.AsEnumerable().Any(Function(p) Convert.ToString(p("CategoryName")).ToUpper() = "PDMP") Then
                    nDestinationCategoryID = Convert.ToInt32(dtCategories.AsEnumerable().FirstOrDefault(Function(p) Convert.ToString(p("CategoryName")).ToUpper() = "PDMP")("CategoryID"))

                    Dim selectPDF As New SelectPdf.HtmlToPdf()
                    Dim selectPDFFile As SelectPdf.PdfDocument = selectPDF.ConvertUrl(sFilePath)

                    If File.Exists(sFilePathPDF) Then
                        File.Delete(sFilePathPDF)
                    End If

                    selectPDFFile.Save(sFilePathPDF)
                    selectPDFFile.Close()

                    Using pbDocument As New ProgressBar() With {.Minimum = 0, .Maximum = 100}
                        oDocManager.SetSettings(GetConnectionString(), DMSConnectionstring, gloEMRGeneralLibrary.gloGeneral.clsgeneral.gDMSV3TempPath + "DMSLogFile.txt", gloSettings.FolderSettings.AppTempFolderPath + "DMSV2Temp")
                        oSourceDocuments.Add(sFilePathPDF)
                        sDMSFileName = eDocManager.eDocValidator.GetNewDocumentName(nPatientID, "PDMP", gnClinicID, Enumeration.enum_OpenExternalSource.None)
                        bReturned = oDocManager.ImportSplit(nPatientID, oSourceDocuments, sDMSFileName, nDestinationCategoryID, "PDMP", "", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), gnClinicID, oDialogContainerID, oDialogDocumentID, True, pbDocument, Enumeration.enum_OpenExternalSource.RxMeds)
                    End Using

                    dtCategories.Dispose()
                    dtCategories = Nothing

                    selectPDF = Nothing
                    selectPDFFile = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally
            If oSourceDocuments IsNot Nothing Then
                oSourceDocuments.Clear()
                oSourceDocuments = Nothing
            End If
        End Try

        Return bReturned
    End Function

    Private myPrinterSetting As System.Drawing.Printing.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
    Dim OldPrinterName As String
    Private Sub Print()
        Try
            If CheckBatchPrintProcessRunning() = False Then

                Using oDialog As gloPrintDialog.gloPrintDialog = New gloPrintDialog.gloPrintDialog(True)
                    oDialog.ConnectionString = gloEMRDatabase.DataBaseLayer.ConnectionString
                    oDialog.TopMost = True
                    oDialog.ModuleName = "PrintInfoDocuments"
                    oDialog.RegistryModuleName = "PrintInfoDocuments"
                    oDialog.ShowPrinterProfileDialog = True

                    If Not gloGlobal.gloTSPrint.isCopyPrint Then
                        Try
                            OldPrinterName = myPrinterSetting.PrinterName
                        Catch
                        End Try

                        If oDialog IsNot Nothing Then
                            oDialog.PrinterSettings = myPrinterSetting
                            oDialog.AllowSomePages = True
                            oDialog.bUseDefaultPrinter = True
                            oDialog.PrinterSettings.ToPage = 1
                            oDialog.PrinterSettings.FromPage = 1
                            oDialog.PrinterSettings.MaximumPage = 1
                            oDialog.PrinterSettings.MinimumPage = 1
                        End If
                    End If

                    oDialog.AllowSomePages = True
                    oDialog.bUseDefaultPrinter = gblnUseDefaultPrinter
                    If oDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        If (oDialog.bUseDefaultPrinter = True) Then
                            oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = True
                            oDialog.CustomPrinterExtendedSettings.IsShowProgress = True
                        End If

                        Dim ogloPrintProgressController As frmgloPrintPrintInfoController = New frmgloPrintPrintInfoController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, Nothing)
                        ogloPrintProgressController.OldPrinterName = OldPrinterName

                        ogloPrintProgressController.InfoButtonWebBrowser = WebBrowser1
                        ogloPrintProgressController._databaseConnectionString = gloEMRDatabase.DataBaseLayer.ConnectionString
                        If oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint Then
                            If oDialog.CustomPrinterExtendedSettings.IsShowProgress Then
                                ogloPrintProgressController.Show()
                            Else
                                ogloPrintProgressController.Show()
                            End If
                        Else
                            ogloPrintProgressController.TopMost = True
                            ogloPrintProgressController.ShowInTaskbar = False
                            ogloPrintProgressController.ShowDialog()
                            If ogloPrintProgressController IsNot Nothing Then
                                ogloPrintProgressController.Dispose()
                            End If

                            ogloPrintProgressController = Nothing
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, False)
            ex = Nothing
        Finally
        End Try
    End Sub

    Private Sub ts_btnPrint_Click(sender As System.Object, e As System.EventArgs)
        Try
            Print()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, "PDMP Document printed", nPatientID, nReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, ex.ToString(), nPatientID, nReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
        End Try
    End Sub



    Private Sub tblbtn_Save_32_Click(sender As System.Object, e As System.EventArgs) Handles tblbtn_Save_32.Click
        Try
            If bIsDocumentLoaded Then
                If SaveToPDF() Then
                    MessageBox.Show("PMP report saved successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.Save, "Narx care report saved successfully.", nPatientID, nReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    MessageBox.Show("PMP report could not be saved successfully.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.Save, "Narx care report could not be saved successfully.", nPatientID, nReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPView, gloAuditTrail.ActivityType.View, ex.ToString(), nPatientID, nReportID, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        End Try
    End Sub
End Class
