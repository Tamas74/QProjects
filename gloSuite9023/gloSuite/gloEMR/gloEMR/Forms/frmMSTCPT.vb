Public Class frmMSTCPT
    Inherits System.Windows.Forms.Form

    '' While Add, return this CPTCode to View-From so the Newly Added CPTs get Highlighted 
    Public _CPTCode As String
    Public _CategoryID As Long
    ''''

    Private m_ID As Long
    Private m_CategoryType As String
    Private m_CategoryID As Long
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MSTCPT As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Public CancelClick As Boolean

    'Private m_Caption As String
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal ID As Long)
        MyBase.New()
        m_ID = ID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub
    Public Sub New(ByVal CategoryID As Long, ByVal CategoryType As String)
        MyBase.New()
        m_CategoryType = CategoryType
        m_CategoryID = CategoryID
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
    Friend WithEvents cmbSpeciality As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCPTCode As System.Windows.Forms.TextBox
    Dim objclsCPT As New clsCPT
    Dim dt As DataTable
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTCPT))
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbSpeciality = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCPTCode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_MSTCPT = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MSTCPT.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbCategory.Location = New System.Drawing.Point(104, 132)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(229, 22)
        Me.cmbCategory.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSpeciality
        '
        Me.cmbSpeciality.ForeColor = System.Drawing.Color.Black
        Me.cmbSpeciality.Location = New System.Drawing.Point(104, 94)
        Me.cmbSpeciality.Name = "cmbSpeciality"
        Me.cmbSpeciality.Size = New System.Drawing.Size(229, 22)
        Me.cmbSpeciality.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Specialty :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(104, 56)
        Me.txtDescription.MaxLength = 60
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(229, 22)
        Me.txtDescription.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Description :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCPTCode
        '
        Me.txtCPTCode.ForeColor = System.Drawing.Color.Black
        Me.txtCPTCode.Location = New System.Drawing.Point(104, 18)
        Me.txtCPTCode.MaxLength = 10
        Me.txtCPTCode.Name = "txtCPTCode"
        Me.txtCPTCode.Size = New System.Drawing.Size(229, 22)
        Me.txtCPTCode.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "CPT Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbCategory)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtCPTCode)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbSpeciality)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(356, 172)
        Me.Panel1.TabIndex = 13
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 168)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(348, 1)
        Me.lbl_pnlBottom.TabIndex = 16
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 165)
        Me.lbl_pnlLeft.TabIndex = 15
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(352, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 165)
        Me.lbl_pnlRight.TabIndex = 14
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(350, 1)
        Me.lbl_pnlTop.TabIndex = 13
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MSTCPT)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(356, 53)
        Me.pnl_tlsp.TabIndex = 15
        '
        'tlsp_MSTCPT
        '
        Me.tlsp_MSTCPT.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTCPT.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsp_MSTCPT.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTCPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTCPT.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTCPT.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTCPT.Name = "tlsp_MSTCPT"
        Me.tlsp_MSTCPT.Size = New System.Drawing.Size(356, 53)
        Me.tlsp_MSTCPT.TabIndex = 0
        Me.tlsp_MSTCPT.Text = "toolStrip1"
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
        'frmMSTCPT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(356, 225)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTCPT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CPT"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MSTCPT.ResumeLayout(False)
        Me.tlsp_MSTCPT.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmMSTCPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Fill_Speciality()
            Fill_Category()
            If m_ID <> 0 Then
                Fill_CPT(m_ID)
            Else
                cmbCategory.Text = m_CategoryType
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Fill_CPT(ByVal ID As Long)
        'Dim objclsCPT As New clsCPT
        Dim dv As New DataView
        objclsCPT.SelectCPT(ID)
        dv = objclsCPT.GetDataview
        txtCPTCode.Text = dv.Item(0)(0).ToString
        txtDescription.Text = dv.Item(0)(1).ToString
        cmbSpeciality.SelectedValue = dv.Item(0)(2)
        cmbCategory.SelectedValue = dv.Item(0)(3)
    End Sub

    Private Sub Fill_Speciality()
        dt = objclsCPT.GetAllSpeciality()
        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                cmbSpeciality.DataSource = dt
                cmbSpeciality.ValueMember = dt.Columns(0).ColumnName
                cmbSpeciality.DisplayMember = dt.Columns(1).ColumnName
                cmbSpeciality.SelectedIndex = 0
            End If
        End If
    End Sub

    Public Sub Fill_Category()

        Dim dt As DataTable = objclsCPT.GetAllCategory()
        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                cmbCategory.DataSource = dt
                cmbCategory.ValueMember = dt.Columns(0).ColumnName
                cmbCategory.DisplayMember = dt.Columns(1).ColumnName

                cmbCategory.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub SaveMSTCPT()

        'Dim objclsCPT As New clsCPT()
        Dim objfrmViewCPT As New frmVWCPT
        Try
            If Trim(txtCPTCode.Text) = "" Then
                MessageBox.Show("CPT Code must be Entered", "CPT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCPTCode.Focus()
                Exit Sub
            End If
            If Trim(cmbCategory.Text) = "" Then
                MessageBox.Show("CPT Code must Have Category", "CPT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCPTCode.Focus()
                Exit Sub
            End If
            If objclsCPT.CheckDuplicate(m_ID, Trim(txtCPTCode.Text), cmbCategory.SelectedValue) = True Then
                MessageBox.Show("CPT Code For This Category Already Exists", "CPT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtCPTCode.Focus()
                Exit Sub
            End If

            objclsCPT.AddNewCPT(m_ID, Trim(txtCPTCode.Text), Trim(txtDescription.Text), cmbSpeciality.SelectedValue, cmbCategory.SelectedValue)
            CancelClick = False

            _CPTCode = Trim(txtCPTCode.Text)
            _CategoryID = cmbCategory.SelectedValue
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmViewCPT = Nothing
        End Try
    End Sub

    'Public Function GetCategoryID(ByVal CategoryID As Long) As Long
    '    Return CategoryID
    'End Function

    Private Sub CloseMSTCPT()

        Try
            CancelClick = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmMSTCPT_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            _CPTCode = Trim(txtCPTCode.Text)
            _CategoryID = cmbCategory.SelectedValue
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tlsp_MSTCPT_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTCPT.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveMSTCPT()

                Case "Close"
                    CloseMSTCPT()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try

    End Sub
End Class
