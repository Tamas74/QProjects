Public Class frmDM_GuidlineDuration
    Inherits System.Windows.Forms.Form

    Private COL_ID As Int16 = 0
    Private COL_CATNAME As Int16 = 1
    Private COL_NAME As Int16 = 2
    Private COL_DURATION As Int16 = 3
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private COL_COUNT As Int16 = 4

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
            If Not (components Is Nothing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
    Friend WithEvents c1Templates As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_GuidlineDuration))
        Me.c1Templates = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.SuspendLayout()
        '
        'c1Templates
        '
        Me.c1Templates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Templates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Templates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Templates.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.c1Templates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Templates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Templates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Templates.Location = New System.Drawing.Point(4, 4)
        Me.c1Templates.Name = "c1Templates"
        Me.c1Templates.Rows.DefaultSize = 19
        Me.c1Templates.Size = New System.Drawing.Size(708, 457)
        Me.c1Templates.StyleInfo = resources.GetString("c1Templates.StyleInfo")
        Me.c1Templates.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(716, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(716, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnRefresh.Tag = "Save"
        Me.ts_btnRefresh.Text = "&Save&&Cls"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnRefresh.ToolTipText = "Save and Close"
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
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.c1Templates)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(716, 464)
        Me.pnl_Base.TabIndex = 12
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 460)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(708, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 457)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(712, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 457)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(710, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmDM_GuidlineDuration
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(716, 518)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_GuidlineDuration"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Guideline Duration"
        CType(Me.c1Templates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_GuidlineDuration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1Templates)
        Try
            DesignGrid()
            Fill_Templates()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DesignGrid()
        With c1Templates
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COL_COUNT
            .Cols.Fixed = 0

            .SetData(0, COL_ID, "ID")
            .SetData(0, COL_NAME, "Template")
            .SetData(0, COL_CATNAME, "Category")
            .SetData(0, COL_DURATION, "Duration")

            .Rows(0).Height = 23
            Dim _Width As Single = (.Width - 20) / 5
            .Cols(COL_ID).Width = 0
            .Cols(COL_CATNAME).Width = _Width * 1.5
            .Cols(COL_NAME).Width = _Width * 2
            .Cols(COL_DURATION).Width = _Width * 1

            Dim _Duration As String
            _Duration = " |1 Day|2 Days|3 Days|4 Days|5 Days|6 Days|1 Week|2 Weeks|3 Weeks|4 Weeks|1 Month|2 Months|3 Months"

            .Cols(COL_ID).DataType = GetType(Long)
            .Cols(COL_NAME).DataType = GetType(String)
            .Cols(COL_CATNAME).DataType = GetType(String)
            .Cols(COL_DURATION).DataType = GetType(String)
            .Cols(COL_DURATION).ComboList = _Duration


            .Cols(COL_ID).Visible = False
            .Cols(COL_NAME).Visible = True
            .Cols(COL_CATNAME).Visible = True
            .Cols(COL_DURATION).Visible = True


            .Cols(COL_ID).AllowEditing = False
            .Cols(COL_NAME).AllowEditing = False
            .Cols(COL_CATNAME).AllowEditing = False
            .Cols(COL_DURATION).AllowEditing = True
        End With
    End Sub

    Private Sub Fill_Templates()
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        Dim _strSQL As String
        Dim _PreviousCategory As String = ""

        '_strSQL = "SELECT TemplateGallery_MST.sTemplateName, TemplateGallery_MST.nTemplateID, Category_MST.sDescription " _
        '& " FROM TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID WHERE (Category_MST.sDescription = 'Patient Education') OR (Category_MST.sDescription = 'Preventive Services') OR (Category_MST.sDescription = 'Wellness Guidelines') " _
        '& " ORDER BY Category_MST.sDescription,TemplateGallery_MST.sTemplateName"

        _strSQL = " SELECT	TemplateGallery_MST.sTemplateName, TemplateGallery_MST.nTemplateID, Category_MST.sDescription, ISNULL(DM_Duration_MST.dm_sDuration,'') AS dm_sDuration " _
                & " FROM	TemplateGallery_MST INNER JOIN Category_MST ON TemplateGallery_MST.nCategoryID = Category_MST.nCategoryID LEFT OUTER JOIN " _
                & " DM_Duration_MST ON TemplateGallery_MST.nTemplateID = DM_Duration_MST.dm_nTemplateID " _
                & " WHERE   (Category_MST.sDescription = 'Patient Education') OR (Category_MST.sDescription = 'Preventive Services') OR " _
                & " (Category_MST.sDescription = 'Wellness Guidelines') " _
                & " ORDER BY Category_MST.sDescription, TemplateGallery_MST.sTemplateName "

        oDB.Connect(GetConnectionString)
        oDataReader = oDB.ReadQueryRecords(_strSQL)

        With c1Templates
            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        If IsDBNull(oDataReader.Item("sTemplateName")) = False AndAlso IsDBNull(oDataReader.Item("nTemplateID")) = False AndAlso IsDBNull(oDataReader.Item("sDescription")) = False Then
                            .Rows.Add()
                            .SetData(c1Templates.Rows.Count - 1, COL_ID, oDataReader.Item("nTemplateID") & "")
                            If _PreviousCategory <> oDataReader.Item("sDescription") & "" Then
                                .SetData(.Rows.Count - 1, COL_CATNAME, oDataReader.Item("sDescription") & "")
                            End If
                            _PreviousCategory = oDataReader("sDescription") & ""
                            .SetData(.Rows.Count - 1, COL_NAME, oDataReader("sTemplateName") & "")
                            .SetData(.Rows.Count - 1, COL_DURATION, oDataReader("dm_sDuration") & "")
                        End If
                    End While
                End If
            End If
        End With
        oDB.Disconnect()
        oDB = Nothing
    End Sub
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub SaveCategory()
        Dim oDuration As New Collection
        Try
            Dim i As Integer
            Dim lst As myList
            With c1Templates
                For i = 1 To .Rows.Count - 1
                    If Trim(.GetData(i, COL_DURATION)) <> "" Then
                        lst = New myList
                        lst.ID = .GetData(i, COL_ID)
                        lst.Description = .GetData(i, COL_DURATION)
                        oDuration.Add(lst)
                        lst = Nothing
                    End If
                Next
            End With

            Dim oDM_Template As New clsDM_Template
            If oDM_Template.Save_GuidelineDuration(oDuration) = True Then
                Me.Close()
            End If
            oDM_Template = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                Call SaveCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub c1Templates_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Templates.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
