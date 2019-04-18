Imports gloEMRGeneralLibrary.gloEMRLab
Public Class frmLab_ViewAcknoledgement
    Inherits System.Windows.Forms.Form

#Region " Windows Controls "
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_Lab_ViewAcknoledgement As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtReviewed As System.Windows.Forms.TextBox
    Private WithEvents Label12 As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal _OrderID As Long, ByVal _OrderNumberPrefix As String, ByVal _OrderNumberID As Long)
        MyBase.New()
        OrderId = _OrderID
        OrderNumberPrefix = _OrderNumberPrefix
        OrderNumberID = _OrderNumberID
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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents ImgList_Pages As System.Windows.Forms.ImageList
    Friend WithEvents pnlFileName As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_ViewAcknoledgement))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlDocument = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtuser = New System.Windows.Forms.TextBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlFileName = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.ImgList_Pages = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_Lab_ViewAcknoledgement = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.txtReviewed = New System.Windows.Forms.TextBox
        Me.pnlMain.SuspendLayout()
        Me.pnlDocument.SuspendLayout()
        Me.pnlFileName.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_Lab_ViewAcknoledgement.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlDocument)
        Me.pnlMain.Controls.Add(Me.pnlFileName)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(433, 200)
        Me.pnlMain.TabIndex = 48
        '
        'pnlDocument
        '
        Me.pnlDocument.Controls.Add(Me.txtReviewed)
        Me.pnlDocument.Controls.Add(Me.Label5)
        Me.pnlDocument.Controls.Add(Me.Label6)
        Me.pnlDocument.Controls.Add(Me.Label7)
        Me.pnlDocument.Controls.Add(Me.Label8)
        Me.pnlDocument.Controls.Add(Me.txtuser)
        Me.pnlDocument.Controls.Add(Me.txtComments)
        Me.pnlDocument.Controls.Add(Me.Label4)
        Me.pnlDocument.Controls.Add(Me.Label3)
        Me.pnlDocument.Controls.Add(Me.Label2)
        Me.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument.Location = New System.Drawing.Point(0, 30)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDocument.Size = New System.Drawing.Size(433, 170)
        Me.pnlDocument.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(425, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 166)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(429, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 166)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(427, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'txtuser
        '
        Me.txtuser.BackColor = System.Drawing.Color.White
        Me.txtuser.ForeColor = System.Drawing.Color.Black
        Me.txtuser.Location = New System.Drawing.Point(111, 13)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.ReadOnly = True
        Me.txtuser.Size = New System.Drawing.Size(307, 22)
        Me.txtuser.TabIndex = 6
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.White
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(111, 83)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(307, 73)
        Me.txtComments.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Location = New System.Drawing.Point(24, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Comments :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(56, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Reviewed By :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFileName
        '
        Me.pnlFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFileName.Controls.Add(Me.Panel1)
        Me.pnlFileName.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFileName.Location = New System.Drawing.Point(0, 0)
        Me.pnlFileName.Name = "pnlFileName"
        Me.pnlFileName.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlFileName.Size = New System.Drawing.Size(433, 30)
        Me.pnlFileName.TabIndex = 42
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(427, 24)
        Me.Panel1.TabIndex = 45
        '
        'txtFileName
        '
        Me.txtFileName.BackColor = System.Drawing.Color.White
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtFileName.ForeColor = System.Drawing.Color.Black
        Me.txtFileName.Location = New System.Drawing.Point(108, 1)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(307, 22)
        Me.txtFileName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "  Order Name :   "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(1, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(425, 1)
        Me.Label9.TabIndex = 12
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
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(426, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(427, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'ImgList_Pages
        '
        Me.ImgList_Pages.ImageStream = CType(resources.GetObject("ImgList_Pages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList_Pages.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList_Pages.Images.SetKeyName(0, "")
        Me.ImgList_Pages.Images.SetKeyName(1, "")
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_Lab_ViewAcknoledgement)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(433, 54)
        Me.pnl_tlsp.TabIndex = 50
        '
        'tlsp_Lab_ViewAcknoledgement
        '
        Me.tlsp_Lab_ViewAcknoledgement.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Lab_ViewAcknoledgement.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_Lab_ViewAcknoledgement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Lab_ViewAcknoledgement.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Lab_ViewAcknoledgement.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Lab_ViewAcknoledgement.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk})
        Me.tlsp_Lab_ViewAcknoledgement.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Lab_ViewAcknoledgement.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Lab_ViewAcknoledgement.Name = "tlsp_Lab_ViewAcknoledgement"
        Me.tlsp_Lab_ViewAcknoledgement.Size = New System.Drawing.Size(433, 53)
        Me.tlsp_Lab_ViewAcknoledgement.TabIndex = 0
        Me.tlsp_Lab_ViewAcknoledgement.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnOk.Tag = "Ok"
        Me.ts_btnOk.Text = "&Close"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Close"
        '
        'txtReviewed
        '
        Me.txtReviewed.BackColor = System.Drawing.Color.White
        Me.txtReviewed.ForeColor = System.Drawing.Color.Black
        Me.txtReviewed.Location = New System.Drawing.Point(111, 48)
        Me.txtReviewed.Name = "txtReviewed"
        Me.txtReviewed.ReadOnly = True
        Me.txtReviewed.Size = New System.Drawing.Size(161, 22)
        Me.txtReviewed.TabIndex = 13
        '
        'frmLab_ViewAcknoledgement
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(433, 254)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_ViewAcknoledgement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab View Acknowledgment"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlDocument.ResumeLayout(False)
        Me.pnlDocument.PerformLayout()
        Me.pnlFileName.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_Lab_ViewAcknoledgement.ResumeLayout(False)
        Me.tlsp_Lab_ViewAcknoledgement.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private OrderId As Long
    Private OrderNumberPrefix As String
    Private OrderNumberID As Long
    Public _ViewedDocumentPath As String = ""
    Public _ViewedDocumentDispName As String = ""

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub frmLab_ViewAcknoledgement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim _DocumentPath As String
        Try


            txtFileName.Text = OrderNumberPrefix & "-" & OrderNumberID
            'Panel2.Text = ""
            'dtpReviwed.Value = Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt")

            Dim oDB As gloStream.gloDataBase.gloDataBase

            Dim dtAcw As DataTable
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)

            dtAcw = oDB.ReadQueryDataTable("SELECT nUserID,ReviewDatetime,Comments FROM Lab_Acknowledgment Where nOrderId = " & OrderId & " and  nOrderNumberPrefix = '" & OrderNumberPrefix & "' and nOrderNumberID = " & OrderNumberID & " ")
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing




            If (IsNothing(dtAcw) = False) Then



                oDB = New gloStream.gloDataBase.gloDataBase
                Dim strLoginName As String
                oDB.Connect(GetConnectionString)
                strLoginName = oDB.ExecuteQueryScaler("SELECT sLoginName FROM User_MST WHERE  nUserID = " & dtAcw.Rows(0)("nUserID") & " ")

                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
                'cmbUsers.Items.Add(strLoginName)
                'cmbUsers.SelectedIndex = 0
                txtuser.Text = strLoginName
                'dtpReviwed.Value = Format(dtAcw.Rows(0)("ReviewDatetime"), "MM/dd/yyyy hh:mm:ss tt")
                txtReviewed.Text = Format(dtAcw.Rows(0)("ReviewDatetime"), "MM/dd/yyyy hh:mm:ss tt")

                txtComments.Text = dtAcw.Rows(0)("Comments")
                dtAcw.Dispose()
                dtAcw = Nothing
            End If

            '4. Progress Bar 
            'ProgressBar1.Minimum = 0
            'ProgressBar1.Maximum = 100
            'ProgressBar1.Value = 0
            'ProgressBar1.Enabled = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub tlsp_Lab_ViewAcknoledgement_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_Lab_ViewAcknoledgement.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Ok"
                    Me.Close()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub

End Class
