<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportDICOM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportDICOM))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tls_MaintainDoc = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlb_Ok = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tlb_Add = New System.Windows.Forms.ToolStripButton()
        Me.tlb_Remove = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtDocumentName = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.lvDocuments = New System.Windows.Forms.ListView()
        Me.colDocument = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFormat = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAdded = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.tls_MaintainDoc.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.Controls.Add(Me.tls_MaintainDoc)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(477, 53)
        Me.pnlTop.TabIndex = 0
        '
        'tls_MaintainDoc
        '
        Me.tls_MaintainDoc.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_MaintainDoc.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_MaintainDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_MaintainDoc.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_MaintainDoc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_Ok, Me.tlb_Cancel, Me.toolStripSeparator1, Me.tlb_Add, Me.tlb_Remove})
        Me.tls_MaintainDoc.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_MaintainDoc.Location = New System.Drawing.Point(0, 0)
        Me.tls_MaintainDoc.Name = "tls_MaintainDoc"
        Me.tls_MaintainDoc.Size = New System.Drawing.Size(477, 53)
        Me.tls_MaintainDoc.TabIndex = 2
        Me.tls_MaintainDoc.Text = "toolStrip1"
        '
        'tlb_Ok
        '
        Me.tlb_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Ok.Image = CType(resources.GetObject("tlb_Ok.Image"), System.Drawing.Image)
        Me.tlb_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Ok.Name = "tlb_Ok"
        Me.tlb_Ok.Size = New System.Drawing.Size(66, 50)
        Me.tlb_Ok.Tag = "OK"
        Me.tlb_Ok.Text = "&Save&&Cls"
        Me.tlb_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Ok.ToolTipText = "Save and Close"
        '
        'tlb_Cancel
        '
        Me.tlb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Cancel.Image = CType(resources.GetObject("tlb_Cancel.Image"), System.Drawing.Image)
        Me.tlb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Cancel.Name = "tlb_Cancel"
        Me.tlb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Cancel.Tag = "Cancel"
        Me.tlb_Cancel.Text = "&Close"
        Me.tlb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Cancel.ToolTipText = "Close"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.AutoSize = False
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 51)
        '
        'tlb_Add
        '
        Me.tlb_Add.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Add.Image = CType(resources.GetObject("tlb_Add.Image"), System.Drawing.Image)
        Me.tlb_Add.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Add.Name = "tlb_Add"
        Me.tlb_Add.Size = New System.Drawing.Size(36, 50)
        Me.tlb_Add.Tag = "Add"
        Me.tlb_Add.Text = "&Add"
        Me.tlb_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Add.ToolTipText = "Add"
        '
        'tlb_Remove
        '
        Me.tlb_Remove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Remove.Image = CType(resources.GetObject("tlb_Remove.Image"), System.Drawing.Image)
        Me.tlb_Remove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Remove.Name = "tlb_Remove"
        Me.tlb_Remove.Size = New System.Drawing.Size(60, 50)
        Me.tlb_Remove.Tag = "Remove"
        Me.tlb_Remove.Text = "&Remove"
        Me.tlb_Remove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlb_Remove.ToolTipText = "Remove"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.txtDocumentName)
        Me.pnlMain.Controls.Add(Me.label1)
        Me.pnlMain.Controls.Add(Me.lvDocuments)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(477, 196)
        Me.pnlMain.TabIndex = 1
        '
        'txtDocumentName
        '
        Me.txtDocumentName.Location = New System.Drawing.Point(132, 164)
        Me.txtDocumentName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDocumentName.MaxLength = 50
        Me.txtDocumentName.Name = "txtDocumentName"
        Me.txtDocumentName.Size = New System.Drawing.Size(331, 22)
        Me.txtDocumentName.TabIndex = 4
        '
        'label1
        '
        Me.label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label1.AutoEllipsis = True
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(13, 169)
        Me.label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(115, 14)
        Me.label1.TabIndex = 18
        Me.label1.Text = "Document Name :"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvDocuments
        '
        Me.lvDocuments.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colDocument, Me.colFormat, Me.colPath, Me.colAdded})
        Me.lvDocuments.FullRowSelect = True
        Me.lvDocuments.HideSelection = False
        Me.lvDocuments.Location = New System.Drawing.Point(13, 11)
        Me.lvDocuments.Margin = New System.Windows.Forms.Padding(2)
        Me.lvDocuments.MultiSelect = False
        Me.lvDocuments.Name = "lvDocuments"
        Me.lvDocuments.Size = New System.Drawing.Size(450, 145)
        Me.lvDocuments.TabIndex = 3
        Me.lvDocuments.UseCompatibleStateImageBehavior = False
        Me.lvDocuments.View = System.Windows.Forms.View.Details
        '
        'colDocument
        '
        Me.colDocument.Text = "Document"
        Me.colDocument.Width = 300
        '
        'colFormat
        '
        Me.colFormat.Text = "Format"
        Me.colFormat.Width = 125
        '
        'colPath
        '
        Me.colPath.Text = "Path"
        Me.colPath.Width = 1
        '
        'colAdded
        '
        Me.colAdded.Text = "isAdded"
        Me.colAdded.Width = 0
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'frmImportDICOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(477, 249)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportDICOM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import DICOM"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tls_MaintainDoc.ResumeLayout(False)
        Me.tls_MaintainDoc.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Private WithEvents tls_MaintainDoc As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents tlb_Add As System.Windows.Forms.ToolStripButton
    Private WithEvents tlb_Remove As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents lvDocuments As System.Windows.Forms.ListView
    Private WithEvents txtDocumentName As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents colDocument As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFormat As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAdded As System.Windows.Forms.ColumnHeader
End Class
