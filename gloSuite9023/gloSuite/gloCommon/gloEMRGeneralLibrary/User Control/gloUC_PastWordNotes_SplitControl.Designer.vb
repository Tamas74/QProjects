<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_PastWordNotes_SplitControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_PastWordNotes_SplitControl))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlData = New System.Windows.Forms.Panel()
        Me.wdSplitControl = New AxDSOFramer.AxFramerControl()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlList = New System.Windows.Forms.Panel()
        Me.dgList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1SuperTooltip2 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlData.SuspendLayout()
        CType(Me.wdSplitControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlList.SuspendLayout()
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlData)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlList)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(377, 670)
        Me.pnlMain.TabIndex = 0
        '
        'pnlData
        '
        Me.pnlData.BackColor = System.Drawing.Color.White
        Me.pnlData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlData.Controls.Add(Me.wdSplitControl)
        Me.pnlData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlData.ForeColor = System.Drawing.Color.White
        Me.pnlData.Location = New System.Drawing.Point(0, 229)
        Me.pnlData.Name = "pnlData"
        Me.pnlData.Size = New System.Drawing.Size(377, 441)
        Me.pnlData.TabIndex = 2
        '
        'wdSplitControl
        '
        Me.wdSplitControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdSplitControl.Enabled = True
        Me.wdSplitControl.Location = New System.Drawing.Point(0, 0)
        Me.wdSplitControl.Name = "wdSplitControl"
        Me.wdSplitControl.OcxState = CType(resources.GetObject("wdSplitControl.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdSplitControl.Size = New System.Drawing.Size(375, 439)
        Me.wdSplitControl.TabIndex = 1
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 226)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(377, 3)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'pnlList
        '
        Me.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlList.Controls.Add(Me.dgList)
        Me.pnlList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlList.Location = New System.Drawing.Point(0, 0)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(377, 226)
        Me.pnlList.TabIndex = 0
        '
        'dgList
        '
        Me.dgList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.dgList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.dgList.BackColor = System.Drawing.Color.White
        Me.dgList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.dgList.ColumnInfo = resources.GetString("dgList.ColumnInfo")
        Me.dgList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgList.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.dgList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.dgList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.dgList.Location = New System.Drawing.Point(0, 0)
        Me.dgList.Name = "dgList"
        Me.dgList.Rows.Count = 1
        Me.dgList.Rows.DefaultSize = 19
        Me.dgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.dgList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.dgList.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.dgList.Size = New System.Drawing.Size(375, 224)
        Me.dgList.StyleInfo = resources.GetString("dgList.StyleInfo")
        Me.dgList.TabIndex = 20
        Me.dgList.Tree.NodeImageCollapsed = CType(resources.GetObject("dgList.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.dgList.Tree.NodeImageExpanded = CType(resources.GetObject("dgList.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'C1SuperTooltip2
        '
        Me.C1SuperTooltip2.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Acknowledge.ico")
        Me.ImageList1.Images.SetKeyName(1, "Note.ico")
        '
        'gloUC_PastWordNotes_SplitControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlMain)
        Me.Name = "gloUC_PastWordNotes_SplitControl"
        Me.Size = New System.Drawing.Size(377, 670)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlData.ResumeLayout(False)
        CType(Me.wdSplitControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlList.ResumeLayout(False)
        CType(Me.dgList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlData As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    'Friend WithEvents dgExams As gloUserControlLibrary.clsDataGrid
    Friend WithEvents wdSplitControl As AxDSOFramer.AxFramerControl
    Friend WithEvents dgList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1SuperTooltip2 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

End Class
