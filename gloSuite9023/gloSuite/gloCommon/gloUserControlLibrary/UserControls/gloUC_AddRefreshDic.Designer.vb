<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_AddRefreshDic
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()

                'SLR: there is an assignment directly referenced from gloUC_PatientStrip1.DTP in frmLMOrders.Vb without allocation and hence can not be disposed. But this is a dangerous implementation eating all memories.
                If (dtAllocated) Then
                    Dim dtpControls As System.Windows.Forms.DateTimePicker() = {dtLetterdate}
                    Dim cntControls As System.Windows.Forms.Control() = {dtLetterdate}


                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                        gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                    Catch

                    End Try
                End If

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_AddRefreshDic))
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnAddFields = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRefresh.Location = New System.Drawing.Point(28, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(20, 20)
        Me.btnRefresh.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnAddFields
        '
        Me.btnAddFields.BackgroundImage = CType(resources.GetObject("btnAddFields.BackgroundImage"), System.Drawing.Image)
        Me.btnAddFields.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddFields.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddFields.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnAddFields.FlatAppearance.BorderSize = 0
        Me.btnAddFields.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAddFields.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAddFields.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddFields.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddFields.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAddFields.Location = New System.Drawing.Point(0, 0)
        Me.btnAddFields.Name = "btnAddFields"
        Me.btnAddFields.Size = New System.Drawing.Size(20, 20)
        Me.btnAddFields.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.btnAddFields, "Add Fields")
        Me.btnAddFields.UseVisualStyleBackColor = True
        '
        'gloUC_AddRefreshDic
        '
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnAddFields)
        Me.Name = "gloUC_AddRefreshDic"
        Me.Size = New System.Drawing.Size(48, 20)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnAddFields As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
