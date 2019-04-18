<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_GridList
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
                If (IsNothing(dvNext) = False) Then
                    dvNext.Dispose()
                    dvNext = Nothing
                End If
                If (IsNothing(ToolTip2) = False) Then
                    ToolTip2.Dispose()
                    ToolTip2 = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_GridList))
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtListSearch = New System.Windows.Forms.TextBox
        Me.btnCloseRefill = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnSelect = New System.Windows.Forms.Button
        Me.lblHeader = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlGridList = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.C1GridList = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlGridList.SuspendLayout()
        CType(Me.C1GridList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(544, 24)
        Me.Panel4.TabIndex = 22
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.txtListSearch)
        Me.Panel1.Controls.Add(Me.btnCloseRefill)
        Me.Panel1.Controls.Add(Me.btnModify)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.btnSelect)
        Me.Panel1.Controls.Add(Me.lblHeader)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(544, 24)
        Me.Panel1.TabIndex = 19
        '
        'txtListSearch
        '
        Me.txtListSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.txtListSearch.ForeColor = System.Drawing.Color.Black
        Me.txtListSearch.Location = New System.Drawing.Point(292, 1)
        Me.txtListSearch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtListSearch.Name = "txtListSearch"
        Me.txtListSearch.Size = New System.Drawing.Size(251, 22)
        Me.txtListSearch.TabIndex = 1
        Me.txtListSearch.Visible = False
        '
        'btnCloseRefill
        '
        Me.btnCloseRefill.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCloseRefill.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCloseRefill.FlatAppearance.BorderSize = 0
        Me.btnCloseRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCloseRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseRefill.Image = CType(resources.GetObject("btnCloseRefill.Image"), System.Drawing.Image)
        Me.btnCloseRefill.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCloseRefill.Location = New System.Drawing.Point(222, 1)
        Me.btnCloseRefill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCloseRefill.Name = "btnCloseRefill"
        Me.btnCloseRefill.Size = New System.Drawing.Size(25, 22)
        Me.btnCloseRefill.TabIndex = 9
        Me.btnCloseRefill.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCloseRefill.Visible = False
        '
        'btnModify
        '
        Me.btnModify.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModify.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnModify.FlatAppearance.BorderSize = 0
        Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Image = CType(resources.GetObject("btnModify.Image"), System.Drawing.Image)
        Me.btnModify.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnModify.Location = New System.Drawing.Point(197, 1)
        Me.btnModify.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(25, 22)
        Me.btnModify.TabIndex = 10
        Me.btnModify.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnModify.Visible = False
        '
        'btnAdd
        '
        Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAdd.Location = New System.Drawing.Point(172, 1)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(25, 22)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAdd.Visible = False
        '
        'btnSelect
        '
        Me.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelect.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect.Image = CType(resources.GetObject("btnSelect.Image"), System.Drawing.Image)
        Me.btnSelect.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSelect.Location = New System.Drawing.Point(148, 1)
        Me.btnSelect.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(24, 22)
        Me.btnSelect.TabIndex = 12
        Me.btnSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSelect.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(1, 1)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(147, 22)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = " Custom List :"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(543, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 22)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(544, 1)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(544, 1)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "label1"
        '
        'pnlGridList
        '
        Me.pnlGridList.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnlGridList.Controls.Add(Me.Label4)
        Me.pnlGridList.Controls.Add(Me.Label5)
        Me.pnlGridList.Controls.Add(Me.Label6)
        Me.pnlGridList.Controls.Add(Me.Label7)
        Me.pnlGridList.Controls.Add(Me.C1GridList)
        Me.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGridList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlGridList.Location = New System.Drawing.Point(0, 24)
        Me.pnlGridList.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlGridList.Name = "pnlGridList"
        Me.pnlGridList.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlGridList.Size = New System.Drawing.Size(544, 299)
        Me.pnlGridList.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(543, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 294)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 294)
        Me.Label5.TabIndex = 217
        Me.Label5.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 298)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(544, 1)
        Me.Label6.TabIndex = 216
        Me.Label6.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(544, 1)
        Me.Label7.TabIndex = 215
        Me.Label7.Text = "label1"
        '
        'C1GridList
        '
        Me.C1GridList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1GridList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.C1GridList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1GridList.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1GridList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1GridList.ExtendLastCol = True
        Me.C1GridList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1GridList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1GridList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.C1GridList.Location = New System.Drawing.Point(0, 3)
        Me.C1GridList.Name = "C1GridList"
        Me.C1GridList.Rows.DefaultSize = 19
        Me.C1GridList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1GridList.Size = New System.Drawing.Size(544, 296)
        Me.C1GridList.StyleInfo = resources.GetString("C1GridList.StyleInfo")
        Me.C1GridList.TabIndex = 214
        '
        'gloUC_GridList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Controls.Add(Me.pnlGridList)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "gloUC_GridList"
        Me.Size = New System.Drawing.Size(544, 323)
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlGridList.ResumeLayout(False)
        CType(Me.C1GridList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents txtListSearch As System.Windows.Forms.TextBox
    Private WithEvents lblHeader As System.Windows.Forms.Label
    Private WithEvents pnlGridList As System.Windows.Forms.Panel
    'Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Public WithEvents C1GridList As C1.Win.C1FlexGrid.C1FlexGrid
    Protected WithEvents btnCloseRefill As System.Windows.Forms.Button
    Protected WithEvents btnAdd As System.Windows.Forms.Button
    Protected WithEvents btnModify As System.Windows.Forms.Button
    Protected WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label

End Class
