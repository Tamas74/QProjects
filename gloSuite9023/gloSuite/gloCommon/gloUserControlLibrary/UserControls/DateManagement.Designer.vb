<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DateManagement
    Inherits System.Windows.Forms.UserControl
    ''Public Shared trvCateogory As System.Windows.Forms.TreeView
    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpControls As System.Windows.Forms.ContextMenu() = {cmnuTasks}
                
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeContextMenu(dtpControls)
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DateManagement))
        Me.trvCateogory = New System.Windows.Forms.TreeView
        Me.ilstPrvPrescription = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.cmnuTasks = New System.Windows.Forms.ContextMenu
        Me.cmnuTask_Delete = New System.Windows.Forms.MenuItem
        Me.cmnuTask_Complete = New System.Windows.Forms.MenuItem
        Me.cmnuTask_Add = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'trvCateogory
        '
        Me.trvCateogory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCateogory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCateogory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCateogory.ForeColor = System.Drawing.Color.Black
        Me.trvCateogory.HideSelection = False
        Me.trvCateogory.ImageIndex = 0
        Me.trvCateogory.ImageList = Me.ilstPrvPrescription
        Me.trvCateogory.Indent = 20
        Me.trvCateogory.ItemHeight = 20
        Me.trvCateogory.Location = New System.Drawing.Point(0, 0)
        Me.trvCateogory.Name = "trvCateogory"
        Me.trvCateogory.SelectedImageIndex = 0
        Me.trvCateogory.ShowNodeToolTips = True
        Me.trvCateogory.Size = New System.Drawing.Size(297, 150)
        Me.trvCateogory.TabIndex = 0
        '
        'ilstPrvPrescription
        '
        Me.ilstPrvPrescription.ImageStream = CType(resources.GetObject("ilstPrvPrescription.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilstPrvPrescription.TransparentColor = System.Drawing.Color.Transparent
        Me.ilstPrvPrescription.Images.SetKeyName(0, "Bullet06.ico")
        Me.ilstPrvPrescription.Images.SetKeyName(1, "")
        Me.ilstPrvPrescription.Images.SetKeyName(2, "")
        Me.ilstPrvPrescription.Images.SetKeyName(3, "")
        Me.ilstPrvPrescription.Images.SetKeyName(4, "")
        Me.ilstPrvPrescription.Images.SetKeyName(5, "")
        Me.ilstPrvPrescription.Images.SetKeyName(6, "")
        Me.ilstPrvPrescription.Images.SetKeyName(7, "")
        Me.ilstPrvPrescription.Images.SetKeyName(8, "")
        Me.ilstPrvPrescription.Images.SetKeyName(9, "")
        Me.ilstPrvPrescription.Images.SetKeyName(10, "Red-arrow_01.gif")
        Me.ilstPrvPrescription.Images.SetKeyName(11, "Green-Arrow_01.gif")
        Me.ilstPrvPrescription.Images.SetKeyName(12, "blue-arrow_01.gif")
        Me.ilstPrvPrescription.Images.SetKeyName(13, "Orange-arrow_02.gif")
        Me.ilstPrvPrescription.Images.SetKeyName(14, "SpeechMicHS.png")
        Me.ilstPrvPrescription.Images.SetKeyName(15, "Health paln (inbetween).ico")
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Task&Messages01.ico")
        Me.ImageList1.Images.SetKeyName(1, "Task&Messages.ico")
        Me.ImageList1.Images.SetKeyName(2, "arrow_01.ico")
        '
        'cmnuTasks
        '
        Me.cmnuTasks.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmnuTask_Delete, Me.cmnuTask_Complete, Me.cmnuTask_Add, Me.MenuItem1, Me.MenuItem2})
        '
        'cmnuTask_Delete
        '
        Me.cmnuTask_Delete.Index = 0
        Me.cmnuTask_Delete.Text = "Delete Task"
        Me.cmnuTask_Delete.Visible = False
        '
        'cmnuTask_Complete
        '
        Me.cmnuTask_Complete.Index = 1
        Me.cmnuTask_Complete.Text = "Complete Task"
        '
        'cmnuTask_Add
        '
        Me.cmnuTask_Add.Index = 2
        Me.cmnuTask_Add.Text = "Add Task"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 3
        Me.MenuItem1.Text = "New Message"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 4
        Me.MenuItem2.Text = "Message History"
        '
        'DateManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.trvCateogory)
        Me.Name = "DateManagement"
        Me.Size = New System.Drawing.Size(297, 150)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmnuTasks As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuTask_Delete As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuTask_Complete As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuTask_Add As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents trvCateogory As System.Windows.Forms.TreeView
    Friend WithEvents ilstPrvPrescription As System.Windows.Forms.ImageList

End Class
