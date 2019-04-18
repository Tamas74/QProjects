Public Class frmMSTICD9
    Inherits System.Windows.Forms.Form

    '''' While Add, return this Paramerters to View-From so the Newly Added Records get Highlighted 
    Public _ICD9Code As String
    Public _SpecialityID As Long
    ''''
    Private m_ID As Long
    Private m_SpecialityName As String
    Private objclsICD9 As New clsICD9
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MSTICD9 As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Public CancelClick As Boolean
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
    Public Sub New(ByVal SpecialityName As String)
        MyBase.New()
        m_SpecialityName = SpecialityName
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
    Friend WithEvents txtICD9Code As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMSTICD9))
        Me.cmbSpeciality = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtICD9Code = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_MSTICD9 = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel2.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MSTICD9.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbSpeciality
        '
        Me.cmbSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpeciality.ForeColor = System.Drawing.Color.Black
        Me.cmbSpeciality.Location = New System.Drawing.Point(95, 96)
        Me.cmbSpeciality.Name = "cmbSpeciality"
        Me.cmbSpeciality.Size = New System.Drawing.Size(247, 22)
        Me.cmbSpeciality.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(29, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Specialty :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(95, 46)
        Me.txtDescription.MaxLength = 255
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(247, 40)
        Me.txtDescription.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(16, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Description :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtICD9Code
        '
        Me.txtICD9Code.ForeColor = System.Drawing.Color.Black
        Me.txtICD9Code.Location = New System.Drawing.Point(95, 15)
        Me.txtICD9Code.MaxLength = 50
        Me.txtICD9Code.Name = "txtICD9Code"
        Me.txtICD9Code.Size = New System.Drawing.Size(247, 22)
        Me.txtICD9Code.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(15, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 14)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "ICD9 Code : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Controls.Add(Me.cmbSpeciality)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.txtDescription)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.txtICD9Code)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(363, 133)
        Me.Panel2.TabIndex = 11
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 129)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(355, 1)
        Me.lbl_pnlBottom.TabIndex = 14
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 126)
        Me.lbl_pnlLeft.TabIndex = 13
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(359, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 126)
        Me.lbl_pnlRight.TabIndex = 12
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(357, 1)
        Me.lbl_pnlTop.TabIndex = 11
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MSTICD9)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.pnl_tlsp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(363, 53)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_MSTICD9
        '
        Me.tlsp_MSTICD9.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MSTICD9.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTICD9.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTICD9.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTICD9.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MSTICD9.Name = "tlsp_MSTICD9"
        Me.tlsp_MSTICD9.Size = New System.Drawing.Size(363, 53)
        Me.tlsp_MSTICD9.TabIndex = 0
        Me.tlsp_MSTICD9.Text = "toolStrip1"
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
        'frmMSTICD9
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(363, 186)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMSTICD9"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IDC9"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MSTICD9.ResumeLayout(False)
        Me.tlsp_MSTICD9.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmICD9_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Fill_Speciality()
            If m_ID <> 0 Then
                Fill_ICD9(m_ID)
            Else
                cmbSpeciality.Text = m_SpecialityName
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fill_ICD9(ByVal ID As Long)
        Dim dv As New DataView
        objclsICD9.SelectICD9(ID)
        dv = objclsICD9.GetDataview
        txtICD9Code.Text = dv.Item(0)(0).ToString
        txtDescription.Text = dv.Item(0)(1).ToString
        cmbSpeciality.SelectedValue = dv.Item(0)(2)
    End Sub

    Private Sub Fill_Speciality()
        Dim dt As DataTable = objclsICD9.GetAllSpeciality()
        If IsNothing(dt) = False Then
            If dt.Rows.Count > 0 Then
                cmbSpeciality.DataSource = dt
                cmbSpeciality.ValueMember = dt.Columns(0).ColumnName
                cmbSpeciality.DisplayMember = dt.Columns(1).ColumnName
                cmbSpeciality.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub SaveMSTICD9()
        Dim objfrmViewICD9 As New frmVWICD9
        Try
            If Trim(txtICD9Code.Text) = "" Then
                MessageBox.Show("ICD9 Code must be Entered", "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtICD9Code.Focus()
                Exit Sub
            End If
            If Trim(txtDescription.Text) = "" Then
                MessageBox.Show("ICD9 Code must have Description", "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescription.Focus()
                Exit Sub
            End If
            If cmbSpeciality.Text = "" Then
                MessageBox.Show("ICD9 Code must have Specialty", "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSpeciality.Focus()
                Exit Sub
            End If
            'If objclsICD9.CheckDuplicate(m_ID, Trim(txtICD9Code.Text), cmbSpeciality.SelectedValue) = True Then
            '    MessageBox.Show("ICD9 Code for this Specialty already exists", "CPT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    txtICD9Code.Focus()
            '    Exit Sub
            'End If

            'If m_ID = 0 Then
            objclsICD9.AddNewICD9(m_ID, Trim(txtICD9Code.Text), Trim(txtDescription.Text), cmbSpeciality.SelectedValue)
            _ICD9Code = Trim(txtICD9Code.Text)
            _SpecialityID = cmbSpeciality.SelectedValue

            CancelClick = False
            'Else
            'objclsICD9.UpdateICD9(m_ID, txtICD9Code.Text, txtDescription.Text, cmbSpeciality.SelectedValue)
            'End If
            'objfrmViewICD9.grdICD9.DataSource = objclsICD9.GetAllICD
            'objfrmViewICD9.CustomGridStyle()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ICD9", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmViewICD9 = Nothing
        End Try
    End Sub

    Private Sub CloseMSTICD9()
        CancelClick = True
        Me.Close()
    End Sub

    Private Sub tlsp_MSTICD9_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MSTICD9.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString().ToUpper()
                Case UCase("Save")
                    SaveMSTICD9()

                Case UCase("Close")
                    CloseMSTICD9()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
