Public Class frmMSTOBPlan
    Inherits System.Windows.Forms.Form

    Public _CategoryID As Long
    Public _Description As String = ""

    Public _SelectedCategoty = ""
    
    
    Private Col_Check As Integer = 0
    Private Col_Name As Integer = 1
    Private Col_DGCustCnt As Integer = 2

    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public _Comments As String = ""
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents lbl_HistoryCaregory As System.Windows.Forms.Label
    Friend WithEvents cmb_HistoryCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents imgTreeVIew As System.Windows.Forms.ImageList
    Public _DialogResult As Windows.Forms.DialogResult = Windows.Forms.DialogResult.Cancel
    Public nICDType As Integer = 9 ''parameter added for ICD10 implementation
    Private dtFillHistoryCategory As DataTable
    Private blnIsSystemDefined As Boolean



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal lnCategoryID As Long, Optional ByVal strHistory As String = "", Optional ByVal strCommets As String = "")
        MyBase.New()
        CategoryId = lnCategoryID
        _Description = strHistory
        _Comments = strCommets
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal lnCategoryID As Long, ByVal lnHistoryId As Long)
        MyBase.New()
        CategoryId = lnCategoryID
        HistoryId = lnHistoryId
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Private objOBPlabDBLayer As New clsOBPlanDBLayer
    Private CategoryId As Long
    Private HistoryId As Long
    Private errprovider As New ErrorProvider

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTOBPlan))
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmb_HistoryCategory = New System.Windows.Forms.ComboBox()
        Me.lbl_HistoryCaregory = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnl_tlsp = New System.Windows.Forms.Panel()
        Me.tlsp = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.imgTreeVIew = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtComments
        '
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(179, 76)
        Me.txtComments.MaxLength = 255
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(390, 122)
        Me.txtComments.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(102, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Comments :"
        '
        'txtDescription
        '
        Me.txtDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(179, 45)
        Me.txtDescription.MaxLength = 255
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(390, 22)
        Me.txtDescription.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(70, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Item Description :"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.cmb_HistoryCategory)
        Me.Panel2.Controls.Add(Me.lbl_HistoryCaregory)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.txtComments)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtDescription)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(637, 233)
        Me.Panel2.TabIndex = 4
        '
        'cmb_HistoryCategory
        '
        Me.cmb_HistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_HistoryCategory.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmb_HistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmb_HistoryCategory.FormattingEnabled = True
        Me.cmb_HistoryCategory.Location = New System.Drawing.Point(179, 14)
        Me.cmb_HistoryCategory.Name = "cmb_HistoryCategory"
        Me.cmb_HistoryCategory.Size = New System.Drawing.Size(390, 22)
        Me.cmb_HistoryCategory.TabIndex = 209
        '
        'lbl_HistoryCaregory
        '
        Me.lbl_HistoryCaregory.AutoSize = True
        Me.lbl_HistoryCaregory.BackColor = System.Drawing.Color.Transparent
        Me.lbl_HistoryCaregory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HistoryCaregory.Location = New System.Drawing.Point(70, 16)
        Me.lbl_HistoryCaregory.Name = "lbl_HistoryCaregory"
        Me.lbl_HistoryCaregory.Size = New System.Drawing.Size(105, 14)
        Me.lbl_HistoryCaregory.TabIndex = 208
        Me.lbl_HistoryCaregory.Text = "OB Plan Category :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(57, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 214
        Me.Label4.Text = "*"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(58, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "*"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 229)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(629, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 226)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(633, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 226)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(631, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(637, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp
        '
        Me.tlsp.BackColor = System.Drawing.Color.Transparent
        Me.tlsp.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp.Location = New System.Drawing.Point(0, 0)
        Me.tlsp.Name = "tlsp"
        Me.tlsp.Size = New System.Drawing.Size(637, 53)
        Me.tlsp.TabIndex = 0
        Me.tlsp.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'imgTreeVIew
        '
        Me.imgTreeVIew.ImageStream = CType(resources.GetObject("imgTreeVIew.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeVIew.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeVIew.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeVIew.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeVIew.Images.SetKeyName(2, "Defination.ico")
        '
        'frmMSTOBPlan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(637, 286)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTOBPlan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OB Plan"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp.ResumeLayout(False)
        Me.tlsp.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property IsSystemDefined As Boolean
        Get
            Return blnIsSystemDefined
        End Get
        Set(value As Boolean)
            blnIsSystemDefined = value
        End Set
    End Property

    Private Sub HistoryMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            If Not dtFillHistoryCategory Is Nothing Then
                dtFillHistoryCategory.Dispose()
                dtFillHistoryCategory = Nothing
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try


    End Sub

    Private Sub HistoryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            FillHistoryCategory()

            If HistoryId <> 0 Then
                Try
                    Dim arrlist As ArrayList
                    arrlist = objOBPlabDBLayer.FetchDataForUpdate(HistoryId, cmb_HistoryCategory.SelectedValue)
                    If arrlist.Count > 0 Then
                        txtDescription.Text = CType(arrlist.Item(0), System.String)
                        txtComments.Text = CType(arrlist.Item(1), System.String)

                      


                    End If

                Catch ex As SqlClient.SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
                End Try
            Else
                txtDescription.Text = _Description
                txtComments.Text = _Comments

               
            End If

            If blnIsSystemDefined = True Then
                cmb_HistoryCategory.Enabled = False
                txtDescription.Enabled = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub

    Private Sub FillHistoryCategory()

        Try


            dtFillHistoryCategory = objOBPlabDBLayer.FillControls()
            cmb_HistoryCategory.ValueMember = dtFillHistoryCategory.Columns(0).ToString
            cmb_HistoryCategory.DisplayMember = dtFillHistoryCategory.Columns(1).ToString()
            cmb_HistoryCategory.DataSource = dtFillHistoryCategory

            If Not _SelectedCategoty = "" Then
                cmb_HistoryCategory.Text = _SelectedCategoty
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub

    Private Sub CloseHistory()
        'errprovider.SetError(txtDescription, "")
        _Description = ""
        objOBPlabDBLayer = Nothing
        Me.Close()
    End Sub

    Private Sub SaveHistory()

        Dim formclose As Boolean = True

        If cmb_HistoryCategory.Text = "Medical Condition" Or cmb_HistoryCategory.Text = "Coded History" Then
            MessageBox.Show("You cannot Add Items to System defined categories.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else

            If Trim(txtDescription.Text) = "" Then
                'errprovider.SetError(txtDescription, "Description is Required")
                MessageBox.Show("Description is Required", "History", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescription.Focus()
                Exit Sub
            Else
                'errprovider.SetError(txtDescription, "")
                If Not objOBPlabDBLayer.ValidateDescription(cmb_HistoryCategory.SelectedValue, Trim(txtDescription.Text), HistoryId) Then
                    'errprovider.SetError(txtDescription, "Duplicate Description")
                    MessageBox.Show("Duplicate Description", "History", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtDescription.Focus()
                    Exit Sub
                Else
                    'errprovider.SetError(txtDescription, "")
                End If
            End If



            If HistoryId = 0 Then
                Try
                    ''IcdType ''parameter added for ICD10 implementation
                    If objOBPlabDBLayer.AddData(Trim(txtDescription.Text), Trim(txtComments.Text), cmb_HistoryCategory.SelectedValue) <> 0 Then
                        _Description = Trim(txtDescription.Text)
                        _Comments = Trim(txtComments.Text)
                        _CategoryID = cmb_HistoryCategory.SelectedValue
                    Else
                        _Description = ""
                    End If

                Catch ex As SqlClient.SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
                Finally
                    objOBPlabDBLayer = Nothing
                    _DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End Try
            Else
                Try



                    ''IcdType ''parameter added for ICD10 implementation
                    objOBPlabDBLayer.UpdateData(txtDescription.Text, txtComments.Text, HistoryId, cmb_HistoryCategory.SelectedValue)
                    _CategoryID = cmb_HistoryCategory.SelectedValue

                Catch ex As SqlClient.SqlException
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
                Finally
                    If formclose = True Then
                        objOBPlabDBLayer = Nothing
                        _DialogResult = Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If

                End Try
            End If
        End If
    End Sub

    Private Sub tlsp_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString().ToUpper()
                Case UCase("Save")
                    SaveHistory()

                Case UCase("Close")
                    CloseHistory()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

   
End Class
