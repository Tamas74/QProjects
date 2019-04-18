<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloMedHxReconcilation
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloMedHxReconcilation))
        Me.pnlConsolidatedList = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C1ConsolidatedList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblConsolidated = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ImgGrid = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlConsolidatedList.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1ConsolidatedList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlConsolidatedList
        '
        Me.pnlConsolidatedList.Controls.Add(Me.Panel1)
        Me.pnlConsolidatedList.Controls.Add(Me.Panel2)
        Me.pnlConsolidatedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlConsolidatedList.Location = New System.Drawing.Point(0, 0)
        Me.pnlConsolidatedList.Name = "pnlConsolidatedList"
        Me.pnlConsolidatedList.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlConsolidatedList.Size = New System.Drawing.Size(1363, 739)
        Me.pnlConsolidatedList.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.C1ConsolidatedList)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(1357, 706)
        Me.Panel1.TabIndex = 44
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Location = New System.Drawing.Point(1, 705)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1355, 1)
        Me.Label17.TabIndex = 43
        Me.Label17.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(1356, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 702)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 702)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1357, 1)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "label1"
        '
        'C1ConsolidatedList
        '
        Me.C1ConsolidatedList.BackColor = System.Drawing.Color.White
        Me.C1ConsolidatedList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ConsolidatedList.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.C1ConsolidatedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ConsolidatedList.ExtendLastCol = True
        Me.C1ConsolidatedList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ConsolidatedList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ConsolidatedList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ConsolidatedList.Location = New System.Drawing.Point(0, 3)
        Me.C1ConsolidatedList.Name = "C1ConsolidatedList"
        Me.C1ConsolidatedList.Rows.Count = 5
        Me.C1ConsolidatedList.Rows.DefaultSize = 21
        Me.C1ConsolidatedList.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible
        Me.C1ConsolidatedList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ConsolidatedList.ShowCellLabels = True
        Me.C1ConsolidatedList.Size = New System.Drawing.Size(1357, 703)
        Me.C1ConsolidatedList.StyleInfo = resources.GetString("C1ConsolidatedList.StyleInfo")
        Me.C1ConsolidatedList.TabIndex = 0
        Me.C1ConsolidatedList.Tag = "ConsolidatedList"
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.lblConsolidated)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.ForeColor = System.Drawing.Color.White
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1357, 27)
        Me.Panel2.TabIndex = 43
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(1356, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 25)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 25)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "label1"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1357, 1)
        Me.Label14.TabIndex = 44
        Me.Label14.Text = "label1"
        '
        'lblConsolidated
        '
        Me.lblConsolidated.BackColor = System.Drawing.Color.Transparent
        Me.lblConsolidated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblConsolidated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsolidated.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblConsolidated.Location = New System.Drawing.Point(0, 0)
        Me.lblConsolidated.Name = "lblConsolidated"
        Me.lblConsolidated.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblConsolidated.Size = New System.Drawing.Size(1357, 26)
        Me.lblConsolidated.TabIndex = 43
        Me.lblConsolidated.Text = "Review the consolidated display.  Unselect any unneeded items. Then press ‘Finali" & _
    "ze’. The system has already removed obvious duplicates and warns you about simil" & _
    "ar items to review."
        Me.lblConsolidated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(0, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1357, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label1"
        '
        'ImgGrid
        '
        Me.ImgGrid.ImageStream = CType(resources.GetObject("ImgGrid.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgGrid.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgGrid.Images.SetKeyName(0, "Duplicate Records.ico")
        Me.ImgGrid.Images.SetKeyName(1, "Img_Status_Alert.ico")
        Me.ImgGrid.Images.SetKeyName(2, "Img_Status_Pending01.ico")
        '
        'gloMedHxReconcilation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlConsolidatedList)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloMedHxReconcilation"
        Me.Size = New System.Drawing.Size(1363, 739)
        Me.pnlConsolidatedList.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1ConsolidatedList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlConsolidatedList As System.Windows.Forms.Panel
    Friend WithEvents C1ConsolidatedList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblConsolidated As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ImgGrid As System.Windows.Forms.ImageList

End Class
