Public Class frmCriteria
    Inherits System.Windows.Forms.Form
    Dim arrlist As New ArrayList
   

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal arrlst As ArrayList)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        arrlist = arrlst
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents cmbValue As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcondition As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectionfield As System.Windows.Forms.Label
    Friend WithEvents lblCondition As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblValue As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCriteria))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.lblValue = New System.Windows.Forms.Label
        Me.lblCondition = New System.Windows.Forms.Label
        Me.lblSelectionfield = New System.Windows.Forms.Label
        Me.cmbcondition = New System.Windows.Forms.ComboBox
        Me.cmbValue = New System.Windows.Forms.ComboBox
        Me.cmbCriteria = New System.Windows.Forms.ComboBox
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlMain.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.lblValue)
        Me.pnlMain.Controls.Add(Me.lblCondition)
        Me.pnlMain.Controls.Add(Me.lblSelectionfield)
        Me.pnlMain.Controls.Add(Me.cmbcondition)
        Me.pnlMain.Controls.Add(Me.cmbValue)
        Me.pnlMain.Controls.Add(Me.cmbCriteria)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(337, 110)
        Me.pnlMain.TabIndex = 0
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblValue.Location = New System.Drawing.Point(10, 48)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(99, 14)
        Me.lblValue.TabIndex = 7
        Me.lblValue.Text = "Selection Value :"
        '
        'lblCondition
        '
        Me.lblCondition.AutoSize = True
        Me.lblCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCondition.Location = New System.Drawing.Point(43, 76)
        Me.lblCondition.Name = "lblCondition"
        Me.lblCondition.Size = New System.Drawing.Size(66, 14)
        Me.lblCondition.TabIndex = 6
        Me.lblCondition.Text = "Condition :"
        '
        'lblSelectionfield
        '
        Me.lblSelectionfield.AutoSize = True
        Me.lblSelectionfield.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSelectionfield.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSelectionfield.Location = New System.Drawing.Point(16, 20)
        Me.lblSelectionfield.Name = "lblSelectionfield"
        Me.lblSelectionfield.Size = New System.Drawing.Size(93, 14)
        Me.lblSelectionfield.TabIndex = 5
        Me.lblSelectionfield.Text = "Selection Field :"
        '
        'cmbcondition
        '
        Me.cmbcondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcondition.ForeColor = System.Drawing.Color.Black
        Me.cmbcondition.Location = New System.Drawing.Point(112, 74)
        Me.cmbcondition.Name = "cmbcondition"
        Me.cmbcondition.Size = New System.Drawing.Size(200, 22)
        Me.cmbcondition.TabIndex = 4
        '
        'cmbValue
        '
        Me.cmbValue.ForeColor = System.Drawing.Color.Black
        Me.cmbValue.Location = New System.Drawing.Point(112, 45)
        Me.cmbValue.Name = "cmbValue"
        Me.cmbValue.Size = New System.Drawing.Size(200, 22)
        Me.cmbValue.TabIndex = 2
        '
        'cmbCriteria
        '
        Me.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbCriteria.Location = New System.Drawing.Point(112, 16)
        Me.cmbCriteria.Name = "cmbCriteria"
        Me.cmbCriteria.Size = New System.Drawing.Size(200, 22)
        Me.cmbCriteria.TabIndex = 0
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(337, 54)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(337, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(43, 50)
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClose.ToolTipText = "Close"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(329, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 103)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(333, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 103)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(331, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'frmCriteria
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(337, 164)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCriteria"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selection Criteria"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmCriteria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Assign Old Criteria Field and Values
            cmbCriteria.DataSource = arrlist
            cmbCriteria.Text = ClsReportExplorer.CriteriaField
            cmbcondition.Text = ClsReportExplorer.Condition
            cmbValue.Text = ClsReportExplorer.CriteriaValue
            FillConditions()
        Catch ex As Exception
            MessageBox.Show("Error Loading Selection Criteria form", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillConditions()
        cmbcondition.Items.Add("=")
        cmbcondition.Items.Add(">")
        cmbcondition.Items.Add("<")
        cmbcondition.Items.Add(">=")
        cmbcondition.Items.Add("<=")
        cmbcondition.Items.Add("<>")
        cmbcondition.Items.Add("!=")
        cmbcondition.Items.Add("!<")
        cmbcondition.Items.Add("!>")
        cmbcondition.Items.Add("Like")
        cmbcondition.Items.Add("Not Like")
    End Sub

    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        Try
            If cmbCriteria.Text <> "" Then
                Dim objreportexplorer As New ClsReportExplorer
                Dim dttable As New DataTable
                'Get Values for Selected Field
                dttable = objreportexplorer.GetFieldValue(cmbCriteria.Text)
                'Check rows returned
                If dttable.Rows.Count > 0 Then
                    cmbValue.DataSource = dttable
                    cmbValue.DisplayMember = dttable.Columns.Item(0).ColumnName
                    cmbValue.ValueMember = dttable.Columns.Item(0).ColumnName
                End If

            End If
        Catch ex As Exception
            MessageBox.Show("Cannot Change Criteria Field", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If cmbCriteria.Text = "" Then
                MessageBox.Show("Please Select Criteria Field", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            ElseIf cmbcondition.Text = "" Then
                MessageBox.Show("Please Select Condition", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            ElseIf cmbValue.Text = "" Then
                MessageBox.Show("Please Select Value", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            BuildCriteria()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error Assigning Selection Criteria", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub BuildCriteria()
        'Build the criteria against selected field and selected value
        Dim strValue As String = ""
        Dim strdatatype As String
        strdatatype = Splittext(cmbCriteria.Text)
        If strdatatype.Substring(0, 1) = "s" Or strdatatype.Substring(0, 1) = "dt" Then
            strValue = "'" & cmbValue.Text & "'"
        ElseIf strdatatype.Substring(0, 1) = "n" Then
            strValue = cmbValue.Text
        End If
        'Assign the criteria to Shared Variable "Selection Criteria"
        ClsReportExplorer.CriteriaField = cmbCriteria.Text
        ClsReportExplorer.CriteriaValue = cmbValue.Text
        ClsReportExplorer.Condition = cmbcondition.Text
        ClsReportExplorer.SelectionCriteria = "where " & cmbCriteria.Text & cmbcondition.Text & strValue
    End Sub

    Private Function Splittext(ByVal strsplittext As String) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, ".")
            If arrstring.Length > 0 Then
                Return arrstring(1)
            Else
                Return strsplittext
            End If
        Else
            Return ""
        End If
    End Function

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

End Class
