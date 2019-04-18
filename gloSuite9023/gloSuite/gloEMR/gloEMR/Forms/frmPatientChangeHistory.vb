Public Class frmPatientChangeHistory
    Inherits System.Windows.Forms.Form
    Dim _PatientID As Long
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        _PatientID = PatientID
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
            Try

                If (IsNothing(grdPatientHistory) = False) Then
                    grdPatientHistory.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdPatientHistory)
                    grdPatientHistory.Dispose()
                    grdPatientHistory = Nothing
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
    Friend WithEvents grdPatientHistory As System.Windows.Forms.DataGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblViewPatientID As System.Windows.Forms.Label
    Friend WithEvents lblPatientID As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_PtChangeHistory As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblViewPatientName As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientChangeHistory))
        Me.grdPatientHistory = New System.Windows.Forms.DataGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblViewPatientName = New System.Windows.Forms.Label
        Me.lblPatientName = New System.Windows.Forms.Label
        Me.lblViewPatientID = New System.Windows.Forms.Label
        Me.lblPatientID = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_PtChangeHistory = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.grdPatientHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_PtChangeHistory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPatientHistory
        '
        Me.grdPatientHistory.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdPatientHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdPatientHistory.BackgroundColor = System.Drawing.Color.White
        Me.grdPatientHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdPatientHistory.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdPatientHistory.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatientHistory.CaptionForeColor = System.Drawing.Color.White
        Me.grdPatientHistory.CaptionVisible = False
        Me.grdPatientHistory.DataMember = ""
        Me.grdPatientHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPatientHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatientHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdPatientHistory.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdPatientHistory.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdPatientHistory.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPatientHistory.HeaderForeColor = System.Drawing.Color.White
        Me.grdPatientHistory.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdPatientHistory.Location = New System.Drawing.Point(3, 0)
        Me.grdPatientHistory.Name = "grdPatientHistory"
        Me.grdPatientHistory.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdPatientHistory.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdPatientHistory.ReadOnly = True
        Me.grdPatientHistory.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdPatientHistory.SelectionForeColor = System.Drawing.Color.Black
        Me.grdPatientHistory.Size = New System.Drawing.Size(638, 359)
        Me.grdPatientHistory.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblViewPatientName)
        Me.Panel2.Controls.Add(Me.lblPatientName)
        Me.Panel2.Controls.Add(Me.lblViewPatientID)
        Me.Panel2.Controls.Add(Me.lblPatientID)
        Me.Panel2.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel2.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel2.Controls.Add(Me.lbl_pnlRight)
        Me.Panel2.Controls.Add(Me.lbl_pnlTop)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(638, 24)
        Me.Panel2.TabIndex = 2
        '
        'lblViewPatientName
        '
        Me.lblViewPatientName.AutoSize = True
        Me.lblViewPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblViewPatientName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblViewPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblViewPatientName.Location = New System.Drawing.Point(226, 1)
        Me.lblViewPatientName.Name = "lblViewPatientName"
        Me.lblViewPatientName.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblViewPatientName.Size = New System.Drawing.Size(23, 20)
        Me.lblViewPatientName.TabIndex = 3
        Me.lblViewPatientName.Text = "aa"
        Me.lblViewPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientName.Location = New System.Drawing.Point(125, 1)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblPatientName.Size = New System.Drawing.Size(101, 20)
        Me.lblPatientName.TabIndex = 2
        Me.lblPatientName.Text = "Patient Name :"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblViewPatientID
        '
        Me.lblViewPatientID.AutoSize = True
        Me.lblViewPatientID.BackColor = System.Drawing.Color.Transparent
        Me.lblViewPatientID.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblViewPatientID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewPatientID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblViewPatientID.Location = New System.Drawing.Point(100, 1)
        Me.lblViewPatientID.Name = "lblViewPatientID"
        Me.lblViewPatientID.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblViewPatientID.Size = New System.Drawing.Size(25, 20)
        Me.lblViewPatientID.TabIndex = 1
        Me.lblViewPatientID.Text = "12"
        Me.lblViewPatientID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatientID
        '
        Me.lblPatientID.AutoSize = True
        Me.lblPatientID.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientID.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblPatientID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblPatientID.Location = New System.Drawing.Point(1, 1)
        Me.lblPatientID.Name = "lblPatientID"
        Me.lblPatientID.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblPatientID.Size = New System.Drawing.Size(99, 20)
        Me.lblPatientID.TabIndex = 0
        Me.lblPatientID.Text = "Patient Code :"
        Me.lblPatientID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 23)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(636, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(637, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(638, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.grdPatientHistory)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 84)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(644, 362)
        Me.Panel3.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 358)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(636, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 358)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(640, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 358)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(638, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_PtChangeHistory)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(644, 54)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_PtChangeHistory
        '
        Me.tlsp_PtChangeHistory.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_PtChangeHistory.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_PtChangeHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_PtChangeHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_PtChangeHistory.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_PtChangeHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.tlsp_PtChangeHistory.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_PtChangeHistory.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_PtChangeHistory.Name = "tlsp_PtChangeHistory"
        Me.tlsp_PtChangeHistory.Size = New System.Drawing.Size(644, 53)
        Me.tlsp_PtChangeHistory.TabIndex = 0
        Me.tlsp_PtChangeHistory.Text = "toolStrip1"
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(644, 30)
        Me.Panel1.TabIndex = 15
        '
        'frmPatientChangeHistory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(644, 446)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientChangeHistory"
        Me.ShowInTaskbar = False
        Me.Text = "Patient Change History"
        CType(Me.grdPatientHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_PtChangeHistory.ResumeLayout(False)
        Me.tlsp_PtChangeHistory.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim dv As DataView
    '   load the form for view change history o fthe patient history in grid.
    Private Sub frmPatientChangeHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objcls As New ClsPatientRegistrationDBLayer

        dv = objcls.viewPatientsHistory(_PatientID)
        objcls.Dispose()
        objcls = Nothing
        grdPatientHistory.DataSource = dv

        ' function for the grid view and its columns
        CustomGridStyle()
        Call Get_PatientDetails()
        ' view patient data 
        lblViewPatientID.Text = strPatientCode
        lblViewPatientName.Text = strPatientFirstName & Space(1) & strPatientLastName
    End Sub
    '   function for the view column header as per the field-titles
    Public Sub CustomGridStyle()
        '  Dim dv As DataView
        '  dv = objclsCPT.GetDataview
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)
        'Dim objclsCPT As New clsCPT

        'objclsCPT.SortDataview(dv.Table.Columns(2).ColumnName)

        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns("nPatientID").ColumnName
        IDCol.HeaderText = "PatientID"

        Dim SpecialityColGenderDt As New DataGridTextBoxColumn
        With SpecialityColGenderDt
            .Width = 0.18 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("dtChangeDateTime").ColumnName
            .HeaderText = "Date"
            .NullText = ""
        End With

        Dim CPTCodeCol As New DataGridTextBoxColumn
        With CPTCodeCol
            .MappingName = dv.Table.Columns("sFirstName").ColumnName
            .HeaderText = "First Name"
            .NullText = ""
            .Width = 0.13 * grdPatientHistory.Width
        End With

        Dim DescCol As New DataGridTextBoxColumn
        With DescCol
            .Width = 0.13 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sMiddleName").ColumnName
            .HeaderText = "Middle Name"
            .NullText = ""
        End With

        Dim SpecialityCol As New DataGridTextBoxColumn
        With SpecialityCol
            .Width = 0.13 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sLastName").ColumnName
            .HeaderText = "Last Name"
            .NullText = ""
        End With

        Dim SpecialityColDOB As New DataGridTextBoxColumn
        With SpecialityColDOB
            .Width = 0.14 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("dtDOB").ColumnName
            .HeaderText = "Date Of Birth"
            .NullText = ""
        End With

        Dim SpecialityColGender As New DataGridTextBoxColumn
        With SpecialityColGender
            .Width = 0.09 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sGender").ColumnName
            .HeaderText = "Gender"
            .NullText = ""
        End With

        Dim SpecialityColGenderAdd1 As New DataGridTextBoxColumn
        With SpecialityColGenderAdd1
            .Width = 0.15 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sAddressLine1").ColumnName
            .HeaderText = "Address 1"
            .NullText = ""
        End With

        Dim SpecialityColGenderAdd2 As New DataGridTextBoxColumn
        With SpecialityColGenderAdd2
            .Width = 0.12 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sAddressLine2").ColumnName
            .HeaderText = "Address 2"
            .NullText = ""
        End With
        ' sState, sZIP, sCounty, sPhone
        Dim SpecialityColGenderCity As New DataGridTextBoxColumn
        With SpecialityColGenderCity
            .Width = 0.09 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sCity").ColumnName
            .HeaderText = "City"
            .NullText = ""
        End With

        Dim SpecialityColGenderZ As New DataGridTextBoxColumn
        With SpecialityColGenderZ
            .Width = 0.07 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sZIP").ColumnName
            .HeaderText = "Zip"
            .NullText = ""
        End With

        Dim SpecialityColGenderCounty As New DataGridTextBoxColumn
        With SpecialityColGenderCounty
            .Width = 0.09 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sCounty").ColumnName
            .HeaderText = "Country"
            .NullText = ""
        End With

        Dim SpecialityColGenderPH As New DataGridTextBoxColumn
        With SpecialityColGenderPH
            .Width = 0.09 * grdPatientHistory.Width
            .MappingName = dv.Table.Columns("sPhone").ColumnName
            .HeaderText = "Phone"
            .NullText = ""
        End With
        'Dim CategoryCol As New DataGridTextBoxColumn
        'With CategoryCol
        '    .Width = 150
        '    .MappingName = objclsCPT.GetDataview.Table.Columns(4).ColumnName
        '    .HeaderText = "Category"
        '    .NullText=""
        'End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, SpecialityColGenderDt, CPTCodeCol, DescCol, SpecialityCol, SpecialityColDOB, SpecialityColGender, SpecialityColGenderAdd1, SpecialityColGenderAdd2, SpecialityColGenderCity, SpecialityColGenderZ, SpecialityColGenderCounty, SpecialityColGenderPH})
        grdPatientHistory.TableStyles.Clear()
        grdPatientHistory.TableStyles.Add(ts)

    End Sub


    Private Sub tlsp_PtChangeHistory_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_PtChangeHistory.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub grdPatientHistory_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdPatientHistory.MouseUp
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdPatientHistory.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.Cell Then
                grdPatientHistory.Select(htInfo.Row)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Change Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
    End Sub
End Class
