Public Class frm_LM_VW_LabResult
    Inherits System.Windows.Forms.Form
    Public Shared blnModify As Boolean
    Public CategoryID As Int64
    Friend CategoryType As String
    Dim ocls_LM_LabResult As New cls_LM_LabResult
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsViewResult As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label

    'Public oMainMenuForMenu As MainMenu

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
            End If
            If (IsNothing(ocls_LM_LabResult) = False) Then
                ocls_LM_LabResult.Dispose()
                ocls_LM_LabResult = Nothing
            End If
            Try

                If (IsNothing(grdFlowSheet) = False) Then
                    grdFlowSheet.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdFlowSheet)
                    grdFlowSheet.Dispose()
                    grdFlowSheet = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents grdFlowSheet As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_VW_LabResult))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlGrid = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.grdFlowSheet = New System.Windows.Forms.DataGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tlsViewResult = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.grdFlowSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsViewResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 52)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlTop.Size = New System.Drawing.Size(888, 28)
        Me.pnlTop.TabIndex = 13
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.pnlTopRight)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(884, 24)
        Me.Panel1.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "label4"
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTopRight.Location = New System.Drawing.Point(628, 1)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(256, 23)
        Me.pnlTopRight.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(3, -1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(100, 23)
        Me.lblSearch.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(106, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(150, 22)
        Me.txtSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(884, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "  View Lab Result FlowSheets"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(884, 1)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "label1"
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.Transparent
        Me.pnlGrid.Controls.Add(Me.Label3)
        Me.pnlGrid.Controls.Add(Me.Label2)
        Me.pnlGrid.Controls.Add(Me.Label4)
        Me.pnlGrid.Controls.Add(Me.Label6)
        Me.pnlGrid.Controls.Add(Me.grdFlowSheet)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 80)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(888, 502)
        Me.pnlGrid.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(3, 498)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(882, 1)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(885, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 495)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(883, 1)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(2, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 496)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "label4"
        '
        'grdFlowSheet
        '
        Me.grdFlowSheet.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdFlowSheet.BackgroundColor = System.Drawing.Color.White
        Me.grdFlowSheet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdFlowSheet.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.grdFlowSheet.CaptionFont = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheet.CaptionForeColor = System.Drawing.Color.White
        Me.grdFlowSheet.CaptionVisible = False
        Me.grdFlowSheet.DataMember = ""
        Me.grdFlowSheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFlowSheet.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheet.ForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.GridLineColor = System.Drawing.Color.Black
        Me.grdFlowSheet.HeaderBackColor = System.Drawing.Color.White
        Me.grdFlowSheet.HeaderForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdFlowSheet.Location = New System.Drawing.Point(2, 3)
        Me.grdFlowSheet.Name = "grdFlowSheet"
        Me.grdFlowSheet.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdFlowSheet.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.ReadOnly = True
        Me.grdFlowSheet.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdFlowSheet.SelectionForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.Size = New System.Drawing.Size(884, 496)
        Me.grdFlowSheet.TabIndex = 1
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsViewResult)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(888, 52)
        Me.pnlToolStrip.TabIndex = 16
        '
        'tlsViewResult
        '
        Me.tlsViewResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.tlsViewResult.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsViewResult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsViewResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlsViewResult.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsViewResult.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsViewResult.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsViewResult.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.tlsViewResult.Location = New System.Drawing.Point(0, 0)
        Me.tlsViewResult.Name = "tlsViewResult"
        Me.tlsViewResult.Size = New System.Drawing.Size(888, 52)
        Me.tlsViewResult.TabIndex = 0
        Me.tlsViewResult.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(41, 49)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New "
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 49)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 49)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 49)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 49)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frm_LM_VW_LabResult
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(888, 582)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_LM_VW_LabResult"
        Me.Text = "View Lab Result Flowsheets"
        Me.pnlTop.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.grdFlowSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsViewResult.ResumeLayout(False)
        Me.tlsViewResult.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frm_LM_VW_LabResult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''clsDataGrid()
        Try
            grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet
            CustomGridStyle()
            'CustomTreeView()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Private Sub UpdateFlowSheet()
        Dim ID As Long
        Dim frm As frm_LM_MST_LabResult = Nothing
        Try

            If grdFlowSheet.VisibleRowCount >= 1 Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    blnModify = True
                    ID = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 0).ToString
                    Dim grdIndex As Integer = grdFlowSheet.CurrentRowIndex

                    ''''''''
                    Dim IsInUse As Boolean = False
                    If ocls_LM_LabResult.CheckIsUsed(ID) = True Then
                        IsInUse = True
                    End If
                    ''''''''
                    frm = New frm_LM_MST_LabResult(ID, IsInUse)
                    With frm
                        '.Text = "Modify FlowSheet" 
                        .ShowInTaskbar = False
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        .BringToFront()

                        If ._Cancel = False Then
                            'oMainMenuForMenu.Set_LM_Menu()
                            grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet

                            Dim myDataView As DataView = CType(grdFlowSheet.DataSource, DataView)
                            ''''' To Remember the Selection of Row 
                            If (IsNothing(myDataView) = False) Then
                                Dim i As Integer
                                For i = 0 To myDataView.Table.Rows.Count - 1
                                    ''''' when ID Found select that matching Row
                                    If ID = grdFlowSheet.Item(i, 0) Then
                                        grdFlowSheet.CurrentRowIndex = i
                                        grdFlowSheet.Select(i)
                                        Exit For
                                    End If
                                Next
                            End If

                        Else
                            grdFlowSheet.Select(grdIndex)
                        End If
                    End With

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        End Try
    End Sub


    Public Sub CustomGridStyle()
        'Dim ts As New DataGridTableStyle

        'ts.ReadOnly = True
        'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
        'ts.BackColor = System.Drawing.Color.WhiteSmoke
        'ts.MappingName = ocls_LM_LabResult.GetDataview.Table.TableName
        'ts.HeaderBackColor = System.Drawing.Color.DimGray
        'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'ts.RowHeadersVisible = False

        Dim dv As DataView
        dv = ocls_LM_LabResult.GetDataview
        Dim ts As New clsDataGridTableStyle(dv.Table.TableName)


        Dim IDCol As New DataGridTextBoxColumn
        IDCol.Width = 0
        IDCol.MappingName = dv.Table.Columns(0).ColumnName
        IDCol.HeaderText = "ID"

        Dim FlowSheetCol As New DataGridTextBoxColumn
        With FlowSheetCol
            .Width = grdFlowSheet.Width
            .MappingName = dv.Table.Columns(1).ColumnName
            .HeaderText = "Flow Sheet Name"
            .NullText = ""
        End With

        'Dim NoOfCol As New DataGridTextBoxColumn
        'With NoOfCol
        '    .Width = 150
        '    .MappingName = ocls_LM_LabResult.GetDataview.Table.Columns(2).ColumnName
        '    .HeaderText = "No Of Columns"
        '    .NullText = ""
        'End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, FlowSheetCol})
        grdFlowSheet.TableStyles.Clear()
        grdFlowSheet.TableStyles.Add(ts)

    End Sub

    Private Sub grdFlowSheet_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdFlowSheet.CurrentCellChanged
        Try
            Select Case grdFlowSheet.CurrentCell.ColumnNumber
                Case 1
                    lblSearch.Text = "Flow Sheet Name"
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dvPatient As DataView
            dvPatient = CType(grdFlowSheet.DataSource(), DataView)
            grdFlowSheet.DataSource = dvPatient
            If (IsNothing(dvPatient) = False) Then


                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                Else
                    strPatientSearchDetails = ""
                End If

                Select Case Trim(lblSearch.Text)
                    Case "Flow Sheet Name"
                        If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                        Else
                            dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                        End If
                End Select
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdFlowSheet_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheet.MouseUp
        Try
            If grdFlowSheet.CurrentRowIndex >= 0 Then
                grdFlowSheet.Select(grdFlowSheet.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdFlowSheet_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFlowSheet.DoubleClick
        Call UpdateFlowSheet()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    grdFlowSheet.Select(0)
                    grdFlowSheet.CurrentRowIndex = 0
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsViewResult_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsViewResult.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"
                    ADDLabResult()
                Case "Modify"
                    UpdateFlowSheet()
                Case "Delete"
                    DeleteLabResult()
                Case "Refresh"
                    grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet
                    CustomGridStyle()
                Case "Close"

                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ADDLabResult()
        Dim frm As New frm_LM_MST_LabResult

        Try
            With frm
                blnModify = False
                '.Text = "Add New FlowSheet"
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet

                'grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet
                ''''' To Remember the Selection of Row 
                Dim myDataView As DataView = CType(grdFlowSheet.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        ''''' when ID Found select that matching Row
                        If ._FlowSheetName = grdFlowSheet.Item(i, 1) Then
                            grdFlowSheet.CurrentRowIndex = i
                            grdFlowSheet.Select(i)
                            Exit For
                        End If
                    Next
                End If

                'oMainMenuForMenu.Set_LM_Menu()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub
    Private Sub DeleteLabResult()
        Dim ID As Long
        Dim FlowSheetName As String
        Try
            If grdFlowSheet.VisibleRowCount >= 1 Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    'blnModify = True
                    ID = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 0).ToString
                    FlowSheetName = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 1).ToString

                    ''''''''
                    If ocls_LM_LabResult.CheckIsUsed(ID) = True Then
                        MessageBox.Show("Data is already entered for Flow Sheet '" & FlowSheetName & "', you cannot delete it", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                        Exit Sub
                    End If
                    ''''''''
                    If MessageBox.Show("Are you sure to Delete Flowsheet Details of '" & FlowSheetName & "'", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        ocls_LM_LabResult.DeleteFlowSheet(ID, FlowSheetName)
                        grdFlowSheet.DataSource = ocls_LM_LabResult.GetAllFlowSheet()
                    End If
                    'oMainMenuForMenu.Set_LM_Menu()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
