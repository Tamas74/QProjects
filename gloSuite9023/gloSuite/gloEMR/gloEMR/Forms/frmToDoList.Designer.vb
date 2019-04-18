<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToDoList
    Inherits System.Windows.Forms.Form

    ''Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing AndAlso components IsNot Nothing Then
    '        components.Dispose()
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub


#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmToDoList
    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True
        Try
            MyBase.Dispose(disposing)
        Catch ex As Exception

        End Try
    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance(ByVal dtDate As Date, ByVal Flag As Int16) As frmToDoList
        '_mu.WaitOne()
        Try
            If frm Is Nothing Then
                frm = New frmToDoList(dtDate, Flag)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToDoList))
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.C1ToDoList = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnl_tls = New System.Windows.Forms.Panel
        Me.tlsNewAppointment = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlMiddle.SuspendLayout()
        CType(Me.C1ToDoList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls.SuspendLayout()
        Me.tlsNewAppointment.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMiddle
        '
        Me.pnlMiddle.Controls.Add(Me.C1ToDoList)
        Me.pnlMiddle.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_RightBrd)
        Me.pnlMiddle.Controls.Add(Me.lbl_TopBrd)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 53)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMiddle.Size = New System.Drawing.Size(455, 181)
        Me.pnlMiddle.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 177)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(447, 1)
        Me.lbl_BottomBrd.TabIndex = 29
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 174)
        Me.lbl_LeftBrd.TabIndex = 28
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(451, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 174)
        Me.lbl_RightBrd.TabIndex = 27
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(449, 1)
        Me.lbl_TopBrd.TabIndex = 26
        Me.lbl_TopBrd.Text = "label1"
        '
        'C1ToDoList
        '
        Me.C1ToDoList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1ToDoList.AllowEditing = False
        Me.C1ToDoList.BackColor = System.Drawing.Color.White
        Me.C1ToDoList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ToDoList.ColumnInfo = "1,0,0,0,0,95,Columns:0{TextAlignFixed:CenterCenter;ImageAlignFixed:CenterCenter;}" & _
            "" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1ToDoList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ToDoList.ExtendLastCol = True
        Me.C1ToDoList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ToDoList.ForeColor = System.Drawing.Color.Black
        Me.C1ToDoList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1ToDoList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1ToDoList.Location = New System.Drawing.Point(4, 4)
        Me.C1ToDoList.Name = "C1ToDoList"
        Me.C1ToDoList.Rows.Count = 1
        Me.C1ToDoList.Rows.DefaultSize = 19
        Me.C1ToDoList.Rows.Fixed = 0
        Me.C1ToDoList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ToDoList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1ToDoList.Size = New System.Drawing.Size(447, 173)
        Me.C1ToDoList.StyleInfo = resources.GetString("C1ToDoList.StyleInfo")
        Me.C1ToDoList.TabIndex = 25
        Me.C1ToDoList.Tree.NodeImageCollapsed = CType(resources.GetObject("C1ToDoList.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1ToDoList.Tree.NodeImageExpanded = CType(resources.GetObject("C1ToDoList.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnl_tls
        '
        Me.pnl_tls.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls.Controls.Add(Me.tlsNewAppointment)
        Me.pnl_tls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls.Name = "pnl_tls"
        Me.pnl_tls.Size = New System.Drawing.Size(455, 53)
        Me.pnl_tls.TabIndex = 5
        '
        'tlsNewAppointment
        '
        Me.tlsNewAppointment.BackColor = System.Drawing.Color.Transparent
        Me.tlsNewAppointment.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsNewAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsNewAppointment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsNewAppointment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Close})
        Me.tlsNewAppointment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsNewAppointment.Location = New System.Drawing.Point(0, 0)
        Me.tlsNewAppointment.Name = "tlsNewAppointment"
        Me.tlsNewAppointment.Size = New System.Drawing.Size(455, 53)
        Me.tlsNewAppointment.TabIndex = 0
        Me.tlsNewAppointment.Text = "toolStrip1"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Close.ToolTipText = "Close"
        '
        'frmToDoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(455, 234)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.pnl_tls)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmToDoList"
        Me.Text = "To Do List"
        Me.pnlMiddle.ResumeLayout(False)
        CType(Me.C1ToDoList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls.ResumeLayout(False)
        Me.pnl_tls.PerformLayout()
        Me.tlsNewAppointment.ResumeLayout(False)
        Me.tlsNewAppointment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents C1ToDoList As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents pnl_tls As System.Windows.Forms.Panel
    Private WithEvents tlsNewAppointment As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
End Class
